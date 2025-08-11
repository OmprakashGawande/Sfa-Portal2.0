<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DailyTaskReport.aspx.cs" Inherits="mis_Report_DailyTaskReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .gv-custom th {
            background-color: #8a292d !important;
            color: white !important;
        }


        @media print {
            .NoPrint {
                display: none !important;
            }

            .page-break {
                page-break-before: always;
                break-before: page;
            }

            .print-header {
                display: block;
                text-align: center;
                margin-bottom: 15px;
            }

                .print-header h4, .print-header h5 {
                    text-decoration: underline;
                    margin: 0;
                }

            body {
                -webkit-print-color-adjust: exact;
                print-color-adjust: exact;
            }
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <!-- Main content -->
    <div class="content-wrapper">
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header NoPrint">
                            <h3 class="box-title">Daily Task Report</h3>
                        </div>
                        <hr />
                        <div class="box-body">
                            <asp:Label runat="server" ID="lblMsg" Text=""></asp:Label>

                            <div class="row" style="padding: 0px 9px 2px 15px;">
                                <div class="card NoPrint">
                                    <div class="card-body">

                                        <div class="row">
                                            <div class="col-md-3">

                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                        ControlToValidate="txtDate"
                                                        ValidationGroup="a"
                                                        ErrorMessage="Select Date"
                                                        ForeColor="Red"
                                                        Display="Dynamic"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Date!'></i>">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label runat="server">
                                                    DATE 
                                                    <label style="color: red;">*</label>

                                                </label>
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="txtDate"
                                                        data-provide="datepicker" placeholder="DD/MM/YYYY"
                                                        autocomplete="off" data-date-format="dd/mm/yyyy"
                                                        data-date-autoclose="true" CssClass="form-control disableFuturedate" OnTextChanged="txtDate_TextChanged"
                                                        AutoPostBack="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2" style="margin-top: 28px;">
                                                <asp:Button ID="btnLoad" runat="server" ValidationGroup="a" Text="Search" CssClass="btn btn-success" OnClick="btnLoad_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row NoPrint">
                                    <div class="col-md-9"></div>
                                    <div class="col-md-1">
                                        <div class="text-end mb-3">
                                            <asp:Button ID="btnPrint" runat="server"
                                                CssClass="btn btn-primary d-inline-block"
                                                Text="🖨️ Print Report"
                                                OnClientClick="printReport(); return false;" />
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="text-end mb-3">
                                           <asp:Button ID="btnExportToExcel" runat="server" Text="Export to Excel" CssClass="btn btn-success NoPrint" OnClick="btnExportToExcel_Click" />

                                        </div>
                                    </div>
                                </div>

                                <div id="printArea">
                                    <div class="row">
                                        <div class="print-header">
                                            <div class="col-md-12 text-center">

                                                <h4 style="text-decoration: underline">Daily Reporting List</h4>
                                                <h5 style="text-decoration: underline">
                                                    <asp:Label ID="lblSelectedDate" runat="server" ForeColor="Blue" Font-Bold="true" /></h5>
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:GridView ID="gvFilledTasks" runat="server" OnRowDataBound="gvFilledTasks_RowDataBound" AutoGenerateColumns="false" CssClass="table table-bordered table-striped gv-custom" EmptyDataText="No tasks filled for selected date">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSerial" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="ProjectName" HeaderText="Project Name">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="TaskAllocatedOrFilled" HeaderText="Task Allocated / Filled" />
                                                    <asp:BoundField DataField="TaskName" HeaderText="Task Name" />
                                                    <asp:BoundField DataField="TaskStatusText" HeaderText="Task Status" />
                                                    <asp:BoundField DataField="Hours" HeaderText="Hours" Visible="false" />
                                                    <asp:BoundField DataField="Minutes" HeaderText="Minutes" Visible="false" />
                                                    <asp:BoundField DataField="WorkingHours" HeaderText="Working Hours" />
                                                    <asp:BoundField DataField="TaskDiscription" HeaderText="Task Description" />

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="page-break">
                                    </div>
                                    <br />
                                    <hr />

                                    <%--  Project Wise Manpower Task Filled --%>
                                    <div class="print-header">
                                        <div class="row">
                                            <div class="col-md-12 text-center">
                                                <h4 style="text-decoration: underline">Project Wise Report</h4>
                                                <h5 style="text-decoration: underline">
                                                    <asp:Label ID="Label2" runat="server" ForeColor="Blue" Font-Bold="true" /></h5>
                                                <br />
                                            </div>
                                            <br />
                                            <hr />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3"></div>
                                        <div class="col-md-6">
                                            <asp:GridView ID="GridProjectwise" runat="server" OnRowDataBound="GridProjectwise_RowDataBound" CssClass="gv-custom  table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="All employees submitted tasks">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSerial" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ProjectName" HeaderText="Project Name" />
                                                    <asp:BoundField DataField="TotalEmployees" HeaderText="Total Manpower" />
                                                    <asp:BoundField DataField="TotalWorkingHours" HeaderText="Total Hours" />


                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-md-3"></div>
                                    </div>
                                    <div class="page-break">
                                    </div>

                                    <div class="row">
                                        <br>
                                        <div class="print-header">
                                            <div class="col-md-12 text-center">
                                                <h4 style="text-decoration: underline">Employee Task Not Filled Or On Leave</h4>
                                                <h5 style="text-decoration: underline">
                                                    <asp:Label ID="Label1" runat="server" ForeColor="Blue" Font-Bold="true" /></h5>
                                                <br />
                                            </div>
                                            <br />
                                            <hr />
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-3"></div>
                                        <div class="col-md-6">
                                            <asp:GridView ID="gvNotFilledTasks" runat="server" OnRowDataBound="gvNotFilledTasks_RowDataBound" CssClass="gv-custom  table table-bordered table-striped" AutoGenerateColumns="false" EmptyDataText="All employees submitted tasks">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSerial" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ManagerNames" HeaderText="Manager Name" />
                                                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server"
                                                                Text='<%# Eval("TaskStatusText") %>'
                                                                CssClass='<%# GetStatusCss(Eval("TaskStatusText").ToString()) %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-md-3"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>
    </div>


    <script type="text/javascript">
        function printReport() {

            window.print();
        }
    </script>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

