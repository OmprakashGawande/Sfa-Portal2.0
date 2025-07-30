<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VaucherApproval.aspx.cs" Inherits="mis_Finance_VaucherApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    
    <div class="content-wrapper">
        
        <section class="content-header">
                <h1>
                    Vaucher Approval
                    <small></small>
                </h1>
               <%-- <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                </ol>--%>
            </section> 
            <section class="content">

                <div class="row">
                    <div class="col-md-12">
                         
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box box-pramod">

                                    <div class="box-body">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <input type="radio" checked="checked" value="" name="A" />&nbsp;&nbsp;Pending For Approval 
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <input type="radio" value="" name="A" /> Approved 
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-pramod">
                             
                            <div class="box-body no-padding">
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>

                                            <th>S.N.</th>
                                            <th>Ledger Name</th>
                                            <th>Date</th>
                                            <th>Voucher Number</th>
                                            <th>Particular</th>
                                            <th>Cheque No</th>
                                            <th>Cheque Date</th>
                                            <th>Amount</th>
                                            <th>Approve</th>


                                        </tr>

                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>1</td>
                                            <td>Hospitality Expenses HO</td>
                                            <td>07/04/2017</td>
                                            <td>bpl101</td>
                                            <td>Paid To Sunil Kumar</td>
                                            <td>187055</td>
                                            <td>07/04/2017</td>
                                            <td>4417</td>
                                            

                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Approve" /></td>

                                        </tr>
                                        
                                        <tr>
                                            <td>1</td>
                                            <td>Office Expenses HO</td>
                                            <td>07/04/2017</td>
                                            <td>bpl101</td>
                                            <td>XYZ</td>
                                            <td>187055</td>
                                            <td>07/04/2017</td>
                                            <td>4417</td>
                                            

                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Approve" /></td>

                                        </tr>

                                        <tr>
                                            <td>1</td>
                                            <td>PAY & ALLOWANCES</td>
                                            <td>07/04/2017</td>
                                            <td>bpl101</td>
                                            <td>ABC</td>
                                            <td>187055</td>
                                            <td>07/04/2017</td>
                                            <td>4417</td>
                                             

                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Approve" /></td>

                                        </tr>

                                        <tr>
                                            <td>1</td>
                                            <td>Salary</td>
                                            <td>07/04/2017</td>
                                            <td>bpl101</td>
                                            <td>Paid To Sunil Kumar</td>
                                            <td>187055</td>
                                            <td>07/04/2017</td>
                                            <td>4417</td>
                                             

                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Approve" /></td>

                                        </tr>

                                        <tr>
                                            <td>1</td>
                                            <td>Hospitality Expenses HO</td>
                                            <td>07/04/2017</td>
                                            <td>bpl101</td>
                                            <td>Paid To Sunil Kumar</td>
                                            <td>187055</td>
                                            <td>07/04/2017</td>
                                            <td>4417</td>
                                         
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Approve" /></td>

                                        </tr>

                                        <tr>
                                            <td>1</td>
                                            <td>Hospitality Expenses HO</td>
                                            <td>07/04/2017</td>
                                            <td>bpl101</td>
                                            <td>Paid To Sunil Kumar</td>
                                            <td>187055</td>
                                            <td>07/04/2017</td>
                                            <td>4417</td>
                                            

                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Approve" /></td>

                                        </tr>



                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

