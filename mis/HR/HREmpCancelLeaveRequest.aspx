<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpCancelLeaveRequest.aspx.cs" Inherits="mis_HR_HREmpCancelLeaveRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Approve Leaves</h3>
                </div>
                <div class="box-body">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    <div class="row">

                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Financial Year</label>
                                <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:Label ID="lblMsg2" runat="server" Text="" Style="color: red; font-size: 15px;"></asp:Label>
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
                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Office_Name" HeaderText="Office Name" />
                                        <asp:BoundField DataField="LeaveAppliedOn" HeaderText="Applied Date" />
                                        <asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />
                                        <asp:BoundField DataField="LeaveFromDate" HeaderText="From Date" />
                                        <asp:BoundField DataField="LeaveToDate" HeaderText="To Date" />
                                        <asp:TemplateField HeaderText="Action" ShowHeader="False" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="ViewMore" CssClass="label label-info" runat="server" CommandName="Select">Cancel Leave</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Leave Approval</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateRows="false">
                                                <Fields>
                                                    <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />
                                                    <asp:BoundField DataField="LeaveFromDate" HeaderText="From Date" />
                                                    <asp:BoundField DataField="LeaveToDate" HeaderText="To Date" />
                                                    <asp:TemplateField HeaderText="Leave Related Doc" ItemStyle-Width="70%">
                                                        <ItemTemplate>
                                                            <a href='<%# Eval("LeaveDocument") %>' target="_blank" class="label label-info"><%# Eval("LeaveDocument").ToString() != "" ? "View" : "" %></a>
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
                                    </div>
                                </div>
                                <fieldset>
                                    <legend>HR Reply Section</legend>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:CheckBox ID="chkCancel" ClientIDMode="Static" runat="server" Text="Cancel" ForeColor="Red" />
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Remark<span style="color: red;">*</span></label>
                                                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="3" placeholder="Enter Remark..." class="form-control" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnCancel" class="btn btn-success" runat="server" Text="Cancel" OnClick="btnCancel_Click" OnClientClick="return validateLeaveForm();" />
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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
        function callalert() {
            $("#myModal").modal('show');
        }

        function validateLeaveForm() {
            var msg = "";
            debugger;
            var chk = document.getElementById("chkCancel");
            if (chk != null) {
                if (chk.checked == false) {
                    msg = msg + "Please check Status \n";
                }
            }
            if (document.getElementById('<%=txtRemark.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Remark \n";
            }

            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (confirm("Do you really want to Save Detail.?")) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }
    </script>
</asp:Content>

