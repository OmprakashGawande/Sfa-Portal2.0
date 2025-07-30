<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptMonthlyCustCashBookHeadWise_PCS.aspx.cs" Inherits="mis_Finance_RptMonthlyCustCashBookHeadWise_PCS" EnableEventValidation="false" %>

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

        @media print {

            .hide_print, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
            }
            .subcategory
                    {
                        font-size:12px;
                    }
            @page 
            {
                size:landscape;
                
            }
            tfoot, thead {
                display: table-row-group;
                bottom: 0;
            }
        }
         .Scut {
            color: tomato;
        }

        .voucherColumn {
            width: 150px !important;
        }
        .p
        {
            margin :2px;
        }
    </style>
    <style>
        .inline-rb label {
            margin-left: 5px;
        }

        .pagination-ys {
            /*display: inline-block;*/
            padding-left: 0;
            margin: 20px 0;
            border-radius: 4px;
        }

            .pagination-ys table > tbody > tr > td {
                display: inline;
            }

                .pagination-ys table > tbody > tr > td > a,
                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    color: #dd4814;
                    background-color: #ffffff;
                    border: 1px solid #dddddd;
                    margin-left: -1px;
                }

                .pagination-ys table > tbody > tr > td > span {
                    position: relative;
                    float: left;
                    padding: 8px 12px;
                    line-height: 1.42857143;
                    text-decoration: none;
                    margin-left: -1px;
                    z-index: 2;
                    color: #aea79f;
                    background-color: #f5f5f5;
                    border-color: #dddddd;
                    cursor: default;
                }

                .pagination-ys table > tbody > tr > td:first-child > a,
                .pagination-ys table > tbody > tr > td:first-child > span {
                    margin-left: 0;
                    border-bottom-left-radius: 4px;
                    border-top-left-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td:last-child > a,
                .pagination-ys table > tbody > tr > td:last-child > span {
                    border-bottom-right-radius: 4px;
                    border-top-right-radius: 4px;
                }

                .pagination-ys table > tbody > tr > td > a:hover,
                .pagination-ys table > tbody > tr > td > span:hover,
                .pagination-ys table > tbody > tr > td > a:focus,
                .pagination-ys table > tbody > tr > td > span:focus {
                    color: #97310e;
                    background-color: #eeeeee;
                    border-color: #dddddd;
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
            padding-left:2px;
        }
        p.subledger {
            border-top: 1px solid #ccc;
            margin: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Monthly Head Wise Cash Book</h3>
                    <p class="hide_print">
                        <span runat="server" id="spnAltW">[<span class="Scut">Alt+w</span> - Condensed & Detailed View]</span></br>
                        <span runat="server" style="color:red;">Note - तिथियों का चयन एक ही माह के बीच होना चाहिए।</span>
                    </p>
                    <%--<asp:HyperLink ID="btnDtlCustDayBook" NavigateUrl="RptDetailedCust_Daybook.aspx" Text="Detailed DayBook" Target="_blank" runat="server" CssClass="btn btn-primary pull-right"></asp:HyperLink><asp:Button ID="btngraphical" runat="server" Text="Graphical Report" Style="margin-right: 10px;" CssClass="btn btn-primary pull-right hidden" OnClick="btngraphical_Click"></asp:Button>--%>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row hidden-print">
                        <div class="col-md-2">
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
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>To Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" placeholder="Select To Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3" id="divdfo" runat="server">
                            <div class="form-group">
                                <label>DFO</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlDFO" CssClass="form-control select2" OnSelectedIndexChanged="ddlDFO_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2">
                                </asp:DropDownList>
                              <%--   <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>--%>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success btn-block" Style="margin-top: 25px;" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    
                    <%--   <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <div class="hide_print">
                                </div>

                            </div>
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime no-print"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div runat="server" id="divExcel">
                                <input type="button" onclick="tableToExcel('tableData', 'W3C Example Table')" class="no-print" value="Export to Excel">
                                <asp:LinkButton ID="btnPrint" runat="server" CssClass="btn btn-default no-print" OnClientClick="window.print();"><i class="fa fa-print"></i>Print</asp:LinkButton>
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
                                        <td colspan="7" style="text-align: center;">
                                            <asp:Label ID="lblheadingFirst" CssClass="lblheadingFirst" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                            </div>
                           <div class="row" id="divbank" runat="server" visible="false">
                                 <div class="col-xs-4">
                                    <div class="form-group">  
                                        <span  style="font-size:17px; color:red">प्रारंभिक राशि : </span><asp:LinkButton ID="lnkBankOpening" style="font-size:17px;" runat="server" Text="" OnClick="lnkBankOpening_Click"></asp:LinkButton>
                                    </div>
                                    
                                </div>
                               <div class="col-xs-4">
                                    <div class="form-group">  
                                      <asp:LinkButton ID="lnkBankTransactionDetails" style="font-size:17px;" runat="server" Text="Bank Transaction Detail" OnClick="lnkBankTransactionDetails_Click"></asp:LinkButton>
                                    </div>
                                    
                                </div>
                                 <div class="col-xs-4">
                                     <div class="form-group">
                                           <span  style="font-size:17px; color:red">शेष राशि : </span><asp:LinkButton ID="lnkBankClosing" style="font-size:17px;" runat="server" Text="" OnClick="lnkBankClosing_Click"></asp:LinkButton>
                                     </div>
                                   
                                </div>
                           </div>
                              
                            
                            
                        <div class="row">
                          
                            <div class="col-xs-6">
                                  <asp:Label ID="lblReceiptHeading" CssClass="lblheadingFirst" runat="server" Text=""></asp:Label>
                                <asp:GridView ID="GvReceiptDetail" DataKeyNames="VoucherTx_ID" runat="server" ShowFooter="true" AutoGenerateColumns="false" CssClass="datatable table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDeleting="GvReceiptDetail_RowDeleting" EmptyDataText="No Record Found" ClientIDMode="Static" OnRowCommand="GvReceiptDetail_RowCommand">
                                    <Columns>
                                       <asp:TemplateField HeaderText="वाउचर दिनांक/क्रमांक" ItemStyle-Width="12%" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Eval("VoucherTx_Date").ToString() %>' runat="server" /></br>/
                                                <asp:Label ID="lblVoucherTx_No" Text='<%# Eval("VoucherTx_No").ToString() %>' runat="server" />
                                                <asp:HiddenField ID="HF_VoucherTx_ID" runat="server" Value='<%# Eval("VoucherTx_ID").ToString() %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="विवरण">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedger_Name" class="ShowRecord" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" /></br>
                                                <div id="divcostcentre" runat="server" class="HideRecord"></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText="वाउचर क्रमांक">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_No" Text='<%# Eval("VoucherTx_No").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                       <%-- <asp:TemplateField HeaderText="कार्यालय">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffice_Name" Text='<%# Eval("Office_Name").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="राशि" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPayAmt" Text='<%# Eval("Amount").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>                                   
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%" ItemStyle-CssClass="hidden-print" HeaderStyle-CssClass="hidden-print" FooterStyle-CssClass="no-print">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="hpView" runat="server" CssClass="label label-info" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View" OnClientClick="window.document.forms[0].target = '_blank';"></asp:LinkButton>
                                                <%--<asp:LinkButton ID="hpEdit" runat="server" CssClass="label label-primary" CommandName="Editing" CommandArgument='<%# Eval("VoucherTx_ID") %>' Text="Edit" OnClientClick="window.document.forms[0].target = '_blank';"></asp:LinkButton>--%>
                                                <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');" CssClass="label label-danger hidden"></asp:LinkButton>
                                                <asp:LinkButton ID="hpprint" CssClass="label label-primary" runat="server" Text="Print" CommandName="Print" CommandArgument='<%# Eval("VoucherTx_ID") %>' OnClientClick="window.document.forms[0].target = '_blank';"></asp:LinkButton>
                                                <asp:Label ID="lblOfficeID" CssClass="hidden" Text='<%# Eval("Office_ID").ToString() %>' runat="server" />
                                                <asp:Label ID="lblV_Editright" CssClass="hidden" Text='<%# Eval("V_Editright").ToString() %>' runat="server" />
                                                 <asp:Label ID="lblPageUrl" CssClass="hidden" Text='<%# Eval("PageURL").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                               
                             </div>
                            <div class="col-xs-6">
                                 <asp:Label ID="lblPaymentHeading" CssClass="lblheadingFirst" runat="server" Text=""></asp:Label>
                                <asp:GridView ID="GvPaymentDetail" DataKeyNames="VoucherTx_ID" runat="server" ShowFooter="true" AutoGenerateColumns="false" CssClass="datatable table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" OnRowDeleting="GvPaymentDetail_RowDeleting" EmptyDataText="No Record Found" ClientIDMode="Static" OnRowCommand="GvPaymentDetail_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="वाउचर दिनांक/क्रमांक" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Eval("VoucherTx_Date").ToString() %>' runat="server" /></br>/
                                                <asp:Label ID="lblVoucherTx_No" Text='<%# Eval("VoucherTx_No").ToString() %>' runat="server" />
                                                <asp:HiddenField ID="HF_VoucherTx_ID" runat="server" Value='<%# Eval("VoucherTx_ID").ToString() %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="विवरण">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedger_Name" class="ShowRecord" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" /></br>
                                               <div id="divPayCostCentre" runat="server" class="HideRecord"></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                   
                                        <asp:TemplateField HeaderText="राशि"  ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecAmt" Text='<%# Eval("Amount").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%" ItemStyle-CssClass="hidden-print" HeaderStyle-CssClass="hidden-print" FooterStyle-CssClass="no-print">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="hpView" runat="server" CssClass="label label-info" CommandArgument='<%# Eval("VoucherTx_ID") %>' CommandName="View" Text="View" OnClientClick="window.document.forms[0].target = '_blank';"></asp:LinkButton>
                                               <%-- <asp:LinkButton ID="hpEdit" runat="server" CssClass="label label-primary" CommandName="Editing" CommandArgument='<%# Eval("VoucherTx_ID") %>' Text="Edit" OnClientClick="window.document.forms[0].target = '_blank';"></asp:LinkButton>--%>
                                                <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');" CssClass="label label-danger hidden"></asp:LinkButton>
                                                <asp:LinkButton ID="hpprint" CssClass="label label-primary" runat="server" Text="Print" CommandName="Print" CommandArgument='<%# Eval("VoucherTx_ID") %>' OnClientClick="window.document.forms[0].target = '_blank';"></asp:LinkButton>
                                                <asp:Label ID="lblOfficeID" CssClass="hidden" Text='<%# Eval("Office_ID").ToString() %>' runat="server" />
                                                <asp:Label ID="lblV_Editright" CssClass="hidden" Text='<%# Eval("V_Editright").ToString() %>' runat="server" />
                                                 <asp:Label ID="lblPageUrl" CssClass="hidden" Text='<%# Eval("PageURL").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                              
                            </div>
                        </div>
                    </div>


                </div>

            </div>
              <div class="modal fade" id="BankOpeningModal" role="dialog" data-backdrop="false">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Bank Opening Details<span id="spnBankOpening" runat="server" style="color:red;"></span> </h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView runat="server" CssClass="table table-bordered" ShowFooter="true" ShowHeaderWhenEmpty="true" ID="gvBankOpening" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="S. NO.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNo" runat="server" Text='<%#Container.DataItemIndex +1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ledger Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLedgerName" runat="server" Text='<%# Bind("Ledger_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpening" runat="server" Text='<%# Eval("PreOpeningBalance").ToString().Contains("-")?Math.Abs(Convert.ToDecimal(Eval("PreOpeningBalance")))+ " " + "Dr" : Math.Abs(Convert.ToDecimal(Eval("PreOpeningBalance")))+ " " + "Cr"%>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
            <div class="modal fade" id="BankClosingModal" role="dialog" data-backdrop="false">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Bank Closing Details</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView runat="server" CssClass="table table-bordered" ShowFooter="true" ShowHeaderWhenEmpty="true" ID="gvBankClosing" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="S. NO.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNo" runat="server" Text='<%#Container.DataItemIndex +1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ledger Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLedgerName" runat="server" Text='<%# Bind("Ledger_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpening" runat="server" Text='<%# Eval("OpeningBalance").ToString().Contains("-")?Math.Abs(Convert.ToDecimal(Eval("OpeningBalance")))+ " " + "Dr" : Math.Abs(Convert.ToDecimal(Eval("OpeningBalance")))+ " " + "Cr"%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
            <div class="modal fade" id="BankTransactionModal" role="dialog" data-backdrop="false">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Bank Transaction Details</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView runat="server" CssClass="table table-bordered" ShowFooter="true" ShowHeaderWhenEmpty="true" ID="gvBankTransactionDetails" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="S. NO.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBankTranRowno" runat="server" Text='<%#Container.DataItemIndex +1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ledger Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBankTranLedgerName" runat="server" Text='<%# Bind("Ledger_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening">
                                        <ItemTemplate>
                                             <asp:Label ID="lblBankTranOpening" runat="server" Text='<%# Eval("PreOpeningBalance").ToString().Contains("-")?Math.Abs(Convert.ToDecimal(Eval("PreOpeningBalance")))+ " " + "Dr" : Math.Abs(Convert.ToDecimal(Eval("PreOpeningBalance")))+ " " + "Cr"%>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Txn[Debit Amt]">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBankTranDebitAmt" runat="server" Text='<%# Eval("DebitAmt")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Txn[Credit Amt]">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBankTranCreditAmt" runat="server" Text='<%# Eval("CreditAmt")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBankTranClosing" runat="server" Text='<%# Eval("OpeningBalance").ToString().Contains("-")?Math.Abs(Convert.ToDecimal(Eval("OpeningBalance")))+ " " + "Dr" : Math.Abs(Convert.ToDecimal(Eval("OpeningBalance")))+ " " + "Cr"%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>

                </div>
                <div class="modal-footer">
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
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script>
        $(window).on('load', function () {
         
            
            $(".HideRecord").css("display", "none");
            $(".ShowRecord").css("display", "block");
        })
        function ShowBankOpeningModal() {

            $('#BankOpeningModal').modal('show');
        }
        function ShowBankClosingModal() {
            $('#BankClosingModal').modal('show');
        }
        function ShowBankTransactionModal() {
            $('#BankTransactionModal').modal('show');
        }
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
                if ($('.HideRecord').is(':visible')) {
                    $(".HideRecord").css("display", "none");
                    $(".ShowRecord").css("display", "block");
                    $("p.subledger").css("border-top", "none");
                }
                else {
                    $(".HideRecord").css("display", "block");
                    $(".ShowRecord").css("display", "none");
                  
                }
            }
                   

            return true;
        }
        document.onkeydown = handleKeyDown;
        //$(function () {
        //    $('[id*=ddlOffice]').multiselect({
        //        includeSelectAllOption: true,
        //        includeSelectAllOption: true,
        //        buttonWidth: '100%',

        //    });


        //});
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
    <script>
        $('.datatable').DataTable({
            paging: false,
            //columnDefs: [{
            //    targets: 'no-sort',
            //    orderable: false
            //}],
            columnDefs: [{
                orderable: false
            }],
            "bSort": false,
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
                
               
            },
            
            buttons: {
                buttons: [
                //    {
                //    extend: 'print',
                //    text: '<i class="fa fa-print"></i> Print',
                //    title: $('h3').text(),
                //    exportOptions: {
                //        columns: [0, 1, 2, 3, 4, 5, 6]
                //    },
                //    footer: true,
                //    autoPrint: true
                //},
                //{
                //    extend: 'excel',
                //    text: '<i class="fa fa-file-excel-o"></i> Excel',
                //    title: $('h3').text(),
                //    exportOptions: {
                //        columns: [0, 1, 2, 3, 4, 5, 6]
                //    },
                //    footer: true
                //}
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

        function validateform() {
            debugger;
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
           
            if (ymonth == zmonth && yyear == zyear)
            {

            }
            else {
                msg += "Selection of Dates  should be between Same Month.(तिथियों का चयन एक ही माह के बीच होना चाहिए।)";
            }
            if (yd != "" && zd != "") {
                if (yd > zd) {
                    msg += "To Date should be greater than From Date ";
                }
                else {

                    if ((FromYear == ToYear - 1) || (FromYear == ToYear)) {
                        //if (FromYear == ToYear && ToMonth <= 12 && ToMonth > 3 && FromMonth >= 4) {
                        //}
                        //if (FromYear == ToYear && FromMonth <= 3 && ToMonth <= 3) {
                        //}
                        //else if (FromYear < ToYear && ToMonth <= 3 && FromMonth >= 4) {
                        //}
                        if (FromYear == ToYear && FromMonth <= 3 && ToMonth <= 3) {
                        }
                        else if (FromYear == ToYear && FromMonth >= 4 && ToMonth <= 12) {
                        }
                        else if (FromYear != ToYear && FromMonth > 3 && ToMonth <= 3) {
                        }
                        else {
                            msg += "Selection of Dates (From Date - To Date) should be between Financial Year.";
                        }
                    }
                    else {
                        msg += "Selection of Dates (From Date - To Date) should be between Financial Year.";
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
            else {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }

        }
      
    </script>
</asp:Content>
