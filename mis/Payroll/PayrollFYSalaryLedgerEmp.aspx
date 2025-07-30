<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollFYSalaryLedgerEmp.aspx.cs" Inherits="mis_Payroll_PayrollFYSalaryLedgerEmp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <link href="../finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../finance/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../finance/css/jquery.dataTables.min.css" rel="stylesheet" />
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
            padding: 5px 5px !important;
        }

        table.dataTable tbody th, table.dataTable thead th {
            padding: 8px 10px !important;
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

        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        tbody tr td:not(:first-child), tfoot tr td:not(:first-child) {
            text-align: right !important;
        }

        .Earning, .Deduction {
            font-weight: 600;
        }

        @media print {
            .print_hidden {
                display: none !important;
            }

            .table-responsive {
                overflow: visible;
            }

            #myInput, .main-footer {
                display: none;
            }

            select {
                border: none !important;
            }

            @page {
                size: landscape;
            }

            .table > tbody > tr > td {
                vertical-align: middle;
                padding: 1px !important;
                font-size: 8px;
                font-family: Verdana;
            }

            select {
                /* for Firefox */
                -moz-appearance: none;
                /* for Chrome */
                -webkit-appearance: none;
            }

                /* For IE10 */
                select::-ms-expand {
                    display: none;
                }

            .datatable > tbody > tr > th {
                padding: 1px !important;
                font-size: 8px;
                font-family: Verdana;
                border: 1px solid #ddd !important;
            }

            table td:nth-child(2), table th:nth-child(2) {
                display: none;
            }

            #GridView1 {
                border: none;
            }

            .datatable > tbody > tr > th {
                border: 1px solid #ddd !important;
            }

            #GridView1 tr {
                border: 1px solid #ddd !important;
            }

            .datatable {
                overflow: visible;
            }

            .box-body, .box, tbody {
                height: auto !important;
            }

                .box.box-success {
                    border: none;
                }

            .table-bordered > tbody > tr > th {
                z-index: initial !important;
                top: initial !important;
                background: initial !important;
                position: initial !important;
            }

            .headingOfDept {
                display: block !important;
            }

            .salary-logo {
                -webkit-filter: grayscale(100%);
                filter: grayscale(100%);
                width: 40px;
            }

            th {
                width: 30px;
                word-break: break-word;
            }

            td {
                color: black;
                font-weight: 400;
            }

            .dataTables_filter, .dt-buttons {
                display: none;
            }

            .lblEmpDetail {
                font-size: 14px !important;
            }

            .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
                line-height: 1 !important;
                font-family: verdana;
            }

            .Earning, .Deduction {
                font-size: 8px !important;
            }

            thead th.sorting_disabled {
                height: 30px !important;
                overflow: hidden;
                font-size: 8.5px !important;
            }

            .dataTables_info {
                display: none;
            }

            th,td {
                -webkit-print-color-adjust: exact !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title print_hidden">My Salary Ledger</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row print_hidden">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control select2" AutoPostBack="True" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Employee<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform1();" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group"></div>


                    <div class="row">
                        <div class="col-md-12 table-responsive">
                            <asp:Label ID="lblEmpDetail" class="lblEmpDetail" runat="server" Text="" Visible="true" Style="color: black; font-size: 17px;"></asp:Label>
                            <p class="hidden-print" style="color: blue"><b>नोट: </b>प्रिंट करने के लिए कृपया Ctrl+P का उपयोग करें |</p>
                            <asp:GridView ID="GridView1" DataKeyNames="EarnDeduction_ID" runat="server" AutoGenerateColumns="False" class="datatable table table-hover table-bordered pagination-ys">
                                <Columns>
                                    <%--<asp:BoundField DataField="EarnDeduction_Name" HeaderText="Head" />--%>
                                    <asp:TemplateField HeaderText="Head" ItemStyle-CssClass="alignR">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetsalary" CssClass='<%# Eval("EarnDeduction_Type")%>' runat="server" Text='<%# Eval("EarnDeduction_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EarnDeduction_Type" HeaderText="Earning & Deduction" />

                                    <asp:BoundField DataField="Total" HeaderText="Total In FY" />

                                    <asp:BoundField DataField="April" HeaderText="April" />
                                    <asp:BoundField DataField="AprilArr" HeaderText="April Arrear" />

                                    <asp:BoundField DataField="May" HeaderText="May" />
                                    <asp:BoundField DataField="MayArr" HeaderText="May Arrear" />

                                    <asp:BoundField DataField="June" HeaderText="June" />
                                    <asp:BoundField DataField="JuneArr" HeaderText="June Arrear" />

                                    <asp:BoundField DataField="July" HeaderText="July" />
                                    <asp:BoundField DataField="JulyArr" HeaderText="July Arrear" />

                                    <asp:BoundField DataField="August" HeaderText="August" />
                                    <asp:BoundField DataField="AugustArr" HeaderText="August Arrear" />

                                    <asp:BoundField DataField="September" HeaderText="September" />
                                    <asp:BoundField DataField="SeptemberArr" HeaderText="September Arrear" />

                                    <asp:BoundField DataField="October" HeaderText="October" />
                                    <asp:BoundField DataField="OctoberArr" HeaderText="October Arrear" />

                                    <asp:BoundField DataField="November" HeaderText="November" />
                                    <asp:BoundField DataField="NovemberArr" HeaderText="November Arrear" />

                                    <asp:BoundField DataField="December" HeaderText="December" />
                                    <asp:BoundField DataField="DecemberArr" HeaderText="December Arrear" />

                                    <asp:BoundField DataField="January" HeaderText="January" />
                                    <asp:BoundField DataField="JanuaryArr" HeaderText="January Arrear" />

                                    <asp:BoundField DataField="February" HeaderText="February" />
                                    <asp:BoundField DataField="FebruaryArr" HeaderText="February Arrear" />

                                    <asp:BoundField DataField="March" HeaderText="March" />
                                    <asp:BoundField DataField="MarchArr" HeaderText="March Arrear" />


                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>



                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">


        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Financial Year. \n";
            }
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to see details ?")) {
                    return true;
                }
                else {
                    return false;
                }

            }
        }

    </script>
    <script src="../finance/js/jquery.dataTables.min.js"></script>
    <script src="../finance/js/jquery.dataTables.min.js"></script>
    <script src="../finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../finance/js/dataTables.buttons.min.js"></script>
    <script src="../finance/js/buttons.flash.min.js"></script>
    <script src="../finance/js/jszip.min.js"></script>
    <script src="../finance/js/pdfmake.min.js"></script>
    <script src="../finance/js/vfs_fonts.js"></script>
    <script src="../finance/js/buttons.html5.min.js"></script>
    <script src="../finance/js/buttons.print.min.js"></script>
    <script src="../finance/js/buttons.colVis.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: false,
            ordering: false,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<""tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('.lblEmpDetail').text(),
                    exportOptions: {
                        //columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.lblEmpDetail').text(),
                    exportOptions: {
                        //columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
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
            }
        });


        //$(".Earning").parent().parent().css("color", "green");
        //$(".Deduction").parent().parent().css("color", "red");
        $(".Deduction,.Earning").css("color", "black");
        $(".Deduction,.Earning").parent().css("background", "#eaeaea");

        $('td:contains("NET SALARY")').parent().find('td').css('background-color', '#99add2');
        $('td:contains("TOTAL DEDUCTION")').parent().find('td').css('background-color', '#ffccc8');
        $('td:contains("TOTAL EARNING")').parent().find('td').css('background-color', '#e4fbca');
        
    </script>
</asp:Content>


