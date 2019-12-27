'------------------------------------------------------------------------------
' <généré automatiquement>
'     Ce code a été généré par un outil.
'
'     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
'     le code est régénéré.
' </généré automatiquement>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class Traitement
    
    '''<summary>
    '''Contrôle HiddenDocID.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents HiddenDocID As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Contrôle HiddenStatut.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents HiddenStatut As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Contrôle HiddenFlagCorrection.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents HiddenFlagCorrection As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Contrôle HiddenFlagEnrichissement.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents HiddenFlagEnrichissement As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Contrôle HiddenNumFacture.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents HiddenNumFacture As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Contrôle HiddenDateFacture.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents HiddenDateFacture As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Contrôle HiddenWorkflow.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents HiddenWorkflow As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Contrôle HiddenProfil.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents HiddenProfil As Global.System.Web.UI.WebControls.HiddenField
    
    '''<summary>
    '''Contrôle ddlAction.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents ddlAction As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Contrôle litNewRejet.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents litNewRejet As Global.System.Web.UI.WebControls.Literal
    
    '''<summary>
    '''Contrôle Nom_Fichier.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents Nom_Fichier As Global.System.Web.UI.WebControls.Literal
    
    '''<summary>
    '''Contrôle pnlCommentaireValideur.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents pnlCommentaireValideur As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Contrôle litValideurCommentaire.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents litValideurCommentaire As Global.System.Web.UI.WebControls.Literal
    
    '''<summary>
    '''Contrôle litDateCommentaire.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents litDateCommentaire As Global.System.Web.UI.WebControls.Literal
    
    '''<summary>
    '''Contrôle litCommentaire.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents litCommentaire As Global.System.Web.UI.WebControls.Literal
    
    '''<summary>
    '''Contrôle iframePDF.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents iframePDF As Global.System.Web.UI.HtmlControls.HtmlIframe
    
    '''<summary>
    '''Contrôle pnlCorrection.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents pnlCorrection As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Contrôle RepeaterCorrection.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents RepeaterCorrection As Global.System.Web.UI.WebControls.Repeater
    
    '''<summary>
    '''Contrôle btnValiderCorrection.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents btnValiderCorrection As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Contrôle pnlEnrichissement.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents pnlEnrichissement As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Contrôle RepeaterEnrichissement.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents RepeaterEnrichissement As Global.System.Web.UI.WebControls.Repeater
    
    '''<summary>
    '''Contrôle btnValiderEnrichissement.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents btnValiderEnrichissement As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Contrôle litValorisationAutomatique.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents litValorisationAutomatique As Global.System.Web.UI.WebControls.Literal
    
    '''<summary>
    '''Contrôle pnlBascule.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents pnlBascule As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Contrôle ddlStatutBascule.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents ddlStatutBascule As Global.System.Web.UI.WebControls.DropDownList
    
    '''<summary>
    '''Contrôle pnlLignesFacture.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents pnlLignesFacture As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Contrôle litLignesFacture.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents litLignesFacture As Global.System.Web.UI.WebControls.Literal
    
    '''<summary>
    '''Contrôle pnlAjoutDocuments.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents pnlAjoutDocuments As Global.System.Web.UI.WebControls.Panel
    
    '''<summary>
    '''Contrôle litFichiersAjoutes.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents litFichiersAjoutes As Global.System.Web.UI.WebControls.Literal
    
    '''<summary>
    '''Contrôle litAlertes.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents litAlertes As Global.System.Web.UI.WebControls.Literal
    
    '''<summary>
    '''Contrôle btnRetour.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents btnRetour As Global.System.Web.UI.WebControls.Button
    
    '''<summary>
    '''Contrôle openModalConfirmValidationFacture.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents openModalConfirmValidationFacture As Global.System.Web.UI.HtmlControls.HtmlButton
    
    '''<summary>
    '''Contrôle litButtonValidation.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents litButtonValidation As Global.System.Web.UI.WebControls.Literal
    
    '''<summary>
    '''Contrôle openModalConfirmInvalidationFacture.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents openModalConfirmInvalidationFacture As Global.System.Web.UI.HtmlControls.HtmlButton
    
    '''<summary>
    '''Contrôle litMotifsAlerte.
    '''</summary>
    '''<remarks>
    '''Champ généré automatiquement.
    '''Pour modifier, déplacez la déclaration de champ du fichier de concepteur dans le fichier code-behind.
    '''</remarks>
    Protected WithEvents litMotifsAlerte As Global.System.Web.UI.WebControls.Literal
End Class
