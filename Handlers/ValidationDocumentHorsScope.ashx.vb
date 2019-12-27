Imports System.Web
Imports System.Web.Services

Public Class ValidationDocumentHorsScope
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)
            Dim docid As String

            If (Not context.Request.Params("mdocid") Is Nothing) Then

                docid = context.Request.Params("mdocid")

                If (docid <> "") Then
                    If (DBFunctions.UpdateDocumentHorsScope(user, docid)) Then

                    End If

                End If

            End If

            context.Response.Redirect(context.Request.UrlReferrer.ToString())

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