Imports System.Web
Imports System.Web.Services

Public Class GestionEvenement
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            If (Not context.Request.Params("modal-evenement-id") Is Nothing And _
                Not context.Request.Params("modal-evenement-titre") Is Nothing And _
                Not context.Request.Params("modal-evenement-description") Is Nothing And _
                Not context.Request.Params("modal-evenement-date-debut") Is Nothing And _
                Not context.Request.Params("modal-evenement-date-fin") Is Nothing And _
                Not context.Request.Params("modal-evenement-suppression") Is Nothing) Then

                Dim id As String = context.Request.Params("modal-evenement-id").ToString()
                Dim titre As String = context.Request.Params("modal-evenement-titre").ToString()
                Dim description As String = context.Request.Params("modal-evenement-description").ToString()
                Dim dateDebut As String = context.Request.Params("modal-evenement-date-debut").ToString()
                Dim dateFin As String = context.Request.Params("modal-evenement-date-fin").ToString()
                Dim flag_suppression As String = context.Request.Params("modal-evenement-suppression").ToString()

                Dim evenement_temp As New EvenementCalendrier(user.Workflow, id, user.Identifiant, titre, dateDebut, dateFin, description)

                If (id.Trim() = "") Then

                    DBFunctions.InsertEvenement(evenement_temp)

                ElseIf (flag_suppression.Trim() = "1") Then

                    DBFunctions.DeleteEvenement(evenement_temp)

                Else

                    DBFunctions.UpdateEvenement(evenement_temp)

                End If

                context.Response.Redirect("../Evenement.aspx")

            Else

                context.Response.Redirect("../Evenement.aspx")

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