<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminAllOffice.aspx.cs" Inherits="mis_Admin_AdminAllOffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Office List</h3>
                </div>
                <asp:label id="lblMsg" runat="server" text=""></asp:label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:label id="lblMsg2" runat="server" text="" style="color: red; font-size: 15px;"></asp:label>
                                <asp:gridview id="GridView1" class="table table-striped table-hover table-bordered" showheaderwhenempty="true" autogeneratecolumns="False" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Office_Name" HeaderText="OFFICE NAME" />
                                        <asp:BoundField DataField="Office_Code" HeaderText="OFFICE CODE" />
                                        <asp:BoundField DataField="Office_ContactNo" HeaderText="CONTACT NO" />
                                        <asp:BoundField DataField="Office_Email" HeaderText="EMAIL" />
                                        <asp:BoundField DataField="OfficeType_Title" HeaderText="OFFICE TYPE TITLE" />
                                    </Columns>
                                </asp:gridview>
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

