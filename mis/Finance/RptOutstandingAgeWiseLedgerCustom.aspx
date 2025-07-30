<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptOutstandingAgeWiseLedgerCustom.aspx.cs" Inherits="mis_Finance_RptOutstandingAgeWiseLedgerCustom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .show_detail {
            margin-top: 24px;
        }

        .table > tbody > tr > th {
            padding: 5px;
        }

        a:hover {
            color: red;
        }

        /*tr:hover td {
            background-color: #fefefe !important;
        }*/
        table.dataTable tbody td, table.dataTable thead td {
            padding: 1px 1px !important;
        }

        table.dataTable tbody th, table.dataTable thead th {
            padding: 2px 3px !important;
        }

        table.dataTable thead th, table.dataTable thead td {
            padding: 5px 7px;
            border-bottom: none !important;
        }

        table.dataTable tfoot th, table.dataTable tfoot td {
            border-bottom: none !important;
        }

        table.dataTable.no-footer {
            border-bottom: none !important;
        }

        a.dt-button.buttons-collection.buttons-colvis, a.dt-button.buttons-collection.buttons-colvis:hover {
            background: #EF5350;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.dt-button.buttons-excel.buttons-html5, a.dt-button.buttons-excel.buttons-html5:hover {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.dt-button.buttons-print, a.dt-button.buttons-print:hover {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

        thead tr th {
            background: #9e9e9ea3 !important;
        }

        tbody tr td(:fifth-child), tfoot tr td(:fifth-child) {
            text-align: right !important;
        }

        table.dataTable tbody tr {
            background-color: #ddd4d1 !important;
        }

        .explode {
            padding: 4px;
            background: #8bc34a;
            border-radius: 50%;
            color: white;
            max-width: 17px;
        }

        .implode {
            padding: 4px;
            background: #ff9800;
            border-radius: 50%;
            color: white;
            display: none;
            max-width: 17px;
        }

        .edit_link, .edit_link:hover {
            color: #286090 !important;
        }

        .view_link, .view_link:hover {
            color: #00c0ef !important;
        }
    </style>
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

        .voucherColumn {
            width: 150px !important;
        }

        .btn-flat {
            background: #1ca79a;
            color: white;
            box-shadow: 1px 1px 1px #808080;
        }

        .mainbutton {
            background: #2b7770;
        }

        i.fa.fa-plus-circle.addItem {
            padding: 5px;
            background: #4CAF50;
            color: white;
            box-shadow: 2px 1px 2px #0000006e;
            margin-left: 10px;
            cursor: pointer;
            width: 23px;
        }

        i.fa.fa-minus-circle.deleteItem {
            padding: 5px;
            background: #F44336;
            color: white;
            box-shadow: 2px 1px 2px #0000006e;
            margin-left: 10px;
            cursor: pointer;
            width: 23px;
        }
    </style>
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->

        <section class="content">
            <asp:Label ID="lblTime" runat="server" CssClass="Dtime" Style="font-weight: 800;" Text="" ClientIDMode="Static"></asp:Label>
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Ledger Outstanding Age Wise</h3>
                    <p class="hide_print">
                        <span runat="server" id="spnAltB">[<span class="Scut">Alt+b</span> - Back View] </span><span runat="server" id="spnAltW">[<span class="Scut">Alt+w</span> - Condensed & Detailed View]</span>

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
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Office Name</label><span style="color: red">*</span>
                                    <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <br />
                                    <asp:CheckBox ID="chk0TxnOpen" runat="server" Text="0 Transaction Or Opening Display" />

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>List Of Group</label><span style="color: red">*</span>
                                    <asp:DropDownList runat="server" ID="ddlGroup" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>List Of Ledgers</label><span style="color: red">*</span>
                                    <asp:DropDownList runat="server" ID="ddlLedger" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlLedger_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Show Bill Type</label><span style="color: red">*</span>
                                    <asp:DropDownList ID="ddlBillType" runat="server" class="form-control select2">
                                        <asp:ListItem Value="AllBill">All Bills </asp:ListItem>
                                        <%--<asp:ListItem Value="AllBillZero" Selected="True" >All Bills Including Zero</asp:ListItem>--%>
                                        <asp:ListItem Value="Cr">CR </asp:ListItem>
                                        <%--<asp:ListItem Value="CrZero">CR Including Zero</asp:ListItem>--%>
                                        <asp:ListItem Value="Dr">DR </asp:ListItem>
                                        <%-- <asp:ListItem Value="DrZero">DR Including Zero</asp:ListItem>--%>
                                        <asp:ListItem Value="ClearedBill">Cleared Bill </asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="col-md-2">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success btn-flat" Style="margin-top: 24px;" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <asp:HiddenField ID="hdnAgeingList" runat="server" ClientIDMode="Static" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div runat="server" id="divExcel">
                                <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime"></asp:Label><br />
                                <input type="button" onclick="tableToExcel('tableData', 'W3C Example Table')" value="Export to Excel" />
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
                    <div id="tableData">
                        <div class="row">
                            <div class="col-md-12">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="8" style="text-align: center;">
                                            <asp:Label ID="lblheadingFirst" runat="server" CssClass="pageheading" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-12">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="8" style="text-align: center;">
                                            <strong style="height: 40px;">
                                                <asp:Label ID="lblHeadName" runat="server" Style="font-size: 20px;" Text=""></asp:Label>
                                               <br />
                                               
                                                <div class="hide_print">
                                                    <asp:Button ID="btnHeadExcel" runat="server" CssClass="btn btn-default" Text="Excel" OnClick="btnHeadExcel_Click" />
                                                </div>
                                            </strong>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-12">


                                <%--LEDGER DETAIL MONTH WISE--%>
                                <div id="DivTable" runat="server" class="table-responsive"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="AgstRefModal" role="dialog" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Company Ageing Alteration</h4>
                            <p style="color: blue"><b>Note:</b> नई Ageing जोड़ने के लिए  "+" चिन्ह पर  क्लिक करे एवं Ageing हटाने के लिए "-" चिन्ह पर क्लिक करे |</p>
                        </div>
                        <div class="modal-body">
                            <div class="row">

                                <asp:HiddenField ID="hdnCurrentRow" runat="server" Value="0" ClientIDMode="Static" />

                                <div class="col-md-12">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <p>
                                                <label>दिन से </label>
                                            </p>
                                            <input class="txtboxa_0" type="text" readonly="readonly" value="0" />
                                        </div>
                                        <div class="col-md-4">
                                            <p>
                                                <label>दिन के बीच</label>
                                            </p>
                                            <input class="txtboxb_0" autofocus="autofocus" type="text" />
                                        </div>
                                        <div class="col-md-4">
                                            <p>
                                                <label></label>
                                            </p>
                                            <i class="fa fa-plus-circle addItem addItem_0" onclick="AddInputText(0)"></i>
                                        </div>

                                    </div>

                                </div>
                                <div class="AdditionalRow" id="AdditionalRow">
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-block btn-success btn-flat" Text="Search" OnClick="btnSearch_Click" OnClientClick="return SaveAllValuesInHiddenField();" />
                                    </div>
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

            if (document.getElementById('<%=ddlGroup.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Group. \n";
            }
            if (document.getElementById('<%=ddlLedger.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Ledger. \n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                //document.querySelector('.popup-wrapper').style.display = 'block';
                //return true;
                $('#AgstRefModal').modal('show');
                return false;
            }

        }

        function PrintPage() {
            window.print();
        }


        function AddInputText(rowno) {
            //debugger;
            var newrowno = rowno + 1;
            var val1 = parseInt($(".txtboxa_" + rowno).val());
            var val2 = parseInt($(".txtboxb_" + rowno).val());
            //var hiddenvalue = $("#hdnAgeingList").val();
            //console.log(hiddenvalue);
            //$("#hdnAgeingList").val(hiddenvalue+","+val2);
            //console.log($("#hdnAgeingList").val());

            //console.log(typeof val1);
            //console.log(typeof val2);
            //console.log(rowno);
            //alert(val2);
            //console.log(val1 + "   " + val2 + "  " + isNaN(val2));
            if (isNaN(val2) || val2 == "" || val1 > val2) {
                alert("Please Enter Valid Number");
                $(".txtboxb_" + rowno).val("");
                //if (val2 == "") {
                //    console.log(" Val2 is blank")
                //}
                //if (val1 > val2) {
                //    console.log(val1 + "_ is greater than _" + val2)

                //}
                //console.log(" Error")
            } else {
                $("#AdditionalRow").append('<div class="col-md-12  NewRow_' + newrowno + '" style="margin-top:5px;"><div class="form-group"><div class="col-md-4"><input class="txtboxa_' + newrowno + '" type="text" value="' + (val2 + 1) + '" readonly="readonly" /></div><div class="col-md-4"><input class="txtboxb_' + newrowno + '" autofocus="autofocus" type="text" value="" /></div><div class="col-md-4"><i class="fa fa-plus-circle addItem addItem_' + newrowno + '" OnClick="AddInputText(' + newrowno + ')"></i><i class="fa fa-minus-circle deleteItem deleteItem_' + newrowno + '" OnClick="DeleteInputText(' + newrowno + ')"></i></div></div></div>');
                $(".addItem_" + rowno).css("display", "none");
                $(".deleteItem_" + rowno).css("display", "none");
                $(".txtboxb_" + rowno).attr('readonly', true);
                /********Set No. Of Inputs*********/
                $("#hdnCurrentRow").val(newrowno);

            }
        }

        function DeleteInputText(rowno) {
            //alert("Escape");
            var lastrowno = rowno - 1;
            //$(".NewRow_" + rowno).css("display", "none");
            $(".NewRow_" + rowno).remove();
            $(".addItem_" + lastrowno).css("display", "block");
            $(".deleteItem_" + lastrowno).css("display", "block");
            $(".txtboxb_" + lastrowno).val("");
            $(".txtboxb_" + lastrowno).attr('readonly', false);
            /********Set No. Of Inputs*********/
            $("#hdnCurrentRow").val(lastrowno);
        }

        function SaveAllValuesInHiddenField() {
            var totalInputFields = parseInt($("#hdnCurrentRow").val());
            var lastInputFieldB = parseInt($(".txtboxb_" + totalInputFields).val());
            var lastInputFieldA = parseInt($(".txtboxa_" + totalInputFields).val());
            var msg = "";
            var text = "";
            if (isNaN(lastInputFieldB) || lastInputFieldB == "" || lastInputFieldA > lastInputFieldB) {
                var msg = "Please Enter Valid Number";
            }
            //debugger;
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                var loop_i;
                for (loop_i = 0; loop_i <= totalInputFields; loop_i++) {
                    if (loop_i == 0) {
                        text += "" + $(".txtboxa_" + loop_i).val();
                        text += "," + $(".txtboxb_" + loop_i).val();
                    } else {
                        text += "," + $(".txtboxa_" + loop_i).val();
                        text += "," + $(".txtboxb_" + loop_i).val();
                    }

                }
                ///console.log(text);
                $("#hdnAgeingList").val(text);
                //alert(text);

                //debugger;
                return true;
            }

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

    <script>
        $('.datatable').DataTable({
            paging: false,
            dom: 'Bfrtip',
            ordering: false,
            oSearch: { bSmart: false, bRegex: true },
            buttons: [
                {
                    extend: 'colvis',
                    collectionLayout: 'fixed two-column',
                    text: '<i class="fa fa-eye"></i> Columns'
                },
                {
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true,
                    //autoPrint: true
                }
                //,
                //{
                //    extend: 'excel',
                //    text: '<i class="fa fa-file-excel-o"></i> Excel',
                //    title: $('h3').text(),
                //    exportOptions: {
                //        columns: [0, 1, 2, 3, 4, 5]
                //    },
                //    footer: true
                //}

            ]
        });
    </script>
    <script src="js/fromkeycode.js" type="text/javascript"></script>
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
                if ($('.GroupChildren').is(':visible')) {
                    $(".GroupChildren").css("display", "none");
                    $(".explode").css("display", "block");
                    $(".implode").css("display", "none");
                }
                else {
                    $(".GroupChildren").css("display", "table-row");
                    $(".explode").css("display", "none");
                    $(".implode").css("display", "block");
                }
            }
            return true;
        }

        document.onkeydown = handleKeyDown;




        //$(document).ready(function () {
        //    $('table thead th').each(function (i) {
        //        calculateColumn(i);
        //    });
        //});

        //function calculateColumn(index) {
        //    var total = 0;
        //    $('table tr').each(function () {
        //        var value = parseInt($('td', this).eq(index).text());
        //        if (!isNaN(value)) {
        //            total += value;
        //        }
        //    });
        //    $('table tfoot td').eq(index).text('Total: ' + total);
        //}

        $(document).on("click", ".explode", function () {
            //$(this).find("tr.GroupChildren").remove();
            //console.log(this);
            //console.log($(this).parent());
            // if ($(this).parent().parent().find('.GroupChildren').is(':visible')) {
            //$(this).parent().parent().find(".GroupChildren").css("display", "none");
            // }
            //else {
            $(this).parent().parent().find(".GroupChildren").css("display", "table-row");
            $(this).parent().find(".explode").css("display", "none");
            $(this).parent().find(".implode").css("display", "block");
            // }

        });

        $(document).on("click", ".implode", function () {
            //if ($(this).parent().parent().find('.GroupChildren').is(':visible')) {
            $(this).parent().parent().find(".GroupChildren").css("display", "none");
            $(this).parent().find(".explode").css("display", "block");
            $(this).parent().find(".implode").css("display", "none");
            // }
            // else {
            //  $(this).parent().parent().find(".GroupChildren").css("display", "table-row");
            // }

        });

    </script>

</asp:Content>

