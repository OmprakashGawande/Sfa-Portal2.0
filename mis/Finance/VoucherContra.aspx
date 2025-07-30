<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VoucherContra.aspx.cs" Inherits="mis_Finance_VoucherContra" %>

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
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Voucher No.<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtVoucherTx_No" placeholder="Enter Voucher No..." MaxLength="50"></asp:TextBox>
                                <small><span id="valtxtVoucherTx_No" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Date<span style="color: red;"> *</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtVoucherTx_Date" placeholder="DD/MM/YYYY"></asp:TextBox>
                                </div>
                                <small><span id="valtxtVoucherTx_Date" style="color: red;"></span></small>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Account<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlLedger_ID" CssClass="form-control select2" runat="server">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valddlLedger_ID" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Current Balance<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCurrentBalance" placeholder="Enter Current Balance..." MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <fieldset>
                        <legend>Particulars Detail</legend>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Particulars<span style="color: red;"> *</span></label>
                                    <asp:DropDownList ID="ddlsubLedger_ID" CssClass="form-control select2" runat="server" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <small><span id="valddlsubLedger_ID" style="color: red;"></span></small>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Amount<span style="color: red;"> *</span></label>
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtLedgerTx_Amount" placeholder="Enter Amount..." MaxLength="50"></asp:TextBox>
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
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" CssClass="table table-bordered" DataKeyNames="Ledger_TableID" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" ID="GridViewLedgerDetail" ShowFooter="True" OnSelectedIndexChanged="GridViewLedgerDetail_SelectedIndexChanged" OnRowDeleting="GridViewLedgerDetail_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particulars" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="subLedger_ID" runat="server" Text='<%# Bind("subLedger_ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Particulars">
                                                    <ItemTemplate>
                                                        <asp:Label ID="subLedger_Name" runat="server" Text='<%# Bind("subLedger_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LedgerTx_Amount" runat="server" Text='<%# Bind("LedgerTx_Amount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:BoundField DataField="subLedger_ID" HeaderText="Particulars" />
                                                        <asp:BoundField DataField="subLedger_Name" HeaderText="Particulars" />
                                                        <asp:BoundField DataField="LedgerTx_Amount" HeaderText="Amount" />--%>

                                                <asp:TemplateField HeaderText="Ledger wise Detail" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ledger_TableID" runat="server" Text='<%# Bind("Ledger_TableID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cheque Detail" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="View" CssClass="label label-info"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" CssClass="label label-danger"></asp:LinkButton>
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
                                <label>Narration</label>
                                <asp:TextBox runat="server" ID="txtVoucherTx_Narration" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-success" ClientIDMode="Static" ID="btnAccept" Text="Accept" OnClientClick="return validateform();" OnClick="btnAccept_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a href="VoucherContra.aspx" class="btn btn-block btn-default">Clear</a>
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
                                <label>Cheque/ DD No.<span style="color: red;"> *</span></label>
                                <asp:TextBox ID="txtChequeTx_No" runat="server" placeholder="Enter Cheque/ DD No." MaxLength="12" CssClass="form-control"></asp:TextBox>
                                <small><span id="valtxtChequeTx_No" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Cheque/ DD Date<span style="color: red;"> *</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtChequeTx_Date" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
                                </div>
                                <small><span id="valtxtChequeTx_Date" style="color: red;"></span></small>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Amount<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtChequeTx_Amount" placeholder="Enter Amount"></asp:TextBox>
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
                                <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GVFinChequeTx" AutoGenerateColumns="false" ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ChequeTx_No" HeaderText="Cheque/ DD No." />
                                        <asp:BoundField DataField="ChequeTx_Date" HeaderText="Cheque/ DD Date" />
                                        <asp:BoundField DataField="ChequeTx_Amount" HeaderText="Amount" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" Text="Add" ID="btnAddChequeDetail" OnClick="btnAddChequeDetail_Click" ClientIDMode="Static" CssClass="btn btn-success" ></asp:Button>
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
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ChequeTx_No" HeaderText="Cheque/ DD No." />
                                        <asp:BoundField DataField="ChequeTx_Date" HeaderText="Cheque/ DD Date" />
                                        <asp:BoundField DataField="ChequeTx_Amount" HeaderText="Amount" />
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
            $("#valtxtVoucherTx_No").html("");
            $("#valtxtVoucherTx_Date").html("");
            $("#valddlLedger_ID").html("");
            //$("#valtxtCurrentBalance").html("");
            var msg = "";
            if (document.getElementById('<%=txtVoucherTx_No.ClientID%>').value.trim() == "") {
                msg += "Enter Voucher No \n";
                $("#valtxtVoucherTx_No").html("Enter Voucher No");
            }
            if (document.getElementById('<%=txtVoucherTx_Date.ClientID%>').value.trim() == "") {
                msg += "Enter Date \n";
                $("#valtxtVoucherTx_Date").html("Enter Date");
            }
            if (document.getElementById('<%=ddlLedger_ID.ClientID%>').selectedIndex == 0) {
                msg += "Select Account \n";
                $("#valddlLedger_ID").html("Select Account");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Save Details ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        function validateLedger() {
            $("#valddlsubLedger_ID").html("");
            $("#valtxtLedgerTx_Amount").html("");
            var msg = "";
            if (document.getElementById('<%=ddlsubLedger_ID.ClientID%>').selectedIndex == 0) {
                msg += "Select Particulars  \n";
                $("#valddlsubLedger_ID").html("Select Particulars");
            }
            if (document.getElementById('<%=txtLedgerTx_Amount.ClientID%>').value.trim() == "") {
                msg += "Enter Amount \n";
                $("#valtxtLedgerTx_Amount").html("Enter Amount");
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
            var msg = "";
            $("#valtxtChequeTx_No").html("");
            $("#valtxtChequeTx_Date").html("");
            $("#valtxtChequeTx_Amount").html("");
            if (document.getElementById('<%=txtChequeTx_No.ClientID%>').value.trim() == "") {
                msg += "Enter Cheque/ DD No.  \n";
                $("#valtxtChequeTx_No").html("Enter Cheque/ DD No");
            }
            if (document.getElementById('<%=txtChequeTx_Date.ClientID%>').value.trim() == "") {
                msg += "Enter Cheque/ DD Date. \n";
                $("#valtxtChequeTx_Date").html("Enter Cheque/ DD Date");
            }
            if (document.getElementById('<%=txtChequeTx_Amount.ClientID%>').value.trim() == "") {
                msg += "Enter Amount. \n";
                $("#valtxtChequeTx_Amount").html("Enter Amount");
            }
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

