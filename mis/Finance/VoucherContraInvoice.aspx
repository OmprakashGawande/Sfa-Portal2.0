<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VoucherContraInvoice.aspx.cs" Inherits="mis_Finance_VoucherContraInvoice" %>

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
            border: 2px solid black;
        }
        .float-right {
    float: right!important;
}
        .lead {
    font-size: 1.50rem;
    font-weight:700;
}
        .small {
    font-size: 1.50rem;
    font-weight:500;
}
    </style>

</head>
<body>
    <form id="form1" runat="server">

        <div>
            <div style="width: 90%; margin: auto;">
                <section class="content">
                    <div id="DivTable" runat="server"></div>
                </section>
                <!-- Main content -->
                <%--<section class="invoice">
                    <!-- title row -->
                    
                    <div class="row">

                        <div class="col-xs-12">
                            <h2 class="text-center" style="font-weight: 800; font-size: 35px;">The M.P State Agro Ind. Dev. Corp. Ltd.</h2>
                        </div>

                        <!-- /.col -->

                    </div>
                    <div class="row">
                        <div class="col-xs-8">
                            <span style="font-size: 17px; font-weight: 800;">Name of the Office.&nbsp;</span>
                        </div>
                        <div class="col-xs-2">
                            <span style="font-size: 17px; font-weight: 800;">Vr.No.&nbsp;</span>509
                        </div>
                        <div class="col-xs-2">
                            <span style="font-size: 17px; font-weight: 800;">Date :&nbsp;</span>01/04/2019
                        </div>
                        <!-- /.col -->

                    </div>
                    <div class="row" style="padding-top: 15px;">
                        <div class="col-xs-12 col-md-12">
                            <table class="table no-border">
                                <tr>
                                </tr>
                                <tr>
                                    <td><b>DEBIT</b></td>
                                    <td>Pay & Allowance Out Source 18%</td>
                                    <td>33108.00</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>CGST</td>
                                    <td>2979.72</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>SGST</td>
                                    <td>2979.72</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td><b>CREDIT</b></td>
                                    <td>Round Off</td>
                                    <td ></td>
                                    <td style="padding-left:120px;">0.44</td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>Income Tax</td>
                                    <td></td>
                                    <td style="padding-left:120px;">662.00</td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>GST (T.D.S)</td>
                                    <td></td>
                                    <td style="padding-left:120px;">662.16</td>
                                </tr>
                                <tr>
                                    <td><b>PAY TO</b></td>
                                    <td>Indoriya Security Force</td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td colspan="3" class="text-center"><b>PARTICULARS</b></td>

                                    <td class="text-center"><b>Amount</b></td>

                                </tr>

                                <tr>
                                    <td></td>
                                    <td colspan="2" style="text-wrap: avoid">Toward Salary 3 Driver For the Month Aug<br />
                                        19 their BillNo. 353/31.8.19 Sanctioned By GM(Dec,MXNP46) File Admin</td>

                                    <td class="text-center" style="border-left: 1px solid black" rowspan="2"><b>37742.84</b></td>



                                </tr>
                                <tr>
                                     <td><b>Rs</b></td>
                                    <td colspan="2" style="text-wrap:normal">Thirty Seven Thousand Seven Hundred Forty Two and Paise Eighty Four Only</td>                                
                                   
                                     
                                </tr>
                                <tr>
                                    <td><b>CHEQUE NO</b>&nbsp;233346</td>
                                     <td style="text-align:right"><b>DATE</b>&nbsp;19.9.2019</td>
                                     <td style="text-align:right"><b>TOTAL</b> </td>
                                     <td class="text-center" style="border-left: 1px solid black"><b>37742.84</b></td>
                                </tr>
                               
                                 
                                <tr>
                                    <td style="padding-top:70px;">Prepared by</td>
                                     <td style="padding-top:70px;"></td>
                                     <td style="padding-top:70px;">Signature of Passing Authority</td>
                                     <td style="padding-top:70px; padding-right:50px; text-align:right">Receiver's Sign.</td>
                                </tr>
                            </table>

                        </div>
                    </div>


                    <!-- info row -->

                    <!-- /.row -->

                    <!-- Table row -->

                    <!-- /.row -->




                    <!-- /.row -->

                    <!-- this row will not appear when printing -->

                </section>--%>
                <!-- /.content -->
                <div class="clearfix"></div>
            </div>
        </div>
        <script src="../../js/jquery-2.2.3.min.js"></script>
        <script>
            window.print();
            function myFunction() {
                window.print();
            }
        </script>
        <!-- ./wrapper -->
        <!-- jQuery 3 -->
        <script src="../../js/jquery-2.2.3.min.js"></script>
        <!-- Bootstrap 3.3.7 -->
        <script src="../../js/bootstrap.js"></script>
        <!-- FastClick -->
    </form>
</body>
</html>
