Public Class Attribut

    Public Sub New()

        _champ = New Champ()
        _operateur = ""
        _valeur = ""

    End Sub

    Public Sub New(p_champ As Champ, p_operateur As String, p_valeur As String)

        _champ = p_champ
        _operateur = p_operateur
        _valeur = p_valeur

    End Sub

    Private _champ As Champ
    Public Property Champ() As Champ
        Get
            Return _champ
        End Get
        Set(ByVal value As Champ)
            _champ = value
        End Set
    End Property

    Private _operateur As String
    Public Property Operateur() As String
        Get
            Return _operateur
        End Get
        Set(ByVal value As String)
            _operateur = value
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
