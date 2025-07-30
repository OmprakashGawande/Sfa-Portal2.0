<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptGSTR_2.aspx.cs" Inherits="mis_Finance_RptGSTR_2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .Dtime {
            display: none;
        }
        @media print {
            .hide_print, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
            }

            tfoot, thead {
                display: table-row-group;
                bottom: 0;
            }
            .Dtime {
                display: block;
            }

        }
    </style>
    <style>
        .table > tbody > tr > td {
            padding: 3px;
        }
        .cssborder
        {
            border:1px solid black !important;
        }
        .rightborder
        {
            border-right:1px solid black !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <asp:Label ID="lblTime" runat="server" CssClass="Dtime" Style="font-weight: 800;" Text="" ClientIDMode="Static"></asp:Label>
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">GSTR - 2</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row hide_print">
                         <div class="col-md-3" runat="server" id="divRegionalOffice">
                                <div class="form-group">
                                    <label>Regional Office</label><span style="color: red">*</span>
                                    <asp:DropDownList ID="ddlRegionalOffice" runat="server"  ClientIDMode="Static" CssClass="form-control" OnSelectedIndexChanged="ddlRegionalOffice_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <%-- <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged">
                                </asp:DropDownList>--%>
                                <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>From Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>To Date</label><span style="color: red">*</span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success Aselect1" Style="margin-top: 24px;" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="hide_print hidden">
                             <label>As Per Trial Balance</label>
                                    <div class="table-responsive">
                                        
                                        <table class="table table-bordered" style="margin-bottom: 0px;">
                                            <tr>
                                                 <th style="background-color: #90add2 !important; width: 10%;">Taxable Value</th>
                                                <th>
                                                    <asp:Label ID="lblTTaxableValue" runat="server" Text=""></asp:Label>
                                                </th>
                                                <th style="background-color: #90add2 !important; width: 10%;">SGST</th>
                                                <th>
                                                    <asp:Label ID="lblSGST" runat="server" Text=""></asp:Label>
                                                </th>
                                                <th style="background-color: #90add2 !important; width: 10%;">CGST</th>
                                                <th>
                                                    <asp:Label ID="lblCGST" runat="server" Text=""></asp:Label>
                                                </th>
                                                <th style="background-color: #90add2 !important; width: 10%;">IGST</th>
                                                <th>
                                                    <asp:Label ID="lblIGST" runat="server" Text=""></asp:Label>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            <asp:Panel ID="DivTable" runat="server">
                                <label  class="hide_print">Export Excel</label>
                                <br />
                                 <asp:Button ID="btnAllExcel" runat="server" CssClass="hide_print" Text="Export GSTR-2" OnClick="btnAllExcel_Click" />
                             <%--<asp:Button ID="btnEB2BVoucher" runat="server" Text="B2B" OnClick="btnEB2BVoucher_Click" />
                                <asp:Button ID="btnEB2CLVoucher" runat="server" Text="B2CL" OnClick="btnEB2CLVoucher_Click" />
                                <asp:Button ID="btnEB2CMVoucher" runat="server" Text="B2CS" OnClick="btnEB2CMVoucher_Click" />
                                <asp:Button ID="btnEHSN" runat="server" Text="HSN" OnClick="btnEHSN_Click" />
                                <asp:Button ID="btnENilRatedVoucher" runat="server" Text="EXEMP" OnClick="btnENilRatedVoucher_Click" />
                                <asp:Button ID="Button3" Visible="false" runat="server" Text="CDNR" OnClick="btnEB2BVoucher_Click" />
                                <asp:Button ID="Button4" Visible="false" runat="server" Text="CDNUR" OnClick="btnEB2BVoucher_Click" />
                                <asp:Button ID="Button5" Visible="false" runat="server" Text="EXP" OnClick="btnEB2BVoucher_Click" />
                                <asp:Button ID="Button6" Visible="false" runat="server" Text="AT" OnClick="btnEB2BVoucher_Click" />
                                <asp:Button ID="Button7" Visible="false" runat="server" Text="ATADJ" OnClick="btnEB2BVoucher_Click" />
                                <asp:Button ID="Button9" Visible="false" runat="server" Text="DOCS" OnClick="btnEB2BVoucher_Click" />--%>
                                <br />
                                <%-- Table Second--%>
                                <div style="background: #ffe6c8; padding: 5px;">
                                    <table style="width: 100%; margin-bottom: 0px;" class="table table-hover no-border">
                                        <tr>
                                            <td style="font-weight: bold; border-top: 1px solid #000;" colspan="8">GSTIN/UIN: 23AACCM0330Q1ZM</td>
                                            <td style="text-align: right; font-weight: bold; border-top: 1px solid #000;" colspan="4">
                                                <asp:Label ID="lblDateRange" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="border-top: 2px solid #000; border-bottom: 1px solid #000;">Returns Summary</td>
                                        </tr>
                                        <tr>
                                            <td colspan="11">
                                                <asp:LinkButton ID="btnTotalVoucher" runat="server" OnClick="btnTotalVoucher_Click">Total number of vouchers for the period</asp:LinkButton></td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblTotalVoucher" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="11">
                                                <asp:LinkButton ID="btnTotalVoucherIncludedRet" runat="server" OnClick="btnTotalVoucherIncludedRet_Click">Included in returns</asp:LinkButton></td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblIncludedRet" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr style="display: none" colspan="11">
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i>
                                                <asp:LinkButton ID="btnReadyToReturn" runat="server" OnClick="btnReadyToReturn_Click">Invoices ready for returns</asp:LinkButton></td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblReadyToReturn" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr style="display: none" colspan="11">
                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i>
                                                <asp:LinkButton ID="btnMismatchInfo" runat="server" OnClick="btnMismatchInfo_Click">Invoices with mismatch in information</asp:LinkButton></td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblMismatchInfo" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="11">Not included in returns due to incomplete information</td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblNotIncludeRetInCompInfo" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="11">
                                                <asp:LinkButton ID="btnVoucherNotRelevent" runat="server" OnClick="btnVoucherNotRelevent_Click">Not relevant for returns </asp:LinkButton></td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblNotReleventRet" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="11">
                                                <asp:LinkButton ID="btnIncompleteHSNSAC" runat="server" OnClick="btnIncompleteHSNSAC_Click">Incomplete HSN/SAC information (to be provided) </asp:LinkButton></td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblIncompleteHSNSAC" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; border-top: 2px solid #000; width: 350px;">Particulars</td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000;">No. of Invoices</td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000;">Taxable Value</td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000; border-bottom: 1px solid #000;" colspan="4">Total<br />
                                                Tax</td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000; border-bottom: 1px solid #000;" colspan="4">Input<br />
                                                Tax Credit</td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000;">Reconciliation Status</td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td style="text-align: center; font-weight: bold; width: 88px;">Integrated
                                            Tax Amount</td>
                                            <td style="text-align: center; font-weight: bold; width: 90px;">Central
                                            Tax Amount </td>
                                            <td style="text-align: center; font-weight: bold; width: 88px;">State
                                            Tax Amount</td>
                                            <td style="text-align: center; font-weight: bold; width: 88px;">Cess
                                            Amount</td>
                                            <td style="text-align: center; font-weight: bold; width: 88px;">Integrated
                                            Tax Amount	</td>
                                            <td style="text-align: center; font-weight: bold; width: 90px;">Central
                                            Tax Amount	</td>
                                            <td style="text-align: center; font-weight: bold; width: 88px;">State
                                            Tax Amount	</td>
                                            <td style="text-align: center; font-weight: bold; width: 88px;">Cess
                                            Tax Amount	</td>
                                            <td style="text-align: center;"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-weight: bold">To be reconciled with the GST portal</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnB2BRMltVCount" runat="server" OnClick="btnB2BRMltVCount_Click">B2B Invoices - 3, 4A </asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BRVCount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BVTaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BVTIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BVTCentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BVTStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BVTCessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BVIIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BVICentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BVIStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BVICessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BVReconciliationStatus" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                         <tr>
                                            <td style="font-style: italic"><i class="fa fa-caret-right" aria-hidden="true"></i>
                                                <asp:LinkButton ID="btnB2BR_TaxPurMltVCount" runat="server" Style="color: #333333; font-weight: 500;" OnClick="btnB2BR_TaxPurMltVCount_Click">Taxable Purchases</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_TaxPurRMltVCount" runat="server" CssClass="hidden" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_TaxPurTaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_TaxPurVTIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_TaxPurVTCentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_TaxPurVTStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_TaxPurVTCessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_TaxPurVIIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_TaxPurVICentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_TaxPurVIStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_TaxPurVICessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_TaxPurVReconciliationStatus" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="font-style: italic"><i class="fa fa-caret-right" aria-hidden="true"></i>
                                                <asp:LinkButton ID="btnB2BR_RevMltVCoun" runat="server" Style="color: #333333; font-weight: 500;" OnClick="btnB2BR_RevMltVCoun_Click">Reverse Charges Supplies</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_RevRMltVCount" runat="server" CssClass="hidden" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_RevTaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_RevVTIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_RevVTCentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_RevVTStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_RevVTCessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_RevVIIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_RevVICentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_RevVIStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_RevVICessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2B_RevVReconciliationStatus" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnDebitMltVCount" runat="server" OnClick="btnDebitMltVCount_Click">Credit/Debit Notes Regular - 6C </asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitCount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitTaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitTIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitTCentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitTStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitTCessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitIIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitICentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitIStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitICessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitReconciliationStatus" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-weight: bold;">To be uploaded on the GST portal</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnB2BURMltVCount" runat="server" OnClick="btnB2BURMltVCount_Click">B2BUR Invoices - 4B </asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BURURVCount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BURVTaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BURVTIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BURVTCentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BURVTStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BURVTCessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BURVIIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BURVICentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BURVIStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BURVICessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BURVReconciliationStatus" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="font-style: italic"><i class="fa fa-caret-right" aria-hidden="true"></i>
                                                <asp:LinkButton ID="btnB2BUR_TaxPurMltVCount" runat="server" Style="color: #333333; font-weight: 500;" OnClick="btnB2BUR_TaxPurMltVCount_Click">Taxable Purchases</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_TaxPurRMltVCount" runat="server" CssClass="hidden" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_TaxPurTaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_TaxPurVTIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_TaxPurVTCentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_TaxPurVTStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_TaxPurVTCessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_TaxPurVIIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_TaxPurVICentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_TaxPurVIStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_TaxPurVICessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_TaxPurVReconciliationStatus" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="font-style: italic"><i class="fa fa-caret-right" aria-hidden="true"></i>
                                                <asp:LinkButton ID="btnB2BUR_RevMltVCount" runat="server" Style="color: #333333; font-weight: 500;" OnClick="btnB2BUR_RevMltVCount_Click">Reverse Charges Supplies</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_RevRMltVCount" runat="server" CssClass="hidden" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_RevTaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_RevVTIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_RevVTCentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_RevVTStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_RevVTCessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_RevVIIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_RevVICentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_RevVIStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_RevVICessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB2BUR_RevVReconciliationStatus" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-style: italic"><i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp;&nbsp;Import of Services - 4C</td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-style: italic"><i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp;&nbsp;Import of Goods - 5</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnDebitUMltVCount" runat="server" OnClick="btnDebitUMltVCount_Click"> Credit/Debit Notes Unregistered - 6C</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitUCount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitUTaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitUTIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitUTCentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitUTStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitUTCessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitUIIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitUICentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitUIStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitUICessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblDebitUReconciliationStatus" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnVNMltVCount" runat="server" OnClick="btnVNMltVCount_Click">Nil Rated Invoices - 7 - (Summary) </asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblVNCount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblVNTaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblVNTIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblVNTCentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblVNTStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblVNTCessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblVNIIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblVNICentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblVNIStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblVNICessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblVNReconciliationStatus" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-style: italic">&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp;&nbsp;Composition</td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-style: italic">&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp;&nbsp;Exempt</td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-style: italic">&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp;&nbsp;Nil Rated</td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-style: italic">&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i>&nbsp;&nbsp;Non GST</td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-style: italic">Advance Paid -10A - (Summary)</td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-style: italic">Adjustment of Advance - 10B - (Summary)</td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold; border-top: 2px solid #000; border-bottom: 1px solid #000;">Total Inward Supplies</td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000; border-bottom: 1px solid #000;">
                                                <asp:Label ID="lblTotalMVCount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000; border-bottom: 1px solid #000;">
                                                <asp:Label ID="lblTotalMTaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000; border-bottom: 1px solid #000;">
                                                <asp:Label ID="lblTotalMIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000; border-bottom: 1px solid #000;">
                                                <asp:Label ID="lblTotalMCentralTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000; border-bottom: 1px solid #000;">
                                                <asp:Label ID="lblTotalMStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000; border-bottom: 1px solid #000;">
                                                <asp:Label ID="lblTotalMCess" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000; border-bottom: 1px solid #000;">
                                                <%--  <asp:Label ID="lblTotalMTaxAmount" runat="server" Text=""></asp:Label>--%>
                                                <asp:Label ID="lblTotalInIntegratedTax" runat="server" Text=""></asp:Label>
                                            </td>

                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000; border-bottom: 1px solid #000;">
                                                <%--   <asp:Label ID="lblTotalMInVoiceAmount" runat="server" Text=""></asp:Label>--%>
                                                <asp:Label ID="lblTotalInCentralTax" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000; border-bottom: 1px solid #000;">
                                                <asp:Label ID="lblTotalInStateTax" runat="server" Text=""></asp:Label>

                                            </td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000; border-bottom: 1px solid #000;">
                                                <asp:Label ID="lblTotalInCess" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000; border-bottom: 1px solid #000;">0</td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-style: italic;">ITC Reversal/Reclaim - 11 - (Summary)</td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-weight: bold; border-bottom: 1px dashed #000;">Total No. of Invoices</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="btnHSNSummary" runat="server" OnClick="btnHSNSummary_Click"> HSN/SAC Summary - 13</asp:LinkButton></td>
                                            <td style="text-align: center;"></td>
                                           <%-- <td style="text-align: center;">1195254.55</td>
                                            <td style="text-align: center;">1195254.55</td>
                                            <td style="text-align: center;">1195254.55</td>
                                            <td style="text-align: center;">1195254.55</td>
                                            <td style="text-align: center;"></td>--%>
                                            <td style="text-align: center; font-weight: bold;">
                                                <asp:Label ID="lblHSNTaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center; font-weight: bold;">
                                                <asp:Label ID="lblHSNTIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center; font-weight: bold;">
                                                <asp:Label ID="lblHSNTCentarlTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center; font-weight: bold;">
                                                <asp:Label ID="lblHSNTStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center; font-weight: bold;">
                                                <asp:Label ID="lblHSNTCessAmt" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;"></td>
                                            <td style="text-align: center;"></td>
                                            <td style="text-align: center;"></td>
                                            <td style="text-align: center;"></td>
                                            <td style="text-align: center;"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-style: italic; font-weight:600;"><u>Reverse Charge Liability to be Booked</u></td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-style: italic;">URD Purchases</td>
                                        </tr>
                                        <tr>
                                            <td colspan="11" style="font-style: italic;">
                                            <asp:LinkButton ID="btnRCM" runat="server" OnClick="btnRCM_Click">Reverse Charge Inward Supplies</asp:LinkButton></td>    
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblRCM" runat="server" Text=""></asp:Label></td>                                       
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-style: italic;">Import of Service</td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-weight: bold; border-bottom: 1px solid #000;">Advance Payments</td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-style: italic;">Amount Unadjusted Against Purchases</td>
                                        </tr>
                                        <tr>
                                            <td colspan="12" style="font-style: italic;">Purchase Against Advance from Previous Periods	</td>
                                        </tr>
                                    </table>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-11">
                            <asp:Label ID="lblParticulars" Style="font-size: 21px; font-weight: 700;" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Label ID="lblParticularsRate" Style="font-size: 19px; font-weight: 700;" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-block label-primary Aselect1" Text="Back" OnClick="btnBack_Click" OnClientClick="return validateform();" />
                            <asp:Button ID="btnBackNext" runat="server" CssClass="btn btn-block label-primary Aselect1" Text="Back" OnClick="btnBackNext_Click" OnClientClick="return validateform();" />
                          <asp:Button ID="btnBackDayBook" runat="server" CssClass="btn btn-block label-primary Aselect1" Text="Back" OnClick="btnBackDayBook_Click" OnClientClick="return validateform();" />
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                             <asp:GridView ID="GV_Statsticts"  runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" OnRowCommand="GV_Statsticts_RowCommand" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                              <asp:Label ID="lblParticulars1" CssClass="hidden" Text='<%# Eval("VoucherTx_Type").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="Aselect1" CommandName="View" HeaderText="Supplier Name" DataTextField="VoucherTx_Type" />
                                    
                                     <asp:TemplateField HeaderText="No. Of Voucher">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVCount" Text='<%# Eval("VCount").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>                                   
                                </Columns>
                            </asp:GridView>

                            <asp:GridView ID="GridRateWise" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" OnRowCommand="GridRateWise_RowCommand" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="Aselect1" CommandName="View" HeaderText="Supplier Name" DataTextField="Particulars" />
                                    <asp:TemplateField HeaderText="GST No." ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGST_No" Text='<%# Eval("GST_No").ToString() %>' runat="server" />
                                            <asp:Label ID="lblTotalLedger" Text='<%# Eval("TotalLedger").ToString() %>' runat="server" Visible="false" />
                                            <asp:Label ID="lblTotalVoucherO" Text='<%# Eval("TotalVoucher").ToString() %>' runat="server" Visible="false" />
                                            <%-- <asp:Label ID="lblTotalVoucher" Text='<%# Eval("TotalVoucher").ToString() %>' runat="server" Visible="false" />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate Of Tax" ItemStyle-Width="13%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRateOfTax" Text='<%# Eval("RateOfTax").ToString() %>' runat="server" />
                                            <asp:Label ID="lblParticulars1" CssClass="hidden" Text='<%# Eval("Particulars").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No of Invoices">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo_of_Invoices" Text='<%# Eval("No_of_Invoices").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Taxable Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaxableValue" Text='<%# Eval("TaxableValue").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IGST" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIGSTAmt" Text='<%# Eval("IGSTAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CGST" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCGSTAmt" Text='<%# Eval("CGSTAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SGST" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSGSTAmt" Text='<%# Eval("SGSTAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cess Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCessAmount" Text='<%# Eval("CessAmount").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TotalTaxAmt" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalTaxAmt" Text='<%# Eval("TotalTaxAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="GridView2" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLedger_Name" Text='<%# Eval("Particulars").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vch Type" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoucherTx_Type" Text='<%# Eval("VoucherTx_Type").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Vch No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoucherTx_No" Text='<%# Eval("VoucherTx_No").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Taxable Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaxableValue" Text='<%# Eval("TaxableValue").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="IGST" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIGSTAmt" Text='<%# Eval("IGSTAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CGST" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCGSTAmt" Text='<%# Eval("CGSTAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SGST" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSGSTAmt" Text='<%# Eval("SGSTAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cess Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCessAmount" Text='<%# Eval("CessAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Tax Amt" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalTaxAmt" Text='<%# Eval("TotalTaxAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Invoice Value" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalTaxAmt" Text='<%# Eval("InvoiceValue").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hpView" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("1")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info">View</asp:HyperLink>
                                            <asp:HyperLink ID="hpEdit" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("2")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info" Visible='<%# Eval("V_Editright").ToString() == "Yes" ? true : false %>'>Edit</asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="GridView1" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLedger_Name" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vch Type" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoucherTx_Type" Text='<%# Eval("VoucherTx_Type").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vch No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoucherTx_No" Text='<%# Eval("VoucherTx_No").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit Amt." ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit Amt." ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("1")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info">View</asp:HyperLink>
                                            <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("2")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info" Visible='<%# Eval("V_Editright").ToString() == "Yes" ? true : false %>'>Edit</asp:HyperLink>

                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="GridHSNSummery" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" OnRowCommand="GridHSNSummery_RowCommand"  ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="Aselect1" CommandName="View" HeaderText="HSN / SAC" DataTextField="HSN" />

                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" Text='<%# Eval("Description").ToString() %>' runat="server" />
                                            <asp:Label ID="lblHSN" CssClass="hidden" Text='<%# Eval("HSN").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type Of Supply">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTypeOfSupply" Text='<%# Eval("TypeOfSupply").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UQC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUQC" Text='<%# Eval("UQC").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalQuantity" Text='<%# Eval("TotalQuantity").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalValue" Text='<%# Eval("TotalValue").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Taxable Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaxableValue" Text='<%# Eval("TaxableValue").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Integrated Tax Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIntegratedTaxAmount" Text='<%# Eval("IntegratedTaxAmount").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Central Tax Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCentralTaxAmount" Text='<%# Eval("CentralTaxAmount").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State Tax Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStateUTTaxAmountt" Text='<%# Eval("StateUTTaxAmount").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Cess Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCessAmt" Text='<%# Eval("CessAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Total Tax Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalTaxAmt" Text='<%# Eval("TotalTaxAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:GridView ID="GridHSNSummeryDes" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoucherTx_Date" Text='<%# Eval("VoucherTx_Date").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLedger_Name" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="GST No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGST_No" Text='<%# Eval("GST_No").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vch Type" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoucherTx_Type" Text='<%# Eval("VoucherTx_Type").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vch No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoucherTx_No" Text='<%# Eval("VoucherTx_No").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UQC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUQC" Text='<%# Eval("UQC").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalQuantity" Text='<%# Eval("TotalQuantity").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalValue" Text='<%# Eval("TotalValue").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Taxable Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaxableValue" Text='<%# Eval("TaxableValue").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Integrated Tax Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIntegratedTaxAmount" Text='<%# Eval("IntegratedTaxAmount").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Central Tax Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCentralTaxAmount" Text='<%# Eval("CentralTaxAmount").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State Tax Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStateTaxAmountt" Text='<%# Eval("StateTaxAmount").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cess Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCessAmt" Text='<%# Eval("CessAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hpView" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("1")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info">View</asp:HyperLink>
                                            <asp:HyperLink ID="hpEdit" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("2")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info" Visible='<%# Eval("V_Editright").ToString() == "Yes" ? true : false %>'>Edit</asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Panel ID="pnlSumrytaxLiability" runat="server">
                                <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div id="divSumrytaxLiability" runat="server" ></div>
                                    </div>
                                </div>
                            </div>
                            </asp:Panel>

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
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select From Date. \n";
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select To Date. \n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }

        }

        $('#txtFromDate').change(function () {
            debugger;
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');

            if ($('#txtToDate').val() != "") {
                if (start > end) {

                    if ($('#txtFromDate').val() != "") {
                        alert("From date should not be greater than To Date.");
                        $('#txtFromDate').val("");
                    }
                }
            }
        });
        $('#txtToDate').change(function () {
            debugger;
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');

            if (start > end) {

                if ($('#txtToDate').val() != "") {
                    alert("To Date can not be less than From Date.");
                    $('#txtToDate').val("");
                }
            }

        });
    </script>
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script>

        $(function () {
            $('[id*=ddlOffice]').multiselect({
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


