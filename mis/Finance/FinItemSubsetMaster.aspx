<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinItemSubsetMaster.aspx.cs" Inherits="mis_Finance_FinItemSubsetMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Item/Set Subset Master</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Set Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlItem_Id" runat="server" class="form-control select2" OnSelectedIndexChanged="ddlItem_Id_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Subset Name<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtSubsetName" runat="server" placeholder="Enter Subset Name..." class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Subset Size/Quantity<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtSubsetSize" runat="server" placeholder="Enter Subset Size/Quantity..." class="form-control"></asp:TextBox>
                            </div>
                        </div>

                         <div class="col-md-3">
                            <div class="form-group">
                                <label>Subset Unit<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtSubsetUnit" runat="server" placeholder="Enter Subset Unit..." class="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys" AutoGenerateColumns="False" DataKeyNames="Ingredient_Id" OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Set Name" />
                                    <asp:BoundField DataField="Ingredient_Name" HeaderText="Subset Name" />
                                    <asp:BoundField DataField="Ingredient_Size" HeaderText="Subset Size/Quantity" />
                                    <asp:BoundField DataField="Ingredient_Unit" HeaderText="Subset Unit" />
                                    <%-- <asp:BoundField DataField="ItemOrderBy" HeaderText="Order By" />--%>

                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit" Enabled='<%# Eval("Status").ToString() == "A" ? true : false %>'></asp:LinkButton>
                                            <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Subset will be deleted. Are you sure want to continue?');" Enabled='<%# Eval("Status").ToString() == "A" ? true : false %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
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
            if (document.getElementById('<%=txtSubsetName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Subset Name. \n";
            }
            if (document.getElementById('<%=ddlItem_Id.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Main Set. \n";
            }
            if (document.getElementById('<%=txtSubsetSize.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Subset Size. \n";
            }
            if (document.getElementById('<%=txtSubsetUnit.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Subset Unit. \n";
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
