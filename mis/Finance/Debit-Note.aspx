<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Debit-Note.aspx.cs" Inherits="mis_Finance_Debit_Note" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content-header">
            <h1>Debit Note 
                    <small>(Purchase Return)</small>
            </h1>
            <%--<ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                </ol>--%>
        </section>
        <section class="content">
            <div class="box box-pramod">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Voucher No <span style="color: red;">*</span></label>
                                <input name="cttxtDOB" type="text" id="txtDOB" class="form-control" placeholder="Enter Voucher No" />
                            </div>
                        </div>

                        <div class="col-md-5"></div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Voucher Date <span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <input name="ctl00$ContainerBody$txtDOB" type="text" id="txtDOB" class="form-control" autocomplete="off" placeholder="Enter Date" data-provide="datepicker" onpaste="return false">
                                </div>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Name of Vendor <span style="color: red;">*</span></label>
                                <asp:DropDownList runat="server" CssClass="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Original Invoice No <span style="color: red;">*</span></label>
                                <asp:DropDownList runat="server" CssClass="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="col-md-2"></div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Date <span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <input name="ctl00$ContainerBody$txtDOB" type="text" id="txtDOB" class="form-control" autocomplete="off" placeholder="Enter Date" data-provide="datepicker" onpaste="return false">
                                </div>

                            </div>
                        </div>
                    </div>
                    <fieldset class="box-body">
                        <legend>Item Detail</legend>
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
                                    <label>Ledger of Item</label>
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
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
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
                                </div>
                            </div>
                        </div>

                    </fieldset>
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="box-body">

                                <legend>Action </legend>
                                <div class="row">
                                    <div class="col-md-10">
                                        <div class="form-group">
                                            <textarea placeholder="Enter Narration" class="form-control" style=""></textarea>
                                        </div>

                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <input type="submit" name="ctl00$ContainerBody$btnSubmit" value="Accept" onclick="return validateform();" id="ctl00_ContainerBody_btnSubmit" class="btn btn-block btn-primary">
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <input type="submit" name="ctl00$ContainerBody$btnSubmit" value="Cancel" onclick="return validateform();" id="ctl00_ContainerBody_btnSubmit" class="btn btn-block btn-primary">
                                        </div>
                                    </div>
                                </div>


                            </fieldset>
                        </div>
                    </div>


                </div>
            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>
