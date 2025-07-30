<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptGSTR_3B.aspx.cs" Inherits="mis_Finance_RptGSTR_3B" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .hide {
            display: none;
        }

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

            .hide1 {
                display: block !important;
            }

            .font {
                font-size: 9px;
            }

            .box {
                border: none;
            }

            .Dtime {
                display: block;
            }
        }

        .tdhead {
            border-bottom: 1px solid black !important;
            font-weight: 700;
        }

        .table > tbody > tr > td {
            padding: 3px;
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
                    <h3 class="box-title">GSTR - 3B</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="">
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
                        <div id="testTable">
                            <asp:Panel ID="DivTable" class="font" runat="server">
                                <div>
                                    <table style="width: 100%">
                                        <tr style="text-align: center">
                                            <td><b>M P State Agro Ind Corpn</b></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div style="word-break: break-all; text-align: center; font-weight: 700" id="spnofcname" runat="server"></div>
                                            </td>
                                        </tr>
                                        <tr style="text-align: center">
                                            <td><b>GST Computation</b></td>
                                        </tr>
                                        <tr style="text-align: center">
                                            <td><b><span id="spnfromdate" runat="server"></span>to <span id="spntodate" runat="server"></span></b></td>
                                        </tr>
                                    </table>
                                </div>
<div class="hide_print" id="hide_print_main" runat="server">
                            <br />
                            <p>
                                <a id="dlink" style="display: none;"></a>
                                <asp:Button runat="server" Text="Print Main Report" class="btn btn-flat btn-success" OnClientClick="window.print();return false;" />
                                <asp:Button runat="server" Text="Export Main Report" OnClientClick="tableToExcel('testTable', 'Day Book','GST_3B')" ID="myButtonControlID" class="btn btn-flat btn-success" />
                            </p>
                        </div>
                                <div style="background: #ffe6c8; padding: 5px;">
                                    <table style="width: 100%; margin-bottom: 0px;" class="table table-hover no-border">
                                        <tr>
                                            <td style="font-weight: bold; border-top: 1px solid #000;" colspan="5">GSTIN/UIN: 23AACCM0330Q1ZM</td>
                                            <td style="text-align: right; font-weight: bold; border-top: 1px solid #000;" colspan="3">
                                                <asp:Label ID="lblDateRange" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="8" style="border-top: 2px solid #000; border-bottom: 1px solid #000;">Returns Summary</td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:LinkButton ID="btnTotalVoucher" runat="server" OnClick="btnTotalVoucher_Click">Total number of vouchers for the period</asp:LinkButton></td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblTotalVoucher" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:LinkButton ID="btnIncludedReturn" runat="server" OnClick="btnIncludedReturn_Click">Included in returns</asp:LinkButton></td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblIncludedReturn" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i>
                                                <asp:LinkButton ID="btnParticipatingReturn" runat="server" OnClick="btnParticipatingReturn_Click">Participating in return tables</asp:LinkButton></td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblParticipatingReturn" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-caret-right" aria-hidden="true"></i>
                                                <asp:LinkButton ID="btnNoDirectReturn" runat="server" OnClick="btnNoDirectReturn_Click">No direct implication in return tables</asp:LinkButton></td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblNoDirectReturn" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:LinkButton ID="btnNotRelevantReturn" runat="server" OnClick="btnNotRelevantReturn_Click">Not relevant for returns</asp:LinkButton></td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblNotRelevantReturn" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:LinkButton ID="btnIncompleteMismatch" runat="server" OnClick="btnIncompleteMismatch_Click">Incomplete/Mismatch in information (to be resolved)</asp:LinkButton></td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblIncompleteMismatch" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000;">S.No.</td>
                                            <td style="font-weight: bold; border-top: 2px solid #000; width: 400px;">Particulars</td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000;">Taxable Value</td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000;">Integrated Tax Amount</td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000;">Central Tax Amount</td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000;">State Tax Amount</td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000;">Cess Amount</td>
                                            <td style="text-align: center; font-weight: bold; border-top: 2px solid #000;">Tax Amount</td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold">3.1</td>
                                            <td style="font-weight: bold">Outward supplies and inward supplies liable to reverse charge</td>
                                            <td style="text-align: center; font-weight: bold;">
                                                <asp:Label ID="lblTaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center; font-weight: bold;">
                                                <asp:Label ID="lblIntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center; font-weight: bold;">
                                                <asp:Label ID="lblCentralTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center; font-weight: bold;">
                                                <asp:Label ID="lblStateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center; font-weight: bold;">
                                                <asp:Label ID="lblCessAmount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center; font-weight: bold;">
                                                <asp:Label ID="lblTaxAmount" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">A</td>
                                            <td>
                                                <asp:LinkButton ID="btn3_1a" runat="server" OnClick="btn3_1A_Click">Outward taxable supplies (other than zero rated, nil rated and exempted)</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblA_TaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblA_IntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblA_CentralTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblA_StateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblA_CessAmount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblA_TaxAmount" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">B</td>
                                            <td>
                                                <asp:LinkButton ID="btn3_1B" runat="server" OnClick="btn3_1B_Click">Outward taxable supplies (zero rated)</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB_TaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB_IntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB_CentralTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB_StateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB_CessAmount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblB_TaxAmount" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">C</td>
                                            <td>
                                                <asp:LinkButton ID="btn3_1C" runat="server" OnClick="btn3_1C_Click">Other Outward supplies (Nil rated, exempted)</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblC_TaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblC_IntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblC_CentralTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblC_StateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblC_CessAmount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblC_TaxAmount" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">D</td>
                                            <td>
                                                <asp:LinkButton ID="btn3_1D" runat="server" OnClick="btn3_1D_Click">Inward supplies (liable to reverse charge)</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblD_TaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblD_IntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblD_CentralTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblD_StateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblD_CessAmount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblD_TaxAmount" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">E</td>
                                            <td>
                                                <asp:LinkButton ID="btn3_1E" runat="server" OnClick="btn3_1E_Click">Non-GST outward supplies</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblE_TaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblE_IntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblE_CentralTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblE_StateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblE_CessAmount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblE_TaxAmount" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold">3.2</td>
                                            <td style="font-weight: bold">Outward supplies and inward supplies liable to reverse charge</td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label1" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label3" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label4" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label5" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label6" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">A</td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton1" runat="server">Supplies made to Unregistered Persons</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label7" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label8" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label9" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label10" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label11" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label12" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">B</td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton2" runat="server">Supplies made to Composition Taxable Persons</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label13" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label14" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label15" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label16" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label17" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label18" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">C</td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton4" runat="server">Supplies made to UIN holders</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label19" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label20" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label21" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label22" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label23" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label24" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold">4</td>
                                            <td style="font-weight: bold">Eligible ITC</td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblITC_TaxableValue" runat="server" Style="font-weight: bold;" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblITC_IntegratedTax" runat="server" Style="font-weight: bold;" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblITC_CentralTax" runat="server" Style="font-weight: bold;" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblITC_StateTax" runat="server" Style="font-weight: bold;" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblITC_CessAmount" runat="server" Style="font-weight: bold;" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblITC_TaxAmount" runat="server" Style="font-weight: bold;" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">A</td>
                                            <td>ITC Available (whether in full or part)</td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label31" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label32" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label33" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label34" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label35" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label36" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton5" runat="server">(1) Import of goods</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label37" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label38" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label39" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label40" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label41" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label42" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton6" runat="server">(2) Import of services</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label43" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label44" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label45" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label46" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label47" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label48" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold"></td>
                                            <td>
                                                <asp:LinkButton ID="btn4A_1" runat="server" OnClick="btn4A_1_Click">(3) Inward supplies liable to reverse charge (other than 1 & 2 above)</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl4A3_TaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl4A3_IntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl4A3_CentralTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl4A3_StateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl4A3_CessAmount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl4A3_TaxAmount" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold"></td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton7" runat="server">(4) Inward supplies from ISD</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label55" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label56" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label57" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label58" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label59" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label60" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold"></td>

                                            <td>
                                                <asp:LinkButton ID="btn4A_5" runat="server" OnClick="btn4A_5_Click">(5) All other ITC</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl4A5_TaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl4A5_IntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl4A5_CentralTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl4A5_StateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl4A5_CessAmount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lbl4A5_TaxAmount" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">B</td>
                                            <td>ITC Reversed</td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label67" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label68" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label69" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label70" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label71" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label72" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold"></td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton8" runat="server">(1) As per rules 42 & 43 of CGST Rules</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label73" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label74" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label75" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label76" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label77" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label78" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold"></td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton9" runat="server">(2) Others</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label79" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label80" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label81" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label82" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label83" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label84" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">C</td>
                                            <td>Net ITC Available (A) - (B)</td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNETITC_TaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNETITC_IntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNETITC_CentralTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNETITC_StateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNETITC_CessAmount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNETITC_TaxAmount" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">D</td>
                                            <td>Ineligible ITC</td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label91" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label92" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label93" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label94" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label95" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label96" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold"></td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton10" runat="server">(1) As per section 17(5)</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label97" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label98" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label99" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label100" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label101" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label102" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold"></td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton11" runat="server">(2) Others</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label103" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label104" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label105" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label106" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label107" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label108" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold">5</td>
                                            <td style="font-weight: bold">Value of exempt, nil rated and non-GST inward supplies</td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNongst5_TaxableValue" Style="font-weight: bold;" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNongst5_IntegratedTax" Style="font-weight: bold;" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNongst5_CentralTax" Style="font-weight: bold;" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNongst5_StateTax" Style="font-weight: bold;" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNongst5_CessAmount" Style="font-weight: bold;" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNongst5_TaxAmount" Style="font-weight: bold;" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">A</td>
                                            <td>
                                                <asp:LinkButton ID="btn5_A" runat="server" OnClick="btn5_A_Click">From a supplier under composition scheme, exempt and nil rated supply</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNongst5A_TaxableValue" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNongst5A_IntegratedTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNongst5A_CentralTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNongst5A_StateTax" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNongst5A_CessAmount" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="lblNongst5A_TaxAmount" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">B</td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton12" runat="server">Non GST supply</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label121" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label122" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label123" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label124" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label125" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label126" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="font-weight: bold">5.1</td>
                                            <td style="font-weight: bold">Interest and Late fee Payable</td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label127" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label128" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label129" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label130" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label131" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label132" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">A</td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton13" runat="server">Interest</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label133" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label134" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label135" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label136" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label137" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label138" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">B</td>
                                            <td>
                                                <asp:LinkButton ID="LinkButton14" runat="server">Late Fees</asp:LinkButton></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label139" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label140" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label141" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label142" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label143" runat="server" Text=""></asp:Label></td>
                                            <td style="text-align: center;">
                                                <asp:Label ID="Label144" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="8" style="font-weight: bold; border-bottom: 1px solid #000; text-align: center;">Reverse Charge Liability and Input Credit to be booked</td>
                                        </tr>
                                        <tr>

                                            <td colspan="11" style="font-style: italic;">
                                                <asp:LinkButton ID="btnRCM" runat="server" OnClick="btnRCM_Click">Reverse Charge Inward Supplies</asp:LinkButton></td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblRCM" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">Import of Service</td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="Label146" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">Input Credit to be Booked</td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="Label147" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="8" style="font-weight: bold; border-bottom: 1px solid #000; text-align: center;">Advance Payments</td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">Amount Unadjusted Against Purchases</td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="Label148" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">Purchase Against Advance from Previous Periods</td>
                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="Label149" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>

                            </asp:Panel>


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
                                <div class="table-responsive hide_print">
                                    <asp:GridView ID="GV_Statsticts" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" OnRowCommand="GV_Statsticts_RowCommand"  ShowFooter="true">
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
                                    <asp:GridView ID="GridView2" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered">
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
                                            <asp:TemplateField HeaderText="GST_No"  Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGstNo" Text='<%# Eval("GST_No").ToString() %>' runat="server" />
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

                                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hpView" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("1")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info">View</asp:HyperLink>
                                                     <%-- <asp:HyperLink ID="hpEdit" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("2")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info" Visible='<%# Eval("V_Editright").ToString() == "Yes" ? true : false %>'>Edit</asp:HyperLink>--%>
<asp:HyperLink ID="hpEdit" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("2")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info" >Edit</asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Panel ID="pnlSumrytaxLiability" runat="server">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <div id="divSumrytaxLiability" runat="server"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div id="divPurchaseExemptnillrated" runat="server">

                                    <table style="width: 100%; margin-bottom: 0px;" class="table table-hover no-border">
                                        <tr>
                                            <td colspan="7" style="border-top: 2px solid #000; border-bottom: 1px solid #000; font-weight: bold">Particulars</td>
                                            <td style="text-align: right; font-weight: bold; border-top: 2px solid #000; border-bottom: 1px solid #000;">
                                                <asp:Label ID="lbl" runat="server">Taxable Value</asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" style="font-weight: bold;">Local</td>

                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblLocalTotalTax" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:LinkButton ID="btnLocalExempted" runat="server" OnClick="btnLocalExempted_Click">Exempt</asp:LinkButton></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblLocalExempted" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:LinkButton ID="btnLocalNillRated" runat="server" OnClick="btnLocalNillRated_Click">Nill Rated</asp:LinkButton></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblLocalNillRated" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:LinkButton ID="LinkButton3" runat="server">Composition Other Purchases</asp:LinkButton></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="Label28" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7" style="font-weight: bold;">Interstate</td>

                                            <td style="text-align: right; font-weight: bold;">
                                                <asp:Label ID="lblInterStateTotalTax" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:LinkButton ID="btnInterstateExempted" runat="server" OnClick="btnInterstateExempted_Click">Exempt</asp:LinkButton></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblInterStateExempted" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="7">
                                                <asp:LinkButton ID="btnInterstateNillRated" runat="server" OnClick="btnInterstateNillRated_Click">Nill Rated</asp:LinkButton></td>
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblInterStateNillRated" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                    </table>
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
    <script>
        var tableToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
              , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
              , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
              , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            // return function (table, name) {
            return function (table, name, filename) {
                var x = $("#" + table).clone();
                $(x).find("tr td a").replaceWith(function () {
                    return $.text([this]);
                });
                //console.log(x);
                //console.log(x.innerHTML);
                if (!table.nodeType) table = x
                //console.log(table[0].innerHTML);
                //if (!table.nodeType) table = document.getElementById(table)
                var ctx = { worksheet: name || 'Worksheet', table: table[0].innerHTML }
                //window.location.href = uri + base64(format(template, ctx))
                document.getElementById("dlink").href = uri + base64(format(template, ctx));
                document.getElementById("dlink").download = filename;
                document.getElementById("dlink").click();
            }
        })()
    </script>
</asp:Content>


