<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREnquiry.aspx.cs" Inherits="mis_HR_HREnquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">New Enquiry</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Name of the accused/ आरोपित अधिकारी का नाम<span style="color: red;">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlEmployee" CssClass="form-control" ClientIDMode="Static">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlEmployee" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Order No /आदेश क्र<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtOrderNo" runat="server" placeholder="Enter Order No (ऑर्डर नं )..." class="form-control" Onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                        <small><span id="valtxtOrderNo" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Order Date /आदेश दिनांक<span style="color: red;">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtOrderDate" runat="server" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" PlaceHolder="Select From Date..." ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                        <small><span id="valtxtOrderDate" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label>Title (शीर्षक )<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtTitle" runat="server" TextMode="MultiLine" placeholder="Enter Title (शीर्षक )..." class="form-control" Onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                        <small><span id="valtxtTitle" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Document 1 (Only 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX')</label>
                                        <asp:Label ID="lblSuppDoc1" runat="server" Text="" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                        <asp:FileUpload ID="txtDocument" CssClass="form-control" runat="server" ClientIDMode="Static" Onchange="uploadDoc(),ValidateFileSize(this,1024*1024)" />
                                        <small><span id="valtxtDocument" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Description (विवरण)<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="4" placeholder="Enter Description (विवरण)..." class="form-control" Onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                        <small><span id="valtxtDescription" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label>&nbsp;</label>
                                    <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Save" ID="btnSave" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                                </div>
                                <div class="col-md-10">
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="EnquiryID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                <asp:BoundField DataField="Enquiry_OrderNo" HeaderText="Enquiry OrderNo" />
                                                <asp:BoundField DataField="Enquiry_OrderDate" HeaderText="Enquiry Order Date" />
                                                <asp:BoundField DataField="Enquiry_Title" HeaderText="Enquiry Title" />
                                                <asp:TemplateField HeaderText="Enquiry Document">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="Attachment" Target="_blank" runat="server" NavigateUrl='<%# Eval("Enquiry_Document").ToString() != "" ? "../HR/UploadDoc/" + Eval("Enquiry_Document") : "" %>' Text='<%# Eval("Enquiry_Document").ToString() != "" ? "VIEW" : "NA" %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Enquiry_Description" HeaderText="Description" />
                                                <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Select" runat="server" CssClass="label badge bg-blue" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="label label-info" ToolTip='<% #Eval("EnquiryID")%>' OnClick="LinkButton1_Click">Add Officer</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="ViewModal" class="modal fade" role="dialog">
                            <div class="modal-dialog">

                                <!-- Modal content-->
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <h4 class="modal-title">Add Officer</h4>
                                        <asp:Label ID="lblMsg1" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="modal-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="box-body">
                                                    <div class="row">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>जाँच अधिकारी<span style="color: red;"> *</span></label>
                                                                <asp:DropDownList runat="server" ID="ddlInquiryofficer" CssClass="form-control " ClientIDMode="Static">
                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <small><span id="valInquiryofficer" class="text-danger"></span></small>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label>प्रस्तुतकर्ता अधिकारी<span style="color: red;"> *</span></label>
                                                                <asp:DropDownList runat="server" ID="ddlPresentingofficer" CssClass="form-control " ClientIDMode="Static">
                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <small><span id="valddlPresentingofficer" class="text-danger"></span></small>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:Button ID="btnAdd" CssClass="btn btn-block btn-success" runat="server" Text="Add" OnClientClick="return validateform();" OnClick="btnAdd_Click" />
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Button ID="btnCancel" CssClass="btn btn-block btn-default" runat="server" Text="Cancel" />
                                                        </div>
                                                        <div class="col-md-6"></div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:GridView ID="GridView2" DataKeyNames="AddOfficerID" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateColumns="false" Width="100%" OnRowDeleting="GridView2_RowDeleting">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="50">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="AddInquiryofficer" HeaderText="Add Inquiry Officer" />
                                                                    <asp:BoundField DataField="AddPresentingOfficer" HeaderText="Add Presenting Officer" />
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="Delete" runat="server" CausesValidation="False" CommandName="Delete" CssClass="label label-danger" Text="DELETE" OnClientClick="return confirm('Job Detail will be deleted. Are you sure want to continue?');"></asp:LinkButton>
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
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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
    <script>

        function validateform() {
            var msg = "";
            $("#valddlEmployee").html("");
            $("#valtxtOrderNo").html("");
            $("#valtxtOrderDate").html("");
            $("#valtxtTitle").html("");
            $("#valtxtDescription").html("");
            $("#valtxtDocument").html("");
         


            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
                $("#valddlEmployee").html("Select Employee Name.");
            }
            
             if (document.getElementById('<%=txtOrderNo.ClientID%>').value.trim() == "") {
                 msg = msg + "please Enter Order No. \n";
                 $("#valtxtOrderNo").html("please Enter Order No.");
             }
             if (document.getElementById('<%=txtOrderDate.ClientID%>').value.trim() == "") {
                 msg = msg + "please Select Order Date. \n";
                 $("#valtxtOrderDate").html("please Select Order Date.");
             }
             if (document.getElementById('<%=txtTitle.ClientID%>').value.trim() == "") {
                 msg = msg + "please Enter Title. \n";
                 $("#valtxtTitle").html("pleaseEnter Title.");
             }
             if (document.getElementById('<%=txtDescription.ClientID%>').value.trim() == "") {
                 msg = msg + "please Enter Description. \n";
                 $("#valtxtDescription").html("please Enter Description.");
             }

             if (msg != "") {
                 alert(msg);
                 return false;
             }
             else {
                 if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }

        function AvoidSpace(event) {
            var k = event ? event.which : window.event.keyCode;
            if (k == 32) return false;
        }

        function uploadDoc() {
            if (document.getElementById('<%=txtDocument.ClientID%>').files.length != 0) {
                var ext = $('#txtDocument').val().split('.').pop().toLowerCase();
                 if ($.inArray(ext, ['png', 'jpg', 'pdf', 'jpeg', 'gif', 'doc', 'docx']) == -1) {
                     $("#valtxtDocument").html("केवल JPEG*PNG*JPG*GIF*PDF*DOC*DOCX' दस्तावेज अपलोड करें।");
                     document.getElementById('txtDocument').value = "";
                 }
                 else {
                     $('#lblSuppDoc1').text("");
                 }
             }
             else {
                 $('#lblSuppDoc1').text("");
             }
         }
         function ValidateFileSize(a, size) {
             // 1 kb =(size=1024) 
             // 1 mb =(size=1024 * 1024 * 1) 

             var uploadcontrol = document.getElementById(a.id);
             if (uploadcontrol.files[0].size > size) {
                 alert('File size should not greater than' + size / 1024 + ' kb.');
                 document.getElementById(a.id).value = '';
                 return false;
             }
             else {
                 return true;
             }

         }
         function OnlyNumbers(event) {
             var e = event || evt; // for trans-browser compatibility
             var charCode = e.which || e.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                 return false;

             return true;
         }

        function callalert1() {
            $('#ViewModal').modal({
                backdrop: 'static',
                keyboard: false
            })
        }
    </script>
</asp:Content>

