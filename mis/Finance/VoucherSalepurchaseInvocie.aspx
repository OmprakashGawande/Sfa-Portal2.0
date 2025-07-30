<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VoucherSalepurchaseInvocie.aspx.cs" Inherits="mis_Finance_VoucherSalepurchaseInvocie" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>&nbsp;</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="../css/bootstrap.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css" />

    <!-- Theme style -->
    <link rel="stylesheet" href="../css/AdminLTE.css" />
    <%--    <link href="../css/bootstrap.css" rel="stylesheet" />--%>
    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
    <style type="text/css">
        .btn-danger {
            color: #4b4c9d;
        }

        table, tr, th, td {
            line-height: 1.6;
            padding: 2px;
            font-size: 11.5px;
        }

        .cssborder {
            border: 1px solid black !important;
        }

        .Pbold {
            font-weight: 700;
        }

        .PSemibold {
            font-weight: 500;
        }

        @media print {
            a[href]:after {
                display: none;
                visibility: hidden;
            }
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <div>
            <div style="width: 100%; margin-left: 15px; margin: auto;">

                <!-- Main content -->
                <section class="content">
                    <div class="invoice">
                        <div class="row">
                            <div class="col-md-2"></div>
                            <div class="col-md-10">
                                <table>
                                    <tr>
                                        <td colspan="5" style="border-top: 1px solid black; border-left: 1px solid black; border-right: 1px solid black; width: 70%" class="Pbold">
                                            <div class="image_section" style="float: left; width: 20%">
                                                <img class="pull-left" src="../image/agro-logo.png" style="margin-top: 1px;" />
                                            </div>
                                            <div class="content_section" style="float: right; width: 79%">
                                                THE M.P STATE AGRO INDUSTRIES DEV. CORPN. LTD.<br />
                                                (A Government of M.P Undertaking)<br />
                                                <span id="OffcAddress" runat="server"></span>
                                                <br />
                                                Email:&nbsp;&nbsp;<span id="spnemail" runat="server"></span><br />
                                                CIN No. U01119MP1969SGC001055<br />
                                                GSTIN -23AACCM0330Q1ZM
                                            </div>
                                        </td>
                                        <td colspan="5" style="border-top: 1px solid black; border-left: 1px solid black; border-right: 1px solid black; width: 30%; padding-bottom: 70px;" rowspan="2" class="Pbold">To,<br />
                                            <span id="spnto" runat="server" class="PSemibold"></span></td>
                                    </tr>

                                    <tr>
                                        <td colspan="5" class="Pbold" style="border-left: 1px solid black; border-right: 1px solid black;">Branch:&nbsp;&nbsp;<span id="spnbranch" class="Pbold" runat="server"></span></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="Pbold" style="border-bottom: 1px solid black; border-left: 1px solid black;">Invoice No.&nbsp;&nbsp;<span id="spninvno" class="Pbold" runat="server"></span></td>
                                        <td colspan="2" class="Pbold" style="border-bottom: 1px solid black; border-right: 1px solid black;">DATE&nbsp;&nbsp;<span id="spndate" class="Pbold" runat="server"></span></td>
                                        <td colspan="5" style="border-bottom: 1px solid black; border-right: 1px solid black;"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" class="Pbold" style="border-top: 1px solid black; border-left: 1px solid black;">ORDER No.&nbsp;&nbsp;<span id="spnorderno" class="PSemibold" runat="server"></span></td>
                                        <td colspan="2" class="Pbold" style="border-top: 1px solid black;">DATED&nbsp;&nbsp;<span id="spnorddate" class="PSemibold" runat="server"></span></td>
                                        <td colspan="5" class="Pbold" style="border-top: 1px solid black; border-right: 1px solid black;">GR/RR No:&nbsp;&nbsp;<span id="spngrrrno" class="PSemibold" runat="server"></span></td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" class="Pbold" style="border-left: 1px solid black;">NAME OF TRANSPORTER.</td>
                                        <td colspan="5" class="Pbold" style="border-right: 1px solid black;">FREIGHT PAID/TO PAY Rs.</td>
                                    </tr>
                                    <tr>
                                        <td colspan="5" class="Pbold" style="border-left: 1px solid black; border-bottom: 1px solid black;">NAME OF THE SCHEME&nbsp;&nbsp;<span id="spnschemename" class="PSemibold" runat="server"></span></td>
                                        <td colspan="5" class="Pbold" style="border-right: 1px solid black; border-bottom: 1px solid black;">Registration No&nbsp;&nbsp;<span id="spnRegNo" class="PSemibold" runat="server"></span></td>
                                    </tr>
                                    <tr>
                                        <td class="cssborder Pbold" rowspan="2" style="text-align: center">S.No.</td>
                                        <td class="cssborder Pbold" rowspan="2" style="text-align: center">NAME OF THE ITEM</td>
                                        <td class="cssborder Pbold" rowspan="2" style="text-align: center">HSN/SAC CODE</td>
                                        <td class="cssborder Pbold" rowspan="2" style="text-align: center">BASIC RATE</td>
                                        <td class="cssborder Pbold" rowspan="2" style="text-align: center">QTY</td>
                                        <td class="cssborder Pbold" rowspan="2" style="text-align: center">AMOUNT</td>
                                        <td class="cssborder Pbold" style="text-align: center" colspan="3">GST DETAILS</td>
                                        <td class="cssborder Pbold" style="padding: 5px; text-align: center" rowspan="2">TOTAL AMOUNT</td>
                                    </tr>
                                    <tr>

                                        <td class="cssborder Pbold">%</td>
                                        <td class="cssborder Pbold">CGST</td>
                                        <td class="cssborder Pbold">SGST</td>

                                    </tr>
                                    <tr>
                                        <td class="cssborder Pbold" style="text-align: center;">1</td>
                                        <td class="cssborder Pbold" style="text-align: center;">2</td>
                                        <td class="cssborder Pbold" style="text-align: center;">3</td>
                                        <td class="cssborder Pbold" style="text-align: center;">4</td>
                                        <td class="cssborder Pbold" style="text-align: center;">5</td>
                                        <td class="cssborder Pbold" style="text-align: center;">6(4 * 5)</td>
                                        <td class="cssborder Pbold" style="text-align: center;">7</td>
                                        <td class="cssborder Pbold" style="text-align: center;">8</td>
                                        <td class="cssborder Pbold" style="text-align: center;">9</td>
                                        <td class="cssborder Pbold" style="text-align: center;">10(6+8+9)</td>
                                    </tr>
                                    <tr>
                                        <div id="divitem" runat="server">
                                        </div>
                                    </tr>

                                    <tr>
                                        <td colspan="2" rowspan="2" style="text-align: center;" class="cssborder Pbold">E & O.E. ACCEPTED</td>
                                        <td colspan="7" style="text-align: center;" class="cssborder Pbold">LESS: ADVANCE RECEIVED (IF ANY)</td>
                                        <td class="cssborder"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" style="text-align: center;" class="cssborder Pbold">NET AMOUNT DUE</td>
                                        <td class="cssborder"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="10" class="cssborder Pbold">Amount in words&nbsp;&nbsp;<span id="spnAmount" class="PSemibold" runat="server"></span></td>
                                    </tr>
                                    <tr>
                                        <td colspan="10" class="cssborder Pbold">Remark&nbsp;&nbsp;<span id="spnNarration" class="PSemibold" runat="server"></span></td>
                                    </tr>
                                    <tr>
                                        <td colspan="10" class="cssborder Pbold">Cheques/Draft Should be in the name of THE M.P. STATE AGRO INDUSTRIES DEV.CORPN.LTD.<br />
                                            PAYABLE AT</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="Pbold" style="border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black;">Name of the Bank&nbsp;&nbsp;<span id="spnbankname" class="PSemibold" runat="server"></span></td>
                                        <td colspan="6" class="Pbold"  style="border-top: 1px solid black; border-right: 1px solid black; border-left: 1px solid black; padding-bottom: 30px; width: 70%" rowspan="3">For: M.P STATE AGRO IND.DEV.CORPN.LTD</td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="Pbold" style="border-right: 1px solid black; border-left: 1px solid black;">A/c No.&nbsp;&nbsp;<span id="spnactno" class="PSemibold" runat="server"></span></td>

                                    </tr>
                                    <tr>
                                        <td colspan="4" class="Pbold" style="border-right: 1px solid black; border-left: 1px solid black;">IFSC Code&nbsp;&nbsp;<span id="spnifsccode" class="PSemibold" runat="server"></span></td>

                                    </tr>
                                    <tr>
                                        <td colspan="4" class="Pbold" style="border-top: 1px solid black; border-bottom: 1px solid black; border-left: 1px solid black; padding-top: 50px;">Receiver's Signature</td>
                                        <td colspan="6" class="Pbold" style="border-bottom: 1px solid black; border-right: 1px solid black; padding-top: 50px; width: 70%">AUTHORISED SIGNATORY(Designation)</td>
                                    </tr>
                                </table>
                            </div>
                            <div class="col-md-2"></div>
                        </div>
                    </div>
                    <div id="DivTable" runat="server"></div>
                </section>
                <!-- /.content -->
                <div class="clearfix"></div>
            </div>
        </div>
        <script src="../../js/jquery-2.2.3.min.js"></script>
        <%--<script>
            window.print();
            function myFunction() {
                window.print();
            }
        </script>--%>
        <!-- ./wrapper -->
        <!-- jQuery 3 -->
        <script src="../../js/jquery-2.2.3.min.js"></script>
        <!-- Bootstrap 3.3.7 -->
        <script src="../../js/bootstrap.js"></script>
        <!-- FastClick -->
    </form>
</body>
</html>
