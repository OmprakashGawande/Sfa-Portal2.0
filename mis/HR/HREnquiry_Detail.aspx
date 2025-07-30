<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREnquiry_Detail.aspx.cs" Inherits="mis_HR_HREnquiry_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <section class="content">
            <div class="row">
                <div class="col-md-6">

                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Inquiry Details</h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Inquiry Description</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="false" class="table table-striped table-bordered">
                                            <Fields>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("Enquiry_Status").ToString()%>' runat="server" ID="Enquiry_Status"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Enquiry_OrderNo" HeaderText="Order No" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Enquiry_OrderDate" HeaderText="Order Date" HeaderStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Enquiry_Title" HeaderText="Title" HeaderStyle-Font-Bold="true" />

                                            </Fields>
                                        </asp:DetailsView>
                                        <div class="form-group">
                                            <label>Application_Descritption</label>
                                            <div id="Enquiry_Description" runat="server" style="word-wrap: break-word; text-align: justify">
                                            </div>
                                            <div class="pull-right">
                                                <div class="form-group">
                                                    <asp:HyperLink ID="hyprEnquiry_Document" Visible="true" runat="server" CommandName="View" Text="Attachment 1 " Target="_blank"></asp:HyperLink>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title">Department Reply</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <!-- DIRECT CHAT -->
                                    <!-- Conversations are loaded here -->
                                    <div class="direct-chat-messages" style="height: 100%;">
                                        <div id="dvChat" runat="server"></div>
                                        <asp:Label ID="lblDepartmentRecord" runat="server" ClientIDMode="Static"></asp:Label>
                                        <!--For Chat-->
                                    </div>
                                    <!-- /.box-body -->
                                    <!-- /.box-footer-->
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

