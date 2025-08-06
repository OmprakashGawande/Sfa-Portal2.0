<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptProjectList.aspx.cs" Inherits="mis_Report_RptProjectList" %>

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
                            <h3 class="box-title">Projects List </h3>
                        </div>
                        <hr />
                        <div class="box-body">

                            <asp:Label runat="server" ID="lblMsg" Text=""></asp:Label>


                            <div class="row">
                                <div class="col-md-3" style="margin-left: 1rem;">
                                    <div class="form-group">
                                        <label>SELECT PROJECT TYPE</label>
                                        <asp:DropDownList ID="ddlTypeOfProject" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                            </div>
                            <hr />
                            <div class="row" style="padding: 0px 9px 2px 15px;">
                                <div class="table-responsive">
                                    <div class="col-md-12">
                                        <asp:GridView ID="Grid" PageSize="50" runat="server" class="datatable  table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="false" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" runat="server"
                                                            Text='<%# Container.DataItemIndex + 1 %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProjectName" runat="server"
                                                            Text='<%# Eval("ProjectName") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Type of Project">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTypeOfProject" runat="server"
                                                            Text='<%# Eval("TypeOfProjectName") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Start Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStartDate" runat="server"
                                                            Text='<%# Eval("ProjectStartDate") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="End Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEndDate" runat="server"
                                                            Text='<%# Eval("ProjectEndDate") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Duration (Days)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDuration" runat="server"
                                                            Text='<%# Eval("TotalNoOfDays") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Technology Used">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTechnology" runat="server"
                                                            Text='<%# Eval("TechnologyNames") %>' />
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        $(document).ready(function () {
            $('.datatable').DataTable({

                paging: false,

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
                        title: 'List of Projects',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6]
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: 'List of Projects',
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6]
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

