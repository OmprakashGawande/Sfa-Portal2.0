<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">

    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <link rel="shortcut icon" href="image/favicon-icon.png" type="image/ico" />
    <title>Log in</title>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <link href="../../../mis/Login/AdminLTE.css" rel="stylesheet" />
    <link href="../../../mis/Login/bootstrap.css" rel="stylesheet" />
    <style>
        .bgleft_login {
            background: #d0343a url(image/SFA_LOGIN1.png) no-repeat center;
            background-size: cover;
            min-height: 100vh;
        }

        .bgright_login {
            background: black url(image/SFA_1.png) no-repeat bottom center;
            background-size: cover;
            min-height: 100vh;
            color: #badce8;
            font-family: 'Bitter', serif;
            padding: 30px;
            border-left: 2px solid #badce8;
            font-size: 16px;
        }

        .login-txt {
            text-align: center;
        }

        .bgright_login h3 {
            text-transform: uppercase;
            font-family: 'Bitter', serif;
            font-size: 18px;
            padding: 10px 0 30px;
            line-height: 22px;
        }

        .txtbox {
            border: 2px solid #78b630;
            background: none;
            padding: 9px 10px;
            width: 100%;
            border-radius: 5px;
            font-size: 14px;
        }

        .btn-submit {
            background: #78b630;
            color: #fff;
            padding: 8px 50px;
            font-size: 18px;
        }

        .brdtop {
            border-top: 2px solid #badce8;
            margin-top: 20px;
            padding-top: 10px;
        }

        .pt10 {
            padding-top: 10px;
        }

        .form-control-feedback {
            line-height: 40px;
        }

        .fancy-title {
            position: relative;
            margin-bottom: 10px;
        }

            .fancy-title.title-dotted-border {
                background-image: url(image/signin-icon.png), url(image/line-bg.png);
                background-position: center right, center;
                background-repeat: no-repeat, repeat-x;
            }

            .fancy-title h3 {
                position: relative;
                display: inline-block;
                background-color: black;
                margin: 0;
                text-transform: uppercase;
                padding: 5px 12px 5px 0;
            }

            .fancy-title.title-border-color:before, .fancy-title.title-border:before, .fancy-title.title-double-border:before {
                content: '';
                position: absolute;
                width: 100%;
                height: 0;
                border-top: 3px double #e5e5e5;
                left: auto;
                top: 46%;
                right: 0;
            }

            .fancy-title.title-border:before {
                top: 49%;
                border-top: 1px solid #eee;
            }

            .fancy-title.title-border-color:before {
                top: 49%;
                border-top: 1px solid #1abc9c;
                opacity: .6;
            }

        a, a.hover {
            color: #badce8;
        }

            a:hover {
                color: green;
            }

        .bgleft_login {
            display: block;
        }

        @media (max-width: 768px) {
            .bgleft_login {
                display: none;
            }
        }
    </style>
