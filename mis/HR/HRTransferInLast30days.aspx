<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRTransferInLast30days.aspx.cs" Inherits="mis_HR_HRTransferInLast30days" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Last 30 Days Transfer List</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RELIEVING STATUS">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Eval("RelievingStatus".ToString())%>' runat="server" ID="FilePriority"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="JOINING STATUS">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Eval("TransferStatus".ToString())%>' runat="server" ID="FilePriority"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="OrderNo" HeaderText="ORDER NO" />
                                        <asp:BoundField DataField="OrderDate" HeaderText="ORDER DATE" />
                                        <asp:BoundField DataField="Emp_Name" HeaderText="EMPLOYEE NAME" />
                                        <asp:BoundField DataField="OldOffice_Name" HeaderText="OLD OFFFICE NAME" />
                                        <asp:BoundField DataField="OldDesignation_Name" HeaderText="OLD DESIGNATION" />
                                        <asp:BoundField DataField="OldDepartment" HeaderText="OLD DEPARTMENT" />
                                        <asp:BoundField DataField="OldPostingDate" HeaderText="OLD POSTING DATE" />
                                        <asp:BoundField DataField="NewOffice_Name" HeaderText="OFFICE NAME" />
                                        <asp:BoundField DataField="NewDesignation_Name" HeaderText="NEW DESIGNATION" />
                                        <asp:BoundField DataField="NewDepartment" HeaderText="NEW DEPARTMENT" />
                                        <asp:BoundField DataField="NewEffectiveDate" HeaderText="EFFECTIVE DATE" />
                                        <asp:BoundField DataField="Remark" HeaderText="REMARK" />
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

