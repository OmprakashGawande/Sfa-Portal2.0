<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpWiseLeaveDetail.aspx.cs" Inherits="mis_HR_HREmpWiseLeaveDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <div class="card mt-3  border-warning">
                    <div class="card-header">
                        <h4>Applied Leave detail</h4>
                    </div>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    <div class="card-body">
                        <div class="row g-4">
                            <div class="col-xl-4 col-sm-6 position-relative">
                                <div class="form-group">
                                    <label>Leave Year</label>
                                    <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row g-4 mt-2">
                            <div class="table-responsive dt-ext ">
                                <asp:GridView ID="GridView1" DataKeyNames="LeaveId" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave Status" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Eval("LeaveStatus".ToString())%>' runat="server" ID="lbLeaveStatus"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="LeaveDay" HeaderText="Leave Day" />
                                        <asp:BoundField DataField="ApprovalAuthority" HeaderText="Approval Authority" />
                                        <asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />
                                        <asp:BoundField DataField="LeaveAppliedOn" HeaderText="Applied Date" />
                                        <asp:BoundField DataField="LeaveFromDate" HeaderText="From Date" />
                                        <asp:BoundField DataField="LeaveToDate" HeaderText="To Date" />
                                        <asp:TemplateField HeaderText="View More" ShowHeader="False" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="ViewMore" CssClass="label label-info" runat="server" CommandName="Select">View More</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lblMsg2" runat="server" Text="" Style="color: red; font-size: 15px;"></asp:Label>
                            </div>
                        </div>
                    </div>


                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Leave Remark</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <fieldset>
                                                <legend>Leave Request</legend>
                                                <div class="table-responsive">
                                                    <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateRows="false">
                                                        <Fields>
                                                            <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                            <asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />
                                                            <asp:BoundField DataField="LeaveFromDate" HeaderText="From Date" />
                                                            <asp:BoundField DataField="LeaveToDate" HeaderText="To Date" />
                                                            <asp:TemplateField HeaderText="Leave Request Doc" ItemStyle-Width="70%">
                                                                <ItemTemplate>
                                                                    <a href='<%# Eval("LeaveDocument") %>' target="_blank" class="btn btn-info btn-sm"><%# Eval("LeaveDocument").ToString() != "" ? "View" : "" %></a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Fields>
                                                    </asp:DetailsView>
                                                    <div class="form-group">
                                                        <asp:Label ID="lblr" runat="server" Text="छुट्टी का कारण (Reason Of Leave)"></asp:Label>
                                                    </div>
                                                    <div class="form-group">

                                                        <div id="LeaveReason" runat="server">
                                                            <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <fieldset>
                                                <legend>Reply from Approval Authority </legend>
                                                <div class="table-responsive">
                                                    <asp:DetailsView ID="DetailsView2" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateRows="false">
                                                        <Fields>
                                                            <asp:TemplateField HeaderText="Leave Status" ItemStyle-Width="70%">
                                                                <ItemTemplate>
                                                                    <asp:Label Text='<%# Eval("LeaveStatus".ToString())%>' runat="server" ID="lbLeaveStatus"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="LeaveApprovalOrderNo" HeaderText="Order No" />
                                                            <asp:BoundField DataField="LeaveApprovalOrderDate" HeaderText="Order Date" />
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblDocHeader" runat="server" Text='Doc'></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <a href='<%# Eval("LeaveApprovalOrderFile") %>' target="_blank" class="label label-info"><%# Eval("LeaveApprovalOrderFile").ToString() != "" ? "View" : "" %></a>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Fields>
                                                    </asp:DetailsView>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label1" runat="server" Text="Remark/Comment"></asp:Label>
                                                    </div>
                                                    <div class="form-group">
                                                        <div id="HRRemark" runat="server">
                                                            <asp:TextBox ID="txtRemarkByHR" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </fieldset>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function callalert() {
            $("#myModal").modal('show');
        }
    </script>
</asp:Content>

