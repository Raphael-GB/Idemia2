Imports System.IO
Imports System.Web
Imports System.Web.Services

Public Class ExtractionHSArchive
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

                Dim statut_recherche As New List(Of String)

                statut_recherche.Add("7")

                Dim liste_documentHorsScope As List(Of DocumentHorsScope) = DBFunctions.getListDocumentHorsScope(current_user, id_filtre, statut_recherche).ToList

                Dim ligne As String

                Dim nom_fichier As String = "EXTRACTION_DOCUMENTS_HORS_SCOPE_ARCHIVE" & timestramp & ".csv"

                Dim ms As New MemoryStream()

                Using sw As New StreamWriter(ms, Encoding.UTF8)

                    Dim entete As String = "ID DOCUMENT;TYPE DE DOCUMENT;ENTITE;DATE INSERTION"

                    sw.WriteLine(entete)

                    For Each document As DocumentHorsScope In liste_documentHorsScope

                        ligne = document.Id.Replace(";", " ") & ";" & document.Type.Replace(";", " ") & ";" & document.Site.Replace(";", " ") & ";" & document.DateInsertion.Replace(";", " ")
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