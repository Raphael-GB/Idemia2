Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class JsonSuggessionEmail
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            If (Not context.Request.QueryString("param") Is Nothing) Then

                Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

                Dim param As String = context.Request.QueryString("param").ToString()

                Dim serializer As New JavaScriptSerializer()

                Dim serializedResult As String = serializer.Serialize(DBFunctions.getSugessionEmail(user, param))

                context.Response.Write(serializedResult)

            Else

                context.Response.Write("")

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