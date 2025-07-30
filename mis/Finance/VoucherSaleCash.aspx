<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VoucherSaleCash.aspx.cs" Inherits="mis_Finance_VoucherSaleCash" %>

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
            <div class="box box-success" style="background-color: #e3dcc0">
                <div class="box-header">
                    <h3 class="box-title">Cash Sale Voucher</h3>
                    <asp:LinkButton ID="lbkbtnAddLedger" class="btn btn-primary pull-right" runat="server" OnClick="lbkbtnAddLedger_Click">Add Ledger</asp:LinkButton>
                    <asp:LinkButton ID="lnkPreviousVoucher" class="btn btn-primary pull-right" style="margin-right:10px;" runat="server" OnClick="lnkPreviousVoucher_Click">Copy Previous Voucher</asp:LinkButton>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblPreviousVoucherNo" runat="server" Text="" Font-Bold="true" style="color:blue"></asp:Label>
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
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtVoucherTx_No" placeholder="Enter Voucher/Bill No." ClientIDMode="Static" MaxLength="10" autocomplete="off"></asp:TextBox>
                                    <small><span id="valtxtVoucherTx_No" style="color: red;"></span></small>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4" style="display:none;">
                            <div class="form-group">
                                <label>Reference No.<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVoucherTx_Ref" placeholder="Enter Reference No..." MaxLength="50" autocomplete="off"></asp:TextBox>
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
                                    <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtVoucherTx_Date" data-date-end-date="0d" autocomplete="off" OnTextChanged="txtVoucherTx_Date_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <small><span id="valtxtVoucherTx_Date" style="color: red;"></span></small>
                            </div>
                        </div>
                    </div>
                    <fieldset>
                        <legend>Add Item</legend>
                        <div id="divitem" runat="server">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Item Name<span style="color: red;"> *</span></label>
                                    <asp:DropDownList ID="ddlItemName" ClientIDMode="Static" runat="server" CssClass="form-control select1 select2" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" AutoPostBack="true">
                                        <%--<asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem>Sprikler</asp:ListItem>
                                                <asp:ListItem>Ancient Plows</asp:ListItem>--%>
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
                                    <asp:TextBox runat="server" CssClass="form-control" ClientIDMode="Static" ID="txtRate" placeholder="Enter Rate..." onchange="CalculateAmount();" MaxLength="11" onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                    <small><span id="valtxtRate" style="color: red;"></span></small>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Amount<span style="color: red;"> *</span></label>
                                    <asp:TextBox ID="txtTotalAmount" runat="server" ClientIDMode="Static" placeholder="Total Amount" CssClass="form-control" MaxLength="12" onchange="CalculateRate();" onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
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
                        <%--<table border="1" class="table table-bordered table-striped table-hover">
                                    <tr>
                                        <th>S. No.</th>
                                        <th>Item Name</th>
                                        <th>Rate</th>
                                        <th>Quantity</th>


                                        <th>Amount</th>

                                        <th>Delete</th>
                                    </tr>
                                    <tr>
                                        <td>1.</td>
                                        <td>Sprinkler</td>
                                        <td>100</td>
                                        <td>1</td>



                                        <td>118</td>

                                        <td>
                                            <asp:LinkButton runat="server" CssClass="label label-default" Text="Edit"></asp:LinkButton>&nbsp;
                                    <asp:LinkButton runat="server" CssClass="label label-danger" Text="Delete"></asp:LinkButton></td>
                                    </tr>


                                </table>--%>
                        <asp:GridView ID="GridViewItem" runat="server" DataKeyNames="ID" ClientIDMode="Static" class="table table-bordered customCSS" Style="margin-bottom: 0px;" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnRowDeleting="GridViewItem_RowDeleting" ShowFooter="true">
                                        <Columns>
                                            <%--<asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                    <HeaderStyle />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Item will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                    <asp:Label ID="lblItemRowNo" Text='<%# Eval("ID").ToString()%>' CssClass="hidden" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Item").ToString()%>'></asp:Label>
                                                    <%--<asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("ID").ToString()%>'></asp:Label>--%>
                                                    <asp:Label ID="lblItemID" CssClass="hidden" runat="server" Text='<%# Eval("ItemID").ToString()%>'></asp:Label>
                                                    <asp:Label ID="lblUnit_id" CssClass="hidden" runat="server" Text='<%# Eval("Unit_id").ToString()%>'></asp:Label>
                                                    <asp:Label ID="lblWarehouse_id" CssClass="hidden" runat="server" Text='<%# Eval("Warehouse_id").ToString()%>'></asp:Label>
                                                    <asp:Label ID="lblWarehouseName" CssClass="hidden" runat="server" Text='<%# Eval("WarehouseName").ToString()%>'></asp:Label>
                                                    <asp:Label ID="lblHSNCode" CssClass="hidden" runat="server" Text='<%# Eval("HSN_Code").ToString()%>'></asp:Label>
                                                    <asp:Label ID="lblTaxbility" CssClass="hidden" runat="server" Text='<%# Eval("Taxbility").ToString()%>'></asp:Label>
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

                                            <%--<asp:TemplateField HeaderText="Total Amount" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                    <HeaderStyle />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAmount" CssClass="paddingLR" runat="server" Text='<%# Eval("TotalAmount").ToString()%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                        <small><span id="valGridViewItem" style="color: red;"></span></small>
                        <div class="row">
                        </div>
                    </fieldset>

                    <div class="row">
                        <div class="col-md-6">
                        </div>
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
                                                    <asp:TextBox ID="txtLedgerAmt" ClientIDMode="Static" runat="server" CssClass="form-control" MaxLength="12" autocomplete="off" onkeypress="return allowNegativeNumber(event);" onblur="return validateAmount(this);"></asp:TextBox>
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
                                        <asp:GridView ID="GridViewLedger" runat="server" DataKeyNames="LedgerID" ClientIDMode="Static" class="table table-bordered customCSS" AutoGenerateColumns="False" ShowHeader="false" OnRowDeleting="GridViewLedger_RowDeleting">
                                            <Columns>
                                                <%-- <asp:TemplateField HeaderText="Action" ShowHeader="False"  ItemStyle-Width="50">                                                                                                                        
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" Visible='<%# Eval("Status").ToString() =="1" ? true:false %>' runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Ledger will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
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
                                                        <asp:TextBox ID="txtAmount"  MaxLength="12" runat="server" onblur="CalculateGrandTotal();" CssClass="form-control AlignR" Style="padding: 3px;" Text='<%# Eval("Amount").ToString()%>' autocomplete="off" onkeypress="return allowNegativeNumber(event);" onfocusout="return validateAmount(this);"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>

                                        <hr />
                                         <span style="color:red">Suggested Round Off:  </span><asp:Label ID="lblroundsuggestion" runat="server" ClientIDMode="Static" Text=""></asp:Label><br/>
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


                    <fieldset>
                        <legend>Debit To (Dr)</legend>

                        <div class="row">

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Ledger<span style="color: red;"> *</span> </label>
                                    <asp:DropDownList runat="server" ID="ddlDebitLedger" CssClass="form-control select2">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtDebitLedger" runat="server" CssClass="form-control" Visible="false"></asp:TextBox> 
                                    <small><span id="valddlDebitLedger" style="color: red;"></span></small>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Sold To<span style="color: red;"> *</span></label>
                                    <asp:TextBox runat="server" ID="txtVoucherTx_SoldTo" ClientIDMode="Static" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                    <small><span id="valtxtVoucherTx_SoldTo" style="color: red;"></span></small>
                                </div>
                            </div>

                            <%--<div class="col-md-3">
                                <div class="form-group">
                                    <label>Amount</label>
                                    <asp:TextBox ID="txtDbtAmt" runat="server" Text="" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>--%>
                            <%--    <div class="col-md-1">
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <asp:Button runat="server" CssClass="btn btn-info btn-block" Text="Add" />
                                        </div>
                                    </div>--%>
                        </div>
                    </fieldset>

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Location<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlWarehouse" runat="server" ClientIDMode="Static" CssClass="form-control select2">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtWareHouse" runat="server" CssClass="form-control" Visible="false"></asp:TextBox> 
                                <small><span id="valddlWarehouse" style="color: red;"></span></small>
                            </div>
                        </div>
                        <%-- <div class="col-md-2">
                            <div class="form-group">
                                <label>Sales Center<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlSalesCenter" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                                <small><span id="valddlSalesCenter" style="color: red;"></span></small>
                            </div>
                        </div>--%>

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Scheme<%--<span style="color: red;"> *</span> [<asp:LinkButton ID="lnkAdd" runat="server" OnClick="lnkAdd_Click">Add</asp:LinkButton>]--%></label>
                                <asp:DropDownList ID="ddlScheme" runat="server" CssClass="form-control select2">
                                </asp:DropDownList>
                                <small><span id="valddlScheme" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Order No.</label>
                                <asp:TextBox runat="server" ID="txtVoucherTx_OrderNo" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                <small><span id="valtxtVoucherTx_OrderNo" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Registration No.</label>
                                <asp:TextBox runat="server" ID="txtVoucherTx_RegNo" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                <small><span id="valtxtVoucherTx_RegNo" style="color: red;"></span></small>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Narration<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" ID="txtVoucherTx_Narration" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                <small><span id="valtxtVoucherTx_Narration" style="color: red;"></span></small>
                            </div>
                        </div>
                         <asp:Button runat="server" CssClass="hidden" ID="btnNarration" OnClick="btnNarration_Click" AccessKey="R" />
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button Text="Accept" ID="btnAccept" runat="server" CssClass="btn btn-success btn-block" OnClick="btnAccept_Click" OnClientClick="return validateform();" />

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">

                                <a href="VoucherSaleCash.aspx" id="btn_Clear" runat="server" class="btn btn-default btn-block">Clear</a>
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
            debugger;
            var Quantity = document.getElementById('<%=txtQuantity.ClientID%>').value.trim();
            var TotalAmount = document.getElementById('<%=txtTotalAmount.ClientID%>').value.trim();
            //var Rate = document.getElementById('<%=txtRate.ClientID%>').value.trim();
            if (Quantity == "") {
                document.getElementById('<%=txtQuantity.ClientID%>').value = "1";
                 Quantity = "1";
             }
             if (TotalAmount == "")
                 TotalAmount = "0";

             document.getElementById('<%=txtRate.ClientID%>').value = (TotalAmount / Quantity).toFixed(2);
        }

        function ShowModal() {
            $("#ItemModal").modal();
        }
        function validateItem() {
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
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }

        }
        $(document).ready(function () {
            CalculateGrandTotal();
        });
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
            else
            {
                sender.value = "0";
            }


        }
        function CalculateGrandTotal() {
            debugger;
            var i = 0;
            var Tval = 0;
            var rowcount = $('#GridViewItem tr').length;
            $('#GridViewItem tr').each(function (index) {
                if (i > 0 && i < rowcount - 1) {
                    var temp = Tval;
                    var val = $(this).children("td").eq(4).find('input[type="text"]').val();

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
            document.getElementById('<%=lblroundsuggestion.ClientID%>').innerText = (Math.round(Tval) - Tval).toFixed(2);
            //document.getElementById('<%=lblGrandTotal.ClientID%>').innerHTML = Tval;




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
                    $("#valtxtChequeTx_Amount").html("Amount cannot be Zero.");
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
       
        <%--function validateScheme() {
            var msg = "";
            $("#valtxtSchemeTx_Name").html("");
            if (document.getElementById('<%=txtSchemeTx_Name.ClientID%>').value.trim() == "") {
                msg += "Enter Scheme Name .";
                $("#valtxtSchemeTx_Name").html("Enter Scheme Name");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }--%>
        function validateform() {
            debugger;
            var msg = "";
            $("#valtxtVoucherTx_No").html("");
            $("#valtxtVoucherTx_Ref").html("");
            $("#valGridViewItem").html("");
            $("#valddlDebitLedger").html("");
            $("#valtxtVoucherTx_SoldTo").html("");
            //$("#valddlSalesCenter").html("");
            //$("#valddlScheme").html("");
            //$("#valtxtVoucherTx_OrderNo").html("");
            //$("#valtxtVoucherTx_RegNo").html("");
            $("#valtxtVoucherTx_Narration").html("");
            if (document.getElementById('<%=txtVoucherTx_No.ClientID%>').value.trim() == "") {
                msg += "Enter Voucher/Bill No. \n";
                $("#valtxtVoucherTx_No").html("Enter Voucher/Bill No");
            }
            <%--if (document.getElementById('<%=txtVoucherTx_Ref.ClientID%>').value.trim() == "") {
                msg += "Enter Reference No. \n";
                $("#valtxtVoucherTx_Ref").html("Enter Reference No");
            }--%>
            var rowscount = $("#<%=GridViewItem.ClientID %> tr").length;
            if (rowscount == "1") {
                msg += "Enter Item Detail. \n";
                $("#valGridViewItem").html("Enter Item Detail");
            }
            if (document.getElementById('<%=ddlDebitLedger.ClientID%>').selectedIndex == 0) {
                msg += "Select Debit To Ledger. \n";
                $("#valddlDebitLedger").html("Select Debit To Ledger");
            }
            if (document.getElementById('<%=txtVoucherTx_SoldTo.ClientID%>').value.trim() == "") {
                msg += "Enter Sold To. \n";
                $("#valtxtVoucherTx_SoldTo").html("Enter Sold To");
            }
            if (document.getElementById('<%= ddlWarehouse.ClientID%>').selectedIndex == 0) {
                msg += "Select Location. \n";
                $("#valddlWarehouse").html("Select Location");
            }
            <%--if (document.getElementById('<%=ddlScheme.ClientID%>').selectedIndex == 0) {
                msg += "Select Scheme. \n";
                $("#valddlScheme").html("Select Scheme");
            }--%>
            <%--if (document.getElementById('<%=txtVoucherTx_OrderNo.ClientID%>').value.trim() == "") {
                msg += "Enter Order No. \n";
                $("#valtxtVoucherTx_OrderNo").html("Enter Order No");
            }
            if (document.getElementById('<%=txtVoucherTx_RegNo.ClientID%>').value.trim() == "") {
                msg += "Enter Registration No. \n";
                $("#valtxtVoucherTx_RegNo").html("Enter Registration No.");
            }--%>
            if (document.getElementById('<%=txtVoucherTx_Narration.ClientID%>').value.trim() == "") {
                msg += "Enter Narration . \n";
                $("#valtxtVoucherTx_Narration").html("Enter Narration .");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnAccept.ClientID%>').value.trim() == "Accept") {
                    
                        document.querySelector('.popup-wrapper').style.display = 'block';
                        return true;
                    
                }
                else if (document.getElementById('<%=btnAccept.ClientID%>').value.trim() == "Update") {
                    
                        document.querySelector('.popup-wrapper').style.display = 'block';
                        return true;
                    
                }
            }

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
    </script>
</asp:Content>
