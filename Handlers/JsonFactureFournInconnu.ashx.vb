Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class JsonFactureFournInconnu
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            Dim filtre As String = context.Request.QueryString("filtre").ToString()
            Dim statut As String = ""
            'Dim statut As String = context.Request.QueryString("statut").ToString()
            Dim echo As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("sEcho").ToString())
            Dim displayLength As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("iDisplayLength").ToString())
            Dim displayStart As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("iDisplayStart").ToString())
            Dim sSearch As String = HttpContext.Current.Request.Params("sSearch").ToString()
            Dim iSortCol As Integer = Convert.ToInt32(HttpContext.Current.Request.Params("iSortCol_0").ToString())
            Dim iSortDir As String = HttpContext.Current.Request.Params("sSortDir_0").ToString()

            Dim serializer As New JavaScriptSerializer()

            HttpContext.Current.Session("filtre_statut_facture_a_traiter") = statut
            HttpContext.Current.Session("filtre_requete") = filtre

            'Tri pour la recherche

            Dim lignes As List(Of LigneFactureCorbeille) = getLignesFactures(current_user, filtre, statut).FindAll(Function(x) x.Site.Contains(sSearch) _
                                                                                                                Or x.Id.Contains(sSearch) _
                                                                                                                Or x.CodeFournisseur.Contains(sSearch) _
                                                                                                                Or x.NomFournisseur.Contains(sSearch) _
                                                                                                                Or x.NumFacture.Contains(sSearch) _
                                                                                                                Or x.DateFacture.Contains(sSearch) _
                                                                                                                Or x.Entite.Contains(sSearch) _
                                                                                                                Or x.MontantTTC.Contains(sSearch) _
                                                                                                                Or x.DateInsertion.Contains(sSearch) _
                                                                                                                Or x.MotifAlerte.Contains(sSearch) _
                                                                                                                Or x.EmetteurAlerte.Contains(sSearch) _
                                                                                                                Or x.Statut2.Contains(sSearch))

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
                    lignes = lignes.OrderBy(Function(x) x.MotifAlerte).ToList()
                ElseIf (iSortCol = 9) Then
                    lignes = lignes.OrderBy(Function(x) x.EmetteurAlerte).ToList()
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
                    lignes = lignes.OrderByDescending(Function(x) x.MotifAlerte).ToList()
                ElseIf (iSortCol = 9) Then
                    lignes = lignes.OrderByDescending(Function(x) x.EmetteurAlerte).ToList()
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

                For Each item As ListItem In DBFunctions.getStatutsFactures("ACTION")
                    liste_statuts.Add(item.Value)
                Next

            End If

            Dim liste_factures As List(Of Facture) = DBFunctions.getFactures(p_user, p_filtre, liste_statuts, True, False, True, "5", 0, "")
            Dim liste_alertes_facture As List(Of AlerteFacture) = DBFunctions.getAlertesFacture("", p_user, False, "")

            For Each fact As Facture In liste_factures

                Dim alertes_facture As List(Of AlerteFacture) = liste_alertes_facture.FindAll(Function(x) x.DocId = fact.IDFacture And x.Workflow = p_user.Workflow)

                Dim image_flag As String = ""
                Dim motif_alerte As String = ""
                Dim emetteur_alerte As String = ""
                Dim LigneFactureCorbeille As New LigneFactureCorbeille()

                If (alertes_facture.Count > 0) Then

                    'on prend en priorité l'alerte non résolue la plus ancienne.
                    Dim sortByStatut As New AlerteFacture_SortByStatut()
                    Dim sortByDate As New AlerteFacture_SortByDate()

                    alertes_facture.Sort(sortByStatut)
                    alertes_facture.Sort(sortByDate)

                    If (alertes_facture.Item(0).Statut.Id = "1") Then
                        image_flag = "<div id=""flag_alerte""><img src=""images/flag_red.png"" alt="""" /></div>"
                    ElseIf (alertes_facture.Item(0).Statut.Id = "2") Then
                        image_flag = "<div id=""flag_alerte""><img src=""images/flag_green.png"" alt="""" /></div>"
                    End If

                    motif_alerte = alertes_facture.Item(0).Motif.Libelle
                    emetteur_alerte = alertes_facture.Item(0).Emetteur.Prenom & " " & alertes_facture.Item(0).Emetteur.Nom

                End If

                LigneFactureCorbeille.Id = fact.IDFacture
                LigneFactureCorbeille.IdStatut = fact.Statut.Id
                LigneFactureCorbeille.CodeFournisseur = fact.CodeFournisseur
                LigneFactureCorbeille.NomFournisseur = fact.NomFournisseur
                LigneFactureCorbeille.NumFacture = fact.NumFacture
                LigneFactureCorbeille.DateFacture = fact.DateFacture
                LigneFactureCorbeille.Entite = fact.EntiteFacture
                LigneFactureCorbeille.MontantTTC = fact.MontantTTC
                LigneFactureCorbeille.DateInsertion = fact.DateInsertion.Substring(0, 10)
                LigneFactureCorbeille.FlagAlerte = image_flag
                LigneFactureCorbeille.MotifAlerte = motif_alerte
                LigneFactureCorbeille.EmetteurAlerte = emetteur_alerte
                LigneFactureCorbeille.Statut2 = fact.Statut2


                resultat.Add(LigneFactureCorbeille)

            Next


        Catch ex As Exception
            resultat = New List(Of LigneFactureCorbeille)
        End Try

        Return resultat

    End Function

End Class