<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" CodeBehind="CorbeilleHorsScope.aspx.vb" Inherits="WORKFLOW_FACTURE.CorbeilleHorsScope" %>

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
                        <i class="fa fa-files-o fa-fw"></i>&nbsp;Documents Hors-Scope
                    </div>

                    <div class="panel-body">

                        <div class="row" style="padding-top: 5px; padding-bottom: 15px;">

                            <div class="col-lg-3">
                                <label for="ddlFiltres">Type de document</label>
                                <asp:DropDownList ID="ddlFiltres" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                            </div>

                        </div>

                        <table style="width: 100%" class="table table-striped table-bordered table-hover" id="dataTables-hors-scope">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Site</th>
                                    <th>Type de document</th>
                                    <th>Entité</th>
                                    <th>Date Insertion</th>
                                    <th>Nom du Lot</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="LitDocument" runat="server"></asp:Literal>
                            </tbody>
                        </table>

                    </div>

                </div>

            </div>

        </div>

    </form>


    <form id="form-extraction" method="post" action="Handlers\ExtractionHorsScopeATraiter.ashx">
        <input type="hidden" id="extIdFiltre" name="extIdFiltre" />
        <input type="hidden" id="extIdStatutFacture" name="extIdStatutFacture" />
    </form>

    <script>

        $(document).ready(function () {

            $("#btnExtraction").on("click", function () {

                $("#extIdFiltre").val($("#<%=ddlFiltres.ClientID%>").val());
                $("#form-extraction").submit();

            });

            var myTable = $('#dataTables-hors-scope').dataTable({
                "responsive": true,
                "bProcessing": true,
                "bServerSide": true,
                "pageLength": 100,
                "columns": [
                    { data: "Id" },
                    { data: "Site2" },
                    { data: "Type" },
                    { data: "Site" },
                    { data: "DateInsertion" },
                    { data: "LotName" },
                ],
                "sAjaxSource": 'Handlers/JsonHorsScope.ashx?filtre=' + $("#<%=ddlFiltres.ClientID%>").val(),
                "fnDrawCallback": function () {
                    $('#dataTables-hors-scope tbody tr').click(function () {

                        // get position of the selected row
                        var position = myTable.fnGetPosition(this);

                        // value of the first column (can be hidden)

                        var id = myTable.fnGetData(position, 0);
                        // redirect
                        if (id != null) {
                            document.location.href = 'TraitementHorsScope.aspx?id=' + id;
                        }

                    })
                }
            });
        });

    </script>



</asp:Content>
