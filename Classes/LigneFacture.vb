Public Class LigneFacture

    Public Sub New()

        _id_facture = ""
        _id_ligne = ""
        _liste_champs = New List(Of Champ)

    End Sub

    Public Sub New(p_id_facture As String, p_id_ligne As String, p_liste_champs As List(Of Champ))

        _id_facture = ""
        _id_ligne = p_id_ligne
        _liste_champs = p_liste_champs

    End Sub

    Private _id_facture As String
    Public Property IdFacture() As String
        Get
            Return _id_facture
        End Get
        Set(ByVal value As String)
            _id_facture = value
        End Set
    End Property


    Private _id_ligne As String
    Public Property IdLigne() As String
        Get
            Return _id_ligne
        End Get
        Set(ByVal value As String)
            _id_ligne = value
        End Set
    End Property

    Private _liste_champs As List(Of Champ)
    Public Property ListeChamps() As List(Of Champ)
        Get
            Return _liste_champs
        End Get
        Set(ByVal value As List(Of Champ))
            _liste_champs = value
        End Set
    End Property



End Class
