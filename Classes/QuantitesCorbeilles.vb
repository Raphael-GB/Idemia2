Public Class QuantitesCorbeilles

    Public Sub New()
        _nb_corbeille_rejet = 0
        _nb_corbeille_four_inconnu = 0
        _nb_corbeille_four_mutliple = 0
        _nb_corbeille_commande = 0
        _nb_hors_scope = 0
        _nb_corbeille_egencia = 0
    End Sub

    Private _nb_corbeille_four_inconnu As Integer
    Public Property NbCorbeilleFourInconnu() As Integer
        Get
            Return _nb_corbeille_four_inconnu
        End Get
        Set(ByVal value As Integer)
            _nb_corbeille_four_inconnu = value
        End Set
    End Property

    Private _nb_corbeille_four_mutliple As Integer
    Public Property NbCorbeilleFourMultiple() As Integer
        Get
            Return _nb_corbeille_four_mutliple
        End Get
        Set(ByVal value As Integer)
            _nb_corbeille_four_mutliple = value
        End Set
    End Property

    Private _nb_corbeille_rejet As Integer
    Public Property NbCorbeilleRejet() As Integer
        Get
            Return _nb_corbeille_rejet
        End Get
        Set(ByVal value As Integer)
            _nb_corbeille_rejet = value
        End Set
    End Property

    Private _nb_corbeille_commande As Integer
    Public Property NbCorbeilleCommande() As Integer
        Get
            Return _nb_corbeille_commande
        End Get
        Set(ByVal value As Integer)
            _nb_corbeille_commande = value
        End Set
    End Property

    Private _nb_corbeille_egencia As Integer
    Public Property NbCorbeilleEgencia
        Get
            Return _nb_corbeille_egencia
        End Get
        Set(value)
            _nb_corbeille_egencia = value
        End Set
    End Property


    Private _nb_hors_scope As Integer
    Public Property NbHorsScope() As Integer
        Get
            Return _nb_hors_scope
        End Get
        Set(ByVal value As Integer)
            _nb_hors_scope = value
        End Set
    End Property


End Class
