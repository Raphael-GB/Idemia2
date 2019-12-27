Imports System.IO

Public Class LogsFunctions

    Shared Sub LogWrite(ByVal logMessage As String, Optional ByVal user As Utilisateur = Nothing)
        Dim m_exePath As String = "\\10.168.94.12\e\SITES\CBWEBVALIDINVOICE\logs"
        Try
            Using w As StreamWriter = File.AppendText(Path.Combine(m_exePath, $"log_workflow_{Date.Now.ToString("ddMMyyyy")}.txt"))
                Log(logMessage, w, user)
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Shared Sub LogWrite2(ByVal logMessage As String, Optional ByVal user As Utilisateur = Nothing)
        Dim m_exePath As String = "\\10.168.94.12\e\SITES\CBWEBVALIDINVOICE\logs"
        Try
            Using w As StreamWriter = File.AppendText(Path.Combine(m_exePath, $"log_esoares_{Date.Now.ToString("ddMMyyyy")}.txt"))
                Log(logMessage, w, user)
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Shared Sub Log(ByVal logMessage As String, ByVal txtWriter As TextWriter, ByVal user As Utilisateur)
        Dim workflow As String = ""
        Dim identifiant As String = ""
        Try
            If Not user Is Nothing Then
                workflow = user.Workflow
                identifiant = user.Identifiant
            End If
            txtWriter.WriteLine($"[ERREUR] Client : {workflow} | Identifiant : {identifiant} | Date : {Date.Now.ToString}")
            txtWriter.WriteLine($"-------- {logMessage}")
            txtWriter.WriteLine($"--------")
        Catch ex As Exception

        End Try
    End Sub

End Class
