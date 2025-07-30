<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinSupplierOrder.aspx.cs" Inherits="mis_Finance_FinSupplierOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .ValueInWords {
            background: none;
            background-color: transparent !important;
            border: none;
            padding: 0;
            height: auto;
            margin-top: -5px;
        }

        th {
            font-size: 10px;
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
                            <h3 class="box-title">Invoice Process</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Bill Process
                                </legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Order No.<span style="color: red;"> *</span></label>
                                            <asp:DropDownList runat="server" CssClass="form-control select2" ID="ddlOrderNo" OnSelectedIndexChanged="ddlOrderNo_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Order Date<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtOrderDate" placeholder="Enter Order Date..."></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Supplier Name<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Plant<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlPlant" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Item Category<span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Bill No.<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtBillNo" placeholder="Enter Bill No..." MaxLength="16" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Bill Date<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtBillDate" placeholder="Enter Bill Date..." autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Quantity Receive Date<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtQualityReceiveDate" placeholder="Enter Quality Receive Date..." autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2 pull-right">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnGetItems" Text="Get Item List" OnClick="btnGetItems_Click" OnClientClick="return validateformGetItem();" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvItemDetail" ClientIDMode="Static" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S. no.">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk" runat="server" Checked='<%# Eval("status").ToString() == "1"?true:false %>' />
                                                            <%--<asp:Label ID="lblno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item" ItemStyle-Width="10">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemID" runat="server" Visible="false" Text='<%#Eval("ItemID") %>'></asp:Label>
                                                            <asp:Label ID="lblItem" runat="server" Text='<%#Eval("ItemName").ToString() %>'></asp:Label>&nbsp;(<asp:Label ID="lblUnit" runat="server" Font-Bold="true" ForeColor="red" Text='<%#Eval("Unit") %>'></asp:Label>)
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate <br/> Inclusive">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblRateInclusive" runat="server" Style="width: 50px;" onblur="return CalculateBasicRate(this);" Text='<%#Eval("ItemRate").ToString() %>' CssClass="ValueInWords"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Quantity" ItemStyle-Width="10">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblItemQuantity" runat="server" Style="width: 50px;" Text='<%#Eval("Quantity").ToString() %>' CssClass="ValueInWords"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cess Applicable">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblCessApplicable" runat="server" Style="width: 50px;" Text='<%#Eval("CessApplicable").ToString() %>' CssClass="ValueInWords"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cess Rate">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblCessRate" runat="server" Style="width: 50px;" Text='<%#Eval("CessRate").ToString() %>' CssClass="ValueInWords"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GST Applicable">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblGSTApplicable" runat="server" Style="width: 50px;" Text='<%#Eval("GSTApplicable").ToString() %>' CssClass="ValueInWords"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GST Rate">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblGSTRate" runat="server" Style="width: 50px;" Text='<%#Eval("GSTRate").ToString() %>' CssClass="ValueInWords"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GST Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGSTType" runat="server" Text='<%#Eval("GSTType").ToString() %>' Style="width: 50px;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GST Include In Deduction">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblGSTInclude" runat="server" Text='<%#Eval("GSTIncInDeduction").ToString() %>' Style="width: 50px;" CssClass="ValueInWords"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity_Receive ">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtQualityReceive" Style="width: 100px;" Text='<%#Eval("QuantityReceive").ToString() %>' ClientIDMode="Static" placeholder="Enter Quantity Receive..." onkeypress="return validateDec4(this,event);" onblur="return CalculateBasicRate(this);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Diff_Quantity ">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtDIFFQuantity" Style="width: 100px;" Text='<%#Eval("DiffQuantity").ToString() %>' ClientIDMode="Static" placeholder="Enter Diff Quantity..." onkeypress="return validateDec4(this,event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Basic_Rate">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtBasicRate" Style="width: 100px;" Text='<%#Eval("BasicRate").ToString() %>' ClientIDMode="Static" placeholder="Enter Basic Rate..." onkeypress="return validateDec4(this,event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GST_Rate" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtGSTRateAmt" Style="width: 100px;" ClientIDMode="Static" Text='<%#Eval("GST").ToString() %>' placeholder="Enter Basic Rate..." onkeypress="return validateDec4(this,event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Basic_Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtBasicAmount" Style="width: 100px;" ClientIDMode="Static" Text='<%#Eval("BasicAmount").ToString() %>' placeholder="Enter Taxable Amount..." onkeypress="return validateDec(this,event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GST_Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtGSTAmount" Style="width: 100px;" ClientIDMode="Static" Text='<%#Eval("GSTAmount").ToString() %>' placeholder="Enter Taxable Amount..." onkeypress="return validateDec(this,event);" onblur="return CalculateAmount(this);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--    <asp:TemplateField HeaderText="CGST">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtCGST" ClientIDMode="Static" placeholder="Enter CGST..." onkeypress="return validateDec(this,event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="SGST">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtSGST" ClientIDMode="Static" placeholder="Enter SGST..." onkeypress="return validateDec(this,event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="IGST">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItem" runat="server" Text='<%#Eval("").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Cess_Rate">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtCess" Style="width: 100px;" placeholder="Enter Cess..." Text='<%#Eval("Cess").ToString() %>' MaxLength="10" onkeypress="return validateDec(this,event);" onblur="return CalculateAmount(this);" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total_Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtTotalAmount" Style="width: 100px;" ClientIDMode="Static" Text='<%#Eval("TotalAmount").ToString() %>' placeholder="Enter Total Amount..." MaxLength="10"></asp:TextBox>
                                                            <asp:HiddenField ID="HF_BasicTotalAmount" runat="server" Value='<%# Eval("BasicAmount").ToString() %>' ClientIDMode="Static" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deduction Proposed (%)">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtDeductionProposed" Style="width: 100px;" ClientIDMode="Static" Text='<%#Eval("DeductionProposed").ToString() %>' placeholder="Enter Deduction Proposed..." onblur="return CalculateAmount(this);" onkeypress="return allowNegativeNumber(event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deduction_Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtDeductionAmount" Style="width: 100px;" ClientIDMode="Static" Text='<%#Eval("DeductionAmount").ToString() %>' placeholder="Enter Taxable Amount..." onkeypress="return validateDec(this,event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Extra Supply Deduction( IF Yes)">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkExtraSupDed" ClientIDMode="Static" runat="server" onclick="Checkbox(this);" />
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtExtraSupDed" Style="width: 100px;" ClientIDMode="Static" Text='<%#Eval("ExtraSupDed").ToString() %>' placeholder="Enter Extra Supply Deduction..." onkeypress="return validateDec(this,event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                            <asp:HiddenField ID="hdExtraDeduction" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TDS(%)">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtTDSPer" Style="width: 100px;" ClientIDMode="Static" Text='<%#Eval("TDSPer").ToString() %>' placeholder="Enter Deduction Proposed..." onblur="return CalculateAmount(this);" onkeypress="return allowNegativeNumber(event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TDS_Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtTDS" Style="width: 100px;" ClientIDMode="Static" Text='<%#Eval("TDS").ToString() %>' placeholder="Enter TDS..." onkeypress="return validateDec(this,event);" onblur="return CalculateAmount(this);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TCS_Amount">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtTCS" Style="width: 100px;" ClientIDMode="Static" Text='<%#Eval("TCS").ToString() %>' placeholder="Enter TCS..." onkeypress="return validateDec(this,event);" onblur="return CalculateAmount(this);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Round_Off">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtRoundOff" Style="width: 100px;" placeholder="Enter Round..." Text='<%#Eval("RoundOff").ToString() %>' MaxLength="10" onblur="return CalculateAmount(this);" onkeypress="return allowNegativeNumber(event);" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Net_Payment">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNetPayment" Style="width: 100px;" ClientIDMode="Static" Text='<%#Eval("NetPayment").ToString() %>' placeholder="Enter Net Payment..." onkeypress="return validateDec(this,event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Plant_Test_Report">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtQualityCleanReport" Style="width: 100px;" Text='<%#Eval("QualityCleanReport").ToString() %>' placeholder="Enter Plant Test Report..." MaxLength="300" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NABL_Report">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtNABLReport" Style="width: 100px;" Text='<%#Eval("NABLReport").ToString() %>' placeholder="Enter NABL Report..." MaxLength="500" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier_In_House_Report">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtSupplierInHose" Style="width: 100px;" Text='<%#Eval("SupplierInHouseReport").ToString() %>' placeholder="Enter Supplier In House Report..." MaxLength="500" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Toll_Kanta_Slip">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtTollKanta" Style="width: 100px;" Text='<%#Eval("TollKantaSlip").ToString() %>' placeholder="Enter Toll Kanta Slip..." MaxLength="500" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="E-way_bill">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtEWayBill" Style="width: 100px;" Text='<%#Eval("EWayBill").ToString() %>' placeholder="Enter E-way bill..." MaxLength="500" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtRemark" Style="width: 100px;" Text='<%#Eval("Remark").ToString() %>' placeholder="Enter Remark..." MaxLength="500" AutoComplete="off"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="FinSupplierOrder.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">

        //function CalExtraSupply() {

        //    if (document.getElementById("chkExtraSupDed").checked == true)
        //    {
        //        alert("yes")
        //    }
        //    else {
        //        alert("No");
        //    }
        //}
        //$('#gvItemDetail #chkExtraSupDed').change(){
        //}
        function CalculateBasicRate(lnk) {
            debugger;
            var row = lnk.parentNode.parentNode;
            var ItemRate = $(row.cells[2]).find('input').val();
            if (ItemRate == "")
                ItemRate = "0";
            //alert(ItemRate);

            //var CessApp = document.getElementById("ddlCessApplicable");
            var CessApplicable = $(row.cells[4]).find('input').val();

            var CessRate = "0";
            if (CessApplicable == "Yes") {
                CessRate = $(row.cells[5]).find('input').val();
            }

            if (CessRate == "")
                CessRate = "0";


            // var GSTApp = document.getElementById("ddlGSTApplicable");
            var GSTApplicable = $(row.cells[6]).find('input').val();

            var GSTRate = "0";
            if (GSTApplicable.trim() == "Yes") {

                GSTRate = $(row.cells[7]).find('input').val();
                //alert(GSTRate);
            }


            if (GSTRate == "")
                GSTRate = "0";

            var lessCessAmt = parseFloat(parseFloat(ItemRate) - parseFloat(CessRate)).toFixed(3);


            var BasicratewithGST = parseFloat(((parseFloat(lessCessAmt) * 100) / (100 + parseFloat(GSTRate)))).toFixed(3);

            var BasicGST = parseFloat((parseFloat(BasicratewithGST) * parseFloat(GSTRate)) / 100).toFixed(3);


            var BasicRate = parseFloat(BasicratewithGST).toFixed(2);

            var bGST = BasicRate
            var BasicRate = parseFloat(BasicRate).toFixed(2);
            var BasicGST = parseFloat(BasicGST).toFixed(2);

            $(row.cells[12]).find('input').val(BasicRate);
            $(row.cells[13]).find('input').val(BasicGST);

            CalculateAmount(lnk);
        }
        function CalculateAmount(lnk) {
            debugger;
            var row = lnk.parentNode.parentNode;
            var Quantity = $(row.cells[3]).find('input').val();
            if (Quantity == "") {
                Quantity = "0";
            }

            var QualityReceive = $(row.cells[10]).find('input').val();
            if (QualityReceive == "") {
                QualityReceive = "0";
            }
            var BasicRate = $(row.cells[12]).find('input').val();
            if (BasicRate == "") {
                BasicRate = "0";
            }
            var GSTRateAmt = $(row.cells[13]).find('input').val();
            if (GSTRateAmt == "") {
                GSTRateAmt = "0";
            }
            var TDSPer = $(row.cells[21]).find('input').val();
            if (TDSPer == "") {
                TDSPer = "0";
            }
            var TCS = $(row.cells[23]).find('input').val();
            if (TCS == "") {
                TCS = "0";
            }

           <%-- var TDS = document.getElementById('<%=txtTDS.ClientID%>').value.trim();--%>
            var CessApplicable = $(row.cells[4]).find('input').val();

            var CessRate = "0";
            if (CessApplicable == "Yes") {
                CessRate = $(row.cells[5]).find('input').val();
                if (CessRate == "") {
                    CessRate = "0";
                }
            }
            var DeductionProposed_Per = $(row.cells[18]).find('input').val();
            if (DeductionProposed_Per == "") {
                DeductionProposed_Per = "0";
            }
            var RoundOff = $(row.cells[24]).find('input').val();
            if (RoundOff == "") {
                RoundOff = "0";
            }
            var GSTIncInDeduction = $(row.cells[9]).find('input').val();
            if (GSTIncInDeduction == "") {
                GSTIncInDeduction = "0";
            }

            if (Quantity == "")
                Quantity = "0";

            if (QualityReceive == "")
                QualityReceive = "0";

            if (BasicRate == "")
                BasicRate = "0";

            if (GSTRateAmt == "")
                GSTRateAmt = "0";
            if (TCS == "")
                TCS = "0";
            if (TDS == "")
                TDS = "0";
            if (CessRate == "")
                CessRate = "0";

            if (DeductionProposed_Per == "")
                DeductionProposed_Per = "0";
            if (RoundOff == "")
                RoundOff = "0";

            if (TDSPer == "")
                TDSPer = "0";



            var TDS = "0";

            var DIFFQuantity = parseFloat(QualityReceive) - parseFloat(Quantity);
            var BasicAmount = parseFloat(parseFloat(QualityReceive) * parseFloat(BasicRate)).toFixed(2);
            var GSTAmount = parseFloat(parseFloat(QualityReceive) * parseFloat(GSTRateAmt)).toFixed(2);
            var CessAmount = parseFloat(parseFloat(QualityReceive) * parseFloat(CessRate)).toFixed(2);

            var TotalAmount = parseFloat(parseFloat(BasicAmount) + parseFloat(GSTAmount) + parseFloat(CessAmount)).toFixed(2);

            var BasicTotalAmount = "0";

            if (GSTIncInDeduction == "Yes") {
                BasicTotalAmount = TotalAmount;
            }
            else {
                BasicTotalAmount = parseFloat(parseFloat(BasicAmount) + parseFloat(CessAmount)).toFixed(2);
            }

            //var DeductionAmount = parseFloat((parseFloat(TotalAmount) * parseFloat(DeductionProposed_Per)) / 100).toFixed(2);

            var DeductionAmount = parseFloat((parseFloat(BasicTotalAmount) * parseFloat(DeductionProposed_Per)) / 100).toFixed(2);

            TDS = parseFloat((parseFloat(BasicAmount) * parseFloat(TDSPer)) / 100).toFixed(2);


            var ExtraSupDed = "0";
            //var ExtraSupDed = lnk.checked;
            //if ($(row.cells[20]).find('chkExtraSupDed:checkbox').val() == true) {
            //if (lnk.checked == true) {
            //    if (DIFFQuantity > 0)
            //        ExtraSupDed = parseFloat((parseFloat(TotalAmount) / parseFloat(QualityReceive)).toFixed(2) * parseFloat(DIFFQuantity)).toFixed(2);
            //}

            if (DIFFQuantity > 0) {
                if ($(row.cells[20]).find('input[type="hidden"]').val() > 0) {
                    ExtraSupDed = parseFloat((parseFloat(TotalAmount) / parseFloat(QualityReceive)).toFixed(2) * parseFloat(DIFFQuantity)).toFixed(2);
                }
            }

            var NetPayment = "0";
            if (RoundOff < 0) {
                NetPayment = parseFloat((parseFloat(TotalAmount) + parseFloat(TCS) - parseFloat(ExtraSupDed) - parseFloat(TDS) - (parseFloat(DeductionAmount)) - parseFloat(Math.abs(RoundOff)))).toFixed(2);
            }
            else {
                NetPayment = parseFloat((parseFloat(TotalAmount) + parseFloat(TCS) - parseFloat(ExtraSupDed) - parseFloat(TDS) - (parseFloat(DeductionAmount)) + parseFloat(RoundOff))).toFixed(2);
            }


            var DIFFQuantity = parseFloat(DIFFQuantity).toFixed(2);
            var BasicAmount = parseFloat(BasicAmount).toFixed(2);
            var GSTAmount = parseFloat(GSTAmount).toFixed(2);
            var CessAmount = parseFloat(CessAmount).toFixed(2);
            var TotalAmount = parseFloat(TotalAmount).toFixed(2);
            var DeductionAmount = parseFloat(DeductionAmount).toFixed(2);
            var NetPayment = parseFloat(NetPayment).toFixed(2);
            var BasicTotalAmount = parseFloat(BasicTotalAmount).toFixed(2);
            var ExtraSupDed = parseFloat(ExtraSupDed).toFixed(2);
            var TDS = parseFloat(TDS).toFixed(2);

            //alert(TotalAmount);

            $(row.cells[11]).find('input').val(DIFFQuantity);
            $(row.cells[14]).find('input').val(BasicAmount);
            $(row.cells[15]).find('input').val(GSTAmount);
            $(row.cells[16]).find('input').val(CessAmount);
            $(row.cells[17]).find('input').val(TotalAmount);
            $(row.cells[19]).find('input').val(DeductionAmount);
            $(row.cells[25]).find('input').val(NetPayment);
            $(row.cells[17]).find('input[type="hidden"]').val(BasicTotalAmount);
            $(row.cells[20]).find('input').val(ExtraSupDed);
            $(row.cells[22]).find('input').val(TDS);
        }
        function validateformGetItem() {
            var msg = "";
            if (document.getElementById('<%=ddlOrderNo.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Order No. \n";
            }
            if (document.getElementById('<%=txtOrderDate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Order Date. \n";
            }
            if (document.getElementById('<%= ddlSupplier.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Supplier Name. \n";
            }
            if (document.getElementById('<%= ddlPlant.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Plant. \n";
            }
            if (document.getElementById('<%= ddlItemCategory.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Item Category. \n";
            }
            if (document.getElementById('<%=txtBillNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Bill No. \n";
            }
            if (document.getElementById('<%=txtBillDate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Bill Date. \n";
            }
            if (document.getElementById('<%=txtQualityReceiveDate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Quality Receive Date. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }

        function Checkbox(lk) {
            debugger;
            debugger;
            var row = lk.parentNode.parentNode;
            var Quantity = $(row.cells[3]).find('input').val();
            if (Quantity == "") {
                Quantity = "0";
            }

            var QualityReceive = $(row.cells[10]).find('input').val();
            if (QualityReceive == "") {
                QualityReceive = "0";
            }
            var BasicRate = $(row.cells[12]).find('input').val();
            if (BasicRate == "") {
                BasicRate = "0";
            }
            var GSTRateAmt = $(row.cells[13]).find('input').val();
            if (GSTRateAmt == "") {
                GSTRateAmt = "0";
            }
            var TDSPer = $(row.cells[21]).find('input').val();
            if (TDSPer == "") {
                TDSPer = "0";
            }
            var TCS = $(row.cells[23]).find('input').val();
            if (TCS == "") {
                TCS = "0";
            }

           <%-- var TDS = document.getElementById('<%=txtTDS.ClientID%>').value.trim();--%>
            var CessApplicable = $(row.cells[4]).find('input').val();

            var CessRate = "0";
            if (CessApplicable == "Yes") {
                CessRate = $(row.cells[5]).find('input').val();
                if (CessRate == "") {
                    CessRate = "0";
                }
            }
            var DeductionProposed_Per = $(row.cells[18]).find('input').val();
            if (DeductionProposed_Per == "") {
                DeductionProposed_Per = "0";
            }
            var RoundOff = $(row.cells[24]).find('input').val();
            if (RoundOff == "") {
                RoundOff = "0";
            }
            var GSTIncInDeduction = $(row.cells[9]).find('input').val();
            if (GSTIncInDeduction == "") {
                GSTIncInDeduction = "0";
            }

            if (Quantity == "")
                Quantity = "0";

            if (QualityReceive == "")
                QualityReceive = "0";

            if (BasicRate == "")
                BasicRate = "0";

            if (GSTRateAmt == "")
                GSTRateAmt = "0";
            if (TCS == "")
                TCS = "0";
            if (TDS == "")
                TDS = "0";
            if (CessRate == "")
                CessRate = "0";

            if (DeductionProposed_Per == "")
                DeductionProposed_Per = "0";
            if (RoundOff == "")
                RoundOff = "0";

            if (TDSPer == "")
                TDSPer = "0";



            var TDS = "0";

            var DIFFQuantity = parseFloat(QualityReceive) - parseFloat(Quantity);
            var BasicAmount = parseFloat(parseFloat(QualityReceive) * parseFloat(BasicRate)).toFixed(2);
            var GSTAmount = parseFloat(parseFloat(QualityReceive) * parseFloat(GSTRateAmt)).toFixed(2);
            var CessAmount = parseFloat(parseFloat(QualityReceive) * parseFloat(CessRate)).toFixed(2);

            var TotalAmount = parseFloat(parseFloat(BasicAmount) + parseFloat(GSTAmount) + parseFloat(CessAmount)).toFixed(2);

            var BasicTotalAmount = "0";

            if (GSTIncInDeduction == "Yes") {
                BasicTotalAmount = TotalAmount;
            }
            else {
                BasicTotalAmount = parseFloat(parseFloat(BasicAmount) + parseFloat(CessAmount)).toFixed(2);
            }

            //var DeductionAmount = parseFloat((parseFloat(TotalAmount) * parseFloat(DeductionProposed_Per)) / 100).toFixed(2);

            var DeductionAmount = parseFloat((parseFloat(BasicTotalAmount) * parseFloat(DeductionProposed_Per)) / 100).toFixed(2);

            TDS = parseFloat((parseFloat(BasicAmount) * parseFloat(TDSPer)) / 100).toFixed(2);


            var ExtraSupDed = "0";
            //var ExtraSupDed = lnk.checked;
            //if ($(row.cells[20]).find('chkExtraSupDed:checkbox').val() == true) {
            if (lk.checked == true) {
                if (DIFFQuantity > 0)
                    ExtraSupDed = parseFloat((parseFloat(TotalAmount) / parseFloat(QualityReceive)).toFixed(2) * parseFloat(DIFFQuantity)).toFixed(2);
                $(row.cells[20]).find('input[type="hidden"]').val(ExtraSupDed);
            }
            else {
                $(row.cells[20]).find('input[type="hidden"]').val(ExtraSupDed);
            }
            var NetPayment = "0";
            if (RoundOff < 0) {
                NetPayment = parseFloat((parseFloat(TotalAmount) + parseFloat(TCS) - parseFloat(ExtraSupDed) - parseFloat(TDS) - (parseFloat(DeductionAmount)) - parseFloat(Math.abs(RoundOff)))).toFixed(2);
            }
            else {
                NetPayment = parseFloat((parseFloat(TotalAmount) + parseFloat(TCS) - parseFloat(ExtraSupDed) - parseFloat(TDS) - (parseFloat(DeductionAmount)) + parseFloat(RoundOff))).toFixed(2);
            }


            var DIFFQuantity = parseFloat(DIFFQuantity).toFixed(2);
            var BasicAmount = parseFloat(BasicAmount).toFixed(2);
            var GSTAmount = parseFloat(GSTAmount).toFixed(2);
            var CessAmount = parseFloat(CessAmount).toFixed(2);
            var TotalAmount = parseFloat(TotalAmount).toFixed(2);
            var DeductionAmount = parseFloat(DeductionAmount).toFixed(2);
            var NetPayment = parseFloat(NetPayment).toFixed(2);
            var BasicTotalAmount = parseFloat(BasicTotalAmount).toFixed(2);
            var ExtraSupDed = parseFloat(ExtraSupDed).toFixed(2);
            var TDS = parseFloat(TDS).toFixed(2);

            //alert(TotalAmount);

            $(row.cells[11]).find('input').val(DIFFQuantity);
            $(row.cells[14]).find('input').val(BasicAmount);
            $(row.cells[15]).find('input').val(GSTAmount);
            $(row.cells[16]).find('input').val(CessAmount);
            $(row.cells[17]).find('input').val(TotalAmount);
            $(row.cells[19]).find('input').val(DeductionAmount);
            $(row.cells[25]).find('input').val(NetPayment);
            $(row.cells[17]).find('input[type="hidden"]').val(BasicTotalAmount);
            $(row.cells[20]).find('input').val(ExtraSupDed);
            $(row.cells[22]).find('input').val(TDS);
        };
        function validateform() {
            var msg = "";
            <%--if (document.getElementById('<%=txtItemName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Item Name. \n";
            }

            if (document.getElementById('<%=txtItemRate.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Item Rate. \n";
            }--%>
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    if (confirm("Do you really want to Edit Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }

                }
            }
        }



        function validateDec4(el, evt) {
            var digit = 4;
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

        function allowNegativeNumber(e) {
            var charCode = (e.which) ? e.which : event.keyCode
            if (charCode > 31 && (charCode < 45 || charCode > 57)) {
                return false;
            }
            return true;

        }
        function validateAmount(sender) {

            var pattern = /^-?[0-9]+(.[0-9]{1,5})?$/;
            var text = sender.value;
            if (text != "") {
                if (text.match(pattern) == null) {
                    alert('the format is wrong');
                    sender.value = "0";
                }
            }
            else {
                sender.value = "0";
            }


        }
    </script>
</asp:Content>






