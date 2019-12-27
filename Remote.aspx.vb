Imports System.Collections.Specialized
Imports System.Configuration

Public Class Remote
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim headers As NameValueCollection = Request.Headers
        Dim identifiant_sso As String = ""


        For i As Integer = 0 To headers.Count - 1

            Dim key As String = headers.GetKey(i)
            Dim value As String = headers.Get(i)

            If (key = "SAMACCOUNTNAME_HEADER") Then
                identifiant_sso = value
            End If

            'Response.Write(key + " = " + value + "<br/>")

        Next

        Dim chaine_connexion = ConfigurationManager.AppSettings("chaine_connexion").ToString()

        If (identifiant_sso <> "" And chaine_connexion <> "") Then

            Dim current_user As Utilisateur = DBFunctions.connexion_sso(identifiant_sso, chaine_connexion)

            If (Not current_user Is Nothing) Then

                DBFunctions.connexion_historique(current_user, "C")

                Session("Utilisateur") = current_user
                Session("filtre_statut_facture_a_traiter") = ""
                Session("filtre_statut_facture_archive") = ""
                Session("filtre_requete") = ""
                Session("page_precedente") = ""
                Session("statut_validation") = ""

                If (current_user.Workflow = "65") Then
                    Response.Redirect("FDJ_Performances.aspx")
                Else
                    Response.Redirect("Indicateurs.aspx")
                End If

            Else

                Response.Redirect("Connexion.aspx")

            End If

        Else

            Response.Redirect("Connexion.aspx")

        End If


    End Sub

End Class