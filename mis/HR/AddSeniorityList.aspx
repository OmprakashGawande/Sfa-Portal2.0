<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AddSeniorityList.aspx.cs" Inherits="mis_HR_AddSeniorityList" %>

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
     <div class="loader"></div>
    <%--Confirmation Modal Start --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>

                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../image/question-circle.png" width="30" />&nbsp;&nbsp; 
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <!-- SELECT2 EXAMPLE -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title" style="margin-top:5px;">&nbsp;&nbsp;&nbsp;&nbsp;Add Seniority List (वरिष्ठता सूची)</h3>
                     <div class="pull-left">
                        <asp:Hyperlink runat="server" NavigateUrl="SeniorityList.aspx" ToolTip="Back to Seniority List" class="btn label-warning"><i class="fa fa-arrow-left"> Back</i></asp:Hyperlink>
                    </div>
                </div>

                <!-- /.box-header -->
                <div class="box-body">

                    <div class="row">
                        <div class="col-lg-12">
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                         <div class="col-md-4">
                            <div class="form-group">
                                <label>Designation(पद)<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="Dynamic" ControlToValidate="ddlDesignation" ValidationGroup="a" InitialValue="0" ErrorMessage="Select Designation" Text="<i class='fa fa-exclamation-circle' title='Select Designation !'></i>"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlDesignation" OnInit="ddlDesignation_Init" runat="server" Width="100%" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Year (वर्ष)<span style="color: red;">*</span></label>
                               
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtYear" ErrorMessage="Enter Year" Text="<i class='fa fa-exclamation-circle' title='Enter Year !'></i>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtYear" ErrorMessage="Invalid Year" Text="<i class='fa fa-exclamation-circle' title='Invalid Year !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]\d*$"></asp:RegularExpressionValidator>
                                </span>
                                    <asp:TextBox ID="txtYear" Width="100%" onkeypress="return validateNum(event)" MaxLength="4" placeholder="Enter Year" runat="server" autocomplete="off" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                

                            </div>
                        </div>
                        <div class="col-md-4">
                                <div class="form-group">
                                    <label>Upload List<span class="hindi">(सूची अपलोड करें)</span><span id="pnlupload" runat="server" style="color: red;">*</span></label>
                                    <span class="pull-right">
                                         <asp:RequiredFieldValidator ID="rfvupload" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="fuSenorilitylistCopy" ErrorMessage="Upload List." Text="<i class='fa fa-exclamation-circle' title='Upload List !'></i>"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="CustomValidator1" OnServerValidate="ValidateFileSize" ControlToValidate="fuSenorilitylistCopy" Display="Dynamic" SetFocusOnError="true" runat="server" />
                                         <asp:CustomValidator ID="custom1" ValidationGroup="Mstr" ClientValidationFunction="ValidateSizeOrType" ErrorMessage="" ControlToValidate="fuSenorilitylistCopy" Display="Dynamic" SetFocusOnError="true" runat="server" />
                                    </span>
                                    <asp:FileUpload ID="fuSenorilitylistCopy" AllowMultiple="false" ClientIDMode="Static" CssClass="form-control" runat="server" />
                                    <asp:HyperLink ID="hypListCopy" Visible="false" Target="_blank" Text="View List Copy" ToolTip="Click to download List Copy!" runat="server"></asp:HyperLink>
                                </div>
                            </div>
                      
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button Text="Save" ID="btnSubmit" CssClass="btn btn-block btn-success" ValidationGroup="a" runat="server" OnClientClick="return ValidatePage();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" CssClass="btn btn-block btn-default" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive rover">
                                <asp:GridView ID="GridView1" Width="100%" ShowHeaderWhenEmpty="true" DataKeyNames="EmpSenorityList_id" EmptyDataText="No records Found" runat="server" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.<br />(क्रं.)" HeaderStyle-Width="10px">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Year <br />(वर्ष)" HeaderStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSLYear" runat="server" Text='<%# Eval("SLYear") %>'></asp:Label>
                                                <asp:Label ID="lblDesignation_ID" Visible="false" runat="server" Text='<%# Eval("Designation_ID") %>'></asp:Label>
                                                <asp:Label ID="lblSLDoc" Visible="false" runat="server" Text='<%# Eval("SLDoc") %>'></asp:Label>
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
                                        <asp:TemplateField HeaderText="Action <br />(कार्य)">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("EmpSenorityList_id") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("EmpSenorityList_id") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- /.box-body -->
            </div>
            <!-- /.box -->
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
        $(document).ready(function () {
            $('.loader').fadeOut();
        });

        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }

        $('.select2').select2();


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
        function ValidateSizeOrType(sender, args) {
            var array = ['pdf', 'jpeg', 'jpg', 'png', 'PDF', 'JPEG', 'JPG', 'PNG'];

            var uploadControl = document.getElementById('<%= fuSenorilitylistCopy.ClientID %>');

            var Extension = uploadControl.value.substring(uploadControl.value.lastIndexOf('.') + 1);

            if (array.indexOf(Extension) <= -1) {

                sender.innerHTML = "<i class='fa fa-exclamation-circle' title='Invalid file , Only pdf,jpg,jpeg,png file allowed. !'></i>";
                alert('Invalid file , Only pdf,jpg,jpeg,png file allowed. !');
                args.IsValid = false;
                $('#fuSenorilitylistCopy').val('');

            }
            else {
                if (uploadControl.files[0].size > 512000) {
                    args.IsValid = false;
                    sender.innerHTML = "<i class='fa fa-exclamation-circle' title='File size must le less than equal to 512 KB. !'></i>";
                    alert('File size must le less than equal to 512 KB.');
                    $('#fuSenorilitylistCopy').val('');
                }
                else {
                    args.IsValid = true;
                }

            }
        };
    </script>
</asp:Content>

