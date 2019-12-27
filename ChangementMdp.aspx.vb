Public Class ChangementMdp
    Inherits System.Web.UI.Page

    Dim id_controle As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Request.QueryString("id") Is Nothing) Then

            id_controle = Request.QueryString("id").ToString()

        Else

            Response.Redirect("Connexion.aspx")

        End If

    End Sub

    Private Sub btnValider_Click(sender As Object, e As EventArgs) Handles btnValider.Click

        If (id_controle.Trim() <> "") Then

            Dim message_erreur As String = ""

            If (txtNouveauMdp.Text.Trim() = "") Then

                message_erreur = "Veuillez saisir un mot de passe"

            ElseIf (txtNouveauMdp.Text <> txtConfirmMdp.Text) Then

                message_erreur = "Le mot de passe et sa confirmation ne correspondent pas"

            ElseIf (Not DBFunctions.UpdateMdp(id_controle, txtNouveauMdp.Text)) Then

                message_erreur = "Erreur de réinitialisation de mot de passe"

            End If

            If (message_erreur <> "") Then

                litErreur.Text = "<div class=""alert alert-danger "">" & message_erreur & "</div>"

            Else

                Response.Redirect("Connexion.aspx")

            End If


        Else

            Response.Redirect("Connexion.aspx")

        End If

    End Sub

End Class