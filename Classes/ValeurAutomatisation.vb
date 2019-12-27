Public Class ValeurAutomatisation

    Public Sub New()

        _id_facture = ""
        _id_champ = ""
        _typage = ""
        _valeur = ""

    End Sub

    Public Sub New(p_id_facture As String, p_id_champ As String, p_typage As String, p_valeur As String)

        _id_facture = p_id_facture
        _id_champ = p_id_champ
        _typage = p_typage
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

    Private _id_champ As String
    Public Property IdChamp() As String
        Get
            Return _id_champ
        End Get
        Set(ByVal value As String)
            _id_champ = value
        End Set
    End Property

    Private _typage As String
    Public Property Typage() As String
        Get
            Return _typage
        End Get
        Set(ByVal value As String)
            _typage = value
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
