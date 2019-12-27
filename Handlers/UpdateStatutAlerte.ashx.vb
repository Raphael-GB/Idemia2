Imports System.Web
Imports System.Web.Services

Public Class UpdateStatutAlerte
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            If (Not context.Request.QueryString("id") Is Nothing) Then

                Dim id_alerte As String = context.Request.QueryString("id").ToString()
                Dim hs As Boolean = False
                If (Not context.Request.QueryString("hs") Is Nothing) Then
                    hs = True
                End If

                If (id_alerte.Trim() <> "") Then

                    DBFunctions.UpdateStatutAlerteFacture(current_user, id_alerte, hs)

                End If

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