<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptExpensesAssets.aspx.cs" Inherits="mis_Finance_RptExpensesAssets" EnableEventValidation="false" %>

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
        .header {
                display: table-header-group;
            }
        @media print {

            .hide_print, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
            }

            tfoot, thead {
                display: table-row-group;
                bottom: 0;
            }
        }
         
      
        .voucherColumn {
            width: 150px !important;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header hide_print">
                    <h3 class="box-title">भुगतान का विवरण</h3>    
                     <asp:Button ID="Button1" runat="server" CssClass="btn btn-default pull-right" Text="Print" Style="margin-left: 10px;" OnClientClick="window.print();"></asp:Button>
                    <p class="hide_print">
                         <span runat="server" id="spnAltB">[<span class="Scut">Alt+b</span> - Back View] </span><span runat="server" id="spnAltW">,[<span class="Scut">Alt+w</span> - Condensed & Detailed View] </span>

                    </p>            
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row hide_print">
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
                                <label>To Date<span style="color: red;">*</span></label>
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
                                <%--<asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2">
                                </asp:DropDownList>--%>
                            </div>
                        </div>
                        </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success btn-block no-print" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    
                   <%-- <div class="row" id="divshow" runat="server" visible="false">
                                    <div class="col-md-12  hide_print">
                                        <div class="form-group">
                                            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="btn btn-primary" OnClick="btnExport_Click"/>
                                             <asp:Button ID="btnprint" Text="Print" runat="server" CssClass="btn btn-primary" OnClientClick="window.print();" />
                                        </div>
                                    </div>
                                </div>
                    <div class="row">
                        <div class="col-md-12">
                             <div id="tableData" runat="server">
                        
                    </div>
                        </div>
                    </div>--%>
                   
                      <div class="row">
                        <div class="col-md-12">
                            <strong style="height: 40px;">
                                <br />
                                <div class="hide_print hidden">
                                   <%-- <asp:Button ID="btnHeadExcel" runat="server" CssClass="btn btn-default hidden" Text="Excel" OnClick="btnHeadExcel_Click" />
                                    <asp:Button ID="btnMonthExcel" runat="server" CssClass="btn btn-default" Text="Excel" OnClick="btnMonthExcel_Click" />
                                    <asp:Button ID="btnDayBookExcel" runat="server" CssClass="btn btn-default hidden" Text="Excel" OnClick="btnDayBookExcel_Click" />--%>
                                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-block btn-success hidden Aselect1" Text="<< BACK " OnClick="btnBack_Click" AccessKey="B" />
                                  <%--  <asp:Button ID="btnShowDetailBook" runat="server" CssClass="hidden" Text="Show Bank Detail" OnClick="btnShowDetailBook_Click" AccessKey="Q" />--%>
                                </div>
                            </strong>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblExecTime" runat="server" CssClass="ExecTime"></asp:Label>
                        </div>
                    </div>
                    <div class="row hide_print">
                        <div class="col-md-12">
                            <div runat="server" id="divExcel">
                                <%-- <input type="button" onclick="tableToExcel('tableData', 'W3C Example Table')" value="Export to Excel">--%>
                                <a id="dlink" style="display: none;"></a>
                                <input type="button" onclick="tableToExcel('tableData', 'W3C Example Table', 'GroupSummary')" value="Export to Excel">
                            </div>
                            <%--  <script type="text/javascript">
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
                            </script>--%>
                        </div>
                    </div>

                    <div id="tableData">
                        <div class="row">
                            <div class="col-md-12">
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="5" style="text-align: center;">
                                            <asp:Label ID="lblheadingFirst" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="5" style="text-align: left;">
                                            <asp:Label ID="lblHeadName" runat="server" Style="font-size: 20px;" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gvData" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="table table-hover table-bordered" ShowFooter="true" OnRowCommand="gvData_RowCommand" OnRowDataBound="gvData_RowDataBound">
                                    <Columns>
                                         <asp:TemplateField HeaderText="मुख्य लेखा शीर्ष">
                                            <ItemTemplate>
                                                <asp:Label ID="lblParentHeadCode" runat="server" Text='<%# Eval("ParentHeadCode") %>' Font-Bold='<%# Eval("Ledger_ID").ToString()==""?true:false %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="विवरण">
                                            <ItemTemplate>
                                                <asp:Label ID="lblParentHeadName" runat="server" Text='<%# Eval("Parent_Head_Name_Hindi") %>' Font-Bold='<%# Eval("Ledger_ID").ToString()==""?true:false %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="लेखा शीर्ष">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHeadCode" runat="server" Text='<%# Eval("Head_Code") %>' Font-Bold='<%# Eval("Ledger_ID").ToString()==""?true:false %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="विवरण">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHeadName" runat="server" Text='<%# Eval("Head_Name_Hindi") %>' Font-Bold='<%# Eval("Ledger_ID").ToString()==""?true:false %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="उपलेखा शीर्ष">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedgerCode" runat="server" Text='<%# Eval("Ledger_Code") %>' Font-Bold='<%# Eval("Ledger_ID").ToString()==""?true:false %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--  <asp:BoundField HeaderText="विवरण" DataField="Ledger_Name_Hindi" Visible='<%# Eval("Ledger_ID").ToString()==""?true:false %>' />--%>
                                        <asp:TemplateField HeaderText="विवरण">

                                            <ItemTemplate>

                                                <asp:LinkButton ID="lblLedger_Name_Hindi" runat="server" Text='<%# Eval("Ledger_Name_Hindi") %>' Visible='<%# Eval("Ledger_ID").ToString()==""?false:true %>' CommandName="View" CommandArgument='<%# Eval("Ledger_ID").ToString()%>'></asp:LinkButton>

                                                <asp:Label ID="lbl" runat="server" Visible='<%# Eval("Ledger_ID").ToString()==""?true:false %>' Text='<%# Eval("Ledger_Name_Hindi") %>' CssClass="align" Font-Bold='<%# Eval("Ledger_ID").ToString()==""?true:false %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="बजट आवंटन">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBudget_Allocation" runat="server" Text='<%# Eval("Budget_Allocation") %>' Font-Bold='<%# Eval("Ledger_ID").ToString()==""?true:false %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="गत माह तक व्यय">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPreFromOpeningBalance" runat="server" Text='<%# Eval("PreFromOpeningBalance") %>' Font-Bold='<%# Eval("Ledger_ID").ToString()==""?true:false %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="वर्तमान माह में व्यय">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrentTxn" runat="server" Text='<%# Eval("CurrentTxn") %>' Font-Bold='<%# Eval("Ledger_ID").ToString()==""?true:false %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="कुल व्यय">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotal" runat="server" Text='<%# Eval("Closing") %>' Font-Bold='<%# Eval("Ledger_ID").ToString()==""?true:false %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="शेष राशि">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBalance" runat="server" Text='<%# Eval("Balance") %>' ForeColor='<%# decimal.Parse(Eval("Balance").ToString())>=0?System.Drawing.Color.Black:System.Drawing.Color.OrangeRed %>'  Font-Bold='<%# Eval("Ledger_ID").ToString()==""?true:false %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                              
                            </div>
                        </div>
                        <div class="col-md-12">
                            <%--LEDGER DETAIL MONTH WISE--%>
                            <asp:GridView ID="GridView3" runat="server" ClientIDMode="Static" AutoGenerateColumns="false" class="table table-hover table-bordered" ShowFooter="true" OnRowCommand="GridView3_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <asp:Label ID="lblLedger_ID" CssClass="hidden" Visible="false" Text='<%# Eval("Ledger_ID").ToString() %>' runat="server" />
                                            <asp:Label ID="lblMonthID" CssClass="hidden" Visible="false" Text='<%# Eval("MonthID").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="Aselect1" CommandName="View" HeaderText="Month Name" DataTextField="MonthName" />

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

                        <div class="col-md-12">
                            <%--LEDGER DETAIL MONTH WISE--%>
                            <div id="DivTable" runat="server"></div>
                            
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
    <script>
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
           <%--if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }--%>
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }

        }
        //function PrintPage() {
        //    window.print();
        //}
    </script>
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
                if ($('.HideRecord').is(':visible')) {
                    $(".HideRecord").css("display", "none");
                    $("p.subledger").css("border-top", "none");
                }
                else {
                    $(".HideRecord").css("display", "table-row");
                    $("p.subledger").css("border-top", "1px solid #ccc");
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
    <script type="text/javascript">
       
        var tableToExcel = (function () {
            debugger;
            var uri = 'data:application/vnd.ms-excel;base64,'
              , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
              , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
              , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
            // return function (table, name) {
            return function (table, name, filename) {
                debugger;
                var x = $("#" + table).clone();
                $(x).find("tr td a").replaceWith(function () {
                    return $.text([this]);
                });
                //console.log(x);
                //console.log(x.innerHTML);
                if (!table.nodeType) table = x
                //console.log(table[0].innerHTML);
                //if (!table.nodeType) table = document.getElementById(table)
                var ctx = { worksheet: name || 'Worksheet', table: table[0].innerHTML }
                //window.location.href = uri + base64(format(template, ctx))
                document.getElementById("dlink").href = uri + base64(format(template, ctx));
                document.getElementById("dlink").download = filename;
                document.getElementById("dlink").click();
            }
        })()
       </script>
</asp:Content>
