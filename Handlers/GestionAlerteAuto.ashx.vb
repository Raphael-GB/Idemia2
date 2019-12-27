Imports System.Web
Imports System.Web.Services

Public Class GestionAlerteAuto
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            If (Not context.Request.Params("modal-alerte-auto-id") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-libelle") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-champ-1") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-champ-2") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-champ-3") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-champ-4") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-champ-5") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-operateur-1") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-operateur-2") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-operateur-3") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-operateur-4") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-operateur-5") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-valeur-1") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-valeur-2") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-valeur-3") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-valeur-4") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-valeur-5") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-duree") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-periodicite") Is Nothing And _
                Not context.Request.Params("modal-alerte-auto-debut") Is Nothing) Then

                Dim id As String = context.Request.Params("modal-alerte-auto-id").ToString()
                Dim libelle As String = context.Request.Params("modal-alerte-auto-libelle").ToString()
                Dim champ1 As String = context.Request.Params("modal-alerte-auto-champ-1").ToString()
                Dim champ2 As String = context.Request.Params("modal-alerte-auto-champ-2").ToString()
                Dim champ3 As String = context.Request.Params("modal-alerte-auto-champ-3").ToString()
                Dim champ4 As String = context.Request.Params("modal-alerte-auto-champ-4").ToString()
                Dim champ5 As String = context.Request.Params("modal-alerte-auto-champ-5").ToString()
                Dim operateur1 As String = context.Request.Params("modal-alerte-auto-operateur-1").ToString()
                Dim operateur2 As String = context.Request.Params("modal-alerte-auto-operateur-2").ToString()
                Dim operateur3 As String = context.Request.Params("modal-alerte-auto-operateur-3").ToString()
                Dim operateur4 As String = context.Request.Params("modal-alerte-auto-operateur-4").ToString()
                Dim operateur5 As String = context.Request.Params("modal-alerte-auto-operateur-5").ToString()
                Dim valeur1 As String = context.Request.Params("modal-alerte-auto-valeur-1").ToString()
                Dim valeur2 As String = context.Request.Params("modal-alerte-auto-valeur-2").ToString()
                Dim valeur3 As String = context.Request.Params("modal-alerte-auto-valeur-3").ToString()
                Dim valeur4 As String = context.Request.Params("modal-alerte-auto-valeur-4").ToString()
                Dim valeur5 As String = context.Request.Params("modal-alerte-auto-valeur-5").ToString()
                Dim duree As String = context.Request.Params("modal-alerte-auto-duree").ToString()
                Dim periodicite As String = context.Request.Params("modal-alerte-auto-periodicite").ToString()
                Dim debut As String = context.Request.Params("modal-alerte-auto-debut").ToString()
                Dim active As String = "0"
                If (Not context.Request.Params("modal-alerte-auto-activer") Is Nothing) Then
                    active = context.Request.Params("modal-alerte-auto-activer").ToString()
                End If

                Dim alerte_auto_temp As AlerteAutomatique
                Dim attribut_temp As Attribut

                alerte_auto_temp = New AlerteAutomatique()
                alerte_auto_temp.Workflow = user.Workflow
                alerte_auto_temp.IDAlerteAuto = id
                alerte_auto_temp.Libelle = libelle
                alerte_auto_temp.Duree = duree
                alerte_auto_temp.Periodicite = periodicite
                alerte_auto_temp.Debut = debut
                alerte_auto_temp.Active = active

                If (champ1.Trim() <> "" And operateur1.Trim() <> "" And valeur1.Trim() <> "") Then
                    attribut_temp = New Attribut(New Champ(alerte_auto_temp.Workflow, champ1, "", "", "", "", "", "", "", New List(Of ListItem), "", "", 0), operateur1, valeur1)
                    alerte_auto_temp.ListeAttributs.Add(attribut_temp)
                End If

                If (champ2.Trim() <> "" And operateur2.Trim() <> "" And valeur2.Trim() <> "") Then
                    attribut_temp = New Attribut(New Champ(alerte_auto_temp.Workflow, champ2, "", "", "", "", "", "", "", New List(Of ListItem), "", "", 0), operateur2, valeur2)
                    alerte_auto_temp.ListeAttributs.Add(attribut_temp)
                End If

                If (champ3.Trim() <> "" And operateur3.Trim() <> "" And valeur3.Trim() <> "") Then
                    attribut_temp = New Attribut(New Champ(alerte_auto_temp.Workflow, champ3, "", "", "", "", "", "", "", New List(Of ListItem), "", "", 0), operateur3, valeur3)
                    alerte_auto_temp.ListeAttributs.Add(attribut_temp)
                End If

                If (champ4.Trim() <> "" And operateur4.Trim() <> "" And valeur4.Trim() <> "") Then
                    attribut_temp = New Attribut(New Champ(alerte_auto_temp.Workflow, champ4, "", "", "", "", "", "", "", New List(Of ListItem), "", "", 0), operateur4, valeur4)
                    alerte_auto_temp.ListeAttributs.Add(attribut_temp)
                End If

                If (champ5.Trim() <> "" And operateur5.Trim() <> "" And valeur5.Trim() <> "") Then
                    attribut_temp = New Attribut(New Champ(alerte_auto_temp.Workflow, champ5, "", "", "", "", "", "", "", New List(Of ListItem), "", "", 0), operateur5, valeur5)
                    alerte_auto_temp.ListeAttributs.Add(attribut_temp)
                End If

                If (id.Trim() = "") Then
                    DBFunctions.InsertAlerteAutomatique(alerte_auto_temp)
                Else
                    DBFunctions.UpdateAlerteAutomatique(alerte_auto_temp)
                End If

                context.Response.Redirect("../ParametrageAlerteAuto.aspx")

            Else

                context.Response.Redirect("../ParametrageAlerteAuto.aspx")

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