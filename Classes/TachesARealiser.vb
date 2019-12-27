Public Class TachesARealiser

    Public Sub New()

        _nb_corbeille_a_traiter = 0
        _nb_corbeille_validation = 0

    End Sub

    Public Sub New(p_nb_corbeille_a_traiter, p_nb_corbeille_validation)

        _nb_corbeille_a_traiter = p_nb_corbeille_a_traiter
        _nb_corbeille_validation = p_nb_corbeille_validation

    End Sub

    Private _nb_corbeille_a_traiter As Integer
    Public Property NbCorbeilleATraiter() As Integer
        Get
            Return _nb_corbeille_a_traiter
        End Get
        Set(ByVal value As Integer)
            _nb_corbeille_a_traiter = value
        End Set
    End Property

    Private _nb_corbeille_validation As Integer
    Public Property NbCorbeilleValidation() As Integer
        Get
            Return _nb_corbeille_validation
        End Get
        Set(ByVal value As Integer)
            _nb_corbeille_validation = value
        End Set
    End Property

End Class
