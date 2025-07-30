<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptTrialBalanceDetailed.aspx.cs" Inherits="mis_Finance_RptTrialBalanceDetailed" EnableEventValidation="true" %>

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
            width: 160px !important;
        }

        .Scut {
            color: tomato;
        }

        .Dtime {
            display: none;
        }

        @media print {

            .hide_print, .main-footer, .dt-buttons, .dataTables_filter, .backCss {
                display: none;
            }

            tfoot, thead {
                display: table-row-group;
                bottom: 0;
            }

            .Dtime {
                display: block;
            }

            .Wcss {
                width: 130px;
            }
        }

        @media print {
            a[href]:after {
                content: none !important;
            }
        }

        .voucherColumn {
            width: 150px !important;
        }

        .cssclass {
            color: black;
        }

            .cssclass:hover {
                color: blue;
            }

        .datepicker-dropdown:after {
            position: initial !important;
        }

        .datepicker.datepicker-dropdown.dropdown-menu.datepicker-orient-left.datepicker-orient-top {
            z-index: 9999 !important;
        }

        table {
            font-family: sans-serif;
        }

        td {
            font-size: 12px;
        }

        .Mhead {
            font-size: 13px;
            font-weight: 700;
        }

        .Ghead {
            font-size: 13px !important;
            font-weight: 700;
            background-color: #ded5d5;
        }

        .Chead {
            font-size: 12px !important;
            font-weight: 700;
        }

        .CHeadC {
            padding: 0px !important;
            padding-left: 8px !important;
        }

        .backCss {
            display: block;
            background-color: burlywood;
            width: 26px;
            text-align: center;
            border-radius: 4px;
            font-weight: 700;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->

        <section class="content">
            <asp:Label ID="lblTime" runat="server" CssClass="Dtime" Style="font-weight: 800;" Text="" ClientIDMode="Static"></asp:Label>
            <div class="box box-success">
                <div class="box-header Hiderow hide_print">
                    <h3 class="box-title">Detailed Trial Balance</h3>

                    <input type="button" class="btn btn-default pull-right" onclick="tableToExcel('DivData', 'W3C Example Table')" value="Export to Excel">
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-default pull-right" Text="Print" Style="margin-left: 10px;" OnClientClick="window.print();"></asp:Button>

                    <p class="hide_print">
                        [<span class="Scut">Alt+W</span> - Condensed & Detailed View]
                        <%--<span runat="server" id="spnAltB">[<span class="Scut">Alt+B</span> - Back View] </span><span runat="server" id="spnAltW">,[<span class="Scut">Alt+W</span> - Condensed & Detailed View] ,[<span class="Scut">Alt+Q</span> - Daily Breakup & Detailed View]</span>--%>
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
                            <div class="col-md-2" runat="server" id="divRegionalOffice">
                                <div class="form-group">
                                    <label>Circle Office</label><span style="color: red">*</span>
                                    <asp:DropDownList ID="ddlRegionalOffice" runat="server" ClientIDMode="Static" CssClass="form-control" OnSelectedIndexChanged="ddlRegionalOffice_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Office Name</label><span style="color: red">*</span>
                                    <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnSearchDetailed" runat="server" CssClass="btn btn-block btn-success Aselect1" Style="margin-top: 24px;" Text="Search Detailed" OnClick="btnSearchDetailed_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>

                    <div id="DivData">
                        <div id="DivTBMain" runat="server" class="DivTBMain"></div>
                        <div id="DivLedgerDetail" class="DivLedgerDetail"></div>
                        <div id="DivDayBook" class="DivDayBook"></div>
                    </div>
                </div>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function GetHeadData(Head_ID) {
            document.querySelector('.popup-wrapper').style.display = 'block';
            $.ajax({
                url: '<%=ResolveUrl("RptTrialBalanceDetailed.aspx/GetHeadDetail") %>',
                data: "{ 'HeadID':" + Head_ID + "}",
                //  var param = { ItemName: $('#txtItem').val() };
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $('#td' + Head_ID).html(data["d"]);
                    document.querySelector('.popup-wrapper').style.display = '';
                },
                error: function (response) {
                    document.querySelector('.popup-wrapper').style.display = '';
                },
                failure: function (response) {
                    document.querySelector('.popup-wrapper').style.display = '';
                }
            });

        }

        function HideFun(Head_ID) {

            $('#td' + Head_ID).html("");
        }

        function BackLedger() {
            $('#DivLedgerDetail').html("");
            $('.DivTBMain').show();
            $(".DivTBMain").css("visibility", "");
        }
        function GetLedgerDetail(LedgerID) {
            document.querySelector('.popup-wrapper').style.display = 'block';
            $('.DivTBMain').hide();
            $(".DivTBMain").css("visibility", "hidden");

            $.ajax({
                url: '<%=ResolveUrl("RptTrialBalanceDetailed.aspx/GetLedgerDetail") %>',
                data: "{ 'LedgerID':" + LedgerID + "}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $('#DivLedgerDetail').html(data["d"]);
                    document.querySelector('.popup-wrapper').style.display = '';
                },
                error: function (response) {
                    document.querySelector('.popup-wrapper').style.display = '';
                },
                failure: function (response) {
                    document.querySelector('.popup-wrapper').style.display = '';
                }
            });
        }

        function BackMonth() {
            $('#DivDayBook').html("");
            $('.DivLedgerDetail').show();
            $(".DivLedgerDetail").css("visibility", "");
        }
        function GetLedgerDayBook(LedgerID, MonthID, OpenBal) {
            document.querySelector('.popup-wrapper').style.display = 'block';
            $('.DivLedgerDetail').hide();
            $(".DivLedgerDetail").css("visibility", "hidden");

            $.ajax({
                url: '<%=ResolveUrl("RptTrialBalanceDetailed.aspx/GetLedgerDayBook") %>',
                data: "{ 'LedgerID':" + LedgerID + ",'MonthID':" + MonthID + ",'OpenBal':" + OpenBal + "}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $('#DivDayBook').html(data["d"]);
                    document.querySelector('.popup-wrapper').style.display = '';
                },
                error: function (response) {
                    document.querySelector('.popup-wrapper').style.display = '';
                },
                failure: function (response) {
                    document.querySelector('.popup-wrapper').style.display = '';
                }
            });
        }

        function PrintPage() {
            window.print();
        }


        //function PrintPage() {
        //    var printContent = document.getElementById('DivData');
        //    var windowUrl = 'about:blank';
        //    var windowName = 'PrintWindow';
        //    var printWindow = window.open(windowUrl, windowName,
        //          'left=50000,top=50000,width=0,height=0');
        //    printWindow.document.write(printContent.innerHTML);
        //    printWindow.document.close();
        //    printWindow.focus();
        //    printWindow.print();
        //    printWindow.close();
        //}
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
</asp:Content>






