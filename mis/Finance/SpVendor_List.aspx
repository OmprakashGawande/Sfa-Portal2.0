<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SpVendor_List.aspx.cs" Inherits="mis_Finance_SpVendor_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <!-- general form elements -->

                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" >Vendor List</h3>
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">


                            <div class="row">
                                <div class="col-md-12">
                                   <div class="table-responsive">
                                        <asp:GridView ID="GVVendorDetail" ShowHeaderWhenEmpty="true"   class="datatable table table-striped no-border" AutoGenerateColumns="False" DataKeyNames="Vendor_id" runat="server" OnSelectedIndexChanged="GVVendorDetail_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="10" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ledger Status" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLedgerStatus" runat="server" Text='<%# Eval("LedgerStatus").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:BoundField DataField="VendorName" HeaderText="Vendor Name"/>  
                                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No"/>  
                                    <asp:TemplateField HeaderText="View More Detail" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkViewMore" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="ViewMore"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                    <%--<table border="1" class="table table-responsive table-striped table-bordered table-hover">
                                        <tr>
                                            <th>S.No</th>
                                            <th>Ledger Status</th>
                                            <th>Vendor Name </th>

                                            <th>Mobile No</th>
                                           
                                            <th>View More Detail</th>

                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td><span class="badge bg-red">Pending</span></td>
                                            <td>Hakimi Rubber Stamp</td>

                                            <td>9893098930</td>
                                            
                                            <td><a href="../../mis/Finance/LedgerMaster.aspx" target="_blank" class="label label-default">View More</a></td>

                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td><span class="badge bg-red">Pending</span></td>
                                            <td>E -stamp Vendor</td>

                                            <td>9893098930</td>
                                           
                                            <td><a href="../../mis/Finance/LedgerMaster.aspx" target="_blank" class="label label-default">View More</a></td>

                                        </tr>
                                        <tr>
                                            <td>3</td>
                                            <td><span class="badge bg-red">Pending</span></td>
                                            <td>Mishra Interprises</td>

                                            <td>9893098930</td>
                                            
                                            <td><a href="../../mis/Finance/LedgerMaster.aspx" target="_blank" class="label label-default">View More</a></td>

                                        </tr>
                                        <tr>
                                            <td>4</td>
                                            <td><span class="badge bg-green">Approved</span></td>
                                            <td>Sahu Stamp Vikreta</td>

                                            <td>9893098930</td>
                                           
                                            <td><a href="../../mis/Finance/LedgerMaster.aspx" target="_blank" class="label label-default">View More</a></td>

                                        </tr>
                                        <tr>
                                            <td>5</td>
                                            <td><span class="badge bg-green">Approved</span></td>
                                            <td>New Adarsh Associates</td>

                                            <td>9893098930</td>
                                            
                                            <td><a href="../../mis/Finance/LedgerMaster.aspx" target="_blank" class="label label-default">View More</a></td>

                                        </tr>
                                    </table>--%>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server"> <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
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
                    title: $('h1').text(),
                    exportOptions: {
                        
                        // javascript: print(),

                        columns: [0,1,2,3]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3]
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

