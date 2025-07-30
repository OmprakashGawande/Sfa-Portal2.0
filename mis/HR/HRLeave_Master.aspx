<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRLeave_Master.aspx.cs" Inherits="mis_HR_HRLeave_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Leave Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Year<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlFinancial_Year" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancial_Year_SelectedIndexChanged">
                                            <asp:ListItem>select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Leave Type<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlLeave_Type" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLeave_Type_SelectedIndexChanged">
                                             <asp:ListItem>select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Leave Days<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtLeave_Days" runat="server" CssClass="form-control" placeholder="Enter Leave Days..." onkeypress="return validateNum(event);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" runat="server" Text="Save" OnClientClick="return validateform();" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <a class="btn btn-block btn-default" href="HRLeave_Master.aspx">Clear</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GridView1" DataKeyNames="LeaveMaster_ID" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Financial_Year" HeaderText="Year" />
                                            <asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />
                                            <asp:BoundField DataField="Leave_Days" HeaderText="Leave Days" />
                                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                                    <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Leave Type will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
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
            var msg = "";
            if (document.getElementById('<%=ddlFinancial_Year.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Year. \n";
            }
           
            if (document.getElementById('<%=ddlLeave_Type.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select LeaveType. \n";
            }
            if (document.getElementById('<%=txtLeave_Days.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Leave Days. \n";
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

