<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Journal-voucher.aspx.cs" Inherits="mis_Finance_Journal_voucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    
    <div class="content-wrapper">
        <section class="content-header">
                <h1>
                    Journal Voucher
                    <small></small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                </ol>
            </section>
            <section class="content">
                <div class="box box-pramod" style="background-color: #f7ebda;"> <!--style="background-color: #f7ebda; "-->
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset class="box-body">
                                    <legend>Voucher Detail </legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Voucher No <span style="color: red;">*</span></label>
                                                        <input name="cttxtDOB" type="text" id="txtDOB" class="form-control" placeholder="Enter Voucher No" />
                                                    </div>
                                                </div>

                                                <div class="col-md-5"></div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Voucher Date <span style="color: red;">*</span></label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <input name="ctl00$ContainerBody$txtDOB" type="text" id="txtDOB" class="form-control" autocomplete="off" placeholder="Enter Date" data-provide="datepicker" onpaste="return false">
                                                        </div>

                                                    </div>
                                                </div>


                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset class="box-body">
                                                        <legend>DR </legend>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div ng-app="MyApp" ng-controller="MyController">
                                                                    <table cellpadding="0" cellspacing="0">

                                                                        <tfoot>
                                                                            <tr>

                                                                                <td style="width:14%; padding-left:5px; margin-top:3px;">
                                                                                    <label ng-model="lblLedger">Ledger :</label>
                                                                                </td>
                                                                                <td>
                                                                                    <select class="form-control" ng-model="Ledger">
                                                                                        <option selected="selected" ng-selected="selected">Select</option>
                                                                                        <option>UNIFORM TO EMPLOYEES WELFARE - (221401)</option>
                                                                                        <option>6TH PAY COMMISSION ARREAR</option>
                                                                                        <option>ABD Publishers Printers Pvt. ltd. Nagpur</option>
                                                                                        <option>Accommodation Vice Chairman</option>
                                                                                        <option>ADMINSTRATIVE CHARGES E.P.F</option>
                                                                                        <option>Advance - Book Purchase (Opening)</option>
                                                                                        <option>Advance - Capital Project</option>
                                                                                        <option>Advance - DPI(Transportation)</option>
                                                                                        <option>Advance - Festival</option>
                                                                                        <option>Advance - House BLD / Plot</option>
                                                                                        <option>Advance - M.P.E.B.</option>
                                                                                        <option>Advance - Medical</option>
                                                                                        <option>Advance - Sales to be collected</option>
                                                                                        <option>Advance - Vehicles</option>
                                                                                        <option>Advance -Grain</option>
                                                                                        <option>Advance Against Contigency - Bhopal</option>
                                                                                    </select>
                                                                                </td>
                                                                                <td style="width: 14%; padding-left: 5px; margin-top: 3px;">
                                                                                    <label ng-model="lblAmount"> Dr Amount :</label>
                                                                                </td>
                                                                                <td>
                                                                                    <input type="text" id="txtabc" class="form-control" onkeyup="abc();" placeholder="Enter Amount rs.">
                                                                                </td>

                                                                                <td style="padding-left: 10px;"><input type="button" class="btn btn-block btn-primary" ng-click="Add()" value="Add" /></td>
                                                                                <td><div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>Total Dr:</label>
                                                        <label id="lbl1" class="form-control">40000</label>
                                                    </div>
                                                </div></td>
                                                                            </tr>
                                                                            
                                                                        </tfoot>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset class="box-body">
                                                        <legend>CR </legend>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div ng-app="MyApp" ng-controller="MyController">
                                                                    <table cellpadding="0" cellspacing="0">

                                                                        <tfoot>
                                                                            <tr>

                                                                                <td style="width:14%; padding-left:5px; margin-top:3px;">
                                                                                    <label ng-model="lblLedger">Ledger :</label>
                                                                                </td>
                                                                                <td>
                                                                                    <select class="form-control" ng-model="Ledger">
                                                                                        <option selected="selected" ng-selected="selected">Select</option>
                                                                                        <option>UNIFORM TO EMPLOYEES WELFARE - (221401)</option>
                                                                                        <option>6TH PAY COMMISSION ARREAR</option>
                                                                                        <option>ABD Publishers Printers Pvt. ltd. Nagpur</option>
                                                                                        <option>Accommodation Vice Chairman</option>
                                                                                        <option>ADMINSTRATIVE CHARGES E.P.F</option>
                                                                                        <option>Advance - Book Purchase (Opening)</option>
                                                                                        <option>Advance - Capital Project</option>
                                                                                        <option>Advance - DPI(Transportation)</option>
                                                                                        <option>Advance - Festival</option>
                                                                                        <option>Advance - House BLD / Plot</option>
                                                                                        <option>Advance - M.P.E.B.</option>
                                                                                        <option>Advance - Medical</option>
                                                                                        <option>Advance - Sales to be collected</option>
                                                                                        <option>Advance - Vehicles</option>
                                                                                        <option>Advance -Grain</option>
                                                                                        <option>Advance Against Contigency - Bhopal</option>
                                                                                    </select>
                                                                                </td>
                                                                                <td style="width:10%; padding-left:5px; margin-top:3px;">
                                                                                    <label ng-model="lblAmount">Cr Amount :</label>
                                                                                </td>
                                                                                <td>
                                                                                    <input name="cttxtDOB" type="text" id="txtDOB" class="form-control" ng-model="Amount" placeholder="Enter Amount Rs.">
                                                                                     
                                                                                </td>
                                                                                <td style="padding-left: 10px;"><input type="button" class="btn btn-block btn-primary" ng-click="Add()" value="Add" /></td>
                                                                                <td><div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>Total Cr :</label>
                                                        <label id="lbl1" class="form-control">50000</label>
                                                    </div>
                                                </div></td>
                                                                            </tr>


                                                                        </tfoot>
                                                                    </table>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-8">
                                                    <div class="form-group">
                                                        <label>Narration :</label>
                                                        <textarea placeholder="Enter Narration" class="form-control" style=""></textarea>
                                                    </div>
                                                </div>

                                                    

                                                

                                            </div> 

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset class="box-body">

                                                        <legend>Action </legend>
                                                        <div class="col-md-2">
                                                            <div class="form-group">
                                                                <input type="submit" name="ctl00$ContainerBody$btnSubmit" value="Accept" onclick="return validateform();" id="ctl00_ContainerBody_btnSubmit" class="btn btn-block btn-primary">
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class="form-group">
                                                                <input type="submit" name="ctl00$ContainerBody$btnSubmit" value="Cancel" onclick="return validateform();" id="ctl00_ContainerBody_btnSubmit" class="btn btn-block btn-primary">
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>

                    </div>
                </div>
            </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
  <%--  <script>
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


        function abc2() {

            var selectval = $("#selectid12").val();

            if (selectval == 2) {
                $("#myModal2").modal();
            }

        }

        function abc() {
            $("#myModal").modal();

        }
    </script>--%>
</asp:Content>
