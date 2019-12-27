Public Class DocumentHorsScope

    Private _statut2 As String
    Public Property Statut2() As String
        Get
            Return _statut2
        End Get
        Set(value As String)
            _statut2 = value
        End Set
    End Property


    Private _site2 As String
    Public Property Site2() As String
        Get
            Return _site2
        End Get
        Set(value As String)
            _site2 = value
        End Set
    End Property



    Private _id As String
    Public Property Id() As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
        End Set
    End Property

    Private _idLot As String
    Public Property IdLot() As String
        Get
            Return _idLot
        End Get
        Set(ByVal value As String)
            _idLot = value
        End Set
    End Property

    Private _type As String
    Public Property Type() As String
        Get
            Return _type
        End Get
        Set(ByVal value As String)
            _type = value
        End Set
    End Property

    Private _dateInsertion As String
    Public Property DateInsertion() As String
        Get
            Return _dateInsertion
        End Get
        Set(ByVal value As String)
            _dateInsertion = value
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

    Private _entite As String
    Public Property Entite() As String
        Get
            Return _entite
        End Get
        Set(ByVal value As String)
            _entite = value
        End Set
    End Property

    Private _site As String
    Public Property Site() As String
        Get
            Return _site
        End Get
        Set(ByVal value As String)
            _site = value
        End Set
    End Property
End Class
