<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VoucherGoodsTransferInward.aspx.cs" Inherits="mis_Finance_VoucherGoodsTransferInward" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .select2 {
            width: 100% !important;
        }
        .Item
        {
            background-color: #f7e3c6;
            color:black;
            
            
        }
        .font
        {
            font-weight:600;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success" style="background-color: #f7e3c6">
                <div class="box-header">
                    <h3 class="box-title">Goods Transfer Inward</h3>

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
                    <div class="row">
                        <%--<div class="col-md-4">
                            <div class="form-group">
                                <label>Voucher No.<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVoucherTx_No" placeholder="Enter Voucher No..." MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                                <small><span id="valtxtVoucherTx_No" style="color: red;"></span></small>
                            </div>
                        </div>--%>
                        <div class="col-md-4">
                            <label>Voucher/Bill No.<span style="color: red;"> *</span></label>
                            <div class="form-group">
                                <div class="col-md-6">
                                    <asp:Label ID="lblVoucherTx_No" runat="server" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                    <asp:Label ID="lblVoucherNo" runat="server" CssClass="form-control" Visible="false" Style="background-color: #eee;"></asp:Label>
                                </div>
                                <div class="col-md-6" style="margin-left: -32px;">
                                    <asp:TextBox runat="server" CssClass="form-control NameNumslashhyphenOnly" ID="txtVoucherTx_No" placeholder="Enter Voucher No..." ClientIDMode="Static" MaxLength="6" autocomplete="off"></asp:TextBox>
                                    <small><span id="valtxtVoucherTx_No" style="color: red;"></span></small>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4"></div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Date<span style="color: red;"> *</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtVoucherTx_Date" data-date-end-date="0d" placeholder="DD/MM/YYYY" autocomplete="off" OnTextChanged="txtVoucherTx_Date_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <small><span id="valtxtVoucherTx_Date" style="color: red;"></span></small>
                            </div>
                        </div>
                    </div>
                    <fieldset>
                        <legend>By</legend>
                        <div id="divparticular" runat="server">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Particulars (DR)<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlLedgerDr" CssClass="form-control select1 select2" runat="server" OnSelectedIndexChanged="ddlLedgerDr_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlLedgerDr" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Current Balance<span style="color: red;"> *</span></label>
                                        <asp:Label ID="txtCurrentBalanceDr" runat="server" Text="" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                        <small><span id="valtxtCurrentBalanceDr" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Debit</label><span style="color: red;"> *</span>
                                        <asp:TextBox runat="server" CssClass="form-control" MaxLength="12" ID="txtLedgerDr_Amount" placeholder="Enter Amount..." ClientIDMode="Static" onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                        <small><span id="valtxtLedgerDr_Amount" style="color: red;"></span></small>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" ID="btnAddLedger" CssClass="btn btn-block btn-default" Text="Add" OnClick="btnAddLedger_Click" OnClientClick="return validateLedger();" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="table-responsive">

                                        <asp:GridView runat="server" CssClass="table table-bordered font" DataKeyNames="RowNo" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" ID="GridViewLedgerDetail" ShowFooter="True" OnSelectedIndexChanged="GridViewLedgerDetail_SelectedIndexChanged" OnRowDeleting="GridViewLedgerDetail_RowDeleting" OnRowDataBound="GridViewLedgerDetail_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Bind("RowNo") %>' runat="server"></asp:Label>
                                                        <%--<asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Particulars" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ledger_ID" runat="server" Text='<%# Bind("Ledger_ID") %>'></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particulars">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ledger_Name" runat="server" Text='<%# Bind("Ledger_Name") %>'></asp:Label>
                                                        <asp:Panel ID="pnlBillByBill" runat="server">
                                                            <asp:GridView runat="server" CssClass="ChildGrid Item font" GridLines="None" ShowHeader="false" Width="100%" ID="gvItem" AutoGenerateColumns="false">
                                                                <Columns>                                                          
                                                                    <asp:BoundField DataField="Item" HeaderText="Item Name"  ControlStyle-Width="40%"/>
                                                                    <asp:TemplateField HeaderText="Rate">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate")+ " " + Eval("UQCCode") %>'></asp:Label>
                                                                                                                                              
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Quantity">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity")+ " " + Eval("UQCCode") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                   
                                                                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Debit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LedgerTx_Debit" runat="server" Text='<%# Eval("LedgerTx_Debit")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Detail" Visible =" false" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="View" CssClass="label label-info"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" CssClass="label label-danger" OnClientClick="return confirm('Do you really want to delete?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </fieldset>
                    <fieldset>
                        <legend>To</legend>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Particulars (CR)<span style="color: red;"> *</span></label>
                                    <asp:DropDownList ID="ddlLedgerCr" CssClass="form-control select2" runat="server" ClientIDMode="Static">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtLedgerCr" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                    <small><span id="valddlLedgerCr" style="color: red;"></span></small>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Current Balance<span style="color: red;"> *</span></label>
                                    <asp:Label ID="txtCurrentBalanceCr" runat="server" Text="" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Credit</label><span style="color: red;"> *</span>
                                    <asp:TextBox runat="server" CssClass="form-control" MaxLength="12" ID="txtLedgerCr_Amount" placeholder="Enter Amount..." ClientIDMode="Static" onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                    <small><span id="valtxtLedgerCr_Amount" style="color: red;"></span></small>
                                </div>
                            </div>
                        </div>


                    </fieldset>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Narration<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" ID="txtVoucherTx_Narration" ClientIDMode="Static" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                <small><span id="valtxtVoucherTx_Narration" style="color: red;"></span></small>
                            </div>
                        </div>
                        <asp:Button runat="server" CssClass="hidden" ID="btnNarration" OnClick="btnNarration_Click" AccessKey="R" />
                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnAccept" ClientIDMode="Static" Text="Accept" OnClick="btnAccept_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a href="VoucherGoodsTransferInward.aspx" id="btnClear" runat="server" class="btn btn-block btn-default">Clear</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Add Item</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Item Name<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlItemName" ClientIDMode="Static" runat="server" CssClass="form-control  select2" OnSelectedIndexChanged="ddlItemName_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                                <small><span id="valddlItemName" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Quantity<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" CssClass="form-control" ClientIDMode="Static" onkeypress="return validateDecUnit(this,event)" ID="txtQuantity" placeholder="Enter Quantity..." onchange="CalculateAmount();" MaxLength="8" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                <small><span id="valtxtQuantity" style="color: red;"></span></small>
                                <asp:Label ID="lblUnit" CssClass="hidden" runat="server" Text="2"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Rate<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" MaxLength="8" CssClass="form-control" ClientIDMode="Static" ID="txtRate" placeholder="Enter Rate..." onchange="CalculateAmount();" onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
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
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-block btn-info" Text="Add Item" OnClientClick="return validateItem();" OnClick="btnAdd_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridViewItem" runat="server" ClientIDMode="Static" class="table table-bordered customCSS" Style="margin-bottom: 0px;" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItem" runat="server" Text='<%# Eval("Item").ToString()%>'></asp:Label>
                                            <asp:Label ID="lblItemID" CssClass="hidden" runat="server" Text='<%# Eval("Item_ID").ToString()%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <HeaderStyle />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate">
                                        <HeaderStyle />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate").ToString()%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount " ItemStyle-HorizontalAlign="Right">
                                        <HeaderStyle />
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount").ToString()%>'></asp:Label>
                                            <asp:TextBox ID="txtAmountH" runat="server" CssClass="hidden" Text='<%# Eval("Amount").ToString()%>'></asp:TextBox>
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
    <div class="modal fade" id="ItemViewModal" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Item Details</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                
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

        function ShowAddItemModal() {
            $('#myModal').modal('show');
        }
        function ShowItemViewModal() {
            $('#ItemViewModal').modal('show');
        }
        function validateform() {

            $("#valtxtVoucherTx_No").html("");
            $("#valtxtVoucherTx_Date").html("");
            $("#valtxtVoucherTx_Narration").html("");
            var msg = "";
            if (document.getElementById('<%=txtVoucherTx_No.ClientID%>').value.trim() == "") {
                msg += "Enter Voucher No \n";
                $("#valtxtVoucherTx_No").html("Enter Voucher No");
            }
            if (document.getElementById('<%=txtVoucherTx_Date.ClientID%>').value.trim() == "") {
                msg += "Enter Date \n";
                $("#valtxtVoucherTx_Date").html("Enter Date");
            }
            if (document.getElementById('<%=txtVoucherTx_Narration.ClientID%>').value.trim() == "") {
                msg += "Enter Narration \n";
                $("#valtxtVoucherTx_Narration").html("Enter Narration");
            }
            if (msg != "") {
                alert(msg);
                return false;

            }
            else {
                if (document.getElementById('<%=btnAccept.ClientID%>').value.trim() == "Accept") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }
                else if (document.getElementById('<%=btnAccept.ClientID%>').value.trim() == "Update") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }
            }
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
                document.getElementById('<%=txtQuantity.ClientID%>').value = "1";
                Quantity = "1";
            }
            if (TotalAmount == "")
                TotalAmount = "0";

            document.getElementById('<%=txtRate.ClientID%>').value = (TotalAmount / Quantity).toFixed(2);
        }
        function validateAmount(sender) {

            var pattern = /^[0-9]+(.[0-9]{1,3})?$/;
            var text = sender.value;
            if (text != "") {
                if (text.match(pattern) == null) {
                    alert('Please Enter Decimal Value Only.');
                    sender.value = "0";
                    CalculateGrandTotal();
                }
                else {
                    CalculateGrandTotal();
                }
            }
        }
        function validateItem() {
            debugger;
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
            if (document.getElementById('<%=txtTotalAmount.ClientID%>').value.trim() != "") {
                var i = 0;
                var Tval = 0;
                var LedgerAmount = parseFloat(document.getElementById('<%=txtLedgerDr_Amount.ClientID%>').value).toFixed(2);
                var ItemAmount = parseFloat(document.getElementById('<%=txtTotalAmount.ClientID%>').value).toFixed(2);

                $('#GridViewItem tr').each(function (index) {

                    var temp = Tval;
                    var val = $(this).children("td").eq(3).find('input[type="text"]').val();

                    if (val == "")
                        val = 0;

                    Tval = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)
                    if (Tval == "NaN")
                        Tval = 0;
                });
                LedgerAmount = parseFloat(LedgerAmount) - parseFloat(Tval);
                LedgerAmount = parseFloat(LedgerAmount.toFixed(2));
                ItemAmount = parseFloat(ItemAmount.toFixed(2));
                if (ItemAmount > LedgerAmount) {
                    msg += "Enter Valid Amount. \n";
                    $("#valtxtTotalAmount").html("Enter Valid Amount");
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
        function validateLedger() {
            var msg = "";
            $("#valddlLedgerDr").html("");
            $("#valtxtLedgerDr_Amount").html("");
            if (document.getElementById('<%=ddlLedgerDr.ClientID%>').selectedIndex == 0) {
                msg += "Select Ledger. \n";
                $("#valddlLedgerDr").html("Select Ledger");
            }
            if (document.getElementById('<%=txtLedgerDr_Amount.ClientID%>').value.trim() == "") {
                msg += "Enter Amount .";
                $("#valtxtLedgerDr_Amount").html("Enter Amount");
            }
            if (document.getElementById('<%=txtLedgerDr_Amount.ClientID%>').value.trim() != "") {
                var amt = document.getElementById('<%=txtLedgerDr_Amount.ClientID%>').value.trim();
                if (parseFloat(amt) == 0) {
                    msg += "Amount cannot be Zero.\n";
                    $("#valtxtLedgerDr_Amount").html("Amount cannot be Zero.");
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

