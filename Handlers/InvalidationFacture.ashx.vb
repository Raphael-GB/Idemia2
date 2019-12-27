Imports System.Web
Imports System.Web.Services

Public Class InvalidationFacture
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then



            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)
            Dim docid, motif, commentaire As String
            Dim url_retour As String = ""
            Dim liste_histo_action As New List(Of Historique)
            Dim flag_envoi_valideur As Boolean

            If (Not (context.Request.Params("mDocId") Is Nothing _
                     Or context.Request.Params("mMotifInvalidite") Is Nothing _
                     Or context.Request.Params("mCommentaireInvalidite") Is Nothing)) Then

                docid = context.Request.Params("mDocId")
                motif = context.Request.Params("mMotifInvalidite")
                commentaire = context.Request.Params("mCommentaireInvalidite")

                If (docid <> "" And motif <> "") Then
                    DBFunctions.UpdateStatutFactureInvalide(docid, user, motif, commentaire)
                End If

                If (user.GestionValidation = "1" And user.CorbeilleValidation = "1") Then

                    'On cherche à savoir si il y a eu un envoi au valideur 
                    liste_histo_action = DBFunctions.getHistorique(user, docid)
                    flag_envoi_valideur = liste_histo_action.FindAll(Function(x) x.Action = "ENVOYE AU VALIDEUR").Count > 0

                    If (flag_envoi_valideur) Then

                        url_retour = "../CorbeilleValidation.aspx"

                    ElseIf (user.CorbeilleATraiter = "1") Then

                        url_retour = "../CorbeilleATraiter.aspx"

                    Else

                        url_retour = "../Connexion.aspx"

                    End If


                ElseIf (user.CorbeilleATraiter = "1") Then

                    url_retour = "../CorbeilleATraiter.aspx"

                Else

                    url_retour = "../Connexion.aspx"

                End If

                context.Response.Redirect(url_retour)

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