Imports System.Web
Imports System.Web.Services

Public Class GestionFiltre
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            If (Not context.Request.Params("modal-filtre-id") Is Nothing And _
                Not context.Request.Params("modal-filtre-libelle") Is Nothing And _
                Not context.Request.Params("modal-filtre-champ-1") Is Nothing And _
                Not context.Request.Params("modal-filtre-champ-2") Is Nothing And _
                Not context.Request.Params("modal-filtre-champ-3") Is Nothing And _
                Not context.Request.Params("modal-filtre-champ-4") Is Nothing And _
                Not context.Request.Params("modal-filtre-champ-5") Is Nothing And _
                Not context.Request.Params("modal-filtre-operateur-1") Is Nothing And _
                Not context.Request.Params("modal-filtre-operateur-2") Is Nothing And _
                Not context.Request.Params("modal-filtre-operateur-3") Is Nothing And _
                Not context.Request.Params("modal-filtre-operateur-4") Is Nothing And _
                Not context.Request.Params("modal-filtre-operateur-5") Is Nothing) Then

                Dim id As String = context.Request.Params("modal-filtre-id").ToString()
                Dim libelle As String = context.Request.Params("modal-filtre-libelle").ToString()
                Dim champ1 As String = context.Request.Params("modal-filtre-champ-1").ToString()
                Dim champ2 As String = context.Request.Params("modal-filtre-champ-2").ToString()
                Dim champ3 As String = context.Request.Params("modal-filtre-champ-3").ToString()
                Dim champ4 As String = context.Request.Params("modal-filtre-champ-4").ToString()
                Dim champ5 As String = context.Request.Params("modal-filtre-champ-5").ToString()
                Dim operateur1 As String = context.Request.Params("modal-filtre-operateur-1").ToString()
                Dim operateur2 As String = context.Request.Params("modal-filtre-operateur-2").ToString()
                Dim operateur3 As String = context.Request.Params("modal-filtre-operateur-3").ToString()
                Dim operateur4 As String = context.Request.Params("modal-filtre-operateur-4").ToString()
                Dim operateur5 As String = context.Request.Params("modal-filtre-operateur-5").ToString()

                Dim valeur1 As String = ""
                Dim valeur2 As String = ""
                Dim valeur3 As String = ""
                Dim valeur4 As String = ""
                Dim valeur5 As String = ""

                If (Not context.Request.Params("modal-filtre-valeur-1") Is Nothing) Then
                    valeur1 = context.Request.Params("modal-filtre-valeur-1").ToString()
                End If

                If (Not context.Request.Params("modal-filtre-valeur-2") Is Nothing) Then
                    valeur2 = context.Request.Params("modal-filtre-valeur-2").ToString()
                End If

                If (Not context.Request.Params("modal-filtre-valeur-3") Is Nothing) Then
                    valeur3 = context.Request.Params("modal-filtre-valeur-3").ToString()
                End If

                If (Not context.Request.Params("modal-filtre-valeur-4") Is Nothing) Then
                    valeur4 = context.Request.Params("modal-filtre-valeur-4").ToString()
                End If

                If (Not context.Request.Params("modal-filtre-valeur-5") Is Nothing) Then
                    valeur5 = context.Request.Params("modal-filtre-valeur-5").ToString()
                End If

                Dim filtre_temp As Filtre
                Dim attribut_temp As Attribut

                filtre_temp = New Filtre
                filtre_temp.Workflow = user.Workflow
                filtre_temp.IDFiltre = id
                filtre_temp.Libelle = libelle

                If (champ1.Trim() <> "" And (operateur1.Trim() = "IS_NULL" Or operateur1.Trim() = "IS_NOT_NULL" Or (operateur1.Trim() <> "" And valeur1.Trim() <> ""))) Then
                    attribut_temp = New Attribut(New Champ(filtre_temp.Workflow, champ1, "", "", "", "", "", "", "", New List(Of ListItem), "", "", 0), operateur1, valeur1)
                    filtre_temp.ListeAttributs.Add(attribut_temp)
                End If

                If (champ2.Trim() <> "" And (operateur2.Trim() = "IS_NULL" Or operateur2.Trim() = "IS_NOT_NULL" Or (operateur2.Trim() <> "" And valeur2.Trim() <> ""))) Then
                    attribut_temp = New Attribut(New Champ(filtre_temp.Workflow, champ2, "", "", "", "", "", "", "", New List(Of ListItem), "", "", 0), operateur2, valeur2)
                    filtre_temp.ListeAttributs.Add(attribut_temp)
                End If

                If (champ3.Trim() <> "" And (operateur3.Trim() = "IS_NULL" Or operateur3.Trim() = "IS_NOT_NULL" Or (operateur3.Trim() <> "" And valeur3.Trim() <> ""))) Then
                    attribut_temp = New Attribut(New Champ(filtre_temp.Workflow, champ3, "", "", "", "", "", "", "", New List(Of ListItem), "", "", 0), operateur3, valeur3)
                    filtre_temp.ListeAttributs.Add(attribut_temp)
                End If

                If (champ4.Trim() <> "" And (operateur4.Trim() = "IS_NULL" Or operateur4.Trim() = "IS_NOT_NULL" Or (operateur4.Trim() <> "" And valeur4.Trim() <> ""))) Then
                    attribut_temp = New Attribut(New Champ(filtre_temp.Workflow, champ4, "", "", "", "", "", "", "", New List(Of ListItem), "", "", 0), operateur4, valeur4)
                    filtre_temp.ListeAttributs.Add(attribut_temp)
                End If

                If (champ5.Trim() <> "" And (operateur5.Trim() = "IS_NULL" Or operateur5.Trim() = "IS_NOT_NULL" Or (operateur5.Trim() <> "" And valeur5.Trim() <> ""))) Then
                    attribut_temp = New Attribut(New Champ(filtre_temp.Workflow, champ5, "", "", "", "", "", "", "", New List(Of ListItem), "", "", 0), operateur5, valeur5)
                    filtre_temp.ListeAttributs.Add(attribut_temp)
                End If

                If (id.Trim() = "") Then
                    DBFunctions.InsertFiltre(filtre_temp)
                Else
                    DBFunctions.UpdateFiltre(filtre_temp)
                End If

                context.Response.Redirect("../ParametrageFiltre.aspx")

            Else

                context.Response.Redirect("../ParametrageFiltre.aspx")

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