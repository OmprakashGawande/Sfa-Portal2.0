<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">

    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <link rel="shortcut icon" href="assets/images/favicon.png" type="image/x-icon" />
    <title>Log in</title>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Google font-->
    <link rel="preconnect" href="https://fonts.googleapis.com">
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin="">
    <link rel="stylesheet" type="text/css" href="assets/css/font-awesome.css">
    <!-- ico-font-->
    <link rel="stylesheet" type="text/css" href="assets/css/vendors/icofont.css">
    <!-- Themify icon-->
    <link rel="stylesheet" type="text/css" href="assets/css/vendors/themify.css">
    <!-- Flag icon-->
    <link rel="stylesheet" type="text/css" href="assets/css/vendors/flag-icon.css">
    <!-- Feather icon-->
    <link rel="stylesheet" type="text/css" href="assets/css/vendors/feather-icon.css">
    <!-- Plugins css start-->
    <!-- Plugins css Ends-->
    <!-- Bootstrap css-->
    <link rel="stylesheet" type="text/css" href="assets/css/vendors/bootstrap.css">
    <!-- App css-->
    <link rel="stylesheet" type="text/css" href="assets/css/style.css">
    <link id="color" rel="stylesheet" href="assets/css/color-1.css" media="screen">
    <!-- Responsive css-->
    <link rel="stylesheet" type="text/css" href="assets/css/responsive.css">
