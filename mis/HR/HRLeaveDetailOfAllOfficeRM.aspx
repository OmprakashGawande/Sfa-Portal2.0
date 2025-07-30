<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRLeaveDetailOfAllOfficeRM.aspx.cs" Inherits="mis_HR_HRLeaveDetailOfAllOfficeRM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }

        .dt-buttons {
            margin-bottom: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Office Wise Leave Detail (RM)</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4 hidden">
                            <div class="form-group">
                                <label>Office Name</label>
                                <asp:DropDownList ID="ddlOfif" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Financial Year</label>
                                <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name</label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <%-- <div class="table table-responsive">--%>
                            <asp:GridView ID="GridView1" DataKeyNames="LeaveId" class="datatable table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Leave Status" ItemStyle-Width="11%">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Eval("LeaveStatus".ToString())%>' runat="server" ID="lbLeaveStatus"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />
                                    <asp:BoundField DataField="LeaveFromDate" HeaderText="From Date (YYYY/MM/DD)" SortExpression="LeaveFromDate" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                                    <asp:BoundField DataField="LeaveToDate" HeaderText="To Date (YYYY/MM/DD)" SortExpression="LeaveToDate" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                                    <asp:TemplateField HeaderText="View More" ShowHeader="False" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="ViewMore" CssClass="label label-info" runat="server" CommandName="Select">View More</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lblMsg2" runat="server" Text="" Style="color: red; font-size: 15px;"></asp:Label>
                            <%-- </div>--%>
                        </div>
                    </div>
                </div>
                <div id="myModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Leave Approval</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Leave Request</legend>
                                            <div class="table-responsive">
                                                <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateRows="false">
                                                    <Fields>
                                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                        <asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />
                                                        <asp:BoundField DataField="LeaveFromDate" HeaderText="From Date" />
                                                        <asp:BoundField DataField="LeaveToDate" HeaderText="To Date" />
                                                        <asp:TemplateField HeaderText="Leave Request Doc" ItemStyle-Width="70%">
                                                            <ItemTemplate>
                                                                <a href='<%# Eval("LeaveDocument") %>' target="_blank" class="label label-info"><%# Eval("LeaveDocument").ToString() != "" ? "View" : "" %></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Fields>
                                                </asp:DetailsView>
                                                <div class="form-group">
                                                    <asp:Label ID="lblr" runat="server" Text="छुट्टी का कारण (Reason Of Leave)"></asp:Label>
                                                </div>
                                                <div class="form-group">

                                                    <div id="LeaveReason" runat="server">
                                                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Reply from Approval Authority </legend>
                                            <div class="table-responsive">
                                                <asp:DetailsView ID="DetailsView2" runat="server" CssClass="datatable table table-bordered table-striped table-hover" AutoGenerateRows="false">
                                                    <Fields>
                                                        <asp:TemplateField HeaderText="Leave Status" ItemStyle-Width="70%">
                                                            <ItemTemplate>
                                                                <asp:Label Text='<%# Eval("LeaveStatus".ToString())%>' runat="server" ID="lbLeaveStatus"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="LeaveApprovalOrderNo" HeaderText="Order No" />
                                                        <asp:BoundField DataField="LeaveApprovalOrderDate" HeaderText="Order Date" />
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblDocHeader" runat="server" Text='Doc'></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <a href='<%# Eval("LeaveApprovalOrderFile") %>' target="_blank" class="label label-info"><%# Eval("LeaveApprovalOrderFile").ToString() != "" ? "View" : "" %></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Fields>
                                                </asp:DetailsView>
                                                <div class="form-group">
                                                    <asp:Label ID="Label1" runat="server" Text="Remark/Comment"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <div id="HRRemark" runat="server">
                                                        <asp:TextBox ID="txtRemarkByHR" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
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
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true,
                    autoPrint: true
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


        function callalert() {
            $("#myModal").modal('show');
        }
    </script>
</asp:Content>

