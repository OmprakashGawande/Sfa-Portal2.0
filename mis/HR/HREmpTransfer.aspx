<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpTransfer.aspx.cs" Inherits="mis_HR_HREmpTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">कर्मचारी स्थानांतरण / Employee Transfer</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>आदेश नंबर / Order No.<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtOrderNo" runat="server" placeholder="Order No..." class="form-control" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>आदेश तारीख / Order Date<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtOrderDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" data-date-end-date="0d" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>प्रभावी तारीख  / Effective Date<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtEffectiveDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Current Office Location</legend>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>कार्यालय / Office <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlOldOfficeName" runat="server" class="form-control select2" OnSelectedIndexChanged="ddlOldOfficeName_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>कर्मचारी का नाम / Employee Name<span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>श्रेणी / Class<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtClass" runat="server" placeholder="Select Date..." class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>पोस्टिंग तारीख  / Posting Date<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtEmp_PostingDate" runat="server" placeholder="Select Date..." class="form-control" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>पद / Designation  <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlOldDesignation" runat="server" class="form-control" Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>विभाग / Department  <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlOldDepartment" runat="server" class="form-control" Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>New Office Location</legend>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>कार्यालय / Office<span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlNewOfficeName" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>पद / Designation  <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlNewDesignation" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>विभाग / Department  <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlNewDepartment" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>टिप्पणी / Remark<span style="color: red;">*</span></label>
                                            <asp:TextBox ID="txtRemark" runat="server" placeholder="Enter Remark..." class="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" style="margin-top: 23px;" href="HREmpTransfer.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered table-striped pagination-ys" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="TransferID" OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="OrderNo" HeaderText="Order No." />
                                    <asp:BoundField DataField="OrderDate" HeaderText="Order Date" />
                                    <asp:BoundField DataField="OldOffice_Name" HeaderText="Existing Office Name" />
                                    <asp:BoundField DataField="OldDesignation_Name" HeaderText="Existing Designation Name" />
                                    <asp:BoundField DataField="OldDepartment" HeaderText="Existing Department Name" />
                                    <asp:BoundField DataField="OldPostingDate" HeaderText="Existing Posting Date" />
                                    <asp:BoundField DataField="NewOffice_Name" HeaderText="New Office Name" />
                                    <asp:BoundField DataField="NewDesignation_Name" HeaderText="New Designation Name" />
                                    <asp:BoundField DataField="NewDepartment" HeaderText="New Department Name" />
                                    <asp:BoundField DataField="NewEffectiveDate" HeaderText="New Effective Date" />

                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnRelieved" runat="server" CssClass="label label-info" CausesValidation="False" CommandName="Select" Text="Confirm Relieving" OnClick="btnRelieved_Click"></asp:LinkButton>
                                            <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                            <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>


            </div>
            <!-- The Modal -->
            <div class="modal fade" id="ModalRelieve">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Employee Relieve</h4>
                            <button type="button" class="close" data-dismiss="modal">×</button>
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Relieving Date<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtRelievingDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnRelieve" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Relieve" OnClick="btnRelieve_Click" OnClientClick="return validateRelieving();" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a class="btn btn-block btn-default" style="margin-top: 23px;" data-dismiss="modal">Cancel</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Modal footer -->
                        <div class="modal-footer">
                            <%--<button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>--%>
                        </div>

                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function ShowModal() {
            $("#ModalRelieve").modal();
        }
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=txtOrderNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Order No. \n";
            }
            if (document.getElementById('<%=txtOrderDate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Order Date. \n";
            }
            if (document.getElementById('<%=ddlOldOfficeName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Current Office. \n";
            }
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }
            if (document.getElementById('<%=txtEmp_PostingDate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Posting Date. \n";
            }
            if (document.getElementById('<%=ddlOldDesignation.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Current Designation. \n";
            }
            if (document.getElementById('<%=ddlOldDepartment.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Current Department. \n";
            }
            if (document.getElementById('<%=ddlNewOfficeName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select New Office. \n";
            }
            if (document.getElementById('<%=txtEffectiveDate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Effective Date. \n";
            }
            if (document.getElementById('<%=txtClass.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Class. \n";
            }
            if (document.getElementById('<%=ddlNewDesignation.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select New Designation. \n";
            }
            if (document.getElementById('<%=ddlNewDepartment.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select New Department. \n";
            }
            if (document.getElementById('<%=txtRemark.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Remark. \n";
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
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Edit") {
                    if (confirm("Do you really want to Edit Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }

                }
            }
        }
        function validateRelieving() {
            var msg = "";

            if (document.getElementById('<%=txtRelievingDate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Relieving Date. \n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Relieve Employee ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script>
</asp:Content>
