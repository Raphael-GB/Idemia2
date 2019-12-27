Imports System.Web
Imports System.Web.Services

Public Class FlagOccupation
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            If (Not (context.Request.QueryString("docid") Is Nothing Or context.Request.QueryString("valeur") Is Nothing)) Then

                Dim docid As String = context.Request.QueryString("docid").ToString()
                Dim valeur As String = context.Request.QueryString("valeur").ToString()

                If (docid.Trim() <> "") Then

                    DBFunctions.setFlagOccupe(docid, valeur, current_user)

                    context.Response.Write("")

                Else

                    context.Response.Write("")

                End If

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