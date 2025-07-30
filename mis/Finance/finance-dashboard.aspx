<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="finance-dashboard.aspx.cs" Inherits="mis_Finance_finance_dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    
    <div class="content-wrapper">
        <section class="content-header">
                <h1>
                    Dashboard
                    <small></small>
                </h1>
                <%--<ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                </ol>--%>
            </section>
         <section class="content">

                <!--<div class="row">
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <div class="info-box">
                            <span class="info-box-icon bg-aqua"><i class="fa fa-building" aria-hidden="true"></i></span>
                            <div class="info-box-content">
                                <span class="info-box-text">Class 1</span>
                                <span class="info-box-number">1 <small> Head Office</small></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <div class="info-box">
                            <span class="info-box-icon bg-red"><i class="fa fa-building-o" aria-hidden="true"></i></span>

                            <div class="info-box-content">
                                <span class="info-box-text">Class 2</span>
                                <span class="info-box-number">7 <small> Regional Office</small></span>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix visible-sm-block"></div>

                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <div class="info-box">
                            <span class="info-box-icon bg-green"><i class="fa fa-building-o" aria-hidden="true"></i></span>

                            <div class="info-box-content">
                                <span class="info-box-text">Class 3</span>
                                <span class="info-box-number">51 <small> Branch Office</small></span>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12">
                        <div class="info-box">
                            <span class="info-box-icon bg-yellow"><i class="ion ion-ios-people-outline"></i></span>

                            <div class="info-box-content">
                                <span class="info-box-text">Class 4</span>
                                <span class="info-box-number">100+ <small> venders</small></span>
                            </div>
                        </div>
                    </div>
                </div>--> 

                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-pramod">
                            <div class="box-header with-border">
                                <h3 class="box-title">Company History</h3>
                            </div>
                            <div class="box-body no-padding">
                                <table id="example1" class="table table-bordered table-striped">

                                    <tbody>

                                        <tr>
                                            <td>Current Period</td>
                                            <td>1-4-2018 to 31-03-2018 </td>
                                        </tr>
                                        <tr>
                                            <td>Current Date</td>
                                            <td>Tuesday, 31 Jul, 2018 </td>
                                        </tr>

                                        <tr>
                                            <td>Company Name</td>
                                            <td>MPAGRO HEAD OFFICE BHOPAL MP </td>
                                        </tr>

                                        <tr>
                                            <td>Date Of Last Entry</td>
                                            <td>31-Jul-2018</td>
                                        </tr>


                                        <tr>
                                            <td>Country</td>
                                            <td>India</td>
                                        </tr>
                                        <tr>
                                            <td>State</td>
                                            <td>Madhaya Pradesh</td>
                                        </tr>

                                        <tr>
                                            <td>City</td>
                                            <td>Bhopal</td>
                                        </tr>

                                        <tr>
                                            <td>Address</td>
                                            <td>Panchanan Bhopal</td>
                                        </tr>

                                        <tr>
                                            <td>Pincode</td>
                                            <td>462010</td>
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


