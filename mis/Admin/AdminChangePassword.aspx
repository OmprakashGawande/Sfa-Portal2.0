<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminChangePassword.aspx.cs" Inherits="mis_Admin_AdminChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">CHANGE PASSWORD</h3>
                </div>
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-4">
                            <div class="form-group">
                                <label><b>OLD PASSWORD<span style="color: red;"> *</span></b></label>
                                <asp:TextBox ID="txtOldPassword" runat="server" placeholder="Enter Old Password (min length 6 character)" class="form-control" MaxLength="32" TextMode="Password" onkeypress="return validateusername(event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label><b>NEW PASSWORD<span style="color: red;"> *</span></b></label>
                                <asp:TextBox ID="txtNewPassword" runat="server" placeholder="Enter New Password (min length 6 character)" class="form-control" MaxLength="32" TextMode="Password" onblur="return CheckPassword();" onkeypress="return validateusername(event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label><b>CONFIRM NEW PASSWORD<span style="color: red;"> *</span></b></label>
                                <asp:TextBox ID="txtConfirmPassword" runat="server" placeholder="Enter Confirm Password (min length 6 character)" class="form-control" MaxLength="32" TextMode="Password" onkeypress="return validateusername(event);"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-4">
                            <div class="row">
                                <div class="col-md-6">
                                    <div style="margin-top: 25px">
                                        <asp:Button ID="btnChange" runat="server" Text="CHANGE" class="btn btn-block btn-success" OnClientClick="return validateform();" OnClick="btnChange_Click" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div style="margin-top: 25px">
                                        <asp:Button ID="btnCancel" runat="server" Text="CANCEL" class="btn btn-block btn-default" OnClientClick="return Cancel()" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function validateform() {
            var msg = "";
            var txtNewPassword = document.getElementById('<%=txtNewPassword.ClientID%>').value.trim();

             var txtConfirmPassword = document.getElementById('<%=txtConfirmPassword.ClientID%>').value.trim();

             if (document.getElementById('<%=txtOldPassword.ClientID%>').value.trim() == "") {
                 msg += "Enter Old Password \n";
             }
             if (document.getElementById('<%=txtNewPassword.ClientID%>').value.trim() == "") {
                 msg += "Enter New Password \n";
             }
             else if (document.getElementById('<%=txtNewPassword.ClientID%>').value.length < 6) {
                msg += "New Password Should Be Atleast 6 or More digit. \n";
            }
            if (document.getElementById('<%=txtConfirmPassword.ClientID%>').value.trim() == "") {
                 msg += "Enter Confirm New Password \n";
             }
             if (txtNewPassword != "" && txtConfirmPassword != "") {
                 if (txtNewPassword != txtConfirmPassword) {
                     msg += "New Password And Confirm New Password Do Not Match. Please Try Again. \n";
                 }
             }
             if (msg != "") {
                 alert(msg);
                 return false;
             }
             else {
                 if (document.getElementById('<%=txtOldPassword.ClientID%>').value.trim() == document.getElementById('<%=txtNewPassword.ClientID%>').value.trim()) {
                    msg += "Old password and new password cannot be same. \n";
                }
                if (msg != "") {
                    alert(msg);
                    return false;
                }
                else {
                    if (confirm("Do you really want to Change Password ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }
        function CheckPassword() {
            if (document.getElementById('<%=txtNewPassword.ClientID%>').value.length < 6) {
                alert("New Password Should Be Atleast 6 digit.");
            }
        }
    </script>
</asp:Content>

