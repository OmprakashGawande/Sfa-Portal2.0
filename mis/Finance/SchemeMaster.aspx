<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SchemeMaster.aspx.cs" Inherits="mis_Finance_SchemeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
         .capitalize
        {
            text-transform: capitalize;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Scheme Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Scheme Name<span style="color: red;">*</span></label>
                                        <asp:TextBox runat="server" ID="txtSchemeTx_Name" ClientIDMode="Static" CssClass="form-control capitalize"></asp:TextBox>
                                        <small><span id="valtxtSchemeTx_Name" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" Text="Add" ID="btnAddScheme" ClientIDMode="Static" CssClass="btn btn-block btn-success" OnClick="btnAddScheme_Click" OnClientClick="return validateScheme();"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" Text="Clear" ID="btnClear" ClientIDMode="Static" CssClass="btn btn-block btn-default" OnClick="btnClear_Click"></asp:Button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView runat="server" CssClass="datatable table table-bordered" DataKeyNames="SchemeTx_ID" ShowHeaderWhenEmpty="true" ID="GridViewSchemeDetail" AutoGenerateColumns="false" OnSelectedIndexChanged="GridViewSchemeDetail_SelectedIndexChanged" OnRowDeleting="GridViewSchemeDetail_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSchemeTx_ID" runat="server" Text='<%# Eval("SchemeTx_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Scheme Name" HeaderStyle-Width="20%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSchemeTx_Name" runat="server" Text='<%# Eval("SchemeTx_Name") %>' ToolTip='<%# Eval("SchemeTx_Description") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LnkEdit" runat="server" CommandName="Select" CausesValidation="false" CssClass="label label-default">Edit</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LnkDelete" runat="server" CommandName="Delete" CausesValidation="false" CssClass="label label-danger" OnClientClick="return getConfirmation();">Delete</asp:LinkButton>
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
            <asp:HiddenField ID="hfvalue" runat="server" />
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
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1]
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
        function validateScheme() {
            var msg = "";
            $("#valtxtSchemeTx_Name").html("");
            if (document.getElementById('<%=txtSchemeTx_Name.ClientID%>').value.trim() == "") {
                msg += "Enter Scheme Name .";
                $("#valtxtSchemeTx_Name").html("Enter Scheme Name");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }
        }
    </script>
</asp:Content>

