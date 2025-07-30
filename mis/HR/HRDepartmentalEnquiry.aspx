<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRDepartmentalEnquiry.aspx.cs" Inherits="mis_HR_HRDepartmentalEnquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title">Departmental Enquiry</h3>
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Office Name <span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlOffice" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Employee Name <span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlEmployeeName" runat="server" CssClass="form-control select2">
                                                <asp:ListItem>Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Order No <span style="color: red;"> *</span></label>
                                            <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control" placeholder="Enter Order No"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Order Date <span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtOrderDate" runat="server" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" data-date-start-date="-365d" onpaste="return false" PlaceHolder="Select Order Date" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Enquiry Officer <span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlEnquiryOfficer" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Presenting Officer <span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlPresentingOfficer" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Remark <span style="color: red;"> *</span></label>
                                            <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" placeholder="Enter Remark"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label></label>
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-block" Text="Save" OnClick="btnSave_Click" OnClientClick="return Validateform();"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function Validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlOffice.ClientID %>').selectedIndex == 0) {
                msg = msg + "Select Office Name.\n";
            }
            if (document.getElementById('<%=ddlEmployeeName.ClientID %>').selectedIndex == 0) {
                msg = msg + "Select Employee Name.\n";
            }
            if (document.getElementById('<%=txtOrderNo.ClientID %>').value.trim() == 0) {
                msg = msg + "Enter Order No.\n";
            }
            if (document.getElementById('<%=txtOrderDate.ClientID %>').value.trim() == 0) {
                msg = msg + "Enter Order Date.\n";
            }
            if (document.getElementById('<%=ddlEnquiryOfficer.ClientID %>').selectedIndex == 0) {
                msg = msg + "Select Enquiry Officer.\n";
            }
            if (document.getElementById('<%=ddlPresentingOfficer.ClientID %>').selectedIndex == 0) {
                msg = msg + "Select Presenting Officer.\n";
            }
            if (document.getElementById('<%=txtRemark.ClientID %>').value.trim() == 0) {
                msg = msg + "Enter Remark.\n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }

        }
    </script>
</asp:Content>

