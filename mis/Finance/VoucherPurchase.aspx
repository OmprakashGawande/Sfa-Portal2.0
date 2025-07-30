<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VoucherPurchase.aspx.cs" Inherits="mis_Finance_PurchaseVoucher" %>

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
                            <h3 class="box-title">Gst 
                                <asp:Label ID="lblVoucherType" runat="server" Text=""></asp:Label>
                                Purchase Voucher</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Voucher No.<span style="color: red;"> *</span></label>
                                        <asp:TextBox ID="txtVoucherTx_No" runat="server" CssClass="form-control" placeholder="Enter Voucher No..." MaxLength="50"></asp:TextBox>
                                        <small><span id="valtxtVoucherTx_No" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Supplier / Invoice No.<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtInvoice" placeholder="Enter Voucher No..." MaxLength="50"></asp:TextBox>
                                        <small><span id="valtxtInvoice" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Date<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtVoucherTx_Date" runat="server" CssClass="form-control DateAdd" placeholder="Enter Voucher No..." autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Party A/c Name<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlPartyName" CssClass="form-control select2" runat="server" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlPartyName_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <small><span id="valddlPartyName" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Current Balance<span style="color: red;"> *</span></label>
                                        <asp:Label ID="txtCurrentBalance" runat="server" Text="" CssClass="form-control" Style="background-color: #eee;"></asp:Label>

                                    </div>
                                </div>
                            </div>


                            <fieldset>
                                <legend>Item Detail</legend>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:LinkButton ID="btnAdd" class="label-success" Style="float: right; padding: 4px 15px; border-radius: 3px;" runat="server" OnClick="btnAdd_Click" OnClientClick="return ValidateItemAdd();">Add</asp:LinkButton>
                                            <asp:GridView ID="GridViewItem" runat="server" DataKeyNames="ItemID" ClientIDMode="Static" class="table table-bordered customCSS" Style="margin-bottom: 0px;" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnRowDeleting="GridViewItem_RowDeleting">
                                                <Columns>
                                                    <%--<asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Item will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Name Of Item [Default Ledger]">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Delete" runat="server" CssClass="label" CausesValidation="False" CommandName="Delete" Text="" OnClientClick="return confirm('The Item will be deleted. Are you sure want to continue?');"><img src="../image/Del.png" /></asp:LinkButton>
                                                            <asp:Label ID="lblItem" CssClass="paddingLR" runat="server" Text='<%# Eval("Item").ToString()%>'></asp:Label>
                                                            <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("ID").ToString()%>'></asp:Label>
                                                            <asp:Label ID="lblItemID" CssClass="hidden" runat="server" Text='<%# Eval("ItemID").ToString()%>'></asp:Label>
                                                            <asp:Label ID="lblUnit_id" CssClass="hidden" runat="server" Text='<%# Eval("Unit_id").ToString()%>'></asp:Label>
                                                            <asp:Label ID="lblWarehouse_id" CssClass="hidden" runat="server" Text='<%# Eval("Warehouse_id").ToString()%>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <HeaderStyle />
                                                        <%-- <ItemStyle HorizontalAlign="Right" />--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" CssClass="paddingLR" runat="server" Text='<%# Eval("Quantity").ToString()%>'></asp:Label>
                                                            <asp:Label ID="lblUnit" CssClass="paddingLR" runat="server" Text='<%# Eval("Unit").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <HeaderStyle />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate" CssClass="paddingLR" runat="server" Text='<%# Eval("Rate").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount ">
                                                        <HeaderStyle />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" CssClass="paddingLR" runat="server" Text='<%# Eval("Amount").ToString()%>'></asp:Label>
                                                            <asp:TextBox ID="txtAmountH" runat="server" CssClass="hidden" Text='<%# Eval("Amount").ToString()%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CGST%" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                        <HeaderStyle />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCGST" CssClass="paddingLR" runat="server" Text='<%# Eval("CGST").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SGST%" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                        <HeaderStyle />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSGST" CssClass="paddingLR" runat="server" Text='<%# Eval("SGST").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Amount" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                                        <HeaderStyle />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <FooterStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalAmount" CssClass="paddingLR" runat="server" Text='<%# Eval("TotalAmount").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="row">
                                <div class="col-md-6"></div>
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>Amount Detail</legend>
                                        <div class="row">

                                            <div class="col-md-12">

                                                <div class="row">
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label>Ledger</label>
                                                            <asp:DropDownList ID="ddlLedger" ClientIDMode="Static" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                            <small><span id="valddlLedger" style="color: red;"></span></small>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label>Amount</label>
                                                            <asp:TextBox ID="txtLedgerAmt" ClientIDMode="Static" runat="server" CssClass="form-control"></asp:TextBox>
                                                            <small><span id="valtxtLedgerAmt" style="color: red;"></span></small>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label>&nbsp;</label>
                                                            <asp:Button ID="btnAddLedgerAmt" runat="server" CssClass="btn btn-block" Text="Add" OnClick="btnAddLedgerAmt_Click" OnClientClick="return ValidateAddLedger()" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <asp:GridView ID="GridViewLedger" runat="server" DataKeyNames="LedgerID" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" ShowHeader="false" OnRowDeleting="GridViewLedger_RowDeleting">
                                                    <Columns>
                                                        <%-- <asp:TemplateField HeaderText="Action" ShowHeader="False"  ItemStyle-Width="50">                                                                                                                        
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" Visible='<%# Eval("Status").ToString() =="1" ? true:false %>' runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Ledger will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Ledger" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" Visible='<%# Eval("Status").ToString() =="1" ? true:false %>' runat="server" CssClass="label " CausesValidation="False" CommandName="Delete" Text="" OnClientClick="return confirm('The Ledger will be deleted. Are you sure want to continue?');"><img src="../image/Del.png" /></asp:LinkButton>
                                                                <asp:Label ID="lblLedgerName" CssClass="paddingLR" runat="server" Text='<%# Eval("LedgerName").ToString()%>'></asp:Label>
                                                                <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("LedgerID").ToString()%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount" ShowHeader="False" ItemStyle-Width="50%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAmount" Enabled='<%# (Eval("LedgerName").ToString() =="CGST" || Eval("LedgerName").ToString() =="SGST") ? false:true %>' runat="server" onblur="CalculateGrandTotal();" CssClass="form-control AlignR" Style="padding: 3px;" Text='<%# Eval("Amount").ToString()%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                    </Columns>
                                                </asp:GridView>

                                                <hr />
                                                <table class="table table-bordered customCSS">
                                                    <tr>
                                                        <th>GRAND TOTAL :
                                                        </th>
                                                        <th style="width: 50%; padding: 3px;">
                                                            <asp:TextBox ID="lblGrandTotal" ClientIDMode="Static" CssClass="form-control AlignR" Style="padding: 3px;" runat="server" Text="0"></asp:TextBox>
                                                        </th>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>

                                    </fieldset>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Narration</label>
                                        <asp:TextBox ID="txtVoucherTx_Narration" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnAccept" Text="Accept" OnClick="btnAccept_Click" OnClientClick="return validateAccept();" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="VoucherPurchase.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="ModalItemDetail" class="modal fade" role="dialog">
                    <div class="modal-dialog modal-lg">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Modal Header</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Name Item<span style="color: red;"> *</span></label><br />
                                            <asp:DropDownList ID="ddlItem" CssClass="form-control select2" Style="width: 100%;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Quantity<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtQuantity" placeholder="Enter Quantity..." onblur="CalculateAmount();" MaxLength="8"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Unit<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtUnitName" placeholder="Enter Unit..."></asp:TextBox>
                                            <asp:Label ID="lblUnitName" CssClass="hidden" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Rate Per<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtRate" placeholder="Enter Rate..." onblur="CalculateAmount();" MaxLength="8"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Amount<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtAmount" placeholder="Enter Amount..." MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Warehouse<span style="color: red;"> *</span></label><br />
                                            <asp:DropDownList ID="ddlWarehouse" CssClass="form-control select2" Style="width: 100%;" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2 hidden">
                                        <div class="form-group">
                                            <asp:Button runat="server" Style="margin-top: 24px;" CssClass="btn btn-block btn-success" ID="btnAddItem" Text="Add Item" OnClick="btnAddItem_Click" />
                                        </div>
                                    </div>
                                </div>

                                <fieldset>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Particulars<span style="color: red;"> *</span></label><br />
                                                <asp:DropDownList ID="ddlParticulars" CssClass="form-control select2" Style="width: 100%;" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Amount<span style="color: red;"> *</span></label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtParticularAmt" placeholder="Enter Amount..." MaxLength="50"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button runat="server" Style="margin-top: 24px;" CssClass="btn btn-block btn-success" ID="btnAddValue" Text="Add Value" OnClick="btnAddValue_Click" OnClientClick="return ValidateItem()" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-8">
                                            <asp:GridView ID="GridViewParticulars" runat="server" DataKeyNames="RID" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" ShowHeader="true" OnRowDeleting="GridViewParticulars_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Particulars ">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="Delete" runat="server" CssClass="label " CausesValidation="False" CommandName="Delete" Text=""><img src="../image/Del.png" /></asp:LinkButton>
                                                            <asp:Label ID="lblParticularsName" CssClass="paddingLR" runat="server" Text='<%# Eval("ParticularName").ToString()%>'></asp:Label>
                                                            <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("RID").ToString()%>'></asp:Label>
                                                            <asp:Label ID="Item_ID" CssClass="hidden" runat="server" Text='<%# Eval("Item_ID").ToString()%>'></asp:Label>
                                                            <asp:Label ID="lblParticularID" CssClass="hidden" runat="server" Text='<%# Eval("ParticularID").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount" ItemStyle-Width="150">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmount" CssClass="form-control paddingLR AlignR" runat="server" Text='<%# Eval("ParticularAmt").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                </fieldset>

                            </div>
                            <div class="modal-footer">
                                <%-- <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                            </div>
                        </div>

                    </div>
                </div>
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
                                                <asp:ListItem Value="0">Advance</asp:ListItem>
                                                <asp:ListItem Value="1">Agst Ref</asp:ListItem>
                                                <asp:ListItem Value="2">New Ref</asp:ListItem>
                                                <asp:ListItem Value="3">On Account</asp:ListItem>
                                            </asp:DropDownList>
                                            <small><span id="valddlRefType" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Name<span style="color: red;">*</span></label>
                                            <asp:TextBox runat="server" ID="txtBillByBillTx_Ref" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                            <asp:DropDownList runat="server" CssClass="form-control" Visible="false" ID="ddlBillByBillTx_Ref">
                                            </asp:DropDownList>
                                            <small><span id="valddlBillByBillTx_Ref" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Amount<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" ID="txtBillByBillTx_Amount" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event);"></asp:TextBox>
                                            <small><span id="valtxtBillByBillTx_Amount" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Cr/Dr<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlBillByBillTx_crdr" CssClass="form-control select2" runat="server">
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
                                        <asp:GridView ID="GridViewRef" runat="server" DataKeyNames="RID" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" ShowHeader="true" OnRowDeleting="GridViewRef_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Type of Ref">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Delete" runat="server" CssClass="label " CausesValidation="False" CommandName="Delete" Text="" OnClientClick="return confirm('The Type of Ref will be deleted. Are you sure want to continue?');"><img src="../image/Del.png" /></asp:LinkButton>
                                                        <asp:Label ID="lblTypeOfRef" CssClass="paddingLR" runat="server" Text='<%# Eval("BillByBillTx_RefType").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("RID").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblRefNo" CssClass="paddingLR" runat="server" Text='<%# Eval("BillByBillTx_Ref").ToString()%>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount" SortExpression="leftBonus">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("BillByBillTx_Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cr/Dr" SortExpression="leftBonus">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
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
                                            <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSubmit" Text="Final Submit" OnClick="btnSubmit_Click" OnClientClick="return validateSubmit()" />
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

            </div>


        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ShowModal() {
            $("#ModalItemDetail").modal();
        }
        function ShowReferanceModal() {
            $("#ModalReferance").modal();
        }

        function CalculateAmount() {
            var Quantity = document.getElementById('<%=txtQuantity.ClientID%>').value.trim();
            var Rate = document.getElementById('<%=txtRate.ClientID%>').value.trim();
            if (Quantity == "")
                Quantity = "0";
            if (Rate == "")
                Rate = "0";

            document.getElementById('<%=txtAmount.ClientID%>').value = (Quantity * Rate).toFixed(2);
            // CalculateGrandTotal();
        }
        $(document).ready(function () {
            CalculateGrandTotal();
        });

        function CalculateGrandTotal() {
            debugger;
            var i = 0;
            var Tval = 0;

            $('#GridViewItem tr').each(function (index) {
                if (i > 0) {
                    var temp = Tval;
                    var val = $(this).children("td").eq(3).find('input[type="text"]').val();

                    if (val == "")
                        val = 0;

                    Tval = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)
                }
                i++;
            });
            $('#GridViewLedger tr').each(function (index) {
                if (i > 0) {
                    var temp = Tval;
                    var val = $(this).children("td").eq(1).find('input[type="text"]').val();

                    if (val == "")
                        val = 0;

                    Tval = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)
                }
                i++;
            });

            document.getElementById('<%=lblGrandTotal.ClientID%>').value = Tval;

            // document.getElementById('<%=lblGrandTotal.ClientID%>').innerHTML = Tval;




        }

        <%--function ChangeRef() {
            $("#lblRefNo").hide();
            $("#txtRefNo").hide();
            $("#ddlRefNo").hide();
            var RefType = document.getElementById('<%=ddlRefType.ClientID%>').selectedIndex;
            if (RefType == 0 || RefType == 2) {
                $("#txtRefNo").show();
            }
            else if (RefType == 3) {
                $("#lblRefNo").show();
            }
            else {
                $("#ddlRefNo").show();
            }
        }--%>
        function ValidateItemAdd() {
            var msg = "";
            if (document.getElementById('<%=ddlPartyName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Party Name.";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
                // ShowModal();
            }
        }
        function ValidateAddLedger() {
            var msg = "";
            if (document.getElementById('<%=ddlLedger.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Ledger.\n";
            }
            if (document.getElementById('<%=txtLedgerAmt.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Amount.";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;

            }
        }

        function ValidateItem() {
            var msg = "";
            $("#valddlItem").html("");
            $("#valtxtQuantity").html("");
            $("#valtxtRate").html("");
            $("#valtxtAmount").html("");
            $("#valddlParticulars").html("");
            $("#valtxtParticularAmt").html("");
            if (document.getElementById('<%=ddlItem.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Item Name. \n";
                $("#valddlItem").html("Select Item Name");
            }
            if (document.getElementById('<%=txtQuantity.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Quantity. \n";
                $("#valtxtQuantity").html("Enter Quantity");
            }
            if (document.getElementById('<%=txtRate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Rate. \n";
                $("#valtxtRate").html("Enter Rate");
            }
            if (document.getElementById('<%=txtAmount.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Amount. \n";
                $("#valtxtAmount").html("Enter Amount");
            }
            if (document.getElementById('<%=ddlParticulars.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Particulars. \n";
                $("#valddlParticulars").html("Select Particulars");
            }
            if (document.getElementById('<%=txtParticularAmt.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Particular Amount. \n";
                $("#valtxtParticularAmt").html("Enter Particular Amount");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }
        function validateAccept() {
            var msg = "";
            $("#valtxtVoucherTx_No").html("Enter Voucher No");
            $("#valtxtVoucherTx_Date").html("Enter Date");
            $("#valddlPartyName").html("Select Party A/c Name");
            //     var VoucherGrandTotal = document.getElementById('<%=lblGrandTotal.ClientID%>').innerHTML;
            //   var RefTotal = document.getElementById('<%=lblRefTotal.ClientID%>').innerHTML;

            if (document.getElementById('<%=txtVoucherTx_No.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Voucher No. \n";
                $("#valtxtVoucherTx_No").html("Enter Voucher No");
                
            }
            if (document.getElementById('<%=txtVoucherTx_Date.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Date. \n";
                $("#valtxtVoucherTx_Date").html("Enter Date");
            }
            if (document.getElementById('<%=ddlPartyName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Party A/c Name. \n";
                $("#valddlPartyName").html("Select Party A/c Name");
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Accept Details ?")) {
                    return true;
                }
                else {
                    return false;
                }

            }
        }

        function validateSubmit() {
            var msg = "";
            debugger;
            var VoucherGrandTotal = document.getElementById('<%=lblGrandTotal.ClientID%>').value.trim();
            var RefTotal = document.getElementById('<%=lblRefTotal.ClientID%>').innerHTML;

            if (document.getElementById('<%=txtVoucherTx_No.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Voucher No. \n";
            }
            if (document.getElementById('<%=txtVoucherTx_Date.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Date. \n";
            }
            if (document.getElementById('<%=ddlPartyName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Party A/c Name. \n";
            }
            //if (parseFloat(VoucherGrandTotal) != parseFloat(RefTotal)) {
            //    msg += "Amount Not Clear. \n";
            //}
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Submit Details ?")) {
                    return true;
                }
                else {
                    return false;
                }

            }
        }
    </script>
</asp:Content>
