Public Class MonCompte
    Inherits System.Web.UI.Page

    Dim current_user As Utilisateur

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Session("utilisateur") Is Nothing) Then

            current_user = CType(Session("utilisateur"), Utilisateur)

            If (Not Page.IsPostBack()) Then

                'accessibilité
                txtIdentifiant.Text = current_user.Identifiant

                'informations générales
                ddlCivilite.DataTextField = "Text"
                ddlCivilite.DataValueField = "Value"
                ddlCivilite.DataSource = DBFunctions.getCivilites()
                ddlCivilite.DataBind()

                ddlCivilite.SelectedValue = current_user.Civilite.Id
                txtNom.Text = current_user.Nom
                txtPrenom.Text = current_user.Prenom
                txtEmail.Text = current_user.Email
                txtFonction.Text = current_user.Fonction

                'champs correction
                lbChampsCorrection.DataTextField = "Description"
                lbChampsCorrection.DataValueField = "IDChamp"
                lbChampsCorrection.DataSource = DBFunctions.getChampsACorriger(current_user)
                lbChampsCorrection.DataBind()

                'champs enrichissement
                lbChampsEnrichissement.DataTextField = "Description"
                lbChampsEnrichissement.DataValueField = "Valeur"
                lbChampsEnrichissement.DataSource = DBFunctions.getChampsAEnrichir(current_user)
                lbChampsEnrichissement.DataBind()

                'alertes automatiques
                lbAlertesAutomatiques.DataTextField = "Libelle"
                lbAlertesAutomatiques.DataValueField = "IDAlerteAuto"
                lbAlertesAutomatiques.DataSource = DBFunctions.getAlertesAutomatiques(current_user, True)
                lbAlertesAutomatiques.DataBind()

                'filtres selection
                lbFiltresSelection.DataTextField = "Libelle"
                lbFiltresSelection.DataValueField = "IDFiltre"
                lbFiltresSelection.DataSource = DBFunctions.getFiltres(current_user, True)
                lbFiltresSelection.DataBind()

                'motifs demande
                ddlMotifsDemande.DataTextField = "Text"
                ddlMotifsDemande.DataValueField = "Value"
                ddlMotifsDemande.DataSource = DBFunctions.getMotifsDemande()
                ddlMotifsDemande.DataBind()

            End If

        Else

            Response.Redirect("Connexion.aspx")

        End If

    End Sub

    Private Sub btnModifierInfos_Click(sender As Object, e As EventArgs) Handles btnModifierInfos.Click

        Dim civilite, nom, prenom, fonction, email As String
        Dim flag_erreur As Boolean = False

        civilite = ddlCivilite.SelectedValue()
        nom = txtNom.Text
        prenom = txtPrenom.Text
        fonction = txtFonction.Text
        email = txtEmail.Text

        If (civilite = "") Then
            flag_erreur = True
            ddlCivilite.BackColor = Drawing.Color.LightPink
        Else
            ddlCivilite.BackColor = Drawing.Color.White
        End If

        If (nom = "") Then
            flag_erreur = True
            txtNom.BackColor = Drawing.Color.LightPink
        Else
            txtNom.BackColor = Drawing.Color.White
        End If

        If (prenom = "") Then
            flag_erreur = True
            txtPrenom.BackColor = Drawing.Color.LightPink
        Else
            txtPrenom.BackColor = Drawing.Color.White
        End If

        Dim emailExpression As Regex = New Regex("^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$")

        If (email = "" Or Not emailExpression.IsMatch(email)) Then
            flag_erreur = True
            txtEmail.BackColor = Drawing.Color.LightPink
        Else
            txtEmail.BackColor = Drawing.Color.White
        End If

        If (Not flag_erreur) Then

            'Mise à jour des infos générales
            DBFunctions.UpdateInfosGenerales(current_user, civilite, nom, prenom, email, fonction)
            Response.Redirect("MonCompte.aspx")

        End If


    End Sub

    Private Sub btnEnvoyerDemande_Click(sender As Object, e As EventArgs) Handles btnEnvoyerDemande.Click

        Dim motif, commentaire As String

        motif = ddlMotifsDemande.SelectedValue
        commentaire = txtCommentaireDemande.InnerText

        If (motif.Trim() <> "") Then

            'Dim demande_temp As Demande = New Demande()
            'demande_temp.Workflow = current_user.Workflow

            'DBFunctions.InsertDemandeAdministrateur(demande_temp)


        End If

    End Sub

End Class