Public Class DetailUtilisateur
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'blocage du champ IDENTIFIANT
        txtIdentifiant.ReadOnly = True

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

                If (Not Request.QueryString("id") Is Nothing) Then

                    Dim utilisateur_parametre As Utilisateur
                    Dim id As String = Request.QueryString("id").ToString()
                    Dim item_champ, item_filtre, item_alerte As ListItem

                    utilisateur_parametre = DBFunctions.getInfosUtilisateur(id, user.Workflow)

                    'champs utilisateur

                    If (Not utilisateur_parametre Is Nothing) Then

                        ddlCivilite.SelectedValue = utilisateur_parametre.Civilite.Id
                        txtNom.Text = utilisateur_parametre.Nom
                        txtPrenom.Text = utilisateur_parametre.Prenom
                        txtEmail.Text = utilisateur_parametre.Email
                        txtFonction.Text = utilisateur_parametre.Fonction
                        txtIdentifiant.Text = utilisateur_parametre.Identifiant
                        txtMdp.Attributes.Add("Value", utilisateur_parametre.MDP)
                        txtConfirmMdp.Attributes.Add("Value", utilisateur_parametre.MDP)

                    End If

                    'champs d'enrichissement et de correction

                    Dim liste_des_champs As List(Of Champ) = DBFunctions.getChamps(user, "", "")
                    Dim liste_des_champs_enrichissement As List(Of Champ) = DBFunctions.getChampsAEnrichir(utilisateur_parametre)
                    Dim liste_des_champs_correction As List(Of Champ) = DBFunctions.getChampsACorriger(utilisateur_parametre)

                    listeEnrichissement.Items.Clear()
                    listeCorrection.Items.Clear()

                    For i As Integer = 0 To liste_des_champs.Count - 1

                        item_champ = New ListItem(liste_des_champs.Item(i).Description, liste_des_champs.Item(i).IDChamp)

                        If (liste_des_champs.Item(i).Action = "CORRECTION") Then

                            For Each ch_cor As Champ In liste_des_champs_correction

                                If (ch_cor.IDChamp = item_champ.Value) Then
                                    item_champ.Selected = True
                                End If

                            Next

                            listeCorrection.Items.Add(item_champ)
                            

                        ElseIf (liste_des_champs.Item(i).Action = "ENRICHISSEMENT") Then

                            For Each ch_enr As Champ In liste_des_champs_enrichissement

                                If (ch_enr.IDChamp = item_champ.Value) Then
                                    item_champ.Selected = True
                                End If

                            Next

                            listeEnrichissement.Items.Add(item_champ)

                        End If

                    Next

                    'filtres de sélection

                    Dim liste_filtres As List(Of Filtre) = DBFunctions.getFiltres(user, False)
                    Dim liste_filtres_utlisateur As List(Of Filtre) = DBFunctions.getFiltres(utilisateur_parametre, True)

                    listeFiltres.Items.Clear()

                    For i As Integer = 0 To liste_filtres.Count - 1

                        item_filtre = New ListItem(liste_filtres.Item(i).Libelle, liste_filtres.Item(i).IDFiltre)

                        For Each fil As Filtre In liste_filtres_utlisateur

                            If (fil.IDFiltre = item_filtre.Value) Then
                                item_filtre.Selected = True
                            End If

                        Next

                        listeFiltres.Items.Add(item_filtre)

                    Next

                    'alertes automatiques

                    Dim liste_alertes As List(Of AlerteAutomatique) = DBFunctions.getAlertesAutomatiques(user, False)
                    Dim liste_alertes_utlisateur As List(Of AlerteAutomatique) = DBFunctions.getAlertesAutomatiques(utilisateur_parametre, True)

                    listeAlertes.Items.Clear()

                    For i As Integer = 0 To liste_alertes.Count - 1

                        item_alerte = New ListItem(liste_alertes.Item(i).Libelle, liste_alertes.Item(i).IDAlerteAuto)

                        For Each ale As AlerteAutomatique In liste_alertes_utlisateur

                            If (ale.IDAlerteAuto = item_alerte.Value) Then
                                item_alerte.Selected = True
                            End If

                        Next

                        listeAlertes.Items.Add(item_alerte)

                    Next

                Else

                    Response.Redirect(ViewState("UrlReferent"))

                End If

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

        If (DBFunctions.UpdateUtilisateur(utilisateur_temp)) Then

            Response.Redirect("ParametrageUtilisateur.aspx")

        End If

    End Sub

End Class