Public Class Civilite

    Public Sub New()
        _id = ""
        _libelle = ""
    End Sub

    Public Sub New(p_id As String, p_libelle As String)
        _id = p_id
        _libelle = p_libelle
    End Sub

    Private _id As String
    Public Property Id() As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
        End Set
    End Property

    Private _libelle As String
    Public Property Libelle() As String
        Get
            Return _libelle
        End Get
        Set(ByVal value As String)
            _libelle = value
        End Set
    End Property

End Class
