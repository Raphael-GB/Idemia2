Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class JsonEvenement
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            Dim serializer As New JavaScriptSerializer()

            Dim serializedResult As String = serializer.Serialize(DBFunctions.getEvenements(user, ""))

            serializedResult = serializedResult.Replace("""IDEvenement""", """id""") _
            .Replace("""DateDebut"":", """start"":") _
            .Replace("""DateFin"":", """end"":") _
            .Replace("""Titre"":", """title"":") _
            .Replace("""Description"":", """description"":")

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