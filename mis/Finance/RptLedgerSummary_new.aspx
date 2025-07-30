<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptLedgerSummary_new.aspx.cs"  Inherits="mis_Finance_RptLedgerSummary_new" EnableEventValidation="false"%>

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

        /*.pay-sheet table {
            border: 1px solid #ddd;
        }*/
        .Dtime {
            display: none;
        }
        ul.ui-autocomplete.ui-menu.ui-widget.ui-widget-content.ui-corner-all {
            height: 197px !important;
            overflow-y: scroll !important;
            width: 520px !important;
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

        .align-right {
            text-align: right !important;
            width: 10% !important;
        }

        span.Ledger_Amt {
            max-width: 30%;
            display: inline;
            float: right;
        }

        span.Ledger_Name {
            max-width: 70%;
            display: inline;
            float: left;
        }

        p.subledger {
            border-top: 1px solid #ccc;
            margin: 0px;
        }

        .report-title {
            font-weight: 600;
            font-size: 17px;
            color: #123456;
        }

        .Scut {
            color: tomato;
        }

        .Narration {
            word-break: break-all !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
       
        <section class="content">
            <asp:Label ID="lblTime" runat="server" CssClass="Dtime" Style="font-weight: 800;" Text="" ClientIDMode="Static"></asp:Label>
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Ledger</h3>
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-default pull-right" Text="Print" Style="margin-left: 10px;" OnClick="btnPrint_Click"></asp:Button>
                    <a class="btn btn-primary pull-right" style="margin-left: 10px;" href="RptLedgerSummaryCustom.aspx">Temporarily Remove Line</a>
                    <asp:Button ID="btngraphical" Visible="false" runat="server" CssClass="btn btn-primary pull-right" Text="Graphical Report" Style="margin-right: 10px;" OnClick="btngraphical_Click"></asp:Button>


                    <p>
                        <span>[<span class="Scut">Alt+w</span> - Detailed View],[<span class="Scut">Alt+b</span> - Condensed View] ,[<span class="Scut">Alt+Q</span> - Daily Breakup & Detailed View]
                        </span>
                    </p>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row Hiderow">

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
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row Hiderow">
                        <div class="col-md-5">
                            <div class="form-group">
                                <label>Filter Amount :</label><span style="color: red">*</span>
                                <br />
                                <asp:CheckBox ID="chkOpeningBal" runat="server" Text="Opening Bal.&nbsp;&nbsp;" />
                                <asp:CheckBox ID="chkDebitAmt" runat="server" Text="&nbsp;Debit Amt.&nbsp;&nbsp;" />
                                <asp:CheckBox ID="chkCreditAmt" runat="server" Text="Credit Amt.&nbsp;&nbsp;" />
                                <asp:CheckBox ID="chkClosingBal" runat="server" Text="Closing Bal.&nbsp;&nbsp;" />
                                <%--<input id="chkClosingBal" type="checkbox"  name="ClosingBal" /> <label>Closing Bal.</label>--%>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <label>Show Details :</label><span style="color: red">*</span>
                                <br />
                                <asp:CheckBox ID="chkNarration" runat="server" Text="Narration&nbsp;&nbsp;" />
                                <asp:CheckBox ID="chkOppositeLedger" runat="server" Text="&nbsp;Opposite Ledger" />
                            </div>
                        </div>
                    </div>
                    <div class="row Hiderow">
                          <div class="col-md-3" runat="server" id="divRegionalOffice">
                                <div class="form-group">
                                    <label>Circle Office</label><span style="color: red">*</span>
                                    <asp:DropDownList ID="ddlRegionalOffice" runat="server" ClientIDMode="Static" CssClass="form-control" OnSelectedIndexChanged="ddlRegionalOffice_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged"></asp:ListBox>
                            </div>
                        </div>
                        <%--<div class="col-md-5">
                            <div class="form-group">
                                <label>List Of Ledger</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlLedger" CssClass="form-control select2" AutoPostBack="false" OnSelectedIndexChanged="ddlLedger_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                        <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Ledger Name</label>
                                        <asp:TextBox runat="server" CssClass="form-control capitalize ui-autocomplete-12" placeholder="Enter Ledger Name" ID="txtLedgerName" MaxLength="255" ClientIDMode="Static"></asp:TextBox>
                                        <asp:HiddenField ID="hfLedgerName" runat="server" ClientIDMode="Static" />
                                        <asp:HiddenField ID="hfLedgerID" runat="server" ClientIDMode="Static" />
                                        <small><span id="valtxtLedgerName" style="color: red;"></span></small>
                                    </div>
                                </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success Aselect1" Style="margin-top: 24px;" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-2 Hiderow">

                            <asp:Button ID="btnBillByBill" runat="server" CssClass="btn-info Aselect1" Text="Bill Details" OnClick="btnBillByBill_Click" />

                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-block btn-success hidden Aselect1" Text="<< BACK " OnClick="btnBack_Click" AccessKey="W" />
                            <asp:Button ID="btnBackN" runat="server" CssClass="btn btn-block btn-success hidden Aselect1" Text="<< BACK " OnClick="btnBackN_Click" AccessKey="B" />
                            <asp:Button ID="btnShowDetailBook" runat="server" CssClass="hidden" Text="Show Bank Detail" OnClick="btnShowDetailBook_Click" AccessKey="Q" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 Hiderow">
                            <div runat="server" id="divExcel">
                                <div class="form-group">
                                    <asp:Button ID="btnExporttotexcel" runat="server" Text="Export to Excel" OnClick="btnExporttotexcel_Click"/>
                                   <%-- <input type="button" onclick="tableToExcel('tableData', 'W3C Example Table')" value="Export to Excel">--%>
                                </div>
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
                    <div id="tableData" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="5" style="text-align: center;">
                                            <p class="report-title">
                                                <asp:Label ID="lblReportName" runat="server" Text="" ClientIDMode="Static"></asp:Label>
                                                <asp:HiddenField ID="HF_ReportName" runat="server" ClientIDMode="Static" />
                                                <asp:Label ID="lblTab" runat="server" Style="font-weight: 700; font-size: 20px; color: red;" Text=""></asp:Label>
                                                <%--   <br />--%>
                                                <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime" Visible="false"></asp:Label>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                                <div id="DivTable" runat="server"></div>
                                <%--LEDGER DAILY BREAKUP DETAIL--%>
                                <span id="spnSearch" visible="false" runat="server"><b>Search: </b></span><asp:TextBox ID="txtSearch" CssClass="hide_print" Visible="false" runat="server" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                
                               
                                <asp:GridView ID="GvDetail" runat="server" ClientIDMode="Static" ShowFooter="true" CssClass="table table-bordered table-striped pagination-ys" AllowPaging="true"  AutoGenerateColumns="false"  OnRowCommand="GvDetail_RowCommand" PageSize="100" OnPageIndexChanging="GvDetail_PageIndexChanging">
                                   <HeaderStyle BackColor="#ff874c" Font-Bold="True" ForeColor="Black" HorizontalAlign="Left" />
                                    <Columns>
                                        
                                        <asp:TemplateField HeaderText="Voucher Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_Date" runat="server" Text='<%# Eval("VoucherTx_Date") %>'></asp:Label>
                                               <%-- <asp:Label ID="lblVoucherTx_ID" CssClass="hidden" runat="server" Text='<%# Eval("VoucherTx_ID") %>'></asp:Label>
                                                <asp:Label ID="lblOffice_ID" CssClass="hidden" runat="server" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                                <asp:Label ID="lblPageURL" CssClass="hidden" runat="server" Text='<%# Eval("PageURL") %>'></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedger_Name" runat="server" Text='<%# Eval("Ledger_Name") %>' Font-Bold='<%# Eval("Ledger_Name").ToString()=="Opening Balance"?true:false %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vch Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_Type" runat="server" Text='<%# Eval("VoucherTx_Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vch No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_No" runat="server" Text='<%# Eval("VoucherTx_No") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebitAmt"  runat="server" Text='<%# Eval("DebitAmt") %>' Visible='<%# Eval("DebitAmt").ToString()=="0.00"?false:true %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreditAmt"  runat="server" Text='<%# Eval("CreditAmt") %>' Visible='<%# Eval("CreditAmt").ToString()=="0.00"?false:true %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Closing Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCB" DataFormatString="{0:C}" runat="server" Font-Bold='<%# Eval("Ledger_Name").ToString()=="Opening Balance"?true:false %>' Text='<%# string.Concat(Eval("CBType")," ", (Convert.ToDecimal(Eval("CB")) >= 0?"Cr":"Dr")) %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="hide_print" ItemStyle-CssClass="hide_print" >
                                            <ItemTemplate>
                                               <%-- <asp:HyperLink ID="lnkView"  CssClass="label label-primary" runat="server" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View"  Visible='<%# Eval("PageURL").ToString() !=""?true:false%>'></asp:HyperLink>--%>
                                               <asp:LinkButton  ID="lnkView" CssClass="label label-primary hide_print" runat="server" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View"  Visible='<%# Eval("PageURL").ToString() !=""?true:false%>'></asp:LinkButton>
                                                  <asp:LinkButton ID="lnkEdit"  CssClass="label label-primary hide_print" runat="server" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="EditRecord" Text="Edit"  Visible='<%# Eval("V_Editright").ToString() =="Yes"?true:false%>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                
                                                <asp:Label ID="lblVoucherTx_ID"  runat="server" Text='<%# Eval("VoucherTx_ID") %>'></asp:Label>
                                                <asp:Label ID="lblOffice_ID" runat="server" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                                <asp:Label ID="lblPageURL"  runat="server" Text='<%# Eval("PageURL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                
				<br />
            			<asp:Label ID="lblTotal" CssClass="hide_print" runat="server" />
                                <asp:GridView ID="gvDetail1" runat="server" ShowFooter="true" CssClass="table table-bordered table-striped pagination-ys" AllowPaging="true" AutoGenerateColumns="false"  OnRowCommand="gvDetail1_RowCommand" OnPageIndexChanging="gvDetail1_PageIndexChanging" PageSize="100">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Voucher Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_Date" runat="server" Text='<%# Eval("VoucherTx_Date") %>'></asp:Label>
                                                <%--<asp:Label ID="lblVoucherTx_ID" CssClass="hidden" runat="server" Text='<%# Eval("VoucherTx_ID") %>'></asp:Label>
                                                <asp:Label ID="lblOffice_ID" CssClass="hidden" runat="server" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                                <asp:Label ID="lblPageURL" CssClass="hidden" runat="server" Text='<%# Eval("PageURL") %>'></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particulars" HeaderStyle-Width="40%" ItemStyle-Width="40%">
                                            <ItemTemplate>
                                                <div><span><%# Eval ("Variable") %></div></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vch Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_Type" runat="server" Text='<%# Eval("VoucherTx_Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vch No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_No" runat="server" Text='<%# Eval("VoucherTx_No") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebitAmt" runat="server" Visible='<%# Eval("DebitAmt").ToString()=="0.00"?false:true %>' Text='<%# Eval("DebitAmt") %>' ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amt.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreditAmt" runat="server" Visible='<%# Eval("CreditAmt").ToString()=="0.00"?false:true %>' Text='<%# Eval("CreditAmt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Closing Bal.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCB" runat="server" Font-Bold='<%# Eval("Ledger_Name").ToString()=="Opening Balance"?true:false %>' Text='<%# string.Concat(Eval("CBType")," ", (Convert.ToDecimal(Eval("CB")) >= 0?"Cr":"Dr")) %>'></asp:Label>
                                                
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="hide_print" ItemStyle-CssClass="hide_print">
                                            <ItemTemplate>
                                               <asp:LinkButton ID="lnkView"  CssClass="label label-primary" runat="server" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View"  Visible='<%# Eval("PageURL").ToString() !=""?true:false%>'></asp:LinkButton>
                                                  <asp:LinkButton ID="lnkEdit"  CssClass="label label-primary" runat="server" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="EditRecord" Text="Edit"  Visible='<%# Eval("V_Editright").ToString() =="Yes"?true:false%>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                
                                                <asp:Label ID="lblVoucherTx_ID"  runat="server" Text='<%# Eval("VoucherTx_ID") %>'></asp:Label>
                                                <asp:Label ID="lblOffice_ID" runat="server" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                                <asp:Label ID="lblPageURL"  runat="server" Text='<%# Eval("PageURL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
				<br />
                                <asp:Label ID="lblDetailTotal"  runat="server" />
                                <asp:GridView ID="GridView4" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="datatable table table-hover table-bordered">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" Text='<%# Eval("Date").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit Amt." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amt." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing Bal." ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            <!--Bill By Bill Modal -->
            <div class="modal fade" id="AgstRefModal" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Pending Ref Details </h4>
                            <span style="font-weight: 700; font-size: 12px;">Ledger Name :
                                <asp:Label ID="lblLedgerName" runat="server"></asp:Label></span>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView runat="server" EmptyDataText="No Record Found" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GridViewRefDetail" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="VoucherTx_Date" HeaderText="Date" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="BillByBillTx_Ref" HeaderText="Name" ItemStyle-Width="40%" HeaderStyle-Width="30%" />
                                            <%--   <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="20%" />--%>
                                            <asp:BoundField DataField="OpeningAmt" HeaderText="Opening" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="DrTxnAmt" HeaderText="Txn. [Debit Amt.]" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="CrTxnAmt" HeaderText="Txn. [Credit Amt.]" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="ClosingAmt" HeaderText="Closing" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" />

                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblBillTotal" runat="server" Style="float: right; font-weight: 700;"></asp:Label>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <%--<asp:Button runat="server" Text="Add" ID="btnBillByBillSave" OnClick="btnBillByBillSave_Click" ClientIDMode="Static" CssClass="btn btn-success"></asp:Button>--%>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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
    <script src="js/fromkeycode.js"></script>

    <script>
        $('.datatable').DataTable({
            paging: false,
            dom: 'Bfrtip',
            ordering: false,
            buttons: [
                {
                    extend: 'colvis',
                    collectionLayout: 'fixed two-column',
                    text: '<i class="fa fa-eye"></i> Columns'
                },
                //{
                //    extend: 'print',
                //    text: '<i class="fa fa-print"></i> Print',
                //    title: $('h1').text(),
                //    footer: true,
                //    autoPrint: true
                //},
                //{
                //    extend: 'excel',
                //    text: '<i class="fa fa-file-excel-o"></i> Excel',
                //    title: $('h1').text(),
                //    exportOptions: {
                //        columns: [0, 1, 2, 3, 4, 5]
                //    },
                //    footer: true
                //}

            ]
        });
    </script>
    <script>
        $('.datatable1').DataTable({
            paging: true,
            dom: 'Bfrtip',
            ordering: false,
            buttons: [
                //{
                //    extend: 'colvis',
                //    collectionLayout: 'fixed two-column',
                //    text: '<i class="fa fa-eye"></i> Columns'
                //},
                {
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h1').text(),
                    footer: true,
                    autoPrint: true
                },
                {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true
                }

            ]
        });
    </script>

    <script>
        function ShowBillDetailModal() {
            $('#AgstRefModal').modal('show');

        }

        function validateform() {
            var msg = "";
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select From Date. \n";
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select To Date. \n";
            }
            var Fromday = 0;
            var FromMonth = 0;
            var FromYear = 0;
            var Today = 0;
            var ToMonth = 0;
            var ToYear = 0;
            var y = document.getElementById("txtFromDate").value; //This is a STRING, not a Date
            if (y != "") {
                var dateParts = y.split("/");   //Will split in 3 parts: day, month and year
                var yday = dateParts[0];
                var ymonth = dateParts[1];
                var yyear = dateParts[2];

                Fromday = dateParts[0];
                FromMonth = dateParts[1];
                FromYear = dateParts[2];

                var yd = new Date(yyear, parseInt(ymonth, 10) - 1, yday);
            }
            else {
                var yd = "";
            }

            var z = document.getElementById("txtToDate").value; //This is a STRING, not a Date
            if (z != "") {
                var dateParts = z.split("/");   //Will split in 3 parts: day, month and year
                var zday = dateParts[0];
                var zmonth = dateParts[1];
                var zyear = dateParts[2];

                Today = dateParts[0];
                ToMonth = dateParts[1];
                ToYear = dateParts[2];

                var zd = new Date(zyear, parseInt(zmonth, 10) - 1, zday);
            }
            else {
                var zd = "";
            }
            if (yd != "" && zd != "") {
                if (yd > zd) {
                    msg += "To Date should be greater than From Date ";
                }
                else {

                    if ((FromYear == ToYear - 1) || (FromYear == ToYear)) {
                        if (FromYear == ToYear && FromMonth <= 3 && ToMonth <= 3) {
                        }
                        else if (FromYear == ToYear && FromMonth >= 4 && ToMonth <= 12) {
                        }
                        else if (FromYear == (ToYear - 1) && FromMonth > 3 && ToMonth <= 3) {
                        }
                        else {
                            msg += "select Valid Date";
                        }
                    }
                    else {
                        msg += "select Valid Date";
                    }

                }
            }
            if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }

        }
        function PrintPage() {
            window.print();
        }



        $('#txtFromDate').change(function () {
            debugger;
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
            debugger;
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


        $(document).ready(function () {

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
    <script src="js/jquery-ui.min.js"></script>
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <%--<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />--%>
   
    <script>
        $(document).ready(function () {

            debugger;
            $("#<%=txtLedgerName.ClientID %>").autocomplete({

                source: function (request, response) {
                    $.ajax({

                        url: '<%=ResolveUrl("RptLedgerSummary_new.aspx/SearchLedger") %>',
                        data: "{ 'Ledger_Name': '" + $('#txtLedgerName').val() + "'}",
                        //  var param = { ItemName: $('#txtItem').val() };
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {

                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-Ledger_Name-')[0],
                                    val: item.split('-Ledger_Name-')[1]
                                    //val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            //alert(response.responseText);
                        },
                        failure: function (response) {
                           // alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("#<%=hfLedgerName.ClientID %>").val(i.item.label);
                    $("#<%=hfLedgerID.ClientID %>").val(i.item.val);
                },
                minLength: 1

            });

        });
    </script>
</asp:Content>
