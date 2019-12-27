Public Class AlerteFacture_SortByStatut
    Implements IComparer(Of AlerteFacture)

    Public Function Compare(x As AlerteFacture, y As AlerteFacture) As Integer Implements IComparer(Of AlerteFacture).Compare

        If (x.Statut.Id > y.Statut.Id) Then
            Return 1
        ElseIf (x.Statut.Id < y.Statut.Id) Then
            Return -1
        Else
            Return 0
        End If

    End Function

End Class
