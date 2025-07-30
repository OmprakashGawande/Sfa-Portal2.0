<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="LedgerList.aspx.cs" Inherits="mis_Finance_LedgerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        div.dataTables_wrapper div.dataTables_length select {
            margin-top:3px !important;
        }
        @media print {
         .hidden {display:none !important;}
      }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Ledger Mapped With Office</h3>
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="box-body">                           
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GVLedgerDetail" ShowHeaderWhenEmpty="true" EmptyDataText="Record Not Found!" EmptyDataRowStyle-ForeColor="Red" class="datatable table table-bordered" AutoGenerateColumns="False" DataKeyNames="Ledger_ID" runat="server" OnSelectedIndexChanged="GVLedgerDetail_SelectedIndexChanged" OnRowDeleting="GVLedgerDetail_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="10" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" ToolTip='<%# Eval("Ledger_ID") %>' Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ledger Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLedger_Name" Text='<%# Eval("Ledger_Name") %>' runat="server" />
                                                        <%--<asp:Label ID="lblID" CssClass="hidden" Text='<%# Eval("Ledger_ID") %>' runat="server" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <ItemTemplate>
                                                        
                                                        <asp:Label ID="lblID" CssClass="hidden" Text='<%# Eval("Ledger_ID") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="GST NO">
                                                    <ItemTemplate>
                                      
                                                                          <asp:Label ID="lblGstNO" Text='<%# Eval("GST_No") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Ledger_Name" HeaderText="Ledger Name" />--%>
                                                <%--<asp:BoundField DataField="Head_Name" HeaderText="Head Name" />--%>
                                                <%--<asp:BoundField DataField="Office_Name" HeaderText="Office Name" />--%>
                                                <asp:TemplateField HeaderText="Status" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server"  Text='<%# Eval("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ShowHeader="False" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Edit" runat="server" CssClass="label Aselect1 label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
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
        $('.datatable').DataTable({
            paging: true,
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
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h3').text(),
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
        
        function getConfirmation() {
            debugger;
            var retVal = confirm("Record will be deleted. Are you sure want to continue?");
            if (retVal == true) {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }
            else {

                return false;
            }
        }
    </script>
</asp:Content>