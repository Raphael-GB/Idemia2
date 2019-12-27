Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class JsonHistorique
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim response As String = ""

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim serializer As New JavaScriptSerializer()

            Try

                Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

                Dim docid = context.Request.Params("id").ToString()

                Dim liste_histo As List(Of Historique) = DBFunctions.getHistorique(current_user, docid)

                response = serializer.Serialize(liste_histo)

                context.Response.Write(response)

            Catch ex As Exception

                response = serializer.Serialize(New List(Of Historique))

            End Try

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