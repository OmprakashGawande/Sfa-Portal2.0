<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SeniorityList.aspx.cs" Inherits="mis_HR_SeniorityList" %>

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
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Seniority List (वरिष्ठता सूची)</h3>
                     <div class="pull-right">
                                <asp:HyperLink runat="server" href="AddSeniorityList.aspx" ToolTip="Click to Add Seniority List" class="btn label-warning"><i class="fa fa-plus"> Add</i></asp:HyperLink>
                      </div>
                </div>
                <div class="box-body">
                     <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive rover">
                                <%--<table class="datatable table table-striped table-bordered table-hover">
                                    <tr>
                                        <th>S.No</th>
                                        <th>Designation <br />(पद)</th>
                                        <th>View Details<br />(विवरण देखें)</th>
                                    </tr>
                                     <tr>
                                        <td>1
                                        </td>
                                        <td>General Manager (General)2019
                                        </td>
                                        <td><a href="UploadDoc/SeniorityList_DesigWise/GMGen2019done.pdf" target="_blank"><i class="fa fa-eye"></i></a></td>
                                    </tr>
                                     <tr>
                                        <td>2
                                        </td>
                                        <td>Deputy General Manager (Account) 2019
                                        </td>
                                        <td><a href="UploadDoc/SeniorityList_DesigWise/DYGENManagerAcc2019done.pdf" target="_blank"><i class="fa fa-eye"></i></a></td>
                                    </tr>
                                      <tr>
                                        <td>3
                                        </td>
                                        <td>Deputy Manager (General) 2019
                                        </td>
                                        <td><a href="UploadDoc/SeniorityList_DesigWise/DuptyManager(Genral-)2019done.pdf" target="_blank"><i class="fa fa-eye"></i></a></td>
                                    </tr>                                
                                  
                                   
                                    <tr>
                                        <td>4
                                        </td>
                                        <td>Deputy General Manager (General) 2019
                                        </td>
                                        <td><a href="UploadDoc/SeniorityList_DesigWise/DyGenManager(Gneral)2019done.pdf" target="_blank"><i class="fa fa-eye"></i></a></td>
                                    </tr>
                                    <tr>
                                        <td>5
                                        </td>
                                        <td>Deputy Manager (Account) 2019
                                        </td>
                                        <td><a href="UploadDoc/SeniorityList_DesigWise/DyManagerAcc2019done.pdf" target="_blank"><i class="fa fa-eye"></i></a></td>
                                    </tr>
                                   
                                    <tr>
                                        <td>6
                                        </td>
                                        <td>Manager (General) 2019
                                        </td>
                                        <td><a href="UploadDoc/SeniorityList_DesigWise/ManagerGen2019done.pdf" target="_blank"><i class="fa fa-eye"></i></a></td>
                                    </tr>
                                    <tr>
                                        <td>7
                                        </td>
                                        <td>Executive Engineer 2019
                                        </td>
                                        <td><a href="UploadDoc/SeniorityList_DesigWise/AssitantEngineer2019.pdf" target="_blank"><i class="fa fa-eye"></i></a></td>
                                    </tr>
                                    <tr>
                                        <td>8
                                        </td>
                                        <td>Microbiologist 2019
                                        </td>
                                        <td><a href="UploadDoc/SeniorityList_DesigWise/MicroBoilogist2019Done.pdf" target="_blank"><i class="fa fa-eye"></i></a></td>
                                    </tr>
                                     <tr>
                                        <td>9
                                        </td>
                                        <td>Personal Assistant  2019
                                        </td>
                                        <td><a href="UploadDoc/SeniorityList_DesigWise/PA2019.pdf" target="_blank"><i class="fa fa-eye"></i></a></td>
                                    </tr>
                                </table>--%>
                                <asp:GridView ID="GridView1" Width="100%" ShowHeaderWhenEmpty="true" EmptyDataText="No records Found" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.<br />(क्रं.)" HeaderStyle-Width="10px">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Year <br />(वर्ष)" HeaderStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSLYear" runat="server" Text='<%# Eval("SLYear") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation <br />(पद)">
                                            <ItemTemplate>
                                                <%# Eval("Designation_Name") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Seniority List <br /> (वरिष्ठता सूची)">
                                            <ItemTemplate>
                                                <center>
                                                <asp:HyperLink ID="hylimageview" runat="server" ToolTip="Click to View Seniority List" ForeColor="DeepSkyBlue" NavigateUrl='<%# "../HR/UploadDoc/SeniorityList/" + Eval("SLDoc") %>' Target="_blank"><i class="fa fa-picture-o"></i></asp:HyperLink>
                                               </center
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
                    title: ('Seniority List'),
                    exportOptions: {
                        columns: [0, 1, 2]
                    },
                    footer: false,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    filename: 'SeniorityList_Report',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: ('Seniority List'),
                    exportOptions: {
                        columns: [0, 1, 2]
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

