<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Enquiry_Reply.aspx.cs" Inherits="mis_HR_Enquiry_Reply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <section class="content">
            <div class="row">
                <div class="col-md-6">

                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Inquiry Details</h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Inquiry Description</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="false" class="table table-striped table-bordered">
                                            <Fields>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("Enquiry_Status").ToString()%>' runat="server" ID="Enquiry_Status"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Enquiry_OrderNo" HeaderText="Order No" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Enquiry_OrderDate" HeaderText="Order Date" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Enquiry_Title" HeaderText="Title" HeaderStyle-Font-Bold="true" />

                                            </Fields>
                                        </asp:DetailsView>
                                        <div class="form-group">
                                            <label>Application_Descritption</label>
                                            <div id="Enquiry_Description" runat="server" style="word-wrap: break-word; text-align: justify">
                                            </div>
                                            <div class="pull-right">
                                                <div class="form-group">
                                                    <asp:HyperLink ID="hyprEnquiry_Document" Visible="true" runat="server" CommandName="View" Text="Attachment 1 " Target="_blank"></asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label2">Reply History</h3>
                        </div>
                        <div class="box-body">
                             <div class="row">
                                <div class="col-md-12">
                                    <div class="direct-chat-messages" style="height: 100%;">
                                        <div id="dvChat" runat="server"></div>
                                        <!--For Chat-->
                                    </div>
                                </div>
                            </div>
                           <div id="DivReply" runat="server">
                                <fieldset>
                                <legend>Reply</legend>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Status<span style="color: red;"> *</span></label>
                                            <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Open</asp:ListItem>
                                                <asp:ListItem>Close</asp:ListItem>
                                            </asp:DropDownList>
                                            <small><span id="valddlStatus" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Document(JPEG*PNG*JPG*PDF*DOC)</label>
                                            <asp:Label ID="lblFUReply_SuppDoc" runat="server" Text="" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                            <asp:FileUpload ID="FUReply_SuppDoc" CssClass="form-control" runat="server" ClientIDMode="Static" onchange="uploadDoc(),ValidateFileSize(this,1024*1024)" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Remark(टिप्पणी)</label><span style="color: red">*</span>
                                            <asp:TextBox runat="server" placeholder="Remark(टिप्पणी)" ID="txtRemark" MaxLength="25" CssClass="form-control" ClientIDMode="Static" TextMode="MultiLine"></asp:TextBox>
                                            <small><span id="valtxtRemark" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Button ID="BtnReply" CssClass="btn btn-block btn-success" runat="server" Text="Reply" OnClick="BtnReply_Click" OnClientClick="return validateform();" />
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
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
            $("#valddlStatus").html("");
            $("#valtxtRemark").html("");
            if (document.getElementById('<%=ddlStatus.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Status Type. \n";
                $("#valddlStatus").html("Select Status Type.");
            }
            if (document.getElementById('<%=txtRemark.ClientID%>').value.trim() == "") {
                msg = msg + "please Enter Remark. \n";
                $("#valtxtRemark").html("please Enter Remark.");
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=BtnReply.ClientID%>').value.trim() == "Reply") {
                     if (confirm("Do you really want to Save Details ?")) {
                         return true;
                     }
                     else {
                         return false;
                     }
                 }
             }
         }
    </script>
</asp:Content>

