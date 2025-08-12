<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProjectWiseModuleProgress.aspx.cs" Inherits="mis_Master_ProjectWiseModuleProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12">
                <div class="row justify-content-center mt-3">
                    <div class="col-md-12 justify-content-center">
                        <asp:Label runat="server" ID="lblMsg" Text=""></asp:Label>
                    </div>
                </div>
                <div class="card">
                    <div class="card-header">
                        <h4>Project Wise Module Progress</h4>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-xl-4 col-sm-6 position-relative">
                                <div class="form-group">
                                    <span class="fa-pull-right">
                                        <asp:RequiredFieldValidator ID="RFV1" ValidationGroup="a"
                                            ErrorMessage="Select To Date" ForeColor="Red"
                                            Text="<i class='fa fa-exclamation-circle' title='Enter Project Name !'></i>"
                                            ControlToValidate="ddlProjectName" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <label runat="server">PROJECT NAME <span style="color: red;">*</span></label>
                                    <asp:DropDownList runat="server" ID="ddlProjectName" ClientIDMode="Static" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true"
                                        CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xl-4 col-sm-6 position-relative">
                                <div class="form-group">
                                    <span class="fa-pull-right">
                                        <asp:RequiredFieldValidator ID="RFV2" ValidationGroup="a"
                                            ErrorMessage="Select Module" ForeColor="Red"
                                            Text="<i class='fa fa-exclamation-circle' title='Select Module!'></i>"
                                            ControlToValidate="ddlProjectModule" InitialValue="0" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <label runat="server">MODULE <span style="color: red;">*</span></label>
                                    <asp:DropDownList runat="server" ID="ddlProjectModule" ClientIDMode="Static"
                                        CssClass="form-control select2">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-xl-4 col-sm-6 position-relative">
                                <div class="form-group">
                                    <span class="fa-pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            ErrorMessage="Select Module Phase" ForeColor="Red"
                                            Text="<i class='fa fa-exclamation-circle' title='Select Module Phase!'></i>"
                                            ControlToValidate="ddlPhase" InitialValue="0" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <label runat="server">MODULE PHASE <span style="color: red;">*</span></label>
                                    <asp:DropDownList runat="server" ID="ddlPhase" ClientIDMode="Static"
                                        CssClass="form-control select2">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                        <asp:ListItem Value="1">Development Phase</asp:ListItem>
                                        <asp:ListItem Value="2">Testing Phase</asp:ListItem>
                                        <asp:ListItem Value="3">Production Phase</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                        <div class="row mt-2">
                            <div class="col-xl-12 position-relative">
                                <div class="form-group">
                                    <label>REMARK</label>

                                    <textarea
                                        id="txtRemark"
                                        runat="server"
                                        class="form-control"
                                        oninput="autoResizeTextarea(this)"
                                        placeholder="Enter Remark" maxlength="150"></textarea>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xl-3 position-relative col-sm-6">
                                <div class="form-group">
                                    <asp:Button runat="server" Style="margin-top: 22px;" CssClass="btn btn-block btn-outline-success" ID="btnSave" Text="Save" ValidationGroup="a" OnClick="btnSave_Click" />
                                    <a href="ProjectWiseModuleProgress.aspx" style="margin-top: 22px;" class="btn btn-block btn-outline-danger">Clear</a>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="card-header">
                        <h4>Detail</h4>
                    </div>
                    <div class="card-body">
                        <div class="row" id="div1" runat="server">
                            <div class="table-responsive">
                                <div class="col-md-12">
                                    <asp:GridView ID="Grid" PageSize="50" runat="server" class="datatable  table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="false" AutoGenerateColumns="False" OnRowCommand="Grid_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PROJECT NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProjectName" Text='<%# Eval("ProjectName").ToString() %>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblProjectId" Text='<%# Eval("ProjectId").ToString() %>' Visible="false" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MODULE NAME ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTModuleName" Text='<%# Eval("ModuleName").ToString() %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MODULE PHASE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPhase" Text='<%# Eval("PhaseName").ToString() %>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblPhaseId" Text='<%# Eval("ModulePhase").ToString() %>' runat="server" Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REMARK ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemark" Text='<%# Eval("Remark").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="MODULE STATUS UPDATE DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDate" Text='<%# Eval("Date").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField> --%>
                                            <asp:TemplateField HeaderText="ACTION">
                                                <ItemTemplate>
                                                    <asp:LinkButton
                                                        ID="lnkEdit"
                                                        runat="server"
                                                        CssClass="btn btn-outline-info"
                                                        CommandArgument='<%# Eval("ModulePhaseId").ToString() %>'
                                                        CausesValidation="False"
                                                        CommandName="EditRecord"
                                                        Text="Edit">
                                                    </asp:LinkButton>
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
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>
        $(document).ready(function () {
            initCustomDataTable('.datatable', 'Project Wise Module Progress', 'Project Wise Module Progress');
        });
    </script>
</asp:Content>

