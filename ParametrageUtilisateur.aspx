<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Masterpage.Master" CodeBehind="ParametrageUtilisateur.aspx.vb" Inherits="WORKFLOW_FACTURE.ParametrageUtilisateur" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row" style="padding-top:15px;">
        <div class="col-lg-3">
            <button id="btnNouvelUtilisateur" type="button" class="btn btn-primary">
                <i class="fa fa-plus"></i>&nbsp;Ajouter un utilisateur
            </button>
        </div>
    </div>

    <div class="row" style="margin-top:15px;">
        <div class="col-lg-12">

            <div class="panel panel-default">

                <div class="panel-heading">
                    <i class="fa fa-user fa-fw"></i>&nbsp;Utilisateurs
                </div>
               
                <div class="panel-body">
                    <table style="width:100%" class="table table-striped table-bordered table-hover" id="dataTables-utilisateur">
                        <thead>
                            <tr>
                                <th>Identifiant</th>
                                <th>Nom</th>
                                <th>Prenom</th>
                                <th>Email</th>
                                <th>Supprimer</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Literal ID="litUtilisateurs" runat="server"></asp:Literal>
                        </tbody>
                    </table>

                </div>
                
            </div>
           
        </div>
       
    </div>
   

    <div class="modal fade" id="modal-confirm-suppression-utilisateur" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">

                <form id="form-confirm-suppression-utilisateur" method="post" action="handlers/SuppressionUtilisateur.ashx">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Confirmation de suppression</h4>
                    </div>

                    <div class="modal-body">
                        <p>Confirmez-vous la suppression de l'utilisateur ?</p>
                        <p><strong>Attention !</strong> Toutes les informations concernant cet utilisateur seront supprimées.</p>
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

    <script>

        $(document).ready(function () {

            var myTable = $('#dataTables-utilisateur').dataTable({
                "responsive": true,
                "fnDrawCallback": function () {
                    $('#dataTables-utilisateur tbody tr td:not(:last-child)').click(function () {

                        // get position of the selected row
                        var position = myTable.fnGetPosition(this);

                        // value of the first column (can be hidden)
                        var id = myTable.fnGetData(position)[0];

                        // redirect
                        if (id != null) {
                            document.location.href = 'DetailUtilisateur.aspx?id=' + id;
                        }

                    })
                },
            });

        });

        $(document).on("click", "#open-modal-confirm-suppression-utilisateur", function () {

            var id = $(this).data('id');
            $("#modal-confirm-suppression-utilisateur #mId").val(id);

        });

        $(document).on("click", "#btnNouvelUtilisateur", function () {

            document.location.href = 'AjoutUtilisateur.aspx';

        });


    </script>

</asp:Content>
