<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="LedgerReport2.aspx.cs" Inherits="mis_LedgerReport2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
       

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

                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Location<span style="color: red;">*</span></label>
                                        <select name="ctl00$ContainerBody$ddlMaritalStatus" id="ctl00_ContainerBody_ddlMaritalStatus" class="form-control">
                                            <option value="Select">Select</option>
                                            <option value="2017">Head Office</option>
                                            <option value="2018">Regional Office Bhopal</option>

                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <input type="submit" name="ctl00$ContainerBody$btnSubmit" value="Show" onclick="return validateform();" id="ctl00_ContainerBody_btnSubmit" class="btn btn-block btn-primary" style="width: 40%; height: 40px; font-size: 16px;">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <table id="example1" class="table table-bordered table-striped">
                                            <thead>
                                                <tr>
                                                    <th>S.N.</th>
                                                    <th>Ladger Head</th>
                                                    <th>Dr.</th>
                                                    <th>Cr.</th>
                                                    <th>Balance</th>
                                                </tr>

                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>1</td>
                                                    <td>Sundry Advance</td>
                                                    <td>1200</td>
                                                    <td>0</td>
                                                    <td>1200 Dr.</td>
                                                </tr>
                                                <tr>
                                                    <td>2</td>
                                                    <td>Subsidy Bullock Cart</td>
                                                    <td>1200</td>
                                                    <td>0</td>
                                                    <td>1200 Dr.</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>

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
