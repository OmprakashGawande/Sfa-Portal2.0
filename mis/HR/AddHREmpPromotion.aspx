<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AddHREmpPromotion.aspx.cs" Inherits="mis_HR_AddHREmpPromotion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title" style="margin-top: 5px;">&nbsp;&nbsp;&nbsp;&nbsp;  Add Employee Promotion / कर्मचारी पदोन्नति</h3>
                    <div class="pull-left">
                        <asp:HyperLink runat="server" NavigateUrl="HREmpPromotion.aspx" ToolTip="Back to Record" class="btn label-warning"><i class="fa fa-arrow-left"> Back</i></asp:HyperLink>
                    </div>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>आदेश नंबर / Order No.<%--<span style="color: red;">*</span>--%></label>
                                <asp:TextBox ID="txtOrderNo" runat="server" placeholder="Order No..." class="form-control" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>आदेश तारीख / Order Date<%--<span style="color: red;">*</span>--%></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtOrderDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>प्रभावी तारीख  / Effective Date<%--<span style="color: red;">*</span>--%></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtEffectiveDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Current Detail</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>कार्यालय / Office <%--<span style="color: red;">*</span>--%></label>
                                            <asp:DropDownList ID="ddlOldOffice" runat="server" class="form-control" OnSelectedIndexChanged="ddlOldOffice_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>कर्मचारी का नाम / Employee Name<%--<span style="color: red;">*</span>--%></label>
                                            <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>स्तर / Level<%--<span style="color: red;">*</span>--%></label>
                                            <asp:DropDownList ID="ddlOldLevel" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>मूल वेतन / Basic Salary<%--<span style="color: red;">*</span>--%></label>
                                            <asp:TextBox ID="txtOldBasicSalary" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>श्रेणी / Class<%--<span style="color: red;">*</span>--%></label>
                                            <asp:DropDownList ID="ddlOldClass" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>पद / Designation  <%--<span style="color: red;">*</span>--%></label>
                                            <asp:DropDownList ID="ddlOldDesignation" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>विभाग / Department  <%--<span style="color: red;">*</span>--%></label>
                                            <asp:DropDownList ID="ddlOldDepartment" runat="server" class="form-control">
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
                                <legend>Promotion Detail</legend>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>स्तर / Level<%--<span style="color: red;">*</span>--%></label>
                                            <asp:DropDownList ID="ddlNewLevel" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlNewLevel_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>मूल वेतन / Basic Salary<%--<span style="color: red;">*</span>--%></label>
                                            <asp:TextBox ID="txtNewBasicSalary" runat="server" CssClass="form-control"></asp:TextBox>
                                            <%--<asp:DropDownList ID="ddlNewBasicSalary" runat="server" class="form-control"></asp:DropDownList>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>विभाग / Department  <%--<span style="color: red;">*</span>--%></label>
                                            <asp:DropDownList ID="ddlNewDepartment" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>श्रेणी / Class<%--<span style="color: red;">*</span>--%></label>
                                            <asp:DropDownList ID="ddlNewClass" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlNewClass_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>पद / Designation  <%--<span style="color: red;">*</span>--%></label>
                                            <asp:DropDownList ID="ddlNewDesignation" runat="server" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                             <label>Promotion Date<%--<span style="color: red;">*</span>--%></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                            
                                            <asp:TextBox ID="txtPromotionDate" runat="server" placeholder="Select Promotion Date..." class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>टिप्पणी / Remark<%--<span style="color: red;">*</span>--%></label>
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
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Save" OnClientClick="return validateform();" OnClick="btnSave_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" style="margin-top: 23px;" href="AddHREmpPromotion.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" runat="server" OnPageIndexChanging="GridView1_PageIndexChanging" class="table table-hover table-bordered table-striped pagination-ys" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="PromotionID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="OrderNo" HeaderText="Order No" />
                                        <asp:TemplateField HeaderText="Order Date">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("OrderDate", "{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Effective Date">
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("NewEffectiveDate","{0:dd/MM/yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="OldLevel_Name" HeaderText="Existing Level" />
                                        <asp:BoundField DataField="OldBasicSalery" HeaderText="Existing Basic Salary" />
                                        <asp:BoundField DataField="OldClass" HeaderText="Old Class" />
                                        <asp:BoundField DataField="OldDepartment" HeaderText="Old Department" />
                                        <asp:BoundField DataField="OldDesignation" HeaderText="Old Designation" />
                                        <asp:BoundField DataField="NewLevel_Name" HeaderText="New Level" />
                                        <asp:BoundField DataField="NewBasicSalery" HeaderText="New Basic Salary" />
                                        <asp:BoundField DataField="NewClass" HeaderText="New Class" />
                                        <asp:BoundField DataField="NewDepartment" HeaderText="New Department" />
                                        <asp:BoundField DataField="NewDesignation" HeaderText="New Designation" />
                                        <asp:TemplateField HeaderText="Promotion Date">
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("PromotionDate","{0:dd/MM/yyyy}") %>'></asp:Label>
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
            <%--if (document.getElementById('<%=txtOrderNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Order No.\n";
            }
            if (document.getElementById('<%=txtOrderDate.ClientID%>').value.trim() == "") {
                msg += "Select Order Date. \n";
            }
            if (document.getElementById('<%=txtEffectiveDate.ClientID%>').value.trim() == "") {
                msg += "select New Effective Date. \n";
            }
            if (document.getElementById('<%=txtPromotionDate.ClientID%>').value.trim() == "") {
                msg += "select New Promotion Date. \n";
            }
            if (document.getElementById('<%=ddlOldOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }
            if (document.getElementById('<%=ddlOldLevel.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Current Level. \n";
            }
            if (document.getElementById('<%=txtOldBasicSalary.ClientID%>').value.trim() == "") {
                msg += "Enter Current Basic Salary. \n";
            }
            if (document.getElementById('<%=ddlOldClass.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Current Class. \n";
            }
            if (document.getElementById('<%=ddlOldDesignation.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Current Designation. \n";
            }
            if (document.getElementById('<%=ddlOldDepartment.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Current Department. \n";
            }
            if (document.getElementById('<%=ddlNewLevel.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select New Level. \n";
            }
            if (document.getElementById('<%=ddlNewBasicSalary.ClientID%>').value.trim() == "") {
                msg = msg + "Select New Basic Salary. \n";
            }
            if (document.getElementById('<%=ddlNewClass.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select New Class. \n";
            }
            if (document.getElementById('<%=ddlNewDesignation.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select New Designation. \n";
            }
            if (document.getElementById('<%=ddlNewDepartment.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select New Department. \n";
            }
            if (document.getElementById('<%=txtRemark.ClientID%>').value.trim() == "") {
                msg += "Enter Remark. \n";
            }--%>
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
    </script>
</asp:Content>

