<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" CodeBehind="DetailUtilisateur.aspx.vb" Inherits="WORKFLOW_FACTURE.DetailUtilisateur" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">

        <div class="row" style="padding-top:15px;">

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

                                <input type="hidden" id="flag-erreur-identifiant-step-1" value="0" />
                                <input type="hidden" id="flag-erreur-mdp-step-1" value="0" />

                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>

        <div class="row">

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

                                <input type="hidden" id="flag-erreur-step-2" value="0" />

                            </div>

                        </div>

                    </div>

                </div>

            </div>

        </div>

        <div class="row">

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

            </div>

        </div>

        <div class="row">

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

            </div>

        </div>

        <div class="row">

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

            </div>

        </div>

       <asp:Button ID="btnTerminer" runat="server" Text="Enregistrer les modifications" class="btn btn-primary pull-right" style="margin-bottom:25px;" />

    </form>

    <script type="text/javascript">

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

            controleMdp();
            controleEtape2();

            $("#<%=btnTerminer.ClientID%>").on('click', function (e) {

                controleMdp();
                controleEtape2();

                if ($('#flag-erreur-identifiant-step-1').val() != "0" || $('#flag-erreur-mdp-step-1').val() != "0") {

                    return false;
                }
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
