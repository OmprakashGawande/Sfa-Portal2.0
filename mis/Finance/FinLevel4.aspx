<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinLevel4.aspx.cs" Inherits="mis_Finance_FinLevel4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Minor Head Master  [ Level - 4 ]</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Group Head [ Level - 1 ] <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlLevel1_ID" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLevel1_ID_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Major Head [ Level-2 ] <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlLevel2_ID" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLevel2_ID_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Sub Head [ Level-3 ]<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlLevel3_ID" runat="server" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Minor Head Name<span style="color: red;">*</span></label>
                                <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Minor Head Name" ID="txtLevel4_Name" ClientIDMode="Static" autocomplete="off" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Applicable On HO<span style="color: red;">*</span></label>
                                <asp:RadioButtonList ID="ddlLevel3_IsAppOnHO" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True">Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>

                <div class="row">
                    <div class="col-md-2">
                        <div class="form-group">
                            <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" runat="server" Text="Accept" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <a class="btn btn-block btn-default" href="FinLevel4.aspx">Clear</a>
                        </div>
                    </div>
                    <div class="col-md-2"></div>
                </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" PageSize="50" runat="server" ShowHeaderWhenEmpty="true" class="table table-hover table-bordered pagination-ys table-striped" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="Level4_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Level4_ID").ToString()%>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Level1_Name" HeaderText="Group Head Name" />
                                    <asp:BoundField DataField="Level2_Name" HeaderText="Major Head Name" />
                                    <asp:BoundField DataField="Level3_Name" HeaderText="Sub Head Name" />
                                    <asp:BoundField DataField="Level4_Name" HeaderText="Minor Head Name" />
                                    <asp:BoundField DataField="Level4_IsAppOnHO" HeaderText="Is Applicable on Head Office" />
                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Department will be deleted. Are you sure want to continue?');"></asp:LinkButton>
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
</asp:Content>

