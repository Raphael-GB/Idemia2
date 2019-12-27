Public Class Demande

    Private Sub New()

        _workflow = ""
        _id_demande = ""
        _identifiant = ""
        _statut = New Statut()
        _motif = New Motif()
        _commentaire = ""
        _date_demande = ""
        _date_traitement = ""



    End Sub

    Private Sub New(p_workflow As String, p_id_demande As String, p_identifiant As String, p_statut As Statut, p_motif As Motif, p_commentaire As String, p_date_demande As String, p_date_traitement As String)

        _workflow = p_workflow
        _id_demande = p_id_demande
        _identifiant = p_identifiant
        _statut = p_statut
        _motif = p_motif
        _commentaire = p_commentaire
        _date_demande = p_date_demande
        _date_traitement = p_date_traitement

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


    Private _id_demande As String
    Public Property Id() As String
        Get
            Return _id_demande
        End Get
        Set(ByVal value As String)
            _id_demande = value
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

    Private _statut As Statut
    Public Property Statut() As Statut
        Get
            Return _statut
        End Get
        Set(ByVal value As Statut)
            _statut = value
        End Set
    End Property


    Private _motif As Motif
    Public Property Motif() As Motif
        Get
            Return _motif
        End Get
        Set(ByVal value As Motif)
            _motif = value
        End Set
    End Property

    Private _commentaire As String
    Public Property Commentaire() As String
        Get
            Return _commentaire
        End Get
        Set(ByVal value As String)
            _commentaire = value
        End Set
    End Property

    Private _date_demande As String
    Public Property DateDemande() As String
        Get
            Return _date_demande
        End Get
        Set(ByVal value As String)
            _date_demande = value
        End Set
    End Property

    Private _date_traitement As String
    Public Property DateTraitement() As String
        Get
            Return _date_traitement
        End Get
        Set(ByVal value As String)
            _date_traitement = value
        End Set
    End Property



End Class
