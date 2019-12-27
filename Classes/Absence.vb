Public Class Absences

    Public Sub New()

        _id_absence = ""
        _identifiant_utilisateur = ""
        _statut = ""
        _destinataires = New List(Of Contact)
        _date_debut = ""
        _date_fin = ""
        _date_insertion = ""
        _workflow = ""

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

    Private _id_absence As String
    Public Property IDAbsence() As String
        Get
            Return _id_absence
        End Get
        Set(ByVal value As String)
            _id_absence = value
        End Set
    End Property

    Private _identifiant_utilisateur As String
    Public Property IdentifiantUtilisateur() As String
        Get
            Return _identifiant_utilisateur
        End Get
        Set(ByVal value As String)
            _identifiant_utilisateur = value
        End Set
    End Property

    Private _statut As String
    Public Property Statut() As String
        Get
            Return _statut
        End Get
        Set(ByVal value As String)
            _statut = value
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

    Private _destinataires As List(Of Contact)
    Public Property Destinataires() As List(Of Contact)
        Get
            Return _destinataires
        End Get
        Set(ByVal value As List(Of Contact))
            _destinataires = value
        End Set
    End Property

    Private _date_insertion As String
    Public Property DateInsertion() As String
        Get
            Return _date_insertion
        End Get
        Set(ByVal value As String)
            _date_insertion = value
        End Set
    End Property



End Class
