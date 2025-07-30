<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SpCustomerList.aspx.cs" Inherits="mis_Finance_SpCustomerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Customer (Debtor) List</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <table border="1" class="table table-bordered table-striped table-hover">
                                        <tr>
                                            <th>Sno.</th>
                                            <th>Store</th>
                                            
                                            <th>Finance & Account</th>
                                            <th>Sale Type</th>
                                            <th>Customer(Debtor) Name</th>
                                            <th>Amount</th>
                                            <th>View More</th>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td><span class="badge bg-green">Approved</span></td>
                                            
                                            <td><span class="badge bg-red">Pending</span></td>
                                            <td>Credit Sale</td>


                                            <td>Ankur Jain</td>
                                            <td>1500</td>
                                            <td><a href="../../mis/Finance/CreditSale.aspx" class="label label-info">View More</a></td>
                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td><span class="badge bg-green">Approved</span></td>
                                            
                                            <td><span class="badge bg-red">Pending</span></td>
                                            <td>Credit Sale</td>


                                            <td>Rajendra Saket</td>
                                            <td>1500</td>
                                            <td><a href="../../mis/Finance/CreditSale.aspx" class="label label-info">View More</a></td>
                                        </tr>
                                        <tr>
                                            <td>3</td>
                                            <td><span class="badge bg-green">Approved</span></td>
                                            
                                            <td><span class="badge bg-green">Approved</span></td>
                                            <td>Credit Sale</td>


                                            <td>Ramveer Dangi</td>
                                            <td>1500</td>
                                            <td><a href="../../mis/Finance/CreditSale.aspx" class="label label-info">View More</a></td>
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

