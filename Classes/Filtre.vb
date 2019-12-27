Public Class Filtre

    Public Sub New()

        _workflow = ""
        _id_filtre = ""
        _libelle = ""
        _liste_attributs = New List(Of Attribut)

    End Sub

    Private _workflow As String
    Public Property Workflow() As String
        Get
            Return _workflow
        End Get
        Set(ByVal value As String)
            _workflow = value
        End Set
    End Property

    Private _id_filtre As String
    Public Property IDFiltre() As String
        Get
            Return _id_filtre
        End Get
        Set(ByVal value As String)
            _id_filtre = value
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

    Private _liste_attributs As List(Of Attribut)
    Public Property ListeAttributs() As List(Of Attribut)
        Get
            Return _liste_attributs
        End Get
        Set(ByVal value As List(Of Attribut))
            _liste_attributs = value
        End Set
    End Property

End Class
