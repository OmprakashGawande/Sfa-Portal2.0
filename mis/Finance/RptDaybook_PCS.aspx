<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptDaybook_PCS.aspx.cs" Inherits="mis_Finance_RptDaybook_PCS" %>

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

        .align-right {
            text-align: right !important;
        }

        .Scut {
            color: tomato;
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
                    <h3 class="box-title">Day Book (Single Date)</h3>
                    <asp:HyperLink ID="btnDtlDayBook" NavigateUrl="RptDetailedDaybook_PCS.aspx" Text="Detailed DayBook" Target="_blank" runat="server" CssClass="btn btn-primary pull-right"></asp:HyperLink><asp:Button ID="btngraphical" runat="server" Style="margin-right: 10px;" CssClass="btn btn-primary pull-right hidden" Text="Graphical Report" OnClick="btngraphical_Click"></asp:Button>
                    <p>
                        <span>[<span class="Scut">Alt <span style="font-size: 17px; font-weight: 700;">-</span> </span>: Previous Date],[<span class="Scut">Alt <span style="font-size: 17px; font-weight: 700;">+</span> </span>: Next Date]
                        </span>
                    </p>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label><span style="color: red">*</span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>

                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" onChange="Search()"></asp:TextBox>
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
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" onChange="Search()">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <asp:Button ID="btnSearch" runat="server" CssClass="hidden Aselect1" Text="Search" OnClick="btnSearch_Click" ClientIDMode="Static" />
                        <asp:Button ID="btnSearchPrv" runat="server" CssClass="hidden Aselect1" Text="Search" OnClick="btnSearchPrv_Click" ClientIDMode="Static" AccessKey="-" />
                        <asp:Button ID="btnSearchNext" runat="server" CssClass="hidden Aselect1" Text="Search" OnClick="btnSearchNext_Click" ClientIDMode="Static" AccessKey="+" />
                    </div>

                    <div class="row">
                        <%--<div class="col-md-12">
                            <asp:Label ID="lblheadingFirst" CssClass="lblheadingFirst" runat="server" Text=""></asp:Label>
                        </div>--%>
                        <div class="col-md-12">
                            <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="hide_print" id="hide_print_main" runat="server">
                                <br />
                                <p>
                                    <a id="dlink" style="display: none;"></a>
                                    <%--<asp:Button runat="server" Text="Print Main Report" class="btn btn-flat btn-success" OnClientClick="window.print();return false;" />--%>
                                    <asp:Button runat="server" CssClass="hidden" Text="Export Main Report" OnClientClick="tableToExcel('testTable', 'Day Book','Day Book')" ID="myButtonControlID" class="btn btn-flat btn-success" />
                                </p>
                            </div>

                            <div runat="server" id="divExcel">
                                <input type="button" class="btn btn-info" onclick="tableToExcel('testTable')" value="Export to Excel">
                            </div>
                            <script type="text/javascript">
                                var tableToExcel = (function () {
                                    var uri = 'data:application/vnd.ms-excel;base64,'
                                      , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><style>.hideCss{display : none;}</style><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
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
                        <div id="testTable">
                            <div class="col-md-12">

                                <div class="col-md-12">
                                    <table style="width:100%;">
                                        <tr>
                                            <td colspan="7" style=" text-align:center;">
                                                <asp:Label ID="lblheadingFirst" CssClass="lblheadingFirst" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <asp:GridView ID="GridView1" DataKeyNames="VoucherTx_ID" runat="server"  AutoGenerateColumns="false" class="datatable table table-hover table-bordered" ShowHeaderWhenEmpty="true" OnRowDeleting="GridView1_RowDeleting" EmptyDataText="No Record Found" OnRowCommand="GridView1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Voucher Date." ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_Date" Text='<%# Eval("VoucherTx_Date").ToString() %>' runat="server" />
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
                                        <asp:TemplateField HeaderText="Office Name.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffice_Name" Text='<%# Eval("Office_Name").ToString() %>' runat="server" />
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
                                                <asp:LinkButton ID="hpView" runat="server" CssClass="hideCss label label-info" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View" OnClientClick="window.document.forms[0].target = '_blank';"></asp:LinkButton>
                                                <asp:LinkButton ID="hpEdit" runat="server" CssClass="label label-primary" CommandName="Editing" CommandArgument='<%# Eval("VoucherTx_ID") %>' Text="Edit" OnClientClick="window.document.forms[0].target = '_blank'; "></asp:LinkButton>
                                                <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');" CssClass="label label-danger"></asp:LinkButton>
                                                <asp:LinkButton ID="hpprint" CssClass="label label-primary" runat="server" Text="Print" CommandName="Print" CommandArgument='<%# Eval("VoucherTx_ID") %>' OnClientClick="window.document.forms[0].target = '_blank';"></asp:LinkButton>
                                                <asp:Label ID="lblOfficeID" CssClass="hidden" Text='<%# Eval("Office_ID").ToString() %>' runat="server" />
                                                <asp:Label ID="lblV_Editright" CssClass="hidden" Text='<%# Eval("V_Editright").ToString() %>' runat="server" />

                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
			"bSort": false,
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('.lblheadingFirst').html(),
					 customize: function(win) {
                        $(win.document.body).append('<table width="100%" style="margin-top:70px;"><tr><td style="text-align:center"><b>Cashier<br/>Signature</b></td><td style="text-align:center"><b>Asst.Grade 1<br/>(Signature)</b></td><td style="text-align:center"><b>Deputy Manager(Account)<br/>(Signature)</b></td><td style="text-align:center"><b>Manager(Accounts )<br/>(Signature)</b></td></tr></table>'); //after the table
                        //$(win.document.body).prepend('<html elements here>'); //before the table
                    },
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }
                //, {
                //    extend: 'excel',
                //    text: '<i class="fa fa-file-excel-o"></i> Excel',
                //    title: $('.lblheadingFirst').text(),
                //    exportOptions: {
                //        columns: [0, 1, 2, 3, 4, 5, 6]
                //    },
                //    footer: true
                //}
                ]
                ,
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
        //  <script>

        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }

        }
        function PrintPage() {
            window.print();
        }
        function Search() {
            //alert(1);
            document.getElementById('<%=btnSearch.ClientID%>').click();
        }
    </script>

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

                // $(x).find('td:nth-child(2), th:nth-child(2)', 'table:eq(0) tr').each.find("td:eq(7), th:eq(7)").remove();

                $(x).find(".dataTables_filter,.dataTables_length,.dt-buttons,.dataTables_info,.dataTables_paginate").replaceWith(function () {
                    return '';
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







