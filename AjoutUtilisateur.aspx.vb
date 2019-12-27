Public Class AjoutUtilisateur
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not IsPostBack) Then
            ViewState("UrlReferent") = Request.UrlReferrer.ToString()
        End If

        If (Not Session("Utilisateur") Is Nothing) Then

            If (Not Page.IsPostBack) Then

                'liste des civilités
                ddlCivilite.DataTextField = "Text"
                ddlCivilite.DataValueField = "Value"

                ddlCivilite.DataSource = DBFunctions.getCivilites()
                ddlCivilite.DataBind()

                Dim user As Utilisateur = CType(Session("Utilisateur"), Utilisateur)
                Dim item As ListItem

                'Ajout des champs

                Dim liste_des_champs As List(Of Champ) = DBFunctions.getChamps(user, "", "0")

                listeCorrection.Items.Clear()
                listeEnrichissement.Items.Clear()

                For i As Integer = 0 To liste_des_champs.Count - 1

                    item = New ListItem(liste_des_champs.Item(i).Description, liste_des_champs.Item(i).IDChamp)

                    If (liste_des_champs.Item(i).Action = "CORRECTION") Then

                        'Par défaut on embarque tous les champs de correction
                        item.Selected = True

                        listeCorrection.Items.Add(item)

                    ElseIf (liste_des_champs.Item(i).Action = "ENRICHISSEMENT") Then

                        listeEnrichissement.Items.Add(item)

                    End If

                Next

                'filtres de sélection

                Dim liste_filtres As List(Of Filtre) = DBFunctions.getFiltres(user, False)

                listeFiltres.Items.Clear()

                For i As Integer = 0 To liste_filtres.Count - 1

                    item = New ListItem(liste_filtres.Item(i).Libelle, liste_filtres.Item(i).IDFiltre)
                    listeFiltres.Items.Add(item)

                Next

                'alertes automatiques

                Dim liste_alertes As List(Of AlerteAutomatique) = DBFunctions.getAlertesAutomatiques(user, False)

                listeAlertes.Items.Clear()

                For i As Integer = 0 To liste_alertes.Count - 1

                    item = New ListItem(liste_alertes.Item(i).Libelle, liste_alertes.Item(i).IDAlerteAuto)
                    listeAlertes.Items.Add(item)

                Next

            End If

        Else

            Response.Redirect("Connexion.aspx")

        End If

    End Sub

    Private Sub btnTerminer_Click(sender As Object, e As EventArgs) Handles btnTerminer.Click

        Dim user As Utilisateur = CType(Session("Utilisateur"), Utilisateur)
        Dim utilisateur_temp As New Utilisateur()
        Dim filtre_temp As New Filtre()
        Dim alerte_auto_temp As New AlerteAutomatique()

        'champs infos

        utilisateur_temp.Workflow = user.Workflow
        utilisateur_temp.Identifiant = txtIdentifiant.Text.Trim()
        utilisateur_temp.MDP = txtMdp.Text.Trim()
        utilisateur_temp.Civilite.Id = ddlCivilite.SelectedValue
        utilisateur_temp.Nom = txtNom.Text.Trim()
        utilisateur_temp.Prenom = txtPrenom.Text.Trim()
        utilisateur_temp.Email = txtEmail.Text.Trim()
        utilisateur_temp.Fonction = txtFonction.Text.Trim()

        Dim champ_temp As Champ

        'champs d'enrichissement

        For Each item As ListItem In listeEnrichissement.Items

            If (item.Selected) Then

                champ_temp = New Champ(user.Workflow, item.Value, "", "", "", "", "", "", "", New List(Of ListItem), "", "", 0)
                utilisateur_temp.ListeChampsEnrichissement.Add(champ_temp)

            End If

        Next

        'champs de correction

        For Each item As ListItem In listeCorrection.Items

            If (item.Selected) Then

                champ_temp = New Champ(user.Workflow, item.Value, "", "", "", "", "", "", "", New List(Of ListItem), "", "", 0)
                utilisateur_temp.ListeChampsCorrection.Add(champ_temp)

            End If

        Next

        'filtres de sélection

        For Each item As ListItem In listeFiltres.Items

            If (item.Selected) Then

                filtre_temp = New Filtre()
                filtre_temp.IDFiltre = item.Value
                filtre_temp.Workflow = user.Workflow
                utilisateur_temp.ListeFiltres.Add(filtre_temp)

            End If

        Next

        'alertes automatiques

        For Each item As ListItem In listeAlertes.Items

            If (item.Selected) Then

                alerte_auto_temp = New AlerteAutomatique()
                alerte_auto_temp.IDAlerteAuto = item.Value
                alerte_auto_temp.Workflow = user.Workflow
                utilisateur_temp.ListeAlertesAuto.Add(alerte_auto_temp)

            End If

        Next

        If (DBFunctions.InsertUtilisateur(utilisateur_temp)) Then

            Response.Redirect("ParametrageUtilisateur.aspx")

        End If

    End Sub

End Class