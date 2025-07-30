<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DifferenceStatement.aspx.cs" Inherits="mis_Finance_DifferenceStatement" %>

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
                    <h3 class="box-title">Transaction  Difference Statement</h3>                  
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
                    <div id="divreport" runat="server" visible="false">
                        <fieldset>
                            <div class="row" >
                            <div class="col-md-12" style="text-align:center;">
                                <div class="form-group">
                                    <label >Transaction  Difference Report</label>
                                </div>
                            </div> 
                        <div class="col-md-12">
                            <div class="form-group">
                            <%--<table class="table table-bordered" style="width:100%;">
                                <tr>
                                    <th>Opening</th>
                                    <th>Receipt</th>
                                    <th>Total Credits</th>
                                    <th>Closing</th>
                                    <th>Payment</th>
                                    <th>Total Debits</th>
                                    <th>Difference</th>
                                    <th>Payment (Cr)</th>
                                </tr>
                                <tr>
                                    
                                    <td><asp:Label ID="lblOpening" runat="server" Text=""></asp:Label></td>
                                    <td><asp:Label ID="lblReceipts" runat="server" Text=""></asp:Label></td>
                                    <td><b><asp:Label ID="lblTotalCredits" runat="server" Text=""></asp:Label></b></td>
                                    <td><asp:Label ID="lblClosing" runat="server" Text=""></asp:Label></td>
                                    <td><asp:Label ID="lblPayment" runat="server" Text=""></asp:Label></td>
                                    <td><b><asp:Label ID="lblTotalDebits" runat="server" Text=""></asp:Label></b></td>
                                    <td><asp:Label ID="lblDifference" runat="server" Text=""></asp:Label></td>
                                    <td><asp:Label ID="lblPaymentCr" runat="server" Text=""></asp:Label></td>
                                </tr>
                                 
                                
                            </table>--%>
                           <table class="table table-bordered" style="width:50%; margin-left:300px;">
                                <tr>
                                    <td>Opening : </td>
                                    <td style="text-align:right"><asp:Label ID="lblOpening" runat="server" Text=""></asp:Label></td>
                                    <td>Closing : </td>
                                    <td style="text-align:right"><asp:Label ID="lblClosing" runat="server" Text=""></asp:Label></td>
                                </tr>
                                 <tr>
                                    <td>Receipts : </td>
                                    <td style="text-align:right"><asp:Label ID="lblReceipts" runat="server" Text=""></asp:Label></td>
                                    <td>Payment : </td>
                                    <td style="text-align:right"><asp:Label ID="lblPayment" runat="server" Text=""></asp:Label></td>
                                </tr>
                                 <tr>
                                     <td>Sum of Credits : </td>
                                    <td  style="text-align:right"><b><asp:Label ID="lblTotalCredits" runat="server" Text=""></asp:Label></b></td>
                                      <td>Sum of Debits : </td>
                                     <td style="text-align:right"><b><asp:Label ID="lblTotalDebits" runat="server" Text=""></asp:Label></b></td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align:right; color:red">Total Debits - Receipts</td>
                                    <td colspan="2" style="text-align:right"><asp:Label ID="lblDifference" runat="server" Text=""></asp:Label></td>
                                </tr>
                                 <tr>
                                    <td colspan="2" style="text-align:right; color:green">Payment Transaction(Cr)</td>
                                    <td colspan="2" style="text-align:right"><asp:Label ID="lblPaymentCr" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr style="background-color:wheat">
                                    
                                    <td colspan="2" style="text-align:right"><b><asp:Label ID="lblLTTotal" runat="server" Text=""></asp:Label></b></td>
                                    <td colspan="2" style="text-align:right"><b><asp:Label ID="lblRTTotal" runat="server" Text=""></asp:Label></b></td>
                                </tr>
                                
                            </table>
                        </div>
                            </div>
                    </div>
                        
                         <div id="divhideshow" runat="server" visible="false">
                  <div class="row">
                      <div class="col-md-12">
                          <div class="form-group">
                              <label>Reason:<span style="color: red;">*</span></label>
                              <asp:TextBox ID="txtDiifReason" CssClass="form-control" runat="server" Text="" TextMode="MultiLine" Rows="2"></asp:TextBox>
                          </div>
                      </div>
                  </div>
                   <fieldset>
                       <legend> Upload Bank Details</legend>
                       <div class="row">
                        <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Bank name(बैंक का नाम)<span style="color: red;">*</span></label>
                                        <%-- <asp:TextBox ID="txtBank_Name" runat="server" placeholder="Enter Bank Name..." class="form-control" MaxLength="100"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlBank_Name" runat="server" placeholder="Select Bank Name..." class="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>UploadDocs <span style="color: red;">*</span></label>
                                <asp:FileUpload ID="fuDocs" CssClass="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Button ID="btnAdd" Text="Add" CssClass="btn btn-primary" style="margin-top:25px;" runat="server"  OnClick="btnAdd_Click"/>
                            </div>
                        </div>
                    </div>
                       <div class="row">
                           <div class="col-md-12">
                               <asp:GridView ID="gvBankDocs" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="gvBankDocs_RowCommand">
                                   <Columns>
                                       <asp:TemplateField HeaderText="S.No">
                                           <ItemTemplate>
                                            <asp:Label ID="lblRowno" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Bank Name">
                                           <ItemTemplate>
                                            <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("BankName") %>'></asp:Label>
                                                <asp:Label ID="lblBank_id" runat="server" Visible="false" Text='<%# Eval("Bank_id") %>'></asp:Label>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Document">
                                           <ItemTemplate>
                                           <asp:HyperLink ID="hyplnkDocs" Text="View" runat="server" NavigateUrl='<%# Eval("BankDoc") %>' Target="_blank"></asp:HyperLink>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Action">
                                           <ItemTemplate>
                                           <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("RowNo") %>' CommandName="DeleteRecord"><i class="fa fa-trash"></i></asp:LinkButton>
                                           </ItemTemplate>
                                       </asp:TemplateField>
                                   </Columns>
                               </asp:GridView>
                           </div>
                       </div>
                   </fieldset>
                    <div class="row">
                        <div class="col-md-3">
                            <asp:Button id="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                             </div> 
                            </fieldset>
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

            if (ymonth == zmonth && yyear == zyear) {

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