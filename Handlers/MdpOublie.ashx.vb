Imports System.Web
Imports System.Web.Services
Imports System.Configuration

Public Class MdpOublie
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Request.Params("txtIdentifiantMdpOublie") Is Nothing And _
            Not context.Request.Params("txtEmailMdpOublie") Is Nothing) Then

            Dim identifiant As String = context.Request.Params("txtIdentifiantMdpOublie").ToString()
            Dim email As String = context.Request.Params("txtEmailMdpOublie").ToString()
            Dim id_controle As String = DBFunctions.ControleMdpOublie(identifiant, email)
            Dim titre_message = "Mot de passe oublié - Workflow de facture Groupe Bernard"
            Dim texte_message As String = ""
            Dim url_site As String = ConfigurationManager.AppSettings("url_site").ToString()


            If (id_controle <> "") Then

                Dim lien As String = url_site & "/ChangementMdp.aspx?id=" & id_controle

                texte_message = "<html>" & _
                                    "Bonjour,<br/><br/>" & _
                                    "Afin de réinitialiser votre mot de passe, veuillez cliquer <a href=""" & lien & """>Ici</a> ou copier le lien suivant dans votre navigateur : <br/><br/>" & _
                                    lien & "<br/><br/>" & _
                                    "Cordialement,<br/><br/>" & _
                                    "Groupe Bernard" & _
                                "</html>"

                MailFunctions.Envoyer("noreply@cba.fr", email, titre_message, texte_message, Nothing)

            End If

        End If

        context.Response.Redirect("../Connexion.aspx")

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class