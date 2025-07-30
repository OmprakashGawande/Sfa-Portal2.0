<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Enquiry_List.aspx.cs" Inherits="mis_HR_Enquiry_List_" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Enquiry List </h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblMsg2" runat="server" Text="" Visible="true" Style="color: red; font-size: 17px;"></asp:Label>
                                        <asp:GridView ID="GridView1" DataKeyNames="EnquiryID" runat="server" AutoGenerateColumns="False" class="datatable  table table-hover table-bordered pagination-ys">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("Enquiry_Status").ToString()%>' runat="server" ID="Enquiry_Status"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                <asp:BoundField DataField="Enquiry_OrderNo" HeaderText="Enquiry OrderNo" />
                                                <asp:BoundField DataField="Enquiry_OrderDate" HeaderText="Enquiry Order Date" />
                                                <asp:BoundField DataField="Enquiry_Title" HeaderText="Enquiry Title" />
                                                <asp:TemplateField HeaderText="Enquiry Document">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="Attachment" Target="_blank" runat="server" NavigateUrl='<%# Eval("Enquiry_Document").ToString() != "" ? "../HR/UploadDoc/" + Eval("Enquiry_Document") : "" %>' Text='<%# Eval("Enquiry_Document").ToString() != "" ? "VIEW" : "NA" %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Enquiry_Description" HeaderText="Description" />

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <a id="a1" runat="server" style="label label-default" target="_blank" href='<%# "Enquiry_Reply.aspx?EnquiryID=" +APIProcedure.Client_Encrypt(Eval("EnquiryID").ToString()) %>'>View More</a>
                                                        <%--  <asp:HyperLink ID="hylnkViewDetail" runat="server" CssClass="label label-default" Text="View More" NavigateUrl='<%# APIProcedure.Client_Encrypt(Eval("GrvApplication_ID", "View_Status.aspx?GrvApplication_ID={0}")) %>'></asp:HyperLink>--%>
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
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

