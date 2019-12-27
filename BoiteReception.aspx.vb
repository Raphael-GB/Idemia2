Public Class Messagerie
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(Session("Utilisateur"), Utilisateur)
            Dim liste_messages As List(Of Message)
            Dim libelle_expediteur As String

            'Liste des messages

            liste_messages = DBFunctions.getMessages(user, "0", "")
            litMessages.Text = ""

            For Each msg As Message In liste_messages

                If ((msg.Expediteur.Nom & msg.Expediteur.Prenom).Trim() <> "") Then
                    libelle_expediteur = msg.Expediteur.Prenom & " " & msg.Expediteur.Nom
                Else
                    libelle_expediteur = msg.Expediteur.Email
                End If

                litMessages.Text += "<tr class=""odd gradeX"">" & _
                                        "<td>" & msg.IDMessage & "</td>" & _
                                        "<td>" & libelle_expediteur & "</td>" & _
                                        "<td>" & msg.DateMessage & "</td>" & _
                                        "<td>" & msg.Sujet & "</td>" & _
                                        "<td>" & msg.LibelleLecture & "</td>" & _
                                        "<td class=""suppression""><a id=""open-modal-confirm-suppression-message"" href=""#"" data-id=""" & msg.IDMessage & """ data-toggle=""modal"" data-target=""#modal-confirm-suppression-message""><i class=""fa fa-times""></i></a></td>" & _
                                    "</tr>"
            Next

        Else

            Response.Redirect("Connexion.aspx")

        End If


    End Sub

End Class