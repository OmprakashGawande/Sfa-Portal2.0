<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayRollMonthlyEarnDedDetail.aspx.cs" Inherits="mis_Payroll_PayRollMonthlyEarnDedDetail" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
     <asp:ScriptManager ID="ScriptManger" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <div class="row">
                                <div class="col-md-10">
                                    <h3 class="box-title" id="Label1">Monthly Earning Deduction Details:</h3>
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
                                        <asp:DropDownList runat="server" ID="ddlOffice_Name" CssClass="form-control" ClientIDMode="Static" OnSelectedIndexChanged="ddlOffice_Name_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlOffice_Name" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Year <span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" AutoPostBack="true">
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
                                        <label>Earning & Deduction Head<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlEarnDeducHead" runat="server" class="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlEarnDeducHead" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Show" ID="btnShow" OnClientClick="return validateform();" OnClick="btnShow_Click" />
                                    </div>
                                </div>
                                 <div class="col-md-2" id="pnldownloadAll" runat="server">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                      <asp:LinkButton ID="lnkbtnDownload" OnClick="lnkbtnDownload_Click" runat="server" CssClass="btn btn-primary btn-block"><i class="fa fa-download"> Download All</i></asp:LinkButton>
                                    </div>
                                </div>
                                 <div class="col-md-2" id="pnldownloadSingle" runat="server">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                      <asp:LinkButton ID="lnkbtnDownloadSingle" OnClick="lnkbtnDownloadSingle_Click" runat="server" CssClass="btn btn-warning btn-block"><i class="fa fa-download"> Download </i></asp:LinkButton>
                                        <asp:HiddenField runat="server" ID="hfempname" />
                                    </div>
                                </div>
                                <div class="col-md-2" id="pnlback" runat="server">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                          <asp:LinkButton ID="lnkBack" OnClick="lnkBack_Click" runat="server" CssClass="btn btn-default btn-block" Text="Back"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 lblheadingFirst">
                                    <p style="color: #123456; font-size: 15px;" runat="server">
                                        <asp:Label ID="lblDeductionDetails" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="col-md-12" id="pnlgriddata" runat="server">
                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="Emp_ID" OnRowCommand="GridView1_RowCommand" class="datatable table table-hover table-bordered pagination-ys" 
                                        AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Employee Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmp_Name" Text='<%# Eval("Emp_Name").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />--%>

                                            <asp:BoundField DataField="DesignationName" HeaderText="Designation" />
                                            <%-- <asp:BoundField DataField="EarningTotal" HeaderText="Total Earning" />      --%>
                                            <asp:TemplateField HeaderText="Earning Deduction Amount" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEarnDed" Text='<%# Eval("EarnDed").ToString() %>' runat="server" />
                                                      <asp:Label ID="lblEarningTotal" Visible="false" Text='<%# Eval("EarningTotal").ToString() %>' runat="server" />
                                                     <asp:Label ID="lblDesignationName" Visible="false" Text='<%# Eval("DesignationName").ToString() %>' runat="server" />
                                                      <asp:Label ID="lblEmp_GpfNo" Visible="false" Text='<%# Eval("Emp_GpfNo").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Arrear Earn/Ded Amt" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblArrearEarnDed" Text='<%# Eval("ArrearEarnDed").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUpdate" CommandName="RecordView" CssClass="btn btn-primary" CommandArgument='<%#Eval("Emp_ID") %>' runat="server" ToolTip="View"><i class="fa fa-eye"> View</i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:BoundField DataField="EarnDed" HeaderText="Report Amount" />
                                                                                        <asp:TemplateField HeaderText="Policy Deduction">
                                                    <ItemTemplate><%#Eval("PolicyDed_PolicyAmt")%></ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbl" runat="server" />
                                                    </FooterTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12" id="pnlreceiptAll" runat="server">
                                    <div class="table-responsive">
                                    <div class="form-group">
                                        <rsweb:ReportViewer ID="ReportViewer1" Height="600px" runat="server" Width="938px"></rsweb:ReportViewer>
                                    </div>
                                    </div>
                                    </div>
                                 <div class="col-md-12" id="pnlreceiptSingle" runat="server">
                                    <div class="table-responsive">
                                    <div class="form-group">
                                        <rsweb:ReportViewer ID="ReportViewer2" Height="600px" runat="server" Width="938px"></rsweb:ReportViewer>
                                    </div>
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
        $("#txtmonth").datepicker({
            format: "MM",
            viewMode: "months",
            minViewMode: "months",
            autoclose: true
        });
        function validateform() {
            var msg = "";
            $("#valddlOffice_Name").html("");
            $("#valddlFinancialYear").html("");
            $("#valtxtmonth").html("");

           <%-- if (document.getElementById('<%=ddlOffice_Name.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office Name. \n";
                $("#valddlOffice_Name").html("Select Office Name.");
            }--%>
            if (document.getElementById('<%=ddlFinancialYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Year. \n";
                $("#valddlFinancialYear").html("Select year.");
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
                $("#valtxtmonth").html("Select Month.");
            }
            if (document.getElementById('<%=ddlEarnDeducHead.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Earning Deduction Head \n";
                $("#valtxtmonth").html("Select Earning Deduction Head.");
            }
           <%-- if (document.getElementById('<%=txtmonth.ClientID%>').value == "") {
                msg = msg + "Select Month. \n";
                $("#valtxtmonth").html("Select Month.");
            }--%>
            if (msg != "") {
                alert(msg);
                return false;
            }
        }
        $(document).ready(function () {
            $('.datatable').DataTable({

                paging: false,
                bSort: false,
                columnDefs: [{
                    targets: 'no-sort',
                    orderable: false
                }],
                "order": [[0, 'asc']],

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
                        //title: 'Monthly Head Wise All Employee Details.',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4]
                        },
                        footer: true,
                        autoPrint: true
                    },
                    //{
                    //    extend: 'excel',
                    //    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    //    title: 'Monthly Head Wise All Employee Details.',
                    //    exportOptions: {
                    //        columns: ':not(.no-print)'
                    //    },
                    //    footer: true
                    //}
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
            t.on('order.dt search.dt', function () {
                t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
            }).draw();
        });
    </script>
</asp:Content>

