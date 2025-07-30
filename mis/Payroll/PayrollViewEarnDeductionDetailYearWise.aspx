<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollViewEarnDeductionDetailYearWise.aspx.cs" Inherits="mis_Payroll_PayrollEarnDeductionDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .Grid td {
            padding: 3px !important;
        }

            .Grid td input {
                padding: 5px 3px !important;
                text-align: right;
                font-size: 12px;
            }

        .Grid th {
            text-align: center;
        }

        .ss {
            text-align: left !important;
        }

        .form-control[disabled] {
            background: #8fbc8f94;
        }

        .Grid td input {
            padding: 0px 0px !important;
            text-align: right;
            font-size: 14px;
            color: #828282;
            font-weight: 600;
            font-family: inherit;
        }

        .Grid td {
            font-weight: 600;
            font-family: inherit;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #F44336;
            color: white;
            margin: 10px;
            box-shadow: 0px 0px 3px #98989861;
        }

        a.btn.btn-default.buttons-print {
            background: #0b87c6;
            color: white;
            margin: 10px;
            box-shadow: 0px 0px 3px #98989861;
        }

        .datatable table {
            font-size: 10px;
        }

            .datatable table thead th {
                max-width: 50px !important;
            }

        td,th {
            padding: 3px !important;
            margin: 0px !important;
            font-size: 12px !important;
            border-top: none !important;
            border-right: none !important;
            max-width:50px !important;
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
                    <h3 class="box-title">Financial Year Wise Complete Earning & Deduction Details</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control select2" AutoPostBack="True" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Employee<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Financial Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform1();" />
                            </div>
                        </div>
                    </div>

                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table datatable">
                                <Columns>
                                    <asp:BoundField DataField="EarnDeduction_Name" HeaderText="Head Name" />
                                    <asp:BoundField DataField="ArrearSum" HeaderText="Total Arrear" />
                                    <asp:BoundField DataField="Apr_Amount" HeaderText="April" />
                                    <asp:BoundField DataField="May_Amount" HeaderText="May" />
                                    <asp:BoundField DataField="Jun_Amount" HeaderText="June" />
                                    <asp:BoundField DataField="Jul_Amount" HeaderText="July" />
                                    <asp:BoundField DataField="Aug_Amount" HeaderText="August" />
                                    <asp:BoundField DataField="Sep_Amount" HeaderText="September" />
                                    <asp:BoundField DataField="Oct_Amount" HeaderText="October" />
                                    <asp:BoundField DataField="Nov_Amount" HeaderText="November" />
                                    <asp:BoundField DataField="Dec_Amount" HeaderText="December" />
                                    <asp:BoundField DataField="Jan_Amount" HeaderText="January" />
                                    <asp:BoundField DataField="Feb_Amount" HeaderText="February" />
                                    <asp:BoundField DataField="Mar_Amount" HeaderText="March" />
                                    <asp:BoundField DataField="TotalAmount" HeaderText="Total" />
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
    <script type="text/javascript">
        function validateform1() {
            var msg = "";
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Year. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;

            }
        }
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
                if (confirm("Do you really want to Save Details ?")) {
                    return true;
                }
                else {
                    return false;
                }

            }
        }

        $('.datatable').DataTable({
            paging: false,
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
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13,14]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13,14]
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

    </script>
</asp:Content>

