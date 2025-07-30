<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollEarDedSummarySection.aspx.cs" Inherits="mis_Payroll_PayrollEarDedSummarySection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .table-responsive table tr {
            font-size: 12px !important;
            font-family: monospace;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #999 !important;
            padding: 3px;
            font-size: 13px !important;
            line-height: 12px !important;
        }

        th {
            background-color: #f5f5f5 !important;
        }

        td.footerdata {
            font-size: 13px !important;
            font-family: monospace;
            padding: 3px !important;
            line-height: 12px !important;
            text-align: initial;
            color: #2b2b2b;
        }

            td.footerdata b {
                font-weight: 900 !important;
                font-size: 13px !important;
                color: black;
            }

        .reportheading h3 {
            font-size: 16px !important;
            line-height: 18px !important;
        }

        .ReportSection {
            background: none;
            margin: 0px;
            font-size: 14px;
            font-weight: 600;
            padding: 2px !important;
            font-family: monospace;
            text-align: center;
        }

        h4 {
            background: none;
            margin: 0px;
            font-size: 14px;
            font-weight: 600;
            padding: 2px !important;
            font-family: monospace;
            text-align: center;
        }

        @media print {
            .printbutton {
                display: none;
            }

            .hiddenfield {
                display: none;
            }

            .box.box-default.section-to-print {
                border: none;
            }

            .box-header {
                display: none;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-default section-to-print">
                <div class="box-header">
                    Final Summary
                </div>
                <div class="box-body">

                    <div class="row Hiderow hiddenfield">
                        <div class="col-md-12">
                            <p>
                                For section wise summary, please <a class="text-orange" target="_blank" href="#">click here.</a>
                            </p>
                            <p>
                                <br />
                            </p>
                        </div>
                        <br />
                        <div class="clearfix"></div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <%--                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Section No <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="form-control" SelectionMode="Multiple">
                                </asp:DropDownList>
                            </div>
                        </div>--%>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Section No </label>
                                <span style="color: red">*</span>
                                <asp:ListBox runat="server" ID="ddlSection" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>


                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Year <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Month <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
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
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Type of Post (पद प्रकार) <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmp_TypeOfPost" runat="server" class="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="Permanent">Regular/Permanent</asp:ListItem>
                                    <asp:ListItem Value="Fixed Employee">Fixed Employee(स्थाई कर्मी)</asp:ListItem>
                                    <asp:ListItem Value="Contigent Employee">Contigent Employee</asp:ListItem>
                                    <asp:ListItem Value="Samvida Employee">Samvida Employee</asp:ListItem>
                                    <asp:ListItem Value="Theka Shramik">Theka Shramik</asp:ListItem>
                                    <asp:ListItem Value="Outsource Employee">Outsource Employee</asp:ListItem>
									<asp:ListItem Value="Other Employee">Other Employee</asp:ListItem>
									<asp:ListItem Value="Deputation Employee">Deputation Employee</asp:ListItem>
                                    <asp:ListItem Value="Contractual Employee">Contractual Employee</asp:ListItem>
                                    <asp:ListItem Value="Daily Wages Employee">Daily Wages Employee</asp:ListItem>
                                    <asp:ListItem Value="Daily Wages Federation">Daily Wages Federation</asp:ListItem>
                                    <asp:ListItem Value="Job Rate Employee">Job Rate Employee</asp:ListItem>
                                     <asp:ListItem Value="Permanent NPF">Regular/Permanent NPF</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Search" ID="btnShow" OnClick="btnShow_Click" OnClientClick="return validateform();" />
                            </div>

                        </div>
                        <div class="col-md-12">
                            <asp:Label ID="lblWarningMessage" runat="server" Text=""></asp:Label>
                        </div>
                    </div>


                    <div class="row ReportSection" id="ReportSection" runat="server" visible="false">
                        <div class="col-md-12">
                            <div class="text-center reportheading">
                                <h3 class="">&nbsp;&nbsp;
                                    <asp:Label ID="lblOfficeName" runat="server" Text=""></asp:Label><br />
                                    <span id="subheading-salary" class="subheading-salary">FINAL SUMMARY FOR THE MONTH OF <span id="lblMonth" runat="server"></span>&nbsp; <span id="lblFinancialYear" runat="server"></span></span></h3>
                            </div>
                            <table class="table table-striped" style="margin-bottom: 0px;">
                                <tbody>
                                    <tr>
                                        <td style="width: 50%; padding: 0px 10px 10px 0px">
                                            <div class="">
                                                <h4>EARNINGS</h4>
                                            </div>
                                            <div class="table-responsive">
                                                <div>
                                                    <table class="table table-bordered table-striped Grid earning-table">
                                                        <tbody>
                                                            <tr>
                                                                <th>BASIC :
                                                                </th>
                                                                <td style="text-align: right;">
                                                                    <asp:Label ID="lblSalary_Basic" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <asp:Repeater ID="RepeaterEarning" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <th>
                                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("EarnDeduction_Name").ToString()%>'></asp:Label>
                                                                            :</th>
                                                                        <td style="text-align: right;">
                                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Amount").ToString()%>'></asp:Label></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>

                                                            <tr class="total_salary">
                                                                <th>GROSS SALARY :</th>
                                                                <th style="text-align: right;">
                                                                    <asp:Label ID="lblSalary_GrossSalary" runat="server" Text=""></asp:Label>
                                                                </th>
                                                            </tr>
                                                            <tr class="total_salary">
                                                                <th>NET AMOUNT :</th>
                                                                <th style="text-align: right;">
                                                                    <asp:Label ID="lblSalary_NetAmount" runat="server" Text=""></asp:Label>
                                                                </th>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </td>
                                        <td style="width: 50%; padding: 0px 10px 10px 0px">
                                            <div class="">
                                                <div class="">
                                                    <h4>DEDUCTIONS</h4>
                                                </div>
                                                <div class="">
                                                    <div class="table-responsive">
                                                        <div>
                                                            <table class="table table-bordered table-striped Grid deduction-table">
                                                                <tbody>
                                                                    <asp:Repeater ID="RepeaterDeduction" runat="server">
                                                                        <ItemTemplate>
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("EarnDeduction_Name").ToString()%>'></asp:Label>
                                                                                    :</th>
                                                                                <td style="text-align: right;">
                                                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Amount").ToString()%>'></asp:Label></td>
                                                                            </tr>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                    <%--                                                                    <tr>
                                                                        <th>LIC PREMIUM:</th>
                                                                        <th style="text-align: right;">
                                                                            <asp:Label ID="lblPolicyDeduction" runat="server" Text=""></asp:Label>
                                                                        </th>
                                                                    </tr>--%>
                                                                    <tr class="total_salary">
                                                                        <th>TOTAL DEDUCTION:</th>
                                                                        <th style="text-align: right;">
                                                                            <asp:Label ID="lblSalary_DeductionTotal" runat="server" Text=""></asp:Label>
                                                                        </th>
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
                                        <td colspan="2" class="footerdata"><b>Gross:</b>
                                            <asp:Label ID="lblGross" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="footerdata"><b>Net Salary:</b>
                                            <asp:Label ID="lblNet" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="footerdata"><b>Total Deduction:</b>
                                            <asp:Label ID="lblDeduction" runat="server" Text=""></asp:Label>
                                        </td>
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

            <!--MisMatch Details Modal-->
            <div class="row">
                <div class="col-md-12">
                    <div class="modal fade" id="MisMatchDetailModal" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>
                                <div class="modal-body">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="box box-success">
                                                <div class="box-header">
                                                    <h3 class="box-title">It seems that there is some issue with these employee salary. Kindly reset and generate again to resolve this issue.</h3>
                                                    <asp:Label runat="server" ID="lblNomineeDetail"></asp:Label>
                                                </div>
                                                <div class="box-body">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:GridView ID="GridMismatchDetail" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                                    <asp:BoundField DataField="UserName" HeaderText="UserID" />
                                                                    <asp:BoundField DataField="SalarySec_No" HeaderText="Section No" />
                                                                    <asp:BoundField DataField="SalaryEmp_No" HeaderText="Emp No" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script>
        $(function () {
            $('[id*=ddlSection]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
    </script>

    <style>
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
            max-height: 200px;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }

        ul.multiselect-container.dropdown-menu {
            overflow-y: scroll;
            overflow-x: hidden;
        }
    </style>
</asp:Content>


