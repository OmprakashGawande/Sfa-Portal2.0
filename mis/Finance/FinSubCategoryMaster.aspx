<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinSubCategoryMaster.aspx.cs" Inherits="mis_Finance_FinSubCategoryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .checkboxes {
            text-align: center;
        }

            .checkboxes input {
                margin: 0px 0px 0px 0px;
            }

            .checkboxes label {
                margin: 0px 20px 0px 3px;
            }
			   .select2 {
            width: 100% !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Sub Category Master</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Sub Category<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Sub Category" ID="txtSubCategory" MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                                <small><span id="valtxtSubCategory" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Button ID="btnAdd" Style="margin-top: 20px;" runat="server" CssClass="btn btn-default" Text="Add" OnClick="btnAdd_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GvCategory" runat="server" CssClass="datatable table table-bordered" AutoGenerateColumns="false" OnRowCommand="GvCategory_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Eval("Status").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SubCategoryName" HeaderText="Sub Category" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" CommandName="EditRecord" CommandArgument='<%# Eval("SubCategoryId") %>' runat="server" CssClass="label label-default" OnClientClick="return confirm('Are you sure want to edit this record?')">Edit</asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelete" CommandName="DeleteRecord" CommandArgument='<%# Eval("SubCategoryId") %>' runat="server" CssClass="label label-danger" OnClientClick="return confirm('Are you sure want to delete this record?')">Delete</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <div class="modal fade" id="SubCategory" role="dialog" data-backdrop="false">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Sub-Category Mapping</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <asp:Label ID="lblModalMsg" runat="server"></asp:Label>
                    </div>
                    <div class="row" id="div" runat="server">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Sub Category</label>
                                <asp:TextBox ID="txtModalSubCategory" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Category<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Opening Date<span style="color: red;"> *</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtOpeningDate" data-date-end-date="0d" placeholder="DD/MM/YYYY" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Opening Balance<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" ID="txtOpeningBalance" CssClass="form-control" MaxLength="12" AutoComplete="off" onkeypress="return validateDec(this,event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Type<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="Cr">Credit</asp:ListItem>
                                    <asp:ListItem Value="Dr">Debit</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Button ID="btnMAdd" Style="margin-top: 20px;" runat="server" CssClass="btn btn-default" Text="Add" OnClick="btnMAdd_Click" OnClientClick="return validateModalform();" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView runat="server" CssClass="datatable table table-bordered" ShowHeaderWhenEmpty="true" ID="GvSubCategory" OnRowCommand="GvSubCategory_RowCommand" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="S. NO.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNo" runat="server" Text='<%#Container.DataItemIndex +1 %>'></asp:Label>
                                            <asp:Label ID="lblStatus" runat="server" Visible="false" Text='<%# Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCategoryId" Visible="false" runat="server" Text='<%# Bind("CategoryId") %>'></asp:Label>
                                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Bind("CategoryName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpeningDate" runat="server" Text='<%# Bind("OpeningDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpeningBalanceShow" runat="server" Text='<%# Bind("OpeningBalanceShow") %>'></asp:Label>
                                            <asp:Label ID="lblOpeningBalance" Visible="false" runat="server" Text='<%# Bind("OpeningBalance") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="RecordEdit" CommandArgument='<%# Eval("RowNo") %>' Style="color: blue;"><i class="fa fa-edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="RecordDelete" CommandArgument='<%# Eval("RowNo") %>'  Visible='<%# Eval("Status").ToString() == "True" ? false : true  %>' Style="color: red;"><i class="fa fa-trash"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnFinalSubmit" runat="server" CssClass="btn btn-primary" Text="Final Submit" OnClick="btnFinalSubmit_Click" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script>
        function ShowSubCategoryModal() {
            $('#SubCategory').modal('show');
        }
        function validateform() {
            debugger;
            var msg = "";
            $("#valtxtSubCategory").html("");

            if (document.getElementById('<%=txtSubCategory.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Sub Category . \n";
                $("#valtxtSubCategory").html("Enter Sub Category ");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnAdd.ClientID%>').value.trim() == "Save") {
                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true;
                }
            }
        }


        function validateModalform() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=ddlCategory.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Category . \n";
            }
            if (document.getElementById('<%=txtOpeningDate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Opening Date . \n";
            }
            if (document.getElementById('<%=txtOpeningBalance.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Opening Balance . \n";
            }
            if (document.getElementById('<%=ddlType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Type . \n";
            }
           
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnMAdd.ClientID%>').value.trim() == "Save") {
                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true;
                }
            }
        }
    </script>

  <script src="../js/ValidationJs.js"></script>

    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>
    <script src="js/dataTables.buttons.min.js"></script>
    <script src="js/buttons.flash.min.js"></script>
    <script src="js/jszip.min.js"></script>
    <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>
    <script src="js/buttons.html5.min.js"></script>
    <script src="js/buttons.print.min.js"></script>
    <script src="js/buttons.colVis.min.js"></script>
    <script src="js/fromkeycode.js"></script>

    <script>
        $('.datatable').DataTable({
            paging: false,
            dom: 'Bfrtip',
            ordering: false,
            buttons: [
                //{
                //    extend: 'colvis',
                //    collectionLayout: 'fixed two-column',
                //    text: '<i class="fa fa-eye"></i> Columns'
                //},
                //{
                //    extend: 'print',
                //    text: '<i class="fa fa-print"></i> Print',
                //    title: $('h1').text(),
                //    footer: true,
                //    autoPrint: true
                //},
               {
                   extend: 'excel',
                   text: '<i class="fa fa-file-excel-o"></i> Excel',
                   title: $('h1').text(),
                   exportOptions: {
                       columns: [0, 1]
                   },
                   footer: true
               }

            ]
        });
    </script>
</asp:Content>

