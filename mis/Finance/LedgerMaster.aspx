<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="LedgerMaster.aspx.cs" Inherits="mis_Finance_LedgerMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .material-switch > input[type="checkbox"] {
            display: none;
        }

        .material-switch > label {
            cursor: pointer;
            height: 0px;
            position: relative;
            width: 40px;
        }

            .material-switch > label::before {
                background: rgb(0, 0, 0);
                box-shadow: inset 0px 0px 10px rgba(0, 0, 0, 0.5);
                border-radius: 8px;
                content: '';
                height: 16px;
                margin-top: -8px;
                position: absolute;
                opacity: 0.3;
                transition: all 0.4s ease-in-out;
                width: 40px;
            }

            .material-switch > label::after {
                background: rgb(255, 255, 255);
                border-radius: 16px;
                box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.3);
                content: '';
                height: 24px;
                left: -4px;
                margin-top: -8px;
                position: absolute;
                top: -4px;
                transition: all 0.3s ease-in-out;
                width: 24px;
            }

        .material-switch > input[type="checkbox"]:checked + label::before {
            background: inherit;
            opacity: 0.5;
        }

        .material-switch > input[type="checkbox"]:checked + label::after {
            background: inherit;
            left: 20px;
        }

        .customCSS td {
            padding: 0px !important;
        }

        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Ledger Master</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Ledger Detail</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Group Name<span style="color: red;"> *</span></label>
                                            <asp:DropDownList runat="server" CssClass="form-control select2" ID="ddlHeadName" OnSelectedIndexChanged="ddlHeadName_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem>Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <small><span id="valddlHeadName" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label>Ledger Name<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Ledger Name" ID="txtLedgerName" MaxLength="50"></asp:TextBox>
                                            <small><span id="valtxtLedgerName" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Alias</label>
                                            <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Alias" ID="txtLedgerAlias" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <fieldset>
                                <legend>Bank Account Details</legend>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>A/c Holders Name<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" placeholder="Enter A/C Holders Name" ID="txtachlder_name" CssClass="form-control" MaxLength="50" onkeypress="return validatename(event);"></asp:TextBox>
                                            <small><span id="valtxtachlder_name" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>A/c No <span style="color: red;">*</span></label>
                                            <asp:TextBox runat="server" placeholder="Enter A/c No" ID="txtacntno" CssClass="form-control" MaxLength="18" onkeypress="return validateNum(event);"></asp:TextBox>
                                            <small><span id="valtxtacntno" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>IFSC code <span style="color: red;">*</span></label>
                                            <asp:TextBox runat="server" ID="txtifsccode" placeholder="Example: SBIN0000058" CssClass="form-control IFSC" MaxLength="12" onkeypress="return alpha(event);"></asp:TextBox>
                                            <small><span id="valtxtifsccode" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Bank Name<span style="color: red;">*</span></label>
                                            <asp:TextBox runat="server" placeholder="Enter Bank Name" ID="txtbankname" CssClass="form-control" MaxLength="50" ClientIDMode="Static" onkeypress="return validatename(event);"></asp:TextBox>
                                            <small><span id="valtxtbankname" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Branch<span style="color: red;">*</span></label>
                                            <asp:TextBox runat="server" placeholder="Enter Branch" ID="txtbranch" CssClass="form-control" MaxLength="50" onkeypress="return validatename(event);"></asp:TextBox>
                                            <small><span id="valtxtbranch" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="col-md-6">
                            <fieldset>
                                <legend>Mailing Details</legend>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Name<span style="color: red;"> *</span></label>
                                            <asp:TextBox runat="server" placeholder="Enter Mailing Name" ID="txtMailing_Name" CssClass="form-control" MaxLength="50" onkeypress="return validatename(event);"></asp:TextBox>
                                            <small><span id="valtxtMailing_Name" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>State <span style="color: red;">*</span></label>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlState">


                                                <asp:ListItem>Select</asp:ListItem>
                                            </asp:DropDownList>
                                            <small><span id="valddlState" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Address <span style="color: red;">*</span></label>
                                            <asp:TextBox runat="server" ID="txtMailing_Address" placeholder="Enter Mailing Address" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                            <small><span id="valtxtMailing_Address" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-6" id="pinno" runat="server" visible="true">
                                        <div class="form-group">
                                            <label>PinCode. <span style="color: red;">*</span></label>
                                            <asp:TextBox runat="server" placeholder="Enter PinCode" ID="txtpincode" CssClass="form-control PinCode" MaxLength="6" onkeypress="return validateNum(event)"></asp:TextBox>
                                            <small><span id="valtxtpincode" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset>
                                <legend>Tax Registration Details</legend>
                                <div class="row">
                                   
                                    <div class="col-md-3" id="panno" runat="server" visible="true">
                                        <div class="form-group">
                                            <label>PAN/IT No. <span style="color: red;">*</span></label>
                                            <asp:TextBox runat="server" placeholder="Example: ABCPE1234F" ID="txtMailing_PanNo" CssClass="form-control PanCard" MaxLength="10"></asp:TextBox>
                                            <small><span id="valtxtMailing_PanNo" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Registration Types<span style="color: red;">*</span></label>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRegistrationType">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem>Composition</asp:ListItem>
                                                <asp:ListItem>Consumer</asp:ListItem>
                                                <asp:ListItem>Regular</asp:ListItem>
                                                <asp:ListItem>Unregistered</asp:ListItem>
                                            </asp:DropDownList>
                                            <small><span id="valddlRegistrationType" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                     <div class="col-md-3" id="gstno" runat="server" visible="true">
                                        <div class="form-group">
                                            <label>GST No. <span style="color: red;" id="gstvisible" runat="server"  visible="false">*</span></label>
                                            <asp:TextBox runat="server" placeholder="Enter GST No." ID="txtGSTNo" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                            <small><span id="valtxtGSTNo" style="color: red;"></span></small>
                                        </div>

                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Maintain Balances BillByBill<span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlBalBillByBill" runat="server" CssClass="form-control">
                                                <asp:ListItem>Select</asp:ListItem>
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No">No</asp:ListItem>
                                            </asp:DropDownList>
                                             <small><span id="valddlBalBillByBill" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="inventoryvalue" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Inventory Value are affected<span style="color: red;"> *</span></label>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlInventoryAffected">
                                                <asp:ListItem Value="NA">Select</asp:ListItem>
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No">No</asp:ListItem>
                                            </asp:DropDownList>
                                            <small><span id="valddlInventoryAffected" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3" id="effectivedate" runat="server" visible="false">
                                        <div class="form-group">
                                            <label>Effective Date for Reconsilation<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtEffectivedate" AutoComplete="off" placeholder="Select Date" data-date-start-date="0d" ClientIDMode="Static" runat="server" class="form-control DateAdd"></asp:TextBox>
                                            </div>
                                            <small><span id="valtxtEffectivedate" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                   
                                    
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Opening Balance</legend>
                                <div class="row">
                                     <div class="col-md-3" runat="server">
                                        <div class="form-group">
                                            <label>Financial Year <span style="color: red;">*</span></label>
                                            <asp:DropDownList runat="server" Enabled="false" CssClass="form-control" ID="ddlFinancialYear">
                                            </asp:DropDownList>
                                            <small><span id="valddlFinancialYear" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Opening Balance </label>
                                            <asp:TextBox runat="server" CssClass="form-control" onkeypress="return validateNum(event);" placeholder="Enter Opening Balance" ID="txtOpeningBalance"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Dr./Cr.</label>
                                            <asp:DropDownList runat="server" ID="ddlDrCr" CssClass="form-control">
                                                <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                                <asp:ListItem Value="Debit">Debit</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">

                            <small><span id="valchkOffice" style="color: red;"></span></small>
                            <fieldset class="box-body">
                                <legend>Applicable on<span class="text-danger">*</span></legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <asp:CheckBox ID="chkOfficeAll" runat="server" CssClass="chkrow" Text="ALL" onclick="CheckOfficeAll();" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:CheckBoxList ID="chkOffice" runat="server" ClientIDMode="Static" CssClass="table customCSS cbl_all_Office" onclick="ClearAll();"  RepeatColumns="5" RepeatDirection="Horizontal">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-block btn-success" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2" id="clear" runat="server">
                            <div class="form-group">
                                <a href="LedgerMaster.aspx" class="btn btn-block btn-default">Clear</a>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Ledger_ID").ToString()%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Ledger_Name" HeaderText="Ledger_Name" />
                            <%--   <asp:BoundField DataField="Office_ContactNo" HeaderText="Office Name" />
                            <asp:BoundField DataField="Office_Email" HeaderText="Office Email" />
                            <asp:BoundField DataField="OfficeType_Title" HeaderText="Office Type Title" />
                            <asp:BoundField DataField="Office_Address" HeaderText="Office Address" />
                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </section>
        <!-- Modal -->
        <%--<div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-lg">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Opening Balance Detail</h4>
                    </div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Opening Balance Detail</legend>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date</label>
                                        <asp:TextBox runat="server" CssClass="form-control" placeholder="Select Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Bill No. / Particular</label>
                                        <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Bill No. / Particular"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Item Detail</label>
                                        <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Item Detail"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Opening Balance</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtOpeningBalance" placeholder="Enter Opening Balance"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label>Dr/Cr</label>
                                        <asp:RadioButtonList runat="server">
                                            <asp:ListItem Selected="True">Dr</asp:ListItem>
                                            <asp:ListItem>Cr</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-10"></div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-info btn-block" Text="Add" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <table class="table table-bordered table-striped table-hover">
                                            <tr>
                                                <th>SNo.</th>
                                                <th>Bill No. / Particular</th>
                                                <th>Item Detail</th>
                                                <th>Opening Balance</th>
                                                <th>Dr/Cr</th>
                                            </tr>
                                           <%-- <tr>
                                                <td colspan="4">Total</td>
                                                <td>0</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        $('.PanCard').blur(function () {
            debugger;
            var Obj = $('.PanCard').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
                var code = /([C,P,H,F,A,T,B,L,J,G])/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(panPat) == -1) {
                    alert("Invalid Pan No");
                    //message_error("Error", "Invalid Pan Card.");
                    //Obj.focus();
                    $('.PanCard').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild PAN Card No.");
                    //message_error("Error", "Invalid Pan Card.");
                    $('.PanCard').val('');
                    return false;
                }
            }
        });
        $('.PinCode').blur(function () {
            var Obj = $('.PinCode').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                var panPat = /^([1-9]{1})([0-9]{5})$/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(panPat) == -1) {
                    alert("Invalid Pin No");
                    //message_error("Error", "Invalid Pan Card.");
                    //Obj.focus();
                    $('.PinCode').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild Pin No.");
                    //message_error("Error", "Invalid Pan Card.");
                    $('.PinCode').val('');
                    return false;
                }
            }
        });
        $('.IFSC').blur(function () {
            debugger;
            var Obj = $('.IFSC').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                var IFSC = /^[A-Za-z]{4}[0]{1}[0-9A-Za-z]{6}$/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(IFSC) == -1) {
                    alert("Invalid IFSC Code");
                    //message_error("Error", "Invalid IFSC Code.");
                    //Obj.focus();
                    $('.IFSC').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild IFSC Code.");
                    //message_error("Error", "Invalid IFSC Code.");
                    $('.IFSC').val('');
                    return false;
                }

                if ($('.IFSC').val.length != 11) {
                    alert("Invaild IFSC Code.");
                    return false;
                }
            }
        });
        //$('.AccountNo').blur(function () {
        //    var Obj = $('.AccountNo').val();
        //    if (Obj == null) Obj = window.event.srcElement;
        //    if (Obj != "") {
        //        ObjVal = Obj;
        //        var panPat = /^([1-9]{1})([0-9]{5})$/;
        //        var code_chk = ObjVal.substring(3, 4);
        //        if (ObjVal.search(panPat) == -1) {
        //            alert("Invalid Account No");
        //            //message_error("Error", "Invalid Pan Card.");
        //            //Obj.focus();
        //            $('.AccountNo').val('');
        //            return false;
        //        }
        //        if (code.test(code_chk) == false) {
        //            alert("Account No.");
        //            //message_error("Error", "Invalid Pan Card.");
        //            $('.AccountNo').val('');
        //            return false;
        //        }
        //    }
        //});
        function CheckOfficeAll() {
            if (document.getElementById('<%=chkOfficeAll.ClientID%>').checked == true) {
                $('.cbl_all_Office').each(function () {

                    $(this).closest('table').find('input[type=checkbox]').prop('checked', true);
                });
            }
            else {
                $('.cbl_all_Office').each(function () {
                    $(this).closest('table').find('input[type=checkbox]').prop('checked', false);
                });
            }
            return false;
        }
        function ClearAll() {
            var intIndex = 0;
            var flag = 0;
            var rowCount = document.getElementById('chkOffice').getElementsByTagName("input").length;
            for (intIndex = 0; intIndex < rowCount; intIndex++) {
                if (document.getElementById("chkOffice" + "_" + intIndex)) {
                    if (document.getElementById("chkOffice" + "_" + intIndex).checked == true) {
                        flag = 1;
                    }
                    else {
                        flag = 0;
                        break;
                    }
                }
            }
            if (flag == 0)
                document.getElementById('<%=chkOfficeAll.ClientID%>').checked = false;
            else
                document.getElementById('<%=chkOfficeAll.ClientID%>').checked = true;

        }
        function ShowModal() {
            $('#myModal').modal('show');
            return true;
        }
        function validateform() {
            debugger;
            var msg = "";
            $("#valddlHeadName").html("");
            $("#valtxtLedgerName").html("");
            $("#valtxtMailing_Name").html("");
            $("#valddlState").html("");
            $("#valtxtMailing_Address").html("");
            $("#valtxtGSTNo").html("");
            $("#valtxtMailing_PanNo").html("");
            $("#valddlInventoryAffected").html("");
            $("#valddlFinancialYear").html("");
            $("#valchkOffice").html("");
            $("#valtxtacntno").html("");
            $("#valtxtEffectivedate").html("");
            $("#valtxtpincode").html(" ");
            $("#valtxtEffectivedate").html("");
            $("#valtxtachlder_name").html("");
            $("#valtxtifsccode").html("");
            $("#valtxtbankname").html("");
            $("#valtxtbranch").html("");
            $("#valddlRegistrationType").html("");
            $("#valddlBalBillByBill").html("");
            if (document.getElementById('<%=ddlHeadName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Head Name. \n";
                $("#valddlHeadName").html("Select Head Name");
            }
            if (document.getElementById('<%=txtLedgerName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Ledger Name. \n";
                $("#valtxtLedgerName").html("Enter Ledger Name");
            }
            if (document.getElementById('<%=txtachlder_name.ClientID%>').value.trim() == "") {
                msg = msg + "Enter A/c Holders Name. \n";
                $("#valtxtachlder_name").html("Enter A/c Holders Name");
            }
            var element = document.getElementById('<%=txtacntno.ClientID%>');
            if (element != null) {
                if (document.getElementById('<%=txtacntno.ClientID%>').value.trim() == "") {
                    msg = msg + "Enter A/c No. \n";
                    $("#valtxtacntno").html("Enter A/c No");
                }
            }
            if (document.getElementById('<%=txtifsccode.ClientID%>').value.trim() == "") {
                msg = msg + "Enter IFSC code. \n";
                $("#valtxtifsccode").html("Enter IFSC code");
            }
            if (document.getElementById('<%=txtbankname.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Bank Name. \n";
                $("#valtxtbankname").html("Enter Bank Name");
            }
            if (document.getElementById('<%=txtbranch.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Branch. \n";
                $("#valtxtbranch").html("Enter Branch");
            }
           <%-- if (document.getElementById('<%=txtLedgerAlias.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Alias. \n";
                $("#valtxtLedgerAlias").html("Enter Alias");
            }--%>
            if (document.getElementById('<%=txtMailing_Name.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Mailing Name. \n";
                $("#valtxtMailing_Name").html("Enter Mailing Name");
            }
            if (document.getElementById('<%=ddlState.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select State. \n";
                $("#valddlState").html("Select State");
            }
            if (document.getElementById('<%=txtMailing_Address.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Mailing Address. \n";
                $("#valtxtMailing_Address").html("Enter Mailing Address");
            }
            
            var element = document.getElementById('<%=txtpincode.ClientID%>');
            if (element != null) {
                if (document.getElementById('<%=txtpincode.ClientID%>').value.trim() == "") {
                    msg = msg + "Enter Mailing Pincode . \n";
                    $("#valtxtpincode").html("Enter Mailing Pincode");
                }
            }
            var element = document.getElementById('<%=txtMailing_PanNo.ClientID%>');
            if (element != null) {
                if (document.getElementById('<%=txtMailing_PanNo.ClientID%>').value.trim() == "") {
                    msg = msg + "Enter Pan No. \n";
                    $("#valtxtMailing_PanNo").html("Enter Pan No");
                }
            }
            if (document.getElementById('<%=ddlRegistrationType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Registration Types. \n";
                $("#valddlRegistrationType").html("Select Registration Types");
            }
            if (document.getElementById('<%=ddlRegistrationType.ClientID%>').selectedIndex > 0) {
                if (document.getElementById('<%=ddlRegistrationType.ClientID%>').value.trim() == "Composition" || document.getElementById('<%=ddlRegistrationType.ClientID%>').value.trim() == "Regular")
                {
                    //document.getElementById('gstvisible').style.display = 'block';
                    if (document.getElementById('<%=txtGSTNo.ClientID%>').value.trim() == "") {
                        msg = msg + "Enter GST No. \n";
                        $("#valtxtGSTNo").html("Enter GST No");
                    }

                }
               
            }
            if (document.getElementById('<%=ddlBalBillByBill.ClientID%>').selectedIndex == 0)
            {
                msg = msg + "Select Maintain Balances BillByBill.\n";
                $("#valddlBalBillByBill").html("Select Maintain Balances BillByBill");
            }
            <%--var element = document.getElementById('<%=txtGSTNo.ClientID%>');
            if (element != null) {
                if (document.getElementById('<%=txtGSTNo.ClientID%>').value.trim() == "") {
                    msg = msg + "Enter GST No. \n";
                    $("#valtxtGSTNo").html("Enter GST No");
                }
            }--%>

            var element = document.getElementById('<%=ddlInventoryAffected.ClientID%>');
            if (element != null) {
                if (document.getElementById('<%=ddlInventoryAffected.ClientID%>').selectedIndex == 0) {
                    msg = msg + "Select Inventory Value are Affected. \n";
                    $("#valddlInventoryAffected").html("Select Inventory Value are Affected");
                }
            }
            var element = document.getElementById('<%=txtEffectivedate.ClientID%>');
            if (element != null) {
                if (document.getElementById('<%=txtEffectivedate.ClientID%>').value.trim() == "") {
                    msg = msg + "Enter Mailing Effective Date. \n";
                    $("#valtxtEffectivedate").html("Enter Mailing Effective Date");
                }
            }
            if ($('#chkOffice input:checked').length > 0) {

            }
            else {
                //alert('Please select atleast one Group')
                msg += "Select atleast one Office. \n"
                $("#valchkOffice").html("Select atleast one Office");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    if (confirm("Do you really want to Update Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }

                }
            }
        }
    </script>
    <script src="../js/ValidationJs.js"></script>
</asp:Content>



