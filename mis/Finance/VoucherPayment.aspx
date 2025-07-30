<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VoucherPayment.aspx.cs" Inherits="mis_Finance_VoucherPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Payment Voucher</h3>
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
                                            <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtVoucherTx_Date" placeholder="DD/MM/YYYY" MaxLength="50"></asp:TextBox>

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
                                        </asp:DropDownList>
                                        <small><span id="valddlLedger_ID" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Current Balance<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtCurrentBalance" placeholder="Enter Current Balance..." onkeypress="if (event.keyCode < 46 || event.keyCode > 57) event.returnValue = false;"></asp:TextBox>
                                        <small><span id="valtxtCurrentBalance" style="color: red;"></span></small>
                                    </div>
                                </div>
                            </div>

                            <fieldset>
                                <legend>Particulars Detail</legend>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Particulars<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlsubLedger_ID" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlsubLedger_ID_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <small><span id="valddlsubLedger_ID" style="color: red;"></span></small>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Amount<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtLedgerTx_Amount" ClientIDMode="Static" placeholder="Enter Amount..." MaxLength="50"></asp:TextBox>
                                            <small><span id="valtxtLedgerTx_Amount" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <asp:Button runat="server" CssClass="btn btn-block btn-default" ID="btnAddLedger" ClientIDMode="Static" OnClick="btnAddLedger_Click" OnClientClick="return validateLedger();" Text="Add" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="table-responsive">
                                                <asp:GridView runat="server" CssClass="table table-bordered" DataKeyNames="BillByBillTx_TableID" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" ID="GridViewLedgerDetail" ShowFooter="True" OnSelectedIndexChanged="GridViewLedgerDetail_SelectedIndexChanged" OnRowDeleting="GridViewLedgerDetail_RowDeleting">
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

                                                        <asp:TemplateField HeaderText="Bill wise Detail" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="BillByBillTx_TableID" runat="server" Text='<%# Bind("BillByBillTx_TableID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bill By Bill Detail" ShowHeader="False">
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
                                        <asp:TextBox runat="server" TextMode="MultiLine" CssClass="form-control" ID="txtVoucherTx_Narration"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ClientIDMode="Static" ID="btnAccept" OnClick="btnAccept_Click" Text="Accept" OnClientClick="return validateform();" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="VoucherPayment.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <!-- Modal -->
    <div class="modal fade" id="myModal" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Bill-wise Details [<a onclick="ShowModalBillByBillDetail();">View Agains Ref Detail</a>]</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Type of Ref<span style="color: red;"> *</span></label>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRefType" onchange="ChangeRef();">
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
                                <asp:DropDownList runat="server" CssClass="form-control" ClientIDMode="Static" ID="ddlBillByBillTx_Ref">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Ref 1</asp:ListItem>
                                    <asp:ListItem Value="2">Ref 2</asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valddlBillByBillTx_Ref" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Amount<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" ID="txtBillByBillTx_Amount" ClientIDMode="Static" CssClass="form-control" onkeypress="return validateDec(this,event);"></asp:TextBox>
                                <small><span id="valtxtBillByBillTx_Amount" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button runat="server" Text="Add" ID="btnAddBillByBill" ClientIDMode="Static" CssClass="btn btn-block btn-default" OnClick="btnAddBillByBill_Click" OnClientClick="return validateBillByBill();"></asp:Button>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GridViewBillByBillDetail" AutoGenerateColumns="false" ShowFooter="True">
                                <Columns>
                                    <asp:BoundField DataField="BillByBillTx_RefType" HeaderText="Type of Ref" />
                                    <asp:BoundField DataField="BillByBillTx_Ref" HeaderText="Name" />
                                    <asp:TemplateField HeaderText="Amount" SortExpression="leftBonus">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("BillByBillTx_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" Text="Add" ID="btnBillByBillSave" OnClick="btnBillByBillSave_Click" ClientIDMode="Static" CssClass="btn btn-success"></asp:Button>

                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

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
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BillByBillTx_RefType" HeaderText="Type of Ref" />
                                        <asp:BoundField DataField="BillByBillTx_Ref" HeaderText="Name" />
                                        <asp:BoundField DataField="BillByBillTx_Amount" HeaderText="Amount" />
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
    <div class="modal fade" id="ModalBillByBillDetail" role="dialog">
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
                                <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GVBillByBillDetailAtModal" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="BillByBillTx_Ref" HeaderText="BillByBillTx_Ref" />
                                        <asp:BoundField DataField="BillByBillTx_Amount" HeaderText="BillByBillTx_Amount" />
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
        function ShowBillDetailModal() {
            $('#myModal').modal('show');
            $("#ddlBillByBillTx_Ref").hide();
        }
        function ShowBillByBillViewModal() {
            $('#BillByBillViewModal').modal('show');
        }
        function ShowModalBillByBillDetail() {
            $('#ModalBillByBillDetail').modal('show');
        }

        function ChangeRef() {
            $("#txtBillByBillTx_Ref").hide();
            $("#ddlBillByBillTx_Ref").hide();
            $("#valddlBillByBillTx_Ref").html("");

            var RefType = document.getElementById('<%=ddlRefType.ClientID%>').selectedIndex;

            if (RefType == 0 || RefType == 2) {
                $("#txtBillByBillTx_Ref").show();
            }
            else if (RefType == 1) {
                $("#ddlBillByBillTx_Ref").show();
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
        function validateform() {
            var msg = "";
            $("#valtxtVoucherTx_No").html("");
            $("#valtxtVoucherTx_Date").html("");
            $("#valddlLedger_ID").html("");
            $("#valtxtCurrentBalance").html("");
            $("#valddlsubLedger_ID").html("");
            $("#valtxtLedgerTx_Amount").html("");
            
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
        function validateBillbyBillSave() {
            if (document.getElementById('<%=btnBillByBillSave.ClientID%>').value.trim() == "Add") {
                if (confirm("Do you really want to Save Details ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

    </script>

</asp:Content>

