<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" CodeBehind="Alerte.aspx.vb" Inherits="WORKFLOW_FACTURE.Alerte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">

        <div class="row" style="padding-top: 15px;">
            <div class="col-lg-12">
                <button type="button" id="btnExtraction" class="btn btn-primary"><i class="fa fa-file-excel-o fa-fw"></i>&nbsp;Générer une extraction</button>
            </div>
        </div>

        <div class="row" style="padding-top: 15px;">

            <div class="col-lg-12">

                <div class="panel panel-default">

                    <div class="panel-heading panel-heading-custom">
                        <i class="fa fa-filter fa-fw"></i>&nbsp;Alertes Factures
                    </div>

                    <div class="panel-body">

                        <div class="row" style="padding-top: 5px;padding-bottom:15px; ">

                            <div class="col-lg-4">
                                <label for="ddlFiltres">Statut des alertes</label>
                                <asp:DropDownList ID="ddlStatutAlerte" runat="server" Cssclass="form-control" AutoPostBack="True"></asp:DropDownList>
                            </div>

                        </div>

                        <table style="width: 100%" class="table table-striped table-bordered table-hover" id="dataTables-alerte">
                            <thead>
                                <tr>
                                    <th>DOCID</th>
                                    <th>Motif</th>
                                    <th>Commentaire</th>
                                    <th>Emetteur</th>
                                    <th>Date d'alerte</th>
                                    <th>Résolu par</th>
                                    <th>Date de résolution</th>
                                    <th>Statut</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="litAlertes" runat="server"></asp:Literal>
                            </tbody>
                        </table>

                    </div>

                </div>

            </div>

        </div>

    </form>

     <form id="form-extraction" method="post" action="Handlers\ExtractionAlerte.ashx">
        <input type="hidden" id="extIdStatutAlerte" name="extIdStatutAlerte" />
    </form>

    <script>

        $(document).ready(function () {

            $("#btnExtraction").on("click", function () {

                 $("#extIdStatutAlerte").val($("#<%=ddlStatutAlerte.ClientID%>").val());
                 $("#form-extraction").submit();

             });

            var myTable = $('#dataTables-alerte').dataTable({
                "responsive": true,
                "fnDrawCallback": function () {
                    $('#dataTables-alerte tbody tr').click(function () {

                        myTable.fnSetColumnVis(0, true);

                        // get position of the selected row
                        var position = myTable.fnGetPosition(this);

                        // value of the first column (can be hidden)
                        var id = myTable.fnGetData(position)[0];

                        // redirect
                        if (id != null) {

                            $.ajax({
                                type: 'GET',
                                async: false,
                                url: 'Handlers/VerificationOccupation.ashx?docid=' + id,
                                success: function (data) {

                                  

                                    if (data == "0") {
                                        document.location.href = 'Traitement.aspx?id=' + id;
                                    } else {
                                        alert("La facture en lien avec cette alerte est actuellement occupée.");
                                    }

                                }
                            });


                        }

                    })
                }
            });

            myTable.fnSetColumnVis(0, true);
   
        });

    </script>

</asp:Content>