</head>
<body>
    <form id="form2" class="theme-form" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-xl-7">
                    <img class="bg-img-cover bg-center" src="assets/images/login/SFA_LOGIN.png" alt="looginpage">
                </div>
                <div class="col-xl-5 p-0">
                    <div class="login-card login-dark">
                        <div>
                            <div>
                                <a class="logo text-start" >
                                    <img class="img-fluid for-dark" src="assets/images/logo/logo.png" alt="looginpage"><img class="img-fluid for-light" src="assets/images/logo/logo_dark-1.png" alt="looginpage">
                                </a>
                            </div>
                            <div class="login-main">

                                <h4>ERP LOGIN</h4>
                                <p>
                                    <asp:Label ID="LblMsg" runat="server" ForeColor="red"></asp:Label>
                                </p>
                                <div class="form-group">
                                    <label class="col-form-label">User ID</label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfvUserId" runat="server" Display="Dynamic" ControlToValidate="txtUserName" Text="<i class='fa fa-exclamation-circle' title='Required to Fill User Name!'></i>" ErrorMessage="Required to Fill User Name!" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Login"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid UserName Exp. XX0001!'></i>" ControlToValidate="txtUserName" ValidationExpression="^^[a-zA-Z0-9]*$" ErrorMessage="Invalid UserName Exp. XX0001" SetFocusOnError="true" ValidationGroup="Login"></asp:RegularExpressionValidator>
                                    </span>
                                    <asp:TextBox ID="txtUserName" runat="server" class="txtbox form-control" placeholder="MY ERP CODE" MaxLength="50"></asp:TextBox>
                                    <span class="glyphicon glyphicon-user form-control-feedback"></span>
                                </div>
                                <div class="form-group">
                                    <label class="col-form-label">Password</label>
                                    <div class="form-input position-relative">
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvpass" runat="server" Display="Dynamic" ControlToValidate="txtUserPassword" ErrorMessage="Required to Fill Password!" Text="<i class='fa fa-exclamation-circle' title='Required to Fill Password!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Login"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="Dynamic" runat="server" ControlToValidate="txtUserPassword" ValidationExpression="^[a-zA-z0-9-_@#!*$&^]+$" Text="<i class='fa fa-exclamation-circle' title='Special Character allowed only (-_@#!*$&^).!'></i>" ErrorMessage="Special Character allowed only (-_@#!*$&^)." SetFocusOnError="true" ValidationGroup="Login"></asp:RegularExpressionValidator>
                                        </span>
                                        <div class="password-container">
                                            <asp:TextBox ID="txtUserPassword" runat="server" class="txtbox form-control" placeholder="ERP PASSWORD" TextMode="Password" MaxLength="50"></asp:TextBox>
                                            <div class="show-hide" onclick="togglePasswordVisibility('txtUserPassword')">show</div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group mb-0 mt-5">
                                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary btn-block w-100" AccessKey="L" ToolTip="Shortcut Key (Alt + L)" OnClientClick="return ValidatePage();" OnClick="btnLogin_Click" Text="Login" />
                                </div>
                                <div class="social mt-4">
                                    <div class="btn-showcase"><a class="btn btn-light" href="https://www.linkedin.com/login" target="_blank"><i class="txt-linkedin" data-feather="linkedin"></i>LinkedIn </a><a class="btn btn-light" href="https://twitter.com/login?lang=en" target="_blank"><i class="txt-twitter" data-feather="twitter"></i>twitter</a><a class="btn btn-light" href="https://www.facebook.com/" target="_blank"><i class="txt-fb" data-feather="facebook"></i>facebook</a></div>
                                </div>
                                <p class="mt-4 mb-0 text-center"><a class="ms-2" href="ForgetPassword.aspx">Forgot Password</a></p>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- latest jquery-->
            <script src="assets/js/jquery.min.js"></script>
            <!-- Bootstrap js-->
            <script src="assets/js/bootstrap/bootstrap.bundle.min.js"></script>
            <!-- feather icon js-->
            <script src="assets/js/icons/feather-icon/feather.min.js"></script>
            <script src="assets/js/icons/feather-icon/feather-icon.js"></script>
            <!-- scrollbar js-->
            <!-- Sidebar jquery-->
            <script src="assets/js/config.js"></script>
            <!-- Plugins JS start-->
            <!-- calendar js-->
            <!-- Plugins JS Ends-->
            <!-- Theme js-->
            <script src="assets/js/script.js"></script>
            <script src="../../../mis/Login/sha512.js"></script>

            <script src="js/sha512.js"></script>
        </div>

    </form>

    <script>
        function ValidatePage() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Login');
            }
            if (Page_IsValid) {
                if (document.getElementById('<%= txtUserPassword.ClientID %>').value.length != 128) {
                    document.getElementById('<%= txtUserPassword.ClientID %>').value =
                        SHA512(SHA512(document.getElementById('<%= txtUserPassword.ClientID %>').value) +
      '<%= ViewState["RandomText"].ToString() %>');
                }
            }
            else {
                if (document.getElementById('<%= txtUserName.ClientID %>').value == "") {
                    $("input[name='txtUserName']").removeClass('TextBoxSuccess');
                    $("input[name='txtUserName']").addClass('TextBoxError');
                }
                else {
                    $("input[name='txtUserName']").removeClass('TextBoxError');
                    $("input[name='txtUserName']").addClass('TextBoxSuccess');
                }
                if (document.getElementById('<%= txtUserPassword.ClientID %>').value == "") {
                    $("input[name='txtUserPassword']").removeClass('TextBoxSuccess');
                    $("input[name='txtUserPassword']").addClass('TextBoxError');
                }
                else {
                    $("input[name='txtUserPassword']").removeClass('TextBoxError');
                    $("input[name='txtUserPassword']").addClass('TextBoxSuccess');
                }
                return false;
            }
        }


    </script>
    <script>
        $(document).ready(function () {
            localStorage.setItem('Cal_Val', "");
        });

        function togglePasswordVisibility(textboxId) {
            var txtBox = document.getElementById(textboxId);
            var toggleDiv = event.currentTarget;

            if (txtBox.type === "password") {
                txtBox.type = "text";
                toggleDiv.textContent = "hide";
            } else {
                txtBox.type = "password";
                toggleDiv.textContent = "show";
            }
        }

    </script>
</body>
</html>
