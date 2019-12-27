<%@ Page Title="" EnableEventValidation="true" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" CodeBehind="ParametrageFiltre.aspx.vb" Inherits="WORKFLOW_FACTURE.ParametrageFiltre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="row" style="padding-top:15px;">
        <div class="col-lg-3">
            <button id="btnAjouterFiltre" type="button" class="btn btn-primary" data-toggle="modal" data-target="#modal-filtre">
                <i class="fa fa-plus"></i>&nbsp;Ajouter un filtre de sélection
            </button>
        </div>
    </div>

    <div class="row" style="margin-top:15px;">

        <div class="col-lg-12">

            <div class="panel panel-default">

                <div class="panel-heading panel-heading-custom">
                    <i class="fa fa-filter fa-fw"></i>&nbsp;Filtres de sélection
                </div>

                <div class="panel-body">

                    <table style="width:100%" class="table table-striped table-bordered table-hover" id="dataTables-filtre">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Libellé du filtre</th>
                                <th>Supprimer</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Literal ID="litFiltres" runat="server"></asp:Literal>
                        </tbody>
                    </table>

                </div>

            </div>

        </div>

    </div>

    <div id="modal-filtre" class="modal fade">

        <div class="modal-dialog">

            <form method="post" id="form-filtre" action="handlers/GestionFiltre.ashx">

                <div class="modal-content">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span> <span class="sr-only">close</span></button>
                        <h4 class="modal-title" id="modal-filtre-titre-general"></h4>
                    </div>

                    <div class="modal-body">

                        <div class="row">
                            <div class="col-lg-3">
                                Nom du filtre : 
                            </div>
                            <div class="col-lg-9">
                                <input id="modal-filtre-libelle" name="modal-filtre-libelle" type="text" class="form-control" maxlength="50" required="required" />
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
                                                <select id="ddlModalChampFiltre_1" name="modal-filtre-champ-1" class="form-control liste-champs">
                                                    <asp:Literal ID="litChamps1" runat="server"></asp:Literal>
                                                </select>
                                            </td>
                                            <td>
                                                <select id="ddlModalOperateurFiltre_1" name="modal-filtre-operateur-1" class="form-control liste-operateurs">
                                                    <option value="" data-index="1"></option>
                                                    <option value="=" data-index="1">=</option>
                                                    <option value="<" data-index="1"><</option>
                                                    <option value=">" data-index="1">></option>
                                                    <option value="<=" data-index="1"><=</option>
                                                    <option value=">=" data-index="1">>=</option>
                                                    <option value="LIKE" data-index="1">Contient</option>
                                                    <option value="IS_NULL" data-index="1">Est nul</option>
                                                    <option value="IS_NOT_NULL" data-index="1">Non nul</option>
                                                    <option value="DATATION_JOURS" data-index="1">Datation >= (en jours)</option>
                                                    <option value="DATATION_MOIS" data-index="1">Datation >= (en mois)</option>
                                                    <option value="DATATION_ANNEES" data-index="1">Datation >= (en années)</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input type="text" id="txtModalValeurFiltre_1" name="modal-filtre-valeur-1" class="form-control" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <select id="ddlModalChampFiltre_2" name="modal-filtre-champ-2" class="form-control liste-champs">
                                                    <asp:Literal ID="litChamps2" runat="server"></asp:Literal>
                                                </select>
                                            </td>
                                            <td>
                                                <select id="ddlModalOperateurFiltre_2" name="modal-filtre-operateur-2" class="form-control liste-operateurs">
                                                    <option value="" data-index="2"></option>
                                                    <option value="=" data-index="2">=</option>
                                                    <option value="<" data-index="2"><</option>
                                                    <option value=">" data-index="2">></option>
                                                    <option value="<=" data-index="2"><=</option>
                                                    <option value=">=" data-index="2">>=</option>
                                                    <option value="LIKE" data-index="2">Contient</option>
                                                    <option value="IS_NULL" data-index="2">Est nul</option>
                                                    <option value="IS_NOT_NULL" data-index="2">Non nul</option>
                                                    <option value="DATATION_JOURS" data-index="2">Datation >= (en jours)</option>
                                                    <option value="DATATION_MOIS" data-index="2">Datation >= (en mois)</option>
                                                    <option value="DATATION_ANNEES" data-index="2">Datation >= (en années)</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input type="text" id="txtModalValeurFiltre_2" name="modal-filtre-valeur-2" class="form-control" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <select id="ddlModalChampFiltre_3" name="modal-filtre-champ-3" class="form-control liste-champs">
                                                    <asp:Literal ID="litChamps3" runat="server"></asp:Literal>
                                                </select>
                                            </td>
                                            <td>
                                                <select id="ddlModalOperateurFiltre_3" name="modal-filtre-operateur-3" class="form-control liste-operateurs">
                                                    <option value="" data-index="3"></option>
                                                    <option value="=" data-index="3">=</option>
                                                    <option value="<" data-index="3"><</option>
                                                    <option value=">" data-index="3">></option>
                                                    <option value="<=" data-index="3"><=</option>
                                                    <option value=">=" data-index="3">>=</option>
                                                    <option value="LIKE" data-index="3">Contient</option>
                                                    <option value="IS_NULL" data-index="3">Est nul</option>
                                                    <option value="IS_NOT_NULL" data-index="3">Non nul</option>
                                                    <option value="DATATION_JOURS" data-index="3">Datation >= (en jours)</option>
                                                    <option value="DATATION_MOIS" data-index="3">Datation >= (en mois)</option>
                                                    <option value="DATATION_ANNEES" data-index="3">Datation >= (en années)</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input type="text" id="txtModalValeurFiltre_3" name="modal-filtre-valeur-3" class="form-control" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <select id="ddlModalChampFiltre_4" name="modal-filtre-champ-4" class="form-control liste-champs">
                                                    <asp:Literal ID="litChamps4" runat="server"></asp:Literal>
                                                </select>
                                            </td>
                                            <td>
                                                <select id="ddlModalOperateurFiltre_4" name="modal-filtre-operateur-4" class="form-control liste-operateurs">
                                                    <option value="" data-index="4"></option>
                                                    <option value="=" data-index="4">=</option>
                                                    <option value="<" data-index="4"><</option>
                                                    <option value=">" data-index="4">></option>
                                                    <option value="<=" data-index="4"><=</option>
                                                    <option value=">=" data-index="4">>=</option>
                                                    <option value="LIKE" data-index="4">Contient</option>
                                                    <option value="IS_NULL" data-index="4">Est nul</option>
                                                    <option value="IS_NOT_NULL" data-index="4">Non nul</option>
                                                    <option value="DATATION_JOURS" data-index="4">Datation >= (en jours)</option>
                                                    <option value="DATATION_MOIS" data-index="4">Datation >= (en mois)</option>
                                                    <option value="DATATION_ANNEES" data-index="4">Datation >= (en années)</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input type="text" id="txtModalValeurFiltre_4" name="modal-filtre-valeur-4" class="form-control" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <select id="ddlModalChampFiltre_5" name="modal-filtre-champ-5" class="form-control liste-champs">
                                                    <asp:Literal ID="litChamps5" runat="server"></asp:Literal>
                                                </select>
                                            </td>
                                            <td>
                                                <select id="ddlModalOperateurFiltre_5" name="modal-filtre-operateur-5" class="form-control liste-operateurs">
                                                    <option value="" data-index="5"></option>
                                                    <option value="=" data-index="5">=</option>
                                                    <option value="<" data-index="5"><</option>
                                                    <option value=">" data-index="5">></option>
                                                    <option value="<=" data-index="5"><=</option>
                                                    <option value=">=" data-index="5">>=</option>
                                                    <option value="LIKE" data-index="5">Contient</option>
                                                    <option value="IS_NULL" data-index="5">Est nul</option>
                                                    <option value="IS_NOT_NULL" data-index="5">Non nul</option>
                                                    <option value="DATATION_JOURS" data-index="5">Datation >= (en jours)</option>
                                                    <option value="DATATION_MOIS" data-index="5">Datation >= (en mois)</option>
                                                    <option value="DATATION_ANNEES" data-index="5">Datation >= (en années)</option>
                                                </select>
                                            </td>
                                            <td>
                                                <input type="text" id="txtModalValeurFiltre_5" name="modal-filtre-valeur-5" class="form-control" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>

                        </div>

                    </div>

                    <input type="hidden" name="modal-filtre-id" id="modal-filtre-id" />

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Annuler</button>
                        <button type="submit" class="btn btn-primary" id="btnModalValiderFiltre">Valider</button>
                    </div>

                </div>

            </form>

        </div>

    </div>

    <div class="modal fade" id="modal-confirm-suppression-filtre" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog">

            <div class="modal-content">

                <form id="form-confirm-suppression-filtre" method="post" action="handlers/SuppressionFiltre.ashx">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Confirmation de suppression</h4>
                    </div>

                    <div class="modal-body">
                        <p>Confirmez-vous la suppression du filtre ?</p>
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

    <script type="text/javascript">

        $(document).ready(function () {

            var myTable = $('#dataTables-filtre').dataTable({
                "responsive": true,
                "fnDrawCallback": function () {
                    $('#dataTables-filtre tbody tr td:not(:last-child)').click(function () {

                        myTable.fnSetColumnVis(0, false);
                        
                        $("#modal-filtre-titre-general").html("Modification du filtre");
                        $("#modal-filtre-libelle").val($(this).data("libelle"));
                        $("#modal-filtre-id").val($(this).data("id"));

                        for (i = 1; i <= 5; i++) {

                            //initialisation typage
                            var typage = $("#ddlModalChampFiltre_" + i).find('option:selected').data('typage');
                            var index = $("#ddlModalChampFiltre_" + i).find('option:selected').data('index');
                            typageChamp(typage, index);

                            $("#ddlModalChampFiltre_" + i).val($(this).data("champ" + i));
                            $("#ddlModalOperateurFiltre_" + i).val($(this).data("operateur" + i));
                            $("#txtModalValeurFiltre_" + i).val($(this).data("valeur" + i));

                        }

                        $("#modal-filtre").modal();

                    })
                },
            });

            myTable.fnSetColumnVis(0, false);

        });

        $(document).on("click", "#open-modal-confirm-suppression-filtre", function () {

            var id = $(this).data('id');
            $("#modal-confirm-suppression-filtre #mId").val(id);

        });


        $(document).on("click", "#btnAjouterFiltre", function () {

            $("#modal-filtre-titre-general").html("Ajout d'un nouveau filtre");
            $("#modal-filtre-libelle").val("");
            $("#modal-filtre-id").val("");

            for (i = 1; i <= 5; i++) {

                //initialisation typage
                typageChamp("TEXTE", i);

                $("#ddlModalChampFiltre_" + i).val("");
                $("#ddlModalOperateurFiltre_" + i).val("");
                $("#txtModalValeurFiltre_" + i).val("");
            }

        });

        //typage des champs du filtre

        $(document).on("change", ".liste-champs", function () {

            var typage = $(this).find('option:selected').data('typage');
            var index = $(this).find('option:selected').data('index');

            $("#ddlModalOperateurFiltre_" + index).val("");
            typageChamp(typage, index);


        });

        $(document).on("change", ".liste-operateurs", function () {

            var index = $(this).find('option:selected').data('index');
            var typage = $("#ddlModalChampFiltre_" + index).find('option:selected').data('typage');
            typageChamp(typage, index);

        });

        function typageChamp(typage, index) {

            var champ_valeur = "#txtModalValeurFiltre_" + index;
            var champ_operateur = "#ddlModalOperateurFiltre_" + index;

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

                    $(champ_valeur).attr("type", "text");
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

            } else if (typage == "ENTIER"){

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


            if ($(champ_operateur).val() == 'IS_NULL' || $(champ_operateur).val() == 'IS_NOT_NULL') {

                $(champ_valeur).val("");
                $(champ_valeur).prop('disabled', true);

            } else {

                $(champ_valeur).prop('disabled', false);

            }

        }

    </script>


</asp:Content>
