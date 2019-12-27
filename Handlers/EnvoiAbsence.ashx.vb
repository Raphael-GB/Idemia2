Imports System.Web
Imports System.Web.Services

Public Class EnvoiAbsence
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            If (Not context.Request.Params("txtDestinataires") Is Nothing And
                Not context.Request.Params("modal-absence-date-debut") Is Nothing And
                Not context.Request.Params("modal-absence-date-fin") Is Nothing) Then

                Dim expediteur As String = context.Request.Params("txtUtilisateur").ToString()
                Dim destinataires As String = context.Request.Params("txtDestinataires").ToString()
                Dim debut As String = DateTime.Parse(context.Request.Params("modal-absence-date-debut").ToString()).ToShortDateString()
                Dim fin As String = DateTime.Parse(context.Request.Params("modal-absence-date-fin").ToString()).ToShortDateString()


                DBFunctions.EnvoyerAbsence(user, expediteur, destinataires, debut, fin)

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