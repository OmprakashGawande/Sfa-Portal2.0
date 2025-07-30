<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRPageforDeleteLeave.aspx.cs" Inherits="mis_HR_HRPageforDeleteLeave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Employee Leave Detail</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOffice" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlYear" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-md-3">
                            <div class="form-group">
                                <label>Leave Type</label>
                                <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Employee Name</label>
                                <asp:DropDownList ID="ddlEmpList" runat="server" CssClass="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Search" OnClientClick="return validateform();" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" style="margin-top: 23px;" href="HRPageforDeleteLeave.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" DataKeyNames="LeaveId" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" runat="server" OnRowDeleting="GridView1_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STATUS">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Eval("LeaveStatus".ToString())%>' runat="server" ID="LeaveStatus"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="EMPLOYEE NAME" />
                                        <asp:BoundField DataField="Leave_Type" HeaderText="LEAVE TYPE" />
                                        <asp:BoundField DataField="LeaveAppliedOn" HeaderText="APPLY DATE" />
                                        <asp:BoundField DataField="LeaveFromDate" HeaderText="FROM DATE" />
                                        <asp:BoundField DataField="LeaveToDate" HeaderText="TO DATE" />
                                         <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Leave will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="LeaveRemark" HeaderText="REMARK" />
                                       
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
            var msg = "";
            if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Year. \n";
            }
            if (document.getElementById('<%=ddlLeaveType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Leave Type. \n";
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
    </script>
</asp:Content>

