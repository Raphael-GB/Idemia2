Public Class Message

    Public Sub New()

        _workflow = ""
        _id_message = ""
        _identifiant_utilisateur = ""
        _statut = New Statut()
        _sujet = ""
        _texte = ""
        _lecture = New Statut()
        _expediteur = New Contact()
        _destinataires = New List(Of Contact)
        _date_message = ""

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

    Private _id_message As String
    Public Property IDMessage() As String
        Get
            Return _id_message
        End Get
        Set(ByVal value As String)
            _id_message = value
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

    Private _statut As Statut
    Public Property Statut() As Statut
        Get
            Return _statut
        End Get
        Set(ByVal value As Statut)
            _statut = value
        End Set
    End Property

    Private _sujet As String
    Public Property Sujet() As String
        Get
            Return _sujet
        End Get
        Set(ByVal value As String)
            _sujet = value
        End Set
    End Property

    Private _texte As String
    Public Property Texte() As String
        Get
            Return _texte
        End Get
        Set(ByVal value As String)
            _texte = value
        End Set
    End Property

    Private _libelle_lecture As String
    Public Property LibelleLecture() As String
        Get
            Return _libelle_lecture
        End Get
        Set(ByVal value As String)
            _libelle_lecture = value
        End Set
    End Property

    Private _lecture As Statut
    Public Property Lecture() As Statut
        Get
            Return _lecture
        End Get
        Set(ByVal value As Statut)
            _lecture = value
        End Set
    End Property

    Private _expediteur As Contact
    Public Property Expediteur() As Contact
        Get
            Return _expediteur
        End Get
        Set(ByVal value As Contact)
            _expediteur = value
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

    Private _date_message As String
    Public Property DateMessage() As String
        Get
            Return _date_message
        End Get
        Set(ByVal value As String)
            _date_message = value
        End Set
    End Property



End Class
