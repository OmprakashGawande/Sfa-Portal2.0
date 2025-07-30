<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="mis_Finance_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper" style="min-height: 414px;">
        <section class="content">
            <img src="../image/download.png" style="width: 75px;" />
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <%--<div class="box-header ui-sortable-handle" style="cursor: move;">
                            <h3 class="box-title"></h3>
                        </div>--%>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <!-- See dist/js/pages/dashboard.js to activate the todoList plugin -->
                            <div class="row">
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>Master Pages</legend>
                                        <ul class="todo-list ui-sortable">
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HRClass.aspx" target="_blank">Class Master</a></span>
                                            </li>

                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/AdminDesignation.aspx" target="_blank">Designation Master</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HRPayScale.aspx" target="_blank">Pay Scale Master</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HRGradePay.aspx" target="_blank">Grade Pay Master</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/AdminDepartment.aspx" target="_blank">Department Master</a></span>
                                            </li>

                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HRLeaveType.aspx" target="_blank">Leave Type Master</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HRHoliday.aspx" target="_blank">Holiday Calendar Master</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HRLeave_Master.aspx" target="_blank">Set Year Wise Leave</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/Payroll/PayrollEarn_DedMaster.aspx" target="_blank">Earning & Deduction Heads</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/Payroll/PayrollEarn_DeductionMaster.aspx" target="_blank">Active Earning & Deduction Heads</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HRPostMaster.aspx" target="_blank">Sanction Post Master</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HRProjectMaster.aspx" target="_blank">Project Master</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HRLevelMaster.aspx" target="_blank">Level Master</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HRLevelPayMaster.aspx" target="_blank">Level Pay Master</a></span>
                                            </li>
                                             <%--<li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/Payroll/PayrollAccountInfo.aspx" target="_blank">Account Information</a></span>
                                            </li>--%>
                                        </ul>
                                    </fieldset>

                                </div>
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>Transaction Pages</legend>
                                        <ul class="todo-list ui-sortable">
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpReg.aspx" target="_blank">Employee Registration</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/Payroll/PayrollAccountInfo.aspx" target="_blank">Employee Account Information</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HROverAllEmpList.aspx" target="_blank">Employee Details</a></span>
                                            </li>
                                             <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpList.aspx" target="_blank">Employee List</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpDetail.aspx" target="_blank">Edit Employee Details</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/Payroll/PayrollPolicyDetail.aspx" target="_blank">Employee Wise Policy Details</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/Payroll/PayrollEarnDeductionDetail.aspx" target="_blank">Employee Wise Earning & Deduction</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/Payroll/PayrollEmpSetAttendance.aspx" target="_blank">Set Attendance</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/Payroll/PayrollEmpSalaryDetails.aspx" target="_blank">Salary Details</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/Payroll/PayrollArrearDetail.aspx" target="_blank">Arrear Details</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpTransfer.aspx" target="_blank">Employee Tranfer</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpTransferPending.aspx" target="_blank">Employee Tranfer Pending List</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpIncrement.aspx" target="_blank">Employee Increment</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpIncreamentPending.aspx" target="_blank">Employee Increment Pending List</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpTimePayScale.aspx" target="_blank">Employee Time PayScale</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpTimePayScalePending.aspx" target="_blank">Employee Time PayScale Pending List</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpPromotion.aspx" target="_blank">Employee Promotion</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpPromotionPending.aspx" target="_blank">Employee Promotion Pending List</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpACR.aspx" target="_blank">Employee ACR Detail</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpACR_Report.aspx" target="_blank">Employees ACR Report</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpRetirement.aspx" target="_blank">Employee Retirement Detail</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpRetirementList.aspx" target="_blank">Employee Retirement List</a></span>
                                            </li>

                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HREmpProjectMapping.aspx" target="_blank">Employee Project Mapping</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HRAchievement.aspx" target="_blank">Employee Achievement</a></span>
                                            </li>
                                            <li>
                                                <span class="handle ui-sortable-handle"></span>
                                                <span class="text"><a href="../../mis/HR/HRPunishment.aspx" target="_blank">Employee Punishment</a></span>
                                            </li>
                                        </ul>

                                    </fieldset>
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
