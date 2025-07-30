<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptLedgerTrialBalance.aspx.cs" Inherits="mis_Finance_RptLedgerTrialBalance_aspx" %>

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
        .Dtime {
            display: none;
        }

        @media print {
            .Hiderow, .main-footer {
                display: none;
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

            .Dtime {
                display: block;
            }
        }

        .align-right {
            text-align: right !important;
            width: 10% !important;
        }

        span.Ledger_Amt {
            max-width: 30%;
            display: inline;
            float: right;
        }

        span.Ledger_Name {
            max-width: 70%;
            display: inline;
            float: left;
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

        .Scut {
            color: tomato;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <asp:Label ID="lblTime" runat="server" CssClass="Dtime" Style="font-weight: 800;" Text="" ClientIDMode="Static"></asp:Label>
            <div class="box box-success">
                <div class="box-header hide_print">
                    <h3 class="box-title">Alphabetical Trial Balance</h3>
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-default pull-right" Text="Print" Style="margin-left: 10px;" OnClientClick="window.print();"></asp:Button>
                    <p class="hide_print">

                        <span runat="server" id="spnAltB">[<span class="Scut">Alt+b</span> - Back View] </span><span runat="server" id="spnAltW">,[<span class="Scut">Alt+w</span> - Condensed & Detailed View]</span>
                    </p>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="hide_print">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Session<span style="color: red;">*</span></label>
                                    <asp:DropDownList ID="ddlSession" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
                                        <asp:ListItem Value="Date Range" Selected="True">Date Range</asp:ListItem>
                                        <asp:ListItem Value="Quarter 1">Quarter 1</asp:ListItem>
                                        <asp:ListItem Value="Quarter 2">Quarter 2</asp:ListItem>
                                        <asp:ListItem Value="Quarter 3">Quarter 3</asp:ListItem>
                                        <asp:ListItem Value="Quarter 4">Quarter 4</asp:ListItem>
                                        <asp:ListItem Value="Quarter 1 & 2">Quarter 1 & 2</asp:ListItem>
                                        <asp:ListItem Value="Quarter 2 & 3">Quarter 2 & 3</asp:ListItem>
                                        <asp:ListItem Value="Quarter 3 & 4">Quarter 3 & 4</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
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
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Filter Amount</label>
                                    : &nbsp;&nbsp;<br />

                                    <asp:CheckBox ID="chkOpeningBal" runat="server" Checked="false" Text="Opening Bal.&nbsp;&nbsp;" />
                                    <asp:CheckBox ID="chkTransactionAmt" runat="server" Checked="false" Text="Transaction&nbsp;&nbsp;" />
                                    <asp:CheckBox ID="chkClosingBal" runat="server" Checked="true" Text="Closing Bal.&nbsp;&nbsp;" />
                                </div>
                            </div>

                            <div class="col-md-3" runat="server" id="divRegionalOffice">
                                <div class="form-group">
                                    <label>Regional Office</label><span style="color: red">*</span>
                                    <asp:DropDownList ID="ddlRegionalOffice" runat="server" ClientIDMode="Static" CssClass="form-control" OnSelectedIndexChanged="ddlRegionalOffice_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Office Name</label><span style="color: red">*</span>
                                    <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success" Style="margin-top: 24px;" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <%--<asp:Button ID="btnBack" runat="server" CssClass="btn btn-block btn-success hidden" Text="<< BACK " OnClick="btnBack_Click" AccessKey="Q" />--%>
                                <asp:Button ID="btnBackB" runat="server" CssClass="btn btn-block btn-success hidden Aselect1" Text="<< BACK " OnClick="btnBackB_Click" AccessKey="B" />
                                <%--<asp:Button ID="btnBackW" runat="server" CssClass="btn btn-block btn-success hidden Aselect1" Text="<< BACK " OnClick="btnBackW_Click" AccessKey="W" />--%>
                                <div runat="server" id="divExcel">
                                    <div class="form-group">
                                        <input type="button" onclick="tableToExcel('tableData', 'W3C Example Table')" value="Export to Excel">
                                    </div>
                                </div>
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
                            </div>
                        </div>
                    </div>


                    <div id="tableData">
                        <div class="row">
                            <div class="col-md-12">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="5" style="text-align: center;">
                                            <asp:Label ID="lblheadingFirst" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <%--LEDGER--%>

                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="5" style="text-align: left;">
                                            <p class="report-title">
                                                <asp:Label ID="lblReportName" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="lblTab" runat="server" Style="font-weight: 700; font-size: 20px; color: red;" Text=""></asp:Label>
                                            </p>
                                        </td>
                                    </tr>
                                </table>


                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" class="datatable table table-hover table-bordered" ShowFooter="true" OnRowCommand="GridView2_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                <asp:Label ID="lblLedger_ID" CssClass="hidden" Text='<%# Eval("Ledger_ID").ToString() %>' runat="server" />
                                                <asp:Label ID="lblLedger_Name" CssClass="hidden" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField ButtonType="Link" CommandName="View" ControlStyle-CssClass="Aselect1" HeaderText="Ledger Name" DataTextField="Ledger_Name" />
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
                                        <%-- <asp:TemplateField HeaderText="Opening Bal." ItemStyle-Width="15%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpeningAmt" Text='<%# Eval("OpeningBalance").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit Amt." ItemStyle-Width="15%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit Amt." ItemStyle-Width="15%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Bal." ItemStyle-Width="15%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClosingAmt" Text='<%# Eval("ClosingAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <%--LEDGER DETAIL MONTH WISE--%>
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" OnRowCommand="GridView3_RowCommand" ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                <asp:Label ID="lblLedger_ID" CssClass="hidden" Text='<%# Eval("Ledger_ID").ToString() %>' runat="server" Visible="false" />
                                                <asp:Label ID="lblMonthID" CssClass="hidden" Text='<%# Eval("MonthID").ToString() %>' runat="server" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="Aselect1" CommandName="View" HeaderText="Month Name" DataTextField="MonthName" />

                                        <asp:TemplateField HeaderText="Debit Amt." ItemStyle-Width="10%" ItemStyle-CssClass="align-right" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amt." ItemStyle-Width="10%" ItemStyle-CssClass="align-right" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Bal." ItemStyle-Width="10%" ItemStyle-CssClass="align-right" ItemStyle-HorizontalAlign="Right">
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
                    //document.getElementById("txtDateOfReceipt").value = "";
                    //document.getElementById("txtDateOfFiling").value = "";
                }
                else {

                    if ((FromYear == ToYear - 1) || (FromYear == ToYear)) {
                        if (FromYear == ToYear && FromMonth <= 3 && ToMonth <= 3) {
                        }
                        else if (FromYear == ToYear && FromMonth >= 4 && ToMonth <= 12) {
                        }
                        else if (FromYear == (ToYear - 1) && FromMonth > 3 && ToMonth <= 3) {
                        }
                        else {
                            msg += "select Valid Date";
                        }
                    }
                    else {
                        msg += "select Valid Date";
                    }

                }
            }
          <%--  if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
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
        function PrintPage() {
            window.print();
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

    <link href="../../../mis/HR/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <%-- <link href="../../../mis/HR/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../../../mis/HR/css/jquery.dataTables.min.css" rel="stylesheet" />--%>


    <script src="../../../mis/HR/js/jquery.dataTables.min.js"></script>
    <script src="../../../mis/HR/js/dataTables.bootstrap.min.js"></script>
    <script src="../../../mis/HR/js/dataTables.buttons.min.js"></script>
    <script src="../../../mis/HR/js/buttons.flash.min.js"></script>
    <script src="../../../mis/HR/js/jszip.min.js"></script>
    <script src="../../../mis/HR/js/pdfmake.min.js"></script>
    <script src="../../../mis/HR/js/vfs_fonts.js"></script>
    <script src="../../../mis/HR/js/buttons.html5.min.js"></script>
    <script src="../../../mis/HR/js/buttons.print.min.js"></script>
    <script src="../../../mis/HR/js/buttons.colVis.min.js"></script>
    <script>
        $(document).ready(function () {
            $('.datatable').DataTable({
                /******Footer sum********/
                paging: false,

                columnDefs: [{
                    orderable: false
                }],
                "bSort": false,
                // "order": [[0, 'asc']],

                dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
                  '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
                  '<"row"<"col-sm-5"i><"col-sm-7"p>>',
                fixedHeader: {
                    header: true
                },
                buttons: {


                    buttons: [{
                        extend: 'print',
                        text: '<i class="fa fa-print"></i> Print',
                        title: $('.report-title').text(),
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6]
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel', footer: true,
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('.report-title').text(),
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6],
                            stripHtml: false,
                            format: {
                                body: function (data, column, row) {
                                    return (column === 1 && column === 5) ? data.replace(/\n/g, '"&CHAR(10)&CHAR(13)&"') : data.replace(/(&nbsp;|<([^>]+)>)/ig, "");;
                                }
                            }
                        },
                        customize: function (xlsx) {
                            var sheet = xlsx.xl.worksheets['sheet1.xml'];
                            $('row c', sheet).attr('s', '55');
                        },

                        footer: true
                    }],

                    dom: {
                        container: {
                            className: 'dt-buttons'
                        },
                        button: {
                            className: 'btn btn-default'
                        }
                    }
                },

            });
        });

    </script>

    <script src="../../../mis/js/jquery.js" type="text/javascript"></script>
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
