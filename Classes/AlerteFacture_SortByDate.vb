Public Class AlerteFacture_SortByDate
    Implements IComparer(Of AlerteFacture)

    Public Function Compare(x As AlerteFacture, y As AlerteFacture) As Integer Implements IComparer(Of AlerteFacture).Compare

        If (Convert.ToDateTime(x.DateAlerte) > Convert.ToDateTime(y.DateAlerte)) Then
            Return 1
        ElseIf (Convert.ToDateTime(x.DateAlerte) < Convert.ToDateTime(y.DateAlerte)) Then
            Return -1
        Else
            Return 0
        End If

    End Function

End Class
