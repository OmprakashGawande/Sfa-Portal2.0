<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VoucherReg.aspx.cs" Inherits="mis_Finance_VoucherReg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
     <style>
        fieldset {
            border: 1px solid #936c32;
            padding: 0.35em 0.625em 0.75em;
            margin-bottom: 10px;
            border-radius: 5px;
            padding-left: 20px;
            border: 1px solid #936c32;
        }

        legend {
            padding: 2px 8px;
            border-radius: 10px;
            width: auto;
            border: 1px solid #936c32;
            font-size: 16px;
            font-weight: 600;
            color: #936c32;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                    <div class="col-md-12">
                        <fieldset>
                            <legend>Voucher Register</legend>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtToDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnSave" runat="server" Text="Search" CssClass="btn btn-outline-success" />
                                </div>
                            </div>
                            <div class="table-responsive">
                                <table class="table table-bordered">
                                    <tr>
                                        <th>S.No.</th>
                                        <th>Voucher Number</th>
                                        <th>Designation</th>
                                        <th>Voucher Type</th>
                                        <th>Amount</th>
                                        <th>Prepared By</th>
                                        <th>Accountant</th>
                                        <th>Authorised Signature</th>
                                    </tr>
                                    <tr>
                                        <td>1.</td>
                                        <td>HO22-23VR291</td>
                                        <td>Accountant</td>
                                        <td>Payment</td>
                                        <td>13000.00</td>
                                        <td>Abhishek Tiwari</td>
                                        <td style="color:red; font-weight:700">Pending</td>
                                        <td style="color:red; font-weight:700">Pending</td>
                                    </tr>
                                      <tr>
                                        <td>2.</td>
                                        <td>HO22-23VR292</td>
                                        <td>Manager</td>
                                        <td>Contra</td>
                                        <td>110315726.00</td>
                                        <td>Mahendra Verma</td>
                                        <td style="color:red; font-weight:700">Pending</td>
                                        <td style="color:red; font-weight:700">Pending</td>
                                    </tr>
                                    <tr>
                                        <td>3.</td>
                                        <td>HO22-23VR293</td>
                                        <td>Project Manager</td>
                                        <td>Journal</td>
                                        <td>234824.00</td>
                                        <td>Mahendra Verma</td>
                                        <td style="color:green; font-weight:700">Approve</td>
                                        <td style="color:red; font-weight:700">Pending</td>
                                    </tr>
                                </table>
                            </div>
                        </fieldset>
                    </div>
                </div>
        </section>
    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

