Public Class Absence
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(Session("Utilisateur"), Utilisateur)
            Dim liste_absence As List(Of Absences)
            Dim libelle_destinataires As String = ""


            Dim list As New List(Of ListItem)
            Dim options As String = ""
            If (user.IDProfil = 1) Then
                list = DBFunctions.getUtilisateursFDJ(user)
            Else
                list.Add(New ListItem($"{user.Prenom} {user.Nom}", user.Identifiant))
            End If

            For Each item In list
                options += $"<option value='{item.Value}'>{item.Text}</option>"
            Next

            lit_option.Text = options
            'ddlUtilisateur.Text = $"<select id='ddlUtilisateur' class='form-control'>{options}</select>"

            'Liste des messages
            liste_absence = DBFunctions.getAbsence(user, "0")
            litMessages.Text = ""

            For Each abs As Absences In liste_absence

                libelle_destinataires = ""

                For Each dest In abs.Destinataires

                    If ((dest.Nom & dest.Prenom).Trim() <> "") Then
                        libelle_destinataires += dest.Prenom & " " & dest.Nom & "<br/>"
                    Else
                        libelle_destinataires += dest.Email & "<br/>"
                    End If

                Next

                litMessages.Text += "<tr class=""odd gradeX"">" &
                                    "<td>" & abs.IDAbsence & "</td>" &
                                    "<td>" & libelle_destinataires & "</td>" &
                                    "<td>" & abs.DateDebut & "</td>" &
                                    "<td>" & abs.DateFin & "</td>" &
                                    "<td class=""suppression""><a id=""open-modal-confirm-suppression-absence"" href=""#"" data-id=""" & abs.IDAbsence & """ data-toggle=""modal"" data-target=""#modal-confirm-suppression-absence""><i class=""fa fa-times""></i></a></td>" &
                                "</tr>"
            Next

        Else

            Response.Redirect("Connexion.aspx")

        End If

    End Sub

End Class