<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinLevel3.aspx.cs" Inherits="mis_Finance_FinLevel2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Sub Head Master  [ Level - 3 ]</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Group Head [ Level-1 ]<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlLevel1_ID" runat="server" CssClass="form-control" ClientIDMode="Static" OnSelectedIndexChanged="ddlLevel1_ID_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Major Head [ Level-2 ]<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlLevel2_ID" runat="server" ClientIDMode="Static" CssClass="form-control" OnSelectedIndexChanged="ddlLevel2_ID_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Sub Head Name <span style="color: red;">*</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Sub Head Name" ID="txtLevel3_Name" ClientIDMode="Static" autocomplete="off" onkeypress='javascript:tbx_fnAlphaOnly(event, this);' MaxLength="100" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Is Applicable on HO <span style="color: red;">*</span></label>
                                        <asp:RadioButtonList ID="ddlLevel3_IsAppOnHO" runat="server" RepeatDirection="Horizontal" ClientIDMode="Static">
                                            <asp:ListItem Selected="True">Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSave" Text="Accept" OnClick="btnSave_Click" OnClientClick="return validateform()" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="FinLevel3.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblRecordMsg" runat="server" Text="" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                    <asp:GridView ID="GridView1" DataKeyNames="Level3_ID" AutoGenerateColumns="False" runat="server" CssClass="table table-bordered table-hover table-responsive table-striped"
                                        ClientIDMode="Static" ShowHeaderWhenEmpty="true" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="50">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Level1_Name" HeaderText="Group Head ( Level-1 )" />
                                            <asp:BoundField DataField="Level2_Name" HeaderText="Major Head ( Level-2 )" />
                                            <asp:BoundField DataField="Level3_Name" HeaderText="Sub Head Name" />
                                            <asp:BoundField DataField="Level3_IsAppOnHO" HeaderText="Is Applicable on HO" />
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Delete" runat="server" CausesValidation="False" CommandName="Delete" CssClass="label label-danger" Text="DELETE" OnClientClick="return confirm('Sub Head Name will be deleted. Are you sure want to continue?');"></asp:LinkButton>
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
        function tbx_fnAlphaOnly(e, cntrl) {
            if (!e) e = window.event; if (e.charCode) {
                if (e.charCode < 65 || (e.charCode > 90 && e.charCode < 97) || e.charCode > 122)
                { if (e.charCode != 95 && e.charCode != 32) { if (e.preventDefault) { e.preventDefault(); } } }
            } else if (e.keyCode) {
                if (e.keyCode < 65 || (e.keyCode > 90 && e.keyCode < 97) || e.keyCode > 122)
                { if (e.keyCode != 95 && e.keyCode != 32) { try { e.keyCode = 0; } catch (e) { } } }
            }
        }


        function validateform() {
            debugger
            var msg = "";
            if (document.getElementById("ddlLevel1_ID").selectedIndex == 0) {
                msg += "Select Level-1 Name\n";
            }
            if (document.getElementById("ddlLevel2_ID").selectedIndex == 0) {
                msg += "Select Level-2 Name\n";
            }
            if (document.getElementById('<%=txtLevel3_Name.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Level-3 Name. \n";
            }
            if (document.getElementById("ddlLevel3_IsAppOnHO").selectedIndex == 0) {
                msg += "Select Applicable On HO\n";
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

