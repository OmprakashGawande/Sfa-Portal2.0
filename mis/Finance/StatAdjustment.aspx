<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="StatAdjustment.aspx.cs" Inherits="mis_Finance_StatAdjustment" %>

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
            <div class="box box-success" style="background-color: #f7e3c6">
                <div class="box-header">
                    <h3 class="box-title">Journal Voucher</h3>
                    <asp:LinkButton ID="lbkbtnAddLedger" class="btn btn-primary pull-right" runat="server" OnClick="lbkbtnAddLedger_Click">Add Ledger</asp:LinkButton>
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
                                    <asp:TextBox runat="server" CssClass="form-control NameNumslashhyphenOnly" ID="txtVoucherTx_No" placeholder="Enter Voucher No..." ClientIDMode="Static" autocomplete="off"></asp:TextBox>
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
                    <div class="row">


                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Type of duty/Tax</label>
                                <asp:DropDownList ID="ddldutytax" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="GST">GST</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Nature Of Adjustment<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlNatofadj" ClientIDMode="Static" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="Increase Of Tax Liability">Increase Of Tax Liability</asp:ListItem>
                                    <asp:ListItem Value="Increase Of Input Tax Credit">Increase Of Input Tax Credit</asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valddlNatofadj" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Additional Details<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlAdddetls" runat="server" ClientIDMode="Static" CssClass="form-control">
                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                    <asp:ListItem Value="Purchase Under Reverse Charges">Purchase Under Reverse Charges</asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valddlAdddetls" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Percent(%)<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlPer" runat="server" ClientIDMode="Static" CssClass="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="0">0%</asp:ListItem>
                                    <asp:ListItem Value="5">5%</asp:ListItem>
                                    <asp:ListItem Value="12">12%</asp:ListItem>
                                    <asp:ListItem Value="18">18%</asp:ListItem>
                                    <asp:ListItem Value="28">28%</asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valddlPer" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Taxable Value<span style="color: red;"> *</span></label>
                                <asp:TextBox ID="txtTaxableValue" runat="server" ClientIDMode="Static" CssClass="form-control">                                   
                                </asp:TextBox>
                                <small><span id="valtxtTaxableValue" style="color: red;"></span></small>
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
                                        <asp:DropDownList ID="ddlcreditdebit" CssClass="form-control select2" runat="server" onchange="ChangeRef();">
                                            <asp:ListItem Value="Cr">Credit</asp:ListItem>
                                            <asp:ListItem Value="Dr">Debit</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlcreditdebit" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Particulars<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlLedger_ID" CssClass="form-control select1 select2" runat="server" OnSelectedIndexChanged="ddlLedger_ID_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlLedger_ID" style="color: red;"></span></small>
                                    </div>
                                </div>


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Current Balance<span style="color: red;"> *</span></label>
                                        <asp:Label ID="txtCurrentBalance" runat="server" Text="" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                        <small><span id="valtxtCurrentBalance" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Amount</label><span style="color: red;"> *</span>
                                        <asp:TextBox runat="server" CssClass="form-control" MaxLength="12" ID="txtLedgerTx_Amount" placeholder="Enter Amount..." ClientIDMode="Static" onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
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

                                        <asp:GridView runat="server" CssClass="table table-bordered" DataKeyNames="RowNo" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" ID="GridViewLedgerDetail" ShowFooter="True" OnRowDeleting="GridViewLedgerDetail_RowDeleting">
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
                                                <asp:TemplateField HeaderText="Debit" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LedgerTx_Debit" runat="server" Visible='<%# ((decimal)Eval("LedgerTx_Debit") == 0)?false:true  %>' Text='<%# Eval("LedgerTx_Debit")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Credit" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LedgerTx_Credit" runat="server" Text='<%# Eval("LedgerTx_Credit")%>' Visible='<%# ((decimal)Eval("LedgerTx_Credit") == 0)?false:true %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Ledger wise Detail" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ledger_TableID" runat="server" Text='<%# Bind("Ledger_TableID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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
                                <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnAccept" ClientIDMode="Static" Text="Accept" OnClick="btnAccept_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a href="VoucherJournal.aspx" id="btnClear" runat="server" class="btn btn-block btn-default">Clear</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

    <!-- Modal -->


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>

        //ON Selecting AgstRef


        function validateform() {

            $("#valtxtVoucherTx_No").html("");
            $("#valtxtVoucherTx_Date").html("");
            $("#valtxtVoucherTx_Narration").html("");
            $("#valddlNatofadj").html("");
            $("#valddlAdddetls").html(""); 
            $("#valddlPer").html("");
            $("#valtxtTaxableValue").html("");
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

            if (document.getElementById('<%=ddlNatofadj.ClientID%>').selectedIndex == 0) {
                msg += "Select  Nature Of Adjustment. \n";
                $("#valddlNatofadj").html("Select Nature Of Adjustment");
            }
            if (document.getElementById('<%=ddlAdddetls.ClientID%>').selectedIndex == 0) {
                msg += "Select  Additional Details. \n";
                $("#valddlAdddetls").html("Select Additional Details");
            }
            if (document.getElementById('<%=ddlPer.ClientID%>').selectedIndex == 0) {
                msg += "Select Percentage(%). \n";
                $("#valddlPer").html("Select Percentage(%)");
            }
            if (document.getElementById('<%=txtTaxableValue.ClientID%>').value.trim() == "") {
                msg += "Enter Taxable Value. \n";
                $("#valtxtTaxableValue").html("Enter Taxable Value");
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

