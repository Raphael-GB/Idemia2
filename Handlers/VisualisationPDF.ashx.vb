Imports System.Web
Imports System.Web.Services
Imports System.IO

Public Class VisualisationPDF
    Implements System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        If (Not context.Session("Utilisateur") Is Nothing) Then

            If (Not context.Request.QueryString("docid") Is Nothing) Then

                Dim user As Utilisateur = CType(context.Session("Utilisateur"), Utilisateur)
                Dim docid As String = context.Request.QueryString("docid").ToString()
                Dim Filename As String = context.Request.QueryString("filename").ToString()
                Dim urlPDF As String = DBFunctions.getUrlPDF(user, docid)


                If (urlPDF.Trim() <> "") Then

                    Dim liste_fichiers As New List(Of FichierAjoute)

                    liste_fichiers = DBFunctions.getFichiersAjoutes(user, docid)
                  
                    Dim fichier As Byte() = PDFFunctions.CreationPDFMulti(urlPDF, liste_fichiers)

                    If (Not fichier Is Nothing) Then

                        context.Response.ContentType = "application/pdf"
                        If (Not context.Request.QueryString("filename") Is Nothing) Then
                            context.Response.AppendHeader("Content-Disposition", "inline; filename=" & Filename & ".pdf")
                        Else
                            context.Response.AppendHeader("Content-Disposition", "inline; filename=" & docid & ".pdf")
                        End If

                        context.Response.BinaryWrite(fichier)
                        context.Response.End()

                    Else

                        context.Response.Write("Fichier non disponible - " & urlPDF)

                    End If

                Else

                    context.Response.Write("Fichier non disponible")

                End If

            Else

                context.Response.Write("Fichier non disponible")

            End If

        Else

            context.Response.Redirect("../Connexion.aspx")

        End If

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class