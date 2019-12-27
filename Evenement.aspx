<%@ Page AutoEventWireup="false" CodeBehind="Evenement.aspx.vb" Inherits="WORKFLOW_FACTURE.Evenement" Language="vb" MasterPageFile="~/Masterpage.Master" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row" style="padding-top: 15px;">

        <div id="calendar"></div>

    </div>

    <div id="modal-evenement" class="modal fade">
        <div class="modal-dialog">
            <form method="post" id="form-evenement" action="handlers/GestionEvenement.ashx" >
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span> <span class="sr-only">close</span></button>
                        <h4 class="modal-title" id="modal-evenement-titre-general"></h4>
                    </div>

                    <div class="modal-body">

                        <div class="row">
                            <div class="col-lg-3">
                                Titre : 
                            </div>
                            <div class="col-lg-9"><input id="modal-evenement-titre" name="modal-evenement-titre" type="text" class="form-control" maxlength="200"/></div>
                        </div>
                        <div class="row" style="margin-top:5px;">
                            <div class="col-lg-3">
                                Description : 
                            </div>
                            <div class="col-lg-9" ><textarea class="form-control" style="height:150px;resize: none;" id="modal-evenement-description" name="modal-evenement-description" maxlength="4000"></textarea></div>
                        </div>
                        <div class="row" style="margin-top:5px;">
                            <div class="col-lg-3">
                                Début : 
                            </div>
                            <div class="col-lg-9">

                                <div class="input-group date" id="modal-evenement-date-debut-group">
                                    <input type="text" class="form-control" id="modal-evenement-date-debut" name="modal-evenement-date-debut" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>

                            </div>
                        </div>
                        <div class="row" style="margin-top:5px;">
                            <div class="col-lg-3">
                                Fin : 
                            </div>
                            <div class="col-lg-9" >

                                <div class="input-group date" id="modal-evenement-date-fin-group">
                                    <input type="text" class="form-control" id="modal-evenement-date-fin" name="modal-evenement-date-fin" />
                                    <span class="input-group-addon">
                                        <span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>

                            </div>
                        </div>

                    </div>

                    <input type="hidden" name="modal-evenement-suppression" id="modal-evenement-suppression" />
                    <input type="hidden" name="modal-evenement-id" id="modal-evenement-id" />

                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Annuler</button>
                        <button class="btn btn-primary" id="btnSupprimerEvenement">Supprimer</button>
                        <button class="btn btn-primary" id="btnValiderEvenement">Valider</button>
                    </div>

                </div>
            </form>
        </div>
    </div>

    <script type="text/javascript">

        $(document).ready(function () {

            $("#modal-evenement-date-debut-group").datetimepicker({ locale: 'fr' });
            $("#modal-evenement-date-fin-group").datetimepicker({ locale: 'fr' });

            $(document).on("click", "#btnValiderEvenement", function () {
                $("#form-evenement").submit();
            });

            $(document).on("click", "#btnSupprimerEvenement", function () {
                $("#modal-evenement-suppression").val("1");
                $("#form-evenement").submit();
            });

            $("#calendar").fullCalendar({
                header: {
                    left: 'prev,next,today',
                    center: 'title',
                    right: 'month,basicWeek,basicDay'
                },
                events: "handlers/JsonEvenement.ashx",
                selectable: false,
                editable: false,
                droppable: false,
                draggable: false,
                selectable: false,
                selectHelper: false,
                eventClick: function (event, jsEvent, view) {
                    $('#modal-evenement-titre-general').html("Modification");
                    $('#modal-evenement-titre').val(event.title);
                    $('#modal-evenement-description').val(event.description);
                    $('#modal-evenement-date-debut').val(moment(event.start).format("DD/MM/YYYY HH:mm"));
                    $('#modal-evenement-date-fin').val(moment(event.end).format("DD/MM/YYYY HH:mm"));
                    $('#modal-evenement-id').val(event.id);
                    $('#modal-evenement').modal();
                },
                dayClick: function (event, jsEvent, view) {

                    $('#modal-evenement-titre-general').html("Ajout d'un événement");
                    $('#modal-evenement-titre').val("");
                    $('#modal-evenement-description').val("");
                    $('#modal-evenement-date-debut').val(moment(event.start).format("DD/MM/YYYY HH:mm"));
                    $('#modal-evenement-date-fin').val(moment(event.end).format("DD/MM/YYYY HH:mm"));
                    $('#modal-evenement-id').val("");
                    $('#modal-evenement').modal();
                }

            });

        });


    </script>

</asp:Content>
