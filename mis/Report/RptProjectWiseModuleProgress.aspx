<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptProjectWiseModuleProgress.aspx.cs" Inherits="mis_Report_RptProjectWiseModuleProgress" %>

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
                        <h4>Project Wise Module Progress Report</h4>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-xl-3 col-sm-6 position-relative ">
                                <div class="form-group">
                                    <span class="fa-pull-right">
                                        <asp:RequiredFieldValidator ID="RFV1" ValidationGroup="a"
                                            ErrorMessage="Select To Date" ForeColor="Red"
                                            Text="<i class='fa fa-exclamation-circle' title='Enter Project Name !'></i>"
                                            ControlToValidate="ddlProjectName" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <label runat="server">PROJECT NAME <span style="color: red;">*</span></label>
                                    <asp:DropDownList runat="server" ID="ddlProjectName" ClientIDMode="Static" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-xl-3 col-sm-6 position-relative">
                                <div class="form-group">
                                    <asp:Button runat="server" CssClass="btn btn-outline-success " ID="btnSearch" Text="Search" ValidationGroup="a" OnClick="btnSearch_Click" />
                                    <a href="ProjectWiseModuleProgress.aspx" class="btn btn-outline-danger ">Clear</a>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <%--grid--%>
                <div class="card">
                    <div class="card-header">
                        <h4>Project Wise Module Progress Detail </h4>
                    </div>
                    <div class="card-body">

                        <div class="row" id="div1" runat="server">

                            <div class="table-responsive">
                                <div class="col-md-12">
                                    <asp:GridView ID="Grid" PageSize="50" runat="server" class="datatable  table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="false" AutoGenerateColumns="False">
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
                                            <asp:TemplateField HeaderText="MODULE STATUS UPDATED BY ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpName" Text='<%# Eval("EmpName").ToString() %>' runat="server" />
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
            initCustomDataTable('.datatable', 'Project Wise Module Progress Report', 'Project Wise Module Progress Report');
        });
    </script>










</asp:Content>

