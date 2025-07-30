<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="TrnMainPowerRequestApprovByManager.aspx.cs" Inherits="mis_Transaction_TrnMainPowerRequestApprovByManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
     <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                 
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row" style="padding: 0px 9px 2px 15px;" id="div1" runat="server">
                                        <div>
                                            <h4 style="margin-left: 2rem;">MAN POWER REQUEST APPROVE DETAIL </h4>
                                        </div>
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
                                                        <asp:TemplateField HeaderText="ACTION">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkbox" runat="server" CssClass="chkSelect" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                                            <asp:HiddenField ID="hdnManPowerReqId" Value='<%# Eval("ManPowerReqId") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                        <div class="row" id="DivBtn" runat="server" style="padding: 0px 9px 2px 15px;" visible="false">
                                            <hr />
                                            <div class="col-md-1">
                                                <div class="form-group">
                                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" CssClass="btn btn-success btn-block" />
                                                </div>
                                            </div>
                                            <div class="col-md-1">
                                                <div class="form-group">
                                                    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-danger btn-block" OnClick="btnReject_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>
                            <br />

                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

