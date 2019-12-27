Public Class Contact

    Public Sub New()

        _identifiant = ""
        _email = ""
        _nom = ""
        _prenom = ""

    End Sub

    Public Sub New(p_identifiant As String, p_email As String, p_nom As String, p_prenom As String)

        _identifiant = p_identifiant
        _email = p_email
        _nom = p_nom
        _prenom = p_prenom

    End Sub

    Private _identifiant As String
    Public Property Identifiant() As String
        Get
            Return _identifiant
        End Get
        Set(ByVal value As String)
            _identifiant = value
        End Set
    End Property

    Private _email As String
    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property

    Private _nom As String
    Public Property Nom() As String
        Get
            Return _nom
        End Get
        Set(ByVal value As String)
            _nom = value
        End Set
    End Property

    Private _prenom As String
    Public Property Prenom() As String
        Get
            Return _prenom
        End Get
        Set(ByVal value As String)
            _prenom = value
        End Set
    End Property

End Class
