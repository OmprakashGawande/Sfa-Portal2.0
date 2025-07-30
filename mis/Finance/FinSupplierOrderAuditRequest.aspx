<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinSupplierOrderAuditRequest.aspx.cs" Inherits="mis_Finance_FinSupplierOrderAuditRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .form-control-Modal {
            display: block;
            width: 100%;
            height: 26px;
            padding: 5px 5px;
            font-size: 12px;
            line-height: 1;
            color: #555;
            background-color: #cccccc2b;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgb(0 0 0 / 8%);
            box-shadow: inset 0 1px 1px rgb(0 0 0 / 8%);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
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
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">	Audit Supplier Order Requests  (By Bill Date)</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>From Date<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtFromDate" placeholder="Enter From Date..." autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>To Date<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtToDate" placeholder="Enter To Date..." autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Status<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="All">All</asp:ListItem>
                                            <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                            <asp:ListItem Value="Approve">Approve</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>



                            <div class="row">
                                <div class="col-md-12">
                                    <h4 class="box-title">Audit Supplier Order Requests By Bill Date</h4>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" DataKeyNames="OrderID" AutoGenerateColumns="False" EmptyDataText="No records Found" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Plant" HeaderText="Plant" />
                                                <asp:BoundField DataField="Item" HeaderText="Item" />
                                                <asp:BoundField DataField="Supplier_Item" HeaderText="Supplier / Item" />
                                                <asp:BoundField DataField="OrderNo_OrderDate" HeaderText="ORDER NO / ORDER DATE" />
                                                <asp:BoundField DataField="Unit" HeaderText="UNIT" />
                                                <asp:BoundField DataField="QTY_ORDEERED" HeaderText="QTY. ORDEERED" />
                                                <asp:BoundField DataField="RATE_INCLUSIVE_OF_ALL" HeaderText="RATE INCLUSIVE OF ALL" />
                                                <asp:BoundField DataField="GST_Per" HeaderText="GST %" />
                                                <asp:BoundField DataField="QTY_RECEIVED" HeaderText="QTY. RECEIVED" />
                                                <asp:BoundField DataField="AccountNetPayment" HeaderText="Net Payble Amount" />
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" Text='<%# Eval("AuditStatus").ToString()%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Select" runat="server" CssClass="label label-info" CausesValidation="False" CommandName="Select" Text="Action"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal -->
            <div class="modal fade" id="myModal" role="dialog">
                <div class="modal-dialog modal-lg">
                    <%--<div class="modal-dialog">--%>
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Payment Approval Details </h4>
                        </div>
                        <div class="modal-body">
                            <fieldset>
                                <legend>Supplier Order</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>Order No.</label><br />
                                        <asp:Label ID="lblOrderNo" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Order Date</label><br />
                                        <asp:Label ID="lblOrderDate" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Supplier Name</label><br />
                                        <asp:Label ID="lblSupplierName" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Plant </label>
                                        <br />
                                        <asp:Label ID="lblPlantName" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>Item</label><br />
                                        <asp:Label ID="lblItemName" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <label>
                                            Rate Inclusive of All(Unit :
                                            <asp:Label ID="lblUnit" runat="server" Style="color: red;" Text=""></asp:Label>)</label><br />
                                        <asp:Label ID="lblItemRate" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Item Quantity</label><br />
                                        <asp:Label ID="lblQuantity" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Cess Applicable </label>
                                        <br />
                                        <asp:Label ID="lblCessApplicable" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <label>Cess Rate </label>
                                        <br />
                                        <asp:Label ID="lblCessRate" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <label>GST Applicable</label><br />
                                        <asp:Label ID="lblGSTApplicable" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Supplier Name</label><br />
                                        <asp:Label ID="lblGSTRate" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>GST Type </label>
                                        <br />
                                        <asp:Label ID="lblGSTType" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>GST Include in Deduction  </label>
                                        <br />
                                        <asp:Label ID="lblGSTIncInDeduction" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>INVOICE PROCESS</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>Quality Clean Report</label><br />
                                        <asp:Label ID="lblQualityCleanReport" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Bill No. </label>
                                        <br />
                                        <asp:Label ID="lblBillNo" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Bill Date</label><br />
                                        <asp:Label ID="lblBillDate" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Quantity Receive Date </label>
                                        <br />
                                        <asp:Label ID="lblQuantityReceiveDate" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>Quantity Receive</label><br />
                                        <asp:Label ID="lblQuantityReceive" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Diff. Quantity</label><br />
                                        <asp:Label ID="lblDiffQuantity" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Basic Rate</label><br />
                                        <asp:Label ID="lblBasicRate" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>GST </label>
                                        <br />
                                        <asp:Label ID="lblGST" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>Basic Amount</label><br />
                                        <asp:Label ID="lblBasicAmount" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>GST Amount</label><br />
                                        <asp:Label ID="lblGSTAmount" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Cess</label><br />
                                        <asp:Label ID="lblCess" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Total Amount </label>
                                        <br />
                                        <asp:Label ID="lblTotalAmount" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>Deduction Proposed (%)</label><br />
                                        <asp:Label ID="lblDeductionProposed" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Deduction Amount</label><br />
                                        <asp:Label ID="lblDeductionAmount" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Extra Supply Deduction</label><br />
                                        <asp:Label ID="lblExtraSupDed" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>TDS % </label>
                                        <br />
                                        <asp:Label ID="lblTDSPer" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>TCS</label><br />
                                        <asp:Label ID="lblTCS" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>TDS </label>
                                        <br />
                                        <asp:Label ID="lblTDS" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                
                                    <div class="col-md-3">
                                        <label>Round</label><br />
                                        <asp:Label ID="lblRoundOff" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Net Payment</label><br />
                                        <asp:Label ID="lblNetPayment" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <label>Remark</label><br />
                                        <asp:Label ID="lblRemark" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>

                                </div>

                            </fieldset>
                            <fieldset>
                                <legend>Approve Account Section</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>Deduction Proposed (%)</label><br />
                                        <asp:Label ID="lblAccountDeductionProposed" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Deduction Amount</label><br />
                                        <asp:Label ID="lblAccountDedAmount" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Round</label><br />
                                        <asp:Label ID="lblAccountRoundOff" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Net Payment </label>
                                        <br />
                                        <asp:Label ID="lblAccountNetPayment" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <label>Remark</label><br />
                                        <asp:Label ID="lblAccountRemark" runat="server" CssClass="form-control-Modal" Text=""></asp:Label>
                                    </div>
                                        </div>
                            </fieldset>
                            <fieldset>
                                <legend>Approve Audit Section</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Deduction Proposed (%)</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtDeductionProposed" ClientIDMode="Static" placeholder="Enter Deduction Proposed..." onblur="return CalculateAmount();" onkeypress="return allowNegativeNumber(event);" MaxLength="10" AutoComplete="off"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Deduction Amount</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtDeductionAmount" ClientIDMode="Static" placeholder="Enter Taxable Amount..." onkeypress="return validateDec(this,event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                            <asp:HiddenField ID="HF_BasicTotalAmount" runat="server" ClientIDMode="Static" />
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Round</label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtRoundOff" placeholder="Enter Round..." MaxLength="10" onblur="return CalculateAmount();" onkeypress="return allowNegativeNumber(event);" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Net Payment  <span style="color: red;"> *</span></label>

                                            <asp:HiddenField ID="HF_DeductionAmount" runat="server" ClientIDMode="Static" />
                                            <asp:HiddenField ID="HF_RoundOff" runat="server" ClientIDMode="Static" />
                                            <asp:HiddenField ID="HF_NetPayment" runat="server" ClientIDMode="Static" />
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNetPayment" ClientIDMode="Static" placeholder="Enter Net Payment..." onkeypress="return validateDec(this,event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <label>Remark <span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtRemark" placeholder="Enter Remark..." MaxLength="500" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" ID="btnApprove" Text="Approve" OnClick="btnApprove_Click" />
                                        </div>
                                    </div>
                                </div>

                            </fieldset>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function ShowPamentApprovalModal(msg) {
            $('#myModal').modal('show');
            if (msg != '')
                alert(msg);
        }

        function CalculateAmount() {

            var BasicTotalAmount = document.getElementById('<%=HF_BasicTotalAmount.ClientID%>').value.trim();
            var OLD_NetPayment = document.getElementById('<%=HF_NetPayment.ClientID%>').value.trim();
            var OLD_DeductionAmount = document.getElementById('<%=HF_DeductionAmount.ClientID%>').value.trim();
            var Cur_DeductionProposed = document.getElementById('<%=txtDeductionProposed.ClientID%>').value.trim();
            var OLD_RoundOff = document.getElementById('<%=HF_RoundOff.ClientID%>').value.trim();
            var RoundOff = document.getElementById('<%=txtRoundOff.ClientID%>').value.trim();



            if (BasicTotalAmount == "")
                BasicTotalAmount = "0";

            if (OLD_NetPayment == "")
                OLD_NetPayment = "0";

            if (OLD_DeductionAmount == "")
                OLD_DeductionAmount = "0";

            if (Cur_DeductionProposed == "")
                Cur_DeductionProposed = "0";

            if (OLD_RoundOff == "")
                OLD_RoundOff = "0";

            if (RoundOff == "")
                RoundOff = "0";

            var CurNetPayment = parseFloat(OLD_NetPayment) + parseFloat(OLD_DeductionAmount);


            var DeductionAmount = parseFloat((parseFloat(BasicTotalAmount) * parseFloat(Cur_DeductionProposed)) / 100).toFixed(2);


            var NetPayment = parseFloat(CurNetPayment) - parseFloat(DeductionAmount);

            var new_NetPayment = "0";
            if (OLD_RoundOff < 0) {
                new_NetPayment = parseFloat(parseFloat(NetPayment) + parseFloat(Math.abs(OLD_RoundOff))).toFixed(2);
            }
            else {
                new_NetPayment = parseFloat(parseFloat(NetPayment) - parseFloat(OLD_RoundOff)).toFixed(2);
            }
            var CurNew_NetPayment = "0";
            if (RoundOff < 0) {
                CurNew_NetPayment = parseFloat(parseFloat(new_NetPayment) - parseFloat(Math.abs(RoundOff))).toFixed(2);
            }
            else {
                CurNew_NetPayment = parseFloat(parseFloat(new_NetPayment) + parseFloat(RoundOff)).toFixed(2);
            }


            document.getElementById('<%=txtDeductionAmount.ClientID%>').value = parseFloat(DeductionAmount).toFixed(2);
            document.getElementById('<%=txtNetPayment.ClientID%>').value = parseFloat(CurNew_NetPayment).toFixed(2);
        }
    </script>
</asp:Content>











