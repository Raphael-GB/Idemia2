Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class JsonValidations
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            Dim statut As String = context.Request.QueryString("statut").ToString()
            Dim echo As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("sEcho").ToString())
            Dim displayLength As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("iDisplayLength").ToString())
            Dim displayStart As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("iDisplayStart").ToString())
            Dim sSearch As String = HttpContext.Current.Request.Params("sSearch").ToString()
            Dim iSortCol As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("iSortCol_0").ToString())
            Dim iSortDir As String = HttpContext.Current.Request.Params("sSortDir_0").ToString()

            Dim serializer As New JavaScriptSerializer()

            'Tri pour la recherche

            Dim lignes As List(Of LigneFactureCorbeille) = getLignesFactures(current_user, "", statut).FindAll(Function(x) x.Id.Contains(sSearch) _
                                                                                                                Or x.CodeFournisseur.Contains(sSearch) _
                                                                                                                Or x.NomFournisseur.Contains(sSearch) _
                                                                                                                Or x.NumFacture.Contains(sSearch) _
                                                                                                                Or x.DateFacture.Contains(sSearch) _
                                                                                                                Or x.Entite.Contains(sSearch) _
                                                                                                                Or x.MontantTTC.Contains(sSearch) _
                                                                                                                Or x.DateInsertion.Contains(sSearch) _
                                                                                                                Or x.DateEnvoiValideur.Contains(sSearch) _
                                                                                                                Or x.DateRetourValideur.Contains(sSearch) _
                                                                                                                Or x.Statut.Contains(sSearch) _
                                                                                                                Or x.MotifInvalidite.Contains(sSearch))

            'tri pour l'ordre
            If (iSortDir = "asc") Then

                If (iSortCol = 0) Then
                    lignes = lignes.OrderBy(Function(x) x.Id).ToList()
                ElseIf (iSortCol = 1) Then
                    lignes = lignes.OrderBy(Function(x) x.CodeFournisseur).ToList()
                ElseIf (iSortCol = 2) Then
                    lignes = lignes.OrderBy(Function(x) x.NomFournisseur).ToList()
                ElseIf (iSortCol = 3) Then
                    lignes = lignes.OrderBy(Function(x) x.NumFacture).ToList()
                ElseIf (iSortCol = 4) Then
                    lignes = lignes.OrderBy(Function(x) x.DateFacture).ToList()
                ElseIf (iSortCol = 5) Then
                    lignes = lignes.OrderBy(Function(x) x.Entite).ToList()
                ElseIf (iSortCol = 6) Then
                    lignes = lignes.OrderBy(Function(x) x.MontantTTC).ToList()
                ElseIf (iSortCol = 7) Then
                    lignes = lignes.OrderBy(Function(x) x.DateInsertion).ToList()
                ElseIf (iSortCol = 8) Then
                    lignes = lignes.OrderBy(Function(x) x.DateEnvoiValideur).ToList()
                ElseIf (iSortCol = 9) Then
                    lignes = lignes.OrderBy(Function(x) x.DateRetourValideur).ToList()
                ElseIf (iSortCol = 10) Then
                    lignes = lignes.OrderBy(Function(x) x.Statut).ToList()
                ElseIf (iSortCol = 11) Then
                    lignes = lignes.OrderBy(Function(x) x.MotifInvalidite).ToList()
                End If

            Else

                If (iSortCol = 0) Then
                    lignes = lignes.OrderByDescending(Function(x) x.Id).ToList()
                ElseIf (iSortCol = 1) Then
                    lignes = lignes.OrderByDescending(Function(x) x.CodeFournisseur).ToList()
                ElseIf (iSortCol = 2) Then
                    lignes = lignes.OrderByDescending(Function(x) x.NomFournisseur).ToList()
                ElseIf (iSortCol = 3) Then
                    lignes = lignes.OrderByDescending(Function(x) x.NumFacture).ToList()
                ElseIf (iSortCol = 4) Then
                    lignes = lignes.OrderByDescending(Function(x) x.DateFacture).ToList()
                ElseIf (iSortCol = 5) Then
                    lignes = lignes.OrderByDescending(Function(x) x.Entite).ToList()
                ElseIf (iSortCol = 6) Then
                    lignes = lignes.OrderByDescending(Function(x) x.MontantTTC).ToList()
                ElseIf (iSortCol = 7) Then
                    lignes = lignes.OrderByDescending(Function(x) x.DateInsertion).ToList()
                ElseIf (iSortCol = 8) Then
                    lignes = lignes.OrderByDescending(Function(x) x.DateEnvoiValideur).ToList()
                ElseIf (iSortCol = 9) Then
                    lignes = lignes.OrderByDescending(Function(x) x.DateRetourValideur).ToList()
                ElseIf (iSortCol = 10) Then
                    lignes = lignes.OrderByDescending(Function(x) x.Statut).ToList()
                ElseIf (iSortCol = 11) Then
                    lignes = lignes.OrderByDescending(Function(x) x.MotifInvalidite).ToList()
                End If

            End If

            Dim itemsToSkip As Integer = 0

            If (displayStart = 0) Then
                itemsToSkip = 0
            Else
                itemsToSkip = displayStart
            End If

            If (itemsToSkip + displayLength > lignes.Count()) Then
                displayLength = lignes.Count - itemsToSkip
            End If

            'tri pour la pagination

            Dim lignes_a_afficher As List(Of LigneFactureCorbeille) = lignes.GetRange(itemsToSkip, displayLength).ToList()

            Dim serializedResult As String = serializer.Serialize(lignes_a_afficher)

            serializedResult = "{""sEcho"":""" & echo & """,""recordsTotal"":" & lignes.Count() & ",""recordsFiltered"":" & lignes.Count() & ",""iTotalRecords"":" & lignes.Count() & ",""iTotalDisplayRecords"":" & lignes.Count() & ",""aaData"":" & serializedResult & "}"

            context.Response.Write(serializedResult)

        Else

            context.Response.Redirect("../Connexion.aspx")

        End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Private Function getLignesFactures(p_user As Utilisateur, p_filtre As String, p_statut As String) As List(Of LigneFactureCorbeille)

        Dim resultat As New List(Of LigneFactureCorbeille)

        Try

            Dim liste_statuts As New List(Of String)

            If (p_statut <> "") Then

                liste_statuts.Add(p_statut)

            Else

                For Each item As ListItem In DBFunctions.getStatutsFactures("SUIVI_VALIDEUR")
                    liste_statuts.Add(item.Value)
                Next

            End If

            Dim liste_factures As List(Of Facture) = DBFunctions.getFactures(p_user, p_filtre, liste_statuts, True, False, False, "", 0, "")

            For Each fact As Facture In liste_factures

                Dim image_flag As String = ""
                Dim motif_alerte As String = ""
                Dim emetteur_alerte As String = ""
                Dim LigneFactureCorbeille As New LigneFactureCorbeille()

                LigneFactureCorbeille.Id = fact.IDFacture
                LigneFactureCorbeille.IdStatut = fact.Statut.Id
                LigneFactureCorbeille.Statut = fact.Statut.Libelle
                LigneFactureCorbeille.CodeFournisseur = fact.CodeFournisseur
                LigneFactureCorbeille.NomFournisseur = fact.NomFournisseur
                LigneFactureCorbeille.NumFacture = fact.NumFacture
                LigneFactureCorbeille.DateFacture = fact.DateFacture
                LigneFactureCorbeille.Entite = fact.EntiteFacture
                LigneFactureCorbeille.MontantTTC = fact.MontantTTC

                If (fact.DateInsertion.Length >= 10) Then
                    LigneFactureCorbeille.DateInsertion = fact.DateInsertion.Substring(0, 10)
                End If

                If (fact.DateEnvoiValideur.Length >= 10) Then
                    LigneFactureCorbeille.DateEnvoiValideur = fact.DateEnvoiValideur.Substring(0, 10)
                End If

                If (fact.DateRetourValideur.Length >= 10) Then
                    LigneFactureCorbeille.DateRetourValideur = fact.DateRetourValideur.Substring(0, 10)
                End If

                LigneFactureCorbeille.MotifInvalidite = fact.MotifInvalidite.Libelle
                LigneFactureCorbeille.CommentaireInvalidite = fact.CommentaireInvalidite

                resultat.Add(LigneFactureCorbeille)

            Next


        Catch ex As Exception
            resultat = New List(Of LigneFactureCorbeille)
        End Try

        Return resultat

    End Function

End Class