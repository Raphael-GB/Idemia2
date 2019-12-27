Imports System.Web
Imports System.Web.Services

Public Class SuppressionMessage
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)
            Dim id_message As String

            If (Not context.Request.Params("mId") Is Nothing) Then

                id_message = context.Request.Params("mId")
                DBFunctions.DeleteMessage(id_message, user.Identifiant)

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