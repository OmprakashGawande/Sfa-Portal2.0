<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgetPassword.aspx.cs" Inherits="mis_ForgetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>SFA-Portal</title>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link rel="shortcut icon" href="image/favicon-icon.png" type="image/ico" />
    <link href="css/font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="css/Ionicons/css/ionicons.css" rel="stylesheet" />
    <link href="css/select2.css" rel="stylesheet" />
    <link href="css/AdminLTE.css" rel="stylesheet" />
    <link href="css/skin-green-light.css" rel="stylesheet" />
    <link href="css/daterangepicker.css" rel="stylesheet" />
    <link href="css/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="css/StyleSheet.css" rel="stylesheet" />
    <link href="css/jquery.datetimepicker.css" rel="stylesheet" />
</head>
<body style="background: #d3313b center; background-size: cover; min-height: 100vh;">
    <form id="form1" runat="server">
           <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="Login" ShowMessageBox="true" ShowSummary="false" HeaderText="Errors: " />
           <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" HeaderText="Errors: " />
        <br />
        <br />
        <div class="container">
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-4">
                    <div class="box box-success" style="border-radius:12px;">
                        <div class="box-header">
                            <h3>Forgot Password</h3>
                        </div>
                        <div class="box-body">
                            <div class="row" runat="server" id="divGenerateForgotPassword" visible="false">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>User-Id <span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvUserId" runat="server" Display="Dynamic" ControlToValidate="txtUserId" Text="<i class='fa fa-exclamation-circle' title='Required to Fill User-Id!'></i>" ErrorMessage="Required to Fill User-Id!" SetFocusOnError="true" ForeColor="Red" ValidationGroup="Login"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Display="Dynamic" runat="server" Text="<i class='fa fa-exclamation-circle' title='Invalid User-Id Exp. XX0001!'></i>" ControlToValidate="txtUserId" ValidationExpression="^^[a-zA-Z0-9]*$" ErrorMessage="Invalid User-Id Exp. XX0001" SetFocusOnError="true" ValidationGroup="Login"></asp:RegularExpressionValidator>
                                        </span>
                                        <asp:TextBox runat="server" ID="txtUserId" CssClass="form-control" MaxLength="6" AutoComplete="off" onpaste="return false;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Registered Email-Id <span class="text-danger">*</span></label>
                                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" MaxLength="6" AutoComplete="off" onpaste="return false;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12" style="margin-top: 23px;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Button runat="server" ID="btnSubmit" ValidationGroup="Login" CausesValidation="true" CssClass="btn btn-success btn-block" Text="Submit" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <a href="Login.aspx" class="btn btn-default btn-block">Back To Log-In</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="divForgotPassword">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>New Password<span style="color: red">*</span></label>
                                        <span class="fa-pull-right">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ToolTip="Invalid Password"
                                                CssClass="fa-pull-right" Text="<i class='fa fa-exclamation-circle'></i>" ControlToValidate="txtNewPassword" ValidationGroup="a" Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Password"
                                                ValidationExpression="^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.* ).{8,16}$"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txtNewPassword"
                                                ErrorMessage="Enter New Password" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ForeColor="Red"></asp:RequiredFieldValidator>

                                        </span>
                                        <asp:TextBox runat="server" TextMode="Password" ID="txtNewPassword" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Confirm new Password<span style="color: red">*</span></label>
                                        <span class="fa-pull-right">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ToolTip="Invalid Password"
                                                CssClass="fa-pull-right" Text="<i class='fa fa-exclamation-circle'></i>" ControlToValidate="txtConfirmPassword" ValidationGroup="a" Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Password"
                                                ValidationExpression="^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.* ).{8,16}$"></asp:RegularExpressionValidator>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtConfirmPassword"
                                                ErrorMessage="Enter Confirm Password" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ForeColor="Red"></asp:RequiredFieldValidator>


                                        </span>
                                        <asp:TextBox runat="server" TextMode="Password" ID="txtConfirmPassword" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPassword"
                                            ControlToValidate="txtConfirmPassword" Display="Dynamic" ToolTip="Enter valid value" ForeColor="Red"
                                            ErrorMessage="New and Confirm Password is not same" Operator="Equal" Type="String"></asp:CompareValidator>
                                    </div>
                                </div>

                                <div class="col-md-12" style="margin-top: 23px;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Button runat="server" ID="btnForgotPassword" ValidationGroup="a" CausesValidation="true" CssClass="btn btn-success btn-block" Text="Forgot Password" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <a href="Login.aspx" class="btn btn-default btn-block">Back To Log-In</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">

                                    <p style="color: red">
                                        <small>Note:- Password must contain
                                        <ul>
                                            <li>one digit from 1 to 9 </li>
                                            <li>one lowercase letter </li>
                                            <li>one uppercase letter </li>
                                            <li>one special character </li>
                                            <li>no space, and it must be 8-16 characters long. </li>
                                        </ul>
                                        </small>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4"></div>
            </div>
        </div>
        <script src="../../../mis/js/jquery-2.2.4.js"></script>
        <script src="../../../mis/js/bootstrap.js"></script>
        <script src="../../../mis/js/Script.js"></script>
        <script src="../../../mis/js/adminlte.js"></script>
        <script src="../../../mis/js/moment.js"></script>
        <script src="../../../mis/js/daterangepicker.js"></script>
        <script src="../../../mis/js/bootstrap-datepicker.js"></script>
        <script src="../../../mis/js/select2.full.js"></script>

        <script src="../../../mis/js/jquery.datetimepicker.js"></script>
    </form>
</body>
</html>
