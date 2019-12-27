Imports System.Web
Imports System.Web.Services
Imports System.Web.Script.Serialization

Public Class JsonIndicateur
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

            If (Not context.Request.Params("graph") Is Nothing) Then

                Dim serializer As New JavaScriptSerializer()
                Dim serializedResult As String = ""
                Dim graph As String = context.Request.Params("graph")
                Dim date_debut As String = context.Request.Params("debut")
                Dim date_fin As String = context.Request.Params("fin")
                Dim identifiant As String = context.Request.Params("identifiant")

                If (graph = "productivite") Then

                    serializedResult = serializer.Serialize(DBFunctions.getIndicateurProductivite(date_debut, date_fin, identifiant, current_user.Workflow))
                    context.Response.Write(serializedResult)

                ElseIf (graph = "temps_connexion") Then

                    'Dim test As String = "[{ ""date_activite"": ""2017-01-01"", ""tps_connexion"": 60},{ ""date_activite"": ""2017-01-01"", ""tps_connexion"": 50},{ ""date_activite"": ""2017-01-01"", ""tps_connexion"": 10},{ ""date_activite"": ""2017-01-01"", ""tps_connexion"": 30}]"
                    Dim test As String = "[{ ""date_activite"": ""2017-01-01"", ""tps_connexion"": 0}]"

                    context.Response.Write(test)

                ElseIf (graph = "delai_alerte") Then

                    'Dim test As String = "[{ ""delai"": ""J+0"", ""nb_alertes"": 60},{ ""delai"": ""J+1"", ""nb_alertes"": 50},{ ""delai"": ""J+2"", ""nb_alertes"": 10},{ ""delai"": ""J+3"", ""nb_alertes"": 5}]"
                    Dim test As String = "[{ ""delai"": ""J+0"", ""nb_alertes"": 0},{ ""delai"": ""J+1"", ""nb_alertes"": 0},{ ""delai"": ""J+2"", ""nb_alertes"": 0},{ ""delai"": ""J+3"", ""nb_alertes"": 0}]"

                    context.Response.Write(test)

                ElseIf (graph = "repartition_statut") Then

                    serializedResult = serializer.Serialize(DBFunctions.getIndicateurRepartitionStatut(current_user, date_debut, date_fin))
                    context.Response.Write(serializedResult)

                ElseIf (graph = "evolution_taux_correction") Then

                    'Dim test As String = "[{ ""date_facturation"": ""2017-01-01"", ""nb_corrections"": 80,""nb_a_corriger"":200},{ ""date_facturation"": ""2017-01-02"", ""nb_corrections"": 25,""nb_a_corriger"":120},{ ""date_facturation"": ""2017-01-03"", ""nb_corrections"": 5,""nb_a_corriger"":90},{ ""date_facturation"": ""2017-01-04"", ""nb_corrections"": 10,""nb_a_corriger"":100}]"
                    Dim test As String = "[{ ""date_facturation"": ""2017-01-01"", ""nb_corrections"": 80,""nb_a_corriger"":200}]"

                    context.Response.Write(test)

                ElseIf (graph = "delai_paiement_responsabilite") Then

                    Dim test As String = "[{ ""periode"": """", ""delai_fournisseur"": 0,""delai_prestataire"":0,""delai_client"":0}]"

                    context.Response.Write(test)

                ElseIf (graph = "motif_responsabilite") Then

                    Dim test As String = "[{ ""motif"": ""motif 1"", ""nb_fournisseur"": 0,""nb_prestataire"":0,""nb_client"":0},{ ""motif"": ""motif 2"", ""nb_fournisseur"": 0,""nb_prestataire"":0,""nb_client"":0}]"

                    context.Response.Write(test)

                ElseIf (graph = "evolution_taux_generique") Then

                    'Dim test As String = "[{ ""date_facturation"": ""2017-01-01"", ""taux_generique"": 0},{ ""date_facturation"": ""2017-01-02"", ""taux_generique"": 25},{ ""date_facturation"": ""2017-01-03"", ""taux_generique"": 80},{ ""date_facturation"": ""2017-01-04"", ""taux_generique"": 25}]"
                    Dim test As String = "[{ ""date_facturation"": ""2017-01-01"", ""taux_generique"": 0}]"

                    context.Response.Write(test)

                Else
                    context.Response.Write("")
                End If

            Else
                context.Response.Write("")
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