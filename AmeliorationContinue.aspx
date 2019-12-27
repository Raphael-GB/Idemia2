<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" CodeBehind="AmeliorationContinue.aspx.vb" Inherits="WORKFLOW_FACTURE.AmeliorationContinue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">

        <div class="row" style="padding-top: 15px;">

            <div class="col-lg-12">

                <div class="panel panel-default">

                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-sliders fa-fw"></i>&nbsp;Paramétres
                    </div>

                    <div class="panel-body">

                        <div class="col-lg-3">
                            Date de début
                            <asp:TextBox ID="dateDebut" name="dateDebut" runat="server" class="form-control"></asp:TextBox>
                        </div>

                        <div class="col-lg-3">
                            Date de fin
                            <asp:TextBox ID="dateFin" name="dateFin" runat="server" class="form-control"></asp:TextBox>
                        </div>

                        <div class="col-lg-3 ui-widget">
                            Fournisseur
                            <asp:TextBox ID="txtFournisseur" name="txtFournisseur" runat="server" class="form-control autocomplete"></asp:TextBox>
                        </div>

                        <div class="col-lg-3">
                            <br />
                            <input type="button" id="btnParametres" class="btn btn-primary btn-block" value="Afficher les résultats" />
                        </div>

                    </div>

                </div>

            </div>

        </div>

        <div class="row">

            <div class="col-lg-8">

                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-bar-chart-o fa-fw"></i>&nbsp;Evolution du taux de corrections sur la période 
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div id="morris-area-chart-evolution-taux-correction"></div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="col-lg-4">

                <div class="panel panel-default">

                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseurs les plus corrigés
                    </div>

                    <div class="panel-body">

                        <div class="list-group">
                            <a href="#" class="list-group-item">
                                <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseur 1
                            </a>
                            <a href="#" class="list-group-item">
                                <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseur 2
                            </a>
                            <a href="#" class="list-group-item">
                                <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseur 3
                            </a>
                            <a href="#" class="list-group-item">
                                <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseur 4
                            </a>
                            <a href="#" class="list-group-item">
                                <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseur 5
                            </a>
                            <a href="#" class="list-group-item">
                                <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseur 6
                            </a>
                            <a href="#" class="list-group-item">
                                <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseur 7
                            </a>

                        </div>
                        <a href="#" class="btn btn-primary btn-block">Voir le classement</a>

                    </div>

                </div>

            </div>

        </div>

        <div class="row">

            <div class="col-lg-8">

                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-bar-chart-o fa-fw"></i>&nbsp;Évolution du taux de valeurs génériques sur la période
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div id="morris-area-chart-evolution-taux-generique"></div>
                        </div>
                    </div>
                </div>

            </div>
            <div class="col-lg-4">

                <div class="panel panel-default">

                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseurs avec valeurs génériques
                    </div>

                    <div class="panel-body">

                        <div class="list-group">
                            <a href="#" class="list-group-item">
                                <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseur 1
                            </a>
                            <a href="#" class="list-group-item">
                                <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseur 2
                            </a>
                            <a href="#" class="list-group-item">
                                <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseur 3
                            </a>
                            <a href="#" class="list-group-item">
                                <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseur 4
                            </a>
                            <a href="#" class="list-group-item">
                                <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseur 5
                            </a>
                            <a href="#" class="list-group-item">
                                <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseur 6
                            </a>
                            <a href="#" class="list-group-item">
                                <i class="fa fa-arrow-circle-o-down fa-fw"></i>&nbsp;Fournisseur 7
                            </a>

                        </div>
                        <a href="#" class="btn btn-primary btn-block">Voir le classement</a>

                    </div>

                </div>

            </div>

        </div>

        <div class="row">

            <div class="col-lg-6">

                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-bar-chart-o fa-fw"></i>&nbsp;Délai moyen de paiement par responsabilité sur la période
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div id="morris-bar-chart-delai-paiement-responsabilite"></div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="col-lg-6">

                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-bar-chart-o fa-fw"></i>&nbsp;Nombre d'alertes par motif et par responsabilité
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div id="morris-bar-chart-motif-responsabilite"></div>
                        </div>
                    </div>
                </div>

            </div>

        </div>

        <!--<div class="row">

            <div class="col-lg-12">

                <div class="panel panel-default">

                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-files-o fa-fw"></i>&nbsp;Factures traitées sur la période
                    </div>

                    <div class="panel-body">

                        <table style="width: 100%" class="table table-striped table-bordered table-hover" id="dataTables-corrections">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Fournisseur</th>
                                    <th>N° fact.</th>
                                    <th>Date facturation</th>
                                    <th>Date réception</th>
                                    <th>Date LAD</th>
                                    <th>Date Traitement</th>
                                    <th>Date Livraision</th>
                                    <th>Date d'acquittement</th>
                                    <th>Responsabilité</th>
                                    <th>Délai</th>
                                    <th>Nb. Alertes</th>
                                    <th>Statut</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="litFacturesCorrigees" runat="server"></asp:Literal>
                            </tbody>
                        </table>

                    </div>

                </div>

            </div>

        </div>-->

    </form>

    <script type="text/javascript">

        $(document).ready(function () {

            $("#<%=dateDebut.ClientID%>").datetimepicker({ locale: 'fr', format: 'DD/MM/YYYY' });
            $("#<%=dateFin.ClientID%>").datetimepicker({ locale: 'fr', format: 'DD/MM/YYYY' });

            $("#<%=txtFournisseur.ClientID%>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "GET",
                        url: 'handlers/JsonFournisseur.ashx?valeur=' + request.term,
                        dataType: 'json',

                        success: function (data) {
                            response($.map(data, function (value, key) {

                                return value.CodeSAP + ' - ' + value.NomFournisseur;

                            }));
                        },
                        error: function (xhr, status, error) {
                            //alert(error);
                        }

                    });
                },
                limit: 20,
                minLength: 2
            });

                  
            /*var myTable = $('#dataTables-corrections').dataTable({
                "responsive": true,
                "fnDrawCallback": function () {
                    $('#dataTables-corrections tbody tr ').click(function () {

                        myTable.fnSetColumnVis(0, false);

                        // get position of the selected row
                        var position = myTable.fnGetPosition(this);

                        // value of the first column (can be hidden)
                        var id = myTable.fnGetData(position)[0];

                        //Valeur du statut
                        var statut = myTable.fnGetData(position)[1];

                        // redirect
                        if (id != null) {
                            document.location.href = 'Traitement.aspx?statut=' + statut + '&id=' + id;
                        }
                        

                    })
                }

            });

            myTable.fnSetColumnVis(0, false);*/

            $.ajax({
                url: 'handlers/JsonIndicateur.ashx?graph=evolution_taux_correction',
                dataType: 'JSON',
                type: 'POST',
                data: {},
                success: function (response) {
                    Morris.Area({
                        element: 'morris-area-chart-evolution-taux-correction',
                        data: response,
                        xkey: 'date_facturation',
                        ykeys: ['nb_corrections', 'nb_a_corriger'],
                        xLabels: 'day',
                        labels: ['Factures corrigées', 'Factures à corriger'],
                        hideHover: 'auto',
                        resize: true
                    });
                },
                error: function (xhr, status, error) {
                    //alert(error);
                }
            });

            $.ajax({
                url: 'handlers/JsonIndicateur.ashx?graph=delai_paiement_responsabilite',
                dataType: 'JSON',
                type: 'POST',
                data: {},
                success: function (response) {

                    Morris.Bar({
                        element: 'morris-bar-chart-delai-paiement-responsabilite',
                        data: response,
                        xkey: 'periode',
                        ykeys: ['delai_fournisseur', 'delai_prestataire', 'delai_client'],
                        labels: ['Fournisseur', 'Prestataire', 'Client'],
                        hideHover: 'auto',
                        resize: true,
                        stacked:true
                    });
                },
                error: function (xhr, status, error) {
                    //alert(error);
                }

            });

            $.ajax({
                url: 'handlers/JsonIndicateur.ashx?graph=evolution_taux_generique',
                dataType: 'JSON',
                type: 'POST',
                data: {},
                success: function (response) {
                    Morris.Area({
                        element: 'morris-area-chart-evolution-taux-generique',
                        data: response,
                        xkey: 'date_facturation',
                        ykeys: ['taux_generique'],
                        xLabels: 'day',
                        labels: ['Taux de valeurs génériques'],
                        hideHover: 'auto',
                        resize: true
                    });
                },
                error: function (xhr, status, error) {
                    //alert(error);
                }
            });

            $.ajax({
                url: 'handlers/JsonIndicateur.ashx?graph=motif_responsabilite',
                dataType: 'JSON',
                type: 'POST',
                data: {},
                success: function (response) {

                    Morris.Bar({
                        element: 'morris-bar-chart-motif-responsabilite',
                        data: response,
                        xkey: 'motif',
                        ykeys: ['nb_fournisseur', 'nb_prestataire', 'nb_client'],
                        labels: ['Fournisseur', 'Prestataire', 'Client'],
                        hideHover: 'auto',
                        resize: true,
                        stacked:true
                    });
                },
                error: function (xhr, status, error) {
                    //alert(error);
                }

            });

        });

    </script>

</asp:Content>
