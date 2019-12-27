Public Class EvenementCalendrier

    Public Sub New()

        _workflow = ""
        _id_evenement = ""
        _identifiant = ""
        _titre = ""
        _date_debut = ""
        _date_fin = ""
        _description = ""

    End Sub

    Public Sub New(p_workflow As String, p_id_evenement As String, p_identifiant As String, p_titre As String, p_date_debut As String, p_date_fin As String, p_description As String)

        _workflow = p_workflow
        _id_evenement = p_id_evenement
        _identifiant = p_identifiant
        _titre = p_titre
        _date_debut = p_date_debut
        _date_fin = p_date_fin
        _description = p_description

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

    Private _id_evenement As String
    Public Property IDEvenement() As String
        Get
            Return _id_evenement
        End Get
        Set(ByVal value As String)
            _id_evenement = value
        End Set
    End Property

    Private _identifiant As String
    Public Property Identifiant() As String
        Get
            Return _identifiant
        End Get
        Set(ByVal value As String)
            _identifiant = value
        End Set
    End Property

    Private _titre As String
    Public Property Titre() As String
        Get
            Return _titre
        End Get
        Set(ByVal value As String)
            _titre = value
        End Set
    End Property

    Private _date_debut As String
    Public Property DateDebut() As String
        Get
            Return _date_debut
        End Get
        Set(ByVal value As String)
            _date_debut = value
        End Set
    End Property

    Private _date_fin As String
    Public Property DateFin() As String
        Get
            Return _date_fin
        End Get
        Set(ByVal value As String)
            _date_fin = value
        End Set
    End Property

    Private _description As String
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property


End Class
