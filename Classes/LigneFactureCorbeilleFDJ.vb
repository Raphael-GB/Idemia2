Public Class LigneFactureCorbeilleFDJ

    Public Sub New()

        _id = ""
        _id_statut = ""
        _nom_fournisseur = ""
        _site_facture = ""
        _num_ap = ""
        _comptable = ""
        _type_gestion = ""
        _date_insertion = ""
        _date_traitement = ""
        _statut = ""
        _flag_alerte = ""
        _motif_alerte = ""
        _emetteur_alerte = ""
        _motif_rejet = ""
        _date_envoi_valideur = ""
        _date_retour_valideur = ""
        _motif_invalidite = ""
        _commentaire_invalidite = ""

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

    Private _id_statut As String
    Public Property IdStatut() As String
        Get
            Return _id_statut
        End Get
        Set(ByVal value As String)
            _id_statut = value
        End Set
    End Property

    Private _site_facture As String
    Public Property SiteFacture() As String
        Get
            Return _site_facture
        End Get
        Set(ByVal value As String)
            _site_facture = value
        End Set
    End Property

    Private _comptable As String
    Public Property Comptable() As String
        Get
            Return _comptable
        End Get
        Set(ByVal value As String)
            _comptable = value
        End Set
    End Property

    Private _code_fournisseur As String
    Public Property CodeFournisseur() As String
        Get
            Return _code_fournisseur
        End Get
        Set(ByVal value As String)
            _code_fournisseur = value
        End Set
    End Property

    Private _nom_fournisseur As String
    Public Property NomFournisseur() As String
        Get
            Return _nom_fournisseur
        End Get
        Set(ByVal value As String)
            _nom_fournisseur = value
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

    Private _entite As String
    Public Property Entite() As String
        Get
            Return _entite
        End Get
        Set(ByVal value As String)
            _entite = value
        End Set
    End Property

    Private _id_engagement As String
    Public Property IdEngagement() As String
        Get
            Return _id_engagement
        End Get
        Set(ByVal value As String)
            _id_engagement = value
        End Set
    End Property

    Private _montant_ttc As String
    Public Property MontantTTC() As String
        Get
            Return _montant_ttc
        End Get
        Set(ByVal value As String)
            _montant_ttc = value
        End Set
    End Property

    Private _num_commande As String
    Public Property NumCommande() As String
        Get
            Return _num_commande
        End Get
        Set(ByVal value As String)
            _num_commande = value
        End Set
    End Property

    Private _num_ap As String
    Public Property NumAP() As String
        Get
            Return _num_ap
        End Get
        Set(ByVal value As String)
            _num_ap = value
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

    Private _date_traitement As String
    Public Property DateTraitement() As String
        Get
            Return _date_traitement
        End Get
        Set(ByVal value As String)
            _date_traitement = value
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


    Private _flag_alerte As String
    Public Property FlagAlerte() As String
        Get
            Return _flag_alerte
        End Get
        Set(ByVal value As String)
            _flag_alerte = value
        End Set
    End Property

    Private _motif_alerte As String
    Public Property MotifAlerte() As String
        Get
            Return _motif_alerte
        End Get
        Set(ByVal value As String)
            _motif_alerte = value
        End Set
    End Property

    Private _emetteur_alerte As String
    Public Property EmetteurAlerte() As String
        Get
            Return _emetteur_alerte
        End Get
        Set(ByVal value As String)
            _emetteur_alerte = value
        End Set
    End Property

    Private _motif_rejet As String
    Public Property MotifRejet() As String
        Get
            Return _motif_rejet
        End Get
        Set(ByVal value As String)
            _motif_rejet = value
        End Set
    End Property

    Private _date_envoi_valideur As String
    Public Property DateEnvoiValideur() As String
        Get
            Return _date_envoi_valideur
        End Get
        Set(ByVal value As String)
            _date_envoi_valideur = value
        End Set
    End Property

    Private _date_retour_valideur As String
    Public Property DateRetourValideur() As String
        Get
            Return _date_retour_valideur
        End Get
        Set(ByVal value As String)
            _date_retour_valideur = value
        End Set
    End Property

    Private _motif_invalidite As String
    Public Property MotifInvalidite() As String
        Get
            Return _motif_invalidite
        End Get
        Set(ByVal value As String)
            _motif_invalidite = value
        End Set
    End Property

    Private _commentaire_invalidite As String
    Public Property CommentaireInvalidite() As String
        Get
            Return _commentaire_invalidite
        End Get
        Set(ByVal value As String)
            _commentaire_invalidite = value
        End Set
    End Property

    Private _type_gestion As String
    Public Property TypeGestion() As String
        Get
            Return _type_gestion
        End Get
        Set(ByVal value As String)
            _type_gestion = value
        End Set
    End Property


End Class
