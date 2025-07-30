<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VoucherConsumption.aspx.cs" Inherits="mis_Finance_VoucherConsumption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        /*.customCSS td {
            padding: 0px !important;
        }*/

        /*.paddingLR {
            padding: 0px 5px;
        }*/
        .AlignR {
            text-align: right !important;
        }

        #GridViewLedger td {
            padding: 3px !important;
        }

        .select2 {
            width: 100% !important;
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
                    <div class="box box-success" style="background-color: #e3dcc0">
                        <div class="box-header">
                            <h3 class="box-title">Consumption Voucher</h3>
                            <asp:LinkButton ID="lbkbtnAddLedger" class="btn btn-primary pull-right" runat="server" OnClick="lbkbtnAddLedger_Click">Add Ledger</asp:LinkButton>
                            <asp:LinkButton ID="lnkPreviousVoucher" class="btn btn-primary pull-right" Style="margin-right: 10px;" runat="server" OnClick="lnkPreviousVoucher_Click">Copy Previous Voucher</asp:LinkButton>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text="" Font-Bold="true" Style="color: blue"></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Label ID="lblPreviousVoucherNo" runat="server" Text="" Font-Bold="true" Style="color: blue"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="panel1" runat="server">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label>Voucher/Bill No.<span style="color: red;"> *</span></label>
                                        <div class="form-group">
                                            <div class="col-md-6">
                                                <asp:Label ID="lblVoucherTx_No" runat="server" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                                <asp:Label ID="lblVoucherNo" runat="server" CssClass="form-control" Visible="false" Style="background-color: #eee;"></asp:Label>
                                            </div>
                                            <div class="col-md-6" style="margin-left: -32px;">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVoucherTx_No" placeholder="Enter Voucher/Bill No." ClientIDMode="Static" MaxLength="6" autocomplete="off" onkeypress="return abc(event);"></asp:TextBox>

                                                <small><span id="valtxtVoucherTx_No" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="display: none;">
                                        <div class="form-group">
                                            <label>Reference No.<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtVoucherTx_Ref" placeholder="Enter Reference No..." MaxLength="50" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                            <small><span id="valtxtVoucherTx_Ref" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox runat="server" CssClass="form-control DateAdd" data-date-end-date="0d" ID="txtVoucherTx_Date" placeholder="Enter Voucher No..." autocomplete="off" ClientIDMode="Static" OnTextChanged="txtVoucherTx_Date_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <small><span id="valtxtVoucherTx_Date" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>By (Dr)<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlLedger" ClientIDMode="Static" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                            <small><span id="valddlLedger" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:CheckBox ID="chkitem" ClientIDMode="Static" runat="server" OnChange="return hideshowitempanel()" />&nbsp;&nbsp;<label>Add Item</label>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlitem" ClientIDMode="Static" runat="server">
                                    <fieldset>
                                        <legend>Add Item</legend>
                                        <div id="divitem" runat="server">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Item Name<span style="color: red;"> *</span></label>
                                                        <asp:DropDownList ID="ddlItemName" ClientIDMode="Static" runat="server" CssClass="form-control select1 select2" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        <small><span id="valddlItemName" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Quantity<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ClientIDMode="Static" onkeypress="return validateDecUnit(this,event)" ID="txtQuantity" placeholder="Enter Quantity..." onchange="CalculateAmount();" MaxLength="11" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                                        <small><span id="valtxtQuantity" style="color: red;"></span></small>
                                                        <asp:Label ID="lblUnit" CssClass="hidden" runat="server" Text="2"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Rate<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" MaxLength="11" CssClass="form-control" ClientIDMode="Static" ID="txtRate" placeholder="Enter Rate..." onchange="CalculateAmount();" onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                                        <small><span id="valtxtRate" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Amount<span style="color: red;"> *</span></label>
                                                        <asp:TextBox ID="txtTotalAmount" runat="server" ClientIDMode="Static" placeholder="Total Amount" CssClass="form-control" onchange="CalculateRate();" MaxLength="12" onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                                        <small><span id="valtxtTotalAmount" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-block btn-info" Text="Add Item" OnClick="btnAdd_Click" OnClientClick="return validateItem();" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                        </div>
                                        <!-- ItemDetail GridView-->
                                        <asp:GridView ID="GridViewItem" runat="server" DataKeyNames="ID" ClientIDMode="Static" class="table table-bordered customCSS" Style="margin-bottom: 0px;" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnRowDeleting="GridViewItem_RowDeleting" ShowFooter="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblItemRowNo" Text='<%# Eval("ID").ToString()%>' CssClass="hidden" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Name">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Item").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("ID").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblItemID" CssClass="hidden" runat="server" Text='<%# Eval("ItemID").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblUnit_id" CssClass="hidden" runat="server" Text='<%# Eval("Unit_id").ToString()%>'></asp:Label>

                                                        <%--<asp:Label ID="lblTaxbility" CssClass="hidden" runat="server" Text='<%# Eval("Taxbility").ToString()%>'></asp:Label>--%>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="HSN Code">
                                                    <HeaderStyle />
                                                    
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHSNCode" runat="server" Text='<%# Eval("HSN_Code").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <HeaderStyle />
                                                    <%-- <ItemStyle HorizontalAlign="Right" />--%>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <HeaderStyle />

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblUnit" CssClass="paddingLR" runat="server" Text='<%# Eval("Unit").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount " ItemStyle-HorizontalAlign="Right">
                                                    <HeaderStyle />

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount").ToString()%>'></asp:Label>
                                                        <asp:TextBox ID="txtAmountH" runat="server" CssClass="hidden" Text='<%# Eval("Amount").ToString()%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CGST">
                                                    <HeaderStyle />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="CGST" CssClass="hidden" runat="server" Text='<%# Eval("CGSTAmt").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblCGST" CssClass="paddingLR" runat="server" Text='<%# string.Concat(Eval("CGSTAmt").ToString(),"(", Eval("CGST_Per").ToString(),"%",")")%>'></asp:Label>
                                                        <asp:Label ID="lblCGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("CGST_Per").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SGST">
                                                    <HeaderStyle />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="SGST" CssClass="hidden" runat="server" Text='<%# Eval("SGSTAmt").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblSGST" CssClass="paddingLR" runat="server" Text='<%# string.Concat(Eval("SGSTAmt").ToString(),"(",Eval("SGST_Per").ToString(),"%",")")%>'></asp:Label>
                                                        <asp:Label ID="lblSGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("SGST_Per").ToString()%>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="IGST" Visible="false">
                                                    <HeaderStyle />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="IGST" CssClass="hidden" runat="server" Text='<%# Eval("IGSTAmt").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblIGST" CssClass="paddingLR" runat="server" Text='<%# string.Concat(Eval("IGSTAmt").ToString(),"(",Eval("IGST_Per").ToString(),"%",")")%>'></asp:Label>
                                                        <asp:Label ID="lblIGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("IGST_Per").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sales Ledger">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLedgerName" runat="server" Text='<%# Eval("Ledger_Name").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblLedgerID" CssClass="hidden" runat="server" Text='<%# Eval("Ledger_ID").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Item will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <small><span id="valGridViewItem" style="color: red;"></span></small>
                                        <!-- End-->
                                        <div class="row">
                                        </div>
                                    </fieldset>
                                </asp:Panel>


                                <div class="row">
                                    <div class="col-md-6"></div>
                                    <div class="col-md-6">
                                        <table class="table table-bordered customCSS">
                                            <tr>
                                                <th>Grand Total :
                                                </th>
                                                <th style="width: 50%; padding: 3px;">
                                                    <asp:TextBox ID="lblGrandTotal" ClientIDMode="Static" CssClass="form-control AlignR" Style="padding: 3px;" runat="server" Text="0"></asp:TextBox>
                                                </th>
                                            </tr>
                                        </table>
                                        <%--<fieldset>
                                            <legend>Amount Detail (Dr)</legend>
                                            <div class="row">
                                                <div class="col-md-12"><table class="table table-bordered customCSS">
                                                        <tr>
                                                            <th>Grand Total :
                                                            </th>
                                                            <th style="width: 50%; padding: 3px;">
                                                                <asp:TextBox ID="lblGrandTotal" ClientIDMode="Static" CssClass="form-control AlignR" Style="padding: 3px;" runat="server" Text="0"></asp:TextBox>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </fieldset>--%>
                                    </div>
                                </div>
                            </asp:Panel>
                            <fieldset>
                                <legend>Creditor Detail (Cr)</legend>
                                <div id="divdebtor" runat="server">
                                    <div class="row">

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Name of Creditor </label>
                                                <asp:DropDownList runat="server" ID="ddlDebitLedger" ClientIDMode="Static" CssClass="form-control select2" autocomplete="off">
                                                </asp:DropDownList>
                                                <small><span id="valddlDebitLedger" style="color: red;"></span></small>
                                            </div>
                                        </div>


                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Amount</label>
                                                <asp:TextBox ID="txDebtorAmt" runat="server" ClientIDMode="Static" Text="" CssClass="form-control" MaxLength="12" onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                                <small><span id="valtxDebtorAmt" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label>&nbsp;</label>
                                                <asp:Button runat="server" ID="btnAddDebtor" CssClass="btn btn-info btn-block" Text="Add" OnClick="btnAddDebtor_Click" OnClientClick="return validateDebtor();" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- DebtorDetail GridView -->
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridViewDebtor" runat="server" DataKeyNames="Ledger_ID" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowFooter="true" OnRowDeleting="GridViewDebtor_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Debtor" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ledger_ID" runat="server" Text='<%# Bind("Ledger_ID") %>'></asp:Label>
                                                        <asp:Label ID="lblMaintainType" runat="server" Text='<%# Bind("LedgerTx_MaintainType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Debtor">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ledger_Name" runat="server" Text='<%# Bind("Ledger_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LedgerTx_Amount" runat="server" Text='<%# Bind("LedgerTx_Amount") %>'></asp:Label>
                                                        <asp:TextBox ID="txtLedgerTx_Amount" runat="server" CssClass="hidden" Text='<%# Bind("LedgerTx_Amount") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" CssClass="label label-danger" OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <!-- End-->
                            </fieldset>
                            <asp:Panel ID="panel2" runat="server">

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Narration<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" ID="txtVoucherTx_Narration" TextMode="MultiLine" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                            <small><span id="valtxtVoucherTx_Narration" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <asp:Button runat="server" CssClass="hidden" ID="btnNarration" OnClick="btnNarration_Click" AccessKey="R" />
                                </div>

                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button runat="server" ClientIDMode="Static" CssClass="btn btn-block btn-success" ID="btnAccept" Text="Accept" OnClientClick="javascript:return validateform();" OnClick="btnAccept_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <a href="VoucherSaleCredit.aspx" id="btn_Clear" runat="server" class="btn btn-block btn-default">Clear</a>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>




            <asp:HiddenField ID="hfvalue" runat="server" />
            <asp:HiddenField ID="hfofficeID" runat="server" />
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>
        function ShowRefDetailModal() {
            $('#AgstRefModal').modal('show');
        }
        function ShowModalChequeDetail() {
            $('#ModalChequeDetail').modal('show');
        }
        function ShowModalChequeDetailView() {
            $('#ModalChequeDetailView').modal('show');
        }
        function ShowBillDetailModal() {
            $('#myModal').modal('show');
            $("#ddlBillByBillTx_Ref").hide();
        }
        function ShowBillByBillViewModal() {
            $('#BillByBillViewModal').modal('show');
        }
        function CalculateAmount() {

            var Quantity = document.getElementById('<%=txtQuantity.ClientID%>').value.trim();
            var Rate = document.getElementById('<%=txtRate.ClientID%>').value.trim();
            if (Quantity == "")
                Quantity = "0";
            if (Rate == "")
                Rate = "0";

            document.getElementById('<%=txtTotalAmount.ClientID%>').value = (Quantity * Rate).toFixed(2);
        }
        function CalculateRate() {

            var Quantity = document.getElementById('<%=txtQuantity.ClientID%>').value.trim();
            var TotalAmount = document.getElementById('<%=txtTotalAmount.ClientID%>').value.trim();
            //var Rate = document.getElementById('<%=txtRate.ClientID%>').value.trim();
            if (Quantity == "") {
                document.getElementById('<%=txtQuantity.ClientID%>').value = "0";
                Quantity = "0";
            }
            if (TotalAmount == "")
                TotalAmount = "0";



            if (Quantity == 0)
                document.getElementById('<%=txtRate.ClientID%>').value = (0 / 1).toFixed(2);
            else
                document.getElementById('<%=txtRate.ClientID%>').value = (TotalAmount / Quantity).toFixed(2);
        }

        function ShowModal() {
            $("#ItemModal").modal();
        }

        function validateItem() {

            var msg = "";
            $("#valddlItemName").html("");

            $("#valtxtQuantity").html("");
            $("#valtxtRate").html("");
            $("#valtxtTotalAmount").html("");
            if (document.getElementById('<%=ddlItemName.ClientID%>').selectedIndex == 0) {
                msg += "Select Item Name. \n";
                $("#valddlItemName").html("Select Item Name");
            }

            if (document.getElementById('<%=txtQuantity.ClientID%>').value.trim() == "") {
                msg += "Enter Quantity. \n";
                $("#valtxtQuantity").html("Enter Quantity");
            }
            if (document.getElementById('<%=txtRate.ClientID%>').value.trim() == "") {
                msg += "Enter Rate. \n";
                $("#valtxtRate").html("Enter Rate");
            }
            if (document.getElementById('<%=txtTotalAmount.ClientID%>').value.trim() == "") {
                msg += "Enter Amount. \n";
                $("#valtxtTotalAmount").html("Enter Amount");
            }
            if (document.getElementById('<%=txtTotalAmount.ClientID%>').value.trim() != "") {
                var amt = document.getElementById('<%=txtTotalAmount.ClientID%>').value.trim();
                if (parseFloat(amt) == 0) {
                    msg += "Amount cannot be Zero.\n";
                    $("#valtxtTotalAmount").html("Amount cannot be Zero.");
                }
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }

        }



        function validateDebtor() {

            var msg = "";
            $("#valddlDebitLedger").html("");
            $("#valtxDebtorAmt").html("");
            if (document.getElementById('<%=ddlDebitLedger.ClientID%>').selectedIndex == 0) {
                msg += "Select Name of Debtor. \n";
                $("#valddlDebitLedger").html("Select Name of Debtor");
            }
            if (document.getElementById('<%=txDebtorAmt.ClientID%>').value.trim() == "") {
                msg += "Enter Amount .\n";
                $("#valtxDebtorAmt").html("Enter Amount");
            }
            if (document.getElementById('<%=txDebtorAmt.ClientID%>').value.trim() != "") {
                var amt = document.getElementById('<%=txDebtorAmt.ClientID%>').value.trim();
                if (parseFloat(amt) == 0) {
                    msg += "Amount cannot be Zero.\n";
                    $("#valtxDebtorAmt").html("Amount cannot be Zero.");
                }
            }
            var chk = document.getElementById('<%=chkitem.ClientID%>').checked;
            if (chk == true) {
                var rowscount = $("#<%=GridViewItem.ClientID %> tr").length;
                if (rowscount == "1") {
                    msg += "Enter Item Detail. \n";
                    $("#valGridViewItem").html("Enter Item Detail");
                }

            }
            if (document.getElementById('<%=txtVoucherTx_No.ClientID%>').value.trim() == "") {
                msg += "Enter Voucher/Bill No. .\n";
                $("#valtxtVoucherTx_No").html("Voucher/Bill No.");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }


        //$(document).ready(function () {
        //    CalculateGrandTotal();
        //});

        function CalculateGrandTotal() {

            var i = 0;
            var Tval = 0;
            var Tval1 = 0;

            var HiddenValue = '<%=hfvalue.Value%>';
            var rowcount = $('#GridViewItem tr').length;
            //alert(rowcount);
            //alert(HiddenValue);
            $('#GridViewItem tr').each(function (index) {

                //if (i > 0 && i < rowcount - 1) {
                if (i > 0 && i < rowcount) {
                    var temp = Tval;
                    var val = $(this).children("td").eq(4).find('input[type="text"]').val();

                    if (val == "")
                        val = 0;

                    Tval = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)

                }
                i++;
            });

            document.getElementById('<%=lblGrandTotal.ClientID%>').value = Tval;
            
           
            <%--document.getElementById('<%=txDebtorAmt.ClientID%>').value = Tval - Tval1;--%>
            if (parseFloat(Math.abs(Tval)) == parseFloat(HiddenValue)) {

                document.getElementById('<%=btnAccept.ClientID%>').disabled = false;
                document.getElementById('<%=btnAddDebtor.ClientID%>').disabled = true;
            }
            else {
                document.getElementById('<%=btnAccept.ClientID%>').disabled = true;
                document.getElementById('<%=btnAddDebtor.ClientID%>').disabled = false;
            }

        }



        function validateform() {

            var msg = "";
            $("#valtxtVoucherTx_No").html("");
            $("#valtxtVoucherTx_Ref").html("");
            $("#valGridViewItem").html("");
            $("#valtxtVoucherTx_Narration").html("");
            if (document.getElementById('<%= txtVoucherTx_No.ClientID%>').value.trim() == "") {
                msg += "Enter Voucher/Bill No. \n";
                $("#valtxtVoucherTx_No").html("Enter Voucher/Bill No");
            }
            //   var rowscount = $("#<%=GridViewItem.ClientID %> tr").length;
            //   if (rowscount == "1") {
            //       msg += "Enter Item Detail. \n";
            //       $("#valGridViewItem").html("Enter Item Detail");
            //   }

            if (document.getElementById('<%= txtVoucherTx_Narration.ClientID%>').value.trim() == "") {
                msg += "Enter Narration. \n";
                $("#valtxtVoucherTx_Narration").html("Enter Narration");
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
        //ON Selecting AgstRef


        // onkeypress="return validateDecUnit(this,event)"
        function allowNegativeNumber(e) {
            var charCode = (e.which) ? e.which : event.keyCode
            if (charCode > 31 && (charCode < 45 || charCode > 57)) {
                return false;
            }
            return true;

        }
        function validateDecUnit(el, evt) {
            var digit = document.getElementById('<%=lblUnit.ClientID%>').innerText;
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (digit == 0 && charCode == 46) {
                return false;
            }

            var number = el.value.split('.');
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            //just one dot (thanks ddlab)
            if (number.length > 1 && charCode == 46) {
                return false;
            }
            //get the carat position
            var caratPos = getSelectionStart(el);
            var dotPos = el.value.indexOf(".");
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > digit - 1)) {
                return false;
            }
            return true;
        }

        function getSelectionStart(o) {
            if (o.createTextRange) {
                var r = document.selection.createRange().duplicate()
                r.moveEnd('character', o.value.length)
                if (r.text == '') return o.value.length
                return o.value.lastIndexOf(r.text)
            } else return o.selectionStart
        }
        function validateAmount(sender) {

            var pattern = /^-?[0-9]+(.[0-9]{1,5})?$/;
            var text = sender.value;
            if (text != "") {
                if (text.match(pattern) == null) {
                    alert('the format is wrong');
                    sender.value = "0";
                    CalculateGrandTotal();
                }
                else {
                    CalculateGrandTotal();
                }
            }
            else {
                sender.value = "0";
            }


        }
        $(document).ready(function () {

            hideshowitempanel();

        });
        function hideshowitempanel() {

            var checkitem = document.getElementById("chkitem").checked;
            if (checkitem == true) {
                document.getElementById('<%=btnAdd.ClientID%>').disabled = false;
            }
            else {
                document.getElementById('<%=btnAdd.ClientID%>').disabled = true;
            }
        }

        $("#<%=txtQuantity.ClientID %>").on('change', function () {

            $.ajax({

                url: '<%=ResolveUrl("VoucherSaleCredit.aspx/Availablestock") %>',
                data: "{ 'Qauntity': '" + $('#txtQuantity').val() + "', 'OfficeID': '" + '<%=hfofficeID.Value%>' + "', 'ItemID': '" + $('#ddlItemName').val() + "', 'WarehouseID': '" + '0' + "'}",
                //  var param = { ItemName: $('#txtItem').val() };
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    if (data.d != "")
                        alert(data.d)
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        });

    </script>

</asp:Content>
