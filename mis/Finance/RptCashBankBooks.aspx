<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptCashBankBooks.aspx.cs" Inherits="mis_Finance_RptCashBankBooks" %>

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


        span.Ledger_Amt {
            max-width: 30%;
            display: inline;
            float: right;
        }

        span.Ledger_Name {
            max-width: 70%;
            display: inline;
            /*float: left;*/
        }

        p.subledger {
            border-top: 1px solid #ccc;
            margin: 0px;
        }

        .report-title {
            font-weight: 600;
            font-size: 15px;
            color: #123456;
        }

        .align-right {
            text-align: right !important;
        }

        .Scut {
            color: tomato;
        }

        .Childtd td {
            padding-left: 15px !important;
        }

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->

        <section class="content">
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Cash / Bank Book(s)</h3>
                    <p class="hide_print">
                        <span runat="server" id="spnAltB">[<span class="Scut">Alt+b</span> - Back View] </span><span runat="server" id="spnAltW">,[<span class="Scut">Alt+w</span> - Condensed & Detailed View]</span>

                    </p>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                </div>

                <div class="box-body">
                    <div class="hide_print">
                        <div class="row">
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
                                    <label>To Date</label><span style="color: red">*</span>
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
                                    <label>Office Name</label><span style="color: red">*</span>
                                    <%--<asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged">
                                </asp:DropDownList>--%>
                                    <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <br />
                                    <label>Filter Amount</label>
                                    : &nbsp;&nbsp;
                               
                                <asp:CheckBox ID="chkOpeningBal" runat="server" Checked="false" Text="Opening Bal.&nbsp;&nbsp;" />
                                    <asp:CheckBox ID="chkTransactionAmt" runat="server" Checked="false" Text="Transaction&nbsp;&nbsp;" />
                                    <asp:CheckBox ID="chkClosingBal" runat="server" Checked="true" Text="Closing Bal.&nbsp;&nbsp;" />
                                    <%--<input id="chkClosingBal" type="checkbox"  name="ClosingBal" /> <label>Closing Bal.</label>--%>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success" Style="margin-top: 24px;" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblheadingFirst" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <strong style="height: 40px;">
                                <asp:Label ID="lblHeadName" runat="server" Style="font-size: 20px;" Text=""></asp:Label>
                                <br />
                                <div class="hide_print">
                                    <asp:Button ID="btnHeadExcel" runat="server" CssClass="btn btn-default" Text="Excel" OnClick="btnHeadExcel_Click" />
                                    <asp:Button ID="btnMonthExcel" runat="server" CssClass="btn btn-default" Text="Excel" OnClick="btnMonthExcel_Click" />
                                    <asp:Button ID="btnDayBookExcel" runat="server" CssClass="btn btn-default hidden" Text="Excel" OnClick="btnDayBookExcel_Click" />
                                </div>
                                <%--  <asp:Label ID="lblGridMsg" runat="server" Style="font-size: 20px; color:red;" Text="" Visible="false"></asp:Label>--%>
                            </strong>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-block btn-success hidden Aselect1" Text="<< BACK " OnClick="btnBack_Click" AccessKey="B" />
                        </div>
                        <div class="col-md-12">
                            <asp:Panel ID="PnlLevel1" runat="server">
                            <table id="Tab1" class=" table table-hover table-bordered">
                                <thead>
                                    <tr>
                                        <th>Group Name</th>
                                        <th>Opening
                                            <br>
                                            [Debit Amt.]</th>
                                        <th>Txn.
                                            <br>
                                            [Debit Amt.]</th>
                                        <th>Txn.
                                            <br>
                                            [Credit Amt.]</th>
                                        <th>Closing 
                                            <br>
                                            [Debit Amt.]</th>
                                    </tr>
                                </thead>
                                <tbody>

                                    <asp:Repeater ID="RptMain" runat="server" OnItemCommand="RptMain_ItemCommand">
                                        <ItemTemplate>
                                            <tr style="background-color: #ea93933d;">
                                                <td style="font-weight: 600">
                                                    <asp:LinkButton ID="btnHead" runat="server" CommandName='<%# Eval("Head_ID").ToString() %>'><%# Eval("Head_Name").ToString() %></asp:LinkButton>
                                                    <asp:Label ID="lblHead_ID" CssClass="hidden" Visible="false" Text='<%# Eval("Head_ID").ToString() %>' runat="server" />
                                                </td>
                                                <td class="align-right"><%# Eval("PreOpeningBalance").ToString() %></td>
                                                <td class="align-right"><%# Eval("DebitAmt").ToString() %></td>
                                                <td class="align-right"><%# Eval("CreditAmt").ToString() %></td>
                                                <td class="align-right"><%# Eval("ClosingBalance").ToString() %></td>
                                            </tr>
                                            <asp:Repeater ID="RptChild" runat="server" OnItemCommand="RptChild_ItemCommand">
                                                <ItemTemplate>
                                                    <tr class="Childtd">
                                                        <td>
                                                            <asp:LinkButton ID="btnLedger" runat="server" CommandName='<%# Eval("Ledger_ID").ToString() %>'><%# Eval("Ledger_Name").ToString() %></asp:LinkButton>
                                                        </td>
                                                        <td class="align-right"><%# Eval("PreOpeningBalance").ToString() %></td>
                                                        <td class="align-right"><%# Eval("DebitAmt").ToString() %></td>
                                                        <td class="align-right"><%# Eval("CreditAmt").ToString() %></td>
                                                        <td class="align-right"><%# Eval("ClosingBalance").ToString() %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tbody>
                            </table>

                            </asp:Panel>
                            <%--HEAD--%>
                            <asp:GridView ID="GridView1" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="datatable table table-hover table-bordered" Style="margin-bottom: 0px;" OnRowCommand="GridView1_RowCommand" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <asp:Label ID="lblHead_ID" CssClass="hidden" Visible="false" Text='<%# Eval("Head_ID").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="Aselect1" CommandName="View" HeaderText="Group Name" DataTextField="Head_Name" />
                                    <asp:TemplateField HeaderText="Opening <br> [Debit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening <br> [Credit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Txn. <br> [Debit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Txn. <br> [Credit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing <br> [Debit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing <br> [Credit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <%--LEDGER--%>
                            <asp:GridView ID="GridView2" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="datatable table table-hover table-bordered" OnRowCommand="GridView2_RowCommand" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <asp:Label ID="lblLedger_ID" CssClass="hidden" Visible="false" Text='<%# Eval("Ledger_ID").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="Aselect1" CommandName="View" HeaderText="Ledger Name" DataTextField="Ledger_Name" />
                                    <asp:TemplateField HeaderText="Opening <br> [Debit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening <br> [Credit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Txn. <br> [Debit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Txn. <br> [Credit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing <br> [Debit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing <br> [Credit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Opening Bal." ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit Amt." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit Amt." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Bal." ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <%--LEDGER DETAIL MONTH WISE--%>
                            <asp:GridView ID="GridView3" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="table table-hover table-bordered" OnRowCommand="GridView3_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <asp:Label ID="lblLedger_ID" CssClass="hidden" Visible="false" Text='<%# Eval("Ledger_ID").ToString() %>' runat="server" />
                                            <asp:Label ID="lblMonthID" CssClass="hidden" Visible="false" Text='<%# Eval("MonthID").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="Aselect1" CommandName="View" HeaderText="Month Name" DataTextField="MonthName" />

                                    <asp:TemplateField HeaderText="Debit Amt." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit Amt." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Bal." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <%--LEDGER DETAIL MONTH WISE--%>
                            <div id="DivTable" runat="server"></div>

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
                        else if (FromYear != ToYear && FromMonth >= 3 && ToMonth <= 3) {
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



            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;

            }

        }

        function PrintPage() {
            window.print();
        }
    </script>

    <%--<script src="../../../mis/js/jquery.js" type="text/javascript"></script>--%>
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
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }
    </style>


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
    <script src="js/fromkeycode.js"></script>

    <script>
        $('.lastdatatable').DataTable({
            paging: false,
            dom: 'Bfrtip',
            ordering: false,
            buttons: [
                {
                    extend: 'colvis',
                    collectionLayout: 'fixed two-column',
                    text: '<i class="fa fa-eye"></i> Columns'
                },
                //{
                //    extend: 'print',
                //    text: '<i class="fa fa-print"></i> Print',
                //    title: $('h1').text(),
                //    footer: true,
                //    autoPrint: true
                //},
                {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true
                }

            ]
        });
    </script>



    <script>
        $('.datatable').DataTable({
            paging: false,
            searching: false,
            info: false,
            lengthChange: false,
            bshort: false
        });

        //function HideRow() {
        $(document).ready(function () {
            debugger;
            // Grid 1
            var i = 0;
            var cols = $("#GridView1").find('tr')[0].cells.length;
            $('#GridView1 tr').each(function (index) {

                if (i > 0) {
                    if (cols == 2) {
                        debugger;
                        var V2 = $(this).children("td").eq(1).find('span').html();
                        if (V2 == "") {
                            $(this).children("td").parent('tr').hide();
                        }
                    }
                    else if (cols == 3) {
                        debugger;
                        var V2 = $(this).children("td").eq(1).find('span').html();
                        var V3 = $(this).children("td").eq(2).find('span').html();
                        if (V2 == "" && V3 == "") {
                            $(this).children("td").parent('tr').hide();
                        }
                    }
                    else if (cols == 4) {
                        debugger;
                        var V2 = $(this).children("td").eq(1).find('span').html();
                        var V3 = $(this).children("td").eq(2).find('span').html();
                        var V4 = $(this).children("td").eq(3).find('span').html();
                        if (V2 == "" && V3 == "" && V4 == "") {
                            $(this).children("td").parent('tr').hide();
                        }
                    }
                    else if (cols == 5) {
                        var V2 = $(this).children("td").eq(1).find('span').html();
                        var V3 = $(this).children("td").eq(2).find('span').html();
                        var V4 = $(this).children("td").eq(3).find('span').html();
                        var V5 = $(this).children("td").eq(4).find('span').html();
                        if (V2 == "" && V3 == "" && V4 == "" && V5 == "") {
                            $(this).children("td").parent('tr').hide();
                        }

                    }
                }
                i++;
            });

            // Grid 2
            i = 0;
            cols = $("#GridView2").find('tr')[0].cells.length;
            $('#GridView2 tr').each(function (index) {

                if (i > 0) {
                    if (cols == 2) {
                        debugger;
                        var V2 = $(this).children("td").eq(1).find('span').html();
                        if (V2 == "") {
                            $(this).children("td").parent('tr').hide();
                        }
                    }
                    else if (cols == 3) {
                        debugger;
                        var V2 = $(this).children("td").eq(1).find('span').html();
                        var V3 = $(this).children("td").eq(2).find('span').html();
                        if (V2 == "" && V3 == "") {
                            $(this).children("td").parent('tr').hide();
                        }
                    }
                    else if (cols == 4) {
                        debugger;
                        var V2 = $(this).children("td").eq(1).find('span').html();
                        var V3 = $(this).children("td").eq(2).find('span').html();
                        var V4 = $(this).children("td").eq(3).find('span').html();
                        if (V2 == "" && V3 == "" && V4 == "") {
                            $(this).children("td").parent('tr').hide();
                        }
                    }
                    else if (cols == 5) {
                        var V2 = $(this).children("td").eq(1).find('span').html();
                        var V3 = $(this).children("td").eq(2).find('span').html();
                        var V4 = $(this).children("td").eq(3).find('span').html();
                        var V5 = $(this).children("td").eq(4).find('span').html();
                        if (V2 == "" && V3 == "" && V4 == "" && V5 == "") {
                            $(this).children("td").parent('tr').hide();
                        }

                    }
                }
                i++;
            });

        });
        // }



    </script>
    <script>
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
                    $("p.subledger").css("border-top", "none");
                }
                else {
                    $(".HideRecord").css("display", "table-row");
                    $("p.subledger").css("border-top", "1px solid #ccc");
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
    </script>

</asp:Content>

