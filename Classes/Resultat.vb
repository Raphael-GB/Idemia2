Public Class Resultat

    Public Sub New()

        _x = ""
        _y1 = 0
        _y2 = 0
        _y3 = 0

    End Sub

    Public Sub New(p_x As String, p_y1 As Double, p_y2 As Double, p_y3 As Double)

        _x = p_x
        _y1 = p_y1
        _y2 = p_y2
        _y3 = p_y3

    End Sub

    Private _x As String
    Public Property X() As String
        Get
            Return _x
        End Get
        Set(ByVal value As String)
            _x = value
        End Set
    End Property

    Private _y1 As Double
    Public Property Y1() As Double
        Get
            Return _y1
        End Get
        Set(ByVal value As Double)
            _y1 = value
        End Set
    End Property

    Private _y2 As Double
    Public Property Y2() As Double
        Get
            Return _y2
        End Get
        Set(ByVal value As Double)
            _y2 = value
        End Set
    End Property

    Private _y3 As Double
    Public Property Y3() As Double
        Get
            Return _y3
        End Get
        Set(ByVal value As Double)
            _y3 = value
        End Set
    End Property


End Class
