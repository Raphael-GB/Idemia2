Public Class Champ

    Public Sub New()

        _workflow = ""
        _id_champ = ""
        _nom_champ = ""
        _description = ""
        _typage = ""
        _valeur = ""
        _action = ""
        _ancienne_valeur = ""
        _requete = ""
        _liste = New List(Of ListItem)
        _expression_reguliere = ""
        _detail_ligne = ""
        _ordre = 0

    End Sub

    Public Sub New(p_workfkow As String, p_id_champ As String, p_nom_champ As String, p_description As String, p_typage As String, p_valeur As String, p_action As String, p_ancienne_valeur As String, p_requete As String, p_liste As List(Of ListItem), p_expression_reguliere As String, p_detail_ligne As String, p_ordre As Integer)

        _workflow = p_workfkow
        _id_champ = p_id_champ
        _nom_champ = p_nom_champ
        _description = p_description
        _typage = p_typage
        _valeur = p_valeur
        _action = p_action
        _ancienne_valeur = p_ancienne_valeur
        _requete = p_requete
        _liste = p_liste
        _expression_reguliere = p_expression_reguliere
        _detail_ligne = p_detail_ligne
        _ordre = p_ordre

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

    Private _id_champ As String
    Public Property IDChamp() As String
        Get
            Return _id_champ
        End Get
        Set(ByVal value As String)
            _id_champ = value
        End Set
    End Property

    Private _nom_champ As String
    Public Property NomChamp() As String
        Get
            Return _nom_champ
        End Get
        Set(ByVal value As String)
            _nom_champ = value
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

    Private _typage As String
    Public Property Typage() As String
        Get
            Return _typage
        End Get
        Set(ByVal value As String)
            _typage = value
        End Set
    End Property

    Private _valeur As String
    Public Property Valeur() As String
        Get
            Return _valeur
        End Get
        Set(ByVal value As String)
            _valeur = value
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

    Private _ancienne_valeur As String
    Public Property AncienneValeur() As String
        Get
            Return _ancienne_valeur
        End Get
        Set(ByVal value As String)
            _ancienne_valeur = value
        End Set
    End Property

    Private _requete As String
    Public Property Requete() As String
        Get
            Return _requete
        End Get
        Set(ByVal value As String)
            _requete = value
        End Set
    End Property

    Private _liste As List(Of ListItem)
    Public Property Liste() As List(Of ListItem)
        Get
            Return _liste
        End Get
        Set(ByVal value As List(Of ListItem))
            _liste = value
        End Set
    End Property

    Private _expression_reguliere As String
    Public Property ExpressionReguliere() As String
        Get
            Return _expression_reguliere
        End Get
        Set(ByVal value As String)
            _expression_reguliere = value
        End Set
    End Property

    Private _detail_ligne As String
    Public Property DetailLigne() As String
        Get
            Return _detail_ligne
        End Get
        Set(ByVal value As String)
            _detail_ligne = value
        End Set
    End Property

    Private _ordre As Integer
    Public Property Ordre() As Integer
        Get
            Return _ordre
        End Get
        Set(ByVal value As Integer)
            _ordre = value
        End Set
    End Property


End Class
