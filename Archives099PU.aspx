<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" CodeBehind="Archives099PU.aspx.vb" Inherits="WORKFLOW_FACTURE.Archives099PU" %>

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
                        <i class="fa fa-files-o fa-fw"></i>&nbsp;Archives
                    </div>

                    <div class="panel-body">

                        <div class="row" style="padding-top: 5px;padding-bottom:15px; ">

                            <div class="col-lg-3">
                                <label for="ddlFiltres">Sélection du filtre</label>
                                <asp:DropDownList ID="ddlFiltres" runat="server" Cssclass="form-control" AutoPostBack="True"></asp:DropDownList>
                            </div>

                        </div>

                        <table style="width: 100%" class="table table-striped table-bordered table-hover" id="dataTables-archives">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Code Fourn.</th>
                                    <th>Nom Fourn.</th>
                                    <th>N°</th>
                                    <th>Date fact.</th>
                                    <th>Entité fact.</th>
                                    <th>Montant TTC</th>
                                    <th>Date insertion</th>
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

    <form id="form-extraction" method="post" action="Handlers\ExtractionArchive.ashx">
        <input type="hidden" id="extIdFiltre" name="extIdFiltre" />
        <input type="hidden" id="extIdStatutFacture" name="extIdStatutFacture" />
        <input type="hidden" id="typeArchive" name="typeArchive" />
    </form>

    <script>

        $(document).ready(function () {

        $("#btnExtraction").on("click", function () {

                $("#extIdStatutFacture").val("7");
                $("#extIdFiltre").val($("#<%=ddlFiltres.ClientID%>").val());
                $("#typeArchive").val("099PU");
                $("#form-extraction").submit();

        });

            var myTable = $('#dataTables-archives').dataTable({
                "responsive": true,
                "bProcessing": true,
                "bServerSide": true,
                "pageLength": 100,
                "columns": [           
                    { data: "Id" },
                    { data: "CodeFournisseur" },
                    { data: "NomFournisseur" },
                    { data: "NumFacture" },
                    { data: "DateFacture" },
                    { data: "Entite" },
                    { data: "MontantTTC" },
                    { data: "DateInsertion" },
                    { data: "FlagAlerte" },
                    { data: "MotifAlerte" },
                    { data: "EmetteurAlerte" }
                ],
                "sAjaxSource": 'Handlers/JsonArchives099PU.ashx?filtre=' + $("#<%=ddlFiltres.ClientID%>").val() <%--+ '&statut=' + $("#<%=ddlStatutFacture.ClientID%>").val()--%>,
                "fnDrawCallback": function () {
                    $('#dataTables-archives tbody tr').click(function () {

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
