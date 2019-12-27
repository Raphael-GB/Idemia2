Public Class Utilisateur

    Public Sub New()

        _identifiant = ""
        _identifiant_sso = ""
        _id_profil = ""
        _workflow = ""
        _email = ""
        _civilite = New Civilite()
        _nom = ""
        _prenom = ""
        _fonction = ""
        _mdp = ""
        _nb_max_utilisateur = 0
        _requete_fournisseurs = ""
        _date_demarrage = ""
        _liste_champs_enrichissement = New List(Of Champ)
        _liste_champs_correction = New List(Of Champ)
        _liste_filtres = New List(Of Filtre)
        _liste_alertes_auto = New List(Of AlerteAutomatique)
        _delai_rejet_archive = ""
        _corbeille_a_traiter = ""
        _corbeille_rejet = ""
        _corbeille_suivi = ""
        _corbeille_archive = ""
        _corbeille_validation = ""
        _corbeille_matching_ko = ""
        _gestion_validation = ""
        _gestion_ligne = ""
        _valorisation_automatique = ""
        _id_champ_pivot_valorisation = ""
        _alerte_mail_valideur = ""
        _table_facture = ""
        _table_ligne_facture = ""

    End Sub

    Private _identifiant As String
    Public Property Identifiant() As String
        Get
            Return _identifiant
        End Get
        Set(ByVal value As String)
            _identifiant = value
        End Set
    End Property

    Private _identifiant_sso As String
    Public Property IdentifiantSSO() As String
        Get
            Return _identifiant_sso
        End Get
        Set(ByVal value As String)
            _identifiant_sso = value
        End Set
    End Property

    Private _id_profil As String
    Public Property IDProfil() As String
        Get
            Return _id_profil
        End Get
        Set(ByVal value As String)
            _id_profil = value
        End Set
    End Property

    Private _workflow As String
    Public Property Workflow() As String
        Get
            Return _workflow
        End Get
        Set(ByVal value As String)
            _workflow = value
        End Set
    End Property

    Private _email As String
    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property

    Private _civilite As Civilite
    Public Property Civilite() As Civilite
        Get
            Return _civilite
        End Get
        Set(ByVal value As Civilite)
            _civilite = value
        End Set
    End Property

    Private _nom As String
    Public Property Nom() As String
        Get
            Return _nom
        End Get
        Set(ByVal value As String)
            _nom = value
        End Set
    End Property

    Private _prenom As String
    Public Property Prenom() As String
        Get
            Return _prenom
        End Get
        Set(ByVal value As String)
            _prenom = value
        End Set
    End Property

    Private _fonction As String
    Public Property Fonction() As String
        Get
            Return _fonction
        End Get
        Set(ByVal value As String)
            _fonction = value
        End Set
    End Property

    Private _mdp As String
    Public Property MDP() As String
        Get
            Return _mdp
        End Get
        Set(ByVal value As String)
            _mdp = value
        End Set
    End Property

    Private _nb_max_utilisateur As Integer
    Public Property NbMaxUtilisateur() As Integer
        Get
            Return _nb_max_utilisateur
        End Get
        Set(ByVal value As Integer)
            _nb_max_utilisateur = value
        End Set
    End Property

    Private _requete_fournisseurs As String
    Public Property RequeteFournisseurs() As String
        Get
            Return _requete_fournisseurs
        End Get
        Set(ByVal value As String)
            _requete_fournisseurs = value
        End Set
    End Property

    Private _date_demarrage As String
    Public Property DateDemarrage() As String
        Get
            Return _date_demarrage
        End Get
        Set(ByVal value As String)
            _date_demarrage = value
        End Set
    End Property

    Private _liste_champs_enrichissement As List(Of Champ)
    Public Property ListeChampsEnrichissement() As List(Of Champ)
        Get
            Return _liste_champs_enrichissement
        End Get
        Set(ByVal value As List(Of Champ))
            _liste_champs_enrichissement = value
        End Set
    End Property

    Private _liste_champs_correction As List(Of Champ)
    Public Property ListeChampsCorrection() As List(Of Champ)
        Get
            Return _liste_champs_correction
        End Get
        Set(ByVal value As List(Of Champ))
            _liste_champs_correction = value
        End Set
    End Property

    Private _liste_filtres As List(Of Filtre)
    Public Property ListeFiltres() As List(Of Filtre)
        Get
            Return _liste_filtres
        End Get
        Set(ByVal value As List(Of Filtre))
            _liste_filtres = value
        End Set
    End Property

    Private _liste_alertes_auto As List(Of AlerteAutomatique)
    Public Property ListeAlertesAuto() As List(Of AlerteAutomatique)
        Get
            Return _liste_alertes_auto
        End Get
        Set(ByVal value As List(Of AlerteAutomatique))
            _liste_alertes_auto = value
        End Set
    End Property

    Private _delai_rejet_archive As String
    Public Property DelaiRejetArchive() As String
        Get
            Return _delai_rejet_archive
        End Get
        Set(ByVal value As String)
            _delai_rejet_archive = value
        End Set
    End Property

    Private _corbeille_a_traiter As String
    Public Property CorbeilleATraiter() As String
        Get
            Return _corbeille_a_traiter
        End Get
        Set(ByVal value As String)
            _corbeille_a_traiter = value
        End Set
    End Property

    Private _corbeille_rejet As String
    Public Property CorbeilleRejet() As String
        Get
            Return _corbeille_rejet
        End Get
        Set(ByVal value As String)
            _corbeille_rejet = value
        End Set
    End Property

    Private _corbeille_suivi As String
    Public Property CorbeilleSuivi() As String
        Get
            Return _corbeille_suivi
        End Get
        Set(ByVal value As String)
            _corbeille_suivi = value
        End Set
    End Property

    Private _corbeille_archive As String
    Public Property CorbeilleArchive() As String
        Get
            Return _corbeille_archive
        End Get
        Set(ByVal value As String)
            _corbeille_archive = value
        End Set
    End Property

    Private _corbeille_validation As String
    Public Property CorbeilleValidation() As String
        Get
            Return _corbeille_validation
        End Get
        Set(ByVal value As String)
            _corbeille_validation = value
        End Set
    End Property

    Private _corbeille_matching_ko As String
    Public Property CorbeilleMatchingKO() As String
        Get
            Return _corbeille_matching_ko
        End Get
        Set(ByVal value As String)
            _corbeille_matching_ko = value
        End Set
    End Property


    Private _gestion_validation As String
    Public Property GestionValidation() As String
        Get
            Return _gestion_validation
        End Get
        Set(ByVal value As String)
            _gestion_validation = value
        End Set
    End Property

    Private _gestion_ligne As String
    Public Property GestionLigne() As String
        Get
            Return _gestion_ligne
        End Get
        Set(ByVal value As String)
            _gestion_ligne = value
        End Set
    End Property

    Private _valorisation_automatique As String
    Public Property ValorisationAutomatique() As String
        Get
            Return _valorisation_automatique
        End Get
        Set(ByVal value As String)
            _valorisation_automatique = value
        End Set
    End Property

    Private _id_champ_pivot_valorisation As String
    Public Property IdChampPivotValorisation() As String
        Get
            Return _id_champ_pivot_valorisation
        End Get
        Set(ByVal value As String)
            _id_champ_pivot_valorisation = value
        End Set
    End Property

    Private _alerte_mail_valideur As String
    Public Property AlerteMailValideur() As String
        Get
            Return _alerte_mail_valideur
        End Get
        Set(ByVal value As String)
            _alerte_mail_valideur = value
        End Set
    End Property

    Private _table_facture As String
    Public Property TableFacture() As String
        Get
            Return _table_facture
        End Get
        Set(ByVal value As String)
            _table_facture = value
        End Set
    End Property

    Private _table_ligne_facture As String
    Public Property TableLigneFacture() As String
        Get
            Return _table_ligne_facture
        End Get
        Set(ByVal value As String)
            _table_ligne_facture = value
        End Set
    End Property


End Class
