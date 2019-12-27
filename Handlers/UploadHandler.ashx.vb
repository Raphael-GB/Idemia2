Imports System.Web
Imports System.Web.Services
Imports System.IO
Imports System.Web.Script.Serialization

Public Class UploadHandler
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)
            Dim docid As String = context.Request.QueryString("docid").ToString()

            If (context.Request.Files.Count > 0 And docid <> "") Then

                Dim repertoire_depot As String = context.Server.MapPath("~/Fichiers/" & user.Workflow & "/" & docid & "/")
                If (Not Directory.Exists(repertoire_depot)) Then
                    Directory.CreateDirectory(repertoire_depot)
                End If

                For i As Integer = 0 To context.Request.Files.Count - 1

                    Dim timestamp As String = Date.Now().ToString("ddMMyyyy_HHmmss")
                    Dim nom_fichier_destination As String = timestamp & "_" & context.Request.Files(i).FileName
                    Dim nom_fichier_origine As String = context.Request.Files(i).FileName

                    Dim fichier_destination As String = Path.Combine(repertoire_depot, nom_fichier_destination)
                    context.Request.Files(i).SaveAs(fichier_destination)

                    Dim fichier As New FichierAjoute()
                    fichier.Workflow = user.Workflow
                    fichier.Identifiant = user.Identifiant
                    fichier.DOCID = docid
                    fichier.NomFichier = nom_fichier_origine
                    fichier.Url = fichier_destination

                    If (File.Exists(fichier_destination)) Then
                        DBFunctions.InsertFichierAjoute(fichier)
                    End If

                Next

                context.Response.ContentType = "application/json"
                context.Response.Write("{}")

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