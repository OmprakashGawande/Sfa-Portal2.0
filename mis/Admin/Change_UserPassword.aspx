<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Change_UserPassword.aspx.cs" Inherits="mis_Masters_Change_UserPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper Watermarkimg">
        <!-- Main content -->
        <section class="content">
            <div class="container-fluid">
               
                
                        <div class="card">
                            <div class="card-header text-light">Change Password</div>
                            <div class="card-body">
                                <asp:ValidationSummary ID="jspopup" runat="server" ValidationGroup="a" ShowMessageBox="true" HeaderText="Errors: " ShowSummary="false" />

                                <asp:Label runat="server" ID="lblMsg"></asp:Label>
                                <fieldset>
                                    <legend>PassWord</legend>
                                    <p style="color: red">Note:-Password must contain one digit from 1 to 9, one lowercase letter, one uppercase letter, one special character, no space, and it must be 8-16 characters long.</p>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Current Password<span style="color: red">*</span></label>
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtCurrentPassword"
                                                        ErrorMessage="Enter Current Password" ValidationGroup="a" Text="<i class='fa fa-exclamation-circle'></i>" ForeColor="Red"></asp:RequiredFieldValidator>

                                                </span>
                                                <asp:TextBox runat="server" TextMode="Password" ID="txtCurrentPassword" CssClass="form-control" MaxLength="20"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
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
                                        <div class="col-md-3">
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
                                        <div class="col-md-3" style="margin-top:20px;">
                                            <div class="row mt-4">
                                                <div class="col-md-6 mt-2">
                                                    <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text="Save" CssClass="btn btn-success btn-block" ValidationGroup="a" />
                                                </div>
                                                <div class="col-md-6 mt-2">
                                                    <a href="Change_UserPassword.aspx" class="btn btn-default btn-block">Clear</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </fieldset>


                            </div>
                        </div>
                  
            </div>

            <!-- /.container-fluid -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

