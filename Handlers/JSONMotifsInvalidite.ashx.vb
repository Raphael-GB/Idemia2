Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class JsonMotifsInvalidite
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)
            Dim serializer As New JavaScriptSerializer()
            Dim liste_motifs As List(Of Motif) = DBFunctions.getMotifsInvalidite(current_user)
            Dim serializedResult As String = serializer.Serialize(liste_motifs)

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