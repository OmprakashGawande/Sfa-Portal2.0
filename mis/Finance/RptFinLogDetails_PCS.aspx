<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptFinLogDetails_PCS.aspx.cs" Inherits="mis_Finance_RptFinLogDetails_PCS" %>

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

            .lblheadingFirst p {
                text-align: center !important;
                font-size: 10px !important;
            }

            .Dtime {
                display: block;
            }
        }

        .cssweek {
            background-color: lavender !important;
            color: Black !important;
        }

        .cssStrong {
            background-color: yellowgreen !important;
            color: Black !important;
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
                    <div style="float: left;">
                        <h3 class="box-title">Office Wise Last Voucher Entry Date</h3>
                    </div>
                    <div style="float: right; font-weight: 700;">
                        <asp:Label ID="lblcssStrong" class="cssStrong" Visible="false" Style="border: 1px solid black; padding: 5px;" runat="server" Text=""></asp:Label>
                         <asp:Label ID="lblcssweek" class="cssweek" Visible="false" Style="border: 1px solid black; padding: 5px;" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="box-body">

                    <div class="row">
                        <div class="col-md-12">
                            <div onclick="exportThis()" class="alert-success" style="cursor: pointer; border: 1px solid #ccc; text-align: center; width: 19%;">Export to Excel</div>
                            <table class="table table-hover table-bordered datatable" border="1" id="GridView1" style="border-collapse: collapse;">
                                <thead>
                                    <tr>
                                        <th>SNo.</th>
                                        <th>Office Name</th>
                                        <th>Last Voucher Date</th>
										<th>Total Voucher Count</th>
                                    </tr>
                                </thead>

                                <tbody>
                                    <asp:Repeater ID="Repeater1" runat="server">

                                        <ItemTemplate>
                                            <tr>
                                                <td <%#  Eval("cssclass").ToString() %> style="width: 5px;"><%# Container.ItemIndex + 1 %>
                                                </td>
                                                <td <%#  Eval("cssclass").ToString() %>><%#  Eval("Office Name").ToString() %>
                                                </td>
                                                <td <%#  Eval("cssclass").ToString() %>><%#  Eval("Last Voucher Date").ToString() %>
                                                </td>
												 <td <%#  Eval("cssclass").ToString() %>><%#  Eval("Total Voucher Count").ToString() %>
                                                </td>

                                            </tr>
                                        </ItemTemplate>

                                    </asp:Repeater>


                                </tbody>
                            </table>

                        </div>
                    </div>

                </div>

            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
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
    <script src="//cdn.datatables.net/plug-ins/1.10.11/sorting/date-eu.js" type="text/javascript"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
            pageLength: 100,
            columnDefs: [{
                targets: 2,
                type: "date-eu",
                orderable: true

            }],

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
                    title: $('.lblheadingFirst').html(),
                    exportOptions: {
                        columns: [0, 1, 2]
                    },
                    footer: true,
                    autoPrint: true
                },
		/* {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.lblheadingFirst').text(),
                    exportOptions: {
                        columns: [0, 1, 2]
                    },
                    footer: true
                }*/
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


        function PrintPage() {
            window.print();
        }
    </script>
    <script type="text/javascript">
        var exportThis = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,',
                template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel"  xmlns="http://www.w3.org/TR/REC-html40"><head> <!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets> <x:ExcelWorksheet><x:Name>{worksheet}</x:Name> <x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions> </x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook> </xml><![endif]--></head><body> <table>{table}</table></body></html>',
                base64 = function (s) {
                    return window.btoa(unescape(encodeURIComponent(s)))
                },
                format = function (s, c) {
                    return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; })
                }
            return function () {
                var ctx = { worksheet: 'Multi Level Export Table Example' || 'Worksheet', table: document.getElementById("GridView1").innerHTML }
                window.location.href = uri + base64(format(template, ctx))
            }
        })()
        var exportThisWithParameter = (function () {
            var uri = 'data:application/vnd.ms-excel;base64,',
                template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel"  xmlns="http://www.w3.org/TR/REC-html40"><head> <!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets> <x:ExcelWorksheet><x:Name>{worksheet}</x:Name> <x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions> </x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook> </xml><![endif]--></head><body> <table>{table}</table></body></html>',
                base64 = function (s) {
                    return window.btoa(unescape(encodeURIComponent(s)))
                },
                format = function (s, c) {
                    return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; })
                }
            return function (tableID, excelName) {
                tableID = document.getElementById(tableID)
                var ctx = { worksheet: excelName || 'Worksheet', table: tableID.innerHTML }
                window.location.href = uri + base64(format(template, ctx))
            }
        })()
    </script>
</asp:Content>









