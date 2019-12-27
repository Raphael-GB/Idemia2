<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" CodeBehind="CorbeilleRejet.aspx.vb" Inherits="WORKFLOW_FACTURE.CorbeilleRejet" %>

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
                        <i class="fa fa-files-o fa-fw"></i>&nbsp;Factures rejetées
                    </div>

                    <div class="panel-body">

                        <div class="row" style="padding-top: 5px; padding-bottom: 15px;">

                            <div class="col-lg-3">
                                <label for="ddlRejet">Motif de rejet</label>
                                <asp:DropDownList ID="ddlRejet" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                            </div>

                            <%--                            <div class="col-lg-3">
                                <label for="ddlStatutFacture">Statut des factures</label>
                                <asp:DropDownList ID="ddlStatutFacture" runat="server" Cssclass="form-control" AutoPostBack="True"></asp:DropDownList>
                            </div>--%>
                        </div>

                        <table style="width: 100%" class="table table-striped table-bordered table-hover" id="dataTables-rejets">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Site</th>
                                    <th>Code Fourn.</th>
                                    <th>Nom Fourn.</th>
                                    <th>N°</th>
                                    <th>Date fact.</th>
                                    <th>Entité fact.</th>
                                    <th>Montant TTC</th>
                                    <th>Date insertion</th>
                                    <th>Motif de rejet</th>
                                    <th>Flag Alerte</th>
                                    <th>Motif Alerte</th>
                                    <th>Emetteur Alerte</th>                                  
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="LitFactures" runat="server"></asp:Literal>
                            </tbody>
                        </table>

                    </div>

                </div>

            </div>

        </div>

    </form>

    <form id="form-extraction" method="post" action="Handlers\ExtractionRejet.ashx">
        <input type="hidden" id="extIdStatutFacture" name="extIdStatutFacture" />
        <input type="hidden" id="extIdMotifRejet" name="extIdMotifRejet" />
    </form>

    <script>
        $(document).ready(function () {

            $("#btnExtraction").on("click", function () {
                $("#extIdStatutFacture").val("2");
                $("#extIdMotifRejet").val($("#<%=ddlRejet.ClientID%>").val());
                $("#form-extraction").submit();
            });


            var myTable = $('#dataTables-rejets').dataTable({
                "responsive": true,
                "bProcessing": true,
                "bServerSide": true,
                "pageLength": 100,
                "columns": [
                    { data: "Id" },
                    { data: "Site" },
                    { data: "CodeFournisseur" },
                    { data: "NomFournisseur" },
                    { data: "NumFacture" },
                    { data: "DateFacture" },
                    { data: "Entite" },
                    { data: "MontantTTC" },
                    { data: "DateInsertion" },
                    { data: "MotifRejet" },
                    { data: "FlagAlerte" },
                    { data: "MotifAlerte" },
                    { data: "EmetteurAlerte" }
                ],
                "sAjaxSource": 'Handlers/JsonFactureRejet.ashx?rejet=' + $("#<%=ddlRejet.ClientID%>").val()<%-- + '&statut=' + $("#<%=ddlStatutFacture.ClientID%>").val()--%>,
                "fnDrawCallback": function () {
                    $('#dataTables-rejets tbody tr').click(function () {

                        // get position of the selected row
                        var position = myTable.fnGetPosition(this);

                        // value of the first column (can be hidden)

                        var id = myTable.fnGetData(position, 0);

                        // redirect
                        if (id != null) {

                            $.ajax({
                                type: 'GET',
                                async: false,
                                url: 'Handlers/VerificationOccupation.ashx?docid=' + id,
                                success: function (data) {

                                    if (data == "0") {
                                        document.location.href = 'Traitement.aspx?id=' + id;
                                    }

                                }
                            });

                        }

                    })
                }
        });

        });

    </script>


</asp:Content>
