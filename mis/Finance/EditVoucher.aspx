<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="EditVoucher.aspx.cs" Inherits="mis_Finance_EditVoucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script type="text/javascript">

        function printDiv(divName) {



            var printContents = document.getElementById(divName).innerHTML;

            var originalContents = document.body.innerHTML;

            document.body.innerHTML = printContents;

            window.print();

            document.body.innerHTML = originalContents;
        }
    </script>
    <style type="text/css" media="print">
        @page {
            size: auto; /* auto is the initial value */
            margin: 0mm; /* this affects the margin in the printer settings */
        }

        html {
            background-color: #f00;
            margin: 0px; /* this affects the margin on the html before sending to printer */
        }

        body {
            margin: 0mm 0mm 0mm 0mm;
            /* margin you want for the content 
            margin:;*/
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">

        <section class="content-header">
            <h1>Voucher Details
                    <small></small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            </ol>
        </section>

        <section class="content">

            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box box-pramod">

                                <div class="box-body">
                                    <div class="col-md-8">
                                        <div class="form-group">

                                            <table id="ctl00_ContainerBody_rbtPlace" class="radioButtonList" border="0">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <input id="" type="radio" name="ctl00$ContainerBody$rbtPlace" value="Head Office" checked="checked"><label for="ctl00_ContainerBody_rbtPlace_0"> &nbsp;Contra voucher &nbsp; &nbsp; &nbsp;</label>
                                                        </td>
                                                        <td>
                                                            <input id="" type="radio" name="ctl00$ContainerBody$rbtPlace" value=""><label for=""> &nbsp;Payment voucher &nbsp; &nbsp; &nbsp;</label>
                                                        </td>
                                                        <td>
                                                            <input id="" type="radio" name="ctl00$ContainerBody$rbtPlace" value=""><label for=""> &nbsp; Receipt voucher &nbsp; &nbsp; &nbsp;</label>
                                                        </td>
                                                        <td>
                                                            <input id="" type="radio" name="ctl00$ContainerBody$rbtPlace" value="">
                                                            <label for="">&nbsp; Journal voucher &nbsp; &nbsp; &nbsp;</label>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>


                                    <div class="col-md-2">
                                        <div class="form-group">

                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <input name="ctl00$ContainerBody$txtDOB" type="text" id="txtDOB" class="form-control" autocomplete="off" placeholder="Enter Date of Birth" value="08/29/2018" data-provide="datepicker" data-date-end-date="0d" onpaste="return false">
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <input type="submit" name="ctl00$ContainerBody$btnSubmit" value="Show Details" onclick="return validateform();" id="ctl00_ContainerBody_btnSubmit" class="btn btn-block btn-primary">
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <table id="example1" class="table table-bordered table-striped">
                                            <thead>
                                                <tr>

                                                    <th>S.N.</th>
                                                    <th>Voucher Type</th>
                                                    <th>Date</th>
                                                    <th>Voucher Number</th>
                                                    <th>Narration</th>
                                                    <th>Dr.  </th>
                                                    <th>Cr.  </th>
                                                    <th>Balance</th>
                                                    <th>Action1</th>
                                                    <th>Action2</th>
                                                    <th>Action3</th>
                                                    <th>Action4</th>


                                                </tr>

                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>1</td>
                                                    <td>Payment voucher</td>
                                                    <td>21/04/17</td>
                                                    <td>97</td>
                                                    <td>Paid to Mr. Rai Sing</td>
                                                    <td>37201</td>
                                                    <td>0</td>
                                                    <td>37201</td>
                                                    <td>
                                                        <button type="button" class="btn btn-warning btn-md" id="myBtn">Add</button></td>
                                                    <td>
                                                        <input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>
                                                    <td>
                                                        <input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Send For Approval" /></td>
                                                    <td><a href="view-voucher.aspx" class="btn btn-warning btn-md">View / Print</a> </td>
                                                </tr>

                                                <tr>
                                                    <td>1</td>
                                                    <td>Payment voucher</td>
                                                    <td>21/04/17</td>
                                                    <td>97</td>
                                                    <td>Paid to Mr. Rai Sing</td>
                                                    <td>37201</td>
                                                    <td>0</td>
                                                    <td>37201</td>
                                                    <td>
                                                        <button type="button" class="btn btn-warning btn-md" id="myBtn1">Add</button></td>
                                                    <td>
                                                        <input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>
                                                    <td>
                                                        <input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Send For Approval" /></td>
                                                    <td><a href="view-voucher.aspx" class="btn btn-warning btn-md">View / Print</a> </td>
                                                </tr>


                                                <tr>
                                                    <td>1</td>
                                                    <td>Payment voucher</td>
                                                    <td>21/04/17</td>
                                                    <td>97</td>
                                                    <td>Paid to Mr. Rai Sing</td>
                                                    <td>37201</td>
                                                    <td>0</td>
                                                    <td>37201</td>
                                                    <td>
                                                        <button type="button" class="btn btn-warning btn-md" id="myBtn3">Add</button></td>
                                                    <td>
                                                        <input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Update" /></td>
                                                    <td>
                                                        <input id="Submit1" type="submit" class="btn btn-warning btn-md" value="Send For Approval" /></td>
                                                    <td><a href="view-voucher.aspx" class="btn btn-warning btn-md">View / Print</a> </td>
                                                </tr>

                                                <tr>
                                                    <td>1</td>
                                                    <td>Payment voucher</td>
                                                    <td>21/04/17</td>
                                                    <td>97</td>
                                                    <td>Paid to Mr. Rai Sing</td>
                                                    <td>37201</td>
                                                    <td>0</td>
                                                    <td>37201</td>
                                                    <td></td>
                                                    <td></td>
                                                    <td>Pending For Approval</td>
                                                    <td><a href="view-voucher.aspx" class="btn btn-warning btn-md">View / Print</a> </td>
                                                </tr>





                                            </tbody>
                                        </table>
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
    <script>
        $(function () {
            $(document).ready(function () {
                $('#example1,#example2,#example3,#example4').DataTable({
                    dom: 'Bfrtip',
                    buttons: [
                        'print'
                    ],
                    "pageLength": 50
                });
            });
        });



        $(document).ready(function () {
            $("#myBtn").click(function () {
                $("#myModal").modal();

            });
        });

        $(document).ready(function () {
            $("#myBtn1").click(function () {
                $("#myModal").modal();

            });
        });

        $(document).ready(function () {
            $("#myBtn2").click(function () {
                $("#myModal").modal();

            });
        });

        $(document).ready(function () {
            $("#myBtn3").click(function () {
                $("#myModal").modal();

            });
        });

    </script>
</asp:Content>
