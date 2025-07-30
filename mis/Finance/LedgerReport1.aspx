<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="LedgerReport1.aspx.cs" Inherits="mis_Finance_LedgerReport1" %>

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
                                                    <th rowspan="2">S.N.</th>
                                                    <th rowspan="2">Ladger Head</th>
                                                    <th colspan="2">Head Office</th>
                                                    <th colspan="2">Bhopal - Division</th>
                                                    <th colspan="2">Gwalior - Division</th>
                                                    <th colspan="2">Indore - Division</th>
                                                    <th colspan="2">Jabalpur - Division</th>
                                                    <th colspan="2">Rewa - Division</th>
                                                    <th colspan="2">Sagar - Division</th>
                                                    <th colspan="2">Ujjain - Division</th>

                                                </tr>
                                                <tr>


                                                    <th>Dr.</th>
                                                    <th>Cr.</th>

                                                    <th>Dr.</th>
                                                    <th>Cr.</th>

                                                    <th>Dr.</th>
                                                    <th>Cr.</th>

                                                    <th>Dr.</th>
                                                    <th>Cr.</th>

                                                    <th>Dr.</th>
                                                    <th>Cr.</th>

                                                    <th>Dr.</th>
                                                    <th>Cr.</th>

                                                    <th>Dr.</th>
                                                    <th>Cr.</th>

                                                    <th>Dr.</th>
                                                    <th>Cr.</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>1</td>
                                                    <td>Sundry Advance</td>
                                                    <td>1250</td>
                                                    <td>0</td>
                                                    <td>1250</td>
                                                    <td>0</td>
                                                    <td>1250</td>
                                                    <td>0</td>
                                                    <td>1250</td>
                                                    <td>0</td>
                                                    <td>1250</td>
                                                    <td>0</td>
                                                    <td>1250</td>
                                                    <td>0</td>
                                                    <td>1250</td>
                                                    <td>0</td>
                                                    <td>1250</td>
                                                    <td>0</td>
                                                </tr>
                                                <tr>
                                                    <td>1</td>
                                                    <td>Food Corporation</td>
                                                    <td>1250</td>
                                                    <td>0</td>
                                                    <td>1250</td>
                                                    <td>0</td>
                                                    <td>1250</td>
                                                    <td>0</td>
                                                    <td>1250</td>
                                                    <td>0</td>
                                                    <td>1250</td>
                                                    <td>0</td>
                                                    <td>1250</td>
                                                    <td>0</td>
                                                    <td>1250</td>
                                                    <td>0</td>
                                                    <td>1250</td>
                                                    <td>0</td>
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
