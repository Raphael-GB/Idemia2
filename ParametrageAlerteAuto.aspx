<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" CodeBehind="ParametrageAlerteAuto.aspx.vb" Inherits="WORKFLOW_FACTURE.ParametrageAlerte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row" style="padding-top: 15px;">
        <div class="col-lg-3">
            <button id="btnAjouterAlerteAuto" type="button" class="btn btn-primary" data-toggle="modal" data-target="#modal-alerte-auto">
                <i class="fa fa-plus"></i>&nbsp;Ajouter une alerte automatique
            </button>
        </div>
    </div>

    <div class="row" style="margin-top: 15px;">

        <div class="col-lg-12">

            <div class="panel panel-default">

                <div class="panel-heading panel-heading-custom">
                    <i class="fa fa-filter fa-fw"></i>&nbsp;Alertes automatiques
                </div>

                <div class="panel-body">

                    <table style="width: 100%" class="table table-striped table-bordered table-hover" id="dataTables-alerte-auto">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Libellé de l'alerte</th>
                                <th>Activé</th>
                                <th>Supprimer</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Literal ID="litAlertesAuto" runat="server"></asp:Literal>
                        </tbody>
                    </table>

                </div>

            </div>

        </div>

    </div>

    <div id="modal-alerte-auto" class="modal fade">

        <div class="modal-dialog">

            <form method="post" id="form-alerte-auto" action="handlers/GestionAlerteAuto.ashx">

                <div class="modal-content">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span> <span class="sr-only">close</span></button>
                        <h4 class="modal-title" id="modal-alerte-auto-titre-general"></h4>
                    </div>

                    <div class="modal-body">

                        <div class="row">
                            <div class="col-lg-3">
                                Libellé de l'alerte : 
                            </div>
                            <div class="col-lg-9">
                                <input id="modal-alerte-auto-libelle" name="modal-alerte-auto-libelle" type="text" class="form-control" maxlength="50" required="required" />
                            </div>
                        </div>
                        <div class="row" style="margin-top: 5px;">
                            <div class="col-lg-3">
                                Lancer tout les  
                            </div>
                            <div class="col-lg-4">
                                <input type="number" step="1" id="modal-alerte-auto-duree" name="modal-alerte-auto-duree" class="form-control" />
                            </div>
                            <div class="col-lg-5">
                                <select id="modal-alerte-auto-periodicite" name="modal-alerte-auto-periodicite" class="form-control" required="required">
                                    <option value=""></option>
                                    <option value="PERODICITE_HEURES">Heure(s)</option>
                                    <option value="PERODICITE_JOURS">Mois</option>
                                    <option value="PERIODICITE_MOIS">Jour(s)</option>
                                </select>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 5px;">
                            <div class="col-lg-3">
                                à partir du
                            </div>
                            <div class="col-lg-9">
                                <input type="date" id="modal-alerte-auto-debut" name="modal-alerte-auto-debut" class="form-control"/>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 5px;">
                            <div class="col-lg-3"></div>
                            <div class="col-lg-9">
                                <div class="checkbox">
                                    <label><input type="checkbox" id="modal-alerte-auto-activer" name="modal-alerte-auto-activer" value="1" />Activer</label>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="margin-top: 5px;">
                            <div class="col-lg-12">
                                Attributs :
                            </div>
                        </div>

                        <div class="row" style="margin-top: 5px;">

                            <div class="col-lg-12">

                                <table class="table table-striped table-bordered">

                                    <thead>
                                        <tr>
                                            <th>Champ</th>
                                            <th>Opérateur</th>
                                            <th>Valeur</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <select id="ddlModalChampAlerteAuto_1" name="modal-alerte-auto-champ-1" class="form-control liste-champs">
                                                    <asp:Literal ID="litChamps1" runat="server"></asp:Literal>
                                                </select>
                                            </td>
                                            <td>
                                                <select id="ddlModalOperateurAlerteAuto_1" name="modal-alerte-auto-operateur-1" class="form-control liste-operateurs">
                                                    <option value="" data-index="1"></option>
                                                    <option value="=" data-index="1">=</option>
                                                    <option value="<" data-index="1"><</option>
                                                    <option value=">" data-index="1">></option>
                                                    <option value="<=" data-index="1"><=</option>
                                                    <option value=">=" data-index="1">>=</option>
                                                    <option value="LIKE" data-index="1">Contient</option>
                                                    <option value="DATATION_JOURS" data-index="1">Datation >= (en jours)</option>
                                                    <option value="DATATION_MOIS" data-index="1">Datation >= (en mois)</option>
                                                    <option value="DATATION_ANNEES" data-index="1">Datation >= (en années)</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input type="text" id="txtModalValeurAlerteAuto_1" name="modal-alerte-auto-valeur-1" class="form-control" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <select id="ddlModalChampAlerteAuto_2" name="modal-alerte-auto-champ-2" class="form-control liste-champs">
                                                    <asp:Literal ID="litChamps2" runat="server"></asp:Literal>
                                                </select>
                                            </td>
                                            <td>
                                                <select id="ddlModalOperateurAlerteAuto_2" name="modal-alerte-auto-operateur-2" class="form-control liste-operateurs">
                                                    <option value="" data-index="2"></option>
                                                    <option value="=" data-index="2">=</option>
                                                    <option value="<" data-index="2"><</option>
                                                    <option value=">" data-index="2">></option>
                                                    <option value="<=" data-index="2"><=</option>
                                                    <option value=">=" data-index="2">>=</option>
                                                    <option value="LIKE" data-index="2">Contient</option>
                                                    <option value="DATATION_JOURS" data-index="2">Datation >= (en jours)</option>
                                                    <option value="DATATION_MOIS" data-index="2">Datation >= (en mois)</option>
                                                    <option value="DATATION_ANNEES" data-index="2">Datation >= (en années)</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input type="text" id="txtModalValeurAlerteAuto_2" name="modal-alerte-auto-valeur-2" class="form-control" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <select id="ddlModalChampAlerteAuto_3" name="modal-alerte-auto-champ-3" class="form-control liste-champs">
                                                    <asp:Literal ID="litChamps3" runat="server"></asp:Literal>
                                                </select>
                                            </td>
                                            <td>
                                                <select id="ddlModalOperateurAlerteAuto_3" name="modal-alerte-auto-operateur-3" class="form-control liste-operateurs">
                                                    <option value="" data-index="3"></option>
                                                    <option value="=" data-index="3">=</option>
                                                    <option value="<" data-index="3"><</option>
                                                    <option value=">" data-index="3">></option>
                                                    <option value="<=" data-index="3"><=</option>
                                                    <option value=">=" data-index="3">>=</option>
                                                    <option value="LIKE" data-index="3">Contient</option>
                                                    <option value="DATATION_JOURS" data-index="3">Datation >= (en jours)</option>
                                                    <option value="DATATION_MOIS" data-index="3">Datation >= (en mois)</option>
                                                    <option value="DATATION_ANNEES" data-index="3">Datation >= (en années)</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input type="text" id="txtModalValeurAlerteAuto_3" name="modal-alerte-auto-valeur-3" class="form-control" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <select id="ddlModalChampAlerteAuto_4" name="modal-alerte-auto-champ-4" class="form-control liste-champs">
                                                    <asp:Literal ID="litChamps4" runat="server"></asp:Literal>
                                                </select>
                                            </td>
                                            <td>
                                                <select id="ddlModalOperateurAlerteAuto_4" name="modal-alerte-auto-operateur-4" class="form-control liste-operateurs">
                                                    <option value="" data-index="4"></option>
                                                    <option value="=" data-index="4">=</option>
                                                    <option value="<" data-index="4"><</option>
                                                    <option value=">" data-index="4">></option>
                                                    <option value="<=" data-index="4"><=</option>
                                                    <option value=">=" data-index="4">>=</option>
                                                    <option value="LIKE" data-index="4">Contient</option>
                                                    <option value="DATATION_JOURS" data-index="4">Datation >= (en jours)</option>
                                                    <option value="DATATION_MOIS" data-index="4">Datation >= (en mois)</option>
                                                    <option value="DATATION_ANNEES" data-index="4">Datation >= (en années)</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input type="text" id="txtModalValeurAlerteAuto_4" name="modal-alerte-auto-valeur-4" class="form-control" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <select id="ddlModalChampAlerteAuto_5" name="modal-alerte-auto-champ-5" class="form-control liste-champs">
                                                    <asp:Literal ID="litChamps5" runat="server"></asp:Literal>
                                                </select>
                                            </td>
                                            <td>
                                                <select id="ddlModalOperateurAlerteAuto_5" name="modal-alerte-auto-operateur-5" class="form-control liste-operateurs">
                                                    <option value="" data-index="5"></option>
                                                    <option value="=" data-index="5">=</option>
                                                    <option value="<" data-index="5"><</option>
                                                    <option value=">" data-index="5">></option>
                                                    <option value="<=" data-index="5"><=</option>
                                                    <option value=">=" data-index="5">>=</option>
                                                    <option value="LIKE" data-index="5">Contient</option>
                                                    <option value="DATATION_JOURS" data-index="5">Datation >= (en jours)</option>
                                                    <option value="DATATION_MOIS" data-index="5">Datation >= (en mois)</option>
                                                    <option value="DATATION_ANNEES" data-index="5">Datation >= (en années)</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input type="text" id="txtModalValeurAlerteAuto_5" name="modal-alerte-auto-valeur-5" class="form-control" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>

                        </div>

                    </div>

                    <input type="hidden" name="modal-alerte-auto-id" id="modal-alerte-auto-id" />

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Annuler</button>
                        <button type="submit" class="btn btn-primary" id="btnModalValiderAlerteAuto">Valider</button>
                    </div>

                </div>

            </form>

        </div>

    </div>

    <div class="modal fade" id="modal-confirm-suppression-alerte-auto" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog">

            <div class="modal-content">

                <form id="form-confirm-suppression-alerte-auto" method="post" action="handlers/SuppressionAlerteAuto.ashx">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Confirmation de suppression</h4>
                    </div>

                    <div class="modal-body">
                        <p>Confirmez-vous la suppression de l'alerte automatique ?</p>
                        <input type="hidden" name="mId" id="mId" value="" />
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Non</button>
                        <button id="btnModalConfirmSuppAlerteAuto" type="submit" class="btn btn-primary btn-ok">Oui</button>
                    </div>

                </form>

            </div>

        </div>

    </div>

    <script type="text/javascript">

        $(document).ready(function () {

            //$("#modal-alerte-auto-debut").datetimepicker({ locale: 'fr', format: 'DD/MM/YYYY HH:mm' });

            var myTable = $('#dataTables-alerte-auto').dataTable({
                "responsive": true,
                "fnDrawCallback": function () {
                    $('#dataTables-alerte-auto tbody tr td:not(:last-child)').click(function () {

                        myTable.fnSetColumnVis(0, false);

                        $("#modal-alerte-auto-id").val($(this).data("id"));
                        $("#modal-alerte-auto-titre-general").html("Modification de l'alerte automatique");
                        $("#modal-alerte-auto-libelle").val($(this).data("libelle"));
                        $("#modal-alerte-auto-debut").val($(this).data("debut"));
                        $("#modal-alerte-auto-duree").val($(this).data("duree"));
                        $("#modal-alerte-auto-periodicite").val($(this).data("periodicite"));

                        if ($(this).data("active") == "1") {
                            $("#modal-alerte-auto-activer").prop("checked", true);
                        }

                        for (i = 1; i <= 5; i++) {

                            //initialisation typage
                            var typage = $("#ddlModalChampAlerteAuto_" + i).find('option:selected').data('typage');
                            var index = $("#ddlModalChampAlerteAuto_" + i).find('option:selected').data('index');
                            typageChamp(typage, index);

                            $("#ddlModalChampAlerteAuto_" + i).val($(this).data("champ" + i));
                            $("#ddlModalOperateurAlerteAuto_" + i).val($(this).data("operateur" + i));
                            $("#txtModalValeurAlerteAuto_" + i).val($(this).data("valeur" + i));

                        }

                        $("#modal-alerte-auto").modal();

                    })
                },
            });

            myTable.fnSetColumnVis(0, false);

        });

        $(document).on("click", "#open-modal-confirm-suppression-alerte-auto", function () {

            var id = $(this).data('id');
            $("#modal-confirm-suppression-alerte-auto #mId").val(id);

        });


        $(document).on("click", "#btnAjouterAlerteAuto", function () {

            $("#modal-alerte-auto-id").val("");
            $("#modal-alerte-auto-titre-general").html("Ajout d'une nouvelle alerte automatique");
            $("#modal-alerte-auto-libelle").val("");
            $("#modal-alerte-auto-debut").val("");
            $("#modal-alerte-auto-duree").val("");
            $("#modal-alerte-auto-periodicite").val("");
            $("#modal-alerte-auto-activer").prop("checked", false);
            
            for (i = 1; i <= 5; i++) {

                //initialisation typage
                typageChamp("TEXTE", i);

                $("#ddlModalChampAlerteAuto_" + i).val("");
                $("#ddlModalOperateurAlerteAuto_" + i).val("");
                $("#txtModalValeurAlerteAuto_" + i).val("");

            }

        });

        //typage des champs du filtre

        $(document).on("change", ".liste-champs", function () {

            var typage = $(this).find('option:selected').data('typage');
            var index = $(this).find('option:selected').data('index');

            $("#ddlModalOperateurAlerteAuto_" + index).val("");
            typageChamp(typage, index);

        });

        $(document).on("change", ".liste-operateurs", function () {

            var index = $(this).find('option:selected').data('index');
            var typage = $("#ddlModalChampAlerteAuto_" + index).find('option:selected').data('typage');
            typageChamp(typage, index);

        });

        function typageChamp(typage, index) {

            var champ_valeur = "#txtModalValeurAlerteAuto_" + index;
            var champ_operateur = "#ddlModalOperateurAlerteAuto_" + index;

            $(champ_valeur).datetimepicker();
            $(champ_valeur).datetimepicker("destroy");

            if (typage == "DATE") {

                // Exceptions typage
                if ($(champ_operateur).val() == "DATATION_JOURS"
                    || $(champ_operateur).val() == "DATATION_MOIS"
                    || $(champ_operateur).val() == "DATATION_ANNEES") {

                    $(champ_valeur).attr("type", "number");
                    $(champ_valeur).attr("step", "1");

                } else {

                    $(champ_valeur).attr("type", "date");
                    $(champ_valeur).datetimepicker({ locale: 'fr', format: 'DD/MM/YYYY' });

                }

                $(champ_operateur + " option[value='LIKE']").hide();
                $(champ_operateur + " option[value='=']").show();
                $(champ_operateur + " option[value='<']").show();
                $(champ_operateur + " option[value='>']").show();
                $(champ_operateur + " option[value='<=']").show();
                $(champ_operateur + " option[value='>=']").show();
                $(champ_operateur + " option[value='DATATION_JOURS']").show();
                $(champ_operateur + " option[value='DATATION_MOIS']").show();
                $(champ_operateur + " option[value='DATATION_ANNEES']").show();

            } else if (typage == "ENTIER") {

                $(champ_valeur).attr("type", "number");
                $(champ_valeur).attr("step", "1");
                $(champ_operateur + " option[value='LIKE']").hide();
                $(champ_operateur + " option[value='=']").show();
                $(champ_operateur + " option[value='<']").show();
                $(champ_operateur + " option[value='>']").show();
                $(champ_operateur + " option[value='<=']").show();
                $(champ_operateur + " option[value='>=']").show();
                $(champ_operateur + " option[value='DATATION_JOURS']").hide();
                $(champ_operateur + " option[value='DATATION_MOIS']").hide();
                $(champ_operateur + " option[value='DATATION_ANNEES']").hide();

            } else if (typage == "DECIMAL") {

                $(champ_valeur).attr("type", "number");
                $(champ_valeur).attr("step", "0.1");
                $(champ_operateur + " option[value='LIKE']").hide();
                $(champ_operateur + " option[value='=']").show();
                $(champ_operateur + " option[value='<']").show();
                $(champ_operateur + " option[value='>']").show();
                $(champ_operateur + " option[value='<=']").show();
                $(champ_operateur + " option[value='>=']").show();
                $(champ_operateur + " option[value='DATATION_JOURS']").hide();
                $(champ_operateur + " option[value='DATATION_MOIS']").hide();
                $(champ_operateur + " option[value='DATATION_ANNEES']").hide();

            } else {

                $(champ_valeur).attr("type", "text");
                $(champ_operateur + " option[value='LIKE']").show();
                $(champ_operateur + " option[value='=']").show();
                $(champ_operateur + " option[value='<']").hide();
                $(champ_operateur + " option[value='>']").hide();
                $(champ_operateur + " option[value='<=']").hide();
                $(champ_operateur + " option[value='>=']").hide();
                $(champ_operateur + " option[value='DATATION_JOURS']").hide();
                $(champ_operateur + " option[value='DATATION_MOIS']").hide();
                $(champ_operateur + " option[value='DATATION_ANNEES']").hide();
            }

        }

    </script>

</asp:Content>
