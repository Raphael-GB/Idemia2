Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class JsonArchivesHorsScope
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

            HttpContext.Current.Session("filtre_statut_hors_scope") = statut
            HttpContext.Current.Session("filtre_requete_hors_scope") = filtre

            'Tri pour la recherche

            Dim statut_recherche As New List(Of String)

            statut_recherche.Add("7")

            Dim lignes As List(Of DocumentHorsScope) = DBFunctions.getListDocumentHorsScope(current_user, filtre, statut_recherche).FindAll(Function(x) x.Id.Contains(sSearch) _
                                                                                                                                      Or x.Type.Contains(sSearch) _
                                                                                                                                      Or x.Site.Contains(sSearch) _
                                                                                                                                      Or x.DateInsertion.Contains(sSearch))


            'tri pour l'ordre
            If (iSortDir = "asc") Then

                If (iSortCol = 0) Then
                    lignes = lignes.OrderBy(Function(x) x.Id).ToList()
                ElseIf (iSortCol = 1) Then
                    lignes = lignes.OrderBy(Function(x) x.Type).ToList()
                ElseIf (iSortCol = 2) Then
                    lignes = lignes.OrderBy(Function(x) x.Site).ToList()
                ElseIf (iSortCol = 3) Then
                    lignes = lignes.OrderBy(Function(x) x.DateInsertion).ToList()
                End If

            Else

                If (iSortCol = 0) Then
                    lignes = lignes.OrderByDescending(Function(x) x.Id).ToList()
                ElseIf (iSortCol = 1) Then
                    lignes = lignes.OrderByDescending(Function(x) x.Type).ToList()
                ElseIf (iSortCol = 2) Then
                    lignes = lignes.OrderByDescending(Function(x) x.Site).ToList()
                ElseIf (iSortCol = 3) Then
                    lignes = lignes.OrderByDescending(Function(x) x.DateInsertion).ToList()
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

            Dim lignes_a_afficher As List(Of DocumentHorsScope) = lignes.GetRange(itemsToSkip, displayLength).ToList()

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


End Class