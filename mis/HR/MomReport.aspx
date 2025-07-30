<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MomReport.aspx.cs" Inherits="mis_HR_MomReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <link href="css/hrcustom.css" rel="stylesheet" />
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <style>
        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class =" box box-success">
                        <div class="box-header">
                            <h1 class="box-title">Financial Year Wise Board Meeting</h1>
                            <asp:label id="lblMsg" runat="server" text=""></asp:label>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">                               
                                <asp:gridview id="GridView1" runat="server" class="datatable table table-hover table-bordered" autogeneratecolumns="False" datakeynames="Mom_ID" EmptyDataText="No Record Found">
                                    <Columns>                             
                                        <asp:BoundField DataField="Mom_No" HeaderText="Board Meeting No" />
                                        <asp:BoundField DataField="Mom_Date" HeaderText="Board Meeting Held On" /> 
                                        <asp:BoundField DataField="FY" HeaderText="Financial Year" />                                    
                                        <asp:TemplateField HeaderText="Uploaded File" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="document" runat="server" NavigateUrl='<%# Eval("Mom_FileUpload").ToString() %>' CssClass="label label-default" Target="_blank" Text='<%# Eval("Mom_FileUpload").ToString() == ""? "NA":"VIEW"  %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:gridview>
                            
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
     <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>
    <script src="js/dataTables.buttons.min.js"></script>
    <%-- <script src="js/buttons.flash.min.js"></script>--%>
    <script src="js/jszip.min.js"></script>
    <%-- <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>--%>
    <script src="js/buttons.html5.min.js"></script>
    <script src="js/buttons.print.min.js"></script>


    <script>

        $(document).ready(function () {
            $('.datatable').DataTable({

                paging: true,

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
                        title: $('h1').text(),
                        exportOptions: {
                            columns: [0, 1,2]
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('h1').text(),
                        exportOptions: {
                            columns: [0, 1,2]
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

