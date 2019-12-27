Imports System.Web
Imports System.Web.Services

Public Class GestionRejet
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            If (Not context.Request.Params("modal-rejet-titre") Is Nothing) Then
                Dim bloquant As String = ""
                Dim rejet As String = context.Request.Params("modal-rejet-titre").ToString()
                'context.Request.Params("modal-rejet-type").ToString()

                If (Not context.Request.Params("modal-rejet-type") Is Nothing) Then
                    bloquant = "O"
                Else
                    bloquant = "N"
                End If


                If (rejet.Trim() <> "") Then

                    DBFunctions.InsertRejet(current_user, rejet, bloquant)

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