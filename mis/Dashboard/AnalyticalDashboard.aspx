<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AnalyticalDashboard.aspx.cs" Inherits="mis_Dashboard_AnalyticalDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">

    <link href="../../../mis/Dashboard/css/bootstrap.css" rel="stylesheet" />
    <link href="../../../mis/Dashboard/js/morris/morris-0.4.3.min.css" rel="stylesheet" />
    <link href="../../../mis/Dashboard/css/custom-styles.css" rel="stylesheet" />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css' />
    <link rel="stylesheet" href="../../../mis/Dashboard/js/Lightweight-Chart/cssCharts.css" />
    <style>
        #page-wrapper {
            position: unset !important;
            top: 0px !important;
        }

        .content {
            padding: 0px !important;
        }

        #page-wrapper {
            box-shadow: none !important;
            -moz-box-shadow: none !important;
            -webkit-box-shadow: none !important;
        }

        .alert-warning {
            font-size: 12px;
        }

        .number h3 {
            font-size: 24px;
            font-weight: 600;
        }

        .board .panel .icon .fa {
            font-size: 24px;
            padding: 14px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <div class="header">
                <h1><span style="color: #006eb3;">Analytical Dashboard</span>
                </h1>

            </div>

        </section>
        <section class="content">
            <div id="page-wrapper">
                <div id="page-wrapper">
                    <!--<div class="header">
                <h1 class="page-header">
                    <span style="color: #006eb3;"></span>
                </h1>
            </div>-->
                    <div id="page-inner">
                        <div class="alert alert-warning">
                            <div class="row">
                                <div class="col-lg-4"><strong>Turnover in 2019-20 Fin. Yr. (Regional wise till 31 Jan 2020) :</strong></div>
                                <div class="col-lg-8" style="padding-left: 15px;">
                                    <marquee behavior="scroll" direction="left" onmouseover="this.stop();" onmouseout="this.start();">
                                        <strong>Bhopal</strong> - 44.99 Cr. | 
                                        <strong>Indore</strong> - 34.92 Cr. | 
                                        <strong>Ujjain</strong> - 30.24 Cr | 
                                        <strong>Jabalpur</strong> - 49.42 Cr. | 
                                        <strong>Gwalior</strong> - 24.79 Cr. | 
                                        <strong>Satna</strong> - 32.45 Cr. | 
                                        <strong>Sagar</strong> - 10.55 Cr. | 
                                        
                                        
                                        <strong>Total</strong> - 227.36 Cr.</marquee>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="fancy-title  title-dotted-border">
                                    <h3>Current Status</h3>
                                </div>
                            </div>

                            <div class="col-md-3 col-sm-12 col-xs-12">
                                <div class="board">
                                    <div class="panel panel-primary">
                                        <div class="number">
                                            <h3>
                                                <h3>227.3702 <span>(In CR)</span></h3>
                                                <small>Total Sales</small>
                                            </h3>
                                        </div>
                                        <div class="icon">
                                            <i class="fa fa-check-circle fa-5x red"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3 col-sm-12 col-xs-12">
                                <div class="board">
                                    <div class="panel panel-primary" style="padding: 13px 15px;">
                                        <div class="number">
                                            <h3>
                                                <a href="#" data-toggle="modal" data-target="#BadiProductionTop">
                                                    <%--<h3>6188.78 <span>(Mt. Tn.)</span>--%>
                                                    <h3>10542.656 <span>(Mt. Tn.)</span>

                                                    </h3>
                                                </a>
                                                <small>Badi Production</small><br />
                                                <span>(1<sup>st</sup> & 2<sup>nd</sup> Quarter)</span>
                                            </h3>
                                        </div>
                                        <div class="icon">
                                            <i class="fa fa-check-circle fa-5x blue"></i>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3 col-sm-12 col-xs-12">
                                <div class="board">
                                    <div class="panel panel-primary" style="padding: 13px 15px;">
                                        <div class="number">

                                            <h3>
                                                <a href="#" data-toggle="modal" data-target="#IndrapuriProduction">
                                                    <h3>186500 <span>(In PKT)</span></h3>
                                                </a>
                                                <small>Indrapuri Production</small><br />
                                                <span>(1<sup>st</sup> & 2<sup>nd</sup> Quarter)</span>
                                            </h3>
                                        </div>
                                        <div class="icon">
                                            <i class="fa fa-check-circle fa-5x green"></i>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3 col-sm-12 col-xs-12">
                                <div class="board">
                                    <div class="panel panel-primary">
                                        <div class="number">
                                            <h3>
                                                <h3>452</h3>
                                                <small>Total Employees</small>
                                            </h3>
                                        </div>
                                        <div class="icon">
                                            <i class="fa fa-check-circle fa-5x yellow"></i>
                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="fancy-title  title-dotted-border">
                                    <h3>Corporation Position</h3>
                                </div>
                            </div>
                            <div class="col-sm-4 col-xs-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading red">
                                        <div class="card-title">
                                            <div class="title">Turnover in Last 5 Year</div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <!--<canvas id="bar-chart" class="chart"></canvas>-->
                                        <div class="table-responsive">
                                            <table class="table table-striped table-bordered table-hover">
                                                <thead>
                                                    <tr>
                                                        <th>Financial Year</th>
                                                        <th>Turnover in INR (In CR)</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>2018-19</td>
                                                        <td><a href="#" data-toggle="modal" data-target="#myModal2"><b>298.5927</b></a></td>
                                                    </tr>
                                                    <tr>
                                                        <td>2017-18</td>
                                                        <td><a href="#" data-toggle="modal" data-target="#myModal3"><b>292.2449</b></a></td>
                                                    </tr>
                                                    <tr>
                                                        <td>2016-17</td>
                                                        <td><a href="#" data-toggle="modal" data-target="#myModal4"><b>540.1916</b></a></td>
                                                    </tr>
                                                    <tr>
                                                        <td>2015-16</td>
                                                        <td><a href="#" data-toggle="modal" data-target="#myModal5"><b>587.7769</b></a></td>
                                                    </tr>
                                                    <tr>
                                                        <td>2014-15</td>
                                                        <td><a href="#" data-toggle="modal" data-target="#myModal6"><b>1259.6281</b></a></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 col-xs-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading red">
                                        <div class="card-title">
                                            <div class="title">Sales (In Last 5 Year)</div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <table class="table table-striped table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Financial Year</th>
                                                    <th>Sales - In CR</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>2018-19</td>
                                                    <td><a href="#" data-toggle="modal" data-target="#"><b>298.5927</b></a></td>
                                                </tr>
                                                <tr>
                                                    <td>2017-18</td>
                                                    <td><a href="#" data-toggle="modal" data-target="#"><b>292.2449</b></a></td>
                                                </tr>
                                                <tr>
                                                    <td>2016-17</td>
                                                    <td><a href="#" data-toggle="modal" data-target="#"><b>540.1916</b></a></td>
                                                </tr>
                                                <tr>
                                                    <td>2015-16</td>
                                                    <td><a href="#" data-toggle="modal" data-target="#"><b>587.7769</b></a></td>
                                                </tr>
                                                <tr>
                                                    <td>2014-15</td>
                                                    <td><a href="#" data-toggle="modal" data-target="#"><b>1259.6281</b></a></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 col-xs-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading red">
                                        <div class="card-title">
                                            <div class="title">Profits (In Last 5 Year)</div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <table class="table table-striped table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Financial Year</th>
                                                    <th>Profits - In CR</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>2018-19</td>
                                                    <td><a href="#"><b>22.1329</b></a></td>
                                                </tr>
                                                <tr>
                                                    <td>2017-18</td>
                                                    <td><a href="#"><b>13.1329</b></a></td>
                                                </tr>
                                                <tr>
                                                    <td>2016-17</td>
                                                    <td><a href="#" data-toggle="modal" data-target="#myModal9"><b>22.0899</b></a></td>
                                                </tr>
                                                <tr>
                                                    <td>2015-16</td>
                                                    <td><a href="#" data-toggle="modal" data-target="#myModal8"><b>25.7770</b></a></td>
                                                </tr>
                                                <tr>
                                                    <td>2014-15</td>
                                                    <td><a href="#" data-toggle="modal" data-target="#myModal7"><b>21.3192</b></a></td>
                                                </tr>
                                            </tbody>
                                        </table>

                                        <!--<canvas id="line-chart" class="chart"></canvas>-->
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-lg-12">
                                <div class="fancy-title  title-dotted-border">
                                    <h3>Top 5 Branches (Achievement % In Last Year)</h3>
                                </div>
                                <div class="row branches">
                                    <div class="col-xs-6 col-md-2">
                                        <div class="panel panel-default">
                                            <div class="panel-body easypiechart-panel PT10">
                                                <h4>Singrauli</h4>
                                                <div class="easypiechart" id="easypiechart-blue" data-percent="638.71">
                                                    <span class="percent">638.71</span>
                                                </div>
                                                <div style="padding: 0 20px;">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>Target (In Lac)</th>
                                                                <th>127.42</th>
                                                            </tr>
                                                            <tr>
                                                                <th>Achievement</th>
                                                                <th>813.85</th>
                                                            </tr>
                                                            <tr>
                                                                <th>Achievement %</th>
                                                                <th>638.71</th>
                                                            </tr>
                                                        </thead>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-md-2">
                                        <div class="panel panel-default">
                                            <div class="panel-body easypiechart-panel PT10">
                                                <h4>Chhattarpur</h4>
                                                <div class="easypiechart" id="easypiechart-orange" data-percent="322">
                                                    <span class="percent">322.65</span>
                                                </div>
                                                <div style="padding: 0 20px;">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>Target (In Lac)</th>
                                                                <th>160.79</th>
                                                            </tr>
                                                            <tr>
                                                                <th>Achievement</th>
                                                                <th>518.79</th>
                                                            </tr>
                                                            <tr>
                                                                <th>Achievement %</th>
                                                                <th>322.65</th>
                                                            </tr>
                                                        </thead>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-md-2">
                                        <div class="panel panel-default">
                                            <div class="panel-body easypiechart-panel PT10">
                                                <h4>Dewas</h4>
                                                <div class="easypiechart" id="easypiechart-teal" data-percent="200">
                                                    <span class="percent">200.14</span>
                                                </div>
                                                <div style="padding: 0 20px;">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>Target (In Lac)</th>
                                                                <th>966.59</th>
                                                            </tr>
                                                            <tr>
                                                                <th>Achievement</th>
                                                                <th>1934.58</th>
                                                            </tr>
                                                            <tr>
                                                                <th>Achievement %</th>
                                                                <th>200.14</th>
                                                            </tr>
                                                        </thead>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-md-2">
                                        <div class="panel panel-default">
                                            <div class="panel-body easypiechart-panel PT10">
                                                <h4>Katni</h4>
                                                <div class="easypiechart" id="easypiechart-red" data-percent="289">
                                                    <span class="percent">289.60</span>
                                                </div>
                                                <div style="padding: 0 20px;">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>Target (In Lac)</th>
                                                                <th>133.29</th>
                                                            </tr>
                                                            <tr>
                                                                <th>Achievement</th>
                                                                <th>386.01</th>
                                                            </tr>
                                                            <tr>
                                                                <th>Achievement %</th>
                                                                <th>289.60</th>
                                                            </tr>
                                                        </thead>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-6 col-md-2">
                                        <div class="panel panel-default">
                                            <div class="panel-body easypiechart-panel PT10">
                                                <h4>Chhindwara</h4>
                                                <div class="easypiechart" id="easypiechart-purple" data-percent="205">
                                                    <span class="percent">205.59</span>
                                                </div>
                                                <div style="padding: 0 20px;">
                                                    <table class="table table-striped table-bordered table-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>Target (In Lac)</th>
                                                                <th>403.88</th>
                                                            </tr>
                                                            <tr>
                                                                <th>Achievement</th>
                                                                <th>830.33</th>
                                                            </tr>
                                                            <tr>
                                                                <th>Achievement %</th>
                                                                <th>205.59</th>
                                                            </tr>
                                                        </thead>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-8 col-xs-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading green">
                                        <div class="card-title">
                                            <div class="title">Production - Last 5 Year</div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-sm-4 col-xs-12">
                                                <h4>Indrapuri</h4>
                                                <table class="table table-striped table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>Financial Year</th>
                                                            <th>Mt. Tn.</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>2018-19</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#Indrapuri19">127.99</a> </td>
                                                        </tr>
                                                        <tr>
                                                            <td>2017-18</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#Indrapuri18">133.79</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2016-17</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#Indrapuri17">255.64</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2015-16</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#Indrapuri16">395.91</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2014-15</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#Indrapuri15">378</a></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="col-sm-4 col-xs-12">
                                                <h4>Babai</h4>
                                                <table class="table table-striped table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>Financial Year</th>
                                                            <th>Mt. Tn.</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>2018-19</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#Babai19">3464.07</a> </td>
                                                        </tr>
                                                        <tr>
                                                            <td>2017-18</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#Babai18">4854.45</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2016-17</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#Babai17">5835.42</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2015-16</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#Babai16">3100.37</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2014-15</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#Babai15">6052.60</a></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                            <div class="col-sm-4 col-xs-12">
                                                <h4>Badi</h4>

                                                <table class="table table-striped table-bordered table-hover">
                                                    <thead>
                                                        <tr>
                                                            <th>Financial Year</th>
                                                            <th>Mt. Tn.</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <tr>
                                                            <td>2018-19</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#badi18">13146.753</a> </td>
                                                        </tr>
                                                        <tr>
                                                            <td>2017-18</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#badi17">17607.687</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2016-17</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#badi16">20238.177</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2015-16</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#badi15">19753.035</a></td>
                                                        </tr>
                                                        <tr>
                                                            <td>2014-15</td>
                                                            <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#badi14">19483.561</a></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 col-xs-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading green">
                                        <div class="card-title">
                                            <div class="title">Balance Sheet Last 5 Year</div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <table class="table table-striped table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Financial Year</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td><a href="#" class="btn btn-primary btn-xs" >2018-19</a></td>
                                                </tr>
                                                <tr>
                                                    <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#bsheet1718">2017-18</a></td>
                                                </tr>
                                                <tr>
                                                    <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#bsheet1617">2016-17</a></td>
                                                </tr>
                                                <tr>
                                                    <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#bsheet1516">2015-16</a></td>
                                                </tr>
                                                <tr>
                                                    <td><a href="#" class="btn btn-primary btn-xs" data-toggle="modal" data-target="#bsheet1415">2014-15</a></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading blue">
                                        <div class="card-title">
                                            <div class="title">Legal Cases</div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="list-group">
                                            <a href="#" class="list-group-item">
                                                <span class="badge blue" style="color: #fff;">164</span>
                                                <i class="fa fa-list"></i>TOTAL LEGAL CASES
                                            </a>
                                            <a href="#" class="list-group-item">
                                                <span class="badge red">164</span>
                                                <i class="fa fa-list"></i>OPEN LEGAL CASES
                                            </a>
                                            <a href="#" class="list-group-item">
                                                <span class="badge">0</span>
                                                <i class="fa fa-list"></i>CLOSED LEGAL CASES
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-5 col-xs-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading blue">
                                        <div class="card-title">
                                            <div class="title">RTI</div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="list-group row">
                                            <div class="col-lg-6">
                                                <a href="#" class="list-group-item">
                                                    <span class="badge blue" style="color: #fff;">96</span>
                                                    <i class="fa fa-list-alt"></i>TOTAL RTI
                                                </a>
                                                <a href="#" class="list-group-item">
                                                    <span class="badge red">10</span>
                                                    <i class="fa fa-list-alt"></i>OPEN RTI
                                                </a>
                                                <a href="#" class="list-group-item">
                                                    <span class="badge">86</span>
                                                    <i class="fa fa-list-alt"></i>CLOSED RTI
                                                </a>
                                            </div>
                                            <div class="col-lg-6">
                                                <a href="#" class="list-group-item">
                                                    <span class="badge blue" style="color: #fff;">9</span>
                                                    <i class="fa fa-list-alt"></i>TOTAL FIRST APPEAL
                                                </a>
                                                <a href="#" class="list-group-item">
                                                    <span class="badge red">7</span>
                                                    <i class="fa fa-list-alt"></i>OPEN FIRST APPEAL
                                                </a>
                                                <a href="#" class="list-group-item">
                                                    <span class="badge">2</span>
                                                    <i class="fa fa-list-alt"></i>CLOSED FIRST APPEAL
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 col-xs-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading blue">
                                        <div class="card-title">
                                            <div class="title">MD'S Session at Corporation</div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <table class="table table-striped table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Name</th>
                                                    <th>Designation</th>
                                                    <th>Date</th>
                                                    <th>To Date</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>Shri Alok Kumar Singh</td>
                                                    <td>M.D.</td>
                                                    <td>14/06/2019</td>
                                                    <td>Present</td>
                                                </tr>
                                                <tr>
                                                    <td>Shri Anirudh Mukerjee</td>
                                                    <td>M.D.</td>
                                                    <td>23/03/2019</td>
                                                    <td>14/06/2019</td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-sm-3 col-xs-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading red">
                                        <div class="card-title">
                                            <div class="title">Scheme Details</div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <table class="table table-striped table-bordered table-hover">
                                            <tbody>
                                                <tr>
                                                    <th colspan="3">Financial Year - 2018-19</th>
                                                </tr>
                                                <tr>
                                                    <th>Category</th>
                                                    <th>Cout</th>
                                                    <th>Amount Disbursement</th>
                                                </tr>
                                                <tr>
                                                    <td>SC</td>
                                                    <td>127</td>
                                                    <td>1654400</td>
                                                </tr>
                                                <tr>
                                                    <td>ST</td>
                                                    <td>329</td>
                                                    <td>4285000</td>
                                                </tr>
                                                <tr>
                                                    <td>Genral</td>
                                                    <td>1449</td>
                                                    <td>17543800</td>
                                                </tr>
                                                <tr>
                                                    <th>Total</th>
                                                    <th>1905</th>
                                                    <th>23483200</th>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-5 col-xs-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading red">
                                        <div class="card-title">
                                            <div class="title">Employees Age Details</div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <table class="table table-striped table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th>Age greater than 50 Years & service duration more than 20 Years</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td><a href="doc/Staff more than 50 years.xlsx" class="btn btn-primary btn-xs">View</a></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-4 col-xs-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading red">
                                        <div class="card-title">
                                            <div class="title">Leaves Details</div>
                                        </div>
                                    </div>
                                    <div class="panel-body">
                                        <table class="table table-striped table-bordered table-hover">
                                            <thead>
                                                <tr>
                                                    <th colspan="6">Total Availed Leaves</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td colspan="6"><a href="doc/Leave%20Record%20District-wise.xlsx" class="btn btn-primary btn-xs">View</a></td>
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


            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Production (Badai) In Last 5 Year</h4>
                        </div>
                        <div class="modal-body">
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <!--<button type="button" class="btn btn-primary">Save changes</button>-->
                        </div>
                    </div>
                </div>
            </div>

            <!-- Financial Year 2018-19 -->
            <div class="modal fade" id="myModal2" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title text-center" id="myModalLabel">BRANCHWISE PERFORMANCE REPORT FROM
                        <br />
                                01/04/2018 TO 31/03/2019</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered">
                                <tr>
                                    <th>S.No.</th>
                                    <th>BRANCH</th>
                                    <th>TARGET 2018-19</th>
                                    <th>TURNOVER WITHOUT RTE CURRENT YEAR</th>
                                    <th colspan="2">ACHIVEMENTS OF TARGET WITHOUT RTE CURRENT YEAR IN %</th>
                                </tr>
                                <tr>
                                    <td>1 </td>
                                    <td>BHOPAL</td>
                                    <td>514.14</td>
                                    <td>685.86</td>
                                    <td>133.40</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>2 </td>
                                    <td>H,BAD</td>
                                    <td>1023.08</td>
                                    <td>1286.32</td>
                                    <td>125.73</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>3 </td>
                                    <td>RAISEN</td>
                                    <td>514.79</td>
                                    <td>470.19</td>
                                    <td>91.34</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>4 </td>
                                    <td>VIDISHA</td>
                                    <td>652.65</td>
                                    <td>809.19</td>
                                    <td>123.99</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>5 </td>
                                    <td>SEHORE</td>
                                    <td>588.60</td>
                                    <td>319.97</td>
                                    <td>54.36</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>6 </td>
                                    <td>BIAORA</td>
                                    <td>894.83</td>
                                    <td>453.35</td>
                                    <td>50.66</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>7 </td>
                                    <td>BETUL</td>
                                    <td>476.07</td>
                                    <td>620.99</td>
                                    <td>130.44</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>8 </td>
                                    <td>HARDA</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>4664.16</th>
                                    <th>4645.87</th>
                                    <th>99.61</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>9 </td>
                                    <td>GWALIOR</td>
                                    <td>631.46</td>
                                    <td>348.28</td>
                                    <td>55.15</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>10 </td>
                                    <td>DATIA</td>
                                    <td>432.74</td>
                                    <td>431.78</td>
                                    <td>99.78</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>11 </td>
                                    <td>SHIVPURI</td>
                                    <td>419.08</td>
                                    <td>425.74</td>
                                    <td>101.59</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>12 </td>
                                    <td>GUNA</td>
                                    <td>262.49</td>
                                    <td>203.34</td>
                                    <td>77.47</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>13 </td>
                                    <td>ASHOK NAGAR</td>
                                    <td>514.94</td>
                                    <td>178.92</td>
                                    <td>34.75</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>14 </td>
                                    <td>BHIND</td>
                                    <td>663.05</td>
                                    <td>473.68</td>
                                    <td>71.44</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>15 </td>
                                    <td>MORENA</td>
                                    <td>559.89</td>
                                    <td>647.23</td>
                                    <td>115.60</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>15 </td>
                                    <td>ASHOK NAGAR</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>#DIV/0!</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>16 </td>
                                    <td>SHEOPUR<span style="mso-spacerun: yes">&nbsp;</span></td>
                                    <td>298.96</td>
                                    <td>299.96</td>
                                    <td>100.33</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>3782.61</th>
                                    <th>3008.93</th>
                                    <th>79.55</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>17 </td>
                                    <td>JABALPUR</td>
                                    <td>510.66</td>
                                    <td>705.37</td>
                                    <td>138.13</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>18 </td>
                                    <td>CHINDWARA</td>
                                    <td>403.88</td>
                                    <td>830.33</td>
                                    <td>205.59</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>19 </td>
                                    <td>NARSINGPUR</td>
                                    <td>365.22</td>
                                    <td>385.95</td>
                                    <td>105.68</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>20 </td>
                                    <td>SEONI</td>
                                    <td>804.50</td>
                                    <td>992.39</td>
                                    <td>123.35</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>21 </td>
                                    <td>BALAGHAT</td>
                                    <td>914.42</td>
                                    <td>1252.79</td>
                                    <td>137.00</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>22 </td>
                                    <td>MANDLA</td>
                                    <td>1566.36</td>
                                    <td>1285.88</td>
                                    <td>82.09</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>23 </td>
                                    <td>DINDORI</td>
                                    <td>1081.86</td>
                                    <td>964.35</td>
                                    <td>89.14</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>24 </td>
                                    <td>KATNI</td>
                                    <td>133.29</td>
                                    <td>386.01</td>
                                    <td>289.60</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>5780.19</th>
                                    <th>6803.07</th>
                                    <th>117.70</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>25 </td>
                                    <td>INDORE</td>
                                    <td>463.59</td>
                                    <td>311.06</td>
                                    <td>67.10</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>26 </td>
                                    <td>KHANDWA</td>
                                    <td>588.49</td>
                                    <td>494.77</td>
                                    <td>84.07</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>27 </td>
                                    <td>KHARGONE</td>
                                    <td>1117.56</td>
                                    <td>895.61</td>
                                    <td>80.14</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>28 </td>
                                    <td>DHAR</td>
                                    <td>2849.30</td>
                                    <td>1022.65</td>
                                    <td>35.89</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>29 </td>
                                    <td>JHABUA</td>
                                    <td>815.89</td>
                                    <td>873.89</td>
                                    <td>107.11</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>30 </td>
                                    <td>BARWANI</td>
                                    <td>561.28</td>
                                    <td>729.18</td>
                                    <td>129.91</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>6396.11</th>
                                    <th>4327.16</th>
                                    <th>67.65</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>31 </td>
                                    <td>UJJAIN</td>
                                    <td>929.15</td>
                                    <td>1104.04</td>
                                    <td>118.82</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>32 </td>
                                    <td>MANDSOUR</td>
                                    <td>479.57</td>
                                    <td>445.03</td>
                                    <td>92.80</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>33 </td>
                                    <td>RATLAM</td>
                                    <td>878.25</td>
                                    <td>572.25</td>
                                    <td>65.16</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>34 </td>
                                    <td>SHAJAPUR</td>
                                    <td>169.40</td>
                                    <td>208.19</td>
                                    <td>122.90</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>35 </td>
                                    <td>DEWAS</td>
                                    <td>966.59</td>
                                    <td>1934.58</td>
                                    <td>200.14</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>36 </td>
                                    <td>NEEMUCH</td>
                                    <td>247.89</td>
                                    <td>216.38</td>
                                    <td>87.29</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>37 </td>
                                    <td>AAGAR</td>
                                    <td>254.07</td>
                                    <td>209.68</td>
                                    <td>82.53</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>3924.92</th>
                                    <th>4690.15</th>
                                    <th>119.50</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>38</td>
                                    <td>SAGAR</td>
                                    <td>364.28</td>
                                    <td>194.17</td>
                                    <td>53.30</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>39</td>
                                    <td>DAMOH</td>
                                    <td>206.92</td>
                                    <td>240.13</td>
                                    <td>116.05</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>40</td>
                                    <td>PANNA</td>
                                    <td>209.68</td>
                                    <td>348.73</td>
                                    <td>166.32</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>41</td>
                                    <td>TIKAMGARH</td>
                                    <td>194.26</td>
                                    <td>312.30</td>
                                    <td>160.76</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>42</td>
                                    <td>CHHATARPUR</td>
                                    <td>160.79</td>
                                    <td>518.79</td>
                                    <td>322.65</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>1135.93</th>
                                    <th>1614.12</th>
                                    <th>142.10</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>43</td>
                                    <td>REWA</td>
                                    <td>472.36</td>
                                    <td>833.92</td>
                                    <td>176.54</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>44</td>
                                    <td>SATNA</td>
                                    <td>603.31</td>
                                    <td>1288.17</td>
                                    <td>213.52</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>45</td>
                                    <td>SIDHI</td>
                                    <td>373.61</td>
                                    <td>594.81</td>
                                    <td>159.21</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>46</td>
                                    <td>SINGRAULI</td>
                                    <td>127.42</td>
                                    <td>813.85</td>
                                    <td>638.71</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>47</td>
                                    <td>SHAHDOL</td>
                                    <td>615.96</td>
                                    <td>327.24</td>
                                    <td>53.13</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>48</td>
                                    <td>ANOOPPUR</td>
                                    <td>940.19</td>
                                    <td>743.87</td>
                                    <td>79.12</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>49</td>
                                    <td>UMARIA</td>
                                    <td>407.72</td>
                                    <td>168.11</td>
                                    <td>41.23</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <td>3540.57</th>
                            <th>4769.97</th>
                                        <th>134.72</th>
                                        <th>%</th>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>GRAND TOTAL</th>
                                    <th>29224.49</th>
                                    <th>29859.27</th>
                                    <th>102.17</th>
                                    <th>%</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <!--<button type="button" class="btn btn-primary">Save changes</button>-->
                        </div>
                    </div>
                </div>
            </div>

            <!-- Financial Year 2017-18 -->
            <div class="modal fade" id="myModal3" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title text-center" id="myModalLabel">BRANCHWISE PERFORMANCE REPORT FROM<br />
                                01/04/2017 TO 31/03/2018</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>S.No.</th>
                                    <th>BRANCH</th>
                                    <th>TARGET 2017-18</th>
                                    <th>TURNOVER WITHOUT RTE CURRENT YEAR</th>
                                    <th>ACHIVEMENTS OF TARGET WITHOUT RTE CURRENT YEAR IN</th>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>1</td>
                                    <td>2</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>1 </td>
                                    <td>BHOPAL</td>
                                    <td>1468.50</td>
                                    <td>514.14</td>
                                    <td>35.01</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>2 </td>
                                    <td>H,BAD</td>
                                    <td>2002.00</td>
                                    <td>1023.08</td>
                                    <td>51.10</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>3 </td>
                                    <td>RAISEN</td>
                                    <td>1250.00</td>
                                    <td>514.79</td>
                                    <td>41.18</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>4 </td>
                                    <td>VIDISHA</td>
                                    <td>1600.00</td>
                                    <td>652.65</td>
                                    <td>40.79</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>5 </td>
                                    <td>SEHORE</td>
                                    <td>1844.00</td>
                                    <td>588.60</td>
                                    <td>31.92</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>6 </td>
                                    <td>BIAORA</td>
                                    <td>1720.00</td>
                                    <td>894.83</td>
                                    <td>52.03</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>7 </td>
                                    <td>BETUL</td>
                                    <td>1107.00</td>
                                    <td>476.07</td>
                                    <td>43.01</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>8 </td>
                                    <td>HARDA</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>10991.50</th>
                                    <th>4664.16</th>
                                    <th>42.43</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>9 </td>
                                    <td>GWALIOR</td>
                                    <td>771.96</td>
                                    <td>631.46</td>
                                    <td>81.80</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>10 </td>
                                    <td>DATIA</td>
                                    <td>415.00</td>
                                    <td>432.74</td>
                                    <td>104.27</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>11 </td>
                                    <td>SHIVPURI</td>
                                    <td>778.75</td>
                                    <td>419.08</td>
                                    <td>53.81</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>12 </td>
                                    <td>GUNA</td>
                                    <td>1198.00</td>
                                    <td>262.49</td>
                                    <td>21.91</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>13 </td>
                                    <td>ASHOK NAGAR</td>
                                    <td>706.50</td>
                                    <td>514.94</td>
                                    <td>72.89</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>14 </td>
                                    <td>BHIND</td>
                                    <td>765.00</td>
                                    <td>663.05</td>
                                    <td>86.67</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>15 </td>
                                    <td>MORENA</td>
                                    <td>614.00</td>
                                    <td>559.89</td>
                                    <td>91.19</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>15 </td>
                                    <td>ASHOK NAGAR</td>
                                    <td>&nbsp;</td>
                                    <td>0.00</td>
                                    <td>#DIV/0!</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>16 </td>
                                    <td>SHEOPUR</td>
                                    <td>226.00</td>
                                    <td>298.96</td>
                                    <td>132.28</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>5475.21</th>
                                    <th>3782.61</th>
                                    <th>69.09</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>17 </td>
                                    <td>JABALPUR</td>
                                    <td>950.00</td>
                                    <td>510.66</td>
                                    <td>53.75</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>18 </td>
                                    <td>CHINDWARA</td>
                                    <td>1600.00</td>
                                    <td>403.88</td>
                                    <td>25.24</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>19 </td>
                                    <td>NARSINGPUR</td>
                                    <td>1100.00</td>
                                    <td>365.22</td>
                                    <td>33.20</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>20 </td>
                                    <td>SEONI</td>
                                    <td>1250.00</td>
                                    <td>804.50</td>
                                    <td>64.36</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>21 </td>
                                    <td>BALAGHAT</td>
                                    <td>1350.00</td>
                                    <td>914.42</td>
                                    <td>67.73</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>22 </td>
                                    <td>MANDLA</td>
                                    <td>1300.00</td>
                                    <td>1566.36</td>
                                    <td>120.49</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>23 </td>
                                    <td>DINDORI</td>
                                    <td>700.00</td>
                                    <td>1081.86</td>
                                    <td>154.55</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>24 </td>
                                    <td>KATNI</td>
                                    <td>550.00</td>
                                    <td>133.29</td>
                                    <td>24.23</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>8800.00</th>
                                    <th>5780.19</th>
                                    <th>65.68</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>25 </td>
                                    <td>INDORE</td>
                                    <td>1654.00</td>
                                    <td>463.59</td>
                                    <td>28.03</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>26 </td>
                                    <td>KHANDWA</td>
                                    <td>1191.00</td>
                                    <td>588.49</td>
                                    <td>49.41</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>27 </td>
                                    <td>KHARGONE</td>
                                    <td>1525.00</td>
                                    <td>1117.56</td>
                                    <td>73.28</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>28 </td>
                                    <td>DHAR</td>
                                    <td>2006.00</td>
                                    <td>2849.30</td>
                                    <td>142.04</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>29 </td>
                                    <td>JHABUA</td>
                                    <td>975.00</td>
                                    <td>815.89</td>
                                    <td>83.68</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>30 </td>
                                    <td>BARWANI</td>
                                    <td>935.00</td>
                                    <td>561.28</td>
                                    <td>60.03</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>8286.00</th>
                                    <th>6396.11</th>
                                    <th>77.19</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>31 </td>
                                    <td>UJJAIN</td>
                                    <td>1800.00</td>
                                    <td>929.15</td>
                                    <td>51.62</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>32 </td>
                                    <td>MANDSOUR</td>
                                    <td>1200.00</td>
                                    <td>479.57</td>
                                    <td>39.96</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>33 </td>
                                    <td>RATLAM</td>
                                    <td>1950.00</td>
                                    <td>878.25</td>
                                    <td>45.04</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>34 </td>
                                    <td>SHAJAPUR</td>
                                    <td>1150.00</td>
                                    <td>169.40</td>
                                    <td>14.73</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>35 </td>
                                    <td>DEWAS</td>
                                    <td>2200.00</td>
                                    <td>966.59</td>
                                    <td>43.94</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>36 </td>
                                    <td>NEEMUCH</td>
                                    <td>950.00</td>
                                    <td>247.89</td>
                                    <td>26.09</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>37 </td>
                                    <td>AAGAR</td>
                                    <td>750.00</td>
                                    <td>254.07</td>
                                    <td>33.88</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>10000.00</th>
                                    <th>3924.92</th>
                                    <th>39.25</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>38</td>
                                    <td>SAGAR</td>
                                    <td>1016.50</td>
                                    <td>364.28</td>
                                    <td>35.84</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>39</td>
                                    <td>DAMOH</td>
                                    <td>1244.10</td>
                                    <td>206.92</td>
                                    <td>16.63</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>40</td>
                                    <td>PANNA</td>
                                    <td>560.00</td>
                                    <td>209.68</td>
                                    <td>37.44</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>41</td>
                                    <td>TIKAMGARH</td>
                                    <td>947.70</td>
                                    <td>194.26</td>
                                    <td>20.50</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>42</td>
                                    <td>CHHATARPUR</td>
                                    <td>1340.10</td>
                                    <td>160.79</td>
                                    <td>12.00</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>5108.40</th>
                                    <th>1135.93</th>
                                    <th>22.24</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>43</td>
                                    <td>REWA</td>
                                    <td>916.60</td>
                                    <td>472.36</td>
                                    <td>51.53</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>44</td>
                                    <td>SATNA</td>
                                    <td>1415.00</td>
                                    <td>603.31</td>
                                    <td>42.64</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>45</td>
                                    <td>SIDHI</td>
                                    <td>349.00</td>
                                    <td>373.61</td>
                                    <td>107.05</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>46</td>
                                    <td>SINGRAULI</td>
                                    <td>288.50</td>
                                    <td>127.42</td>
                                    <td>44.17</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>47</td>
                                    <td>SHAHDOL</td>
                                    <td>760.70</td>
                                    <td>615.96</td>
                                    <td>80.97</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>48</td>
                                    <td>ANOOPPUR</td>
                                    <td>821.30</td>
                                    <td>940.19</td>
                                    <td>114.48</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>49</td>
                                    <td>UMARIA</td>
                                    <td>479.90</td>
                                    <td>407.72</td>
                                    <td>84.96</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>5031.00</th>
                                    <th>3540.57</th>
                                    <th>70.38</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <th>GRAND TOTAL</th>
                                    <th>53692.11</th>
                                    <th>29224.49</th>
                                    <th>54.43</th>
                                    <th>%</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <!--<button type="button" class="btn btn-primary">Save changes</button>-->
                        </div>
                    </div>
                </div>
            </div>

            <!-- Financial Year 2016-17 -->
            <div class="modal fade" id="myModal4" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title text-center" id="myModalLabel">BRANCHWISE PERFORMANCE REPORT FROM
                        <br />
                                01/04/2016 TO 31/03/2017</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>S.No.</th>
                                    <th>BRANCH</th>
                                    <th>TARGET 2016-17</th>
                                    <th>TURNOVER WITHOUT RTE CURRENT YEAR</th>
                                    <th>ACHIVEMENTS OF TARGET WITHOUT RTE CURRENT YEAR IN</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>1</td>
                                    <td>2</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>1 </td>
                                    <td>BHOPAL</td>
                                    <td>1472.57</td>
                                    <td>1460.42</td>
                                    <td>99</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>2 </td>
                                    <td>H,BAD</td>
                                    <td>3850.00</td>
                                    <td>1805.40</td>
                                    <td>47</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>3 </td>
                                    <td>RAISEN</td>
                                    <td>1128.60</td>
                                    <td>1454.43</td>
                                    <td>129</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>4 </td>
                                    <td>VIDISHA</td>
                                    <td>1542.75</td>
                                    <td>1552.00</td>
                                    <td>101</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>5 </td>
                                    <td>SEHORE</td>
                                    <td>2200.00</td>
                                    <td>1606.14</td>
                                    <td>73</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>6 </td>
                                    <td>BIAORA</td>
                                    <td>2200.00</td>
                                    <td>1720.80</td>
                                    <td>78</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>7 </td>
                                    <td>BETUL</td>
                                    <td>1104.40</td>
                                    <td>959.37</td>
                                    <td>87</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>8 </td>
                                    <td>HARDA</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>0</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>TOTAL</td>
                                    <td>13498.32</td>
                                    <td>10558.56</td>
                                    <td>78</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>9 </td>
                                    <td>GWALIOR</td>
                                    <td>662.37</td>
                                    <td>778.81</td>
                                    <td>118</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>10 </td>
                                    <td>DATIA</td>
                                    <td>553.30</td>
                                    <td>355.43</td>
                                    <td>64</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>11 </td>
                                    <td>SHIVPURI</td>
                                    <td>1028.50</td>
                                    <td>702.12</td>
                                    <td>68</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>12 </td>
                                    <td>GUNA</td>
                                    <td>1945.30</td>
                                    <td>1245.69</td>
                                    <td>64</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>13 </td>
                                    <td>ASHOK NAGAR</td>
                                    <td>0.00</td>
                                    <td>840.73</td>
                                    <td>0</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>14 </td>
                                    <td>BHIND</td>
                                    <td>867.70</td>
                                    <td>1073.90</td>
                                    <td>124</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>15 </td>
                                    <td>MORENA</td>
                                    <td>769.70</td>
                                    <td>773.70</td>
                                    <td>101</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>15 </td>
                                    <td>ASHOK NAGAR</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>#DIV/0!</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>16 </td>
                                    <td>SHEOPUR</td>
                                    <td>231.00</td>
                                    <td>212.08</td>
                                    <td>92</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>TOTAL</td>
                                    <td>6057.87</td>
                                    <td>5982.46</td>
                                    <td>99</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>17 </td>
                                    <td>JABALPUR</td>
                                    <td>1430.10</td>
                                    <td>954.74</td>
                                    <td>67</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>18 </td>
                                    <td>CHINDWARA</td>
                                    <td>1485.00</td>
                                    <td>773.30</td>
                                    <td>52</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>19 </td>
                                    <td>NARSINGPUR</td>
                                    <td>1540.03</td>
                                    <td>1143.57</td>
                                    <td>74</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>20 </td>
                                    <td>SEONI</td>
                                    <td>1925.00</td>
                                    <td>1288.69</td>
                                    <td>67</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>21 </td>
                                    <td>BALAGHAT</td>
                                    <td>1089.00</td>
                                    <td>1644.08</td>
                                    <td>151</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>22 </td>
                                    <td>MANDLA</td>
                                    <td>1374.96</td>
                                    <td>1078.11</td>
                                    <td>78</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>23 </td>
                                    <td>DINDORI</td>
                                    <td>742.70</td>
                                    <td>661.13</td>
                                    <td>89</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>24 </td>
                                    <td>KATNI</td>
                                    <td>1127.40</td>
                                    <td>468.12</td>
                                    <td>42</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>TOTAL</td>
                                    <td>10714.19</td>
                                    <td>8011.74</td>
                                    <td>75</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>25 </td>
                                    <td>INDORE</td>
                                    <td>1927.70</td>
                                    <td>1385.05</td>
                                    <td>72</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>26 </td>
                                    <td>KHANDWA</td>
                                    <td>1940.77</td>
                                    <td>1456.00</td>
                                    <td>75</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>27 </td>
                                    <td>KHARGONE</td>
                                    <td>1875.17</td>
                                    <td>2122.12</td>
                                    <td>113</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>28 </td>
                                    <td>DHAR</td>
                                    <td>3960.60</td>
                                    <td>2865.43</td>
                                    <td>72</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>29 </td>
                                    <td>JHABUA</td>
                                    <td>1809.70</td>
                                    <td>1351.48</td>
                                    <td>75</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>30 </td>
                                    <td>BARWANI</td>
                                    <td>967.27</td>
                                    <td>850.56</td>
                                    <td>88</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>TOTAL</td>
                                    <td>12481.21</td>
                                    <td>10030.64</td>
                                    <td>80</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>31 </td>
                                    <td>UJJAIN</td>
                                    <td>1667.49</td>
                                    <td>1680.28</td>
                                    <td>101</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>32 </td>
                                    <td>MANDSOUR</td>
                                    <td>1680.69</td>
                                    <td>1153.83</td>
                                    <td>69</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>33 </td>
                                    <td>RATLAM</td>
                                    <td>2531.14</td>
                                    <td>1902.55</td>
                                    <td>75</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>34 </td>
                                    <td>SHAJAPUR</td>
                                    <td>1691.96</td>
                                    <td>1136.05</td>
                                    <td>67</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>35 </td>
                                    <td>DEWAS</td>
                                    <td>2957.90</td>
                                    <td>2112.98</td>
                                    <td>71</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>36 </td>
                                    <td>NEEMUCH</td>
                                    <td>1411.64</td>
                                    <td>916.78</td>
                                    <td>65</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>37 </td>
                                    <td>AAGAR</td>
                                    <td>1691.96</td>
                                    <td>706.88</td>
                                    <td>42</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>TOTAL</td>
                                    <td>13632.78</td>
                                    <td>9609.35</td>
                                    <td>70</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>38</td>
                                    <td>SAGAR</td>
                                    <td>1870.00</td>
                                    <td>831.75</td>
                                    <td>44</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>39</td>
                                    <td>DAMOH</td>
                                    <td>1980.00</td>
                                    <td>1015.70</td>
                                    <td>51</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>40</td>
                                    <td>PANNA</td>
                                    <td>880.43</td>
                                    <td>433.15</td>
                                    <td>49</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>41</td>
                                    <td>TIKAMGARH</td>
                                    <td>1649.98</td>
                                    <td>789.75</td>
                                    <td>48</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>42</td>
                                    <td>CHHATARPUR</td>
                                    <td>2860.01</td>
                                    <td>1219.09</td>
                                    <td>43</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>TOTAL</td>
                                    <td>9240.42</td>
                                    <td>4289.44</td>
                                    <td>46</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>43</td>
                                    <td>REWA</td>
                                    <td>1881.34</td>
                                    <td>868.32</td>
                                    <td>46</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>44</td>
                                    <td>SATNA</td>
                                    <td>1727.35</td>
                                    <td>1337.72</td>
                                    <td>77</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>45</td>
                                    <td>SIDHI</td>
                                    <td>863.52</td>
                                    <td>332.39</td>
                                    <td>38</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>46</td>
                                    <td>SINGRAULI</td>
                                    <td>590.35</td>
                                    <td>274.46</td>
                                    <td>46</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>47</td>
                                    <td>SHAHDOL</td>
                                    <td>1015.53</td>
                                    <td>724.47</td>
                                    <td>71</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>48</td>
                                    <td>ANOOPPUR</td>
                                    <td>871.53</td>
                                    <td>1551.98</td>
                                    <td>178</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>49</td>
                                    <td>UMARIA</td>
                                    <td>849.75</td>
                                    <td>447.63</td>
                                    <td>53</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>7799.37</th>
                                    <th>5536.97</th>
                                    <th>71</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>GRAND TOTAL</th>
                                    <th>73424.16</th>
                                    <th>54019.16</th>
                                    <th>74</th>
                                    <th>%</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Financial Year 2015-16 -->
            <div class="modal fade" id="myModal5" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title text-center" id="myModalLabel">BRANCHWISE PERFORMANCE REPORT FROM
                        <br />
                                01/04/2015 TO 31/03/2016</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>S.No.</th>
                                    <th>BRANCH</th>
                                    <th>TARGET 2015-16</th>
                                    <th>Turnover upto MARCH 2016 WITH RTE</th>
                                    <th>TURNOVER WITHOUT RTE CURRENT YEAR</th>
                                    <th>Achivment %</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>1</td>
                                    <td>2</td>
                                    <td>&nbsp;</td>
                                    <td>4</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>1 </td>
                                    <td>BHOPAL</td>
                                    <td>1338.70</td>
                                    <td>3924.91</td>
                                    <td>1381.12</td>
                                    <td>293.19</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>2 </td>
                                    <td>H,BAD</td>
                                    <td>3850.00</td>
                                    <td>4096.06</td>
                                    <td>2599.61</td>
                                    <td>106.39</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>3 </td>
                                    <td>RAISEN</td>
                                    <td>1128.60</td>
                                    <td>2006.88</td>
                                    <td>795.88</td>
                                    <td>177.82</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>4 </td>
                                    <td>VIDISHA</td>
                                    <td>1402.50</td>
                                    <td>2618.07</td>
                                    <td>1405.04</td>
                                    <td>186.67</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>5 </td>
                                    <td>SEHORE</td>
                                    <td>2200.00</td>
                                    <td>3046.72</td>
                                    <td>1810.31</td>
                                    <td>138.49</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>6 </td>
                                    <td>BIAORA</td>
                                    <td>2200.00</td>
                                    <td>3993.87</td>
                                    <td>1621.26</td>
                                    <td>181.54</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>7 </td>
                                    <td>BETUL</td>
                                    <td>1104.40</td>
                                    <td>2743.89</td>
                                    <td>648.69</td>
                                    <td>248.45</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>8 </td>
                                    <td>HARDA</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>TOTAL</td>
                                    <td>13224.20</td>
                                    <td>22430.40</td>
                                    <td>10261.91</td>
                                    <td>169.62</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>9 </td>
                                    <td>GWALIOR</td>
                                    <td>662.37</td>
                                    <td>1847.75</td>
                                    <td>628.36</td>
                                    <td>278.96</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>10 </td>
                                    <td>DATIA</td>
                                    <td>553.30</td>
                                    <td>1044.18</td>
                                    <td>333.22</td>
                                    <td>188.72</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>11 </td>
                                    <td>SHIVPURI</td>
                                    <td>1028.50</td>
                                    <td>2673.24</td>
                                    <td>726.17</td>
                                    <td>259.92</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>12 </td>
                                    <td>GUNA</td>
                                    <td>1945.30</td>
                                    <td>2092.45</td>
                                    <td>1015.51</td>
                                    <td>107.56</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>13 </td>
                                    <td>ASHOK NAGAR</td>
                                    <td>0.00</td>
                                    <td>1368.59</td>
                                    <td>591.24</td>
                                    <td>0.00</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>14 </td>
                                    <td>BHIND</td>
                                    <td>788.82</td>
                                    <td>3685.04</td>
                                    <td>1035.35</td>
                                    <td>467.16</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>15 </td>
                                    <td>MORENA</td>
                                    <td>699.73</td>
                                    <td>2874.32</td>
                                    <td>777.75</td>
                                    <td>410.78</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>15 </td>
                                    <td>ASHOK NAGAR</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>16 </td>
                                    <td>SHEOPUR</td>
                                    <td>231.00</td>
                                    <td>1325.91</td>
                                    <td>120.33</td>
                                    <td>573.99</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>TOTAL</td>
                                    <td>5909.02</td>
                                    <td>16911.48</td>
                                    <td>5227.93</td>
                                    <td>286.20</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>17 </td>
                                    <td>JABALPUR</td>
                                    <td>1430.10</td>
                                    <td>3769.35</td>
                                    <td>1018.40</td>
                                    <td>263.57</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>18 </td>
                                    <td>CHINDWARA</td>
                                    <td>1484.97</td>
                                    <td>2938.03</td>
                                    <td>1168.41</td>
                                    <td>197.85</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>19 </td>
                                    <td>NARSINGPUR</td>
                                    <td>1540.00</td>
                                    <td>2117.10</td>
                                    <td>1257.89</td>
                                    <td>137.47</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>20 </td>
                                    <td>SEONI</td>
                                    <td>1925.00</td>
                                    <td>2785.28</td>
                                    <td>1525.10</td>
                                    <td>144.69</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>21 </td>
                                    <td>BALAGHAT</td>
                                    <td>990.00</td>
                                    <td>3561.61</td>
                                    <td>1062.68</td>
                                    <td>359.76</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>22 </td>
                                    <td>MANDLA</td>
                                    <td>1374.96</td>
                                    <td>2214.33</td>
                                    <td>1336.54</td>
                                    <td>161.05</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>23 </td>
                                    <td>DINDORI</td>
                                    <td>742.70</td>
                                    <td>1593.48</td>
                                    <td>690.80</td>
                                    <td>214.55</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>24 </td>
                                    <td>KATNI</td>
                                    <td>1127.40</td>
                                    <td>1630.04</td>
                                    <td>431.72</td>
                                    <td>144.58</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>TOTAL</td>
                                    <td>10615.13</td>
                                    <td>20609.22</td>
                                    <td>8491.54</td>
                                    <td>194.15</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>25 </td>
                                    <td>INDORE</td>
                                    <td>1927.70</td>
                                    <td>3947.66</td>
                                    <td>1626.79</td>
                                    <td>204.79</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>26 </td>
                                    <td>KHANDWA</td>
                                    <td>1764.34</td>
                                    <td>4095.85</td>
                                    <td>2195.48</td>
                                    <td>232.15</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>27 </td>
                                    <td>KHARGONE</td>
                                    <td>1704.70</td>
                                    <td>4385.48</td>
                                    <td>2672.28</td>
                                    <td>257.26</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>28 </td>
                                    <td>DHAR</td>
                                    <td>3960.60</td>
                                    <td>6012.66</td>
                                    <td>3454.20</td>
                                    <td>151.81</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>29 </td>
                                    <td>JHABUA</td>
                                    <td>1809.70</td>
                                    <td>5614.11</td>
                                    <td>1107.93</td>
                                    <td>310.22</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>30 </td>
                                    <td>BARWANI</td>
                                    <td>879.34</td>
                                    <td>2600.74</td>
                                    <td>1058.70</td>
                                    <td>295.76</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>TOTAL</td>
                                    <td>12046.38</td>
                                    <td>26656.50</td>
                                    <td>12115.38</td>
                                    <td>221.28</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>31 </td>
                                    <td>UJJAIN</td>
                                    <td>1515.90</td>
                                    <td>3344.01</td>
                                    <td>1709.44</td>
                                    <td>220.60</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>32 </td>
                                    <td>MANDSOUR</td>
                                    <td>1527.90</td>
                                    <td>3919.95</td>
                                    <td>2672.95</td>
                                    <td>256.56</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>33 </td>
                                    <td>RATLAM</td>
                                    <td>2301.04</td>
                                    <td>4651.17</td>
                                    <td>3531.29</td>
                                    <td>202.13</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>34 </td>
                                    <td>SHAJAPUR</td>
                                    <td>1691.96</td>
                                    <td>2313.06</td>
                                    <td>1448.10</td>
                                    <td>136.71</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>35 </td>
                                    <td>DEWAS</td>
                                    <td>2957.90</td>
                                    <td>3437.84</td>
                                    <td>2028.10</td>
                                    <td>116.23</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>36 </td>
                                    <td>NEEMUCH</td>
                                    <td>1411.64</td>
                                    <td>2200.13</td>
                                    <td>1150.80</td>
                                    <td>155.86</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>37 </td>
                                    <td>AAGAR</td>
                                    <td>1691.96</td>
                                    <td>1823.46</td>
                                    <td>1130.80</td>
                                    <td>107.77</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>TOTAL</td>
                                    <td>13098.30</td>
                                    <td>21689.62</td>
                                    <td>13671.48</td>
                                    <td>165.59</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>38</td>
                                    <td>SAGAR</td>
                                    <td>1870.00</td>
                                    <td>4453.78</td>
                                    <td>719.96</td>
                                    <td>238.17</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>39</td>
                                    <td>DAMOH</td>
                                    <td>1980.00</td>
                                    <td>3141.50</td>
                                    <td>1410.01</td>
                                    <td>158.66</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>40</td>
                                    <td>PANNA</td>
                                    <td>880.43</td>
                                    <td>1438.80</td>
                                    <td>482.17</td>
                                    <td>163.42</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>41</td>
                                    <td>TIKAMGARH</td>
                                    <td>1649.98</td>
                                    <td>3195.07</td>
                                    <td>598.82</td>
                                    <td>193.64</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>42</td>
                                    <td>CHHATARPUR</td>
                                    <td>2860.01</td>
                                    <td>3220.54</td>
                                    <td>1037.08</td>
                                    <td>112.61</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>TOTAL</td>
                                    <td>9240.42</td>
                                    <td>15449.69</td>
                                    <td>4248.04</td>
                                    <td>167.20</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>43</td>
                                    <td>REWA</td>
                                    <td>1881.34</td>
                                    <td>6060.65</td>
                                    <td>1096.88</td>
                                    <td>322.15</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>44</td>
                                    <td>SATNA</td>
                                    <td>1727.35</td>
                                    <td>3668.76</td>
                                    <td>1342.72</td>
                                    <td>212.39</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>45</td>
                                    <td>SIDHI</td>
                                    <td>863.52</td>
                                    <td>2788.09</td>
                                    <td>639.08</td>
                                    <td>322.87</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>46</td>
                                    <td>SINGRAULI</td>
                                    <td>590.35</td>
                                    <td>1677.17</td>
                                    <td>255.77</td>
                                    <td>284.10</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>47</td>
                                    <td>SHAHDOL</td>
                                    <td>1015.53</td>
                                    <td>1481.43</td>
                                    <td>478.29</td>
                                    <td>145.88</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>48</td>
                                    <td>ANOOPPUR</td>
                                    <td>871.53</td>
                                    <td>1444.04</td>
                                    <td>750.93</td>
                                    <td>165.69</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>49</td>
                                    <td>UMARIA</td>
                                    <td>849.75</td>
                                    <td>861.47</td>
                                    <td>197.74</td>
                                    <td>101.38</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>7799.37</th>
                                    <th>17981.61</th>
                                    <th>4761.41</th>
                                    <th>230.55</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <th>GRAND TOTAL</th>
                                    <th>71932.82</th>
                                    <th>141728.52</th>
                                    <th>58777.69</th>
                                    <th>197.03</th>
                                    <th>%</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Financial Year 2014-15 -->
            <div class="modal fade" id="myModal6" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title text-center" id="myModalLabel">BRANCHWISE PERFORMANCE REPORT FROM
                        <br />
                                01/04/2014 TO 31/03/2015</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>S.No.</th>
                                    <th>BRANCH</th>
                                    <th>Turnover upto MARCH 2014</th>
                                    <th>Turnover upto MARCH 2015</th>
                                    <th>Achivment %</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>1</td>
                                    <td>2</td>
                                    <td>3</td>
                                    <td>4</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>1 </td>
                                    <td>BHOPAL</td>
                                    <td>3365.55</td>
                                    <td>3274.31</td>
                                    <td>269.05</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>2 </td>
                                    <td>H,BAD</td>
                                    <td>5432.30</td>
                                    <td>4185.06</td>
                                    <td>119.57</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>3 </td>
                                    <td>RAISEN</td>
                                    <td>2079.90</td>
                                    <td>1791.96</td>
                                    <td>174.65</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>4 </td>
                                    <td>VIDISHA</td>
                                    <td>2587.07</td>
                                    <td>2135.31</td>
                                    <td>167.48</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>5 </td>
                                    <td>SEHORE</td>
                                    <td>3192.47</td>
                                    <td>2550.21</td>
                                    <td>127.51</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>6 </td>
                                    <td>BIAORA</td>
                                    <td>4630.67</td>
                                    <td>3726.82</td>
                                    <td>186.34</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>7 </td>
                                    <td>BETUL</td>
                                    <td>2798.33</td>
                                    <td>2470.77</td>
                                    <td>246.09</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>8 </td>
                                    <td>HARDA</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>24086.29</th>
                                    <th>20134.44</th>
                                    <th>167.48</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>9 </td>
                                    <td>GWALIOR</td>
                                    <td>1449.03</td>
                                    <td>1455.68</td>
                                    <td>94.64</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>10 </td>
                                    <td>DATIA</td>
                                    <td>839.20</td>
                                    <td>1054.14</td>
                                    <td>87.63</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>11 </td>
                                    <td>SHIVPURI</td>
                                    <td>3178.95</td>
                                    <td>2350.92</td>
                                    <td>87.30</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>12 </td>
                                    <td>GUNA</td>
                                    <td>4033.76</td>
                                    <td>3115.72</td>
                                    <td>85.87</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>13 </td>
                                    <td>BHIND</td>
                                    <td>3027.09</td>
                                    <td>2766.89</td>
                                    <td>91.64</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>14 </td>
                                    <td>MORENA</td>
                                    <td>2124.22</td>
                                    <td>2208.07</td>
                                    <td>97.69</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>15 </td>
                                    <td>ASHOK NAGAR</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>0.00</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>15 </td>
                                    <td>SHEOPUR</td>
                                    <td>1082.97</td>
                                    <td>1141.67</td>
                                    <td>95.14</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>15735.22</th>
                                    <th>14093.09</th>
                                    <th>90.68</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>16 </td>
                                    <td>JABALPUR</td>
                                    <td>3658.79</td>
                                    <td>3107.14</td>
                                    <td>80.70</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>17 </td>
                                    <td>CHINDWARA</td>
                                    <td>3186.53</td>
                                    <td>2751.42</td>
                                    <td>90.21</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>18 </td>
                                    <td>NARSINGPUR</td>
                                    <td>2108.12</td>
                                    <td>1718.67</td>
                                    <td>78.12</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>19 </td>
                                    <td>SEONI</td>
                                    <td>2790.34</td>
                                    <td>2160.30</td>
                                    <td>76.47</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>20 </td>
                                    <td>BALAGHAT</td>
                                    <td>3514.01</td>
                                    <td>3213.19</td>
                                    <td>103.65</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>21 </td>
                                    <td>MANDLA</td>
                                    <td>2048.20</td>
                                    <td>1662.08</td>
                                    <td>77.31</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>22 </td>
                                    <td>DINDORI</td>
                                    <td>1614.06</td>
                                    <td>1225.85</td>
                                    <td>83.11</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>23 </td>
                                    <td>KATNI</td>
                                    <td>2742.02</td>
                                    <td>1881.59</td>
                                    <td>86.51</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>21662.07</th>
                                    <th>17720.24</th>
                                    <th>85.09</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>24 </td>
                                    <td>INDORE</td>
                                    <td>3954.12</td>
                                    <td>3567.46</td>
                                    <td>89.13</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>25 </td>
                                    <td>KHANDWA</td>
                                    <td>3673.49</td>
                                    <td>4201.53</td>
                                    <td>116.39</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>26 </td>
                                    <td>KHARGONE</td>
                                    <td>3147.77</td>
                                    <td>4316.19</td>
                                    <td>136.07</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>27 </td>
                                    <td>DHAR</td>
                                    <td>5941.94</td>
                                    <td>6747.78</td>
                                    <td>112.65</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>28 </td>
                                    <td>JHABUA</td>
                                    <td>5137.61</td>
                                    <td>4727.69</td>
                                    <td>85.38</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>29 </td>
                                    <td>BARWANI</td>
                                    <td>2117.37</td>
                                    <td>2158.14</td>
                                    <td>97.15</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>23972.30</th>
                                    <th>25718.79</th>
                                    <th>104.83</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>30 </td>
                                    <td>UJJAIN</td>
                                    <td>2866.66</td>
                                    <td>2533.63</td>
                                    <td>86.40</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>31 </td>
                                    <td>MANDSOUR</td>
                                    <td>2454.47</td>
                                    <td>3114.46</td>
                                    <td>123.56</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>32 </td>
                                    <td>RATLAM</td>
                                    <td>3078.68</td>
                                    <td>3361.13</td>
                                    <td>105.75</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>33 </td>
                                    <td>SHAJAPUR</td>
                                    <td>4322.74</td>
                                    <td>2002.20</td>
                                    <td>89.60</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>34 </td>
                                    <td>DEWAS</td>
                                    <td>3947.17</td>
                                    <td>2809.33</td>
                                    <td>68.94</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>35 </td>
                                    <td>NEEMUCH</td>
                                    <td>2173.84</td>
                                    <td>2451.61</td>
                                    <td>109.69</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>36 </td>
                                    <td>AAGAR</td>
                                    <td>0.00</td>
                                    <td>1384.84</td>
                                    <td>61.97</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>18843.56</th>
                                    <th>17657.20</th>
                                    <th>90.97</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>36</td>
                                    <td>SAGAR</td>
                                    <td>4666.51</td>
                                    <td>4503.80</td>
                                    <td>86.39</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>37</td>
                                    <td>DAMOH</td>
                                    <td>3467.09</td>
                                    <td>3085.65</td>
                                    <td>87.52</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>38</td>
                                    <td>PANNA</td>
                                    <td>1529.42</td>
                                    <td>1557.25</td>
                                    <td>90.35</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>39</td>
                                    <td>TIKAMGARH</td>
                                    <td>3373.88</td>
                                    <td>3072.12</td>
                                    <td>86.44</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>40</td>
                                    <td>CHHATARPUR</td>
                                    <td>4449.55</td>
                                    <td>3011.81</td>
                                    <td>70.13</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>17486.45</th>
                                    <th>15230.63</th>
                                    <th>83.18</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <td>41</td>
                                    <td>REWA</td>
                                    <td>5572.78</td>
                                    <td>5274.52</td>
                                    <td>90.14</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>42</td>
                                    <td>SATNA</td>
                                    <td>3718.66</td>
                                    <td>2869.31</td>
                                    <td>73.80</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>43</td>
                                    <td>SIDHI</td>
                                    <td>4563.08</td>
                                    <td>4014.67</td>
                                    <td>85.89</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>44</td>
                                    <td>SHAHDOL</td>
                                    <td>1742.64</td>
                                    <td>1368.35</td>
                                    <td>74.78</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>45</td>
                                    <td>ANOOPPUR</td>
                                    <td>1346.88</td>
                                    <td>885.43</td>
                                    <td>62.61</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <td>46</td>
                                    <td>UMARIA</td>
                                    <td>1233.92</td>
                                    <td>996.14</td>
                                    <td>76.88</td>
                                    <td>%</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>TOTAL</th>
                                    <th>18177.96</th>
                                    <th>15408.42</th>
                                    <th>81.30</th>
                                    <th>%</th>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>GRAND TOTAL</th>
                                    <th>139963.85</th>
                                    <th>125962.81</th>
                                    <th>97.20</th>
                                    <th>%</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Sales Year 2014-15 -->
            <div class="modal fade" id="myModal7" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title text-center" id="myModalLabel">M P State Agro Ind. Dev. Corpn. Ltd., Bhopal<br />
                                BRANCH WISE PROFIT / LOSS FOR 2014-15</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>Sno</th>
                                    <th>AC_HEAD</th>
                                    <th>DR_BAL</th>
                                    <th>CR_BAL</th>
                                    <th>LOC</th>
                                </tr>
                                <tr>
                                    <td>1</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>5389027.93</td>
                                    <td>BAR</td>
                                </tr>
                                <tr>
                                    <td>2</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>2474675.30</td>
                                    <td>BFP</td>
                                </tr>
                                <tr>
                                    <td>3</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6426953.62</td>
                                    <td>BGT</td>
                                </tr>
                                <tr>
                                    <td>4</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>9081669.94</td>
                                    <td>BIO</td>
                                </tr>
                                <tr>
                                    <td>5</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>4989489.64</td>
                                    <td>BND</td>
                                </tr>
                                <tr>
                                    <td>6</td>
                                    <td>LOSS OF THE BRANCH</td>
                                    <td>492221.72</td>
                                    <td>0.00</td>
                                    <td>BPL</td>
                                </tr>
                                <tr>
                                    <td>7</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>2866712.04</td>
                                    <td>BTL</td>
                                </tr>
                                <tr>
                                    <td>8</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7288018.55</td>
                                    <td>CHP</td>
                                </tr>
                                <tr>
                                    <td>9</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>5696420.74</td>
                                    <td>CHW</td>
                                </tr>
                                <tr>
                                    <td>10</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>25528674.73</td>
                                    <td>DHR</td>
                                </tr>
                                <tr>
                                    <td>11</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>8214344.98</td>
                                    <td>DMH</td>
                                </tr>
                                <tr>
                                    <td>12</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>2247190.53</td>
                                    <td>DTA</td>
                                </tr>
                                <tr>
                                    <td>13</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7068265.16</td>
                                    <td>DWS</td>
                                </tr>
                                <tr>
                                    <td>14</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7559138.81</td>
                                    <td>GNA</td>
                                </tr>
                                <tr>
                                    <td>15</td>
                                    <td>LOSS OF THE BRANCH</td>
                                    <td>1246201.46</td>
                                    <td>0.00</td>
                                    <td>GWL</td>
                                </tr>
                                <tr>
                                    <td>16</td>
                                    <td>LOSS OF THE BRANCH</td>
                                    <td>233545848.24</td>
                                    <td>0.00</td>
                                    <td>HO</td>
                                </tr>
                                <tr>
                                    <td>17</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>17793521.47</td>
                                    <td>HSD</td>
                                </tr>
                                <tr>
                                    <td>18</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>3676789.63</td>
                                    <td>IND</td>
                                </tr>
                                <tr>
                                    <td>19</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>11705379.40</td>
                                    <td>JBA</td>
                                </tr>
                                <tr>
                                    <td>20</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>8208495.25</td>
                                    <td>JBP</td>
                                </tr>
                                <tr>
                                    <td>21</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>13486476.90</td>
                                    <td>KGN</td>
                                </tr>
                                <tr>
                                    <td>22</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>13507189.90</td>
                                    <td>KHW</td>
                                </tr>
                                <tr>
                                    <td>23</td>
                                    <td>LOSS OF THE BRANCH</td>
                                    <td>6580941.78</td>
                                    <td>0.00</td>
                                    <td>MAF</td>
                                </tr>
                                <tr>
                                    <td>24</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>19178964.66</td>
                                    <td>MDS</td>
                                </tr>
                                <tr>
                                    <td>25</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7526861.96</td>
                                    <td>MND</td>
                                </tr>
                                <tr>
                                    <td>26</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>5061681.34</td>
                                    <td>MRN</td>
                                </tr>
                                <tr>
                                    <td>27</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>4464597.37</td>
                                    <td>NSP</td>
                                </tr>
                                <tr>
                                    <td>28</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>2933422.11</td>
                                    <td>PNA</td>
                                </tr>
                                <tr>
                                    <td>29</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>1535347.72</td>
                                    <td>RSN</td>
                                </tr>
                                <tr>
                                    <td>30</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>163397340.86</td>
                                    <td>RTE</td>
                                </tr>
                                <tr>
                                    <td>31</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>12045764.33</td>
                                    <td>RTL</td>
                                </tr>
                                <tr>
                                    <td>32</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>11803403.72</td>
                                    <td>RWA</td>
                                </tr>
                                <tr>
                                    <td>33</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>2167680.90</td>
                                    <td>SGR</td>
                                </tr>
                                <tr>
                                    <td>34</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7584833.79</td>
                                    <td>SHD</td>
                                </tr>
                                <tr>
                                    <td>35</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>4975848.07</td>
                                    <td>SHP</td>
                                </tr>
                                <tr>
                                    <td>36</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>4414435.35</td>
                                    <td>SHR</td>
                                </tr>
                                <tr>
                                    <td>37</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>10469310.83</td>
                                    <td>SID</td>
                                </tr>
                                <tr>
                                    <td>38</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>11434392.88</td>
                                    <td>SJP</td>
                                </tr>
                                <tr>
                                    <td>39</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>5911822.57</td>
                                    <td>SNI</td>
                                </tr>
                                <tr>
                                    <td>40</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>2034202.43</td>
                                    <td>SPK</td>
                                </tr>
                                <tr>
                                    <td>41</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>4176566.86</td>
                                    <td>STN</td>
                                </tr>
                                <tr>
                                    <td>42</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7321972.04</td>
                                    <td>TKG</td>
                                </tr>
                                <tr>
                                    <td>43</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>683397.84</td>
                                    <td>UJN</td>
                                </tr>
                                <tr>
                                    <td>44</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>2343052.35</td>
                                    <td>VDS</td>
                                </tr>
                                <tr>
                                    <td>45</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>383896.85</td>
                                    <td>WSI</td>
                                </tr>
                                <tr>
                                    <th>&nbsp;</th>
                                    <th>Total</th>
                                    <th>241865213.20</th>
                                    <th>455057231.35</th>
                                    <th>&nbsp;</th>
                                </tr>
                                <tr>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                    <th>213192018.15</th>
                                    <th></th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Sales Year 2015-16 -->
            <div class="modal fade" id="myModal8" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title text-center" id="myModalLabel">BRANCH WISE PROFIT / LOSS FOR THE YEAR 2015-16</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>S. No.</th>
                                    <th>AC_HEAD</th>
                                    <th>DR_BAL</th>
                                    <th>CR_BAL</th>
                                    <th>LOC</th>
                                </tr>
                                <tr>
                                    <td>1</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6306627.33</td>
                                    <td>AGR</td>
                                </tr>
                                <tr>
                                    <td>2</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>3765017.45</td>
                                    <td>ANP</td>
                                </tr>
                                <tr>
                                    <td>3</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>4448909.88</td>
                                    <td>ASN</td>
                                </tr>
                                <tr>
                                    <td>4</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>8017826.38</td>
                                    <td>BAR</td>
                                </tr>
                                <tr>
                                    <td>5</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>2232196.37</td>
                                    <td>BFP</td>
                                </tr>
                                <tr>
                                    <td>6</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>8594733.60</td>
                                    <td>BGT</td>
                                </tr>
                                <tr>
                                    <td>7</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>11424393.22</td>
                                    <td>BIO</td>
                                </tr>
                                <tr>
                                    <td>8</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>8239439.39</td>
                                    <td>BND</td>
                                </tr>
                                <tr>
                                    <td>9</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>569140.40</td>
                                    <td>BPL</td>
                                </tr>
                                <tr>
                                    <td>10</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>4188127.08</td>
                                    <td>BTL</td>
                                </tr>
                                <tr>
                                    <td>11</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7351315.26</td>
                                    <td>CHP</td>
                                </tr>
                                <tr>
                                    <td>12</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6200726.43</td>
                                    <td>CHW</td>
                                </tr>
                                <tr>
                                    <td>13</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>22962131.31</td>
                                    <td>DHR</td>
                                </tr>
                                <tr>
                                    <td>14</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>8558548.40</td>
                                    <td>DMH</td>
                                </tr>
                                <tr>
                                    <td>15</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>5387576.41</td>
                                    <td>DND</td>
                                </tr>
                                <tr>
                                    <td>16</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>1228699.88</td>
                                    <td>DTA</td>
                                </tr>
                                <tr>
                                    <td>17</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6052403.10</td>
                                    <td>DWS</td>
                                </tr>
                                <tr>
                                    <td>18</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>4672015.82</td>
                                    <td>GNA</td>
                                </tr>
                                <tr>
                                    <td>19</td>
                                    <td>LOSS OF THE BRANCH</td>
                                    <td>475583.04</td>
                                    <td>0.00</td>
                                    <td>GWL</td>
                                </tr>
                                <tr>
                                    <td>20</td>
                                    <td>LOSS OF THE BRANCH</td>
                                    <td>260754094.77</td>
                                    <td>0.00</td>
                                    <td>HO</td>
                                </tr>
                                <tr>
                                    <td>21</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>3813881.03</td>
                                    <td>HRD</td>
                                </tr>
                                <tr>
                                    <td>22</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7147346.31</td>
                                    <td>HSD</td>
                                </tr>
                                <tr>
                                    <td>23</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6290812.84</td>
                                    <td>IND</td>
                                </tr>
                                <tr>
                                    <td>24</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>14302945.80</td>
                                    <td>JBA</td>
                                </tr>
                                <tr>
                                    <td>25</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>5530348.34</td>
                                    <td>JBP</td>
                                </tr>
                                <tr>
                                    <td>26</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>14256027.57</td>
                                    <td>KGN</td>
                                </tr>
                                <tr>
                                    <td>27</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>13696816.33</td>
                                    <td>KHW</td>
                                </tr>
                                <tr>
                                    <td>28</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>3537340.78</td>
                                    <td>KTN</td>
                                </tr>
                                <tr>
                                    <td>29</td>
                                    <td>LOSS OF THE BRANCH</td>
                                    <td>8033412.26</td>
                                    <td>0.00</td>
                                    <td>MAF</td>
                                </tr>
                                <tr>
                                    <td>30</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>14803343.97</td>
                                    <td>MDS</td>
                                </tr>
                                <tr>
                                    <td>31</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7635827.15</td>
                                    <td>MND</td>
                                </tr>
                                <tr>
                                    <td>32</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6292569.34</td>
                                    <td>MRN</td>
                                </tr>
                                <tr>
                                    <td>33</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7417634.49</td>
                                    <td>NMH</td>
                                </tr>
                                <tr>
                                    <td>34</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6135460.51</td>
                                    <td>NSP</td>
                                </tr>
                                <tr>
                                    <td>35</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>2333213.79</td>
                                    <td>PNA</td>
                                </tr>
                                <tr>
                                    <td>36</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>2663456.25</td>
                                    <td>RSN</td>
                                </tr>
                                <tr>
                                    <td>37</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>187201351.99</td>
                                    <td>RTE</td>
                                </tr>
                                <tr>
                                    <td>38</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>17737032.52</td>
                                    <td>RTL</td>
                                </tr>
                                <tr>
                                    <td>39</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>15170090.93</td>
                                    <td>RWA</td>
                                </tr>
                                <tr>
                                    <td>40</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>871894.72</td>
                                    <td>SGR</td>
                                </tr>
                                <tr>
                                    <td>41</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>2490711.17</td>
                                    <td>SHD</td>
                                </tr>
                                <tr>
                                    <td>42</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6440065.51</td>
                                    <td>SHP</td>
                                </tr>
                                <tr>
                                    <td>43</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>5415274.82</td>
                                    <td>SHR</td>
                                </tr>
                                <tr>
                                    <td>44</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>11367936.54</td>
                                    <td>SID</td>
                                </tr>
                                <tr>
                                    <td>45</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7884039.73</td>
                                    <td>SJP</td>
                                </tr>
                                <tr>
                                    <td>46</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>8221415.59</td>
                                    <td>SNI</td>
                                </tr>
                                <tr>
                                    <td>47</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>3207340.73</td>
                                    <td>SPK</td>
                                </tr>
                                <tr>
                                    <td>48</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6825305.97</td>
                                    <td>STN</td>
                                </tr>
                                <tr>
                                    <td>49</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7293503.15</td>
                                    <td>TKG</td>
                                </tr>
                                <tr>
                                    <td>50</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>4068273.99</td>
                                    <td>UJN</td>
                                </tr>
                                <tr>
                                    <td>51</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>1410052.46</td>
                                    <td>UMR</td>
                                </tr>
                                <tr>
                                    <td>52</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>5993422.33</td>
                                    <td>VDS</td>
                                </tr>
                                <tr>
                                    <td>53</td>
                                    <td>LOSS OF THE BRANCH</td>
                                    <td>620752.38</td>
                                    <td>0.00</td>
                                    <td>WSI</td>
                                </tr>
                                <tr>
                                    <th></th>
                                    <th></th>
                                    <th>269883842.45</th>
                                    <th>527654564.36</th>
                                    <th></th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Sales Year 2016-17 -->
            <div class="modal fade" id="myModal9" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title text-center" id="myModalLabel">BRANCH WISE PROFIT & LOSS 2016-17</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>AC_CODE</th>
                                    <th>AC_HEAD</th>
                                    <th>DR_BAL</th>
                                    <th>CR_BAL</th>
                                    <th>LOC</th>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>3756658.63</td>
                                    <td>AGR</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>8951413.67</td>
                                    <td>ANP</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6469727.48</td>
                                    <td>ASN</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6280448.25</td>
                                    <td>BAR</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>LOSS OF THE BRANCH</td>
                                    <td>680879.92</td>
                                    <td>0.00</td>
                                    <td>BFP</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>12450658.19</td>
                                    <td>BGT</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>11189567.70</td>
                                    <td>BIO</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>11344671.62</td>
                                    <td>BND</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>21111710.07</td>
                                    <td>BPL</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6766516.73</td>
                                    <td>BTL</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7774230.92</td>
                                    <td>CHP</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>4599194.44</td>
                                    <td>CHW</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>20073415.31</td>
                                    <td>DHR</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6589655.35</td>
                                    <td>DMH</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>4544771.50</td>
                                    <td>DND</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>1176820.61</td>
                                    <td>DTA</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>11886616.52</td>
                                    <td>DWS</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6727644.54</td>
                                    <td>GNA</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>1084037.07</td>
                                    <td>GWL</td>
                                </tr>
                                <tr>
                                    <td>030903</td>
                                    <td>LOSS OF THE BRANCH</td>
                                    <td>256058328.64</td>
                                    <td>0.00</td>
                                    <td>HO</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>2645499.35</td>
                                    <td>HRD</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>5039098.00</td>
                                    <td>HSD</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>4811577.72</td>
                                    <td>IND</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>15699302.79</td>
                                    <td>JBA</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7199781.14</td>
                                    <td>JBP</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>11980785.33</td>
                                    <td>KGN</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>9975472.08</td>
                                    <td>KHW</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>4239529.48</td>
                                    <td>KTN</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>LOSS OF THE BRANCH</td>
                                    <td>276970.49</td>
                                    <td>0.00</td>
                                    <td>MAF</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7572537.14</td>
                                    <td>MDS</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7179189.07</td>
                                    <td>MND</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>8091519.61</td>
                                    <td>MRN</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6651231.97</td>
                                    <td>NMH</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>5574984.08</td>
                                    <td>NSP</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>3696204.96</td>
                                    <td>PNA</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7224698.23</td>
                                    <td>RSN</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>100690066.70</td>
                                    <td>RTB</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>10166456.82</td>
                                    <td>RTL</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>15995030.50</td>
                                    <td>RWA</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>9828159.85</td>
                                    <td>SGR</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>5403957.01</td>
                                    <td>SHD</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7100020.44</td>
                                    <td>SHP</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>5337132.18</td>
                                    <td>SHR</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>11866989.49</td>
                                    <td>SID</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6082371.95</td>
                                    <td>SJP</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6710844.74</td>
                                    <td>SNI</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>4067352.98</td>
                                    <td>SPK</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>9856477.91</td>
                                    <td>STN</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7342103.44</td>
                                    <td>TKG</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>6938506.60</td>
                                    <td>UJN</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>3230305.87</td>
                                    <td>UMR</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>PROFIT OF THE BRANCH</td>
                                    <td>0.00</td>
                                    <td>7170733.71</td>
                                    <td>VDS</td>
                                </tr>
                                <tr>
                                    <td>030101</td>
                                    <td>LOSS OF THE BRANCH</td>
                                    <td>230416.70</td>
                                    <td>0.00</td>
                                    <td>WSI</td>
                                </tr>
                                <tr>
                                    <th></th>
                                    <th>220899083.99</th>
                                    <th>257246595.75</th>
                                    <th>478145679.74</th>
                                    <th></th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>


            <!-- Balance Sheet 2017-18 -->
            <div class="modal fade" id="bsheet1718" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">BALANCE SHEET AS AT 31ST MARCH 2018</h4>
                        </div>
                        <div class="modal-body">
                            <img src="images/bs17-18.jpg" class="img-responsive" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <!--<button type="button" class="btn btn-primary">Save changes</button>-->
                        </div>
                    </div>
                </div>
            </div>

            <!-- Balance Sheet 2016-17 -->
            <div class="modal fade" id="bsheet1617" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">BALANCE SHEET AS AT 31ST MARCH 2017</h4>
                        </div>
                        <div class="modal-body">
                            <img src="images/bs16-17.jpg" class="img-responsive" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <!--<button type="button" class="btn btn-primary">Save changes</button>-->
                        </div>
                    </div>
                </div>
            </div>
            <!-- Balance Sheet 2015-16 -->
            <div class="modal fade" id="bsheet1516" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">BALANCE SHEET AS AT 31ST MARCH 2016</h4>
                        </div>
                        <div class="modal-body">
                            <img src="images/bs15-16.jpg" class="img-responsive" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <!--<button type="button" class="btn btn-primary">Save changes</button>-->
                        </div>
                    </div>
                </div>
            </div>
            <!-- Balance Sheet 2014-15 -->
            <div class="modal fade" id="bsheet1415" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">BALANCE SHEET AS AT 31ST MARCH 2015</h4>
                        </div>
                        <div class="modal-body">
                            <img src="images/bs14-15.jpg" class="img-responsive" />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <!--<button type="button" class="btn btn-primary">Save changes</button>-->
                        </div>
                    </div>
                </div>
            </div>

              <!-- Badi Production 18  -->
            <div class="modal fade" id="badi18" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Badi Plant Production - 2018-19</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>Product Name</th>
                                    <th>Quantity in Mt. Tn.</th>
                                </tr>
                                <tr>
                                    <td>Halwa</td>
                                    <td>4951.344</td>
                                </tr>
                                 <tr>
                                    <td>Bal Aahar</td>
                                    <td>3370.968</td>
                                </tr>
                                <tr>
                                    <td>Khichadi 625</td>
                                    <td>0.00</td>
                                </tr>
                                 <tr>
                                    <td>Barfi</td>
                                    <td>2690.475</td>
                                </tr>
                                 <tr>
                                    <td>Laddu</td>
                                    <td>1690.680</td>
                                </tr>
                                 <tr>
                                    <td>Khichadi 750</td>
                                    <td>0.00</td>
                                </tr>
                                 <tr>
                                    <td>Sabla Barfi</td>
                                    <td>443.286</td>
                                </tr>
                                 <tr>
                                    <td>Khichadi 900</td>
                                    <td>0.00</td>
                                </tr>
                                <tr>
                                    <th>Total Production</th>
                                    <th>13146.753</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Badi Production 17  -->
            <div class="modal fade" id="badi17" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Badi Plant Production - 2017-18</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>Product Name</th>
                                    <th>Quantity in Mt. Tn.</th>
                                </tr>
                                <tr>
                                    <td>BAL AAHAR 600 GRAM</td>
                                    <td>0</td>
                                </tr>
                                <tr>
                                    <td>BARFI 750GM</td>
                                    <td>5325.84</td>
                                </tr>
                                <tr>
                                    <td>BARFI 900 GRAM</td>
                                    <td>0</td>
                                </tr>
                                <tr>
                                    <td>HALWA 600 GRAM</td>
                                    <td>9148.512</td>
                                </tr>
                                <tr>
                                    <td>LADDU 750GRAM</td>
                                    <td>3133.335</td>
                                </tr>
                                <tr>
                                    <th>Total Production</th>
                                    <th>17607.687</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Badi Production 16  -->
            <div class="modal fade" id="badi16" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Badi Plant Production - 2016-17</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>Product Name</th>
                                    <th>Quantity in Mt. Tn.</th>
                                </tr>
                                <tr>
                                    <td>BARFI 750GM</td>
                                    <td>6157.575</td>
                                </tr>
                                <tr>
                                    <td>BARFI 900 GRAM</td>
                                    <td>6.876</td>
                                </tr>
                                <tr>
                                    <td>HALWA 600 GRAM</td>
                                    <td>10503.216</td>
                                </tr>
                                <tr>
                                    <td>LADDU 750GRAM</td>
                                    <td>3570.51</td>
                                </tr>
                                <tr>
                                    <th>Total Production</th>
                                    <th>20238.177</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Badi Production 15  -->
            <div class="modal fade" id="badi15" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Badi Plant Production - 2015-16</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>Product Name</th>
                                    <th>Quantity in Mt. Tn.</th>
                                </tr>
                                <tr>
                                    <td>BARFI 750GM</td>
                                    <td>5550.48</td>
                                </tr>
                                <tr>
                                    <td>BARFI 900 GRAM</td>
                                    <td>1390.122</td>
                                </tr>
                                <tr>
                                    <td>HALWA 600 GRAM</td>
                                    <td>9830.688</td>
                                </tr>
                                <tr>
                                    <td>LADDU 750GRAM</td>
                                    <td>2981.745</td>
                                </tr>
                                <tr>
                                    <th>Total Production</th>
                                    <th>19753.035</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Badi Production 15  -->
            <div class="modal fade" id="badi14" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Badi Plant Production - 2014-15</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>Product Name</th>
                                    <th>Quantity in Mt. Tn.</th>
                                </tr>
                                <tr>
                                    <td>BARFI 750GM</td>
                                    <td>4709.14</td>
                                </tr>
                                <tr>
                                    <td>BARFI 900 GRAM</td>
                                    <td>3560.229</td>
                                </tr>
                                <tr>
                                    <td>HALWA 600 GRAM</td>
                                    <td>8859.312</td>
                                </tr>
                                <tr>
                                    <td>LADDU 750GRAM</td>
                                    <td>2354.88</td>
                                </tr>
                                <tr>
                                    <th>Total Production</th>
                                    <th>19483.561</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>


            <!-- Indrapuri Production 19  -->
            <div class="modal fade" id="Indrapuri19" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Indrapuri Plant Production - 2018-19</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>Product Name</th>
                                    <th>Quantity in Mt. Tn.</th>
                                </tr>
                                <tr>
                                    <td>P.S.B (250 gm pkt)</td>
                                    <td>75.28</td>
                                </tr>
                                <tr>
                                    <td>Rhizobium (150 gm pkt)</td>
                                    <td>39.50</td>
                                </tr>
                                <tr>
                                    <td>Azotobacter</td>
                                    <td>13.21</td>
                                </tr>
                                <tr>
                                    <th>Total Production</th>
                                    <th>127.99</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Indrapuri Production 18  -->
            <div class="modal fade" id="Indrapuri18" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Indrapuri Plant Production - 2017-18</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>Product Name</th>
                                    <th>Quantity in Mt. Tn.</th>
                                </tr>
                                <tr>
                                    <td>P.S.B (250 gm pkt)</td>
                                    <td>77.67</td>
                                </tr>
                                <tr>
                                    <td>Rhizobium (150 gm pkt)</td>
                                    <td>45.24</td>
                                </tr>
                                <tr>
                                    <td>Azotobacter</td>
                                    <td>10.89</td>
                                </tr>
                                <tr>
                                    <th>Total Production</th>
                                    <th>133.79</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Indrapuri Production 17  -->
            <div class="modal fade" id="Indrapuri17" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Indrapuri Plant Production - 2016-17</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>Product Name</th>
                                    <th>Quantity in Mt. Tn.</th>
                                </tr>
                                <tr>
                                    <td>P.S.B (250 gm pkt)</td>
                                    <td>149.68</td>
                                </tr>
                                <tr>
                                    <td>Rhizobium (150 gm pkt)</td>
                                    <td>82.76</td>
                                </tr>
                                <tr>
                                    <td>Azotobacter</td>
                                    <td>23.20</td>
                                </tr>
                                <tr>
                                    <th>Total Production</th>
                                    <th>255.64</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Indrapuri Production 16  -->
            <div class="modal fade" id="Indrapuri16" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Indrapuri Plant Production - 2015-16</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>Product Name</th>
                                    <th>Quantity in Mt. Tn.</th>
                                </tr>
                                <tr>
                                    <td>P.S.B (250 gm pkt)</td>
                                    <td>258.27</td>
                                </tr>
                                <tr>
                                    <td>Rhizobium (150 gm pkt)</td>
                                    <td>112.80</td>
                                </tr>
                                <tr>
                                    <td>Azotobacter</td>
                                    <td>24.84</td>
                                </tr>
                                <tr>
                                    <th>Total Production</th>
                                    <th>395.91</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Indrapuri Production 15  -->
            <div class="modal fade" id="Indrapuri15" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Indrapuri Plant Production - 2014-15</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>Product Name</th>
                                    <th>Quantity in Mt. Tn.</th>
                                </tr>
                                <tr>
                                    <td>P.S.B (250 gm pkt)</td>
                                    <td>224.1</td>
                                </tr>
                                <tr>
                                    <td>Rhizobium (150 gm pkt)</td>
                                    <td>120.61</td>
                                </tr>
                                <tr>
                                    <td>Azotobacter</td>
                                    <td>34.15</td>
                                </tr>
                                <tr>
                                    <th>Total Production</th>
                                    <th>378</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Babai Production 19  -->
            <div class="modal fade" id="Babai19" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Babai Plant Production - 2018-19</h4>
                        </div>
                        <div class="modal-body">
                            <h4>फसल के आये – व्यय की जानकारी</h4>
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>फसल का नाम किस्म</th>
                                    <th>रकबा एकड़ में</th>
                                    <th>उत्पादन क्विंटल में</th>
                                    <th>औसत उत्पादन क्विंटल में</th>
                                    <th>आय रुपये</th>
                                    <th>व्यय रुपये</th>
                                    <th>लाभ / हानि</th>
                                </tr>
                                <tr>
                                    <td>धान 1121</td>
                                    <td>137.00</td>
                                    <td>1007.47</td>
                                    <td>7.35</td>
                                    <td>-----</td>
                                    <td>2546237.00</td>
                                    <td>-----</td>
                                </tr>
                                <tr>
                                    <td>गेहू 1203</td>
                                    <td>297.00</td>
                                    <td>2556.60</td>
                                    <td>8.60</td>
                                    <td>-----</td>
                                    <td>23759089.00</td>
                                    <td>-----</td>
                                </tr>
                                <tr>
                                    <th>योग</th>
                                    <th></th>
                                    <th>3464.07</th>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                    <th>-----</th>
                                </tr>
                            </table>

                            <h4>बगीचों के आये – व्यय की जानकारी</h4>
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>बगीचे का नाम</th>
                                    <th>आय रुपये</th>
                                    <th>व्यय रुपये</th>
                                    <th>लाभ / हानि</th>
                                </tr>
                                <tr>
                                    <td>आम</td>
                                    <td>3374099.00</td>
                                    <td>00.00</td>
                                    <td>3374099.00</td>
                                </tr>
                                <tr>
                                    <td>कटहल</td>
                                    <td>531786.00</td>
                                    <td>00.00</td>
                                    <td>531786.00</td>
                                </tr>
                                <tr>
                                    <td>अमरुद</td>
                                    <td>893999.00</td>
                                    <td>00.00</td>
                                    <td>893999.00</td>
                                </tr>
                                <tr>
                                    <td>नींबू</td>
                                    <td>16786.00</td>
                                    <td>00.00</td>
                                    <td>16786.00</td>
                                </tr>
                                <tr>
                                    <td>आंवला</td>
                                    <td>20786.00</td>
                                    <td>00.00</td>
                                    <td>20786.00</td>
                                </tr>
                                <tr>
                                    <th>योग</th>
                                    <th>4837456.00</th>
                                    <th>00.00</th>
                                    <th>4837456.00</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Babai Production 18  -->
            <div class="modal fade" id="Babai18" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Babai Plant Production - 2017-18</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>फसल का नाम किस्म</th>
                                    <th>रकबा एकड़ में</th>
                                    <th>उत्पादन क्विंटल में</th>
                                    <th>औसत उत्पादन क्विंटल में</th>
                                    <th>आय रुपये</th>
                                    <th>व्यय रुपये</th>
                                    <th>लाभ / हानि</th>
                                </tr>
                                <tr>
                                    <td>धान 1121</td>
                                    <td>193.00</td>
                                    <td>1421.25</td>
                                    <td>7.36</td>
                                    <td>3817768.00</td>
                                    <td>2846908.00</td>
                                    <td>970860.00</td>
                                </tr>
                                <tr>
                                    <td>गेहू 322</td>
                                    <td>400.00</td>
                                    <td>3334.20</td>
                                    <td>8.33</td>
                                    <td>5936075.00</td>
                                    <td>3756735.00</td>
                                    <td>2179340.00</td>
                                </tr>
                                <tr>
                                    <td>मूंग</td>
                                    <td>98.00</td>
                                    <td>99.00</td>
                                    <td>1.02</td>
                                    <td>301104.00</td>
                                    <td>642862.00</td>
                                    <td>-347158.00</td>
                                </tr>
                                <tr>
                                    <th>Total</th>
                                    <th></th>
                                    <th>4854.45</th>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                    <th>2803042.00</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Babai Production 17  -->
            <div class="modal fade" id="Babai17" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Babai Plant Production - 2016-17</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>फसल का नाम किस्म</th>
                                    <th>रकबा एकड़ में</th>
                                    <th>उत्पादन क्विंटल में</th>
                                    <th>औसत उत्पादन क्विंटल में</th>
                                    <th>आय रुपये</th>
                                    <th>व्यय रुपये</th>
                                    <th>लाभ / हानि</th>
                                </tr>
                                <tr>
                                    <td>धान 1121</td>
                                    <td>177.50</td>
                                    <td>1988.82</td>
                                    <td>11.20</td>
                                    <td>4952250.00</td>
                                    <td>2784137.00</td>
                                    <td>2168113.00</td>
                                </tr>
                                <tr>
                                    <td>गेहू</td>
                                    <td>425.00</td>
                                    <td>3846.60</td>
                                    <td>9.05</td>
                                    <td>6250724.00</td>
                                    <td>4754327.00</td>
                                    <td>1496397.00</td>
                                </tr>
                                <tr>
                                    <th>Toatal</th>
                                    <th></th>
                                    <th>5835.42</th>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                    <th>3664510.00</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Babai Production 16  -->
            <div class="modal fade" id="Babai16" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Babai Plant Production - 2015-16</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>फसल का नाम किस्म</th>
                                    <th>रकबा एकड़ में</th>
                                    <th>उत्पादन क्विंटल में</th>
                                    <th>औसत उत्पादन क्विंटल में</th>
                                    <th>आय रुपये</th>
                                    <th>व्यय रुपये</th>
                                    <th>लाभ / हानि</th>
                                </tr>
                                <tr>
                                    <td>गेहू</td>
                                    <td>400.00</td>
                                    <td>3100.37</td>
                                    <td>7.75</td>
                                    <td>4855180.00</td>
                                    <td>3854471.00</td>
                                    <td>960709.00</td>
                                </tr>
                                <tr>
                                    <th>Total</th>
                                    <th></th>
                                    <th>3100.37</th>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                    <th>960709.00</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Babai Production 15  -->
            <div class="modal fade" id="Babai15" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myModalLabel">Babai Plant Production - 2014-15</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th>फसल का नाम किस्म</th>
                                    <th>रकबा एकड़ में</th>
                                    <th>उत्पादन क्विंटल में</th>
                                    <th>औसत उत्पादन क्विंटल में</th>
                                    <th>आय रुपये</th>
                                    <th>व्यय रुपये</th>
                                    <th>लाभ / हानि</th>
                                </tr>
                                <tr>
                                    <td>धान 1121</td>
                                    <td>224.00</td>
                                    <td>2084.80</td>
                                    <td>9.30</td>
                                    <td>2985930.00</td>
                                    <td>3962162.00</td>
                                    <td>-976232.00</td>
                                </tr>
                                <tr>
                                    <td>गेहू 322</td>
                                    <td>435.00</td>
                                    <td>3967.80</td>
                                    <td>9.12</td>
                                    <td>5663440.00</td>
                                    <td>4773103.00</td>
                                    <td>890337.00</td>
                                </tr>
                                <tr>
                                    <th>Total</th>
                                    <th></th>
                                    <th>6052.60</th>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                    <th>89895.00</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>



            <div class="modal fade" id="BadiProductionTop" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myBadiProductionTop">Badi Production</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th height="40" rowspan="2" width="100" style="vertical-align: middle;">Month
                                        <br />
                                        2019 - 20</th>
                                    <th colspan="8" width="307" style="text-align: center">Product Name</th>
                                </tr>
                                <tr>
                                    <th>Halwa</th>
                                    <th>Bal Aahar</th>
                                    <th>Khichadi 625</th>
                                    <th>Barfi</th>
                                    <th>Laddu</th>
                                    <th>Khichadi 750</th>
                                    <th>Sabla Barfi</th>
                                    <th>Khichadi 900</th>
                                </tr>
                                <tr>
                                    <td>April</td>
                                    <td>404.042</td>
                                    <td>199.680</td>
                                    <td>0.00</td>
                                    <td>228.180</td>
                                    <td>232.740</td>
                                    <td>0.00</td>
                                    <td>25.920</td>
                                    <td>0.00</td>
                                </tr>

                                <tr>
                                    <td>May</td>
                                    <td>382.872</td>
                                    <td>193.632</td>
                                    <td>0.00</td>
                                    <td>223.140</td>
                                    <td>105.960</td>
                                    <td>0.00</td>
                                    <td>27.522</td>
                                    <td>0.00</td>
                                </tr>
                                <tr>
                                    <td>June</td>
                                    <td>389.232</td>
                                    <td>195.240</td>
                                    <td>0.00</td>
                                    <td>212.280</td>
                                    <td>105.795</td>
                                    <td>0.00</td>
                                    <td>45.270</td>
                                    <td>0.00</td>
                                </tr>
                                <tr>
                                    <td>July&nbsp;</td>
                                    <td>393.048</td>
                                    <td>394.560</td>
                                    <td>0.00</td>
                                    <td>216.840</td>
                                    <td>109.245</td>
                                    <td>0.00</td>
                                    <td>43.794</td>
                                    <td>0.00</td>
                                </tr>
                                <tr>
                                    <td>August</td>
                                    <td>517.032</td>
                                    <td>293.880</td>
                                    <td>0.00</td>
                                    <td>294.810</td>
                                    <td>146.280</td>
                                    <td>0.00</td>
                                    <td>58.032</td>
                                    <td>0.00</td>
                                </tr>
                                <tr>
                                    <td>September</td>
                                    <td>282.120</td>
                                    <td>93.984</td>
                                    <td>0.00</td>
                                    <td>139.630</td>
                                    <td>67.230</td>
                                    <td>0.00</td>
                                    <td>17.388</td>
                                    <td>0.00</td>
                                </tr>
                                <tr>
                                    <td>October</td>
                                    <td>336.552</td>
                                    <td>178.896</td>
                                    <td>0.00</td>
                                    <td>192.480</td>
                                    <td>197.925</td>
                                    <td>0.00</td>
                                    <td>29.268</td>
                                    <td>0.00</td>
                                </tr>
                                <tr>
                                    <td>November</td>
                                    <td>371.208</td>
                                    <td>185.904</td>
                                    <td>0.00</td>
                                    <td>203.475</td>
                                    <td>101.925</td>
                                    <td>0.00</td>
                                    <td>24.624</td>
                                    <td>0.00</td>
                                </tr>
                                <tr>
                                    <td>December</td>
                                    <td>368.952</td>
                                    <td>366.048</td>
                                    <td>185.500</td>
                                    <td>200.055</td>
                                    <td>101.445</td>
                                    <td>183.090</td>
                                    <td>20.448</td>
                                    <td>19.746</td>
                                </tr>
                                <tr>
                                    <td>January</td>
                                    <td>371.640</td>
                                    <td>186.360</td>
                                    <td>200.950</td>
                                    <td>202.410</td>
                                    <td>101.130</td>
                                    <td>120.855</td>
                                    <td>23.040</td>
                                    <td>25.380</td>
                                </tr>
                                <tr>
                                    <th>Total </th>
                                    <th>3820.680</th>
                                    <th>2288.184</th>
                                    <th>386.450</th>
                                    <th>2113.290</th>
                                    <th>1269.675</th>
                                    <th>303.945</th>
                                    <th>315.306</th>
                                    <th>45.126</th>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>


            <div class="modal fade" id="IndrapuriProduction" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog" style="width: 850px;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" id="myIndrapuriProduction">Indrapuri Production</h4>
                        </div>
                        <div class="modal-body">
                            <table class="table table-bordered table-hover">
                                <tr>
                                    <th height="60" rowspan="3" width="30">S.No</th>
                                    <th rowspan="3" width="82">Date</th>
                                    <th colspan="10" width="726">production</th>
                                    <th rowspan="3" width="69">Total</th>
                                </tr>
                                <tr>
                                    <th height="40" rowspan="2">PSB</th>
                                    <th colspan="8">RHIZOBUM spc.</th>
                                    <th rowspan="2">AZOTOBACTOR</th>
                                </tr>
                                <tr>
                                    <th>soyabean</th>
                                    <th>gram(chana)</th>
                                    <th>moog<span>&nbsp;</span></th>
                                    <th>urad</th>
                                    <th>G.nut</th>
                                    <th>Pea(matter)</th>
                                    <th>lentil(masur)</th>
                                    <th>arhar</th>
                                </tr>
                                <tr>
                                    <td align="right">1</td>
                                    <td align="right">4/1/2019</td>
                                    <td align="right">12800</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">12800</td>
                                </tr>
                                <tr>
                                    <td align="right">2</td>
                                    <td align="right">4/2/2019</td>
                                    <td align="right">13800</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">13800</td>
                                </tr>
                                <tr>
                                    <td align="right">3</td>
                                    <td align="right">4/3/2019</td>
                                    <td align="right">12300</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">12300</td>
                                </tr>
                                <tr>
                                    <td align="right">4</td>
                                    <td align="right">4/4/2019</td>
                                    <td align="right">11800</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">11800</td>
                                </tr>
                                <tr>
                                    <td align="right">5</td>
                                    <td align="right">4/5/2019</td>
                                    <td align="right">11800</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">11800</td>
                                </tr>
                                <tr>
                                    <td align="right">6</td>
                                    <td align="right">4/8/2019</td>
                                    <td align="right">5700</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">5700</td>
                                </tr>
                                <tr>
                                    <td align="right">7</td>
                                    <td align="right">4/9/2019</td>
                                    <td align="right">5700</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">5700</td>
                                </tr>
                                <tr>
                                    <td align="right">8</td>
                                    <td align="right">4/10/2019</td>
                                    <td align="right">3700</td>
                                    <td align="right">3000</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">6700</td>
                                </tr>
                                <tr>
                                    <td align="right">9</td>
                                    <td align="right">4/11/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">7500</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">7500</td>
                                </tr>
                                <tr>
                                    <td align="right">10</td>
                                    <td align="right">4/12/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">8400</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">8400</td>
                                </tr>
                                <tr>
                                    <td align="right">11</td>
                                    <td align="right">4/15/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">7500</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">7500</td>
                                </tr>
                                <tr>
                                    <td align="right">12</td>
                                    <td align="right">4/16/2019</td>
                                    <td align="right">1900</td>
                                    <td align="right">5400</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">7300</td>
                                </tr>
                                <tr>
                                    <td align="right">13</td>
                                    <td align="right">4/18/2019</td>
                                    <td align="right">4400</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4400</td>
                                </tr>
                                <tr>
                                    <td align="right">14</td>
                                    <td align="right">4/20/2019</td>
                                    <td align="right">5600</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">5600</td>
                                </tr>
                                <tr>
                                    <td align="right">15</td>
                                    <td align="right">4/22/2019</td>
                                    <td align="right">5400</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">5400</td>
                                </tr>
                                <tr>
                                    <td align="right">16</td>
                                    <td align="right">4/23/2019</td>
                                    <td align="right">3200</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">3300</td>
                                    <td align="right">6500</td>
                                </tr>
                                <tr>
                                    <td align="right">17</td>
                                    <td align="right">4/24/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">3000</td>
                                    <td align="right">3000</td>
                                </tr>
                                <tr>
                                    <td align="right">18</td>
                                    <td align="right">4/25/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">7600</td>
                                    <td align="right">7600</td>
                                </tr>
                                <tr>
                                    <td align="right">19</td>
                                    <td align="right">4/26/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">9800</td>
                                    <td align="right">9800</td>
                                </tr>
                                <tr>
                                    <td align="right">20</td>
                                    <td align="right">4/27/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">12300</td>
                                    <td align="right">12300</td>
                                </tr>
                                <tr>
                                    <td align="right">21</td>
                                    <td align="right">4/29/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">6000</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">6000</td>
                                </tr>
                                <tr>
                                    <td align="right">22</td>
                                    <td align="right">4/30/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">15900</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">15900</td>
                                </tr>
                                <tr>
                                    <td align="right">23</td>
                                    <td align="right">5/1/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">13700</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">13700</td>
                                </tr>
                                <tr>
                                    <td align="right">24</td>
                                    <td align="right">5/2/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">14500</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">14500</td>
                                </tr>
                                <tr>
                                    <td align="right">25</td>
                                    <td align="right">5/3/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">13300</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">13300</td>
                                </tr>
                                <tr>
                                    <td align="right">26</td>
                                    <td align="right">5/4/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">10700</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">10700</td>
                                </tr>
                                <tr>
                                    <td align="right">27</td>
                                    <td align="right">5/6/2019</td>
                                    <td align="right">1800</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">1800</td>
                                </tr>
                                <tr>
                                    <td align="right">28</td>
                                    <td align="right">5/8/2019</td>
                                    <td align="right">2100</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">2100</td>
                                </tr>
                                <tr>
                                    <td align="right">29</td>
                                    <td align="right">5/9/2019</td>
                                    <td align="right">2300</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">2300</td>
                                </tr>
                                <tr>
                                    <td align="right">30</td>
                                    <td align="right">5/14/2019</td>
                                    <td align="right">2000</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">2000</td>
                                </tr>
                                <tr>
                                    <td align="right">31</td>
                                    <td align="right">5/15/2019</td>
                                    <td align="right">3400</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">3400</td>
                                </tr>
                                <tr>
                                    <td align="right">32</td>
                                    <td align="right">5/16/2019</td>
                                    <td align="right">4300</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4300</td>
                                </tr>
                                <tr>
                                    <td align="right">33</td>
                                    <td align="right">5/17/2019</td>
                                    <td align="right">4100</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4100</td>
                                </tr>
                                <tr>
                                    <td align="right">34</td>
                                    <td align="right">5/20/2019</td>
                                    <td align="right">3500</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">3500</td>
                                </tr>
                                <tr>
                                    <td align="right">35</td>
                                    <td align="right">5/21/2019</td>
                                    <td align="right">4200</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4200</td>
                                </tr>
                                <tr>
                                    <td align="right">36</td>
                                    <td align="right">5/22/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">4200</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4200</td>
                                </tr>
                                <tr>
                                    <td align="right">37</td>
                                    <td align="right">5/23/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">3900</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">2800</td>
                                    <td align="right">6700</td>
                                </tr>
                                <tr>
                                    <td align="right">38</td>
                                    <td align="right">5/24/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4300</td>
                                    <td align="right">4300</td>
                                </tr>
                                <tr>
                                    <td align="right">39</td>
                                    <td align="right">5/25/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">6000</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">6000</td>
                                </tr>
                                <tr>
                                    <td align="right">40</td>
                                    <td align="right">5/26/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">5900</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">5900</td>
                                </tr>
                                <tr>
                                    <td align="right">41</td>
                                    <td align="right">5/27/2019</td>
                                    <td align="right">800</td>
                                    <td align="right">7400</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">8200</td>
                                </tr>
                                <tr>
                                    <td align="right">42</td>
                                    <td align="right">5/28/2019</td>
                                    <td align="right">5400</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">2100</td>
                                    <td align="right">7500</td>
                                </tr>
                                <tr>
                                    <td align="right">43</td>
                                    <td align="right">5/29/2019</td>
                                    <td align="right">5600</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">5600</td>
                                </tr>
                                <tr>
                                    <td align="right">44</td>
                                    <td align="right">5/30/2019</td>
                                    <td align="right">800</td>
                                    <td align="right">2800</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">3600</td>
                                </tr>
                                <tr>
                                    <td align="right">45</td>
                                    <td align="right">5/31/2019</td>
                                    <td align="right">3300</td>
                                    <td align="right">5600</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">8900</td>
                                </tr>
                                <tr>
                                    <td align="right">46</td>
                                    <td align="right">6/1/2019</td>
                                    <td align="right">800</td>
                                    <td align="right">3600</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td>&nbsp;</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">5100</td>
                                    <td align="right">9500</td>
                                </tr>
                                <tr>
                                    <td align="right">47</td>
                                    <td align="right">6/3/2019</td>
                                    <td align="right">2100</td>
                                    <td align="right">8500</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">10600</td>
                                </tr>
                                <tr>
                                    <td align="right">48</td>
                                    <td align="right">6/4/2019</td>
                                    <td align="right">7100</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">7100</td>
                                </tr>
                                <tr>
                                    <td align="right">49</td>
                                    <td align="right">6/6/2019</td>
                                    <td align="right">7800</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">7800</td>
                                </tr>
                                <tr>
                                    <td align="right">50</td>
                                    <td align="right">6/7/2019</td>
                                    <td align="right">4600</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4600</td>
                                </tr>
                                <tr>
                                    <td align="right">51</td>
                                    <td align="right">6/8/2019</td>
                                    <td align="right">3000</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">3000</td>
                                </tr>
                                <tr>
                                    <td align="right">52</td>
                                    <td align="right">6/9/2019</td>
                                    <td align="right">3900</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">1700</td>
                                    <td align="right">5600</td>
                                </tr>
                                <tr>
                                    <td align="right">53</td>
                                    <td align="right">6/10/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">7700</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">3400</td>
                                    <td align="right">11100</td>
                                </tr>
                                <tr>
                                    <td align="right">54</td>
                                    <td align="right">6/11/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">7200</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">7200</td>
                                </tr>
                                <tr>
                                    <td align="right">55</td>
                                    <td align="right">6/12/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">7400</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">7400</td>
                                </tr>
                                <tr>
                                    <td align="right">56</td>
                                    <td align="right">6/13/2019</td>
                                    <td align="right">7600</td>
                                    <td align="right">2800</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">10400</td>
                                </tr>
                                <tr>
                                    <td align="right">57</td>
                                    <td align="right">6/14/2019</td>
                                    <td align="right">8500</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">8500</td>
                                </tr>
                                <tr>
                                    <td align="right">58</td>
                                    <td align="right">6/15/2019</td>
                                    <td align="right">800</td>
                                    <td align="right">100</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">900</td>
                                </tr>
                                <tr>
                                    <td align="right">59</td>
                                    <td align="right">6/17/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">200</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">200</td>
                                </tr>
                                <tr>
                                    <td align="right">60</td>
                                    <td align="right">6/18/2019</td>
                                    <td align="right">2000</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">2000</td>
                                </tr>
                                <tr>
                                    <td align="right">61</td>
                                    <td align="right">6/19/2019</td>
                                    <td align="right">5700</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">5700</td>
                                </tr>
                                <tr>
                                    <td align="right">62</td>
                                    <td align="right">6/20/2019</td>
                                    <td align="right">2000</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">2000</td>
                                </tr>
                                <tr>
                                    <td align="right">63</td>
                                    <td align="right">6/21/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">4200</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4200</td>
                                </tr>
                                <tr>
                                    <td align="right">64</td>
                                    <td align="right">6/22/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">6400</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">6400</td>
                                </tr>
                                <tr>
                                    <td align="right">65</td>
                                    <td align="right">6/24/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">5200</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">5200</td>
                                </tr>
                                <tr>
                                    <td align="right">66</td>
                                    <td align="right">6/25/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">4900</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4900</td>
                                </tr>
                                <tr>
                                    <td align="right">67</td>
                                    <td align="right">6/27/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">175</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">175</td>
                                </tr>
                                <tr>
                                    <td align="right">68</td>
                                    <td align="right">7/2/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4</td>
                                </tr>
                                <tr>
                                    <td align="right">69</td>
                                    <td align="right">7/6/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td class="xl72">TOTAL</td>
                                    <td align="right">197600</td>
                                    <td align="right">200075</td>
                                    <td align="right">0</td>
                                    <td align="right">4</td>
                                    <td align="right">4</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">55400</td>
                                    <td align="right">453083</td>
                                </tr>
                                <tr>
                                    <td align="right">1</td>
                                    <td align="right">9/18/2019</td>
                                    <td align="right">4500</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4500</td>
                                </tr>
                                <tr>
                                    <td align="right">2</td>
                                    <td align="right">9/19/2019</td>
                                    <td align="right">5500</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">5500</td>
                                </tr>
                                <tr>
                                    <td align="right">3</td>
                                    <td align="right">9/20/2019</td>
                                    <td align="right">4100</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4100</td>
                                </tr>
                                <tr>
                                    <td align="right">4</td>
                                    <td align="right">9/21/2019</td>
                                    <td align="right">3500</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">3500</td>
                                </tr>
                                <tr>
                                    <td align="right">5</td>
                                    <td align="right">9/23/2019</td>
                                    <td align="right">3300</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">3300</td>
                                </tr>
                                <tr>
                                    <td align="right">6</td>
                                    <td align="right">9/24/2019</td>
                                    <td align="right">3900</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">3900</td>
                                </tr>
                                <tr>
                                    <td align="right">7</td>
                                    <td align="right">9/24/2019</td>
                                    <td align="right">4500</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">4500</td>
                                </tr>
                                <tr>
                                    <td align="right">8</td>
                                    <td align="right">9/25/2019</td>
                                    <td align="right">8000</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">8000</td>
                                </tr>
                                <tr>
                                    <td align="right">9</td>
                                    <td align="right">9/26/2019</td>
                                    <td align="right">8300</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">8300</td>
                                </tr>
                                <tr>
                                    <td align="right">10</td>
                                    <td align="right">9/27/2019</td>
                                    <td align="right">2300</td>
                                    <td align="right">0</td>
                                    <td align="right">5400</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">7700</td>
                                </tr>
                                <tr>
                                    <td align="right">11</td>
                                    <td align="right">9/28/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">11600</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">11600</td>
                                </tr>
                                <tr>
                                    <td align="right">12</td>
                                    <td align="right">9/30/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">9100</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">9100</td>
                                </tr>
                                <tr>
                                    <td align="right">13</td>
                                    <td align="right">1/1/2019</td>
                                    <td>&nbsp;</td>
                                    <td align="right">0</td>
                                    <td align="right">12300</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">12300</td>
                                </tr>
                                <tr>
                                    <td align="right">14</td>
                                    <td align="right">10/3/2019</td>
                                    <td align="right">7000</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">7000</td>
                                </tr>
                                <tr>
                                    <td align="right">15</td>
                                    <td align="right">10/4/2019</td>
                                    <td align="right">10900</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">10900</td>
                                </tr>
                                <tr>
                                    <td align="right">16</td>
                                    <td align="right">10/5/2019</td>
                                    <td align="right">9500</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">9500</td>
                                </tr>
                                <tr>
                                    <td align="right">17</td>
                                    <td align="right">10/6/2019</td>
                                    <td align="right">5900</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">5900</td>
                                </tr>
                                <tr>
                                    <td align="right">18</td>
                                    <td align="right">10/9/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">10300</td>
                                    <td align="right">10300</td>
                                </tr>
                                <tr>
                                    <td align="right">19</td>
                                    <td align="right">10/10/2019</td>
                                    <td align="right">2400</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">5800</td>
                                    <td align="right">8200</td>
                                </tr>
                                <tr>
                                    <td align="right">20</td>
                                    <td align="right">10/11/2019</td>
                                    <td align="right">8200</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">8200</td>
                                </tr>
                                <tr>
                                    <td align="right">21</td>
                                    <td align="right">10/15/2019</td>
                                    <td align="right">0</td>
                                    <td align="right" class="xl65">0</td>
                                    <td align="right">12300</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">12300</td>
                                </tr>
                                <tr>
                                    <td align="right">22</td>
                                    <td align="right">10/16/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">13100</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">13100</td>
                                </tr>
                                <tr>
                                    <td align="right">23</td>
                                    <td align="right">10/17/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">5700</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">5700</td>
                                </tr>
                                <tr>
                                    <td align="right">24</td>
                                    <td align="right">10/18/2019</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">5400</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">3700</td>
                                    <td align="right">9100</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td align="right">91800</td>
                                    <td align="right">0</td>
                                    <td align="right">74900</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">0</td>
                                    <td align="right">19800</td>
                                    <td align="right">186500</td>
                                </tr>
                            </table>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script src="../../../mis/Dashboard/js/jquery-1.10.2.js"></script>
    <!-- Bootstrap Js -->
    <%-- <script src="js/bootstrap.min.js"></script>--%>

    <!-- Metis Menu Js -->
    <script src="../../../mis/Dashboard/js/jquery.metisMenu.js"></script>
    <!-- Morris Chart Js -->
    <script src="../../../mis/Dashboard/js/morris/raphael-2.1.0.min.js"></script>
    <script src="../../../mis/Dashboard/js/morris/morris.js"></script>


    <script src="../../../mis/Dashboard/js/easypiechart.js"></script>
    <script src="../../../mis/Dashboard/js/easypiechart-data.js"></script>

    <script src="../../../mis/Dashboard/js/Lightweight-Chart/jquery.chart.js"></script>

    <!-- Custom Js -->
    <script src="../../../mis/Dashboard/js/custom-scripts.js"></script>

    <!-- Chart Js -->
    <script type="text/javascript" src="../../../mis/Dashboard/js/chart.min.js"></script>
    <script type="text/javascript" src="../../../mis/Dashboard/js/chartjs.js"></script>
</asp:Content>

