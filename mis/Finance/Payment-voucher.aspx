<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Payment-voucher.aspx.cs" Inherits="mis_Finance_Payment_voucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content-header">
            <h1>Payment Voucher
                    <small></small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            </ol>
        </section>
        <section class="content">
            <div class="box box-pramod" style="background-color: #FFFFD9;">
                <!--style="background-color: #FFFFD9;"-->
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
                                                <div class="box-body">
                                                    <!--<legend></legend>-->
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <fieldset class="box-body">
                                                                <legend>DR </legend>
                                                                <div ng-app="MyApp" ng-controller="MyController">
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tbody ng-repeat="m in Customers">
                                                                        </tbody>
                                                                        <tfoot>
                                                                            <tr>

                                                                                <td style="width: 10%; padding-left: 5px; margin-top: 3px;">
                                                                                    <label ng-model="lblLedger">Ledger :</label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" ID="ddlLedger" OnSelectedIndexChanged="ddlLedger_SelectedIndexChanged">
                                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                                        <asp:ListItem>Goods Ledger 1</asp:ListItem>
                                                                                        <asp:ListItem>Goods Ledger 2</asp:ListItem>
                                                                                        <asp:ListItem>Service</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td style="width: 10%; padding-left: 5px; margin-top: 3px;">
                                                                                    <label ng-model="lblAmount">Amount Rs. :</label>
                                                                                </td>
                                                                                <td>

                                                                                    <input type="text" id="txtabc" class="form-control" onkeyup="abc();" placeholder="Enter Amount rs.">
                                                                                </td>
                                                                                <td style="padding-left: 10px;">
                                                                                    <input type="button" class="btn btn-block btn-primary" ng-click="Add()" value="Add" /></td>
                                                                                <td style="text-align: right;"><b>Total : 00.00 Rs.</b></td>
                                                                            </tr>
                                                                        </tfoot>
                                                                    </table>

                                                                </div>
                                                            </fieldset>
                                                            <fieldset class="box-body">
                                                                <legend>CR </legend>
                                                                <div ng-app="MyApp" ng-controller="MyController">
                                                                    <table cellpadding="0" cellspacing="0">
                                                                        <tbody ng-repeat="m in Customers">
                                                                        </tbody>
                                                                        <tfoot>
                                                                            <tr>

                                                                                <td style="width: 10%; padding-left: 5px; margin-top: 3px;">
                                                                                    <label ng-model="lblLedger">Ledger :</label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" ID="DropDownList1" OnSelectedIndexChanged="ddlLedger_SelectedIndexChanged">
                                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                                        <asp:ListItem>Goods Ledger 1</asp:ListItem>
                                                                                        <asp:ListItem>Goods Ledger 2</asp:ListItem>
                                                                                        <asp:ListItem>Service</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                </td>
                                                                                <td style="width: 10%; padding-left: 5px; margin-top: 3px;">
                                                                                    <label ng-model="lblAmount">Amount Rs. :</label>
                                                                                </td>
                                                                                <td>

                                                                                    <input type="text" id="txtabc" class="form-control" onkeyup="abc();" placeholder="Enter Amount rs.">
                                                                                </td>
                                                                                <td style="padding-left: 10px;">
                                                                                    <input type="button" class="btn btn-block btn-primary" ng-click="Add()" value="Add" /></td>
                                                                                <td style="text-align: right;"><b>Total : 00.00 Rs.</b></td>
                                                                            </tr>
                                                                        </tfoot>
                                                                    </table>

                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <fieldset class="box-body">
                                                                            <legend>Bank Detail </legend>
                                                                            <div class="row">
                                                                                <div class="col-md-2">
                                                                                    <div class="form-group">
                                                                                        <input type="radio" checked="checked" value="" name="A" />Cash
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <div class="form-group">
                                                                                        <input type="radio" value="" name="A" />Bank
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-2">
                                                                                    <div class="form-group">
                                                                                        <input type="radio" value="" name="A" />E-Payment
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row">
                                                                                <div class="col-md-12">

                                                                                    <div class="row">
                                                                                        <div class="col-md-3">
                                                                                            <div class="form-group">
                                                                                                <label>Bank Name : <span style="color: red;">*</span></label>
                                                                                                <select class="form-control">
                                                                                                    <option value="volvo">Allahabad Bank, HO</option>
                                                                                                    <option value="saab">State Bank Of India, HO</option>
                                                                                                    <option value="mercedes">Allahabad Bank Sweep Transfer Account</option>
                                                                                                    <option value="audi">Vidisha Bhopal Gramin Bank</option>
                                                                                                </select>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-3">
                                                                                            <div class="form-group">
                                                                                                <label>Account No : <span style="color: red;">*</span></label>
                                                                                                <input name="cttxtDOB" readonly="true" type="text" id="txtDOB" class="form-control" ng-model="Amount1" placeholder="Enter Account No">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-3">
                                                                                            <div class="form-group">
                                                                                                <label>Cheque/DD No : <span style="color: red;">*</span></label>
                                                                                                <input name="cttxtDOB" type="text" id="txtDOB" class="form-control" ng-model="Amount" placeholder="Enter Amount Rs.">
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-3">
                                                                                            <div class="form-group">
                                                                                                <label>Total Payable :</label>
                                                                                                <label id="lbl1" class="form-control">50000</label>
                                                                                            </div>
                                                                                        </div>





                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                        </fieldset>
                                                                    </div>
                                                                </div>
                                                            </fieldset>
                                                        </div>
                                                    </div>



                                                    <div class="modal fade" id="myModal" role="dialog">
                                                        <div class="modal-dialog" style="width: 60%;">

                                                            <!-- Modal content-->
                                                            <div class="modal-content">
                                                                <div class="modal-header">
                                                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                    <h4 class="modal-title">Bill-Wise Breckup Of : MPAGRO</h4>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label>Type Of Ref</label>
                                                                            <select class="form-control" onchange="abc2();" id="selectid12">
                                                                                <option value="1">Agst Ref</option>
                                                                                <option value="2">New Ref</option>
                                                                            </select>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label>Name</label>

                                                                            <label class="form-control">Bill1</label>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label>Due Date</label>
                                                                            <label class="form-control">31-Apr-2018</label>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label>Balance</label>
                                                                            <label class="form-control">3000 Cr</label>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label>Final Balance</label>
                                                                            <label class="form-control">2000 Cd</label>


                                                                        </div>
                                                                    </div>


                                                                    <div class="modal-footer">
                                                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                                    </div>

                                                                </div>


                                                            </div>

                                                        </div>
                                                    </div>


                                                    <div class="modal fade" id="myModal2" role="dialog">
                                                        <div class="modal-dialog" style="width: 80%;">

                                                            <!-- Modal content-->
                                                            <div class="modal-content">
                                                                <div class="modal-header">
                                                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                                    <h4 class="modal-title">Bill-Wise Breckup Of : MPAGRO</h4>
                                                                </div>
                                                                <div class="modal-body">
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label>Date <span style="color: red;">*</span></label>
                                                                            <div class="input-group date">
                                                                                <div class="input-group-addon">
                                                                                    <i class="fa fa-calendar"></i>
                                                                                </div>
                                                                                <input name="ctl00$ContainerBody$txtDOB" type="text" id="txtDOB" class="form-control" autocomplete="off" placeholder="Enter Date" data-provide="datepicker" onpaste="return false">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label>Name <span style="color: red;">*</span></label>
                                                                            <input name="ctl00$ContainerBody$txtEmp_ID" type="text" maxlength="50" id="txtEmp_ID" class="form-control" placeholder="Enter Name" />

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label>Due Date<span style="color: red;">*</span></label>
                                                                            <div class="input-group date">
                                                                                <div class="input-group-addon">
                                                                                    <i class="fa fa-calendar"></i>
                                                                                </div>
                                                                                <input name="ctl00$ContainerBody$txtDOB" type="text" id="txtDOB" class="form-control" autocomplete="off" placeholder="Enter Due Date" data-provide="datepicker" onpaste="return false">
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label>Amount<span style="color: red;">*</span></label>
                                                                            <input name="ctl00$ContainerBody$txtEmp_ID" type="text" maxlength="50" id="txtEmp_ID" class="form-control" placeholder="Enter Amount" />

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label>Dr / Cr <span style="color: red;">*</span></label>
                                                                            <select name="ctl00$ContainerBody$ddlMaritalStatus" id="ctl00_ContainerBody_ddlMaritalStatus" class="form-control">
                                                                                <option value="1">Dr</option>
                                                                                <option value="2">Cr</option>

                                                                            </select>


                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <div class="form-group">
                                                                            <label>Action <span style="color: red;">*</span></label>
                                                                            <input type="button" class="btn btn-block btn-primary" ng-click="Add()" value="Add" />

                                                                        </div>
                                                                    </div>

                                                                    <br />
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label>Date <span style="color: red;">*</span></label>
                                                                            <div class="input-group date">
                                                                                <div class="input-group-addon">
                                                                                    <i class="fa fa-calendar"></i>
                                                                                </div>
                                                                                <input name="ctl00$ContainerBody$txtDOB" type="text" id="txtDOB" class="form-control" autocomplete="off" placeholder="Enter Date" data-provide="datepicker" onpaste="return false">
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label>Name <span style="color: red;">*</span></label>
                                                                            <input name="ctl00$ContainerBody$txtEmp_ID" type="text" maxlength="50" id="txtEmp_ID" class="form-control" placeholder="Enter Name" />

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label>Due Date<span style="color: red;">*</span></label>
                                                                            <div class="input-group date">
                                                                                <div class="input-group-addon">
                                                                                    <i class="fa fa-calendar"></i>
                                                                                </div>
                                                                                <input name="ctl00$ContainerBody$txtDOB" type="text" id="txtDOB" class="form-control" autocomplete="off" placeholder="Enter Due Date" data-provide="datepicker" onpaste="return false">
                                                                            </div>

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label>Amount<span style="color: red;">*</span></label>
                                                                            <input name="ctl00$ContainerBody$txtEmp_ID" type="text" maxlength="50" id="txtEmp_ID" class="form-control" placeholder="Enter Amount" />

                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label>Dr / Cr <span style="color: red;">*</span></label>
                                                                            <select name="ctl00$ContainerBody$ddlMaritalStatus" id="ctl00_ContainerBody_ddlMaritalStatus" class="form-control">
                                                                                <option value="1">Dr</option>
                                                                                <option value="2">Cr</option>

                                                                            </select>


                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-1">
                                                                        <div class="form-group">
                                                                            <label>Action <span style="color: red;">*</span></label>
                                                                            <input type="button" class="btn btn-block btn-primary" ng-click="Add()" value="Add" />

                                                                        </div>
                                                                    </div>


                                                                    <div class="modal-footer">
                                                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                                                    </div>

                                                                </div>


                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <div class="col-md-9">
                                                            <div class="form-group">
                                                                <label>Narration :</label>
                                                                <textarea placeholder="Enter Narration" class="form-control" style=""></textarea>
                                                            </div>
                                                        </div>



                                                    </div>


                                                </div>
                                            </div>
                                        </div>





                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="box-body">

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

                                                </div>
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
        <div id="ModalVendor" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Vendor Detail</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Name</label>
                                    <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>GST No.</label>
                                    <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>TAN No.</label>
                                    <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>Address</label>
                                    <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-primary" Text="Save" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>

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


        function abc2() {

            var selectval = $("#selectid12").val();

            if (selectval == 2) {
                $("#myModal2").modal();
            }

        }
        function abc() {
            $("#myModal").modal();

        }
        function CallModalVendor() {
            $("#ModalVendor").modal();

        }

    </script>
</asp:Content>
