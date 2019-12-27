Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class JsonFournisseur
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            Dim valeur As String = context.Request.QueryString("valeur")

            Dim serializer As New JavaScriptSerializer()

            Dim serializedResult As String = serializer.Serialize(DBFunctions.getFournisseurs(user, valeur, 20))

            context.Response.Write(serializedResult)

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