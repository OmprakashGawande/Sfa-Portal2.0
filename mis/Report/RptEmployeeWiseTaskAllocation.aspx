<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptEmployeeWiseTaskAllocation.aspx.cs" Inherits="mis_Report_RptEmployeeWiseTaskAllocation" %>

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
                <div class="card mt-3">
                    <div class="card-header">
                        <h4>Employee Wise Task Allocation Report </h4>
                    </div>
                    <div class="card-body">

                        <div class="row">
                            <div class="col-xl-3 position-relative col-sm-6">

                                <div class="form-group">
                                    <span class="fa-pull-right">
                                        <asp:RequiredFieldValidator ID="RFV1" ValidationGroup="a"
                                            ErrorMessage="Select Employee" ForeColor="Red"
                                            Text="<i class='fa fa-exclamation-circle' title='Enter Employee !'></i>"
                                            ControlToValidate="ddlEmp" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <label>EMPLOYEE <span style="color: red;">*</span></label>
                                    <asp:DropDownList ID="ddlEmp" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-xl-3 position-relative col-sm-6">
                                <div class="form-group">
                                    <label runat="server">FROM DATE </label>
                                    <asp:TextBox
                                        ID="txtFromDate"
                                        runat="server"
                                        CssClass="form-control datetime-local"
                                        placeholder="DD/MM/YYYY"
                                        autocomplete="off"
                                        data-date-start-date="0d"
                                        onkeydown="return false;" onchange="validateDates()" />
                                </div>
                            </div>
                            <div class="col-xl-3 position-relative col-sm-6">
                                <div class="form-group">
                                    <label runat="server">TO DATE </label>
                                    <asp:TextBox
                                        ID="txtToDate"
                                        runat="server"
                                        CssClass="form-control datetime-local"
                                        placeholder="DD/MM/YYYY"
                                        autocomplete="off"
                                        data-date-start-date="0d"
                                        onkeydown="return false;" onchange="validateDates()" />
                                </div>
                            </div>


                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-xl-3 position-relative">
                                <div class="form-group">
                                    <asp:Button runat="server" CssClass="btn btn-block btn-outline-info" ID="btnSearch" Text="Search" ValidationGroup="a" OnClick="btnSearch_Click" />
                                    <a href="RptEmployeeWiseTaskAllocation.aspx" class="btn btn-block btn-outline-danger">Clear</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="card-header">
                        <h4>Employee Wise Task Allocation Detail </h4>
                    </div>
                    <div class="card-body">
                        <div class="row" id="Datagrid" runat="server" style="padding: 0px 9px 2px 15px;">

                            <div class="table-responsive">
                                <div class="col-md-12">
                                    <asp:GridView ID="Grid" PageSize="50" runat="server"
                                        class="datatable table table-hover table-bordered pagination-ys"
                                        ShowHeaderWhenEmpty="false" AutoGenerateColumns="False" OnRowCommand="Grid_RowCommand" OnRowDataBound="Grid_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.NO." ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAllocatedBy" runat="server" Text='<%# Eval("AllocatedBy") %>' />

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ManagerID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpId" runat="server" Text='<%# Eval("Emp_ID") %>' />
                                                    <asp:Label ID="lblProjectId" runat="server" Text='<%# Eval("ProjectId") %>' />

                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="PROJECT NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>' />

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TOTAL TASK ASSIGNED">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWeekTasks" runat="server" Text='<%# Eval("TotalTasksAssigned") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="COMPLETED TASK">
                                                <ItemTemplate>
                                                    <asp:LinkButton
                                                        ID="lnkTotalTasksCompleted"
                                                        runat="server"
                                                        Text='<%# Eval("TotalTasksCompleted").ToString() == "0" ? "0" : Eval("TotalTasksCompleted").ToString() %>'
                                                        CommandName="ViewCompleteTasks"
                                                        CommandArgument='<%# Eval("ProjectId") + ";" + Eval("Emp_ID")  %>'
                                                        CssClass="btn btn-link p-0"
                                                        ToolTip="Click to view completed task" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TASK IN PROGRESS">
                                                <ItemTemplate>
                                                    <asp:LinkButton
                                                        ID="lnkTotalTasksInProgress"
                                                        runat="server"
                                                        Text='<%# Eval("TotalTasksInProgress").ToString() == "0" ? "0" : Eval("TotalTasksInProgress").ToString() %>'
                                                        CommandName="ViewWipTasks"
                                                        CommandArgument='<%# Eval("ProjectId") + ";" + Eval("Emp_ID")  %>'
                                                        CssClass="btn btn-link p-0"
                                                        ToolTip="Click to view Work In Progress tasks" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PENDING TASK">
                                                <ItemTemplate>
                                                    <asp:LinkButton
                                                        ID="lnkViewTotalTasksPending"
                                                        runat="server"
                                                        Text='<%# Eval("TotalTasksPending").ToString() == "0" ? "0" : Eval("TotalTasksPending").ToString() %>'
                                                        CommandName="ViewPendingTasks"
                                                        CommandArgument='<%# Eval("ProjectId") + ";" + Eval("Emp_ID")  %>'
                                                        CssClass="btn btn-link p-0"
                                                        ToolTip="Click to view Pending tasks" />
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
    <div id="exampleModal" class="modal fade bd-example-modal-xl" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel">Task Detail</h4>
                    <button class="btn-close py-0" type="button" onclick="closePopup('#exampleModal')"></button>
                </div>
                <div class="modal-body dark-modal">
                    <div class="card">
                        <asp:Label runat="server" ID="lblMsgManPower" Text=""></asp:Label>
                        <div class="card-body fa-border">
                            <!-- Modal Body -->
                            <div class="modal-body">
                                <div class="row"></div>
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridTaskDetail" PageSize="50" runat="server" class="datatable2 table  table-hover table-bordered pagination-ys" OnRowDataBound="GridTaskDetail_RowDataBound" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnRowCommand="Grid_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ProjectId").ToString() %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TASK STATUS">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaskStatusText" Text='<%# Eval("TaskStatusText") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PROJECT NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProjectName" Text='<%# Eval("ProjectName") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaskPerformerName" Text='<%# Eval("TaskPerformerName") %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="TASK NAME (CODE)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaskName" Text='<%# Eval("TaskName") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="TASK TYPE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaskType" Text='<%# Eval("TaskType") %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="TASK PRIORITY">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaskPriority" Text='<%# Eval("TaskPriority") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TASK DESCRIPTION">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaskDescription" Text='<%# Eval("Discription") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ALLOCATE FROM DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromDate" Text='<%# Eval("FromDate") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ALLOCATE TO DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblToDate" Text='<%# Eval("ToDate") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="TASK ALLOCATION DOCUMENT">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HyperLink1" runat="server"
                                                            Target="_blank"
                                                            NavigateUrl='<%#
                      (Eval("TaskAllocationDoc") != null && Eval("TaskAllocationDoc") != DBNull.Value)
                      && !string.IsNullOrWhiteSpace(Convert.ToString(Eval("TaskAllocationDoc")))
                      && Convert.ToString(Eval("TaskAllocationDoc")).Trim().ToLower() != "null"
                      && Convert.ToString(Eval("TaskAllocationDoc")).Trim() != "0"
                      ? "~/mis/Document/" + Convert.ToString(Eval("TaskAllocationDoc")).Trim()
                      : "" %>'
                                                            CssClass="label label-info" Text="View"
                                                            Visible='<%# (Eval("TaskAllocationDoc") != null && Eval("TaskAllocationDoc") != DBNull.Value) && !string.IsNullOrWhiteSpace(Convert.ToString(Eval("TaskAllocationDoc")))  && Convert.ToString(Eval("TaskAllocationDoc")).Trim().ToLower() != "null" && Convert.ToString(Eval("TaskAllocationDoc")).Trim() != "0" %>'>
                                                        </asp:HyperLink>
                                                        <%--<asp:HyperLink ID="hyperTaskAllocationDoc" runat="server" Target="_blank" Enabled='<%# Eval("TaskAllocationDoc").ToString() == "" ? false : true %>' NavigateUrl='<%# "~/mis/Document/" + Eval("TaskAllocationDoc") %>' CssClass="label label-info" Visible='<%# (Eval("TaskAllocationDoc") != null && Eval("TaskAllocationDoc") != DBNull.Value) && !string.IsNullOrWhiteSpace(Convert.ToString(Eval("TaskAllocationDoc")))  && Convert.ToString(Eval("TaskAllocationDoc")).Trim().ToLower() != "null" && Convert.ToString(Eval("TaskAllocationDoc")).Trim() != "0" %>' Text="View"></asp:HyperLink>--%>
                                                        <asp:Label ID="lblTaskAllocationDocPath" runat="server" Visible="false" Text='<%# Eval("TaskAllocationDoc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Modal Footer -->
                        <div class="modal-footer">

                            <button
                                type="button"
                                class="btn btn-secondary"
                                onclick="closePopup('#exampleModal')">
                                Close
                            </button>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Bootstrap Modal -->


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>
        function validateDates() {
            var fromDateElem = document.getElementById('<%= txtFromDate.ClientID %>');
            var toDateElem = document.getElementById('<%= txtToDate.ClientID %>');

            var fromDate = fromDateElem.value;
            var toDate = toDateElem.value;

            if (fromDate !== '' && toDate !== '') {
                var partsFrom = fromDate.split('/');
                var partsTo = toDate.split('/');

                var from = new Date(partsFrom[2], partsFrom[1] - 1, partsFrom[0]); // dd/mm/yyyy
                var to = new Date(partsTo[2], partsTo[1] - 1, partsTo[0]);

                if (from > to) {
                    alert('From Date cannot be greater than To Date!');
                    // You can clear one or both fields, depending on preference:
                    fromDateElem.value = '';
                    // toDateElem.value = '';
                    fromDateElem.focus();
                }
            }
        }

        $('#exampleModal').on('shown.bs.modal', function () {
            // Destroy if already initialized
            if ($.fn.DataTable.isDataTable('.datatable2')) {
                $('.datatable2').DataTable().destroy();
            }
            // Initialize DataTable in modal
            initCustomDataTable('.datatable2', 'Employee task Detail', 'Employee task Detail');
            // Adjust columns after showing
            $('.datatable2').DataTable().columns.adjust();
        });
        $(document).ready(function () {
            initCustomDataTable('.datatable', 'Employee Wise Task Allocation Report', 'Employee Wise Task Allocation Report');
        });

    </script>
</asp:Content>

