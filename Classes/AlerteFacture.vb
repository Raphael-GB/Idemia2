Public Class AlerteFacture

    Public Sub New()

        _workflow = ""
        _id_alerte = ""
        _motif = New Motif()
        _statut = New Statut()
        _date_alerte = ""
        _emetteur = New Contact()
        _destinataires = New List(Of Contact)
        _commentaire = ""
        _docid = ""
        _num_facture = ""
        _date_facture = ""
        _date_resolution = ""
        _resolu_par = New Contact()

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

    Private _id_alerte As String
    Public Property IDAlerte() As String
        Get
            Return _id_alerte
        End Get
        Set(ByVal value As String)
            _id_alerte = value
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

    Private _statut As Statut
    Public Property Statut() As Statut
        Get
            Return _statut
        End Get
        Set(ByVal value As Statut)
            _statut = value
        End Set
    End Property

    Private _date_alerte As String
    Public Property DateAlerte() As String
        Get
            Return _date_alerte
        End Get
        Set(ByVal value As String)
            _date_alerte = value
        End Set
    End Property

    Private _emetteur As Contact
    Public Property Emetteur() As Contact
        Get
            Return _emetteur
        End Get
        Set(ByVal value As Contact)
            _emetteur = value
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

    Private _commentaire As String
    Public Property Commentaire() As String
        Get
            Return _commentaire
        End Get
        Set(ByVal value As String)
            _commentaire = value
        End Set
    End Property

    Private _docid As String
    Public Property DocId() As String
        Get
            Return _docid
        End Get
        Set(ByVal value As String)
            _docid = value
        End Set
    End Property

    Private _num_facture As String
    Public Property NumFacture() As String
        Get
            Return _num_facture
        End Get
        Set(ByVal value As String)
            _num_facture = value
        End Set
    End Property

    Private _date_facture As String
    Public Property DateFacture() As String
        Get
            Return _date_facture
        End Get
        Set(ByVal value As String)
            _date_facture = value
        End Set
    End Property

    Private _date_resolution As String
    Public Property DateResolution() As String
        Get
            Return _date_resolution
        End Get
        Set(ByVal value As String)
            _date_resolution = value
        End Set
    End Property

    Private _resolu_par As Contact
    Public Property ResoluPar() As Contact
        Get
            Return _resolu_par
        End Get
        Set(ByVal value As Contact)
            _resolu_par = value
        End Set
    End Property


End Class
