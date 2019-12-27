Public Class Historique

    Public Sub New()

        _workflow = ""
        _docid = ""
        _action = ""
        _date_action = ""
        _id_champ = ""
        _id_alerte = ""
        _identifiant = ""
        _ancienne_valeur = ""
        _nouvelle_valeur = ""
        _libelle_champ = ""
        _nom_utilisateur = ""

    End Sub

    Public Sub New(p_workflow As String, p_docid As String, p_action As String, p_date_action As String, p_id_champ As String, p_id_alerte As String, p_identifiant As String, p_ancienne_valeur As String, p_nouvelle_valeur As String, p_libelle_champ As String, p_nom_utilisateur As String)

        _workflow = p_workflow
        _docid = p_docid
        _action = p_action
        _date_action = p_date_action
        _id_champ = p_id_champ
        _id_alerte = p_id_alerte
        _identifiant = p_identifiant
        _ancienne_valeur = p_ancienne_valeur
        _nouvelle_valeur = p_nouvelle_valeur
        _libelle_champ = p_libelle_champ
        _nom_utilisateur = p_nom_utilisateur

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

    Private _docid As String
    Public Property DocId() As String
        Get
            Return _docid
        End Get
        Set(ByVal value As String)
            _docid = value
        End Set
    End Property

    Private _action As String
    Public Property Action() As String
        Get
            Return _action
        End Get
        Set(ByVal value As String)
            _action = value
        End Set
    End Property

    Private _date_action As String
    Public Property DateAction() As String
        Get
            Return _date_action
        End Get
        Set(ByVal value As String)
            _date_action = value
        End Set
    End Property

    Private _id_champ As String
    Public Property IdChamp() As String
        Get
            Return _id_champ
        End Get
        Set(ByVal value As String)
            _id_champ = value
        End Set
    End Property

    Private _id_alerte As String
    Public Property IdAlerte() As String
        Get
            Return _id_alerte
        End Get
        Set(ByVal value As String)
            _id_alerte = value
        End Set
    End Property

    Private _identifiant As String
    Public Property identifiant() As String
        Get
            Return _identifiant
        End Get
        Set(ByVal value As String)
            _identifiant = value
        End Set
    End Property

    Private _ancienne_valeur As String
    Public Property AncienneValeur() As String
        Get
            Return _ancienne_valeur
        End Get
        Set(ByVal value As String)
            _ancienne_valeur = value
        End Set
    End Property

    Private _nouvelle_valeur As String
    Public Property NouvelleValeur() As String
        Get
            Return _nouvelle_valeur
        End Get
        Set(ByVal value As String)
            _nouvelle_valeur = value
        End Set
    End Property

    Private _libelle_champ As String
    Public Property LibelleChamp() As String
        Get
            Return _libelle_champ
        End Get
        Set(ByVal value As String)
            _libelle_champ = value
        End Set
    End Property

    Private _nom_utilisateur As String
    Public Property NomUtilisateur() As String
        Get
            Return _nom_utilisateur
        End Get
        Set(ByVal value As String)
            _nom_utilisateur = value
        End Set
    End Property


End Class
