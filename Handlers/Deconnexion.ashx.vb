Imports System.Web
Imports System.Web.Services
Imports System.Web.Security

Public Class Deconnexion
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        If (Not context.Session("Utilisateur") Is Nothing) Then
            DBFunctions.connexion_historique(context.Session("Utilisateur"), "D")
        End If

        context.Session.Clear()
        context.Session.Abandon()

        context.Response.Redirect("../Connexion.aspx")

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class