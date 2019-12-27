Imports System.Web
Imports System.Web.Services

Public Class EnvoiMessage
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            If (Not context.Request.Params("txtDestinataires") Is Nothing And _
                Not context.Request.Params("txtSujet") Is Nothing And _
                Not context.Request.Params("hiddenTxtMessage") Is Nothing) Then

                Dim destinataires As String = context.Request.Params("txtDestinataires").ToString()
                Dim sujet As String = context.Request.Params("txtSujet").ToString()
                Dim message As String = context.Request.Params("hiddenTxtMessage").ToString()

                DBFunctions.EnvoyerMessage(user, destinataires, sujet, message)

            End If

        Else

            context.Response.Redirect("../Connexion.aspx")

        End If


        context.Response.Redirect(context.Request.UrlReferrer.ToString())

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class