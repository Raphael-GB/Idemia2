Imports System.Web
Imports System.Web.Services

Public Class SuppressionFichierAjoute
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)
            Dim fichier_temp As FichierAjoute

            If (Not context.Request.Params("mId") Is Nothing) Then

                fichier_temp = New FichierAjoute()
                fichier_temp.IDAjout = context.Request.Params("mId")
                fichier_temp.Workflow = user.Workflow

                DBFunctions.DeleteFichierAjoute(fichier_temp)

                context.Response.Redirect(context.Request.UrlReferrer.ToString())

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