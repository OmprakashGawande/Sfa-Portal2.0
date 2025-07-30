<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Account-approval-officer.aspx.cs" Inherits="mis_Finance_Account_approval_officer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content-header">
            <h1>Account Approval officer
                    <small></small>
            </h1>
            <%--<ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            </ol>--%>
        </section>

        <section class="content">

            <div class="row">
                <div class="col-md-12">

                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-pramod">

                                <div class="box-body">


                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Select Branch Name <span style="color: red;">*</span></label>
                                            <select name="ctl00$ContainerBody$ddlMaritalStatus" id="ctl00_ContainerBody_ddlMaritalStatus" class="form-control">
                                                <option value="Select">Select</option>
                                                <option value="Current Liabilities">Head Office</option>
                                                <option value="Current assest">Branch1</option>
                                                <option value="Current assest">Branch2</option>
                                                <option value="Current assest">Branch3</option>
                                                <option value="Current assest">Branch4</option>
                                                <option value="Current assest">Branch5</option>
                                                <option value="Current assest">Branch6</option>
                                                <option value="Current assest">Branch7</option>
                                            </select>
                                        </div>
                                    </div>


                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Select Officer's Name<span style="color: red;">*</span></label>
                                            <select name="ctl00$ContainerBody$ddlMaritalStatus" id="ctl00_ContainerBody_ddlMaritalStatus" class="form-control">
                                                <option value="Select">Select</option>
                                                <option value="NAME1">NAME1</option>
                                                <option value="NAME1">NAME1</option>
                                                <option value="NAME1">NAME1</option>

                                            </select>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Select voucher Type <span style="color: red;">*</span></label>
                                            <select name="ctl00$ContainerBody$ddlMaritalStatus" id="ctl00_ContainerBody_ddlMaritalStatus" class="form-control">
                                                <option value="Select">Select</option>

                                                <option value="1">Payment Voucher
                                                </option>
                                                <option value="2">Receipt Voucher
                                                </option>
                                                <option value="3">Contra Voucher
                                                </option>
                                                <option value="4">Journal Voucher
                                                </option>

                                            </select>
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>




            <div class="row" style="margin-bottom: 10px;">
                <div class="col-md-12">
                    <div class="box-footer" align="center">

                        <input type="submit" name="ctl00$ContainerBody$btnSubmit" value="Accept" onclick="return validateform();" id="ctl00_ContainerBody_btnSubmit" class="btn btn-block btn-primary" style="width: 12%; height: 40px; font-size: 16px;">
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
                                        <th>Branch Name</th>
                                        <th>Officer's Name</th>
                                        <th>Voucher Type</th>
                                        <th>User Id</th>
                                        <th>Password</th>
                                    </tr>

                                </thead>
                                <tbody>
                                    <tr>
                                        <td>1</td>
                                        <td>Head Office</td>
                                        <td>NAME12</td>
                                        <td>Paymenr Voucher</td>
                                        <td>Admin</td>
                                        <th>PWD</th>
                                    </tr>

                                    <tr>
                                        <td>2</td>
                                        <td>Branch Name 1</td>
                                        <td>NAME13</td>
                                        <td>Paymenr Voucher</td>
                                        <td>Admin</td>
                                        <th>PWD</th>


                                    </tr>
                                    <tr>
                                        <td>3</td>
                                        <td>Branch Name 2</td>
                                        <td>NAME14</td>
                                        <td>Paymenr Voucher</td>
                                        <td>Admin</td>
                                        <th>PWD</th>


                                    </tr>

                                    <tr>
                                        <td>4</td>
                                        <td>Branch Name 4</td>
                                        <td>NAME15</td>
                                        <td>Paymenr Voucher</td>
                                        <td>Admin</td>
                                        <th>PWD</th>


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
