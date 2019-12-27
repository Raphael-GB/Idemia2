Imports System.Web
Imports System.Web.Services
Imports System.IO
Imports System.Web.Script.Serialization

Public Class EnregistrerValorisationAutomatique
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            Dim response As String = "0"
            Dim erreur As Boolean = False

            Try

                context.Response.ContentType = "application/json"

                Dim current_user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)

                Dim sr As New StreamReader(context.Request.InputStream)

                Dim stream As String = sr.ReadToEnd()

                Dim serializer As New JavaScriptSerializer()

                Dim valorisations As List(Of ValeurAutomatisation) = serializer.Deserialize(Of List(Of ValeurAutomatisation))(stream)

                Dim id_facture As String = (From res In valorisations
                                                Select res.IdFacture Distinct).ToList()(0)

                Dim liste_champs_valorises As New List(Of Champ)

                For Each va As ValeurAutomatisation In valorisations

                    Dim champ_temp As Champ = New Champ()
                    champ_temp.IDChamp = va.IdChamp
                    champ_temp.Typage = va.Typage
                    champ_temp.Valeur = va.Valeur

                    liste_champs_valorises.Add(champ_temp)

                Next

                If (DBFunctions.insertReglesValorisation(current_user, id_facture, liste_champs_valorises)) Then
                    response = "1"
                End If

            Catch ex As Exception
                response = "0"
            End Try

            context.Response.Write(response)

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