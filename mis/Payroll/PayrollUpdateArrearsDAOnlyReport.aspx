<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollUpdateArrearsDAOnlyReport.aspx.cs" Inherits="mis_Payroll_PayrollUpdateArrearsDAOnlyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Report Of Generated DA Arrears </h3>
                    <p><strong>Note:</strong> From Jan 2019 to August 2019  Permanent Officers/Employee <span style="color: blue">(12 %)</span> , Fixed Employee <span style="color: blue">(154%)</span></p>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Report Type<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlArrearMonths" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlArrearMonths_SelectedIndexChanged" Enabled="false">
                                    <asp:ListItem Value="1" Text="Report I (Jan'19 To March'19)" />
                                    <asp:ListItem Value="2" Text="Report II (Apr'19 To Aug'19)" />
                                    <asp:ListItem Value="3" selected="selected" Text="Combined Report ( Jan'19 To Aug'19 ) " />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Employee Type<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                    <asp:ListItem Value="Permanent" Text="Permanent Employee" />
                                    <asp:ListItem Value="Fixed Employee" Text="Fixed Employee" />
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered  datatable table-striped pagination-ys " ShowHeaderWhenEmpty="true" ClientIDMode="Static" AutoGenerateColumns="true" ShowFooter="true">
                                   <%-- <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%# Eval("CurrentMonthNo")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month">
                                            <ItemTemplate>
                                                <asp:Label ID="ArrearMonth" runat="server" Text='<%# Eval("ArrearMonth") %>' ToolTip='<%# Eval("CurrentYear")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid BasicSalary">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBasicSalary" runat="server" Text='<%# Eval("BasicSalary") %>' CssClass="form-control" placeholder="Basic Salary" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid Basic Salary In Arrear">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtArrearBasicSalary" runat="server" Text='<%# Eval("BasicPaidInArrear") %>' CssClass="form-control" placeholder="Arrears Basic Salary" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Basic Salary Paid">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTotalBasicSalary" runat="server" Text='<%# Eval("TotalBasicPaid") %>' CssClass="form-control" placeholder="Total Basic Paid" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid DA">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPaidDa" runat="server" Text='<%# Eval("PaidDa") %>' CssClass="form-control" placeholder="Paid Da" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DA To Be Paid(As Per New Rate)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDaToBePaid" runat="server" Text='<%# Eval("DaToBePaid") %>' CssClass="form-control" placeholder="DA To Be Paid" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DA Remaining Arrear Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDaRemainingArrearAmount" runat="server" Text='<%# Eval("DaRemainingArrearAmount") %>' CssClass="form-control txtDaRemainingArrearAmount" onkeypress="return validateNum(event)" placeholder="DaRemainingArrearAmount" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remaining EPF Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRemainingEpfAmount" runat="server" Text='<%# Eval("RemainingEpfAmount") %>' CssClass="form-control txtRemainingEpfAmount" placeholder="RemainingEpfAmount" onkeypress="return validateNum(event)" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Payment">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNetPayment" runat="server" Text='<%# Eval("NetPayment") %>' CssClass="form-control txtNetPayment" placeholder="NetPayment" onkeypress="return validateNum(event)" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>--%>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../finance/js/jquery.dataTables.min.js"></script>
    <script src="../finance/js/jquery.dataTables.min.js"></script>
    <script src="../finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../finance/js/dataTables.buttons.min.js"></script>
    <script src="../finance/js/buttons.flash.min.js"></script>
    <script src="../finance/js/jszip.min.js"></script>
    <script src="../finance/js/pdfmake.min.js"></script>
    <script src="../finance/js/vfs_fonts.js"></script>
    <script src="../finance/js/buttons.html5.min.js"></script>
    <script src="../finance/js/buttons.print.min.js"></script>
    <script src="../finance/js/buttons.colVis.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: false,
            aaSorting: [],
            columnDefs: [{
                targets: 'no-sort',
                orderable: false,
                bsort: false,
                bSortable: false
            }],
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
                    title: "DA Arrear Report ",
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: "DA Arrear Report  ",
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
    </script>

</asp:Content>

