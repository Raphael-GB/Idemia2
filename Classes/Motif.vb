Public Class Motif

    Public Sub New()
        _id = ""
        _libelle = ""
        _workflow = ""
    End Sub

    Public Sub New(p_id As String, p_libelle As String, p_workflow As String)
        _id = p_id
        _libelle = p_libelle
        _workflow = p_workflow
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

    Private _libelle As String
    Public Property Libelle() As String
        Get
            Return _libelle
        End Get
        Set(ByVal value As String)
            _libelle = value
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


End Class
