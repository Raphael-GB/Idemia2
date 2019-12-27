Imports System.Web
Imports System.Web.Services

Public Class SuppressionAlerteAuto
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)
            Dim alerte_auto_temp As AlerteAutomatique

            If (Not context.Request.Params("mId") Is Nothing) Then

                alerte_auto_temp = New AlerteAutomatique()
                alerte_auto_temp.IDAlerteAuto = context.Request.Params("mId")
                alerte_auto_temp.Workflow = user.Workflow

                DBFunctions.DeleteAlerteAutomatique(alerte_auto_temp)

                context.Response.Redirect("../ParametrageAlerteAuto.aspx")

            Else

                context.Response.Redirect("../ParametrageAlerteAuto.aspx")

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