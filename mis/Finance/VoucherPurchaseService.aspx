<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VoucherPurchaseService.aspx.cs" Inherits="mis_Finance_VoucherPurchaseService" %>

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
                    <div class="box box-success" style="background-color: #faf4c7">
                        <div class="box-header">
                            <h3 class="box-title">Gst Service Purchase Voucher</h3>
                            <asp:LinkButton ID="lbkbtnAddLedger" class="btn btn-primary pull-right" runat="server" OnClick="lbkbtnAddLedger_Click">Add Ledger</asp:LinkButton>
                             <asp:LinkButton ID="btnTDSView" runat="server" CssClass="btn btn-primary pull-right" OnClick="btnTDSView_Click" >View TDS</asp:LinkButton>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
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
                                        <label>Voucher No.<span style="color: red;"> *</span></label>
                                        <div class="form-group">
                                            <div class="col-md-6">
                                                <asp:Label ID="lblVoucherNo" runat="server" Visible="false" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                                <asp:Label ID="lblVoucherTx_No" runat="server" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                            </div>
                                            <div class="col-md-6" style="margin-left: -32px;">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVoucherTx_No" placeholder="Enter Voucher/Bill No." ClientIDMode="Static" MaxLength="6" autocomplete="off"></asp:TextBox>
                                                <small><span id="valtxtVoucherTx_No" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Supplier's Invoice No.</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtInvoice" placeholder="Enter Supplier's Invoice No." MaxLength="16" autocomplete="off" onchange="FillNarration();"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Supplier's Invoice Date<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtSupplierInvoiceDate" runat="server" CssClass="form-control DateAdd" data-date-end-date="0d" onchange="FillNarration(),CompareSupplierInvocieDate();" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtVoucherTx_Date" runat="server" data-date-end-date="0d" CssClass="form-control DateAdd" placeholder="Enter Voucher No..." autocomplete="off" OnTextChanged="txtVoucherTx_Date_TextChanged" AutoPostBack="true" onchange="CompareSupplierInvocieDate();"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Ledger Name (CR)<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlPartyName" CssClass="form-control select1 select2" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged" onchange="FillNarration();">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtPartyName" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Current Balance<span style="color: red;"> *</span></label>
                                            <asp:Label ID="txtCurrentBalance" runat="server" Text="" CssClass="form-control" Style="background-color: #eee;" autocomplete="off"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <asp:Panel ID="itempanel" runat="server">
                                    <fieldset>
                                        <legend>Supplier Detail</legend>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Supplier Name</label>
                                                    <asp:TextBox ID="txtSuplierName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Supplier Address</label>
                                                    <asp:TextBox ID="txtsupplieraddress" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>State</label>
                                                    <asp:DropDownList runat="server" autocomplete="off" CssClass="form-control" ID="ddlState">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <small><span id="valddlState" style="color: red;"></span></small>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>City </label>
                                                    <asp:TextBox runat="server" autocomplete="off" placeholder="Enter City" ID="txtCity" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                    <small><span id="valtxtCity" style="color: red;"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Registration Types</label>
                                                    <asp:DropDownList runat="server" autocomplete="off" CssClass="form-control" ID="ddlRegistrationType">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem>Composition</asp:ListItem>
                                                        <asp:ListItem>Consumer</asp:ListItem>
                                                        <asp:ListItem>Regular</asp:ListItem>
                                                        <asp:ListItem>Unregistered</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <small><span id="valddlRegistrationType" style="color: red;"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-3" id="gstno" runat="server" visible="true">
                                                <div class="form-group">
                                                    <label>GST No.(CAPITAL LETTERS ONLY)<span style="color: red;" id="gstvisible" runat="server" visible="false">*</span></label>
                                                    <asp:TextBox runat="server" autocomplete="off" placeholder="Enter GST No." ID="txtGSTNo" ClientIDMode="Static" CssClass="form-control GSTNo" MaxLength="15"></asp:TextBox>
                                                    <small><span id="valtxtGSTNo" style="color: red;"></span></small>
                                                </div>

                                            </div>
                                        </div>
                                    </fieldset>
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-md-6"></div>
                                    <div class="col-md-6">
                                        <fieldset>
                                            <legend>Amount Detail (DR)                                               
                                            </legend>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div id="divamount" runat="server">
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group">
                                                                    <label>Ledger</label>
                                                                    <asp:DropDownList ID="ddlLedger" runat="server" CssClass="form-control select2">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group">
                                                                    <label>Amount</label>
                                                                    <asp:TextBox ID="txtLedgerAmt" runat="server" MaxLength="12" CssClass="form-control" autocomplete="off" onblur="return validateAmount(this);" onkeypress="return allowNegativeNumber(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>&nbsp;</label>
                                                                    <asp:Button ID="btnAddLedgerAmt" runat="server" CssClass="btn btn-block" Text="Add" OnClick="btnAddLedgerAmt_Click" OnClientClick="return ValidateAddLedger()" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-- Ledger/Amount Detail GridView-->
                                                    <asp:GridView ID="GridViewLedger" runat="server" DataKeyNames="LedgerID" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" ShowHeader="false" OnRowDeleting="GridViewLedger_RowDeleting">
                                                        <Columns>
                                                            <%-- <asp:TemplateField HeaderText="Action" ShowHeader="False"  ItemStyle-Width="50">                                                                                                                        
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" Visible='<%# Eval("Status").ToString() =="1" ? true:false %>' runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Ledger will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Ledger" ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="Delete" Visible='<%# Eval("Status").ToString() =="1" ? true:false %>' runat="server" CssClass="label " CausesValidation="False" CommandName="Delete" Style="color: red;" Text="" OnClientClick="return confirm('The Ledger will be deleted. Are you sure want to continue?');"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                    <asp:Label ID="lblLedgerName" CssClass="paddingLR" runat="server" Text='<%# Eval("LedgerName").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("LedgerID").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblHSNCode" CssClass="hidden" runat="server" Text='<%# Eval("HSN_Code").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblCGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("CGST_Per").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblSGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("SGST_Per").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblIGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("IGST_Per").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblCGSTAmt" CssClass="hidden" runat="server" Text='<%# Eval("CGSTAmt").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblSGSTAmt" CssClass="hidden" runat="server" Text='<%# Eval("SGSTAmt").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblIGSTAmt" CssClass="hidden" runat="server" Text='<%# Eval("IGSTAmt").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblrcm" CssClass="hidden" runat="server" Text='<%# Eval("Isreversechargeapplicable").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblGSTApplicable" CssClass="hidden" runat="server" Text='<%# Eval("GSTApplicable").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblneligibleforinputcredit" CssClass="hidden" runat="server" Text='<%# Eval("IsIneligibleforinputcredit").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblTaxbility" CssClass="hidden" runat="server" Text='<%# Eval("Taxbility").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" ShowHeader="False" ItemStyle-Width="50%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control AlignR" Style="padding: 3px;" MaxLength="12" Text='<%# Eval("Amount").ToString()%>' onblur="return validateAmount(this);" onkeypress="return allowNegativeNumber(event);" onfocusout="return validateAmount(this);"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                        </Columns>
                                                    </asp:GridView>
                                                    <!-- End-->
                                                    <hr />
                                                    <span style="color: red">Suggested Round Off:  </span>
                                                    <asp:Label ID="lblroundsuggestion" runat="server" ClientIDMode="Static" Text=""></asp:Label><br />
                                                    <table class="table table-bordered customCSS">
                                                        <tr>
                                                            <th>GRAND TOTAL :
                                                            </th>
                                                            <th style="width: 50%; padding: 3px;">
                                                                <asp:TextBox ID="lblGrandTotal" ClientIDMode="Static" CssClass="form-control AlignR" Style="padding: 3px;" runat="server" Text="0" autocomplete="off"></asp:TextBox>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                                <asp:Panel ID="BillByBillDetail" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="GridViewBillByBillDetail" runat="server" ShowHeaderWhenEmpty="true" DataKeyNames="RID" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Type of Ref" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTypeOfRef" CssClass="paddingLR" runat="server" Text='<%# Eval("BillByBillTx_RefType").ToString()%>'></asp:Label>
                                                            <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("RID").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>

                                                            <asp:Label ID="lblRefNo" CssClass="paddingLR" runat="server" Text='<%# Eval("BillByBillTx_Ref").ToString()%>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount" SortExpression="leftBonus" ItemStyle-Width="15%" HeaderStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("BillByBillTx_Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cr/Dr" SortExpression="leftBonus" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblType" runat="server" Text='<%# Bind("BillByBillTxType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlChequeDetail" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GridViewChequeDetail" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                            <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("RID").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque/ DD No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblChequeTx_No" runat="server" Text='<%# Eval("ChequeTx_No") %>'>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque/ DD Date.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblChequeTx_Date" runat="server" Text='<%# Eval("ChequeTx_Date") %>'>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ChequeTx_Amount." HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblChequeTx_Amount" runat="server" Text='<%# Eval("ChequeTx_Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Narration<span style="color: red;"> *</span></label>
                                            <asp:TextBox ID="txtVoucherTx_Narration" runat="server" TextMode="MultiLine" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <asp:Button runat="server" CssClass="hidden" ID="btnNarration" OnClick="btnNarration_Click" AccessKey="R" />
                                </div>

                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnAccept" Text="Accept" OnClick="btnAccept_Click" OnClientClick="return validateSubmit();" autocomplete="off" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <a href="VoucherPurchaseService.aspx" id="btn_Clear" runat="server" class="btn btn-block btn-default">Clear</a>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>


            </div>

            <!-- Start Add BillByBillDetail Modal-->
            <div id="ModalReferance" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Bill-wise Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Type of Ref<span style="color: red;"> *</span></label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRefType" OnSelectedIndexChanged="ddlRefType_SelectedIndexChanged" AutoPostBack="true">
                                            <%--<asp:ListItem Value="0">Advance</asp:ListItem>--%>
                                            <asp:ListItem Value="2">New Ref</asp:ListItem>
                                            <asp:ListItem Value="1">Agst Ref</asp:ListItem>

                                            <%--<asp:ListItem Value="3">On Account</asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <small><span id="valddlRefType" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Name<span style="color: red;">*</span></label>
                                        <asp:HyperLink ID="lnkView" onclick="ShowRefDetailModal();" Visible="false" runat="server">View AgstRef</asp:HyperLink>
                                        <asp:TextBox runat="server" ID="txtBillByBillTx_Ref" ClientIDMode="Static" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                        <asp:DropDownList runat="server" CssClass="form-control select2" Visible="false" ID="ddlBillByBillTx_Ref" onchange="ChangeRef()">
                                        </asp:DropDownList>
                                        <small><span id="valddlBillByBillTx_Ref" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Amount<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" ID="txtBillByBillTx_Amount" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event);" MaxLength="12" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                        <small><span id="valtxtBillByBillTx_Amount" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Cr/Dr<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlBillByBillTx_crdr" CssClass="form-control" ClientIDMode="Static" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="Cr">Credit</asp:ListItem>
                                            <asp:ListItem Value="Dr">Debit</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlBillByBillTx_crdr" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" Text="Add" ID="btnAddBillByBill" ClientIDMode="Static" CssClass="btn btn-block btn-default" OnClick="btnAddBillByBill_Click" OnClientClick="return validateBillByBill();"></asp:Button>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="GridViewRef" runat="server" DataKeyNames="RID" ShowHeaderWhenEmpty="true" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" OnRowCommand="GridViewRef_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Type of Ref" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblTypeOfRef" CssClass="paddingLR" runat="server" Text='<%# Eval("BillByBillTx_RefType").ToString()%>'></asp:Label>
                                                    <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("RID").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblRefNo" CssClass="paddingLR" runat="server" Text='<%# Eval("BillByBillTx_Ref").ToString()%>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount" SortExpression="leftBonus" ItemStyle-Width="15%" HeaderStyle-Width="15%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("BillByBillTx_Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cr/Dr" HeaderStyle-Width="10%" ItemStyle-Width="10%" SortExpression="leftBonus">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType" runat="server" Text='<%# Bind("BillByBillTxType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtn" runat="server" CommandName="BillByBillDelete" CommandArgument='<%# Eval("RID") %>'><i class="fa fa-trash"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <table class="table table-bordered hidden">
                                        <tr>
                                            <th style="width: 50%;">VOUCHER GRAND TOTAL :&nbsp;
                                                   <%-- <asp:Label ID="lblGrandTotal" ClientIDMode="Static" runat="server" Text="0"></asp:Label>--%>
                                            </th>

                                            <th>REF. TOTAL :&nbsp;
                                                    <asp:Label ID="lblRefTotal" ClientIDMode="Static" runat="server" Text="0"></asp:Label>
                                            </th>
                                        </tr>

                                    </table>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <%--<asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSubmit" Text="Final Submit" OnClick="btnSubmit_Click" Visible="false" OnClientClick="return validateSubmit();" />--%>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>

                </div>
            </div>
            <!-- End-->

            <%--Start ViewAgstRefDetail Modal--%>
            <div class="modal fade" id="AgstRefModal" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Against Ref Details </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GridViewRefDetail" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="VoucherTx_Date" HeaderText="Date" ItemStyle-Width="10%" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="BillByBillTx_Ref" HeaderText="Name" />
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="20%" />

                                        </Columns>
                                    </asp:GridView>

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
            <!-- End-->

            <!-- Start Add ChequeDetail Modal-->
            <div class="modal fade" id="ModalChequeDetail" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Cheque-wise Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cheque/ DD No.</label>
                                        <asp:TextBox ID="txtChequeTx_No" onkeypress="return validateNum(event);" runat="server" placeholder="Enter Cheque/ DD No." MaxLength="6" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                        <small><span id="valtxtChequeTx_No" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cheque/ DD Date</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtChequeTx_Date" placeholder="DD/MM/YYYY" data-date-start-date="-89d" autocomplete="off"></asp:TextBox>
                                        </div>
                                        <small><span id="valtxtChequeTx_Date" style="color: red;"></span></small>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Amount<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtChequeTx_Amount" placeholder="Enter Amount" MaxLength="12" onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                        <small><span id="valtxtChequeTx_Amount" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" Text="Add" CssClass="btn btn-block btn-default" ID="btnAddCheque" OnClick="btnAddCheque_Click" OnClientClick="return validateCheque();"></asp:Button>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" CssClass="table table-bordered" DataKeyNames="RID" ShowHeaderWhenEmpty="true" ID="GVFinChequeTx" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                        <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("RID").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cheque/ DD No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChequeTx_No" runat="server" Text='<%# Eval("ChequeTx_No") %>'>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cheque/ DD Date.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChequeTx_Date" runat="server" Text='<%# Eval("ChequeTx_Date") %>'>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ChequeTx_Amount." HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChequeTx_Amount" runat="server" Text='<%# Eval("ChequeTx_Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <%-- <asp:Button runat="server" Text="Add" ID="btnAddChequeDetail" ClientIDMode="Static" CssClass="btn btn-success" OnClick="btnAddChequeDetail_Click"></asp:Button>--%>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End-->

            <!-- Start View ChequeDetail Modal-->
            <div class="modal fade" id="ModalChequeDetailView" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Cheque Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GVViewFinChequeTx" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ChequeTx_No" HeaderText="Cheque/ DD No." />
                                                <asp:BoundField DataField="ChequeTx_Date" HeaderText="Cheque/ DD Date" />
                                                <asp:BoundField DataField="ChequeTx_Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>


                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End-->

        </section>
    </div>
    <div class="modal fade" id="TdsGST" role="dialog" data-backdrop="false">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">TDS-GST (Cr) Entry </h4>
                    <asp:Label ID="lblGSTModal" runat="server"></asp:Label>
                    <asp:HyperLink ID="HL_AddLEdger" runat="server" class="btn btn-primary pull-right" Target="_blank">Add Ledger</asp:HyperLink>
                    &nbsp; &nbsp;
                      <asp:LinkButton ID="btnRefreshLedger" class="btn btn-primary pull-right" runat="server" OnClick="btnRefreshLedger_Click">Refresh Ledger</asp:LinkButton>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Party Name<span style="color: red;"> *</span></label>
                                <asp:DropDownList runat="server" CssClass="form-control select2" ID="ddlPartyNameTDS" OnSelectedIndexChanged="ddlPartyNameTDS_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>GST No.<span style="color: red;">*</span></label>
                                <asp:TextBox runat="server" ID="txtTdsGSTNo" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateNum(event);" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Bill No<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" ID="txtBillNo" MaxLength="16" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateusername(event);" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Bill Date<span style="color: red;"> *</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtBillDate" data-date-end-date="0d" placeholder="DD/MM/YYYY" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Total Bill Amt<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtTdsBillAmount" MaxLength="12" onkeypress="return validateDec(this,event);" placeholder="Total Bill Amount" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="pymtamt" runat="server" ToolTip="Payment Amount Need to be Done"><b>Payment Amt</b><span style="color: red;"> *</span></asp:Label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtPaymentAmountNTBD" MaxLength="12" placeholder="Total Bill Amount" onkeypress="return validateDec(this,event);" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Basic Amt<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtBasicAmount" MaxLength="12" placeholder="Total Bill Amount" onkeypress="return validateDec(this,event);" onchange="CalculateGSTTDSAmt();" autocomplete="off" OnTextChanged="txtBasicAmount_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>GST TDS Amt<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtGSTTDSAmount" MaxLength="12" onkeypress="return validateDec(this,event);" placeholder="Total Bill Amount" autocomplete="off" OnTextChanged="txtGSTTDSAmount_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>CGST<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCGST" placeholder="CGST" onkeypress="return validateDec(this,event);" MaxLength="12" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>SGST<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtSGST" onkeypress="return validateDec(this,event);" placeholder="SGST" MaxLength="12" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>IGST<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtIGST" onkeypress="return validateDec(this,event);" placeholder="IGST" MaxLength="12" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button runat="server" Text="Add" ID="btnTDSSave" ClientIDMode="Static" CssClass="btn btn-block btn-success" OnClick="btnTDSSave_Click" OnClientClick="return validateTDS();"></asp:Button>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="gvTdsGSTDetail" AutoGenerateColumns="false" OnRowCommand="gvTdsGSTDetail_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.no">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrow" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedger_ID" runat="server" Text='<%# Bind("Ledger_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ledger Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedger_Name" runat="server" Text='<%# Bind("Ledger_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GST No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGSTNo" runat="server" Text='<%# Bind("GSTNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillNo" runat="server" Text='<%# Bind("BillNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillDate" runat="server" Text='<%# Bind("BillDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Bill Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalBillAmount" runat="server" Text='<%# Bind("TotalBillAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaymentAmount" runat="server" Text='<%# Bind("PaymentAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Basic Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBasicAmount" runat="server" Text='<%# Bind("BasicAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GST TDS Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGSTTDSAmount" runat="server" Text='<%# Bind("GSTTDSAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGST">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCGST" runat="server" Text='<%# Bind("CGST") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SGST">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSGST" runat="server" Text='<%# Bind("SGST") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGST">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIGST" runat="server" Text='<%# Bind("IGST") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtn" runat="server" CommandName="TDSGSTDelete" CommandArgument='<%# Eval("RowNo") %>'><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-md-2 pull-right">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button runat="server" Text="Final Submit" ID="btnFSubmit" ClientIDMode="Static" CssClass="btn btn-block btn-warning" OnClick="btnFSubmit_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="TdsGSTDetail" role="dialog" data-backdrop="false">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">TDS-GST (Cr) Details </h4>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="gvTDSDetail" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.no">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ledger Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedger_Name" runat="server" Text='<%# Bind("Ledger_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GST No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGSTNo" runat="server" Text='<%# Bind("GSTNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillNo" runat="server" Text='<%# Bind("BillNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillDate" runat="server" Text='<%# Bind("BillDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Bill Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalBillAmount" runat="server" Text='<%# Bind("TotalBillAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payment Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaymentAmount" runat="server" Text='<%# Bind("PaymentAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Basic Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBasicAmount" runat="server" Text='<%# Bind("BasicAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GST TDS Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGSTTDSAmount" runat="server" Text='<%# Bind("GSTTDSAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGST">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCGST" runat="server" Text='<%# Bind("CGST") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SGST">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSGST" runat="server" Text='<%# Bind("SGST") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGST">
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
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ShowRefDetailModal() {
            $('#AgstRefModal').modal('show');

        }

        function ShowModal() {
            $("#ModalItemDetail").modal();
        }
        function ShowReferanceModal() {
            $("#ModalReferance").modal();
        }
        function ParticularDetailViewModal() {
            $('#ParticularDetailViewModal').modal('show');
        }
        function ShowModalChequeDetail() {
            $('#ModalChequeDetail').modal('show');
        }
        function ShowModalChequeDetailView() {
            $('#ModalChequeDetailView').modal('show');
        }
       <%-- function CalculateAmount() {
            var Quantity = document.getElementById('<%=txtQuantity.ClientID%>').value.trim();
            var Rate = document.getElementById('<%=txtRate.ClientID%>').value.trim();
            if (Quantity == "")
                Quantity = "0";
            if (Rate == "")
                Rate = "0";

            document.getElementById('<%=txtAmount.ClientID%>').value = (Quantity * Rate).toFixed(2);
            // CalculateGrandTotal();
        }--%>
        //$(document).ready(function () {
        //    CalculateGrandTotal();
        //});

        function CalculateGrandTotal() {
            debugger;
            var i = 0;
            var Tval = 0;

            $('#GridViewItem tr').each(function (index) {
                if (i > 0) {
                    var temp = Tval;
                    var val = $(this).children("td").eq(4).find('input[type="text"]').val();

                    if (val == "")
                        val = 0;

                    Tval = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)
                }
                i++;
            });
            $('#GridViewLedger tr').each(function (index) {
                // if (i > 0) {
                var temp = Tval;
                var val = $(this).children("td").eq(1).find('input[type="text"]').val();

                if (val == "")
                    val = 0;

                Tval = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)
                // }
                i++;
            });

            document.getElementById('<%=lblGrandTotal.ClientID%>').value = Tval;
            document.getElementById('<%=lblroundsuggestion.ClientID%>').innerText = (Math.round(Tval) - Tval).toFixed(2);
            // document.getElementById('<%=lblGrandTotal.ClientID%>').innerHTML = Tval;

        }

        function validateBillByBill() {
            var msg = "";
            $("#valddlBillByBillTx_Ref").html("");
            $("#valtxtBillByBillTx_Amount").html("");
            if (document.getElementById('<%=ddlRefType.ClientID%>').selectedIndex == 1) {
                if (document.getElementById('<%=ddlBillByBillTx_Ref.ClientID%>').selectedIndex == 0) {
                    msg += "Select Name \n";
                    $("#valddlBillByBillTx_Ref").html("Select Name");
                }
            }
            else if (document.getElementById('<%=ddlRefType.ClientID%>').selectedIndex == 0) {
                if (document.getElementById('<%=txtBillByBillTx_Ref.ClientID%>').value.trim() == "") {
                    msg += "Enter Name \n";
                    $("#valddlBillByBillTx_Ref").html("Enter Name");
                }

            }
            else {
                $("#valddlBillByBillTx_Ref").html("");
            }


        if (document.getElementById('<%=txtBillByBillTx_Amount.ClientID%>').value.trim() == "") {
                msg += "Enter Amount \n";
                $("#valtxtBillByBillTx_Amount").html("Enter Amount");
            }
            if (document.getElementById('<%=txtBillByBillTx_Amount.ClientID%>').value.trim() != "") {
                var amt = document.getElementById('<%=txtBillByBillTx_Amount.ClientID%>').value.trim();
                if (parseFloat(amt) == 0) {
                    msg += "Amount Cannot Be zero.\n";
                    $("#valtxtBillByBillTx_Amount").html("Amount Cannot Be zero.");
                }

            }
            if (document.getElementById('<%=ddlBillByBillTx_crdr.ClientID%>').selectedIndex == 0) {
                msg += "Select Cr/Dr \n";
                $("#valddlBillByBillTx_crdr").html("Select Cr/Dr");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {

                return true;

            }
        }

        function validateCheque() {
            debugger;
            var msg = "";
            $("#valtxtChequeTx_No").html("");
            //$("#valtxtChequeTx_Date").html("");
            $("#valtxtChequeTx_Amount").html("");
            var CheckNo = document.getElementById('<%=txtChequeTx_No.ClientID%>').value.trim();
            if (document.getElementById('<%=txtChequeTx_No.ClientID%>').value.trim() != "") {
                if (CheckNo.length != 6) {
                    msg += "Enter 6 Digit Cheque/ DD No.  \n";
                    $("#valtxtChequeTx_No").html("Enter  6 Digit Cheque/ DD No");
                }
            }

            if (document.getElementById('<%=txtChequeTx_Amount.ClientID%>').value.trim() == "") {
                msg += "Enter Amount. \n";
                $("#valtxtChequeTx_Amount").html("Enter Amount");
            }
            if (document.getElementById('<%=txtChequeTx_Amount.ClientID%>').value.trim() != "") {
                var amt = document.getElementById('<%=txtChequeTx_Amount.ClientID%>').value.trim();
                if (parseFloat(amt) == 0) {
                    msg += "Amount cannot be Zero.\n";
                    $("#valtxtChequeTx_Amount").html("Amount cannot be Zero.");
                }
            }
            if (document.getElementById('<%=txtChequeTx_Amount.ClientID%>').value.trim() != "") {

                var LedgerAmount = parseFloat(document.getElementById('<%=lblGrandTotal.ClientID%>').value);
                var ChequeAmount = parseFloat(document.getElementById('<%=txtChequeTx_Amount.ClientID%>').value);

                var rowscount = $("#<%=GVFinChequeTx.ClientID %> tr").length;
                if (rowscount > "1") {
                    if (ChequeAmount >= LedgerAmount) {

                        msg += "Enter Valid Amount. \n";
                        $("#valtxtChequeTx_Amount").html("Enter Valid Amount");
                    }
                }
                else {
                    if (ChequeAmount > LedgerAmount) {
                        msg += "Enter Valid Amount. \n";
                        $("#valtxtChequeTx_Amount").html("Enter Valid Amount");
                    }
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
        function ValidateAddLedger() {
            var msg = "";
            $("#valddlLedger").html("");
            $("#valtxtLedgerAmt").html("");
            if (document.getElementById('<%=ddlLedger.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Ledger.\n";
                $("#valddlLedger").html("Select Ledger");
            }
            if (document.getElementById('<%=txtLedgerAmt.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Amount.";
                $("#valtxtLedgerAmt").html("Enter Amount");
            }
            if (document.getElementById('<%=txtLedgerAmt.ClientID%>').value.trim() != "") {
                var amt = document.getElementById('<%=txtLedgerAmt.ClientID%>').value.trim();
                if (parseFloat(amt) == 0) {
                    msg += "Amount cannot be Zero.\n";
                    $("#valtxtLedgerAmt").html("Amount cannot be Zero.");
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

        function validateSubmit() {
            var msg = "";
            debugger;
            var VoucherGrandTotal = document.getElementById('<%=lblGrandTotal.ClientID%>').value.trim();
            var RefTotal = document.getElementById('<%=lblRefTotal.ClientID%>').innerHTML;

            if (document.getElementById('<%=txtVoucherTx_No.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Voucher/Bill No. \n";
            }
            if (document.getElementById('<%=txtSupplierInvoiceDate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter  Supplier's Invoice Date. \n";
            }
            if (document.getElementById('<%=txtVoucherTx_Date.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Date. \n";
            }
            if (document.getElementById('<%=ddlPartyName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Party A/c Name. \n";
            }
            if (document.getElementById('<%=ddlRegistrationType.ClientID%>').selectedIndex > 0) {
                if (document.getElementById('<%=ddlRegistrationType.ClientID%>').value.trim() == "Composition" || document.getElementById('<%=ddlRegistrationType.ClientID%>').value.trim() == "Regular") {
                    //document.getElementById('gstvisible').style.display = 'block';
                    if (document.getElementById('<%=txtGSTNo.ClientID%>').value.trim() == "") {
                        msg = msg + "Enter GST No. \n";
                        $("#valtxtGSTNo").html("Enter GST No");
                    }
                }
            }
            if (document.getElementById('<%=txtVoucherTx_Narration.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Narration. \n";
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
        $('.GSTNo').blur(function () {

            //var reg = /^(d{2}[A-Z]{5}\d{4}[A-Z]{1}[A-Z\d]{1}[Z]{1}[A-Z\d]{1})$/;
            //var reg = /^(\d{2})([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})(\d{1})([a-zA-Z]{1})(\d{1})$/;
            var reg = /^(\d{2})([A-Z]{5})(\d{4})([A-Z]{1})([0-9A-Z]{1})([Z]{1})([0-9A-Z]{1})$/;
            if (document.getElementById('txtGSTNo').value != "") {
                if (reg.test(document.getElementById('txtGSTNo').value) == false) {
                    alert("Invalid GST Number.");
                    document.getElementById('txtGSTNo').value = "";
                }
            }

        });
        function getSelectionStart(o) {
            if (o.createTextRange) {
                var r = document.selection.createRange().duplicate()
                r.moveEnd('character', o.value.length)
                if (r.text == '') return o.value.length
                return o.value.lastIndexOf(r.text)
            } else return o.selectionStart
        }
        function allowNegativeNumber(e) {
            var charCode = (e.which) ? e.which : event.keyCode
            if (charCode > 31 && (charCode < 45 || charCode > 57)) {
                return false;
            }
            return true;

        }
        function validateAmount(sender) {

            var pattern = /^-?[0-9]+(.[0-9]{1,2})?$/;
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
        function FillNarration() {
            debugger;
            var partyselect = document.getElementById('<%= ddlPartyName.ClientID%>');
            var PartyName = partyselect.options[partyselect.selectedIndex].text;
            var SupplierInvoice = document.getElementById('<%= txtInvoice.ClientID%>').value.trim();
            var SupplierInvoiceDate = document.getElementById('<%= txtSupplierInvoiceDate.ClientID%>').value.trim();
            if (PartyName == "Select") {
                var PartyName = "";
            }
            if (SupplierInvoice == "") {
                SupplierInvoice = "NA";
            }
            var Narration = PartyName + ' ' + 'Supplier’s Invoice No - ' + SupplierInvoice + ' ' + 'Supplier’s Invoice Date -' + SupplierInvoiceDate;
            document.getElementById('<%= txtVoucherTx_Narration.ClientID%>').value = Narration;
        }
        function CompareSupplierInvocieDate() {
            var VoucherDate = document.getElementById('<%= txtVoucherTx_Date.ClientID%>').value.trim();
            if (VoucherDate != "") {
                var dateParts = VoucherDate.split("/");   //Will split in 3 parts: day, month and year
                var yday = dateParts[0];
                var ymonth = dateParts[1];
                var yyear = dateParts[2];
                var xd = new Date(yyear, parseInt(ymonth, 10) - 1, yday);
            }
            else {

                var xd = "";
            }
            var SupplierInvoiceDate = document.getElementById('<%= txtSupplierInvoiceDate.ClientID%>').value.trim();
            if (SupplierInvoiceDate != "") {
                var dateParts = SupplierInvoiceDate.split("/");   //Will split in 3 parts: day, month and year
                var yday = dateParts[0];
                var ymonth = dateParts[1];
                var yyear = dateParts[2];
                var yd = new Date(yyear, parseInt(ymonth, 10) - 1, yday);
            }
            else {

                var yd = "";
            }
            if (yd > xd) {
                alert("Supplier Invoice Date should be smaller than Voucher Date");
                document.getElementById('<%= txtSupplierInvoiceDate.ClientID%>').value = "";
            }


        }
        //ON Selecting AgstRef
        function ChangeRef() {
            debugger;
            var billbybillrefSelect = document.getElementById('<%=ddlBillByBillTx_Ref.ClientID%>');
            var selectedText = billbybillrefSelect.options[billbybillrefSelect.selectedIndex].text;
            var fields = selectedText.split('[');
            var value = fields[1].split(' ');
            var BillByBillAmount = value[1]
            $("#txtBillByBillTx_Amount").val(BillByBillAmount);
            <%--document.getElementById('<%=txtBillByBillTx_Amount.ClientID%>').value() = BillByBillAmount;--%>
            if (value[2] == "Dr") {

                $("#ddlBillByBillTx_crdr").val("Cr");

            }
            else {

                $("#ddlBillByBillTx_crdr").val("Dr");
            }

        }
         <%--  TDS DETAILS --%>
        function ShowTDSDetailModal() {
            $('#TdsGST').modal('show');
        }

        function ShowTDSDetail() {
            $('#TdsGSTDetail').modal('show');
        }
        function CalculateGSTTDSAmt() {
            var BasicAmt = document.getElementById('<%=txtBasicAmount.ClientID%>').value;
            var GSTTDS = (parseFloat(BasicAmt) * 2) / 100;
            document.getElementById('<%=txtGSTTDSAmount.ClientID%>').value = parseFloat(GSTTDS).toFixed(2);
        }
        function validateTDS() {
            var msg = "";
            //$("#valddlBillByBillTx_Ref").html("");
            //$("#valtxtBillByBillTx_Amount").html("");
            if (document.getElementById('<%=ddlPartyName.ClientID%>').selectedIndex == 0) {
                msg += "Select Party Name. \n";
            }
            if (document.getElementById('<%=txtBillNo.ClientID%>').value.trim() == "") {
                msg += "Enter Bill No \n";
            }
            if (document.getElementById('<%=txtBillDate.ClientID%>').value.trim() == "") {
                msg += "Select Bill Date.\n";
            }
            if (document.getElementById('<%=txtTdsBillAmount.ClientID%>').value.trim() == "") {
                msg += "Enter Total Bill Amount.  \n";
            }
            if (document.getElementById('<%=txtPaymentAmountNTBD.ClientID%>').value.trim() == "") {
                msg += "Enter Payment Amount. \n";
            }
            if (document.getElementById('<%=txtBasicAmount.ClientID%>').value.trim() == "") {
                msg += "Enter Basic Amount. \n";
            }
            if (document.getElementById('<%=txtGSTTDSAmount.ClientID%>').value.trim() == "") {
                msg += "Enter GST TDS Amount. \n";
            }
       <%-- if (document.getElementById('<%=txtCGST.ClientID%>').value.trim() == "") {
            msg += "Enter CGST Amount. \n";
        }
        if (document.getElementById('<%=txtSGST.ClientID%>').value.trim() == "") {
            msg += "Enter CGST Amount. \n";
        }
        if (document.getElementById('<%=txtIGST.ClientID%>').value.trim() == "") {
            msg += "Enter CGST Amount. \n";
        }--%>
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {

                return true;

            }
        }
    </script>
</asp:Content>

