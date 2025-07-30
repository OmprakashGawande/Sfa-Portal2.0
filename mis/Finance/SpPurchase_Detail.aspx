<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SpPurchase_Detail.aspx.cs" Inherits="mis_Finance_SpPurchase_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <!-- general form elements -->

                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label2">Purchase Detail</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">


                            <div class="row">
                                <div class="col-md-12">
                                    <table border="1" class="table table-responsive table-striped table-bordered table-hover">
                                        <tr>
                                            <th>S.No</th>
                                            <th>Store</th>
                                          
                                            <th>Finance</th>
                                            <th>Receiving Date</th>
                                            <th>PO No.</th>
                                            <th>Vendor</th>

                                            <th>Amount (In Rs.)</th>
                                            <th>View More</th>

                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td><span class="badge bg-green">Approved</span></td>
                                            
                                            <td><span class="badge bg-red">Pending</span></td>
                                            <td>12/12/2018</td>
                                            <td>123</td>
                                            <td>R.K. Supplier</td>

                                            <td>12000</td>
                                            <td><a href="PurchaseMaster.aspx" class="label label-default">View More</a></td>

                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td><span class="badge bg-green">Approved</span></td>
                                            
                                            <td><span class="badge bg-red">Pending</span></td>
                                            <td>12/12/2018</td>
                                            <td>1234</td>
                                            <td>Raju Supplier</td>

                                            <td>1800</td>
                                            <td><a href="PurchaseMaster.aspx" class="label label-default">View More</a></td>

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

