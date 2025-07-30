<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpTransferHistory.aspx.cs" Inherits="mis_HR_HREmpTransferHistory" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Transfer History</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:Label ID="lblMsg2" runat="server" Text="" style="color:red; font-size: 15px;"></asp:Label>
                                <asp:GridView ID="GridView1" class="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" AutoGenerateColumns="False" runat="server" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="OrderNo" HeaderText="Order No." />
                                        <asp:BoundField DataField="OrderDate" HeaderText="Order Date" />
                                        <asp:BoundField DataField="OldOffice_Name" HeaderText="Existing Office Name" />
                                        <asp:BoundField DataField="OldDesignation_Name" HeaderText="Existing Designation Name" />
                                        <asp:BoundField DataField="OldPostingDate" HeaderText="Existing Posting Date" />
                                        <asp:BoundField DataField="NewOffice_Name" HeaderText="New Office Name" />
                                        <asp:BoundField DataField="NewDesignation_Name" HeaderText="New Designation Name" />
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>





