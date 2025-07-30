<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VoucherDebitNote.aspx.cs" Inherits="mis_Finance_VoucherDebitNote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
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
                    <div class="box box-success" style="background-color: #e3dcc5">
                        <div class="box-header">
                            <div class="row">
                                <div class="col-md-4">
                                    <h3 class="box-title">Debit Note</h3>
                                </div>
                                <div class="col-md-8">
                                    <a target="_blank" href="LedgerMasterB.aspx" class="btn btn-primary pull-right">Add Ledger</a>

                                    <asp:LinkButton ID="btnRefreshLedgerList" class="btn btn-primary pull-right Aselect1" Style="margin-right: 10px;" runat="server" OnClick="btnRefreshLedgerList_Click">Refresh Ledger</asp:LinkButton>
                                    <asp:LinkButton ID="lbkbtnAddLedger" class="btn btn-primary pull-right hidden" runat="server" OnClick="lbkbtnAddLedger_Click">Add Ledger</asp:LinkButton>
                                </div>
                            </div>
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
                                            <asp:TextBox runat="server" CssClass="form-control NameNumslashhyphenOnly" ID="txtVoucherTx_No" placeholder="Enter Voucher No..." ClientIDMode="Static" MaxLength="9" autocomplete="off"></asp:TextBox>
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
                                            <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtVoucherTx_Date" data-date-end-date="0d" placeholder="DD/MM/YYYY" MaxLength="50" autocomplete="off" OnTextChanged="txtVoucherTx_Date_TextChanged" AutoPostBack="true"></asp:TextBox>

                                        </div>
                                        <small><span id="valtxtVoucherTx_Date" style="color: red;"></span></small>
                                    </div>
                                </div>
                            </div>
                            <fieldset>
                                <legend>Particulars Detail</legend>
                                <div id="divparticular" runat="server">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Cr/Dr<span style="color: red;"> *</span></label>
                                                <asp:DropDownList ID="ddlcreditdebit" CssClass="form-control select2" runat="server">

                                                    <asp:ListItem Value="Cr">Credit</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="Dr">Debit</asp:ListItem>
                                                </asp:DropDownList>
                                                <small><span id="valddlcreditdebit" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Particulars<span style="color: red;"> *</span></label>
                                                <asp:DropDownList ID="ddlLedger_ID" CssClass="form-control select1 select2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLedger_ID_SelectedIndexChanged" ClientIDMode="Static">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                </asp:DropDownList>
                                                <small><span id="valddlLedger_ID" style="color: red;"></span></small>
                                            </div>
                                        </div>


                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Current Balance<span style="color: red;"> *</span></label>
                                                <%--<asp:TextBox runat="server" CssClass="form-control" ReadOnly="true" ID="txtCurrentBalance" placeholder="Enter Amount..." MaxLength="50" Text="0"></asp:TextBox>--%>
                                                <asp:Label ID="txtCurrentBalance" runat="server" Text="" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                                <small><span id="valtxtCurrentBalance" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label id="lblname" runat="server">Amount</label><span style="color: red;"> *</span>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtLedgerTx_Amount" MaxLength="12" placeholder="Enter Amount..." onkeypress="return validateDec(this,event);" autocomplete="off" ClientIDMode="Static" onblur="return validateAmount(this);"></asp:TextBox>
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
                                <!-- Start LedgerDetail GridView-->
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="table-responsive">

                                                <asp:GridView runat="server" CssClass="table table-bordered" DataKeyNames="RowNo" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" ID="GridViewLedgerDetail" ShowFooter="True" OnSelectedIndexChanged="GridViewLedgerDetail_SelectedIndexChanged" OnRowDeleting="GridViewLedgerDetail_RowDeleting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Bind("RowNo") %>' runat="server"></asp:Label>
                                                                <%--<asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Type" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaintainType" runat="server" Text='<%# Bind("LedgerTx_MaintainType") %>'></asp:Label>
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

                                                        <asp:TemplateField HeaderText="Bill By Bill Detail" ShowHeader="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text='<%# Eval("LedgerTx_MaintainType").ToString()=="None"?"":"View" %>' CssClass="label label-info"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
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
                                <!-- End-->
                            </fieldset>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Narration<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" ID="txtVoucherTx_Narration" TextMode="MultiLine" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
                                        <small><span id="valtxtVoucherTx_Narration" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <asp:Button runat="server" CssClass="hidden" ID="btnNarration" OnClick="btnNarration_Click" AccessKey="R" />
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ClientIDMode="Static" ID="btnAccept" Text="Accept" OnClientClick="return validateform();" OnClick="btnAccept_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="VoucherDebitNote.aspx" id="btnClear" runat="server" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <!--Start Add BillByBillDetail Modal -->
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
                                <asp:DropDownList runat="server" CssClass="form-control select1" ID="ddlRefType" OnSelectedIndexChanged="ddlRefType_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value="2">New Ref</asp:ListItem>
                                    <asp:ListItem Value="1">Agst Ref</asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valddlRefType" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Name<span style="color: red;">*</span></label>
                                <asp:HyperLink ID="lnkView" onclick="ShowRefDetailModal();" Visible="false" runat="server">View AgstRef</asp:HyperLink>
                                <%--  <asp:LinkButton ID="lnkView" runat="server" OnClick="lnkView_Click" Visible="false" >View AgstRef</asp:LinkButton>--%>
                                <asp:TextBox runat="server" ID="txtBillByBillTx_Ref" ClientIDMode="Static" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                <asp:DropDownList runat="server" CssClass="form-control select2" Visible="false" ID="ddlBillByBillTx_Ref" onchange="ChangeRef()">
                                </asp:DropDownList>
                                <small><span id="valddlBillByBillTx_Ref" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Amount<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" ID="txtBillByBillTx_Amount" MaxLength="12" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                <small><span id="valtxtBillByBillTx_Amount" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Cr/Dr<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlBillByBillTx_crdr" ClientIDMode="Static" CssClass="form-control" runat="server">
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
                            <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GridViewBillByBillDetail" AutoGenerateColumns="false">
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
                                    <%--<asp:BoundField DataField="Type" HeaderText="Cr/Dr" ItemStyle-Width="5%" HeaderStyle-Width="5%" />--%>
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

    <!--Start ViewBillByBillDetail Modal-->
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
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BillByBillTx_RefType" HeaderText="Type of Ref" ItemStyle-Width="10%" />
                                        <asp:BoundField DataField="BillByBillTx_Ref" HeaderText="Name" />
                                        <asp:BoundField DataField="BillByBillTx_Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="18%" />
                                        <asp:BoundField DataField="Type" HeaderText="Cr/Dr" ItemStyle-Width="5%" />
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

    <!--Start Add ChequeDetail Modal-->
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
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtChequeTx_Amount" MaxLength="12" placeholder="Enter Amount" onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
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
                    <%--<asp:Button runat="server" Text="Add" ID="btnAddChequeDetail" ClientIDMode="Static" CssClass="btn btn-success" OnClick="btnAddChequeDetail_Click"></asp:Button>--%>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <!-- End-->

    <!--Start ViewChequeDetail Modal-->
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>
        function ShowBillDetailModal() {
            $('#myModal').modal('show');
            $("#ddlBillByBillTx_Ref").hide();
        }
        function ShowBillByBillViewModal() {
            $('#BillByBillViewModal').modal('show');
        }
        function ShowModalChequeDetail() {
            $('#ModalChequeDetail').modal('show');
        }
        function ShowModalChequeDetailView() {
            $('#ModalChequeDetailView').modal('show');
        }
        function ShowRefDetailModal() {
            $('#AgstRefModal').modal('show');

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
        function ChangeAmount() {

            debugger;
            var LedgerAmount = document.getElementById('<%=txtLedgerTx_Amount.ClientID%>').value;
            var BillByBillAmount = document.getElementById('<%=txtBillByBillTx_Amount.ClientID%>').value;
            var grid = document.getElementById('<%= GridViewBillByBillDetail.ClientID %>');
            var FooterTextBoxName = grid.getElementsByTagName('Label5');
            if (FooterTextBoxName != null) {
                if (FooterTextBoxName < BillByBillAmount) {
                    $("#txtBillByBillTx_Amount").val(FooterTextBoxName);
                }
            }
            else if (LedgerAmount < BillByBillAmount) {
                $("#txtBillByBillTx_Amount").val($("#txtLedgerTx_Amount").val());
            }


        }

        //Start Validate ReceiptForm
        function validateform() {
            var msg = "";
            $("#valtxtVoucherTx_No").html("");
            $("#valtxtVoucherTx_Date").html("");
            $("#valtxtVoucherTx_Narration").html("");


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
    //End

    //Start Validate BillByBillDetail
    function validateBillByBill() {
        var msg = "";
        $("#valddlBillByBillTx_Ref").html("");
        $("#valtxtBillByBillTx_Amount").html("");
        $("#valddlBillByBillTx_crdr").html("");
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
    //End

    //Start Validate LedgerDetail
    function validateLedger() {
        debugger;
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
        var Element = document.getElementById('<%=txtVoucherTx_No.ClientID%>')
        if (Element != null) {
            if (document.getElementById('<%=txtVoucherTx_No.ClientID%>').value.trim() == "") {
                    msg += "Enter Voucher No \n";
                    $("#valtxtVoucherTx_No").html("Enter Voucher No.");
                }
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
        //End

        //Start Validate ChequeDetail
        function validateCheque() {
            debugger;
            var msg = "";
            $("#valtxtChequeTx_No").html("")
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
        //End


        $('.NameNumslashhyphenOnly').keypress(function (e) {
            debugger;//.:46, >:62, , :44, #:35
            if (!e) e = window.event;
            if (e.charCode) {
                // alert(e.charCode);
                if ((e.charCode < 48 || (e.charCode > 57 && e.charCode < 65) || (e.charCode > 90 && e.charCode < 97) || e.charCode > 122) && e.charCode != 32 && e.charCode != 95 && e.charCode != 46) {
                    if (e.preventDefault) e.preventDefault();
                }

            }
        });

        function validateNum1(evt) {
            debugger;
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
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
