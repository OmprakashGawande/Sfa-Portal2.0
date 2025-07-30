<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinCategoryMaster.aspx.cs" Inherits="mis_Finance_FinCategoryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        @media print {
            .Hiderow, .main-footer, .dt-buttons, .dataTables_filter, .dataTables_info {
                display: none;
            }

            .box-title {
                text-align: center;
            }

            .box, .box-header {
                border: none;
            }
        }

        .btn-default {
            background-color: #e6e6e6 !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">

                    <h3 class="box-title">Category Master</h3>

                </div>
                <a class="btn btn-default pull-right Hiderow" onclick="window.print()">Print</a>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                <div class="box-body">

                    <div class="row Hiderow">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Category<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Category" ID="txtCategory" MaxLength="100" ClientIDMode="Static"></asp:TextBox>
                                <small><span id="valtxtCategory" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" Style="margin-top: 20px;" CssClass="btn btn-block btn-success" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a href="FinCategoryMaster.aspx" style="margin-top: 20px;" class="btn btn-block btn-default">Clear</a>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                            </div>
                            <asp:GridView ID="GvCategory" runat="server" CssClass="datatable table table-bordered" AutoGenerateColumns="false" OnRowCommand="GvCategory_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="Hiderow" ItemStyle-CssClass="Hiderow" FooterStyle-CssClass="Hiderow">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" CommandName="EditRecord" CommandArgument='<%# Eval("CategoryId") %>' runat="server" CssClass="label label-default" OnClientClick="return confirm('Are you sure want to edit this record?')">Edit</asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelete" CommandName="DeleteRecord" CommandArgument='<%# Eval("CategoryId") %>' runat="server" CssClass="label label-danger" OnClientClick="return confirm('Are you sure want to delete this record?')">Delete</asp:LinkButton>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function validateform() {
            debugger;
            var msg = "";
            $("#valtxtCategory").html("");

            if (document.getElementById('<%=txtCategory.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Category . \n";
                $("#valtxtCategory").html("Enter Category ");
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
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {

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

