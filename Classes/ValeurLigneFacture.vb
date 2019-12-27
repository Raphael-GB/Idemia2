Public Class ValeurLigneFacture

    Public Sub New()

        _id_facture = ""
        _id_ligne = ""
        _id_champ = ""
        _valeur = ""

    End Sub

    Public Sub New(p_id_facture As String, p_id_ligne As String, p_id_champ As String, p_valeur As String)

        _id_facture = p_id_facture
        _id_ligne = p_id_ligne
        _id_champ = p_id_champ
        _valeur = p_valeur

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

    Private _id_champ As String
    Public Property IdChamp() As String
        Get
            Return _id_champ
        End Get
        Set(ByVal value As String)
            _id_champ = value
        End Set
    End Property

    Private _valeur As String
    Public Property Valeur() As String
        Get
            Return _valeur
        End Get
        Set(ByVal value As String)
            _valeur = value
        End Set
    End Property

End Class
