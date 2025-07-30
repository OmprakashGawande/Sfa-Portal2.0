<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinSupplierAuditTrail.aspx.cs" Inherits="mis_Finance_FinSupplierAuditTrail" %>

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
                            <h3 class="box-title">Supplier Audit Trail</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Supplier Name<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Bill No.<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlBillNo" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlBillNo_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Net Payment<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNetPayment" placeholder="Enter Net Payment..." onkeypress="return validateDec(this,event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cheque No.</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtChequeNo" placeholder="Enter Cheque No..." MaxLength="50" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cheque Date</label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtChequeDate" placeholder="Enter Cheque Date..." autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="FinSupplierMaster.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <h4 class="box-title">Audit Trail Details</h4>
                                    <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="ID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ID").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                            <asp:BoundField DataField="BillNo" HeaderText="Bill No." />
                                            <asp:BoundField DataField="NetPayment" HeaderText="Net Payment" />
                                            <asp:BoundField DataField="ChequeNo" HeaderText="Cheque No" />
                                            <asp:BoundField DataField="ChequeDate" HeaderText="Cheque Date" />

                                        </Columns>
                                    </asp:GridView>
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







