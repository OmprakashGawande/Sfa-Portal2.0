<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalarySlip.aspx.cs" Inherits="mis_Payroll_SalarySlip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <style>
        .box {
            position: relative;
            border-radius: 3px;
            background: #ffffff;
            border-top: 3px solid #d2d6de;
            margin-bottom: 20px;
            width: 100%;
            box-shadow: 0 1px 1px rgba(0,0,0,0.1);
            box-shadow: none;
            border-top: none;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #e1e1e1;
        }

        .text-center h3 {
           font-size: 15px;
    font-family: monospace;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 0px 2px;
        }

        #subheading-salary {
            font-size: 13px;
        }

        .salary-logo {
            -webkit-filter: grayscale(100%);
            filter: grayscale(100%);
            width: 40px;
        }

        .printbutton {
            border-top: 1px dashed #838383;
            margin-top: 5px;
            padding-top: 5px;
        }

        table h4 {
            font-size: 15px;
        }

        .table {
            margin-bottom: 5px;
        }

        th, td, h3 {
            text-transform: uppercase !important;
        }
        .watermark {
  width: 300px;
  height: 100px;
  display: block;
  position: relative;
}

.watermark::after {
  content: "";
 background:url('../image/mpagro-logo.png');
  opacity: 0.2;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  position: absolute;
  z-index: -1;   
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="content-wrapper">
                <section class="content watermark" id="DivSlip" runat="server" style="padding-top: 0px; height: 60px;">
                    <!-- Default box -->
                    <div class="box box-default">
                        <div class="box-header text-center">
                            <h3 class="">
                                <img src="../image/mpagro-logo.png" class="salary-logo">
                                &nbsp;&nbsp; THE M.P. STATE AGRO INDUSTRIES DEVELOPMENT CORPORATION LTD.  HEAD OFFICE 
							<br>
                                <span id="subheading-salary">PAY SLIP FOR THE MONTH OF <span id="lblMonth" runat="server"></span>&nbsp; <span id="lblFinancialYear" runat="server"></span>&nbsp; <span style="color: red;" id="lblGenStatus" runat="server"></span></span></h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div>
                                        <%--<table class="table table-striped" style="margin-bottom: 0px;">
                                            <tbody>
                                                <tr>
                                                    <th>NAME OF EMPLOYEE :
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblEmp_Name" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <th>BANK ACCOUNT NUMBER :
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblBank_AccountNo" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>DESIGNATION :
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblDesignation_Name" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <th>
                                                        <asp:Label ID="lblEmp_GpfType" runat="server" Text=""></asp:Label>
                                                        NUMBER :
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblEmp_GpfNo" runat="server" Text=""></asp:Label>
                                                    </td>


                                                </tr>
                                                <tr>
                                                    <th>EMPLOYEE CODE :
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblUserName" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <th>NET SALARY :
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblSalary_NetSalary" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <%--<table class="table table-striped" style="margin-bottom: 0px;">
                                        <tbody>
                                            <tr>
                                                <td style="width: 50%">
                                                    <div class="" style="padding-bottom: 0; padding-left: 5px; font-weight: 700;">
                                                        <h4>EARNINGS</h4>
                                                    </div>
                                                    <div class="table-responsive">
                                                        <div>
                                                            <table class="table table-bordered table-striped Grid earning-table">
                                                                <tbody>
                                                                    <tr>
                                                                        <th>BASIC SALARY :</th>
                                                                        <td>
                                                                            <asp:Label ID="lblSalary_Basic" runat="server" Text=""></asp:Label></td>
                                                                    </tr>

                                                                    <tr>
                                                                        <th>
                                                                            <asp:Label ID="lblEarnDeduction_Name" runat="server" Text=""></asp:Label>
                                                                            :</th>
                                                                        <td>
                                                                            <asp:Label ID="lblEarning" runat="server" Text=""></asp:Label></td>
                                                                    </tr>

                                                                    <tr class="total_salary">
                                                                        <th>TOTAL EARNINGS :</th>
                                                                        <th>
                                                                            <asp:Label ID="lblSalary_EarningTotal" runat="server" Text=""></asp:Label></th>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>

                                                    </div>

                                                </td>
                                                <td style="width: 50%">
                                                    <div class="">
                                                        <div class="" style="padding-bottom: 0; padding-left: 5px; font-weight: 700;">
                                                            <h4>DEDUCTIONS</h4>

                                                        </div>
                                                        <div class="">
                                                            <div class="table-responsive">
                                                                <div>
                                                                    <table class="table table-bordered table-striped Grid deduction-table">
                                                                        <tbody>
                                                                            <tr>
                                                                                <th>SALARY DEDUCTION (For Absent Days) :</th>
                                                                                <td>
                                                                                    <asp:Label ID="lblSalary_NoDayDeduAmt" runat="server" Text=""></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <th>
                                                                                    <asp:Label ID="lbl_EarnDeduction_Name" runat="server" Text=""></asp:Label>
                                                                                    :</th>
                                                                                <td>
                                                                                    <asp:Label ID="lbl_Earning" runat="server" Text=""></asp:Label></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <th>POLICY :</th>
                                                                                <th>
                                                                                    <asp:Label ID="lblPolicyDeduction" runat="server" Text=""></asp:Label></th>
                                                                            </tr>
                                                                            <tr class="total_salary">
                                                                                <th>TOTAL DEDUCTION:</th>
                                                                                <th>
                                                                                    <asp:Label ID="lblSalary_DeductionTotal" runat="server" Text=""></asp:Label></th>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th></th>
                                                <th style="text-align: right;">DGM (ACCTTS)</th>
                                            </tr>
                                            <tr>
                                                <th colspan="2">DO GOOD, THINK GOOD, HELP OTHERS</th>
                                            </tr>
                                        </tbody>
                                    </table>--%>
                                    <div style="text-align: center;" class="printbutton">
                                        <input type="button" class="btn btn-primary" value="Print" onclick="window.print()">
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </form>
</body>
</html>
