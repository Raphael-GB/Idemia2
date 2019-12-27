Imports System.Text.RegularExpressions

Public Class TraitementHorsScope
    Inherits System.Web.UI.Page

    Dim liste_champs_correction As New List(Of Champ)
    Dim liste_champs_enrichissement As New List(Of Champ)
    Dim liste_champs As New List(Of Champ)
    Dim liste_champs_ligne As New List(Of Champ)
    Dim valeurs_champs_ligne As New List(Of LigneFacture)
    Dim current_user As Utilisateur
    Dim docid, statut As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        pnlCorrection.Visible = False
        current_user = CType(Session("Utilisateur"), Utilisateur)

        If (Not current_user Is Nothing) Then

            If (Not Request.QueryString("id") Is Nothing) Then
                docid = Request.QueryString("id").ToString().Trim()
                statut = DBFunctions.getStatutDocument(current_user, docid)

            Else
                docid = ""
                statut = ""
            End If

            HiddenProfil.Value = current_user.IDProfil

            HiddenWorkflow.Value = current_user.Workflow
            HiddenDocID.Value = docid

            Dim document As DocumentHorsScope = DBFunctions.getDocumentHorsScope(current_user, docid)
            Dim listeType As List(Of ListItem) = DBFunctions.getTypeDocument(current_user)
            Dim Statutdocument As String = DBFunctions.getStatutDocument2(current_user, docid)
            Dim Sitedocument As String = DBFunctions.getSiteDocument(current_user, docid)
            Dim listeStatut As List(Of ListItem) = New List(Of ListItem)
            listeStatut.Add(New ListItem("a traiter", "29"))
            listeStatut.Add(New ListItem("traité", "1"))

            Dim listSite As List(Of ListItem) = New List(Of ListItem)

            listSite.Add(New ListItem("Courbevoie", "0"))
            listSite.Add(New ListItem("Dijon", "1"))
            listSite.Add(New ListItem("Vitré", "2"))
            listSite.Add(New ListItem("", "3"))


            Dim listeSociete As List(Of ListItem) = DBFunctions.getSocieteIdm(current_user)

            listeSociete.AddRange({
                New ListItem("IDEMIA", "IDE"), New ListItem("OBERTHUR TECHNNOLOGIES", "OBE")
            })

            Dim typeDocument As ListItem = ddlTypeDocument.SelectedItem
            Dim entiteDocument As ListItem = ddlSociete.SelectedItem
            Dim statut2 As ListItem = ddlStatut2.SelectedItem
            Dim site As ListItem = ddlSite2.SelectedItem

            ddlStatut2.DataTextField = "Text"
            ddlStatut2.DataValueField = "Value"
            ddlStatut2.DataSource = listeStatut
            ddlStatut2.DataBind()
            If (Not Page.IsPostBack()) Then
                ddlStatut2.SelectedIndex = listeStatut.Where(Function(x) x.Value = Statutdocument).FirstOrDefault().Value
            Else
                If (statut2.Value = 29) Then
                    ddlStatut2.SelectedIndex = 0
                Else
                    ddlStatut2.SelectedIndex = 1
                End If

            End If

            ddlSite2.DataTextField = "Text"
            ddlSite2.DataValueField = "Value"
            ddlSite2.DataSource = listSite
            ddlSite2.DataBind()
            If (Not Page.IsPostBack()) Then
                ddlSite2.SelectedIndex = listSite.Where(Function(x) x.Text = Sitedocument).FirstOrDefault().Value
            Else
                ddlSite2.SelectedIndex = site.Value
            End If


            ddlTypeDocument.DataTextField = "Text"
            ddlTypeDocument.DataValueField = "Value"
            ddlTypeDocument.DataSource = listeType
            ddlTypeDocument.DataBind()
            ddlTypeDocument.Items.Insert(0, New ListItem("", ""))
            If (Not Page.IsPostBack()) Then
                ddlTypeDocument.SelectedIndex = listeType.Where(Function(x) x.Text = document.Type).FirstOrDefault().Value
            Else
                ddlTypeDocument.SelectedIndex = typeDocument.Value
            End If
            ddlSociete.DataTextField = "Text"
            ddlSociete.DataValueField = "Value"
            ddlSociete.DataSource = listeSociete
            ddlSociete.DataBind()
            ddlSociete.Items.Insert(0, New ListItem("", ""))

            If (Not Page.IsPostBack()) Then
                ddlSociete.SelectedIndex = listeSociete.FindIndex(Function(x) x.Value = document.Entite) + 1
            Else
                ddlSociete.SelectedIndex = listeSociete.FindIndex(Function(x) x.Value = entiteDocument.Value) + 1
            End If
            'ddlSociete.SelectedValue = document.Entite

            AffichagePanneaux()

                If (Not Page.IsPostBack()) Then

                    'If (Request.UrlReferrer.ToString().IndexOf("TraitementHorsScope.aspx") = -1) Then
                    '    Session("page_precedente") = Request.UrlReferrer.ToString()
                    'End If

                    'ViewState("UrlReferent") = Request.UrlReferrer.ToString()

                    'Infos du modal d'alerte

                    litMotifsAlerte.Text = ""

                    statut = document.Statut.Id
                    iframePDF.Src = "handlers/VisualisationHorsScope.ashx?id=" & docid

                    Dim liste_motifs_alerte As List(Of ListItem) = DBFunctions.getMotifsAlerte(current_user)

                    For Each item As ListItem In liste_motifs_alerte
                        litMotifsAlerte.Text += "<option value=""" & item.Value & """>" & item.Text & "</option>"
                    Next

                    'Affichage des panneaux
                    AffichagePanneaux()

                    If (docid <> "" And statut <> "") Then

                        Dim liste_alertes_facture As List(Of AlerteFacture) = DBFunctions.getAlertesFacture(docid, current_user, False, "", True)

                        litAlertes.Text = ""

                        If (liste_alertes_facture.Count = 0) Then

                            litAlertes.Text = "<tr><td colspan=""7"" style=""text-align:center;"">Aucune alerte</td></tr>"

                        Else

                            For Each alerte As AlerteFacture In liste_alertes_facture

                                litAlertes.Text += "<tr class=""odd gradeX"">" &
                                                    "<td>" & alerte.Motif.Libelle & "</td>" &
                                                    "<td>" & alerte.Commentaire & "</td>" &
                                                    "<td>" & alerte.Emetteur.Prenom & " " & alerte.Emetteur.Nom & "</td>" &
                                                    "<td>" & alerte.DateAlerte & "</td>" &
                                                    "<td>" & alerte.ResoluPar.Prenom & " " & alerte.ResoluPar.Nom & "</td>" &
                                                    "<td>" & alerte.DateResolution & "</td>" &
                                                    "<td><input class=""checkbox_alerte_resolue"" type=""checkbox"" data-id=""" & alerte.IDAlerte & """ value=""2"" "

                                If (alerte.Statut.Id = "2") Then
                                    litAlertes.Text += " checked=""checked"" /></td></tr>"
                                Else
                                    litAlertes.Text += " /></td></tr>"
                                End If


                            Next

                        End If

                    End If

                Else

                    'Affichage des panneaux
                    AffichagePanneaux()

                End If
            Else

                Response.Redirect("Connexion.aspx")

        End If

    End Sub

    Private Sub AffichagePanneaux()

        'openModalConfirmValidationFacture.Visible = False

        If Not String.IsNullOrEmpty(statut) Then
            If statut = "7" Then
                pnlCorrection.Visible = True
                pnlCorrection.Enabled = False
                pnlAlertes.Visible = False
                pnlAlertes.Enabled = False
                ddlTypeDocument.Enabled = False

            Else
                pnlCorrection.Visible = True
                pnlCorrection.Enabled = True
                pnlAlertes.Visible = True
                pnlAlertes.Enabled = True
                'openModalConfirmValidationFacture.Visible = True
            End If
        End If
    End Sub

    Private Sub btnRetour_Click(sender As Object, e As EventArgs) Handles btnRetour.Click

        If (DBFunctions.setFlagOccupe(docid, "", current_user)) Then
            Response.Redirect(Session("page_precedente"))
        End If

    End Sub

    Private Sub btnValiderCorrection_Click(sender As Object, e As EventArgs) Handles btnValiderCorrection.Click
        Dim vClassif As String = ddlTypeDocument.SelectedItem.Value.ToString()
        Dim vEntite As String = ddlSociete.SelectedValue
        Dim vSite As String = ddlSociete.SelectedItem.Text
        Dim vStatut As String = ddlStatut2.SelectedItem.Value
        Dim vSite2 As String = ddlSite2.SelectedItem.Text

        DBFunctions.UpdateInfoDocumentHorsScope(current_user, docid, vClassif, vEntite, vSite, vSite2, vStatut)



        'CType(e.Items.FindControl("ddlInformations"), DropDownList)


    End Sub

End Class