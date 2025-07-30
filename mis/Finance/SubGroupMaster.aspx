<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SubGroupMaster.aspx.cs" Inherits="mis_Finance_SubGroupMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    
    <div class="content-wrapper">
        <section class="content-header">
                <h1>
                    Create Sub Group
                    <small></small>
                </h1>
                <%--<ol class="breadcrumb">
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

                                        

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Select Group Head<span style="color: red;">*</span></label>
                                                <select name="ctl00$ContainerBody$ddlMaritalStatus" id="ctl00_ContainerBody_ddlMaritalStatus" class="form-control">
                                                    <option value="Select">Select</option>
                                                    <option value="Current Liabilities">Current Liabilities</option>
                                                    <option value="Current assest">Current Assets</option>
                                                    <option value="Salse and Accounts">Sales Accounts</option>
                                                    <option value="Purchange Account">Purchase Account</option>
                                                    <option value="Direct Expences">Indirect Expenses</option>
                                                    <option value="Indirect Income">Direct Expenses</option>
                                                    <option value="Direct Expences">Indirect Income</option>
                                                    <option value="Indirect Income">Direct income</option>
                                                </select>
                                            </div>
                                        </div>


                                        <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Select Group Name <span style="color: red;">*</span></label>
                                            <select name="ctl00$ContainerBody$ddlMaritalStatus" id="ctl00_ContainerBody_ddlMaritalStatus" class="form-control">
                                                <option value="Select">Select</option>

                                                <option value="">
                                                    Duties and Taxes
                                                </option>
                                                <option value="">
                                                    Sundry Creditores
                                                </option>
                                                <option value="">
                                                    Bonus / Exgratia Payable
                                                </option>
                                                <option value="">
                                                    Security Deposit Payable
                                                </option>
                                                <option value="">
                                                    Festivle Advance
                                                </option>
                                                <option value="">
                                                    Grain Advance
                                                </option>
                                                <option value="">
                                                    Staff Advance
                                                </option>
                                                <option value="">
                                                    Opening Stock
                                                </option>
                                                <option value="">
                                                    Sundry Debtors
                                                </option>
                                                <option value="">
                                                    Cash-in-hand
                                                </option>
                                                <option value="">
                                                    licence Fee
                                                </option>
                                                <option value="">
                                                    GST Sales
                                                </option>
                                                <option value="">
                                                    Branch Purchages
                                                </option>
                                                <option value="">
                                                    GST Purchages
                                                </option>
                                                <option value="">
                                                    Trading Expences
                                                </option>
                                                <option value="">
                                                    Interest of festival Advance
                                                </option>
                                                <option value="">
                                                    Interest Recipt
                                                </option>
                                                <option value="">
                                                    Mislinious Re
                                                </option>
                                                <option value="">
                                                    Bank commion
                                                </option>
                                                <option value="">
                                                    Computer Expeces
                                                </option>
                                                <option value="">
                                                    Electricity Expences
                                                </option>
                                                <option value="">
                                                    Godown Rent
                                                </option>
                                                <option value="">
                                                    Licence Fee
                                                </option>
                                                <option value="">
                                                    Office expence
                                                </option>
                                                <option value="">  Office Rent </option> 
                                            </select>
                                        </div>
                                    </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Sub Group Name <span style="color: red;">*</span></label>
                                                <input name="ctl00$ContainerBody$txtEmp_ID" type="text" maxlength="50" id="txtEmp_ID" class="form-control" placeholder="Enter Sub Group Name">

                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <input type="submit" name="ctl00$ContainerBody$btnSubmit" value="Accept" onclick="return validateform();" id="ctl00_ContainerBody_btnSubmit" class="btn btn-block btn-primary" style="margin-top: 25px;">
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
                            <div class="box-header with-border">
                                <h3 class="box-title">Sub Group</h3>
                            </div>
                            <div class="box-body no-padding">
                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>

                                            <th>S.N.</th>
                                            <th>Sub Group Name</th> 
                                            <th>Group Name</th> 
                                            <th>Group Head</th>
                                            <th>Action1</th>
                                             

                                        </tr>

                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>1</td>
                                            <td>CGST</td>
                                            
                                            <td>Duties and Taxes</td>
                                            <td>Current Liabilities</td>
                                            
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Edit" /></td>
                                           
                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td>SGST</td>
                                            
                                            <td>Duties and Taxes</td>
                                            <td>Current Liabilities</td> 
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Edit" /></td>
                                             
                                        </tr>
                                        <tr>
                                            <td>3</td>
                                            <td>Input vat 14%</td> 
                                            <td>Duties and Taxes</td>
                                            <td>Current Liabilities</td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Edit" /></td>
                                             
                                        </tr>
                                        <tr>
                                            <td>4</td>
                                            <td>Input vat 15%</td> 
                                            <td>Duties and Taxes</td>
                                            <td>Current Liabilities</td> 
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Edit" /></td>
                                             
                                        </tr>
                                        <tr>
                                            <td>5</td>
                                            <td>Input vat 5%</td> 
                                            <td>Duties and Taxes</td>
                                            <td>Current Liabilities</td> 
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Edit" /></td>
                                             
                                        </tr>
                                        <tr>
                                            <td>6</td>
                                            <td>Output vat 14%</td> 
                                            <td>Duties and Taxes</td>
                                            <td>Current Liabilities</td> 
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Edit" /></td>
                                            
                                        </tr>
                                        <tr>
                                            <td>7</td>
                                            <td>Output vat 14%</td>
                                            <td>Duties and Taxes</td>
                                            <td>Current Liabilities</td> 
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Edit" /></td>
                                            
                                        </tr>
                                        <tr>
                                            <td>8</td>
                                            <td> Output vat 15%</td> 
                                            <td>Duties and Taxes</td>
                                            <td>Current Liabilities</td> 
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Edit" /></td>
                                             
                                        </tr>
                                        <tr>
                                            <td>9</td>
                                            <td> Output vat 5%</td>
                                           
                                            <td>Duties and Taxes</td>
                                            <td>Current Liabilities</td>  
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Edit" /></td>
                                             
                                        </tr>
                                         

                                        <tr>
                                            <td>10</td>
                                            <td>Biogas</td>
                                            <td>Sundry Creditores</td>
                                            <td>Current Liabilities</td> 
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Edit" /></td>
                                             
                                        </tr>
                                        <tr>
                                            <td>11</td>
                                            <td>Sundry Creditores(Br)</td>
                                           
                                            <td>Sundry Creditores</td>
                                            <td>Current Liabilities</td> 
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Edit" /></td>
                                             
                                        </tr>
                                        <tr>
                                            <td>12</td>

                                            <td> Sundry Creditores(SME)</td>
                                            <td>Sundry Creditores</td>
                                            <td>Current Liabilities</td>  
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Edit" /></td>
                                             
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

