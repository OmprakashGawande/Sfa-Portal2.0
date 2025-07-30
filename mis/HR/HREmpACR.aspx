<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpACR.aspx.cs" Inherits="mis_HR_HREmpACR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Upload Annual Confidential Report(ACR)</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlOldOffice" runat="server" class="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Employee Name<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Department<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" class="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Designation<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlDesignation" runat="server" class="form-control" Enabled="false"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Year<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlYear" runat="server" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>From Date<span style="color: red;">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>To Date<span style="color: red;">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Attach Document<span style="color: red;">*</span></label>
                                        <asp:FileUpload ID="Doc_Upload" CssClass="form-control" runat="server" ClientIDMode="Static" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*XLS*XLSX*DOC*DOCX', this),ValidateFileSize(1024)" />
                                        <span style="font-size:10px; color:blue;">Only JPEG/PNG/JPG/PDF/DOC/DOCX Formats are allowed.<br /> Maximum Allowed Filesize (1MB)
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Remark<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="3" placeholder="Enter Remark..." class="form-control" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Save" OnClientClick="return validateform();" OnClick="btnSave_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" style="margin-top: 23px;" href="HREmpACR.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" DataKeyNames="ACR_ID" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Department_Name" HeaderText="Department" />
                                        <asp:BoundField DataField="Designation_Name" HeaderText="Designation" />
                                        <asp:BoundField DataField="Year" HeaderText="Year" />
                                        <asp:BoundField DataField="From_Date" HeaderText="From Date" />
                                        <asp:BoundField DataField="To_Date" HeaderText="To Date" />
                                        <asp:BoundField DataField="Remark" HeaderText="Remark" />
                                        <asp:TemplateField HeaderText="Uploaded Document">
                                            <ItemTemplate>
                                                <a href='<%# Eval("Attach_Doc") %>' target="_blank" class="label label-info"><%# Eval("Attach_Doc").ToString() != "" ? "View" : "NA" %></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">

        function validateform() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=ddlOldOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Old Office. \n";
            }
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }
            if (document.getElementById('<%=ddlDepartment.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Department. \n";
            }
            if (document.getElementById('<%=ddlDesignation.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Designation. \n";
            }
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Year. \n";
            }
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select From Date.\n";
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg += "Select To Date. \n";
            }
            if (document.getElementById('<%=Doc_Upload.ClientID%>').value.trim() == "") {
                msg += "select File. \n";
            }
            if (document.getElementById('<%=txtRemark.ClientID%>').value.trim() == "") {
                msg += "Enter Remark. \n";
            }
            if (msg != "") {
                alert(msg);
                return false
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
    </script>
</asp:Content>

