Public Class Indicateurs
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim user = CType(Session("Utilisateur"), Utilisateur)

        If (Not user Is Nothing) Then

            If (Not Page.IsPostBack) Then

                dateDebut.Text = Convert.ToDateTime(user.DateDemarrage).ToShortDateString()
                dateFin.Text = DateTime.Now().ToString("dd/MM/yyyy")

                If (user.IDProfil = "1") Then

                    pnlSelectionUtilisateur.Visible = True

                    ddlUtilisateur.Items.Clear()
                    ddlUtilisateur.Items.Add(New ListItem("", ""))

                    For Each user_temp As Utilisateur In DBFunctions.getUtilisateurs(user)
                        ddlUtilisateur.Items.Add(New ListItem(user_temp.Prenom & " " & user_temp.Nom, user_temp.Identifiant))
                    Next

                Else

                    pnlSelectionUtilisateur.Visible = False

                End If

            End If

            'messages non lus
            litNbMessages.Text = DBFunctions.getMessages(user, "0", "0").Count().ToString()

            'événements à venir
            litNbEvenements.Text = DBFunctions.getEvenements(user, "1").Count().ToString()

            'taches à réaliser

            Dim tar As QuantitesCorbeilles = DBFunctions.getQuantitesCorbeilles(user)


            litHorsScope.Text = "<div class=""huge"">" & tar.NbHorsScope.ToString & "</div><div>Hors scope</div>"

            litTachesATraiter.Text = "<div class=""huge"">" & tar.NbCorbeilleRejet.ToString & "</div><div>rejetés</div>"
                    litTachesAValider.Text = "<div class=""huge"">" & (tar.NbCorbeilleCommande + tar.NbCorbeilleFourInconnu + tar.NbCorbeilleFourMultiple).ToString() & "</div><div>invalides</div>"

            litLienTaches.Text = "<a href=""CorbeilleRejet.aspx"">" &
                                            "<div class=""panel-footer"">" &
                                                "<span class=""pull-left"">Voir le détail des rejets</span>" &
                                                "<span class=""pull-right""><i class=""fa fa-arrow-circle-right""></i></span>" &
                                                 "<div class=""clearfix""></div>" &
                                            "</div>" &
                                         "</a>" &
                                         "<a href=""CorbeilleFourInconnu.aspx"">" &
                                            "<div class=""panel-footer"">" &
                                                "<span class=""pull-left"">Voir le détail ""Fournisseur Inconnu""</span>" &
                                                "<span class=""pull-right""><i class=""fa fa-arrow-circle-right""></i></span>" &
                                                 "<div class=""clearfix""></div>" &
                                            "</div>" &
                                         "</a>" &
                                        "<a href=""CorbeilleFourMultiple.aspx"">" &
                                            "<div class=""panel-footer"">" &
                                                "<span class=""pull-left"">Voir le détail ""Fournisseur Multiple""</span>" &
                                                "<span class=""pull-right""><i class=""fa fa-arrow-circle-right""></i></span>" &
                                                 "<div class=""clearfix""></div>" &
                                            "</div>" &
                                         "</a>" &
                                         "<a href=""CorbeilleCommande.aspx"">" &
                                            "<div class=""panel-footer"">" &
                                                "<span class=""pull-left"">Voir le détail des commande 099PU</span>" &
                                                "<span class=""pull-right""><i class=""fa fa-arrow-circle-right""></i></span>" &
                                                 "<div class=""clearfix""></div>" &
                                            "</div>" &
                                         "</a>"


            Session("statut_validation") = "9"


            litAlertesFacture.Text = "<div class=""huge"">" & DBFunctions.getAlertesFacture("", user, True, "1").Count().ToString() & "</div><div>factures</div>"
            litAlertesHs.Text = "<div class=""huge"">" & DBFunctions.getAlertesFacture("", user, True, "1", True).Count().ToString() & "</div><div>documents</div>"

            litLienAlertes.Text = "<a href=""Alerte.aspx"">" &
                                            "<div class=""panel-footer"">" &
                                                "<span class=""pull-left"">Voir les alertes ""Factures""</span>" &
                                                "<span class=""pull-right""><i class=""fa fa-arrow-circle-right""></i></span>" &
                                                 "<div class=""clearfix""></div>" &
                                            "</div>" &
                                         "</a>" &
                                         "<a href=""AlerteHorsScope.aspx"">" &
                                            "<div class=""panel-footer"">" &
                                                "<span class=""pull-left"">Voir les alertes ""Hors-Scope""</span>" &
                                                "<span class=""pull-right""><i class=""fa fa-arrow-circle-right""></i></span>" &
                                                 "<div class=""clearfix""></div>" &
                                            "</div>" &
                                             "</a>"


        Else
            Response.Redirect("Connexion.aspx")
        End If

    End Sub

End Class