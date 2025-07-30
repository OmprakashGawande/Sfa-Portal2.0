<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayRollQuarterlyEarnDedDetail.aspx.cs" Inherits="mis_Payroll_PayRollQuarterlyEarnDedDetail" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">

    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="css/hrcustom.css" rel="stylesheet" />
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" />



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
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <div class="row">
                                <div class="col-md-4">
                                   <%-- <h4 class="box-title" id="Label1">[Quarterly ITax and PTax]Quarterly Earning Deduction Details:</h4>--%>
                                    <h4 class="box-title" id="Label1">(Quarterly ITax and PTax)Quarterly Earning Deduction Details:</h4>
                                </div>
                                <div class="col-md-2">
                                    <asp:RadioButtonList ID="rbnlist" AutoPostBack="true" OnSelectedIndexChanged="rbnlist_SelectedIndexChanged" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="&nbsp;New Report &nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" style="float: right" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="&nbsp; Old Report" style="float: left" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
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
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="rfvYear" ValidationGroup="a"
                                                ErrorMessage="Select Office" ForeColor="Red" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Office Name !'></i>"
                                                ControlToValidate="ddlOffice_Name" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList runat="server" ID="ddlOffice_Name" CssClass="form-control" ClientIDMode="Static" OnSelectedIndexChanged="ddlOffice_Name_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlOffice_Name" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Year <span class="text-danger">*</span></label>
                                         <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                ErrorMessage="Select Year" ForeColor="Red" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Year !'></i>"
                                                ControlToValidate="ddlFinancialYear" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlFinancialYear" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3" id="pnloldmonth" runat="server">
                                    <div class="form-group">
                                        <label>Month <span style="color: red;">*</span></label>
                                         <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                ErrorMessage="Select Month" ForeColor="Red" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                                ControlToValidate="ddlMonth" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                            <asp:ListItem Value="0">Select Quarter</asp:ListItem>
                                            <asp:ListItem Value="1">January-February-March</asp:ListItem>
                                            <asp:ListItem Value="2">April-May-June</asp:ListItem>
                                            <asp:ListItem Value="3">July-August-September</asp:ListItem>
                                            <asp:ListItem Value="4">October-November-December</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlMonth" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3" id="pnlnewmonth" runat="server">
                                    <div class="form-group">
                                        <label>Month <span style="color: red;">*</span></label>
                                        <asp:ListBox runat="server" ID="ddlMonthNew"
                                            ClientIDMode="Static" CssClass="form-control"
                                            SelectionMode="Multiple">
                                            <asp:ListItem Value="1" Text="January-February-March"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="April-May-June"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="July-August-September"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="October-November-December"></asp:ListItem>
                                        </asp:ListBox>

                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Earning & Deduction Head<span style="color: red;">*</span></label>
                                         <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                ErrorMessage="Select Earning & Deduction Head" ForeColor="Red" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Earning & Deduction Head !'></i>"
                                                ControlToValidate="ddlEarnDeducHead" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
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
                                        <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Show" ID="btnShow" ValidationGroup="a" OnClick="btnShow_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:LinkButton ID="lnkExport" runat="server" CssClass="btn btn-primary btn-block" OnClick="lnkExport_Click"><i class="fa fa-file-excel-o"> Export</i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <p style="color: #123456; font-size: 15px;" runat="server">
                                        <asp:Label ID="lblDeductionDetails" runat="server" Text=""></asp:Label>
                                    </p>
                                </div>
                                <div class="col-md-12 table-responsive" id="pnlgrid" runat="server">
                                    <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowFooter="true" HeaderStyle-BackColor="#9AD6ED" HeaderStyle-ForeColor="#636363">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="DesignationName" HeaderText="Designation" />
                                            <asp:BoundField DataField="Emp_PanCardNo" HeaderText="Pan No" />

                                            <asp:BoundField DataField="ArrearEarnings" HeaderText="Earning Arrear" />
                                            <asp:BoundField DataField="EarningTotal1" HeaderText="EarningTotal1" />
                                            <asp:BoundField DataField="EarningTotal2" HeaderText="EarningTotal2" />
                                            <asp:BoundField DataField="EarningTotal3" HeaderText="EarningTotal3" />
                                            <asp:TemplateField HeaderText="Earning Total" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEarningTotal" Text='<%# Eval("PTaxDed3").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--                                            <asp:BoundField DataField="ArrearITaxDed" HeaderText="ITax Arrear" />
                                            <asp:BoundField DataField="ITaxDed1" HeaderText="ITaxDed1" />
                                            <asp:BoundField DataField="ITaxDed2" HeaderText="ITaxDed2" />
                                            <asp:BoundField DataField="ITaxDed3" HeaderText="ITaxDed3" />
                                            <asp:TemplateField HeaderText="ITax Total" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblITaxDedTotal" Text='<%# Eval("PTaxDed3").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:BoundField DataField="ArrearPTaxDed" HeaderText="EarnDed Arrear" />
                                            <asp:BoundField DataField="PTaxDed1" HeaderText="EarnDed 1" />
                                            <asp:BoundField DataField="PTaxDed2" HeaderText="EarnDed 2" />
                                            <asp:BoundField DataField="PTaxDed3" HeaderText="EarnDed 3" />
                                            <asp:TemplateField HeaderText="EarnDed Total" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPTaxDedTotal" Text='<%# Eval("PTaxDed3").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <%--                                            <asp:TemplateField HeaderText="Earning Deduction Amount" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEarnDed" Text='<%# Eval("PTaxDed3").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Arrear Earn/Ded Amt" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblArrearEarnDed" Text='<%# Eval("PTaxDed3").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>


                                <div class="col-md-12" id="pnlITOrPT_Part1" runat="server">
                                    <div class="table-responsive">
                                        <div class="form-group">
                                            <rsweb:ReportViewer ID="ReportViewer1" Height="600px" runat="server" Width="1200px"></rsweb:ReportViewer>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12" id="pnlITOrPT_Part2" runat="server" visible="false">
                                    <div class="table-responsive">
                                        <div class="form-group">
                                            <rsweb:ReportViewer ID="ReportViewer2" Height="600px" runat="server" Width="1200px"></rsweb:ReportViewer>
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
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>
    <script src="js/dataTables.buttons.min.js"></script>
    <%-- <script src="js/buttons.flash.min.js"></script>--%>
    <script src="js/jszip.min.js"></script>
    <%-- <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>--%>
    <script src="js/buttons.html5.min.js"></script>
    <script src="js/buttons.print.min.js"></script>
    <script src="../../../mis/js/jquery-2.2.4.js"></script>
    <script src="../js/bootstrap-multiselect.js"></script>
    <script>
        $(document).ready(function () {
            $(function () {
                $('[id*=ddlMonthNew]').multiselect({
                    includeSelectAllOption: true,
                    buttonWidth: '100%'
                });
            });
        });
    </script>
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
                msg = msg + "Select Quarter. \n";
                $("#valtxtmonth").html("Select Quarter.");
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
                        title: 'Quarterly Head Wise All Employee Details.',
                        exportOptions: {
                            columns: ':not(.no-print)'
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: 'Quarterly Head Wise All Employee Details.',
                        exportOptions: {
                            columns: ':not(.no-print)'
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

