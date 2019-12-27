Public Class CorbeilleHorsScope
    Inherits System.Web.UI.Page

    Dim liste_statuts As New List(Of String)
    Dim current_user As Utilisateur

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        current_user = CType(Session("Utilisateur"), Utilisateur)

        If (Not current_user Is Nothing) Then

            If (Not Page.IsPostBack()) Then

                'chargement des filtres
                Dim liste As New List(Of ListItem)
                liste = DBFunctions.getTypeDocument(current_user)

                ddlFiltres.DataTextField = "Text"
                ddlFiltres.DataValueField = "Value"

                ddlFiltres.DataSource = liste
                ddlFiltres.DataBind()

                ddlFiltres.Items.Insert(0, New ListItem("", ""))

                ddlFiltres.SelectedValue = Session("filtre_requete_hors_scope")


            End If

        Else

            Response.Redirect("Connexion.aspx")

        End If

    End Sub

End Class