<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="view-voucher.aspx.cs" Inherits="mis_Finance_view_voucher" %>

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
                <h1>
                    Voucher Detail and Print
                    <small></small>
                </h1>
               <%-- <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                </ol>--%>
            </section> 
            <section class="content">

                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box box-pramod" style="background-color: #D6F6D6;">

                                    <div class="box-body">

                                        <div id="printableArea" style="border:1px solid #000">


                                            <div align="center">
                                                <h1> M.P. State Agro Ind. Dev. Corp. Ltd.</h1>
                                            </div>
                                            <table class="table table-bordered">



                                                <tbody>

                                                    <tr>

                                                        <th>Name Of The Office.................. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;C.B./Follo.......................</th>

                                                        <th> Vr. No....................... &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Date......................</th>



                                                    </tr>
                                                    <tr>

                                                        <td>Debit-  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;RTE Bari</td>

                                                        <td>Amount :  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;21,05,753</td>

                                                    </tr>

                                                    <tr>

                                                        <td>Credit-  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;PNB 012</td>

                                                        <td>Amount :  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2,000</td>

                                                    </tr>

                                                    <tr>

                                                        <td>Pay To -  your Self.....</td>

                                                    </tr>


                                                    <tr>

                                                        <td>Particulars</td>

                                                        <td>Amount : </td>

                                                    </tr>

                                                    <tr>

                                                        <td>Being paid by Mp Agro to ....................</td>
                                                        <td>
                                                            <table class="table table-bordered" style="background-color: #D6F6D6;">
                                                                <tr>
                                                                    <td>Rs.</td>
                                                                    <td>P.</td>
                                                                </tr>

                                                                <tr>
                                                                    <td>2,000</td>
                                                                    <td>00 </td>
                                                                </tr>

                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td> &nbsp;</td>
                                                                </tr>


                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp; </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;</td>
                                                                    <td>&nbsp; </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Total : 2,000</td>
                                                                    <td>00 </td>
                                                                </tr>



                                                            </table>
                                                        </td>

                                                    </tr>

                                                    <tr>
                                                        <td>Rs. &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Two Thousand Rupees Only</td>
                                                        <td>CHEQUE NO.&nbsp;&nbsp; 390057 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date &nbsp;&nbsp; 02/08/2018 </td>
                                                    </tr>

                                                    <tr>

                                                        <td>&nbsp;&nbsp; </td>
                                                    </tr>

                                                    <tr>
                                                        <td>Prepared By &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Signature of Passing Authority</td>

                                                        <td>Checked By  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Receiver's Sign.</td>


                                                    </tr>



                                                </tbody>
                                            </table>
                                        </div>
                                        <br /> 
                                        <div class="col-md-2">
                                            <div class="form-group">

                                                <input class="btn btn-block btn-primary" onclick="printDiv('printableArea')" type="button" value="Print This" name="submitForm">

                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <input type="submit" name="ctl00$ContainerBody$btnSubmit" value="Cancel" onclick="return validateform();" id="ctl00_ContainerBody_btnSubmit" class="btn btn-block btn-primary">
                                                 
                                            </div>
                                        </div>

                                    </div>

                                    


                                </div>


                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                         
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label> Note : Kindly print the voucher and confirm by signing Authority after that you can submit.</label>
                                </div>
                            </div>
                              
                    </div>
                </div>
                

            </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

