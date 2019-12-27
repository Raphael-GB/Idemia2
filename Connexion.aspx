<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Connexion.aspx.vb" Inherits="WORKFLOW_FACTURE.Connexion" %>

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

    <div class="container">

        <div class="card card-container">

             <img class="profile-img-card" src="images/logo_idemia.png" />

            <p id="profile-name" class="profile-name-card"></p>

            <form class="form-signin" runat="server">

                <span id="reauth-email" class="reauth-email"></span>

                <asp:TextBox ID="txtLogin" runat="server" placeholder="Identifiant" required="required" CssClass="form-control input-lg" Text=""></asp:TextBox>
                <asp:TextBox ID="txtMdp" runat="server" CssClass="form-control input-lg" placeholder="Mot de passe" required="required" Text="" TextMode="Password"></asp:TextBox>
                <asp:Button ID="btnConnexion" runat="server" Text="Connexion" CssClass="btn btn-primary btn-block" />

                <div class="row" style="margin-top: 15px; margin-bottom: 15px;">

                    <div class="col-lg-12 text-center">
                        <a data-toggle="modal" href="#modal-mdp-oublie">Réinitialisation du mot de passe</a>
                    </div>

                </div>

                <asp:Panel ID="pnlInfo" runat="server" Visible="False">

                    <div class="alert alert-danger">
                        Identification incorrecte.
                    </div>

                </asp:Panel>

            </form>

        </div>

    </div>

    <div class="modal fade" id="modal-mdp-oublie" tabindex="-1" role="dialog" aria-hidden="true">

        <div class="modal-dialog" role="document">

            <div class="modal-content">

                <form id="form-mdp-oublie" method="post" action="handlers/MdpOublie.ashx">

                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title">Réinitialisation du mot de passe</h4>
                    </div>

                    <div class="modal-body">

                        <div class="row">

                            <div class="col-lg-12">

                                <div class="alert alert-success">
                                    Veillez saisir les informations suivantes et valider.<br />
                                    Un email vous sera envoyé avec les instructions à suivre. 
                                </div>

                            </div>

                        </div>

                        <div class="row" style="margin-top: 5px;">

                            <div class="col-lg-4">Votre identifiant</div>
                            <div class="col-lg-8">
                                <input type="text" class="form-control" id="txtIdentifiantMdpOublie" name="txtIdentifiantMdpOublie" />
                            </div>

                        </div>

                        <div class="row" style="margin-top: 5px">

                            <div class="col-lg-4">Votre adresse email</div>
                            <div class="col-lg-8">
                                <input type="email" class="form-control" id="txtEmailMdpOublie" name="txtEmailMdpOublie" />
                            </div>

                        </div>

                    </div>
                  
                    <div class="modal-footer">

                        <div class="col-lg-12">
                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Annuler</button>
                            <button type="submit" class="btn btn-primary pull-right" id="btnValider">Valider</button>
                        </div>

                    </div>

                </form>

            </div>

        </div>

    </div>

</body>

</html>