</head>
<body>

    <form id="form2" runat="server">
        <div class="container-fluid">
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Login" ShowMessageBox="true" ShowSummary="false" HeaderText="Errors: " />
            <div class="row  form1" runat="server" id="form_login" visible="true">
                <div class="col-lg-8 bgleft_login"></div>
                <div class="col-lg-4 bgright_login">
                    <div>
                        <div class="login-txt">
                            <%--<img src="image/logo_new.png" />--%>
                            <h3>SFA Technologies</h3>
                        </div>
                        <div class="fancy-title  title-dotted-border">
                            <h3>ERP LOGIN</h3>
                        </div>
                        <div class="form-group has-feedback">
                            <asp:Label ID="LblMsg" runat="server" ForeColor="red"></asp:Label>
                        </div>

                        <div class="form-group has-feedback">
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="rfvUserId" runat="server" Display="Dynamic" ControlToValidate="txtUserName" Text="<i class='fa fa-exclamation-circle' title='Required to Fill User Name!'></i>" ErrorMessage="Required to Fill User Name!" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Login"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid UserName Exp. XX0001!'></i>" ControlToValidate="txtUserName" ValidationExpression="^^[a-zA-Z0-9]*$" ErrorMessage="Invalid UserName Exp. XX0001" SetFocusOnError="true" ValidationGroup="Login"></asp:RegularExpressionValidator>
                            </span>
                            <asp:TextBox ID="txtUserName" runat="server" class="txtbox" placeholder="MY ERP CODE" MaxLength="50"></asp:TextBox>
                            <span class="glyphicon glyphicon-user form-control-feedback"></span>
                        </div>
                        <div class="form-group has-feedback">
                            <span class="pull-right">
                                <asp:RequiredFieldValidator ID="rfvpass" runat="server" Display="Dynamic" ControlToValidate="txtUserPassword" ErrorMessage="Required to Fill Password!" Text="<i class='fa fa-exclamation-circle' title='Required to Fill Password!'></i>" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Login"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="Dynamic" runat="server" ControlToValidate="txtUserPassword" ValidationExpression="^[a-zA-z0-9-_@#!*$&^]+$" Text="<i class='fa fa-exclamation-circle' title='Special Character allowed only (-_@#!*$&^).!'></i>" ErrorMessage="Special Character allowed only (-_@#!*$&^)." SetFocusOnError="true" ValidationGroup="Login"></asp:RegularExpressionValidator>
                            </span>
                            <asp:TextBox ID="txtUserPassword" runat="server" class="txtbox" placeholder="ERP PASSWORD" TextMode="Password" MaxLength="50"></asp:TextBox>
                            <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                        </div>
                        <div class="row">
                            <div class="col-lg-6 pt10">
                            </div>
                            <div class="col-lg-6 text-right">
                                <asp:ImageButton ID="btnLogin" runat="server" AccessKey="L" ToolTip="Shortcut Key (Alt + L)" OnClientClick="return ValidatePage();" OnClick="btnLogin_Click" ImageUrl="~/mis/image/login-btn.png" />
                                <%--<asp:Button ID="btnLogin" runat="server" AccessKey="L" class="btn btn-submit" Text="Login" ToolTip="Shortcut Key (Alt + L)" OnClick="btnLogin_Click" />--%>
                            </div>
                        </div>
                        <div class="brdtop">
                            <div class="row">
                                 <div class="col-lg-6">
                                    <p class=""><a id="A_Foorgot" href="ForgetPassword.aspx" class="btn">Forgot Password </a><i class="fa fa-question-circle"></i></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row form2" runat="server" id="form_otp_submit" visible="false">
                <div class="col-lg-8 bgleft_login"></div>
                <div class="col-lg-4 bgright_login">
                    <div>
                        <div class="login-txt">
                            <img src="image/logo_new.png" />
                            <h3>SFA Tchnologies</h3>
                        </div>
                        <div class="fancy-title  title-dotted-border">
                            <h3>OTP VERIFICATION</h3>
                        </div>
                        <div class="form-group has-feedback">
                            <asp:Label ID="lblOtpMsg" runat="server" ForeColor="red"></asp:Label>
                            <asp:HiddenField ID="hf_emp_id" runat="server" />
                        </div>

                        <div class="form-group has-feedback">
                            <asp:TextBox ID="txtEnterOTP" runat="server" class="txtbox" placeholder="ENTER OTP" MaxLength="50"></asp:TextBox>
                            <span class="glyphicon glyphicon-user form-control-feedback"></span>
                            <p style="margin-top: 10px; font-size: 13px; color: currentColor;">Please enter OTP to verify your mobile number.</p>
                        </div>
                        <div class="brdtop">
                            <div class="row">
                                <div class="col-lg-12 text-right">
                                    <a id="A_Back2" href="login.aspx" class="btn">
                                        <img src="image/backhome.png" style="height: 20px">
                                        Back To Login Page</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


        </div>
        <script src="../../../mis/Login/jquery.js"></script>
        <script src="../../../mis/Login/bootstrap.js"></script>
        <script src="../../../mis/Login/sha512.js"></script>

        <script src="js/sha512.js"></script>
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
    </form>

    <script>
        $(document).ready(function () {
            localStorage.setItem('Cal_Val', "");
        });
    </script>
</body>
</html>
