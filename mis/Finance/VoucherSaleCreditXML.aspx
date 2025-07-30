<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VoucherSaleCreditXML.aspx.cs" Inherits="mis_Finance_VoucherSaleCreditXML" %>

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
                            <h3 class="box-title">Credit Sale Voucher</h3>
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
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVoucherTx_No" placeholder="Enter Voucher/Bill No." ClientIDMode="Static" MaxLength="10" autocomplete="off" onkeypress="return abc(event);"></asp:TextBox>

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
                                        <asp:GridView ID="GridViewItem" runat="server" DataKeyNames="ID" ClientIDMode="Static" class="table table-bordered customCSS" Style="margin-bottom: 0px;" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnRowDeleting="GridViewItem_RowDeleting" ShowFooter="true">
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
                                                        <asp:Label ID="lblWarehouse_id" CssClass="hidden" runat="server" Text='<%# Eval("Warehouse_id").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblWarehouseName" CssClass="hidden" runat="server" Text='<%# Eval("WarehouseName").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblTaxbility" CssClass="hidden" runat="server" Text='<%# Eval("Taxbility").ToString()%>'></asp:Label>

                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HSN Code">
                                                    <HeaderStyle />
                                                    <%-- <ItemStyle HorizontalAlign="Right" />--%>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHSNCode" runat="server" Text='<%# Eval("HSN_Code").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CGST">
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
                                                </asp:TemplateField>
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
                                        <fieldset>
                                            <legend>Amount Detail (Cr)</legend>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div id="divamount" runat="server">
                                                        <div class="row">
                                                            <div class="col-md-5">
                                                                <div class="form-group">
                                                                    <label>Ledger</label>
                                                                    <asp:DropDownList ID="ddlLedger" ClientIDMode="Static" runat="server" CssClass="form-control select2">
                                                                    </asp:DropDownList>
                                                                    <small><span id="valddlLedger" style="color: red;"></span></small>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-5">
                                                                <div class="form-group">
                                                                    <label>Amount</label>
                                                                    <asp:TextBox ID="txtLedgerAmt" ClientIDMode="Static" runat="server" CssClass="form-control" MaxLength="12" autocomplete="off" onblur="return validateAmount(this);" onkeypress="return allowNegativeNumber(event);"></asp:TextBox>
                                                                    <small><span id="valtxtLedgerAmt" style="color: red;"></span></small>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <label>&nbsp;</label>
                                                                    <asp:Button ID="btnAddLedgerAmt" runat="server" CssClass="btn btn-block" Text="Add" OnClick="btnAddLedgerAmt_Click" OnClientClick="return validateLedger();" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-- Ledger/Amount Detail GridView-->
                                                    <asp:GridView ID="GridViewLedger" runat="server" DataKeyNames="LedgerID" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" ShowHeader="false" OnRowDeleting="GridViewLedger_RowDeleting">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Ledger" ShowHeader="False">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="Delete" Visible='<%# Eval("Status").ToString() =="1" ? true:false %>' runat="server" CssClass="label " CausesValidation="False" CommandName="Delete" Text="" Style="color: red;" OnClientClick="return confirm('The Ledger will be deleted. Are you sure want to continue?');"><i class="fa fa-trash"></i></asp:LinkButton>
                                                                    <asp:Label ID="lblLedgerName" CssClass="paddingLR" runat="server" Text='<%# Eval("LedgerName").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("LedgerID").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblHSNCode" CssClass="hidden" runat="server" Text='<%# Eval("HSN_Code").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblCGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("CGST_Per").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblSGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("SGST_Per").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblIGSTPer" CssClass="hidden" runat="server" Text='<%# Eval("IGST_Per").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblCGSTAmt" CssClass="hidden" runat="server" Text='<%# Eval("CGSTAmt").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblSGSTAmt" CssClass="hidden" runat="server" Text='<%# Eval("SGSTAmt").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblIGSTAmt" CssClass="hidden" runat="server" Text='<%# Eval("IGSTAmt").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblGSTApplicable" CssClass="hidden" runat="server" Text='<%# Eval("GSTApplicable").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblTaxbility" CssClass="hidden" runat="server" Text='<%# Eval("Taxbility").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" ShowHeader="False" ItemStyle-Width="50%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtAmount" MaxLength="12" runat="server" onblur="return validateAmount(this);" CssClass="form-control AlignR" Style="padding: 3px;" Text='<%# Eval("Amount").ToString()%>' autocomplete="off" onkeypress="return allowNegativeNumber(event);" onfocusout="return validateAmount(this);"></asp:TextBox>
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
                                                                <asp:TextBox ID="lblGrandTotal" ClientIDMode="Static" CssClass="form-control AlignR" Style="padding: 3px;" runat="server" Text="0"></asp:TextBox>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                            </asp:Panel>
                            <fieldset>
                                <legend>Debtor Detail (Dr)</legend>
                                <div id="divdebtor" runat="server">
                                    <div class="row">

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Name of Debtor</label>
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
                                        <asp:GridView ID="GridViewDebtor" runat="server" DataKeyNames="Ledger_ID" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" ShowFooter="true" OnSelectedIndexChanged="GridViewDebtor_SelectedIndexChanged" OnRowDeleting="GridViewDebtor_RowDeleting">
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
                                                <asp:TemplateField HeaderText="Bill By Bill Detail" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkView" runat="server" CausesValidation="False" CommandName="Select" CssClass="label label-info" Text='<%# Eval("LedgerTx_MaintainType").ToString()=="None"?"NA":"View" %>'  OnClick="lnkbtnView_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CausesValidation="False" CommandName="Select" CssClass="label label-info" Text='<%# ((Eval("LedgerTx_MaintainType").ToString()=="None") || (Eval("LedgerTx_MaintainType").ToString()=="Cheque"))?"":"Edit" %>'  Visible='<%# ((Eval("LedgerTx_MaintainType").ToString()=="None") || (Eval("LedgerTx_MaintainType").ToString()=="Cheque")) ?false:true %>' OnClick="lnkbtnEdit_Click"></asp:LinkButton>
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
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Name of Consignee</label>
                                            <asp:TextBox runat="server" ID="txtNameofConsignee" ClientIDMode="Static" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                            <small><span id="valtxtNameofConsignee" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Location <span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlWarehouse" runat="server" ClientIDMode="Static" CssClass="form-control select2">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtWareHouse" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                            <small><span id="valddlWarehouse" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>
                                                Scheme<%-- [<asp:HyperLink ID="lnkAdd" Target="_blank" runat="server" NavigateUrl="SchemeMaster.aspx" >Add</asp:HyperLink>]--%>
                                            </label>
                                            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                                <asp:ListItem>Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <small><span id="valddlScheme" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Order No.</label>
                                            <asp:TextBox runat="server" ID="txtVoucherTx_OrderNo" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                            <small><span id="valtxtVoucherTx_OrderNo" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Order Date</label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtVoucherTx_OrderDate" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Registration No.</label>
                                            <asp:TextBox runat="server" ID="txtVoucherTx_RegNo" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                            <small><span id="valtxtVoucherTx_RegNo" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                </div>
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

            <!-- Start Add BillByBillDetail Modal-->
            <div class="modal fade" id="myModal" role="dialog">
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
                                        <label>Ref Number<span style="color: red;">*</span></label>
                                        <asp:HyperLink ID="lnkView" onclick="ShowRefDetailModal();" Visible="false" runat="server">View AgstRef</asp:HyperLink>
                                        <asp:TextBox runat="server" ID="txtBillByBillTx_Ref"  ClientIDMode="Static" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                        <asp:DropDownList runat="server" CssClass="form-control select2" Visible="false" ID="ddlBillByBillTx_Ref" onchange="ChangeRef()">
                                        </asp:DropDownList>
                                        <small><span id="valddlBillByBillTx_Ref" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Amount<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" ID="txtBillByBillTx_Amount" ClientIDMode="Static" CssClass="form-control" MaxLength="12" onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
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
                                    <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GridViewBillByBillDetail" AutoGenerateColumns="false" OnRowCommand="GridViewBillByBillDetail_RowCommand">
                                        <Columns>
                                            <asp:BoundField DataField="BillByBillTx_RefType" HeaderText="Type of Ref" HeaderStyle-Width="12%" ItemStyle-Width="12%" />
                                            <asp:BoundField DataField="BillByBillTx_Ref" HeaderText="Name" />
                                            <asp:TemplateField HeaderText="Amount" SortExpression="leftBonus" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="18%" HeaderStyle-Width="18%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("BillByBillTx_Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtn" runat="server" CommandName="BillByBillDelete" CommandArgument='<%# Eval("RowNo") %>'><i class="fa fa-trash"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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

            <!-- Start ViewBillByBillDetail Modal-->
            <div class="modal fade" id="BillByBillViewModal" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Bill-wise Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GridViewBillByBillViewDetail" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="BillByBillTx_RefType" HeaderText="Type of Ref" HeaderStyle-Width="10%" ItemStyle-Width="10%" />
                                                <asp:BoundField DataField="BillByBillTx_Ref" HeaderText="Name" />
                                                <asp:BoundField DataField="BillByBillTx_Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10%" />
                                                <asp:BoundField DataField="Type" HeaderText="Cr/Dr" ItemStyle-Width="5%" HeaderStyle-Width="5%" />
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
                                        <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GVFinChequeTx" AutoGenerateColumns="false" ClientIDMode="Static">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:BoundField DataField="ChequeTx_No" HeaderText="Cheque/ DD No." />--%>
                                                <asp:TemplateField HeaderText="Cheque/ DD No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChequeTx_No" runat="server" Text='<%# Eval("ChequeTx_No").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cheque/ DD Date.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblChequeTx_Date" runat="server" Text='<%# Eval("ChequeTx_Date").ToString()%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="ChequeTx_Date" HeaderText="Cheque/ DD Date" />--%>
                                                <asp:TemplateField HeaderText="ChequeTx_Amount.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAmountH" runat="server" Text='<%# Eval("ChequeTx_Amount").ToString()%>'></asp:Label>
                                                        <asp:TextBox ID="txtAmountH" runat="server" CssClass="hidden" Text='<%# Eval("ChequeTx_Amount").ToString()%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="ChequeTx_Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" />--%>
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

            <!-- Start ViewChequeDetail Modal-->
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
                                            <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-Width=" 20%" ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="20%" />

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
            debugger;
            var Quantity = document.getElementById('<%=txtQuantity.ClientID%>').value.trim();
            var Rate = document.getElementById('<%=txtRate.ClientID%>').value.trim();
            if (Quantity == "")
                Quantity = "0";
            if (Rate == "")
                Rate = "0";

            document.getElementById('<%=txtTotalAmount.ClientID%>').value = (Quantity * Rate).toFixed(2);
        }
        function CalculateRate() {
            debugger;
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
            debugger;
            var msg = "";
            $("#valddlItemName").html("");
            $("#valddlWarehouse").html("");
            $("#valtxtQuantity").html("");
            $("#valtxtRate").html("");
            $("#valtxtTotalAmount").html("");
            if (document.getElementById('<%=ddlItemName.ClientID%>').selectedIndex == 0) {
                msg += "Select Item Name. \n";
                $("#valddlItemName").html("Select Item Name");
            }
            if (document.getElementById('<%=ddlWarehouse.ClientID%>').selectedIndex == 0) {
                msg += "Select Location. \n";
                $("#valddlWarehouse").html("Select Location");
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

        function validateLedger() {
            var msg = "";
            $("#valddlLedger").html("");
            $("#valtxtLedgerAmt").html("");
            if (document.getElementById('<%=ddlLedger.ClientID%>').selectedIndex == 0) {
                msg += "Select Ledger. \n";
                $("#valddlLedger").html("Select Ledger");
            }
            if (document.getElementById('<%=txtLedgerAmt.ClientID%>').value.trim() == "") {
                msg += "Enter Amount .";
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

        function validateDebtor() {
            debugger;
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
            debugger;
            var i = 0;
            var Tval = 0;
            var Tval1 = 0;

            var HiddenValue = '<%=hfvalue.Value%>';
            var rowcount = $('#GridViewItem tr').length;
            //alert(rowcount);
            //alert(HiddenValue);
            $('#GridViewItem tr').each(function (index) {

                if (i > 0 && i < rowcount - 1) {
                    var temp = Tval;
                    var val = $(this).children("td").eq(5).find('input[type="text"]').val();

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
            //$('#GridViewDebtor tr').each(function (index) {
            //    if (i > 0) {
            //        debugger;
            //        var val = $(this).children("td").eq(3).find('input[type="text"]').val();

            //        if (val == "")
            //            val = 0;

            //        Tval1 = parseFloat(val).toFixed(2)

            //    }
            //    i++;
            //});
            document.getElementById('<%=lblGrandTotal.ClientID%>').value = Tval;
            document.getElementById('<%=lblroundsuggestion.ClientID%>').innerText = (Math.round(Tval) - Tval).toFixed(2);
            <%--document.getElementById('<%=txDebtorAmt.ClientID%>').value = Tval - Tval1;--%>
            if (parseFloat(Math.abs(Tval)) == parseFloat(HiddenValue)) {

                if (Math.abs(Tval) != 0) {
                    document.getElementById('<%=btnAccept.ClientID%>').disabled = false;
                    document.getElementById('<%=btnAddDebtor.ClientID%>').disabled = true;
                }
                else {
                    document.getElementById('<%=btnAccept.ClientID%>').disabled = true;
                }
            }
            else {

                document.getElementById('<%=btnAccept.ClientID%>').disabled = true;
                document.getElementById('<%=btnAddDebtor.ClientID%>').disabled = false;


            }

        }
        function validateCheque() {
            debugger;
            var msg = "";
            $("#valtxtChequeTx_No").html("");
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
                var i = 0;
                var Tval = 0;
                var LedgerAmount = parseFloat(document.getElementById('<%=txDebtorAmt.ClientID%>').value);
                var ChequeAmount = parseFloat(document.getElementById('<%=txtChequeTx_Amount.ClientID%>').value);
                $('#GVFinChequeTx tr').each(function (index) {
                    debugger;
                    var temp = Tval;
                    var val = $(this).children("td").eq(3).find('input[type="text"]').val();

                    if (val == "")
                        val = 0;

                    Tval = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)
                    if (Tval == "NaN")
                        Tval = 0;

                });
                LedgerAmount = LedgerAmount - Tval;
                if (ChequeAmount > LedgerAmount) {

                    msg += "Enter Valid Amount. \n";
                    $("#valtxtChequeTx_Amount").html("Enter Valid Amount");
                }
                else {
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
            else if (document.getElementById('<%=ddlRefType.ClientID%>').selectedIndex == 0 || document.getElementById('<%=ddlRefType.ClientID%>').selectedIndex == 2) {
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
            if (document.getElementById('<%= ddlWarehouse.ClientID%>').selectedIndex == 0) {
                msg += "Select Location. \n";
                $("#valddlWarehouse").html("Select Location");
            }
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
            debugger;
            hideshowitempanel();

        });
        function hideshowitempanel() {
            debugger;
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
                    data: "{ 'Qauntity': '" + $('#txtQuantity').val() + "', 'OfficeID': '" + '<%=hfofficeID.Value%>' + "', 'ItemID': '" + $('#ddlItemName').val() + "', 'WarehouseID': '" + $('#ddlWarehouse').val() + "'}",
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
            function changeref() {
                var ref = document.getElementById('<%# ddlRefType.ClientID%>').value;
            if (ref == "1") {
                $("#txtBillByBillTx_Ref").style("display", "block");
                $("#ddlBillByBillTx_Ref").style("display", "none");
                $("#lnkView").style("display", "none");
            }
            else {
                $("#txtBillByBillTx_Ref").style("display", "none");
                $("#ddlBillByBillTx_Ref").style("display", "block");
                $("#lnkView").style("display", "block");
            }
        }
    </script>

</asp:Content>


