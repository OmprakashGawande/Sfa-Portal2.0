<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinSupplierItemCreation.aspx.cs" Inherits="mis_Finance_FinSupplierItemCreation" %>

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
                            <h3 class="box-title">Supplier Item Creation</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Item Category<span style="color: red;"> *</span></label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlItemCategory"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Item Name<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtItemName" placeholder="Enter Item Name..." MaxLength="200" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Item Group<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlItemGroup" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Item Unit<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlItemUnit" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Item Rate<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtItemRate" placeholder="Enter Item Rate..." MaxLength="10" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Cess Applicable<span style="color: red;"> *</span><br />
                                            &nbsp;</label>
                                        <asp:DropDownList ID="ddlCessApplicable" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCessApplicable_SelectedIndexChanged">
                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="No">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            Cess Rate<span style="color: red;"> *</span><br />
                                            &nbsp;</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCessRate" placeholder="Enter Cess Rate..." onkeypress="return validateDec(this,event);" onblur="return CalculateBasicRate();" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>
                                            GST Applicable<span style="color: red;"> *</span><br />
                                            &nbsp;</label>
                                        <asp:DropDownList ID="ddlGSTApplicable" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGSTApplicable_SelectedIndexChanged">
                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                            <asp:ListItem Value="No">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2" runat="server" id="divGSTRate">
                                    <div class="form-group">
                                        <label>
                                            GST Rate<span style="color: red;"> *</span><br />
                                            &nbsp;</label>
                                        <asp:DropDownList ID="ddlGSTRate" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                            <asp:ListItem Value="0.00">0.00 %</asp:ListItem>
                                            <asp:ListItem Value="5.00">5.00 %</asp:ListItem>
                                            <asp:ListItem Value="12.00">12.00 %</asp:ListItem>
                                            <asp:ListItem Value="18.00">18.00 %</asp:ListItem>
                                            <asp:ListItem Value="28.00">28.00 %</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>GST Include in Deduction<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlGSTIncInDeduction" runat="server" CssClass="form-control" ClientIDMode="Static" onchange="return CalculateBasicRate();">
                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="No">No</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Effective Date<span style="color: red;">*</span><br />&nbsp;</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtEffectiveDate" runat="server" placeholder="Select Effective Date" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="FinSupplierItemCreation.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <h3 class="box-title">Item Details</h3>
                                    <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="ItemID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ItemID").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                            <asp:BoundField DataField="ItemGroup" HeaderText="Item Group" />
                                            <asp:BoundField DataField="ItemUnit" HeaderText="Item Unit" />
                                            <asp:BoundField DataField="CessApplicable" HeaderText="CessApplicable" />
                                            <asp:BoundField DataField="CessRate" HeaderText="Cess Rate" />
                                            <asp:BoundField DataField="GSTApplicable" HeaderText="GST Applicable" />
                                            <asp:BoundField DataField="GSTRate" HeaderText="GST Rate" />
                                            <asp:BoundField DataField="GSTIncInDeduction" HeaderText="GST Include In Deduction" />
                                            <asp:BoundField DataField="ItemRate" HeaderText="Item Rate" />
                                            <asp:BoundField DataField="EffectiveDate" HeaderText="Effective Date" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Select" runat="server" CssClass="label label-info" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
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
            if (document.getElementById('<%=ddlItemCategory.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Item Category. \n";
            }
            if (document.getElementById('<%=txtItemName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Item Name. \n";
            }
            if (document.getElementById('<%=ddlItemGroup.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Item Group. \n";
            }
            if (document.getElementById('<%=ddlItemUnit.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Item Unit. \n";
            }
            if (document.getElementById('<%=txtItemRate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Item Rate. \n";
            }
            if (document.getElementById('<%=txtEffectiveDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select Effective Date. \n";
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
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
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




