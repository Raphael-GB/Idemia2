Public Class ParametrageUtilisateur
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(Session("Utilisateur"), Utilisateur)
            Dim liste_utilisateurs As List(Of Utilisateur)

            'Liste des utilisateurs

            liste_utilisateurs = DBFunctions.getUtilisateurs(user)
            litUtilisateurs.Text = ""

            For Each util As Utilisateur In liste_utilisateurs

                litUtilisateurs.Text += "<tr class=""odd gradeX"">" & _
                                        "<td>" & util.Identifiant & "</td>" & _
                                        "<td>" & util.Nom & "</td>" & _
                                        "<td>" & util.Prenom & "</td>" & _
                                        "<td>" & util.Email & "</td>" & _
                                        "<td class=""suppression""><a id=""open-modal-confirm-suppression-utilisateur"" href=""#"" data-id=""" & util.Identifiant & """ data-toggle=""modal"" data-target=""#modal-confirm-suppression-utilisateur""><i class=""fa fa-times""></i></a></td>" & _
                                    "</tr>"
            Next

        Else

            Response.Redirect("Connexion.aspx")

        End If

    End Sub

End Class