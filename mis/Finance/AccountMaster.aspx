<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AccountMaster.aspx.cs" Inherits="mis_Finance_AccountMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    
    <div class="content-wrapper">
        <section class="content-header">
                <h1>
                    Account Master
                    <small></small>
                </h1> 
              <%--  <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                </ol>--%>
            </section>
             
            <section class="content">

                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-pramod">
                            

                            <div class="box-body">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Bank Name<span style="color: red;">*</span></label>
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
                                        <label>Branch Name<span style="color: red;">*</span></label>
                                        <select class="form-control">
                                            <option value="volvo">MP Nagar</option>
                                            <option value="saab">Arera Colony</option>
                                            <option value="mercedes">Ashoka Garden</option>
                                        </select>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>A/c Type<span style="color: red;">*</span></label>
                                        <select class="form-control">
                                            <option value="volvo">Select</option>
                                            <option value="saab">Saving Account</option>
                                            <option value="mercedes">Current Account</option>
                                        </select>

                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>A/c No.<span style="color: red;">*</span></label>
                                        <input name="ctl00$ContainerBody$txtEmp_ID" type="text" maxlength="50" id="txtEmp_ID" class="form-control" placeholder="Enter A/C No">

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
                                            <th>Branch Name</th>
                                            <th>A/c No</th>
                                            <th>A/c Type</th>
                                            <th>Centralised A/c</th>
                                            <th>Update</th>
                                        </tr>

                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>1</td>
                                            <td>Allahabad Bank, HO</td>
                                            <td>Bhopal</td>
                                            <td>7412589</td>
                                            <td>Saving A/c</td>
                                            <td>Manager Account</td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>

                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td>State Bank Of India, HO</td>
                                            <td>Arera Colony</td>
                                            <td>7400089</td>
                                            <td>Current A/c</td>
                                            <td>Manager Account</td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>
                                        </tr>
                                        <tr>
                                            <td>3</td>
                                            <td>Allahabad Bank Sweep Transfer Account</td>
                                            <td>Link Road</td>
                                            <td>7400412</td>
                                            <td>Saving A/c</td>
                                            <td>Manager Account</td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>
                                        </tr>

                                        <tr>
                                            <td>4</td>
                                            <td>Vidisha Bhopal Gramin Bank</td>
                                            <td>Arera Colony</td>
                                            <td>1232322</td>
                                            <td>Current A/c</td>
                                            <td>Manager Account</td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>
                                        </tr>
                                        <tr>
                                            <td>3</td>
                                            <td>Allahabad Bank Sweep Transfer Account</td>
                                            <td>Link Road</td>
                                            <td>5345333</td>
                                            <td>Saving A/c</td>
                                            <td>Manager Account</td>
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
        <script src="../js/jquery.dataTables.min.js"></script>
        <script src="../js/dataTables.bootstrap.js"></script>
        <link href="../css/dataTables.bootstrap.css" rel="stylesheet" />
        <link href="../css/jquery.dataTables.min.css" rel="stylesheet" />
    
     <script>
         $(function () {
             $(document).ready(function () {
                 $('#example1,#example2,#example3,#example4').DataTable({
                     dom: 'Bfrtip',
                     buttons: [
                         'print'
                     ],
                     "pageLength": 50
                 });
             });
         });
    </script>
</asp:Content>
