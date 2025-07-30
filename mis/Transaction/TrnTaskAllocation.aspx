<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="TrnTaskAllocation.aspx.cs" Inherits="mis_Transaction_TrnTaskAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        #txtDiscription {
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
        }

        #txtDiscription {
            border-color: #5c6ac4;
            outline: none;
            box-shadow: 0 0 0 3px rgba(92, 106, 196, 0.3);
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
                            <h3 class="box-title" style="margin-left: 2rem;">Task Allocation</h3>
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
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RFV2" ValidationGroup="a"
                                                        ErrorMessage="Select To Date" ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select MAIN POWER!'></i>"
                                                        ControlToValidate="ddlMainPower" InitialValue="0" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label runat="server">MAN POWER <span style="color: red;">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlMainPower" ClientIDMode="Static"
                                                    CssClass="form-control select2" OnSelectedIndexChanged="ddlMainPower_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RFV3" ValidationGroup="a"
                                                        ErrorMessage="Select To Date" ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select TASK!'></i>"
                                                        ControlToValidate="ddlTask" InitialValue="0" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label runat="server">TASK<span style="color: red;">*</span></label>
                                                <%--<asp:ListBox ID="ddlTask" runat="server" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>--%>
                                                <asp:DropDownList runat="server" ID="ddlTask" ClientIDMode="Static"
                                                    CssClass="form-control select2">
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>TASK CATEGOREY<span style="color: red;"> *</span></label>
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="a"
                                                        ErrorMessage="Select" ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Task Category'></i>"
                                                        ControlToValidate="ddlWorkCategoryId" InitialValue="0" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>

                                                <asp:DropDownList runat="server" ID="ddlWorkCategoryId" ClientIDMode="Static"
                                                    CssClass="form-control select2">
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator5"
                                                        ValidationGroup="a"
                                                        ErrorMessage="Select Start Date"
                                                        ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Work Order Date'></i>"
                                                        ControlToValidate="txtFromDate"
                                                        Display="Dynamic"
                                                        runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>

                                                <label>FROM DATE  <span style="color: red;">*</span></label>

                                                <asp:TextBox ID="txtFromDate" runat="server"
                                                    data-provide="datepicker"
                                                    placeholder="DD/MM/YYYY"
                                                    autocomplete="off"
                                                    data-date-format="dd/mm/yyyy"
                                                    data-date-autoclose="true"
                                                    CssClass="form-control"
                                                    data-date-start-date="-2d"
                                                    onkeydown="return false;" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator6"
                                                        ValidationGroup="a"
                                                        ErrorMessage="Select Start Date"
                                                        ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Complition Date'></i>"
                                                        ControlToValidate="txtToDate"
                                                        Display="Dynamic"
                                                        runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>

                                                <label>TO DATE <span style="color: red;">*</span></label>
                                                <asp:TextBox ID="txtToDate" runat="server"
                                                    data-provide="datepicker" placeholder="DD/MM/YYYY"
                                                    autocomplete="off" data-date-format="dd/mm/yyyy"
                                                    data-date-autoclose="true"
                                                    CssClass="form-control" />
                                                 <%--onchange="validateDates()"--%>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>TASK PRIORITY<span style="color: red;"> *</span></label>
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                        ErrorMessage="Select" ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Task Category'></i>"
                                                        ControlToValidate="ddlTaPriority" InitialValue="0" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:DropDownList runat="server" ID="ddlTaPriority" ClientIDMode="Static"
                                                    CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>DOCUMENT</label>
                                                <asp:FileUpload runat="server" ID="FUDoc" CssClass="form-control"></asp:FileUpload>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>REMARK<%--<span style="color: red;"> *</span>--%></label>
                                                <%--  <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator7"
                                                        ValidationGroup="a"
                                                        ErrorMessage="Enter REMARK"
                                                        ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter REMARK!'></i>"
                                                        ControlToValidate="txtDiscription"
                                                        Display="Dynamic"
                                                        runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>--%>
                                                <textarea
                                                    id="txtDiscription"
                                                    runat="server"
                                                    class="form-control"
                                                    oninput="autoResizeTextarea(this)"
                                                    onkeypress="javascript:tbx_fnAlphaOnly(event, this);"
                                                    placeholder="Enter Remark" maxlength="100"></textarea>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive" runat="server" id="DivWorkinProject" visible="false">
                                        <div class="col-md-12">
                                            <asp:GridView ID="grvWorkingProject" PageSize="50" runat="server" class="datatable  table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="false" AutoGenerateColumns="False" OnRowCommand="Grid_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PROJECT NAME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProjectName" Text='<%# Eval("ProjectName").ToString() %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MANAGER NAME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOWNERNAME" Text='<%# Eval("OWNERNAME").ToString() %>' runat="server"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ALLOCATION DATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAllocationDate" Text='<%# Eval("AllocationDate").ToString() %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button runat="server" Style="margin-top: 22px;" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" ValidationGroup="a" OnClick="btnSave_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <a href="TrnTaskAllocation.aspx" style="margin-top: 22px;" class="btn btn-block btn-default">Clear</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />

                            <%--grid--%>
                            <div class="card">
                                <div class="card">
                                    <div class="row" style="padding: 0px 9px 2px 15px;" id="div1" runat="server">
                                        <div>
                                            <h4 style="margin-left: 2rem;">Detail </h4>
                                        </div>
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
                                                        <asp:TemplateField HeaderText="MAN POWER ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmp_Name" Text='<%# Eval("Emp_Name").ToString() %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblEmpId" Text='<%# Eval("EmpId").ToString() %>' runat="server" Visible="false"></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TASK">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaskName" Text='<%# Eval("TaskName").ToString() %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblTaskId" Text='<%# Eval("TaskId").ToString() %>' runat="server" Visible="false"></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CATEGORY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblWorkCategoryEng" Text='<%# Eval("WorkCategoryEng").ToString() %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblCategoreyId" Text='<%# Eval("CategoreyId").ToString() %>' runat="server" Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TASK ALLOCATION DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaskAllocationDate" Text='<%# Eval("TaskAllocationDate").ToString() %>' runat="server" />

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
                                                        <asp:TemplateField HeaderText="TASK PRIORITY">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaskPriority" Text='<%# Eval("TaskPriority").ToString() %>' runat="server" />
                                                                <asp:Label ID="lblTaskPriorityId" Text='<%# Eval("TaskPriorityId").ToString() %>' Visible="false" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REMARK ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDiscrisption" Text='<%# Eval("Discription").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="WORK STATUS">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTaskStatusText" Text='<%# Eval("TaskStatusText").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DOCUMENT">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="HyperLink1" runat="server"
                                                                    Target="_blank"
                                                                    NavigateUrl='<%# 
                                                                            (Eval("TaskAllocationDoc") != null && Eval("TaskAllocationDoc") != DBNull.Value)
                                                                            && !string.IsNullOrWhiteSpace(Convert.ToString(Eval("TaskAllocationDoc")))
                                                                            && Convert.ToString(Eval("TaskAllocationDoc")).Trim().ToLower() != "null"
                                                                            && Convert.ToString(Eval("TaskAllocationDoc")).Trim() != "0"
                                                                            ? "~/mis/Document/" + Convert.ToString(Eval("TaskAllocationDoc")).Trim()
                                                                            : "" %>' CssClass="label label-info" Text="View"
                                                                    Visible='<%# (Eval("TaskAllocationDoc") != null && Eval("TaskAllocationDoc") != DBNull.Value) && !string.IsNullOrWhiteSpace(Convert.ToString(Eval("TaskAllocationDoc")))  && Convert.ToString(Eval("TaskAllocationDoc")).Trim().ToLower() != "null" && Convert.ToString(Eval("TaskAllocationDoc")).Trim() != "0" %>'>
                                                                </asp:HyperLink>
                                                                <%--<asp:HyperLink ID="hyperTaskAllocationDoc" runat="server" Target="_blank" Enabled='<%# Eval("TaskAllocationDoc").ToString() == "" ? false : true %>' NavigateUrl='<%# "~/mis/Document/" + Eval("TaskAllocationDoc") %>' CssClass="label label-info" Visible='<%# (Eval("TaskAllocationDoc") != null && Eval("TaskAllocationDoc") != DBNull.Value) && !string.IsNullOrWhiteSpace(Convert.ToString(Eval("TaskAllocationDoc")))  && Convert.ToString(Eval("TaskAllocationDoc")).Trim().ToLower() != "null" && Convert.ToString(Eval("TaskAllocationDoc")).Trim() != "0" %>' Text="View"></asp:HyperLink>--%>
                                                                <asp:Label ID="lblTaskAllocationDocPath" runat="server" Visible="false" Text='<%# Eval("TaskAllocationDoc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="30" HeaderText="STATUS">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkStatus" runat="server" CommandArgument='<%# Eval("TaskAllocationId").ToString()%>' CssClass='<%# Eval("IsActive").ToString() =="True"?"label label-success":"label label-danger"  %>' CausesValidation="False" CommandName="ChangeStatus" Text='<%# Eval("IsActive").ToString() =="True"?"Active":"Deactive"  %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="30px"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ACTION">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-default" CommandArgument='<%# Eval("TaskAllocationId").ToString()%>' CausesValidation="False" CommandName="EditRecord" Text="Edit"></asp:LinkButton>
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
    <style>
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }
    </style>
    <%--<link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>--%>
    <script>
        function autoResizeTextarea(ths) {
            ths.style.height = 'auto'; // Reset height to calculate new scrollHeight
            ths.style.height = ths.scrollHeight + 'px'; // Set height to fit content
        }
        //$('[id*=ddlTask]').multiselect({
        //    includeSelectAllOption: true,
        //    includeSelectAllOption: true,
        //    buttonWidth: '100%'
        //});
    </script>
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
                        title: 'Task Allocation',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: 'Task Allocation',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
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

       <%-- window.onload = function () {
            // ----- Start of Week (Monday) -----
            var today = new Date();
            var dayOfWeek = today.getDay(); // 0 = Sunday, 1 = Monday, ..., 6 = Saturday
            var diffToMonday = (dayOfWeek === 0 ? -6 : 1 - dayOfWeek);
            var monday = new Date(today);
            monday.setDate(today.getDate() + diffToMonday);

            // Format Monday (DD/MM/YYYY)
            var dd = String(monday.getDate()).padStart(2, '0');
            var mm = String(monday.getMonth() + 1).padStart(2, '0');
            var yyyy = monday.getFullYear();
            var mondayFormatted = dd + '/' + mm + '/' + yyyy;

            // Set Monday to txtFromDate
            document.getElementById('<%= txtFromDate.ClientID %>').value = mondayFormatted;

            // ----- End of Week (Sunday) -----
            var sunday = new Date(monday);
            sunday.setDate(monday.getDate() + 4);

            // Format Sunday (DD/MM/YYYY)
            var dd2 = String(sunday.getDate()).padStart(2, '0');
            var mm2 = String(sunday.getMonth() + 1).padStart(2, '0');
            var yyyy2 = sunday.getFullYear();
            var sundayFormatted = dd2 + '/' + mm2 + '/' + yyyy2;

            // Set Sunday to txtToDate
            document.getElementById('<%= txtToDate.ClientID %>').value = sundayFormatted;
        };--%>
    </script>
    <%--<script type="text/javascript">
        function validateDates() {
            var fromDate = document.getElementById('<%= txtFromDate.ClientID %>').value;
            var toDate = document.getElementById('<%= txtToDate.ClientID %>').value;

            if (fromDate && toDate) {
                var partsFrom = fromDate.split('/');
                var partsTo = toDate.split('/');

                var from = new Date(partsFrom[2], partsFrom[1] - 1, partsFrom[0]); // dd/mm/yyyy
                var to = new Date(partsTo[2], partsTo[1] - 1, partsTo[0]);

                if (from > to) {
                    alert('From Date cannot be greater than To Date!');
                    document.getElementById('<%= txtFromDate.ClientID %>').value = '';
                     document.getElementById('<%= txtToDate.ClientID %>').value = '';
                }
            }
        }
    </script>--%>
</asp:Content>

