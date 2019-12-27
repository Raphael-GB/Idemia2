Imports System.Web
Imports System.Web.Services
Imports System.IO

Public Class VisualisationFichierAjoute
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            If (Not context.Request.Params("id") Is Nothing) Then

                Dim id As String = context.Request.Params("id").ToString()
                Dim url_fichier As String = DBFunctions.getUrlFichierAjoute(user, id)

                If (url_fichier.Trim() <> "") Then

                    Dim fi As New FileInfo(url_fichier)

                    context.Response.ContentType = "application/pdf"
                    context.Response.AppendHeader("Content-Disposition", "inline; filename=" & fi.Name & ".pdf")
                    context.Response.WriteFile(url_fichier)
                    context.Response.End()

                Else

                    context.Response.Write("Fichier non disponible")

                End If

            Else

                context.Response.Write("Fichier non disponible")

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