Imports System.Web
Imports System.Web.Services

Public Class SuppressionFiltre
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)
            Dim filtre_temp As Filtre

            If (Not context.Request.Params("mId") Is Nothing) Then

                filtre_temp = New Filtre()
                filtre_temp.IDFiltre = context.Request.Params("mId")
                filtre_temp.Workflow = user.Workflow

                DBFunctions.DeleteFiltre(filtre_temp)

                context.Response.Redirect("../ParametrageFiltre.aspx")

            Else

                context.Response.Redirect("../ParametrageFiltre.aspx")

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