<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SpVendor_Detail.aspx.cs" Inherits="mis_Finance_SpVendor_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) --
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-6">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Vendor Detail</h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <table border="1" class="table table-bordered table table-responsive table-striped table-bordered table-hover">
                                        <tbody>
                                            <tr>
                                                <th>Ledger Name (Vendor)</th>
                                                <td>Hakimi Rubber Stamp</td>
                                            </tr>
                                            <tr>
                                                <th>Vendor Name</th>
                                                <td>Rajendra</td>
                                            </tr>
                                            <tr>
                                                <th>Mobile No</th>
                                                <td>9893098930</td>
                                            </tr>
                                            <tr>
                                                <th>Email ID</th>
                                                <td>rajendra@gmail.com</td>
                                            </tr>
                                            <tr>
                                                <th>State</th>
                                                <td>Mp</td>
                                            </tr>
                                            <tr>
                                                <th>Financial Year</th>
                                                <td>2017-2018</td>
                                            </tr>

                                            <tr>
                                                <th>Address</th>
                                                <td>Bhopal</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                            <%--<fieldset>
                                <legend>Applicant Detail</legend>
                                <div class="row">
                                    <div class="col-md-12">

                                        <table border="1" class="table table-bordered table table-responsive table-striped table-bordered table-hover">
                                            <tbody>
                                                <tr>
                                                    <th>Applicant Name</th>
                                                    <td>Ankur Jain
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>City</th>
                                                    <td>Bhopal</td>
                                                </tr>
                                                <tr>
                                                    <th>Mobiel No.</th>
                                                    <td>9893098930</td>
                                                </tr>
                                                <tr>
                                                    <th>Email ID</th>
                                                    <td>ankurjain112233@gmail.com</td>
                                                </tr>
                                                <tr>
                                                    <th>Address</th>
                                                    <td>C-20 3rd Floor Bhopal</td>
                                                </tr>


                                            </tbody>
                                        </table>
                                    </div>
                                </div>

                            </fieldset>--%>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Item Name<span style="color: red;"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlItemName" CssClass="form-control select2" ClientIDMode="Static">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Item Rate<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtRate"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <a href="SpVendor_Detail.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <table border="1" class="table table-responsive table-striped table-bordered table-hover">
                                        <tr>
                                            <th>S.No</th>
                                            <th>Item Name</th>
                                            <th>Item Rate</th>
                                            <th>Action</th>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>abc</td>
                                            <td>5</td>
                                            <td>
                                                <label class="label label-info">Edit</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td>abc</td>
                                            <td>5</td>
                                            <td>
                                                <label class="label label-info">Edit</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>3</td>
                                            <td>abc</td>
                                            <td>5</td>
                                            <td>
                                                <label class="label label-info">Edit</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>4</td>
                                            <td>abc</td>
                                            <td>10</td>
                                            <td>
                                                <label class="label label-info">Edit</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>5</td>
                                            <td>abc</td>
                                            <td>5</td>
                                            <td>
                                                <label class="label label-info">Edit</label>
                                            </td>
                                        </tr>
                                    </table>
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

