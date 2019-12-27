<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" EnableEventValidation="false" CodeBehind="TraitementHorsScope.aspx.vb" Inherits="WORKFLOW_FACTURE.TraitementHorsScope" %>

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
            <div class="col-lg-8">
                <div class="row">
                    <iframe id="iframePDF" runat="server" class="cadre_pdf"></iframe>
                </div>
            </div>
            <div class="col-lg-4">
                <asp:Panel ID="pnlCorrection" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-heading panel-heading-custom">
                            <i class="fa fa-bell-o fa-fw"></i>&nbsp;Correction
                        </div>
                        <div class="panel-body">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlTypeDocument" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                    <asp:DropDownList ID="ddlSociete" runat="server" CssClass="form-control" Enabled="true" AutoPostBack="true"></asp:DropDownList>
                                    <asp:DropDownList ID="ddlStatut2" runat="server"  CssClass="form-control" Enabled="true" AutoPostBack="true"></asp:DropDownList>
                                    <asp:DropDownList ID="ddlSite2" runat="server"  CssClass="form-control" Enabled="true" AutoPostBack="true"></asp:DropDownList>
                                    <asp:Button ID="btnValiderCorrection" runat="server" Text="Enregistrer les corrections" class="btn btn-primary btn-block" CausesValidation="False" UseSubmitBehavior="False" Enabled="True" />
                                </ContentTemplate>                          
                            </asp:UpdatePanel>
                       
                        </div>
                    </div>
                </asp:Panel>
            </div>

           

            <asp:Panel ID="pnlAlertes" runat="server">
                <div class="col-lg-12">
                    
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
            </asp:Panel>
        </div>

        <div class="row" style="margin-bottom: 25px; padding: 0px;">

            <div class="col-lg-12" style="padding: 0px;">
                <asp:Button ID="btnRetour" runat="server" CssClass="btn btn-lg btn-default pull-left" Text="Retour" />

               <%-- <button type="button" runat="server" id="openModalConfirmValidationFacture" data-toggle="modal" data-target="#modal-confirm-validation-facture" class="btn btn-success btn-lg pull-right" style="margin-left: 20px;">
                    Valider le document
                </button>--%>

            </div>

        </div>

    </form>

    <div class="modal fade" id="modal-alerte" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog" role="document">

            <div class="modal-content">

                <form id="form-alerte" method="post" action="Handlers/GestionAlerteHorsScope.ashx">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Ajout d'une Alerte</h4>
                    </div>

                    <div class="modal-body">
                    
                        <div class="row" style="margin-top: 5px">

                            <div class="col-lg-4">Id du document</div>
                            <div class="col-lg-8">
                                <input type="text" class="form-control" id="modal-alerte-docid" name="modal-alerte-docid" readonly="readonly" />
                            </div>

                        </div>

                        <div class="row" style="margin-top: 5px">

                            <div class="col-lg-4">Motif de l'alerte</div>
                            <div class="col-lg-8">
                                <%--<input type="text" class="form-control" id="modal-alerte-motif" name="modal-alerte-motif" />--%>
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



    <div class="modal fade" id="modal-confirm-validation-facture" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog">

            <div class="modal-content">

                <form id="form-confirm-validation-facture" method="post" action="handlers/ValidationDocumentHorsScope.ashx">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Traitement du document</h4>
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

    <script type="text/javascript">
        var workflow =  $("#<%=HiddenWorkflow.ClientID%>").val();
        var docid = $("#<%=HiddenDocID.ClientID%>").val();
        var statut = $("#<%=HiddenStatut.ClientID%>").val();



        $(document).ready(function () {


            $(document).on("click", "#open-modal-alerte", function () {
                $("#modal-alerte-docid").val(docid);
            });


           <%-- if (statut == "7") {
                $("#<%=openModalConfirmValidationFacture.ClientID%>").hide();
            }

            $(document).on("click", "#<%=openModalConfirmValidationFacture.ClientID%>", function () {
                $("#mdocid_valide").val(docid);
                $("#mMessage").html("Confirmez-vous la validation du document ?");
            });--%>

            $(document).on("click", ".checkbox_alerte_resolue", function () {

                var id_alerte = $(this).data("id");

                $.ajax({
                    type: 'GET',
                    async: false,
                    url: 'Handlers/UpdateStatutAlerte.ashx?id=' + id_alerte + "&hs=1",
                    success: function () {
                        location.reload();
                    }
                });

            });
        });

        $("#<%=ddlTypeDocument.ClientID%>").change(function () {
            $(this).
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
