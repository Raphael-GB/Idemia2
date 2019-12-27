Imports System.Web
Imports System.Web.Services
Imports System.IO
Imports System.Web.Script.Serialization

Public Class EnregistrerLignes
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim response As String = "0"
            Dim erreur As Boolean = False

            Try

                context.Response.ContentType = "application/json"

                Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

                Dim sr As New StreamReader(context.Request.InputStream)

                Dim stream As String = sr.ReadToEnd()

                Dim serializer As New JavaScriptSerializer()

                Dim resultats As List(Of ValeurLigneFacture) = serializer.Deserialize(Of List(Of ValeurLigneFacture))(stream)

                Dim liste_id_ligne As List(Of String) = (From res In resultats
                                                        Select res.IdLigne Distinct).ToList()

                Dim id_facture As String = (From res In resultats
                                                Select res.IdFacture Distinct).ToList()(0)

                Dim liste_champs As List(Of Champ) = DBFunctions.getChamps(current_user, "", "1")

                For Each id_ligne As String In liste_id_ligne

                    Dim liste_champs_valorises As New List(Of Champ)

                    For Each vlf As ValeurLigneFacture In resultats.FindAll(Function(x) x.IdLigne = id_ligne)

                        Dim champ_temp As Champ = liste_champs.Find(Function(x) x.IDChamp = vlf.IdChamp)

                        If (Not champ_temp Is Nothing) Then

                            champ_temp.Valeur = vlf.Valeur

                            liste_champs_valorises.Add(champ_temp)

                        End If

                    Next

                    If (Not DBFunctions.UpdateLigneFacture(current_user, id_facture, id_ligne, liste_champs_valorises)) Then
                        erreur = True
                    End If

                Next

                If (erreur) Then
                    response = "0"
                Else
                    response = "1"
                End If

                'historisation de la modification de ligne

                Dim liste_hiso As New List(Of Historique)

                liste_hiso.Add(New Historique(current_user.Workflow, id_facture, "MODIFICATION LIGNE FACTURE", "", "", "", current_user.Identifiant, "", "", "", ""))
                liste_hiso.Add(New Historique(current_user.Workflow, id_facture, "ENRICHISSEMENT", "", "", "", current_user.Identifiant, "", "", "", ""))

                DBFunctions.InsertHistorique(liste_hiso)

            Catch ex As Exception
                response = "0"
            End Try

            context.Response.Write(response)

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