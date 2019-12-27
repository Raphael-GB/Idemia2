Public Class CommentaireValideur

    Public Sub New()

        _id_facture = ""
        _date_retour = ""
        _identifiant = ""
        _nom = ""
        _commentaire = ""
        _motif = ""

    End Sub

    Public Sub New(p_id_facture As String, p_date_retour As String, p_identifiant As String, p_nom As String, p_commentaire As String, p_motif As String)

        _id_facture = p_id_facture
        _date_retour = p_date_retour
        _identifiant = p_identifiant
        _nom = p_nom
        _commentaire = p_commentaire
        _motif = p_motif

    End Sub

    Private _id_facture As String
    Public Property IdFacture() As String
        Get
            Return _id_facture
        End Get
        Set(ByVal value As String)
            _id_facture = value
        End Set
    End Property

    Private _date_retour As String
    Public Property DateRetour() As String
        Get
            Return _date_retour
        End Get
        Set(ByVal value As String)
            _date_retour = value
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

    Private _nom As String
    Public Property Nom() As String
        Get
            Return _nom
        End Get
        Set(ByVal value As String)
            _nom = value
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

    Private _motif As String
    Public Property Motif() As String
        Get
            Return _motif
        End Get
        Set(ByVal value As String)
            _motif = value
        End Set
    End Property


End Class
