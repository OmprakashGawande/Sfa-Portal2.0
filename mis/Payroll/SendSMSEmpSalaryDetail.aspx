<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SendSMSEmpSalaryDetail.aspx.cs" Inherits="mis_Payroll_SendSMSEmpSalaryDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">SEND SMS</h3>
                </div>

                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Month <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                    <asp:ListItem Value="0">Select Month</asp:ListItem>
                                    <asp:ListItem Value="01">January</asp:ListItem>
                                    <asp:ListItem Value="02">February</asp:ListItem>
                                    <asp:ListItem Value="03">March</asp:ListItem>
                                    <asp:ListItem Value="04">April</asp:ListItem>
                                    <asp:ListItem Value="05">May</asp:ListItem>
                                    <asp:ListItem Value="06">June</asp:ListItem>
                                    <asp:ListItem Value="07">July</asp:ListItem>
                                    <asp:ListItem Value="08">August</asp:ListItem>
                                    <asp:ListItem Value="09">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Employee Type<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmpType" runat="server" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="Permanent">Regular/Permanent</asp:ListItem>
                                    <asp:ListItem Value="Fixed Employee">Fixed Employee(स्थाई कर्मी)</asp:ListItem>
                                    <asp:ListItem Value="Contigent Employee">Contigent Employee</asp:ListItem>
                                    <asp:ListItem Value="Samvida Employee">Samvida Employee</asp:ListItem>
                                    <asp:ListItem Value="Theka Shramik">Theka Shramik</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="30">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="30px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="EMPLOYEE NAME" />
                                        <asp:BoundField DataField="Emp_MobileNo" HeaderText="MOBILE NUMBER" />
                                        <asp:BoundField DataField="Salary_NetSalary" HeaderText="SALARY" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group pull-left">
                                <asp:Button ID="btnSendSMS" runat="server" Text="Send SMS" CssClass="btn btn-success" OnClick="btnSendSMS_Click" OnClientClick="return SendSMS();" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function checkAll(objRef) {
            debugger;
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }

        function validateform() {
            var msg = "";

            if (document.getElementById('<%=ddlOfficeName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office Name. \n";
            }
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Year. \n";
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
            }
            if (document.getElementById('<%=ddlEmpType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Type. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }

        function SendSMS() {
            if (document.getElementById('<%=btnSendSMS.ClientID%>').value.trim() == "Send SMS") {
                if (confirm("Do you really want to Send SMS ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>

