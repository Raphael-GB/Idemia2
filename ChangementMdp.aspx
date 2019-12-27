<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ChangementMdp.aspx.vb" Inherits="WORKFLOW_FACTURE.ChangementMdp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Groupe Bernard - Workflow de facture</title>

    <link href="Content/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css/connexion.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-3.1.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.12.1.min.js" type="text/javascript"></script>
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="Scripts/respond.min.js" type="text/javascript"></script>
    <script src="Scripts/scripts.js" type="text/javascript"></script>
</head>

<body>

    <img class="logo-tool" src="images/validinvoices.png" />

    <div class="container" style="padding-top: 250px;">

        <div class="col-lg-6 col-lg-offset-3">

            <form runat="server">
             <%--<form id="form-changement-mdp" method="post" action="handlers/UpdateMdp.ashx">--%>

                <div class="row">

                    <div class="col-lg-12">

                        <div class="alert alert-success">
                            Vous avez demandé une réinitialisation de votre mot de passe.<br />
                            Veuillez préciser votre nouveau mot de passe ci-dessous.
                        </div>

                    </div>

                </div>

                <div class="row">

                    <div class="col-lg-4">
                        Nouveau mot de passe
                    </div>

                    <div class="col-lg-8">
                         <%--<input type="password" class="form-control" id="txtNouveauMdp" name="txtNouveauMdp" />--%>
                        <asp:TextBox ID="txtNouveauMdp" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>

                </div>

                <div class="row">

                    <div class="col-lg-4">
                        Confirmation
                    </div>

                    <div class="col-lg-8">
                         <%--<input type="password" class="form-control" id="txtConfirmMdp" name="txtConfirmMdp" />--%>
                        <asp:TextBox ID="txtConfirmMdp" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>

                </div>

                <div class="row">

                    <div class="col-lg-12">
                        <asp:Literal ID="litErreur" runat="server"></asp:Literal>
                    </div>

                </div>

                <div class="row" style="margin-top:5px;">

                    <div class="col-lg-6 col-lg-offset-3">
                        <asp:Button ID="btnValider" runat="server" Text="Valider" CssClass="btn btn-primary btn-block" />
                                                    <%--<button id="btnValider" type="button" class="btn btn-primary btn-block">Valider</button>--%>
                    </div>

                </div>

            </form>

        </div>

    </div>

    <script>
        $(document).ready(function () {

            //$(document).on("click", "#btnValider", function () {
            //    $("#form-changement-mdp").submit();
            //});

        });
    </script>
</body>

</html>
