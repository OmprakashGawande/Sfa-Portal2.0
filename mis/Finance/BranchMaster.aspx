<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BranchMaster.aspx.cs" Inherits="mis_Finance_BranchMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    
    <div class="content-wrapper">
         <section class="content-header">
                <h1>
                    Branch Master
                    <small></small>
                </h1> 
               <%-- <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                </ol>--%>
            </section> 
            <section class="content">


                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-pramod">
                            

                            <div class="box-body">

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Select Bank Name<span style="color: red;">*</span></label>
                                        <select class="form-control">
                                            <option value="volvo">Allahabad Bank, HO</option>
                                            <option value="saab">State Bank Of India, HO</option>
                                            <option value="mercedes">Allahabad Bank Sweep Transfer Account</option>
                                            <option value="audi">	Vidisha Bhopal Gramin Bank</option>
                                        </select>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Branch Name<span style="color: red;">*</span></label>
                                        <input name="ctl00$ContainerBody$txtEmp_ID" type="text" maxlength="50" id="txtEmp_ID" class="form-control" placeholder="Enter Branch Name">

                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>IFSC Code<span style="color: red;">*</span></label>
                                        <input name="ctl00$ContainerBody$txtEmp_ID" type="text" maxlength="50" id="txtEmp_ID" class="form-control" placeholder="Enter IFSC Code">

                                    </div>
                                </div>

                                <!--<div class="col-md-3">
                                    <div class="form-group">
                                        <label>Micro code<span style="color: red;">*</span></label>
                                        <input name="ctl00$ContainerBody$txtEmp_ID" type="text" maxlength="50" id="txtEmp_ID" class="form-control" placeholder="Enter Micro Code">

                                    </div>
                                </div>-->
                                <!--<div class="col-md-3">
                                    <div class="form-group">
                                        <label>Pin Code<span style="color: red;">*</span></label>
                                        <input name="ctl00$ContainerBody$txtEmp_ID" type="text" maxlength="50" id="txtEmp_ID" class="form-control" placeholder="Enter Pin Code">

                                    </div>
                                </div>-->


                            </div>

                            <div class="box-body">

                                 
                                
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Address<span style="color: red;">*</span></label>
                                        <textarea name="ctl00$ContainerBody$txtPostalAddress" rows="2" cols="20" id="txtPostalAddress" class="form-control" placeholder="Enter Address" style="min-height: 70px"></textarea>

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
                                            <th>Branch Name</th>
                                            <th>Bank Name</th>
                                            <th>IFSC CODE</th>
                                            <th>MICRO CODE</th>
                                            <th>Action</th>
                                        </tr>

                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>1</td>
                                            <td>TT NAGHAR BRANCH</td>
                                            <td>Allahabad Bank, HO</td>
                                            <td>101</td>
                                            <td>110</td>
                                            <td><input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>

                                        </tr>
                                        <tr>
                                            <td>2</td>
                                            <td>ASHOKA GARDEN</td>
                                            <td>State Bank Of India, HO</td>

                                            <td>102</td>
                                            <td>120</td>
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
