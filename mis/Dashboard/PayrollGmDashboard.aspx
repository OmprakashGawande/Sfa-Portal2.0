<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollGmDashboard.aspx.cs" Inherits="mis_Dashboard_PayrollGmDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="assets_dashboard/custom-dashboard.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content performance-block" id="performance-dashboard">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-pramod">
                        <div class="box-body">
                            <h2 class="title-heading">Payroll Management System<span>
पेरोल प्रबंधन प्रणाली</span></h2>
                            <div class="other-schemes">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <p class="DashboardSubHeading">
                                                &nbsp;अंतिम माह में  वेतन संवितरण (Last Month Salary Disbursement  - <span id="spnMonth" runat="server"></span>&nbsp;<span id="spnYear" runat="server"></span>)
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="list-block">
                                                <div class="scheme-block">
                                                    <a href="#" style="cursor: default;">
                                                        <img src="assets_dashboard/rupee-gold.png" alt="scheme" />
                                                        <div class="title-text">Total Salary Disbursed</div>
                                                    </a>
                                                    <a href="../Payroll/PayrollTotalSalaryandGenrateSalary.aspx?Parameter=TotalSalary" target="_blank" class="detail" style="cursor: default;">
                                                        <div class="detail-inner">
                                                            <div class="scm-text">कुल वितरित राशि </div>
                                                            <div class="scm-text"><i class="fa fa-inr"></i><span id="salaryDisbursed" runat="server"></span></div>
                                                            <div class="scm-text"><span></span></div>
                                                        </div>
                                                    </a>
                                                    <p><i class="fa fa-inr"></i>&nbsp;&nbsp;<span class="Count" id="TotalsalaryDisbursed" runat="server"></span></p>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="list-block">
                                                <div class="scheme-block">
                                                    <a href="#" style="cursor: default;">
                                                        <img src="assets_dashboard/collaboration.png" alt="scheme" />
                                                        <div class="title-text">Total Employee(Salary Disbursed)</div>
                                                    </a>
                                                    <a href="../Payroll/PayrollTotalSalaryandGenrateSalary.aspx?Parameter=SalaryGenerated" target="_blank" class="detail" style="cursor: default;">
                                                        <div class="detail-inner">
                                                            <div class="scm-text">कुल कर्मचारी (लाभार्थी)</div>
                                                            <div class="scm-text text-white"><span id="spnEmpSalaryDisbursed" runat="server"></span></div>
                                                            <div class="scm-text"><span></span></div>
                                                        </div>
                                                    </a>
                                                    <p><span class="Count text-green" id="spnTotalEmpSalaryDisbursed" runat="server">458</span></p>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="list-block">
                                                <div class="scheme-block">
                                                    <a href="#" style="cursor: default;">
                                                        <img src="assets_dashboard/collaboration.png" alt="scheme" />
                                                        <div class="title-text">Total Employee(Salary Not Disbursed)</div>
                                                    </a>
                                                    <a href="#" class="detail" style="cursor: default;">
                                                        <div class="detail-inner">
                                                            <div class="scm-text">कर्मचारी जिनको वेतन नहीं दिया गया </div>
                                                            <div class="scm-text text-white"><span id="spnEmpNotSlryDisbursment" runat="server"></span></div>
                                                            <div class="scm-text"><span></span></div>
                                                        </div>
                                                    </a>
                                                    <p><span class="Count text-red" id="spnTotalEmpNotSlryDisbursment" runat="server"></span></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <p class="DashboardSubHeading">
                                                &nbsp; अंतिम माह में वितरित की गई बकाया राशि
 (Arrear Amount Distributed In Last Month  - <span id="spnArrearMonth" runat="server"></span>&nbsp;<span id="spnArrearYear" runat="server"></span>)
                                            </p>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="list-block">
                                                <div class="scheme-block">
                                                    <a href="#" style="cursor: default;">
                                                        <img src="assets_dashboard/rupee.png" alt="scheme" />
                                                        <div class="title-text">Total Arrear Distributed</div>
                                                    </a>
                                                    <a href="../Payroll/PayrollTotallArrearDistribute.aspx" target="_blank" class="detail" style="cursor: default;">
                                                        <div class="detail-inner">
                                                            <div class="scm-text">
                                                                कुल बकाया राशि वितरित
                                                            </div>
                                                            <div class="scm-text"><i class="fa fa-inr"></i><span id="spnArrearDistributed" runat="server"></span></div>
                                                            <div class="scm-text"><span></span></div>
                                                        </div>
                                                    </a>
                                                    <p><i class="fa fa-inr"></i>&nbsp;&nbsp;<span class="Count" id="spnTotalArrearDistributed" runat="server"></span></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="list-block">
                                                <div class="scheme-block">
                                                    <a href="#" style="cursor: default;">
                                                        <img src="assets_dashboard/money.png" alt="scheme" />
                                                        <div class="title-text">EPF Amount For Distributed Arrear</div>
                                                    </a>
                                                    <a href="../Payroll/PayrollEpfAmountDistributed.aspx" target="_blank" class="detail" style="cursor: default;">
                                                        <div class="detail-inner">
                                                            <div class="scm-text">ई. पी. एफ. की राशि</div>
                                                            <div class="scm-text"><i class="fa fa-inr"></i><span id="SpnEPF" runat="server"></span></div>
                                                            <div class="scm-text"><span></span></div>
                                                        </div>
                                                    </a>
                                                    <p><i class="fa fa-inr"></i>&nbsp;&nbsp;<span class="Count" id="SpnTotalEPF" runat="server">145454</span></p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="list-block">
                                                <div class="scheme-block">
                                                    <a href="#" style="cursor: default;">
                                                        <img src="assets_dashboard/collaboration.png" alt="scheme" />
                                                        <div class="title-text">Total  Arrear Beneficiaries </div>
                                                    </a>
                                                    <a href="../Payroll/PayrollTotalArrearBenificiary.aspx" target="_blank" class="detail" style="cursor: default;">
                                                        <div class="detail-inner">
                                                            <div class="scm-text">बकाया राशि के कुल लाभार्थी</div>
                                                            <div class="scm-text"><span id="spnEmpArrear" runat="server"></span></div>
                                                            <div class="scm-text"><span></span></div>
                                                        </div>
                                                    </a>
                                                    <p><span class="Count" id="spnTotalEmpArrear" runat="server"></span></p>
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
</asp:Content>

