Public Class FichierAjoute

    Public Sub New()

        _workflow = ""
        _id_ajout = ""
        _nom_fichier = ""
        _date_ajout = ""
        _identifiant = ""
        _ajoute_par = ""
        _url = ""
        _docid = ""

    End Sub

    Public Sub New(p_workflow As String, p_id_ajout As String, p_nom_fichier As String, p_date_ajout As String, p_identifiant As String, p_ajoute_par As String, p_url As String, p_docid As String)

        _workflow = p_workflow
        _id_ajout = p_id_ajout
        _nom_fichier = p_nom_fichier
        _date_ajout = p_date_ajout
        _identifiant = p_identifiant
        _ajoute_par = p_ajoute_par
        _url = p_url
        _docid = p_docid

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

    Private _id_ajout As String
    Public Property IDAjout() As String
        Get
            Return _id_ajout
        End Get
        Set(ByVal value As String)
            _id_ajout = value
        End Set
    End Property

    Private _nom_fichier As String
    Public Property NomFichier() As String
        Get
            Return _nom_fichier
        End Get
        Set(ByVal value As String)
            _nom_fichier = value
        End Set
    End Property

    Private _date_ajout As String
    Public Property DateAjout() As String
        Get
            Return _date_ajout
        End Get
        Set(ByVal value As String)
            _date_ajout = value
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

    Private _ajoute_par As String
    Public Property AjoutePar() As String
        Get
            Return _ajoute_par
        End Get
        Set(ByVal value As String)
            _ajoute_par = value
        End Set
    End Property

    Private _url As String
    Public Property Url() As String
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
        End Set
    End Property

    Private _docid As String
    Public Property DOCID() As String
        Get
            Return _docid
        End Get
        Set(ByVal value As String)
            _docid = value
        End Set
    End Property


End Class
