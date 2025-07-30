<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptDetailedDaybook_PCS.aspx.cs" Inherits="mis_Finance_RptDetailedDaybook_PCS" %>

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
        .Dtime {
            display: none;
        }

        @media print {
            .Hiderow, .main-footer, .dataTables_filter, .dataTables_info {
                display: none;
            }
			.NonPrintable {
                display: block;
            }
            .box {
                border: none;
            }

            th {
                background-color: #ddd;
                text-decoration: solid;
            }

            .tblheadingslip {
                font-size: 8px !important;
                background: black;
                color: red;
            }

            .lblheadingFirst p {
                text-align: center !important;
                font-size: 10px !important;
            }

            .Dtime {
                display: block;
            }
        }

        .align-right {
            text-align: right !important;
        }

        .UnderLine {
            style ="text-decoration:underline";
        }

        .item {
            background-color: wheat;
        }

        .billbybill {
            /*background-color: gray !important;
            color: white !important;*/
            margin-left: 3%;
            color: black !important;
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

        table.dataTable tbody th, table.dataTable tbody td {
            padding: 3px 3px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <asp:Label ID="lblTime" runat="server" CssClass="Dtime" Style="font-weight: 800;" Text="" ClientIDMode="Static"></asp:Label>
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Detailed Day Book</h3>
                    <input type="button" id="btnHide" onclick="window.print()" value="Print" class="pull-right" />
                    <input type="button" id="btnExcel" onclick="tableToExcel('tableData', 'W3C Example Table')" value="Export to Excel" class="pull-right" />
                    <p class="hide_print">
                        <span runat="server" id="spnAltW">[<span class="Scut">Alt+w</span> - Condensed & Detailed View]</span>
                    </p>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                </div>
                <div class="box-body">
                    <div class="row Hiderow">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label><span style="color: red">*</span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>

                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" OnTextChanged="txtDate_TextChanged" AutoPostBack="true"></asp:TextBox>
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
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row Hiderow pull-right HideRecord hide_print">
                        <div class="col-md-12  ">
                            <div class="form-group Hiderow" style="margin-top: 28px;">
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
                                <%-- <div class="table-responsive"> </div>--%>
                                <asp:GridView ID="GridView1" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" class="datatable table table-hover table-bordered" ShowHeaderWhenEmpty="true" OnRowDeleting="GridView1_RowDeleting" EmptyDataText="No Record Found" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Voucher Date." ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_Date" Text='<%# Eval("VoucherTx_Date").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="100%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedger_Name" Font-Bold="true" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                                <%-- <div id="div" runat="server">                                              
                                            </div>--%>
                                                <asp:Panel ID="pnlLedger" runat="server" Style="display: block" Width="100%">
                                                    <span class="HideRecord">(As Per Details)</span>
                                                    <asp:GridView ID="GVLBillbyBill" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid rajendra billbybill billbybill HideRecord" GridLines="None" ShowHeader="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="BillByBillTx_RefType" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="BillByBillTx_Ref" ItemStyle-Width="50%" ItemStyle-Font-Size="X-Small" />
                                                            <asp:BoundField DataField="BillByBillTx_Date" ItemStyle-Width="10%" ItemStyle-Font-Size="X-Small" />
                                                            <asp:TemplateField ItemStyle-Width="30%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl" runat="server" Text='<%#Eval("BillByBillTx_Amount")+ " " + Eval("AmtType")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
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
                                                            <asp:TemplateField ItemStyle-Width="100%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLName" Font-Bold="true" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                                                    <asp:Label ID="lblBVID" CssClass="hidden" Text='<%# Eval("VoucherTx_ID").ToString() %>' runat="server" />
																	  <asp:Label ID="lblLedgerTx_OrderBy" Visible="false" Text='<%# Eval("LedgerTx_OrderBy").ToString() %>' runat="server" />
                                                                    <asp:Label ID="lblBLID" CssClass="hidden" Text='<%# Eval("Ledger_ID").ToString() %>' runat="server" />
                                                                    <asp:Panel ID="pnlBillByBill" runat="server" Style="display: block">
                                                                        <asp:GridView ID="GVMLBillbyBill" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid billbybill BillByBill HideRecord" GridLines="None" ShowHeader="false" Width="95%">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="BillByBillTx_RefType" ItemStyle-Width="10%" />
                                                                                <asp:BoundField DataField="BillByBillTx_Ref" ItemStyle-Width="50%" ItemStyle-Font-Size="X-Small" />
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
                                                                               <%-- <asp:BoundField DataField="AcPayee" ItemStyle-Width="10%" />
                                                                                <asp:BoundField DataField="FavouringName"  ItemStyle-Width="10%"  />--%>
                                                                                <asp:BoundField DataField="ChequeTx_No"  ItemStyle-Width="30%"  />
                                                                                 <asp:BoundField DataField="ChequeTx_Date"  ItemStyle-Width="30%"  />
                                                                                <asp:BoundField DataField="ChequeTx_Amount" ItemStyle-Width="60%"  />
                                                                               
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </asp:Panel>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField ItemStyle-Width="20%" ItemStyle-Font-Bold="true">
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
                                        <asp:TemplateField HeaderText="Office Name.">
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
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%" HeaderStyle-CssClass="Hiderow" ItemStyle-CssClass="Hiderow">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="hpView" runat="server" CssClass="label label-info" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View" OnClientClick="window.document.forms[0].target = '_blank';"></asp:LinkButton>
                                                <asp:LinkButton ID="hpEdit" runat="server" CssClass="label label-primary" CommandName="Editing" CommandArgument='<%# Eval("VoucherTx_ID") %>' Text="Edit" OnClientClick="window.document.forms[0].target = '_blank';"></asp:LinkButton>
                                                <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');" CssClass="label label-danger"></asp:LinkButton>
                                                <asp:LinkButton ID="hpprint" CssClass="label label-primary" runat="server" Text="Print" CommandName="Print" CommandArgument='<%# Eval("VoucherTx_ID") %>' OnClientClick="window.document.forms[0].target = '_blank';"></asp:LinkButton>
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


                $(".billbybill").css("display", "block");
            }
            else {
                $(".billbybill").css("display", "none");
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
        } 
        function HideOppositeLedger() {
            debugger;
            if (document.getElementById('<%= ChkLedger.ClientID%>').checked) {


                $(".Ledger").css("display", "block");
            }
            else {
                $(".Ledger").css("display", "none");
            }
        }


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

</asp:Content>







