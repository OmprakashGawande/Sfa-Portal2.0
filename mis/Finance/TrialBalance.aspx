<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="TrialBalance.aspx.cs" Inherits="mis_Finance_TrialBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content-header">
            <h1>Trial Balance

                    <small></small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            </ol>
        </section>

        <section class="content">

            <div class="row">
                <div class="col-md-12">
                    <div class="box box-pramod">

                        <div class="box-body">
                            <div class="row">

                                <div class="col-md-3">
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

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>As On Date <span style="color: red;">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input name="ctl00$ContainerBody$txtDOB" type="text" id="txtDOB" class="form-control" autocomplete="off" placeholder="Enter..." data-provide="datepicker" data-date-end-date="0d" onpaste="return false">
                                        </div>
                                    </div>
                                </div>



                                <div class="col-md-2">
                                    <div class="form-group">
                                        <input type="submit" name="ctl00$ContainerBody$btnSubmit" value="Show Report" onclick="return validateform();" id="ctl00_ContainerBody_btnSubmit" class="btn btn-block btn-primary" style="margin-top: 25px;">
                                    </div>
                                </div>




                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <table id="example1" class="table table-bordered table-striped">
                                        <thead>
                                            <tr>
                                                <th>S.N.</th>
                                                <th>Group Name</th>
                                                <th>Ledger Name</th>
                                                <th>Opning Balance </th>
                                                <th>Dr</th>
                                                <th>Cr</th>
                                                <th>Closing Balance</th>

                                            </tr>

                                        </thead>
                                        <tbody>




                                            <tr>
                                                <td>1</td>
                                                <td>Sundry Creditor</td>
                                                <td>R.K.Computer</td>
                                                <td>0.00</td>
                                                <td>750	</td>
                                                <td>600.00</td>
                                                <td>150.00</td>
                                                <td>(Dr)</td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td>Advance SCERT for Building</td>
                                                <td>61,79,639.00</td>
                                                <td>0.00	</td>
                                                <td>0.00</td>
                                                <td>61,79,639.00</td>
                                                <td>(Cr)</td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td>Development of IT Department</td>
                                                <td>1,01,795.90</td>
                                                <td>0.00	</td>
                                                <td>0.00</td>
                                                <td>1,01,795.90</td>
                                                <td>(Dr)</td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td>TEST</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>Cr</td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td>TEST</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>Cr</td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td>TEST</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>Cr</td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td>TEST</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>Cr</td>
                                            </tr>

                                            <tr style="background-color: #DCDCDC;">
                                                <td></td>
                                                <td></td>
                                                <td>Total[Sundry Creditor ]</td>
                                                <td>6554,62,356.26</td>
                                                <td>750.00		</td>
                                                <td>600.00</td>
                                                <td>6554,62,206.26</td>
                                                <td>(Cr)</td>
                                            </tr>

                                            <tr>
                                                <td>2</td>
                                                <td>Expenses Payable</td>
                                                <td>Charges Payable</td>
                                                <td>1259,66,655.48</td>
                                                <td>00.00	</td>
                                                <td>00.00</td>
                                                <td>1259,66,655.48</td>
                                                <td>(Dr)</td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td>Purchase Payable</td>
                                                <td>2300,14,253.00</td>
                                                <td>0.00	</td>
                                                <td>0.00</td>
                                                <td>2300,14,253.00</td>
                                                <td>(Dr)</td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td>TEST</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>Cr</td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td>TEST</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>Cr</td>
                                            </tr>


                                            <tr style="background-color: #DCDCDC;">
                                                <td></td>
                                                <td></td>
                                                <td>Total[Expenses Payable]</td>
                                                <td>3559,80,908.48</td>
                                                <td>750.00		</td>
                                                <td>600.00</td>
                                                <td>3559,80,908.48</td>
                                                <td>(Cr)</td>
                                            </tr>

                                            <tr>
                                                <td>3</td>
                                                <td>Professional Fee/Remuneration</td>
                                                <td>IT/COMPUTER EXPERT FEE- H.O (220106)</td>
                                                <td>00.00</td>
                                                <td>500.00	</td>
                                                <td>0.00</td>
                                                <td>500.00</td>
                                                <td>(Dr)</td>
                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td>TEST</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>Cr</td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td>TEST</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>0.00</td>
                                                <td>Cr</td>
                                            </tr>

                                            <tr style="background-color: #DCDCDC">
                                                <td></td>
                                                <td></td>
                                                <td>Total[Professional Fee/Remuneration]</td>
                                                <td>0.00	</td>
                                                <td>500.00		</td>
                                                <td>0.00</td>
                                                <td>500</td>
                                                <td>(Dr)</td>
                                            </tr>



                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>

                                            <tr style="background-color: #DCDCDC;">
                                                <td></td>
                                                <td></td>
                                                <td>Grand Total</td>
                                                <td>28192,00,527.29	</td>
                                                <td>55,574.00</td>
                                                <td>55,574.00</td>
                                                <td>28192,00,527.29</td>
                                                <td></td>
                                            </tr>



                                        </tbody>
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
