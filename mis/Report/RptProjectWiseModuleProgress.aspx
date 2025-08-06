<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptProjectWiseModuleProgress.aspx.cs" Inherits="mis_Report_RptProjectWiseModuleProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Project Wise Module Progress</h3>
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
                                                <asp:DropDownList runat="server" ID="ddlProjectName" ClientIDMode="Static" CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Button runat="server" Style="margin-top: 22px;" CssClass="btn btn-success btn-sm" ID="btnSearch" Text="Search" ValidationGroup="a" OnClick="btnSearch_Click" />
                                                    <a href="ProjectWiseModuleProgress.aspx" style="margin-top: 22px;" class="btn btn-sm btn-default">Clear</a>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <hr />
                            </div>
                            <br />
                            <%--grid--%>
                            <div class="card">
                                <div class="card-body">
                                    <div class="row" style="padding: 0px 9px 2px 15px;" id="div1" runat="server">
                                        <div>
                                            <h4 style="margin-left: 2rem;">Detail </h4>
                                        </div>
                                        <div class="table-responsive">
                                            <div class="col-md-12">
                                                <asp:GridView ID="Grid" PageSize="50" runat="server" class="datatable  table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="false" AutoGenerateColumns="False">
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
                                                        <asp:TemplateField HeaderText="MODULE NAME ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTModuleName" Text='<%# Eval("ModuleName").ToString() %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MODULE PHASE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPhase" Text='<%# Eval("PhaseName").ToString() %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblPhaseId" Text='<%# Eval("ModulePhase").ToString() %>' runat="server" Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REMARK ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemark" Text='<%# Eval("Remark").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MODULE STATUS UPDATED BY ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpName" Text='<%# Eval("EmpName").ToString() %>' runat="server" />
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
    <script>
        $(document).ready(function () {
            // Function to format date for filename: dd_mm_yyyy_HH_MM_AM/PM
            function getFormattedDateTimeForFileName() {
                var now = new Date();
                var day = ("0" + now.getDate()).slice(-2);
                var month = ("0" + (now.getMonth() + 1)).slice(-2);
                var year = now.getFullYear();

                var hours = now.getHours();
                var minutes = ("0" + now.getMinutes()).slice(-2);

                var ampm = hours >= 12 ? 'PM' : 'AM';
                hours = hours % 12;
                hours = hours ? hours : 12; // the hour '0' should be '12'
                var strHours = ("0" + hours).slice(-2);

                return day + "_" + month + "_" + year + "_" + strHours + "_" + minutes + "_" + ampm;
            }
            // Function to format date for display inside Excel (dd/mm/yyyy HH:MM AM/PM)
            function getFormattedDateTimeForExcel() {
                var now = new Date();
                var day = ("0" + now.getDate()).slice(-2);
                var month = ("0" + (now.getMonth() + 1)).slice(-2);
                var year = now.getFullYear();

                var hours = now.getHours();
                var minutes = ("0" + now.getMinutes()).slice(-2);

                var ampm = hours >= 12 ? 'PM' : 'AM';
                hours = hours % 12;
                hours = hours ? hours : 12;
                var strHours = ("0" + hours).slice(-2);

                return day + "/" + month + "/" + year + " " + strHours + ":" + minutes + " " + ampm;
            }
            var dateTimeForFileName = getFormattedDateTimeForFileName();
            var dateTimeForExcel = getFormattedDateTimeForExcel();
            var t = $('.datatable').DataTable({
                paging: true,
                columnDefs: [{
                    targets: 'no-sort',
                    orderable: false
                }],
                order: [[0, 'asc']],

                dom: '<"row"<"col-sm-6"B><"col-sm-6"f>>' +
                    '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
                    '<"row"<"col-sm-5"i><"col-sm-7"p>>',
                fixedHeader: {
                    header: true
                },

                buttons: {
                    buttons: [
                        {
                            extend: 'print',
                            text: '<i class="fa fa-print"></i> Print',
                            title: 'Project Wise Module phase',
                            exportOptions: {
                                columns: [0, 1, 2, 3, 4, 5]
                            },
                            footer: true,
                            autoPrint: true
                        },
                        {
                            extend: 'excel',
                            text: '<i class="fa fa-file-excel-o"></i> Excel',
                            title: 'Project Wise Module phase',
                            filename: 'ProjectWiseModulePhase_' + dateTimeForFileName,
                            exportOptions: {
                                columns: [0, 1, 2, 3, 4, 5],
                                // Make sure headers are included
                                orthogonal: 'export'
                            },
                            footer: true,
                            customize: function (xlsx) {
                                var sheet = xlsx.xl.worksheets['sheet1.xml'];
                                var sheetData = sheet.getElementsByTagName('sheetData')[0];

                                // Create a new row with export date/time
                                var row2 = `<row r="1">
                    <c r="A1" t="inlineStr">
                        <is><t>Exported on: ${dateTimeForExcel}</t></is>
                    </c>
                </row>`;

                                // Shift all existing rows down by 1 starting from row 1
                                var rows = sheetData.getElementsByTagName('row');
                                for (var i = rows.length - 1; i >= 0; i--) {
                                    var row = rows[i];
                                    var r = parseInt(row.getAttribute('r'));
                                    var newR = r + 1;
                                    row.setAttribute('r', newR);
                                    // Also update the cell references inside the row
                                    var cells = row.getElementsByTagName('c');
                                    for (var j = 0; j < cells.length; j++) {
                                        var cell = cells[j];
                                        var cellRef = cell.getAttribute('r');
                                        var col = cellRef.match(/[A-Z]+/)[0]; // letters only
                                        cell.setAttribute('r', col + newR);
                                    }
                                }

                                // Insert new row 1 at the top of the sheet
                                var parser = new DOMParser();
                                var newRow = parser.parseFromString(row2, 'text/xml').documentElement;
                                sheetData.insertBefore(newRow, sheetData.firstChild);
                            }

                        }
                    ],

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

            // Update serial numbers on order or search
            t.on('order.dt search.dt', function () {
                t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
            }).draw();
        });
    </script>
    >t>









</asp:Content>

