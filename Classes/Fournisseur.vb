Public Class Fournisseur

    Public Sub New()

        _code_sap = ""
        _nom_fournisseur = ""

    End Sub

    Public Sub New(p_code_sap As String, p_nom_fournisseur As String)

        _code_sap = p_code_sap
        _nom_fournisseur = p_nom_fournisseur

    End Sub

    Private _code_sap As String
    Public Property CodeSAP() As String
        Get
            Return _code_sap
        End Get
        Set(ByVal value As String)
            _code_sap = value
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



End Class
