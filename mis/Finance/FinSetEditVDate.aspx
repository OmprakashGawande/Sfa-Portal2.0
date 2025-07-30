<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinSetEditVDate.aspx.cs" Inherits="mis_Finance_FinSetEditVDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">SET EDIT VOUCHER DATE</h3>
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" ShowHeaderWhenEmpty="true" EmptyDataText="Record Not Found!" EmptyDataRowStyle-ForeColor="Red" class="table table-bordered" AutoGenerateColumns="False" runat="server">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="10" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" ToolTip='<%# Eval("Office_ID") %>' Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Office_Name" HeaderText="OFFICE NAME" />
                                                <asp:TemplateField HeaderText="Date" ItemStyle-Width="30%">
                                                    <ItemTemplate>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtEditDate" runat="server" placeholder="Select Date" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false"></asp:TextBox>
                                                            
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="req1" ValidationGroup="a"  Display="Dynamic" runat="server" ErrorMessage="Date cannot be Blank." ControlToValidate="txtEditDate" Text="Date cannot be Blank"></asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label style="text-align: right;">BULK UPDATE</label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtBulkDate" runat="server" placeholder="Select Bulk Date" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static" OnTextChanged="txtBulkDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" ValidationGroup="a" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
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
     <script>

         function validateform() {
             var msg = "";
            <%--var price = document.getElementById('<%=((TextBox)GridView1.FooterRow.FindControl("txtprice")).ClientID %>');--%>
            
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Save Details ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script>
</asp:Content>

