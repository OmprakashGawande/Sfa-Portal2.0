<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VoucherContraD.aspx.cs" Inherits="mis_Finance_VoucherContraD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Contra Voucher</h3>
                    <asp:LinkButton ID="lbkbtnAddLedger" class="btn btn-primary pull-right hidden" runat="server" OnClick="lbkbtnAddLedger_Click">Add Ledger</asp:LinkButton>
                   <%-- <a href="LedgerMasterB.aspx" class="btn btn-primary pull-right"><i class="fa fa-plus-circle"></i> Add Ledger</a>--%>
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
                                <asp:TextBox runat="server" CssClass="form-control" ClientIDMode="Static" ID="txtVoucherTx_No" placeholder="Enter Voucher No..." MaxLength="50"></asp:TextBox>
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
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVoucherTx_No" placeholder="Enter Voucher/Bill No." ClientIDMode="Static" MaxLength="10" autocomplete="off" onkeypress="return abc(event);"></asp:TextBox>

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
                                    <asp:TextBox runat="server" ClientIDMode="Static" data-date-end-date="0d" CssClass="form-control DateAdd" ID="txtVoucherTx_Date" placeholder="DD/MM/YYYY" OnTextChanged="txtVoucherTx_Date_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <small><span id="valtxtVoucherTx_Date" style="color: red;"></span></small>
                            </div>
                        </div>
                    </div>
                    <fieldset>
                        <legend>Particulars Detail</legend>
                        <div id="divparticular" runat="server" >
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Cr/Dr<span style="color: red;"> *</span></label>
                                    <asp:DropDownList ID="ddlcreditdebit" CssClass="form-control select2" runat="server" onchange="ChangeRef();">

                                        <asp:ListItem Value="Cr">Credit</asp:ListItem>
                                        <asp:ListItem Value="Dr">Debit</asp:ListItem>
                                    </asp:DropDownList>
                                    <small><span id="valddlcreditdebit" style="color: red;"></span></small>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Particulars<span style="color: red;"> *</span>
                                         (
                                            <asp:ImageButton ID="btnRefreshLedgerList" runat="server" ImageUrl="css/btnRefresh.png" CssClass="Aselect1" AlternateText="Refresh" Style="height: 13px; border-radius: 3px; border-color: white;" OnClick="btnRefreshLedgerList_Click" />
                                            )
                                    </label>
                                    <asp:DropDownList ID="ddlLedger_ID" CssClass="form-control Aselect1 select2" runat="server" OnSelectedIndexChanged="ddlLedger_ID_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                    <small><span id="valddlLedger_ID" style="color: red;"></span></small>
                                </div>
                            </div>


                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Current Balance<span style="color: red;"> *</span></label><br />
                                    <%--<asp:TextBox runat="server" CssClass="form-control" ReadOnly="true" ID="txtCurrentBalance" placeholder="Enter Amount..." MaxLength="50" Text="0" ></asp:TextBox>--%>
                                    <asp:Label ID="txtCurrentBalance" runat="server" Text="" CssClass="form-control" style="background-color:#eee;"></asp:Label>
                                    <small><span id="valtxtCurrentBalance" style="color: red;"></span></small>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Amount</label><span style="color: red;"> *</span>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtLedgerTx_Amount" placeholder="Enter Amount..." MaxLength="12" onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                    <small><span id="valtxtLedgerTx_Amount" style="color: red;"></span></small>
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

                                        <asp:GridView runat="server" CssClass="table table-bordered" DataKeyNames="RowNo" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" ID="GridViewLedgerDetail" ShowFooter="True" OnSelectedIndexChanged="GridViewLedgerDetail_SelectedIndexChanged" OnRowDeleting="GridViewLedgerDetail_RowDeleting" OnRowDataBound="GridViewLedgerDetail_RowDataBound" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.NO" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        
                                                        <asp:Label ID="lblRowNumber"  Text='<%# Bind("RowNo") %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Type" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                                        <asp:Label ID="LedgerTx_MaintainType" Visible="false" runat="server" Text='<%# Bind("LedgerTx_MaintainType") %>'></asp:Label>
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
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Debit" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LedgerTx_Debit" runat="server" Visible='<%# ((decimal)Eval("LedgerTx_Debit") == 0)?false:true  %>' Text='<%# Eval("LedgerTx_Debit")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Credit" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LedgerTx_Credit" runat="server" Text='<%# Eval("LedgerTx_Credit")%>' Visible='<%# ((decimal)Eval("LedgerTx_Credit") == 0)?false:true %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="Ledger wise Detail" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ledger_TableID" runat="server" Text='<%# Bind("Ledger_TableID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Cheque Detail" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text='<%# Eval("LedgerTx_MaintainType").ToString()=="None"?"":"View" %>' CssClass="label label-info" ></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="10%">
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


                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Narration<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" ID="txtVoucherTx_Narration" TextMode="MultiLine" ClientIDMode="Static" Rows="18" CssClass="form-control"></asp:TextBox>
                                <small><span id="valtxtVoucherTx_Narration" style="color: red;"></span></small>
                            </div>
                        </div>
                        <asp:Button runat="server" CssClass="hidden" ID="btnNarration"  OnClick="btnNarration_Click" AccessKey="R" />

                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-success" ClientIDMode="Static" ID="btnAccept" Text="Accept" OnClick="btnAccept_Click" OnClientClick="return validateform();"/>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a href="VoucherContraD.aspx" id="btnClear" runat="server" class="btn btn-block btn-default">Clear</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

    <!-- Modal -->
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
                                <label>Cheque/ DD No/RTGS/NEFT</label>
                                <asp:TextBox ID="txtChequeTx_No" runat="server" placeholder="Enter Cheque/ DD No." MaxLength="22" CssClass="form-control" onkeypress="return validateNum(event);" autocomplete="off"></asp:TextBox>
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
                                    <%-- <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtChequeTx_Date"  placeholder="DD/MM/YYYY" data-date-start-date="-89d" autocomplete="off"></asp:TextBox>--%>
                                     <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtChequeTx_Date"  placeholder="DD/MM/YYYY"  autocomplete="off"></asp:TextBox>
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
                               <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GVFinChequeTx"  AutoGenerateColumns="false" ClientIDMode="Static">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:BoundField DataField="ChequeTx_No" HeaderText="Cheque/ DD No." />--%>
                                        <asp:TemplateField HeaderText="Cheque/ DD No/RTGS/NEFT">
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
                    <%--<asp:Button runat="server" Text="Add" ID="btnAddChequeDetail" ClientIDMode="Static" CssClass="btn btn-success" OnClick="btnAddChequeDetail_Click"></asp:Button>--%>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
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
                                        <asp:BoundField DataField="ChequeTx_No" HeaderText="Cheque/ DD No/RTGS/NEFT" />
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ShowModalChequeDetail() {
            $('#ModalChequeDetail').modal('show');
        }
        function ShowModalChequeDetailView() {
            $('#ModalChequeDetailView').modal('show');
        }
        function validateform() {
            debugger;
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
                    return true;

                }
                else if (document.getElementById('<%=btnAccept.ClientID%>').value.trim() == "Update") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true;

                }

            }
        }
        function validateLedger() {
            $("#valddlLedger_ID").html("");
            $("#valtxtLedgerTx_Amount").html("");
            var msg = "";
            if (document.getElementById('<%=ddlLedger_ID.ClientID%>').selectedIndex == 0) {
                msg += "Select Particulars  \n";
                $("#valddlLedger_ID").html("Select Particulars");
            }
            if (document.getElementById('<%=txtLedgerTx_Amount.ClientID%>').value.trim() == "") {
                msg += "Enter Amount \n";
                $("#valtxtLedgerTx_Amount").html("Enter Amount");
            }
            if (document.getElementById('<%=txtLedgerTx_Amount.ClientID%>').value.trim() != "") {
                var amt = document.getElementById('<%=txtLedgerTx_Amount.ClientID%>').value.trim();
                if (parseFloat(amt) == 0) {
                    msg += "Amount cannot be Zero.\n";
                    $("#valtxtLedgerTx_Amount").html("Amount cannot be Zero.");
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
        function validateCheque() {
            debugger;
            var msg = "";
            $("#valtxtChequeTx_No").html("");
            //$("#valtxtChequeTx_Date").html("");
            $("#valtxtChequeTx_Amount").html("");
            var CheckNo = document.getElementById('<%=txtChequeTx_No.ClientID%>').value.trim();
            //if (document.getElementById('<%=txtChequeTx_No.ClientID%>').value.trim() != "") {
                //if (CheckNo.length != 6) {
                   // msg += "Enter 6 Digit Cheque/ DD No.  \n";
                   // $("#valtxtChequeTx_No").html("Enter  6 Digit Cheque/ DD No");
                //}
            //}

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
                var LedgerAmount = parseFloat(document.getElementById('<%=txtLedgerTx_Amount.ClientID%>').value);
                var ChequeAmount = parseFloat(document.getElementById('<%=txtChequeTx_Amount.ClientID%>').value);

                $('#GVFinChequeTx tr').each(function (index) {

                    var temp = Tval;
                    var val = $(this).children("td").eq(3).find('input[type="text"]').val();

                    if (val == "")
                        val = 0;

                    Tval = parseFloat(parseFloat(temp) + parseFloat(val)).toFixed(2)
                    if (Tval == "NaN")
                        Tval = 0;
                });
                LedgerAmount = parseFloat(LedgerAmount) - parseFloat(Tval);
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
        function validateAmount(sender) {

            var pattern = /^[0-9]+(.[0-9]{1,2})?$/;
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
    </script>

</asp:Content>



