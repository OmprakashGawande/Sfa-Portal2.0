<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptTaskAllocationStatics.aspx.cs" Inherits="mis_Report_RptTaskAllocationStatics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <!-- Main content -->
    <div class="content-wrapper">
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Task Allocation Report</h3>
                        </div>
                        <hr />
                        <asp:Label runat="server" ID="lblMsg" Text=""></asp:Label>
                        <div class="row" style="padding: 0px 9px 2px 15px;">
                            <div class="col-md-3">
                                <div class="form-group">

                                    <label runat="server">DATE <span style="color: red;">*</span></label>
                                    <asp:TextBox runat="server" ID="txtDate"
                                        data-provide="datepicker" placeholder="DD/MM/YYYY"
                                        autocomplete="off" data-date-format="dd/mm/yyyy"
                                        data-date-autoclose="true" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>


                            <div class="col-md-1">
                                <div class="form-group">
                                    <asp:Button runat="server" Style="margin-top: 22px;" CssClass="btn btn-block btn-success" ID="btnSearch" Text="Search" ValidationGroup="a" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                            <div class="col-md-1">
                                <div class="form-group">
                                    <a href="RptTaskAllocationStatics.aspx" style="margin-top: 22px;" class="btn btn-block btn-default">Clear</a>
                                </div>
                            </div>
                        </div>
                        <hr />
                        &nbsp
                        &nbsp
                        &nbsp
                        <p style="color:cornflowerblue ;     font-weight: 800;">
                            &nbsp
                        &nbsp
                        &nbsp Task Allocaton Report From Date  <asp:Label ID="lblWeekStart" runat="server" /> To Date <asp:Label ID="lblWeekEnd" runat="server" />
                                   
                            
                        </p>
                        <div class="row" id="Datagrid" runat="server" style="padding: 0px 9px 2px 15px;">
                            <div class="col-md-12">
                                <asp:GridView ID="Grid" PageSize="50" runat="server"
                                    class="datatable table table-hover table-bordered pagination-ys"
                                    ShowHeaderWhenEmpty="false" AutoGenerateColumns="False" OnRowCommand="Grid_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Allocated By">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAllocatedBy" runat="server" Text='<%# Eval("AllocatedBy") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Emp ID And ProjectId and AssignedToEmpId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpId" runat="server" Text='<%# Eval("Emp_ID") %>' />
                                                <asp:Label ID="lblProjectId" runat="server" Text='<%# Eval("ProjectId") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Role AssignedToEmpId" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRole" runat="server" Text='<%# Eval("Role") %>' />
                                                <asp:Label ID="lblAssignedToEmpId" runat="server" Text='<%# Eval("AssignedToEmpId") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tasks Assigned Today">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTodayTasks" runat="server" Text='<%# Eval("TotalTasksAssignedToday") %>' />

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tasks This Week">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWeekTasks" runat="server" Text='<%# Eval("TotalTasksAssignedThisWeek") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Completed Tasks">
                                            <ItemTemplate>
                                                <asp:LinkButton
                                                    ID="lnkTotalTasksCompleted"
                                                    runat="server"
                                                    Text='<%# Eval("TotalTasksCompleted").ToString() == "0" ? "0" : Eval("TotalTasksCompleted").ToString() %>'
                                                    CommandName="ViewCompleteTasks"
                                                    CommandArgument='<%# Eval("ProjectId") + ";" + Eval("Emp_ID") + ";" + Eval("Role") + ";" + Eval("AssignedToEmpId") %>'
                                                    CssClass="btn btn-link p-0"
                                                    ToolTip="Click to view completed tasks" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tasks In Progress">
                                            <ItemTemplate>
                                                <asp:LinkButton
                                                    ID="lnkTotalTasksInProgress"
                                                    runat="server"
                                                    Text='<%# Eval("TotalTasksInProgress").ToString() == "0" ? "0" : Eval("TotalTasksInProgress").ToString() %>'
                                                    CommandName="ViewWipTasks"
                                                    CommandArgument='<%# Eval("ProjectId") + ";" + Eval("Emp_ID") + ";" + Eval("Role") + ";" + Eval("AssignedToEmpId") %>'
                                                    CssClass="btn btn-link p-0"
                                                    ToolTip="Click to view Work In Progress tasks" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pending Tasks">
                                            <ItemTemplate>
                                                <asp:LinkButton
                                                    ID="lnkViewTotalTasksPending"
                                                    runat="server"
                                                    Text='<%# Eval("TotalTasksPending").ToString() == "0" ? "0" : Eval("TotalTasksPending").ToString() %>'
                                                    CommandName="ViewPendingTasks"
                                                    CommandArgument='<%# Eval("ProjectId") + ";" + Eval("Emp_ID") + ";" + Eval("Role") + ";" + Eval("AssignedToEmpId") %>'
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

            <!-- Bootstrap Modal -->
            <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title" id="exampleModalLongTitle">Task Detail</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>

                        <div class="card">
                            <asp:Label runat="server" ID="lblMsgManPower" Text=""></asp:Label>
                            <div class="card-body fa-border">
                                <!-- Modal Body -->
                                <div class="modal-body">
                                    <div class="row" style="padding: 0px 9px 2px 15px;">

                                        <div class="table-responsive">
                                            <div class="col-md-12">
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
                                        data-dismiss="modal">
                                        Close
                                    </button>
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
    <script>
        function setupDataTable() {
            // Destroy if already exists
            if ($.fn.DataTable.isDataTable('#<%= GridTaskDetail.ClientID %>')) {
                $('#<%= GridTaskDetail.ClientID %>').DataTable().destroy();
            }

            $('#<%= GridTaskDetail.ClientID %>').DataTable({
                paging: true,
                searching: true,
                info: true,
                ordering: true,
                responsive: true,

                // Add buttons extension
                dom: 'Bfrtip',  // Position buttons at the top

                buttons: [
                    {
                        extend: 'excelHtml5',
                        text: '<i class="btn btn-default fa fa-file-excel-o"> Excel</i> ',
                        title: 'Task Detail Report',
                        exportOptions: {
                            columns: ':visible'
                        },
                        footer: true
                    },
                    {
                        extend: 'pdfHtml5',
                        text: '<i class="btn btn-default fa fa-file-pdf-o"> PDF</i> ',
                        title: 'Task Detail Report',
                        exportOptions: {
                            columns: ':visible'
                        },
                        footer: true,
                        orientation: 'landscape',
                        pageSize: 'A4'
                    },
                    {
                        extend: 'print',
                        text: '<i class="btn btn-default fa fa-print"> Print</i> ',
                        title: 'Task Detail Report',
                        exportOptions: {
                            columns: ':visible'
                        },
                        footer: true,
                        autoPrint: true
                    }
                ]
            });
        }

        $(document).ready(function () {

            // Function to initialize DataTable with common options
            function setupDataTable($table, options = {}) {
                if ($.fn.DataTable.isDataTable($table)) {
                    $table.DataTable().destroy();
                }
                return $table.DataTable($.extend(true, {
                    paging: false,
                    ordering: true,
                    order: [[0, 'asc']],
                    columnDefs: [{
                        targets: 'no-sort',
                        orderable: false
                    }],
                    dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
                        '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
                        '<"row"<"col-sm-5"i><"col-sm-7"p>>',
                    fixedHeader: { header: true },
                    buttons: [
                        {
                            extend: 'print',
                            text: '<i class="fa fa-print"></i> Print',
                            title: options.title || 'Report',
                            exportOptions: {
                                columns: options.exportColumns || ':visible'
                            },
                            footer: true,
                            autoPrint: true
                        },
                        {
                            extend: 'excel',
                            text: '<i class="fa fa-file-excel-o"></i> Excel',
                            title: options.title || 'Report',
                            exportOptions: {
                                columns: options.exportColumns || ':visible'
                            },
                            footer: true
                        }
                    ],
                    buttons: {
                        dom: {
                            container: { className: 'dt-buttons' },
                            button: { className: 'btn btn-default' }
                        }
                    }
                }, options));
            }



            // 🧩 Modal table (GridView with class "datatable2")
            $('#exampleModal').on('shown.bs.modal', function () {
                const $modalTable = $('.datatable2');

                // Avoid destroying uninitialized tables
                if ($.fn.DataTable.isDataTable($modalTable)) {
                    $modalTable.DataTable().destroy();
                }

                const modalDT = setupDataTable($modalTable, {
                    title: 'Task Detail Report'
                });

                // Serial number for modal table
                modalDT.on('order.dt search.dt', function () {
                    modalDT.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = i + 1;
                    });
                }).draw();
            });

        });




        window.onload = function () {
            function formatDate(d) {
                var dd = String(d.getDate()).padStart(2, '0');
                var mm = String(d.getMonth() + 1).padStart(2, '0');
                var yyyy = d.getFullYear();
                return dd + '/' + mm + '/' + yyyy;
            }

            function setWeekRange(date) {
                var dayOfWeek = date.getDay();
                var diffToMonday = (dayOfWeek + 6) % 7;
                var weekStart = new Date(date);
                weekStart.setDate(date.getDate() - diffToMonday);
                var weekEnd = new Date(weekStart);
                weekEnd.setDate(weekStart.getDate() + 6);

                document.getElementById('<%= lblWeekStart.ClientID %>').innerText = formatDate(weekStart);
                document.getElementById('<%= lblWeekEnd.ClientID %>').innerText = formatDate(weekEnd);
            }

            var today = new Date();
            var txtDate = document.getElementById('<%= txtDate.ClientID %>');
            txtDate.value = formatDate(today);
            setWeekRange(today);

            // Assuming Bootstrap Datepicker is initialized on txtDate
            $('#<%= txtDate.ClientID %>').datepicker()
                .on('changeDate', function (e) {
                    var selectedDate = e.date;  // e.date is a JS Date object
                    setWeekRange(selectedDate);
                });
        };


        document.getElementById('<%= txtDate.ClientID %>').addEventListener('input', function () {
            var val = this.value; // format yyyy-mm-dd (HTML5 date input format)
            var parts = val.split('-');
            if (parts.length === 3) {
                var y = parseInt(parts[0], 10);
                var m = parseInt(parts[1], 10) - 1;
                var d = parseInt(parts[2], 10);
                var selectedDate = new Date(y, m, d);
                if (!isNaN(selectedDate)) {
                    setWeekRange(selectedDate);
                }
            }
        });


    </script>
</asp:Content>

