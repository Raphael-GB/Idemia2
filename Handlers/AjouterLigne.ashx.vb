Imports System.Web
Imports System.Web.Services

Public Class AjouterLigne
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim response As String = ""

            Try

                Dim id_facture As String = context.Request.Params("id").ToString()

                Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

                response = DBFunctions.InsertLigneFacture(current_user, id_facture)

            Catch ex As Exception

                response = ""

            End Try

            context.Response.Write(response)

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