Imports Oracle.DataAccess.Client
Imports System.Configuration

Public Class Connexion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        DBFunctions.ChaineConnexion = ConfigurationManager.AppSettings("chaine_connexion").ToString()
        MailFunctions.ServeurMail = ConfigurationManager.AppSettings("serveur_mail").ToString()

    End Sub

    Private Sub btnConnexion_Click(sender As Object, e As EventArgs) Handles btnConnexion.Click

        Dim user As Utilisateur

        pnlInfo.Visible = False

        user = DBFunctions.connexion(txtLogin.Text, txtMdp.Text)

        If (Not user Is Nothing) Then

            DBFunctions.connexion_historique(user, "C")

            Session("Utilisateur") = user
            Session("filtre_statut_facture_a_traiter") = ""
            Session("filtre_statut_facture_archive") = ""
            Session("filtre_requete") = ""
            Session("page_precedente") = ""
            Session("statut_validation") = ""

            If (user.Workflow = "65") Then
                Response.Redirect("FDJ_Performances.aspx")
            Else
                Response.Redirect("Indicateurs.aspx")
            End If



        Else

            pnlInfo.Visible = True

        End If

    End Sub

End Class