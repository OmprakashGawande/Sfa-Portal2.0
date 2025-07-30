<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminItemType.aspx.cs" Inherits="mis_Admin_AdminItemType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        div.dataTables_wrapper div.dataTables_length select {
            margin-top:3px !important;
        }
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
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Item Group Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row" >
                                <div class="col-md-6" style="display:none;">
                                    <div class="form-group">
                                        <label>Nature<span class="text-danger"> *</span></label>
                                        <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlItemCategory_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlItemCategory" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-6" >
                                    <div class="form-group">
                                        <label>Item Group<span class="text-danger"> *</span></label>
                                        <asp:TextBox ID="txtItem_Type"  runat="server" placeholder="Item Group..." class="form-control capitalize" MaxLength="100" onkeypress="return validatename(event);"></asp:TextBox>
                                        <small><span id="valtxtItem_Type" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" OnClientClick="return validateform();" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <a href="AdminItemType.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered" AutoGenerateColumns="False" DataKeyNames="ItemType_id" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting" ShowHeaderWhenEmpty="True">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:BoundField DataField="ItemCatName" HeaderText="Nature" />--%>
                                            <asp:TemplateField HeaderText="Item Group">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemGroup" runat="server" Text='<%# Bind("ItemTypeName") %>'></asp:Label>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" Visible="false">
                                                <ItemTemplate>
                                                    
                                                    <asp:Label ID="lblid" CssClass="hidden" runat="server" Text='<%# Bind("ItemType_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action" ShowHeader="False" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Select" runat="server" CssClass="label Aselect1 label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                                    <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return getConfirmation();"></asp:LinkButton>
                                                </ItemTemplate>

<ItemStyle Width="25%"></ItemStyle>
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
                        columns:[0]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0]
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
        function validateform() {
            var msg = "";
            $("#valddlItemCategory").html("");
            $("#valtxtItem_Type").html("");
            if (document.getElementById('<%=ddlItemCategory.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Nature. \n";
                $("#valddlItemCategory").html("Select Nature");
            }
            if (document.getElementById('<%=txtItem_Type.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Item Group. \n";
                $("#valtxtItem_Type").html("Enter Item Group");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                        document.querySelector('.popup-wrapper').style.display = 'block';
                        return true;  
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Edit") {
                    
                        document.querySelector('.popup-wrapper').style.display = 'block';
                        return true;
                }
            }
        }
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

