<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminBlockOld.aspx.cs" Inherits="mis_Admin_AdminBlock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Block Master</h3>
                </div>
                <asp:label id="lblMsg" runat="server" text=""></asp:label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>State Name<span style="color: red;"> *</span></label>
                                <asp:dropdownlist runat="server" id="ddlState_Name" cssclass="form-control select2" autopostback="true" clientidmode="Static" onselectedindexchanged="ddlState_Name_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:dropdownlist>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Division Name<span style="color: red;"> *</span></label>
                                <asp:dropdownlist runat="server" id="ddlDivision_Name" cssclass="form-control" autopostback="true" onselectedindexchanged="ddlDivisionName_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:dropdownlist>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>District Name<span style="color: red;"> *</span></label>
                                <asp:dropdownlist runat="server" id="ddlDistrict_Name" cssclass="form-control" autopostback="true">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:dropdownlist>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Block Name<span style="color: red;">*</span></label>
                                <asp:textbox id="txtBlock_Name" runat="server" placeholder="Enter Block Name..." class="form-control" maxlength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:textbox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:button id="btnSave" cssclass="btn btn-block btn-success" runat="server" text="Save" onclick="btnSave_Click" onclientclick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" href="AdminBlock.aspx">Clear</a>
                            </div>
                        </div>
                        <div class="col-md-2"></div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:gridview id="GridView1" pagesize="50" runat="server" class="table table-hover table-bordered pagination-ys" autogeneratecolumns="False" allowpaging="True" datakeynames="Block_ID" onpageindexchanging="GridView1_PageIndexChanging" onselectedindexchanged="GridView1_SelectedIndexChanged">
                               <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Block_ID").ToString()%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="State_Name" HeaderText="State Name" />
                                    <asp:BoundField DataField="Division_Name" HeaderText="Division Name" />
                                    <asp:BoundField DataField="District_Name" HeaderText="District Name" />
                                   <asp:BoundField DataField="Block_Name" HeaderText="Block Name" />
                                    <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30" HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" ToolTip='<%# Eval("Block_ID").ToString()%>' Checked='<%# Eval("Block_IsActive").ToString()=="1" ? true : false %>' AutoPostBack="true" />
                                        </ItemTemplate>
                                        <ItemStyle Width="30px"></ItemStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:gridview>
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
            var msg = "";
            if (document.getElementById('<%=ddlState_Name.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select State Name. \n";
            }
            if (document.getElementById('<%=ddlDivision_Name.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Division Name. \n";
            }
            if (document.getElementById('<%=ddlDistrict_Name.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select District Name. \n";
            }
            if (document.getElementById('<%=txtBlock_Name.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Block Name. \n";
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
    </script>
</asp:Content>

