Imports System.Web
Imports System.Web.Services

Public Class VerificationIdentifiant
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Request.Params("valeur") Is Nothing) Then

            Dim valeur As String = context.Request.Params("valeur")

            If (DBFunctions.IdentifiantExistant(valeur)) Then
                context.Response.Write("1")
            Else
                context.Response.Write("0")
            End If

        End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class