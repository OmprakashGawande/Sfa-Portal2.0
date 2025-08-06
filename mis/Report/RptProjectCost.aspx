<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptProjectCost.aspx.cs" Inherits="mis_Report_RptProjectCost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Projects Cost Employee Wise </h3>
                        </div>
                        <hr />
                        <div class="box-body">

                            <asp:Label runat="server" ID="lblMsg" Text=""></asp:Label>


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
                                        <asp:DropDownList runat="server" ID="ddlProjectName" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged"
                                            CssClass="form-control select2">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>
                            <hr />
                            <div class="row" style="padding: 0px 9px 2px 15px;">
                                <div class="table-responsive">
                                    <div class="col-md-12">
                                        <asp:GridView ID="Grid" runat="server" ShowFooter="true"
                                            AutoGenerateColumns="False" CssClass="table table-hover table-bordered pagination-ys"
                                            OnRowDataBound="Grid_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" runat="server" Text='<%# Container.DataItemIndex + 1 %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("Emp_Name") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Gross Salary">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmp_GrossSalery" runat="server" Text='<%# Eval("Emp_GrossSalery") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Per Day Salary">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPerDaySalary" runat="server" Text='<%# Eval("PerDaySalary") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Per Hour Salary">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPerHourSalary" runat="server" Text='<%# Eval("PerHourSalary") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Hour Worked">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalHoursWorked" runat="server" Text='<%# Eval("TotalHoursWorked") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalHoursWorkedFooter" runat="server" Font-Bold="true" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Days Worked">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalDaysWorked" runat="server" Text='<%# Eval("TotalDaysWorked") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalDaysWorkedFooter" runat="server" Font-Bold="true" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Cost">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalCost" runat="server" Text='<%# Eval("TotalCost") %>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblTotalCostFooter" runat="server" Font-Bold="true" />
                                                    </FooterTemplate>
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
        </section>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>
        $(document).ready(function () {
            var t = $('.datatable').DataTable({
                paging: true,
                responsive: true,
                columnDefs: [{
                    targets: 'no-sort',
                    orderable: false
                }],
                order: [[0, 'asc']],
                dom: '<"row mb-3"<"col-sm-6"Bl><"col-sm-6"f>>' +
                    '<"row"<"col-sm-12 table-responsive"tr>>' +
                    '<"row mt-3"<"col-sm-5"i><"col-sm-7"p>>',
                fixedHeader: {
                    header: true
                },
                buttons: [
                    {
                        extend: 'print',
                        text: '<i class="fa fa-print"></i> Print',
                        title: 'List of Projects',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6],
                            footer: true
                        },
                        autoPrint: true
                    },
                    {
                        extend: 'excel', 
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: 'List of Projects',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6],
                            footer: true
                        }
                    }
                ]
            });

            t.on('order.dt search.dt', function () {
                t.column(0, { search: 'applied', order: 'applied' })
                    .nodes()
                    .each(function (cell, i) {
                        cell.innerHTML = i + 1;
                    });
            }).draw();
        });



    </script>
</asp:Content>

