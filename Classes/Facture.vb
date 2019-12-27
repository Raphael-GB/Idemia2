Public Class Facture

    Public Sub New()

        _id_facture = ""
        _code_fournisseur = ""
        _nom_fournisseur = ""
        _num_facture = ""
        _date_facture = ""
        _entite_facture = ""
        _id_engagement = ""
        _montant_ttc = ""
        _num_commande = ""
        _date_insertion = ""
        _date_traitement = ""
        _date_livraison = ""
        _date_acquittement = ""
        _statut = New Statut()
        _rejet = New Rejet()
        _date_envoi_valideur = ""
        _date_retour_valideur = ""
        _motif_invalidite = New Motif()
        _commentaire_invalidite = ""
        _identifiant_valideur = ""
        _comptable = ""
        _type_gestion = ""
        _site = ""
        _statut2 = ""


    End Sub

    Public Sub New(p_id_facture As String, p_code_fournisseur As String, p_nom_fournisseur As String, p_num_facture As String,
                   p_date_facture As String, p_entite_facture As String, p_id_engagement As String, p_montant_ttc As String, p_num_commande As String,
                   p_date_insertion As String, p_date_traitement As String, p_date_livraison As String, p_date_acquittement As String,
                   p_statut As Statut, p_rejet As Rejet, p_date_envoi_valideur As String, p_date_retour_valideur As String, p_motif_invalidite As Motif,
                   p_commentaire_invalidite As String, p_identifiant_valideur As String, p_site As String, p_statut2 As String)

        _id_facture = p_id_facture
        _code_fournisseur = p_code_fournisseur
        _nom_fournisseur = p_nom_fournisseur
        _num_facture = p_num_facture
        _date_facture = p_date_facture
        _entite_facture = p_entite_facture
        _id_engagement = p_id_engagement
        _montant_ttc = p_montant_ttc
        _num_commande = p_num_commande
        _date_insertion = p_date_insertion
        _date_traitement = p_date_traitement
        _date_livraison = p_date_livraison
        _date_acquittement = p_date_acquittement
        _statut = p_statut
        _rejet = p_rejet
        _date_envoi_valideur = p_date_envoi_valideur
        _date_retour_valideur = p_date_retour_valideur
        _motif_invalidite = p_motif_invalidite
        _commentaire_invalidite = p_commentaire_invalidite
        _identifiant_valideur = p_identifiant_valideur
        _site = p_site
        _statut2 = p_statut2

    End Sub

    Private _statut2 As String
    Public Property Statut2() As String
        Get
            Return _statut2
        End Get
        Set(value As String)
            _statut2 = value
        End Set
    End Property


    Private _site As String
    Public Property Site() As String
        Get
            Return _site
        End Get
        Set(value As String)
            _site = value
        End Set
    End Property

    Private _id_facture As String
    Public Property IDFacture() As String
        Get
            Return _id_facture
        End Get
        Set(ByVal value As String)
            _id_facture = value
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

    Private _entite_facture As String
    Public Property EntiteFacture() As String
        Get
            Return _entite_facture
        End Get
        Set(ByVal value As String)
            _entite_facture = value
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


    Private _date_livraison As String
    Public Property DateLivraison() As String
        Get
            Return _date_livraison
        End Get
        Set(ByVal value As String)
            _date_livraison = value
        End Set
    End Property

    Private _date_acquittement As String
    Public Property DateAcquittement() As String
        Get
            Return _date_acquittement
        End Get
        Set(ByVal value As String)
            _date_acquittement = value
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

    Private _rejet As Rejet
    Public Property Rejet() As Rejet
        Get
            Return _rejet
        End Get
        Set(ByVal value As Rejet)
            _rejet = value
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

    Private _motif_invalidite As Motif
    Public Property MotifInvalidite() As Motif
        Get
            Return _motif_invalidite
        End Get
        Set(ByVal value As Motif)
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

    Private _identifiant_valideur As String
    Public Property IdentifiantValideur() As String
        Get
            Return _identifiant_valideur
        End Get
        Set(ByVal value As String)
            _identifiant_valideur = value
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
