<%@ Page Title="" EnableEventValidation="false" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" CodeBehind="Indicateurs.aspx.vb" Inherits="WORKFLOW_FACTURE.Indicateurs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">

        <div class="row" style="padding-top: 15px;">
            <div class="col-lg-3 col-md-6">
                <div class="panel panel-primary">
                    <div class="panel-heading panel-heading-custom">
                        <div class="row">
                            <div class="col-lg-3">
                                <i class="fa fa-envelope-o fa-5x"></i>
                            </div>
                            <div class="col-lg-9 text-right">
                                <div class="huge">
                                    <asp:Literal ID="litNbMessages" runat="server"></asp:Literal>
                                </div>
                                <div>Messages</div>
                            </div>
                        </div>
                    </div>
                    <a href="BoiteReception.aspx">
                        <div class="panel-footer">
                            <span class="pull-left">Voir les messages</span>
                            <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                            <div class="clearfix"></div>
                        </div>
                    </a>
                </div>
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="panel panel-green">
                    <div class="panel-heading panel-heading-custom">
                        <div class="row">
                            <div class="col-lg-3">
                                <i class="fa fa-list-alt fa-5x"></i>
                            </div>
                            <div class="col-lg-3 text-center">
                                <asp:Literal ID="litTachesATraiter" runat="server"></asp:Literal>
                            </div>
                            <div class="col-lg-3 text-center">
                                <asp:Literal ID="litTachesAValider" runat="server"></asp:Literal>
                            </div>
                            <div class="col-lg-3 text-center">
                                <asp:Literal ID="litHorsScope" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                    <asp:Literal ID="litLienTaches" runat="server"></asp:Literal>

                    

                </div>
            </div>
            <div class="col-lg-3 col-md-6">
                <div class="panel panel-red">
                    <div class="panel-heading panel-heading-custom">
                        <div class="row">
                              <div class="col-lg-4">
                               <i class="fa fa-bell fa-5x"></i>
                            </div>

                                <div class="col-lg-4 text-left">
                                    <asp:Literal ID="litAlertesFacture" runat="server"></asp:Literal>
                                </div>
                                <div class="col-lg-4 text-left">
                                    <asp:Literal ID="litAlertesHs" runat="server"></asp:Literal>
                                </div>

                        </div>
                    </div>
                   <asp:Literal ID="litLienAlertes" runat="server"></asp:Literal>
                </div>
            </div>

        </div>

        <div class="row" style="padding-top: 15px;">
            <div class="col-lg-3 col-md-6">
                <div class="panel panel-yellow">
                        <div class="panel-heading panel-heading-custom">
                            <div class="row">
                                <div class="col-lg-3">
                                    <i class="fa fa-calendar fa-5x"></i>
                                </div>
                                <div class="col-lg-9 text-right">
                                    <div class="huge">
                                        <asp:Literal ID="litNbEvenements" runat="server"></asp:Literal>
                                    </div>
                                    <div>Événements</div>
                                </div>
                            </div>
                        </div>
                        <a href="Evenement.aspx">
                            <div class="panel-footer">
                                <span class="pull-left">Voir les événements</span>
                                <span class="pull-right"><i class="fa fa-arrow-circle-right"></i></span>
                                <div class="clearfix"></div>
                            </div>
                        </a>
                 </div>
            </div>
        </div>

        <div class="row">

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

                        <asp:Panel ID="pnlSelectionUtilisateur" runat="server">

                            <div class="col-lg-3">
                                Utilisateur
                            <asp:DropDownList ID="ddlUtilisateur" runat="server" class="form-control"></asp:DropDownList>
                            </div>

                        </asp:Panel>

                        <div class="col-lg-3">
                            <br />
                            <input type="button" id="btnParametres" class="btn btn-primary btn-block" value="Afficher les résultats" />
                        </div>

                    </div>

                </div>

            </div>

        </div>

        <div class="row">

            <div class="col-lg-6">
                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-bar-chart-o fa-fw"></i>&nbsp;Productivité
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div id="morris-bar-chart-productivite"></div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-6">
                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-bar-chart-o fa-fw"></i>&nbsp;Temps de connexion
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div id="morris-bar-chart-temps-connexion"></div>
                        </div>
                    </div>
                </div>
            </div>

        </div>

        <div class="row">

            <div class="col-lg-6">
                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-bar-chart-o fa-fw"></i>&nbsp;Délai des alertes en cours
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div id="morris-bar-chart-delai-alerte"></div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-6">
                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-bar-chart-o fa-fw"></i>&nbsp;Répartion par statut 
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-12">
                            <div id="morris-area-chart-repartition-statut"></div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>

    <script type="text/javascript">

        $(document).ready(function () {

            $("#<%=dateDebut.ClientID%>").datetimepicker({ locale: 'fr', format: 'DD/MM/YYYY' });
            $("#<%=dateFin.ClientID%>").datetimepicker({ locale: 'fr', format: 'DD/MM/YYYY' });

            var date_debut = $("#<%=dateDebut.ClientID%>").val();
            var date_fin = $("#<%=dateFin.ClientID%>").val();
            var identifiant = $("#<%=ddlUtilisateur.ClientID%>").val();

            if (identifiant == null) {
                identifiant = '';
            }

            $.ajax({
                url: 'handlers/JsonIndicateur.ashx',
                dataType: 'json',
                type: 'post',
                data: 'graph=productivite&debut=' + date_debut + '&fin=' + date_fin + "&identifiant=" + identifiant,
                success: function (response) {

                    Morris.Bar({
                        element: 'morris-bar-chart-productivite',
                        data: response,
                        xkey: 'X',
                        ykeys: ['Y1'],
                        labels: ['Nb. factures traitées'],
                        hideHover: 'auto',
                        resize: true,
                        parseTime: false
                    });
                },
                error: function (xhr, status, error) {
                    //alert('productivité->', error);
                }
            });

            $.ajax({
                url: 'handlers/JsonIndicateur.ashx',
                dataType: 'json',
                type: 'post',
                data: 'graph=temps_connexion',
                success: function (response) {

                    Morris.Bar({
                        element: 'morris-bar-chart-temps-connexion',
                        data: response,
                        xkey: 'date_activite',
                        ykeys: ['tps_connexion'],
                        labels: ['Temps de connexion'],
                        hideHover: 'auto',
                        resize: true
                    });
                },
                error: function (xhr, status, error) {
                    //alert('temps connexion->' + error);
                }
            });

            $.ajax({
                url: 'handlers/JsonIndicateur.ashx',
                dataType: 'json',
                type: 'post',
                data: 'graph=delai_alerte',
                success: function (response) {

                    Morris.Bar({
                        element: 'morris-bar-chart-delai-alerte',
                        data: response,
                        xkey: 'delai',
                        ykeys: ['nb_alertes'],
                        labels: ['Nb. Alertes en cours'],
                        hideHover: 'auto',
                        resize: true
                    });
                },
                error: function (xhr, status, error) {
                    //alert('délai alerte->' + error);
                }
            });

            $.ajax({
                url: 'handlers/JsonIndicateur.ashx',
                dataType: 'json',
                type: 'post',
                data: 'graph=repartition_statut&debut=' + date_debut + '&fin=' + date_fin,
                success: function (response) {

                    Morris.Area({
                        element: 'morris-area-chart-repartition-statut',
                        data: response,
                        xkey: 'X',
                        ykeys: ['Y1', 'Y2', 'Y3'],
                        labels: ['Nb. factures traitées', 'Nb. factures à traiter', 'Nb. factures CBBAP'],
                        hideHover: 'auto',
                        resize: true,
                        parseTime: false
                    });
                },
                error: function (xhr, status, error) {
                    //alert('répartition statut->' + error);
                }
            });

        });


    </script>


</asp:Content>
