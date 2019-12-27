Imports System.Security.Cryptography
Imports System.Text
Imports Oracle.ManagedDataAccess.Client

Public Class DBFunctions

    Private Shared Property _chaineConnexion As String
    Public Shared Property ChaineConnexion As String
        Get
            Return _chaineConnexion
        End Get
        Set(value As String)
            _chaineConnexion = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.94.1.242)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=swt)));User Id=ADM_WORKFLOW;Password=devel;"
        End Set
    End Property

    Private Shared Property _table_facture As String
    Public Shared Property TableFacture As String
        Get
            Return _table_facture
        End Get
        Set(value As String)
            _table_facture = value
        End Set
    End Property

    Private Shared Property _table_ligne_facture As String
    Public Shared Property TableLigneFacture As String
        Get
            Return _table_ligne_facture
        End Get
        Set(value As String)
            _table_ligne_facture = value
        End Set
    End Property

    Public Shared Function GetMd5Hash(ByVal input As String) As String

        Dim md5Hash As MD5 = MD5.Create()

        ' Convert the input string to a byte array and compute the hash.
        Dim data As Byte() = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input))

        ' Create a new Stringbuilder to collect the bytes
        ' and create a string.
        Dim sBuilder As New StringBuilder()

        ' Loop through each byte of the hashed data 
        ' and format each one as a hexadecimal string.
        Dim i As Integer
        For i = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next i

        ' Return the hexadecimal string.
        Return sBuilder.ToString()

    End Function 'GetMd5Hash

    ' Verify a hash against a string.
    Public Shared Function VerifyMd5Hash(ByVal md5Hash As MD5, ByVal input As String, ByVal hash As String) As Boolean
        ' Hash the input.
        Dim hashOfInput As String = GetMd5Hash(input)

        ' Create a StringComparer an compare the hashes.
        Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase

        If 0 = comparer.Compare(hashOfInput, hash) Then
            Return True
        Else
            Return False
        End If

    End Function 'VerifyMd5Hash

    Public Shared Function connexion(identifiant As String, mdp As String) As Utilisateur

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String = ""
        Dim resultat As Utilisateur

        Try
            resultat = Nothing
            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            identifiant = identifiant.Replace("'", "''")
            mdp = mdp.Replace("'", "")

            Try
                requete = "select u.*,c.libelle_civilite,wc.nb_max_utilisateur,wc.requete_fournisseurs,wc.date_demarrage,wc.vue_facture,wc.vue_ligne_facture,wc.delai_rejet_archive, " &
                    "corbeille_a_traiter,corbeille_rejet,corbeille_suivi,corbeille_archive,corbeille_validation,corbeille_matching_ko,gestion_validation,gestion_ligne,valorisation_automatique,id_champ_pivot_valorisation,alerte_mail_valideur " &
                    "from utilisateur u " &
                    "inner join civilite c on (u.id_civilite=c.id_civilite) " &
                    "inner join workflow_client wc on (u.workflow=wc.workflow) " &
                    "where u.identifiant='" & identifiant & "' " &
                    "and u.mot_de_passe='" & mdp & "' "

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                If (ora_dr.Read()) Then

                    resultat = New Utilisateur()
                    resultat.Identifiant = ora_dr("IDENTIFIANT").ToString()
                    resultat.IdentifiantSSO = ora_dr("IDENTIFIANT_SSO").ToString()
                    resultat.IDProfil = ora_dr("ID_PROFIL").ToString()
                    resultat.Nom = ora_dr("NOM").ToString()
                    resultat.Prenom = ora_dr("PRENOM").ToString()
                    resultat.Fonction = ora_dr("LIBELLE_FONCTION").ToString()
                    resultat.Workflow = ora_dr("WORKFLOW").ToString()
                    resultat.Email = ora_dr("EMAIL").ToString()
                    resultat.Civilite = New Civilite(ora_dr("ID_CIVILITE").ToString(), ora_dr("LIBELLE_CIVILITE").ToString())
                    resultat.RequeteFournisseurs = ora_dr("REQUETE_FOURNISSEURS").ToString()
                    resultat.DateDemarrage = ora_dr("DATE_DEMARRAGE").ToString()
                    resultat.IdChampPivotValorisation = ora_dr("ID_CHAMP_PIVOT_VALORISATION").ToString()
                    resultat.ListeChampsCorrection = getChampsACorriger(resultat)
                    resultat.ListeChampsEnrichissement = getChampsAEnrichir(resultat)

                    If (ora_dr("DELAI_REJET_ARCHIVE").ToString() <> "") Then
                        resultat.DelaiRejetArchive = ora_dr("DELAI_REJET_ARCHIVE").ToString()
                    Else
                        resultat.DelaiRejetArchive = ""
                    End If

                    If (ora_dr("VUE_FACTURE").ToString().Trim() <> "") Then
                        resultat.TableFacture = ora_dr("VUE_FACTURE").ToString().Trim()
                    Else
                        resultat.TableFacture = ConfigurationManager.AppSettings("table_facture_defaut").ToString()
                    End If

                    If (ora_dr("VUE_LIGNE_FACTURE").ToString().Trim() <> "") Then
                        resultat.TableLigneFacture = ora_dr("VUE_LIGNE_FACTURE").ToString().Trim()
                    Else
                        resultat.TableLigneFacture = ConfigurationManager.AppSettings("table_ligne_facture_defaut").ToString()
                    End If

                    resultat.CorbeilleATraiter = ora_dr("CORBEILLE_A_TRAITER").ToString()
                    resultat.CorbeilleRejet = ora_dr("CORBEILLE_REJET").ToString()
                    resultat.CorbeilleSuivi = ora_dr("CORBEILLE_SUIVI").ToString()
                    resultat.CorbeilleArchive = ora_dr("CORBEILLE_ARCHIVE").ToString()
                    resultat.CorbeilleValidation = ora_dr("CORBEILLE_VALIDATION").ToString()
                    resultat.CorbeilleMatchingKO = ora_dr("CORBEILLE_MATCHING_KO").ToString()
                    resultat.GestionValidation = ora_dr("GESTION_VALIDATION").ToString()
                    resultat.GestionLigne = ora_dr("GESTION_LIGNE").ToString()
                    resultat.AlerteMailValideur = ora_dr("ALERTE_MAIL_VALIDEUR").ToString()

                End If

                ora_dr.Close()

            Catch ex As Exception
                resultat = Nothing
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
                LogsFunctions.LogWrite(requete)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = Nothing
            LogsFunctions.LogWrite(ex.ToString())
            LogsFunctions.LogWrite(requete)
        End Try

        Return resultat

    End Function

    Public Shared Function connexion_sso(p_identifiant_sso As String, p_chaine_connexion As String) As Utilisateur

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String = ""
        Dim resultat As Utilisateur

        Try
            resultat = Nothing
            ora_connexion = New OracleConnection(p_chaine_connexion)
            ora_connexion.Open()

            p_identifiant_sso = p_identifiant_sso.Replace("'", "''")

            Try
                requete = "select u.*,c.libelle_civilite,wc.nb_max_utilisateur,wc.requete_fournisseurs,wc.date_demarrage,wc.vue_facture,wc.vue_ligne_facture,wc.delai_rejet_archive, " &
                    "corbeille_a_traiter,corbeille_rejet,corbeille_suivi,corbeille_archive,corbeille_validation,corbeille_matching_ko,gestion_validation,gestion_ligne,valorisation_automatique,id_champ_pivot_valorisation,alerte_mail_valideur " &
                    "from utilisateur u " &
                    "inner join civilite c on (u.id_civilite=c.id_civilite) " &
                    "inner join workflow_client wc on (u.workflow=wc.workflow) " &
                    "where u.identifiant_sso='" & p_identifiant_sso & "'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                If (ora_dr.Read()) Then

                    resultat = New Utilisateur()
                    resultat.Identifiant = ora_dr("IDENTIFIANT").ToString()
                    resultat.IdentifiantSSO = ora_dr("IDENTIFIANT_SSO").ToString()
                    resultat.IDProfil = ora_dr("ID_PROFIL").ToString()
                    resultat.Nom = ora_dr("NOM").ToString()
                    resultat.Prenom = ora_dr("PRENOM").ToString()
                    resultat.Fonction = ora_dr("LIBELLE_FONCTION").ToString()
                    resultat.Workflow = ora_dr("WORKFLOW").ToString()
                    resultat.Email = ora_dr("EMAIL").ToString()
                    resultat.Civilite = New Civilite(ora_dr("ID_CIVILITE").ToString(), ora_dr("LIBELLE_CIVILITE").ToString())
                    resultat.RequeteFournisseurs = ora_dr("REQUETE_FOURNISSEURS").ToString()
                    resultat.DateDemarrage = ora_dr("DATE_DEMARRAGE").ToString()
                    resultat.IdChampPivotValorisation = ora_dr("ID_CHAMP_PIVOT_VALORISATION").ToString()
                    resultat.ListeChampsCorrection = getChampsACorriger(resultat)
                    resultat.ListeChampsEnrichissement = getChampsAEnrichir(resultat)

                    If (ora_dr("DELAI_REJET_ARCHIVE").ToString() <> "") Then
                        resultat.DelaiRejetArchive = ora_dr("DELAI_REJET_ARCHIVE").ToString()
                    Else
                        resultat.DelaiRejetArchive = ""
                    End If

                    If (ora_dr("VUE_FACTURE").ToString().Trim() <> "") Then
                        resultat.TableFacture = ora_dr("VUE_FACTURE").ToString().Trim()
                    Else
                        resultat.TableFacture = ConfigurationManager.AppSettings("table_facture_defaut").ToString()
                    End If

                    If (ora_dr("VUE_LIGNE_FACTURE").ToString().Trim() <> "") Then
                        resultat.TableLigneFacture = ora_dr("VUE_LIGNE_FACTURE").ToString().Trim()
                    Else
                        resultat.TableLigneFacture = ConfigurationManager.AppSettings("table_ligne_facture_defaut").ToString()
                    End If

                    resultat.CorbeilleATraiter = ora_dr("CORBEILLE_A_TRAITER").ToString()
                    resultat.CorbeilleRejet = ora_dr("CORBEILLE_REJET").ToString()
                    resultat.CorbeilleSuivi = ora_dr("CORBEILLE_SUIVI").ToString()
                    resultat.CorbeilleArchive = ora_dr("CORBEILLE_ARCHIVE").ToString()
                    resultat.CorbeilleValidation = ora_dr("CORBEILLE_VALIDATION").ToString()
                    resultat.CorbeilleMatchingKO = ora_dr("CORBEILLE_MATCHING_KO").ToString()
                    resultat.GestionValidation = ora_dr("GESTION_VALIDATION").ToString()
                    resultat.GestionLigne = ora_dr("GESTION_LIGNE").ToString()
                    resultat.AlerteMailValideur = ora_dr("ALERTE_MAIL_VALIDEUR").ToString()

                End If

                ora_dr.Close()

            Catch ex As Exception
                resultat = Nothing
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
                LogsFunctions.LogWrite(requete)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = Nothing
            LogsFunctions.LogWrite(ex.ToString())
            LogsFunctions.LogWrite(requete)
        End Try

        Return resultat

    End Function

    Public Shared Function connexion_historique(user As Utilisateur, etat As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As Integer
        Dim requete As String = ""
        Dim resultat As Boolean = False

        Try
            resultat = Nothing
            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try
                requete = $"insert into historique_connexion(identifiant, id_client, etat) values ('{user.Identifiant}', '{user.Workflow}', '{etat}')"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteNonQuery()

                If (ora_dr > -1) Then
                    resultat = True
                End If

            Catch ex As Exception
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
                LogsFunctions.LogWrite(requete)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
            LogsFunctions.LogWrite(requete)
        End Try

        Return resultat

    End Function

    Public Shared Function getAbsence(user As Utilisateur, id_statut As String) As List(Of Absences)

        Dim ora_connexion As OracleConnection
        Dim ora_commande_message, ora_commande_destinataire As OracleCommand
        Dim ora_dr_message, ora_dr_destinataire As OracleDataReader
        Dim requete_message As String = ""
        Dim requete_destinataire As String = ""
        Dim resultat As New List(Of Absences)
        Dim message_temp As Absences
        Dim liste_destinataires As List(Of String)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            user.Identifiant = user.Identifiant.Replace("'", "''").Trim()
            id_statut = id_statut.Replace("'", "''").Trim()

            Try

                requete_message = "select a.*, u.* from attribution_absence a, utilisateur u where a.utilisateur_absent = u.identifiant and a.workflow=u.workflow and a.utilisateur_absent='" & user.Identifiant & "' and a.workflow='" & user.Workflow & "' and a.cloture='" & id_statut & "' order by date_debut desc"


                ora_commande_message = New OracleCommand(requete_message, ora_connexion)
                ora_dr_message = ora_commande_message.ExecuteReader()

                While (ora_dr_message.Read())

                    message_temp = New Absences()
                    message_temp.Workflow = ora_dr_message("WORKFLOW").ToString()
                    message_temp.IDAbsence = ora_dr_message("ID_ABSENCE").ToString()
                    message_temp.IdentifiantUtilisateur = ora_dr_message("IDENTIFIANT").ToString()
                    message_temp.Statut = ora_dr_message("CLOTURE").ToString()
                    message_temp.DateDebut = ora_dr_message("DATE_DEBUT").ToString()
                    message_temp.DateFin = ora_dr_message("DATE_FIN").ToString()
                    message_temp.DateInsertion = ora_dr_message("DATE_INSERTION").ToString()

                    liste_destinataires = New List(Of String)(ora_dr_message("DESTINATAIRES").ToString().Split(","c))

                    For Each email_dest As String In liste_destinataires

                        email_dest = email_dest.Replace("'", "")
                        requete_destinataire = "select identifiant, nom, prenom, email from utilisateur where email='" & email_dest & "' and workflow='" & user.Workflow & "'"
                        ora_commande_destinataire = New OracleCommand(requete_destinataire, ora_connexion)
                        ora_dr_destinataire = ora_commande_destinataire.ExecuteReader()

                        If (ora_dr_destinataire.Read()) Then
                            message_temp.Destinataires.Add(New Contact(ora_dr_destinataire.Item("IDENTIFIANT").ToString(), ora_dr_destinataire.Item("EMAIL").ToString(), ora_dr_destinataire.Item("NOM").ToString(), ora_dr_destinataire.Item("PRENOM").ToString()))
                        Else
                            message_temp.Destinataires.Add(New Contact("", email_dest, "", ""))
                        End If

                        ora_dr_destinataire.Close()

                    Next

                    resultat.Add(message_temp)

                End While

                ora_dr_message.Close()

            Catch ex As Exception
                resultat = New List(Of Absences)
                ora_connexion.Close()

                LogsFunctions.LogWrite(ex.ToString(), user)
                LogsFunctions.LogWrite(requete_message, user)
                LogsFunctions.LogWrite(requete_destinataire, user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Absences)
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function getMessages(user As Utilisateur, id_statut As String, id_lecture As String) As List(Of Message)

        Dim ora_connexion As OracleConnection
        Dim ora_commande_message, ora_commande_destinataire As OracleCommand
        Dim ora_dr_message, ora_dr_destinataire As OracleDataReader
        Dim requete_message As String = ""
        Dim requete_destinataire As String = ""
        Dim resultat As New List(Of Message)
        Dim message_temp As Message
        Dim expediteur_temp As Contact
        Dim liste_destinataires As List(Of String)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            user.Identifiant = user.Identifiant.Replace("'", "''").Trim()
            id_statut = id_statut.Replace("'", "''").Trim()
            id_lecture = id_lecture.Replace("'", "''").Trim()

            Try
                requete_message = "select m.*,s.libelle_statut_message,l.libelle_statut_lecture,u.nom as nom_expediteur,u.prenom as prenom_expediteur,u.email as email_expediteur from message m " &
                    "inner join statut_message s on (s.id_statut_message=m.id_statut_message) " &
                    "inner join statut_lecture l on (l.id_statut_lecture=m.id_statut_lecture) " &
                    "left join utilisateur u on (u.identifiant=m.expediteur) " &
                    "where m.identifiant='" & user.Identifiant & "' and m.workflow='" & user.Workflow & "'"

                If (id_statut <> "") Then
                    requete_message += " and s.id_statut_message='" & id_statut & "' "
                End If

                If (id_lecture <> "") Then
                    requete_message += " and l.id_statut_lecture='" & id_lecture & "' "
                End If

                requete_message += " order by m.date_message desc"

                ora_commande_message = New OracleCommand(requete_message, ora_connexion)
                ora_dr_message = ora_commande_message.ExecuteReader()

                While (ora_dr_message.Read())

                    message_temp = New Message()
                    message_temp.Workflow = ora_dr_message("WORKFLOW").ToString()
                    message_temp.IDMessage = ora_dr_message("ID_MESSAGE").ToString()
                    message_temp.IdentifiantUtilisateur = ora_dr_message("IDENTIFIANT").ToString()
                    message_temp.Statut = New Statut(ora_dr_message("ID_STATUT_MESSAGE").ToString(), ora_dr_message("LIBELLE_STATUT_MESSAGE").ToString())
                    message_temp.Sujet = ora_dr_message("SUJET").ToString()
                    message_temp.Texte = ora_dr_message("TEXTE").ToString()
                    message_temp.Lecture = New Statut(ora_dr_message("ID_STATUT_LECTURE").ToString(), ora_dr_message("LIBELLE_STATUT_LECTURE").ToString())
                    message_temp.DateMessage = ora_dr_message("DATE_MESSAGE").ToString()

                    expediteur_temp = New Contact(ora_dr_message("EXPEDITEUR").ToString(), ora_dr_message("EMAIL_EXPEDITEUR").ToString(), ora_dr_message("NOM_EXPEDITEUR").ToString(), ora_dr_message("PRENOM_EXPEDITEUR").ToString())
                    message_temp.Expediteur = expediteur_temp

                    liste_destinataires = New List(Of String)(ora_dr_message("DESTINATAIRES").ToString().Split(","c))

                    For Each email_dest As String In liste_destinataires

                        email_dest = email_dest.Replace("'", "")
                        requete_destinataire = "select identifiant, nom, prenom, email from utilisateur where email='" & email_dest & "'"
                        ora_commande_destinataire = New OracleCommand(requete_destinataire, ora_connexion)
                        ora_dr_destinataire = ora_commande_destinataire.ExecuteReader()

                        If (ora_dr_destinataire.Read()) Then
                            message_temp.Destinataires.Add(New Contact(ora_dr_destinataire.Item("IDENTIFIANT").ToString(), ora_dr_destinataire.Item("EMAIL").ToString(), ora_dr_destinataire.Item("NOM").ToString(), ora_dr_destinataire.Item("PRENOM").ToString()))
                        Else
                            message_temp.Destinataires.Add(New Contact("", email_dest, "", ""))
                        End If

                        ora_dr_destinataire.Close()

                    Next

                    resultat.Add(message_temp)

                End While

                ora_dr_message.Close()

            Catch ex As Exception
                resultat = New List(Of Message)
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), user)
                LogsFunctions.LogWrite(requete_message, user)
                LogsFunctions.LogWrite(requete_destinataire, user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Message)
            LogsFunctions.LogWrite(ex.ToString(), user)
            LogsFunctions.LogWrite(requete_message, user)
            LogsFunctions.LogWrite(requete_destinataire, user)
        End Try

        Return resultat

    End Function

    Public Shared Function getInfosMessage(id_message As String, identifiant As String) As Message

        Dim ora_connexion As OracleConnection
        Dim ora_commande_message, ora_commande_destinataire As OracleCommand
        Dim ora_dr_message, ora_dr_destinataire As OracleDataReader
        Dim requete_message As String = ""
        Dim requete_destinataire As String = ""
        Dim resultat As Message
        Dim expediteur_temp As Contact
        Dim liste_destinataires As List(Of String)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            resultat = Nothing
            identifiant = identifiant.Replace("'", "''").Trim()
            id_message = id_message.Replace("'", "''").Trim()

            Try
                requete_message = "select m.*,s.libelle_statut_message,l.libelle_statut_lecture,u.nom as nom_expediteur,u.prenom as prenom_expediteur,u.email as email_expediteur from message m " &
                    "inner join statut_message s on s.id_statut_message=m.id_statut_message " &
                    "inner join statut_lecture l on l.id_statut_lecture=m.id_statut_lecture " &
                    "left join utilisateur u on u.identifiant=m.expediteur " &
                    "where m.identifiant='" & identifiant & "' and m.id_message='" & id_message & "'"

                ora_commande_message = New OracleCommand(requete_message, ora_connexion)
                ora_dr_message = ora_commande_message.ExecuteReader()

                If (ora_dr_message.Read()) Then

                    resultat = New Message()
                    resultat.IDMessage = ora_dr_message("ID_MESSAGE").ToString()
                    resultat.IdentifiantUtilisateur = ora_dr_message("IDENTIFIANT").ToString()
                    resultat.Statut = New Statut(ora_dr_message("ID_STATUT_MESSAGE").ToString(), ora_dr_message("LIBELLE_STATUT_MESSAGE").ToString())
                    resultat.Sujet = ora_dr_message("SUJET").ToString()
                    resultat.Texte = ora_dr_message("TEXTE").ToString()
                    resultat.Lecture = New Statut(ora_dr_message("ID_STATUT_LECTURE").ToString(), ora_dr_message("LIBELLE_STATUT_LECTURE").ToString())
                    resultat.DateMessage = ora_dr_message("DATE_MESSAGE").ToString()

                    expediteur_temp = New Contact(ora_dr_message("EXPEDITEUR").ToString(), ora_dr_message("EMAIL_EXPEDITEUR").ToString(), ora_dr_message("NOM_EXPEDITEUR").ToString(), ora_dr_message("PRENOM_EXPEDITEUR").ToString())
                    resultat.Expediteur = expediteur_temp

                    liste_destinataires = New List(Of String)(ora_dr_message("DESTINATAIRES").ToString().Split(","c))

                    For Each email_dest As String In liste_destinataires

                        email_dest = email_dest.Replace("'", "")
                        requete_destinataire = "select identifiant, nom, prenom, email from utilisateur where email='" & email_dest & "'"
                        ora_commande_destinataire = New OracleCommand(requete_destinataire, ora_connexion)
                        ora_dr_destinataire = ora_commande_destinataire.ExecuteReader()

                        If (ora_dr_destinataire.Read()) Then
                            resultat.Destinataires.Add(New Contact(ora_dr_destinataire.Item("IDENTIFIANT").ToString(), ora_dr_destinataire.Item("EMAIL").ToString(), ora_dr_destinataire.Item("NOM").ToString(), ora_dr_destinataire.Item("PRENOM").ToString()))
                        Else
                            resultat.Destinataires.Add(New Contact("", email_dest, "", ""))
                        End If

                        ora_dr_destinataire.Close()

                    Next

                End If

                ora_dr_message.Close()

            Catch ex As Exception
                resultat = Nothing
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
                LogsFunctions.LogWrite(requete_message)
                LogsFunctions.LogWrite(requete_destinataire)

            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = Nothing
            LogsFunctions.LogWrite(ex.ToString())
            LogsFunctions.LogWrite(requete_message)
            LogsFunctions.LogWrite(requete_destinataire)
        End Try

        Return resultat

    End Function
    Public Shared Function DeleteAbsence(id As String, identifiant As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete_message As String = ""
        Dim resultat As Boolean
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            resultat = False
            identifiant = identifiant.Replace("'", "''").Trim()
            id = id.Replace("'", "''").Trim()

            Try
                requete_message = "delete from attribution_absence " &
                    "where utilisateur_absent='" & identifiant & "' and id_absence='" & id & "'"

                ora_commande = New OracleCommand(requete_message, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb > 0) Then

                    resultat = True

                End If

            Catch ex As Exception
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
                LogsFunctions.LogWrite(requete_message)

            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
            LogsFunctions.LogWrite(requete_message)
        End Try

        Return resultat

    End Function

    Public Shared Function DeleteMessage(id_message As String, identifiant As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete_message As String
        Dim resultat As Boolean
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            resultat = False
            identifiant = identifiant.Replace("'", "''").Trim()
            id_message = id_message.Replace("'", "''").Trim()

            Try
                requete_message = "delete from message " &
                    "where identifiant='" & identifiant & "' and id_message='" & id_message & "'"

                ora_commande = New OracleCommand(requete_message, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb > 0) Then

                    resultat = True

                End If

            Catch ex As Exception
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function getDestinataires(user As Utilisateur) As List(Of Contact)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of Contact)
        Dim workflow As String
        Dim destinataire_temp As Contact

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            workflow = user.Workflow.Replace("'", "''").Trim()

            Try
                requete = "select distinct identifiant,email,nom,prenom " &
                    "from utilisateur " &
                    "where workflow='" & workflow & "' " &
                    "order by identifiant"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    destinataire_temp = New Contact()
                    destinataire_temp.Identifiant = ora_dr("IDENTIFIANT").ToString()
                    destinataire_temp.Email = ora_dr("EMAIL").ToString()
                    destinataire_temp.Nom = ora_dr("NOM").ToString()
                    destinataire_temp.Prenom = ora_dr("PRENOM").ToString()

                    resultat.Add(destinataire_temp)

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of Contact)
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Contact)
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function EnvoyerAbsence(user As Utilisateur, expediteur As String, destinataires As String, debut As String, fin As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_commande_utilisateur As OracleCommand
        Dim ora_dr_utilisateur As OracleDataReader
        Dim requete_reception, requete_envoi, requete_utilisateur, requete_absence As String
        Dim resultat As Boolean = False
        Dim nb As Integer

        Dim identifiant, workflow, email, date_message As String
        Dim liste_destinataires As New List(Of String)

        Dim message As String
        Dim sujet As String

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try
                Dim utilisateur As Utilisateur = DBFunctions.getInfosUtilisateur(expediteur, user.Workflow)

                If utilisateur Is Nothing Then GoTo fin

                identifiant = utilisateur.Identifiant.Replace("'", "''").Trim()
                email = utilisateur.Email.Replace("'", "''").Trim()
                workflow = user.Workflow.Replace("'", "''").Trim()
                debut = debut.Replace("'", "''").Trim()
                fin = fin.Replace("'", "''").Trim()

                'Date message 
                date_message = Date.Now().ToString()

                'découpe de destinataires
                destinataires = destinataires.Replace(" ", "").Replace(",,", ",").Replace("'", "")
                liste_destinataires = New List(Of String)(destinataires.Split(","c))

                sujet = $"Absence {utilisateur.Prenom} {utilisateur.Nom}"
                message = $"Bonjour, {utilisateur.Civilite.Libelle} {utilisateur.Prenom} {utilisateur.Nom} est indisponible sur la période du {debut} au {fin}. Les documents, associés à son compte, vous seront partagés pendant cette absence."

                requete_absence = "insert into attribution_absence(UTILISATEUR_ABSENT,DESTINATAIRES, DATE_DEBUT, DATE_FIN, WORKFLOW) " &
                    $"values ('{utilisateur.Identifiant}','{destinataires}',to_date('{debut}','dd/MM/yyyy'),to_date('{fin}','dd/MM/yyyy'),'{workflow}')"

                ora_commande = New OracleCommand(requete_absence, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb = 1) Then
                    'Insertion dans les messages envoyés de l'expéditeur
                    requete_envoi = "insert into message(workflow,identifiant,id_statut_message,texte,id_statut_lecture,expediteur,sujet,date_message,destinataires) " &
                    "values('" & workflow & "','" & identifiant & "','1','" & message & "','1','" & identifiant & "','" & sujet & "',to_date('" & date_message & "','DD/MM/YYYY HH24:MI:SS'),'" & destinataires & "')"

                    ora_commande = New OracleCommand(requete_envoi, ora_connexion)
                    nb = ora_commande.ExecuteNonQuery()

                    If (nb = 1) Then

                        For Each email_dest As String In liste_destinataires

                            If (email_dest.Trim() <> "") Then

                                'recherche du destinataire dans la liste des utilisateur
                                requete_utilisateur = "select identifiant from utilisateur where email='" & email_dest & "'"
                                ora_commande_utilisateur = New OracleCommand(requete_utilisateur, ora_connexion)
                                ora_dr_utilisateur = ora_commande_utilisateur.ExecuteReader()

                                While (ora_dr_utilisateur.Read())

                                    'Insertion dans les messages reçus des utilisateurs
                                    requete_reception = "insert into message(workflow,identifiant,id_statut_message,texte,id_statut_lecture,expediteur,sujet,date_message,destinataires) " &
                                "values('" & workflow & "','" & ora_dr_utilisateur.Item("IDENTIFIANT") & "','0','" & message & "','0','" & identifiant & "','" & sujet & "',to_date('" & date_message & "','DD/MM/YYYY HH24:MI:SS'),'" & destinataires & "')"
                                    ora_commande = New OracleCommand(requete_reception, ora_connexion)
                                    nb = ora_commande.ExecuteNonQuery()

                                End While

                                ora_dr_utilisateur.Close()

                            End If

                        Next
                        'envoi par mail car tous les destinataires ne sont pas des utilisateurs
                        MailFunctions.Envoyer(email, destinataires, sujet, message, Nothing)
                    End If
                End If

            Catch ex As Exception
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try
fin:
        Return resultat

    End Function


    Public Shared Function EnvoyerMessage(user As Utilisateur, destinataires As String, sujet As String, message As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_commande_utilisateur As OracleCommand
        Dim ora_dr_utilisateur As OracleDataReader
        Dim requete_reception, requete_envoi, requete_utilisateur As String
        Dim resultat As Boolean = False
        Dim nb As Integer

        Dim identifiant, workflow, email, date_message As String
        Dim liste_destinataires As New List(Of String)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try
                identifiant = user.Identifiant.Replace("'", "''").Trim()
                email = user.Email.Replace("'", "''").Trim()
                workflow = user.Workflow.Replace("'", "''").Trim()
                sujet = sujet.Replace("'", "''").Trim()
                message = message.Replace("'", "''").Trim()

                'Date message 
                date_message = Date.Now().ToString()

                'découpe de destinataires
                destinataires = destinataires.Replace(" ", "").Replace(",,", ",").Replace("'", "")
                liste_destinataires = New List(Of String)(destinataires.Split(","c))

                'Insertion dans les messages envoyés de l'expéditeur
                requete_envoi = "insert into message(workflow,identifiant,id_statut_message,texte,id_statut_lecture,expediteur,sujet,date_message,destinataires) " &
                    "values('" & workflow & "','" & identifiant & "','1','" & message & "','1','" & identifiant & "','" & sujet & "',to_date('" & date_message & "','DD/MM/YYYY HH24:MI:SS'),'" & destinataires & "')"

                ora_commande = New OracleCommand(requete_envoi, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb = 1) Then

                    For Each email_dest As String In liste_destinataires

                        If (email_dest.Trim() <> "") Then

                            'recherche du destinataire dans la liste des utilisateur
                            requete_utilisateur = "select identifiant from utilisateur where email='" & email_dest & "'"
                            ora_commande_utilisateur = New OracleCommand(requete_utilisateur, ora_connexion)
                            ora_dr_utilisateur = ora_commande_utilisateur.ExecuteReader()

                            While (ora_dr_utilisateur.Read())

                                'Insertion dans les messages reçus des utilisateurs
                                requete_reception = "insert into message(workflow,identifiant,id_statut_message,texte,id_statut_lecture,expediteur,sujet,date_message,destinataires) " &
                                "values('" & workflow & "','" & ora_dr_utilisateur.Item("IDENTIFIANT") & "','0','" & message & "','0','" & identifiant & "','" & sujet & "',to_date('" & date_message & "','DD/MM/YYYY HH24:MI:SS'),'" & destinataires & "')"
                                ora_commande = New OracleCommand(requete_reception, ora_connexion)
                                nb = ora_commande.ExecuteNonQuery()

                            End While

                            ora_dr_utilisateur.Close()

                        End If

                    Next

                    'envoi par mail car tous les destinataires ne sont pas des utilisateurs
                    MailFunctions.Envoyer(email, destinataires, sujet, message, Nothing)

                End If


            Catch ex As Exception
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function SetLectureMessage(id_message As String, identifiant As String, valeur As String)
        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete_message As String
        Dim resultat As Boolean
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            resultat = False
            identifiant = identifiant.Replace("'", "''").Trim()
            id_message = id_message.Replace("'", "").Trim()
            valeur = valeur.Replace("'", "").Trim()

            Try
                requete_message = "update message set id_statut_lecture='" & valeur & "' " &
                    "where identifiant='" & identifiant & "' and id_message='" & id_message & "'"

                ora_commande = New OracleCommand(requete_message, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb > 0) Then
                    resultat = True
                End If

            Catch ex As Exception
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat
    End Function

    Public Shared Function getEvenements(user As Utilisateur, flag_exclusion_depasses As String) As List(Of EvenementCalendrier)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of EvenementCalendrier)
        Dim identifiant As String
        Dim evenement_temp As EvenementCalendrier

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            identifiant = user.Identifiant.Replace("'", "''").Trim()

            Try
                requete = "select * " &
                    "from evenement " &
                    "where identifiant='" & identifiant & "' and workflow='" & user.Workflow & "' "

                If (flag_exclusion_depasses = "1") Then
                    requete += " and date_debut>=sysdate "
                End If

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    evenement_temp = New EvenementCalendrier()
                    evenement_temp.Workflow = ora_dr("WORKFLOW").ToString()
                    evenement_temp.Identifiant = ora_dr("IDENTIFIANT").ToString()
                    evenement_temp.IDEvenement = ora_dr("ID_EVENEMENT").ToString()
                    evenement_temp.Titre = ora_dr("TITRE").ToString()
                    evenement_temp.DateDebut = Format(ora_dr("DATE_DEBUT"), "yyyy-MM-dd HH:mm")
                    evenement_temp.DateFin = Format(ora_dr("DATE_FIN"), "yyyy-MM-dd HH:mm")
                    evenement_temp.Description = ora_dr("DESCRIPTION").ToString()

                    resultat.Add(evenement_temp)

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of EvenementCalendrier)
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of EvenementCalendrier)
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function IsValidEmail(ByVal email As String) As Boolean

        Dim expr As String = "^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$"
        Dim emailExpr As New System.Text.RegularExpressions.Regex(expr)
        Return emailExpr.IsMatch(email)

    End Function

    Public Shared Function getSugessionEmail(user As Utilisateur, contenu As String) As List(Of String)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of String)
        Dim identifiant As String

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            identifiant = user.Identifiant.Replace("'", "''").Trim()
            contenu = contenu.Replace("'", "''")

            Try

                requete = $"select email from utilisateur where workflow='{user.Workflow}'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    Dim adr_temp As String = ora_dr.Item(0).ToString().ToLower()

                    If (adr_temp.IndexOf(",") > -1) Then

                        Dim liste_temp As New List(Of String)(adr_temp.Split(","))

                        For Each adr In liste_temp

                            If (IsValidEmail(adr)) Then

                                If (adr.IndexOf(contenu.ToLower()) > -1) Then

                                    If (Not resultat.Contains(adr)) Then

                                        resultat.Add(adr)

                                    End If

                                End If

                            End If

                        Next

                    Else

                        If (IsValidEmail(adr_temp)) Then

                            If (adr_temp.IndexOf(contenu.ToLower()) > -1) Then

                                resultat.Add(adr_temp)

                            End If

                        End If

                    End If

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of String)
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of String)
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat.Distinct().ToList()

    End Function

    Public Shared Function InsertEvenement(p_evenement As EvenementCalendrier) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete_evenement As String
        Dim resultat As Boolean
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            resultat = False
            p_evenement.Identifiant = p_evenement.Identifiant.Replace("'", "''").Trim()
            p_evenement.Titre = p_evenement.Titre.Replace("'", "''").Trim()
            p_evenement.Description = p_evenement.Description.Replace("'", "''").Trim()

            Try
                requete_evenement = "insert into evenement(workflow,identifiant,titre,description,date_debut,date_fin) " &
                    "values('" & p_evenement.Workflow & "','" & p_evenement.Identifiant & "','" & p_evenement.Titre & "','" & p_evenement.Description & "',to_date('" & p_evenement.DateDebut & "','DD/MM/YYYY HH24:MI'),to_date('" & p_evenement.DateFin & "','DD/MM/YYYY HH24:MI'))"

                ora_commande = New OracleCommand(requete_evenement, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb > 0) Then

                    resultat = True

                End If

            Catch ex As Exception
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function UpdateEvenement(p_evenement As EvenementCalendrier) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete_evenement As String
        Dim resultat As Boolean
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            resultat = False
            p_evenement.Identifiant = p_evenement.Identifiant.Replace("'", "''").Trim()
            p_evenement.Titre = p_evenement.Titre.Replace("'", "''").Trim()
            p_evenement.Description = p_evenement.Description.Replace("'", "''").Trim()

            Try
                requete_evenement = "update evenement set titre='" & p_evenement.Titre & "',description='" & p_evenement.Description & "',date_debut=to_date('" & p_evenement.DateDebut & "','DD/MM/YYYY HH24:MI'),date_fin=to_date('" & p_evenement.DateFin & "','DD/MM/YYYY HH24:MI') " &
                    "where identifiant='" & p_evenement.Identifiant & "' and id_evenement='" & p_evenement.IDEvenement & "' and workflow='" & p_evenement.Workflow & "'"

                ora_commande = New OracleCommand(requete_evenement, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb > 0) Then

                    resultat = True

                End If

            Catch ex As Exception
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function DeleteEvenement(p_evenement As EvenementCalendrier) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete_evenement As String
        Dim resultat As Boolean
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            resultat = False
            p_evenement.Identifiant = p_evenement.Identifiant.Replace("'", "''").Trim()

            Try
                requete_evenement = "delete from evenement " &
                    "where identifiant='" & p_evenement.Identifiant & "' and id_evenement='" & p_evenement.IDEvenement & "' and workflow='" & p_evenement.Workflow & "'"

                ora_commande = New OracleCommand(requete_evenement, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb > 0) Then

                    resultat = True

                End If

            Catch ex As Exception
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function getUtilisateurs(user As Utilisateur) As List(Of Utilisateur)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of Utilisateur)
        Dim workflow As String
        Dim utilisateur_temp As Utilisateur

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            workflow = user.Workflow.Replace("'", "").Trim()

            Try
                If (workflow = "65") Then
                    requete = "select u.*,c.libelle_civilite " &
                   "from utilisateur u " &
                   "left join civilite c on u.id_civilite=c.id_civilite " &
                   "left join profil p on u.id_profil=p.id_profil " &
                   "where u.workflow='" & workflow & "' "
                Else
                    requete = "select u.*,c.libelle_civilite " &
                   "from utilisateur u " &
                   "left join civilite c on u.id_civilite=c.id_civilite " &
                   "left join profil p on u.id_profil=p.id_profil " &
                   "where u.workflow='" & workflow & "' and u.id_profil>=2 "
                End If


                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    utilisateur_temp = New Utilisateur()
                    utilisateur_temp.Identifiant = ora_dr("IDENTIFIANT").ToString()
                    utilisateur_temp.Workflow = ora_dr("WORKFLOW").ToString()
                    utilisateur_temp.IDProfil = ora_dr("ID_PROFIL").ToString()
                    utilisateur_temp.Email = ora_dr("EMAIL").ToString()
                    utilisateur_temp.Civilite = New Civilite(ora_dr("ID_CIVILITE").ToString(), ora_dr("LIBELLE_CIVILITE").ToString())
                    utilisateur_temp.Nom = ora_dr("NOM").ToString()
                    utilisateur_temp.Prenom = ora_dr("PRENOM").ToString()
                    utilisateur_temp.Fonction = ora_dr("LIBELLE_FONCTION").ToString()

                    resultat.Add(utilisateur_temp)

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of Utilisateur)
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Utilisateur)
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function InsertUtilisateur(p_utilisateur As Utilisateur) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande_utilisateur, ora_commande_enrichissement, ora_commande_correction, ora_commande_filtre, ora_commande_alerte As OracleCommand
        Dim requete_utilisateur, requete_enrichissement, requete_correction, requete_filtre, requete_alerte As String
        Dim resultat As Boolean
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            resultat = False
            p_utilisateur.Identifiant = p_utilisateur.Identifiant.Replace("'", "''").Trim()
            p_utilisateur.Nom = p_utilisateur.Nom.Replace("'", "''").Trim()
            p_utilisateur.Prenom = p_utilisateur.Prenom.Replace("'", "''").Trim()
            p_utilisateur.MDP = p_utilisateur.MDP.Replace("'", "''").Trim()
            p_utilisateur.Email = p_utilisateur.Email.Replace("'", "").Trim()
            p_utilisateur.Fonction = p_utilisateur.Fonction.Replace("'", "''").Trim()

            If (p_utilisateur.Workflow = "65") Then
                p_utilisateur.IDProfil = "4"
            Else
                p_utilisateur.IDProfil = "2"
            End If

            Try
                requete_utilisateur = "insert into utilisateur(identifiant,workflow,mot_de_passe,id_profil,email,id_civilite,nom,prenom,libelle_fonction) " &
                    "values('" & p_utilisateur.Identifiant & "','" & p_utilisateur.Workflow & "','" & p_utilisateur.MDP & "','" & p_utilisateur.IDProfil & "','" & p_utilisateur.Email & "','" & p_utilisateur.Civilite.Id & "','" & p_utilisateur.Nom & "','" & p_utilisateur.Prenom & "','" & p_utilisateur.Fonction & "')"

                ora_commande_utilisateur = New OracleCommand(requete_utilisateur, ora_connexion)
                nb = ora_commande_utilisateur.ExecuteNonQuery()

                If (nb > 0) Then

                    'enrichissement

                    For Each ch As Champ In p_utilisateur.ListeChampsEnrichissement

                        requete_enrichissement = "insert into champ_enrichissement (identifiant,workflow,id_champ) " &
                            "values ('" & p_utilisateur.Identifiant & "','" & p_utilisateur.Workflow & "','" & ch.IDChamp & "')"

                        ora_commande_enrichissement = New OracleCommand(requete_enrichissement, ora_connexion)
                        nb = ora_commande_enrichissement.ExecuteNonQuery()

                    Next

                    'correction

                    For Each ch As Champ In p_utilisateur.ListeChampsCorrection

                        requete_correction = "insert into champ_correction (identifiant,workflow,id_champ) " &
                            "values ('" & p_utilisateur.Identifiant & "','" & p_utilisateur.Workflow & "','" & ch.IDChamp & "')"

                        ora_commande_correction = New OracleCommand(requete_correction, ora_connexion)
                        nb = ora_commande_correction.ExecuteNonQuery()

                    Next

                    'filtres

                    For Each fil As Filtre In p_utilisateur.ListeFiltres

                        requete_filtre = "insert into filtre_corbeille_utilisateur (workflow,id_filtre_corbeille,identifiant) " &
                            "values ('" & p_utilisateur.Workflow & "','" & fil.IDFiltre & "','" & p_utilisateur.Identifiant & "')"

                        ora_commande_filtre = New OracleCommand(requete_filtre, ora_connexion)
                        nb = ora_commande_filtre.ExecuteNonQuery()

                    Next

                    'alertes

                    For Each ale As AlerteAutomatique In p_utilisateur.ListeAlertesAuto

                        requete_alerte = "insert into alerte_automatique_utilisateur (workflow,id_alerte_auto,identifiant) " &
                            "values ('" & p_utilisateur.Workflow & "','" & ale.IDAlerteAuto & "','" & p_utilisateur.Identifiant & "')"

                        ora_commande_alerte = New OracleCommand(requete_alerte, ora_connexion)
                        nb = ora_commande_alerte.ExecuteNonQuery()

                    Next


                    resultat = True

                End If

            Catch ex As Exception
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), p_utilisateur)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), p_utilisateur)
        End Try

        Return resultat

    End Function

    Public Shared Function UpdateUtilisateur(p_utilisateur As Utilisateur) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande_utilisateur, ora_commande_enrichissement, ora_commande_correction, ora_commande_filtre, ora_commande_alerte As OracleCommand
        Dim requete_utilisateur, requete_enrichissement, requete_correction, requete_filtre, requete_alerte As String
        Dim resultat As Boolean
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            resultat = False
            p_utilisateur.Identifiant = p_utilisateur.Identifiant.Replace("'", "''").Trim()
            p_utilisateur.Nom = p_utilisateur.Nom.Replace("'", "''").Trim()
            p_utilisateur.Prenom = p_utilisateur.Prenom.Replace("'", "''").Trim()
            p_utilisateur.MDP = p_utilisateur.MDP.Replace("'", "''").Trim()
            p_utilisateur.Email = p_utilisateur.Email.Replace("'", "").Trim()
            p_utilisateur.Fonction = p_utilisateur.Fonction.Replace("'", "''").Trim()

            Try
                requete_utilisateur = "update utilisateur set mot_de_passe='" & p_utilisateur.MDP & "',email='" & p_utilisateur.Email & "',id_civilite='" & p_utilisateur.Civilite.Id & "',nom='" & p_utilisateur.Nom & "',prenom='" & p_utilisateur.Prenom & "',libelle_fonction='" & p_utilisateur.Fonction & "' " &
                    " where identifiant='" & p_utilisateur.Identifiant & "' and workflow='" & p_utilisateur.Workflow & "'"

                ora_commande_utilisateur = New OracleCommand(requete_utilisateur, ora_connexion)
                nb = ora_commande_utilisateur.ExecuteNonQuery()

                If (nb > 0) Then

                    'enrichissement

                    requete_enrichissement = "delete from champ_enrichissement " &
                        " where identifiant='" & p_utilisateur.Identifiant & "' and workflow='" & p_utilisateur.Workflow & "'"
                    ora_commande_enrichissement = New OracleCommand(requete_enrichissement, ora_connexion)
                    nb = ora_commande_enrichissement.ExecuteNonQuery()

                    For Each ch As Champ In p_utilisateur.ListeChampsEnrichissement

                        requete_enrichissement = "insert into champ_enrichissement (identifiant,workflow,id_champ) " &
                            "values ('" & p_utilisateur.Identifiant & "','" & p_utilisateur.Workflow & "','" & ch.IDChamp & "')"

                        ora_commande_enrichissement = New OracleCommand(requete_enrichissement, ora_connexion)
                        nb = ora_commande_enrichissement.ExecuteNonQuery()

                    Next

                    'correction

                    requete_correction = "delete from champ_correction " &
                        " where identifiant='" & p_utilisateur.Identifiant & "' and workflow='" & p_utilisateur.Workflow & "'"
                    ora_commande_correction = New OracleCommand(requete_correction, ora_connexion)
                    nb = ora_commande_correction.ExecuteNonQuery()

                    For Each ch As Champ In p_utilisateur.ListeChampsCorrection

                        requete_correction = "insert into champ_correction (identifiant,workflow,id_champ) " &
                            "values ('" & p_utilisateur.Identifiant & "','" & p_utilisateur.Workflow & "','" & ch.IDChamp & "')"

                        ora_commande_correction = New OracleCommand(requete_correction, ora_connexion)
                        nb = ora_commande_correction.ExecuteNonQuery()

                    Next

                    'filtres

                    requete_filtre = "delete from filtre_corbeille_utilisateur " &
                        " where identifiant='" & p_utilisateur.Identifiant & "' and workflow='" & p_utilisateur.Workflow & "'"
                    ora_commande_filtre = New OracleCommand(requete_filtre, ora_connexion)
                    nb = ora_commande_filtre.ExecuteNonQuery()

                    For Each fil As Filtre In p_utilisateur.ListeFiltres

                        requete_filtre = "insert into filtre_corbeille_utilisateur (workflow,id_filtre_corbeille,identifiant) " &
                            "values ('" & p_utilisateur.Workflow & "','" & fil.IDFiltre & "','" & p_utilisateur.Identifiant & "')"

                        ora_commande_filtre = New OracleCommand(requete_filtre, ora_connexion)
                        nb = ora_commande_filtre.ExecuteNonQuery()

                    Next

                    'alertes

                    requete_alerte = "delete from alerte_automatique_utilisateur " &
                        " where identifiant='" & p_utilisateur.Identifiant & "' and workflow='" & p_utilisateur.Workflow & "'"
                    ora_commande_alerte = New OracleCommand(requete_alerte, ora_connexion)
                    nb = ora_commande_alerte.ExecuteNonQuery()

                    For Each ale As AlerteAutomatique In p_utilisateur.ListeAlertesAuto

                        requete_alerte = "insert into alerte_automatique_utilisateur (workflow,id_alerte_auto,identifiant) " &
                            "values ('" & p_utilisateur.Workflow & "','" & ale.IDAlerteAuto & "','" & p_utilisateur.Identifiant & "')"

                        ora_commande_alerte = New OracleCommand(requete_alerte, ora_connexion)
                        nb = ora_commande_alerte.ExecuteNonQuery()

                    Next

                    resultat = True

                End If

            Catch ex As Exception
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), p_utilisateur)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), p_utilisateur)
        End Try

        Return resultat

    End Function

    Public Shared Function getCivilites() As List(Of ListItem)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of ListItem)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try
                requete = "select * " &
                    "from civilite order by libelle_civilite"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    resultat.Add(New ListItem(ora_dr.Item("LIBELLE_CIVILITE").ToString(), ora_dr.Item("ID_CIVILITE").ToString()))

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of ListItem)
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of ListItem)
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function getInfosUtilisateur(identifiant As String, workflow As String) As Utilisateur

        Dim ora_connexion As OracleConnection
        Dim ora_commande_utilisateur As OracleCommand
        Dim ora_dr_utilisateur As OracleDataReader
        Dim requete_utilisateur As String
        Dim resultat As Utilisateur

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            resultat = Nothing
            identifiant = identifiant.Replace("'", "''").Trim()
            workflow = workflow.Replace("'", "").Trim()

            Try
                requete_utilisateur = "select u.*,c.libelle_civilite " &
                    "from utilisateur u " &
                    "left join civilite c on u.id_civilite=u.id_civilite " &
                    "where u.workflow='" & workflow & "' and u.identifiant='" & identifiant & "'"

                ora_commande_utilisateur = New OracleCommand(requete_utilisateur, ora_connexion)
                ora_dr_utilisateur = ora_commande_utilisateur.ExecuteReader()

                If (ora_dr_utilisateur.Read()) Then

                    resultat = New Utilisateur()
                    resultat.Identifiant = ora_dr_utilisateur("IDENTIFIANT").ToString()
                    resultat.Workflow = ora_dr_utilisateur("WORKFLOW").ToString()
                    resultat.IDProfil = ora_dr_utilisateur("ID_PROFIL").ToString()
                    resultat.Email = ora_dr_utilisateur("EMAIL").ToString()
                    resultat.Civilite = New Civilite(ora_dr_utilisateur("ID_CIVILITE").ToString(), ora_dr_utilisateur("LIBELLE_CIVILITE").ToString())
                    resultat.Nom = ora_dr_utilisateur("NOM").ToString()
                    resultat.Prenom = ora_dr_utilisateur("PRENOM").ToString()
                    resultat.Fonction = ora_dr_utilisateur("LIBELLE_FONCTION").ToString()
                    resultat.MDP = ora_dr_utilisateur("MOT_DE_PASSE").ToString()

                End If

                ora_dr_utilisateur.Close()

            Catch ex As Exception
                resultat = Nothing
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = Nothing
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function getChamps(user As Utilisateur, action As String, detail_ligne As String) As List(Of Champ)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of Champ)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                action = action.Replace("'", "''").Trim()
                detail_ligne = detail_ligne.Replace("'", "").Trim()

                requete = "select * " &
                    "from champ_workflow where workflow='" & user.Workflow & "' "

                If (action <> "") Then

                    requete += " and action='" & action & "' "

                End If

                If (detail_ligne <> "") Then

                    requete += "and detail_ligne='" & detail_ligne & "'"

                End If

                requete += " order by ordre,description "

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    resultat.Add(New Champ(ora_dr.Item("WORKFLOW").ToString(), ora_dr.Item("ID_CHAMP").ToString(), ora_dr.Item("NOM_CHAMP").ToString(), ora_dr.Item("DESCRIPTION").ToString(), ora_dr.Item("TYPAGE").ToString(), "", ora_dr.Item("ACTION").ToString(), "", ora_dr.Item("REQUETE_LISTE").ToString(), New List(Of ListItem), ora_dr.Item("EXPRESSION_REGULIERE").ToString(), ora_dr.Item("DETAIL_LIGNE").ToString(), ora_dr.Item("ORDRE").ToString()))

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of Champ)
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Champ)
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function getChampsAEnrichir(user_parametre As Utilisateur) As List(Of Champ)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of Champ)
        Dim identifiant As String

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                identifiant = user_parametre.Identifiant.Replace("'", "''")

                requete = "select ce.*,cw.nom_champ,cw.description,cw.typage,cw.action,cw.requete_liste,cw.expression_reguliere,cw.detail_ligne,cw.ordre " &
                    "from champ_enrichissement ce inner join champ_workflow cw on (cw.workflow=ce.workflow and cw.id_champ=ce.id_champ) " &
                    "where ce.identifiant='" & identifiant & "' and ce.workflow='" & user_parametre.Workflow & "' " &
                    "order by cw.description"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    resultat.Add(New Champ(ora_dr.Item("WORKFLOW").ToString(), ora_dr.Item("ID_CHAMP").ToString(), ora_dr.Item("NOM_CHAMP").ToString(), ora_dr.Item("DESCRIPTION").ToString(), ora_dr.Item("TYPAGE").ToString(), "", ora_dr.Item("ACTION").ToString(), "", ora_dr.Item("REQUETE_LISTE").ToString(), New List(Of ListItem), ora_dr.Item("EXPRESSION_REGULIERE").ToString(), ora_dr.Item("DETAIL_LIGNE").ToString(), ora_dr.Item("ORDRE").ToString()))

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of Champ)
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), user_parametre)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Champ)
            LogsFunctions.LogWrite(ex.ToString(), user_parametre)
        End Try

        Return resultat

    End Function

    Public Shared Function getChampsACorriger(user_parametre As Utilisateur) As List(Of Champ)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of Champ)
        Dim identifiant As String

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try
                identifiant = user_parametre.Identifiant.Replace("'", "''")

                requete = "select cc.*,cw.nom_champ,cw.description,cw.typage,cw.action,cw.requete_liste,cw.expression_reguliere,cw.detail_ligne,cw.ordre " &
                   "from champ_correction cc inner join champ_workflow cw on (cw.workflow=cc.workflow and cw.id_champ=cc.id_champ) " &
                   "where cc.identifiant='" & identifiant & "' and cc.workflow='" & user_parametre.Workflow & "' " &
                   "order by cw.description"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    resultat.Add(New Champ(ora_dr.Item("WORKFLOW").ToString(), ora_dr.Item("ID_CHAMP").ToString(), ora_dr.Item("NOM_CHAMP").ToString(), ora_dr.Item("DESCRIPTION").ToString(), ora_dr.Item("TYPAGE").ToString(), "", ora_dr.Item("ACTION").ToString(), "", ora_dr.Item("REQUETE_LISTE").ToString(), New List(Of ListItem), ora_dr.Item("EXPRESSION_REGULIERE").ToString(), ora_dr.Item("DETAIL_LIGNE").ToString(), ora_dr.Item("ORDRE").ToString()))

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of Champ)
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), user_parametre)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Champ)
            LogsFunctions.LogWrite(ex.ToString(), user_parametre)
        End Try

        Return resultat

    End Function

    Public Shared Function getSocieteIdm(ByVal pUser As Utilisateur) As List(Of ListItem)
        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader

        Dim requete As String
        Dim resultat As New List(Of ListItem)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try
                requete = $"select * from BPROCESS.SOCIETE_IDM"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    resultat.Add(New ListItem(ora_dr.Item("NOM_SOCIETE").ToString(), ora_dr.Item("CODE_ENTITE").ToString()))

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of ListItem)
                ora_connexion.Close()
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of ListItem)
        End Try


        Return resultat
    End Function

    Public Shared Function getSiteDocument(ByVal pUser As Utilisateur, docid As String) As String
        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader

        Dim requete As String
        Dim resultat As String = ""

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try
                requete = $"select * from tb_document_hors_scope where id_client='{pUser.Workflow}' and ID_DOCUMENT='" + docid + "'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    resultat = ora_dr.Item("SITE").ToString()

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = ""
                ora_connexion.Close()
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = ""
        End Try


        Return resultat
    End Function

    Public Shared Function getStatutDocument2(ByVal pUser As Utilisateur, docid As String) As String
        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader

        Dim requete As String
        Dim resultat As String = ""

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try
                requete = $"select * from tb_document_hors_scope where id_client='{pUser.Workflow}' and ID_DOCUMENT='" + docid + "'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    resultat = ora_dr.Item("STATUT2").ToString()

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = ""
                ora_connexion.Close()
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = ""
        End Try


        Return resultat
    End Function

    Public Shared Function getTypeDocument(ByVal pUser As Utilisateur) As List(Of ListItem)
        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader

        Dim requete As String
        Dim resultat As New List(Of ListItem)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try
                requete = $"select * from tb_TYPE_DOCUMENT where id_client='{pUser.Workflow}'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    resultat.Add(New ListItem(ora_dr.Item("NOM_TYPE").ToString(), ora_dr.Item("ID_TYPE").ToString()))

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of ListItem)
                ora_connexion.Close()
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of ListItem)
        End Try


        Return resultat
    End Function

    Public Shared Function getDocumentHorsScope(ByVal pUser As Utilisateur, ByVal pId As String) As DocumentHorsScope
        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader

        Dim requete As String
        Dim resultat As New DocumentHorsScope

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = $"select * from vw_document_hors_scope where id_client='{pUser.Workflow}' and id_document='{pId}'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    resultat = New DocumentHorsScope() With {
                        .Id = ora_dr.Item("ID_DOCUMENT").ToString(),
                        .IdLot = ora_dr.Item("ID_LOT").ToString(),
                        .Type = ora_dr.Item("NOM_TYPE").ToString(),
                        .DateInsertion = IIf(Not String.IsNullOrEmpty(ora_dr.Item("DATE_INSERTION").ToString()), ora_dr.Item("DATE_INSERTION").ToString().Substring(0, 10), ""),
                        .Statut = New Statut(ora_dr.Item("id_statut").ToString(), ora_dr.Item("libelle_statut_facture").ToString()),
                        .Entite = ora_dr.Item("CODE_SOCIETE").ToString()
                    }

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New DocumentHorsScope()
                ora_connexion.Close()
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New DocumentHorsScope()
        End Try


        Return resultat
    End Function


    Public Shared Function getListDocumentHorsScope(ByVal pUser As Utilisateur, ByVal pType As String, Optional ByVal pStatut As List(Of String) = Nothing) As List(Of DocumentHorsScope)
        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim statut2 As String

        Dim requete As String
        Dim resultat As New List(Of DocumentHorsScope)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try
                Dim vOption As New List(Of String)
                If Not String.IsNullOrEmpty(pType) Then vOption.Add($" and id_type='{pType}' ")
                If Not pStatut Is Nothing Then
                    If pStatut.Count > 0 Then
                        vOption.Add($" and id_statut in ('{String.Join("','", pStatut)}') ")
                    End If
                End If

                requete = $"select * from vw_document_hors_scope where id_client='{pUser.Workflow}' {String.Join(" ", vOption)}"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    If ora_dr.Item("Statut2").ToString() = "29" Or ora_dr.Item("Statut2").ToString() = "" Then
                        statut2 = "à traiter"
                    Else
                        statut2 = "Traité"
                    End If

                    resultat.Add(New DocumentHorsScope() With {
                        .Id = ora_dr.Item("ID_DOCUMENT").ToString(),
                        .IdLot = ora_dr.Item("ID_LOT").ToString(),
                        .Type = ora_dr.Item("NOM_TYPE").ToString(),
                        .DateInsertion = IIf(Not String.IsNullOrEmpty(ora_dr.Item("DATE_INSERTION").ToString()), ora_dr.Item("DATE_INSERTION").ToString().Substring(0, 10), ""),
                        .Statut = New Statut(ora_dr.Item("id_statut").ToString(), ora_dr.Item("libelle_statut_facture").ToString()),
                        .Entite = ora_dr.Item("CODE_SOCIETE").ToString(),
                        .Site = ora_dr.Item("NOM_SOCIETE").ToString(),
                        .Site2 = ora_dr.Item("SITE").ToString(),
                        .Statut2 = statut2
                    })

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of DocumentHorsScope)
                ora_connexion.Close()
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of DocumentHorsScope)
        End Try


        Return resultat
    End Function


    Public Shared Function getFiltres(user_parametre As Utilisateur, utilisateur_uniquement As Boolean) As List(Of Filtre)

        Dim ora_connexion As OracleConnection
        Dim ora_commande_filtre, ora_commande_attribut As OracleCommand
        Dim ora_dr_filtre, ora_dr_attribut As OracleDataReader
        Dim requete_filtre, requete_attribut As String
        Dim resultat As New List(Of Filtre)
        Dim filtre_temp As Filtre
        Dim attribut_temp As Attribut

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                user_parametre.Identifiant = user_parametre.Identifiant.Replace("'", "''")

                If (utilisateur_uniquement) Then

                    requete_filtre = "select fc.* " &
                    "from filtre_corbeille fc inner join filtre_corbeille_utilisateur fcu on (fc.workflow=fcu.workflow and fc.id_filtre_corbeille=fcu.id_filtre_corbeille) " &
                    "where fc.workflow='" & user_parametre.Workflow & "' and fcu.identifiant='" & user_parametre.Identifiant & "' " &
                    "order by fc.libelle_filtre_corbeille"

                Else

                    requete_filtre = "select * " &
                    "from filtre_corbeille " &
                    "where workflow='" & user_parametre.Workflow & "' " &
                    "order by libelle_filtre_corbeille"

                End If

                ora_commande_filtre = New OracleCommand(requete_filtre, ora_connexion)
                ora_dr_filtre = ora_commande_filtre.ExecuteReader()

                While (ora_dr_filtre.Read())

                    filtre_temp = New Filtre()
                    filtre_temp.Workflow = ora_dr_filtre.Item("WORKFLOW").ToString()
                    filtre_temp.IDFiltre = ora_dr_filtre.Item("ID_FILTRE_CORBEILLE").ToString()
                    filtre_temp.Libelle = ora_dr_filtre.Item("LIBELLE_FILTRE_CORBEILLE").ToString()

                    requete_attribut = "select cfc.*,cw.nom_champ,cw.description,cw.typage,cw.action,cw.requete_liste,cw.expression_reguliere,cw.detail_ligne,cw.ordre " &
                    "from champ_filtre_corbeille cfc " &
                    "inner join champ_workflow cw on (cfc.id_champ=cw.id_champ and cw.workflow=cfc.workflow) " &
                    "where cfc.workflow='" & user_parametre.Workflow & "' and cfc.id_filtre_corbeille='" & filtre_temp.IDFiltre & "' " &
                    "order by cfc.id_filtre_corbeille "

                    ora_commande_attribut = New OracleCommand(requete_attribut, ora_connexion)
                    ora_dr_attribut = ora_commande_attribut.ExecuteReader()

                    While (ora_dr_attribut.Read())

                        attribut_temp = New Attribut()
                        attribut_temp.Operateur = ora_dr_attribut.Item("OPERATEUR").ToString()
                        attribut_temp.Valeur = ora_dr_attribut.Item("VALEUR").ToString()
                        attribut_temp.Champ = New Champ(filtre_temp.Workflow, ora_dr_attribut.Item("ID_CHAMP").ToString(), ora_dr_attribut.Item("NOM_CHAMP").ToString(), ora_dr_attribut.Item("DESCRIPTION").ToString(), ora_dr_attribut.Item("TYPAGE").ToString(), "", ora_dr_attribut.Item("ACTION").ToString(), "", ora_dr_attribut.Item("REQUETE_LISTE").ToString(), New List(Of ListItem), ora_dr_attribut.Item("EXPRESSION_REGULIERE").ToString(), ora_dr_attribut.Item("DETAIL_LIGNE").ToString(), ora_dr_attribut.Item("ORDRE").ToString())

                        filtre_temp.ListeAttributs.Add(attribut_temp)

                    End While

                    ora_dr_attribut.Close()

                    resultat.Add(filtre_temp)

                End While

                ora_dr_filtre.Close()

            Catch ex As Exception
                resultat = New List(Of Filtre)
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), user_parametre)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Filtre)
            LogsFunctions.LogWrite(ex.ToString(), user_parametre)
        End Try

        Return resultat

    End Function

    Public Shared Function InsertFiltre(p_filtre As Filtre) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande_filtre, ora_commande_attribut As OracleCommand
        Dim ora_parametre As OracleParameter
        Dim requete_filtre, requete_attribut As String
        Dim ora_trans As OracleTransaction
        Dim resultat As Boolean
        Dim nb As Integer
        Dim id_filtre As String = ""

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            ora_trans = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

            resultat = False
            p_filtre.Libelle = p_filtre.Libelle.Replace("'", "''").Trim()

            Try


                requete_filtre = "insert into filtre_corbeille(workflow,libelle_filtre_corbeille,date_creation) " &
                    "values('" & p_filtre.Workflow & "','" & p_filtre.Libelle & "',sysdate) " &
                    "returning id_filtre_corbeille INTO :id_filtre"

                ora_parametre = New OracleParameter("id_filtre", OracleDbType.Int32)
                ora_parametre.Direction = ParameterDirection.Output
                ora_commande_filtre = New OracleCommand(requete_filtre, ora_connexion)
                ora_commande_filtre.Parameters.Add(ora_parametre)

                nb = ora_commande_filtre.ExecuteNonQuery()

                If (nb > 0) Then

                    id_filtre = ora_commande_filtre.Parameters("id_filtre").Value.ToString()

                    For Each att As Attribut In p_filtre.ListeAttributs

                        att.Valeur = att.Valeur.Replace("'", "''")

                        requete_attribut = "insert into champ_filtre_corbeille(id_filtre_corbeille,workflow,id_champ,valeur,operateur) " &
                            "values('" & id_filtre & "','" & p_filtre.Workflow & "','" & att.Champ.IDChamp & "','" & att.Valeur & "','" & att.Operateur & "')"

                        ora_commande_attribut = New OracleCommand(requete_attribut, ora_connexion)
                        nb = ora_commande_attribut.ExecuteNonQuery()

                    Next

                    ora_trans.Commit()

                    resultat = True

                End If

            Catch ex As Exception
                ora_trans.Rollback()
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function DeleteFiltre(p_filtre As Filtre) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande_filtre, ora_commande_attribut As OracleCommand
        Dim ora_trans As OracleTransaction
        Dim requete_filtre, requete_attribut, requete_filtre_utilisateur As String
        Dim resultat As Boolean
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            ora_trans = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

            Try

                resultat = False

                requete_filtre = "delete from filtre_corbeille " &
                    "where id_filtre_corbeille='" & p_filtre.IDFiltre & "' and workflow='" & p_filtre.Workflow & "'"

                ora_commande_filtre = New OracleCommand(requete_filtre, ora_connexion)
                nb = ora_commande_filtre.ExecuteNonQuery()

                If (nb >= 0) Then

                    requete_attribut = "delete from champ_filtre_corbeille " &
                    "where id_filtre_corbeille='" & p_filtre.IDFiltre & "' and workflow='" & p_filtre.Workflow & "'"

                    ora_commande_attribut = New OracleCommand(requete_attribut, ora_connexion)
                    nb = ora_commande_attribut.ExecuteNonQuery()

                    If (nb >= 0) Then

                        requete_filtre_utilisateur = "delete from filtre_corbeille_utilisateur " &
                            "where id_filtre_corbeille='" & p_filtre.IDFiltre & "' and workflow='" & p_filtre.Workflow & "'"
                        ora_commande_filtre = New OracleCommand(requete_filtre_utilisateur, ora_connexion)
                        nb = ora_commande_filtre.ExecuteNonQuery()

                        If (nb >= 0) Then
                            ora_trans.Commit()
                        Else
                            ora_trans.Rollback()
                        End If

                    Else
                        ora_trans.Rollback()
                    End If

                Else
                    ora_trans.Rollback()
                End If

            Catch ex As Exception
                ora_trans.Rollback()
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function UpdateFiltre(p_filtre As Filtre) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande_filtre, ora_commande_attribut As OracleCommand
        Dim requete_filtre, requete_attribut As String
        Dim ora_trans As OracleTransaction
        Dim resultat As Boolean
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            ora_trans = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

            resultat = False
            p_filtre.Libelle = p_filtre.Libelle.Replace("'", "''").Trim()

            Try

                requete_filtre = "update filtre_corbeille set libelle_filtre_corbeille='" & p_filtre.Libelle & "', date_creation=sysdate " &
                    "where id_filtre_corbeille='" & p_filtre.IDFiltre & "' and workflow='" & p_filtre.Workflow & "'"

                ora_commande_filtre = New OracleCommand(requete_filtre, ora_connexion)
                nb = ora_commande_filtre.ExecuteNonQuery()

                If (nb >= 0) Then

                    requete_attribut = "delete from champ_filtre_corbeille " &
                    "where id_filtre_corbeille='" & p_filtre.IDFiltre & "' and workflow='" & p_filtre.Workflow & "'"

                    ora_commande_attribut = New OracleCommand(requete_attribut, ora_connexion)
                    nb = ora_commande_attribut.ExecuteNonQuery()

                    If (nb >= 0) Then

                        For Each att As Attribut In p_filtre.ListeAttributs

                            att.Valeur = att.Valeur.Replace("'", "''")

                            requete_attribut = "insert into champ_filtre_corbeille(id_filtre_corbeille,workflow,id_champ,valeur,operateur) " &
                                "values('" & p_filtre.IDFiltre & "','" & p_filtre.Workflow & "','" & att.Champ.IDChamp & "','" & att.Valeur & "','" & att.Operateur & "')"

                            ora_commande_attribut = New OracleCommand(requete_attribut, ora_connexion)
                            nb = ora_commande_attribut.ExecuteNonQuery()

                        Next

                        ora_trans.Commit()

                    End If

                    resultat = True

                End If

            Catch ex As Exception
                ora_trans.Rollback()
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function getAlertesAutomatiques(user_parametre As Utilisateur, utilisateur_uniquement As Boolean) As List(Of AlerteAutomatique)

        Dim ora_connexion As OracleConnection
        Dim ora_commande_alerte, ora_commande_attribut As OracleCommand
        Dim ora_dr_alerte, ora_dr_attribut As OracleDataReader
        Dim requete_alerte, requete_attribut As String
        Dim resultat As New List(Of AlerteAutomatique)
        Dim alerte_temp As AlerteAutomatique
        Dim attribut_temp As Attribut

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                user_parametre.Identifiant = user_parametre.Identifiant.Replace("'", "''")

                If (utilisateur_uniquement) Then

                    requete_alerte = "select aa.* " &
                    "from alerte_automatique aa inner join alerte_automatique_utilisateur aau on (aa.workflow=aau.workflow and aa.id_alerte_auto=aau.id_alerte_auto) " &
                    "where aa.workflow='" & user_parametre.Workflow & "' and aau.identifiant='" & user_parametre.Identifiant & "' " &
                    "order by aa.libelle_alerte_auto"

                Else

                    requete_alerte = "select * " &
                    "from alerte_automatique " &
                    "where workflow='" & user_parametre.Workflow & "' " &
                    "order by libelle_alerte_auto"

                End If

                ora_commande_alerte = New OracleCommand(requete_alerte, ora_connexion)
                ora_dr_alerte = ora_commande_alerte.ExecuteReader()

                While (ora_dr_alerte.Read())

                    alerte_temp = New AlerteAutomatique()
                    alerte_temp.Workflow = ora_dr_alerte.Item("WORKFLOW").ToString()
                    alerte_temp.IDAlerteAuto = ora_dr_alerte.Item("ID_ALERTE_AUTO").ToString()
                    alerte_temp.Libelle = ora_dr_alerte.Item("LIBELLE_ALERTE_AUTO").ToString()
                    alerte_temp.Debut = Convert.ToDateTime(ora_dr_alerte.Item("DATE_DEBUT").ToString()).ToString("dd/MM/yyyy HH:mm")
                    alerte_temp.Duree = ora_dr_alerte.Item("DUREE").ToString()
                    alerte_temp.Periodicite = ora_dr_alerte.Item("PERIODICITE").ToString()
                    alerte_temp.Active = ora_dr_alerte.Item("ACTIVE").ToString()

                    requete_attribut = "select cau.*,cw.nom_champ,cw.description,cw.typage,cw.action,cw.requete_liste,cw.expression_reguliere,cw.detail_ligne,cw.ordre " &
                    "from champ_alerte_automatique cau " &
                    "inner join champ_workflow cw on (cau.id_champ=cw.id_champ and cw.workflow=cau.workflow) " &
                    "where cau.workflow='" & user_parametre.Workflow & "' and cau.id_alerte_auto='" & alerte_temp.IDAlerteAuto & "' " &
                    "order by cau.id_alerte_auto "

                    ora_commande_attribut = New OracleCommand(requete_attribut, ora_connexion)
                    ora_dr_attribut = ora_commande_attribut.ExecuteReader()

                    While (ora_dr_attribut.Read())

                        attribut_temp = New Attribut()
                        attribut_temp.Operateur = ora_dr_attribut.Item("OPERATEUR").ToString()
                        attribut_temp.Valeur = ora_dr_attribut.Item("VALEUR").ToString()
                        attribut_temp.Champ = New Champ(alerte_temp.Workflow, ora_dr_attribut.Item("ID_CHAMP").ToString(), ora_dr_attribut.Item("NOM_CHAMP").ToString(), ora_dr_attribut.Item("DESCRIPTION").ToString(), ora_dr_attribut.Item("TYPAGE").ToString(), "", ora_dr_attribut.Item("ACTION").ToString(), "", ora_dr_attribut.Item("REQUETE_LISTE").ToString(), New List(Of ListItem), ora_dr_attribut.Item("EXPRESSION_REGULIERE").ToString(), ora_dr_attribut.Item("DETAIL_LIGNE").ToString(), ora_dr_attribut.Item("ORDRE").ToString())

                        alerte_temp.ListeAttributs.Add(attribut_temp)

                    End While

                    ora_dr_attribut.Close()

                    resultat.Add(alerte_temp)

                End While

                ora_dr_alerte.Close()

            Catch ex As Exception
                resultat = New List(Of AlerteAutomatique)
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), user_parametre)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of AlerteAutomatique)
            LogsFunctions.LogWrite(ex.ToString(), user_parametre)
        End Try

        Return resultat

    End Function

    Public Shared Function InsertAlerteAutomatique(p_alerte_auto As AlerteAutomatique) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande_alerte, ora_commande_attribut As OracleCommand
        Dim ora_parametre As OracleParameter
        Dim requete_alerte, requete_attribut As String
        Dim ora_trans As OracleTransaction
        Dim resultat As Boolean
        Dim nb As Integer
        Dim id_alerte As String = ""

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            ora_trans = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

            resultat = False
            p_alerte_auto.Libelle = p_alerte_auto.Libelle.Replace("'", "''").Trim()

            Try

                requete_alerte = "insert into alerte_automatique(workflow,libelle_alerte_auto,duree,periodicite,date_debut,active,date_creation) " &
                    "values('" & p_alerte_auto.Workflow & "','" & p_alerte_auto.Libelle & "','" & p_alerte_auto.Duree & "','" & p_alerte_auto.Periodicite & "', to_date('" & p_alerte_auto.Debut & "','YYYY-MM-DD') ,'" & p_alerte_auto.Active & "',sysdate) " &
                    "returning id_alerte_auto INTO :id_alerte_auto"

                ora_parametre = New OracleParameter("id_alerte_auto", OracleDbType.Int32)
                ora_parametre.Direction = ParameterDirection.Output
                ora_commande_alerte = New OracleCommand(requete_alerte, ora_connexion)
                ora_commande_alerte.Parameters.Add(ora_parametre)

                nb = ora_commande_alerte.ExecuteNonQuery()

                If (nb > 0) Then

                    id_alerte = ora_commande_alerte.Parameters("id_alerte_auto").Value.ToString()

                    For Each att As Attribut In p_alerte_auto.ListeAttributs

                        att.Valeur = att.Valeur.Replace("'", "''")

                        requete_attribut = "insert into champ_alerte_automatique(workflow,id_alerte_auto,id_champ,operateur,valeur) " &
                            "values('" & p_alerte_auto.Workflow & "','" & id_alerte & "','" & att.Champ.IDChamp & "','" & att.Operateur & "','" & att.Valeur & "')"

                        ora_commande_attribut = New OracleCommand(requete_attribut, ora_connexion)
                        nb = ora_commande_attribut.ExecuteNonQuery()

                    Next

                    ora_trans.Commit()

                    resultat = True

                End If

            Catch ex As Exception
                ora_trans.Rollback()
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function UpdateAlerteAutomatique(p_alerte_auto As AlerteAutomatique) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande_alerte, ora_commande_attribut As OracleCommand
        Dim requete_alerte, requete_attribut As String
        Dim ora_trans As OracleTransaction
        Dim resultat As Boolean
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            ora_trans = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

            resultat = False
            p_alerte_auto.Libelle = p_alerte_auto.Libelle.Replace("'", "''").Trim()

            Try

                requete_alerte = "update alerte_automatique set " &
                    "libelle_alerte_auto='" & p_alerte_auto.Libelle & "', " &
                    "duree='" & p_alerte_auto.Duree & "', " &
                    "periodicite='" & p_alerte_auto.Periodicite & "', " &
                    "date_debut=to_date('" & p_alerte_auto.Debut & "','DD/MM/YYYY HH24:MI'), " &
                    "active='" & p_alerte_auto.Active & "', " &
                    "date_creation=sysdate " &
                    "where id_alerte_auto='" & p_alerte_auto.IDAlerteAuto & "' and workflow='" & p_alerte_auto.Workflow & "' "

                ora_commande_alerte = New OracleCommand(requete_alerte, ora_connexion)
                nb = ora_commande_alerte.ExecuteNonQuery()

                If (nb >= 0) Then

                    requete_attribut = "delete from champ_alerte_automatique " &
                    "where id_alerte_auto='" & p_alerte_auto.IDAlerteAuto & "' and workflow='" & p_alerte_auto.Workflow & "'"

                    ora_commande_attribut = New OracleCommand(requete_attribut, ora_connexion)
                    nb = ora_commande_attribut.ExecuteNonQuery()

                    If (nb >= 0) Then

                        For Each att As Attribut In p_alerte_auto.ListeAttributs

                            att.Valeur = att.Valeur.Replace("'", "''")

                            requete_attribut = "insert into champ_alerte_automatique(workflow,id_alerte_auto,id_champ,operateur,valeur) " &
                                "values('" & p_alerte_auto.Workflow & "','" & p_alerte_auto.IDAlerteAuto & "','" & att.Champ.IDChamp & "','" & att.Operateur & "','" & att.Valeur & "')"

                            ora_commande_attribut = New OracleCommand(requete_attribut, ora_connexion)
                            nb = ora_commande_attribut.ExecuteNonQuery()

                        Next

                        ora_trans.Commit()

                    End If

                    resultat = True

                End If

            Catch ex As Exception
                ora_trans.Rollback()
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function DeleteAlerteAutomatique(p_alerte_auto As AlerteAutomatique) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande_alerte, ora_commande_attribut As OracleCommand
        Dim ora_trans As OracleTransaction
        Dim requete_alerte, requete_attribut, requete_alerte_utilisateur As String
        Dim resultat As Boolean
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            ora_trans = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

            Try

                resultat = False

                requete_alerte = "delete from alerte_automatique " &
                    "where id_alerte_auto='" & p_alerte_auto.IDAlerteAuto & "' and workflow='" & p_alerte_auto.Workflow & "'"

                ora_commande_alerte = New OracleCommand(requete_alerte, ora_connexion)
                nb = ora_commande_alerte.ExecuteNonQuery()

                If (nb >= 0) Then

                    requete_attribut = "delete from alerte_automatique " &
                    "where id_alerte_auto='" & p_alerte_auto.IDAlerteAuto & "' and workflow='" & p_alerte_auto.Workflow & "'"

                    ora_commande_attribut = New OracleCommand(requete_attribut, ora_connexion)
                    nb = ora_commande_attribut.ExecuteNonQuery()

                    If (nb >= 0) Then

                        requete_alerte_utilisateur = "delete from alerte_automatique_utilisateur " &
                            "where id_alerte_auto='" & p_alerte_auto.IDAlerteAuto & "' and workflow='" & p_alerte_auto.Workflow & "'"
                        ora_commande_alerte = New OracleCommand(requete_alerte_utilisateur, ora_connexion)
                        nb = ora_commande_alerte.ExecuteNonQuery()

                        If (nb >= 0) Then
                            ora_trans.Commit()
                        Else
                            ora_trans.Rollback()
                        End If

                    Else
                        ora_trans.Rollback()
                    End If

                Else
                    ora_trans.Rollback()
                End If

            Catch ex As Exception
                ora_trans.Rollback()
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function getAlertesFacture(docid As String, user_parametre As Utilisateur, utilisateur_uniquement As Boolean, id_statut As String, Optional ByVal hs As Boolean = False) As List(Of AlerteFacture)

        Dim ora_connexion As OracleConnection
        Dim ora_commande_alerte As OracleCommand
        Dim ora_dr_alerte As OracleDataReader
        Dim requete_alerte As String
        Dim resultat As New List(Of AlerteFacture)
        Dim alerte_temp As AlerteFacture
        Dim liste_destinataires As List(Of String)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                user_parametre.Identifiant = user_parametre.Identifiant.Replace("'", "''")
                docid = docid.Replace("'", "")

                requete_alerte = "select af.*,u1.nom as nom_emetteur,u1.prenom as prenom_emetteur,u1.email as email_emetteur,u2.nom as nom_resolution,u2.prenom as prenom_resolution,u2.email as email_resolution,ma.libelle_motif_alerte,sa.libelle_statut_alerte " &
                $"from {IIf(hs, "alerte_hors_scope", "alerte_facture")} af " &
                "left join utilisateur u1 on (af.workflow=u1.workflow and af.identifiant=u1.identifiant) " &
                "left join utilisateur u2 on (af.workflow=u2.workflow and af.identifiant_resolution=u2.identifiant) " &
                "left join statut_alerte sa on (sa.id_statut_alerte=af.id_statut_alerte) " &
                "left join motif_alertes ma on (ma.id_motif_alerte=af.id_motif_alerte) " &
                "where 1=1 "

                If (utilisateur_uniquement) Then
                    requete_alerte += " and af.workflow='" & user_parametre.Workflow & "' and (af.identifiant='" & user_parametre.Identifiant & "' or identifiant_resolution='" & user_parametre.Identifiant & "' or destinataires like '%" & user_parametre.Email & "%' ) "
                End If

                If (docid <> "") Then
                    requete_alerte += " and af.docid='" & docid & "' "
                End If

                If (id_statut <> "") Then
                    requete_alerte += " and af.id_statut_alerte='" & id_statut & "' "
                End If

                requete_alerte += " order by af.date_alerte "

                ora_commande_alerte = New OracleCommand(requete_alerte, ora_connexion)
                ora_dr_alerte = ora_commande_alerte.ExecuteReader()

                While (ora_dr_alerte.Read())

                    alerte_temp = New AlerteFacture()

                    alerte_temp.Workflow = ora_dr_alerte.Item("WORKFLOW").ToString()
                    alerte_temp.IDAlerte = ora_dr_alerte.Item("ID_ALERTE").ToString()
                    alerte_temp.Motif = New Motif(ora_dr_alerte.Item("ID_MOTIF_ALERTE").ToString(), ora_dr_alerte.Item("LIBELLE_MOTIF_ALERTE").ToString(), "")
                    alerte_temp.Statut = New Statut(ora_dr_alerte.Item("ID_STATUT_ALERTE").ToString(), ora_dr_alerte.Item("LIBELLE_STATUT_ALERTE").ToString())
                    alerte_temp.DateAlerte = ora_dr_alerte.Item("DATE_ALERTE").ToString()
                    alerte_temp.Emetteur = New Contact(ora_dr_alerte.Item("IDENTIFIANT").ToString(), ora_dr_alerte.Item("EMAIL_EMETTEUR").ToString(), ora_dr_alerte.Item("NOM_EMETTEUR").ToString(), ora_dr_alerte.Item("PRENOM_EMETTEUR").ToString())
                    alerte_temp.Commentaire = ora_dr_alerte.Item("COMMENTAIRE").ToString()
                    alerte_temp.DocId = ora_dr_alerte.Item("DOCID").ToString()
                    alerte_temp.DateResolution = ora_dr_alerte.Item("DATE_RESOLUTION").ToString()
                    alerte_temp.ResoluPar = New Contact(ora_dr_alerte.Item("IDENTIFIANT_RESOLUTION").ToString(), ora_dr_alerte.Item("EMAIL_RESOLUTION").ToString(), ora_dr_alerte.Item("NOM_RESOLUTION").ToString(), ora_dr_alerte.Item("PRENOM_RESOLUTION").ToString())

                    liste_destinataires = New List(Of String)(ora_dr_alerte("DESTINATAIRES").ToString().Split(","c))

                    'For Each email_dest As String In liste_destinataires

                    '    email_dest = email_dest.Replace("'", "")

                    '    requete_destinataires = "select identifiant, nom, prenom, email from utilisateur where email='" & email_dest & "'"
                    '    ora_commande_destinataires = New OracleCommand(requete_destinataires, ora_connexion)
                    '    ora_dr_destinataires = ora_commande_destinataires.ExecuteReader()

                    '    If (ora_dr_destinataires.Read()) Then
                    '        alerte_temp.Destinataires.Add(New Contact(ora_dr_destinataires.Item("IDENTIFIANT").ToString(), ora_dr_destinataires.Item("EMAIL").ToString(), ora_dr_destinataires.Item("NOM").ToString(), ora_dr_destinataires.Item("PRENOM").ToString()))
                    '    Else
                    '        alerte_temp.Destinataires.Add(New Contact("", email_dest, "", ""))
                    '    End If

                    '    ora_dr_destinataires.Close()

                    'Next

                    resultat.Add(alerte_temp)

                End While

                ora_dr_alerte.Close()

            Catch ex As Exception
                resultat = New List(Of AlerteFacture)
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), user_parametre)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of AlerteFacture)
            LogsFunctions.LogWrite(ex.ToString(), user_parametre)
        End Try

        Return resultat

    End Function

    Public Shared Function IdentifiantExistant(p_identifiant As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As Boolean = True

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                p_identifiant = p_identifiant.Replace("'", "''")

                requete = "select identifiant from utilisateur " &
                   "where identifiant='" & p_identifiant & "'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                If (Not ora_dr.Read()) Then
                    resultat = False
                End If

                ora_dr.Close()

            Catch ex As Exception
                resultat = True
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = True
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function DeleteUtilisateur(p_utilisateur As Utilisateur) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_trans As OracleTransaction
        Dim requete_utilisateur, requete_filtre, requete_alerte, requete_correction, requete_enrichissement, requete_message, requete_piece_jointe, requete_evenement As String
        Dim resultat As Boolean
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            ora_trans = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

            Try

                resultat = False

                requete_utilisateur = "delete from utilisateur " &
                    "where identifiant='" & p_utilisateur.Identifiant & "' and workflow='" & p_utilisateur.Workflow & "'"

                requete_alerte = "delete from alerte_automatique_utilisateur " &
                   "where identifiant='" & p_utilisateur.Identifiant & "' and workflow='" & p_utilisateur.Workflow & "'"

                requete_filtre = "delete from filtre_corbeille_utilisateur " &
                   "where identifiant='" & p_utilisateur.Identifiant & "' and workflow='" & p_utilisateur.Workflow & "'"

                requete_correction = "delete from champ_correction " &
                   "where identifiant='" & p_utilisateur.Identifiant & "' and workflow='" & p_utilisateur.Workflow & "'"

                requete_enrichissement = "delete from champ_enrichissement " &
                   "where identifiant='" & p_utilisateur.Identifiant & "' and workflow='" & p_utilisateur.Workflow & "'"

                requete_evenement = "delete from evenement " &
                   "where identifiant='" & p_utilisateur.Identifiant & "' and workflow='" & p_utilisateur.Workflow & "'"

                requete_message = "delete from message " &
                  "where identifiant='" & p_utilisateur.Identifiant & "' and workflow='" & p_utilisateur.Workflow & "'"

                requete_piece_jointe = "delete from piece_jointe_message " &
                  "where identifiant='" & p_utilisateur.Identifiant & "' and workflow='" & p_utilisateur.Workflow & "'"

                ora_commande = New OracleCommand(requete_utilisateur, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb > 0) Then

                    ora_commande = New OracleCommand(requete_alerte, ora_connexion)
                    nb = ora_commande.ExecuteNonQuery()

                    ora_commande = New OracleCommand(requete_filtre, ora_connexion)
                    nb = ora_commande.ExecuteNonQuery()

                    ora_commande = New OracleCommand(requete_correction, ora_connexion)
                    nb = ora_commande.ExecuteNonQuery()

                    ora_commande = New OracleCommand(requete_enrichissement, ora_connexion)
                    nb = ora_commande.ExecuteNonQuery()

                    ora_commande = New OracleCommand(requete_evenement, ora_connexion)
                    nb = ora_commande.ExecuteNonQuery()

                    ora_commande = New OracleCommand(requete_message, ora_connexion)
                    nb = ora_commande.ExecuteNonQuery()

                    ora_commande = New OracleCommand(requete_piece_jointe, ora_connexion)
                    nb = ora_commande.ExecuteNonQuery()

                    ora_trans.Commit()

                    resultat = True

                End If

            Catch ex As Exception
                ora_trans.Rollback()
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), p_utilisateur)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), p_utilisateur)
        End Try

        Return resultat

    End Function

    Public Shared Function getFournisseurs(p_utilisateur As Utilisateur, valeur_saisie As String, nb_resultats As Integer) As List(Of Fournisseur)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete_fournisseurs As String
        Dim resultat As New List(Of Fournisseur)
        Dim fournisseur_temp As Fournisseur

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                'La requête paramétrée en base doit retouner les champs "ID" et "LIBELLE"

                requete_fournisseurs = p_utilisateur.RequeteFournisseurs
                ora_commande = New OracleCommand(requete_fournisseurs, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    fournisseur_temp = New Fournisseur(ora_dr.Item("ID").ToString(), ora_dr.Item("LIBELLE").ToString())
                    resultat.Add(fournisseur_temp)

                End While

                'Filtrage par rapport à la valeur saisie

                resultat = resultat.FindAll(Function(x) x.NomFournisseur.ToUpper() Like "*" & valeur_saisie.ToUpper() & "*").OrderBy(Function(x) x.NomFournisseur).Take(nb_resultats).ToList()

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of Fournisseur)
                LogsFunctions.LogWrite(ex.ToString(), p_utilisateur)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Fournisseur)
            LogsFunctions.LogWrite(ex.ToString(), p_utilisateur)
        End Try

        Return resultat

    End Function

    Public Shared Function ControleMdpOublie(identifiant As String, email As String) As String

        Dim ora_connexion As OracleConnection
        Dim ora_commande_recherche, ora_commande_id_controle As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete_recherche, requete_id_controle As String
        Dim resultat As String = ""
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                identifiant = identifiant.Replace("'", "''").Trim()
                email = email.Replace("'", "").Trim()

                requete_recherche = "select * from utilisateur where upper(identifiant)='" & identifiant.ToUpper() & "' and upper(email)='" & email.ToUpper() & "'"
                ora_commande_recherche = New OracleCommand(requete_recherche, ora_connexion)
                ora_dr = ora_commande_recherche.ExecuteReader()

                If (ora_dr.Read()) Then

                    Dim id_controle As Guid
                    id_controle = Guid.NewGuid()

                    requete_id_controle = "update utilisateur set id_controle='" & id_controle.ToString() & "' where upper(identifiant)='" & identifiant.ToUpper() & "'"
                    ora_commande_id_controle = New OracleCommand(requete_id_controle, ora_connexion)
                    nb = ora_commande_id_controle.ExecuteNonQuery()

                    If (nb = 1) Then
                        resultat = id_controle.ToString()
                    End If

                End If

                ora_dr.Close()

            Catch ex As Exception
                resultat = ""
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = ""
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function UpdateInfoDocumentHorsScope(pUser As Utilisateur, pDocid As String, pClassif As String, pEntite As String, pSite As String, pSite2 As String, pStatut As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete As String
        Dim resultat As Boolean = False

        Try


            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try
                If pStatut = "à traiter" Or pStatut = "" Then
                    requete = $"update tb_document_hors_scope set id_type='{pClassif}', code_societe='{pEntite}', nom_societe='{pSite}', statut='2' , SITE = '{pSite2}' , STATUT2 = '{pStatut}' where id_document='{pDocid}' and id_client='{pUser.Workflow}'"
                Else
                    requete = $"update tb_document_hors_scope set id_type='{pClassif}', code_societe='{pEntite}', nom_societe='{pSite}', statut='7' , SITE = '{pSite2}' , STATUT2 = '{pStatut}' where id_document='{pDocid}' and id_client='{pUser.Workflow}'"
                End If
                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_commande.ExecuteNonQuery()


                resultat = True


            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function UpdateDocumentHorsScope(pUser As Utilisateur, pDocid As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete As String
        Dim resultat As Boolean = False
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try



                requete = "update tb_document_hors_scope set statut='7' where id_document='" & pDocid & "'"
                ora_commande = New OracleCommand(requete, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb >= 0) Then
                    resultat = True
                End If

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function
    Public Shared Function UpdateMdp(id_controle As String, mdp As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete As String
        Dim resultat As Boolean = False
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                mdp = mdp.Replace("'", "''")

                requete = "update utilisateur set mot_de_passe='" & mdp & "',id_controle=null where id_controle='" & id_controle & "'"
                ora_commande = New OracleCommand(requete, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb >= 0) Then
                    resultat = True
                End If

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function UpdateMdpId(identifiant As String, mdp As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete As String
        Dim resultat As Boolean = False
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                mdp = mdp.Replace("'", "''")
                identifiant = identifiant.Replace("'", "''")

                requete = "update utilisateur set mot_de_passe='" & mdp & "',id_controle=null where identifiant='" & identifiant & "'"
                ora_commande = New OracleCommand(requete, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb >= 0) Then
                    resultat = True
                End If

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function getFactures(user As Utilisateur, id_filtre As String, liste_statuts As List(Of String), exclure_occupe As Boolean, exclure_rejets As Boolean, rejets_uniquement As Boolean, id_rejet As String, nb_jours_limite As Integer, p_periode_archive As String, Optional ByVal archive As String = "") As List(Of Facture)

        Dim ora_connexion As OracleConnection
        Dim ora_commande_filtre, ora_commande_facture As OracleCommand
        Dim ora_dr_filtre, ora_dr_facture As OracleDataReader
        Dim requete_filtre As String = ""
        Dim requete_facture As String = ""
        Dim resultat As New List(Of Facture)
        Dim chaine_parametres As String = ""
        Dim facture_temp As Facture
        Dim annee, mois As String

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                'recherche des paramêtres du filtre

                requete_filtre = "select cw.nom_champ,cw.typage,cw.requete_liste,cw.expression_reguliere,cfc.valeur,cfc.operateur " &
                    "from champ_filtre_corbeille cfc " &
                    "inner join champ_workflow cw on (cfc.workflow=cw.workflow and cfc.id_champ=cw.id_champ) " &
                    "where cfc.workflow='" & user.Workflow & "' and cfc.id_filtre_corbeille='" & id_filtre & "'"

                ora_commande_filtre = New OracleCommand(requete_filtre, ora_connexion)
                ora_dr_filtre = ora_commande_filtre.ExecuteReader()

                While (ora_dr_filtre.Read())

                    Dim operateur As String = ora_dr_filtre.Item("OPERATEUR").ToString()
                    Dim valeur As String = ora_dr_filtre.Item("VALEUR").ToString()
                    Dim nom_champ As String = ora_dr_filtre.Item("NOM_CHAMP").ToString()
                    Dim typage As String = ora_dr_filtre.Item("TYPAGE").ToString()

                    If (typage = "TEXTE" Or typage = "LISTE") Then

                        If (operateur = "LIKE") Then

                            chaine_parametres += " and f." & nom_champ & " like '%" & valeur & "%' "

                        ElseIf (operateur = "IS_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is null "

                        ElseIf (operateur = "IS_NOT_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is not null "

                        Else

                            chaine_parametres += " and f." & nom_champ & operateur & "'" & valeur & "' "

                        End If

                    ElseIf (typage = "ENTIER" Or typage = "DECIMAL") Then

                        If (operateur = "IS_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is null "

                        ElseIf (operateur = "IS_NOT_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is not null "
                        Else

                            chaine_parametres += " and f." & nom_champ & operateur & "'" & valeur & "' "

                        End If

                    ElseIf (typage = "DATE") Then

                        If (operateur = "DATATION_JOURS") Then

                            chaine_parametres += " and f." & nom_champ & " <= sysdate-" & valeur & " "

                        ElseIf (operateur = "DATATION_MOIS") Then

                            chaine_parametres += " and f." & nom_champ & " <= add_months(sysdate,-" & valeur & ") "

                        ElseIf (operateur = "DATATION_ANNEES") Then

                            chaine_parametres += " and f." & nom_champ & " <= add_months(sysdate,-12*" & valeur & ") "

                        ElseIf (operateur = "IS_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is null "

                        ElseIf (operateur = "IS_NOT_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is not null "

                        Else

                            chaine_parametres += " and f." & nom_champ & operateur & "to_date('" & valeur & "','dd/mm/yyyy') "

                        End If

                    End If

                End While

                ora_dr_filtre.Close()

                'Recherche des factures correspondantes

                Dim statuts As String = ""

                For Each s As String In liste_statuts

                    If (s.Trim() <> "") Then
                        statuts += "'" & s & "',"
                    End If

                Next

                If (statuts.Length > 0) Then
                    statuts = statuts.Substring(0, statuts.Length - 1)
                End If

                requete_facture = "select distinct f.*,sf.libelle_statut_facture,tba.date_insertion as date_acquittement,tbr.libelle_rejet,inv.libelle_motif_invalidite " &
                    "from " & user.TableFacture & " f " &
                    "inner join statut_facture sf on f.id_statut_facture=sf.id_statut_facture " &
                    "left join tb_livraison tbl on (tbl.id_facture=f.id_facture) " &
                    "left join tb_acquittement tba on (tba.id_livraison=tbl.id_livraison) " &
                    "left join tb_rejet tbr on (tbr.id_rejet=f.id_rejet and tbr.id_client=f.id_client) " &
                    "left join motif_invalidite inv on (f.id_motif_invalidite=inv.id_motif_invalidite and inv.workflow=f.id_client) " &
                    "left join historique h on (f.id_facture=h.docid and f.id_client=h.workflow) " &
                    "where f.id_client='" & user.Workflow & "' " &
                    "and f.id_statut_facture in (" & statuts & ") " & chaine_parametres

                If (Not String.IsNullOrEmpty(archive)) Then
                    If (archive = "HISTORIQUE") Then
                        requete_facture += " and h.id_champ='34'"
                    ElseIf (archive = "099PU") Then
                        requete_facture += " and f.num_commande like '099PU%'"
                    ElseIf (archive = "EGENCIA") Then
                        requete_facture += " and f.nom_fournisseur='EGENCIA'"
                    End If
                End If

                If (exclure_occupe) Then
                    requete_facture += " and (f.flag_occupe is null or f.flag_occupe<>'1') "
                End If

                If (exclure_rejets) Then
                    requete_facture += " and f.id_rejet is null "
                End If

                If (rejets_uniquement) Then

                    requete_facture += " and f.id_rejet is not null "

                    If (id_rejet.Trim() <> "") Then

                        requete_facture += " and f.id_rejet = '" & id_rejet & "' "

                    End If

                End If

                If (nb_jours_limite > 0) Then

                    requete_facture += " and date_insertion > (sysdate-" & nb_jours_limite.ToString() & ") "

                End If


                If (p_periode_archive <> "") Then

                    annee = p_periode_archive.Split("_")(0).ToString()
                    mois = p_periode_archive.Split("_")(1).ToString()

                    requete_facture += " and archive_annee='" & annee & "' and archive_mois='" & mois & "' "

                End If

                ora_commande_facture = New OracleCommand(requete_facture, ora_connexion)
                ora_dr_facture = ora_commande_facture.ExecuteReader()

                While (ora_dr_facture.Read())

                    facture_temp = New Facture()
                    facture_temp.IDFacture = ora_dr_facture.Item("id_facture").ToString()
                    facture_temp.CodeFournisseur = ora_dr_facture.Item("code_fournisseur").ToString()
                    facture_temp.NomFournisseur = ora_dr_facture.Item("nom_fournisseur").ToString()
                    facture_temp.NumFacture = ora_dr_facture.Item("num_facture").ToString()
                    facture_temp.DateFacture = ora_dr_facture.Item("date_facture").ToString()
                    facture_temp.EntiteFacture = ora_dr_facture.Item("nom_societe").ToString()
                    facture_temp.MontantTTC = ora_dr_facture.Item("montant_total").ToString()
                    facture_temp.NumCommande = ora_dr_facture.Item("num_commande").ToString()
                    facture_temp.DateInsertion = ora_dr_facture.Item("date_insertion").ToString()
                    facture_temp.DateAcquittement = ora_dr_facture.Item("date_acquittement").ToString()
                    facture_temp.Statut = New Statut(ora_dr_facture.Item("id_statut_facture").ToString(), ora_dr_facture.Item("libelle_statut_facture").ToString())
                    facture_temp.DateTraitement = ora_dr_facture.Item("date_traitement_workflow").ToString()
                    facture_temp.Rejet = New Rejet(ora_dr_facture.Item("id_rejet").ToString(), ora_dr_facture.Item("libelle_rejet").ToString())
                    facture_temp.MotifInvalidite = New Motif(ora_dr_facture.Item("id_motif_invalidite").ToString(), ora_dr_facture.Item("libelle_motif_invalidite").ToString(), "")
                    facture_temp.CommentaireInvalidite = ora_dr_facture.Item("commentaire_invalidite").ToString()
                    facture_temp.DateEnvoiValideur = ora_dr_facture.Item("date_envoi_valideur").ToString()
                    facture_temp.DateRetourValideur = ora_dr_facture.Item("date_retour_valideur").ToString()
                    facture_temp.IdentifiantValideur = ora_dr_facture.Item("identifiant_valideur").ToString()
                    facture_temp.Site = ora_dr_facture.Item("SITE").ToString()

                    If ora_dr_facture.Item("TRAITEMENT").ToString() = "29" Or ora_dr_facture.Item("TRAITEMENT").ToString() = "" Then
                        facture_temp.Statut2 = "à traiter"
                    Else
                        facture_temp.Statut2 = "traité"
                    End If

                    'Exception des champs spécifiques pour AG2R
                    If (ColumnExists(ora_dr_facture, "id_engagement")) Then
                        facture_temp.IdEngagement = ora_dr_facture.Item("id_engagement").ToString()
                    End If
                    If (ColumnExists(ora_dr_facture, "date_prestation")) Then
                        facture_temp.DateLivraison = ora_dr_facture.Item("date_prestation").ToString()
                    End If


                    'Try
                    '    facture_temp.IdEngagement = ora_dr_facture.Item("id_engagement").ToString()
                    'Catch
                    'End Try

                    'Try
                    '    facture_temp.DateLivraison = ora_dr_facture.Item("date_prestation").ToString()
                    'Catch
                    'End Try


                    If (facture_temp.DateFacture.ToString().Length > 0) Then
                        facture_temp.DateFacture = facture_temp.DateFacture.Substring(0, 10)
                    End If

                    If (facture_temp.DateInsertion.ToString().Length > 0) Then
                        facture_temp.DateInsertion = facture_temp.DateInsertion.Substring(0, 10)
                    End If

                    If (facture_temp.DateLivraison.ToString().Length > 0) Then
                        facture_temp.DateLivraison = facture_temp.DateLivraison.Substring(0, 10)
                    End If

                    If (facture_temp.DateAcquittement.ToString().Length > 0) Then
                        facture_temp.DateAcquittement = facture_temp.DateAcquittement.Substring(0, 10)
                    End If

                    If (facture_temp.DateTraitement.ToString().Length > 0) Then
                        facture_temp.DateTraitement = facture_temp.DateTraitement.Substring(0, 10)
                    End If

                    If (facture_temp.DateEnvoiValideur.ToString().Length > 0) Then
                        facture_temp.DateEnvoiValideur = facture_temp.DateEnvoiValideur.Substring(0, 10)
                    End If

                    If (facture_temp.DateRetourValideur.ToString().Length > 0) Then
                        facture_temp.DateRetourValideur = facture_temp.DateRetourValideur.Substring(0, 10)
                    End If


                    resultat.Add(facture_temp)

                End While

                ora_dr_facture.Close()

                If (user.Email = "esoares@cba.fr") Then
                    LogsFunctions.LogWrite2(requete_facture, user)
                    LogsFunctions.LogWrite2(requete_filtre, user)
                    LogsFunctions.LogWrite2(resultat.Count, user)
                End If


            Catch ex As Exception
                resultat = New List(Of Facture)
                LogsFunctions.LogWrite(ex.ToString(), user)
                LogsFunctions.LogWrite(requete_facture, user)
                LogsFunctions.LogWrite(requete_filtre, user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Facture)
            LogsFunctions.LogWrite(ex.ToString(), user)
            LogsFunctions.LogWrite(requete_facture, user)
            LogsFunctions.LogWrite(requete_filtre, user)
        End Try

        Return resultat

    End Function

    Public Shared Function ColumnExists(reader As OracleDataReader, colonne As String)

        Dim iIndex As Integer
        Dim ifieldName As String

        For iIndex = 0 To reader.FieldCount - 1
            ifieldName = reader.GetName(iIndex)
            If (ifieldName = colonne) Then
                Return True
            End If
        Next

        Return False
    End Function


    Public Shared Function getFacturesEgencia(user As Utilisateur, id_filtre As String, liste_statuts As List(Of String), exclure_occupe As Boolean, exclure_rejets As Boolean, rejets_uniquement As Boolean, id_rejet As String, nb_jours_limite As Integer, p_periode_archive As String) As List(Of Facture)

        Dim ora_connexion As OracleConnection
        Dim ora_commande_filtre, ora_commande_facture As OracleCommand
        Dim ora_dr_filtre, ora_dr_facture As OracleDataReader
        Dim requete_filtre As String = ""
        Dim requete_facture As String = ""
        Dim resultat As New List(Of Facture)
        Dim chaine_parametres As String = ""
        Dim facture_temp As Facture
        Dim annee, mois As String

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                'recherche des paramêtres du filtre

                requete_filtre = "select cw.nom_champ,cw.typage,cw.requete_liste,cw.expression_reguliere,cfc.valeur,cfc.operateur " &
                    "from champ_filtre_corbeille cfc " &
                    "inner join champ_workflow cw on (cfc.workflow=cw.workflow and cfc.id_champ=cw.id_champ) " &
                    "where cfc.workflow='" & user.Workflow & "' and cfc.id_filtre_corbeille='" & id_filtre & "'"

                ora_commande_filtre = New OracleCommand(requete_filtre, ora_connexion)
                ora_dr_filtre = ora_commande_filtre.ExecuteReader()

                While (ora_dr_filtre.Read())

                    Dim operateur As String = ora_dr_filtre.Item("OPERATEUR").ToString()
                    Dim valeur As String = ora_dr_filtre.Item("VALEUR").ToString()
                    Dim nom_champ As String = ora_dr_filtre.Item("NOM_CHAMP").ToString()
                    Dim typage As String = ora_dr_filtre.Item("TYPAGE").ToString()

                    If (typage = "TEXTE" Or typage = "LISTE") Then

                        If (operateur = "LIKE") Then

                            chaine_parametres += " and f." & nom_champ & " like '%" & valeur & "%' "

                        ElseIf (operateur = "IS_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is null "

                        ElseIf (operateur = "IS_NOT_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is not null "

                        Else

                            chaine_parametres += " and f." & nom_champ & operateur & "'" & valeur & "' "

                        End If

                    ElseIf (typage = "ENTIER" Or typage = "DECIMAL") Then

                        If (operateur = "IS_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is null "

                        ElseIf (operateur = "IS_NOT_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is not null "
                        Else

                            chaine_parametres += " and f." & nom_champ & operateur & "'" & valeur & "' "

                        End If

                    ElseIf (typage = "DATE") Then

                        If (operateur = "DATATION_JOURS") Then

                            chaine_parametres += " and f." & nom_champ & " <= sysdate-" & valeur & " "

                        ElseIf (operateur = "DATATION_MOIS") Then

                            chaine_parametres += " and f." & nom_champ & " <= add_months(sysdate,-" & valeur & ") "

                        ElseIf (operateur = "DATATION_ANNEES") Then

                            chaine_parametres += " and f." & nom_champ & " <= add_months(sysdate,-12*" & valeur & ") "

                        ElseIf (operateur = "IS_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is null "

                        ElseIf (operateur = "IS_NOT_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is not null "

                        Else

                            chaine_parametres += " and f." & nom_champ & operateur & "to_date('" & valeur & "','dd/mm/yyyy') "

                        End If

                    End If

                End While

                ora_dr_filtre.Close()

                'Recherche des factures correspondantes

                Dim statuts As String = ""

                For Each s As String In liste_statuts

                    If (s.Trim() <> "") Then
                        statuts += "'" & s & "',"
                    End If

                Next

                If (statuts.Length > 0) Then
                    statuts = statuts.Substring(0, statuts.Length - 1)
                End If

                requete_facture = "select distinct f.*,sf.libelle_statut_facture,tba.date_insertion as date_acquittement,tbr.libelle_rejet,inv.libelle_motif_invalidite " &
                    "from " & user.TableFacture & " f " &
                    "inner join statut_facture sf on f.id_statut_facture=sf.id_statut_facture " &
                    "left join tb_livraison tbl on (tbl.id_facture=f.id_facture) " &
                    "left join tb_acquittement tba on (tba.id_livraison=tbl.id_livraison) " &
                    "left join tb_rejet tbr on (tbr.id_rejet=f.id_rejet and tbr.id_client=f.id_client) " &
                    "left join motif_invalidite inv on (f.id_motif_invalidite=inv.id_motif_invalidite and inv.workflow=f.id_client) " &
                    "where f.id_client='" & user.Workflow & "' and f.NOM_FOURNISSEUR = 'EGENCIA'"

                If (nb_jours_limite > 0) Then

                    requete_facture += " and date_insertion > (sysdate-" & nb_jours_limite.ToString() & ") "

                End If


                If (p_periode_archive <> "") Then

                    annee = p_periode_archive.Split("_")(0).ToString()
                    mois = p_periode_archive.Split("_")(1).ToString()

                    requete_facture += " and archive_annee='" & annee & "' and archive_mois='" & mois & "' "

                End If

                ora_commande_facture = New OracleCommand(requete_facture, ora_connexion)
                ora_dr_facture = ora_commande_facture.ExecuteReader()

                While (ora_dr_facture.Read())

                    facture_temp = New Facture()
                    facture_temp.IDFacture = ora_dr_facture.Item("id_facture").ToString()
                    facture_temp.CodeFournisseur = ora_dr_facture.Item("code_fournisseur").ToString()
                    facture_temp.NomFournisseur = ora_dr_facture.Item("nom_fournisseur").ToString()
                    facture_temp.NumFacture = ora_dr_facture.Item("num_facture").ToString()
                    facture_temp.DateFacture = ora_dr_facture.Item("date_facture").ToString()
                    facture_temp.EntiteFacture = ora_dr_facture.Item("nom_societe").ToString()
                    facture_temp.MontantTTC = ora_dr_facture.Item("montant_total").ToString()
                    facture_temp.NumCommande = ora_dr_facture.Item("num_commande").ToString()
                    facture_temp.DateInsertion = ora_dr_facture.Item("date_insertion").ToString()
                    facture_temp.DateAcquittement = ora_dr_facture.Item("date_acquittement").ToString()
                    facture_temp.Statut = New Statut(ora_dr_facture.Item("id_statut_facture").ToString(), ora_dr_facture.Item("libelle_statut_facture").ToString())
                    facture_temp.DateTraitement = ora_dr_facture.Item("date_traitement_workflow").ToString()
                    facture_temp.Rejet = New Rejet(ora_dr_facture.Item("id_rejet").ToString(), ora_dr_facture.Item("libelle_rejet").ToString())
                    facture_temp.MotifInvalidite = New Motif(ora_dr_facture.Item("id_motif_invalidite").ToString(), ora_dr_facture.Item("libelle_motif_invalidite").ToString(), "")
                    facture_temp.CommentaireInvalidite = ora_dr_facture.Item("commentaire_invalidite").ToString()
                    facture_temp.DateEnvoiValideur = ora_dr_facture.Item("date_envoi_valideur").ToString()
                    facture_temp.DateRetourValideur = ora_dr_facture.Item("date_retour_valideur").ToString()
                    facture_temp.IdentifiantValideur = ora_dr_facture.Item("identifiant_valideur").ToString()
                    facture_temp.Site = ora_dr_facture.Item("SITE").ToString()
                    facture_temp.Statut2 = ora_dr_facture.Item("TRAITEMENT").ToString()

                    'Exception des champs spécifiques pour AG2R
                    'Try
                    '    facture_temp.IdEngagement = ora_dr_facture.Item("id_engagement").ToString()
                    'Catch
                    'End Try

                    'Try
                    '    facture_temp.DateLivraison = ora_dr_facture.Item("date_prestation").ToString()
                    'Catch
                    'End Try


                    If (facture_temp.DateFacture.ToString().Length > 0) Then
                        facture_temp.DateFacture = facture_temp.DateFacture.Substring(0, 10)
                    End If

                    If (facture_temp.DateInsertion.ToString().Length > 0) Then
                        facture_temp.DateInsertion = facture_temp.DateInsertion.Substring(0, 10)
                    End If

                    If (facture_temp.DateLivraison.ToString().Length > 0) Then
                        facture_temp.DateLivraison = facture_temp.DateLivraison.Substring(0, 10)
                    End If

                    If (facture_temp.DateAcquittement.ToString().Length > 0) Then
                        facture_temp.DateAcquittement = facture_temp.DateAcquittement.Substring(0, 10)
                    End If

                    If (facture_temp.DateTraitement.ToString().Length > 0) Then
                        facture_temp.DateTraitement = facture_temp.DateTraitement.Substring(0, 10)
                    End If

                    If (facture_temp.DateEnvoiValideur.ToString().Length > 0) Then
                        facture_temp.DateEnvoiValideur = facture_temp.DateEnvoiValideur.Substring(0, 10)
                    End If

                    If (facture_temp.DateRetourValideur.ToString().Length > 0) Then
                        facture_temp.DateRetourValideur = facture_temp.DateRetourValideur.Substring(0, 10)
                    End If


                    resultat.Add(facture_temp)

                End While

                ora_dr_facture.Close()

                If (user.Email = "esoares@cba.fr") Then
                    LogsFunctions.LogWrite2(requete_facture, user)
                    LogsFunctions.LogWrite2(requete_filtre, user)
                    LogsFunctions.LogWrite2(resultat.Count, user)
                End If


            Catch ex As Exception
                resultat = New List(Of Facture)
                LogsFunctions.LogWrite(ex.ToString(), user)
                LogsFunctions.LogWrite(requete_facture, user)
                LogsFunctions.LogWrite(requete_filtre, user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Facture)
            LogsFunctions.LogWrite(ex.ToString(), user)
            LogsFunctions.LogWrite(requete_facture, user)
            LogsFunctions.LogWrite(requete_filtre, user)
        End Try

        Return resultat

    End Function

    Public Shared Function getUrlHorsScope(user As Utilisateur, docid As String) As String

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As String = ""

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = "select chemin_image from tb_document_hors_scope where id_document='" & docid & "'"
                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                If (ora_dr.Read()) Then

                    Dim repertoires_images As New List(Of String)(ConfigurationManager.AppSettings("repertoires_images_base").Split(";"))

                    resultat = ora_dr.Item("CHEMIN_IMAGE").ToString().ToUpper()

                    For Each repertoire As String In repertoires_images

                        resultat = resultat.Replace(repertoire.ToUpper(), ConfigurationManager.AppSettings("repertoire_pdf").ToString())

                    Next

                End If

                ora_dr.Close()

            Catch ex As Exception
                resultat = ""
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = ""
        End Try

        Return resultat

    End Function

    Public Shared Function getUrlPDF(user As Utilisateur, docid As String) As String

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As String = ""

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = "select chemin_image from tb_image where id_facture='" & docid & "'"
                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                If (ora_dr.Read()) Then

                    Dim repertoires_images As New List(Of String)(ConfigurationManager.AppSettings("repertoires_images_base").Split(";"))

                    resultat = ora_dr.Item("CHEMIN_IMAGE").ToString().ToUpper()

                    For Each repertoire As String In repertoires_images

                        resultat = resultat.Replace(repertoire.ToUpper(), ConfigurationManager.AppSettings("repertoire_pdf").ToString())

                    Next

                End If

                ora_dr.Close()

            Catch ex As Exception
                resultat = ""
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = ""
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function getValeursFacture(ByRef liste_champs As List(Of Champ), user As Utilisateur, docid As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_commande_liste As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim ora_dr_liste As OracleDataReader
        Dim requete As String
        Dim resultat As Boolean = False

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = "select * from " & user.TableFacture & " where id_client='" & user.Workflow & "' and id_facture='" & docid & "'"
                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                If (ora_dr.Read()) Then

                    For Each ch As Champ In liste_champs

                        If (ch.Typage = "LISTE") Then

                            If (ch.Requete.Trim() <> "") Then

                                Try

                                    ora_commande_liste = New OracleCommand(ch.Requete, ora_connexion)
                                    ora_dr_liste = ora_commande_liste.ExecuteReader()

                                    While (ora_dr_liste.Read())

                                        ch.Liste.Add(New ListItem(ora_dr_liste("LIBELLE").ToString(), ora_dr_liste("ID").ToString()))

                                    End While

                                    ora_dr_liste.Close()

                                Catch ex As Exception
                                    ch.Liste = New List(Of ListItem)
                                End Try
                            End If
                        End If

                        ch.Valeur = ora_dr.Item(ch.NomChamp).ToString()

                        If (ch.Typage = "DATE" And ch.Valeur.Length > 0) Then
                            ch.Valeur = ch.Valeur.Substring(0, 10)
                        End If

                    Next

                    resultat = True

                End If

                ora_dr.Close()

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function getValeursLignesFacture(ByRef liste_champs As List(Of Champ), user As Utilisateur, docid As String) As List(Of LigneFacture)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_commande_liste As OracleCommand
        Dim ora_commande_val_regles As OracleCommand
        Dim ora_commande_val_test As OracleCommand
        Dim ora_commande_val_update As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim ora_dr_liste As OracleDataReader
        Dim ora_dr_val_regles As OracleDataReader
        Dim ora_dr_val_test As OracleDataReader
        Dim requete, requete_valorisation_regles, requete_valorisation_test, requete_valorisation_update As String
        Dim resultat As New List(Of LigneFacture)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = "select * from " & user.TableLigneFacture & " where id_client='" & user.Workflow & "' and id_facture='" & docid & "'"
                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    Dim liste_champ_valorises As New List(Of Champ)

                    For Each ch As Champ In liste_champs

                        If (ch.Typage = "LISTE") Then

                            If (ch.Requete.Trim() <> "") Then

                                Try

                                    ora_commande_liste = New OracleCommand(ch.Requete, ora_connexion)
                                    ora_dr_liste = ora_commande_liste.ExecuteReader()

                                    While (ora_dr_liste.Read())

                                        ch.Liste.Add(New ListItem(ora_dr_liste("LIBELLE").ToString(), ora_dr_liste("ID").ToString()))

                                    End While

                                    ora_dr_liste.Close()

                                Catch ex As Exception
                                    ch.Liste = New List(Of ListItem)
                                    LogsFunctions.LogWrite(ex.ToString(), user)
                                End Try

                            End If

                        End If

                        ch.Valeur = ora_dr.Item(ch.NomChamp).ToString()

                        If (ch.Typage = "DATE" And ch.Valeur.Length > 0) Then
                            ch.Valeur = ch.Valeur.Substring(0, 10)
                        End If

                        'REMPLISSAGE AUTOMATIQUE PAR RAPPORT A LA TABLE REGLE_VALORISATION_LIGNE

                        If (ch.Valeur.Trim().Length = 0) Then

                            requete_valorisation_regles = "select RVL.*,CWL.NOM_CHAMP as NOM_CHAMP_LIGNE,CWR.NOM_CHAMP as NON_CHAMP_REFERENCE " &
                                "from REGLE_VALORISATION_LIGNE RVL " &
                                "inner join CHAMP_WORKFLOW CWL on (RVL.ID_CHAMP_LIGNE=CWL.ID_CHAMP and RVL.workflow=CWL.WORKFLOW) " &
                                "inner join CHAMP_WORKFLOW CWR on (RVL.ID_CHAMP_REFERENCE=CWR.ID_CHAMP and RVL.workflow=CWR.WORKFLOW) " &
                                "where RVL.workflow='" & user.Workflow & "' and RVL.id_champ_ligne='" & ch.IDChamp & "'"

                            ora_commande_val_regles = New OracleCommand(requete_valorisation_regles, ora_connexion)
                            ora_dr_val_regles = ora_commande_val_regles.ExecuteReader()

                            While (ora_dr_val_regles.Read())

                                If (ora_dr_val_regles.Item("WORKFLOW").ToString() = "62" And
                                        ora_dr_val_regles.Item("VALEUR_REFERENCE").ToString() = "#COMPTE_GENERAL#" And
                                        ora_dr_val_regles.Item("VALEUR_LIGNE").ToString() = "#COLLECTIF#") Then

                                    'EXCEPTION WORKFLOW 62 : Calcul du compte_collectif en fonction du code_comptable

                                    ch.Valeur = DBFunctions.getCollectif(user, ora_dr.Item("COMPTE_GENERAL").ToString())

                                    requete_valorisation_update = "update " & user.TableLigneFacture &
                                    " set COLLECTIF='" & ch.Valeur & "' " &
                                    " where id_ligne='" & ora_dr.Item("ID_LIGNE").ToString() & "' and id_facture='" & ora_dr.Item("ID_FACTURE").ToString() & "'"

                                    ora_commande_val_update = New OracleCommand(requete_valorisation_update, ora_connexion)
                                    ora_commande_val_update.ExecuteNonQuery()

                                Else

                                    requete_valorisation_test = "select " & ora_dr_val_regles.Item("NON_CHAMP_REFERENCE").ToString() &
                                        " from " & user.TableFacture & " where ID_FACTURE='" & docid & "'"

                                    ora_commande_val_test = New OracleCommand(requete_valorisation_test, ora_connexion)
                                    ora_dr_val_test = ora_commande_val_test.ExecuteReader()

                                    If (ora_dr_val_test.Read()) Then

                                        If (ora_dr_val_test.Item(0).ToString().Trim() = ora_dr_val_regles.Item("VALEUR_REFERENCE").ToString()) Then

                                            ch.Valeur = ora_dr_val_regles.Item("VALEUR_LIGNE")

                                            requete_valorisation_update = "update " & user.TableLigneFacture &
                                                " set " & ora_dr_val_regles.Item("NOM_CHAMP_LIGNE").ToString() & "='" & ora_dr_val_regles.Item("VALEUR_LIGNE").ToString() & "' " &
                                                " where id_ligne='" & ora_dr.Item("ID_LIGNE").ToString() & "' and id_facture='" & ora_dr.Item("ID_FACTURE").ToString() & "'"

                                            ora_commande_val_update = New OracleCommand(requete_valorisation_update, ora_connexion)

                                            If (ora_commande_val_update.ExecuteNonQuery() > 0) Then

                                                ch.Valeur = ora_dr_val_regles.Item("VALEUR_LIGNE")

                                            End If

                                        End If

                                    End If

                                    ora_dr_val_test.Close()

                                End If

                            End While

                            ora_dr_val_regles.Close()

                        End If

                        Dim champ_valorise = New Champ(ch.Workflow, ch.IDChamp, ch.NomChamp, ch.Description, ch.Typage, ch.Valeur, ch.Action, ch.AncienneValeur, ch.Requete, ch.Liste, ch.ExpressionReguliere, ch.DetailLigne, ch.Ordre)

                        liste_champ_valorises.Add(champ_valorise)

                    Next

                    resultat.Add(New LigneFacture(ora_dr.Item("ID_FACTURE").ToString(), ora_dr.Item("ID_LIGNE").ToString(), liste_champ_valorises))

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of LigneFacture)
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of LigneFacture)
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function getStatutsFactures(filtre As String) As List(Of ListItem)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of ListItem)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = "select * from statut_facture"

                If (filtre <> "") Then
                    requete += " where filtre = '" & filtre & "'"
                End If

                requete += " order by id_statut_facture "

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    resultat.Add(New ListItem(ora_dr.Item("LIBELLE_STATUT_FACTURE"), ora_dr.Item("ID_STATUT_FACTURE")))

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of ListItem)
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of ListItem)
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function getFichiersAjoutes(user As Utilisateur, docid As String) As List(Of FichierAjoute)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of FichierAjoute)
        Dim fichier_temp As FichierAjoute

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                docid = docid.Replace("'", "''")

                requete = "select af.*,u.nom,u.prenom from ajout_fichier af " &
                    "left join utilisateur u on (af.identifiant=u.identifiant and af.workflow=u.workflow) " &
                    "where af.docid='" & docid & "' and af.workflow='" & user.Workflow & "' " &
                    "order by af.date_ajout"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    fichier_temp = New FichierAjoute()
                    fichier_temp.Workflow = ora_dr.Item("WORKFLOW").ToString()
                    fichier_temp.IDAjout = ora_dr.Item("ID_AJOUT").ToString()
                    fichier_temp.Identifiant = ora_dr.Item("IDENTIFIANT").ToString()
                    fichier_temp.AjoutePar = ora_dr.Item("PRENOM").ToString() & " " & ora_dr.Item("NOM").ToString()
                    fichier_temp.DateAjout = ora_dr.Item("DATE_AJOUT").ToString()
                    fichier_temp.NomFichier = ora_dr.Item("NOM_FICHIER").ToString()
                    fichier_temp.Url = ora_dr.Item("URL").ToString()

                    resultat.Add(fichier_temp)

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of FichierAjoute)
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of FichierAjoute)
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function InsertFichierAjoute(fichier As FichierAjoute) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete As String
        Dim resultat As Boolean = False
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                fichier.Identifiant = fichier.Identifiant.Replace("'", "''")
                fichier.NomFichier = fichier.NomFichier.Replace("'", "''")
                fichier.Url = fichier.Url.Replace("'", "''")
                fichier.DOCID = fichier.DOCID.Replace("'", "''")

                requete = "insert into ajout_fichier(workflow,identifiant,nom_fichier,url,date_ajout,docid) " &
                    "values('" & fichier.Workflow & "','" & fichier.Identifiant & "','" & fichier.NomFichier & "','" & fichier.Url & "',sysdate,'" & fichier.DOCID & "') "

                ora_commande = New OracleCommand(requete, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb = 1) Then
                    resultat = True
                End If

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function DeleteFichierAjoute(fichier As FichierAjoute) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete As String
        Dim resultat As Boolean = False
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = "delete from ajout_fichier " &
                    "where id_ajout='" & fichier.IDAjout & "' and workflow='" & fichier.Workflow & "'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb >= 0) Then
                    resultat = True
                End If

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function getUrlFichierAjoute(user As Utilisateur, id As String) As String

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As String = ""

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = "select url from ajout_fichier where workflow='" & user.Workflow & "' and id_ajout='" & id & "'"
                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                If (ora_dr.Read()) Then

                    resultat = ora_dr.Item("URL").ToString()

                End If

                ora_dr.Close()

            Catch ex As Exception
                resultat = ""
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = ""
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function UpdateFacture(docid As String, user As Utilisateur, liste_champs_concernes As List(Of Champ)) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_trans As OracleTransaction
        Dim requete As String = ""
        Dim resultat As Boolean = False
        Dim nb As Integer
        Dim chaine_corrections As String = ""
        Dim liste_histo_temp As New List(Of Historique)
        Dim action As String = ""
        Dim trouve As Boolean = False


        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            ora_trans = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

            Try
                For Each cc As Champ In liste_champs_concernes

                    'On ne modifie en base que les valeurs qui ont été changées

                    If (cc.AncienneValeur <> cc.Valeur) Then

                        cc.Valeur = cc.Valeur.Replace("'", "''")

                        If (cc.Valeur <> "") Then

                            If (cc.Typage = "DATE") Then
                                cc.Valeur = cc.Valeur.Replace("/", "")
                                chaine_corrections += " " & cc.NomChamp & " = to_date('" & cc.Valeur & "','ddmmyyyy');"
                            Else
                                chaine_corrections += " " & cc.NomChamp & " = '" & cc.Valeur & "';"
                            End If

                        Else
                            chaine_corrections += " " & cc.NomChamp & " = null;"
                        End If

                        'Création de l'historique
                        liste_histo_temp.Add(New Historique(user.Workflow, docid, cc.Action, "", cc.IDChamp, "", user.Identifiant, cc.AncienneValeur, cc.Valeur, "", ""))

                    End If

                    action = cc.Action

                Next

                If (chaine_corrections.Length > 0) Then

                    chaine_corrections = chaine_corrections.Substring(0, chaine_corrections.Length - 1)

                    Dim tab_corrections() As String = chaine_corrections.Split(";")

                    For i As Integer = 0 To tab_corrections.Length - 1

                        If (user.Workflow = "62") Then
                            requete = "update " & user.TableFacture & " set " & tab_corrections(i).ToString() &
                            " where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"
                        Else
                            requete = "update " & user.TableFacture & " set " & tab_corrections(i).ToString() &
                            " where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"
                        End If

                        ora_commande = New OracleCommand(requete, ora_connexion)
                        nb = ora_commande.ExecuteNonQuery()

                    Next



                End If

                'Même si aucun champ n'a été modifié on garde la trace de l'action effectuée
                If (liste_histo_temp.Count = 0) Then
                    liste_histo_temp.Add(New Historique(user.Workflow, docid, action, "", "", "", user.Identifiant, "", "", "", ""))
                End If

                'enregistrement de l'historique
                resultat = InsertHistorique(liste_histo_temp)



                If (resultat = True) Then
                    ora_trans.Commit()
                Else
                    ora_trans.Rollback()
                End If

            Catch ex As Exception
                ora_trans.Rollback()
                resultat = False
                LogsFunctions.LogWrite(ex.ToString(), user)
                LogsFunctions.LogWrite(requete, user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function UpdateStatutFactureInvalide(docid As String, user As Utilisateur, motif As String, commentaire As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_trans As OracleTransaction
        Dim ora_parametre As OracleParameter
        Dim requete, requete_date_traitement As String
        Dim resultat As Boolean = False
        Dim nb As Integer
        Dim liste_histo_action As New List(Of Historique)
        Dim liste_histo As New List(Of Historique)

        Dim flag_correction As Boolean = False
        Dim flag_enrichissement As Boolean = False

        Dim nouveau_statut As String

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            ora_trans = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

            Try

                If (user.IDProfil = "3" And user.GestionValidation = "1") Then

                    'Facture invalide

                    docid = docid.Replace("'", "")
                    motif = motif.Replace("'", "")
                    commentaire = commentaire.Replace("'", "''")

                    requete = "update tb_facture set " &
                        "id_statut_facture=9, " &
                        "flag_occupe=null, " &
                        "id_motif_invalidite='" & motif & "', " &
                        "identifiant_valideur='" & user.Identifiant.Replace("'", "''") & "', " &
                        "commentaire_invalidite='" & commentaire & "' " &
                        "where id_facture='" & docid & "' and id_client='" & user.Workflow & "' " &
                        "returning id_statut_facture INTO :nouveau_statut"

                    'Modification en base
                    ora_parametre = New OracleParameter("nouveau_statut", OracleDbType.Int32)
                    ora_parametre.Direction = ParameterDirection.Output
                    ora_commande = New OracleCommand(requete, ora_connexion)
                    ora_commande.Parameters.Add(ora_parametre)

                    nb = ora_commande.ExecuteNonQuery()

                    If (nb >= 0) Then

                        nouveau_statut = ora_commande.Parameters("nouveau_statut").Value.ToString()

                        If (user.Workflow = "62") Then
                            requete_date_traitement = "update " & user.TableFacture & " set date_retour_valideur=sysdate where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"
                        Else
                            requete_date_traitement = "update " & user.TableFacture & " set date_retour_valideur=sysdate where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"
                        End If


                        ora_commande = New OracleCommand(requete_date_traitement, ora_connexion)
                        ora_commande.ExecuteNonQuery()

                        liste_histo.Add(New Historique(user.Workflow, docid, "RETOUR VALIDEUR KO", "", "", "", user.Identifiant, "", "", "", ""))

                    End If

                    'Ajout en base
                    If (InsertHistorique(liste_histo)) Then

                        resultat = True
                        ora_trans.Commit()

                    Else

                        resultat = False
                        ora_trans.Rollback()

                    End If

                Else

                    resultat = False

                End If

            Catch ex As Exception

                ora_trans.Rollback()
                resultat = False
                LogsFunctions.LogWrite(ex.ToString(), user)

            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat


    End Function

    Public Shared Function UpdateStatutFactureCorrectif(docid As String, user As Utilisateur, motif As String, liste_champs_concernes As List(Of Champ)) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_trans As OracleTransaction
        Dim requete As String
        Dim resultat As Boolean = False
        Dim liste_histo_action As New List(Of Historique)
        Dim liste_histo As New List(Of Historique)

        Dim flag_correction As Boolean = False
        Dim flag_enrichissement As Boolean = False


        For Each cc As Champ In liste_champs_concernes
            If cc.NomChamp = "ID_REJET" Then
                If cc.Valeur = "" Then

                    Try

                        ora_connexion = New OracleConnection(ChaineConnexion)
                        ora_connexion.Open()

                        ora_trans = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

                        Try



                            'Facture invalide

                            docid = docid.Replace("'", "")
                            motif = motif.Replace("'", "")


                            requete = "update tb_facture set " &
                                    "id_statut_facture=2 " &
                                    "where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"

                            'Modification en base
                            ora_commande = New OracleCommand(requete, ora_connexion)
                            ora_commande.ExecuteNonQuery()


                        Catch ex As Exception

                            ora_trans.Rollback()
                            resultat = False
                            LogsFunctions.LogWrite(ex.ToString(), user)

                        End Try

                        ora_connexion.Close()

                    Catch ex As Exception
                        resultat = False
                        LogsFunctions.LogWrite(ex.ToString(), user)
                    End Try

                    Return resultat
                End If
            End If

        Next
        Return resultat
    End Function


    Public Shared Function UpdateStatutFacture(docid As String, user As Utilisateur) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_trans As OracleTransaction
        Dim ora_parametre As OracleParameter
        Dim requete As String = ""
        Dim requete_date_traitement As String = ""
        Dim resultat As Boolean = False
        Dim nb As Integer
        Dim liste_histo_action As New List(Of Historique)
        Dim liste_histo As New List(Of Historique)

        Dim flag_correction As Boolean = False
        Dim flag_enrichissement As Boolean = False

        Dim nouveau_statut As String


        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            ora_trans = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

            Try

                'On cherche à savoir les actions menées afin de déterminer le nouveau statut
                liste_histo_action = getHistorique(user, docid)
                flag_correction = liste_histo_action.FindAll(Function(x) x.Action = "CORRECTION").Count > 0
                flag_enrichissement = liste_histo_action.FindAll(Function(x) x.Action = "ENRICHISSEMENT").Count > 0

                requete = ""

                If (user.IDProfil = "2") Then

                    'Pour l'utilisateur (comptable)

                    If (flag_correction And flag_enrichissement) Then

                        If (user.GestionValidation = "1") Then

                            'Si gestion avec valideur on met le statut "A valider" (8)
                            requete = "update tb_facture set id_statut_facture=8,flag_occupe=null where id_facture='" & docid & "' and id_client='" & user.Workflow & "' " &
                            " returning id_statut_facture INTO :nouveau_statut"

                        Else

                            'Sinon correction + enrichissement = traité
                            requete = "update tb_facture set id_statut_facture=8,flag_occupe=null where id_facture='" & docid & "' and id_client='" & user.Workflow & "' " &
                            " returning id_statut_facture INTO :nouveau_statut"

                        End If


                    ElseIf (flag_correction) Then

                        If (user.GestionValidation = "1") Then

                            'Si gestion avec valideur on met le statut "A valider" (8) sauf s'il reste l'enrichissement à faire (3)
                            requete = "update tb_facture set id_statut_facture=case when(id_statut_facture=4) then 3 else case when(id_statut_facture=2 or id_statut_facture=9) then 9 else id_statut_facture end end, " &
                                "flag_occupe=null where id_facture='" & docid & "' and id_client='" & user.Workflow & "' " &
                           " returning id_statut_facture INTO :nouveau_statut"

                        Else


                            requete = "update tb_facture set id_statut_facture=case when(id_statut_facture=4) then 3 else case when(id_statut_facture=2) then 7 else id_statut_facture end end, " &
                                "flag_occupe=null where id_facture='" & docid & "' and id_client='" & user.Workflow & "' " &
                            " returning id_statut_facture INTO :nouveau_statut"

                        End If


                    ElseIf (flag_enrichissement) Then

                        If (user.GestionValidation = "1") Then

                            'Si gestion avec valideur on met le statut "A valider" (8) sauf s'il reste la correction à faire (2)
                            requete = "update tb_facture set id_statut_facture=case when(id_statut_facture=4) then 2 else case when(id_statut_facture=3 or id_statut_facture=9) then 8 else id_statut_facture end end, " &
                                "flag_occupe=null where id_facture='" & docid & "' and id_client='" & user.Workflow & "' " &
                          " returning id_statut_facture INTO :nouveau_statut"

                        Else

                            requete = "update tb_facture set id_statut_facture=case when(id_statut_facture=4) then 2 else case when(id_statut_facture=3) then 1 else id_statut_facture end end, " &
                                "flag_occupe=null where id_facture='" & docid & "' and id_client='" & user.Workflow & "' " &
                           " returning id_statut_facture INTO :nouveau_statut"

                        End If

                    End If

                ElseIf (user.IDProfil = "3") Then

                    'Pour le valideur

                    If (user.GestionValidation = "1") Then

                        'Facture valide

                        requete = "update tb_facture set id_statut_facture=10,flag_occupe=null where id_facture='" & docid & "' and id_client='" & user.Workflow & "' " &
                          " returning id_statut_facture INTO :nouveau_statut"

                    End If

                End If

                If (requete <> "") Then

                    'Modification en base
                    ora_parametre = New OracleParameter("nouveau_statut", OracleDbType.Int32)
                    ora_parametre.Direction = ParameterDirection.Output
                    ora_commande = New OracleCommand(requete, ora_connexion)
                    ora_commande.Parameters.Add(ora_parametre)

                    nb = ora_commande.ExecuteNonQuery()

                    If (nb >= 0) Then

                        nouveau_statut = ora_commande.Parameters("nouveau_statut").Value.ToString()

                        'Création de l'historique
                        If (nouveau_statut = "1") Then

                            If (user.Workflow = "62") Then
                                requete_date_traitement = "update " & user.TableFacture & " set date_traitement_workflow=sysdate where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"
                            Else
                                requete_date_traitement = "update " & user.TableFacture & " set date_traitement_workflow=sysdate where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"
                            End If

                            ora_commande = New OracleCommand(requete_date_traitement, ora_connexion)
                            ora_commande.ExecuteNonQuery()

                            liste_histo.Add(New Historique(user.Workflow, docid, "ENREGISTREMENT TOTAL", "", "", "", user.Identifiant, "", "", "", ""))

                        ElseIf (nouveau_statut = "8") Then

                            'Envoyé au valideur

                            If (user.Workflow = "62") Then
                                requete_date_traitement = "update " & user.TableFacture & " set date_envoi_valideur=sysdate where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"
                            Else
                                requete_date_traitement = "update " & user.TableFacture & " set date_envoi_valideur=sysdate where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"
                            End If


                            ora_commande = New OracleCommand(requete_date_traitement, ora_connexion)
                            ora_commande.ExecuteNonQuery()

                            liste_histo.Add(New Historique(user.Workflow, docid, "ENVOYE AU VALIDEUR", "", "", "", user.Identifiant, "", "", "", ""))

                            Try

                                'On envoi un mail au(x) valideur(s)
                                If (user.AlerteMailValideur = "1" And user.GestionValidation = "1" And user.IDProfil = "2") Then

                                    Dim liste_emails_valideurs As List(Of String) = getEmailsValideurs(user)

                                    If (liste_emails_valideurs.Count > 0 And user.Email.Trim() <> "") Then

                                        Dim destinataires As String = ""

                                        For Each email In liste_emails_valideurs
                                            destinataires = email & ","
                                        Next

                                        destinataires = destinataires.Substring(0, destinataires.Length - 1)

                                        Dim sujet As String = "Nouvelle facture à valider"

                                        Dim message As String = getMessageValideur(user, docid)

                                        If (message <> "") Then
                                            MailFunctions.Envoyer(user.Email, destinataires, sujet, message, Nothing)
                                        End If


                                    End If

                                End If

                            Catch ex As Exception
                                LogsFunctions.LogWrite(ex.ToString(), user)
                            End Try


                        ElseIf (nouveau_statut = "10") Then

                            'Facture validée

                            If (user.Workflow = "62") Then
                                requete_date_traitement = "update " & user.TableFacture & " set date_retour_valideur=sysdate,date_traitement_workflow=sysdate,identifiant_valideur='" & user.Identifiant.Replace("'", "''") & "' where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"
                            Else
                                requete_date_traitement = "update " & user.TableFacture & " set date_retour_valideur=sysdate,date_traitement_workflow=sysdate,identifiant_valideur='" & user.Identifiant.Replace("'", "''") & "' where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"
                            End If

                            ora_commande = New OracleCommand(requete_date_traitement, ora_connexion)
                            ora_commande.ExecuteNonQuery()

                            liste_histo.Add(New Historique(user.Workflow, docid, "RETOUR VALIDEUR OK", "", "", "", user.Identifiant, "", "", "", ""))

                        ElseIf (nouveau_statut = "2") Then

                            liste_histo.Add(New Historique(user.Workflow, docid, "ENREGISTREMENT ENRICHISSEMENT", "", "", "", user.Identifiant, "", "", "", ""))

                        ElseIf (nouveau_statut = "3") Then

                            liste_histo.Add(New Historique(user.Workflow, docid, "ENREGISTREMENT CORRECTION", "", "", "", user.Identifiant, "", "", "", ""))


                        ElseIf (nouveau_statut = "7") Then

                            liste_histo.Add(New Historique(user.Workflow, docid, "VALIDATION FACTURE", "", "", "", user.Identifiant, "", "", "", ""))

                        End If

                        'Ajout en base
                        If (InsertHistorique(liste_histo)) Then

                            resultat = True
                            ora_trans.Commit()

                        Else

                            resultat = False
                            ora_trans.Rollback()

                        End If

                    End If

                Else

                    resultat = True

                End If

            Catch ex As Exception

                ora_trans.Rollback()
                resultat = False
                LogsFunctions.LogWrite(ex.ToString(), user)
                LogsFunctions.LogWrite(requete, user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function InsertHistorique(liste_histo As List(Of Historique)) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete As String
        Dim resultat As Boolean = False
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                For Each histo As Historique In liste_histo

                    histo.identifiant = histo.identifiant.Replace("'", "''")
                    histo.AncienneValeur = histo.AncienneValeur.Replace("'", "''")
                    histo.NouvelleValeur = histo.NouvelleValeur.Replace("'", "''")

                    requete = "insert into historique(workflow,docid,action,date_action,id_champ,id_alerte,identifiant,ancienne_valeur,nouvelle_valeur) " &
                        "values('" & histo.Workflow & "','" & histo.DocId & "','" & histo.Action & "',sysdate,'" & histo.IdChamp & "','" & histo.IdAlerte & "','" & histo.identifiant & "','" & histo.AncienneValeur & "','" & histo.NouvelleValeur & "')"

                    ora_commande = New OracleCommand(requete, ora_connexion)
                    nb = ora_commande.ExecuteNonQuery()

                    resultat = True

                Next

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function getHistorique(user As Utilisateur, docid As String) As List(Of Historique)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of Historique)
        Dim histo_temp As Historique

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                docid = docid.Replace("'", "''")

                requete = "select h.*,cw.description,u.nom,u.prenom from historique h " &
                    "left join champ_workflow cw on (h.workflow=cw.workflow and h.id_champ=cw.id_champ) " &
                    "left join utilisateur u on (h.workflow=u.workflow and h.identifiant=u.identifiant) " &
                    "where h.docid='" & docid & "' and h.workflow='" & user.Workflow & "' " &
                    "order by h.date_action"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    histo_temp = New Historique()
                    histo_temp.Workflow = ora_dr.Item("WORKFLOW").ToString()
                    histo_temp.DocId = ora_dr.Item("DOCID").ToString()
                    histo_temp.Action = ora_dr.Item("ACTION").ToString()
                    histo_temp.DateAction = ora_dr.Item("DATE_ACTION").ToString()
                    histo_temp.IdChamp = ora_dr.Item("ID_CHAMP").ToString()
                    histo_temp.IdAlerte = ""
                    histo_temp.identifiant = ora_dr.Item("IDENTIFIANT").ToString()
                    histo_temp.AncienneValeur = ora_dr.Item("ANCIENNE_VALEUR").ToString()
                    histo_temp.NouvelleValeur = ora_dr.Item("NOUVELLE_VALEUR").ToString()
                    histo_temp.NomUtilisateur = ora_dr.Item("PRENOM").ToString() & " " & ora_dr.Item("NOM").ToString()
                    histo_temp.LibelleChamp = ora_dr.Item("DESCRIPTION").ToString()

                    resultat.Add(histo_temp)

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of Historique)
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Historique)
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function getMotifsAlerte(ByVal pUser As Utilisateur) As List(Of ListItem)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of ListItem)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = $"select * from motif_alertes where id_client='{pUser.Workflow}' order by ordre"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    resultat.Add(New ListItem(ora_dr.Item("LIBELLE_MOTIF_ALERTE").ToString(), ora_dr.Item("ID_MOTIF_ALERTE").ToString()))

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of ListItem)
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of ListItem)
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function InsertAlerteHorsScope(alerte As AlerteFacture, destinataires As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande, ora_commande_bis As OracleCommand
        Dim ora_dr_bis As OracleDataReader
        Dim ora_trans As OracleTransaction
        Dim requete, requete_bis As String
        Dim resultat As Boolean = False
        Dim nb As Integer
        Dim liste_destinataires As List(Of String)
        Dim message, sujet As String

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            ora_trans = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

            Try

                alerte.Commentaire = alerte.Commentaire.Replace("'", "''")
                destinataires = destinataires.Replace(" ", "").Replace(",,", ",").Replace("'", "")
                liste_destinataires = New List(Of String)(destinataires.Split(","c))

                requete = "insert into alerte_hors_scope(workflow,identifiant,id_statut_alerte,date_alerte,commentaire,docid,id_motif_alerte,destinataires) " &
                    "values('" & alerte.Workflow & "','" & alerte.Emetteur.Identifiant & "','" & alerte.Statut.Id & "',sysdate,'" & alerte.Commentaire & "','" & alerte.DocId & "','" & alerte.Motif.Id & "','" & destinataires & "') "

                ora_commande = New OracleCommand(requete, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb = 1) Then

                    For Each email_dest As String In liste_destinataires

                        If (email_dest.Trim() <> "") Then

                            'recherche du libellé du motif d'alerte
                            requete_bis = "select libelle_motif_alerte from motif_alertes where id_motif_alerte='" & alerte.Motif.Id & "'"
                            ora_commande_bis = New OracleCommand(requete_bis, ora_connexion)
                            ora_dr_bis = ora_commande_bis.ExecuteReader()

                            If (ora_dr_bis.Read()) Then
                                alerte.Motif.Libelle = ora_dr_bis.Item("LIBELLE_MOTIF_ALERTE").ToString()
                            End If

                            'ora_dr_bis.Close()

                            '///// Supprimé le 07/08/2017
                            'recherche du destinataire dans la liste des utilisateur
                            'requete_bis = "select identifiant from utilisateur where email='" & email_dest & "'"
                            'ora_commande_bis = New OracleCommand(requete_bis, ora_connexion)
                            'ora_dr_bis = ora_commande_bis.ExecuteReader()

                            'If (Not ora_dr_bis.Read()) Then

                            'Envoi d'un mail aux destinataires non enregitrés
                            '/////

                            sujet = "Alerte Hors Scope " & alerte.DocId & " du " & Date.Now.ToShortDateString()
                            message = "<html>Motif : " & alerte.Motif.Libelle & "<br/><br/>Commentaire : " & alerte.Commentaire & "<br/><br/>Id du document : " & alerte.DocId & "</html>"

                            MailFunctions.Envoyer(alerte.Emetteur.Email, email_dest, sujet, message, Nothing)

                            '/////End If

                            'ora_dr_bis.Close() /////

                        End If

                    Next

                    ora_trans.Commit()

                Else

                    ora_trans.Rollback()

                End If

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function InsertAlerteFacture(alerte As AlerteFacture, destinataires As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande, ora_commande_bis As OracleCommand
        Dim ora_dr_bis As OracleDataReader
        Dim ora_trans As OracleTransaction
        Dim requete, requete_bis As String
        Dim resultat As Boolean = False
        Dim nb As Integer
        Dim liste_destinataires As List(Of String)
        Dim message, sujet As String

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            ora_trans = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

            Try

                alerte.Commentaire = alerte.Commentaire.Replace("'", "''")
                destinataires = destinataires.Replace(" ", "").Replace(",,", ",").Replace("'", "")
                liste_destinataires = New List(Of String)(destinataires.Split(","c))

                requete = "insert into alerte_facture(workflow,identifiant,id_statut_alerte,date_alerte,commentaire,docid,id_motif_alerte,destinataires) " &
                    "values('" & alerte.Workflow & "','" & alerte.Emetteur.Identifiant & "','" & alerte.Statut.Id & "',sysdate,'" & alerte.Commentaire & "','" & alerte.DocId & "','" & alerte.Motif.Id & "','" & destinataires & "') "

                ora_commande = New OracleCommand(requete, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb = 1) Then

                    For Each email_dest As String In liste_destinataires

                        If (email_dest.Trim() <> "") Then

                            'recherche du libellé du motif d'alerte
                            requete_bis = "select libelle_motif_alerte from motif_alerte where id_motif_alerte='" & alerte.Motif.Id & "'"
                            ora_commande_bis = New OracleCommand(requete_bis, ora_connexion)
                            ora_dr_bis = ora_commande_bis.ExecuteReader()

                            If (ora_dr_bis.Read()) Then
                                alerte.Motif.Libelle = ora_dr_bis.Item("LIBELLE_MOTIF_ALERTE").ToString()
                            End If

                            ora_dr_bis.Close()

                            '///// Supprimé le 07/08/2017
                            'recherche du destinataire dans la liste des utilisateur
                            'requete_bis = "select identifiant from utilisateur where email='" & email_dest & "'"
                            'ora_commande_bis = New OracleCommand(requete_bis, ora_connexion)
                            'ora_dr_bis = ora_commande_bis.ExecuteReader()

                            'If (Not ora_dr_bis.Read()) Then

                            'Envoi d'un mail aux destinataires non enregitrés
                            '/////

                            sujet = "Alerte facture " & alerte.NumFacture & " du " & alerte.DateFacture
                            message = "<html>Motif : " & alerte.Motif.Libelle & "<br/><br/>Commentaire : " & alerte.Commentaire & "<br/><br/>Id du document : " & alerte.DocId & "</html>"

                            MailFunctions.Envoyer(alerte.Emetteur.Email, email_dest, sujet, message, Nothing)

                            '/////End If

                            'ora_dr_bis.Close() /////

                        End If

                    Next

                    ora_trans.Commit()

                Else

                    ora_trans.Rollback()

                End If

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function getStatutDocument(user As Utilisateur, docid As String) As String

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As String = ""

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = "select STATUT from TB_DOCUMENT_HORS_SCOPE where id_client='" & user.Workflow & "' and ID_DOCUMENT='" & docid & "'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                If (ora_dr.Read()) Then

                    resultat = ora_dr.Item("STATUT").ToString()

                End If

                ora_dr.Close()

            Catch ex As Exception
                resultat = ""
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = ""
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function getNom_Fichier(user As Utilisateur, docid As String) As String

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As String = ""

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = "select * from " & user.TableFacture & " where id_client='" & user.Workflow & "' and id_facture='" & docid & "'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                If (ora_dr.Read()) Then

                    resultat = ora_dr.Item("NOM_FOURNISSEUR").ToString() + "_" + ora_dr.Item("CODE_FOURNISSEUR").ToString() + "_" + ora_dr.Item("NUM_COMMANDE").ToString()

                End If

                ora_dr.Close()

            Catch ex As Exception
                resultat = ""
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = ""
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function getStatutFacture(user As Utilisateur, docid As String) As String

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As String = ""

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = "select id_statut_facture from " & user.TableFacture & " where id_client='" & user.Workflow & "' and id_facture='" & docid & "'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                If (ora_dr.Read()) Then

                    resultat = ora_dr.Item("ID_STATUT_FACTURE").ToString()

                End If

                ora_dr.Close()

            Catch ex As Exception
                resultat = ""
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = ""
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function getMotifsDemande() As List(Of ListItem)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of ListItem)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = "select * from motif_demande order by ordre"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    resultat.Add(New ListItem(ora_dr.Item("LIBELLE_MOTIF_DEMANDE").ToString(), ora_dr.Item("ID_MOTIF_DEMANDE").ToString()))

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of ListItem)
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of ListItem)
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function UpdateInfosGenerales(ByRef user As Utilisateur, civilite As String, nom As String, prenom As String, email As String, fonction As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete As String
        Dim resultat As Boolean = False
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                Dim identifiant As String

                identifiant = user.Identifiant.Replace("'", "''")
                civilite = civilite.Replace("'", "")
                nom = nom.Replace("'", "''")
                prenom = prenom.Replace("'", "''")
                email = email.Replace("'", "''")
                fonction = fonction.Replace("'", "''")


                If (user.Workflow = "65") Then
                    DBFunctions.UpdateInvoiceByComptable(user, email)
                End If

                requete = "update utilisateur set " &
                    "id_civilite='" & civilite & "', " &
                    "nom='" & nom & "', " &
                    "prenom='" & prenom & "', " &
                    "email='" & email & "', " &
                    "libelle_fonction='" & fonction & "' " &
                    "where identifiant='" & identifiant & "'"
                ora_commande = New OracleCommand(requete, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb >= 0) Then

                    resultat = True

                    user.Civilite.Id = civilite
                    user.Nom = nom
                    user.Prenom = prenom
                    user.Email = email
                    user.Fonction = fonction

                End If

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function InsertDemandeAdministrateur(p_demande As Demande) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete As String
        Dim resultat As Boolean = False
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                p_demande.Commentaire = p_demande.Commentaire.Replace("'", "''")
                p_demande.Identifiant = p_demande.Identifiant.Replace("'", "''")

                requete = "insert into demande(workflow,identifiant,date_demande,id_motif_demande,commentaire,id_statut_demande) " &
                    "values('" & p_demande.Workflow & "','" & p_demande.Identifiant & "',sysdate,'" & p_demande.Motif.Id & "','" & p_demande.Commentaire & "','1') "

                ora_commande = New OracleCommand(requete, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb = 1) Then

                    resultat = True

                End If

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function basculeStatut(docid As String, user As Utilisateur, statut_bascule As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_trans As OracleTransaction
        Dim requete As String
        Dim resultat As Boolean = False
        Dim nb As Integer
        Dim liste_histo As New List(Of Historique)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            ora_trans = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

            Try

                statut_bascule = statut_bascule.Replace("'", "")
                docid = docid.Replace("'", "")

                If (user.Workflow = "62") Then

                    requete = "update " & user.TableFacture & " " &
                    " set date_traitement_workflow=null, id_statut_facture='" & statut_bascule & "' " &
                    " where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"
                Else

                    requete = "update " & user.TableFacture &
                    " set date_traitement_workflow=null, id_statut_facture='" & statut_bascule & "' " &
                    " where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"

                End If




                ora_commande = New OracleCommand(requete, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb = 1) Then

                    'Création de l'historique
                    liste_histo.Add(New Historique(user.Workflow, docid, "BASCULE", "", "", "", user.Identifiant, "", "", "", ""))

                    'Ajout en base
                    If (InsertHistorique(liste_histo)) Then

                        resultat = True
                        ora_trans.Commit()

                    Else

                        resultat = False
                        ora_trans.Rollback()

                    End If

                    resultat = True

                End If

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function getIndicateurProductivite(date_debut As String, date_fin As String, identifiant As String, workflow As String) As List(Of Resultat)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim result As New List(Of Resultat)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                identifiant = identifiant.Replace("'", "''")
                workflow = workflow.Replace("'", "")
                date_debut = date_debut.Replace("'", "").Trim()
                date_fin = date_fin.Replace("'", "").Trim()

                requete = "select to_date(to_char(date_action,'dd/mm/yyyy'),'dd/mm/yyyy') as date_action, " &
                    "count(*) as nb_factures_traitees " &
                    "from historique " &
                    "where action in ('VALIDATION TOTALE','ENREGISTREMENT TOTAL') and workflow='" & workflow & "' "

                If (identifiant <> "") Then
                    requete += " and identifiant='" & identifiant & "' "
                End If

                If (date_debut <> "") Then
                    requete += " and date_action>=to_date('" & date_debut & "','dd/mm/yyyy') "
                End If

                If (date_fin <> "") Then
                    requete += " and date_action<to_date('" & date_fin & "','dd/mm/yyyy') + 1 "
                End If

                requete += "group by to_date(to_char(date_action,'dd/mm/yyyy'),'dd/mm/yyyy')" &
                    "order by to_date(to_char(date_action,'dd/mm/yyyy'),'dd/mm/yyyy') "

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    result.Add(New Resultat(ora_dr.Item("DATE_ACTION").ToString().Substring(0, 10), Convert.ToDouble(ora_dr.Item("NB_FACTURES_TRAITEES").ToString()), 0, 0))

                End While

                ora_dr.Close()

            Catch ex As Exception
                result = New List(Of Resultat)
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            result = New List(Of Resultat)
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return result

    End Function

    Public Shared Function getIndicateurRepartitionStatut(user As Utilisateur, date_debut As String, date_fin As String) As List(Of Resultat)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim result As New List(Of Resultat)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                date_debut = date_debut.Replace("'", "").Trim()
                date_fin = date_fin.Replace("'", "").Trim()

                requete = "select to_date(to_char(date_insertion,'dd/mm/yyyy'),'dd/mm/yyyy') as date_insertion, " &
                    "sum(case when id_statut_facture in ('2','3','4') then 1 else 0 end) as nb_factures_a_traiter, " &
                    "sum(case when id_statut_facture in ('1','5','6') then 1 else 0 end) as nb_factures_traitees, " &
                    "sum(case when id_statut_facture in ('0') then 1 else 0 end) as nb_factures_sans_action " &
                    "from " & user.TableFacture & " " &
                    "where id_client='" & user.Workflow & "' "

                If (date_debut <> "") Then
                    requete += " and date_insertion>=to_date('" & date_debut & "','dd/mm/yyyy') "
                End If

                If (date_fin <> "") Then
                    requete += " and date_insertion<to_date('" & date_fin & "','dd/mm/yyyy') + 1 "
                End If

                requete += "group by to_date(to_char(date_insertion,'dd/mm/yyyy'),'dd/mm/yyyy') " &
                "order by to_date(to_char(date_insertion,'dd/mm/yyyy'),'dd/mm/yyyy')"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    result.Add(New Resultat(ora_dr.Item("DATE_INSERTION").ToString().Substring(0, 10), Convert.ToDouble(ora_dr.Item("NB_FACTURES_TRAITEES").ToString()), Convert.ToDouble(ora_dr.Item("NB_FACTURES_A_TRAITER").ToString()), Convert.ToDouble(ora_dr.Item("NB_FACTURES_SANS_ACTION").ToString())))

                End While

                ora_dr.Close()

            Catch ex As Exception
                result = New List(Of Resultat)
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            result = New List(Of Resultat)
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return result

    End Function

    Public Shared Function setFlagOccupe(docid As String, valeur As String, user As Utilisateur) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete_message As String = ""
        Dim resultat As Boolean
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            resultat = False
            docid = docid.Replace("'", "''").Trim()

            Try

                'If (user.Workflow = "62") Then

                '    If (valeur.Trim() = "") Then
                '        requete_message = "update " & user.TableFacture & "_UPDATE set flag_occupe=null " &
                '       "where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"

                '    Else
                '        requete_message = "update " & user.TableFacture & "_UPDATE set flag_occupe='" & valeur & "' " &
                '       "where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"

                '    End If

                'Else

                If (valeur.Trim() = "") Then
                    requete_message = "update " & user.TableFacture & " set flag_occupe=null " &
                       "where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"

                Else
                    requete_message = "update " & user.TableFacture & " set flag_occupe='" & valeur & "' " &
                       "where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"

                End If

                'End If



                ora_commande = New OracleCommand(requete_message, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb > 0) Then

                    resultat = True

                End If

            Catch ex As Exception
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), user)
                LogsFunctions.LogWrite(requete_message, user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function getFlagOccupe(docid As String, user As Utilisateur) As String

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As String = "0"

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            resultat = False
            docid = docid.Replace("'", "''").Trim()

            Try
                requete = "select flag_occupe from " & user.TableFacture &
                    " where id_facture='" & docid & "' and id_client='" & user.Workflow & "'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                If (ora_dr.Read()) Then

                    If (ora_dr.Item("FLAG_OCCUPE").ToString() = "1") Then
                        resultat = "1"
                    Else
                        resultat = "0"
                    End If

                End If

                ora_dr.Close()

            Catch ex As Exception
                resultat = "0"
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = "0"
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function UpdateStatutAlerteFacture(current_user As Utilisateur, id_alerte As String, Optional ByVal hs As Boolean = False) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete As String
        Dim resultat As Boolean
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            resultat = False
            id_alerte = id_alerte.Replace("'", "").Trim()

            Try
                requete = $"update {IIf(hs, "alerte_hors_scope", "alerte_facture")} set " &
                    " id_statut_alerte=case when(id_statut_alerte=1) then 2 else 1 end, " &
                    " identifiant_resolution=case when(id_statut_alerte=1) then '" & current_user.Identifiant & "' else null end, " &
                    " date_resolution=case when(id_statut_alerte=1) then sysdate else null end " &
                    "where id_alerte='" & id_alerte & "' and workflow='" & current_user.Workflow & "'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If (nb > 0) Then

                    resultat = True

                End If

            Catch ex As Exception
                resultat = False
                ora_connexion.Close()
                LogsFunctions.LogWrite(ex.ToString(), current_user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), current_user)
        End Try

        Return resultat

    End Function

    Public Shared Function getListeRejets(current_user As Utilisateur) As List(Of ListItem)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of ListItem)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = "select id_rejet,libelle_rejet from tb_rejet where id_client='" & current_user.Workflow & "' and invisible is null order by libelle_rejet"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    resultat.Add(New ListItem(ora_dr.Item("LIBELLE_REJET"), ora_dr.Item("ID_REJET")))

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of ListItem)
                LogsFunctions.LogWrite(ex.ToString(), current_user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of ListItem)
            LogsFunctions.LogWrite(ex.ToString(), current_user)
        End Try

        Return resultat

    End Function

    Public Shared Function getStatutsAlerte() As List(Of ListItem)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of ListItem)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = "select id_statut_alerte,libelle_statut_alerte from statut_alerte order by id_statut_alerte"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    resultat.Add(New ListItem(ora_dr.Item("libelle_statut_alerte"), ora_dr.Item("id_statut_alerte")))

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of ListItem)
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of ListItem)
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function getPeriodeArchive(p_id_client As String) As List(Of ListItem)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As New List(Of ListItem)
        Dim periode As String
        Dim date_temp As Date
        Dim mois, annee As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                p_id_client = p_id_client.Replace("'", "")

                requete = "select distinct archive_annee,archive_mois " &
                    "from tb_facture " &
                    "where id_client='" & p_id_client & "' and archive_annee is not null and archive_mois is not null " &
                    "order by archive_annee desc,archive_mois desc"


                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    mois = Convert.ToInt32(ora_dr.Item("archive_mois").ToString())

                    annee = Convert.ToInt32(ora_dr.Item("archive_annee").ToString())

                    date_temp = New Date(annee, mois, 1)

                    periode = date_temp.ToString("MMMM yyyy")
                    periode = periode.Substring(0, 1).ToUpper() & periode.Substring(1).ToLower()

                    resultat.Add(New ListItem(periode, annee.ToString() & "_" & mois.ToString()))

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of ListItem)
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of ListItem)
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat

    End Function

    Public Shared Function getMotifsInvalidite(p_utilisateur As Utilisateur) As List(Of Motif)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String = ""
        Dim resultat As New List(Of Motif)

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = "select id_motif_invalidite,libelle_motif_invalidite from motif_invalidite where workflow='" & p_utilisateur.Workflow & "'"
                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())

                    resultat.Add(New Motif(ora_dr.Item("id_motif_invalidite").ToString(), ora_dr.Item("libelle_motif_invalidite").ToString(), p_utilisateur.Workflow))

                End While

                ora_dr.Close()

            Catch ex As Exception
                resultat = New List(Of Motif)
                LogsFunctions.LogWrite(ex.ToString(), p_utilisateur)
                LogsFunctions.LogWrite(requete, p_utilisateur)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Motif)
            LogsFunctions.LogWrite(ex.ToString(), p_utilisateur)
        End Try

        Return resultat

    End Function

    Public Shared Function UpdateLigneFacture(p_user As Utilisateur, p_id_facture As String, p_id_ligne As String, p_liste_champs As List(Of Champ)) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete As String = ""
        Dim resultat As Boolean = False
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                Dim parametres As String = ""

                p_id_ligne = p_id_ligne.Replace("'", "")
                p_id_facture = p_id_facture.Replace("'", "")

                For Each ch As Champ In p_liste_champs

                    If (ch.Typage = "DATE") Then
                        parametres = ch.NomChamp & "= to_date('" & ch.Valeur & "','dd/mm/yyyy') "
                    Else
                        parametres = ch.NomChamp & "='" & ch.Valeur & "' "
                    End If

                    requete = "update " & p_user.TableLigneFacture & " set " & parametres & " where id_ligne='" & p_id_ligne & "' and id_client='" & p_user.Workflow & "'"
                    ora_commande = New OracleCommand(requete, ora_connexion)

                    nb = ora_commande.ExecuteNonQuery()

                Next

                resultat = True

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString(), p_user)
                LogsFunctions.LogWrite(requete, p_user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), p_user)
        End Try

        Return resultat

    End Function

    Public Shared Function DeleteLigneFacture(p_user As Utilisateur, p_id_facture As String, p_id_ligne As String) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete As String
        Dim resultat As Boolean = False
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                p_id_ligne = p_id_ligne.Replace("'", "")
                p_id_facture = p_id_facture.Replace("'", "")

                requete = "delete from " & p_user.TableLigneFacture & " where id_ligne='" & p_id_ligne & "' and id_facture='" & p_id_facture & "' and id_client='" & p_user.Workflow & "'"
                ora_commande = New OracleCommand(requete, ora_connexion)

                nb = ora_commande.ExecuteNonQuery()

                If (nb >= 0) Then

                    resultat = True

                    'historisation de la suppression de ligne

                    Dim liste_hiso As New List(Of Historique)

                    liste_hiso.Add(New Historique(p_user.Workflow, p_id_facture, "SUPPRESSION LIGNE FACTURE", "", "", "", p_user.Identifiant, "", "", "", ""))

                    InsertHistorique(liste_hiso)

                End If

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString(), p_user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), p_user)
        End Try

        Return resultat

    End Function

    Public Shared Function InsertLigneFacture(p_user As Utilisateur, p_id_facture As String) As String

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_commande_bis As OracleCommand
        Dim ora_commande_info As OracleCommand
        Dim ora_dr_info As OracleDataReader
        Dim requete, requete_info, requete_bis As String
        Dim resultat As String = ""
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                p_id_facture = p_id_facture.Replace("'", "")

                requete_info = "select ID_FACTURE,ID_LOT,ID_CLIENT from " & p_user.TableFacture & " where ID_CLIENT='" & p_user.Workflow & "' and ID_FACTURE='" & p_id_facture & "' "
                ora_commande_info = New OracleCommand(requete_info, ora_connexion)
                ora_dr_info = ora_commande_info.ExecuteReader()

                If (ora_dr_info.Read()) Then

                    requete = "insert into " & p_user.TableLigneFacture & " (ID_FACTURE,ID_LOT,ID_CLIENT) " &
                    "values ('" & ora_dr_info.Item("ID_FACTURE").ToString() & "','" & ora_dr_info.Item("ID_LOT").ToString() & "','" & ora_dr_info.Item("ID_CLIENT").ToString() & "') " &
                    "returning id_ligne INTO :new_id_ligne"

                    Dim ora_parametre As New OracleParameter("new_id_ligne", OracleDbType.Int32)

                    ora_parametre.Direction = ParameterDirection.Output

                    ora_commande = New OracleCommand(requete, ora_connexion)
                    ora_commande.Parameters.Add(ora_parametre)

                    nb = ora_commande.ExecuteNonQuery()

                    If (nb = 1) Then

                        Dim liste_champs_ligne As List(Of Champ) = getChamps(p_user, "", "1")
                        Dim id_ligne As String = ora_commande.Parameters("new_id_ligne").Value.ToString()

                        If (p_user.Workflow = "62") Then

                            requete_bis = "insert into TB_LIGNE_FACTURE_RGDS(ID_FACTURE,ID_LIGNE) " &
                            "values ('" & ora_dr_info.Item("ID_FACTURE").ToString() & "','" & id_ligne & "')"
                            ora_commande_bis = New OracleCommand(requete_bis, ora_connexion)
                            ora_commande_bis.ExecuteNonQuery()

                        End If


                        resultat = "<tr id=""index-" & id_ligne & """ class=""odd gradeX"">"

                        For Each ch_val As Champ In liste_champs_ligne

                            resultat += "<td><input type=""text"" id=""txtChampLigne_" & id_ligne & "_" & ch_val.IDChamp & """ value=""" & ch_val.Valeur & """ class=""form-control champ-ligne"" data-ligne=""" & id_ligne & """ data-champ=""" & ch_val.IDChamp & """ /></td>"

                        Next

                        resultat += "<td><button type=""button"" id=""btnSupprimerLigne_" & id_ligne & """ data-ligne=""" & id_ligne & """ class=""btn btn-sm btn-danger supp-ligne"" data-toggle=""modal"" data-target=""#modal-confirm-supp-ligne"">Supprimer</button></td></tr>"

                        'historisation de l'ajout de ligne

                        Dim liste_hiso As New List(Of Historique)

                        liste_hiso.Add(New Historique(p_user.Workflow, p_id_facture, "AJOUT LIGNE FACTURE", "", "", "", p_user.Identifiant, "", "", "", ""))

                        InsertHistorique(liste_hiso)

                    End If

                End If

                ora_dr_info.Close()

            Catch ex As Exception
                resultat = ""
                LogsFunctions.LogWrite(ex.ToString(), p_user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = ""
            LogsFunctions.LogWrite(ex.ToString(), p_user)
        End Try

        Return resultat

    End Function

    Public Shared Function GetCommentaireValideur(p_user As Utilisateur, p_id_facture As String) As CommentaireValideur

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As CommentaireValideur = Nothing

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                p_id_facture = p_id_facture.Replace("'", "")

                requete = "select f.id_facture,f.date_retour_valideur,f.commentaire_invalidite,f.identifiant_valideur,mi.libelle_motif_invalidite,(u.prenom || ' ' || u.nom) as nom_valideur " &
                    "from " & p_user.TableFacture & " f " &
                    "inner join motif_invalidite mi on (f.id_motif_invalidite=mi.id_motif_invalidite and mi.workflow='" & p_user.Workflow & "') " &
                    "left join utilisateur u on (f.identifiant_valideur=u.identifiant) " &
                    "where id_facture='" & p_id_facture & "' and id_client='" & p_user.Workflow & "' " &
                    "and mi.libelle_motif_invalidite is not null and identifiant_valideur is not null and date_retour_valideur is not null"
                ora_commande = New OracleCommand(requete, ora_connexion)

                ora_dr = ora_commande.ExecuteReader()

                If (ora_dr.Read()) Then

                    resultat = New CommentaireValideur(ora_dr.Item("id_facture").ToString(), ora_dr.Item("date_retour_valideur").ToString(), ora_dr.Item("identifiant_valideur").ToString(), ora_dr.Item("nom_valideur").ToString(), ora_dr.Item("commentaire_invalidite").ToString(), ora_dr.Item("libelle_motif_invalidite").ToString())

                End If

                ora_dr.Close()

            Catch ex As Exception
                resultat = Nothing
                LogsFunctions.LogWrite(ex.ToString(), p_user)

            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = Nothing
            LogsFunctions.LogWrite(ex.ToString(), p_user)
        End Try

        Return resultat

    End Function

    Public Shared Function GetNbTachesARealiser(p_user As Utilisateur) As TachesARealiser

        Dim resultat As New TachesARealiser

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete_validation, requete_a_traiter As String

        Try

            ora_connexion = New OracleConnection(_chaineConnexion)
            ora_connexion.Open()


            Try

                Dim param_statuts_validation As String = ""
                Dim param_statuts_a_traiter As String = ""

                If (p_user.GestionValidation = "1") Then

                    If (p_user.IDProfil = "2") Then

                        param_statuts_validation = "9"
                        param_statuts_a_traiter = "2,3,4"

                    ElseIf (p_user.IDProfil = "3") Then

                        param_statuts_validation = "8"
                        param_statuts_a_traiter = ""

                    End If

                    requete_validation = "select count(*) from tb_facture where id_statut_facture in (" & param_statuts_validation & ") and id_rejet is null and (flag_occupe is null or flag_occupe<>1) and id_client='" & p_user.Workflow & "'"
                    ora_commande = New OracleCommand(requete_validation, ora_connexion)
                    ora_dr = ora_commande.ExecuteReader()

                    If (ora_dr.Read()) Then

                        If (ora_dr.Item(0).ToString().Trim() <> "") Then

                            resultat.NbCorbeilleValidation = Convert.ToInt32(ora_dr.Item(0).ToString())

                        End If

                    End If

                    ora_dr.Close()

                    If (p_user.IDProfil = "2") Then

                        requete_a_traiter = "select count(*) from tb_facture where id_statut_facture in (" & param_statuts_a_traiter & ") and id_rejet is null and (flag_occupe is null or flag_occupe<>1) and id_client='" & p_user.Workflow & "'"
                        ora_commande = New OracleCommand(requete_a_traiter, ora_connexion)
                        ora_dr = ora_commande.ExecuteReader()

                        If (ora_dr.Read()) Then

                            If (ora_dr.Item(0).ToString().Trim() <> "") Then

                                resultat.NbCorbeilleATraiter = Convert.ToInt32(ora_dr.Item(0).ToString())

                            End If

                        End If

                        ora_dr.Close()

                    End If

                Else

                    If (p_user.IDProfil = "2") Then

                        param_statuts_validation = ""
                        param_statuts_a_traiter = "2,3,4"

                        requete_a_traiter = "select count(*) from " & p_user.TableFacture & " where id_statut_facture in (" & param_statuts_a_traiter & ") and id_rejet is null and (flag_occupe is null or flag_occupe<>1)"
                        ora_commande = New OracleCommand(requete_a_traiter, ora_connexion)
                        ora_dr = ora_commande.ExecuteReader()

                        If (ora_dr.Read()) Then

                            If (ora_dr.Item(0).ToString().Trim() <> "") Then

                                resultat.NbCorbeilleATraiter = Convert.ToInt32(ora_dr.Item(0).ToString())

                            End If

                        End If

                        ora_dr.Close()

                    End If

                End If


            Catch ex As Exception
                resultat = New TachesARealiser()
                LogsFunctions.LogWrite(ex.ToString(), p_user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New TachesARealiser()
            LogsFunctions.LogWrite(ex.ToString(), p_user)
        End Try

        Return resultat

    End Function

    Public Shared Function insertReglesValorisation(p_user As Utilisateur, p_id_facture As String, p_liste_champs As List(Of Champ)) As Boolean

        Dim resultat As Boolean = False

        If (Not (p_user Is Nothing Or p_liste_champs Is Nothing)) Then

            Dim ora_connexion As OracleConnection
            Dim ora_commande As OracleCommand
            Dim ora_dr As OracleDataReader
            Dim ora_transaction As OracleTransaction
            Dim id_champ_pivot As String = p_user.IdChampPivotValorisation
            Dim typage_champ_pivot As String = ""
            Dim nom_champ_pivot As String = ""
            Dim requete_regle, requete_champ, requete_recherche, requete_valeur_pivot As String
            Dim valeur As String = ""
            Dim valeur_pivot As String = ""
            Dim condition As String = ""
            Dim regle_existe As Boolean = False

            If (id_champ_pivot <> "") Then

                Try

                    ora_connexion = New OracleConnection(_chaineConnexion)
                    ora_connexion.Open()

                    ora_transaction = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

                    Try

                        p_id_facture = p_id_facture.Replace("'", "")

                        'Recherche des information du champ pivot
                        requete_champ = "select nom_champ,typage from champ_workflow where id_champ='" & id_champ_pivot & "' and workflow='" & p_user.Workflow & "'"
                        ora_commande = New OracleCommand(requete_champ, ora_connexion)
                        ora_dr = ora_commande.ExecuteReader()

                        If (ora_dr.Read()) Then
                            nom_champ_pivot = ora_dr.Item("nom_champ").ToString()
                            typage_champ_pivot = ora_dr.Item("typage").ToString()
                        End If

                        ora_dr.Close()

                        If (nom_champ_pivot.Trim() <> "" And typage_champ_pivot <> "") Then

                            'Recherche de la valeur du champ pivot
                            requete_valeur_pivot = "select " & nom_champ_pivot & " from " & p_user.TableFacture & " where id_facture='" & p_id_facture & "' and id_client='" & p_user.Workflow & "'"
                            ora_commande = New OracleCommand(requete_valeur_pivot, ora_connexion)
                            ora_dr = ora_commande.ExecuteReader()

                            If (ora_dr.Read()) Then
                                valeur_pivot = ora_dr.Item(0).ToString()
                            End If

                            ora_dr.Close()

                            'Définition de la condition d'application de la régle
                            If (typage_champ_pivot = "DATE") Then
                                condition = "'" & nom_champ_pivot & "=to_date(''" & valeur_pivot & "'',''dd/mm/yyyy'')' "
                            Else
                                condition = "'" & nom_champ_pivot & "=''" & valeur_pivot & "'''"
                            End If

                            For Each ch As Champ In p_liste_champs

                                If (ch.Typage = "DATE") Then
                                    valeur = "to_date('" & ch.Valeur.Replace("'", "") & "','dd/mm/yyyy')"
                                Else
                                    valeur = "'" & ch.Valeur.Replace("'", "''") & "'"
                                End If

                                'On vérifie si la régle existe
                                requete_recherche = "select * from regle_valorisation_automatique where id_champ='" & ch.IDChamp & "' and workflow='" & p_user.Workflow & "' and condition=" & condition & " "
                                ora_commande = New OracleCommand(requete_recherche, ora_connexion)
                                ora_dr = ora_commande.ExecuteReader()

                                If (ora_dr.Read()) Then
                                    regle_existe = True
                                End If

                                ora_dr.Close()

                                If (regle_existe) Then

                                    'Si la régle existe on la met à jour
                                    requete_regle = "update regle_valorisation_automatique set " &
                                                    "valeur=" & valeur & " " &
                                                    "where id_champ='" & ch.IDChamp & "' and workflow='" & p_user.Workflow & "' and condition=" & condition & " "
                                Else

                                    'Sinon on ajoute la nouvelle régle
                                    requete_regle = "insert into regle_valorisation_automatique(workflow,id_champ,valeur,condition) " &
                                        "values('" & p_user.Workflow & "','" & ch.IDChamp & "'," & valeur & "," & condition & ")"

                                End If

                                ora_commande = New OracleCommand(requete_regle, ora_connexion)
                                ora_commande.ExecuteNonQuery()

                            Next

                            ora_transaction.Commit()
                            resultat = True

                            'Historisation de l'enregistrement des régles

                            Dim liste_hiso As New List(Of Historique)
                            liste_hiso.Add(New Historique(p_user.Workflow, p_id_facture, "COCHE VALORISATION AUTOMATIQUE", "", "", "", p_user.Identifiant, "", "", "", ""))
                            InsertHistorique(liste_hiso)

                        Else

                            ora_transaction.Rollback()
                            resultat = False

                        End If

                    Catch ex As Exception
                        ora_transaction.Rollback()
                        resultat = False
                        LogsFunctions.LogWrite(ex.ToString(), p_user)
                    End Try

                    ora_connexion.Close()

                Catch ex As Exception
                    resultat = False
                    LogsFunctions.LogWrite(ex.ToString(), p_user)
                End Try

            End If

        End If

        Return resultat

    End Function

    Public Shared Function DeleteReglesValorisation(p_user As Utilisateur, p_id_facture As String, p_liste_champs As List(Of Champ)) As Boolean

        Dim resultat As Boolean = False

        If (Not (p_user Is Nothing Or p_liste_champs Is Nothing)) Then

            Dim ora_connexion As OracleConnection
            Dim ora_commande As OracleCommand
            Dim ora_dr As OracleDataReader
            Dim ora_transaction As OracleTransaction
            Dim id_champ_pivot As String = p_user.IdChampPivotValorisation
            Dim typage_champ_pivot As String = ""
            Dim nom_champ_pivot As String = ""
            Dim requete_regle, requete_champ, requete_valeur_pivot As String
            Dim valeur As String = ""
            Dim valeur_pivot As String = ""
            Dim condition As String = ""
            Dim regle_existe As Boolean = False

            If (id_champ_pivot <> "") Then

                Try

                    ora_connexion = New OracleConnection(_chaineConnexion)
                    ora_connexion.Open()

                    ora_transaction = ora_connexion.BeginTransaction(IsolationLevel.ReadCommitted)

                    Try

                        p_id_facture = p_id_facture.Replace("'", "")

                        'Recherche des information du champ pivot
                        requete_champ = "select nom_champ,typage from champ_workflow where id_champ='" & id_champ_pivot & "' and workflow='" & p_user.Workflow & "'"
                        ora_commande = New OracleCommand(requete_champ, ora_connexion)
                        ora_dr = ora_commande.ExecuteReader()

                        If (ora_dr.Read()) Then
                            nom_champ_pivot = ora_dr.Item("nom_champ").ToString()
                            typage_champ_pivot = ora_dr.Item("typage").ToString()
                        End If

                        ora_dr.Close()

                        If (nom_champ_pivot.Trim() <> "" And typage_champ_pivot <> "") Then

                            'Recherche de la valeur du champ pivot
                            requete_valeur_pivot = "select " & nom_champ_pivot & " from " & p_user.TableFacture & " where id_facture='" & p_id_facture & "' and id_client='" & p_user.Workflow & "'"
                            ora_commande = New OracleCommand(requete_valeur_pivot, ora_connexion)
                            ora_dr = ora_commande.ExecuteReader()

                            If (ora_dr.Read()) Then
                                valeur_pivot = ora_dr.Item(0).ToString()
                            End If

                            ora_dr.Close()

                            'Définition de la condition d'application de la régle
                            If (typage_champ_pivot = "DATE") Then
                                condition = "'" & nom_champ_pivot & "=to_date(''" & valeur_pivot & "'',''dd/mm/yyyy'')' "
                            Else
                                condition = "'" & nom_champ_pivot & "=''" & valeur_pivot & "'''"
                            End If

                            For Each ch As Champ In p_liste_champs

                                'Suppression des régles correspondantes
                                requete_regle = "delete from regle_valorisation_automatique " &
                                                "where id_champ='" & ch.IDChamp & "' and workflow='" & p_user.Workflow & "' and condition=" & condition & " "

                                ora_commande = New OracleCommand(requete_regle, ora_connexion)
                                ora_commande.ExecuteNonQuery()

                            Next

                            ora_transaction.Commit()
                            resultat = True

                            'Historisation de l'enregistrement des régles

                            Dim liste_hiso As New List(Of Historique)
                            liste_hiso.Add(New Historique(p_user.Workflow, p_id_facture, "DECOCHE VALORISATION AUTOMATIQUE", "", "", "", p_user.Identifiant, "", "", "", ""))
                            InsertHistorique(liste_hiso)

                        Else

                            ora_transaction.Rollback()
                            resultat = False

                        End If

                    Catch ex As Exception
                        ora_transaction.Rollback()
                        resultat = False
                        LogsFunctions.LogWrite(ex.ToString(), p_user)
                    End Try

                    ora_connexion.Close()

                Catch ex As Exception
                    resultat = False
                    LogsFunctions.LogWrite(ex.ToString(), p_user)
                End Try

            End If

        End If

        Return resultat

    End Function

    Public Shared Function getEmailsValideurs(p_user As Utilisateur) As List(Of String)

        Dim resultat As New List(Of String)

        If (Not p_user Is Nothing) Then

            Dim connexion As OracleConnection
            Dim commande As OracleCommand
            Dim dr As OracleDataReader
            Dim requete As String

            Try
                connexion = New OracleConnection(ChaineConnexion)
                connexion.Open()

                Try
                    requete = "select email from utilisateur where workflow='" & p_user.Workflow & "' and id_profil=3"
                    commande = New OracleCommand(requete, connexion)
                    dr = commande.ExecuteReader()

                    While (dr.Read())

                        resultat.Add(dr.Item("email").ToString())

                    End While

                    dr.Close()

                Catch ex As Exception
                    resultat = New List(Of String)
                    LogsFunctions.LogWrite(ex.ToString(), p_user)
                End Try

                connexion.Close()

            Catch ex As Exception
                resultat = New List(Of String)
                LogsFunctions.LogWrite(ex.ToString(), p_user)
            End Try

        End If

        Return resultat

    End Function

    Public Shared Function getMessageValideur(p_user As Utilisateur, p_docid As String) As String

        Dim resultat As String = ""

        If (Not p_user Is Nothing And p_docid.Trim() <> "") Then

            Dim connexion As OracleConnection
            Dim commande As OracleCommand
            Dim dr As OracleDataReader
            Dim requete As String

            Try
                connexion = New OracleConnection(ChaineConnexion)
                connexion.Open()

                Try
                    requete = "select * from " & p_user.TableFacture & " where id_client='" & p_user.Workflow & "' and id_facture='" & p_docid & "'"
                    commande = New OracleCommand(requete, connexion)
                    dr = commande.ExecuteReader()

                    If (dr.Read()) Then

                        Dim date_facture As String = dr.Item("date_facture").ToString()

                        If (date_facture.Length > 10) Then
                            date_facture = date_facture.Substring(0, 10)
                        End If

                        resultat = "<!doctype html>" &
                                    "<html lang=""fr"">" &
                                        "<head>" &
                                            "<meta charset=""utf-8"" />" &
                                        "</head>" &
                                        "<body>" &
                                            "Bonjour <br/><br/>" &
                                            "Vous avez une nouvelle facture à valider. <br/><br/>" &
                                            "Numéro : " & dr.Item("num_facture").ToString() & "</br>" &
                                            "Date : " & date_facture & "</br>" &
                                            "Code Fournisseur : " & dr.Item("code_fournisseur").ToString() & "</br></br>" &
                                            "Cordialement. </br>" &
                                        "</body>" &
                                    "</html>"

                    End If

                    dr.Close()

                Catch ex As Exception
                    resultat = ""
                    LogsFunctions.LogWrite(ex.ToString(), p_user)
                End Try

                connexion.Close()

            Catch ex As Exception
                resultat = ""
                LogsFunctions.LogWrite(ex.ToString(), p_user)
            End Try

        End If

        Return resultat

    End Function

    Public Shared Function getQuantitesCorbeilles(p_user As Utilisateur) As QuantitesCorbeilles

        Dim resultat As New QuantitesCorbeilles()

        Dim connexion As OracleConnection
        Dim commande As OracleCommand
        Dim dr As OracleDataReader
        Dim requete As String
        Dim statut_corbeille_validation As String = ""

        Try

            connexion = New OracleConnection(_chaineConnexion)
            connexion.Open()

            Try
                If (p_user.IDProfil = "3") Then
                    statut_corbeille_validation = "8"
                Else
                    statut_corbeille_validation = "9"
                End If

                requete = "Select sum(case when(f.id_statut_facture = 2  And f.Nom_Fournisseur <> 'EGENCIA' And f.id_rejet not in ('5','6')) then 1 else 0 end) as nb_corbeille_rejet," &
                    "sum(Case When(f.id_statut_facture = 2 And f.id_rejet = '5') then 1 else 0 end) as nb_corbeille_four_inconnu," &
                    "sum(case when(f.id_statut_facture = 2 And f.id_rejet = '6') then 1 else 0 end) as nb_corbeille_four_mutliple," &
                    "sum(case when(f.id_statut_facture = 2 And SUBSTR(f.NUM_COMMANDE, 0, 5) = '099PU' ) then 1 else 0 end) as nb_corbeille_commande " &
                    ",sum(case when(f.NOM_FOURNISSEUR = 'EGENCIA') Then 1 else 0 end) as nb_corbeille_egencia " &
                    "from " & p_user.TableFacture & " f " &
                    "where f.id_client='" & p_user.Workflow & "' and (f.flag_occupe is null or f.flag_occupe<>'1')"

                commande = New OracleCommand(requete, connexion)
                dr = commande.ExecuteReader()

                Dim res As Boolean = False

                If (dr.Read()) Then
                    res = Integer.TryParse(dr.Item("nb_corbeille_rejet").ToString(), resultat.NbCorbeilleRejet)
                    res = Integer.TryParse(dr.Item("nb_corbeille_four_inconnu").ToString(), resultat.NbCorbeilleFourInconnu)
                    res = Integer.TryParse(dr.Item("nb_corbeille_four_mutliple").ToString(), resultat.NbCorbeilleFourMultiple)
                    res = Integer.TryParse(dr.Item("nb_corbeille_commande").ToString(), resultat.NbCorbeilleCommande)
                    res = Integer.TryParse(dr.Item("nb_corbeille_egencia").ToString(), resultat.NbCorbeilleEgencia)
                End If

                requete = $" select sum (case when(t.id_statut = 2 And t.id_client = '{p_user.Workflow}') then 1 else 0 end) as nb_hors_scope " &
                    "From vw_document_hors_scope t " &
                    "Where t.id_client='" & p_user.Workflow & "'"

                commande = New OracleCommand(requete, connexion)
                dr = commande.ExecuteReader()

                If (dr.Read()) Then
                    res = Integer.TryParse(dr.Item("nb_hors_scope").ToString(), resultat.NbHorsScope)
                End If

                dr.Close()


            Catch ex As Exception
                resultat = New QuantitesCorbeilles()
                LogsFunctions.LogWrite(ex.ToString(), p_user)
            End Try

            connexion.Close()

        Catch ex As Exception
            resultat = New QuantitesCorbeilles()
            LogsFunctions.LogWrite(ex.ToString(), p_user)
        End Try

        Return resultat

    End Function

    Public Shared Function getCollectif(p_user As Utilisateur, p_compte_comptable As String) As String

        Dim resultat As String = ""

        Dim connexion As OracleConnection
        Dim commande As OracleCommand
        Dim dr As OracleDataReader
        Dim requete As String

        Try

            connexion = New OracleConnection(_chaineConnexion)
            connexion.Open()

            Try

                p_compte_comptable = p_compte_comptable.Replace(" '", "")

                requete = "select distinct CODE_APPEL " &
                    "from COMMANDE_RGDS@LIEN_BPROCESS " &
                    "where COMPTE_GENERAL='" & p_compte_comptable & "' and ID_WORKFLOW='" & p_user.Workflow & "'"

                commande = New OracleCommand(requete, connexion)
                dr = commande.ExecuteReader()

                If (dr.Read()) Then
                    resultat = dr.Item("CODE_APPEL").ToString()
                End If

                dr.Close()


            Catch ex As Exception
                resultat = ""
                LogsFunctions.LogWrite(ex.ToString(), p_user)
            End Try

            connexion.Close()

        Catch ex As Exception
            resultat = ""
            LogsFunctions.LogWrite(ex.ToString(), p_user)
        End Try

        Return resultat

    End Function

    Public Shared Function getCodeComptable(p_user As Utilisateur) As List(Of ListItem)

        Dim resultat As New List(Of ListItem)

        Dim connexion As OracleConnection
        Dim commande As OracleCommand
        Dim dr As OracleDataReader
        Dim requete As String

        Try

            connexion = New OracleConnection(_chaineConnexion)
            connexion.Open()

            Try



                requete = "select distinct COMPTE_GENERAL " &
                    "from COMMANDE_RGDS@LIEN_BPROCESS " &
                    "where ID_WORKFLOW='" & p_user.Workflow & "' order by compte_general"

                commande = New OracleCommand(requete, connexion)
                dr = commande.ExecuteReader()

                resultat.Add(New ListItem("", ""))

                While (dr.Read())
                    resultat.Add(New ListItem(dr.Item("COMPTE_GENERAL").ToString(), dr.Item("COMPTE_GENERAL").ToString()))
                End While
                If (dr.Read()) Then

                End If

                dr.Close()


            Catch ex As Exception
                resultat = New List(Of ListItem)
                LogsFunctions.LogWrite(ex.ToString(), p_user)
            End Try

            connexion.Close()

        Catch ex As Exception
            resultat = New List(Of ListItem)
            LogsFunctions.LogWrite(ex.ToString(), p_user)
        End Try

        Return resultat

    End Function

    Public Shared Function GetNbATraiterFDJ(d_debut As String, d_fin As String, identifiant As String, p_user As Utilisateur) As List(Of Resultat)
        Dim resultat As New List(Of Resultat)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim profil_request As String = ""

        Try

            ora_connexion = New OracleConnection(_chaineConnexion)
            ora_connexion.Open()


            Try

                If (p_user.IDProfil = "4") Then
                    profil_request = $" and f.comptable ='{p_user.Email}' "
                ElseIf (p_user.IDProfil = "1") Then
                    If (Not String.IsNullOrEmpty(identifiant)) Then
                        profil_request = $" and f.comptable ='{identifiant}' "
                    End If
                End If

                If (Not String.IsNullOrEmpty(d_debut)) Then
                    profil_request += " and f.date_insertion>=to_date('" & d_debut & "','dd/mm/yyyy') "
                End If

                If (Not String.IsNullOrEmpty(d_fin)) Then
                    profil_request += " and f.date_insertion<=to_date('" & d_fin & "','dd/mm/yyyy') + 1 "
                End If




                requete = $"select COALESCE(u.prenom, 'A attribuer') as prenom, u.nom, count(*) as nb_facture from VW_FACTURES_FDJ f, Utilisateur u where f.comptable=u.email(+) and nom_fournisseur<>'FRANCAISE DE MOTIVATION' and f.numero_ap is null and f.date_insertion-sysdate <= -5 {profil_request} group by u.prenom, u.nom order by u.prenom"
                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())
                    resultat.Add(New Resultat($"{ora_dr.Item("prenom").ToString()} {ora_dr.Item("nom").ToString()}", Convert.ToDouble(ora_dr.Item("nb_facture").ToString()), 0, 0))
                End While



            Catch ex As Exception
                resultat = New List(Of Resultat)
                LogsFunctions.LogWrite(ex.ToString(), p_user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Resultat)
            LogsFunctions.LogWrite(ex.ToString(), p_user)
        End Try

        If resultat.Count = 0 Then
            resultat.Add(New Resultat("NONE", 0, 0, 0))
        End If


        Return resultat

    End Function

    Public Shared Function GetNbRejetFDJ(d_debut As String, d_fin As String, identifiant As String, p_user As Utilisateur) As List(Of Resultat)
        Dim resultat As New List(Of Resultat)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim profil_request As String = ""

        Try

            ora_connexion = New OracleConnection(_chaineConnexion)
            ora_connexion.Open()


            Try

                If (p_user.IDProfil = "4") Then
                    profil_request = $" and f.comptable ='{p_user.Email}' "
                ElseIf (p_user.IDProfil = "1") Then
                    If (Not String.IsNullOrEmpty(identifiant)) Then
                        profil_request = $" and f.comptable ='{identifiant}' "
                    End If
                End If

                If (Not String.IsNullOrEmpty(d_debut)) Then
                    profil_request += " and f.date_insertion>=to_date('" & d_debut & "','dd/mm/yyyy') "
                End If

                If (Not String.IsNullOrEmpty(d_fin)) Then
                    profil_request += " and f.date_insertion<to_date('" & d_fin & "','dd/mm/yyyy') + 1 "
                End If


                requete = $"select r.libelle_rejet, count(*) as nb_facture from VW_FACTURES_FDJ f, TB_REJET r where f.ID_rejet=r.id_rejet and r.id_client='{p_user.Workflow}' {profil_request} group by r.libelle_rejet"
                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())
                    resultat.Add(New Resultat(ora_dr.Item("libelle_rejet").ToString(), Convert.ToDouble(ora_dr.Item("nb_facture").ToString()), 0, 0))
                End While




            Catch ex As Exception
                resultat = New List(Of Resultat)
                LogsFunctions.LogWrite(ex.ToString(), p_user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Resultat)
            LogsFunctions.LogWrite(ex.ToString(), p_user)
        End Try

        If resultat.Count = 0 Then
            resultat.Add(New Resultat("NONE", 0, 0, 0))
        End If

        Return resultat

    End Function


    Public Shared Function GetNbTachesARealiserFDJ(p_user As Utilisateur) As TachesARealiser

        Dim resultat As New TachesARealiser

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete_validation, requete_a_traiter As String

        Try

            ora_connexion = New OracleConnection(_chaineConnexion)
            ora_connexion.Open()


            Try

                Dim param_statuts_validation As String = ""
                Dim param_statuts_a_traiter As String = ""
                Dim temp As String = ""

                If (p_user.IDProfil = "4") Then
                    temp = $" and comptable='{p_user.Email}'"
                End If

                requete_validation = $"Select count(*) from VW_FACTUREs_FDJ where pilote = '1' and numero_ap is null {temp}"
                ora_commande = New OracleCommand(requete_validation, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                If (ora_dr.Read()) Then

                    If (ora_dr.Item(0).ToString().Trim() <> "") Then

                        resultat.NbCorbeilleValidation = Convert.ToInt32(ora_dr.Item(0).ToString())

                    End If

                End If

                requete_a_traiter = $"select count(*) from VW_FACTUREs_FDJ where nom_fournisseur = 'FRANCAISE DE MOTIVATION' and pilote = '0'  and numero_ap is null {temp}"
                ora_commande = New OracleCommand(requete_a_traiter, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                If (ora_dr.Read()) Then

                    If (ora_dr.Item(0).ToString().Trim() <> "") Then

                        resultat.NbCorbeilleATraiter = Convert.ToInt32(ora_dr.Item(0).ToString())

                    End If

                End If

            Catch ex As Exception
                resultat = New TachesARealiser()
                LogsFunctions.LogWrite(ex.ToString(), p_user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New TachesARealiser()
            LogsFunctions.LogWrite(ex.ToString(), p_user)
        End Try

        Return resultat

    End Function

    Public Shared Function getFacturesFDJ(user As Utilisateur, id_filtre As String, liste_statuts As List(Of String), exclure_occupe As Boolean, exclure_rejets As Boolean, rejets_uniquement As Boolean, id_rejet As String, nb_jours_limite As Integer, p_periode_archive As String) As List(Of Facture)

        Dim ora_connexion As OracleConnection
        Dim ora_commande_filtre, ora_commande_facture As OracleCommand
        Dim ora_dr_filtre, ora_dr_facture As OracleDataReader
        Dim requete_filtre As String = ""
        Dim requete_facture As String = ""
        Dim resultat As New List(Of Facture)
        Dim chaine_parametres As String = ""
        Dim facture_temp As Facture
        Dim annee, mois As String

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                'recherche des paramêtres du filtre

                requete_filtre = "select cw.nom_champ,cw.typage,cw.requete_liste,cw.expression_reguliere,cfc.valeur,cfc.operateur " &
                    "from champ_filtre_corbeille cfc " &
                    "inner join champ_workflow cw on (cfc.workflow=cw.workflow and cfc.id_champ=cw.id_champ) " &
                    "where cfc.workflow='" & user.Workflow & "' and cfc.id_filtre_corbeille='" & id_filtre & "'"

                ora_commande_filtre = New OracleCommand(requete_filtre, ora_connexion)
                ora_dr_filtre = ora_commande_filtre.ExecuteReader()

                While (ora_dr_filtre.Read())

                    Dim operateur As String = ora_dr_filtre.Item("OPERATEUR").ToString()
                    Dim valeur As String = ora_dr_filtre.Item("VALEUR").ToString()
                    Dim nom_champ As String = ora_dr_filtre.Item("NOM_CHAMP").ToString()
                    Dim typage As String = ora_dr_filtre.Item("TYPAGE").ToString()

                    If (typage = "TEXTE" Or typage = "LISTE") Then

                        If (operateur = "LIKE") Then

                            chaine_parametres += " and f." & nom_champ & " like '%" & valeur & "%' "

                        ElseIf (operateur = "IS_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is null "

                        ElseIf (operateur = "IS_NOT_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is not null "

                        Else

                            chaine_parametres += " and f." & nom_champ & operateur & "'" & valeur & "' "

                        End If

                    ElseIf (typage = "ENTIER" Or typage = "DECIMAL") Then

                        If (operateur = "IS_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is null "

                        ElseIf (operateur = "IS_NOT_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is not null "
                        Else

                            chaine_parametres += " and f." & nom_champ & operateur & "'" & valeur & "' "

                        End If

                    ElseIf (typage = "DATE") Then

                        If (operateur = "DATATION_JOURS") Then

                            chaine_parametres += " and f." & nom_champ & " <= sysdate-" & valeur & " "

                        ElseIf (operateur = "DATATION_MOIS") Then

                            chaine_parametres += " and f." & nom_champ & " <= add_months(sysdate,-" & valeur & ") "

                        ElseIf (operateur = "DATATION_ANNEES") Then

                            chaine_parametres += " and f." & nom_champ & " <= add_months(sysdate,-12*" & valeur & ") "

                        ElseIf (operateur = "IS_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is null "

                        ElseIf (operateur = "IS_NOT_NULL") Then

                            chaine_parametres += " and f." & nom_champ & " is not null "

                        Else

                            chaine_parametres += " and f." & nom_champ & operateur & "to_date('" & valeur & "','dd/mm/yyyy') "

                        End If

                    End If

                End While

                ora_dr_filtre.Close()

                'Recherche des factures correspondantes

                Dim statuts As String = ""

                For Each s As String In liste_statuts

                    If (s.Trim() <> "") Then
                        statuts += "'" & s & "',"
                    End If

                Next

                If (statuts.Length > 0) Then
                    statuts = statuts.Substring(0, statuts.Length - 1)
                End If

                requete_facture = "select distinct f.*,sf.libelle_statut_facture,tba.date_insertion as date_acquittement,tbr.libelle_rejet,inv.libelle_motif_invalidite " &
                    "from " & user.TableFacture & " f " &
                    "inner join statut_facture sf on f.id_statut_facture=sf.id_statut_facture " &
                    "left join tb_livraison tbl on (tbl.id_facture=f.id_facture) " &
                    "left join tb_acquittement tba on (tba.id_livraison=tbl.id_livraison) " &
                    "left join tb_rejet tbr on (tbr.id_rejet=f.id_rejet and tbr.id_client=f.id_client) " &
                    "left join motif_invalidite inv on (f.id_motif_invalidite=inv.id_motif_invalidite and inv.workflow=f.id_client) " &
                    "where f.id_client='" & user.Workflow & "' " &
                    "and f.id_statut_facture in (" & statuts & ") " & chaine_parametres

                If (exclure_occupe) Then
                    requete_facture += " and (f.flag_occupe is null or f.flag_occupe<>'1') "
                End If

                If (exclure_rejets) Then
                    requete_facture += " and f.id_rejet is null "
                End If

                If (rejets_uniquement) Then

                    requete_facture += " and f.id_rejet is not null "

                    If (id_rejet.Trim() <> "") Then

                        requete_facture += " and f.id_rejet = '" & id_rejet & "' "

                    End If

                End If

                If (nb_jours_limite > 0) Then

                    requete_facture += " and date_insertion > (sysdate-" & nb_jours_limite.ToString() & ") "

                End If


                If (p_periode_archive <> "") Then

                    annee = p_periode_archive.Split("_")(0).ToString()
                    mois = p_periode_archive.Split("_")(1).ToString()

                    requete_facture += " and archive_annee='" & annee & "' and archive_mois='" & mois & "' "

                End If

                ora_commande_facture = New OracleCommand(requete_facture, ora_connexion)
                ora_dr_facture = ora_commande_facture.ExecuteReader()

                While (ora_dr_facture.Read())

                    facture_temp = New Facture()
                    facture_temp.IDFacture = ora_dr_facture.Item("id_facture").ToString()
                    'facture_temp.CodeFournisseur = ora_dr_facture.Item("code_fournisseur").ToString()
                    facture_temp.NomFournisseur = ora_dr_facture.Item("nom_fournisseur").ToString()
                    facture_temp.NumFacture = ora_dr_facture.Item("num_facture").ToString()
                    'facture_temp.DateFacture = ora_dr_facture.Item("date_facture").ToString()
                    facture_temp.EntiteFacture = ora_dr_facture.Item("nom_societe").ToString()
                    'facture_temp.MontantTTC = ora_dr_facture.Item("montant_total").ToString()
                    facture_temp.NumCommande = ora_dr_facture.Item("numero_ap").ToString()
                    facture_temp.DateInsertion = ora_dr_facture.Item("date_insertion").ToString()
                    'facture_temp.DateAcquittement = ora_dr_facture.Item("date_acquittement").ToString()
                    facture_temp.Statut = New Statut(ora_dr_facture.Item("id_statut_facture").ToString(), ora_dr_facture.Item("libelle_statut_facture").ToString())
                    'facture_temp.DateTraitement = ora_dr_facture.Item("date_traitement_workflow").ToString()
                    facture_temp.Rejet = New Rejet(ora_dr_facture.Item("id_rejet").ToString(), ora_dr_facture.Item("libelle_rejet").ToString())
                    'facture_temp.MotifInvalidite = New Motif(ora_dr_facture.Item("id_motif_invalidite").ToString(), ora_dr_facture.Item("libelle_motif_invalidite").ToString(), "")
                    'facture_temp.CommentaireInvalidite = ora_dr_facture.Item("commentaire_invalidite").ToString()
                    'facture_temp.DateEnvoiValideur = ora_dr_facture.Item("date_envoi_valideur").ToString()
                    'facture_temp.DateRetourValideur = ora_dr_facture.Item("date_retour_valideur").ToString()
                    'facture_temp.IdentifiantValideur = ora_dr_facture.Item("identifiant_valideur").ToString()
                    facture_temp.Comptable = ora_dr_facture.Item("comptable").ToString()
                    facture_temp.TypeGestion = ora_dr_facture.Item("pilote").ToString()

                    'Exception des champs spécifiques pour AG2R
                    Try
                        facture_temp.IdEngagement = ora_dr_facture.Item("id_engagement").ToString()
                    Catch
                    End Try

                    Try
                        facture_temp.DateLivraison = ora_dr_facture.Item("date_prestation").ToString()
                    Catch
                    End Try


                    If (facture_temp.DateFacture.ToString().Length > 0) Then
                        facture_temp.DateFacture = facture_temp.DateFacture.Substring(0, 10)
                    End If

                    If (facture_temp.DateInsertion.ToString().Length > 0) Then
                        facture_temp.DateInsertion = facture_temp.DateInsertion.Substring(0, 10)
                    End If

                    If (facture_temp.DateLivraison.ToString().Length > 0) Then
                        facture_temp.DateLivraison = facture_temp.DateLivraison.Substring(0, 10)
                    End If

                    If (facture_temp.DateAcquittement.ToString().Length > 0) Then
                        facture_temp.DateAcquittement = facture_temp.DateAcquittement.Substring(0, 10)
                    End If

                    If (facture_temp.DateTraitement.ToString().Length > 0) Then
                        facture_temp.DateTraitement = facture_temp.DateTraitement.Substring(0, 10)
                    End If

                    If (facture_temp.DateEnvoiValideur.ToString().Length > 0) Then
                        facture_temp.DateEnvoiValideur = facture_temp.DateEnvoiValideur.Substring(0, 10)
                    End If

                    If (facture_temp.DateRetourValideur.ToString().Length > 0) Then
                        facture_temp.DateRetourValideur = facture_temp.DateRetourValideur.Substring(0, 10)
                    End If


                    resultat.Add(facture_temp)

                End While

                ora_dr_facture.Close()

            Catch ex As Exception
                resultat = New List(Of Facture)
                LogsFunctions.LogWrite(ex.ToString(), user)
                LogsFunctions.LogWrite(requete_facture, user)
                LogsFunctions.LogWrite(requete_filtre, user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of Facture)
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

    Public Shared Function GetAbsence(p_user As Utilisateur) As List(Of String)
        Dim resultat As New List(Of String)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String

        Try

            ora_connexion = New OracleConnection(_chaineConnexion)
            ora_connexion.Open()

            Try
                requete = $"SELECT * FROM attribution_absence a, utilisateur u WHERE a.utilisateur_absent = u.identifiant and a.workflow='{p_user.Workflow}' and a.cloture = '0' and a.destinataires like '%{p_user.Email}%'"
                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())
                    resultat.Add(ora_dr.Item("email").ToString())
                End While

            Catch ex As Exception
                resultat = New List(Of String)
                LogsFunctions.LogWrite(ex.ToString(), p_user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of String)
            LogsFunctions.LogWrite(ex.ToString(), p_user)
        End Try

        Return resultat
    End Function


    Public Shared Function getUtilisateursFDJ(p_user As Utilisateur) As List(Of ListItem)
        Dim resultat As New List(Of ListItem)

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String

        Try

            ora_connexion = New OracleConnection(_chaineConnexion)
            ora_connexion.Open()

            Try
                requete = $"SELECT * FROM utilisateur where workflow='{p_user.Workflow}'"
                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())
                    resultat.Add(New ListItem($"{ora_dr.Item("prenom").ToString()} {ora_dr.Item("nom").ToString()}", ora_dr.Item("identifiant").ToString()))
                End While

            Catch ex As Exception
                resultat = New List(Of ListItem)
                LogsFunctions.LogWrite(ex.ToString(), p_user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = New List(Of ListItem)
            LogsFunctions.LogWrite(ex.ToString(), p_user)
        End Try

        Return resultat
    End Function


    Public Shared Function InsertRejet(p_user As Utilisateur, rejet As String, bloquant As String) As Boolean

        Dim resultat As Boolean = False

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim id As Integer
        Dim nb As Integer

        Try

            ora_connexion = New OracleConnection(_chaineConnexion)
            ora_connexion.Open()

            rejet = rejet.Replace("'", "")
            bloquant = bloquant.Replace("'", "")

            Try
                requete = $"SELECT MAX(ID_REJET) FROM TB_REJET WHERE id_client='{p_user.Workflow}'"
                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                If (ora_dr.Read()) Then

                    If (ora_dr.Item(0).ToString().Trim() <> "") Then
                        id = Convert.ToInt32(ora_dr.Item(0).ToString())
                    Else
                        id = 0
                    End If
                Else
                    id = 0
                End If

                requete = $"INSERT INTO TB_REJET(ID_REJET, ID_CLIENT, LIBELLE_REJET, BLOQUANT) VALUES ('{id + 1}','{p_user.Workflow}','{rejet}','{bloquant}')"
                ora_commande = New OracleCommand(requete, ora_connexion)
                nb = ora_commande.ExecuteNonQuery()

                If nb = 1 Then
                    resultat = True
                End If
            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString(), p_user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), p_user)
        End Try

        Return resultat

    End Function
    Public Shared Function UpdateStatutFDJ(docid As String, user As Utilisateur, liste_champs As List(Of Champ)) As Boolean
        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim ora_dr As OracleDataReader
        Dim requete As String
        Dim resultat As Boolean = False
        Dim rejet As String = ""
        Dim num_ap As String = ""
        Dim trouve As Boolean = False

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = $"select id_rejet, numero_ap from vw_factures_fdj where ID_FACTURE='{docid}'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_dr = ora_commande.ExecuteReader()

                While (ora_dr.Read())
                    rejet = ora_dr.Item("id_rejet").ToString()
                    num_ap = ora_dr.Item("numero_ap").ToString()
                End While

                Dim champ As Champ = liste_champs.Where(Function(x) x.IDChamp = "12").FirstOrDefault()

                If Not champ Is Nothing Then
                    If champ.AncienneValeur <> champ.Valeur Then
                        trouve = True
                    End If
                End If

                If trouve Then
                    UpdateStatutWithValue(docid, champ.Valeur, user)
                Else
                    If (rejet = "9" Or (Not String.IsNullOrEmpty(num_ap) And String.IsNullOrEmpty(rejet))) Then
                        UpdateStatutWithValue(docid, "7", user)
                    Else
                        UpdateStatutWithValue(docid, "2", user)
                    End If
                End If


            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat
    End Function

    Public Shared Function UpdateInvoiceByComptable(user As Utilisateur, new_email As String) As Boolean
        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete As String
        Dim resultat As Boolean = False

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = $"update {user.TableFacture} Set comptable='{new_email}' where comptable='{user.Email}'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_commande.ExecuteReader()

                resultat = True

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString())
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString())
        End Try

        Return resultat
    End Function



    Public Shared Function UpdateStatutWithValue(docid As String, statut As String, user As Utilisateur) As Boolean

        Dim ora_connexion As OracleConnection
        Dim ora_commande As OracleCommand
        Dim requete As String
        Dim resultat As Boolean = False

        Try

            ora_connexion = New OracleConnection(ChaineConnexion)
            ora_connexion.Open()

            Try

                requete = $"update tb_facture Set ID_STATUT_FACTURE='{statut}' where ID_FACTURE='{docid}'"

                ora_commande = New OracleCommand(requete, ora_connexion)
                ora_commande.ExecuteReader()

                resultat = True

            Catch ex As Exception
                resultat = False
                LogsFunctions.LogWrite(ex.ToString(), user)
            End Try

            ora_connexion.Close()

        Catch ex As Exception
            resultat = False
            LogsFunctions.LogWrite(ex.ToString(), user)
        End Try

        Return resultat

    End Function

End Class
