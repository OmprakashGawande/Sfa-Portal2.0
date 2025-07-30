<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MainDashboard.aspx.cs" Inherits="MainDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-header ui-sortable-handle" style="cursor: move;">
                            <i class="fa fa-users"></i>

                            <h3 class="box-title">Head Office ERP Users</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <!-- See dist/js/pages/dashboard.js to activate the todoList plugin -->
                            <ul class="todo-list ui-sortable">
                                <li>
                                    <!-- drag handle -->
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    <span class="text"><a href="MainDashboard.aspx?User=1">Chairman</a></span>
                                </li>
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    <span class="text"><a href="MainDashboard.aspx?User=2">Managing Director (MD)</a></span>
                                </li>
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    <span class="text">Head of the Depratment (HOD)</span>
                                    <ul>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=3">HOD Admin</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=4">HOD Accounts</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=5">HOD HRD</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=6">HOD Business</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=7">HOD Projects</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=8">HOD Fertilizers</a></span></li>
                                    </ul>

                                </li>
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    <span class="text">District General Manager (DGM)</span>
                                    <ul>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=9">DGM Admin</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=10">DGM Accounts</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=11">DGM HRD</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=12">DGM Business</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=13">DGM Projects</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=14">DGM Fertilizers</a></span></li>
                                    </ul>
                                </li>
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    <span class="text">Managers</span>
                                    <ul>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=15">Manager Admin</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=16">Manager Accounts</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=17">Manager HRD</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=18">Manager Business</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=19">Manager Projects</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=20">Manager Fertilizers</a></span></li>
                                    </ul>
                                </li>
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    <span class="text">Asst. Managers</span>
                                    <ul>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=21">Asst. Manager Admin</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=22">Asst. Manager Accounts</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=23">Asst. Manager HRD</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=24">Asst. Manager Business</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=25">Asst. Manager Projects</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=26">Asst. Manager Fertilizers</a></span></li>
                                    </ul>
                                </li>
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    <span class="text">Employees</span>
                                    <ul>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=27">Admin Employee</a></span>
                                            <ul>
                                                <li><span class="text"><a href="MainDashboard.aspx?User=28">Operator</a></span></li>
                                            </ul>
                                        </li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=29">Accounts Employee</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=30">HRD Employee</a></span>
                                            <ul>
                                                <li><span class="text"><a href="MainDashboard.aspx?User=31">Operator</a></span></li>
                                            </ul>
                                        </li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=32">Business Employee</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=33">Projects Employee</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=34">Fertilizers Employee</a></span>
                                            <ul>
                                                <li><span class="text"><a href="MainDashboard.aspx?User=35">Operator</a></span></li>
                                            </ul>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-header ui-sortable-handle" style="cursor: move;">
                            <i class="fa fa-users"></i>

                            <h3 class="box-title">Regional Officers ERP Users</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <!-- See dist/js/pages/dashboard.js to activate the todoList plugin -->
                            <ul class="todo-list ui-sortable">
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    <span class="text"><a href="MainDashboard.aspx?User=36">Regional Manager</a></span>
                                </li>
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    <span class="text">Assistant Manager</span>
                                    <ul>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=37">Assistant</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=38">Assistant Accounts</a></span></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="box box-success">
                        <div class="box-header ui-sortable-handle" style="cursor: move;">
                            <i class="fa fa-users"></i>
                            <h3 class="box-title">District Office ERP Users</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <!-- See dist/js/pages/dashboard.js to activate the todoList plugin -->
                            <ul class="todo-list ui-sortable">
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    <span class="text"><a href="MainDashboard.aspx?User=39">District Manager</a></span>
                                </li>
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    <span class="text">Assistant Manager</span>
                                    <ul>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=40">Assistant</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=41">Assistant Accounts</a></span></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="box box-success">
                        <div class="box-header ui-sortable-handle" style="cursor: move;">
                            <i class="fa fa-users"></i>

                            <h3 class="box-title">Sales Center ERP Users</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <!-- See dist/js/pages/dashboard.js to activate the todoList plugin -->
                            <ul class="todo-list ui-sortable">
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    <span class="text"><a href="MainDashboard.aspx?User=42">Sales Center Incharge</a></span>
                                </li>
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    <span class="text">Assistant Incharge</span>
                                    <ul>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=43">Assistant</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=44">Assistant Accounts</a></span></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="box box-success">
                        <div class="box-header ui-sortable-handle" style="cursor: move;">
                            <i class="fa fa-users"></i>

                            <h3 class="box-title">Production Unit ERP Users</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <!-- See dist/js/pages/dashboard.js to activate the todoList plugin -->
                            <ul class="todo-list ui-sortable">
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    <span class="text"><a href="MainDashboard.aspx?User=45">Production Unit Incharge</a></span>
                                </li>
                                <li>
                                    <span class="handle ui-sortable-handle">
                                        <i class="fa fa-user"></i>
                                    </span>
                                    <span class="text">Assistant Incharge</span>
                                    <ul>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=46">Assistant</a></span></li>
                                        <li><span class="text"><a href="MainDashboard.aspx?User=47">Assistant Accounts</a></span></li>
                                    </ul>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>

            <!-- /.box -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

