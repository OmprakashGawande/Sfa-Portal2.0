<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpPromotionHistory.aspx.cs" Inherits="mis_HR_HREmpPromotionHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Promotion History</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:Label ID="lblMsg2" runat="server" Text="" Style="color: red; font-size: 15px;"></asp:Label>
                                <asp:GridView ID="GridView1" class="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="OrderNo" HeaderText="Order No" />
                                        <asp:BoundField DataField="OrderDate" HeaderText="Order Date" />
                                        <asp:BoundField DataField="OldLevel_Name" HeaderText="Existing Level" />
                                        <asp:BoundField DataField="OldBasicSalery" HeaderText="Existing Basic Salary" />
                                        <asp:BoundField DataField="NewLevel_Name" HeaderText="New Level" />
                                        <asp:BoundField DataField="NewBasicSalery" HeaderText="New Basic Salary" />
                                        <asp:BoundField DataField="NewEffectiveDate" HeaderText="New Effective Date" />
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
</asp:Content>



