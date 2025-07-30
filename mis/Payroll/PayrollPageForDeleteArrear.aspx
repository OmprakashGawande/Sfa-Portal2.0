<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollPageForDeleteArrear.aspx.cs" Inherits="mis_Payroll_PayrollPageForDeleteArrear" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) --
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Arrear Detail</h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Employee<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GridView1" runat="server" class="table datatable  table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="EmpArrearID" OnRowDeleting="GridView1_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S. NO" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Emp_Name" HeaderText="EMPLOYEE NAME" />
                                             <asp:BoundField DataField="Office_Name" HeaderText="Office" />
                                            <asp:BoundField DataField="OrderNo" HeaderText="ORDER NO" />
                                            <asp:BoundField DataField="ApplyDate" HeaderText="APPLY DATE" />
                                            <asp:BoundField DataField="BasicSalary" HeaderText="BASIC SALARY" />
                                            <asp:BoundField DataField="TotalEarning" HeaderText="TOTAL EARNING" />
                                            <asp:BoundField DataField="TotalDeduction" HeaderText="TOTAL DEDUCTION" />
                                            <asp:BoundField DataField="NetPaymentAmount" HeaderText="NET SALARY" />
                                            <asp:BoundField DataField="ArrearPaidIn" HeaderText="Arrear Paid In" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" ToolTip='<%# Eval("Arrear_UpdatedOn") %>' CssClass="label label-danger" runat="server" CommandName="delete">Delete</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
    <link href="../HR/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="../HR/js/jquery.dataTables.min.js"></script>
    <script src="../HR/js/dataTables.bootstrap.min.js"></script>
    <script src="../HR/js/dataTables.buttons.min.js"></script>
    <script src="../HR/js/jszip.min.js"></script>
    <script src="../HR/js/pdfmake.min.js"></script>
    <script src="../HR/js/vfs_fonts.js"></script>
    <script src="../HR/js/buttons.html5.min.js"></script>
    <script src="../HR/js/buttons.print.min.js"></script>
    <script type="text/javascript">

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
                        title: $('.empoyee-name').text() + " - Arrear Detail",
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7,8]
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('.empoyee-name').text() + " - Arrear Detail",
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7,8]
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
        });
        </script>
</asp:Content>

