<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VoucherTransferOfMaterial.aspx.cs" Inherits="mis_Finance_VoucherTransferOfMaterial" %>

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

        legend {
            width: initial;
            padding: 0px 10px;
            margin: 0;
            font-size: 12px;
            color: #333333;
            text-transform: uppercase;
            background-color: #FFC107;
            border: 1px solid #ddd;
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
                            <h3 class="box-title">Stock Journal (<span style="color: teal">Transfer Of Material</span>)</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <asp:Panel ID="panel1" runat="server">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="col-md-12">
                                            <label>Stock Journal No.<span style="color: red;"> *</span></label>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-md-5">
                                                <asp:Label ID="lblVoucherTx_No" runat="server" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                            </div>
                                            <div class="col-md-5" style="margin-left: -32px;">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVoucherTx_No" placeholder="Stock Journal No." ClientIDMode="Static" MaxLength="16" autocomplete="off"></asp:TextBox>
                                                <small><span id="valtxtVoucherTx_No" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Reference No.</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtInvoice" placeholder="Reference No." MaxLength="16" autocomplete="off"></asp:TextBox>
                                            <small><span id="valtxtInvoice" style="color: red;"></span></small>
                                        </div>
                                    </div>

                                    <%--<div class="col-md-3">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtVoucherTx_Date" runat="server" CssClass="form-control DateAdd" data-date-end-date="0d" autocomplete="off" OnTextChanged="txtVoucherTx_Date_TextChanged" onchange="CompareSupplierInvocieDate();" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox runat="server" ClientIDMode="Static" data-date-end-date="0d" CssClass="form-control DateAdd" ID="txtVoucherTx_Date" placeholder="DD/MM/YYYY" OnTextChanged="txtVoucherTx_Date_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <small><span id="valtxtVoucherTx_Date" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                </div>


                                <asp:Panel ID="itemdetail" runat="server">
                                    <fieldset>
                                        <legend>Source (Consumption)</legend>
                                        <div id="divitem" runat="server">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Item Name<span style="color: red;"> *</span></label><br />
                                                        <asp:DropDownList ID="ddlItem" CssClass="form-control select1 select2" Style="width: 100%;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Warehouse<span style="color: red;"> *</span></label><br />
                                                        <asp:DropDownList ID="ddlWarehouse" CssClass="form-control select2" Style="width: 100%;" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Quantity<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtQuantity" onkeypress="return validateDecUnit(this,event)" placeholder="Enter Quantity..." onblur="CalculateAmount();" MaxLength="8" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                   
                                                    <asp:Label ID="lblUnit" CssClass="hidden" runat="server" ClientIDMode="Static"></asp:Label>
                                                </div>
                                                <div class="col-md-2 hidden">
                                                    <div class="form-group">
                                                        <label>Unit<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtUnitName" placeholder="Enter Unit..."></asp:TextBox>
                                                        <asp:Label ID="lblUnitName" CssClass="hidden" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Rate Per<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" MaxLength="8" onkeypress="return validateDec(this,event);" ID="txtRate" placeholder="Enter Rate..." onblur="CalculateAmount();" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Amount<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtAmount" placeholder="Enter Amount..." MaxLength="50" autocomplete="off" onblur="CalculateRate();"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnAdd" class="btn btn-success btn-block" Style="margin-top: 25px;" runat="server" OnClick="btnAdd_Click" Text="Add" OnClientClick="return ValidateItemAdd();"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">


                                                    <asp:GridView ID="GridViewItem" runat="server" DataKeyNames="ItemID" ClientIDMode="Static" class="table table-bordered customCSS" Style="margin-bottom: 0px;" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnRowDeleting="GridViewItem_RowDeleting">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Name">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Item").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("ID").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblItemID" CssClass="hidden" runat="server" Text='<%# Eval("ItemID").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblUnit_id" CssClass="hidden" runat="server" Text='<%# Eval("Unit_id").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblWarehouse_id" CssClass="hidden" runat="server" Text='<%# Eval("Warehouse_id").ToString()%>'></asp:Label>


                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="WareHouse">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWarehouse_Name" runat="server" Text='<%# Eval("WarehouseName").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <HeaderStyle />

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rate Per">
                                                                <HeaderStyle />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblUnit" CssClass="paddingLR" runat="server" Text='<%# Eval("Unit").ToString()%>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount ">
                                                                <HeaderStyle />

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount").ToString()%>'></asp:Label>
                                                                    <asp:TextBox ID="txtAmountH" runat="server" CssClass="hidden" Text='<%# Eval("Amount").ToString()%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Item will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </asp:Panel>

                                <asp:Panel ID="itemdetailProd" runat="server">
                                    <fieldset>
                                        <legend>Destination (Production)</legend>
                                        <div id="div1" runat="server">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Item Name<span style="color: red;"> *</span></label><br />
                                                        <asp:DropDownList ID="ddlItemProd" CssClass="form-control select1 select2" Style="width: 100%;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItemProd_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Warehouse<span style="color: red;"> *</span></label><br />
                                                        <asp:DropDownList ID="ddlWarehouseProd" CssClass="form-control select2" Style="width: 100%;" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Quantity<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtQuantityProd" onkeypress="return validateDecUnitProd(this,event)" placeholder="Enter Quantity..." onblur="CalculateAmountProd();" MaxLength="8" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="lblUnitProd" CssClass="hidden" runat="server" ClientIDMode="Static"></asp:Label>
                                                </div>
                                                <div class="col-md-2 hidden">
                                                    <div class="form-group">
                                                        <label>Unit<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtUnitNameProd" placeholder="Enter Unit..."></asp:TextBox>
                                                        <asp:Label ID="lblUnitNameProd" CssClass="hidden" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Rate Per<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" MaxLength="8" onkeypress="return validateDecProd(this,event);" ID="txtRateProd" placeholder="Enter Rate..." onblur="CalculateAmountProd();" autocomplete="off"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Amount<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtAmountProd" placeholder="Enter Amount..." MaxLength="50" autocomplete="off" onblur="CalculateRateProd();"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnAddProd" class="btn btn-success btn-block" Style="margin-top: 25px;" runat="server" OnClick="btnAddProd_Click" Text="Add" OnClientClick="return ValidateItemAddProd();"></asp:Button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">


                                                    <asp:GridView ID="GridViewItemProd" runat="server" DataKeyNames="ItemID" ClientIDMode="Static" class="table table-bordered customCSS" Style="margin-bottom: 0px;" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnRowDeleting="GridViewItemProd_RowDeleting">
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Name">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Item").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("ID").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblItemID" CssClass="hidden" runat="server" Text='<%# Eval("ItemID").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblUnit_id" CssClass="hidden" runat="server" Text='<%# Eval("Unit_id").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblWarehouse_id" CssClass="hidden" runat="server" Text='<%# Eval("Warehouse_id").ToString()%>'></asp:Label>


                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="WareHouse">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWarehouse_Name" runat="server" Text='<%# Eval("WarehouseName").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity">
                                                                <HeaderStyle />

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity").ToString()%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Rate Per">
                                                                <HeaderStyle />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate").ToString()%>'></asp:Label>
                                                                    <asp:Label ID="lblUnit" CssClass="paddingLR" runat="server" Text='<%# Eval("Unit").ToString()%>'></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount ">
                                                                <HeaderStyle />

                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount").ToString()%>'></asp:Label>
                                                                    <asp:TextBox ID="txtAmountH" runat="server" CssClass="hidden" Text='<%# Eval("Amount").ToString()%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Item will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </asp:Panel>
                                <%--<asp:Panel ID="Panel2" runat="server">
                                <fieldset>
                                    <legend>Destination (Production)</legend>
                                    <div id="div1" runat="server">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Item Name<span style="color: red;"> *</span></label><br />
                                                <asp:DropDownList ID="ddlItemP" CssClass="form-control select1 select2" Style="width: 100%;" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Warehouse<span style="color: red;"> *</span></label><br />
                                                <asp:DropDownList ID="ddlWarehouseP" CssClass="form-control select2" Style="width: 100%;" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Quantity<span style="color: red;"> *</span></label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtQuantityP" onkeypress="return validateDecUnit(this,event)" placeholder="Enter Quantity..." onblur="CalculateAmount();" MaxLength="8" autocomplete="off"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="Label1" CssClass="hidden" runat="server" Text="2"></asp:Label>
                                        </div>
                                        <div class="col-md-2 hidden">
                                            <div class="form-group">
                                                <label>Unit<span style="color: red;"> *</span></label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtUnitNameP" placeholder="Enter Unit..."></asp:TextBox>
                                                <asp:Label ID="Label2" CssClass="hidden" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Rate Per<span style="color: red;"> *</span></label>
                                                <asp:TextBox runat="server" CssClass="form-control" MaxLength="8" onkeypress="return validateDec(this,event);" ID="txtRateP" placeholder="Enter Rate..." onblur="CalculateAmount();" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Amount<span style="color: red;"> *</span></label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtAmountP" placeholder="Enter Amount..." MaxLength="50" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="Button1" class="btn btn-success btn-block" Style="margin-top: 25px;" runat="server" OnClick="btnAdd_Click" Text="Add" OnClientClick="return ValidateItemAdd();"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                        </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">


                                                <asp:GridView ID="GridView1" runat="server" DataKeyNames="ItemID" ClientIDMode="Static" class="table table-bordered customCSS" Style="margin-bottom: 0px;" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnRowDeleting="GridViewItem_RowDeleting" >
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Name">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Item").ToString()%>'></asp:Label>
                                                                <asp:Label ID="lblID" CssClass="hidden" runat="server" Text='<%# Eval("ID").ToString()%>'></asp:Label>
                                                                <asp:Label ID="lblItemID" CssClass="hidden" runat="server" Text='<%# Eval("ItemID").ToString()%>'></asp:Label>
                                                                <asp:Label ID="lblUnit_id" CssClass="hidden" runat="server" Text='<%# Eval("Unit_id").ToString()%>'></asp:Label>
                                                                <asp:Label ID="lblWarehouse_id" CssClass="hidden" runat="server" Text='<%# Eval("Warehouse_id").ToString()%>'></asp:Label>
                                                                

                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="WareHouse">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblWarehouse_Name" runat="server" Text='<%# Eval("WarehouseName").ToString()%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity">
                                                            <HeaderStyle />

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity").ToString()%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate Per">
                                                            <HeaderStyle />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate").ToString()%>'></asp:Label>
                                                                <asp:Label ID="lblUnit" CssClass="paddingLR" runat="server" Text='<%# Eval("Unit").ToString()%>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount ">
                                                            <HeaderStyle />

                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount").ToString()%>'></asp:Label>
                                                                <asp:TextBox ID="txtAmountH" runat="server" CssClass="hidden" Text='<%# Eval("Amount").ToString()%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                                                                          
                                                        
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('The Item will be deleted. Are you sure want to continue?');"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </asp:Panel>--%>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Narration<span style="color: red;"> *</span></label>
                                            <asp:TextBox ID="txtVoucherTx_Narration" runat="server" TextMode="MultiLine" CssClass="form-control" autocomplete="off"></asp:TextBox>
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
                                            <a href="VoucherPurchaseGoods.aspx" id="btn_Clear" runat="server" class="btn btn-default btn-block">Clear</a>

                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                    </div>
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

            document.getElementById('<%=txtAmount.ClientID%>').value = (Quantity * Rate).toFixed(2);
            // CalculateGrandTotal();
        }
	 function CalculateRate() {
            var Quantity = document.getElementById('<%=txtQuantity.ClientID%>').value.trim();
            var Amount = document.getElementById('<%=txtAmount.ClientID%>').value.trim();
            if (Quantity == "")
                Quantity = "0";
            if (Amount == "")
                Amount = "0";

            document.getElementById('<%=txtRate.ClientID%>').value = (Amount / Quantity).toFixed(2);
            // CalculateGrandTotal();
        }
        function CalculateRateProd() {
            var Quantity = document.getElementById('<%=txtQuantityProd.ClientID%>').value.trim();
             var Amount = document.getElementById('<%=txtAmountProd.ClientID%>').value.trim();
             if (Quantity == "")
                 Quantity = "0";
             if (Amount == "")
                 Amount = "0";

             document.getElementById('<%=txtRateProd.ClientID%>').value = (Amount / Quantity).toFixed(2);
             // CalculateGrandTotal();
        }
        function CalculateAmountProd() {
            var Quantity = document.getElementById('<%=txtQuantityProd.ClientID%>').value.trim();
            var Rate = document.getElementById('<%=txtRateProd.ClientID%>').value.trim();
            if (Quantity == "")
                Quantity = "0";
            if (Rate == "")
                Rate = "0";

            document.getElementById('<%=txtAmountProd.ClientID%>').value = (Quantity * Rate).toFixed(2);
            // CalculateGrandTotal();
        }



        function ValidateItemAdd() {
            var msg = "";

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
                // ShowModal();
            }
        }

        function ValidateItemAddProd() {
            var msg = "";

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
                // ShowModal();
            }
        }


        function ValidateItem() {
            var msg = "";
            if (document.getElementById('<%=ddlItem.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Item Name. \n";
            }
            if (document.getElementById('<%=txtQuantity.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Quantity. \n";
            }
            if (document.getElementById('<%=txtRate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Rate. \n";
            }
            if (document.getElementById('<%=txtAmount.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Amount. \n";
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


            if (document.getElementById('<%=txtVoucherTx_No.ClientID%>').value.trim() == "") {
                        msg = msg + "Enter Voucher No. \n";
                    }
<%--            if (document.getElementById('<%=txtInvoice.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Supplier's Invoice No. \n";
            }--%>
                    if (document.getElementById('<%=txtVoucherTx_Date.ClientID%>').value.trim() == "") {
                        msg = msg + "Enter Date. \n";
                    }


<%--            var rowscount = $("#<%=GridViewItem.ClientID %> tr").length;
            if (rowscount == "1") {
                msg += "Enter Item Detail. \n";
                $("#valGridViewItem").html("Enter Item Detail");
            }--%>
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

                function validateSubmit() {
                    var msg = "";
                    debugger;


                    if (document.getElementById('<%=txtVoucherTx_No.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Voucher No. \n";
            }
            if (document.getElementById('<%=txtVoucherTx_Date.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Date. \n";
            }

            //if (parseFloat(VoucherGrandTotal) != parseFloat(RefTotal)) {
            //    msg += "Amount Not Clear. \n";
            //}
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
            var reg = /^(\d{2})([A-Z]{5})(\d{4})([A-Z]{1})(\d{1})([Z]{1})([0-9A-Z]{1})$/;
            if (document.getElementById('txtGSTNo').value != "") {
                if (reg.test(document.getElementById('txtGSTNo').value) == false) {
                    alert("Invalid GST Number.");
                    document.getElementById('txtGSTNo').value = "";
                }
            }

        });
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

        function validateDecUnitProd(el, evt) {
            var digit = document.getElementById('<%=lblUnitProd.ClientID%>').innerText;           
           
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


