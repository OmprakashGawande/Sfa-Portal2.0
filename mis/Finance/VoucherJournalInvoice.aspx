<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VoucherJournalInvoice.aspx.cs" Inherits="mis_Finance_VoucherJournalInvoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <title>&nbsp;</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport"/>
    <!-- Bootstrap 3.3.7 -->
    <link rel="stylesheet" href="../css/bootstrap.css"/>
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../font-awesome/css/font-awesome.min.css"/>

    <!-- Theme style -->
    <link rel="stylesheet" href="../css/AdminLTE.css"/>
<%--    <link href="../css/bootstrap.css" rel="stylesheet" />--%>
    <!-- Google Font -->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic"/>
    <style type="text/css">
        .btn-danger {
            color: #4b4c9d;
        }
        table,tr, th, td {
         border: 2px solid black;
         border-bottom:2px solid black;
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
            <div style="width:90%; margin: auto;">
                
                <!-- Main content -->
                <section class="content">
                    <div id="DivTable" runat="server"></div>
                </section>
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
