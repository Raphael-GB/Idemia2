Public Class Message1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not IsPostBack) Then
            ViewState("UrlReferent") = Request.UrlReferrer.ToString()
        End If

        If (Not Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(Session("Utilisateur"), Utilisateur)
            Dim id_message As String
            Dim msg As Message

            If (Not Request.QueryString("id") Is Nothing) Then

                id_message = Request.QueryString("id").ToString()

                msg = DBFunctions.getInfosMessage(id_message, user.Identifiant)

                If (Not msg Is Nothing) Then

                    litSujetMessage.Text = msg.Sujet
                    litTexteMessage.Text = msg.Texte
                    txtDateMessage.Text = msg.DateMessage
                    txtExpediteurMessage.Text = ""
                    txtDestinatairesMessage.Text = ""

                    If ((msg.Expediteur.Nom & msg.Expediteur.Prenom).Trim() <> "") Then
                        txtExpediteurMessage.Text = msg.Expediteur.Prenom & " " & msg.Expediteur.Nom
                    Else
                        txtExpediteurMessage.Text = msg.Expediteur.Email
                    End If

                    HiddenExpediteur.Value = msg.Expediteur.Email
                    HiddenSujet.Value = msg.Sujet
                    HiddenID.value = msg.IDMessage

                    For Each dest As Contact In msg.Destinataires
                        If ((dest.Nom & dest.Prenom).Trim() <> "") Then
                            txtDestinatairesMessage.Text = dest.Prenom & " " & dest.Nom
                        Else
                            txtDestinatairesMessage.Text = dest.Email
                        End If
                    Next

                    'Mettre le message comme Lu
                    DBFunctions.SetLectureMessage(msg.IDMessage, msg.IdentifiantUtilisateur, "1")

                Else

                    Response.Redirect(ViewState("UrlReferent"))

                End If

            End If

        Else

            Response.Redirect("Connexion.aspx")

        End If

    End Sub

    Private Sub btnRetour_Click(sender As Object, e As EventArgs) Handles btnRetour.Click

        Response.Redirect(ViewState("UrlReferent"))

    End Sub

End Class