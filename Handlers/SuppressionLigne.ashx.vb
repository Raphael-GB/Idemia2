Imports System.Web
Imports System.Web.Services

Public Class SuppressionLigne
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim response As String = "0"

            Try

                Dim id_ligne As String = context.Request.Params("idligne").ToString()
                Dim id_facture As String = context.Request.Params("idfacture").ToString()

                Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

                If (DBFunctions.DeleteLigneFacture(current_user, id_facture, id_ligne)) Then

                    response = "1"

                End If


            Catch ex As Exception

                response = "0"

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