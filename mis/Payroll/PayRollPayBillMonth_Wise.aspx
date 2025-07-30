<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayRollPayBillMonth_Wise.aspx.cs" Inherits="mis_Payroll_PayRollPayBillMonth_Wise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .NonPrintable {
            display: none;
        }

        @media print {
            .NonPrintable {
                display: block;
            }

            .noprint {
                display: none;
            }

            .header {
                display: table-header-group;
            }
        }

        .table1 > tbody > tr > td, .table1 > tbody > tr > th, .table1 > tfoot > tr > td, .table1 > tfoot > tr > th, .table1 > thead > tr > td, .table1 > thead > tr > th {
            padding: 4px 2px;
            font-size: 10.5px;
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

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }

        table {
            white-space: nowrap;
        }

        .alignR {
            text-align: right !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content noprint">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Monthly Pay Bill - Pay Bank Wise Employee</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label><span style="color: red">*</span>
                                        <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" ClientIDMode="Static" Enabled="false">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                       <%-- <small><span id="valddlOffice" class="text-danger"></span></small>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Year <span class="text-danger">*</span></label>
                                         <%--<small><span id="valddlFinancialYear" class="text-danger"></span></small>--%>
                                        <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                        </asp:DropDownList>
                                       
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Month <span style="color: red;">*</span></label>
                                        <%-- <small><span id="valddlMonth" class="text-danger"></span></small>--%>
                                        <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                            <%--<asp:ListItem Value="0">Select Month</asp:ListItem>
                                            <asp:ListItem Value="January">January</asp:ListItem>
                                            <asp:ListItem Value="February">February</asp:ListItem>
                                            <asp:ListItem Value="March">March</asp:ListItem>
                                            <asp:ListItem Value="April">April</asp:ListItem>
                                            <asp:ListItem Value="May">May</asp:ListItem>
                                            <asp:ListItem Value="June">June</asp:ListItem>
                                            <asp:ListItem Value="July">July</asp:ListItem>
                                            <asp:ListItem Value="August">August</asp:ListItem>
                                            <asp:ListItem Value="September">September</asp:ListItem>
                                            <asp:ListItem Value="October">October</asp:ListItem>
                                            <asp:ListItem Value="November">November</asp:ListItem>
                                            <asp:ListItem Value="December">December</asp:ListItem>--%>
                                        </asp:DropDownList>
                                       
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Type of Post (पद प्रकार) <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlEmp_TypeOfPost" runat="server" class="form-control">
                                            <asp:ListItem Value="All">All</asp:ListItem>
                                            <asp:ListItem Value="Permanent">Regular/Permanent</asp:ListItem>
                                            <asp:ListItem Value="Traniee">Traniee</asp:ListItem>
                                            <%--<asp:ListItem Value="Fixed Employee">Fixed Employee(स्थाई कर्मी)</asp:ListItem>
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
                                             <asp:ListItem Value="Permanent NPF">Regular/Permanent NPF</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Bank name(बैंक का नाम)<span style="color: red;">*</span></label>
                                        <%-- <asp:TextBox ID="txtBank_Name" runat="server" placeholder="Enter Bank Name..." class="form-control" MaxLength="100"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlBank_Name" runat="server" placeholder="Select Bank Name..." class="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Show" ID="btnShow" OnClick="btnShow_Click" OnClientClick="return validateform();" />
                                    </div>

                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <a href="PayRollPayBillMonth_Wise.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>

                                </div>


                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnPrint" Visible="false" CssClass="btn btn-default" runat="server" Text="Print" OnClientClick="window.print();" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">

                                    <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" ShowFooter="true">
                                        <Columns>
                                            <%-- <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="SNo" HeaderText="SNo" ControlStyle-Width="5" />
                                            <asp:BoundField DataField="Salary_NetSalary" HeaderText="Amount" ItemStyle-CssClass="alignR" />
                                            <%-- <asp:TemplateField HeaderText="Amount" ItemStyle-CssClass="alignR">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetsalary" runat="server" Text='<%# Eval("Salary_NetSalary")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="SubTotal" HeaderText="BANK WISE TOTAL" ItemStyle-CssClass="alignR" />
                                            <%-- <asp:TemplateField HeaderText="BANK WISE TOTAL" ItemStyle-CssClass="alignR">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNetsalary" runat="server" Text='<%# Eval("SubTotal")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="Bank_Name" HeaderText="Bank Name" />
                                            <asp:BoundField DataField="Bank_IfscCode" HeaderText="IFSC_Code" />
                                            <asp:BoundField DataField="Bank_AccountNo" HeaderText="Bank_Account_Number" />
                                            <%--<asp:BoundField DataField="Bank_AccountNo1" HeaderText="Bank_Account_Number" />--%>
                                            <asp:BoundField DataField="Emp_Name" HeaderText="Beneficiary_Name" />
                                            <%--<asp:BoundField DataField="Address" HeaderText="Address" />--%>
                                            <%-- <asp:TemplateField HeaderText="Address">                                               
                                                <ItemTemplate>
                                                 
                                                     <asp:Label ID="Label2" runat="server" Text="BHOPAL"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <section class="content">
            <div id="printsection" class="NonPrintable" runat="server"></div>
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
                            columns: ':not(.no-print)'
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('h3').text(),
                        exportOptions: {
                            orthogonal: 'sort',
                            format: {
                                body: function (data, row, column, node) {
                                    data = data.trim();
                                    data = column === 4 ? "\0" + data : data;
                                    return data.replace(/(&nbsp;|<([^>]+)>)/ig, "");
                                    //var data = data.find("span").text();
                                    //return data;
                                }
                            }
                        },
                        //customizeData: function (data) {
                        //    for (var i = 0; i < data.body.length; i++) {
                        //        for (var j = 0; j < data.body[i].length; j++) {
                        //            if (j ==4) {
                        //                data.body[i][j] = '\u200C' + data.body[i][j];
                        //            }

                        //        }
                        //    }
                        //},
                        // footer: true
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
            //t.on('order.dt search.dt', function () {
            //    t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            //        cell.innerHTML = i + 1;
            //    });
            //}).draw();
        });
    </script>

</asp:Content>

