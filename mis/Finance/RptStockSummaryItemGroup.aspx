<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="RptStockSummaryItemGroup.aspx.cs" Inherits="mis_Finance_RptStockSummaryItemGroup" %>

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
            padding: 1px 1px !important;
        }

        table.dataTable tbody th, table.dataTable thead th {
            padding: 2px 3px !important;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content-header">
            <h1>Stock Summary
                   <small></small>
            </h1>
        </section>
        <section class="content">
            <div class="box box-pramod" style="background-color: #FFFFFF;">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="box-body">
                                <legend>Stock Summary</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>From Date<span style="color: red;">*</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>To Date</label><span style="color: red">*</span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Office Name</label><span style="color: red">*</span>
                                            <%--                                            <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" AutoPostBack="true">
                                            </asp:DropDownList>--%>
                                            <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button ID="btn" CssClass="btn btn-md btn-primary show_detail Aselect1" runat="server" Text="Show Stock Summary" OnClick="btn_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row" id="DivGrid2" runat="server">
                                    <div class="col-md-12">
                                        <fieldset class="box-body">
                                            <asp:Label ID="lblheadingFirst" runat="server" Text=""></asp:Label>
                                            <div id="FirstPrint" class="table-responsive FirstPrint" runat="server">

                                                <%--<asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />--%>



                                                <asp:GridView ID="GridView2" runat="server" ShowFooter="True" AutoGenerateColumns="False" class="table table-hover table-bordered GridView2" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found" OnRowCreated="grvMergeHeader_RowCreated">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Particular">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnParticular" CausesValidation="false" CommandName="Select" Text='<%# Eval("Particular") %>' ToolTip='<%# Eval("Item_Id") %>' runat="server" CssClass="Aselect1">LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ControlStyle Font-Bold="True" />
                                                            <FooterStyle BackColor="#EAEAEA" Font-Bold="True" />
                                                            <HeaderStyle BackColor="#EAEAEA" />
                                                            <ItemStyle BackColor="#EAEAEA" Font-Bold="True" />
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="Item_Id" HeaderText="Item Id" />--%>
                                                        <asp:BoundField DataField="OpeningQuantity" HeaderText="Opening Quantity" HeaderStyle-BackColor="Red">
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OpeningRate" HeaderText="Opening Rate">
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OpeningValue" HeaderText="Opening Value">

                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="InwardQuantity" HeaderText="Inward Quantity">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InwardRate" HeaderText="Inward Rate">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InwardValue" HeaderText="Inward Value">

                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="OutwardQuantity" HeaderText="Outward Quantity">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OutwardRate" HeaderText="Outward Rate">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OutwardValue" HeaderText="Outward Value">

                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="ClosingQuantity" HeaderText="Closing Quantity">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ClosingRate" HeaderText="Closing Rate">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ClosingValue" HeaderText="Closing Value">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>


                                <div class="row" id="DivGrid3" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <fieldset class="box-body">
                                            <div class="table-responsive">
                                                <p class="text-center">

                                                    <asp:Button ID="btnDaily" runat="server" Text="Daily Break-Up" CssClass="btn btn-warning btn-sm btn-flat" OnClick="btnDaily_Click" />

                                                    <asp:Button ID="btnMonthly" runat="server" Text="Monthly Break-Up" CssClass="btn btn-warning btn-sm  btn-flat" OnClick="btnMonthly_Click" />

                                                    <asp:Button ID="btnQuarterly" runat="server" Text="Quarterly Break-Up" CssClass="btn btn-warning btn-sm  btn-flat" OnClick="btnQuarterly_Click" />

                                                </p>

                                                <asp:Label ID="lblItemId" runat="server" Text="" ToolTip="" CssClass="hidden"></asp:Label>

                                                <asp:Label ID="lblheadingSecond" runat="server" Text=""></asp:Label>
                                                <asp:GridView ID="GridView3" runat="server" ShowFooter="True" AutoGenerateColumns="False" class="table table-hover table-bordered GridView3" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Particular">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnParticular" CausesValidation="false" CommandName="Select" Text='<%# Eval("Particular") %>' ToolTip='<%# Eval("MonthDates") %>' runat="server" CssClass="Aselect1">LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ControlStyle Font-Bold="True" />
                                                            <FooterStyle BackColor="#EAEAEA" Font-Bold="True" />
                                                            <HeaderStyle BackColor="#EAEAEA" />
                                                            <ItemStyle BackColor="#EAEAEA" Font-Bold="True" />
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="Item_Id" HeaderText="Item Id" />--%>
                                                        <asp:BoundField DataField="OpeningQuantity" HeaderText="Opening Quantity" HeaderStyle-BackColor="Red">
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OpeningRate" HeaderText="Opening Rate">
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OpeningValue" HeaderText="Opening Value">

                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="InwardQuantity" HeaderText="Inward Quantity">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InwardRate" HeaderText="Inward Rate">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InwardValue" HeaderText="Inward Value">

                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="OutwardQuantity" HeaderText="Outward Quantity">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OutwardRate" HeaderText="Outward Rate">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OutwardValue" HeaderText="Outward Value">

                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="ClosingQuantity" HeaderText="Closing Quantity">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ClosingRate" HeaderText="Closing Rate">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ClosingValue" HeaderText="Closing Value">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>

                                <div class="row" id="DivGrid4" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <fieldset class="box-body">
                                            <div class="table-responsive">
                                                <asp:Label ID="lblheadingThird" runat="server" Text=""></asp:Label>
                                                <asp:GridView ID="GridView4" runat="server" ShowFooter="True" AutoGenerateColumns="False" class="table table-hover table-bordered GridView4" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="TranDt2" HeaderText="Date" />
                                                        <asp:TemplateField HeaderText="Particular">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtnParticular" CausesValidation="false" CommandName="Select" Text='<%# Eval("Particular") %>' ToolTip='<%# Eval("Particular") %>' runat="server" CssClass="Aselect1">LinkButton</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ControlStyle Font-Bold="True" />
                                                            <FooterStyle BackColor="#EAEAEA" Font-Bold="True" />
                                                            <HeaderStyle BackColor="#EAEAEA" />
                                                            <ItemStyle BackColor="#EAEAEA" Font-Bold="True" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="VoucherType" HeaderText="Voucher Type" />
                                                        <asp:BoundField DataField="VoucherNo" HeaderText="Voucher No" />
                                                        <asp:BoundField DataField="Office_Name" HeaderText="Office Name" />
                                                        <%--<asp:BoundField DataField="OpeningQuantity" HeaderText="Opening Quantity" HeaderStyle-BackColor="Red">
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OpeningRate" HeaderText="Opening Rate">
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OpeningValue" HeaderText="Opening Value">

                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <HeaderStyle BackColor="#FFFFCC" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                        </asp:BoundField>--%>

                                                        <asp:BoundField DataField="InwardQuantity" HeaderText="Inward Quantity">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InwardRate" HeaderText="Inward Rate">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="InwardValue" HeaderText="Inward Value">

                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <HeaderStyle BackColor="#cfdcc1" />
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                        </asp:BoundField>

                                                        <asp:BoundField DataField="OutwardQuantity" HeaderText="Outward Quantity">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OutwardRate" HeaderText="Outward Rate">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OutwardValue" HeaderText="Outward Value">

                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <HeaderStyle BackColor="#ffb88f" />
                                                            <ItemStyle BackColor="#ffb88f" />
                                                        </asp:BoundField>

                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%">
                                                            <ItemTemplate>
                                                                <asp:HyperLink ID="hpView" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("TransactionID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("1")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info" Visible='<%# Eval("PageURL").ToString()=="" ? false: true %>'>View</asp:HyperLink>

                                                                <asp:Label ID="lblOfficeID" CssClass="hidden" Text='<%# Eval("Office_ID").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--<asp:BoundField DataField="ClosingQuantity" HeaderText="Closing Quantity">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ClosingRate" HeaderText="Closing Rate">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ClosingValue" HeaderText="Closing Value">
                                                            <FooterStyle BackColor="#FFCC99" />
                                                            <HeaderStyle BackColor="#FFCC99" />
                                                            <ItemStyle BackColor="#FFCC99" />
                                                        </asp:BoundField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>


                                <%--                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table datatable table-hover table-bordered">
                                            <Columns>
                                                <asp:BoundField DataField="VoucherTx_Date" HeaderText="VchDate" />
                                                <asp:BoundField DataField="Particular" HeaderText="Particulars" />
                                                <asp:BoundField DataField="VoucherTx_Type" HeaderText="VchType" />
                                                <asp:BoundField DataField="VoucherTx_No" HeaderText="VchNo" />
                                                <asp:BoundField DataField="Office_Name" HeaderText="Office Name" />
                                                <asp:BoundField DataField="DebitAmount" HeaderText="Debit (Amount)" />
                                                <asp:BoundField DataField="CreditAmount" HeaderText="Credit (Amount)" />
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hpView" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("1") +"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info">View</asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>--%>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <%-- start data table--%>
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
                        columns: [0, 1, 2, 3, 4, 5, 6]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
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

        //end data table

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

        });
    </script>
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script>
        $('.GridView4, .GridView2, .GridView3').DataTable({
            paging: false,
            dom: 'Bfrtip',
            ordering: false,
            buttons: [
                {
                    extend: 'colvis',
                    collectionLayout: 'fixed two-column',
                    text: '<i class="fa fa-eye"></i> Columns'
                }, {
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h1').text(),
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    footer: true
                }
            ]
        });
    </script>
    <script>

        $(function () {
            $('[id*=ddlOffice]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
    </script>
    <script>
        $('table tr td').each(function () {
            if ($(this).text() == '0') {
                $(this).css('color', 'red');
            }
        });
    </script>
    <style>
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
            max-height: 200px;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }

        ul.multiselect-container.dropdown-menu {
            overflow-y: scroll;
            overflow-x: hidden;
        }
    </style>

    <script src="js/fromkeycode.js" type="text/javascript"></script>
    <script>
        function handleKeyDown(e) {
            var ctrlPressed = 0;
            var altPressed = 0;
            var shiftPressed = 0;
            var evt = (e == null ? event : e);

            shiftPressed = evt.shiftKey;
            altPressed = evt.altKey;
            ctrlPressed = evt.ctrlKey;
            self.status = ""
               + "shiftKey=" + shiftPressed
               + ", altKey=" + altPressed
               + ", ctrlKey=" + ctrlPressed

            if ((altPressed) && (evt.keyCode == 87)) {
                if ($('.GroupChildren').is(':visible')) {
                    $(".GroupChildren").css("display", "none");
                }
                else {
                    $(".GroupChildren").css("display", "table-row");
                }
            }
            //alert("You pressed the " + fromKeyCode(evt.keyCode)
            // + " key (keyCode " + evt.keyCode + ")\n"
            // + "together with the following keys:\n"
            // + (shiftPressed ? "Shift " : "")
            // + (altPressed ? "Alt " : "")
            // + (ctrlPressed ? "Ctrl " : "")
            //)            

            return true;
        }

        document.onkeydown = handleKeyDown;
    </script>
</asp:Content>
