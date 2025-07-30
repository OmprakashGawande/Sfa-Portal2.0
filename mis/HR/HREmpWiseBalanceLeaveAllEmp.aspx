<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpWiseBalanceLeaveAllEmp.aspx.cs" Inherits="mis_HR_HREmpWiseBalanceLeaveAllEmp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .lblInfo {
            font-weight: 600;
            font-size: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Officers Balance Leave</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Financial Year</label>
                                <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged"></asp:DropDownList>
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
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success btn-block" Text="Search" OnClick="btnSearch_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:Label ID="lblInfo" runat="server" Text="" CssClass="lblInfo"></asp:Label>
                                <asp:GridView ID="GridView1" class="table table-hover table-bordered" AutoGenerateColumns="False" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                      <%--  <asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />--%>
                                        <asp:BoundField DataField="LeaveOpening" HeaderText="Opening As On 1st Jan Of Selected Year" />
                                        <asp:BoundField DataField="Leave_Days" HeaderText="Leave Credit For Selected Year" />
                                        <asp:BoundField DataField="TotalTakenLeave" HeaderText="Total Taken Leave (By ERP)" />
                                        <%--<asp:BoundField DataField="RemainingLeave" HeaderText="Balance Leave" />--%>
                                        <asp:BoundField DataField="TotalRemainingLeaves" HeaderText="Total Leave Balance Till Date" />
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lblMsg2" runat="server" Text="" Style="color: red; font-size: 15px;"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>



