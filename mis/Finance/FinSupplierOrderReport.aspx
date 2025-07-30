<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinSupplierOrderReport.aspx.cs" Inherits="mis_Finance_FinSupplierOrderReport" %>

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
                            <h3 class="box-title">Supplier Order Details  (By Bill Date)</h3>
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
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>



                            <div class="row">
                                <div class="col-md-12">
                                    <h4 class="box-title">Order Details By Bill Date</h4>
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" EmptyDataText="No records Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="HL_Edit" CssClass="label label-info" Target="_blank" Visible='<%# Eval("AccountStatus").ToString() == "Pending" ?  true: false %>' NavigateUrl='<%# "~/mis/Finance/FinSupplierOrder.aspx?Action=" + APIProcedure.Client_Encrypt(Eval("OrderID").ToString())%>' runat="server">Edit</asp:HyperLink>
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
                                                <asp:BoundField DataField="DIFF" HeaderText="DIFF" />
                                                <asp:BoundField DataField="BillNo_BillDate" HeaderText="BILL NO/BILL DATE" />
                                                <asp:BoundField DataField="BASIC_AMOUNT" HeaderText="BASIC AMOUNT" />
                                                <asp:BoundField DataField="GST_Amount" HeaderText="GST AMOUNT" />
                                                <asp:BoundField DataField="Cess" HeaderText="CESS" />
                                                <asp:BoundField DataField="Total_Amount" HeaderText="TOTAL AMOUNT" />
                                                <asp:BoundField DataField="Deduction_if_any" HeaderText="Deduction if any" />
                                                <asp:BoundField DataField="ROUNDoff" HeaderText="ROUND" />
                                                <asp:BoundField DataField="Net_Payble_Amount" HeaderText="Net Payble Amount" />
                                                <asp:BoundField DataField="QualityCleanReport" HeaderText="Quality Clean Report" />
                                                <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" />
                                                <asp:BoundField DataField="NABLReport" HeaderText="NABL Report" />
                                                <asp:BoundField DataField="SupplierInHouseReport" HeaderText="Supplier In House Report" />
                                                <asp:BoundField DataField="TollKantaSlip" HeaderText="Toll Kanta Slip" />
                                                <asp:BoundField DataField="EWayBill" HeaderText="E-Way Bill" />
                                            </Columns>
                                        </asp:GridView>
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
</asp:Content>






