<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRGMDashboard.aspx.cs" Inherits="mis_Dashboard_HRGMDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="assets_dashboard/custom-dashboard.css" rel="stylesheet" />
    <style>
        .info-box-icon {
            height: 50px;
            width: 50px;
            font-size: 25px;
            line-height: 50px;
        }

        .info-box {
            min-height: 50px;
        }

        .box {
            border-top: 3px solid #ffffff;
        }

        .info-box-number {
            display: block;
            font-weight: bold;
            font-size: 12px;
        }

        .info-box-content {
            padding: 5px 10px;
            margin-left: 50px;
        }

        main {
            width: 100%;
            margin: 10px auto;
            padding: 10px 20px 30px;
            background: #FFF;
            box-shadow: 0 3px 5px rgba(0,0,0,0.2);
        }

        p {
            margin-top: 2rem;
            font-size: 13px;
        }

        #bar-chart {
            width: 100%;
            height: 300px;
            position: relative;
        }

        #line-chart {
            width: 500px;
            height: 300px;
            position: relative;
        }

            #bar-chart::before, #line-chart::before {
                content: "";
                position: absolute;
                display: block;
                width: 240px;
                height: 30px;
                left: 155px;
                top: 254px;
                background: #FAFAFA;
                box-shadow: 1px 1px 0 0 #DDD;
            }

        #pie-chart {
            width: 100%;
            height: 250px;
            position: relative;
        }

            #pie-chart::before {
                content: "";
                position: absolute;
                display: block;
                width: 120px;
                height: 115px;
                left: 315px;
                top: 0;
                background: #FAFAFA;
                box-shadow: 1px 1px 0 0 #DDD;
            }

            #pie-chart::after {
                content: "";
                position: absolute;
                display: block;
                top: 260px;
                left: 70px;
                width: 170px;
                height: 2px;
                background: rgba(0,0,0,0.1);
                border-radius: 50%;
                box-shadow: 0 0 3px 4px rgba(0,0,0,0.1);
            }

        .box {
            min-height: auto;
        }

        .info-box-content {
            padding: 3px 5px;
            margin-left: 50px;
        }

        .info-box {
            min-height: 45px !important;
        }

        .info-box {
            display: block;
            min-height: 90px;
            background: #fff;
            width: 100%;
            box-shadow: 0 2px 3px rgba(0, 0, 0, 0.13);
            border-radius: 2px;
            margin-bottom: 12px;
        }

        .info-box-icon {
            height: 46px;
            width: 50px;
            font-size: 25px;
            line-height: 45px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content performance-block" id="performance-dashboard">
            <div class="row">
                <div class="col-md-12">
                    <div class="">
                        <div class="">
                            <h2 class="title-heading">Human Resource Management System<span>
मानव संसाधन प्रबंधन प्रणाली</span></h2>
                            <div class="other-schemes">
                                <!---<div class="row">
                                    <div class="col-md-4">
                                        <div class="box box-pramod">
                                            <div class="box-body">
                                                <div class="col-md-12">
                                                    <div class="box-header with-border">
                                                        <h3 class="box-title">निगम के कार्यालय  <small>(Corporation Offices)</small></h3>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="info-box">
                                                            <span class="info-box-icon bg-green"><i class="ion ion-ios-home-outline"></i></span>
                                                            <div class="info-box-content">
                                                                <span class="info-box-text">प्रधान कार्यालय</span>
                                                                <span class="info-box-number">01 <small>Head Office</small></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix visible-sm-block"></div>
                                                    <div class="col-md-12">
                                                        <div class="info-box">
                                                            <span class="info-box-icon bg-aqua"><i class="ion ion-ios-people-outline"></i></span>

                                                            <div class="info-box-content">
                                                                <span class="info-box-text">क्षेत्रीय कार्यालय
                                                                </span>
                                                                <span class="info-box-number">07 <small>Regional Office</small></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="info-box">
                                                            <span class="info-box-icon bg-red"><i class="ion ion-ios-people-outline"></i></span>

                                                            <div class="info-box-content">
                                                                <span class="info-box-text">जिला एवं शाखा कार्यालय</span>
                                                                <span class="info-box-number">50 <small>Branch Office</small></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="clearfix visible-sm-block"></div>

                                                    <div class="col-md-12">
                                                        <div class="info-box">
                                                            <span class="info-box-icon bg-green"><i class="ion ion-xbox"></i></span>

                                                            <div class="info-box-content">
                                                                <span class="info-box-text">उत्पादन इकार्इयां</span>
                                                                <span class="info-box-number">02 <small>Production Unit</small></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="info-box">
                                                            <span class="info-box-icon bg-yellow"><i class="ion ion-ios-people-outline"></i></span>

                                                            <div class="info-box-content">
                                                                <span class="info-box-text">कृषि प्रक्षेत्र
                                                                </span>
                                                                <span class="info-box-number">01 <small>Agriculture Farm</small></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <div class="info-box">
                                                            <span class="info-box-icon bg-blue"><i class="ion ion-ios-people-outline"></i></span>

                                                            <div class="info-box-content">
                                                                <span class="info-box-text">निर्माण इकाई
                                                                </span>
                                                                <span class="info-box-number">01 <small>Fabrication Unit</small></span>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-8">
                                        <div class="box box-pramod">
                                            <div class="box-header with-border">
                                                <h3 class="box-title">स्वीकृत पदों की संख्या/ भरे पदो की संख्या <small>(Overall Sanctioned & Filled Post In The Department) </small></h3>
                                                <script type="text/javascript" src="https://www.google.com/jsapi"></script>
                                                <main>
				  <h5></h5>
				  <div id="bar-chart"></div>
				</main>
                                            </div>
                                        </div>
                                    </div>
                                </div>
								--->

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <p class="DashboardSubHeading">&nbsp;निगम में कुल कर्मचारी एवं कार्यालय</p>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="list-block">
                                                <div class="scheme-block">
                                                    <a href="../HR/HREmpDynamicReport.aspx" style="cursor: default;">
                                                        <img src="assets_dashboard/collaboration.png" alt="scheme" />
                                                        <div class="title-text">Total Employees in Corporation</div>
                                                    </a>
                                                    <a href="../HR/HREmpList.aspx" target="_blank" class="detail" style="cursor: default;">
                                                        <div class="detail-inner">
                                                            <div class="scm-text">निगम में कुल कर्मचारियों की संख्या </div>
                                                            <div class="scm-text"><span id="TotalEmp" runat="server"></span></div>
                                                            <div class="scm-text"><span></span></div>
                                                        </div>
                                                    </a>
                                                    <p><span class="Count" id="spnEmpCount" runat="server"></span></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="list-block">
                                                <div class="scheme-block">
                                                    <a href="#" style="cursor: default;">
                                                        <img src="assets_dashboard/offices.png" alt="scheme" />
                                                        <div class="title-text">Total Offices In Corporation</div>
                                                    </a>
                                                    <a href="../Admin/AdminAllOffice.aspx" class="detail" target="_blank" style="cursor: default;">
                                                        <div class="detail-inner">
                                                            <div class="scm-text">निगम में कुल कार्यालयों की संख्या </div>
                                                            <div class="scm-text"><span id="TotalOffice" runat="server"></span></div>
                                                            <div class="scm-text"><span></span></div>
                                                        </div>
                                                    </a>
                                                    <p><span class="Count" id="spnOfficeCount" runat="server"></span></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="list-block">
                                                <div class="scheme-block">
                                                    <a href="#" style="cursor: default;">
                                                        <img src="assets_dashboard/collaboration.png" alt="scheme" />
                                                        <div class="title-text">Today Employee On Leave</div>
                                                    </a>
                                                    <a href="../HR/HREmpTodayOnLeave.aspx" target="_blank" class="detail" style="cursor: default;">
                                                        <div class="detail-inner">
                                                            <div class="scm-text">
                                                                आज कर्मचारी छुट्टी पर हैं
                                                            </div>
                                                            <div class="scm-text"><span id="TodayOnLeave" runat="server"></span></div>
                                                            <div class="scm-text"><span></span></div>
                                                        </div>
                                                    </a>
                                                    <p><span class="Count" id="spnTodayOnLeave" runat="server"></span></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <p class="DashboardSubHeading">&nbsp;स्थानांतरण</p>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="list-block">
                                                <div class="scheme-block">
                                                    <a href="#" style="cursor: default;">
                                                        <img src="assets_dashboard/transfer.png" alt="scheme" />
                                                        <div class="title-text">Transfers In Last 30 Days</div>
                                                    </a>
                                                    <a href="../HR/HRTransferInLast30days.aspx" class="detail" target="_blank" style="cursor: default;">
                                                        <div class="detail-inner">
                                                            <div class="scm-text">विगत 30 दिन में हुए स्थानांतरण</div>
                                                            <div class="scm-text"><span id="LastMonthTransfer" runat="server"></span></div>
                                                            <div class="scm-text"><span></span></div>
                                                        </div>
                                                    </a>
                                                    <p><span class="Count" id="spnLastMonthTransfer" runat="server"></span></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="list-block">
                                                <div class="scheme-block">
                                                    <a href="#" style="cursor: default;">
                                                        <img src="assets_dashboard/relieving.png" alt="scheme" />
                                                        <div class="title-text">Pending Office Relievings After Transfer</div>
                                                    </a>
                                                    <a href="../HR/HRPandingRelieving.aspx" target="_blank" class="detail" style="cursor: default;">
                                                        <div class="detail-inner">
                                                            <div class="scm-text">स्थानांतरण के बाद  लंबित कार्यालय से राहत</div>
                                                            <div class="scm-text"><span id="PendingReleiving" runat="server"></span></div>
                                                            <div class="scm-text"><span></span></div>
                                                        </div>
                                                    </a>
                                                    <p><span class="Count" id="spnPendingReleiving" runat="server"></span></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="list-block">
                                                <div class="scheme-block">
                                                    <a href="#" style="cursor: default;">
                                                        <img src="assets_dashboard/joining.png" alt="scheme" />
                                                        <div class="title-text">Pending joinings After transfer</div>
                                                    </a>
                                                    <a href="../HR/HRPendingJoiningList.aspx" class="detail" target="_blank" style="cursor: default;">
                                                        <div class="detail-inner">
                                                            <div class="scm-text">स्थानांतरण के बाद लंबित ज्वाइनिंग</div>
                                                            <div class="scm-text"><span id="PendingJoining" runat="server"></span></div>
                                                            <div class="scm-text"><span></span></div>
                                                        </div>
                                                    </a>
                                                    <p><span class="Count" id="spnPendingJoining" runat="server"></span></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <p class="DashboardSubHeading">&nbsp; जन्मदिन एवं  सेवा-निवृत्ति</p>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="list-block">
                                                <div class="scheme-block">
                                                    <a href="#" style="cursor: default;">
                                                        <img src="assets_dashboard/directory.png" alt="scheme" />
                                                        <div class="title-text">Employee Directory</div>
                                                    </a>
                                                    <a href="../HR/HREmpDirectory.aspx" class="detail" target="_blank" style="cursor: default;">
                                                        <div class="detail-inner">
                                                            <div class="scm-text">कर्मचारी निर्देशिका</div>
                                                            <div class="scm-text"><span id="TotalEmployee" runat="server"></span></div>
                                                            <div class="scm-text"><span></span></div>
                                                        </div>
                                                    </a>
                                                    <p><span class="Count" id="spnTotalEmployee" runat="server"></span></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="list-block">
                                                <div class="scheme-block">
                                                    <a href="#" style="cursor: default;">
                                                        <img src="assets_dashboard/cake.png" alt="scheme" />
                                                        <div class="title-text">Birthdays In this Month</div>
                                                    </a>
                                                    <a href="../HR/HREmpBdyCuurentMonth.aspx" class="detail" target="_blank" style="cursor: default;">
                                                        <div class="detail-inner">
                                                            <div class="scm-text">
                                                                इस महीने में जन्मदिन
                                                            </div>
                                                            <div class="scm-text"><span id="BirthdayThisMonth" runat="server"></span></div>
                                                            <div class="scm-text"><span></span></div>
                                                        </div>
                                                    </a>
                                                    <p><span class="Count" id="spnBirthdayThisMonth" runat="server"></span></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="list-block">
                                                <div class="scheme-block">
                                                    <a href="#" style="cursor: default;">
                                                        <img src="assets_dashboard/retirement.png" alt="scheme" />
                                                        <div class="title-text">Employee requested to you for leave </div>
                                                    </a>
                                                    <a href="../HR/HRPendingLeaveRequestforDshbrd.aspx" target="_blank" class="detail" style="cursor: default;">
                                                        <div class="detail-inner">
                                                            <div class="scm-text">
                                                                कर्मचारी ने आपसे छुट्टी के लिए अनुरोध किया

                                                            </div>
                                                            <div class="scm-text"><span id="LeaveRequest" runat="server"></span></div>
                                                            <div class="scm-text"><span></span></div>
                                                        </div>
                                                    </a>
                                                    <p><span class="Count" id="spnLeaveRequest" runat="server"></span></p>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                </div>

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="box box-pramod">
                                            <div class="box-header with-border">
                                                <h3 class="box-title">कर्मचारी इस महीने में सेवानिवृत्त हो रहे हैं <small>(Employee's getting retired in this month)</small></h3>
                                            </div>
                                            <div class="box-body">
                                                <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-striped table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="Emp_ID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Emp_Name" HeaderText="कर्मचारी का नाम <br/><small>(Employee Name )</small>" HtmlEncode="False" />
                                                        <asp:BoundField DataField="FromDept" HeaderText="पदनाम <br/><small>(Designation) </small>" HtmlEncode="False" />
                                                        <asp:BoundField DataField="Emp_Class" HeaderText="श्रेणी <br/><small>( Class) </small>" HtmlEncode="False" />
                                                        <asp:BoundField DataField="FromOffice" HeaderText="कार्यालय  <br/><small>(Office) </small>" HtmlEncode="False" />
                                                        <asp:BoundField DataField="Emp_PostingDate" HeaderText="कार्यग्रहण दिनांक <br/><small>(Joining date) </small>" HtmlEncode="False" />
                                                        <asp:BoundField DataField="Emp_RetirementDate" HeaderText="सेवानिवृत्ति की  दिनांक <br/><small>(Retirement Date) </small>" HtmlEncode="False" />
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <div class="text-red">** इस माह कोई भी कर्मचारी सेवानिवृत्ति नहीं हो रहे हैं | **</div>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                        </div>
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
    <script src="assets_dashboard/jquery.min.js"></script>
    <script src="assets_dashboard/waypoints.min.js"></script>
    <script src="assets_dashboard/jquery.counterup.min.js"></script>
    <script>
        jQuery(document).ready(function ($) {
            $('.Count').counterUp({
                delay: 100,
                time: 1000
            });
        });
    </script>
    <script>
        /********
        google.load("visualization", "1", { packages: ["corechart"] });
        google.setOnLoadCallback(drawCharts);
        function drawCharts() {
            var barData = google.visualization.arrayToDataTable([
              ['पदनाम', 'स्वीकृत पदों की संख्या', 'भरे पदो की संख्या	', 'रिक्त पदों की संख्या'],
              ['प्रथम श्रेणी', 56, 26, 30],
              ['द्धितीय श्रेणी', 72, 25, 47],
              ['तृतीय श्रेणी', 528, 241, 287],
              ['चतुर्थ श्रेणी', 180, 112, 68],
              ['दैनिक वेतनभोगी व संविदा कर्मचारी', 660, 660, 0]
            ]);
            var barOptions = {
                focusTarget: 'category',
                backgroundColor: 'transparent',
                colors: ['cornflowerblue', 'darkcyan', 'coral'],
                fontName: 'Open Sans',
                chartArea: {
                    left: 50,
                    top: 10,
                    width: '100%',
                    height: '70%'
                },
                bar: {
                    groupWidth: '80%'
                },
                hAxis: {
                    textStyle: {
                        fontSize: 12
                    }
                },
                vAxis: {
                    minValue: 0,
                    maxValue: 1500,
                    baselineColor: '#DDD',
                    gridlines: {
                        color: '#DDD',
                        count: 4
                    },
                    textStyle: {
                        fontSize: 11
                    }
                },
                legend: {
                    position: 'bottom',
                    textStyle: {
                        fontSize: 12
                    }
                },
                animation: {
                    duration: 1200,
                    easing: 'out',
                    startup: true
                }
            };
            var barChart = new google.visualization.ColumnChart(document.getElementById('bar-chart'));
            barChart.draw(barData, barOptions);
        }
		****/
    </script>
</asp:Content>
