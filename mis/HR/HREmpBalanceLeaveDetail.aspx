<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpBalanceLeaveDetail.aspx.cs" Inherits="mis_HR_HREmpBalanceLeaveDetail" %>

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
                            <h3 class="box-title" id="Label1">Employee Leave Opening Balance </h3>
							              <%--      <small>(Only Carry Forwarded Leaves to 1st January 2019)</small>--%>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label>
                                        <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Employee Name</label>
                                        <asp:DropDownList ID="ddlEmpList" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpList_SelectedIndexChanged">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Date</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="TxtDate" runat="server" class="form-control" autocomplete="off" Text="31/12/2018"></asp:TextBox>
                                            <%--<asp:TextBox ID="TxtDate" runat="server" placeholder="Select Date" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-block" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div id="DivInsertDetail" runat="server">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Leave Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave_Type" runat="server" Text='<%# Eval("Leave_Type") %>'></asp:Label>
                                                            <asp:Label ID="lblLeave_ID" runat="server" Visible="false" Text='<%# Eval("Leave_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remaining Leaves">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtLeaveDays" runat="server" CssClass="form-control" onkeypress="return validateNum(event);" placeholder="Enter Remaining Leave" autocomplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group"></div>
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-block" Text="Save" OnClick="btnSave_Click" OnClientClick="return SaveValidateform();" />
                                    </div>
                                </div>
                                <div id="DivFillDetail" runat="server">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView2" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />
                                                    <asp:BoundField DataField="LeaveDays" HeaderText="Leave Days" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
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
    <script type="text/javascript">
        function validateform() {
            debugger;
            var msg = "";

            if (document.getElementById('<%=TxtDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select Date. \n";
            }
            if (document.getElementById('<%=ddlOfficeName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office Name. \n";
            }
            if (document.getElementById('<%=ddlEmpList.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }
        function SaveValidateform() {

            if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                if (confirm("Do you really want to Save Detail ?")) {
                    return true;
                }
                else {
                    return false;
                }

            }
        }

    </script>
</asp:Content>

