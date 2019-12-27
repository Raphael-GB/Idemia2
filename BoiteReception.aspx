<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" CodeBehind="BoiteReception.aspx.vb" Inherits="WORKFLOW_FACTURE.Messagerie" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   
    <div class="row" style="padding-top:15px;">
        <div class="col-lg-3">
            <button id="open-modal-nouveau-message" data-toggle="modal" data-target="#modal-nouveau-message" type="button" class="btn btn-primary">
                <i class="fa fa-plus"></i>&nbsp;Nouveau message
            </button>
        </div>
    </div>

    <div class="row" style="margin-top:15px;">
        <div class="col-lg-12">

            <div class="panel panel-default">

                <div class="panel-heading panel-heading-custom">
                    <i class="fa fa-envelope fa-fw"></i>&nbsp;Messages reçus  
                </div>

                <!-- /.panel-heading -->
                <div class="panel-body">
                    <table style="width:100%" class="table table-striped table-bordered table-hover" id="dataTables-reception">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Expéditeur</th>
                                <th>Date</th>
                                <th>Sujet</th>
                                <th>Lu / Non lu</th>
                                <th>Supprimer</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Literal ID="litMessages" runat="server"></asp:Literal>
                        </tbody>
                    </table>

                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </div>
        <!-- /.col-lg-12 -->
    </div>
    <!-- /.row -->

    <div class="modal fade" id="modal-confirm-suppression-message" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">

                <form id="form-confirm-suppression-message" method="post" action="handlers/SuppressionMessage.ashx">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Confirmation de suppression</h4>
                    </div>

                    <div class="modal-body">
                        <p>Confirmez-vous la suppression du message ?</p>
                        <input type="hidden" name="mId" id="mId" value="" />
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Non</button>
                        <button id="btnConfirmSupp" type="submit" class="btn btn-primary btn-ok">Oui</button>
                    </div>

                </form>

            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-nouveau-message" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">

                <form id="form-nouveau-message" method="post" action="handlers/EnvoiMessage.ashx">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Nouveau message</h4>
                    </div>

                    <div class="modal-body">

                        <div class="row" style="margin-top: 15px;">
                            <div class="col-lg-12">Destinataires</div>
                        </div>

                         <div class="row" style="margin-top: 5px">

                            <div class="col-lg-12">
                                <input type="text" class="form-control" style="height: 150px; resize: none;" id="txtDestinataires" name="txtDestinataires" maxlength="1000"/>
                            </div>

                        </div>

                        <div class="row" style="margin-top: 5px">

                            <div class="col-lg-8">
                                <input type="text" class="form-control" id="txtAjoutDestinataire" name="txtAjoutDestinataire" maxlength="1000"/>
                            </div>
                            <div class="col-lg-4"><button type="button" id="btnAJouterDestinataire" class="btn btn-primary btn-block">Ajouter</button></div>

                        </div>

                         <div class="row" style="margin-top: 15px;">
                            <div class="col-lg-2">Sujet </div>
                            <div class="col-lg-10"><input id="txtSujet" name="txtSujet" type="text" class="form-control" value="" maxlength="200"/></div>
                        </div>

                        <div class="row" style="margin-top: 15px;">
                            <div class="col-lg-12">
                                Message
                            </div>
                        </div>

                        <div class="row" style="margin-top: 5px;">
                            <div class="col-lg-12">
                                <textarea id="txtMessage" name="txtMessage" class="form-control" style="height:300px;resize: none;" maxlength="4000"></textarea>
                                <input type="hidden" id="hiddenTxtMessage" name="hiddenTxtMessage"/>
                            </div>
                        </div>

                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Annuler</button>
                        <button id="btnAjoutMessage" type="button" class="btn btn-primary btn-ok">Envoyer</button>
                    </div>

                </form>

            </div>
        </div>
    </div>

    <script>

        $(document).ready(function () {

            $("#txtMessage").Editor();

            var myTable = $('#dataTables-reception').dataTable({
                "responsive": true,
                "fnDrawCallback": function () {
                    $('#dataTables-reception tbody tr td:not(:last-child)').click(function () {

                        myTable.fnSetColumnVis(0, false);

                        // get position of the selected row
                        var position = myTable.fnGetPosition(this);

                        // value of the first column (can be hidden)
                        var id = myTable.fnGetData(position)[0];

                        // redirect
                        if (id != null) {
                            document.location.href = 'DetailMessage.aspx?id=' + id;
                        }

                    })
                },
            });

            myTable.fnSetColumnVis(0, false);

        });

        $(document).on("click", "#open-modal-confirm-suppression-message", function () {

            var id = $(this).data('id');

            $("#modal-confirm-suppression-message #mId").val(id);

        });

        $(document).on("click", "#btnAjoutMessage", function () {
            $("#form-nouveau-message").submit();
        });

        function validateEmail(input) {
            var re = /\S+@\S+\.\S+/;
            var valid = re.test(input);
            return re.test(input);
        }

        $('#txtDestinataires').tagsinput({
            freeInput: false
        });

        $(document).on("click", "#btnAjoutMessage", function () {

            $("#hiddenTxtMessage").val($("#txtMessage").Editor("getText"));

        });

        $("#btnAJouterDestinataire").on("click", function () {

            var dest = $('#txtAjoutDestinataire').val()

            if (validateEmail(dest)) {
                $('#txtDestinataires').tagsinput('add', dest);
                $('#txtAjoutDestinataire').val("");
            }

        });

        $('#txtAjoutDestinataire').autocomplete({

            source: function (request, response) {

                $.ajax({

                    type: "get",
                    url: 'Handlers/JsonSuggessionEmail.ashx?param=' + request.term,
                    dataType: 'json',
                    success: function (data) {
                        response($.map(data, function (value, key) {
                            return value;
                        }))
                    },

                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //alert(textStatus);
                    }

                });

            },
            minLength: 2

        });

    </script>

</asp:Content>
