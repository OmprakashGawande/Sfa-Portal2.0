<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="RptMis.aspx.cs" Inherits="mis_Finance_RptMis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <%--<style>
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
    </style>--%>
    <style>
        .Dtime {
            display: none;
        }

        @media print {
            .hide_print, .Hiderow, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
            }

            /*.box {
                border: none;
            }*/

            th {
                background-color: #ddd;
                text-decoration: solid;
            }

            .tblheadingslip {
                font-size: 8px !important;
                background: black;
                color: red;
            }

            footer {
                position: relative;
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
        <section class="content-header">
            <h1>Sales&Purchase of Inventory(MIS Report)
            </h1>
        </section>
        <section class="content">
            <asp:Label ID="lblTime" runat="server" CssClass="Dtime" Style="font-weight: 800;" Text="" ClientIDMode="Static"></asp:Label>
            <div class="box box-pramod" style="background-color: #FFFFFF;">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <%--  <fieldset class="box-body">
                                <legend>MIS Report</legend>--%>
                            <div class="row no-print">
                                <div class="col-md-2">
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
                                <div class="col-md-2">
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
                                <div class="col-md-2">
                                <div class="form-group">
                                    <label>Regional Office</label><span style="color: red">*</span>
                                   <%-- <asp:DropDownList ID="ddlRegionalOffice" runat="server" ClientIDMode="Static" CssClass="form-control" OnSelectedIndexChanged="ddlRegionalOffice_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>--%>
                                     <asp:ListBox ID="ddlRegionalOffice" runat="server" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple" OnSelectedIndexChanged="ddlRegionalOffice_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                </div>
                            </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Office Name</label><span style="color: red">*</span>
                                        <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Group Name</label><span style="color: red">*</span>
                                        <asp:ListBox runat="server" ID="ddlGroup" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Report Type</label><span style="color: red">*</span>
                                        <asp:DropDownList ID="ddlReportType" runat="server" class="form-control select2">
                                            <asp:ListItem Value="Group Wise">Group Wise</asp:ListItem>
                                            <asp:ListItem Value="Item Wise">Item Wise</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btn" CssClass="btn btn-md btn-primary show_detail Aselect1" runat="server" Style="margin-top: 25px;" Text="Show Mis Report" OnClick="btn_Click" />
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblprinttext" CssClass="printtext" ToolTip="" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblexceltext" CssClass="exceltext" ToolTip="" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row no-print">
                                <div class="col-md-12">
                                    <div runat="server" id="divExcel">
                                        <input type="button" onclick="tableToExcel('tableData', 'W3C Example Table')" value="Export to Excel">
                                        <input type="button" value="Print" onclick="window.print();" />
                                    </div>
                                    <script type="text/javascript">
                                        var tableToExcel = (function () {
                                            var uri = 'data:application/vnd.ms-excel;base64,'
                                              , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>'
                                              , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
                                              , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
                                            return function (table, name) {
                                                if (!table.nodeType) table = document.getElementById(table)
                                                var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                                                window.location.href = uri + base64(format(template, ctx))
                                            }
                                        })()
                                    </script>
                                </div>
                            </div>
                            <div id="tableData">
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset class="box-body">
                                            <div class="table-responsive">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td colspan="5" style="text-align: center;">
                                                            <asp:Label ID="lblheadingFirst" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" EmptyDataText="No Record Found" ShowFooter="True" OnRowCommand="GridView1_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Font-Bold="true" ItemStyle-BackColor="#EAEAEA">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <ItemStyle BackColor="#FFFFCC" Font-Bold="True"></ItemStyle>
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <%--<HeaderStyle BackColor="#666" />--%>
                                                            <%-- <HeaderStyle BackColor="#FFFFCC" />--%>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Group">

                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkgroup" runat="server" Text='<%# Eval("ItemTypeName") %>' CommandArgument='<%# Eval("ItemType_id") %>' CommandName="View"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <%-- <HeaderStyle BackColor="#000666" />--%>
                                                            <%-- <HeaderStyle BackColor="#FFFFCC" />--%>
                                                            <ItemStyle BackColor="#FFFFCC" Font-Bold="True" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Purchase Quantity">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("PurchaseQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="#CFDCC1" />
                                                            <%--  <HeaderStyle BackColor="#CFDCC1" />--%>
                                                            <ItemStyle BackColor="#CFDCC1" />
                                                            <%--   <HeaderStyle BackColor="#666" />--%>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Purchase Value Without Tax" DataField="PurchaseValueWithoutTax">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <%--  <HeaderStyle BackColor="#cfdcc1" />--%>
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                            <%--  <HeaderStyle BackColor="#666" />--%>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="GST" DataField="PurchaseTaxAmount">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <%--  <HeaderStyle BackColor="#cfdcc1" />--%>
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                            <%-- <HeaderStyle BackColor="#666" />--%>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Gross Purchase" DataField="PurchaseValuewithTax">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <%-- <HeaderStyle BackColor="#cfdcc1" />--%>
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                            <%--<HeaderStyle BackColor="#666" />--%>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Sale Quantity">

                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("SaleQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="#FFB88F" />
                                                            <%--<HeaderStyle BackColor="#FFB88F" />--%>
                                                            <ItemStyle BackColor="#FFB88F" />
                                                            <%--   <HeaderStyle BackColor="#666" />--%>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Sale Value Without Tax" DataField="SaleValueWithoutTax">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <%--   <HeaderStyle BackColor="#ffb88f" />--%>
                                                            <ItemStyle BackColor="#ffb88f" />
                                                            <%-- <HeaderStyle BackColor="#666" />--%>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="GST" DataField="SaleTaxAmount">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <%--<HeaderStyle BackColor="#ffb88f" />--%>
                                                            <ItemStyle BackColor="#ffb88f" />
                                                            <%--  <HeaderStyle BackColor="#666" />--%>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Gross Sale" DataField="SaleValuewithTax">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <%--  <HeaderStyle BackColor="#ffb88f" />--%>
                                                            <ItemStyle BackColor="#ffb88f" />
                                                            <%-- <HeaderStyle BackColor="#666" />--%>
                                                        </asp:BoundField>

                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" EmptyDataText="No Record Found" ShowFooter="True" OnRowCommand="GridView2_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Font-Bold="true" ItemStyle-BackColor="#EAEAEA">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>

                                                            <ItemStyle BackColor="#FFFFCC" Font-Bold="True"></ItemStyle>
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <%-- <HeaderStyle BackColor="#666" />--%>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Group" DataField="ItemTypeName">
                                                            <ItemStyle Font-Bold="true" />
                                                            <ItemStyle BackColor="#FFFFCC" />
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <%-- <HeaderStyle BackColor="#666" />--%>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Item">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkItem" runat="server" Text='<%# Eval("ItemName") %>' CommandArgument='<%# Eval("Item_id") %>' CommandName="View"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="#FFFFCC" />
                                                            <%--  <HeaderStyle BackColor="#000666" />--%>
                                                            <ItemStyle BackColor="#FFFFCC" Font-Bold="True" Wrap="False" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Purchase Quantity">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("PurchaseQuantity") + " " + Eval("UQCCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="#CFDCC1" />
                                                            <%-- <HeaderStyle BackColor="#CFDCC1" />--%>
                                                            <ItemStyle BackColor="#CFDCC1" />
                                                            <%--  <HeaderStyle BackColor="#666" />--%>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Purchase Value Without Tax" DataField="PurchaseValueWithoutTax">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <%--  <HeaderStyle BackColor="#cfdcc1" />--%>
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                            <%--  <HeaderStyle BackColor="#666" />--%>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="GST" DataField="PurchaseTaxAmount">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <%-- <HeaderStyle BackColor="#cfdcc1" />--%>
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                            <%-- <HeaderStyle BackColor="#666" />--%>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Gross Purchase" DataField="PurchaseValuewithTax">
                                                            <FooterStyle BackColor="#cfdcc1" />
                                                            <%-- <HeaderStyle BackColor="#cfdcc1" />--%>
                                                            <ItemStyle BackColor="#cfdcc1" />
                                                            <%--<HeaderStyle BackColor="#666" />--%>
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Sale Quantity">

                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("SaleQuantity")+ " " + Eval("UQCCode") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle BackColor="#FFB88F" />
                                                            <%-- <HeaderStyle BackColor="#FFB88F" />--%>
                                                            <ItemStyle BackColor="#FFB88F" />
                                                            <%-- <HeaderStyle BackColor="#666" />--%>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Sale Value Without Tax" DataField="SaleValueWithoutTax">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <%-- <HeaderStyle BackColor="#ffb88f" />--%>
                                                            <ItemStyle BackColor="#ffb88f" />
                                                            <%-- <HeaderStyle BackColor="#666" />--%>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="GST" DataField="SaleTaxAmount">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <%-- <HeaderStyle BackColor="#ffb88f" />--%>
                                                            <ItemStyle BackColor="#ffb88f" />
                                                            <%--  <HeaderStyle BackColor="#666" />--%>
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Gross Sale" DataField="SaleValuewithTax">
                                                            <FooterStyle BackColor="#ffb88f" />
                                                            <%--<HeaderStyle BackColor="#ffb88f" />--%>
                                                            <ItemStyle BackColor="#ffb88f" />
                                                            <%-- <HeaderStyle BackColor="#666" />--%>
                                                        </asp:BoundField>

                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="GridView3" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowCommand="GridView3_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Voucher Date." ItemStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVoucherTx_Date" Text='<%# Eval("VoucherTx_Date").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Particulars">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLedger_Name" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Vch Type" ItemStyle-Width="13%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVoucherTx_Type" Text='<%# Eval("VoucherTx_Type").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Vch No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVoucherTx_No" Text='<%# Eval("VoucherTx_No").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Office Name.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOffice_Name" Text='<%# Eval("Office_Name").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Debit Amt." ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Credit Amt." ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="hpView" runat="server" CssClass="label label-info" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                                                <asp:Label ID="lblOfficeID" CssClass="hidden" Text='<%# Eval("Office_ID").ToString() %>' runat="server" />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                            <%--  </fieldset>--%>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <%-- start data table--%>
    <script>

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
        $(function () {
            $('[id*=ddlOffice]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
        $(function () {
            $('[id*=ddlGroup]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
        $(function () {
            $('[id*=ddlRegionalOffice]').multiselect({
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
</asp:Content>
