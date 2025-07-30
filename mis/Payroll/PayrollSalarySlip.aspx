<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollSalarySlip.aspx.cs" Inherits="mis_Payroll_PayrollSalarySlip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .box {
            position: relative;
            border-radius: 3px;
            background: #ffffff;
            border-top: 3px solid #d2d6de;
            margin-bottom: 20px;
            width: 100%;
            box-shadow: 0 1px 1px rgba(0,0,0,0.1);
            box-shadow: none;
            border-top: none;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #e1e1e1;
        }

        .text-center h3 {
            font-size: 20px;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 2px 3px;
        }

        #subheading-salary {
            font-size: 16px;
        }

        .salary-logo {
            -webkit-filter: grayscale(100%);
            filter: grayscale(100%);
            width: 60px;
        }

        .printbutton {
            border-top: 1px dashed #838383;
            margin-top: 5px;
            padding-top: 5px;
        }

        table h4 {
            font-size: 15px;
        }

        .table {
            margin-bottom: 5px;
        }

        th, td, h3 {
            text-transform: uppercase !important;
        }
        @media print {
            body * {
                visibility: hidden;
            }

            .printbutton {
                display: none;
            }

            .text-center h3 {
                font-size: 13px;
                margin: 0px;
                padding: 0px;
            }

            .section-to-print, .section-to-print * {
                visibility: visible;
                font-size: 10px;
                margin: 0px !important;
            }

            .subheading-salary {
                font-size: 12px !important;
            }

            .salary-logo {
                width: 40px;
            }

            .box-header {
                padding: 2px;
                margin-top:-10px;
            }
            .section-to-print{
                margin-top:-20px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content" style="min-height: 80px; padding-bottom: 0px;">
            <!-- Default box -->
            <div class="box box-success" style="min-height: 80px;">
                <div class="box-header">
                    <h3 class="box-title">Employee Wise Salary Detail</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
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
                                    <%--<asp:ListItem Value="0">Select Month</asp:ListItem>
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
                                    <asp:ListItem Value="12">December</asp:ListItem>--%>

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
                            <div class="form-group">
                                <asp:Label ID="lblNotGenerated" Style="font-size:18px; color:red;" runat="server" Text="Salary Not Generated"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="content" id="DivSlip" runat="server" style="padding-top: 0px;">
            <!-- Default box -->
             <div class="box box-default section-to-print">
                <div class="box-header text-center">
                    <h3 class="">
                        <img src="../image/Sfa_logo.png" class="salary-logo" />
                        &nbsp;&nbsp; SFA Technologies Pvt. Ltd. SFA Tower 28-Sector A, Kasturba Nagar, Chetak Bridge, Bhopal - 462023, India<br />
                        <span id="subheading-salary" class="subheading-salary">PAY SLIP FOR THE MONTH OF <span id="lblMonth" runat="server"></span>&nbsp; <span id="lblFinancialYear" runat="server"></span>&nbsp; <span style="color: red;" id="lblGenStatus" runat="server"></span></span></h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div>
                                <table class="table table-bordered ">
                                    <tbody>
                                        <tr>
                                            <th>NAME OF EMPLOYEE :
                                            </th>
                                            <td>
                                                <asp:Label ID="lblEmp_Name" runat="server" Text=""></asp:Label>
                                            </td>
                                            <th>BANK ACCOUNT NUMBER :
                                            </th>
                                            <td>
                                                <asp:Label ID="lblBank_AccountNo" runat="server" Text=""></asp:Label>
                                            </td>
                                            <th>EPF/UAN :
                                            </th>
                                            <td>
                                                <asp:Label ID="lblEPF_No" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>DESIGNATION :
                                            </th>
                                            <td>
                                                <asp:Label ID="lblDesignation_Name" runat="server" Text=""></asp:Label>
                                            </td>
                                            <th>BANK NAME :
                                            </th>
                                            <td>
                                                <asp:Label ID="lblBank_Name" runat="server" Text=""></asp:Label>
                                            </td>
                                            <th>G.Ins NUMBER :
                                            </th>
                                            <td>
                                                <asp:Label ID="lblGroupInsurance_No" runat="server" Text=""></asp:Label>
                                            </td>
                                            <%--<th>
                                                <asp:Label ID="lblEmp_GpfType" runat="server" Text=""></asp:Label>
                                                NUMBER :
                                            </th>
                                            <td>
                                                <asp:Label ID="lblEmp_GpfNo" runat="server" Text=""></asp:Label>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <th>EMPLOYEE CODE :
                                            </th>
                                            <td>
                                                <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                                            </td>
                                            <th>IFSC CODE :
                                            </th>
                                            <td>
                                                <asp:Label ID="lblIFSCCode" runat="server" Text=""></asp:Label>
                                            </td>
                                            <th>NET SALARY :
                                            </th>
                                            <td>
                                                <asp:Label ID="lblSalary_NetSalary" runat="server" Text=""></asp:Label>
                                            </td>
                                            <%-- <th>G.I. NUMBER :
                                            </th>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text="NA"></asp:Label>
                                            </td>--%>
                                        </tr>
                                        <%-- <tr>
                                            <th>BASIC SALARY :
                                            </th>
                                            <td>
                                                <asp:Label ID="lblSalary_Basic" runat="server" Text=""></asp:Label>
                                            </td>
                                            <th>NET SALARY :
                                            </th>
                                            <td>
                                                <asp:Label ID="lblSalary_NetSalary" runat="server" Text=""></asp:Label>
                                            </td>

                                        </tr>--%>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <table class="table table-striped" style="margin-bottom: 0px;">
                                <tbody>
                                    <tr>
                                        <td style="width: 50%">

                                            <div class="" style="padding-bottom: 0; padding-left: 5px; font-weight: 700;">
                                                <h4>PAY</h4>
                                            </div>
                                            <div class="table-responsive">
                                                <div>
                                                    <table class="table table-bordered table-striped Grid earning-table">
                                                        <tbody>
                                                            <tr>
                                                                <th>BASIC SALARY :
                                                                </th>
                                                                <td style="text-align:right;">
                                                                    <asp:Label ID="lblSalary_Basic" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <%--<th>BASIC PAY :</th>
                                                                <td>
                                                                    <asp:Label ID="lblSalary_NoDayEarnAmt" runat="server" Text=""></asp:Label></td>--%>
                                                            </tr>
                                                            <asp:Repeater ID="RepeaterEarning" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <th>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("EarnDeduction_Name").ToString()%>'></asp:Label>
                                                                            :</th>
                                                                        <td style="text-align:right;">
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Earning").ToString()%>'></asp:Label></td>
                                                                    </tr>
                                                                </ItemTemplate>

                                                            </asp:Repeater>

                                                            <tr class="total_salary">
                                                                <th>TOTAL PAY :</th>
                                                                <th style="text-align:right;">
                                                                    <asp:Label ID="lblSalary_EarningTotal" runat="server" Text=""></asp:Label></th>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>

                                            </div>

                                        </td>
                                        <td style="width: 50%">
                                            <div class="">
                                                <div class="" style="padding-bottom: 0; padding-left: 5px; font-weight: 700;">
                                                    <h4>DEDUCTIONS</h4>

                                                </div>
                                                <div class="">
                                                    <div class="table-responsive">
                                                        <div>
                                                            <table class="table table-bordered table-striped Grid deduction-table">
                                                                <tbody>
                                                                    <tr class="hidden">
                                                                        <th>SALARY DEDUCTION (For Absent Days) :</th>
                                                                        <td style="text-align:right;">
                                                                            <asp:Label ID="lblSalary_NoDayDeduAmt" runat="server" Text=""></asp:Label></td>
                                                                    </tr>
                                                                    <asp:Repeater ID="RepeaterDeduction" runat="server">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("EarnDeduction_Name").ToString()%>'></asp:Label>
                                                                                    :</th>
                                                                                <td style="text-align:right;">
                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Earning").ToString()%>'></asp:Label></td>
                                                                            </tr>
                                                                        </ItemTemplate>

                                                                    </asp:Repeater>
                                                                    <tr>
                                                                        <th>LIC PREMIUM:</th>
                                                                        <th style="text-align:right;">
                                                                            <asp:Label ID="lblPolicyDeduction" runat="server" Text=""></asp:Label></th>
                                                                    </tr>
                                                                    <tr class="total_salary">
                                                                        <th>TOTAL DEDUCTION:</th>
                                                                        <th style="text-align:right;">
                                                                            <asp:Label ID="lblSalary_DeductionTotal" runat="server" Text=""></asp:Label></th>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th><!-- WISH YOU A VERY HAPPY NEW YEAR...!!--></th>
                                        <th style="text-align: right;">THIS IS A COMPUTER GENERATED PAYSLIP, SIGNATURE NOT REQUIRED</th>
                                    </tr>
                                </tbody>
                            </table>
                            <div style="text-align: center;" class="printbutton">
                                <input type="button" class="btn btn-primary" value="Print" onclick="window.print()">
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

            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Financial Year. \n";
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
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

