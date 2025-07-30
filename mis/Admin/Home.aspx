<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="mis_Finance_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper" style="min-height: 414px;">
        <section class="content">
            <img src="../image/download.png" style="width: 75px;" />
            <div class="row">
                <div class="col-md-6">
                    <div class="box box-success">
                        <%--<div class="box-header ui-sortable-handle" style="cursor: move;">
                          
                            <h3 class="box-title"></h3>
                        </div>--%>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <!-- See dist/js/pages/dashboard.js to activate the todoList plugin -->
                            <fieldset>
                                <legend>Admin Master Pages</legend>
                                <ul class="todo-list ui-sortable">
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/AdminState.aspx" target="_blank">State Master</a></span>
                                    </li>

                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/AdminDivision.aspx" target="_blank">Division Master</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/AdminDistrict.aspx" target="_blank">District Master</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/AdminBlock.aspx" target="_blank">Block Master</a></span>
                                    </li>


                                   <%-- <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/HR/HRYearMaster.aspx" target="_blank">Financial Year Master</a></span>
                                    </li>--%>
                                </ul>
                            </fieldset>
                            <fieldset>
                                <legend>HR & Payroll Master Pages</legend>
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
                                        <span class="text"><a href="../../mis/Payroll/PayrollEarn_DedMaster.aspx" target="_blank">Earning & Deduction Heads</a></span>
                                    </li>
                                     <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Payroll/PayrollEarn_DeductionMaster.aspx" target="_blank">Active Earning & Deduction Heads</a></span>
                                    </li>
                                     <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/HR/HREmpReg.aspx" target="_blank">Employee Registraion</a></span>
                                    </li>
									 <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/HR/HROverAllEmpList.aspx" target="_blank">Employee Detail</a></span>
                                    </li>


                                </ul>
                            </fieldset>
                            <fieldset>
                                <legend>Meeting Masters </legend>
                                <ul class="todo-list ui-sortable">
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/AdminMeetingMaster.aspx" target="_blank">Meeting Master</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/MileStoneMaster.aspx" target="_blank">Mile Stone Master</a></span>
                                    </li>

                                    
                                </ul>
                            </fieldset>
                            
                            
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-success">
                        <%--<div class="box-header ui-sortable-handle" style="cursor: move;">
                          
                            <h3 class="box-title"></h3>
                        </div>--%>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <!-- See dist/js/pages/dashboard.js to activate the todoList plugin -->

                            <fieldset>
                                <legend>Sales & Purchase</legend>
                                <ul class="todo-list ui-sortable">
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="http://45.114.143.215:8022/mis/HeadAdmin/productsize.aspx" target="_blank">Product Size Master</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="http://45.114.143.215:8022/mis/HeadAdmin/addproductspec.aspx" target="_blank">Add Product Specification</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="http://45.114.143.215:8022/mis/HeadAdmin/addmbvk.aspx" target="_blank">Mahila Bal Vikas Kendra Master</a></span>
                                    </li>

                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="http://45.114.143.215:8022/mis/HeadAdmin/addcoalrate.aspx" target="_blank">Coal Rate Master</a></span>
                                    </li>

                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="http://45.114.143.215:8022/mis/HeadAdmin/vendormaster.aspx" target="_blank">Vendor Master</a></span>
                                    </li>

                                     <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="http://45.114.143.215:8022/mis/HeadAdmin/addcustomer.aspx" target="_blank">Customer Master</a></span>
                                    </li>

                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="http://45.114.143.215:8022/mis/HeadAdmin/setRteRatio.aspx" target="_blank">RTE Set Ratio</a></span>
                                    </li>


                                </ul>
                            </fieldset>
                            <fieldset>
                                <legend>User Management</legend>
                                <ul class="todo-list ui-sortable">
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/UMModuleMaster.aspx" target="_blank">Module Master</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/UMFormMaster.aspx" target="_blank">Form Master</a></span>
                                    </li>

                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/UMMenuMaster.aspx" target="_blank">Menu Master</a></span>
                                    </li>


                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/UMMenuFormMap.aspx" target="_blank">Menu Form Mapping</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/UMRoleMaster.aspx" target="_blank">Role Master</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/UMRoleFormMap.aspx" target="_blank">Role Form Mapping</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/UMEmpRoleMap.aspx" target="_blank">Employee Role Mapping</a></span>
                                    </li>

                                </ul>
                            </fieldset>
                           <%-- <fieldset>
                                <legend>Sales & Purchase</legend>
                                <ul class="todo-list ui-sortable">
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="http://45.114.143.215:8022/mis/HeadAdmin/productsize.aspx" target="_blank">Product Size Master</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="http://45.114.143.215:8022/mis/HeadAdmin/addproductspec.aspx" target="_blank">Add Product Specification</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="http://45.114.143.215:8022/mis/HeadAdmin/addmbvk.aspx" target="_blank">Mahila Bal Vikas Kendra Master</a></span>
                                    </li>

                                </ul>
                            </fieldset>--%>


                        </div>
                    </div>
                </div>

            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>
