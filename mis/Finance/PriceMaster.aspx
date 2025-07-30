<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PriceMaster.aspx.cs" Inherits="mis_Finance_PriceMaster" %>

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
                    <h3 class="box-title">Price Master</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Category</label>
                                <asp:DropDownList ID="ItemCategory" runat="server" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>Agriculture</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Type</label>
                                <asp:DropDownList ID="ddlItemType" runat="server" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>sprinkler</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Name</label>
                                <asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>sprinkler</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Item Unit</label>
                                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>Drum</asp:ListItem>
                                    <asp:ListItem>Role</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Purchase Price</label>
                                <asp:TextBox ID="txtPrice" runat="server" MaxLength="10" placeholder="Enter Purchase Price" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Selling Price</label>
                                <asp:TextBox ID="TextBox1" runat="server" MaxLength="10" placeholder="Enter Selling Price" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>

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
                            <th>S. No.</th>
                            <th>Item Category</th>
                            <th>Item Type</th>
                            <th>Item Name</th>
                            <th>Item Unit</th>
                            <th>Item Price</th>
                        </tr>
                        <tr>
                            <td>1.</td>
                            <td>Agriculture</td>
                            <td>sprinkler</td>
                            <td>sprinkler</td>
                            <td>Drum</td>
                            <td>400</td>
                        </tr>
                    </table>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

