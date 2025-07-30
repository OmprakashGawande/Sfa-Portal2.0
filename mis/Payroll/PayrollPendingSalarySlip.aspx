<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollPendingSalarySlip.aspx.cs" Inherits="mis_Payroll_PayrollPendingSalarySlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .Grid td {
            padding: 3px !important;
        }

            .Grid td input {
                padding: 3px 3px !important;
                text-align: right !important;
                font-size: 12px !important;
                height: 26px !important;
            }

        .Grid th {
            text-align: center;
        }

        .ss {
            text-align: left !important;
        }

        .bgcolor {
            background-color: #eeeeee !important;
        }

        .box {
            min-height: initial !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Pending salary Detail</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-block" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateSearch();" />
                            </div>
                        </div>
                    </div>
                    <div id="DivDetail" runat="server">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Employee<span style="color: red;">*</span></label>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Month<span style="color: red;">*</span></label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select Month</asp:ListItem>
                                        <asp:ListItem Value="1">January</asp:ListItem>
                                        <asp:ListItem Value="2">February</asp:ListItem>
                                        <asp:ListItem Value="3">March</asp:ListItem>
                                        <asp:ListItem Value="4">April</asp:ListItem>
                                        <asp:ListItem Value="5">May</asp:ListItem>
                                        <asp:ListItem Value="6">June</asp:ListItem>
                                        <asp:ListItem Value="7">July</asp:ListItem>
                                        <asp:ListItem Value="8">August</asp:ListItem>
                                        <asp:ListItem Value="9">September</asp:ListItem>
                                        <asp:ListItem Value="10">October</asp:ListItem>
                                        <asp:ListItem Value="11">November</asp:ListItem>
                                        <asp:ListItem Value="12">December</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="box-header">
                                    <h3 class="box-title">Earning Detail</h3>
                                </div>
                                <div class="table-responsive">
                                    <table id="tblEarning" class="table table-bordered table-striped Grid">
                                        <tr>
                                            <th scope="col">Earning Heads</th>
                                            <th scope="col" style="width: 100px;">Remaining Amount To Be Paid</th>
                                        </tr>
                                        <tr>
                                            <td>BASIC SALARY</td>
                                            <td>
                                                <asp:TextBox ID="txtSalary_Basic" runat="server" Text="0" class="form-control" MaxLength="13" Enabled="false"></asp:TextBox></td>
                                        </tr>
                                        <asp:Repeater ID="RepeaterEarning" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEarnDeduction_Name" runat="server" Text='<%# Eval("EarnDeduction_Name").ToString()%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemainingEarning" runat="server" Text="0" class="form-control" MaxLength="13" onfocusout="return CalculateEarning();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <td><b>Total Earning</b></td>
                                            <td>
                                                <asp:TextBox ID="txtETotalRemaining" ClientIDMode="Static" runat="server" Text="0" class="form-control" MaxLength="13"></asp:TextBox></td>
                                        </tr>
                                    </table>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box-header">
                                    <h3 class="box-title">Deduction Detail</h3>
                                </div>
                                <div class="table-responsive">
                                    <table id="tblDeduction" class="table table-bordered table-striped Grid">
                                        <tr>
                                            <th scope="col">Deduction Heads</th>
                                            <th scope="col" style="width: 100px;">Remaining Amount To Be Paid</th>
                                        </tr>
                                        <asp:Repeater ID="RepeaterDeduction" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEarnDeduction_Name" runat="server" Text='<%# Eval("EarnDeduction_Name").ToString()%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemainingDeduction" runat="server" Text="0" class="form-control" MaxLength="13" onfocusout="return CalculateDeduction();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <td><b>Policy : </b></td>
                                            <td>
                                                <asp:TextBox ID="txtPolicyRemaining" runat="server" Text="0" class="form-control" MaxLength="13" onfocusout="return CalculateDeduction();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td><b>Total Deduction</b></td>
                                            <td>
                                                <asp:TextBox ID="txtDTotalRemaining" ClientIDMode="Static" runat="server" Text="0" class="form-control" MaxLength="13"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Total Arrear Earning Amount<span style="color: red;">*</span></label>
                                    <asp:TextBox ID="txtTotalEarning" ClientIDMode="Static" runat="server" Text='0' class="form-control" MaxLength="13"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Total Arrear Deduction Amount<span style="color: red;">*</span></label>
                                    <asp:TextBox ID="txtTotalDeduction" ClientIDMode="Static" runat="server" Text='0' class="form-control" MaxLength="13"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Arrear Net Payment Amount<span style="color: red;">*</span></label>
                                    <asp:TextBox ID="txtNetPayment" ClientIDMode="Static" runat="server" Text='0' class="form-control" MaxLength="13"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group"></div>
                        <div class="row">
                            <div class="col-md-8"></div>
                            <div class="col-md-2">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                            </div>
                            <div class="col-md-2">
                                <a class="btn btn-block btn-default" href="PayrollEmpArrearDetail.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Pending Salary Detail</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <%--<asp:GridView ID="GridView1" DataKeyNames="EmpArrearID" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("Emp_Name").ToString()%>'></asp:Label>
                                                <asp:Label ID="Emp_ID" runat="server" Text='<%# Eval("Emp_ID").ToString()%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("FromYear").ToString()%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="To Year">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("ToYear").ToString()%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Arrear Earning">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("TotalEarning").ToString()%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Arrear Deduction">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("TotalDeduction").ToString()%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Arrear Net Payment Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("NetPaymentAmount").ToString()%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Earn Deduction Detail">
                                            <ItemTemplate>
                                                <a id="a1" runat="server" style="font-weight: bold" target="_blank" href='<%# "EmployeeWiseEarnDedDetail.aspx?Emp_ID=" +APIProcedure.Client_Encrypt(Eval("Emp_ID").ToString())+"&&EmpArrearID=" +APIProcedure.Client_Encrypt(Eval("EmpArrearID").ToString()) %>'>View Detail</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>--%>
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
        function CalculateEarning() {

            var i = 0;
            var TotalPaidEarning = 0;
            var TotalRemaining = 0;
            var trCount = $('#tblEarning tr').length - 1;

            $('#tblEarning tr').each(function (index) {
                if (i > 0 && i < trCount) {
                    debugger;
                    var PaidEarning = $(this).children("td").eq(1).find('input[type="text"]').val();

                    if (PaidEarning.trim() == "")
                        PaidEarning = 0;

                    var TotPaidEarning = TotalPaidEarning;

                    TotalPaidEarning = parseFloat(parseFloat(TotPaidEarning) + parseFloat(PaidEarning)).toFixed(2);
                }
                i++;
            });

            document.getElementById("txtTotalEarning").value = TotalPaidEarning;
            document.getElementById("txtETotalRemaining").value = TotalPaidEarning;

            var TotalDeduction = document.getElementById("txtTotalDeduction").value.trim();
            if (TotalDeduction.trim() == "")
                TotalDeduction = 0;

            document.getElementById("txtNetPayment").value = parseFloat(parseFloat(TotalPaidEarning) - parseFloat(TotalDeduction)).toFixed(2);

            return true;
        }

        function CalculateDeduction() {

            var i = 0;
            var TotalPaidDeduction = 0;
            var TotalRemaining = 0;

            var trCount = $('#tblDeduction tr').length - 1;

            $('#tblDeduction tr').each(function (index) {
                if (i > 0 && i < trCount) {
                    debugger;
                    var PaidDeduction = $(this).children("td").eq(1).find('input[type="text"]').val();

                    if (PaidDeduction.trim() == "")
                        PaidDeduction = 0;

                    var TotPaidDeduction = TotalPaidDeduction;

                    TotalPaidDeduction = parseFloat(parseFloat(TotPaidDeduction) + parseFloat(PaidDeduction)).toFixed(2);

                }
                i++;
            });

            document.getElementById("txtTotalDeduction").value = TotalPaidDeduction;
            document.getElementById("txtDTotalRemaining").value = TotalPaidDeduction;

            var TotalDeduction = document.getElementById("txtTotalEarning").value.trim();
            if (TotalDeduction.trim() == "")
                TotalDeduction = 0;

            document.getElementById("txtNetPayment").value = parseFloat(parseFloat(TotalPaidEarning) - parseFloat(TotalDeduction)).toFixed(2);

            return true;
        }


        function validateSearch() {
            var msg = "";
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Year. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;

            }
        }
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Save Details ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

    </script>
</asp:Content>

