Imports System.Web
Imports System.Web.Services

Public Class VerificationOccupation
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            If (Not context.Request.QueryString("docid") Is Nothing) Then

                Dim docid As String = context.Request.QueryString("docid").ToString()

                context.Response.Write(DBFunctions.getFlagOccupe(docid, current_user))

            Else

                context.Response.Write("1")

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