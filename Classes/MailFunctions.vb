Imports System.Net.Mail

Public Class MailFunctions

    Private Shared Property _serveur_mail As String

    Public Shared Property ServeurMail As String
        Get
            Return _serveur_mail
        End Get
        Set(value As String)
            _serveur_mail = value
        End Set
    End Property

    Public Shared Function Envoyer(envoyeur As String, destinataires As String, sujet As String, message As String, liste_fichiers_joints As List(Of String)) As Boolean

        Dim resultat As String
        Dim fichier As Attachment

        Try
            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = True
            'Smtp_Server.Credentials = New Net.NetworkCredential("username@gmail.com", "password")
            'Smtp_Server.Port = 587
            'Smtp_Server.EnableSsl = True
            Smtp_Server.Host = _serveur_mail

            e_mail = New MailMessage()
            e_mail.From = New MailAddress(envoyeur)
            e_mail.To.Add(destinataires)
            e_mail.Subject = sujet
            e_mail.IsBodyHtml = True
            e_mail.Body = message

            If (Not liste_fichiers_joints Is Nothing) Then
                For Each f As String In liste_fichiers_joints
                    fichier = New Attachment(f)
                    e_mail.Attachments.Add(fichier)
                Next
            End If

            Smtp_Server.Send(e_mail)

            resultat = True

        Catch error_t As Exception
            resultat = False
        End Try

        Return resultat

    End Function

End Class
