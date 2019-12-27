Public Class BoiteEnvoi
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(Session("Utilisateur"), Utilisateur)
            Dim liste_messages As List(Of Message)
            Dim libelle_destinataires As String = ""

            'Liste des messages

            liste_messages = DBFunctions.getMessages(user, "1", "")
            litMessages.Text = ""

            For Each msg As Message In liste_messages

                libelle_destinataires = ""

                For Each dest In msg.Destinataires

                    If ((dest.Nom & dest.Prenom).Trim() <> "") Then
                        libelle_destinataires += dest.Prenom & " " & dest.Nom & "<br/>"
                    Else
                        libelle_destinataires += dest.Email & "<br/>"
                    End If

                Next

                litMessages.Text += "<tr class=""odd gradeX"">" & _
                                        "<td>" & msg.IDMessage & "</td>" & _
                                        "<td>" & libelle_destinataires & "</td>" & _
                                        "<td>" & msg.DateMessage & "</td>" & _
                                        "<td>" & msg.Sujet & "</td>" & _
                                        "<td class=""suppression""><a id=""open-modal-confirm-suppression-message"" href=""#"" data-id=""" & msg.IDMessage & """ data-toggle=""modal"" data-target=""#modal-confirm-suppression-message""><i class=""fa fa-times""></i></a></td>" & _
                                    "</tr>"
            Next

        Else

            Response.Redirect("Connexion.aspx")

        End If

    End Sub

End Class