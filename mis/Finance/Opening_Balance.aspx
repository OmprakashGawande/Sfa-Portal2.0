<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Opening_Balance.aspx.cs" Inherits="mis_Finance_Default" %>

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
                    <h3 class="box-title">Opening Balance </h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Group</label>
                                <asp:DropDownList ID="ddlItemCategory" runat="server" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Category</label>
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Name</label>
                                <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Unit</label>
                                <asp:TextBox ID="txtItemType" runat="server" ReadOnly="true" MaxLength="10" placeholder="Item Unit" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Financial year</label>
                                <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>2016-2017</asp:ListItem>
                                    <asp:ListItem>2017-2018</asp:ListItem>
                                    <asp:ListItem>2018-2019</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Opening Balance</label>
                                <asp:TextBox ID="TextBox1" runat="server" MaxLength="10" placeholder="Opening Balance" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Ledger</label>
                                <asp:DropDownList ID="ddlLedger" runat="server" CssClass="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-block btn-success" Text="Accept" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-block btn-default" Text="Clear" />
                            </div>
                        </div>
                        <div class="col-md-8"></div>
                    </div>
                </div>
                <div class="box-body">
                    <table border="1" class="table table-bordered table-striped table-hover">
                        <tr>
                            <th>S.No.</th>
                            <th>Item Category</th>
                            <th>Item Type</th>
                            <th>Item Name</th>
                            <th>Unit</th>
                            <th>Financial year</th>
                            <th>Opening Balance</th>
                            <th>Ledger</th>
                        </tr>
                        <tr>
                            <td>1.</td>
                            <td>Agriculture</td>
                            <td>sprinkler</td>
                            <td>Abc</td>
                            <td>BAGS</td>
                            <td>2018-2019</td>
                            <td>200</td>
                            <td>sprinkler</td>

                        </tr>
                    </table>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

