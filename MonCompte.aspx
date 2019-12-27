<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" CodeBehind="MonCompte.aspx.vb" Inherits="WORKFLOW_FACTURE.MonCompte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">

        <div class="row" style="padding-top: 15px;">

            <div class="col-lg-6">

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
                                    <div class="col-lg-9">
                                        <asp:TextBox ID="txtIdentifiant" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-9">
                                        <button type="button" id="btnOpenModalChangementMdp" class="btn btn-primary btn-block" data-toggle="modal" data-target="#modal-changement-mdp">Changer le mot de passe</button>
                                    </div>
                                </div>

                            </div>

                        </div>

                    </div>

                </div>

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
                                    <div class="col-lg-9">
                                        <asp:DropDownList ID="ddlCivilite" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px">
                                    <div class="col-lg-3">
                                        <label>Nom</label>
                                    </div>
                                    <div class="col-lg-9">
                                        <asp:TextBox ID="txtNom" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px">
                                    <div class="col-lg-3">
                                        <label>Prénom</label>
                                    </div>
                                    <div class="col-lg-9">
                                        <asp:TextBox ID="txtPrenom" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px">
                                    <div class="col-lg-3">
                                        <label>Email</label>
                                    </div>
                                    <div class="col-lg-9">
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px">
                                    <div class="col-lg-3">
                                        <label>Fonction</label>
                                    </div>
                                    <div class="col-lg-9">
                                        <asp:TextBox ID="txtFonction" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="row" style="margin-top: 5px">
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-9">
                                        <asp:Button ID="btnModifierInfos" runat="server" Text="Modifier les informations" class="btn btn-primary btn-block" />
                                    </div>
                                </div>

                            </div>

                        </div>

                    </div>

                </div>

                <div class="row">

                    <div class="col-lg-6">

                        <div class="panel panel-default">
                            <div class="panel-heading panel-heading-custom">
                                <i class="fa fa-keyboard-o fa-fw"></i>&nbsp;Champs à enrichir 
                            </div>

                            <div class="panel-body">
                                <asp:ListBox ID="lbChampsEnrichissement" runat="server" CssClass="form-control" Style="height: 100px;"></asp:ListBox>
                            </div>

                        </div>

                    </div>

                    <div class="col-lg-6">

                        <div class="panel panel-default">
                            <div class="panel-heading panel-heading-custom">
                                <i class="fa fa-keyboard-o fa-fw"></i>&nbsp;Champs à corriger
                            </div>

                            <div class="panel-body">
                                <asp:ListBox ID="lbChampsCorrection" runat="server" CssClass="form-control" Style="height: 100px;"></asp:ListBox>
                            </div>

                        </div>

                    </div>

                </div>

                <div class="row">

                    <div class="col-lg-6">

                        <div class="panel panel-default">
                            <div class="panel-heading panel-heading-custom">
                                <i class="fa fa-filter fa-fw"></i>&nbsp;Filtres de sélection
                            </div>

                            <div class="panel-body">
                                <asp:ListBox ID="lbFiltresSelection" runat="server" CssClass="form-control" Style="height: 100px;"></asp:ListBox>
                            </div>

                        </div>

                    </div>

                    <div class="col-lg-6">

                        <div class="panel panel-default">
                            <div class="panel-heading panel-heading-custom">
                                <i class="fa fa-bell fa-fw"></i>&nbsp;Alertes automatiques
                            </div>

                            <div class="panel-body">
                                <asp:ListBox ID="lbAlertesAutomatiques" runat="server" CssClass="form-control" Style="height: 100px;"></asp:ListBox>
                            </div>

                        </div>

                    </div>

                </div>

            </div>

            <div class="col-lg-6">

                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-commenting-o fa-fw"></i>&nbsp;Demandes administrateur
                    </div>

                    <div class="panel-body">

                        <div class="alert alert-success">
                            Pour contacter votre administrateur, renseignez les informations ci dessous :
                        </div>

                        <div class="row" style="padding-top: 10px;">
                            <div class="col-lg-3">
                                <label>Motif</label>
                            </div>
                            <div class="col-lg-9">
                                <asp:DropDownList ID="ddlMotifsDemande" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="row" style="padding-top: 10px;">
                            <div class="col-lg-12">
                                <label>Commentaire</label>
                            </div>
                        </div>

                        <div class="row" style="padding-top: 10px;">
                            <div class="col-lg-12">
                                <textarea class="form-control" style="height: 190px; resize: none;" id="txtCommentaireDemande" name="txtCommentaireDemande" maxlength="4000" runat="server"></textarea>
                            </div>
                        </div>

                        <div class="row" style="padding-top: 10px;">

                            <div class="col-lg-12">
                                <asp:Button ID="btnEnvoyerDemande" runat="server" Text="Envoyer la demande" class="btn btn-primary btn-block" />
                            </div>
                        </div>

                    </div>

                </div>

                <div class="panel panel-default">

                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-commenting-o fa-fw"></i>&nbsp;Mes demandes
                    </div>

                    <div class="panel-body">

                        <table style="width: 100%" class="table table-striped table-bordered table-hover" id="dataTables-demandes">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Date</th>
                                    <th>Sujet</th>
                                    <th>Statut</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="litDemandes" runat="server"></asp:Literal>
                            </tbody>
                        </table>

                    </div>

                </div>

            </div>

        </div>

    </form>

    <div class="modal fade" id="modal-changement-mdp" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog" role="document">

            <form id="form-changement-mdp" method="post" action="handlers/UpdateMdp.ashx">

                <div class="modal-content">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Changement du mot de passe</h4>
                    </div>

                    <div class="modal-body">

                        <div class="row">

                            <div class="col-lg-12">

                                <div class="alert alert-success">
                                    Veuillez préciser votre nouveau mot de passe ci-dessous.
                                </div>

                            </div>

                        </div>

                        <div class="row" style="padding-top: 5px;">

                            <div class="col-lg-4">
                                Nouveau mot de passe
                            </div>

                            <div class="col-lg-8">
                                <input type="password" class="form-control" id="txtNouveauMdp" name="txtNouveauMdp" />
                            </div>

                        </div>

                        <div class="row" style="padding-top: 5px;">

                            <div class="col-lg-4">
                                Confirmation
                            </div>

                            <div class="col-lg-8">
                                <input type="password" class="form-control" id="txtConfirmMdp" name="txtConfirmMdp" />
                            </div>

                        </div>

                        <div class="row" style="padding-top: 5px;">

                            <div class="col-lg-12">
                                <span id="msgErreur"></span>
                            </div>

                        </div>
                    </div>

                    <div class="modal-footer">

                        <div class="col-lg-12">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Annuler</button>
                            <button id="btnValiderMdp" type="button" class="btn btn-primary pull-right">Valider</button>
                        </div>

                    </div>

                </div>

            </form>

        </div>

    </div>

    <script type="text/javascript">


        $(document).ready(function () {

            $(document).on("click", "#btnValiderMdp", function () {
                $("#form-changement-mdp").submit();
            });

            var myTable = $('#dataTables-demandes').dataTable({
                "responsive": true,
                "fnDrawCallback": function () {
                    $('#dataTables-demandes tbody tr td').click(function () {

                        myTable.fnSetColumnVis(0, false);

                        // get position of the selected row
                        var position = myTable.fnGetPosition(this);

                        // value of the first column (can be hidden)
                        var id = myTable.fnGetData(position)[0];

                    })
                },
            });

            myTable.fnSetColumnVis(0, false);

        });

        myTable.fnSetColumnVis(0, false);

        $("#btnValiderMdp").on("click", function () {

            var mdp = $("#txtNouveauMdp").val();
            var confirm = $("#txtConfirmMdp").val();

            if (mdp.trim() == "" || confirm.trim() == "") {

                $("#msgErreur").html('<div class="alert alert-danger">Veuillez renseigner les champs</div>');

            } else if (mdp != confirm) {

                $("#msgErreur").html('<div class="alert alert-danger">Les valeurs saisies sont différentes</div>');

            } else {

                $("#form-changement-mdp").submit();

            }

        });

    </script>

</asp:Content>
