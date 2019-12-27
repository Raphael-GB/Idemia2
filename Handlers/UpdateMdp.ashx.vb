Imports System.Web
Imports System.Web.Services

Public Class UpdateMdp
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            If (Not (context.Request.Params("txtNouveauMdp") Is Nothing Or context.Request.Params("txtConfirmMdp") Is Nothing)) Then

                Dim mdp As String = context.Request.Params("txtNouveauMdp").ToString()
                Dim confirm As String = context.Request.Params("txtConfirmMdp").ToString()

                If (mdp.Trim() <> "" And confirm.Trim() <> "" And (mdp = confirm)) Then

                    DBFunctions.UpdateMdpId(user.Identifiant, mdp)

                    context.Response.Redirect(context.Request.UrlReferrer.ToString())

                Else

                    context.Response.Redirect(context.Request.UrlReferrer.ToString())

                End If

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