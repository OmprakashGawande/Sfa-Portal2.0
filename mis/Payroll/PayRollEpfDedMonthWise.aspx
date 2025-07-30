<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayRollEpfDedMonthWise.aspx.cs" Inherits="mis_Payroll_PayRollPayBillMonth_Wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
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

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }

        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">

                            <div class="row">
                                <div class="col-md-8">
                                    <h3 class="box-title" id="Label1">Monthly EPF REPORT</h3>
                                </div>
<%--                                <div class="col-md-2" id="DivTextFileExport" runat="server">
                                    <asp:Button runat="server" CssClass="btn btn-block btn-default" Text="Text File Export" ID="btnTextFileExport" OnClick="btnTextFileExport_Click" OnClientClick="return validateform();" />
                                </div>--%>
                                <div class="col-md-2" id="Div1" runat="server">
                                    <asp:Button runat="server" CssClass="btn btn-block btn-default" Text="Text File Export New" ID="btnTextFileExportNew" OnClick="btnTextFileExportNew_Click" OnClientClick="return validateform();" />
                                </div>
                            </div>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label><span style="color: red">*</span>
                                        <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control" ClientIDMode="Static">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlOffice" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Year <span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlFinancialYear" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Month <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                            <asp:ListItem Value="0">Select Month</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlMonth" class="text-danger"></span></small>
                                    </div>
                                </div>
                               <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Type of Post (पद प्रकार)</label>
                                      <%--  <asp:DropDownList ID="ddlEmp_TypeOfPost" runat="server" class="form-control">
                                            <asp:ListItem Value="All">All</asp:ListItem>
                                            <asp:ListItem Value="Permanent">Regular/Permanent</asp:ListItem>
                                            <asp:ListItem Value="Fixed Employee">Fixed Employee(स्थाई कर्मी)</asp:ListItem>
                                            <asp:ListItem Value="Contigent Employee">Contigent Employee</asp:ListItem>
                                            <asp:ListItem Value="Samvida Employee">Samvida Employee</asp:ListItem>
                                            <asp:ListItem Value="Theka Shramik">Theka Shramik</asp:ListItem>
                                            <asp:ListItem Value="Outsource Employee">Outsource Employee</asp:ListItem>
                                            <asp:ListItem Value="Other Employee">Other Employee</asp:ListItem>
											<%--<asp:ListItem Value="Deputation Employee">Deputation Employee</asp:ListItem>
                                        </asp:DropDownList>---%>
                                         <asp:ListBox ID="ddlEmp_TypeOfPost" ClientIDMode="Static" 
                                             SelectionMode="Multiple" runat="server" class="form-control">
                                
                                    <asp:ListItem Value="Permanent">Regular/Permanent</asp:ListItem>
                                    <asp:ListItem Value="Fixed Employee">Fixed Employee(स्थाई कर्मी)</asp:ListItem>
                                    <asp:ListItem Value="Contigent Employee">Contigent Employee</asp:ListItem>
                                    <asp:ListItem Value="Samvida Employee">Samvida Employee</asp:ListItem>
                                    <asp:ListItem Value="Theka Shramik">Theka Shramik</asp:ListItem>
                                    <asp:ListItem Value="Outsource Employee">Outsource Employee</asp:ListItem>
									<asp:ListItem Value="Other Employee">Other Employee</asp:ListItem>
									<asp:ListItem Value="Deputation Employee">Deputation Employee</asp:ListItem>
                                    <asp:ListItem Value="Contractual Employee">Contractual Employee</asp:ListItem>
                                    <asp:ListItem Value="Daily Wages Employee">Daily Wages Employee</asp:ListItem>
                                    <asp:ListItem Value="Daily Wages Federation">Daily Wages Federation</asp:ListItem>
                                    <asp:ListItem Value="Job Rate Employee">Job Rate Employee</asp:ListItem>
                                     <asp:ListItem Value="Permanent NPF">Regular/Permanent NPF</asp:ListItem>
                                </asp:ListBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Show" ID="btnShow" OnClick="btnShow_Click" OnClientClick="return validateform();" />
                                    </div>

                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <a href="PayRollEpfDedMonthWise.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>

                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UAN_No" HeaderText="UAN" />
                                            <asp:BoundField DataField="EPF_No" HeaderText="EPF NO" />
                                            <asp:BoundField DataField="Office_Name" HeaderText="BRANCH" />
                                            <asp:BoundField DataField="Emp_Name" HeaderText="MEMBER_NAME" />
                                            <asp:BoundField DataField="Salary_EarningTotal" HeaderText="GROSS_WAGES" />
                                            <asp:BoundField DataField="EPFWAGES" HeaderText="EPF_WAGES" />
                                            <asp:BoundField DataField="EPS_WAGES" HeaderText="EPS_WAGES" />
                                            <asp:BoundField DataField="EDLI_WAGES" HeaderText="EDLI_WAGES" />
                                            <asp:BoundField DataField="EPF" HeaderText="EPF_CONTRI_REMITTED" />
                                            <asp:BoundField DataField="EPS_CONTRI_REMITTED" HeaderText="EPS_CONTRI_REMITTED" />
                                            <asp:BoundField DataField="EPF_EPS_DIFF_REMITTED" HeaderText="EPF_EPS_DIFF_REMITTED" />
                                            <asp:BoundField DataField="NCP_DAYS" HeaderText="NCP_DAYS" />
                                            <asp:BoundField DataField="REFUND_OF_ADVANCES" HeaderText="REFUND_OF_ADVANCES" />

                                            <%--                                            <asp:BoundField DataField="Emp_Name" HeaderText="Name Of Employee" />
                                            <asp:BoundField DataField="Emp_BasicSalery" HeaderText="Basic Salary" />
                                            <asp:BoundField DataField="Office_Name" HeaderText="Office Name" />
                                            <asp:BoundField DataField="EPF" HeaderText="EPF Amount" />--%>
                                        </Columns>
                                    </asp:GridView>
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
     <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script>
        $(function () {
            $('[id*=ddlEmp_TypeOfPost]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
    </script>
    <script>

        function validateform() {
            var msg = "";
            $("#valddlOffice").html("");
            $("#valddlFinancialYear").html("");
            $("#valddlMonth").html("");

            if (document.getElementById('<%=ddlFinancialYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select year. \n";
                $("#valddlFinancialYear").html("Select year.");
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
                $("#valddlMonth").html("Select Month.");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }

        }


        $(document).ready(function () {
            $('.datatable').DataTable({

                paging: false,

                columnDefs: [{
                    targets: 'no-sort',
                    orderable: false
                }],
                // "order": [[0, 'asc']],
                "bSort": false,

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
                        title: $('h3').text(),
                        exportOptions: {
                            columns: [1, 2, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('h3').text(),
                        exportOptions: {
                            columns: [1, 2, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13]
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
            t.on('order.dt search.dt', function () {
                t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
            }).draw();
        });
    </script>

</asp:Content>

