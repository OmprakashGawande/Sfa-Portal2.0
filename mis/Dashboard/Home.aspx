<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="mis_Dashboard_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="assets/css/Dashboard.css" rel="stylesheet" />
    <style>
        .counter {
            color: #2793D3;
            font-family: 'Poppins', sans-serif;
            text-align: center;
            height: 240px;
            width: 210px;
            padding: 70px 20px;
            margin: 0 auto;
            position: relative;
            z-index: 1;
        }

            .counter:before {
                content: '';
                background: linear-gradient(to right, #f5f5f5, #fff);
                border-radius: 7px;
                border: 3px solid #fff;
                box-shadow: 0 0 8px rgba(0, 0, 0, 0.2);
                transform: rotate(45deg);
                position: absolute;
                left: 30px;
                right: 30px;
                bottom: 45px;
                top: 45px;
                z-index: -1;
            }

            .counter .counter-value {
                font-size: 28px;
                font-weight: 600;
                line-height: 30px;
                display: block;
                margin: 0 0 9px;
            }

                .counter .counter-value:before,
                .counter .counter-value:after {
                    content: '';
                    background: linear-gradient(to right bottom, #5ED3DA, #2793D3, #5ED3DA);
                    height: 80px;
                    width: 80px;
                    border-radius: 10px;
                    transform: translateX(-50%) rotate(45deg);
                    position: absolute;
                    left: 50%;
                    top: 15px;
                    z-index: -2;
                }

                .counter .counter-value:after {
                    top: auto;
                    bottom: 15px;
                }

            .counter h3 {
                color: #888;
                font-size: 16px;
                font-weight: 400;
                text-transform: capitalize;
                margin: 0 0 15px;
            }

            .counter .counter-icon {
                font-size: 30px;
                line-height: 30px;
                margin: 0 0 25px;
            }

            .counter.magenta {
                color: #B20005;
            }

                .counter.magenta .counter-value:before,
                .counter.magenta .counter-value:after {
                    background: linear-gradient(to right bottom, #F00374, #B20005, #F00374);
                }

            .counter.purple {
                color: #8264CC;
            }

                .counter.purple .counter-value:before,
                .counter.purple .counter-value:after {
                    background: linear-gradient(to right bottom, #A978BA, #8264CC, #A978BA);
                }

            .counter.blue {
                color: #183A8C;
            }

                .counter.blue .counter-value:before,
                .counter.blue .counter-value:after {
                    background: linear-gradient(to right bottom, #2873E8, #183A8C, #2873E8);
                }

        @media screen and (max-width:990px) {
            .counter {
                margin-bottom: 40px;
            }
        }


        .schedule-tag {
            border-radius: 10px !important;
            background-color: #e7744a !important;
        }

        .LinkbtnFont {
            font-weight: 900 !important;
            font-size: xx-large !important;
        }

        .BoxLabel {
            font-size: x-large !important;
            font-weight: bold !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="container-fluid">
        <section class="content">
            <div class="page-title">
                <div class="row">
                    <div class="col-6">
                        <h4>ERP</h4>
                    </div>
                    <div class="col-6">
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="../Dashboard/Home.aspx">
                                <svg class="stroke-icon">
                                    <use href="../assets/svg/icon-sprite.svg#stroke-home"></use>
                                </svg></a></li>
                            <li class="breadcrumb-item">ERP</li>
                        </ol>
                    </div>
                </div>
            </div>
            <asp:Label runat="server" ID="lblMsg"></asp:Label>
            <div class="row">
                <div class="col-md-12">
                    <div class="">
                        <div class="box-header ui-sortable-handle" style="cursor: move;">
                            <h3 class=""></h3>
                        </div>
                        <!-- /.box-header -->


                        <div class="row justify-content-center">
                            <div class="col-xl-3 col-sm-6" runat="server" id="Div_DailyTask" visible="false">
                                <div class="bounce-card">
                                    <div class="card o-hidden small-widget">
                                        <div class="card-body total-P border-b-primary border-2">
                                            <span class="f-light f-w-500 f-14">Daily Task</span>
                                            <div class="project-details">
                                                <a href="/mis/Daily_Task/TrnDailyReporting.aspx">
                                                    <asp:Label ID="lblReportStatus" runat="server" CssClass="info-box-number BoxLabel" Text=""></asp:Label>
                                                </a>
                                                <span class="f-12 f-w-400"></span>
                                                <div class="product-sub bg-primary-light">
                                                    <svg class="invoice-icon">
                                                        <use href="../assets/svg/icon-sprite.svg#task-square"></use>
                                                    </svg>
                                                </div>
                                            </div>
                                            <ul class="bubbles">
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                            </ul>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <div class="col-xl-3 col-sm-6" runat="server" id="Div_ResourcesOnProjects" visible="false">

                                <div class="bounce-card">
                                    <div class="card o-hidden small-widget">
                                        <div class="card-body daily-task border-b-purple border-2">
                                            <span class="f-light f-w-500 f-14">Total Resources On Projects</span>
                                            <div class="project-details">
                                                <div class="project-counter">

                                                    <asp:LinkButton ID="lblProjects" CssClass="LinkbtnFont" runat="server" Text="" OnClick="lblProjects_Click"></asp:LinkButton>

                                                    <span class="f-12 f-w-400"></span>
                                                </div>
                                                <div class="product-sub bg-purple-light">
                                                    <svg class="invoice-icon">
                                                        <use href="../assets/svg/icon-sprite.svg#color-swatch"></use>
                                                    </svg>
                                                </div>
                                            </div>
                                            <ul class="bubbles">
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                                <li class="bubble"></li>
                                            </ul>
                                        </div>
                                    </div>

                                </div>

                            </div>
                            <div class="col-xl-3 col-sm-6" runat="server" id="Div_ResourcesOnBench" visible="false">
                                <div class="card o-hidden small-widget">
                                    <div class="card-body total-Progress border-b-warning border-2">
                                        <span class="f-light f-w-500 f-14">Total Resources on Bench</span>
                                        <div class="project-details">
                                            <div class="project-counter">
                                                <h2 class="f-w-900">
                                                    <asp:LinkButton ID="lblbanch" CssClass="LinkbtnFont" runat="server" Text="" OnClick="lblbanch_Click"></asp:LinkButton>
                                                </h2>
                                                <span class="f-12 f-w-400"></span>
                                            </div>
                                            <div class="product-sub bg-warning-light">
                                                <svg class="invoice-icon">
                                                    <use href="../assets/svg/icon-sprite.svg#tick-circle"></use>
                                                </svg>
                                            </div>
                                        </div>
                                        <ul class="bubbles">
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xl-3 col-sm-6">
                                <div class="card o-hidden small-widget">
                                    <div class="card-body total-Complete border-b-secondary border-2">
                                        <span class="f-light f-w-500 f-14">Total Task Filled</span>
                                        <div class="project-details">
                                            <div class="project-counter">
                                                <h1 class="f-w-600">
                                                    <asp:LinkButton ID="lblTotalFilled" CssClass="LinkbtnFont" runat="server" Text="" OnClick="lblTotalFilled_Click"></asp:LinkButton>
                                                </h1>
                                                <span class="f-12 f-w-400">(<asp:Label runat="server" ID="lblPreviousdate"></asp:Label>) </span>
                                            </div>
                                            <div class="product-sub bg-secondary-light">
                                                <svg class="invoice-icon">
                                                    <use href="../assets/svg/icon-sprite.svg#add-square"></use>
                                                </svg>
                                            </div>
                                        </div>
                                        <ul class="bubbles">
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-sm-6">
                                <div class="card o-hidden small-widget">
                                    <div class="card-body total-upcoming">
                                        <span class="f-light f-w-500 f-14">Total Task Not Filled</span>
                                        <div class="project-details">
                                            <div class="project-counter">
                                                <h2 class="f-w-600">
                                                    <asp:LinkButton ID="lblTotalNotFilled" CssClass="LinkbtnFont" runat="server" Text="" OnClick="lblTotalNotFilled_Click"></asp:LinkButton></h2>
                                                <span class="f-12 f-w-400">(<asp:Label runat="server" ID="lblDate2"></asp:Label>) </span>
                                            </div>
                                            <div class="product-sub bg-light-light">
                                                <svg class="invoice-icon">
                                                    <use href="../assets/svg/icon-sprite.svg#spam"></use>
                                                </svg>
                                            </div>
                                        </div>
                                        <ul class="bubbles">
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-4 col-sm-6">
                                <div class="card o-hidden small-widget">
                                    <div class="card-body total-allocated">
                                        <span class="f-light f-w-500 f-14">Total Tasks Allocated</span>
                                        <div class="project-details">
                                            <div class="project-counter">
                                                <h2 class="f-w-600">
                                                    <asp:LinkButton ID="lblTotaltaskAllocated" CssClass="LinkbtnFont" runat="server" Text="" OnClick="lblTotaltaskAllocated_Click"></asp:LinkButton>
                                                </h2>
                                                <span class="f-12 f-w-400"></span>
                                            </div>
                                            <div class="product-sub bg-light-light1">
                                                <svg class="invoice-icon">
                                                    <use href="../assets/svg/icon-sprite.svg#profile-check"></use>
                                                </svg>
                                            </div>
                                        </div>
                                        <ul class="bubbles">
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-4 col-sm-6">
                                <div class="card o-hidden small-widget">
                                    <div class="card-body total-pendingleave">
                                        <span class="f-light f-w-500 f-14">Your Pending Leave Application</span>
                                        <div class="project-details">
                                            <div class="project-counter">
                                                <h2 class="f-w-600">
                                                    <asp:LinkButton ID="lblMyPendingLeave" CssClass="LinkbtnFont" runat="server" Text="0" OnClick="lblMyPendingLeave_Click"></asp:LinkButton></h2>
                                                <span class="f-12 f-w-400"></span>
                                            </div>
                                            <div class="product-sub bg-light-light2">
                                                <svg class="invoice-icon">
                                                    <use href="../assets/svg/icon-sprite.svg#clock"></use>
                                                </svg>
                                            </div>
                                        </div>
                                        <ul class="bubbles">
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-4 col-sm-6">
                                <div class="card o-hidden small-widget">
                                    <div class="card-body total-leaveapproval">
                                        <span class="f-light f-w-500 f-14">Pending Leave Applications For Approval</span>
                                        <div class="project-details">
                                            <div class="project-counter">
                                                <h2 class="f-w-600">
                                                    <asp:LinkButton ID="lblOtherPendingLeave" CssClass="LinkbtnFont" runat="server" Text="0" OnClick="lblOtherPendingLeave_Click"></asp:LinkButton>
                                                </h2>
                                                <span class="f-12 f-w-400"></span>
                                            </div>
                                            <div class="product-sub bg-light-light3">
                                                <svg class="invoice-icon">
                                                    <use href="../assets/svg/icon-sprite.svg#clock"></use>
                                                </svg>
                                            </div>
                                        </div>
                                        <ul class="bubbles">
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                            <li class="bubble"></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-4" style="display: none">
                                <div class="info-box">
                                    <a href="../mis/Daily_Task/Daily_Reporting.aspx" runat="server" id="a1">
                                        <span class="info-box-icon bg-green "><i class="fa fa-tasks" aria-hidden="true"></i></span>

                                        <div class="info-box-content">
                                            <span class="info-box-text">Daily Task</span>
                                            <%--<asp:Label ID="lblReportStatus" runat="server" class="info-box-number" Text=""></asp:Label>--%>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <div class="col-md-4" style="display: none">
                                <div class="info-box">

                                    <span class="info-box-icon bg-yellow"><i class="fa fa-calendar" aria-hidden="true"></i></span>

                                    <div class="info-box-content">
                                        <span class="info-box-text">Your Pending Leave Application </span>

                                    </div>

                                </div>
                            </div>
                            <div class="col-md-4" style="display: none">
                                <div class="info-box">
                                    <a href="../HR/HREmpLeaveRequests.aspx" runat="server" id="aPendingLeave">
                                        <span class="info-box-icon bg-red"><i class="fa fa-calendar-o" aria-hidden="true"></i></span>

                                        <div class="info-box-content">
                                            <span class="info-box-text"></span>
                                            <asp:Label ID="lbl2" runat="server" class="info-box-number" Text=""></asp:Label>
                                        </div>
                                    </a>
                                </div>
                            </div>



                        </div>

                        <div class="" runat="server" visible="false">
                            <div class="container">
                                <div class="row">
                                    <div class="col-md-3 col-sm-6">
                                        <div class="counter">
                                            <span class="counter-value">87</span>
                                            <h3>Web Designing</h3>
                                            <div class="counter-icon">
                                                <i class="fa fa-briefcase"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-6">
                                        <div class="counter magenta">
                                            <span class="counter-value">82</span>
                                            <h3>Web Development</h3>
                                            <div class="counter-icon">
                                                <i class="fa fa-globe"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-6">
                                        <div class="counter magenta">
                                            <span class="counter-value">82</span>
                                            <h3>Web Development</h3>
                                            <div class="counter-icon">
                                                <i class="fa fa-globe"></i>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-6">
                                        <div class="counter magenta">
                                            <span class="counter-value">82</span>
                                            <h3>Web Development</h3>
                                            <div class="counter-icon">
                                                <i class="fa fa-globe"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="display: none">

                            <div class="col-md-4">

                                <div class="row">


                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="schedule-warp">
                                                <div class="day-one">
                                                    Total Resources (Bench & Projects)
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="schedule-card">
                                                            <div class="insidebox">
                                                                <span class="schedule-tag">On    
                                                                        <br />
                                                                    Projects</span>
                                                                <span class="schedule-start"></span>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="schedule-card">
                                                            <div class="insidebox">
                                                                <span class="schedule-tag">On
                                                                        <br />
                                                                    Bench</span>
                                                                <span class="schedule-start"><i class="fa fa-coun"></i>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="schedule-warp">
                                                <div class="day-one">
                                                    Total Daily Tasks
                                                        

                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="schedule-card">
                                                            <div class="insidebox">
                                                                <span class="schedule-tag">Task
                <br />
                                                                    Filled</span>
                                                                <span class="schedule-start"></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="schedule-card">
                                                            <div class="insidebox">
                                                                <span class="schedule-tag">Task Not 
                                                                        <br />
                                                                    Filled</span>
                                                                <span class="schedule-start"><i class="fa fa-coun"></i>

                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">

                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="schedule-warp">
                                                <div class="day-one">
                                                    Total Tasks Allocated<asp:Label runat="server" ID="lbl"></asp:Label>

                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="schedule-card">
                                                            <div class="insidebox">
                                                                <span class="schedule-tag">Task
                <br />
                                                                    Allocated</span>
                                                                <span class="schedule-start"></span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">
                                <div class="card">
                                    <div class="card-header">
                                        <h4>Holiday</h4>
                                    </div>
                                    <div class="table-responsive custom-scrollbar">

                                        <asp:GridView ID="GridViewHoliday" runat="server" class="table table-bordered" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ControlStyle-CssClass="border-bottom-warning" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="HolidayDate" ControlStyle-CssClass="border-bottom-secondary" HeaderText="Date" />
                                                <asp:TemplateField HeaderText="Holiday" ControlStyle-CssClass="border-bottom-warning" ItemStyle-Width="45%">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblHoliday" Text='<%#Eval("HolidayName").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="card">
                                    <div class="card-header">
                                        <h4>
                                            <img src="assets_dashboard/cake.png" alt="scheme" style="width: 30px;">&nbsp; &nbsp; &nbsp;Birthdays</h4>
                                    </div>
                                    <div class="table-responsive custom-scrollbar">
                                        <asp:GridView ID="GridViewBirth" runat="server" class="table table-bordered" OnRowDataBound="GridViewBirth_RowDataBound" AutoGenerateColumns="False" DataKeyNames="Emp_ID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        <%-- <asp:Image runat="server" ID="ImgNew" Style="width: 40px; margin-left: 5px;" />--%>
                                                        <asp:Label runat="server" ID="lblBirthdate" Text='<%#Eval("Birthdate").ToString() %>' Visible="false"></asp:Label>
                                                        <asp:Label runat="server" ID="lblDOB_Day" Text='<%#Eval("DOB_Day").ToString() %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name" ItemStyle-Width="45%">
                                                    <ItemTemplate>
                                                        <img class="img-30 me-2"
                                                            src='<%# "../HR/UploadDoc/" + Eval("Emp_ProfileImage") %>'
                                                            alt="profile"
                                                            onerror='<%# "this.onerror=null;this.src=\"" + 
                                                                      (Eval("Emp_Gender").ToString() == "Male" 
                                                                          ? "../HR/ProfileImg/male.jpg" 
                                                                          : "../HR/ProfileImg/female.jpg") + "\";" %>' />

                                                        <asp:Label runat="server" ID="lblEmployeeName" Text='<%#Eval("EmployeeName").ToString() %>'></asp:Label>
                                                        <asp:Image runat="server" ID="ImgNew" Style="width: 40px; margin-left: 5px;" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--   <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />--%>
                                                <asp:BoundField DataField="EmpOffice" HeaderText="Office  Name" Visible="false" />
                                                <asp:BoundField DataField="Birthdate" HeaderText="Birth Date" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row" runat="server" id="divTask">
                            <div class="col-md-12">
                                <div class="card">
                                    <div class="card-header">
                                        <h4>Task</h4>

                                    </div>
                                    <div class="card-body">
                                        <div class="table-responsive custom-scrollbar">
                                            <asp:GridView runat="server" AutoGenerateColumns="false" ID="gridvew1"
                                                CssClass="table table-bordered table-hover" OnRowCommand="gridvew1_RowCommand">

                                                <Columns>

                                                    <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="13">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="STATUS">
                                                        <ItemTemplate>
                                                            <%-- <asp:LinkButton ID="lnkViewDetails" CommandName="ViewDetails" ToolTip="View"
                                                    CommandArgument='<%# Eval("Task_Id") %>' runat="server" CssClass="btn btn-sm btn-success" ><i class="fa fa-eye"></i></asp:LinkButton> "Not Filled"--%>
                                                            <asp:LinkButton ID="lnkViewDetails" CommandName="ViewDetails" ToolTip="View"
                                                                CommandArgument='<%# Eval("Task_Id") %>' runat="server"
                                                                CssClass='<%#Eval("Task_StatusClass").ToString() %>'
                                                                Text='<%# Eval("Task_Status").ToString()  %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DATE">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" CssClass='<%#Eval("Task_Id").ToString() == "0"? "text-danger" : "text-black" %>' Text='<%#Eval("Task_date").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DAY">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" CssClass='<%#Eval("Task_Id").ToString() == "0"? "text-danger" : "text-black" %>' Text='<%#Eval("Day_Name").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" CssClass='<%#Eval("Task_Id").ToString() == "0"? "text-danger" : "text-black" %>' Text='<%#Eval("Emp_Name").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TOTAL WORKING HOURS">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" CssClass='<%#Eval("Task_Id").ToString() == "0"? "text-danger" : "text-black" %>' Text='<%#Eval("Total_Hourse").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TASK SUBMIT ON">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" CssClass='<%#Eval("Task_Id").ToString() == "0"? "text-danger" : "text-black" %>' Text='<%#Eval("Task_Submit_On").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>No record found</EmptyDataTemplate>
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
    <div id="OnProjectModal" class="modal fade bd-example-modal-xl" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel">Total Resources on Project</h4>
                    <button class="btn-close py-0" type="button" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body dark-modal">
                    <div class="">

                        <asp:GridView ID="GridOnProject" class="table  table-bordered  table-hover" runat="server" PageSize="10" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("MainPowerId").ToString() %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Emp_Name" HeaderText="Employee" />
                                <asp:BoundField DataField="Designation_Name" HeaderText="Designation" />
                                <asp:BoundField DataField="Manager" HeaderText="MANAGER" />
                                <asp:BoundField DataField="ProjectName" HeaderText="PROJECT" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="OnBenchModal" class="modal fade bd-example-modal-xl" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="exampleModalLongTitle">Total Resources on Bench</h4>
                    <button class="btn-close py-0" type="button" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body dark-modal">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">

                                <asp:GridView ID="GridonBench" class="datatable  table table-hover table-bordered pagination-ys" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("MainPowerId").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Designation_Name" HeaderText="Designation / Role" />
                                        <asp:BoundField DataField="BenchFromDate" HeaderText="BENCH FROM DATE" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="TaskFilledModal" class="modal fade bd-example-modal-xl" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="TaskFilledModalLongTitle">Total Task Filled</h4>
                    <button class="btn-close py-0" type="button" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body dark-modal">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">

                                <asp:GridView ID="GridTaskFilled" class="datatable  table table-hover table-bordered pagination-ys" runat="server" PageSize="10" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="ProjectName" HeaderText="Project Name" />
                                        <asp:BoundField DataField="TaskName" HeaderText="Task" />
                                        <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                                        <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="TaskNotFilledModal" class="modal fade bd-example-modal-xl" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="TaskNotFilledModalLongTitle">Total Task Not Filled</h4>
                    <button class="btn-close py-0" type="button" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body dark-modal">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">

                                <asp:GridView ID="GridTaskNotFilled" class="datatable  table table-hover table-bordered pagination-ys" runat="server" PageSize="10" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="ProjectName" HeaderText="Project Name" />
                                        <asp:BoundField DataField="TaskName" HeaderText="Task" />
                                        <asp:BoundField DataField="FromDate" HeaderText="From Date" />
                                        <asp:BoundField DataField="ToDate" HeaderText="To Date" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    

    <div class="modal fade" id="ViewDetails" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" style="color: blue">TASK STATUS :-</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView runat="server" ID="gridEmpTask" CssClass="table table-bordered"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                <asp:Image runat="server" ID="ImgNew" Style="width: 40px; margin-left: 5px;" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PROJECT NAME" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%#Eval("Project_Name").ToString() %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WORK CATEGORY" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%#Eval("WorkCategory").ToString() %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="WORK DESCRIPTION " HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="EffectiveDat" Text='<%#Eval("Work_Description").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TOTAL WORKING HOURS" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="PurchaseRat" Text='<%#Eval("Total_Hourse").ToString() %>'></asp:Label>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                                </asp:GridView>
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
    <script>
        // OnBenchModal Initialization
        $('#OnBenchModal').on('shown.bs.modal', function () {
            var tableId = '<%= GridonBench.ClientID %>';
            var $table = $('#' + tableId);

            if (!$.fn.DataTable.isDataTable($table)) {
                var t1 = $table.DataTable({
                    paging: true,
                    columnDefs: [{
                        targets: 'no-sort',
                        orderable: false
                    }],
                    order: [[0, 'asc']],
                    dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
                        '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
                        '<"row"<"col-sm-5"i><"col-sm-7"p>>',
                    fixedHeader: {
                        header: true
                    },
                    buttons: {
                        buttons: [{
                            extend: 'print',
                            text: '<i class="fa fa-print"></i> Print',
                            title: $('h1').text(),
                            exportOptions: {
                                columns: ':not(.no-print)'
                            },
                            footer: true,
                            autoPrint: true
                        }, {
                            extend: 'excel',
                            text: '<i class="fa fa-file-excel-o"></i> Excel',
                            title: $('h1').text(),
                            exportOptions: {
                                columns: ':not(.no-print)'
                            },
                            footer: true
                        }],
                        dom: {
                            container: {
                                className: 'dt-buttons'
                            },
                            button: {
                                className: 'btn btn-default'
                            }
                        }
                    }
                });

                t1.on('order.dt search.dt', function () {
                    t1.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = i + 1;
                    });
                }).draw();
            }
        });

        // OnProjectModal Initialization
        $('#OnProjectModal').on('shown.bs.modal', function () {
            var tableId = '<%= GridOnProject.ClientID %>';
            var $table = $('#' + tableId);

            if (!$.fn.DataTable.isDataTable($table)) {
                var t2 = $table.DataTable({
                    paging: true,
                    columnDefs: [{
                        targets: 'no-sort',
                        orderable: false
                    }],
                    order: [[0, 'asc']],
                    dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
                        '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
                        '<"row"<"col-sm-5"i><"col-sm-7"p>>',
                    fixedHeader: {
                        header: true
                    },
                    buttons: {
                        buttons: [{
                            extend: 'print',
                            text: '<i class="fa fa-print"></i> Print',
                            title: $('h1').text(),
                            exportOptions: {
                                columns: ':not(.no-print)'
                            },
                            footer: true,
                            autoPrint: true
                        }, {
                            extend: 'excel',
                            text: '<i class="fa fa-file-excel-o"></i> Excel',
                            title: $('h1').text(),
                            exportOptions: {
                                columns: ':not(.no-print)'
                            },
                            footer: true
                        }],
                        dom: {
                            container: {
                                className: 'dt-buttons'
                            },
                            button: {
                                className: 'btn btn-default'
                            }
                        }
                    }
                });

                t2.on('order.dt search.dt', function () {
                    t2.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = i + 1;
                    });
                }).draw();
            }
        });
        // TaskFilledModal Initialization
        $('#TaskFilledModal').on('shown.bs.modal', function () {
            var tableId = '<%= GridTaskFilled.ClientID %>';
            var $table = $('#' + tableId);

            if (!$.fn.DataTable.isDataTable($table)) {
                var t2 = $table.DataTable({
                    paging: true,
                    columnDefs: [{
                        targets: 'no-sort',
                        orderable: false
                    }],
                    order: [[0, 'asc']],
                    dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
                        '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
                        '<"row"<"col-sm-5"i><"col-sm-7"p>>',
                    fixedHeader: {
                        header: true
                    },
                    buttons: {
                        buttons: [{
                            extend: 'print',
                            text: '<i class="fa fa-print"></i> Print',
                            title: $('h1').text(),
                            exportOptions: {
                                columns: ':not(.no-print)'
                            },
                            footer: true,
                            autoPrint: true
                        }, {
                            extend: 'excel',
                            text: '<i class="fa fa-file-excel-o"></i> Excel',
                            title: $('h1').text(),
                            exportOptions: {
                                columns: ':not(.no-print)'
                            },
                            footer: true
                        }],
                        dom: {
                            container: {
                                className: 'dt-buttons'
                            },
                            button: {
                                className: 'btn btn-default'
                            }
                        }
                    }
                });

                t2.on('order.dt search.dt', function () {
                    t2.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = i + 1;
                    });
                }).draw();
            }
        });
        // TaskNotFilledModal Initialization
        $('#TaskNotFilledModal').on('shown.bs.modal', function () {
            var tableId = '<%= GridTaskNotFilled.ClientID %>';
            var $table = $('#' + tableId);

            if (!$.fn.DataTable.isDataTable($table)) {
                var t2 = $table.DataTable({
                    paging: true,
                    columnDefs: [{
                        targets: 'no-sort',
                        orderable: false
                    }],
                    order: [[0, 'asc']],
                    dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
                        '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
                        '<"row"<"col-sm-5"i><"col-sm-7"p>>',
                    fixedHeader: {
                        header: true
                    },
                    buttons: {
                        buttons: [{
                            extend: 'print',
                            text: '<i class="fa fa-print"></i> Print',
                            title: $('h1').text(),
                            exportOptions: {
                                columns: ':not(.no-print)'
                            },
                            footer: true,
                            autoPrint: true
                        }, {
                            extend: 'excel',
                            text: '<i class="fa fa-file-excel-o"></i> Excel',
                            title: $('h1').text(),
                            exportOptions: {
                                columns: ':not(.no-print)'
                            },
                            footer: true
                        }],
                        dom: {
                            container: {
                                className: 'dt-buttons'
                            },
                            button: {
                                className: 'btn btn-default'
                            }
                        }
                    }
                });

                t2.on('order.dt search.dt', function () {
                    t2.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                        cell.innerHTML = i + 1;
                    });
                }).draw();
            }
        });
    </script>


    <%--<script type="text/javascript" src="https://code.jquery.com/jquery-1.12.0.min.js"></script>--%>
    <script>


        $(document).ready(function () {
            $('.counter-value').each(function () {
                $(this).prop('Counter', 0).animate({
                    Counter: $(this).text()
                }, {
                    duration: 1000,
                    easing: 'swing',
                    step: function (now) {
                        $(this).text(Math.ceil(now));
                    }
                });
            });
        });

        $(window).on('load', function () {
            $(".bounce-card")
                .addClass("animate__animated animate__bounce")
                .one("animationend", function () {
                    // Optional: remove animation class after it ends
                    $(this).removeClass("animate__animated animate__bounce");
                });
        });



    </script>
</asp:Content>

