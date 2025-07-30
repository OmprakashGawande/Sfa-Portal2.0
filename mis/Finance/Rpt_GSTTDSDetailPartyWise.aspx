<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_GSTTDSDetailPartyWise.aspx.cs" Inherits="mis_Finance_Rpt_GSTTDSDetailPartyWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        @media print {

            .hide_print, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
            }

            tfoot, thead {
                display: table-row-group;
                bottom: 0;
            }
        }

        .right-align {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title" id="vname" runat="server">GST TDS Party Wise Detail</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3 no-print" runat="server" id="divRegionalOffice">
                                    <div class="form-group">
                                        <label>Regional Office</label><span style="color: red">*</span>
                                        <asp:DropDownList ID="ddlRegionalOffice" runat="server" ClientIDMode="Static" CssClass="form-control" OnSelectedIndexChanged="ddlRegionalOffice_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 no-print">
                                    <div class="form-group">
                                        <label>Office<span style="color: red;">*</span></label>
                                        <asp:ListBox ID="ddlOffice" runat="server" SelectionMode="Multiple" class="form-control"></asp:ListBox>
                                    </div>
                                </div>

                                <div class="col-md-3 no-print">
                                    <div class="form-group">
                                        <label>Voucher From Date<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtFromDate" data-date-end-date="0d" placeholder="DD/MM/YYYY" MaxLength="50" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3 no-print">
                                    <div class="form-group">
                                        <label>Voucher To Date<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtTodate" data-date-end-date="0d" placeholder="DD/MM/YYYY" MaxLength="50" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2 no-print">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" ClientIDMode="Static" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div runat="server" id="divExcel">
                                    <div class="col-md-2 no-print">
                                        <input type="button" onclick="tableToExcel('tableData', 'W3C Example Table')" value="Export to Excel">

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
                                    <div class="col-md-1 no-print">
                                        <div class="form-group">
                                            <input type="button" class="btn-block" value="Print" onclick="window.print();">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="tableData">
                                <div class="row">
                                    <div class="col-md-12">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td colspan="5" style="text-align: center;">
                                                    <b>
                                                        <asp:Label ID="lblGrid" runat="server"></asp:Label></b>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="table-responsive">
                                                <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="gvTDSDetail" AutoGenerateColumns="false" ShowFooter="true">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.no">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Office Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOffice_Name" runat="server" Text='<%# Bind("Office_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Party Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLedger_Name" runat="server" Text='<%# Bind("Ledger_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GST No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGSTNo" runat="server" Text='<%# Bind("GSTNo") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Bill Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalBillAmount" runat="server" Text='<%# Bind("TotalBillAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPaymentAmount" runat="server" Text='<%# Bind("PaymentAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Basic Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBasicAmount" runat="server" Text='<%# Bind("BasicAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GST TDS Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGSTTDSAmount" runat="server" Text='<%# Bind("GSTTDSAmount") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CGST" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCGST" runat="server" Text='<%# Bind("CGST") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SGST" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSGST" runat="server" Text='<%# Bind("SGST") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="IGST" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIGST" runat="server" Text='<%# Bind("IGST") %>'></asp:Label>
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
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }
    </style>
</asp:Content>

