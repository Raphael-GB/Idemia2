Public Class ParametrageAlerte
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(Session("Utilisateur"), Utilisateur)

            'alertes automatiques

            Dim liste_des_champs As List(Of Champ) = DBFunctions.getChamps(user, "CORRECTION", "0")
            Dim liste_alertes As List(Of AlerteAutomatique) = DBFunctions.getAlertesAutomatiques(user, False)
            Dim chaine_attributs As String

            litAlertesAuto.Text = ""

            For Each ale As AlerteAutomatique In liste_alertes

                chaine_attributs = ""

                For i As Integer = 0 To ale.ListeAttributs.Count - 1

                    chaine_attributs += " data-champ" & (i + 1).ToString() & "=""" & ale.ListeAttributs.Item(i).Champ.IDChamp & """ " & _
                                        " data-operateur" & (i + 1).ToString() & "=""" & ale.ListeAttributs.Item(i).Operateur & """ " & _
                                        " data-valeur" & (i + 1).ToString() & "=""" & ale.ListeAttributs.Item(i).Valeur & """ "
                Next

                Dim statut_active As String = "Non"
                If (ale.Active = "1") Then
                    statut_active = "Oui"
                End If


                litAlertesAuto.Text += "<tr class=""odd gradeX"">" & _
                                        "<td>" & ale.IDAlerteAuto & "</td>" & _
                                        "<td data-id=""" & ale.IDAlerteAuto & """ data-active=""" & ale.Active & """ data-debut=""" & ale.Debut & """ data-duree=""" & ale.Duree & """ data-periodicite=""" & ale.Periodicite & """ data-libelle=""" & ale.Libelle & """" & chaine_attributs & ">" & ale.Libelle & "</td>" & _
                                        "<td data-id=""" & ale.IDAlerteAuto & """ data-active=""" & ale.Active & """ data-debut=""" & ale.Debut & """ data-duree=""" & ale.Duree & """ data-periodicite=""" & ale.Periodicite & """ data-libelle=""" & ale.Libelle & """" & chaine_attributs & ">" & statut_active & "</td>" & _
                                        "<td class=""suppression""><a id=""open-modal-confirm-suppression-alerte-auto"" href=""#"" data-id=""" & ale.IDAlerteAuto & """ data-toggle=""modal"" data-target=""#modal-confirm-suppression-alerte-auto""><i class=""fa fa-times""></i></a></td>" & _
                                    "</tr>"

            Next


            'champs du modal des attributs

            litChamps1.Text = "<option value="""" data-typage=""""></option>" & vbNewLine
            litChamps2.Text = "<option value="""" data-typage=""""></option>" & vbNewLine
            litChamps3.Text = "<option value="""" data-typage=""""></option>" & vbNewLine
            litChamps4.Text = "<option value="""" data-typage=""""></option>" & vbNewLine
            litChamps5.Text = "<option value="""" data-typage=""""></option>" & vbNewLine

            For Each ch As Champ In liste_des_champs

                litChamps1.Text += "<option value=""" & ch.IDChamp & """ data-typage=""" & ch.Typage & """ data-index=""1"" >" & ch.Description & "</option>" & vbNewLine
                litChamps2.Text += "<option value=""" & ch.IDChamp & """ data-typage=""" & ch.Typage & """ data-index=""2"" >" & ch.Description & "</option>" & vbNewLine
                litChamps3.Text += "<option value=""" & ch.IDChamp & """ data-typage=""" & ch.Typage & """ data-index=""3"" >" & ch.Description & "</option>" & vbNewLine
                litChamps4.Text += "<option value=""" & ch.IDChamp & """ data-typage=""" & ch.Typage & """ data-index=""4"" >" & ch.Description & "</option>" & vbNewLine
                litChamps5.Text += "<option value=""" & ch.IDChamp & """ data-typage=""" & ch.Typage & """ data-index=""5"" >" & ch.Description & "</option>" & vbNewLine

            Next

        Else

            Response.Redirect("Connexion.aspx")

        End If

    End Sub

End Class