<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HOLoanReconciliationReport.aspx.cs" Inherits="mis_Finance_HOLoanReconciliationReport" %>

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
        }

        .align-right {
            text-align: right !important;
            width: 10% !important;
        }
         .alignR {
            text-align: right !important;
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
            font-size: 15px;
            color: #123456;
        }

        .Scut {
            color: tomato;
        }

        .tab1 td {
            padding: 2px 5px !important;
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

        .lblheadingFirst p {
                text-align: center !important;
                font-size: 16px !important;
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
                    <h3 class="box-title">HO Loan Reconciliation Report</h3>
                     <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-default pull-right" Text="Print" Style="margin-left: 10px;" OnClientClick="window.print();"></asp:Button>
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
                       
                        <div class="col-md-3 hidden">
                            <div class="form-group">
                                <label>Filter Amount :</label><span style="color: red">*</span>
                                <br />
                                <asp:CheckBox ID="chkDebitAmt" Checked="true" runat="server" Text="&nbsp;Debit Amt.&nbsp;&nbsp;" ClientIDMode="Static" onchange="checkboxDebitchange();" />
                                <asp:CheckBox ID="chkCreditAmt" runat="server" Checked="true" Text="Credit Amt." ClientIDMode="Static" onchange="checkboxCreditchange();" />
                            </div>
                        </div>
                    </div>
                    <div class="row Hiderow">
                        <div class="col-md-5">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <label>List Of Ledger</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlLedger" CssClass="form-control select2" >
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success Aselect1" Style="margin-top: 24px;" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <div class="row">
                         <div class="col-md-12">  
                              <asp:Label ID="lblheadingFirst" CssClass="lblheadingFirst" runat="server" Text=""></asp:Label> 
                             </div>
                        <div class="col-md-12">                           
                            <asp:GridView ID="GridView1" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="tab1 table table-hover table-bordered"  style="margin-bottom: 0px;" EmptyDataText="No Record Found.">
                                <Columns>
                                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" Text='<%# Eval("VoucherTx_Date").ToString() %>' runat="server" />                                         
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ledger Name.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLedger_Name" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Voucher No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoucherTx_No" Text='<%# Eval("VoucherTx_No").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Voucher Type.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoucherTx_Type" Text='<%# Eval("VoucherTx_Type").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>                                   
                                    <asp:TemplateField HeaderText="HO Date">
                                        <ItemTemplate>
                                             <asp:Label ID="lblHOTxn_Date" Text='<%# Eval("HOTxn_Date").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit Amt." ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit Amt." ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <asp:Panel ID="pnlclosing" runat="server">
                        <div class="row">
                            <div class="col-md-6"></div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <table class="table table-bordered">
                                        <tr>
                                            <th style="text-align:center;">Particulars</th>
                                            <th style="text-align:center;">Debit Amt.</th>
                                            <th style="text-align:center;">Credit Amt.</th>
                                        </tr>
                                         <tr>
                                            <th>Ledger Transaction Total Amount</th>
                                            <th class="alignR">
                                                <asp:Label ID="lblDebitAmtT" runat="server" Text=""></asp:Label>
                                            </th>
                                           <th class="alignR">
                                                <asp:Label ID="lblCreditAmtT" runat="server" Text=""></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <th>Balance as per Company Books</th>
                                           <th class="alignR">
                                                <asp:Label ID="lblClosingBalanceDr" runat="server" Text=""></asp:Label>
                                            </th>
                                            <th class="alignR">
                                                <asp:Label ID="lblClosingBalanceCr" runat="server" Text=""></asp:Label>
                                            </th>
                                        </tr>
                                        <tr>
                                            <th>Amounts not reflected in HO Loan</th>
                                           <th class="alignR">
                                                <asp:Label ID="lblNotRefHODr" runat="server" Text=""></asp:Label>
                                            </th>
                                           <th class="alignR">
                                                <asp:Label ID="lblNotRefHOCr" runat="server" Text=""></asp:Label>
                                            </th>
                                        </tr>
                                         <tr>
                                            <th>As Per HO Loan Balance</th>
                                           <th class="alignR">
                                                <asp:Label ID="lblHOBalanceDr" runat="server" Text=""></asp:Label>
                                            </th>
                                           <th class="alignR">
                                                <asp:Label ID="lblHOBalanceCr" runat="server" Text=""></asp:Label>
                                            </th>
                                        </tr>
                                    
                                    </table>
                                </div>
                            </div>

                        </div>
                    </asp:Panel>

                </div>
                </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>

        function ModalReconcile() {
            $('#ModalReconcile').modal('show');
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

        function checkboxDebitchange() {
            debugger;
            var checkbox1 = document.getElementById('<%= chkDebitAmt.ClientID%>').checked;
            var checkbox2 = document.getElementById('<%= chkCreditAmt.ClientID%>').checked;
            if (checkbox1 == false && checkbox2 == false) {
                document.getElementById('<%= chkDebitAmt.ClientID%>').checked = true;
            }

        }
        function checkboxCreditchange() {
            debugger;
            var checkbox1 = document.getElementById('<%= chkDebitAmt.ClientID%>').checked;
            var checkbox2 = document.getElementById('<%= chkCreditAmt.ClientID%>').checked;
            if (checkbox1 == false && checkbox2 == false) {
                document.getElementById('<%= chkCreditAmt.ClientID%>').checked = true;
            }

        }
    </script>


</asp:Content>

