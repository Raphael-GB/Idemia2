Imports System.Web
Imports System.Web.Services

Public Class GestionAlerte
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            If (Not context.Request.Params("modal-alerte-numFacture") Is Nothing And _
                Not context.Request.Params("modal-alerte-dateFacturation") Is Nothing And _
                Not context.Request.Params("modal-alerte-motif") Is Nothing And _
                Not context.Request.Params("modal-alerte-commentaire") Is Nothing And _
                Not context.Request.Params("modal-alerte-destinataires") Is Nothing And _
                Not context.Request.Params("modal-alerte-docid") Is Nothing) Then

                Dim num_facture As String = context.Request.Params("modal-alerte-numFacture").ToString()
                Dim date_facturation As String = context.Request.Params("modal-alerte-dateFacturation").ToString()
                Dim motif As String = context.Request.Params("modal-alerte-motif").ToString()
                Dim commentaire As String = context.Request.Params("modal-alerte-commentaire").ToString()
                Dim docid As String = context.Request.Params("modal-alerte-docid").ToString()
                Dim destinataires As String = context.Request.Params("modal-alerte-destinataires").ToString()

                If (docid.Trim() <> "" And motif.Trim() <> "" And destinataires.Trim() <> "") Then

                    Dim alerte_temp As New AlerteFacture()

                    alerte_temp.Workflow = current_user.Workflow
                    alerte_temp.Emetteur.Identifiant = current_user.Identifiant
                    alerte_temp.Emetteur.Email = current_user.Email
                    alerte_temp.Statut = New Statut("1", "")
                    alerte_temp.DocId = docid
                    alerte_temp.NumFacture = num_facture
                    alerte_temp.DateFacture = date_facturation
                    alerte_temp.Motif = New Motif(motif, "", "")
                    alerte_temp.Commentaire = commentaire

                    DBFunctions.InsertAlerteFacture(alerte_temp, destinataires)

                End If

                context.Response.Redirect(context.Request.UrlReferrer.ToString())

            Else

                context.Response.Redirect(context.Request.UrlReferrer.ToString())

            End If

        Else

            context.Response.Redirect("../Connexion.aspx")

        End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class