<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SpecialTask.aspx.cs" Inherits="mis_Admin_SpecialTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Special Task</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RFV1" ValidationGroup="a"
                                                        ErrorMessage="Select To Date" ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Employee !'></i>"
                                                        ControlToValidate="ddlEmployee" InitialValue="0" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label>EMPLOYEE NAME <span style="color: red;">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlEmployee" ClientIDMode="Static"
                                                    CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RFV2" ValidationGroup="a"
                                                        ErrorMessage="Select To Date" ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Project Name !'></i>"
                                                        ControlToValidate="ddlProjectName" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label runat="server">PROJECT NAME <span style="color: red;">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlProjectName" ClientIDMode="Static"
                                                    CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RFV3" ValidationGroup="a"
                                                        ErrorMessage="Select To Date" ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Completion Date !'></i>"
                                                        ControlToValidate="txtCompletionDate" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label runat="server">COMPLETION DATE<span style="color: red;">*</span></label>
                                                <asp:TextBox ID="txtCompletionDate" runat="server"
                                                    data-provide="datepicker" placeholder="DD/MM/YYYY"
                                                    autocomplete="off" data-date-format="dd/mm/yyyy"
                                                    data-date-autoclose="true"
                                                    CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RFV4" ValidationGroup="a"
                                                        ErrorMessage="Select To Date" ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Task Priority !'></i>"
                                                        ControlToValidate="ddlTaskPriority" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label runat="server">TASK PRIORITY <span style="color: red;">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlTaskPriority" ClientIDMode="Static"
                                                    CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>REASON</label>
                                                <textarea
                                                    id="txtReason"
                                                    runat="server"
                                                    class="form-control"
                                                    oninput="autoResizeTextarea(this)"
                                                    onkeypress="javascript:tbx_fnAlphaOnly(event, this);"
                                                    placeholder="Enter Reason" maxlength="100">
    </textarea>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Button runat="server" Style="margin-top: 22px;" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" ValidationGroup="a" OnClick="btnSave_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <a href="SpecialTask.aspx" style="margin-top: 22px;" class="btn btn-block btn-default">Clear</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>  
                            <hr />
                            <div class="card">
                                <div class="card">
                                    <div class="row" style="padding: 0px 9px 2px 15px;" id="div1" runat="server">
                                        <div>
                                            <h4 style="margin-left: 2rem;">Special Task Detail </h4>
                                        </div>
                                        <div class="table-responsive">
                                            <div class="col-md-12">
                                                <asp:GridView ID="Grid" PageSize="50" runat="server" class="datatable  table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="false" AutoGenerateColumns="false" OnRowCommand="Grid_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PROJECT NAME ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProjectName" Text='<%# Eval("ProjectName").ToString() %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblProjectId" Text='<%# Eval("SpecialTaskId").ToString() %>' runat="server" Visible="false"></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpName" Text='<%# Eval("Emp_Name").ToString() %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblEmpId" Text='<%# Eval("Emp_Id").ToString() %>' Visible="false" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="COMPLETION DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCompletionDate" Text='<%# Eval("CompletionDate").ToString() %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TASK PRIORITY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaskPriority" Text='<%# Eval("TaskPriority").ToString() %>' runat="server"></asp:Label>                                                              
                                                                <asp:Label ID="lblTaskPriorityId" Text='<%# Eval("TaskPriorityId").ToString() %>' runat="server" Visible="false"></asp:Label>                                                              
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REASON">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReason" Text='<%# Eval("Reason").ToString() %>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField ItemStyle-Width="30" HeaderText="STATUS">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkStatus" runat="server" CommandArgument='<%# Eval("SpecialTaskId").ToString()%>' CssClass='<%# Eval("IsActive").ToString() =="True"?"label label-success":"label label-danger"  %>' CausesValidation="False" CommandName="ChangeStatus" Text='<%# Eval("IsActive").ToString() =="True"?"Active":"Deactive"  %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="30px"></ItemStyle>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="ACTION">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-default" CommandArgument='<%# Eval("SpecialTaskId").ToString()%>' CausesValidation="False" CommandName="EditRecord" Text="Edit"></asp:LinkButton>
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

