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
                        <div class="box-header with-border">
                            <h3 class="box-title">Head Office Pages</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <!-- See dist/js/pages/dashboard.js to activate the todoList plugin -->
                            <fieldset>
                                <legend>ONE TIME ACTIVITY</legend>
                                <ul class="todo-list ui-sortable">
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/AdminUnitMaster.aspx" target="_blank">Unit Master</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/HSNMaster.aspx" target="_blank">HSN Master</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/AdminItemCategory.aspx" target="_blank">Item Group Master</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/AdminItemType.aspx" target="_blank">Item Sub-Group Master</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/ItemMaster.aspx" target="_blank">Item Master</a></span>
                                    </li>

                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Admin/AdminOffice.aspx" target="_blank">Company [Office] Master</a></span>
                                    </li>
                                     <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/FinLevel2.aspx" target="_blank">Major Head Master [ Level -2 ]</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/FinLevel3.aspx" target="_blank">Sub Head Master [ Level - 3 ]</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/FinLevel4.aspx" target="_blank">Minor Head Master [ Level - 4 ]</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/FinLevel5.aspx" target="_blank">Micro  Head Master [ Level - 5 ]</a></span>
                                    </li>
                                </ul>

                            </fieldset>
                            <fieldset>
                                <legend>Transaction Pages</legend>
                                <%--<ul class="todo-list ui-sortable">
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/BankMaster.aspx" target="_blank">Bank Master</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/BranchMaster.aspx" target="_blank">Branch Master</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/AccountMaster.aspx" target="_blank">Account Master</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/ChequeBookMaster.aspx" target="_blank">Cheque Book Master</a></span>
                                    </li>

                                </ul>--%>
                                <ul class="todo-list ui-sortable">


                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/LedgerMaster.aspx" target="_blank">Create Ledger</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Contra-voucher.aspx" target="_blank">Contra voucher</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Payment-voucher.aspx" target="_blank">Payment voucher</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Receipt-voucher.aspx" target="_blank">Receipt voucher</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Journal-voucher.aspx" target="_blank">Journal voucher</a></span>
                                    </li>
                                 
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/EditVoucher.aspx" target="_blank">Voucher Details</a></span>
                                    </li>
                                </ul>
                            </fieldset>  
                             <fieldset>
                                <legend>Proposed Reports</legend>
                                <ul class="todo-list ui-sortable">
                                      <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Balance-Sheet-Report.aspx" target="_blank">Balance Sheet</a></span>
                                    </li>
                                     <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Profit-and-Loss-Report.aspx" target="_blank">Profit &amp; Loss</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Stock Summary</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/TrialBalance.aspx" target="_blank">Trial Balance</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Day Book</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Cash Book</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Bank Book</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/LedgerReport.aspx" target="_blank">Ledger</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Sale Register</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Purchase Register</a></span>
                                    </li>
                                     <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">General Register</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Group Summary</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">OutStanding Reports</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Stastics Voucher Detail</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Monument Analysis</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/GST1.aspx" target="_blank">GSTR 1</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">GSTR2</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/GST3.aspx" target="_blank">GSTR3</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Cash Flow</a></span>
                                    </li>
                                     <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Funds Flow</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">List Of Accounts</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Exception Reports</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Bank Reconsilation Statement</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">HO Reconsilation Statement</a></span>
                                    </li>   
                                      <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/LedgerReport1.aspx" target="_blank">Ledger Wise Report 1</a></span>
                                    </li>
                                     <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/LedgerReport2.aspx" target="_blank">Ledger Wise Report 2</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/HeadReport.aspx" target="_blank">All Heads</a></span>
                                    </li>                                         
                                </ul>    
                            </fieldset>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title">Head Office / Regional Office / Branch Office</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <!-- See dist/js/pages/dashboard.js to activate the todoList plugin -->

                            <fieldset>
                                <legend>VENDOR REGISTRATION PROCESS</legend>
                                <ul class="todo-list ui-sortable">
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="http://45.114.143.215:8022/mis/HeadAdmin/vendormaster.aspx" target="_blank">Vendor Registration (By concerning officer)</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/SpVendor_List.aspx" target="_blank">Vendor List</a></span>
                                    </li>

                                </ul>
                            </fieldset>
                            <fieldset>
                                <legend>PURCHASE PROCESS</legend>
                                <ul class="todo-list ui-sortable">
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="http://45.114.143.215:8022/mis/SPCommPage/POReceiveForDRM.aspx" target="_blank">Purchase Form</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/SpPurchase_Detail.aspx" target="_blank">Purchase List</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Debit-Note.aspx" target="_blank">Debit Note (Purchase Return)</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Transfer Purchase voucher</a></span>
                                    </li>
                                </ul>
                            </fieldset>
                            <fieldset>
                                <legend>SALE PROCESS</legend>
                                <ul class="todo-list ui-sortable">
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/CreditSale.aspx" target="_blank">Credit Sale Form</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/CashSale.aspx" target="_blank">Cash Sale Form</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/SpCustomerList.aspx" target="_blank">Credit/Cash Sale List</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Credit-Note.aspx" target="_blank">Credit Note (Sales Return)</a></span>
                                    </li>
                                    <li>
                                        <!-- drag handle -->
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Transfer Sale voucher</a></span>
                                    </li>
                                </ul>
                            </fieldset>
                            <fieldset>
                                <legend>Inward/Outward Summary</legend>
                                <ul class="todo-list ui-sortable">
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Inward Summary</a></span>
                                    </li>
                                    <li>
                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="#" target="_blank">Outward Summary</a></span>
                                    </li>

                                    <li>

                                        <span class="handle ui-sortable-handle"></span>
                                        <span class="text"><a href="../../mis/Finance/Opening_Balance.aspx" target="_blank">Opening Balance for Inventory</a></span>
                                    </li>
                                </ul>
                            </fieldset>
                        </div>
                    </div>
                </div>

            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>
