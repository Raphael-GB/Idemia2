Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.html
Imports System.Drawing
Imports System.IO
Imports System.Threading

Public Class PDFFunctions


    Public Shared Function CreationPDFMulti(ByVal url_pdf_origine As String, ByVal liste_fichiers_ajoutes As List(Of FichierAjoute)) As Byte()
        Dim resultat As Byte() = Nothing
        Try
            Using ms As New MemoryStream()
                Using document As New Document()
                    Using pdfCopy As New PdfCopy(document, ms)
                        Dim reader As PdfReader = Nothing
                        Try
                            'ouverture du document
                            document.Open()

                            'ajout du premier fichier
                            reader = New PdfReader(url_pdf_origine)
                            pdfCopy.AddDocument(reader)
                            reader.Close()

                            'ajout des fichiers ajoutés
                            For Each fichier In liste_fichiers_ajoutes
                                reader = New PdfReader(fichier.Url)
                                pdfCopy.AddDocument(reader)
                                reader.Close()
                            Next
                        Catch ex As Exception
                            If reader IsNot Nothing Then
                                reader.Close()
                            End If
                        End Try
                    End Using
                End Using
                resultat = ms.ToArray()
            End Using
        Catch ex As Exception
            resultat = Nothing
        End Try
        Return resultat
    End Function

    'OB: Modification de la fonction le 03/09/2019 pour la prise en compte de l'orientation
    Public Shared Function CreationPDFMultiOld(ByVal url_pdf_origine As String, ByVal liste_fichiers_ajoutes As List(Of FichierAjoute)) As Byte()

        Dim resultat As Byte() = Nothing

        Try

            Using myMemoryStream As New MemoryStream()



                Dim myDocument As New iTextSharp.text.Document(PageSize.A4, 0, 0, 0, 0)
                Dim myPDFwriter As PdfWriter = PdfWriter.GetInstance(myDocument, myMemoryStream)
                Dim myPDFReader As PdfReader
                Dim cb As PdfContentByte
                Dim page As PdfImportedPage
                Dim bf As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
                Dim texte As String = ""

                myDocument.Open()

                PdfReader.unethicalreading = True

                'Fichier d'origine

                myPDFReader = New PdfReader(url_pdf_origine)
                myPDFReader.ConsolidateNamedDestinations()

                cb = myPDFwriter.DirectContent

                For i As Integer = 1 To myPDFReader.NumberOfPages

                    myDocument.NewPage()

                    page = myPDFwriter.GetImportedPage(myPDFReader, i)
                    cb.AddTemplate(page, 1, 0, 0, 1, 0, 0)

                Next

                'Fichiers ajoutés

                For Each fic As FichierAjoute In liste_fichiers_ajoutes

                    myPDFReader = New PdfReader(fic.Url)
                    myPDFReader.ConsolidateNamedDestinations()

                    cb = myPDFwriter.DirectContent

                    texte = "Ajouté par " & fic.AjoutePar & " le " & fic.DateAjout

                    For i As Integer = 1 To myPDFReader.NumberOfPages

                        myDocument.NewPage()

                        page = myPDFwriter.GetImportedPage(myPDFReader, i)

                        cb.BeginText()
                        cb.SetColorFill(BaseColor.DARK_GRAY)
                        cb.SetFontAndSize(bf, 10)
                        cb.ShowTextAligned(1, texte, 130, 10, 0)
                        cb.EndText()

                        cb.AddTemplate(page, 0, 0)

                    Next

                Next

                myDocument.Close()

                resultat = myMemoryStream.ToArray()

            End Using

        Catch ex As Exception

            resultat = Nothing

        End Try

        Return resultat

    End Function

End Class
