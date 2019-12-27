Imports System.Text.RegularExpressions

Public Class Traitement
    Inherits System.Web.UI.Page

    Dim liste_champs_correction As New List(Of Champ)
    Dim liste_champs_enrichissement As New List(Of Champ)
    Dim liste_champs As New List(Of Champ)
    Dim liste_champs_ligne As New List(Of Champ)
    Dim valeurs_champs_ligne As New List(Of LigneFacture)
    Dim current_user As Utilisateur
    Dim docid, statut As String
    Dim label As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        pnlCorrection.Visible = False
        pnlEnrichissement.Visible = False
        pnlBascule.Visible = False
        pnlLignesFacture.Visible = False
        openModalConfirmInvalidationFacture.Visible = False

        current_user = CType(Session("Utilisateur"), Utilisateur)

        If (Not current_user Is Nothing) Then

            If (Not Request.QueryString("id") Is Nothing) Then
                docid = Request.QueryString("id").ToString().Trim()
                statut = DBFunctions.getStatutFacture(current_user, docid)

                label = DBFunctions.getNom_Fichier(current_user, docid)

                Nom_Fichier.Text = label

            Else
                docid = ""
                statut = ""
            End If

            HiddenProfil.Value = current_user.IDProfil
            HiddenWorkflow.Value = current_user.Workflow

            If current_user.Workflow = "65" Then
                litNewRejet.Text = "<button id='open-modal-nouveau-rejet' data-toggle='modal' data-target='#modal-rejet' type='button' class='btn btn-primary'><i class='fa fa-plus'></i>&nbsp;Nouveau rejet</button>"
            End If

            'Gestion validation
            If (current_user.GestionValidation = "1") Then

                If (current_user.IDProfil = "2") Then

                    litButtonValidation.Text = "Envoyer au valideur"

                ElseIf (current_user.IDProfil = "3") Then

                    litButtonValidation.Text = "Valider la facture"

                End If

            Else

                litButtonValidation.Text = "Valider la facture"

            End If

            'Valorisation automatique

            If (current_user.GestionValidation = "1") Then

                litValorisationAutomatique.Text = "<div Class=""row"" style=""padding-top:25px;"">" & _
                                                        "<div class=""col-lg-12"">" & _
                                                            "<div class=""form-check"">" & _
                                                                "<input type=""checkbox"" class=""form-check-input"" id=""chkValAuto""/>" & _
                                                                "&nbsp;<label class=""form-check-label"" for=""chkValAuto"">Valoriser automatiquement pour ce fournisseur</label>" & _
                                                            "</div>" &
                                                        "</div>" & _
                                                    "</div>" & _
                                                    "<div class=""row"">" & _
                                                        "<div class=""col-lg-12"">" & _
                                                            "<div id=""messageValorisation""></div>" & _
                                                        "</div>" & _
                                                    "</div>"
            Else

                litValorisationAutomatique.Text = ""

            End If


            liste_champs_correction = DBFunctions.getChampsACorriger(current_user)
            liste_champs_enrichissement = DBFunctions.getChampsAEnrichir(current_user)

            liste_champs = DBFunctions.getChamps(current_user, "CORRECTION", "0")

            DBFunctions.getValeursFacture(liste_champs, current_user, docid)

            'Reprise des valeurs de ligne

            If (current_user.GestionLigne = "1") Then
                liste_champs_ligne = DBFunctions.getChamps(current_user, "", "1")
                valeurs_champs_ligne = DBFunctions.getValeursLignesFacture(liste_champs_ligne, current_user, docid)
            End If

            If (current_user.Workflow = "65") Then
                If liste_champs.Where(Function(x) x.IDChamp = "12").FirstOrDefault().Valeur = "2" Then
                    liste_champs_correction.RemoveAll(Function(x) x.IDChamp = "12")
                End If
            End If


                If (Not Page.IsPostBack()) Then

                If (Request.UrlReferrer.ToString().IndexOf("Traitement.aspx") = -1) Then
                    Session("page_precedente") = Request.UrlReferrer.ToString()
                End If

                ViewState("UrlReferent") = Request.UrlReferrer.ToString()

                'flag d'occupation
                DBFunctions.setFlagOccupe(docid, "1", current_user)

                'Infos du modal d'alerte

                litMotifsAlerte.Text = ""

                Dim liste_motifs_alerte As List(Of ListItem) = DBFunctions.getMotifsAlerte(current_user)

                For Each item As ListItem In liste_motifs_alerte
                    litMotifsAlerte.Text += "<option value=""" & item.Value & """>" & item.Text & "</option>"
                Next

                Dim champ_num_facture As Champ = liste_champs.FindLast(Function(x) x.NomChamp = "NUM_FACTURE")
                Dim champ_date_facture As Champ = liste_champs.FindLast(Function(x) x.NomChamp = "DATE_FACTURE")

                If (Not champ_num_facture Is Nothing) Then
                    HiddenNumFacture.Value = champ_num_facture.Valeur.ToString()
                End If

                If (Not champ_num_facture Is Nothing) Then
                    If current_user.Workflow <> "65" Then
                        HiddenDateFacture.Value = champ_date_facture.Valeur.ToString()
                    End If
                End If
                'Affichage des panneaux
                AffichagePanneaux()

                If (docid <> "" And statut <> "") Then

                    HiddenStatut.Value = statut

                    If (docid.Trim() = "undefined") Then
                        Response.Redirect(ViewState("UrlReferent").ToString())
                    Else
                        HiddenDocID.Value = docid
                    End If

                    iframePDF.Src = "handlers/VisualisationPDF.ashx?docid=" & docid + "&filename=" + label.Replace(" ", "_").Replace("&", "ET")

                    'Champs correction
                    RepeaterCorrection.DataSource = liste_champs.FindAll(Function(x) x.Action = "CORRECTION").ToArray()
                    RepeaterCorrection.DataBind()

                    'Champs enrichissement
                    RepeaterEnrichissement.DataSource = liste_champs.FindAll(Function(x) x.Action = "ENRICHISSEMENT").ToArray()
                    RepeaterEnrichissement.DataBind()

                    If (current_user.GestionLigne = "1") Then

                        'Lignes de facture

                        litLignesFacture.Text = "<table class=""table table-striped table-bordered"" id=""tableLignesFacture"">" & _
                                                    "<thead>" & _
                                                        "<tr>"

                        For Each ch_lib As Champ In liste_champs_ligne

                            litLignesFacture.Text += "<th>" & ch_lib.Description & "</th>"

                        Next

                        litLignesFacture.Text += "<th></th></tr></thead>" &
                                                    "<tbody>"

                        Dim liste As New List(Of ListItem)
                        If (current_user.Workflow = "62") Then
                            liste = DBFunctions.getCodeComptable(current_user)
                        End If

                        For Each ligne As LigneFacture In valeurs_champs_ligne

                            litLignesFacture.Text += "<tr id=""index-" & ligne.IdLigne & """ class=""odd gradeX"">"

                            For Each ch_val As Champ In ligne.ListeChamps

                                'Exception : Calcul compte_collectif par rapport au compte comptable pour workflow 62 fait appel à l'ajax

                                If (ch_val.Workflow = "62" And ch_val.NomChamp = "COMPTE_GENERAL") Then
                                    Dim select_list As String = ""
                                    Dim js_function As String = $"CalculerCollectif('txtChampLigne_{ligne.IdLigne}_{ch_val.IDChamp}')"
                                    For Each item In liste
                                        If (item.Value = ch_val.Valeur) Then
                                            select_list += $"<option value='{item.Value}' selected>{item.Text}</option>"
                                        Else
                                            select_list += $"<option value='{item.Value}'>{item.Text}</option>"
                                        End If
                                    Next

                                    select_list = $"<select id='txtChampLigne_{ligne.IdLigne}_{ch_val.IDChamp}' class='selectpicker champ-ligne' data-live-search='true' data-ligne='{ligne.IdLigne}' data-champ='{ch_val.IDChamp}'  data-champvalorise='49'>{select_list}</select>"

                                    litLignesFacture.Text += $"<td>{select_list}</td>"

                                    'litLignesFacture.Text += "<td><input type=""text"" id=""txtChampLigne_" & ligne.IdLigne & "_" & ch_val.IDChamp & """ value=""" & ch_val.Valeur & """ class=""form-control input-sm champ-ligne"" data-ligne=""" & ligne.IdLigne & """ data-champ=""" & ch_val.IDChamp & """ data-champvalorise=""49"" onKeyUp=""CalculerCollectif('txtChampLigne_" & ligne.IdLigne & "_" & ch_val.IDChamp & "')"" /></td>"

                                Else

                                    litLignesFacture.Text += "<td><input type=""text"" id=""txtChampLigne_" & ligne.IdLigne & "_" & ch_val.IDChamp & """ value=""" & ch_val.Valeur & """ class=""form-control input-sm champ-ligne"" data-ligne=""" & ligne.IdLigne & """ data-champ=""" & ch_val.IDChamp & """ /></td>"

                                End If

                            Next

                            litLignesFacture.Text += "<td><button type=""button"" id=""btnSupprimerLigne_" & ligne.IdLigne & """ data-ligne=""" & ligne.IdLigne & """ class=""btn btn-sm btn-danger supp-ligne"" data-toggle=""modal"" data-target=""#modal-confirm-supp-ligne"">Supprimer</button></td>" & _
                                                "</tr>"

                        Next

                        litLignesFacture.Text += "</tbody>" & _
                                                "</table>"


                    End If

                    'Liste des fichiers ajoutés

                    Dim liste_fichiers_ajoutes As List(Of FichierAjoute) = DBFunctions.getFichiersAjoutes(current_user, docid)
                    litFichiersAjoutes.Text = ""

                    If (liste_fichiers_ajoutes.Count = 0) Then

                        litFichiersAjoutes.Text = "<tr><td colspan=""5"" style=""text-align:center;"">Aucun fichier n'a été ajouté</td></tr>"

                    Else

                        For Each fichier As FichierAjoute In liste_fichiers_ajoutes

                            litFichiersAjoutes.Text += "<tr class=""odd gradeX"">" & _
                                                    "<td data-id=""" & fichier.IDAjout & """>" & fichier.NomFichier & "</td>" & _
                                                    "<td data-id=""" & fichier.IDAjout & """>" & fichier.DateAjout & "</td>" & _
                                                    "<td data-id=""" & fichier.IDAjout & """>" & fichier.AjoutePar & "</td>" & _
                                                    "<td class=""suppression""><a id=""open-modal-confirm-suppression-fichier"" href=""#"" data-id=""" & fichier.IDAjout & """ data-toggle=""modal"" data-target=""#modal-confirm-suppression-fichier""><i class=""fa fa-times""></i></a></td>" & _
                                                "</tr>"
                        Next

                    End If

                    'Liste alertes

                    Dim liste_alertes_facture As List(Of AlerteFacture) = DBFunctions.getAlertesFacture(docid, current_user, False, "")

                    litAlertes.Text = ""

                    If (liste_alertes_facture.Count = 0) Then

                        litAlertes.Text = "<tr><td colspan=""7"" style=""text-align:center;"">Aucune alerte</td></tr>"

                    Else

                        For Each alerte As AlerteFacture In liste_alertes_facture

                            litAlertes.Text += "<tr class=""odd gradeX"">" & _
                                                    "<td>" & alerte.Motif.Libelle & "</td>" & _
                                                    "<td>" & alerte.Commentaire & "</td>" & _
                                                    "<td>" & alerte.Emetteur.Prenom & " " & alerte.Emetteur.Nom & "</td>" & _
                                                    "<td>" & alerte.DateAlerte & "</td>" & _
                                                    "<td>" & alerte.ResoluPar.Prenom & " " & alerte.ResoluPar.Nom & "</td>" & _
                                                    "<td>" & alerte.DateResolution & "</td>" & _
                                                    "<td><input class=""checkbox_alerte_resolue"" type=""checkbox"" data-id=""" & alerte.IDAlerte & """ value=""2"" "

                            If (alerte.Statut.Id = "2") Then
                                litAlertes.Text += " checked=""checked"" /></td></tr>"
                            Else
                                litAlertes.Text += " /></td></tr>"
                            End If


                        Next

                    End If

                    'Historique

                    Dim liste_historique As List(Of Historique) = DBFunctions.getHistorique(current_user, docid)
                    'litHistorique.Text = ""

                    If (liste_historique.Count = 0) Then

                        'litHistorique.Text = "<tr><td colspan=""6"" style=""text-align:center;""> Historique vide </td></tr>"

                    Else
                        For Each histo As Historique In liste_historique

                            ''litHistorique.Text += "<tr class=""odd gradeX"">" & _
                            '                                "<td>" & histo.DateAction & "</td>" & _
                            '                               "<td>" & histo.Action & "</td>" & _
                            '                            "<td>" & histo.NomUtilisateur & "</td>" & _
                            '                                "<td>" & histo.LibelleChamp & "</td>" & _
                            '                                "<td>" & histo.AncienneValeur & "</td>" & _
                            '                                "<td>" & histo.NouvelleValeur & "</td>" & _
                            '                            "</tr>"

                            If (histo.Action = "CORRECTION") Then
                                HiddenFlagCorrection.Value = "1"
                            ElseIf (histo.Action = "ENRICHISSEMENT") Then
                                HiddenFlagEnrichissement.Value = "1"
                            End If

                        Next
                    End If

                    'Liste des statuts pour bascule
                    ddlStatutBascule.DataTextField = "Text"
                    ddlStatutBascule.DataValueField = "Value"

                    ddlStatutBascule.DataSource = DBFunctions.getStatutsFactures("ACTION")
                    ddlStatutBascule.DataBind()

                    ddlStatutBascule.Items.Insert(0, New ListItem("", ""))


                End If

            Else

                'Affichage des panneaux
                AffichagePanneaux()

            End If

            If (ddlAction.SelectedValue = "Correction") Then
                ControleCorrection()
            ElseIf (ddlAction.SelectedValue = "Enrichissement") Then
                ControleEnrichissement()
            End If

        Else

            Response.Redirect("Connexion.aspx")

        End If

    End Sub

    Private Sub AffichagePanneaux()

        openModalConfirmValidationFacture.Visible = False
        openModalConfirmInvalidationFacture.Visible = False

        If (statut <> "") Then

            If (Not Page.IsPostBack()) Then
                ddlAction.Items.Clear()
            End If

            If (statut = "1" Or statut = "0" Or statut = "5" Or statut = "6" Or statut = "10" Or (statut = "8" And current_user.IDProfil <> "3") Or (statut = "9" And current_user.IDProfil = "3")) Then

                If (Not Page.IsPostBack()) Then
                    ddlAction.Enabled = False
                    ddlAction.Items.Clear()
                End If

                If (statut = "1" Or statut = "0" Or statut = "5" Or statut = "6") Then

                    pnlCorrection.Visible = False
                    pnlCorrection.Enabled = False

                    pnlBascule.Visible = True
                    pnlBascule.Enabled = True

                Else

                    pnlCorrection.Visible = True
                    pnlCorrection.Enabled = False

                    pnlBascule.Visible = False
                    pnlBascule.Enabled = False

                End If

                pnlEnrichissement.Visible = False
                pnlEnrichissement.Enabled = False

                pnlAjoutDocuments.Visible = False
                pnlAjoutDocuments.Enabled = False

                pnlLignesFacture.Visible = False
                pnlLignesFacture.Enabled = False


            ElseIf (statut = "2") Or (statut = "7" And current_user.Workflow = "75") Then

                openModalConfirmValidationFacture.Visible = True

                If (Not Page.IsPostBack()) Then
                    ddlAction.Items.Add("Correction")
                    ddlAction.Enabled = False
                End If

                pnlCorrection.Visible = True
                pnlCorrection.Enabled = True

                pnlEnrichissement.Visible = False
                pnlEnrichissement.Enabled = False

                pnlAjoutDocuments.Visible = True
                pnlAjoutDocuments.Enabled = True

                pnlBascule.Visible = False
                pnlBascule.Enabled = False


                If (current_user.GestionLigne = "1") Then
                    pnlLignesFacture.Visible = True
                    pnlLignesFacture.Enabled = True
                End If


            ElseIf (statut = "3") Then

                openModalConfirmValidationFacture.Visible = True

                If (Not Page.IsPostBack()) Then
                    ddlAction.Items.Add("Enrichissement")
                End If

                pnlCorrection.Visible = False
                pnlCorrection.Enabled = False

                pnlEnrichissement.Visible = True
                pnlEnrichissement.Enabled = True

                pnlAjoutDocuments.Visible = True
                pnlAjoutDocuments.Enabled = True

                pnlBascule.Visible = False
                pnlBascule.Enabled = False


                If (current_user.GestionLigne = "1") Then
                    pnlLignesFacture.Visible = True
                    pnlLignesFacture.Enabled = True
                End If


            ElseIf (statut = "4") Then

                openModalConfirmValidationFacture.Visible = True

                If (Not Page.IsPostBack()) Then

                    ddlAction.Items.Add("Correction")
                    ddlAction.Items.Add("Enrichissement")
                    ddlAction.SelectedIndex = 0
                End If

                pnlCorrection.Visible = True
                pnlCorrection.Enabled = True

                pnlEnrichissement.Visible = False
                pnlEnrichissement.Enabled = True

                pnlAjoutDocuments.Visible = True
                pnlAjoutDocuments.Enabled = True

                pnlBascule.Visible = False
                pnlBascule.Enabled = False


                If (current_user.GestionLigne = "1") Then
                    pnlLignesFacture.Visible = True
                    pnlLignesFacture.Enabled = True
                End If


            ElseIf ((statut = "8" And current_user.IDProfil = "3") Or (statut = "9" And current_user.IDProfil <> "3")) Then

                If (current_user.IDProfil = "3") Then

                    openModalConfirmValidationFacture.Visible = True
                    openModalConfirmInvalidationFacture.Visible = True

                Else

                    openModalConfirmValidationFacture.Visible = True

                End If


                pnlAjoutDocuments.Visible = True
                pnlAjoutDocuments.Enabled = True

                pnlBascule.Visible = False
                pnlBascule.Enabled = False

                'On cherche à savoir les actions menées afin de déterminer ce qu'il faut activer pour le valideur
                'Dim liste_histo_action As List(Of Historique) = DBFunctions.getHistorique(current_user, docid)
                'Dim flag_correction As Boolean = liste_histo_action.FindAll(Function(x) x.Action = "CORRECTION").Count > 0
                'Dim flag_enrichissement As Boolean = liste_histo_action.FindAll(Function(x) x.Action = "ENRICHISSEMENT").Count > 0

                'If (flag_correction And flag_enrichissement) Then

                If (Not Page.IsPostBack()) Then
                    ddlAction.Items.Add("Correction")
                    ddlAction.Items.Add("Enrichissement")
                    ddlAction.SelectedIndex = 0
                End If

                pnlCorrection.Visible = True
                pnlCorrection.Enabled = True

                pnlEnrichissement.Visible = False
                pnlEnrichissement.Enabled = True

                If (current_user.GestionLigne = "1") Then
                    pnlLignesFacture.Visible = True
                    pnlLignesFacture.Enabled = True
                End If

                'ElseIf (flag_correction) Then

                '    If (Not Page.IsPostBack()) Then
                '        ddlAction.Items.Add("Correction")
                '        ddlAction.SelectedIndex = 0
                '    End If

                '    pnlCorrection.Visible = True
                '    pnlCorrection.Enabled = True

                '    pnlEnrichissement.Visible = False
                '    pnlEnrichissement.Enabled = False

                '    If (current_user.GestionLigne = "1") Then
                '        pnlLignesFacture.Visible = True
                '        pnlLignesFacture.Enabled = True
                '    End If


                'ElseIf (flag_enrichissement) Then

                '    If (Not Page.IsPostBack()) Then
                '        ddlAction.Items.Add("Enrichissement")
                '        ddlAction.SelectedIndex = 0
                '    End If

                '    pnlCorrection.Visible = False
                '    pnlCorrection.Enabled = False

                '    pnlEnrichissement.Visible = True
                '    pnlEnrichissement.Enabled = True

                '    If (current_user.GestionLigne = "1") Then
                '        pnlLignesFacture.Visible = True
                '        pnlLignesFacture.Enabled = True
                '    End If

                'End If


            End If

        Else

            pnlCorrection.Visible = False
            pnlCorrection.Enabled = False

            pnlEnrichissement.Visible = False
            pnlEnrichissement.Enabled = False

            pnlBascule.Visible = False
            pnlBascule.Enabled = False

            pnlLignesFacture.Visible = False
            pnlLignesFacture.Enabled = False

        End If

        'panel commentaire

        If (current_user.GestionValidation = "1") Then
            Dim commentaire_valideur As CommentaireValideur = DBFunctions.GetCommentaireValideur(current_user, docid)
            If (Not commentaire_valideur Is Nothing And statut = "9") Then
                pnlCommentaireValideur.Visible = True
                litCommentaire.Text = "<strong>Motif d'invalidité : </strong>" & commentaire_valideur.Motif & ""
                If (commentaire_valideur.Commentaire.Trim() <> "") Then
                    litCommentaire.Text += "<p class=""commentaire""><i class=""fa fa-comment-o""></i>&nbsp;&nbsp;&nbsp;" & commentaire_valideur.Commentaire & "</p>"
                End If

                If (commentaire_valideur.Nom.Trim() = "") Then
                    litValideurCommentaire.Text = commentaire_valideur.Identifiant
                Else
                    litValideurCommentaire.Text = commentaire_valideur.Nom
                End If
                litDateCommentaire.Text = commentaire_valideur.DateRetour
            Else
                pnlCommentaireValideur.Visible = False
                litCommentaire.Text = ""
                litValideurCommentaire.Text = ""
                litDateCommentaire.Text = ""
            End If
        Else
            pnlCommentaireValideur.Visible = False
            litCommentaire.Text = ""
            litValideurCommentaire.Text = ""
            litDateCommentaire.Text = ""
        End If


    End Sub

    Private Sub btnRetour_Click(sender As Object, e As EventArgs) Handles btnRetour.Click

        If (DBFunctions.setFlagOccupe(docid, "", current_user)) Then
            Response.Redirect(Session("page_precedente"))
        End If

    End Sub

    Private Sub RepeaterCorrection_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles RepeaterCorrection.ItemDataBound

        Dim litCorLibelle As Literal = CType(e.Item.FindControl("litCorLibelle"), Literal)
        Dim txtCorChamp As TextBox = CType(e.Item.FindControl("txtCorChamp"), TextBox)
        Dim ddlCorChamp As DropDownList = CType(e.Item.FindControl("ddlCorChamp"), DropDownList)
        Dim hiddenCorIdChamp As HiddenField = CType(e.Item.FindControl("hiddenCorIdChamp"), HiddenField)
        Dim hiddenCorTypage As HiddenField = CType(e.Item.FindControl("hiddenCorTypage"), HiddenField)

        Dim id_champ As String = hiddenCorIdChamp.Value

        Dim champ_correction As Champ = liste_champs_correction.FindLast(Function(x) x.IDChamp = id_champ)
        Dim champ_valeurs As Champ = liste_champs.FindLast(Function(x) x.IDChamp = id_champ)

        'initialisation
        txtCorChamp.Visible = False
        txtCorChamp.Enabled = False
        txtCorChamp.ReadOnly = False

        ddlCorChamp.Visible = False
        ddlCorChamp.Enabled = False

        'adaptation 
        If (hiddenCorTypage.Value = "LISTE") Then

            ddlCorChamp.Visible = True

            If (Not champ_valeurs Is Nothing) Then

                ddlCorChamp.DataTextField = "Text"
                ddlCorChamp.DataValueField = "Value"

                ddlCorChamp.DataSource = champ_valeurs.Liste
                ddlCorChamp.DataBind()
                ddlCorChamp.Items.Insert(0, New ListItem("", ""))

                ddlCorChamp.SelectedValue = champ_valeurs.Valeur

                If (champ_correction Is Nothing) Then
                    ddlCorChamp.Enabled = False
                Else
                    ddlCorChamp.Enabled = True
                End If

            End If
        Else
            txtCorChamp.Visible = True

            If (champ_correction Is Nothing) Then
               
            Else
                txtCorChamp.ReadOnly = False
                txtCorChamp.Enabled = True
            End If

        End If

    End Sub

    Private Sub RepeaterEnrichissement_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles RepeaterEnrichissement.ItemDataBound

        Dim litEnrLibelle As Literal = CType(e.Item.FindControl("litEnrLibelle"), Literal)
        Dim txtEnrChamp As TextBox = CType(e.Item.FindControl("txtEnrChamp"), TextBox)
        Dim ddlEnrChamp As DropDownList = CType(e.Item.FindControl("ddlEnrChamp"), DropDownList)
        Dim hiddenEnrIdChamp As HiddenField = CType(e.Item.FindControl("hiddenEnrIdChamp"), HiddenField)
        Dim hiddenEnrTypage As HiddenField = CType(e.Item.FindControl("hiddenEnrTypage"), HiddenField)

        Dim id_champ As String = hiddenEnrIdChamp.Value

        'initialisation
        txtEnrChamp.Visible = False
        txtEnrChamp.Enabled = False
        txtEnrChamp.ReadOnly = True

        ddlEnrChamp.Visible = False
        ddlEnrChamp.Enabled = False

        'adaptation 
        If (hiddenEnrTypage.Value = "LISTE") Then

            ddlEnrChamp.Visible = True

            Dim champ_correction As Champ = liste_champs_correction.FindLast(Function(x) x.IDChamp = id_champ)
            Dim champ_valeurs As Champ = liste_champs.FindLast(Function(x) x.IDChamp = id_champ)

            If (Not champ_valeurs Is Nothing) Then

                ddlEnrChamp.DataTextField = "Text"
                ddlEnrChamp.DataValueField = "Value"

                ddlEnrChamp.DataSource = champ_valeurs.Liste
                ddlEnrChamp.DataBind()
                ddlEnrChamp.Items.Insert(0, New ListItem("", ""))

                ddlEnrChamp.SelectedValue = champ_valeurs.Valeur

                If (champ_correction Is Nothing) Then
                    ddlEnrChamp.Enabled = False
                Else
                    ddlEnrChamp.Enabled = True
                End If

            End If

        Else

            txtEnrChamp.Visible = True

            If (liste_champs_enrichissement.FindLast(Function(x) x.IDChamp = id_champ) Is Nothing) Then
                txtEnrChamp.ReadOnly = True
                txtEnrChamp.Enabled = False
            Else
                txtEnrChamp.ReadOnly = False
                txtEnrChamp.Enabled = True
            End If

        End If

    End Sub

    Private Sub ddlAction_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAction.SelectedIndexChanged

        If (Not current_user Is Nothing) Then

            If (ddlAction.Text = "Correction") Then

                pnlCorrection.Visible = True
                pnlEnrichissement.Visible = False

            ElseIf (ddlAction.Text = "Enrichissement") Then

                pnlCorrection.Visible = False
                pnlEnrichissement.Visible = True

            End If

        Else
            Response.Redirect("Connexion.aspx")
        End If

    End Sub

    Private Function ControleCorrection() As Boolean

        Dim flag_ok As Boolean = True

        For Each item As RepeaterItem In RepeaterCorrection.Items

            Dim txtCorChamp As TextBox = CType(item.FindControl("txtCorChamp"), TextBox)
            Dim ddlCorChamp As DropDownList = CType(item.FindControl("ddlCorChamp"), DropDownList)
            Dim hiddenCorIdChamp As HiddenField = CType(item.FindControl("hiddenCorIdChamp"), HiddenField)
            Dim hiddenCorNomChamp As HiddenField = CType(item.FindControl("hiddenCorNomChamp"), HiddenField)
            Dim hiddenCorTypage As HiddenField = CType(item.FindControl("hiddenCorTypage"), HiddenField)

            If (Not ((txtCorChamp Is Nothing) Or (ddlCorChamp Is Nothing) Or (hiddenCorIdChamp Is Nothing) Or (hiddenCorNomChamp Is Nothing))) Then

                If (hiddenCorTypage.Value = "TEXTE") Then

                    'Vérification de l'expression reguliére

                    Dim champ_correction As Champ = liste_champs.FindLast(Function(x) x.IDChamp = hiddenCorIdChamp.Value)

                    Dim expr As String = champ_correction.ExpressionReguliere.Trim()

                    If (expr <> "") Then

                        Dim resultat As Boolean = Regex.IsMatch(txtCorChamp.Text, expr)

                        If (resultat = False) Then
                            flag_ok = False
                            txtCorChamp.BackColor = Drawing.Color.LightPink
                        Else
                            txtCorChamp.BackColor = Drawing.Color.White
                        End If

                    End If

                End If

            End If

        Next

        Return flag_ok

    End Function

    Private Function ControleEnrichissement() As Boolean

        Dim flag_ok As Boolean = True

        For Each item As RepeaterItem In RepeaterEnrichissement.Items

            Dim txtEnrChamp As TextBox = CType(item.FindControl("txtEnrChamp"), TextBox)
            Dim ddlEnrChamp As DropDownList = CType(item.FindControl("ddlEnrChamp"), DropDownList)
            Dim hiddenEnrIdChamp As HiddenField = CType(item.FindControl("hiddenEnrIdChamp"), HiddenField)
            Dim hiddenEnrNomChamp As HiddenField = CType(item.FindControl("hiddenEnrNomChamp"), HiddenField)
            Dim hiddenEnrTypage As HiddenField = CType(item.FindControl("hiddenEnrTypage"), HiddenField)

            If (Not ((txtEnrChamp Is Nothing) Or (ddlEnrChamp Is Nothing) Or (hiddenEnrIdChamp Is Nothing) Or (hiddenEnrNomChamp Is Nothing))) Then

                If (hiddenEnrTypage.Value = "TEXTE") Then

                    'Vérification de l'expression reguliére

                    Dim champ_enrichissement As Champ = liste_champs.FindLast(Function(x) x.IDChamp = hiddenEnrIdChamp.Value)

                    Dim expr As String = champ_enrichissement.ExpressionReguliere.Trim()

                    If (expr <> "") Then

                        Dim resultat As Boolean = Regex.IsMatch(txtEnrChamp.Text, expr)

                        If (resultat = False) Then
                            flag_ok = False
                            txtEnrChamp.BackColor = Drawing.Color.LightPink
                        Else
                            txtEnrChamp.BackColor = Drawing.Color.White
                        End If

                    End If

                End If

            End If

        Next

        Return flag_ok

    End Function

    Private Sub btnValiderCorrection_Click(sender As Object, e As EventArgs) Handles btnValiderCorrection.Click

        If (ControleCorrection()) Then

            Dim liste_champs_corriges As New List(Of Champ)
            Dim champ_temp As Champ
            Dim flag_exp_ok As Boolean = True

            'récupération des valeurs

            For Each item As RepeaterItem In RepeaterCorrection.Items

                Dim txtCorChamp As TextBox = CType(item.FindControl("txtCorChamp"), TextBox)
                Dim ddlCorChamp As DropDownList = CType(item.FindControl("ddlCorChamp"), DropDownList)
                Dim hiddenCorIdChamp As HiddenField = CType(item.FindControl("hiddenCorIdChamp"), HiddenField)
                Dim hiddenCorNomChamp As HiddenField = CType(item.FindControl("hiddenCorNomChamp"), HiddenField)
                Dim hiddenCorTypage As HiddenField = CType(item.FindControl("hiddenCorTypage"), HiddenField)

                If (Not ((txtCorChamp Is Nothing) Or (ddlCorChamp Is Nothing) Or (hiddenCorIdChamp Is Nothing) Or (hiddenCorNomChamp Is Nothing))) Then

                    'On récupére l'ancienne valeur pour traitement ultérieur

                    txtCorChamp.ForeColor = Drawing.Color.White

                    Dim ancienne_valeur As String = ""
                    Dim champ_correction As Champ = liste_champs.FindLast(Function(x) x.IDChamp = hiddenCorIdChamp.Value)

                    If (Not champ_correction Is Nothing) Then
                        ancienne_valeur = champ_correction.Valeur
                    End If

                    If (hiddenCorTypage.Value = "LISTE") Then

                        If (ddlCorChamp.Enabled = True) Then

                            champ_temp = New Champ(current_user.Workflow, hiddenCorIdChamp.Value, hiddenCorNomChamp.Value, "", hiddenCorTypage.Value, ddlCorChamp.SelectedValue, "CORRECTION", ancienne_valeur, "", New List(Of ListItem), champ_correction.ExpressionReguliere, champ_correction.DetailLigne, champ_correction.Ordre)
                            liste_champs_corriges.Add(champ_temp)

                        End If

                    Else

                        If (txtCorChamp.Enabled = True) Then

                            champ_temp = New Champ(current_user.Workflow, hiddenCorIdChamp.Value, hiddenCorNomChamp.Value, "", hiddenCorTypage.Value, txtCorChamp.Text, "CORRECTION", ancienne_valeur, "", New List(Of ListItem), champ_correction.ExpressionReguliere, champ_correction.DetailLigne, champ_correction.Ordre)
                            liste_champs_corriges.Add(champ_temp)

                        End If

                    End If

                End If

            Next




            'Enregistrement

            If (liste_champs_corriges.Count() > 0) Then

                DBFunctions.UpdateFacture(docid, current_user, liste_champs_corriges)

                DBFunctions.UpdateStatutFactureCorrectif(docid, current_user, "", liste_champs_corriges)




                Response.Redirect(Request.RawUrl)

            End If

        End If

    End Sub

    Private Sub btnValiderEnrichissement_Click(sender As Object, e As EventArgs) Handles btnValiderEnrichissement.Click

        If (ControleEnrichissement()) Then

            Dim liste_champs_enrichis As New List(Of Champ)
            Dim champ_temp As Champ

            'récupération des valeurs

            For Each item As RepeaterItem In RepeaterEnrichissement.Items

                Dim txtEnrChamp As TextBox = CType(item.FindControl("txtEnrChamp"), TextBox)
                Dim ddlEnrChamp As DropDownList = CType(item.FindControl("ddlEnrChamp"), DropDownList)
                Dim hiddenEnrIdChamp As HiddenField = CType(item.FindControl("hiddenEnrIdChamp"), HiddenField)
                Dim hiddenEnrNomChamp As HiddenField = CType(item.FindControl("hiddenEnrNomChamp"), HiddenField)
                Dim hiddenEnrTypage As HiddenField = CType(item.FindControl("hiddenEnrTypage"), HiddenField)

                If (Not ((txtEnrChamp Is Nothing) Or (ddlEnrChamp Is Nothing) Or (hiddenEnrIdChamp Is Nothing) Or (hiddenEnrNomChamp Is Nothing))) Then

                    If (hiddenEnrTypage.Value = "LISTE") Then

                        If (ddlEnrChamp.Enabled = True) Then

                            'On récupére l'ancienne valeur pour traitement ultérieur

                            Dim ancienne_valeur As String = ""
                            Dim champ_enrichissement As Champ = liste_champs.FindLast(Function(x) x.IDChamp = hiddenEnrIdChamp.Value)

                            If (Not champ_enrichissement Is Nothing) Then
                                ancienne_valeur = champ_enrichissement.Valeur
                            End If

                            champ_temp = New Champ(current_user.Workflow, hiddenEnrIdChamp.Value, hiddenEnrNomChamp.Value, "", hiddenEnrTypage.Value, ddlEnrChamp.Text, "ENRICHISSEMENT", ancienne_valeur, "", New List(Of ListItem), champ_enrichissement.ExpressionReguliere, champ_enrichissement.DetailLigne, champ_enrichissement.Ordre)
                            liste_champs_enrichis.Add(champ_temp)

                        End If

                    End If

                    If (txtEnrChamp.Enabled = True) Then

                        'On récupére l'ancienne valeur pour traitement ultérieur

                        Dim ancienne_valeur As String = ""
                        Dim champ_enrichissement As Champ = liste_champs.FindLast(Function(x) x.IDChamp = hiddenEnrIdChamp.Value)

                        If (Not champ_enrichissement Is Nothing) Then
                            ancienne_valeur = champ_enrichissement.Valeur
                        End If

                        champ_temp = New Champ(current_user.Workflow, hiddenEnrIdChamp.Value, hiddenEnrNomChamp.Value, "", hiddenEnrTypage.Value, txtEnrChamp.Text, "ENRICHISSEMENT", ancienne_valeur, "", New List(Of ListItem), champ_enrichissement.ExpressionReguliere, champ_enrichissement.DetailLigne, champ_enrichissement.Ordre)
                        liste_champs_enrichis.Add(champ_temp)

                    End If

                End If

            Next

            'Enregistrement

            If (liste_champs_enrichis.Count() > 0) Then

                DBFunctions.UpdateFacture(docid, current_user, liste_champs_enrichis)

                Response.Redirect(Request.RawUrl)

            End If

        Else

            Response.Redirect(Request.RawUrl)

        End If

    End Sub

End Class