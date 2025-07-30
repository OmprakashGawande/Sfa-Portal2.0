<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" Culture = "en-GB" CodeFile="AddPromotionHistory.aspx.cs" Inherits="mis_HR_AddPromotionHistory" %>

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
                    <h3 class="box-title" style="margin-top:5px;">&nbsp;&nbsp;&nbsp;&nbsp;Add Employee Promotion</h3>
                     <div class="pull-left">
                        <asp:Hyperlink runat="server" NavigateUrl="HREmpPromotionAllHistory.aspx" ToolTip="Back to Employee Promotion" class="btn label-warning"><i class="fa fa-arrow-left"> Back</i></asp:Hyperlink>
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
                                <label>Employee Name(कर्मचारी का नाम)</label>
                                <span style="color: red">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ValidationGroup="a" Display="Dynamic" ControlToValidate="ddlEmployye_Name" ErrorMessage="Select Employee Name" Text="<i class='fa fa-exclamation-circle' title='Select Employee Name !'></i>"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlEmployye_Name" runat="server" OnInit="ddlEmployye_Name_Init" CssClass="form-control select2" OnSelectedIndexChanged="ddlEmployye_Name_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                       
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
                                <label>Department (विभाग)<span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ControlToValidate="ddlDepartment" ValidationGroup="a" InitialValue="0" ErrorMessage="Select Designation" Text="<i class='fa fa-exclamation-circle' title='Select Designation !'></i>"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlDepartment" OnInit="ddlDepartment_Init" runat="server" Width="100%" CssClass="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Date Of Birth (जन्म तिथि) </label>
                                <span style="color: red">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtDOB" ErrorMessage="Enter Date of Birth" Text="<i class='fa fa-exclamation-circle' title='Enter Date of Birth !'></i>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revdate" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDOB" ErrorMessage="Invalid Date of Birth" Text="<i class='fa fa-exclamation-circle' title='Invalid Date of Birth !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                </span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtDOB" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date Of Birth" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                         <div class="col-md-4">
                            <div class="form-group">
                                <label>Date of Retirement(सेवानिवृत्ति की तिथि) </label>
                                <span style="color: red">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="txtDOR" ErrorMessage="Enter Date of Retirement." Text="<i class='fa fa-exclamation-circle' title='Enter Date of Retirement !'></i>"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" runat="server" Display="Dynamic" ControlToValidate="txtDOR" ErrorMessage="Invalid Date of Retirement" Text="<i class='fa fa-exclamation-circle' title='Invalid Date of Retirement !'></i>" SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                    <asp:CompareValidator ID="CompareValidator1" ValidationGroup = "a" ForeColor = "Red" runat="server" Display="Dynamic" ControlToValidate = "txtDOB" ControlToCompare = "txtDOR" Operator = "LessThan" Type = "Date" ErrorMessage="Date of Birth must be less than Date of Retirement." Text="<i class='fa fa-exclamation-circle' title='Date of Birth must be less than Date of Retirement !'></i>"></asp:CompareValidator>
                                </span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtDOR" onkeypress="javascript: return false;" Width="100%" MaxLength="10" onpaste="return false;" placeholder="Date of Retirement" runat="server" autocomplete="off" CssClass="form-control" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                          <div class="col-md-4">
                                <div class="form-group">
                                    <label>Current Posting<span class="hindi">(वर्तमान पोस्टिंग)</span><span style="color: red;"> *</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a" runat="server" ControlToValidate="ddlOffice" InitialValue="0" ErrorMessage="Select Current Posting." Text="<i class='fa fa-exclamation-circle' title='Select Current Posting !'></i>"></asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList CssClass="form-control select2" ID="ddlOffice" OnInit="ddlOffice_Init" runat="server"></asp:DropDownList>

                                </div>
                            </div>
                        <div class="col-md-4">
                                <div class="form-group">
                                    <label>Upload Service Book<span class="hindi">(सेवा विवरण अपलोड करें)</span><span id="pnlupload" runat="server" style="color: red;">*</span></label>
                                    <span class="pull-right">
                                         <asp:RequiredFieldValidator ID="rfvupload" runat="server" ValidationGroup="a" Display="Dynamic" ControlToValidate="fuPromotionlistCopy" ErrorMessage="Upload Service Book." Text="<i class='fa fa-exclamation-circle' title='Upload Service Book !'></i>"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="CustomValidator1" OnServerValidate="ValidateFileSize" ControlToValidate="fuPromotionlistCopy" Display="Dynamic" SetFocusOnError="true" runat="server" />
                                         <asp:CustomValidator ID="custom1" ValidationGroup="Mstr" ClientValidationFunction="ValidateSizeOrType" ErrorMessage="" ControlToValidate="fuPromotionlistCopy" Display="Dynamic" SetFocusOnError="true" runat="server" />
                                    </span>
                                    <asp:FileUpload ID="fuPromotionlistCopy" AllowMultiple="false" ClientIDMode="Static" CssClass="form-control" runat="server" />
                                    <asp:HyperLink ID="hypListCopy" Visible="false" Target="_blank" ClientIDMode="Static" Text="View List Copy" ToolTip="Click to download List Copy!" runat="server"></asp:HyperLink>
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
                                <asp:GridView ID="GridView1" Width="100%" ShowHeaderWhenEmpty="true" DataKeyNames="EmpPromotionHistory_id" EmptyDataText="No records Found" runat="server" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.<br />(क्रं.)" HeaderStyle-Width="10px">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp_Name <br />(वर्ष)" HeaderStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmp_Name" runat="server" Text='<%# Eval("Emp_Name") %>'></asp:Label>
                                                <asp:Label ID="lblDepartment_ID" Visible="false" runat="server" Text='<%# Eval("Department_ID") %>'></asp:Label>
                                                <asp:Label ID="lblDesignation_ID" Visible="false" runat="server" Text='<%# Eval("Designation_ID") %>'></asp:Label>
                                                <asp:Label ID="lblCurrentPosting" Visible="false" runat="server" Text='<%# Eval("CurrentPosting") %>'></asp:Label>
                                                <asp:Label ID="lblEmp_ID" Visible="false" runat="server" Text='<%# Eval("Emp_ID") %>'></asp:Label>
                                                <asp:Label ID="lblServiceBook" Visible="false" runat="server" Text='<%# Eval("ServiceBook") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation <br />(पद)">
                                            <ItemTemplate>
                                                <%# Eval("Designation_Name") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Department  <br />(विभाग)">
                                            <ItemTemplate>
                                                <%# Eval("Department_Name") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Current Posting <br />(वर्तमान पोस्टिंग)">
                                            <ItemTemplate>
                                                <%# Eval("Office_Name") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date Of Birth <br />(जन्म तिथि)">
                                            <ItemTemplate>
                                              <asp:Label ID="lblDOB" runat="server" Text='<%# Eval("DOB","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date of Retirement <br />(सेवानिवृत्ति की तिथि)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOR" runat="server" Text='<%# Eval("DOR","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Service Book <br /> (सेवा विवरण)">
                                            <ItemTemplate>
                                                <center>
                                                <asp:HyperLink ID="hylimageview" runat="server" ToolTip="Click to View Promotion List" ForeColor="DeepSkyBlue" NavigateUrl='<%#"../HR/UploadDoc/Promotion_Details/" + Eval("ServiceBook") %>' Target="_blank"><i class="fa fa-picture-o"></i></asp:HyperLink>
                                               </center
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action <br />(कार्य)">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("EmpPromotionHistory_id") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("EmpPromotionHistory_id") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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

            var uploadControl = document.getElementById('<%= fuPromotionlistCopy.ClientID %>');

            var Extension = uploadControl.value.substring(uploadControl.value.lastIndexOf('.') + 1);

            if (array.indexOf(Extension) <= -1) {

                sender.innerHTML = "<i class='fa fa-exclamation-circle' title='Invalid file , Only pdf,jpg,jpeg,png file allowed. !'></i>";
                alert('Invalid file , Only pdf,jpg,jpeg,png file allowed. !');
                args.IsValid = false;
                $('#fuPromotionlistCopy').val('');

            }
            else {
                if (uploadControl.files[0].size > 512000) {
                    args.IsValid = false;
                    sender.innerHTML = "<i class='fa fa-exclamation-circle' title='File size must le less than equal to 512 KB. !'></i>";
                    alert('File size must le less than equal to 512 KB.');
                    $('#fuPromotionlistCopy').val('');
                }
                else {
                    args.IsValid = true;
                }

            }
        };
    </script>
</asp:Content>

