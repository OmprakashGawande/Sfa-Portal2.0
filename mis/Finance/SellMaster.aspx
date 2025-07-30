<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SellMaster.aspx.cs" Inherits="mis_Finance_SellMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">Sale Voucher</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="text-center">
                                <asp:RadioButtonList runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True">Credit Sale&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem>Cash Sale</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>

                        </div>
                        <hr />
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Bill No.</label>
                                <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Bill No."></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Name of Debtor [<a href="#"> Add </a>]</label>
                                <asp:DropDownList runat="server" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Name of Consignee [<a href="#"> Add </a>]</label>
                                <asp:DropDownList runat="server" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date</label>
                                <asp:TextBox ID="txtDate" runat="server" Text="22/08/2018" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    </div>
                    <fieldset>
                        <legend>Add Item</legend>
                        <div class="row">
                            
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Item Name</label>
                                    <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control select2">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>Sprikler</asp:ListItem>
                                        <asp:ListItem>Ancient Plows</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Quantity</label>
                                    <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" placeholder="Quantity"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Purchase Ledger of item</label>
                                    <asp:TextBox runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Rate</label>
                                    <asp:TextBox ID="txtRate" runat="server" Text="200" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>SGST <small class="text-danger">@ 9 %</small></label>
                                    <asp:TextBox ID="txtSGST" runat="server" Text="12" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>CGST <small class="text-danger">@ 9 %</small></label>
                                    <asp:TextBox ID="txtCGST" runat="server" Text="12" CssClass="form-control" ReadOnly="true"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>TDS</label>
                                    <asp:TextBox ID="txtISGST" runat="server" Text="24" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Total Amount</label>
                                    <asp:TextBox ID="txtTotalAmount" runat="server" placeholder="Total Amount" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>&nbsp;</label>
                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-block btn-info" Text="Add Item" />
                                </div>
                            </div>
                        </div>
                        <table border="1" class="table table-bordered table-striped table-hover">
                            <tr>
                                <th>S. No.</th>
                                <th>Item Name</th>
                                <th>Rate</th>
                                <th>Quantity</th>
                                <th>SGST </th>
                                <th>CGST</th>
                                <th>TDS</th>
                                <th>Amount</th>
                                <th>Ledger</th>
                                <th>Delete</th>
                            </tr>
                            <tr>
                                <td>1.</td>
                                <td>Sprinkler</td>
                                <td>100</td>
                                <td>1</td>
                                <td>9</td>
                                <td>9</td>
                                <td>0</td>

                                <td>118</td>
                                <td>Sprinkler</td>
                                <td>
                                    <asp:LinkButton runat="server" CssClass="label label-default" Text="Edit"></asp:LinkButton>&nbsp;
                                    <asp:LinkButton runat="server" CssClass="label label-danger" Text="Delete"></asp:LinkButton></td>
                            </tr>

                        </table>
                        <div class="row">
                            
                        </div>
                    </fieldset>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button Text="Save" runat="server" CssClass="btn btn-success btn-block" />

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">

                                <a href="#" class="btn btn-default btn-block">Clear</a>
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

