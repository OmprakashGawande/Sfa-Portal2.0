<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ChequeBookMaster.aspx.cs" Inherits="mis_Finance_ChequeBookMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    
    <div class="content-wrapper">
        <section class="content-header">
                <h1>
                    Cheque Book Master
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

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Select Bank Name<span style="color: red;">*</span></label>
                                        <select class="form-control">
                                            <option value="volvo">Allahabad Bank, HO</option>
                                            <option value="saab">State Bank Of India, HO</option>
                                            <option value="mercedes">Allahabad Bank Sweep Transfer Account</option>
                                            <option value="audi">Vidisha Bhopal Gramin Bank</option>
                                        </select>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Select Account No<span style="color: red;">*</span></label>
                                        <select class="form-control">
                                            <option value="volvo">123</option>
                                            <option value="saab">456</option>
                                            <option value="mercedes">789</option>
                                        </select>
                                    </div>
                                </div>


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Cheque Book No.<span style="color: red;">*</span></label>
                                        <input name="ctl00$ContainerBody$txtEmp_ID" type="text" maxlength="50" id="txtEmp_ID" class="form-control" placeholder="Cheque Book No.">

                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Cheque No. From.<span style="color: red;">*</span></label>
                                        <input name="ctl00$ContainerBody$txtEmp_ID" type="text" maxlength="50" id="txtEmp_ID" class="form-control" placeholder="No. From">

                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Cheque No. To<span style="color: red;">*</span></label>
                                        <input name="ctl00$ContainerBody$txtEmp_ID" type="text" maxlength="50" id="txtEmp_ID" class="form-control" placeholder="No. To">

                                    </div>
                                </div>

                            </div>


                            <div class="box-body">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <input type="submit" name="ctl00$ContainerBody$btnSubmit" value="Accept" onclick="return validateform();" id="ctl00_ContainerBody_btnSubmit" class="btn btn-block btn-primary" style="margin-top: 25px;">
                                    </div>
                                </div>
                            </div>



                        </div>
                    </div>
                </div> 
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-pramod">


                            <div class="box-body">

                                <table id="example1" class="table table-bordered table-striped">
                                    <thead>
                                        <tr>

                                            <th>S.N.</th>
                                            <th>Bank Name</th>
                                            <th>Account No</th>
                                            <th>Cheque Book No.</th>
                                            <th>Cheque No. From</th>
                                            <th>Cheque No. To</th>
                                            <th>Cheque Information</th>
                                            <th>Update</th>
                                        </tr>

                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>1</td>
                                            <td>Allahabad Bank, HO</td>
                                            <td>123</td>
                                            <td>7412589</td>
                                            <td>1001</td>
                                            <td>1024</td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>

                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td>State Bank Of India, HO</td>
                                            <td>456</td>
                                            <td>7400089</td>
                                            <td>1041</td>
                                            <td>1084</td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>
                                        </tr>
                                        <tr>
                                            <td>3</td>
                                            <td>Allahabad Bank Sweep Transfer Account</td>
                                            <td>789</td>
                                            <td>7400412</td>
                                            <td>1010</td>
                                            <td>1064</td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>
                                        </tr>

                                        <tr>
                                            <td>4</td>
                                            <td>Vidisha Bhopal Gramin Bank</td>
                                            <td>123</td>
                                            <td>1232322</td>
                                            <td>1044</td>
                                            <td>1054</td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>
                                        </tr>
                                        <tr>
                                            <td>3</td>
                                            <td>Allahabad Bank Sweep Transfer Account</td>
                                            <td>456</td>
                                            <td>5345333</td>
                                            <td>1074</td>
                                            <td>1084</td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>
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

