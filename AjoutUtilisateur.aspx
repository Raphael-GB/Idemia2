<%@ Page Title="" Language="vb" EnableEventValidation="false" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" CodeBehind="AjoutUtilisateur.aspx.vb" Inherits="WORKFLOW_FACTURE.AjoutUtilisateur" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">
        <div class="row form-group" style="padding-top: 15px;">
            <div class="col-lg-12">
                <ul class="nav nav-pills nav-justified thumbnail setup-panel">
                    <li class="active"><a href="#step-1">
                        <h4 class="list-group-item-heading">Étape 1</h4>
                        <p class="list-group-item-text">Accessibilité</p>
                    </a></li>
                    <li class="disabled"><a href="#step-2">
                        <h4 class="list-group-item-heading">Étape 2</h4>
                        <p class="list-group-item-text">Informations générales</p>
                    </a></li>
                    <li class="disabled"><a href="#step-3">
                        <h4 class="list-group-item-heading">Étape 3</h4>
                        <p class="list-group-item-text">Paramétrage des champs</p>
                    </a></li>
                    <li class="disabled"><a href="#step-4">
                        <h4 class="list-group-item-heading">Étape 4</h4>
                        <p class="list-group-item-text">Affection des filtres</p>
                    </a></li>
                    <li class="disabled"><a href="#step-5">
                        <h4 class="list-group-item-heading">Étape 5</h4>
                        <p class="list-group-item-text">Affection des alertes</p>
                    </a></li>
                </ul>
            </div>
        </div>

        <div class="row setup-content" id="step-1">

            <div class="col-lg-10">

                <div class="panel panel-default">

                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-user fa-fw"></i>&nbsp;Accessibilité
                    </div>

                    <div class="panel-body">

                        <div class="row">

                            <div class="col-lg-12">

                                <div class="row">
                                    <div class="col-lg-3">
                                        <label>identifiant</label>
                                    </div>
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtIdentifiant" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4">
                                        <span id="messageIdentifiant"></span>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px">
                                    <div class="col-lg-3">
                                        <label>Mot de passe</label>
                                    </div>
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtMdp" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4">
                                        <span id="messageMdp"></span>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px">
                                    <div class="col-lg-3">
                                        <label>Confirmation</label>
                                    </div>
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtConfirmMdp" runat="server" class="form-control" TextMode="Password"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4">
                                        <span id="messageConfirmMdp"></span>
                                    </div>
                                </div>

                                <input type="hidden" id="flag-erreur-identifiant-step-1" value="" />
                                <input type="hidden" id="flag-erreur-mdp-step-1" value="" />

                            </div>

                        </div>

                    </div>

                </div>

                <button type="button" id="activate-step-2" class="btn btn-primary pull-right">
                    Étape suivante &nbsp; <i class="fa fa-arrow-circle-right"></i>
                </button>

            </div>

        </div>

        <div class="row setup-content" id="step-2">

            <div class="col-lg-10">

                <div class="panel panel-default">

                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-info-circle fa-fw"></i>&nbsp;Informations générales
                    </div>

                    <div class="panel-body">

                        <div class="row">

                            <div class="col-lg-12">

                                <div class="row">
                                    <div class="col-lg-3">
                                        <label>Civilité</label>
                                    </div>
                                    <div class="col-lg-5">
                                        <asp:DropDownList ID="ddlCivilite" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                        <span id="messageCivilite"></span>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px">
                                    <div class="col-lg-3">
                                        <label>Nom</label>
                                    </div>
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtNom" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4">
                                        <span id="messageNom"></span>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px">
                                    <div class="col-lg-3">
                                        <label>Prénom</label>
                                    </div>
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtPrenom" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4">
                                        <span id="messagePrenom"></span>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px">
                                    <div class="col-lg-3">
                                        <label>Email</label>
                                    </div>
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtEmail" runat="server" class="form-control" TextMode="Email"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4">
                                        <span id="messageEmail"></span>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px">
                                    <div class="col-lg-3">
                                        <label>Fonction</label>
                                    </div>
                                    <div class="col-lg-5">
                                        <asp:TextBox ID="txtFonction" runat="server" class="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-4">
                                        <span id="messageFonction"></span>
                                    </div>
                                </div>

                                <input type="hidden" id="flag-erreur-step-2" value="" />

                            </div>

                        </div>

                    </div>

                </div>

                <button type="button" id="activate-step-1-prec" class="btn btn-primary pull-left">
                    <i class="fa fa-arrow-circle-left"></i>&nbsp;Étape précédente 
                </button>

                <button type="button" id="activate-step-3" class="btn btn-primary pull-right">
                    Étape suivante &nbsp; <i class="fa fa-arrow-circle-right"></i>
                </button>

            </div>

        </div>

        <div class="row setup-content" id="step-3" style="margin-bottom: 15px;">

            <div class="col-lg-12">

                <div class="row">

                    <div class="col-lg-12">

                        <div class="panel panel-default">
                            <div class="panel-heading panel-heading-custom">
                                <i class="fa fa-keyboard-o fa-fw"></i>&nbsp;Champs à enrichir 
                            </div>

                            <div class="panel-body">
                                <asp:ListBox ID="listeEnrichissement" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>

                        </div>

                    </div>

                </div>

                <div class="row">

                    <div class="col-lg-12">

                        <div class="panel panel-default">
                            <div class="panel-heading panel-heading-custom">
                                <i class="fa fa-keyboard-o fa-fw"></i>&nbsp;Champs à corriger
                            </div>

                            <div class="panel-body">
                                <asp:ListBox ID="listeCorrection" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>

                        </div>

                    </div>

                </div>

                <button type="button" id="activate-step-2-prec" class="btn btn-primary pull-left">
                    <i class="fa fa-arrow-circle-left"></i>&nbsp;Étape précédente 
                </button>

                <button type="button" id="activate-step-4" class="btn btn-primary pull-right">
                    Étape suivante &nbsp; <i class="fa fa-arrow-circle-right"></i>
                </button>

            </div>

        </div>

        <div class="row setup-content" id="step-4" style="margin-bottom: 15px;">

            <div class="col-lg-12">

                <div class="row">

                    <div class="col-lg-12">

                        <div class="panel panel-default">
                            <div class="panel-heading panel-heading-custom">
                                <i class="fa fa-filter fa-fw"></i>&nbsp;Filtres de sélection
                            </div>

                            <div class="panel-body">
                                <asp:ListBox ID="listeFiltres" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>

                        </div>

                    </div>

                </div>

                <button type="button" id="activate-step-3-prec" class="btn btn-primary pull-left">
                    <i class="fa fa-arrow-circle-left"></i>&nbsp;Étape précédente 
                </button>

                <button type="button" id="activate-step-5" class="btn btn-primary pull-right">
                    Étape suivante &nbsp; <i class="fa fa-arrow-circle-right"></i>
                </button>

            </div>

        </div>

        <div class="row setup-content" id="step-5" style="margin-bottom: 15px;">

            <div class="col-lg-12">

                <div class="row">

                    <div class="col-lg-12">

                        <div class="panel panel-default">
                            <div class="panel-heading panel-heading-custom">
                                <i class="fa fa-bell fa-fw"></i>&nbsp;Alertes automatiques
                            </div>

                            <div class="panel-body">
                                <asp:ListBox ID="listeAlertes" runat="server" SelectionMode="Multiple"></asp:ListBox>
                            </div>

                        </div>

                    </div>

                </div>

                <button type="button" id="activate-step-4-prec" class="btn btn-primary pull-left">
                    <i class="fa fa-arrow-circle-left"></i>&nbsp;Étape précédente 
                </button>

                <asp:Button ID="btnTerminer" runat="server" Text="Terminer" class="btn btn-primary pull-right" />

            </div>

        </div>

    </form>

    <script type="text/javascript">

        function controleIdentifiant() {

            var valeur = $("#<%=txtIdentifiant.ClientID%>").val();

            if (valeur.length > 0) {

                $("#messageIdentifiant").html('Vérification ...');

                $.ajax({
                    type: 'GET',
                    url: 'handlers/VerificationIdentifiant.ashx?valeur=' + valeur,
                    data: {},
                    success: function (data) {

                        $("#flag-erreur-identifiant-step-1").val(data);

                        if (data == "1") {
                            $("#messageIdentifiant").html('<div class="alert alert-danger" style="padding:5px;margin:0">Non disponible</div>');
                        } else {
                            $("#messageIdentifiant").html('<div class="alert alert-success" style="padding:5px;margin:0">Disponible</div>');
                        }

                    }
                });
                return false;

            }
            else {
                $("#flag-erreur-identifiant-step-1").val("1");
                $("#messageIdentifiant").html('<div class="alert alert-danger" style="padding:5px;margin:0">Valeur obligatoire</div>');
            }
        }

        function controleMdp() {

            var valeur_mdp = $("#<%=txtMdp.ClientID%>").val();
            var valeur_confirm = $("#<%=txtConfirmMdp.ClientID%>").val();

            if (valeur_mdp.length > 0 && valeur_confirm.length > 0) {

                if (valeur_mdp == valeur_confirm) {

                    $("#flag-erreur-mdp-step-1").val("0");
                    $("#messageMdp").html('<div class="alert alert-success" style="padding:5px;margin:0">Ok</div>');
                    $("#messageConfirmMdp").html('<div class="alert alert-success" style="padding:5px;margin:0">Ok</div>');

                } else {

                    $("#flag-erreur-mdp-step-1").val("1");
                    $("#messageMdp").html('<div class="alert alert-success" style="padding:5px;margin:0">Ok</div>');
                    $("#messageConfirmMdp").html('<div class="alert alert-danger" style="padding:5px;margin:0">La valeur le correspond pas.</div>');

                }

            } else {

                $("#flag-erreur-mdp-step-1").val("1");
                $("#messageMdp").html('<div class="alert alert-danger" style="padding:5px;margin:0">Valeur obligatoire</div>');
                $("#messageConfirmMdp").html('');

            }
        }

        function controleEtape2() {

            $("#flag-erreur-step-2").val("0");

            if ($("#<%=ddlCivilite.ClientID%>").val() == "") {
                $("#messageCivilite").html('<div class="alert alert-danger" style="padding:5px;margin:0">Valeur obligatoire</div>');
                $("#flag-erreur-step-2").val("1");
            } else {
                $("#messageCivilite").html('<div class="alert alert-success" style="padding:5px;margin:0">Ok</div>');
            }

            if ($("#<%=txtNom.ClientID%>").val() == "") {
                $("#messageNom").html('<div class="alert alert-danger" style="padding:5px;margin:0">Valeur obligatoire</div>');
                $("#flag-erreur-step-2").val("1");
            } else {
                $("#messageNom").html('<div class="alert alert-success" style="padding:5px;margin:0">Ok</div>');
            }

            if ($("#<%=txtPrenom.ClientID%>").val() == "") {
                $("#messagePrenom").html('<div class="alert alert-danger" style="padding:5px;margin:0">Valeur obligatoire</div>');
                $("#flag-erreur-step-2").val("1");
            } else {
                $("#messagePrenom").html('<div class="alert alert-success" style="padding:5px;margin:0">Ok</div>');
            }

            if ($("#<%=txtEmail.ClientID%>").val() == "") {
                $("#messageEmail").html('<div class="alert alert-danger" style="padding:5px;margin:0">Valeur obligatoire</div>');
                $("#flag-erreur-step-2").val("1");
            } else {

                if (IsEmail($("#<%=txtEmail.ClientID%>").val()) == true) {
                    $("#messageEmail").html('<div class="alert alert-success" style="padding:5px;margin:0">Ok</div>');
                } else {
                    $("#messageEmail").html('<div class="alert alert-danger" style="padding:5px;margin:0">Valeur incorrecte</div>');
                    $("#flag-erreur-step-2").val("1");
                }

            }

            if ($("#<%=txtFonction.ClientID%>").val() == "") {
                $("#messageFonction").html('<div class="alert alert-danger" style="padding:5px;margin:0">Valeur obligatoire</div>');
                $("#flag-erreur-step-2").val("1");
            } else {
                $("#messageFonction").html('<div class="alert alert-success" style="padding:5px;margin:0">Ok</div>');
            }

        }

        function IsEmail(email) {
            var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            if (!regex.test(email)) {
                return false;
            } else {
                return true;
            }
        }


        var dualBoxEnrichissement = $("#<%=listeEnrichissement.ClientID%>").bootstrapDualListbox({
            nonSelectedListLabel: 'Champs disponibles',
            selectedListLabel: 'Champs sélectionnés',
            preserveSelectionOnMove: 'moved',
            moveOnSelect: false
        });

        var dualBoxCorrection = $("#<%=listeCorrection.ClientID%>").bootstrapDualListbox({
            nonSelectedListLabel: 'Champs disponibles',
            selectedListLabel: 'Champs sélectionnés',
            preserveSelectionOnMove: 'moved',
            moveOnSelect: false
        });

        var dualBoxFiltres = $("#<%=listeFiltres.ClientID%>").bootstrapDualListbox({
            nonSelectedListLabel: 'Filtres disponibles',
            selectedListLabel: 'Filtres sélectionnés',
            preserveSelectionOnMove: 'moved',
            moveOnSelect: false
        });

        var dualBoxAlertes = $("#<%=listeAlertes.ClientID%>").bootstrapDualListbox({
            nonSelectedListLabel: 'Alertes disponibles',
            selectedListLabel: 'Alertes sélectionnés',
            preserveSelectionOnMove: 'moved',
            moveOnSelect: false
        });

        $(document).ready(function () {

            controleIdentifiant();
            controleMdp();
            controleEtape2();

            var navListItems = $('ul.setup-panel li a'),
                allWells = $('.setup-content');

            allWells.hide();

            navListItems.click(function (e) {
                e.preventDefault();
                var $target = $($(this).attr('href')),
                    $item = $(this).closest('li');

                if (!$item.hasClass('disabled')) {
                    navListItems.closest('li').removeClass('active');
                    $item.addClass('active');
                    allWells.hide();
                    $target.show();
                }
            });

            $('ul.setup-panel li.active a').trigger('click');

            $('#activate-step-2').on('click', function (e) {

                controleIdentifiant();
                controleMdp();

                if ($('#flag-erreur-identifiant-step-1').val() == "0" && $('#flag-erreur-mdp-step-1').val() == "0") {

                    $('ul.setup-panel li:eq(1)').removeClass('disabled');
                    $('ul.setup-panel li:eq(0)').addClass('disabled');
                    $('ul.setup-panel li a[href="#step-2"]').trigger('click');
                    //$(this).remove();

                }

            });

            $('#activate-step-3').on('click', function (e) {

                controleEtape2();

                if ($('#flag-erreur-step-2').val() == "0") {

                    $('ul.setup-panel li:eq(2)').removeClass('disabled');
                    $('ul.setup-panel li:eq(1)').addClass('disabled');
                    $('ul.setup-panel li a[href="#step-3"]').trigger('click');
                    //$(this).remove();

                }
            });

            $('#activate-step-4').on('click', function (e) {
                $('ul.setup-panel li:eq(3)').removeClass('disabled');
                $('ul.setup-panel li:eq(2)').addClass('disabled');
                $('ul.setup-panel li a[href="#step-4"]').trigger('click');
                //$(this).remove();
            });

            $('#activate-step-5').on('click', function (e) {
                $('ul.setup-panel li:eq(4)').removeClass('disabled');
                $('ul.setup-panel li:eq(3)').addClass('disabled');
                $('ul.setup-panel li a[href="#step-5"]').trigger('click');
                //$(this).remove();
            });

            $('#activate-step-1-prec').on('click', function (e) {
                $('ul.setup-panel li:eq(0)').removeClass('disabled');
                $('ul.setup-panel li:eq(1)').addClass('disabled');
                $('ul.setup-panel li a[href="#step-1"]').trigger('click');
                //$(this).remove();
            });

            $('#activate-step-2-prec').on('click', function (e) {
                $('ul.setup-panel li:eq(1)').removeClass('disabled');
                $('ul.setup-panel li:eq(2)').addClass('disabled');
                $('ul.setup-panel li a[href="#step-2"]').trigger('click');
                //$(this).remove();
            });

            $('#activate-step-3-prec').on('click', function (e) {
                $('ul.setup-panel li:eq(2)').removeClass('disabled');
                $('ul.setup-panel li:eq(3)').addClass('disabled');
                $('ul.setup-panel li a[href="#step-3"]').trigger('click');
                //$(this).remove();
            });

            $('#activate-step-4-prec').on('click', function (e) {
                $('ul.setup-panel li:eq(3)').removeClass('disabled');
                $('ul.setup-panel li:eq(4)').addClass('disabled');
                $('ul.setup-panel li a[href="#step-4"]').trigger('click');
               // $(this).remove();
            });

            $("#<%=txtIdentifiant.ClientID%>").keyup(function () {
                controleIdentifiant();
            });

            $("#<%=ddlCivilite.ClientID%>").change(function () {
                controleEtape2();
            });

            $("#<%=txtNom.ClientID%>").change(function () {
                controleEtape2();
            });

            $("#<%=txtPrenom.ClientID%>").change(function () {
                controleEtape2();
            });

            $("#<%=txtEmail.ClientID%>").change(function () {
                controleEtape2();
            });

            $("#<%=txtFonction.ClientID%>").change(function () {
                controleEtape2();
            });

            $("#<%=txtMdp.ClientID%>").change(function () {
                controleMdp();
            });

            $("#<%=txtConfirmMdp.ClientID%>").change(function () {
                controleMdp();
            });

        });


    </script>

</asp:Content>
