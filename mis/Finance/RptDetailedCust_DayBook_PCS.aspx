<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptDetailedCust_DayBook_PCS.aspx.cs" Inherits="mis_Finance_RptDetailedCust_DayBook_PCS" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">

    <style>
        .pay-sheet table tr th, .pay-sheet table tr td {
            font-size: 12px;
            width: 10%;
            border: 1px dashed #ddd;
            padding-left: 1px;
            padding-top: 1px;
            line-height: 14px;
            font-family: monospace;
            overflow: hidden;
        }

        .pay-sheet table {
            width: 100%;
        }

            .pay-sheet table thead {
                background: #eee;
            }
		.NonPrintable {
            display: none;
        }
        /*.pay-sheet table {
            border: 1px solid #ddd;
        }*/

        @media print {

            .hide_print, .main-footer, .dt-buttons, .dataTables_filter, .dataTables_info {
                display: none;
            }
			.NonPrintable {
            display: block;
        }
            tfoot, thead {
                display: table-row-group;
                bottom: 0;
            }
        }

        .voucherColumn {
            width: 150px !important;
        }
    </style>
    <style>
        .inline-rb label {
            margin-left: 5px;
        }

        /*.pagination-ys {
            
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a,
                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a,
                .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a,
                .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover,
                .pagination-ys table > tbody > tr > td > span:hover,
                .pagination-ys table > tbody > tr > td > a:focus,
                .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
                }*/
        .item {
            background-color: wheat;
        }

        .billbybill {
            background-color: gray;
            color: white;
        }
          .costcentre {
            /*background-color: gray !important;
            color: white !important;*/
            margin-left: 3%;
            color: black !important;
        }

        .ledger {
            background-color: #e6e6e6;
        }

        .narration {
            background-color: #ffffe6;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header hide_print">
                    <h3 class="box-title">Custom Day Book</h3>
                    <input type="button" id="btnHide" onclick="window.print()" value="Print" class="pull-right" />
                    <input type="button" id="btnExcel" onclick="tableToExcel('tableData', 'W3C Example Table')" value="Export to Excel" class="pull-right" />
                    <p class="hide_print">
                        <span runat="server" id="spnAltW">[<span class="Scut">Alt+w</span> - Condensed & Detailed View]</span>
                    </p>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row hidden-print">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>From Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>To Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="Select To Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                         <div class="col-md-3" id="divdfo" runat="server">
                            <div class="form-group">
                                <label>DFO</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlDFO" CssClass="form-control select2" OnSelectedIndexChanged="ddlDFO_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                 <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2">
                                </asp:DropDownList>
                              <%--   <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>--%>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success btn-block" Style="margin-top: 25px;" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="row pull-right HideRecord hide_print">
                        <div class="col-md-12">
                            <div class="form-group hide_print" style="margin-top: 28px;">
                                <label>Filter: &nbsp;&nbsp;</label>
                                <asp:CheckBox ID="ChkLedger" ClientIDMode="Static" runat="server" Checked="true" Text="Opposite Ledger.&nbsp;&nbsp;" onchange="HideOppositeLedger();" />
                                <asp:CheckBox ID="chkBillByBill" ClientIDMode="Static" runat="server" Checked="true" Text="BillWiseDetail.&nbsp;&nbsp;" onchange="HideBillByBill();" />
                                 <asp:CheckBox ID="chkCostCentre" ClientIDMode="Static" runat="server" Checked="false" Text="CostCentreDetail.&nbsp;&nbsp;" onchange="HideCostCentre();" />
                                <asp:CheckBox ID="chkItemDetail" ClientIDMode="Static" runat="server" Checked="true" Text="Inventory.&nbsp;" onchange="HideItem();" />
                                <asp:CheckBox ID="chkNarration" ClientIDMode="Static" runat="server" Checked="true" Text="Narration.&nbsp;&nbsp;" onchange="HideNarration();" />
								<asp:CheckBox ID="chkChequeDetail" ClientIDMode="Static" runat="server" Checked="true" Text="Cheque Detail.&nbsp;&nbsp;" onchange="HideCheque();" />
                            </div>
                        </div>
                    </div>
                    <div class="row hide_print">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime"></asp:Label>
                                <div class="hide_print hidden">
                                    <asp:LinkButton ID="btnPrint" runat="server" CssClass="btn btn-default" OnClick="btnPrint_Click"><i class="fa fa-print"></i>Print</asp:LinkButton>
                                </div>

                            </div>
                        </div>
                    </div>

                    <div id="tableData">
                        <div class="row">
                            <div class="col-md-12">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="7" style="text-align: center;">
                                            <asp:Label ID="lblheadingFirst" CssClass="lblheadingFirst" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">

                                <%--<asp:GridView ID="GridView1" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="50" CssClass="dataTables table table-hover table-bordered pagination" ShowHeaderWhenEmpty="true" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting" EmptyDataText="No Record Found" ClientIDMode="Static" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">--%>
                                <asp:GridView ID="GridView1" DataKeyNames="VoucherTx_ID" runat="server"  AllowPaging="false" AutoGenerateColumns="false" CssClass="datatable table table-hover table-bordered" ShowHeaderWhenEmpty="true" OnRowDeleting="GridView1_RowDeleting" EmptyDataText="No Record Found" ClientIDMode="Static" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Voucher Date" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Eval("VoucherTx_Date").ToString() %>' runat="server" />
                                                <asp:HiddenField ID="HF_VoucherTx_ID" runat="server" Value='<%# Eval("VoucherTx_ID").ToString() %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="100%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedger_Name" Font-Bold="true" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                                <%-- <div id="div" runat="server">                                              
                                            </div>--%>
                                                <asp:Panel ID="pnlLedger" runat="server" Style="display: block" Width="100%">
                                                    <span class="HideRecord">(As Per Details)</span>
                                                    <asp:GridView ID="GVLBillbyBill" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid billbybill BillByBill HideRecord" GridLines="None" ShowHeader="false" Width="80%">
                                                        <Columns>
                                                            <asp:BoundField DataField="BillByBillTx_RefType" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="BillByBillTx_Ref" ItemStyle-Width="20%" ItemStyle-Font-Size="X-Small" />
                                                            <asp:BoundField DataField="BillByBillTx_Date" ItemStyle-Width="10%" ItemStyle-Font-Size="X-Small" />
                                                            <asp:TemplateField ItemStyle-Width="30%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" runat="server" Text='<%#Eval("BillByBillTx_Amount")+ " " + Eval("AmtType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                      <asp:GridView ID="GVLCostCentre" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid rajendra costcentre costcentre" GridLines="None" ShowHeader="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="CategoryName" ItemStyle-Width="40%" />
                                                            <asp:BoundField DataField="SubCategoryName" ItemStyle-Width="40%"/>                                                        
                                                            <asp:TemplateField ItemStyle-Width="20%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="gvLedger" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid Ledger ledger HideRecord" GridLines="None" ShowHeader="false" OnRowDataBound="gvLedger_RowDataBound" Width="100%">
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-Width="90%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLName" Font-Bold="true" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                                                    <asp:Label ID="lblBVID" Visible="false" Text='<%# Eval("VoucherTx_ID").ToString() %>' runat="server" />
																	<asp:Label ID="lblLedgerTx_OrderBy" Visible="false" Text='<%# Eval("LedgerTx_OrderBy").ToString() %>' runat="server" />
                                                                    <asp:Label ID="lblBLID" Visible="false" Text='<%# Eval("Ledger_ID").ToString() %>' runat="server" />
                                                                    <asp:Panel ID="pnlBillByBill" runat="server" Style="display: block">
                                                                        <asp:GridView ID="GVMLBillbyBill" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid billbybill BillByBill HideRecord" GridLines="None" ShowHeader="false" Width="80%">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="BillByBillTx_RefType" ItemStyle-Width="10%" />
                                                                                <asp:BoundField DataField="BillByBillTx_Ref" ItemStyle-Width="20%" ItemStyle-Font-Size="X-Small" />
                                                                                <asp:BoundField DataField="BillByBillTx_Date" ItemStyle-Width="10%" ItemStyle-Font-Size="X-Small" />
                                                                                <asp:TemplateField ItemStyle-Width="30%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl" runat="server" Text='<%#Eval("BillByBillTx_Amount")+ " " + Eval("AmtType")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                          <asp:GridView ID="GVMLCostCentre" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid costcentre costcentre" GridLines="None" ShowHeader="false" Width="95%">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="CategoryName" ItemStyle-Width="40%" />
                                                                                <asp:BoundField DataField="SubCategoryName" ItemStyle-Width="40%"  />
                                                                                 
                                                                                <asp:TemplateField ItemStyle-Width="20%">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbl" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>
																		<asp:GridView ID="GVMLChequeDetail" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid cheque cheque" GridLines="None" ShowHeader="false" Width="95%">
                                                                            <Columns>        
                                                                                <asp:BoundField DataField="ChequeTx_No"  ItemStyle-Width="30%"  />
                                                                                 <asp:BoundField DataField="ChequeTx_Date"  ItemStyle-Width="30%"  />
                                                                                <asp:BoundField DataField="ChequeTx_Amount" ItemStyle-Width="60%"  />
                                                                               
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField ItemStyle-Width="30%" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" runat="server" Text='<%#Eval("Tx_Amount")+ " " + Eval("AmtType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="GvItem" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid Item item HideRecord" GridLines="None" ShowHeader="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="ItemName" ItemStyle-Width="50%" />
                                                            <asp:BoundField DataField="Quantity" ItemStyle-Width="20%" />
                                                            <asp:TemplateField ItemStyle-Width="50%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" runat="server" Text='<%#Eval("Rate")+ " /" + Eval("UQCCode")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Amount" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Right" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:GridView ID="gvSubLedger" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid Ledger HideRecord" GridLines="None" ShowHeader="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="Ledger_Name" ItemStyle-Font-Bold="true" ItemStyle-Width="50%" />
                                                            <asp:TemplateField ItemStyle-Width="20%" ItemStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" runat="server" Text='<%#Eval("Tx_Amount")+ " " + Eval("AmtType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:Label ID="lblNarration" class="Narration narration HideRecord" Text="" runat="server" />
                                                </asp:Panel>
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
                                        <asp:TemplateField HeaderText="Office Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffice_Name" Text='<%# Eval("Office_Name").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit Amt." ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amt." ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%" ItemStyle-CssClass="hidden-print" HeaderStyle-CssClass="hidden-print">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="hpView" runat="server" CssClass="label label-info" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                                <asp:LinkButton ID="hpEdit" runat="server" CssClass="label label-primary" CommandName="Editing" CommandArgument='<%# Eval("VoucherTx_ID") %>' Text="Edit" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                                <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');" CssClass="label label-danger hidden"></asp:LinkButton>
                                                <asp:LinkButton ID="hpprint" CssClass="label label-primary" runat="server" Text="Print" CommandName="Print" CommandArgument='<%# Eval("VoucherTx_ID") %>' OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                                <asp:Label ID="lblOfficeID" CssClass="hidden" Text='<%# Eval("Office_ID").ToString() %>' runat="server" />
                                                <asp:Label ID="lblV_Editright" CssClass="hidden" Text='<%# Eval("V_Editright").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="." ItemStyle-Width="12%" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVID" Text='<%# Eval("VoucherTx_ID").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="." ItemStyle-Width="12%" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLID" Text='<%# Eval("Ledger_ID").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
								<div class="NonPrintable">
                                    <table width="100%" style="margin-top: 70px;">
                                        <tr>
                                            <td style="text-align: center"><b>Cashier<br />
                                                Signature</b></td>
                                            <td style="text-align: center"><b>Asst.Grade 1<br />
                                                (Signature)</b></td>
                                            <td style="text-align: center"><b>Deputy Manager(Account)<br />
                                                (Signature)</b></td>
                                            <td style="text-align: center"><b>Manager(Accounts )<br />
                                                (Signature)</b></td>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <%-- <asp:Button ID="btnPrintAll" runat="server" Text="Print All Pages" OnClick="btnPrintAll_Click" />
                           <asp:Button ID="btnPrintCurrent" runat="server" Text ="Print Current Page" OnClick="btnPrintCurrent_Click" />--%>

                                <asp:Label ID="lblTotal" runat="server" />
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
        $(window).on('load', function () {
            $(".HideRecord").css("display", "none");
            $(".costcentre").css("display", "none");
        })
        function handleKeyDown(e) {
            var ctrlPressed = 0;
            var altPressed = 0;
            var shiftPressed = 0;
            var evt = (e == null ? event : e);

            shiftPressed = evt.shiftKey;
            altPressed = evt.altKey;
            ctrlPressed = evt.ctrlKey;
            self.status = ""
               + "shiftKey=" + shiftPressed
               + ", altKey=" + altPressed
               + ", ctrlKey=" + ctrlPressed

            if ((altPressed) && (evt.keyCode == 87)) {
                if ($('.HideRecord').is(':visible')) {
                    $(".HideRecord").css("display", "none");
                    $(".costcentre").css("display", "none");
                    $("p.subledger").css("border-top", "none");
                }
                else {
                    $(".HideRecord").css("display", "block");
                    document.getElementById('<%= chkBillByBill.ClientID%>').checked = true;
                    document.getElementById('<%= chkItemDetail.ClientID%>').checked = true;
                    document.getElementById('<%= chkNarration.ClientID%>').checked = true;
                    document.getElementById('<%= ChkLedger.ClientID%>').checked = true;
                    document.getElementById('<%= chkCostCentre.ClientID%>').checked = false;

                    //$("p.subledger").css("border-top", "1px solid #ccc");
                }
            }
            //alert("You pressed the " + fromKeyCode(evt.keyCode)
            // + " key (keyCode " + evt.keyCode + ")\n"
            // + "together with the following keys:\n"
            // + (shiftPressed ? "Shift " : "")
            // + (altPressed ? "Alt " : "")
            // + (ctrlPressed ? "Ctrl " : "")
            //)            

            return true;
        }

        document.onkeydown = handleKeyDown;
        function HideBillByBill() {
            debugger;
            if (document.getElementById('<%= chkBillByBill.ClientID%>').checked) {


                $(".BillByBill").css("display", "block");
            }
            else {
                $(".BillByBill").css("display", "none");
            }
        }
        function HideCostCentre() {
            debugger;

            if (document.getElementById('<%= chkCostCentre.ClientID%>').checked) {


                $(".costcentre").css("display", "block");
            }
            else {
                $(".costcentre").css("display", "none");
            }
        }
		function HideCheque() {
            debugger;

            if (document.getElementById('<%= chkChequeDetail.ClientID%>').checked) {


                $(".cheque").css("display", "block");
            }
            else {
                $(".cheque").css("display", "none");
            }
        }
        function HideItem() {
            debugger;
            if (document.getElementById('<%= chkItemDetail.ClientID%>').checked) {


                $(".Item").css("display", "block");
            }
            else {
                $(".Item").css("display", "none");
            }
        }
        function HideNarration() {
            debugger;
            if (document.getElementById('<%= chkNarration.ClientID%>').checked) {


                $(".Narration").css("display", "block");
            }
            else {
                $(".Narration").css("display", "none");
            }
        } HideOppositeLedger
        function HideOppositeLedger() {
            debugger;
            if (document.getElementById('<%= ChkLedger.ClientID%>').checked) {


               $(".Ledger").css("display", "block");
           }
           else {
               $(".Ledger").css("display", "none");
           }
       }


       function validateform() {
           debugger;
           var msg = "";
           if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select From Date. \n";
            }

            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select To Date. \n";
            }
            var Fromday = 0;
            var FromMonth = 0;
            var FromYear = 0;
            var Today = 0;
            var ToMonth = 0;
            var ToYear = 0;
            var y = document.getElementById("txtFromDate").value; //This is a STRING, not a Date
            if (y != "") {
                var dateParts = y.split("/");   //Will split in 3 parts: day, month and year
                var yday = dateParts[0];
                var ymonth = dateParts[1];
                var yyear = dateParts[2];

                Fromday = dateParts[0];
                FromMonth = dateParts[1];
                FromYear = dateParts[2];

                var yd = new Date(yyear, parseInt(ymonth, 10) - 1, yday);
            }
            else {
                var yd = "";
            }

            var z = document.getElementById("txtToDate").value; //This is a STRING, not a Date
            if (z != "") {
                var dateParts = z.split("/");   //Will split in 3 parts: day, month and year
                var zday = dateParts[0];
                var zmonth = dateParts[1];
                var zyear = dateParts[2];

                Today = dateParts[0];
                ToMonth = dateParts[1];
                ToYear = dateParts[2];

                var zd = new Date(zyear, parseInt(zmonth, 10) - 1, zday);
            }
            else {
                var zd = "";
            }
            if (yd != "" && zd != "") {
                if (yd > zd) {
                    msg += "To Date should be greater than From Date ";
                }
                else {

                    if ((FromYear == ToYear - 1) || (FromYear == ToYear)) {
                        //if (FromYear == ToYear && ToMonth <= 12 && ToMonth > 3 && FromMonth >= 4) {
                        //}
                        //if (FromYear == ToYear && FromMonth <= 3 && ToMonth <= 3) {
                        //}
                        //else if (FromYear < ToYear && ToMonth <= 3 && FromMonth >= 4) {
                        //}
                        if (FromYear == ToYear && FromMonth <= 3 && ToMonth <= 3) {
                        }
                        else if (FromYear == ToYear && FromMonth >= 4 && ToMonth <= 12) {
                        }
                        else if (FromYear != ToYear && FromMonth > 3 && ToMonth <= 3) {
                        }
                        else {
                            msg += "Selection of Dates (From Date - To Date) should be between Financial Year.";
                        }
                    }
                    else {
                        msg += "Selection of Dates (From Date - To Date) should be between Financial Year.";
                    }

                }
            }
           <%-- if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }--%>
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }

        }
        //function PrintPage() {
        //    window.print();
        //}
    </script>
    <script type="text/javascript">
        var tableToExcel = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,'
              , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
              , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
              , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                window.location.href = uri + base64(format(template, ctx))
            }
        })()
    </script>
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>
    <script src="js/dataTables.buttons.min.js"></script>
    <script src="js/buttons.flash.min.js"></script>
    <script src="js/jszip.min.js"></script>
    <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>
    <script src="js/buttons.html5.min.js"></script>
    <script src="js/buttons.print.min.js"></script>
    <script src="js/buttons.colVis.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: false,
            //columnDefs: [{
            //    targets: 'no-sort',
            //    orderable: false
            //}],
            columnDefs: [{
                orderable: false
            }],
            "bSort": false,
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [
                //    {
                //    extend: 'print',
                //    text: '<i class="fa fa-print"></i> Print',
                //    title: $('h3').text(),
                //    exportOptions: {
                //        columns: [0, 1, 2, 3, 4, 5, 6]
                //    },
                //    footer: true,
                //    autoPrint: true
                //},
                //{
                //    extend: 'excel',
                //    text: '<i class="fa fa-file-excel-o"></i> Excel',
                //    title: $('h3').text(),
                //    exportOptions: {
                //        columns: [0, 1, 2, 3, 4, 5, 6]
                //    },
                //    footer: true
                //}
                ],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
    </script>
      <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script>

        //$(function () {
        //    $('[id*=ddlOffice]').multiselect({
        //        includeSelectAllOption: true,
        //        includeSelectAllOption: true,
        //        buttonWidth: '100%',

        //    });


        //});
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
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }
    </style>
    <script>
         //$('.datatable').DataTable({
        //    paging: false,
        //    //columnDefs: [{
        //    //    targets: 'no-sort',
        //    //    orderable: false
        //    //}],
        //    columnDefs: [{
        //        orderable: false
        //    }],
        //    "bSort": false,
        //    dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
        //      '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
        //      '<"row"<"col-sm-5"i><"col-sm-7"p>>',
        //    fixedHeader: {
        //        header: true
        //    },
        //    buttons: {
        //        buttons: [
        //        //    {
        //        //    extend: 'print',
        //        //    text: '<i class="fa fa-print"></i> Print',
        //        //    title: $('h3').text(),
        //        //    exportOptions: {
        //        //        columns: [0, 1, 2, 3, 4, 5, 6]
        //        //    },
        //        //    footer: true,
        //        //    autoPrint: true
        //        //},
        //        //{
        //        //    extend: 'excel',
        //        //    text: '<i class="fa fa-file-excel-o"></i> Excel',
        //        //    title: $('h3').text(),
        //        //    exportOptions: {
        //        //        columns: [0, 1, 2, 3, 4, 5, 6]
        //        //    },
        //        //    footer: true
        //        //}
        //        ],
        //        dom: {
        //            container: {
        //                className: 'dt-buttons'
        //            },
        //            button: {
        //                className: 'btn btn-default'
        //            }
        //        }
        //    }
        //});

        function validateform() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select From Date. \n";
            }

            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select To Date. \n";
            }
            var Fromday = 0;
            var FromMonth = 0;
            var FromYear = 0;
            var Today = 0;
            var ToMonth = 0;
            var ToYear = 0;
            var y = document.getElementById("txtFromDate").value; //This is a STRING, not a Date
            if (y != "") {
                var dateParts = y.split("/");   //Will split in 3 parts: day, month and year
                var yday = dateParts[0];
                var ymonth = dateParts[1];
                var yyear = dateParts[2];

                Fromday = dateParts[0];
                FromMonth = dateParts[1];
                FromYear = dateParts[2];

                var yd = new Date(yyear, parseInt(ymonth, 10) - 1, yday);
            }
            else {
                var yd = "";
            }

            var z = document.getElementById("txtToDate").value; //This is a STRING, not a Date
            if (z != "") {
                var dateParts = z.split("/");   //Will split in 3 parts: day, month and year
                var zday = dateParts[0];
                var zmonth = dateParts[1];
                var zyear = dateParts[2];

                Today = dateParts[0];
                ToMonth = dateParts[1];
                ToYear = dateParts[2];

                var zd = new Date(zyear, parseInt(zmonth, 10) - 1, zday);
            }
            else {
                var zd = "";
            }
            if (yd != "" && zd != "") {
                if (yd > zd) {
                    msg += "To Date should be greater than From Date ";
                }
                else {

                    if ((FromYear == ToYear - 1) || (FromYear == ToYear)) {
                        //if (FromYear == ToYear && ToMonth <= 12 && ToMonth > 3 && FromMonth >= 4) {
                        //}
                        //if (FromYear == ToYear && FromMonth <= 3 && ToMonth <= 3) {
                        //}
                        //else if (FromYear < ToYear && ToMonth <= 3 && FromMonth >= 4) {
                        //}
                        if (FromYear == ToYear && FromMonth <= 3 && ToMonth <= 3) {
                        }
                        else if (FromYear == ToYear && FromMonth >= 4 && ToMonth <= 12) {
                        }
                        else if (FromYear != ToYear && FromMonth > 3 && ToMonth <= 3) {
                        }
                        else {
                            msg += "Selection of Dates (From Date - To Date) should be between Financial Year.";
                        }
                    }
                    else {
                        msg += "Selection of Dates (From Date - To Date) should be between Financial Year.";
                    }

                }
            }
         if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
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
        //function PrintPage() {
        //    window.print();
        //}
    </script>
</asp:Content>
