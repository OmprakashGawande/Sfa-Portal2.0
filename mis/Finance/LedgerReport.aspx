<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="LedgerReport.aspx.cs" Inherits="mis_Finance_LedgerReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content-header">
            <h1>Ledger Report
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
                                        <label>Financial Year<span style="color: red;">*</span></label>
                                        <select name="ctl00$ContainerBody$ddlMaritalStatus" id="ctl00_ContainerBody_ddlMaritalStatus" class="form-control">
                                            <option value="Select">Select</option>
                                            <option value="2017">2017-18</option>
                                            <option value="2018">2018-19</option>
                                            <option value="2019">2019-20</option>
                                            <option value="2020">2020-21</option>
                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Select Group Head <span style="color: red;">*</span></label>
                                        <select name="ctl00$ContainerBody$ddlMaritalStatus" id="ctl00_ContainerBody_ddlMaritalStatus" class="form-control">
                                            <option value="Select">Select</option>
                                            <option value="Current Liabilities">Current Liabilities</option>
                                            <option value="Current assest">Current assest</option>
                                            <option value="Salse and Accounts">Salse and Accounts</option>
                                            <option value="Purchange Account">Purchange Account</option>
                                            <option value="Direct Expences">Direct Expences</option>
                                            <option value="Indirect Income">Indirect Income</option>
                                            <option value="Indirect Expences">Indirect Expences</option>

                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Select Group Name <span style="color: red;">*</span></label>
                                        <select name="ctl00$ContainerBody$ddlMaritalStatus" id="ctl00_ContainerBody_ddlMaritalStatus" class="form-control">
                                            <option value="Select">Select</option>

                                            <option value="">Duties and Texes
                                            </option>
                                            <option value="">Sundry Creditores
                                            </option>
                                            <option value="">Bonus / Exgratia Payable
                                            </option>
                                            <option value="">Security Deposit Payable
                                            </option>
                                            <option value="">Festivle Advance
                                            </option>
                                            <option value="">Grain Advance
                                            </option>
                                            <option value="">Staff Advance
                                            </option>
                                            <option value="">Opening Stock
                                            </option>
                                            <option value="">Sundry Debtors
                                            </option>
                                            <option value="">Cash-in-hand
                                            </option>
                                            <option value="">licence Fee
                                            </option>
                                            <option value="">GST Sales
                                            </option>
                                            <option value="">Branch Purchages
                                            </option>
                                            <option value="">GST Purchages
                                            </option>
                                            <option value="">Trading Expences
                                            </option>
                                            <option value="">Interest of festival Advance
                                            </option>
                                            <option value="">Interest Recipt
                                            </option>
                                            <option value="">Mislinious Re
                                            </option>
                                            <option value="">Bank commion
                                            </option>
                                            <option value="">Computer Expeces
                                            </option>
                                            <option value="">Electricity Expences
                                            </option>
                                            <option value="">Godown Rent
                                            </option>
                                            <option value="">Licence Fee
                                            </option>
                                            <option value="Office expence">Office expence
                                            </option>
                                            <option value=" Office expence">Office Rent </option>


                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Select Sub Group<span style="color: red;">*</span></label>
                                        <select name="ctl00$ContainerBody$ddlMaritalStatus" id="ctl00_ContainerBody_ddlMaritalStatus" class="form-control">
                                            <option value="Select">Select</option>
                                            <option value="CGST">CGST</option>
                                            <option value="SGCT">SGCT</option>
                                            <option value="Salse and Accounts">Input vat 14%</option>
                                            <option value="Purchange Account">Input vat 15%</option>
                                            <option value="Direct Expences">Input vat 5%</option>
                                            <option value="Indirect Income">Output vat 14%</option>
                                            <option value="Indirect Expences">Output vat 15%</option>
                                            <option value="Indirect Expences">Output vat 5%</option>
                                            <option value="Indirect Expences">Biogas</option>
                                            <option value="Indirect Expences">Sundry Creditores(Br)</option>
                                            <option value="Indirect Expences">Sundry Creditores(SME)</option>

                                        </select>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Select Ledger<span style="color: red;">*</span></label>
                                        <select name="ctl00$ContainerBody$ddlMaritalStatus" id="ctl00_ContainerBody_ddlMaritalStatus" class="form-control">
                                            <option value="Select">Select</option>
                                            <option value="1">L1</option>
                                            <option value="2">L2</option>
                                            <option value="3">L3</option>
                                            <option value="4">L4</option>
                                            <option value="5">L5</option>
                                            <option value="6">L6</option>
                                            <option value="7">L7</option>
                                            <option value="8">L8</option>
                                            <option value="9">L9</option>
                                            <option value="10">L10</option>
                                        </select>
                                    </div>
                                </div>
                                  <div class="col-md-3">
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

                                <div class="col-md-3">
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


                                <div class="col-md-3">
                                    <div class="form-group">

                                        <input type="submit" name="ctl00$ContainerBody$btnSubmit" value="Show" onclick="return validateform();" id="ctl00_ContainerBody_btnSubmit" class="btn btn-block btn-primary" style="width: 40%; height: 40px; font-size: 16px;">
                                    </div>
                                </div>


                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <table id="example1" class="table table-bordered table-striped">
                                        <thead>
                                            <tr>
                                                <th>S.N.</th>
                                                <th>Ladger Name</th>
                                                <th>Group Head</th>
                                                <th>Group Name</th>
                                                <th>Sub Group</th>
                                                <th>Financial Year</th>
                                                <th>Dr.</th>
                                                <th>Cr.</th>

                                            </tr>

                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>1</td>
                                                <td><a href="LedgerDetailsReport.aspx" target="_blank">L1</a></td>
                                                <td>Current Liabilities</td>
                                                <td>Duties and Texes</td>
                                                <td>CGST</td>
                                                <th>2018-2019</th>
                                                <th>0</th>
                                                <th>0</th>

                                            </tr>
                                            <tr>
                                                <td>2</td>
                                                <td><a href="LedgerDetailsReport.aspx" target="_blank">L2</a></td>
                                                <td>Current Liabilities</td>
                                                <td>Duties and Texes</td>
                                                <td>SGST</td>
                                                <th>2018-2019</th>
                                                <th>0</th>
                                                <th>0</th>

                                            </tr>
                                            <tr>
                                                <td>3</td>
                                                <td><a href="LedgerDetailsReport.aspx" target="_blank">L3</a></td>
                                                <td>Current Liabilities</td>
                                                <td>Duties and Texes</td>
                                                <td>Input vat 14%</td>
                                                <th>2018-2019</th>
                                                <th>0</th>
                                                <th>0</th>

                                            </tr>
                                            <tr>
                                                <td>4</td>
                                                <td><a href="LedgerDetailsReport.aspx" target="_blank">L4</a></td>
                                                <td>Current Liabilities</td>
                                                <td>Duties and Texes</td>
                                                <td>Input vat 15%</td>
                                                <th>2018-2019</th>
                                                <th>0</th>
                                                <th>0</th>

                                            </tr>
                                            <tr>
                                                <td>5</td>
                                                <td><a href="LedgerDetailsReport.aspx" target="_blank">L5</a></td>
                                                <td>Current Liabilities</td>
                                                <td>Duties and Texes</td>
                                                <td>Input vat 5%</td>
                                                <th>2018-2019</th>
                                                <th>0</th>
                                                <th>0</th>

                                            </tr>
                                            <tr>
                                                <td>6</td>
                                                <td><a href="LedgerDetailsReport.aspx" target="_blank">L6</a></td>
                                                <td>Current Liabilities</td>
                                                <td>Duties and Texes</td>
                                                <td>Output vat 14%</td>
                                                <th>2018-2019</th>
                                                <th>0</th>
                                                <th>0</th>


                                            </tr>
                                            <tr>
                                                <td>7</td>
                                                <td><a href="LedgerDetailsReport.aspx" target="_blank">L7</a></td>
                                                <td>Current Liabilities</td>
                                                <td>Duties and Texes</td>
                                                <td>Output vat 14%</td>
                                                <th>2018-2019</th>
                                                <th>0</th>
                                                <th>0</th>

                                            </tr>
                                            <tr>
                                                <td>8</td>
                                                <td><a href="LedgerDetailsReport.aspx" target="_blank">L8</a></td>
                                                <td>Current Liabilities</td>
                                                <td>Duties and Texes</td>
                                                <td>Output vat 15%</td>

                                                <th>2018-2019</th>
                                                <th>0</th>
                                                <th>0</th>


                                            </tr>
                                            <tr>
                                                <td>9</td>
                                                <td><a href="LedgerDetailsReport.aspx" target="_blank">L9</a></td>
                                                <td>Current Liabilities</td>
                                                <td>Duties and Texes</td>
                                                <td>Output vat 5%</td>


                                                <th>2018-2019</th>
                                                <th>0</th>
                                                <th>0</th>

                                            </tr>


                                            <tr>
                                                <td>10</td>
                                                <td><a href="LedgerDetailsReport.aspx" target="_blank">L10</a></td>
                                                <td>Current Liabilities</td>
                                                <td>Sundry Creditores</td>
                                                <td>Biogas</td>

                                                <th>2018-2019</th>
                                                <th>0</th>
                                                <th>0</th>
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
