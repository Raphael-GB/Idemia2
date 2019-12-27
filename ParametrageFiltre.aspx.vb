Public Class ParametrageFiltre
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(Session("Utilisateur"), Utilisateur)

            'filtres

            Dim liste_des_champs As List(Of Champ) = DBFunctions.getChamps(user, "CORRECTION", "0")
            Dim liste_filtres As List(Of Filtre) = DBFunctions.getFiltres(user, False)
            Dim chaine_attributs As String

            litFiltres.Text = ""

            For Each fil As Filtre In liste_filtres

                chaine_attributs = ""

                For i As Integer = 0 To fil.ListeAttributs.Count - 1

                    chaine_attributs += " data-champ" & (i + 1).ToString() & "=""" & fil.ListeAttributs.Item(i).Champ.IDChamp & """ " & _
                                        " data-operateur" & (i + 1).ToString() & "=""" & fil.ListeAttributs.Item(i).Operateur & """ " & _
                                        " data-valeur" & (i + 1).ToString() & "=""" & fil.ListeAttributs.Item(i).Valeur & """ "
                Next


                litFiltres.Text += "<tr class=""odd gradeX"">" & _
                                        "<td>" & fil.IDFiltre & "</td>" & _
                                        "<td data-id=""" & fil.IDFiltre & """ data-libelle=""" & fil.Libelle & """" & chaine_attributs & ">" & fil.Libelle & "</td>" & _
                                        "<td class=""suppression""><a id=""open-modal-confirm-suppression-filtre"" href=""#"" data-id=""" & fil.IDFiltre & """ data-toggle=""modal"" data-target=""#modal-confirm-suppression-filtre""><i class=""fa fa-times""></i></a></td>" & _
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