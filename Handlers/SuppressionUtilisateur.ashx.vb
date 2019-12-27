Imports System.Web
Imports System.Web.Services

Public Class SuppressionUtilisateur
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

        If (current_user Is Nothing) Then
            context.Response.Redirect("../Connexion.aspx")
        End If

        Dim utilisateur_temp As New Utilisateur()

        If (current_user.IDProfil = "1") Then

            If (Not context.Request.Params("mId") Is Nothing) Then

                utilisateur_temp.Workflow = current_user.Workflow
                utilisateur_temp.Identifiant = context.Request.Params("mId")

                DBFunctions.DeleteUtilisateur(utilisateur_temp)

            End If

        End If

        context.Response.Redirect("../ParametrageUtilisateur.aspx")


    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class