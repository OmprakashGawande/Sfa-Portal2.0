<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinCostCentreReport.aspx.cs" Inherits="mis_Finance_FinCostCentreReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .pay-sheet table tr th, .pay-sheet table tr td {
            font-size: 12px;
            width: 10%;
            border: 1px dashed #ddd;
            padding-left: 1px;
            padding-top: 1px;
            line-height: 14px;
            font-family: monospace;
            overflow: hidden;
        }

        .pay-sheet table {
            width: 100%;
        }

            .pay-sheet table thead {
                background: #eee;
            }


        span.Ledger_Amt {
            max-width: 30%;
            display: inline;
            float: right;
        }

        span.Ledger_Name {
            max-width: 70%;
            display: inline;
            /*float: left;*/
        }

        p.subledger {
            border-top: 1px solid #ccc;
            margin: 0px;
        }

        .report-title {
            font-weight: 600;
            font-size: 15px;
            color: #123456;
        }

        .align-right {
            text-align: right !important;
        }

        .Scut {
            color: tomato;
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

        .voucherColumn {
            width: 150px !important;
        }

        .cssclass {
            color: black;
        }

            .cssclass:hover {
                color: blue;
            }

        .datepicker-dropdown:after {
            position: initial !important;
        }

        .datepicker.datepicker-dropdown.dropdown-menu.datepicker-orient-left.datepicker-orient-top {
            z-index: 9999 !important;
        }
        p{
            margin-bottom:0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:Label ID="lblTime" runat="server" CssClass="Dtime" Style="font-weight: 800;" Text="" ClientIDMode="Static"></asp:Label>
            <div class="box box-success">
                <div class="box-header Hiderow hide_print">
                    <h3 class="box-title">Cost Centre Report</h3>
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-default pull-right" Text="Print" Style="margin-left: 10px;" OnClientClick="window.print();"></asp:Button>
                    <p class="hide_print">
                        <span runat="server" id="spnAltB">[<span class="Scut">Alt+B</span> - Back View] </span>
                    </p>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="hide_print">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group" style="margin-top: 28px;">
                                    <label>Filter Amount</label>
                                    : &nbsp;&nbsp;                               
                                <asp:CheckBox ID="chkOpeningBal" runat="server" Checked="false" Text="Opening Bal.&nbsp;&nbsp;" />
                                    <asp:CheckBox ID="chkTransactionAmt" runat="server" Checked="false" Text="Transaction&nbsp;&nbsp;" />
                                    <asp:CheckBox ID="chkClosingBal" runat="server" Checked="true" Text="Closing Bal.&nbsp;&nbsp;" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>From Date<span style="color: red;">*</span></label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
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
                                        <asp:TextBox ID="txtToDate" runat="server" placeholder="Select To Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-3" runat="server" id="divRegionalOffice">
                                <div class="form-group">
                                    <label>Circle Office</label><span style="color: red">*</span>
                                    <asp:DropDownList ID="ddlRegionalOffice" runat="server" ClientIDMode="Static" CssClass="form-control" OnSelectedIndexChanged="ddlRegionalOffice_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Office Name</label><span style="color: red">*</span>
                                    <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success" Style="margin-top: 22px;" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>

                    <div runat="server" class="no-print" id="divExcel">
                        <asp:Label ID="lblExecTime" CssClass="ExecTime no-print" runat="server" Text=""></asp:Label><br />
                        <input type="button" onclick="tableToExcel('tableData', 'W3C Example Table')" value="Export to Excel" />
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
                    <div id="tableData">
                        <div class="row">
                            <div class="col-md-2">
                                <asp:Button ID="btnBack" runat="server" CssClass="btn btn-block btn-success hidden Aselect1" Text="<< BACK " OnClick="btnBack_Click" AccessKey="B" />
                            </div>
                            <div class="col-md-12">
                                <asp:Label ID="lblheadingFirst" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblHeadingSecond" CssClass="center-block text-center"  style="font-weight:600" runat="server" Text=""></asp:Label>
                                <asp:Label ID="lblHeadName"  CssClass="center-block"  style="font-weight:600" runat="server"  Text=""></asp:Label>
                                 <asp:Label ID="lblOpening" CssClass="center-block"   style="font-weight:600" runat="server"  Text=""></asp:Label>
                                 <asp:Label ID="lblMonth"  CssClass="center-block"  style="font-weight:600" runat="server"  Text=""></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <%--Category Wise Detail--%>
                                <asp:GridView ID="GridView1" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="datatable table table-hover table-bordered" Style="margin-bottom: 0px;" OnRowCommand="GridView1_RowCommand" ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                <asp:Label ID="lblCategoryId" CssClass="hidden" Visible="false" Text='<%# Eval("CategoryId").ToString() %>' runat="server" />
                                                <asp:Label ID="lblCategoryName" CssClass="hidden" Visible="false" Text='<%# Eval("CategoryName").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="Aselect1" CommandName="View" HeaderText="Category Name" DataTextField="CategoryName" />
                                        <asp:TemplateField HeaderText="Opening Bal. <br> [Debit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Bal. <br> [Credit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Txn. <br> [Debit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebitAmt" Text='<%# Eval("TxnDebitAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Txn. <br> [Credit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreditAmt" Text='<%# Eval("TxnCreditAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Bal. <br> [Debit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Bal. <br> [Credit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <%--Category Wise Detail--%>
                                <asp:GridView ID="GridView2" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="datatable table table-hover table-bordered" Style="margin-bottom: 0px;" OnRowCommand="GridView2_RowCommand" ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                <asp:Label ID="lblCategory_ID" CssClass="hidden" Visible="false" Text='<%# Eval("Category_ID").ToString() %>' runat="server" />
                                                <asp:Label ID="lblSubCategoryId" CssClass="hidden" Visible="false" Text='<%# Eval("SubCategoryId").ToString() %>' runat="server" />
                                                <asp:Label ID="lblSubCategoryName" CssClass="hidden" Visible="false" Text='<%# Eval("SubCategoryName").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="Aselect1" CommandName="View" HeaderText="Sub Category Name" DataTextField="SubCategoryName" />
                                        <asp:TemplateField HeaderText="Opening Bal. <br> [Debit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening Bal. <br> [Credit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Txn. <br> [Debit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebitAmt" Text='<%# Eval("TxnDebitAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Txn. <br> [Credit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreditAmt" Text='<%# Eval("TxnCreditAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Bal. <br> [Debit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Bal. <br> [Credit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <%-- Month Wise Detail--%>
                                <asp:GridView ID="GridView3" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="datatable table table-hover table-bordered" Style="margin-bottom: 0px;" OnRowCommand="GridView3_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                <asp:Label ID="lblMonthID" CssClass="hidden" Visible="false" Text='<%# Eval("MonthID").ToString() %>' runat="server" />
                                                 <asp:Label ID="lblMonthName" CssClass="hidden" Visible="false" Text='<%# Eval("MonthName").ToString() %>' runat="server" />
                                                <asp:Label ID="lblMonthOpening" CssClass="hidden" Visible="false" Text='' runat="server" />

                                                <asp:Label ID="lblMCategory_ID" CssClass="hidden" Visible="false" Text='<%# Eval("Category_ID").ToString() %>' runat="server" />
                                                <asp:Label ID="lblMSubCategoryId" CssClass="hidden" Visible="false" Text='<%# Eval("SubCategoryId").ToString() %>' runat="server" />

                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="Aselect1" CommandName="View" HeaderText="Month Name" DataTextField="MonthName" />
                                        <asp:TemplateField HeaderText="Txn. <br> [Debit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebitAmt" Text='<%# Eval("TxnDrAmount").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Txn. <br> [Credit Amt.]" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreditAmt" Text='<%# Eval("TxnCrAmount").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Bal." ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <%--Month wise Record--%>
                                 <div id="DivTable" runat="server"></div>
                                <asp:GridView ID="GridView4" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-hover table-bordered pagination-ys hidden" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" ClientIDMode="Static" OnRowCommand="GridView4_RowCommand" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Voucher Date" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Eval("VoucherTx_Date").ToString() %>' runat="server" />
                                                <asp:HiddenField ID="HF_VoucherTx_ID" runat="server" Value='<%# Eval("VoucherTx_ID").ToString() %>' />
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
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%" ItemStyle-CssClass="hidden-print" HeaderStyle-CssClass="hidden-print">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="hpView" runat="server" CssClass="label label-info" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                                <asp:LinkButton ID="hpEdit" runat="server" CssClass="label label-primary" CommandName="Editing" CommandArgument='<%# Eval("VoucherTx_ID") %>' Text="Edit" OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                                <asp:LinkButton ID="hpprint" CssClass="label label-primary" runat="server" Text="Print" CommandName="Print" CommandArgument='<%# Eval("VoucherTx_ID") %>' OnClientClick="window.document.forms[0].target = '_blank'; setTimeout(function () { window.document.forms[0].target = '' }, 0);"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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

