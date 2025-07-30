<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ManPowerRequestStatus.aspx.cs" Inherits="mis_Report_ManPowerRequestStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .label-success {
            color: #fff;
            background-color: green;
        }

        .label-danger {
            color: #fff;
            background-color: red;
        }

        .label-pending {
            color: #000;
            background-color: #ffc107; /* yellow */
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">MAN POWER REQUEST STATUS</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>EMPLOYEE NAME </label>
                                                <asp:DropDownList runat="server" ID="ddlEmployee" ClientIDMode="Static"
                                                    CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>REQUEST TYPE</label>
                                                <asp:DropDownList runat="server" ID="ddlForwardedto" ClientIDMode="Static"
                                                    CssClass="form-control select2">
                                                    <asp:ListItem Value="0">ALL</asp:ListItem>
                                                    <asp:ListItem Value="HR">HR</asp:ListItem>
                                                    <asp:ListItem Value="Manager">Manager</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Button runat="server" Style="margin-top: 22px;" CssClass="btn btn-block btn-success" ID="btnSearch" Text="Search" ValidationGroup="a" OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <a href="ManPowerRequestStatus.aspx" style="margin-top: 22px;" class="btn btn-block btn-default">Clear</a>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding: 0px 9px 2px 15px;" id="div1" runat="server">

                                        <div class="table-responsive">
                                            <div class="col-md-12">
                                                <asp:GridView ID="Grid" PageSize="50" runat="server" class="datatable  table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="false"
                                                    EmptyDataText="NO RECORD FOUND" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ManPowerReqId").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REQUEST BY ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequestBy" Text='<%# Eval("RequestBy").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="REQUEST DATE ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequestDate" Text='<%# Eval("RequestDate").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REQUEST TYPE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblForwardedto" Text='<%# Eval("Forwardedto").ToString() %>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REQUEST TO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblManagerName" Text='<%# Eval("ManagerName").ToString() %>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CURRENT PROJECT NAME ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCurrentProjectName" Text='<%# Eval("CurrentProjectName").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REQUESTED PROJECT NAME ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequestedProjectName" Text='<%# Eval("RequestedProjectName").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmployeeName" Text='<%# Eval("EmpName").ToString() %>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DESIGNATION">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesignation" Text='<%# Eval("Designation_Name").ToString() %>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FROM DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFromDate" Text='<%# Eval("FromDate").ToString() %>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TO DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblToDate" Text='<%# Eval("ToDate").ToString() %>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="REQUEST STATUS">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblForwardedto"
                                                                    Text='<%# Eval("RequestStatus").ToString() %>'
                                                                    CssClass='<%# Eval("RequestStatus").ToString() == "Pending" ? "label label-pending" : 
                                                                                  Eval("RequestStatus").ToString() == "Approve" ? "label label-success" : 
                                                                                  Eval("RequestStatus").ToString() == "Reject" ? "label label-danger" : 
                                                                                  "label" %>'
                                                                    runat="server" />


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
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

