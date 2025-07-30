<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="TrnDailyReporting.aspx.cs" Inherits="mis_Daily_Task_TrnDailyReporting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        #txtRemark {
            width: 100%;
            min-height: calc(1.5em * 2 + 1rem); /* roughly 2 rows line height + padding */
            padding: 0.5rem 0.75rem;
            font-size: 1rem;
            line-height: 1.5;
            border: 1px solid #ced4da;
            border-radius: 0.375rem;
            resize: none; /* prevent manual resize */
            overflow-y: hidden; /* hide scrollbar */
            box-sizing: border-box;
            font-family: inherit;
            transition: border-color 0.2s ease;
            border-color: #5c6ac4;
            outline: none;
            box-shadow: 0 0 0 3px rgba(92, 106, 196, 0.3);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="VDS" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="a" />
    <div class="content-wrapper">
        <asp:HiddenField runat="server" ID="hfProjectID" />
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Daily Reporting</h3>
                        </div>
                        <asp:Label runat="server" ID="lblMsg"></asp:Label>
                        <hr />
                        <div class="row" style="padding: 0px 9px 2px 15px;">

                            <div class="col-md-3">
                                <label runat="server">
                                    DATE 
                                    <label style="color: red;">*</label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtDate"
                                        data-provide="datepicker" placeholder="DD/MM/YYYY"
                                        autocomplete="off" data-date-format="dd/mm/yyyy"
                                        data-date-autoclose="true" CssClass="form-control disableFuturedate"
                                        AutoPostBack="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-5"></div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label runat="server">EMPLOYEE NAME  </label>
                                    <label style="color: red;">*</label>
                                    <asp:TextBox runat="server" ID="txtEmp" CssClass="form-control">

                                    </asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding: 0px 9px 2px 15px;">
                            <div class="table-responsive">
                                <div class="col-md-12">
                                    <asp:GridView ID="Grid" PageSize="50" runat="server" class="datatable table table-hover table-bordered pagination-ys" OnRowDataBound="Grid_RowDataBound" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnRowCommand="Grid_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ProjectId").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PROJECT NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProjectName" Text='<%# Eval("ProjectName") %>' runat="server" />
                                                    <asp:HiddenField ID="hfTaskAllocationId" runat="server" Value='<%# Eval("TaskAllocationId") %>' />
                                                    <asp:HiddenField ID="hfEmpId" runat="server" Value='<%# Eval("EmpId") %>' />
                                                    <asp:HiddenField ID="hfProjectId" runat="server" Value='<%# Eval("ProjectId") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PARENT TASK">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParentTask" Text='<%# Eval("ParentTaskName") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TASK NAME (CODE)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaskName" Text='<%# Eval("TaskName") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TASK TYPE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaskType" Text='<%# Eval("TaskType") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TASK PRIORITY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaskPriority" Text='<%# Eval("TaskPriority") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TASK DESCRIPTION">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaskDescription" Text='<%# Eval("TaskDescription") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ASSIGNED BY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAssignBy" Text='<%# Eval("AssignBy") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TASK ALLOCATION DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedDate" Text='<%# Eval("CreatedDate") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FROM DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFromDate" Text='<%# Eval("FromDate") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TO DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblToDate" Text='<%# Eval("ToDate") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TASK STATUS">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaskStatusText" Text='<%# Eval("TaskStatusText") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="FILL REPORT">
                                                <ItemTemplate>
                                                    <asp:LinkButton
                                                        ID="btnFillTaskDetail"
                                                        runat="server"
                                                        Text="Fill Report"
                                                        CssClass="label label-success"
                                                        CommandName="OpenTaskDetailModel"
                                                        CommandArgument='<%# Eval("ProjectId") %>' />
                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="FILL REPORT">
                                                <ItemTemplate>
                                                    <asp:Panel runat="server">
                                                        <asp:LinkButton
                                                            ID="btnFillTaskDetail"
                                                            runat="server"
                                                            Text="Fill Report"
                                                            CssClass="label label-success"
                                                            CommandName="OpenTaskDetailModel"
                                                            CommandArgument='<%# Eval("ProjectId") %>'
                                                            Visible='<%# Eval("TaskStatusToday").ToString() == "0" %>' />

                                                        <asp:Label
                                                            ID="lblTaskFilled"
                                                            runat="server"
                                                            Text="Filled"
                                                            ForeColor="Green"
                                                            Font-Bold="true"
                                                            Visible='<%# Eval("TaskStatusToday").ToString() == "1" %>' />
                                                    </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <%-- <asp:TemplateField HeaderText="HOURS" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtHours" runat="server" CssClass="form-control" placeholder="Hours" AutoComplete="off" MaxLength="2" oninput="this.value = this.value.replace(/[^0-9]/g, '').slice(0, 2);" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MINUTES" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMinutes" runat="server" CssClass="form-control" placeholder="Minutes" AutoComplete="off" MaxLength="2" oninput="this.value = this.value.replace(/[^0-9]/g, '').slice(0, 2);" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TASK STATUS" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:RadioButton ID="rbComplete" runat="server" GroupName="TaskStatus" Text="Complete" Style="margin-right: 10px;" />
                                                        <asp:RadioButton ID="rbWorking" runat="server" Checked="true" GroupName="TaskStatus" Text="Working" />
                                                        <asp:RadioButton ID="rbPending" runat="server" GroupName="TaskStatus" Text="Pending" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REMARK">
                                                <ItemTemplate>
                                                    <textarea id="txtRemark" runat="server" class="form-control" oninput="autoResizeTextarea(this)" onkeypress="javascript:tbx_fnAlphaOnly(event, this);" placeholder="Enter Remark" rows="2"></textarea>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ACTION">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkbox" runat="server" CssClass="chkSelect" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <%-- <div class="row" id="DivBtn" runat="server" style="padding: 0px 9px 2px 15px;">
                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" runat="server" Text="Submit" CssClass="btn btn-success btn-block" />
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </div>
            </div>
        </section>
    </div>

    <!-- Bootstrap Modal -->
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" style="width: 104rem;">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title" id="exampleModalLongTitle">Fill Report</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="card">
                    <asp:Label runat="server" ID="lblMsgManPower" Text=""></asp:Label>
                    <div class="card-body fa-border">
                        <!-- Modal Body -->
                        <div class="modal-body">
                            <asp:HiddenField ID="HiddenProjectID" runat="server" />
                            <div class="card">
                                <div class="card-header">
                                    <div class="row" style="padding: 0px 9px 2px 15px;">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label runat="server">PROJECT NAME </label>
                                                <label style="color: red;">*</label>
                                                <asp:TextBox runat="server" ID="rdProjectName" CssClass="form-control">

                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <label runat="server">
                                                DATE 
                                                  <label style="color: red;">*</label>
                                            </label>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="txtDate2"
                                                    data-provide="datepicker" placeholder="DD/MM/YYYY"
                                                    autocomplete="off" data-date-format="dd/mm/yyyy"
                                                    data-date-autoclose="true" CssClass="form-control disableFuturedate"
                                                    AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>



                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label runat="server">EMPLOYEE NAME  </label>
                                                <label style="color: red;">*</label>
                                                <asp:TextBox runat="server" ID="txtEmp1" CssClass="form-control">

                                                </asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label runat="server">
                                                TASK ALLOCATION DATE 
                                                  <label style="color: red;">*</label>
                                            </label>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="txtTaskAllocationDate"
                                                    data-provide="datepicker" placeholder="DD/MM/YYYY"
                                                    autocomplete="off" data-date-format="dd/mm/yyyy"
                                                    data-date-autoclose="true" CssClass="form-control disableFuturedate"
                                                    AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <label runat="server">
                                                FROM DATE 
                                                  <label style="color: red;">*</label>
                                            </label>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="txtFromDate"
                                                    data-provide="datepicker" placeholder="DD/MM/YYYY"
                                                    autocomplete="off" data-date-format="dd/mm/yyyy"
                                                    data-date-autoclose="true" CssClass="form-control disableFuturedate"
                                                    AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <label runat="server">
                                                TO DATE 
                                                  <label style="color: red;">*</label>
                                            </label>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="txtTodate"
                                                    data-provide="datepicker" placeholder="DD/MM/YYYY"
                                                    autocomplete="off" data-date-format="dd/mm/yyyy"
                                                    data-date-autoclose="true" CssClass="form-control disableFuturedate"
                                                    AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>




                                    </div>
                                </div>
                                <br />
                                <div class="card-body">

                                    <div class="row">
                                        <!-- HOURS -->
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                        ControlToValidate="txtHours"
                                                        ValidationGroup="a"
                                                        ErrorMessage="Enter Hours"
                                                        ForeColor="Red"
                                                        Display="Dynamic"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Hours!'></i>">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label for="txtHours">HOURS <span style="color: red;">*</span></label>
                                                <asp:TextBox ID="txtHours" runat="server" CssClass="form-control"
                                                    placeholder="Hours" AutoComplete="off" MaxLength="2"
                                                    oninput="this.value = this.value.replace(/[^0-9]/g, '').slice(0, 2);" />

                                            </div>
                                        </div>

                                        <!-- MINUTES -->
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txtMinutes"
                                                        ValidationGroup="a"
                                                        ErrorMessage="Enter Minutes"
                                                        ForeColor="Red"
                                                        Display="Dynamic"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Minutes!'></i>">
                                                    </asp:RequiredFieldValidator>
                                                </span>


                                                <label for="txtMinutes">MINUTES <span style="color: red;">*</span></label>
                                                <asp:TextBox ID="txtMinutes" runat="server" CssClass="form-control"
                                                    placeholder="Minutes" AutoComplete="off" MaxLength="2"
                                                    oninput="this.value = this.value.replace(/[^0-9]/g, '').slice(0, 2);" />

                                            </div>
                                        </div>

                                        <!-- STATUS -->
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                        ControlToValidate="ddlStatus"
                                                        ValidationGroup="a"
                                                        ErrorMessage="Select Status "
                                                        ForeColor="Red"
                                                        Display="Dynamic"
                                                        InitialValue="0"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Minutes!'></i>">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label>TASK STATUS<span style="color: red;">*</span></label>
                                                <%--<asp:RadioButton ID="rbComplete" runat="server" GroupName="TaskStatus" Text="Complete" Style="margin-right: 15px;" />
                                                <asp:RadioButton ID="rbWorking" runat="server" GroupName="TaskStatus" Text="Work In Progress" Checked="true" Style="margin-right: 15px;" />
                                                <asp:RadioButton ID="rbPending" runat="server" GroupName="TaskStatus" Text="Pending" />--%>

                                                <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Work In Progress</asp:ListItem>
                                                    <asp:ListItem Value="2">Complete</asp:ListItem>
                                                    <asp:ListItem Value="3">Pending</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                        ControlToValidate="ddlWorkingStatus"
                                                        ValidationGroup="a"
                                                        ErrorMessage="Select Working Status "
                                                        ForeColor="Red"
                                                        Display="Dynamic"
                                                        InitialValue="0"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Working Status'></i>">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label>WORKING STATUS<span style="color: red;">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlWorkingStatus" CssClass="form-control">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">On Time</asp:ListItem>
                                                    <asp:ListItem Value="2">Delay</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                        ControlToValidate="txtInternalChallange"
                                                        ValidationGroup="a"
                                                        ErrorMessage="Enter Internal Challange"
                                                        ForeColor="Red"
                                                        Display="Dynamic"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Internal Challange'></i>">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label>INTERNAL CHALLANGE <span style="color: red;">*</span> </label>
                                                <%-- <asp:TextBox ID="txtInternalChallange" placeholder="Enter Internal Challange" runat="server" CssClass="form-control"></asp:TextBox>--%>

                                                <textarea id="txtInternalChallange" runat="server" class="form-control"
                                                    oninput="autoResizeTextarea(this)"
                                                    placeholder="Enter Internal Challange" rows="2" maxlength="200"></textarea>
                                                <asp:Label runat="server" ID="lblCounter"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                        ControlToValidate="txtExternalChallange"
                                                        ValidationGroup="a"
                                                        ErrorMessage="Enter External Challange"
                                                        ForeColor="Red"
                                                        Display="Dynamic"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter External Challange'></i>">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label>EXTERNAL CHALLANGE <span style="color: red;">*</span> </label>

                                                <%--  <asp:TextBox ID="txtExternalChallange" runat="server" placeholder="Enter External Challange" CssClass="form-control"></asp:TextBox>--%>
                                                <textarea id="txtExternalChallange" runat="server" class="form-control"
                                                    oninput="autoResizeTextarea(this)"
                                                    placeholder="Enter External Challange" rows="2" maxlength="200"></textarea>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>FILE UPLOAD</label>
                                                <asp:FileUpload runat="server" ID="FUDoc" CssClass="form-control"></asp:FileUpload>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label for="txtRemark">TASK DISCRIPTION</label>
                                                <textarea id="txtRemark" runat="server" class="form-control"
                                                    oninput="autoResizeTextarea(this)"
                                                    onkeypress="javascript:tbx_fnAlphaOnly(event, this);"
                                                    placeholder="Enter Task Discription" rows="2"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <hr />
                                    <div class="row" style="padding: 0px 9px 2px 15px;">
                                        <h4>Task Detail</h4>
                                        <div class="table-responsive">
                                            <div class="col-md-12">
                                                <asp:GridView ID="GridOldTask" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys" OnRowDataBound="GridOldTask_RowDataBound" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnRowCommand="Grid_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ProjectId").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PROJECT NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProjectName" Text='<%# Eval("ProjectName") %>' runat="server" />
                                                                <asp:HiddenField ID="hfTaskAllocationId" runat="server" Value='<%# Eval("TaskAllocationId") %>' />
                                                                <asp:HiddenField ID="hfEmpId" runat="server" Value='<%# Eval("EmpId") %>' />
                                                                <asp:HiddenField ID="hfProjectId" runat="server" Value='<%# Eval("ProjectId") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PARENT TASK">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblParentTask" Text='<%# Eval("ParentTaskName") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TASK NAME (CODE)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaskName" Text='<%# Eval("TaskName") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TASK TYPE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaskType" Text='<%# Eval("TaskType") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TASK PRIORITY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaskPriority" Text='<%# Eval("TaskPriority") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TASK DESCRIPTION">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaskDescription" Text='<%# Eval("TaskDescription") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ASSIGNED BY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAssignBy" Text='<%# Eval("AssignBy") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TASK FIll DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFillDate" Text='<%# Eval("FilledDate") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TASK STATUS">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaskStatusText" Text='<%# Eval("TaskStatusText") %>' runat="server" />
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
                        <!-- Modal Footer -->
                        <div class="modal-footer">
                            <asp:Button
                                ID="btnSaveTask"
                                runat="server"
                                CssClass="btn btn-success"
                                Text="Save" ValidationGroup="a" OnClick="btnSaveTask_Click" />
                            <button
                                type="button"
                                class="btn btn-secondary"
                                data-dismiss="modal">
                                Cancel
                            </button>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>


    <script>
        CharactersCount(350);
        const element = document.getElementById('<%=txtRemark.ClientID%>');
        // Pass CharactersCount directly with a parameter using an inline arrow function
        element.addEventListener("keyup", (event) => CharactersCount(1000));

        function CharactersCount(_length) {
            var txtMsg = document.getElementById('<%=txtRemark.ClientID%>');
            var lblCount = document.getElementById('<%=lblCounter.ClientID%>');
            if (txtMsg.value.length > _length) {
                txtMsg.value = txtMsg.value.substring(0, _length);
            }
            // Calculate and display the remaining characters
            const remaining = _length - txtMsg.value.length;
            lblCount.innerHTML = `${remaining} characters remaining`;
        }

        function autoResizeTextarea(ths) {
            ths.style.height = 'auto'; // Reset height to calculate new scrollHeight
            ths.style.height = ths.scrollHeight + 'px'; // Set height to fit content
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        $(document).ready(function () {
            $('.datatable').DataTable({

                paging: true,

                columnDefs: [{
                    targets: 'no-sort',
                    orderable: false
                }],
                "order": [[0, 'asc']],

                dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
                    '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
                    '<"row"<"col-sm-5"i><"col-sm-7"p>>',
                fixedHeader: {
                    header: true
                },

                buttons: {
                    buttons: [{
                        extend: 'print',
                        text: '<i class="fa fa-print"></i> Print',
                        title: 'Daily Reporting ',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: 'Daily Reporting',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]
                        },
                        footer: true
                    }],

                    dom: {
                        container: {
                            className: 'dt-buttons'
                        },
                        button: {
                            className: 'btn btn-default'
                        }
                    }
                }
            });
            t.on('order.dt search.dt', function () {
                t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
            }).draw();
        });

    </script>
</asp:Content>

