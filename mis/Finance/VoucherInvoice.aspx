<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VoucherInvoice.aspx.cs" Inherits="mis_Finance_VoucherInvoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <title>&nbsp;</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="../css/bootstrap.css"/>
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css"/>

    <!-- Theme style -->
    <link rel="stylesheet" href="../css/AdminLTE.css"/>
<%--    <link href="../css/bootstrap.css" rel="stylesheet" />--%>
    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic"/>
    <style type="text/css">
        .btn-danger {
            color: #4b4c9d;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <div>
            <div style="width: 90%; margin: auto; border-radius: 5px; border: 1px solid #b5b5b5; padding: 5px">
                <a href="purchaseorderdetail.aspx" class="btn btn-success no-print" style="position: fixed; z-index: 999999">Back</a>
                <!-- Main content -->
                <section class="invoice">
                    <!-- title row -->
                    <div class="row">
                        <div class="col-xs-12" style="border-bottom: 1px solid #eee;">
                            <div class="col-xs-2 col-md-2">
                                <img class="pull-right" src="../image/mpagro-logo.png" / style="margin-top: 1px;"/>
                            </div>
                            <div class="col-xs-10 col-md-10">
                                <h2 class="page-header text-center" style="border-bottom: 1px solid #FFF!important; margin-top: 2px">The M.P. Agro Industries Development Corpn. Ltd.<br />
                                    <small>(A Govt. of Madhya Pradesh Undertaking)<br />
                                        <span style="margin-top: 10px">H.O. 3rd Floor, "Panchanan" Building, Malviya Nagar, Bhopal</span></small><br />
                                    <p style="font-size: 20px;">VOUCHER INVOICE</p>
                                </h2>
                            </div>
                            <%--<div class="col-xs-0 col-md-3"></div>--%>
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- info row -->
                    <div class="row invoice-info">
                        <%--<div class="col-sm-4 invoice-col">
                            To
          <address>
              <strong>
                  <asp:Label ID="lblVname" runat="server"></asp:Label></strong><br/>
              <asp:Label ID="lblVAddress" runat="server"></asp:Label><br/>
              Phone:
              <asp:Label ID="lblVMobileNo" runat="server"></asp:Label><br/>
              Email:
              <asp:Label ID="lblVEmail" runat="server"></asp:Label><br/>
              GSTN:
              <asp:Label ID="lblGSTN" runat="server"></asp:Label>
          </address>
                        </div>
                        <!-- /.col -->
                        <div class="col-sm-4 invoice-col">
                            From
          <address>
              <strong>
                  <asp:Label ID="lblDepartName" runat="server"></asp:Label></strong><br/>

              <asp:Label ID="lblDepartAddress" runat="server"></asp:Label><br/>
              Phone:   
              <asp:Label ID="lblDepartMobileNo" runat="server"></asp:Label><br/>
              Email:
              <asp:Label ID="lblDepartEmail" runat="server"></asp:Label><br/>
             
          </address>
                        </div>--%>
                        <!-- /.col -->
                        <div class="col-sm-4 invoice-col">
                            <b>Voucher NO:</b>
                            <asp:Label ID="lblVoucherNo" runat="server"></asp:Label><br>
                            <b>Voucher Date:</b>
                            <asp:Label ID="lblVoucherDt" runat="server"></asp:Label><br>
                            <b>Voucher Amount:</b>
                            <asp:Label ID="lblVoucherAmt" runat="server"></asp:Label><br>
                             <b>Voucher Name:</b>
                            <asp:Label ID="lblVoucherName" runat="server"></asp:Label><br>
                             <b>Voucher Type:</b>
                            <asp:Label ID="lblVoucherType" runat="server"></asp:Label>
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->

                    <!-- Table row -->
                    <div class="row">
                        <div class="col-xs-12 table-responsive">
                            <asp:GridView ID="gvAddItems" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found." UseAccessibleHeader="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Item Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemCat_id" runat="server" Text='<%#Eval("ItemCat_id") %>' Visible="false"></asp:Label>
                                            <%# Eval("ItemCatName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                   <%-- <asp:TemplateField HeaderText="HSN Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHSN_Code" runat="server" Text='<%#Eval("HSN_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIItem_id" runat="server" Text='<%#Eval("Item_id") %>' Visible="false"></asp:Label>
                                            <%# Eval("ItemName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
<%--                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <%# Eval("UQCCode") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                        </ItemTemplate>
                                        <%-- <FooterTemplate>
                                            <b>Total:</b>
                                        </FooterTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>    
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                        <%-- <FooterTemplate>
                                            <b>Total:</b>
                                        </FooterTemplate>--%>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="SGST(%)">
                                        <ItemTemplate>

                                            <asp:Label ID="lblSGSTPer" runat="server" Text='<%# Eval("CPur_SGSTPer") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CGST(%)">
                                        <ItemTemplate>

                                            <asp:Label ID="lblCGSTPer" runat="server" Text='<%# Eval("CPur_CGSTPer") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <b>Total:</b>
                                        </FooterTemplate>
                                    </asp:TemplateField>--%>
                                   <%-- <asp:TemplateField HeaderText="SGST Amount">
                                        <ItemTemplate>

                                            <asp:Label ID="lblSGSTAmt" runat="server" Text='<%# Eval("CPur_SGSTAmt") %>'></asp:Label>
                                             <small><b><label id="lblCPur_SGSTPer1" runat="server" style="color:red"><%# Eval("CPur_SGSTPer").ToString()%></label></b></small>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFSGSTAmt" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CGST Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCGSTAmt" runat="server" Text='<%# Eval("CPur_CGSTAmt") %>'></asp:Label>
                                            <small><b><label id="lblCPur_CGSTPer1" runat="server" style="color:red"><%# Eval("CPur_CGSTPer").ToString()%></label></b></small>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFCGSTAmt" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>

                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("CPur_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFAmount" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>--%>

                                </Columns>
                            </asp:GridView>
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->

                   <div class="row">
                        <!-- accepted payments column -->
                        <div class="col-xs-7">
                        </div>
                        <div class="col-xs-5">
                            <asp:GridView ID="GridViewLedger" runat="server" DataKeyNames="LedgerID" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" ShowHeader="false" >
                                            <Columns>
                                                <%-- <asp:TemplateField HeaderText="Action" ShowHeader="False"  ItemStyle-Width="50">                                                                                                                        
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" Visible='<%# Eval("Status").ToString() =="1" ? true:false %>' runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Ledger will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Ledger" ShowHeader="False">
                                                    <ItemTemplate>
                                                       <%-- <asp:LinkButton ID="Delete" Visible='<%# Eval("Status").ToString() =="1" ? true:false %>' runat="server" CssClass="label " CausesValidation="False" CommandName="Delete" Text="" Style="color: red;" OnClientClick="return confirm('The Ledger will be deleted. Are you sure want to continue?');"><i class="fa fa-trash"></i></asp:LinkButton>--%>
                                                        <asp:Label ID="lblLedgerName" CssClass="paddingLR" runat="server" Text='<%# Eval("LedgerName").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("LedgerID").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" ShowHeader="False" ItemStyle-Width="50%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtAmount" Enabled='<%# (Eval("LedgerName").ToString() =="CGST" || Eval("LedgerName").ToString() =="SGST") ? false:true %>' runat="server" onblur="CalculateGrandTotal();" CssClass="AlignR" Style="padding: 3px;" Text='<%# Eval("Amount").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                             <hr />
                                                <table class="table table-bordered customCSS" id="divgrandtotal" runat="server" visible="false">
                                                    <tr>
                                                        <th>GRAND TOTAL :
                                                        </th>
                                                        <th style="width: 50%; padding: 3px;">
                                                            <asp:label ID="lblGrandTotal" ClientIDMode="Static" CssClass="AlignR" Style="padding: 3px;" runat="server" Text="0"></asp:label>
                                                        </th>
                                                    </tr>
                                                </table>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 table-responsive">
                            <asp:GridView ID="gvAddLedger" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover"
                                EmptyDataText="No Record Found." ShowFooter="true" UseAccessibleHeader="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--  <asp:TemplateField HeaderText="Item Category">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemCat_id" runat="server" Text='<%#Eval("ItemCat_id") %>' Visible="false"></asp:Label>
                                            <%# Eval("ItemCatName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" runat="server" Text='<%#Eval("Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars">
                                        <ItemTemplate>
                                            <asp:Label ID="lblParticulars" runat="server" Text='<%#Eval("Ledger_Name") %>' ></asp:Label>
                                            
                                        </ItemTemplate>
                                    </asp:TemplateField>
<%--                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <%# Eval("UQCCode") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Credit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" runat="server" Text='<%# Eval("LedgerTx_Credit")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDebit" runat="server" Text='<%# Eval("LedgerTx_Debit")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="SGST(%)">
                                        <ItemTemplate>

                                            <asp:Label ID="lblSGSTPer" runat="server" Text='<%# Eval("CPur_SGSTPer") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CGST(%)">
                                        <ItemTemplate>

                                            <asp:Label ID="lblCGSTPer" runat="server" Text='<%# Eval("CPur_CGSTPer") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <b>Total:</b>
                                        </FooterTemplate>
                                    </asp:TemplateField>--%>
                                    <%--<asp:TemplateField HeaderText="SGST Amount">
                                        <ItemTemplate>

                                            <asp:Label ID="lblSGSTAmt" runat="server" Text='<%# Eval("CPur_SGSTAmt") %>'></asp:Label>
                                             <small><b><label id="lblCPur_SGSTPer1" runat="server" style="color:red"><%# Eval("CPur_SGSTPer").ToString()%></label></b></small>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFSGSTAmt" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CGST Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCGSTAmt" runat="server" Text='<%# Eval("CPur_CGSTAmt") %>'></asp:Label>
                                            <small><b><label id="lblCPur_CGSTPer1" runat="server" style="color:red"><%# Eval("CPur_CGSTPer").ToString()%></label></b></small>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFCGSTAmt" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>

                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("CPur_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblFAmount" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>--%>

                                </Columns>
                            </asp:GridView>
                        </div>
                        <!-- /.col -->
                    </div>
                    <!-- /.row -->

                    
                    <div class="row">
                        <div class="col-xs-12">
                            <label>Narration:</label>
                            <asp:label ID="lblNarration"  runat="server"></asp:label>
                        </div>
                    </div>
                   
                    <!-- /.row -->

                    <!-- this row will not appear when printing -->
                    <div class="row no-print">
                        <div class="col-xs-12">
                            <a onclick="myFunction()" target="_blank" class="btn btn-default"><i class="fa fa-print"></i>Print</a>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
                <div class="clearfix"></div>
            </div>
        </div>
        <script src="../../js/jquery-2.2.3.min.js"></script>
        <script>
            function myFunction() {
                window.print();
            }
        </script>
        <!-- ./wrapper -->
        <!-- jQuery 3 -->
        <script src="../../js/jquery-2.2.3.min.js"></script>
        <!-- Bootstrap 3.3.7 -->
        <script src="../../js/bootstrap.js"></script>
        <!-- FastClick -->
    </form>
</body>
</html>
