<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" EnableEventValidation="false" CodeBehind="Traitement.aspx.vb" Inherits="WORKFLOW_FACTURE.Traitement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">

        <asp:HiddenField ID="HiddenDocID" runat="server" />
        <asp:HiddenField ID="HiddenStatut" runat="server" />
        <asp:HiddenField ID="HiddenFlagCorrection" Value="0" runat="server" />
        <asp:HiddenField ID="HiddenFlagEnrichissement" Value="0" runat="server" />
        <asp:HiddenField ID="HiddenNumFacture" runat="server" />
        <asp:HiddenField ID="HiddenDateFacture" runat="server" />
        <asp:HiddenField ID="HiddenWorkflow" runat="server" />
        <asp:HiddenField ID="HiddenProfil" runat="server" />

        <div class="row" style="padding-top: 15px;">

            <div class="col-lg-12">

                <label for="ddlAction">
                    Actions possibles
                </label>
                <div class="row">
                    <div class="col-lg-3">
                        <asp:DropDownList ID="ddlAction" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>         
                    </div>
                     <div class="col-lg-3">
                        <asp:Literal ID="litNewRejet" runat="server"></asp:Literal>
                    </div>
                </div>                 
                <div class="row">
                    <div class="col-lg-12">
                      <p class="text-primary"><asp:Literal ID="Nom_Fichier" runat="server"></asp:Literal></p>
                    </div>
                </div>
            </div>

            <asp:Panel ID="pnlCommentaireValideur" runat="server">

                <div class="col-lg-9">

                    <div class="row">

                        <div class="col-lg-6">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <strong><asp:Literal ID="litValideurCommentaire" runat="server"></asp:Literal></strong> <span class="text-muted"><asp:Literal ID="litDateCommentaire" runat="server"></asp:Literal></span>
                                </div>
                                <div class="panel-body">
                                    <asp:Literal ID="litCommentaire" runat="server"></asp:Literal>
                                </div>
                               
                            </div>
                            
                        </div> 

                    </div>

                </div>

            </asp:Panel>

        </div>
        

        <div class="row" style="margin-top: 15px;">
            <div class="col-lg-8">
                <div class="row">
                    <iframe id="iframePDF" runat="server" class="cadre_pdf"></iframe>
                </div>
            </div>

            <div class="col-lg-4">

                <asp:Panel ID="pnlCorrection" runat="server">

                    <div class="panel panel-default">

                        <div class="panel-heading panel-heading-custom">
                            <i class="fa fa-keyboard-o fa-fw"></i>&nbsp;Corrections de valeurs
                        </div>

                        <div class="panel-body">

                            <div class="row">

                                <div class="col-lg-12">

                                    <div class="form-group" style="overflow-y: scroll; height: auto; max-height: 545px;">

                                        <asp:Repeater ID="RepeaterCorrection" runat="server">

                                            <ItemTemplate>

                                                <label>
                                                    <asp:Literal ID="litCorLibelle" runat="server" Text='<%# Eval("DESCRIPTION")%>'></asp:Literal>
                                                </label>

                                                <asp:TextBox ID="txtCorChamp" runat="server" CssClass="form-control" Text='<%# Eval("VALEUR")%>'></asp:TextBox>
                                                <asp:DropDownList ID="ddlCorChamp" runat="server" CssClass="form-control"></asp:DropDownList>

                                                <p class="help-block"></p>

                                                <asp:HiddenField ID="hiddenCorIdChamp" runat="server" Value='<%# Eval("IDCHAMP") %>' />
                                                <asp:HiddenField ID="hiddenCorNomChamp" runat="server" Value='<%# Eval("NOMCHAMP")%>' />
                                                <asp:HiddenField ID="hiddenCorTypage" runat="server" Value='<%# Eval("TYPAGE")%>' />

                                            </ItemTemplate>

                                        </asp:Repeater>

                                    </div>

                                    <asp:Button ID="btnValiderCorrection" runat="server" Text="Enregistrer les corrections" class="btn btn-primary btn-block" CausesValidation="False" UseSubmitBehavior="False" Enabled="True" />

                                </div>

                            </div>

                        </div>

                    </div>

                </asp:Panel>

                <asp:Panel ID="pnlEnrichissement" runat="server">

                    <div class="panel panel-default">

                        <div class="panel-heading panel-heading-custom">
                            <i class="fa fa-keyboard-o fa-fw"></i>&nbsp;Enrichissements de valeurs
                        </div>

                        <div class="panel-body">

                            <div class="row">

                                <div class="col-lg-12">

                                    <div class="form-group" style="overflow-y: scroll; height: auto; max-height: 545px;">

                                        <asp:Repeater ID="RepeaterEnrichissement" runat="server">

                                            <ItemTemplate>

                                                <label>
                                                    <asp:Literal ID="litEnrLibelle" runat="server" Text='<%# Eval("DESCRIPTION")%>'></asp:Literal>
                                                </label>

                                                <asp:TextBox ID="txtEnrChamp" runat="server" CssClass="form-control champ-automatisation" Text='<%# Eval("VALEUR")%>' data-id='<%# Eval("IDCHAMP")%>' data-typage='<%# Eval("TYPAGE")%>'></asp:TextBox>
                                                <asp:DropDownList ID="ddlEnrChamp" runat="server" CssClass="form-control champ-automatisation" data-id='<%# Eval("IDCHAMP")%>' data-typage='<%# Eval("TYPAGE")%>'></asp:DropDownList>
                                                <p class="help-block"></p>

                                                <asp:HiddenField ID="hiddenEnrIdChamp" runat="server" Value='<%# Eval("IDCHAMP")%>' />
                                                <asp:HiddenField ID="hiddenEnrNomChamp" runat="server" Value='<%# Eval("NOMCHAMP")%>' />
                                                <asp:HiddenField ID="hiddenEnrTypage" runat="server" Value='<%# Eval("TYPAGE")%>' />

                                            </ItemTemplate>

                                        </asp:Repeater>

                                    </div>

                                    <asp:Button ID="btnValiderEnrichissement" runat="server" Text="Enregistrer les enrichissements" class="btn btn-primary btn-block" CausesValidation="True" UseSubmitBehavior="False" Enabled="True" />

                                </div>

                            </div>

                            <asp:Literal ID="litValorisationAutomatique" runat="server"></asp:Literal>


                        </div>

                    </div>

                </asp:Panel>

                <asp:Panel ID="pnlBascule" runat="server">

                    <div class="panel panel-default">

                        <div class="panel-heading panel-heading-custom">
                            <i class="fa fa-keyboard-o fa-fw"></i>&nbsp;Bascule de statut
                        </div>

                        <div class="panel-body">

                            <div class="row">

                                <div class="col-lg-12">

                                    <div class="alert alert-success">
                                        Si vous souhaitez traiter à nouveau cette facture, veuillez choisir un statut ci-dessous :
                                    </div>

                                </div>

                            </div>

                            <div class="row" style="margin-top: 5px;">

                                <div class="col-lg-12">

                                    <label>Statut</label>
                                    <asp:DropDownList ID="ddlStatutBascule" runat="server" CssClass="form-control"></asp:DropDownList>

                                </div>

                            </div>

                            <div class="row" style="margin-top: 5px;">

                                <div class="col-lg-12">

                                    <button id="open-modal-confirm-bascule-statut" class="btn btn-primary btn-block" type="button" data-toggle="modal" data-target="#modal-confirm-bascule-statut">Basculer</button>

                                </div>

                            </div>

                        </div>

                    </div>

                </asp:Panel>

            </div>
        </div>

        <asp:Panel ID="pnlLignesFacture" runat="server">

            <div class="row">

                <div class="panel panel-default">

                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-keyboard-o fa-fw"></i>&nbsp;Lignes de facture
                    </div>

                    <div class="panel-body">

                        <div class="row">
                            <div class="col-lg-12">
                                <div id="messageLignes"></div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                <asp:Literal ID="litLignesFacture" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                <button id="btnAjouterLigne" type="button" class="btn btn-primary pull-left"><i class="fa fa-plus"></i>&nbsp;Ajouter une ligne</button>
                                <button id="btnEnregistrerLignes" type="button" class="btn btn-success pull-right">Enregistrer les modifications</button>
                            </div>
                        </div>

                    </div>

                </div>

            </div>

        </asp:Panel>

        <asp:Panel ID="pnlAjoutDocuments" runat="server">

            <div class="row">

                <div class="panel panel-default">

                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-keyboard-o fa-fw"></i>&nbsp;Ajout de documents complémentaires
                    </div>

                    <div class="panel-body">

                        <div class="row">

                            <div class="col-lg-12">

                                <label class="control-label">Selection du fichier</label>
                                <input id="fileUpload" name="fileUpload[]" type="file" multiple="multiple" class="file file-loading" />

                            </div>

                        </div>

                        <div class="row" style="margin-top: 5px;">

                            <div class="col-lg-12">

                                <table style="width: 100%" class="table table-striped table-bordered table-hover" id="dataTables-fichier">
                                    <thead>
                                        <tr>
                                            <th>Nom du fichier</th>
                                            <th>Date d'ajout</th>
                                            <th>Ajouté par</th>
                                            <th>Supprimer</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Literal ID="litFichiersAjoutes" runat="server"></asp:Literal>
                                    </tbody>
                                </table>

                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </asp:Panel>

        <div class="row">

            <div class="col-lg-12">

                <div class="row">

                    <div class="panel panel-default">

                        <div class="panel-heading panel-heading-custom">
                            <i class="fa fa-bell-o fa-fw"></i>&nbsp;Alertes
                        </div>

                        <div class="panel-body">

                            <div class="row">

                                <div class="col-lg-12">

                                    <table style="width: 100%" class="table table-striped table-bordered table-hover" id="dataTables-alertes">
                                        <thead>
                                            <tr>
                                                <th>Motif</th>
                                                <th>Commentaire</th>
                                                <th>Emetteur</th>
                                                <th>Date d'alerte</th>
                                                <th>Résolu par</th>
                                                <th>Date de résolution</th>
                                                <th>Résolu</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Literal ID="litAlertes" runat="server"></asp:Literal>
                                        </tbody>
                                    </table>

                                </div>

                            </div>

                            <button id="open-modal-alerte" type="button" class="btn btn-primary pull-right" data-toggle="modal" data-target="#modal-alerte">Ajouter une alerte</button>

                        </div>

                    </div>

                </div>

                <div class="row">

                    <div class="panel panel-default">

                        <div class="panel-heading panel-heading-custom">
                            <i class="fa fa-keyboard-o fa-fw"></i>&nbsp;Historique de la facture
                        </div>

                        <div class="panel-body">

                            <div class="row">

                                <div class="col-lg-12">

                                    <table style="width: 100%" class="table table-striped table-bordered table-hover" id="dataTables-historique">
                                        <thead>
                                            <tr>
                                                <th>Date</th>
                                                <th>Motif</th>
                                                <th>Utilisateur</th>
                                                <th>Champ</th>
                                                <th>Valeur précédente</th>
                                                <th>Valeur de remplacement</th>
                                            </tr>
                                        </thead>
                                        <tbody id="lignes-historique">
                                        </tbody>
                                    </table>

                                </div>

                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>

        <div class="row" style="margin-bottom: 25px; padding: 0px;">

            <div class="col-lg-12" style="padding: 0px;">
                <asp:Button ID="btnRetour" runat="server" CssClass="btn btn-lg btn-default pull-left" Text="Retour" />

                <button type="button" runat="server" id="openModalConfirmValidationFacture" data-toggle="modal" data-target="#modal-confirm-validation-facture" class="btn btn-success btn-lg pull-right" style="margin-left: 20px;">
                    <asp:Literal ID="litButtonValidation" runat="server"></asp:Literal>
                </button>

                <button type="button" runat="server" id="openModalConfirmInvalidationFacture" data-toggle="modal" data-target="#modal-confirm-invalidation-facture" class="btn btn-danger btn-lg pull-right">
                    Invalider la facture
                </button>

            </div>

        </div>

    </form>

    <div class="modal fade" id="modal-alerte" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog" role="document">

            <div class="modal-content">

                <form id="form-alerte" method="post" action="Handlers/GestionAlerte.ashx">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Ajout d'une Alerte</h4>
                    </div>

                    <div class="modal-body">

                        <div class="row">

                            <div class="col-lg-4">Facture N°</div>
                            <div class="col-lg-8">
                                <input type="text" class="form-control" id="modal-alerte-numFacture" name="modal-alerte-numFacture" readonly="readonly" />
                            </div>

                        </div>

                        <div class="row" style="margin-top: 5px">

                            <div class="col-lg-4">Date de facturation</div>
                            <div class="col-lg-8">
                                <input type="text" class="form-control" id="modal-alerte-dateFacturation" name="modal-alerte-dateFacturation" readonly="readonly" />
                            </div>

                        </div>

                        <div class="row" style="margin-top: 5px">

                            <div class="col-lg-4">DOCID</div>
                            <div class="col-lg-8">
                                <input type="text" class="form-control" id="modal-alerte-docid" name="modal-alerte-docid" readonly="readonly" />
                            </div>

                        </div>

                        <div class="row" style="margin-top: 5px">

                            <div class="col-lg-4">Motif de l'alerte</div>
                            <div class="col-lg-8">
                                <select id="modal-alerte-motif" name="modal-alerte-motif" class="form-control">
                                    <asp:Literal ID="litMotifsAlerte" runat="server"></asp:Literal>
                                </select>
                            </div>

                        </div>

                        <div class="row" style="margin-top: 5px">

                            <div class="col-lg-12">Destinataires</div>

                        </div>

                        <div class="row" style="margin-top: 5px">

                            <div class="col-lg-12">
                                <input type="text" class="form-control" style="height: 150px; resize: none;" id="modal-alerte-destinataires" name="modal-alerte-destinataires" maxlength="1000" />
                            </div>

                        </div>

                        <div class="row" style="margin-top: 5px">

                            <div class="col-lg-8">
                                <input type="text" class="form-control" id="modal-alerte-ajout-destinataire" name="modal-alerte-ajout-destinataire" maxlength="1000" />
                            </div>
                            <div class="col-lg-4">
                                <button type="button" id="btnAJouterDestinataire" class="btn btn-primary btn-block">Ajouter</button>
                            </div>

                        </div>

                        <div class="row" style="margin-top: 5px">

                            <div class="col-lg-12">Commentaire</div>

                        </div>

                        <div class="row" style="margin-top: 5px">

                            <div class="col-lg-12">
                                <textarea class="form-control" style="height: 150px; resize: none;" id="modal-alerte-commentaire" name="modal-alerte-commentaire" maxlength="4000"></textarea>
                            </div>

                        </div>

                    </div>

                    <div class="modal-footer">

                        <div class="col-lg-12">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Annuler</button>
                            <button type="submit" class="btn btn-primary pull-right" id="btnValider">Valider</button>
                        </div>

                    </div>

                </form>

            </div>

        </div>

    </div>

    <div class="modal fade" id="modal-confirm-suppression-fichier" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog">

            <div class="modal-content">

                <form id="form-confirm-suppression-fichier" method="post" action="handlers/SuppressionFichierAjoute.ashx">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Confirmation de suppression</h4>
                    </div>

                    <div class="modal-body">
                        <p>Confirmez-vous la suppression du fichier ?</p>
                        <input type="hidden" name="mId" id="mId" value="" />
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Non</button>
                        <button id="btnModalConfirmSuppFiltre" type="submit" class="btn btn-primary btn-ok">Oui</button>
                    </div>

                </form>

            </div>

        </div>

    </div>

    <div class="modal fade" id="modal-confirm-validation-facture" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog">

            <div class="modal-content">

                <form id="form-confirm-validation-facture" method="post" action="handlers/ValidationFacture.ashx">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Traitement de la facture</h4>
                    </div>

                    <div class="modal-body">
                        <p><span id="mMessage"></span></p>
                        <input type="hidden" name="mdocid" id="mdocid_valide" value="" />
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Annuler</button>
                        <button id="btnModalConfirmValidationFacture" type="submit" class="btn btn-primary btn-ok">Confirmer</button>
                    </div>

                </form>

            </div>

        </div>

    </div>

    <div class="modal fade" id="modal-confirm-invalidation-facture" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog">

            <div class="modal-content">

                <form id="form-confirm-invalidation-facture" method="post" action="handlers/InvalidationFacture.ashx">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Invalidation de la facture</h4>
                    </div>

                    <div class="modal-body">

                        <div class="row">
                            <div class="col-lg-12">

                                <label>Motif d'invalidité</label>
                                <select id="motif_invalidite" name="mMotifInvalidite" class="form-control">
                                    <option value=""></option>
                                </select>

                            </div>
                        </div>

                        <div class="row">

                            <div class="col-lg-12">

                                <label>Commentaire</label>
                                <textarea id="commentaire_invalidite" name="mCommentaireInvalidite" class="form-control" style="height: 150px; resize: none;" maxlength="500"></textarea>

                            </div>

                        </div>

                        <div class="row">

                            <div class="col-lg-12">

                                <span id="erreur_invalidite"></span>

                            </div>

                        </div>

                        <input type="hidden" name="mdocid" id="mdocid_invalide" value="" />

                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Annuler</button>
                        <button id="btnModalConfirmInvalidationFacture" type="submit" class="btn btn-primary btn-ok">Confirmer</button>
                    </div>

                </form>

            </div>

        </div>

    </div>

    <div class="modal fade" id="modal-confirm-bascule-statut" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog">

            <div class="modal-content">

                <form id="form-confirm-bascule-statut" method="post" action="handlers/BasculeStatut.ashx">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Bascule du statut</h4>
                    </div>

                    <div class="modal-body">
                        <p><span id="bascule-message"></span></p>
                        <input type="hidden" name="bascule-docid" id="bascule-docid" value="" />
                        <input type="hidden" name="bascule-statut" id="bascule-statut" value="" />
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Annuler</button>
                        <button id="btnModalBasculeStatut" type="submit" class="btn btn-primary btn-ok">Confirmer</button>
                    </div>

                </form>

            </div>

        </div>

    </div>

    <div class="modal fade" id="modal-confirm-supp-ligne" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog">

            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                    <h4 class="modal-title">Suppression de ligne</h4>
                </div>

                <div class="modal-body">
                    <p>Confirmez-vous la suppression de la ligne ?</p>
                    <input type="hidden" name="supp-id-ligne" id="supp-id-ligne" value="" />
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Annuler</button>
                    <button id="btnConfirmSupprimerLigne" type="button" class="btn btn-primary btn-ok">Confirmer</button>
                </div>

            </div>

        </div>

    </div>

    <div id="modal-visualisation-fichier" class="modal fade">

        <div class="modal-dialog">

            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span> <span class="sr-only">close</span></button>
                    <h4 class="modal-title" id="modal-visualisation-fichier-titre">Visualisation du fichier</h4>
                </div>

                <div class="modal-body">
                    <iframe class="cadre_pdf" id="iFrameFichierAjoute"></iframe>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Fermer</button>
                </div>

            </div>

        </div>
    </div>

   <div id="modal-rejet" class="modal fade">
        <div class="modal-dialog">
            <form method="post" id="form-rejet" action="handlers/GestionRejet.ashx" >
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span> <span class="sr-only">close</span></button>
                        <h4 class="modal-title" id="modal-rejet-titre-general">Nouveau rejet</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-3">
                                Libellé du rejet : 
                            </div>
                            <div class="col-lg-9"><input id="modal-rejet-titre" name="modal-rejet-titre" type="text" class="form-control" maxlength="200"/></div>
                        </div> 
                        <div class="row">
                            <div class="col-lg-12">
                                <input type="checkbox" id="modal-rejet-type" name="modal-rejet-type" />
                                <label for="modal-rejet-type">Souhaitez-vous que ce rejet soit bloquant ?</label>
                            </div>
                        </div>                            
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Annuler</button>
                        <button class="btn btn-primary" id="btnValiderRejet">Valider</button>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <script type="text/javascript">
        var workflow =  $("#<%=HiddenWorkflow.ClientID%>").val();
        var docid = $("#<%=HiddenDocID.ClientID%>").val();
        var statut = $("#<%=HiddenStatut.ClientID%>").val();
        var flagCorrection = $("#<%=HiddenFlagCorrection.ClientID%>").val();
        var flagEnrichissement = $("#<%=HiddenFlagEnrichissement.ClientID%>").val();
        var numFacture = $("#<%=HiddenNumFacture.ClientID%>").val();
        var dateFacture = $("#<%=HiddenDateFacture.ClientID%>").val();

        function getHistorique(p_docid){

            $.ajax({
                type: 'POST',
                url: 'Handlers/JsonHistorique.ashx',
                data: 'id=' + p_docid,
                dataType: "json",
                success: function (response) {
                    $("#lignes-historique").empty();
                    var lignes = "";
                    $.each(response, function(i, item) {
                        lignes = lignes + '<tr class="odd gradeX"><td>'+item.DateAction+'</td><td>'+item.Action+'</td><td>'+item.NomUtilisateur+'</td><td>'+item.LibelleChamp+'</td><td>'+item.AncienneValeur+'</td><td>'+item.NouvelleValeur+'</td></tr>';      
                    });
                    $("#lignes-historique").html(lignes);
                },
                error: function () {
                }
            });
        };

        function CalculerCollectif(id) {

            var compteComptable = $("#" + id).val();
            var idChampValorise = $("#" + id).data('champvalorise');
            var idLigne = $("#" + id).data('ligne');

            $.ajax({
                type: 'POST',
                url: 'Handlers/CalculCollectif.ashx',
                data: 'comptecomptable=' + compteComptable,
                dataType: "text",
                success: function (response) {

                    $(".champ-ligne").each(function () {

                        if (($(this).data("ligne") == idLigne) && ($(this).data("champ") == idChampValorise)) {
                            $(this).val(response);  
                        }

                    });

                },
                error: function () {

                    alert("erreur");

                }

            });

        };

        $("select[id*='txtChampLigne_']").on('change', function () {
            CalculerCollectif(this.id)
        });


        $("#fileUpload").fileinput({
            language: "fr",
            uploadUrl: "handlers/UploadHandler.ashx?docid=" + docid,
            allowedFileExtensions: ["pdf"]
        });

        $("#fileUpload").on('fileuploaded', function (event, data, previewId, index) {
            location.reload();
        });

        $(document).ready(function () {

            getHistorique(docid);

            $(document).on("click", ".checkbox_alerte_resolue", function () {

                var id_alerte = $(this).data("id");

                $.ajax({
                    type: 'GET',
                    async: false,
                    url: 'Handlers/UpdateStatutAlerte.ashx?id=' + id_alerte,
                    success: function () {
                        location.reload();
                    }
                });

            });


            $(document).on("click", "#open-modal-alerte", function () {
                $("#modal-alerte-numFacture").val(numFacture);
                $("#modal-alerte-dateFacturation").val(dateFacture);
                $("#modal-alerte-docid").val(docid);
            });

            $(document).on("click", "#open-modal-confirm-suppression-fichier", function () {
                var id = $(this).data('id');
                $("#modal-confirm-suppression-fichier #mId").val(id);
            });

            if (statut == "0" || statut == "1") {
                $("#<%=openModalConfirmValidationFacture.ClientID%>").hide();
        }

            $(document).on("click", "#<%=openModalConfirmValidationFacture.ClientID%>", function () {

                $("#mdocid_valide").val(docid);

                if ($("#<%=HiddenProfil.ClientID%>").val() == "3") {

                    $("#mMessage").html("Confirmez-vous la validation de la facture ?");

                } else {

                    if (statut == "4" && (flagCorrection != "1" || flagEnrichissement != "1")) {

                        $("#btnModalConfirmValidationFacture").hide();

                        if (flagCorrection != "1") {
                            $("#mMessage").html("Aucune action en correction n'a encore été effectuée.");
                        } else if (flagEnrichissement != "1") {
                            $("#mMessage").html("Aucune action en enrichissement n'a encore été effectuée.");
                        }

                    } else if (statut == "3" && flagEnrichissement != "1") {
                        $("#mMessage").html("Aucune action d'enrichissement n'a été effectuée.");
                        $("#btnModalConfirmValidationFacture").hide();


                    } else if (statut == "2" && flagCorrection != "1") {
                         
                        $("#mMessage").html("Aucune action de correction n'a été effectuée.");
                        $("#btnModalConfirmValidationFacture").hide();


                    }
                    else if ($('#ContentPlaceHolder1_RepeaterCorrection_ddlCorChamp_27').val() != "") {
                        $("#mMessage").html("Le rejet n'a pas été corrigé.");
                        $("#btnModalConfirmValidationFacture").hide();
                    }
                    else {
                        $("#mMessage").html("Confirmez-vous cette action ?");
                        $("#btnModalConfirmValidationFacture").show();
                    }

                }

            });

            $(document).on("click", "#<%=openModalConfirmInvalidationFacture.ClientID%>", function () {

                $("#mdocid_invalide").val(docid);
                $("#motif_invalidite").empty;
                $("#commentaire_invalidite").val("");

                $.ajax({
                    type: 'POST',
                    url: 'Handlers/JsonMotifsInvalidite.ashx',
                    dataType: "json",
                    success: function (response) {

                        var items = '';
                        $.each(response, function (key, value) {
                            items += '<option value=' + value.Id + '>' + value.Libelle + '</option>';
                        });

                        $("#motif_invalidite").append(items);

                    }

                });

            });

            $(document).on("click", "#btnModalConfirmInvalidationFacture", function () {

                if ($.trim($("#motif_invalidite").val()) == "") {

                    $("#motif_invalidite").css("background-color", '#F2DEDE');
                    $("#erreur_invalidite").html('<div class="alert alert-danger">Veuillez choisir un motif.</div>');

                    return false;

                } else {
                    $("#motif_invalidite").css("background-color", 'white');
                    $("#erreur_invalidite").html("");
                }

            });

            $(document).on("click", "#open-modal-confirm-bascule-statut", function () {

                var statut_bascule = $("#<%=ddlStatutBascule.ClientID%>").val();

                $("#bascule-docid").val(docid);
                $("#bascule-statut").val(statut_bascule);
                $("#btnModalBasculeStatut").show();

                if (statut_bascule == "2") {
                    $("#bascule-message").html("Confirmez-vous la bascule en CORRECTION ?");

                } else if (statut_bascule == "3") {
                    $("#bascule-message").html("Confirmez-vous la bascule en ENRICHISSEMENT ?");

                } else if (statut_bascule == "4") {
                    $("#bascule-message").html("Confirmez-vous la bascule en CORRECTION ET ENRICHISSEMENT ?");

                } else {
                    $("#bascule-message").html("Veuillez choisir un statut.");
                    $("#btnModalBasculeStatut").hide();
                }


            });

            $(document).on("click", "#btnEnregistrerLignes", function () {

                var json_values = "";

                $(".champ-ligne").each(function () {

                    var id_ligne = $(this).data("ligne");
                    var id_champ = $(this).data("champ");
                    var valeur = $(this).val().replace("'", "''").replace('"', '');


                    json_values = json_values + '{"IdFacture":"' + docid + '","IdLigne":"' + id_ligne + '","IdChamp":"' + id_champ + '","Valeur":"' + valeur + '"},';

                });

                json_values = "[" + json_values.substring(0, json_values.length - 1) + "]"

                $.ajax({
                    type: 'POST',
                    url: 'Handlers/EnregistrerLignes.ashx',
                    data: json_values,
                    dataType: "text",
                    success: function (response) {

                        if (response == "1") {
                            $("#messageLignes").html('<div class="alert alert-success alert-dismissible"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>Les lignes ont été mises à jour avec succés.</div>')
                        } else {
                            $("#messageLignes").html('<div class="alert alert-danger alert-dismissible"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>Une erreur est survenue lors de la mise à jour des lignes.</div>')

                        }

                        getHistorique(docid);

                    },
                    error: function () {
                        $("#messageLignes").html('<div class="alert alert-danger alert-dismissible><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>Une erreur est survenue lors de la mise à jour des lignes.</div>')
                    }

                });



            });

            $(document).on("click", ".supp-ligne", function () {

                $("#supp-id-ligne").val($(this).data("ligne"));

            });

            $(document).on("click", "#btnConfirmSupprimerLigne", function () {

                $("#modal-confirm-supp-ligne").modal("hide");
                var id_ligne = $("#supp-id-ligne").val();

                $.ajax({
                    type: 'POST',
                    url: 'Handlers/SuppressionLigne.ashx',
                    data: 'idligne=' + id_ligne + '&idfacture=' + docid,
                    dataType: "text",
                    success: function (response) {

                        if (response == "1") {

                            $("#index-" + id_ligne).remove();


                        } else {
                            $("#messageLignes").html('<div class="alert alert-danger alert-dismissible"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>Une erreur est survenue lors de la suppression de la ligne.</div>')

                        }

                        getHistorique(docid);

                    },
                    error: function () {
                        $("#messageLignes").html('<div class="alert alert-danger alert-dismissible><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>Une erreur est survenue lors de la suppression de la ligne.</div>')
                    }

                });
                
            });

            $(document).on("click", "#btnAjouterLigne", function () {

                $.ajax({
                    type: 'POST',
                    url: 'Handlers/AjouterLigne.ashx',
                    data: 'id=' + docid,
                    dataType: "text",
                    success: function (response) {

                        if ($.trim(response) != "") {

                            $("#tableLignesFacture > tbody:last").append(response);

                        } else {


                        }

                        getHistorique(docid);

                    },
                    error: function () {

                    }

                });

            });

            $(document).on("click", "#chkValAuto", function () {

                var json_values = "";

                $(".champ-automatisation").each(function () {

                    var id_champ = $(this).data("id");
                    var typage = $(this).data("typage");
                    var valeur = $(this).val().replace("'", "''").replace('"', '');

                    if ($.trim(valeur) != "") {
                        json_values = json_values + '{"IdFacture":"' + docid + '","IdChamp":"' + id_champ + '","Typage":"' + typage + '","Valeur":"' + valeur + '"},';
                    }

                });

                json_values = "[" + json_values.substring(0, json_values.length - 1) + "]";

                if ($("#chkValAuto").is(':checked')) {

                    if (json_values != "[]") {

                        $.ajax({
                            type: 'POST',
                            url: 'Handlers/EnregistrerValorisationAutomatique.ashx',
                            data: json_values,
                            dataType: "text",
                            success: function (response) {

                                if (response == "1") {
                                    $("#messageValorisation").html('<div class="alert alert-success alert-dismissible"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>Automatisation enregistrée.</div>')
                                } else {
                                    $("#messageValorisation").html('<div class="alert alert-danger alert-dismissible"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>Automatisation non enregistrée.</div>')

                                }

                                getHistorique(docid);

                            },
                            error: function () {
                                $("#messageValorisation").html('<div class="alert alert-danger alert-dismissible><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>Automatisation non enregistrée.</div>')
                            }

                        });

                    }

                } else {

                    $.ajax({
                        type: 'POST',
                        url: 'Handlers/SupprimerValorisationAutomatique.ashx',
                        data: json_values,
                        dataType: "text",
                        success: function (response) {

                            if (response == "1") {
                                $("#messageValorisation").html('<div class="alert alert-success alert-dismissible"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>Automatisation supprimée.</div>')
                            } else {
                                $("#messageValorisation").html('<div class="alert alert-danger alert-dismissible"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>Automatisation non supprimée.</div>')

                            }

                            getHistorique(docid);

                        },
                        error: function () {
                            $("#messageValorisation").html('<div class="alert alert-danger alert-dismissible><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>Automatisation non supprimée.</div>')
                        }

                    });

                }

            });


            $('#dataTables-fichier tbody tr td:not(:last-child)').click(function () {

                var id = $(this).data("id");
                var url = "handlers/VisualisationFichierAjoute.ashx?id=" + id;

                $("#iFrameFichierAjoute").prop('src', url);
                $("#modal-visualisation-fichier").modal();

            });

        });

        function validateEmail(input) {
            var re = /\S+@\S+\.\S+/;
            var valid = re.test(input);
            return re.test(input);
        }

        $('#modal-alerte-destinataires').tagsinput({
            freeInput: false
        });

        var _wasPageCleanedUp = false;

        function pageCleanup() {
            if (!_wasPageCleanedUp) {
                $.ajax({
                    type: 'GET',
                    async: false,
                    url: 'Handlers/flagOccupation.ashx?docid=' + docid + '&valeur=',
                    success: function () {
                        _wasPageCleanedUp = true;
                    }
                });
            }
        }


        $(window).on('beforeunload', function () {
            //this will work only for Chrome
            pageCleanup();
        });

        $(window).on("unload", function () {
            //this will work for other browsers
            pageCleanup();
        });

        $("#btnAJouterDestinataire").on("click", function () {

            var dest = $('#modal-alerte-ajout-destinataire').val()

            if (validateEmail(dest)) {
                $('#modal-alerte-destinataires').tagsinput('add', dest);
                $('#modal-alerte-ajout-destinataire').val("");
            }

        });

        $('#modal-alerte-ajout-destinataire').autocomplete({

            source: function (request, response) {

                $.ajax({

                    type: "get",
                    url: 'Handlers/JsonSuggessionEmail.ashx?param=' + request.term,
                    dataType: 'json',
                    success: function (data) {
                        response($.map(data, function (value, key) {
                            return value;
                        }))
                    },

                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //alert(textStatus);
                    }

                });

            },
            minLength: 2

        });



    </script>

</asp:Content>
