Imports System.Web
Imports System.Web.Services

Public Class BasculeStatut
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            If (Not (context.Request.Params("bascule-docid") Is Nothing Or context.Request.Params("bascule-statut") Is Nothing)) Then

                Dim docid As String = context.Request.Params("bascule-docid").ToString()
                Dim statut As String = context.Request.Params("bascule-statut").ToString()

                If (docid.Trim() <> "" And statut.Trim() <> "") Then

                    DBFunctions.basculeStatut(docid, current_user, statut)

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