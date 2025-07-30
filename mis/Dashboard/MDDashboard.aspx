<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MDDashboard.aspx.cs" Inherits="mis_Dashboard_MDDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="assets/css/Dashboard.css" rel="stylesheet" />
    <style>
        .box {
            min-height: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">



    <div class="content-wrapper">

        <section class="content-header">
            <h1>Dashboard
        
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Dashboard</li>
            </ol>
        </section>

        <section class="content">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <div class="schedule-warp">
                            <div class="day-one">
                                Today's Meeting :
                                                <asp:Label ID="lblToDay" runat="server" Text=""></asp:Label>
                            </div>
                            <asp:Repeater ID="RepeaterTodayMeeting" runat="server">
                                <ItemTemplate>
                                    <div class="schedule-card">
                                        <div class="insidebox">
                                            <span class="schedule-tag"><%# Eval("Meeting_StartTime").ToString()%></span>
                                            <span class="schedule-start"><%# Eval("Meeting_Subject").ToString()%></span>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <div class="schedule-warp">
                            <div class="day-one">
                                MileStone :
                                <asp:Label ID="lblTomorrow" runat="server" Text=""></asp:Label>
                            </div>
                            <asp:Repeater ID="RepeaterTomorrowMeeting" runat="server">
                                <ItemTemplate>
                                    <div class="schedule-card">
                                        <div class="insidebox">
                                            <span class="schedule-tag"><%# Eval("TLDate").ToString()%></span>
                                            <span class="schedule-start"><%# Eval("ImportantSubject").ToString()%></span>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="info-box">
                        <a href="../../mis/HR/HREmpList.aspx" target="_blank">
                            <span class="info-box-icon bg-aqua"><i class="fa fa-users" aria-hidden="true"></i></span>
                            <div class="info-box-content">
                                <span class="info-box-text">TOTAL Employee</span>
                                <asp:Label ID="lblTotalEmp" runat="server" class="info-box-number" Text=""></asp:Label>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="info-box">
                        <a href="../../mis/HR/HREmpPresentList.aspx" target="_blank">
                            <span class="info-box-icon bg-green"><i class="fa fa-user-circle-o" aria-hidden="true"></i></span>
                            <div class="info-box-content">
                                <span class="info-box-text">PRESENT EMPLOYEE</span>
                                <asp:Label ID="lblTotalPresent" runat="server" class="info-box-number" Text=""></asp:Label>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="info-box">
                        <a href="../../mis/HR/HREmpTodayOnLeave.aspx" target="_blank">
                            <span class="info-box-icon bg-yellow"><i class="fa fa-user" aria-hidden="true"></i></span>

                            <div class="info-box-content">
                                <span class="info-box-text">EMPLOYEE ON LEAVE</span>
                                <asp:Label ID="lblTotalOnLeave" runat="server" class="info-box-number" Text=""></asp:Label>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="info-box">
                        <a href="../../mis/HR/HREmpAbsentList.aspx" target="_blank">
                            <span class="info-box-icon bg-red"><i class="fa fa-user" aria-hidden="true"></i></span>

                            <div class="info-box-content">
                                <span class="info-box-text">ABSENT EMPLOYEE</span>
                                <asp:Label ID="lblTotalAbsent" runat="server" class="info-box-number" Text=""></asp:Label>
                            </div>
                        </a>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="info-box">
                        <a href="../../mis/Legal/HearingList.aspx" target="_blank">
                            <span class="info-box-icon bg-orange"><i class="fa fa-balance-scale" aria-hidden="true"></i></span>


                            <div class="info-box-content">
                                <span class="info-box-text">ACTIVE LEGAL CASES</span>
                                <asp:Label ID="lblLegalCount" runat="server" class="info-box-number" Text=""></asp:Label>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="info-box">
                        <a href="../../mis/HR/HREnquiry_stList.aspx" target="_blank">
                            <span class="info-box-icon bg-red"><i class="fa  fa-search" aria-hidden="true"></i></span>
                            <div class="info-box-content">
                                <span class="info-box-text">OPENED DEPT. ENQ.</span>
                                <asp:Label ID="lblDepEnq" runat="server" class="info-box-number hidden" Text=""></asp:Label>
                                <asp:Label ID="Label2" runat="server" class="info-box-number" Text="8"></asp:Label>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="info-box">
                        <a href="../../mis/RTI/PendingRequestedRTI .aspx" target="_blank">
                            <span class="info-box-icon bg-blue"><i class="fa fa-envelope-o" aria-hidden="true"></i></span>
                            <div class="info-box-content">
                                <span class="info-box-text">PENDING RTI</span>
                                <asp:Label ID="lblPendingRTI" runat="server" class="info-box-number" Text=""></asp:Label>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="info-box">
                        <a href="../../mis/RTI/PendingRequestedFirstAppeal.aspx" target="_blank">
                            <span class="info-box-icon bg-blue"><i class="fa fa-envelope-o" aria-hidden="true"></i></span>
                            <div class="info-box-content">
                                <span class="info-box-text">PENDING FIRST APPEAL</span>
                                <asp:Label ID="lblPendingFirstAppeal" runat="server" class="info-box-number" Text=""></asp:Label>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="info-box">
                        <a href="../../mis/Grievance/ViewPendingGrievance_Details.aspx" target="_blank">
                            <span class="info-box-icon bg-yellow"><i class="fa fa-comments" aria-hidden="true"></i></span>

                            <div class="info-box-content">
                                <span class="info-box-text">PENDING GRIEVANCE</span>
                                <asp:Label ID="lblPendingGrvCount" runat="server" class="info-box-number" Text=""></asp:Label>

                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="info-box">
                        <a href="../../mis/filetracking/FTMyDeskFileList.aspx" target="_blank">
                            <span class="info-box-icon bg-yellow"><i class="ion ion-clipboard" aria-hidden="true"></i></span>

                            <div class="info-box-content">
                                <span class="info-box-text">Files on My Desk</span>
                                <asp:Label ID="lblFileOnDes" runat="server" class="info-box-number" Text=""></asp:Label>

                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <div class="info-box">
                        <a href="../../mis/HR/HREmpBirthdayList.aspx" target="_blank">
                            <span class="info-box-icon bg-orange"><i class="fa fa-birthday-cake" aria-hidden="true"></i></span>
                            <div class="info-box-content">
                                <span class="info-box-text">Total Birthday</span>
                                <asp:Label ID="lblBirthday" runat="server" class="info-box-number" Text="10"></asp:Label>
                            </div>
                        </a>
                    </div>
                </div>
                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="info-box bg-yellow">
                        <span class="info-box-icon"><i class="ion ion-ios-pricetag-outline"></i></span>

                        <div class="info-box-content">
                            <span class="info-box-text">HR & Payroll</span>
                            <span class="info-box-number">...</span>

                            <div class="progress">
                                <div class="progress-bar" style="width: 50%"></div>
                            </div>
                            <span class="progress-description"><a href="../../mis/HR/HRHomeDashboard.aspx" target="_blank" style="color: white;">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </span>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                </div>


            </div>

            <div class="row">
                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="info-box bg-yellow">
                        <span class="info-box-icon"><i class="ion ion-ios-pricetag-outline"></i></span>

                        <div class="info-box-content">
                            <span class="info-box-text">Finance & Inventry</span>
                            <span class="info-box-number">...</span>

                            <div class="progress">
                                <div class="progress-bar" style="width: 50%"></div>
                            </div>
                            <span class="progress-description"><a href="../../mis/Finance/Home.aspx" target="_blank" style="color: white;">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </span>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                </div>
                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="info-box bg-yellow">
                        <span class="info-box-icon"><i class="ion ion-ios-pricetag-outline"></i></span>

                        <div class="info-box-content">
                            <span class="info-box-text">Total Sales</span>
                            <span class="info-box-number"><i class="fa fa-inr" aria-hidden="true"></i>222.20 Lacs</span>

                            <div class="progress">
                                <div class="progress-bar" style="width: 50%"></div>
                            </div>
                            <span class="progress-description"><a href="../../mis/Admin/MISReport.aspx" target="_blank" style="color: white;">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </span>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                </div>
                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="info-box bg-yellow">
                        <span class="info-box-icon"><i class="ion ion-ios-pricetag-outline"></i></span>

                        <div class="info-box-content">
                            <span class="info-box-text">Total Purchase</span>
                            <span class="info-box-number">...</span>

                            <div class="progress">
                                <div class="progress-bar" style="width: 50%"></div>
                            </div>
                            <span class="progress-description"><a href="#" style="color: white;">More info <i class="fa fa-arrow-circle-right"></i></a>
                            </span>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                </div>
                <div class="col-lg-3 col-xs-6">
                    <!-- small box -->
                    <div class="info-box bg-yellow">
                        <span class="info-box-icon"><i class="ion ion-ios-pricetag-outline"></i></span>

                        <div class="info-box-content">
                            <span class="info-box-text">FDR</span>
                            <span class="info-box-number">...</span>

                            <div class="progress">
                                <div class="progress-bar" style="width: 50%"></div>
                            </div>
                            <span class="progress-description"><a href="#" style="color: white;" id="myModal" data-toggle="modal" data-target="#myModa32">More info <i class="fa fa-arrow-circle-right"></i></a>

                            </span>
                        </div>
                        <!-- /.info-box-content -->
                    </div>
                    <div class="modal fade in" id="myModa32" role="dialog">
                        <div class="modal-dialog">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">×</button>
                                    <h4 class="modal-title">FDR</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="table-responsive">

                                        <table class="table table-hover">
                                            <thead>
                                                <tr>
                                                    <th scope="col">S.No </th>
                                                    <th scope="col">FDR No</th>
                                                    <th scope="col">Bank Name</th>
                                                    <th scope="col">Amount</th>
                                                    <th scope="col">FDR Date</th>
                                                    <th scope="col">Period</th>

                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>

                                                    <td>1</td>
                                                    <td>12121212</td>
                                                    <td>SBI</td>
                                                    <td>455555</td>
                                                    <td>27/09/2018</td>
                                                    <td>wqewqe</td>

                                                </tr>

                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>


            </div>

            <div class="row">
                <div class="col-md-6">
                    <div class="box">
                        <div class="box-header with-border">
                            <i class="ion ion-clipboard"></i>
                            <h3 class="box-title">RTE SUPPLY</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <table class="table table-hover">
                                <thead>
                                    <tr>
                                        <th scope="col">S. No.</th>
                                        <th scope="col">Product</th>
                                        <th scope="col">Total In Current</th>
                                        <th scope="col">Last Day</th>
                                        <th>View More</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>

                                        <td>1</td>
                                        <td>Wheat Soya Barfi</td>
                                        <td>208.986 M. Ton</td>
                                        <td>50 KG</td>
                                        <td><a href="#" id="s" data-toggle="modal" data-target="#myModaProduction">View </a></td>
                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td>Atta Besan Laddo </td>
                                        <td>208.986 M. Ton</td>
                                        <td>100 KG</td>
                                        <td><a href="#" id="d" data-toggle="modal" data-target="#myModaProduction">View </a></td>
                                    </tr>
                                    <tr>
                                        <td>3</td>
                                        <td>Halwa </td>
                                        <td>394.176 M. Ton</td>
                                        <td>96 KG</td>
                                        <td><a href="#" id="h" data-toggle="modal" data-target="#myModaProduction">View </a></td>
                                    </tr>
                                    <tr>
                                        <td>4</td>
                                        <td>Bal Aahar </td>
                                        <td>197.088 M. Ton</td>
                                        <td>96 KG</td>
                                        <td><a href="#" id="h" data-toggle="modal" data-target="#myModaProduction">View</a></td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box">
                        <div class="box-header with-border">
                            <i class="ion ion-clipboard"></i>
                            <h3 class="box-title">BFP Indrapuri SUPPLY</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <table class="table table-hover">
                                <thead>
                                    <tr>
                                        <th scope="col">S. No.</th>
                                        <th scope="col">Product</th>
                                        <th scope="col">Total In Current</th>
                                        <th scope="col">Last Day</th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>

                                        <td>1</td>
                                        <td>Phosphate Solubilizing Bacteria(PSB)</td>
                                        <td>115000 Pkt.</td>
                                        <td>800 Pkt</td>

                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td>Rhizobium (SP) </td>
                                        <td>115000 Pkt.</td>
                                        <td>760 Pkt</td>

                                    </tr>
                                    <tr>
                                        <td>3</td>
                                        <td>Azotobacter </td>
                                        <td>30000 Pkt.</td>
                                        <td>100 Pkt</td>

                                    </tr>


                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="box">
                        <div class="box-header with-border">
                            <i class="ion ion-clipboard"></i>
                            <h3 class="box-title">MAF Babai SUPPLY</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <table class="table table-hover">
                                <thead>
                                    <tr>
                                        <th scope="col">S. No.</th>
                                        <th scope="col">Product</th>
                                        <th scope="col">FY Year</th>
                                        <th scope="col">Production in Quintal</th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>

                                        <td>1</td>
                                        <td>Wheat</td>
                                        <td>2017-18</td>
                                        <td>2926.80</td>

                                    </tr>
                                    <tr>
                                        <td>2</td>
                                        <td>Dhan </td>
                                        <td>2017-18</td>
                                        <td>230.50</td>

                                    </tr>



                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-default" style="position: relative; left: 0px; top: 0px;">
                        <div class="box-header ui-sortable-handle with-border" style="cursor: move;">
                            <i class="ion ion-clipboard"></i>

                            <h3 class="box-title">IMPORTANT LINKS</h3>


                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <!-- See dist/js/pages/dashboard.js to activate the todoList plugin -->
                            <ul class="todo-list ui-sortable">
                                <li>
                                    <!-- drag handle -->
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-ellipsis-v"></i>
                                        <i class="fa fa-ellipsis-v"></i>
                                    </span>
                                    <!-- checkbox -->

                                    <!-- todo text -->
                                    <span class="text"><a href="../../mis/admin/AdminLiveTender.aspx" target="_blank">Live Tenders's</a></span>
                                    <!-- Emphasis label -->
                                    <!-- General tools such as edit or delete-->
                                    <div class="tools">
                                        <i class="fa fa-edit"></i>
                                        <i class="fa fa-trash-o"></i>
                                    </div>
                                </li>
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-ellipsis-v"></i>
                                        <i class="fa fa-ellipsis-v"></i>
                                    </span>

                                    <span class="text"><a href="../../mis/admin/AdminCircular.aspx" target="_blank">IMP Circulars</a></span>

                                    <div class="tools">
                                        <i class="fa fa-edit"></i>
                                        <i class="fa fa-trash-o"></i>
                                    </div>
                                </li>
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-ellipsis-v"></i>
                                        <i class="fa fa-ellipsis-v"></i>
                                    </span>

                                    <span class="text"><a href="../../mis/admin/AdminBoardMeeting.aspx" target="_blank">Board Meeting</a></span>

                                    <div class="tools">
                                        <i class="fa fa-edit"></i>
                                        <i class="fa fa-trash-o"></i>
                                    </div>
                                </li>


                            </ul>
                        </div>

                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <i class="ion ion-clipboard"></i>
                            <h3 class="box-title">UPCOMING RETIREMENTS</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <asp:GridView ID="GridViewRetirement" runat="server" class="table table-bordered table-dark table-hover" AutoGenerateColumns="False" DataKeyNames="Emp_ID">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="Office_Name" HeaderText="Office  Name" />
                                    <asp:BoundField DataField="Emp_RetirementDate" HeaderText="Emp_RetirementDate" />
                                </Columns>
                            </asp:GridView>

                        </div>

                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="box">
                        <div class="box-header with-border">
                            <h3 class="box-title"><i class="ion ion-clipboard"></i>DIRECTORY</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <asp:GridView ID="GridViewDirectory" runat="server" class="table table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="Emp_ID">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                    <%--<asp:BoundField DataField="Office_Name" HeaderText="Office Name" />--%>
                                    <asp:BoundField DataField="Emp_MobileNo" HeaderText="Mobile No" />
                                    <asp:BoundField DataField="Emp_Email" HeaderText="Email" />

                                </Columns>
                            </asp:GridView>

                        </div>

                    </div>
                </div>
            </div>

            <div class="modal fade in" id="myModaProduction" role="dialog">
                <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">×</button>
                            <h4 class="modal-title">RTE SUPPLY</h4>
                        </div>
                        <div class="modal-body">
                            <div class="table-responsive">

                                <table class="table table-hover">
                                    <thead>
                                        <tr>
                                            <th scope="col">S.No </th>
                                            <th scope="col">District</th>
                                            <th scope="col">Qty. in Packets</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>

                                            <td>1</td>
                                            <td>Bhopal</td>
                                            <td>73822 Pkt.</td>
                                        </tr>
                                        <tr>

                                            <td>2</td>
                                            <td>Raisen</td>
                                            <td>47804 Pkt.</td>
                                        </tr>
                                        <tr>

                                            <td>3</td>
                                            <td>Rajgarh</td>
                                            <td>57704 Pkt.</td>
                                        </tr>
                                        <tr>
                                            <td>4</td>
                                            <td>Sehor</td>
                                            <td>51936 Pkt.</td>
                                        </tr>

                                    </tbody>
                                </table>
                            </div>
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
    <script src="assets/js/Dashboard.js"></script>
    <script>
        $(document).ready(function () {
            $("#myModa32").click(function () {

            });
        });
    </script>
</asp:Content>
