Imports System.Web
Imports System.Web.Services
Imports System.IO

Public Class ExtractionATraiter
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            If (Not ((context.Request.Params("extIdStatutFacture") Is Nothing) Or (context.Request.Params("extIdFiltre") Is Nothing))) Then

                Dim id_statut_facture As String = context.Request.Params("extIdStatutFacture").ToString()

                Dim id_filtre As String = context.Request.Params("extIdFiltre").ToString()

                Dim timestramp As String = Date.Now.ToString("ddMMyyyy_HHmmss")

                Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

                Dim liste_statuts As New List(Of String)

                If (id_statut_facture <> "") Then

                    liste_statuts.Add(id_statut_facture)

                Else

                    For Each item As ListItem In DBFunctions.getStatutsFactures("ACTION")
                        liste_statuts.Add(item.Value)
                    Next

                End If


                Dim liste_factures As List(Of Facture) = DBFunctions.getFactures(current_user, id_filtre, liste_statuts, True, False, False, "", 0, "", "099PU").ToList()
                Dim liste_alertes_facture As List(Of AlerteFacture) = DBFunctions.getAlertesFacture("", current_user, False, "")

                Dim ligne As String

                Dim nom_fichier As String = "EXTRACTION_COMMANDE_" & timestramp & ".csv"

                Dim ms As New MemoryStream()

                Using sw As New StreamWriter(ms, Encoding.UTF8)

                    Dim entete As String = "ID FACTURE;CODE FOURNISSEUR;NOM FOURNISSEUR;NUM FACTURE;DATE FACTURE;ENTITE FACTURE;MONTANT TTC;DATE INSERTION;FLAG ALERTE;MOTIF ALERTE;EMETTEUR ALERTE;SITE;MOTIF REJET"

                    sw.WriteLine(entete)

                    For Each fact As Facture In liste_factures

                        Dim alertes_facture As List(Of AlerteFacture) = liste_alertes_facture.FindAll(Function(x) x.DocId = fact.IDFacture And x.Workflow = current_user.Workflow)

                        Dim flag_alerte As String = ""
                        Dim motif_alerte As String = ""
                        Dim emetteur_alerte As String = ""

                        If (alertes_facture.Count > 0) Then

                            'on prend en priorité l'alerte non résolue la plus ancienne.
                            Dim sortByStatut As New AlerteFacture_SortByStatut()
                            Dim sortByDate As New AlerteFacture_SortByDate()

                            alertes_facture.Sort(sortByStatut)
                            alertes_facture.Sort(sortByDate)

                            If (alertes_facture.Item(0).Statut.Id = "1") Then
                                flag_alerte = "En cours"
                            ElseIf (alertes_facture.Item(0).Statut.Id = "2") Then
                                flag_alerte = "Résolu"
                            End If

                            motif_alerte = alertes_facture.Item(0).Motif.Libelle
                            emetteur_alerte = alertes_facture.Item(0).Emetteur.Prenom & " " & alertes_facture.Item(0).Emetteur.Nom

                        End If

                        ligne = fact.IDFacture.Replace(";", " ") & ";" & fact.CodeFournisseur.Replace(";", " ") & ";" & fact.NomFournisseur.Replace(";", " ") & ";" & fact.NumFacture.Replace(";", " ") & ";" & fact.DateFacture.Replace(";", " ") & ";" & fact.EntiteFacture.Replace(";", " ") & ";" & fact.MontantTTC.Replace(";", " ") & ";" & fact.DateInsertion.Replace(";", " ") & ";" & flag_alerte.Replace(";", " ") & ";" & motif_alerte.Replace(";", " ") & ";" & emetteur_alerte.Replace(";", " ") & ";" & fact.Site.Replace(";", " ") & ";" & fact.Rejet.Libelle.Replace(";", " ")
                        ligne = ligne.Replace(vbNewLine, " ")


                        sw.WriteLine(ligne)

                    Next

                    sw.Flush()

                End Using

                Dim fileBytes() As Byte = ms.ToArray()

                context.Response.Clear()
                context.Response.AddHeader("Content-Length", fileBytes.Length)
                context.Response.ContentType = "application/vnd.ms-excel"
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" & nom_fichier)
                context.Response.BinaryWrite(fileBytes)
                context.Response.Flush()
                context.Response.Close()
                context.Response.End()

            Else

                context.Response.Redirect(context.Request.UrlReferrer.ToString())

            End If

        Else

            context.Response.Redirect("../Connexion.aspx")

        End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class