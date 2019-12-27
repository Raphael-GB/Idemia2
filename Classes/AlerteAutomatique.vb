Public Class AlerteAutomatique

    Public Sub New()

        _workflow = ""
        _id_alerte_auto = ""
        _libelle = ""
        _debut = ""
        _duree = ""
        _periodicite = ""
        _active = ""
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

    Private _id_alerte_auto As String
    Public Property IDAlerteAuto() As String
        Get
            Return _id_alerte_auto
        End Get
        Set(ByVal value As String)
            _id_alerte_auto = value
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

    Private _active As String
    Public Property Active() As String
        Get
            Return _active
        End Get
        Set(ByVal value As String)
            _active = value
        End Set
    End Property

    Private _debut As String
    Public Property Debut() As String
        Get
            Return _debut
        End Get
        Set(ByVal value As String)
            _debut = value
        End Set
    End Property

    Private _duree As String
    Public Property Duree() As String
        Get
            Return _duree
        End Get
        Set(ByVal value As String)
            _duree = value
        End Set
    End Property

    Private _periodicite As String
    Public Property Periodicite() As String
        Get
            Return _periodicite
        End Get
        Set(ByVal value As String)
            _periodicite = value
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
