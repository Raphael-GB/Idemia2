Imports System.Web
Imports System.Web.Services

Public Class CalculCollectif
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim reponse As String = ""

            Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            If (Not context.Request.Params("comptecomptable") Is Nothing) Then

                Dim compte_comptable As String = context.Request.Params("comptecomptable").ToString()

                reponse = DBFunctions.getCollectif(current_user, compte_comptable)

            End If

            context.Response.Write(reponse)

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