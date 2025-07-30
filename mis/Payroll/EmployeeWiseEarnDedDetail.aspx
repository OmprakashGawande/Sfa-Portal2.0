<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeWiseEarnDedDetail.aspx.cs" Inherits="mis_Payroll_EmployeeWiseEarnDedDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <style>
        .Grid td {
            padding: 3px !important;
        }

            .Grid td input {
                padding: 3px 3px !important;
                text-align: right !important;
                font-size: 12px !important;
                height: 26px !important;
            }

        .Grid th {
            text-align: center;
        }

        .ss {
            text-align: left !important;
        }

        .bgcolor {
            background-color: #eeeeee !important;
        }

        .box {
            min-height: initial !important;
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
                                <%--<img src="../image/mpagro-logo.png" class="salary-logo">--%>
                                &nbsp; M.P. STATE MINOR FOREST PRODUCE(T & D)CO-OP. FED. LTD
							<br>
                                <span id="subheading-salary">Policy Earning Deduction Detail</span></h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="DivDetail" runat="server" style="width: 97.5%;">
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <table id="tblEarning" class="table table-bordered table-striped Grid" style="margin-top: -20px;">
                                        <asp:Repeater ID="RepeaterEarn" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEarnDeductionName" runat="server" Text='<%# Eval("EarnDeductionName").ToString()%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRemainingAmt" runat="server" Text='<%# Eval("RemainingAmount").ToString()%>'></asp:Label></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                                <div class="col-md-6">
                                    <table id="tblDed" class="table table-bordered table-striped Grid" style="margin-top: -20px; margin-left: -30px;">
                                        <asp:Repeater ID="RepeaterDed" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEarnDeductionName" runat="server" Text='<%# Eval("EarnDeductionName").ToString()%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblRemainingAmt" runat="server" Text='<%# Eval("RemainingAmount").ToString()%>'></asp:Label></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-9"></div>
                                <div class="col-md-3">
                                    <div class="form-group" style="margin-right: 26px;">
                                        <label>Net Payment</label>
                                        <asp:TextBox ID="txtNetPayment" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
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
