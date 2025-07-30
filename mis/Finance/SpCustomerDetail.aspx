<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SpCustomerDetail.aspx.cs" Inherits="mis_Finance_SpCustomerDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Customer Detail</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>Debtor Detail</legend>
                                        <table border="1" class="table table-bordered table-striped table-hover">
                                            <tr>
                                                <td>Date</td>
                                                <td>25/08/2018</td>
                                            </tr>
                                            <tr>
                                                <td>Name of Debtor</td>
                                                <td>Ankur Jain</td>
                                            </tr>
                                            <tr>
                                                <td>Name of Consignee</td>
                                                <td>Ankur Jain</td>
                                            </tr>
                                            <tr>
                                                <td>Amount</td>
                                                <td>1500</td>
                                            </tr>
                                        </table>
                                    </fieldset>

                                </div>
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>SALES DETAIL</legend>
                                        <table border="1" class="table table-bordered table-striped table-hover">
                                            <tr>
                                                <td>Location</td>
                                                <td>Branch Office</td>
                                            </tr>
                                            <tr>
                                                <td>Scheme</td>
                                                <td>ABC</td>
                                            </tr>
                                            <tr>
                                                <td>Order No</td>
                                                <td>123</td>
                                            </tr>
                                            <tr>
                                                <td>Registration No</td>
                                                <td>abc</td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <table border="1" class="table table-bordered table-striped table-hover">
                                        <tr>
                                            <th>S. No.</th>
                                            <th>Item Name</th>
                                            <th>Rate</th>
                                            <th>Quantity</th>
                                            <th>SGST</th>
                                            <th>CGST</th>
                                            <th>Amount</th>
                                        </tr>
                                        <tr>
                                            <td>1.</td>
                                            <td>Sprinkler</td>
                                            <td>100</td>
                                            <td>1</td>
                                            <td>9</td>
                                            <td>9</td>
                                            <td>118</td>
                                        </tr>
                                        <tr>
                                            <td colspan="6" class="">
                                                <p style="text-align: right; font-weight: 900;">Total</p>
                                            </td>
                                            <td>118</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                        </div>
                        <div class="box-body">
                            <div class="row">

                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label>Remark</label>
                                        <asp:TextBox placeholder="Mention Remark if you reject." ID="TextBox1" runat="server" Rows="4" Columns="3" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button Text="Approve" runat="server" CssClass="btn btn-success btn-block" />
                                        <asp:Button Text="Reject" runat="server" CssClass="btn btn-danger btn-block" />

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

