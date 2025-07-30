<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollEmpSalarySlipMassage.aspx.cs" Inherits="mis_Payroll_PayrollEmpSalarySlipMassage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Salary Slip Massage</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Financial Year<span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="form-control">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlFinancialYear" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Month <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                            <asp:ListItem Value="0">Select Month</asp:ListItem>
                                            <asp:ListItem Value="January">January</asp:ListItem>
                                            <asp:ListItem Value="February">February</asp:ListItem>
                                            <asp:ListItem Value="March">March</asp:ListItem>
                                            <asp:ListItem Value="April">April</asp:ListItem>
                                            <asp:ListItem Value="May">May</asp:ListItem>
                                            <asp:ListItem Value="June">June</asp:ListItem>
                                            <asp:ListItem Value="July">July</asp:ListItem>
                                            <asp:ListItem Value="August">August</asp:ListItem>
                                            <asp:ListItem Value="September">September</asp:ListItem>
                                            <asp:ListItem Value="October">October</asp:ListItem>
                                            <asp:ListItem Value="November">November</asp:ListItem>
                                            <asp:ListItem Value="December">December</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlMonth" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label>Massage</label><span style="color: red">*</span>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtMassage" onkeypress="javascript:tbx_fnAlphaOnly(event, this);" placeholder="Enter Massage" autocomplete="off" MaxLength="100"></asp:TextBox>
                                        <small><span id="valtxtMassage" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Show" ID="btnShow" OnClick="btnShow_Click" OnClientClick="return validateform();" />
                                    </div>

                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <a href="PayrollEmpSalarySlipMassage.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" PageSize="50" DataKeyNames="SalaryMsgID" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" AllowPaging="True" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Salary_Year" HeaderText="Salary Yeare" />
                                                <asp:BoundField DataField="Salary_Month" HeaderText="Salary Month" />
                                                <asp:BoundField DataField="Salary_Massage" HeaderText="Salary Massage" />
                                            </Columns>
                                        </asp:GridView>
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
        function validateform() {
            var msg = "";
            $("#valddlFinancialYear").html("");
            $("#valddlMonth").html("");
            $("#valtxtMassage").html("");
            if (document.getElementById('<%=ddlFinancialYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Financial year. \n";
                $("#valddlFinancialYear").html("Select Financial year.");
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
                $("#valddlMonth").html("Select Month.");
            }
            if (document.getElementById('<%=txtMassage.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Massage. \n";
                $("#valtxtMassage").html("Enter Massage.");
            }
             if (msg != "") {
                 alert(msg);
                 return false;
             }

         }
    </script>
</asp:Content>

