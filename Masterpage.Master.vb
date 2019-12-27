Public Class Site1
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1))
        HttpContext.Current.Response.Cache.SetValidUntilExpires(False)
        HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches)
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)
        HttpContext.Current.Response.Cache.SetNoStore()

        Dim user = CType(Session("Utilisateur"), Utilisateur)
        Dim limite, cpt As Integer

        limite = 5
        cpt = 0

        If (Not user Is Nothing) Then
            litCorbeille.Text = ""
            litCorbeilleBis.Text = ""
            litAbsence.Text = ""
            litPerformances.Text = "<li>" &
                                    "<a href = '#'><i Class='fa fa-line-chart fa-fw'></i>&nbsp;Performances</a>" &
                                    "<ul class='nav nav-second-level'>" &
                                        "<li>" &
                                            "<a href = 'Indicateurs.aspx' > Indicateurs</a>" &
                                        "</li>" &
                                        "<li>" &
                                            "<a href = 'AmeliorationContinue.aspx' > Amelioration Continue</a>" &
                                        "</li>" &
                                    "</ul>" &
                                 "</li>"

            'litAlertes.Text = "<li>" &
            '                " <a href='Alerte.aspx'><i class='fa fa-bell-o fa-fw'></i>&nbsp;Mes alertes</a>" &
            '                "</li>"

            litAlertes.Text = "<li>" &
                                "<a href=""#""><i class=""fa fa-bell-o fa-fw""></i>&nbsp;Mes alertes<span class=""fa arrow""></span></a>" &
                                    "<ul class=""nav nav-second-level"">" &
                                        "<li>" &
                                            "<a href=""Alerte.aspx"">Alertes Factures</a>" &
                                        "</li>" &
                                        "<li>" &
                                            "<a href=""AlerteHorsScope.aspx"">Alertes Hors-Scope</a>" &
                                        "</li>" &
                                    "</ul>" &
                                "</li>"

            If (user.IDProfil = "1") Then

                'administrateur
                litParametrage.Text = "<li>" &
                                "<a href=""#""><i class=""fa fa-sliders fa-fw""></i>&nbsp;Paramétrages<span class=""fa arrow""></span></a>" &
                                    "<ul class=""nav nav-second-level"">" &
                                        "<li>" &
                                            "<a href=""ParametrageFiltre.aspx"">Filtres de sélection</a>" &
                                        "</li>" &
                                        "<li>" &
                                            "<a href=""ParametrageAlerteAuto.aspx"">Alertes automatiques</a>" &
                                        "</li>" &
                                        "<li>" &
                                            "<a href=""ParametrageUtilisateur.aspx"">Utilisateurs</a>" &
                                        "</li>" &
                                    "</ul>" &
                                "</li>"

            ElseIf (user.IDProfil = "2") Then

                Dim badge As QuantitesCorbeilles = DBFunctions.getQuantitesCorbeilles(user)


                'utilisateur

                litParametrage.Text = ""

                'litCorbeille.Text = "<li>" &
                '                        "<a href=""#""><i class=""fa fa-trash-o fa-fw""></i>&nbsp;Corbeilles<span class=""fa arrow""></span></a>" &
                '                        "<ul class=""nav nav-second-level"">"

                litCorbeille.Text = "<li>" &
                                 "<a href=""#""><i class=""fa fa-trash-o fa-fw""></i>&nbsp;Bannettes<span class=""fa arrow""></span></a>" &
                                    "<ul class=""nav nav-second-level"">" &
                                        "<li>" &
                                            "<a href=""CorbeilleRejet.aspx"">Factures rejetées<span Class=""badge badge-pill badge-perso pull-right"">" & badge.NbCorbeilleRejet & "</span></a> " &
                                        "</li>" &
                                        "<li>" &
                                             "<a href=""CorbeilleFournInconnu.aspx"">Fournisseur non reconnu <span Class=""badge badge-pill badge-perso pull-right"">" & badge.NbCorbeilleFourInconnu & "</span></a> " &
                                        "</li>" &
                                        "<li>" &
                                             "<a href=""CorbeilleFournMultiple.aspx"">Fournisseur multiple <span Class=""badge badge-pill badge-perso pull-right"">" & badge.NbCorbeilleFourMultiple & "</span></a> " &
                                        "</li>" &
                                        "<li>" &
                                             "<a href=""CorbeilleCommande.aspx"">Factures 099PU  <span Class=""badge badge-pill badge-perso pull-right"">" & badge.NbCorbeilleCommande & "</span></a>" &
                                        "</li>" &
                                        "<li>" &
                                             "<a href=""CorbeilleEgencia.aspx"">Factures Egencia  <span Class=""badge badge-pill badge-perso pull-right"">" & badge.NbCorbeilleEgencia & "</span></a>" &
                                        "</li>" &
                                        "<li>" &
                                            "<a href=""#"">Archives<span class=""fa arrow""></span></a>" &
                                                               "<ul class=""nav nav-third-level"">" &
                                                "<li>" &
                                                    "<a href=""Archives.aspx"">Archives</a>" &
                                                "</li>" &
                                                "<li>" &
                                                    "<a href=""Archives099PU.aspx"">Archives 099PU</a>" &
                                                "</li>" &
                                                   "<li>" &
                                                    "<a href=""ArchivesEgencia.aspx"">Archives Egencia</a>" &
                                                "</li>" &
                                                 "<li>" &
                                                    "<a href=""ArchivesRejet.aspx"">Archives Rejetées</a>" &
                                                "</li>" &
                                            "</ul>" &
                                                            "</li>" &
                                     "</ul>" & "</li>"



                litCorbeilleBis.Text = "<li>" &
                                 "<a href=""#""><i class=""fa fa-file-o fa-fw""></i>&nbsp;Documents Hors Scope<span class=""fa arrow""></span></a>" &
                                    "<ul class=""nav nav-second-level"">" &
                                        "<li>" &
                                            "<a href=""CorbeilleHorsScope.aspx"">A traiter  <span Class=""badge badge-pill badge-perso pull-right"">" & badge.NbHorsScope & "</span></a>" &
                                        "</li>" &
                                        "<li>" &
                                            "<a href=""ArchivesHorsScope.aspx"">Archives</a>" &
                                        "</li>" &
                                    "</ul>" &
                                "</li>"




            End If


            'Nom de l'utilisateur
            litNomUtilisateur.Text = user.Civilite.Libelle & " " & user.Prenom & " " & user.Nom


        Else
            Response.Redirect("Connexion.aspx")
        End If

    End Sub

End Class