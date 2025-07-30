<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ItemCostingMethod.aspx.cs" Inherits="mis_Finance_ItemCostingMethod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
<style>
        .show_detail {
            margin-top: 24px;
        }

        .table > tbody > tr > th {
            padding: 5px;
        }

        a:hover {
            color: red;
        }

        /*tr:hover td {
            background-color: #fefefe !important;
        }*/
        table.dataTable tbody td, table.dataTable thead td {
            padding: 5px 5px !important;
        }

        table.dataTable tbody th, table.dataTable thead th {
            padding: 8px 10px !important;
        }

        table.dataTable thead th, table.dataTable thead td {
            padding: 5px 7px;
            border-bottom: none !important;
        }

        table.dataTable tfoot th, table.dataTable tfoot td {
            border-bottom: none !important;
        }

        table.dataTable.no-footer {
            border-bottom: none !important;
        }

        a.dt-button.buttons-collection.buttons-colvis, a.dt-button.buttons-collection.buttons-colvis:hover {
            background: #EF5350;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.dt-button.buttons-excel.buttons-html5, a.dt-button.buttons-excel.buttons-html5:hover {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.dt-button.buttons-print, a.dt-button.buttons-print:hover {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

        thead tr th {
            background: #9e9e9ea3 !important;
        }

        tbody tr td:not(:first-child), tfoot tr td:not(:first-child) {
            text-align: right !important;
        }

        .Dtime {
            display: none;
        }

        @media print {
            .hide_print, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
            }

            tfoot, thead {
                display: table-row-group;
                bottom: 0;
            }

            .Dtime {
                display: block;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Item Costing Method</h3>
                    <asp:HyperLink ID="hyperlink" runat="server" NavigateUrl="RptStockSummaryItem.aspx" CssClass="badge bg-teal new_back_button">Click Here to go to Stock Summary Page</asp:HyperLink>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Office</label><span class="text-danger">*</span>
                                <asp:DropDownList ID="ddlOffice" ClientIDMode="Static" runat="server" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Item</label><span class="text-danger">*</span>
                                <asp:DropDownList ID="ddlItem" runat="server" ClientIDMode="Static" CssClass="form-control select2" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Costing Method</label><span class="text-danger">*</span>
                                <asp:DropDownList ID="ddlcostingmethod" runat="server" ClientIDMode="Static" CssClass="form-control select2" OnSelectedIndexChanged="ddlcostingmethod_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <%--<asp:ListItem Value="Average Costing">Average Costing</asp:ListItem>--%>
                                    <asp:ListItem Value="Standard Costing">Standard Costing</asp:ListItem>
                                    <%--                                    <asp:ListItem Value="NRV">NRV</asp:ListItem>
                                    <asp:ListItem Value="FIFO">FIFO</asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Standard Cost</label><span class="text-danger">*</span>
                                <asp:TextBox ID="txtstandardcost" runat="server" ClientIDMode="Static" CssClass="form-control" placeholder="Enter Std Cost"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>दिनांक से (From Date)<span class="text-red">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="From Date" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>दिनांक तक  (To Date)<span class="text-red">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="To Date" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-success" ClientIDMode="Static" ID="btnUpdate" Text="Update" OnClientClick="return validateform();" OnClick="btnUpdate_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a href="ItemCostingMethod.aspx" id="btnClear" runat="server" class="btn btn-block btn-default">Clear</a>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="CostingId" OnRowDeleting="GridView1_RowDeleting" EmptyDataText="No Record Found.">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                    <asp:BoundField DataField="ItemCostingMethod" HeaderText="CostingMethod" />
                                     <asp:BoundField DataField="ItemStandardCost" HeaderText="Cost" HeaderStyle-Width="100" />
                                    <asp:BoundField DataField="FromDate" HeaderText="From Date" HeaderStyle-Width="100" />
                                    <asp:BoundField DataField="ToDate" HeaderText="To Date" HeaderStyle-Width="100" />
                                    <asp:TemplateField HeaderText="Action" ShowHeader="False" ItemStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Costing Method will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>


                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>
    <script src="js/dataTables.buttons.min.js"></script>
    <script src="js/buttons.flash.min.js"></script>
    <script src="js/jszip.min.js"></script>
    <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>
    <script src="js/buttons.html5.min.js"></script>
    <script src="js/buttons.print.min.js"></script>
    <script src="js/buttons.colVis.min.js"></script>
    <script>

        //alert($('.printtext').attr('title'));

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
                    //title: $('h1').text(),
                    title: 'Costing Method',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4,5]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    //title: $('h1').text(),
                    title: 'Costing Method',
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4,5]
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


        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlItem.ClientID%>').selectedIndex == 0) {
                msg += "Select Item Name. \n";
            }
            if (document.getElementById('<%=ddlcostingmethod.ClientID%>').selectedIndex == 0) {
                msg += "Select Costing Method. \n";
            }
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select From Date. \n";
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg += "Select To Date. \n";
            }
            if (document.getElementById('<%=ddlcostingmethod.ClientID%>').selectedIndex == 2) {
                if (document.getElementById('<%=txtstandardcost.ClientID%>').value.trim() == "") {
                    msg += "Enter Standard Cost. \n";
                }
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {

                return true;

            }
        }
        function onchange() {
            debugger;
            var textbox = document.getElementsByName("txtstandardcost");
            var Index = document.getElementById('<%=ddlcostingmethod.ClientID%>').selectedIndex;
            if (Index == 1) {
                textbox.setAttribute("disabled", false);
            }
            else {

            }
        }

        $('#txtFromDate').change(function () {
            //debugger;
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');

            if ($('#txtToDate').val() != "") {
                if (start > end) {

                    if ($('#txtFromDate').val() != "") {
                        alert("From date should not be greater than To Date.");
                        $('#txtFromDate').val("");
                    }
                }
            }
            Showhide();
        });
        $('#txtToDate').change(function () {
            //debugger;
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');

            if (start > end) {

                if ($('#txtToDate').val() != "") {
                    alert("To Date can not be less than From Date.");
                    $('#txtToDate').val("");
                }
            }
            Showhide();
        });


    </script>
</asp:Content>

