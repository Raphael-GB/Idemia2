Public Class AmeliorationContinue
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If (Not Session("Utilisateur") Is Nothing) Then

            Dim current_user As Utilisateur = CType(Session("Utilisateur"), Utilisateur)

            If (Not Page.IsPostBack) Then

                dateDebut.Text = Convert.ToDateTime(current_user.DateDemarrage).ToString("dd/MM/yyyy")
                dateFin.Text = DateTime.Now().ToString("dd/MM/yyyy")

            End If

            'Dim liste_statuts As New List(Of String)
            'liste_statuts.Add("")

            'Dim liste_factures As List(Of Facture) = DBFunctions.getFactures(current_user, "", liste_statuts, False)

            'litFacturesCorrigees.Text = ""

            'For Each fact As Facture In liste_factures

            '    litFacturesCorrigees.Text = "<tr class=""odd gradeX"">" & _
            '                                "<td>1</td>" & _
            '                                "<td>Fournisseur 1</td>" & _
            '                                "<td>1234</td>" & _
            '                                "<td>01/01/2017</td>" & _
            '                                "<td>02/01/2017</td>" & _
            '                                "<td>03/01/2017</td>" & _
            '                                "<td>04/01/2017</td>" & _
            '                                "<td>F/P/C</td>" & _
            '                                "<td>3</td>" & _
            '                                "<td>0</td>" & _
            '                                "<td>0</td>" & _
            '                                "<td>0</td>" & _
            '                            "</tr>"

            'Next

            

        Else

            Response.Redirect("Connexion.aspx")

        End If


    End Sub

End Class