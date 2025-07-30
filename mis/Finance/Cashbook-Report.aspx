<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Cashbook-Report.aspx.cs" Inherits="mis_Finance_Cashbook_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    
    <div class="content-wrapper">
        <section class="content-header">
                <h1>
                    Cashbook Report
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                </ol>
            </section>

            <section class="content">

                <div class="row">
                    <div class="col-md-12">


                        <div class="box box-pramod">

                            <div class="box-body">


                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Select Financial Year<span style="color: red;">*</span></label>
                                            <select name="ctl00$ContainerBody$ddlMaritalStatus" id="ctl00_ContainerBody_ddlMaritalStatus" class="form-control">
                                                <option value="Select">Select</option>
                                                <option value="2017">2017</option>
                                                <option value="2018">2018</option>
                                                <option value="2019">2019</option>
                                                <option value="2020">2020</option>
                                            </select>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>From Date <span style="color: red;">*</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <input name="ctl00$ContainerBody$txtDOB" type="text" id="txtDOB" class="form-control" autocomplete="off" placeholder="Enter From Date" data-provide="datepicker" data-date-end-date="0d" onpaste="return false">
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>To Date <span style="color: red;">*</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <input name="ctl00$ContainerBody$txtDOB" type="text" id="txtDOB" class="form-control" autocomplete="off" placeholder="Enter To Date" data-provide="datepicker" data-date-end-date="0d" onpaste="return false">
                                            </div>

                                        </div>
                                    </div>

                                </div>



                                <div class="row">


                                    <div class="col-md-4">
                                        <div class="form-group">

                                            <input type="submit" name="ctl00$ContainerBody$btnSubmit" value="Show Report" onclick="return validateform();" id="ctl00_ContainerBody_btnSubmit" class="btn btn-block btn-primary" style="width:40%; height:40px; font-size:16px;">

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

                            
                            <div class="box-header with-border">
                                <a href="" id="at1" style="padding-right: 10px; width: 110px;">Export To Excel</a>

                                <a href="" id="at2" style="padding-right: 10px; width: 110px;">Print Report</a>
                            </div>
                             


                            <div class="box-body no-padding">
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>

                                        <tr style="background-color: #DCDCDC;">
                                            <th colspan="4"> </th>
                                            <th colspan="2"> Receipt</th>
                                            <th colspan="2"> Payment</th>
                                            <th colspan="2"> Cumulative balance</th>
                                        </tr>


                                        <tr>
                                            <th>S.N.</th>
                                            <th>Date</th>
                                            <th>Details</th>
                                            <th>Voucher No.</th>
                                            <th>Cash</th>
                                            <th>Bank</th>
                                            <th>Cash</th>
                                            <th>Bank</th>
                                            <th>Cash</th>
                                            <th>Bank</th>
                                        </tr>

                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>1</td>
                                            <td>30/06/2017</td>
                                            <td>Cash-Bank</td>
                                            <td>0</td>
                                            <td>0.00</td>
                                            <td>0.00</td>
                                            <td>0.00</td>
                                            <td>0.00</td>
                                            <td>0</td>
                                            <td>0</td>

                                        </tr>

                                        <tr>
                                            <td>2</td>
                                            <td>25/07/2017</td>
                                            <td><b>R.K.Computer</b><br /> Cash Received agst excess pmt</td>
                                            <td>R6128</td>
                                            <td>100.00</td>
                                            <td>0.00</td>
                                            <td>0.00</td>
                                            <td>0.00</td>
                                            <td>0.00</td>
                                            <td>0.00</td>

                                        </tr>

                                        <tr>
                                            <td>3</td>
                                            <td>25/07/2017</td>
                                            <td><b>Cash in Hand HO </b><br />Withdrawal from Bank</td>
                                            <td>C6134</td>
                                            <td>5,000.00</td>
                                            <td>0.00</td>
                                            <td>0.00</td>
                                            <td>0.00</td>
                                            <td>5,100.00</td>
                                            <td>0.00</td>

                                        </tr>

                                        <tr>
                                            <td>4</td>
                                            <td>25/07/2017</td>
                                            <td><b>Allahabad Bank Ho-234848 </b><br />Withdrawal from Bank</td>
                                            <td>C6134</td>
                                            <td>0.00</td>
                                            <td>0.00</td>
                                            <td>0.00</td>
                                            <td>5,000.00</td>
                                            <td>5,100.00</td>
                                            <td>-5,000.00</td>

                                        </tr>


                                        <tr>
                                            <td>5</td>
                                            <td>25/07/2017</td>
                                            <td><b>R.K.Computer</b><br />Agst Computer Bill</td>
                                            <td>P6105</td>
                                            <td>0.00</td>
                                            <td>0.00</td>
                                            <td>0.00</td>
                                            <td>500.00</td>
                                            <td>5,100.00</td>
                                            <td>-5,500.00</td>

                                        </tr>


                                        <tr>
                                            <td>5</td>
                                            <td>25/07/2017</td>
                                            <td><b>R.K.Computer</b><br />Paid against Reparing</td>
                                            <td>P6145</td>
                                            <td>0.00</td>
                                            <td>0.00</td>
                                            <td>250.00</td>
                                            <td>0.00</td>
                                            <td>4,850.00</td>
                                            <td>-5,500.00</td>

                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr style="background-color: #DCDCDC;">
                                            <th colspan="3"> Total Receipt:</th>
                                            <th colspan="1"> 5,100.00</th>
                                            <th colspan="1"> 0.00</th>

                                            <th colspan="1"> Total Exp: </th>

                                            <th colspan="1"> 250.00  </th>
                                            <th colspan="1"> 5,500.00  </th>
                                            <th colspan="1">   </th>
                                            <th colspan="1">   </th>

                                        </tr>
                                        <tr style="background-color: #DCDCDC;">
                                            <th colspan="3"> Opning Balance:</th>
                                            <th colspan="1"> 0.00</th>
                                            <th colspan="1"> 0.00</th>

                                            <th colspan="1"> Closing Balance: </th>
                                            <th colspan="1"> 4,850.00  </th>
                                            <th colspan="1">  -5,500.00 </th>
                                            <th colspan="1">   </th>
                                            <th colspan="1">   </th>
                                        </tr>
                                        <tr style="background-color: #DCDCDC;">
                                            <th colspan="3"> Grand Total :</th>
                                            <th colspan="1"> 5,100.00</th>
                                            <th colspan="1"> 0.00</th>

                                            <th colspan="1"> Grand Total: </th>
                                            <th colspan="1"> 5,100.00  </th>
                                            <th colspan="1"> 0.00  </th>
                                            <th colspan="1">   </th>
                                            <th colspan="1">   </th>
                                        </tr>

                                    </tfoot>



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
