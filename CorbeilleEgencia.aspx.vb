
Public Class CorbeilleEGENCIA
    Inherits System.Web.UI.Page

    Dim liste_statuts As New List(Of String)
    Dim current_user As Utilisateur


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        current_user = CType(Session("Utilisateur"), Utilisateur)

        If (Not current_user Is Nothing) Then

            If (Not Page.IsPostBack()) Then

                'chargement des rejets

                ddlRejet.DataTextField = "Text"
                ddlRejet.DataValueField = "Value"

                ddlRejet.DataSource = DBFunctions.getListeRejets(current_user)
                ddlRejet.DataBind()

                ddlRejet.Items.Insert(0, New ListItem("", ""))

                ddlRejet.SelectedValue = Session("filtre_rejet")

            End If

        Else

            Response.Redirect("Connexion.aspx")

        End If

    End Sub


End Class