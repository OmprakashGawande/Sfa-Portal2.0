<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRPunishmentHistory.aspx.cs" Inherits="mis_HR_HRPunishmentHistory" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Punishment History</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:Label ID="lblMsg2" runat="server" Text="" style="color:red; font-size: 15px;"></asp:Label>
                                <asp:GridView ID="GridView1" class="table table-striped table-hover table-bordered" ShowHeaderWhenEmpty="true"  EmptyDataText="No Record Found" AutoGenerateColumns="False" runat="server" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PunishmentSuspension" HeaderText="Punishment Suspension" />
                                        <asp:BoundField DataField="Punishment_Type" HeaderText="Punishment Type" />
                                        <asp:BoundField DataField="Punishment_Status" HeaderText="Punishment Status" />
                                        <asp:BoundField DataField="From_Date" HeaderText="From Date" />
                                        <asp:BoundField DataField="End_Date" HeaderText="End Date" />
                                        <asp:BoundField DataField="Title" HeaderText="Title" />
                                        <asp:BoundField DataField="Description" HeaderText="Description" />   
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



