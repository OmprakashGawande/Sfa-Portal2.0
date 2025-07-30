<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptLedgerForOtherOffice.aspx.cs" Inherits="mis_Finance_Rpt" EnableEventValidation="false" %>

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

        /*.pay-sheet table {
            border: 1px solid #ddd;
        }*/

        @media print {

            .hide_print, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
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


        .item {
            background-color: wheat;
        }

        .billbybill {
            background-color: gray;
            color: white;
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
                <div class="box-header Hiderow">
                    <h3 class="box-title">Branch Ledger Copy In HO</h3>
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
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>List Of Ledger</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlLedger" CssClass="form-control select2" AutoPostBack="false">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success btn-block" Style="margin-top: 25px;" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="row pull-right HideRecord">
                        <div class="col-md-12">
                            <div class="form-group" style="margin-top: 28px;">
                                <label>Filter: &nbsp;&nbsp;</label>
                                <asp:CheckBox ID="ChkLedger" ClientIDMode="Static" runat="server" Checked="true" Text="Opposite Ledger.&nbsp;&nbsp;" onchange="HideOppositeLedger();" />
                                <asp:CheckBox ID="chkBillByBill" ClientIDMode="Static" runat="server" Checked="true" Text="BillWiseDetail.&nbsp;&nbsp;" onchange="HideBillByBill();" />
                                <asp:CheckBox ID="chkItemDetail" ClientIDMode="Static" runat="server" Checked="true" Text="Inventory.&nbsp;" onchange="HideItem();" />
                                <asp:CheckBox ID="chkNarration" ClientIDMode="Static" runat="server" Checked="true" Text="Narration.&nbsp;&nbsp;" onchange="HideNarration();" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="hide_print">
                                    <asp:LinkButton ID="btnPrint" runat="server" CssClass="btn btn-default" OnClick="btnPrint_Click"><i class="fa fa-print"></i>Print</asp:LinkButton>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime"></asp:Label>
                            <asp:GridView ID="GridView1" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="50" CssClass="table table-hover table-bordered pagination" ShowHeaderWhenEmpty="true" OnPageIndexChanging="GridView1_PageIndexChanging" EmptyDataText="No Record Found" ClientIDMode="Static" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Voucher Date" ItemStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Eval("VoucherTx_Date").ToString() %>' runat="server" />
                                            <asp:HiddenField ID="HF_VoucherTx_ID" runat="server" Value='<%# Eval("VoucherTx_ID").ToString() %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="100%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLedger_Name" Font-Bold="true" CssClass="ShowRecord" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
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
                                                <asp:GridView ID="gvLedger" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid Ledger ledger HideRecord" GridLines="None" ShowHeader="false" OnRowDataBound="gvLedger_RowDataBound" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="90%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLName" Font-Bold="true" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                                                <asp:Label ID="lblBVID" Visible="false" Text='<%# Eval("VoucherTx_ID").ToString() %>' runat="server" />
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
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Width="30%" ItemStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl" runat="server" Text='<%#Eval("Tx_Amount")+ " " + Eval("AmtType")%>'></asp:Label>
                                                            </ItemTemplate>
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
                                                        <asp:BoundField DataField="Amount" ItemStyle-Width="20%" />
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="gvSubLedger" runat="server" AutoGenerateColumns="false" CssClass="ChildGrid Ledger HideRecord" GridLines="None" ShowHeader="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="Ledger_Name" ItemStyle-Font-Bold="true" ItemStyle-Width="50%" />
                                                        <asp:TemplateField ItemStyle-Width="20%" ItemStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl" runat="server" Text='<%#Eval("Tx_Amount")+ " " + Eval("AmtType")%>'></asp:Label>
                                                            </ItemTemplate>
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
                                    <%--  <asp:TemplateField HeaderText="Office Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOffice_Name" Text='<%# Eval("Office_Name").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
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
                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%" ItemStyle-CssClass="hidden-print" HeaderStyle-CssClass="hidden-print">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="hpView" runat="server" CssClass="label label-info" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                            <asp:LinkButton ID="hpprint" CssClass="label label-primary" runat="server" Text="Print" CommandName="Print" CommandArgument='<%# Eval("VoucherTx_ID") %>' OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                            <asp:Label ID="lblOfficeID" CssClass="hidden" Text='<%# Eval("Office_ID").ToString() %>' runat="server" />
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
                            <br />
                            <%-- <asp:Button ID="btnPrintAll" runat="server" Text="Print All Pages" OnClick="btnPrintAll_Click" />
                           <asp:Button ID="btnPrintCurrent" runat="server" Text ="Print Current Page" OnClick="btnPrintCurrent_Click" />--%>

                            <asp:Label ID="lblTotal" runat="server" />
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
            $(".ShowRecord").css("display", "block");

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
                    $(".ShowRecord").css("display", "block");

                    $("p.subledger").css("border-top", "none");
                }
                else {
                    $(".HideRecord").css("display", "block");
                    $(".ShowRecord").css("display", "none");
                    document.getElementById('<%= chkBillByBill.ClientID%>').checked = true;
                    document.getElementById('<%= chkItemDetail.ClientID%>').checked = true;
                    document.getElementById('<%= chkNarration.ClientID%>').checked = true;
                    document.getElementById('<%= ChkLedger.ClientID%>').checked = true;

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
</asp:Content>
