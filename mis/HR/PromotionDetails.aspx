<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PromotionDetails.aspx.cs" Inherits="mis_HR_PromotionDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
     <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
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
        .rover{
            overflow-x: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="row">
               
                <div class="col-md-12">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Promotion History</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <%--<div class="col-md-12">
                            <div class="table-responsive">
                                <table class="datatable table table-striped table-bordered table-hover">
                                    <tr>
                                        <th>S.No</th>
                                        <th>Employee Name</th>
                                        <th>Designation</th>
                                        <th>Department</th>
                                        <th>Location</th>
                                        <th>Date Of Joining</th>
                                        <th>Date of Retirement</th>
                                        <th>View Promotion Details</th>
                                    </tr>
                                    <tr>
                                        <td>
                                            1
                                        </td>
                                        <td>
                                           MANOJ JAIN
                                        </td>
                                        <td>
                                            Manager (General)
                                        </td>
                                        <td>
                                            HR
                                        </td>
                                        <td>
                                            Head Office
                                        </td>
                                        <td>
                                            15/08/1963
                                        </td>
                                        <td>
                                            31/08/2025
                                        </td>
                                        <td><a href="UploadDoc/Promotion_Details/ManojJain.pdf" target="_blank"><i class="fa fa-eye"></i></a></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            2
                                        </td>
                                        <td>
                                            DILIP KUMAR PARMAR
                                        </td>
                                        <td>
                                            Deputy manager (Accounts)
                                        </td>
                                        <td>
                                            Accounts
                                        </td>
                                        <td>
                                           Head Office
                                        </td>
                                        <td>
                                            01/06/1982
                                        </td>
                                        <td>
                                            30/09/2023
                                        </td>
                                        <td><a href="UploadDoc/Promotion_Details/DKParmar.pdf" target="_blank"><i class="fa fa-eye"></i></a></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            3
                                        </td>
                                        <td>
                                            CHANDRA SHEKHAR SHRIVASTAVA
                                        </td>
                                        <td>
                                            Deputy General Manager (HRD)
                                        </td>
                                        <td>
                                           HRD
                                        </td>
                                        <td>
                                           Head Office
                                        </td>
                                        <td>
                                            01/10/1984
                                        </td>
                                        <td>
                                            30/09/2020
                                        </td>
                                        <td><a href="UploadDoc/Promotion_Details/CSSrivastava.pdf" target="_blank"><i class="fa fa-eye"></i></a></td>
                                    </tr>

                                    <tr>
                                        <td>
                                            4
                                        </td>
                                        <td>
                                            ADITYA KUMAR KHARE 
                                        </td>
                                        <td>
                                            Regional Manager
                                        </td>
                                        <td>
                                           -
                                        </td>
                                        <td>
                                          Jabalpur - Regional Office
                                        </td>
                                        <td>
                                            02/05/1985
                                        </td>
                                        <td>
                                            31/01/2023
                                        </td>
                                        <td><a href="UploadDoc/Promotion_Details/AdityaKumarKhare.pdf" target="_blank"><i class="fa fa-eye"></i></a></td>
                                    </tr>

                                </table>
                            </div>
                        </div>--%>
                        <div class="col-md-12">
                            <div class="table table-responsive rover">
                                <asp:Label ID="lblMsg2" runat="server" Text="" Style="color: red; font-size: 15px;"></asp:Label>
                                <asp:GridView ID="GridView1" class="datatable table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" AutoGenerateColumns="False" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="OrderNo" HeaderText="Order No" />
                                       <asp:TemplateField HeaderText="Order Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrderDate" Text='<%# Eval("OrderDate","{0:dd/MM/yyyy}") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NewLevel_Name" HeaderText="Current Level" />
                                        <asp:BoundField DataField="NewBasicSalery" HeaderText="Current Basic Salary" />
                                        <asp:TemplateField HeaderText="Current Effective Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNewEffectiveDate" Text='<%# Eval("NewEffectiveDate","{0:dd/MM/yyyy}") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NewDepartment" HeaderText="Current Department" />
                                        <asp:BoundField DataField="NewDesignation" HeaderText="Current Designation" />
                                        <asp:BoundField DataField="OldLevel_Name" HeaderText="Previous Level" />
                                        <asp:BoundField DataField="OldDepartment" HeaderText="Previous Department" />
                                        <asp:BoundField DataField="OldDesignation" HeaderText="Previous Designation" />
                                        <asp:BoundField DataField="OldBasicSalery" HeaderText="Previous Basic Salary" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

                   </div>
                </div>
            <!-- /.box-body -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
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
        $('.datatable').DataTable({
            paging: false,
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
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: ('Promotion History'),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    filename: 'EmployeePromotion_Report',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: ('Promotion History'),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                    },
                    footer: false
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

