<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptStatistics.aspx.cs" Inherits="mis_Finance_RptStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .show_detail {
            margin-top: 24px;
        }

        .Dtime {
            display: none;
        }

        @media print {

            .hide_print, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
            }
            .noprint
            {
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <div class="content-header">
            <h1>Statistics
                   <small></small>
            </h1>
        </div>
        <section class="content">
            <asp:Label ID="lblTime" runat="server" CssClass="Dtime" Style="font-weight: 800;" Text="" ClientIDMode="Static"></asp:Label>
            <div class="box box-pramod" style="background-color: #FFFFFF;">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="box-body">
                                <legend class="hide_print">Statistics</legend>
                                <div class="row hide_print">
                                    <div class="col-md-3">
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
                                    <div class="col-md-3">
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
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Office Name</label><span style="color: red">*</span>
                                            <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button ID="btn" CssClass="btn btn-md btn-primary show_detail Aselect1" runat="server" Text="Show Statistics" OnClick="btn_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row hide_print">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime"></asp:Label>
                                    </div>
                                </div>
                                <div class="row hide_print">
                                    <div class="col-md-12">
                                        <div runat="server" id="divExcel">
                                            <input type="button" onclick="tableToExcel('PrintList', 'W3C Example Table')" value="Export to Excel" />
                                            <input type="button" onclick="PrintList()" value="Print" />
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


                                <div class="row" id="PrintList">
                                    <div class="col-md-12">

                                        <table style="width: 100%;">
                                            <tr>
                                                <td colspan="5" style="text-align: center;">
                                                    <asp:Label ID="lblheadingFirst" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-md-12">
                                        <fieldset class="box-body BoxData">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 50%; vertical-align: top;">
                                                        <asp:GridView ID="GridView2" runat="server" ShowFooter="true" AutoGenerateColumns="false" class="table table-hover table-bordered" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton1" CausesValidation="false" CommandName="Select" Text='<%# Eval("VoucherTx_Type") %>' runat="server" CssClass="Aselect1">LinkButton</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--<asp:ButtonField ButtonType="Link" CommandName="View" HeaderText="Type OF Vouchers" DataTextField="VoucherTx_Type" />--%>
                                                                <asp:BoundField DataField="TotalVouchers" HeaderText="Total Vouchers" />
                                                            </Columns>
                                                        </asp:GridView>
                                                    </td>
                                                    <td style="width: 50%; vertical-align: top;">
                                                        <table class="table  table-bordered">
                                                            <thead>
                                                                <tr>
                                                                    <th>Type Of Accounts</th>
                                                                    <th></th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <tr>
                                                                    <td>Groups</td>
                                                                    <td>
                                                                        <asp:Label ID="lblGroups" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Ledgers</td>
                                                                    <td>
                                                                        <asp:Label ID="lblLedgers" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Stock Groups</td>
                                                                    <td>
                                                                        <asp:Label ID="lblStockGroups" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Stock Items</td>
                                                                    <td>
                                                                        <asp:Label ID="lblStockIems" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Voucher Types</td>
                                                                    <td>
                                                                        <asp:Label ID="lblVoucherType" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Units</td>
                                                                    <td>
                                                                        <asp:Label ID="lblUnits" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Currencies</td>
                                                                    <td>
                                                                        <asp:Label ID="lblcurrencies" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td>Cost Centre Category</td>
                                                                    <td>
                                                                        <asp:Label ID="lblCostCentreCategory" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                 <tr>
                                                                    <td>Cost Centre Sub Category</td>
                                                                    <td>
                                                                        <asp:Label ID="lblCostCentreSubCategory" runat="server" Text=""></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr></tr>
                                            </table>

                                        </fieldset>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <input type="button" onclick="tableToExcel('PrintData', 'W3C Example Table')" value="Export to Excel" />
                                        <input type="button" onclick="PrintData()" value="Print" />
                                    </div>
                                </div>

                                <div id="PrintData">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td colspan="5" style="text-align: center;">
                                                        <asp:Label ID="lblheadingSec" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table datatable table-hover table-bordered" OnRowCommand="GridView1_RowCommand">
                                                <Columns>
                                                    <asp:BoundField DataField="VoucherTx_Date" HeaderText="VchDate" />
                                                    <asp:BoundField DataField="Particular" HeaderText="Particulars" />
                                                    <asp:BoundField DataField="VoucherTx_Type" HeaderText="VchType" />
                                                    <asp:BoundField DataField="VoucherTx_No" HeaderText="VchNo" />
                                                    <asp:BoundField DataField="Office_Name" HeaderText="Office Name" />
                                                    <asp:BoundField DataField="DebitAmount" HeaderText="Debit (Amount)" />
                                                    <asp:BoundField DataField="CreditAmount" HeaderText="Credit (Amount)" />
                                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="hpView" runat="server" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);" CssClass="label label-info">View</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <%-- start data table--%>
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
        $('.datatable').DataTable({
            paging: false,
            dom: 'Bfrtip',
            ordering: false,
            buttons: [
                {
                    extend: 'colvis',
                    collectionLayout: 'fixed two-column',
                    text: '<i class="fa fa-eye"></i> Columns'
                }
                //,
                //{
                //    extend: 'print',
                //    text: '<i class="fa fa-print"></i> Print',
                //    title: $('h1').text(),
                //    footer: true,
                //    autoPrint: true
                //},
                //{
                //    extend: 'excel',
                //    text: '<i class="fa fa-file-excel-o"></i> Excel',
                //    title: $('h1').text(),
                //    exportOptions: {
                //        columns: [0, 1, 2, 3, 4, 5]
                //    },
                //    footer: true
                //}

            ]
        });
    </script>
    <%-- <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
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
    <script>
        $('.datatable').DataTable({
            paging: true,
            columnDefs: [{
                targets: 'no-sort',
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
                //buttons: [{
                //    extend: 'print',
                //    text: '<i class="fa fa-print"></i> Print',
                //    title: $('h1').text(),
                //    exportOptions: {
                //        columns: [0, 1, 2, 3, 4, 5, 6]
                //    },
                //    footer: true,
                //    autoPrint: true
                //}, {
                //    extend: 'excel',
                //    text: '<i class="fa fa-file-excel-o"></i> Excel',
                //    title: $('h1').text(),
                //    exportOptions: {
                //        columns: [0, 1, 2, 3, 4, 5, 6]
                //    },
                //    footer: true
                //}],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });--%>


    <script>
        //end data table
        //$('#txtFromDate').change(function () {
        //    //debugger;
        //    var start = $('#txtFromDate').datepicker('getDate');
        //    var end = $('#txtToDate').datepicker('getDate');
        //    if ($('#txtToDate').val() != "") {
        //        if (start > end) {
        //            if ($('#txtFromDate').val() != "") {
        //                alert("From date should not be greater than To Date.");
        //                $('#txtFromDate').val("");
        //            }
        //        }
        //    }
        //});
        //$('#txtToDate').change(function () {
        //    //debugger;
        //    var start = $('#txtFromDate').datepicker('getDate');
        //    var end = $('#txtToDate').datepicker('getDate');
        //    if (start > end) {
        //        if ($('#txtToDate').val() != "") {
        //            alert("To Date can not be less than From Date.");
        //            $('#txtToDate').val("");
        //        }
        //    }

        //});

        //function PrintList() {
        //    var printContents = document.getElementById('PrintList').innerHTML;
        //    var originalContents = document.body.innerHTML;

        //    document.body.innerHTML = printContents;

        //    window.print();

        //    //document.body.innerHTML = originalContents;
        //}
        //function PrintData() {
        //    var printContents = document.getElementById('PrintData').innerHTML;
        //    var originalContents = document.body.innerHTML;

        //    document.body.innerHTML = printContents;

        //    window.print();

        //   // document.body.innerHTML = originalContents;
        //}


        function PrintList() {
            var printContent = document.getElementById('PrintList');
            var windowUrl = 'about:blank';
            var windowName = 'PrintWindow';
            var printWindow = window.open(windowUrl, windowName,
                  'left=50000,top=50000,width=0,height=0');
            printWindow.document.write(printContent.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        }
        function PrintData() {
            var printContent = document.getElementById('PrintData');
            var windowUrl = 'about:blank';
            var windowName = 'PrintWindow';
            var printWindow = window.open(windowUrl, windowName,
                  'left=50000,top=50000,width=0,height=0');
            printWindow.document.write(printContent.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        }
    </script>
</asp:Content>
