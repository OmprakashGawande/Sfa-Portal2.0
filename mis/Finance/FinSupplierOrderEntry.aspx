<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinSupplierOrderEntry.aspx.cs" Inherits="mis_Finance_FinSupplierOrderEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        @media print {
            body * {
                visibility: hidden;
            }

            #section-to-print, #section-to-print * {
                visibility: visible;
            }

            #section-to-print {
                position: absolute;
                left: 0;
                top: 0;
            }

            .hidePrint {
                display: none;
            }
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
                            <h3 class="box-title">Supplier Order Entry</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Supplier Order</legend>
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Order No.<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" CssClass="form-control" ID="txtOrderNo" placeholder="Enter Order No..." MaxLength="50" AutoComplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Order Date<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtOrderDate" placeholder="Enter Order Date..." autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Supplier Name<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <fieldset>
                                    <legend>Add Multiple Items in Order</legend>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Plant<span style="color: red;"> *</span></label>
                                                <asp:DropDownList ID="ddlPlant" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Item Category<span style="color: red;">*</span></label>
                                                <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button ID="btnGetItemList" runat="server" Text="Get Item List" Style="margin-top: 23px;" CssClass="btn btn-success btn-block" OnClick="btnGetItemList_Click" />
                                            </div>
                                        </div>
                                        <%-- <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Item <span style="color: red;">*</span></label>
                                                <asp:DropDownList ID="ddlItem" runat="server" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>
                                                    Rate Inclusive of All<span style="color: red;"> *</span>

                                                </label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtItemRate" placeholder="Enter Rate..." ClientIDMode="Static" onkeypress="return validateDec4(this,event);" onblur="return CalculateBasicRate();" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    Item Quantity<span style="color: red;"> *</span>(Unit :
                                                <asp:Label ID="lblUnit" runat="server" Style="color: red;" Text=""></asp:Label>)</label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtQuantity" placeholder="Enter Item Quantity..." onkeypress="return validateDec4(this,event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>--%>
                                    </div>
                                    <%-- <div class="row">
                                       <div class="col-md-2">
                                            <div class="form-group">
                                                <label>
                                                    Cess Applicable<span style="color: red;"> *</span><br />
                                                    &nbsp;</label>
                                                <asp:DropDownList ID="ddlCessApplicable" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCessApplicable_SelectedIndexChanged">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>
                                                    Cess Rate<span style="color: red;"> *</span><br />
                                                    &nbsp;</label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCessRate" placeholder="Enter Cess Rate..." onkeypress="return validateDec(this,event);" onblur="return CalculateBasicRate();" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>
                                                    GST Applicable<span style="color: red;"> *</span><br />
                                                    &nbsp;</label>
                                                <asp:DropDownList ID="ddlGSTApplicable" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlGSTApplicable_SelectedIndexChanged">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2" runat="server" id="divGSTRate">
                                            <div class="form-group">
                                                <label>
                                                    GST Rate<span style="color: red;"> *</span><br />
                                                    &nbsp;</label>
                                                <asp:DropDownList ID="ddlGSTRate" runat="server" CssClass="form-control select2" ClientIDMode="Static">
                                                    <asp:ListItem Value="0.00">0.00 %</asp:ListItem>
                                                    <asp:ListItem Value="5.00">5.00 %</asp:ListItem>
                                                    <asp:ListItem Value="12.00">12.00 %</asp:ListItem>
                                                    <asp:ListItem Value="18.00">18.00 %</asp:ListItem>
                                                    <asp:ListItem Value="28.00">28.00 %</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>
                                                    GST Type
                                                    <br />
                                                    &nbsp;</label>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtGSTType" placeholder="Enter GST Type..." MaxLength="20" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>GST Include in Deduction<span style="color: red;"> *</span></label>
                                                <asp:DropDownList ID="ddlGSTIncInDeduction" runat="server" CssClass="form-control" ClientIDMode="Static" onchange="return CalculateBasicRate();">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <%--<div class="row">
                                        <div class="col-md-1 pull-right">
                                            <div class="form-group">
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-success btn-block" OnClick="btnAdd_Click" />
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table table-responsive">
                                                <asp:GridView ID="gvItemList" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S. no." ItemStyle-Width="5%">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAll" runat="server" onclick="checkAllbox(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk" runat="server" />
                                                                <%--<asp:Label ID="lblno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemCategoryID" runat="server" Visible="false" Text='<%#Eval("ItemCategory") %>'></asp:Label>
                                                                <asp:Label ID="lblItemID" runat="server" Visible="false" Text='<%#Eval("ItemID") %>'></asp:Label>
                                                                <asp:Label ID="lblItem" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>&nbsp;(<asp:Label ID="lblUnit" runat="server" ForeColor="red" Font-Bold="true" Text='<%#Eval("ItemUnit") %>'></asp:Label>)
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cess Applicable">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCessApplicable" runat="server" Text='<%#Eval("CessApplicable") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cess Rate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCessRate" runat="server" Text='<%#Eval("CessRate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GST Applicable">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGSTApplicable" runat="server" Text='<%#Eval("GSTApplicable") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GST Rate">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGSTRate" runat="server" Text='<%#Eval("GSTRate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GST Type" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGSTType" runat="server" Text='<%#Eval("GSTType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="GST Include In Deduction">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGSTIncInDeduction" runat="server" Text='<%#Eval("GSTIncInDeduction") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Rate Inclusive" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtItemRateInclusive" placeholder="Enter Rate..." ClientIDMode="Static" onkeypress="return validateDec4(this,event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Quantity" ItemStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtQuantity" placeholder="Enter Item Quantity..." onkeypress="return validateDec4(this,event);" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <div class="col-md-1 pull-right">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-success btn-block" OnClick="btnAdd_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="gvSupplier" runat="server" DataKeyNames="RowNo" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowDeleting="gvSupplier_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S. no." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Plant">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPlantID" runat="server" Visible="false" Text='<%#Eval("PlantID") %>'></asp:Label>
                                                            <asp:Label ID="lblPlant" runat="server" Text='<%#Eval("Plant") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCategoryID" runat="server" Visible="false" Text='<%#Eval("ItemCategoryID") %>'></asp:Label>
                                                            <asp:Label ID="lblItemID" runat="server" Visible="false" Text='<%#Eval("ItemID") %>'></asp:Label>
                                                            <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item") %>'></asp:Label>
                                                            (<asp:Label ID="lblUnit" runat="server" Text='<%#Eval("Unit") %>'></asp:Label>)
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cess Applicable">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCessApplicable" runat="server" Text='<%#Eval("CessApplicable") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cess Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCessRate" runat="server" Text='<%#Eval("CessRate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GST Applicable">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGSTApplicable" runat="server" Text='<%#Eval("GSTApplicable") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GST Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGSTRate" runat="server" Text='<%#Eval("GSTRate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GST Type" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGSTType" runat="server" Text='<%#Eval("GSTType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GST Include In Deduction">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGSTIncInDeduction" runat="server" Text='<%#Eval("GSTIncludeInDeduction") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate Inclusive">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRateInclusive" runat="server" Text='<%#Eval("RateInclusive") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemQuantity" runat="server" Text='<%#Eval("ItemQuantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnbDelete" OnClientClick="return confirm('Are you sure want to delete this record?')" runat="server" CommandName="Delete"><i class="fa fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2 pull-right">
                                            <div class="form-group">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Final Submit" CssClass="btn btn-success btn-block" OnClick="btnSubmit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </fieldset>
                            <fieldset>
                                <legend>Supplier Order Detail</legend>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Plant<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlPlantSearch" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Supplier Name<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlddlSupplierSearch" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
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
                                    <div class="col-md-2">
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
                                        <div class="table table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S. no." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOrderNo" runat="server" Text='<%#Eval("OrderNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOrderDate" runat="server" Text='<%#Eval("OrderDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSupplierName" runat="server" Text='<%#Eval("SupplierName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnbView" runat="server" CommandArgument='<%#Eval("OrderNo") %>' CommandName="ViewRecord"><i class="fa fa-eye"></i>&<i class="fa fa-print"></i></asp:LinkButton> &nbsp;
                                                            <asp:LinkButton ID="lnbEdit" runat="server" CommandArgument='<%#Eval("OrderNo") %>' CommandName="EditRecord"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
            </div>

        </section>
        <div class="modal fade" id="Modal" role="dialog">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Items Detail</h4>
                        &nbsp;&nbsp;&nbsp;&nbsp;<span id="Span1" runat="server" style="color: red; font-weight: bold; font-size: 18px;"></span>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <div class="modal-body" id="section-to-print">
                        <style>
                            .modal-header {
                                padding-bottom: 0 !important;
                            }
                            .modal-body {
                                padding-top: 0px !important;
                            }
                            .mdt{
                                margin-top:0px !important;
                            }
                        </style>
                        <h3 class="box-title mdt">Supplier Order Details</h3>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Order No. : </label>
                                    &nbsp;<asp:Label ID="lblOrderNo" runat="server" Text=""></asp:Label>
                                    <br />
                                    <label>Order Date : </label>
                                    &nbsp;<asp:Label ID="lblOrderDate" runat="server" Text=""></asp:Label>
                                    <br />
                                    <label>Supplier Name : </label>
                                    &nbsp;<asp:Label ID="lblSupplierName" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="GvItemDetail" runat="server" CssClass="table table-bordered table-hover table-striped" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S. no.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblno" runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Plant">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlant" runat="server" Text='<%#Eval("PlantName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItem" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                                                    (<asp:Label ID="lblUnit" runat="server" Text='<%#Eval("Unit") %>'></asp:Label>)
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate Inclusive">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRateInclusive" runat="server" Text='<%#Eval("ItemRate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cess Applicable" HeaderStyle-CssClass="hidePrint" ControlStyle-CssClass="hidePrint" ItemStyle-CssClass="hidePrint">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCessApplicable" runat="server" Text='<%#Eval("CessApplicable") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cess Rate" HeaderStyle-CssClass="hidePrint" ControlStyle-CssClass="hidePrint" ItemStyle-CssClass="hidePrint">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCessRate" runat="server" Text='<%#Eval("CessRate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GST Applicable" HeaderStyle-CssClass="hidePrint" ControlStyle-CssClass="hidePrint" ItemStyle-CssClass="hidePrint">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGSTApplicable" runat="server" Text='<%#Eval("GSTApplicable") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GST Rate" HeaderStyle-CssClass="hidePrint" ControlStyle-CssClass="hidePrint" ItemStyle-CssClass="hidePrint">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGSTRate" runat="server" Text='<%#Eval("GSTRate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="GST Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGSTType" runat="server" Text='<%#Eval("GSTType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="GST Include In Deduction" HeaderStyle-CssClass="hidePrint" ControlStyle-CssClass="hidePrint" ItemStyle-CssClass="hidePrint">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGSTIncludeInDeduction" runat="server" Text='<%#Eval("GSTIncInDeduction") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" onclick="window.print();">Print</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">

        function ShowModal() {
            $('#Modal').modal('show');
        }




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
                if (document.getElementById('<%=btnAdd.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
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

        function checkAllbox(objRef) {
            var GridView = document.getElementById("<%=gvItemList.ClientID %>");
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
</asp:Content>

