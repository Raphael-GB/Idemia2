<%@ Page Title="" EnableEventValidation="false" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" CodeBehind="DetailMessage.aspx.vb" Inherits="WORKFLOW_FACTURE.Message1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form runat="server">

        <div class="row">
            <div class="col-lg-12">
                <h1 class="page-header">
                    <asp:Literal ID="litSujetMessage" runat="server"></asp:Literal>
                </h1>
            </div>
        </div>

        <div class="row">
            <div class="col-lg-2">Date : </div>
            <div class="col-lg-10">
                <asp:TextBox class="form-control" ID="txtDateMessage" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        <div class="row" style="margin-top: 5px;">
            <div class="col-lg-2">Expéditeur : </div>
            <div class="col-lg-10">
                <asp:TextBox class="form-control" ID="txtExpediteurMessage" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        <div class="row" style="margin-top: 5px;">
            <div class="col-lg-2">Destinataire(s) : </div>
            <div class="col-lg-10">
                <asp:TextBox class="form-control" ID="txtDestinatairesMessage" runat="server" ReadOnly="true"></asp:TextBox>
            </div>
        </div>

        <div class="row" style="margin-top: 15px; margin-bottom: 15px;">
 
            <div class="col-lg-12">

                <div id="cadre_message" class="form-control" style="height: 300px;">
                    <asp:Literal ID="litTexteMessage" runat="server"></asp:Literal>
                </div>

            </div>
        </div>

        <button id="open-modal-nouveau-message" data-toggle="modal" data-target="#modal-nouveau-message" type="button" class="btn btn-primary pull-right">Répondre</button>
        <button id="open-modal-confirm-suppression-message" data-toggle="modal" data-target="#modal-confirm-suppression-message" type="button" class="btn btn-primary pull-right">Supprimer</button>
        <asp:Button ID="btnRetour" runat="server" class="btn btn-primary pull-left" Text="Retourner sur la boite d'envoi" />

        <asp:HiddenField ID="HiddenExpediteur" runat="server" />
        <asp:HiddenField ID="HiddenSujet" runat="server" />
        <asp:HiddenField ID="HiddenID" runat="server" />

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
                            <input type="hidden" name="mExpediteur" id="mExpediteur" value="" />
                            <input type="hidden" name="mDate" id="mDate" value="" />
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
                            <h4 class="modal-title">Répondre</h4>
                        </div>

                        <div class="modal-body">

                            <div class="row" style="margin-top: 15px;">
                                <div class="col-lg-2">Destinataires</div>
                                <div class="col-lg-10">
                                    <input type="text" class="form-control" id="txtDestinataires" name="txtDestinataires" />
                                </div>
                            </div>

                            <div class="row" style="margin-top: 15px;">
                                <div class="col-lg-2">Sujet </div>
                                <div class="col-lg-10">
                                    <input id="txtSujet" name="txtSujet" type="text" class="form-control" value="" />
                                </div>
                            </div>

                            <div class="row" style="margin-top: 15px;">
                                <div class="col-lg-12">
                                    Message
                                </div>
                            </div>

                            <div class="row" style="margin-top: 5px;">
                                <div class="col-lg-12">
                                    <textarea id="txtMessage" name="txtMessage" class="form-control" style="height: 300px; resize: none;" maxlength="4000"></textarea>
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

    </form>

    <script type="text/javascript">

        $("#txtMessage").Editor();
       
        $(document).on("click", "#open-modal-confirm-suppression-message", function () {
            $("#modal-confirm-suppression-message #mId").val($("#<%=HiddenID.ClientID%>").val());
        });

        $(document).on("click", "#btnAjoutMessage", function () {
            $("#form-nouveau-message").submit();
        });

        $('#txtDestinataires')

            .on('tokenfield:createdtoken', function (e) {

                // Über-simplistic e-mail validation
                var re = /\S+@\S+\.\S+/
                var valid = re.test(e.attrs.value)
                if (!valid) {
                    $(e.relatedTarget).empty();
                }
            })

            .tokenfield();

        $(document).on("click", "#open-modal-nouveau-message", function () {

            if ($("#txtDestinataires").val() == "") {
                $('#txtDestinataires').tokenfield('createToken', $("#<%=HiddenExpediteur.ClientID%>").val());
            }

            $("#txtSujet").val("RE:" + $("#<%=HiddenSujet.ClientID%>").val());

        });

        $("#form-nouveau-message").submit(function () {

            $("#hiddenTxtMessage").val($("#txtMessage").Editor("getText"));

            return true;

        });

    </script>

</asp:Content>
