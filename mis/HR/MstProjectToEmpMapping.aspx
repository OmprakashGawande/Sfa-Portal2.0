<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MstProjectToEmpMapping.aspx.cs" Inherits="mis_HR_MstProjectToEmpMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <asp:HiddenField runat="server" ID="hfProjectID" />
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Project To Employee Mapping </h3>
                        </div>
                        <hr />
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label runat="server">
                                            PROJECT NAME
                                            <label style="color: red;">*</label></label>
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                ErrorMessage="Select Project Name" InitialValue="0" ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select Project !'></i>"
                                                ControlToValidate="ddlProject" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <div class="ms form-group">
                                            <asp:DropDownList runat="server" ID="ddlProject" ClientIDMode="Static"
                                                CssClass="form-control select2">
                                                <asp:ListItem Value="0">No record found</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                ErrorMessage="Select Category" InitialValue="0" ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                ControlToValidate="ddlCategory" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <label>CATEGORY <span style="color: red;">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlCategory" ClientIDMode="Static"
                                            CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Development</asp:ListItem>
                                            <asp:ListItem Value="2">Bug Fix</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                ErrorMessage="Select Employee" InitialValue="0" ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select Employee !'></i>"
                                                ControlToValidate="ddlEmployee" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <label>EMPLOYEE <span style="color: red;">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlEmployee" ClientIDMode="Static"
                                            CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Kapil</asp:ListItem>
                                            <asp:ListItem Value="2">Ajay</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                ErrorMessage="Select Allocaton from Date" ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select Allocaton from Date !'></i>"
                                                ControlToValidate="txtAllocationFromDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <label>ALLOCATION FROM DATE<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtAllocationFromDate" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                                ErrorMessage="Select Allocaton To Date" ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select Allocaton To Date !'></i>"
                                                ControlToValidate="txtAllocationToDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <label>ALLOCATION TO DATE<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtAllocationToDate" TextMode="Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label for="txtTaskDescription">REMARK</label>
                                        <textarea
                                            id="txtRemark"
                                            runat="server"
                                            class="form-control"
                                            oninput="autoResizeTextarea(this)"
                                            onkeypress="javascript:tbx_fnAlphaOnly(event, this);"
                                            placeholder="Enter Remark"
                                            rows="2"></textarea>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="IsActive">STATUS</label>
                                        <input type="checkbox" id="IsActive" class="form-check-input" style="margin-top: 3rem;" checked="checked" />
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="BtnAdd" Text="ADD" ValidationGroup="a" />
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <a href="MstProjectManagerToEmpMapping.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gridProject" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="false" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Project_ID").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PROJECT NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProject_Name_Eng" Text='<%# Eval("Project_Name_Eng").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CATEGORY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory" Text="Development" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpName" Text="Kapil" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ALLOCATION FROM DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAllocationFromDate" Text="10/05/2025" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ALLOCATION TO DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAllocationToDate" Text="11/05/2025" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REMARK">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemark" Text="Allocate For Two Days Only" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30" HeaderText="STATUS">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkStatus" runat="server" CommandArgument='<%# Eval("Project_ID").ToString()%>' CssClass='<%# Eval("IsActive").ToString() =="True"?"label label-success":"label label-danger"  %>' CausesValidation="False" CommandName="ChangeStatus" Text='<%# Eval("IsActive").ToString() =="True"?"Active":"Deactive"  %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="30px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ACTION">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-default" CommandArgument='<%# Eval("Project_ID").ToString()%>' CausesValidation="False" CommandName="EditRecord" Text="Edit"></asp:LinkButton>
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
        </section>
    </div>
    <script>
        function autoResizeTextarea(ths) {
            ths.style.height = 'auto';
            ths.style.height = ths.scrollHeight + 'px';
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

