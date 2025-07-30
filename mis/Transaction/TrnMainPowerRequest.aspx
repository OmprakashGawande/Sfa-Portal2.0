<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="TrnMainPowerRequest.aspx.cs" Inherits="mis_Transaction_TrnMainPowerRequest" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Man Power Request</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
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
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Button runat="server" Style="margin-top: 22px;" CssClass="btn btn-block btn-success" ID="btnSearch" Text="Search" ValidationGroup="a" OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <a href="TrnMainPowerRequest.aspx" style="margin-top: 22px;" class="btn btn-block btn-default">Clear</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="card" id="div1" runat="server" visible="false">
                                <div class="card-body">
                                    <div class="row" style="padding: 0px 9px 2px 15px;">
                                        <div>
                                            <h4>MAN POWER ON BENCH DETAIL </h4>
                                        </div>
                                        <div class="table-responsive">
                                            <div class="col-md-12">
                                                <asp:GridView ID="Grid" PageSize="50" runat="server" class="datatable  table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="false"
                                                    OnRowCommand="Grid_RowCommand" AutoGenerateColumns="False" EmptyDataText="NO RECORD FOUND">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("EmpId").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmployeeName" Text='<%# Eval("Emp_Name").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DESIGNATION">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesignation" Text='<%# Eval("Designation_Name").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="BENCH FROM DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBenchFromDate" Text='<%# Eval("BenchFromDate").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REQUEST">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%# Eval("EmpId") %>' CommandName="View" ToolTip="View" CssClass="label label-success">Request for Allocation </asp:LinkButton>

                                                                <%-- <button type="button" class="label label-success" data-toggle="modal" data-target="#exampleModal2">
                                                                    Request for Allocation 
                                                                </button>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="card" id="div2" runat="server" visible="false">
                                <div class="card-body">
                                    <div class="row" style="padding: 0px 9px 2px 15px;">
                                        <div>
                                            <h4>MAN POWER WITH WORK DETAIL </h4>
                                        </div>
                                        <div class="table-responsive">
                                            <div class="col-md-12">
                                                <asp:GridView ID="Grid2" PageSize="50" runat="server" class="datatable  table table-hover table-bordered pagination-ys" 
                                                    ShowHeaderWhenEmpty="false" AutoGenerateColumns="False" EmptyDataText="NO RECORD FOUND" OnRowCommand="Grid2_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("EmpId").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmployeeName2" Text='<%# Eval("Emp_Name").ToString() %>' runat="server" />
                                                                <asp:Label ID="lblManagerId" Text='<%# Eval("ManagerId").ToString() %>' runat="server" Visible="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DESIGNATION">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesignation2" Text='<%# Eval("Designation_Name").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MANAGER">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblManager" Text='<%# Eval("Manager").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PROJECT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProjectName" Text='<%# Eval("ProjectName").ToString() %>' runat="server" />
                                                                <asp:Label ID="lblWorkingProjectId" Text='<%# Eval("WorkingProjectId").ToString() %>' Visible="false" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REQUEST">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%# Eval("EmpId") %>' CommandName="View" ToolTip="View" CssClass="label label-success">Request for Allocation </asp:LinkButton>

                                                                <%--   <button type="button" class="label label-success" data-toggle="modal" data-target="#exampleModal3">
                                                                    Request for Allocation 
                                                                </button>--%>
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
    <!-- Man Power In BENCH -->
    <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">

        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title" id="exampleModalLongTitle2">Man Power Detail</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="card">
                    <div class="card-body fa-border">
                        <!-- Modal Body -->
                        <div class="modal-body">
                            <asp:HiddenField ID="HiddenField1" runat="server" />

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>EMPLOYEE NAME</label>
                                        <asp:TextBox CssClass="form-control" runat="server" ReadOnly ID="txtEmpName"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>DESIGNATION</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ReadOnly ID="txtDesignation"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator8"
                                                ValidationGroup="b"
                                                ErrorMessage="Select From Date"
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select Project'></i>"
                                                ControlToValidate="ddlProjectB"
                                                Display="Dynamic"
                                                runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <label>Project<span style="color: red;">*</span></label>
                                        <asp:DropDownList CssClass="form-control" runat="server" ID="ddlProjectB">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator5"
                                                ValidationGroup="b"
                                                ErrorMessage="Select From Date"
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select FROM DATE'></i>"
                                                ControlToValidate="txtFromDateB"
                                                Display="Dynamic"
                                                runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>

                                        <label>FROM DATE  <span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtFromDateB"
                                            data-provide="datepicker" placeholder="DD/MM/YYYY"
                                            autocomplete="off" data-date-format="dd/mm/yyyy"
                                            data-date-autoclose="true"
                                            runat="server" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator6"
                                                ValidationGroup="b"
                                                ErrorMessage="Select From Date"
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select FROM DATE DAY'></i>"
                                                ControlToValidate="ddlFromDateDay"
                                                Display="Dynamic"
                                                runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <label>FROM DATE DAY<span style="color: red;">*</span></label>
                                        <asp:DropDownList CssClass="form-control" runat="server" ID="ddlFromDateDay">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Full Day</asp:ListItem>
                                            <asp:ListItem Value="1">Half Day</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator1"
                                                ValidationGroup="b"
                                                ErrorMessage="Select To  Date"
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select TO DATE'></i>"
                                                ControlToValidate="txtToDateB"
                                                Display="Dynamic"
                                                runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>

                                        <label>TO DATE  <span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtToDateB" runat="server"
                                            data-provide="datepicker" placeholder="DD/MM/YYYY"
                                            autocomplete="off" data-date-format="dd/mm/yyyy"
                                            data-date-autoclose="true" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator7"
                                                ValidationGroup="b"
                                                ErrorMessage="Select From Date"
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select TO DATE DAY'></i>"
                                                ControlToValidate="ddlToDateDay"
                                                Display="Dynamic"
                                                runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <label>TO DATE DAY<span style="color: red;">*</span></label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlToDateDay">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Full Day</asp:ListItem>
                                            <asp:ListItem Value="1">Half Day</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>REMARK<span style="color: red;"> *</span></label>
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator12"
                                                ValidationGroup="b"
                                                ErrorMessage="Enter Remark"
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Enter Remark!'></i>"
                                                ControlToValidate="txtRemarkB"
                                                Display="Dynamic"
                                                runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <textarea
                                            id="txtRemarkB"
                                            runat="server"
                                            class="form-control"
                                            oninput="autoResizeTextarea(this)"
                                            onkeypress="javascript:tbx_fnAlphaOnly(event, this);"
                                            placeholder="Enter Remark" maxlength="100"
                                            rows="2">
    </textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Modal Footer -->
                        <div class="modal-footer">
                            <asp:Button ID="btnBech" runat="server" CssClass="btn btn-success" Text="Request" ValidationGroup="b" OnClick="btnBech_Click" />
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <!-- Man Power With Work  -->
    <div class="modal fade" id="exampleModal3" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">

        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title" id="exampleModalLongTitle3">Man Power Detail</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="card">
                    <div class="card-body fa-border">
                        <!-- Modal Body -->
                        <div class="modal-body">
                            <asp:HiddenField ID="HiddenField2" runat="server" />

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>EMPLOYEE NAME</label>
                                        <asp:TextBox CssClass="form-control" runat="server" ReadOnly ID="txtEmpName2"></asp:TextBox>
                                    </div>

                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>DESIGNATION</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ReadOnly ID="txtDesignation2"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>WORKING PROJECT</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ReadOnly ID="txtWorkingProject"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator9"
                                                ValidationGroup="W"
                                                ErrorMessage="Select From Date"
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select Project'></i>"
                                                ControlToValidate="ddlProjectW"
                                                Display="Dynamic"
                                                runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <label>Project<span style="color: red;">*</span></label>
                                        <asp:DropDownList CssClass="form-control" runat="server" ID="ddlProjectW">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator2"
                                                ValidationGroup="W"
                                                ErrorMessage="Select From Date"
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select FROM DATE'></i>"
                                                ControlToValidate="txtFromDateW"
                                                Display="Dynamic"
                                                runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>

                                        <label>FROM DATE <span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtFromDateW" runat="server"
                                            data-provide="datepicker" placeholder="DD/MM/YYYY"
                                            autocomplete="off" data-date-format="dd/mm/yyyy"
                                            data-date-autoclose="true" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator10"
                                                ValidationGroup="W"
                                                ErrorMessage="Select From Date"
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select FROM DATE DAY'></i>"
                                                ControlToValidate="ddlfromDatedayW"
                                                Display="Dynamic"
                                                runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <label>FROM DATE DAY<span style="color: red;">*</span></label>
                                        <asp:DropDownList CssClass="form-control" runat="server" ID="ddlfromDatedayW">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Full Day</asp:ListItem>
                                            <asp:ListItem Value="1">Half Day</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator3"
                                                ValidationGroup="W"
                                                ErrorMessage="Select To  Date"
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select TO DATE'></i>"
                                                ControlToValidate="txtToDateW"
                                                Display="Dynamic"
                                                runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>

                                        <label>TO DATE  <span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtToDateW" runat="server"
                                            data-provide="datepicker" placeholder="DD/MM/YYYY"
                                            autocomplete="off" data-date-format="dd/mm/yyyy"
                                            data-date-autoclose="true" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator11"
                                                ValidationGroup="W"
                                                ErrorMessage="Select To Date Day"
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select TO DATE DAY'></i>"
                                                ControlToValidate="ddlTodatedayW"
                                                Display="Dynamic"
                                                runat="server" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <label>TO DATE DAY<span style="color: red;">*</span></label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTodatedayW">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Full Day</asp:ListItem>
                                            <asp:ListItem Value="1">Half Day</asp:ListItem>

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>REMARK<span style="color: red;"> *</span></label>
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator13"
                                                ValidationGroup="W"
                                                ErrorMessage="Enter Remark"
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Enter Remark!'></i>"
                                                ControlToValidate="txtRemarkW"
                                                Display="Dynamic"
                                                runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <textarea
                                            id="txtRemarkW"
                                            runat="server"
                                            class="form-control"
                                            oninput="autoResizeTextarea(this)"
                                            onkeypress="javascript:tbx_fnAlphaOnly(event, this);"
                                            placeholder="Enter Remark" maxlength="100"
                                            rows="2">
    </textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Modal Footer -->
                        <div class="modal-footer">
                            <asp:Button ID="tbnWorkin" runat="server" CssClass="btn btn-success" Text="Request" ValidationGroup="W" OnClick="tbnWorkin_Click" />
                            <button
                                type="button"
                                class="btn btn-secondary"
                                data-dismiss="modal">
                                Close
                            </button>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

