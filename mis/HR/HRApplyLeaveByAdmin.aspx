<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRApplyLeaveByAdmin.aspx.cs" Inherits="mis_HR_HRApplyLeaveByAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="css/hrcustom.css" rel="stylesheet" />
    <style>
        .lblBalanceLeave {
            font-size: 18px;
            color: cornflowerblue;
            font-weight: 600;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper" style="min-height: 960px;">
        <section class="content-header">
            <h1>Entry Of Approved Leaves
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">Entry Of Approved Leaves</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12 main-form">
                    <div class="box box-pramod">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:label id="lblMsg" runat="server" text=""></asp:label>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Employee Name<span style="color: red;">*</span></label>
                                        <asp:dropdownlist id="ddlEmployee" runat="server"  class="form-control select2" onselectedindexchanged="ddlEmployee_SelectedIndexChanged" autopostback="true"></asp:dropdownlist>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>अवकाश का प्रकार (Types of Leave)<span class="text-red">*</span></label>
                                        <asp:dropdownlist id="ddlLeaveTpye" runat="server" class="form-control" clientidmode="Static" onchange="Showhide()" onselectedindexchanged="ddlLeaveTpye_SelectedIndexChanged" autopostback="true">
                                        </asp:dropdownlist>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Select Year<span style="color: red;">*</span></label>
                                        <asp:dropdownlist id="ddlFinancial_Year" runat="server" cssclass="form-control" autopostback="true" onselectedindexchanged="ddlLeaveTpye_SelectedIndexChanged">
                                        </asp:dropdownlist>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>दिनांक से (From Date)<span class="text-red">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:textbox id="txtFromDate" runat="server" placeholder="From Date" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" clientidmode="Static"></asp:textbox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>दिनांक तक  (To Date)<span class="text-red">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:textbox id="txtToDate" runat="server" placeholder="To Date" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" clientidmode="Static"></asp:textbox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>
                                <div class="col-md-12">
                                    <asp:label id="lblBalanceLeave" runat="server" text="" cssclass="lblBalanceLeave"></asp:label>
                                    <asp:hiddenfield id="hfBalanceLeave" runat="server" />
                                    <hr />
                                </div>
                                <div class="clearfix"></div>

                            </div>
                            <div class="row">
                                <div id="divLeaveDay">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Leave Day</label>
                                            <asp:dropdownlist id="ddlLeaveDays" runat="server" class="form-control select2">
                                                <asp:ListItem>First Half</asp:ListItem>
                                                <asp:ListItem>Second Half</asp:ListItem>
                                                <asp:ListItem Selected="True">Full Day</asp:ListItem>
                                            </asp:dropdownlist>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label id="lblDoc">
                                            दस्तावेज संलग्न करें (Attach Supporting Document)<span id="spnAsterisk" style="position: absolute; right: 13%; top: 0px; color: red">*</span>
                                        </label>
                                        <div class="form-group">
                                            <asp:fileupload id="EmpLeaveDoc" cssclass="form-control" runat="server" clientidmode="Static" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*XLS*XLSX*DOC*DOCX', this),ValidateFileSize(this)" />
                                            <span style="font-size: 10px; color: blue;">Only JPEG/PNG/JPG/PDF/DOC/DOCX Formats are allowed.<br />
                                                Maximum Allowed Filesize (2MB)
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Order Date<span class="text-red">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:textbox id="txtOrderDate" runat="server" placeholder="To Date" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" clientidmode="Static"></asp:textbox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Order No<span class="text-red">*</span></label>
                                        <div class="">
                                            <asp:textbox id="txtOrderNo" runat="server" placeholder="Order No" class="form-control" autocomplete="off" clientidmode="Static"></asp:textbox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label id="lblReason">छुट्टी का कारण (Reason Of Leave)</label>
                                        <asp:textbox id="txtLeaveRemark" textmode="MultiLine" rows="3" class="form-control" runat="server" placeholder="Reason Of Leave"></asp:textbox>
                                        <span class="text-red text-error"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:button id="applyleave" class="btn btn-block btn-success" runat="server" text="Save Leave Record" onclick="applyleave_Click" onclientclick="return validateLeaveForm();" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a class="btn btn-block btn-default" href="HRApplyLeave.aspx">Clear</a>
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
    <script type="text/javascript">
        $(document).ready(function () {
            Showhide();
        });

        function Showhide() {
            //debugger;
            var FromDate = "";
            var ToDate = "";
            var e = document.getElementById("ddlLeaveTpye");
            var strUser = e.options[e.selectedIndex].value;
            var RadioValue
            FromDate = document.getElementById('<%=txtFromDate.ClientID%>').value.trim();
            ToDate = document.getElementById('<%=txtToDate.ClientID%>').value.trim();
            $("div[id*=divLeaveDay]").hide();
            if (strUser == 1 || strUser == 3) {
                if (FromDate == ToDate) {
                    $("div[id*=divLeaveDay]").show();
                }
            }
            else {
                $("div[id*=divLeaveDay]").hide();
            }
            if (strUser == 3) {
                document.getElementById("spnAsterisk").style.display = 'block';
                //spanid.show();

            }
            else {
                document.getElementById("spnAsterisk").style.display = 'none';
                //spanid.hide();
            }
        }


        function Radiobutton() {
            var LeaveDays = "";
            LeaveDays = $('input[name=RbLeaveDays]:checked').text;
            if (LeaveDays == "Half Day") {

            }
        }
        function validateLeaveForm() {
            var msg = "";
            var LeaveType = "";
            debugger;
            if (document.getElementById('<%=ddlLeaveTpye.ClientID%>').selectedIndex == 0) {
                msg = msg + "कृपया अवकाश का प्रकार चुनें | \n";
            }
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "कृपया कर्मचारी चुनें | \n";
            }

            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "कृपया दिनांक से चुने | \n";
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg += "कृपया दिनांक तक चुने | \n";
            }

            if (document.getElementById('<%=txtOrderDate.ClientID%>').value.trim() == "") {
                msg += "कृपया  आदेश दिनांक चुनें | \n";
            }
            if (document.getElementById('<%=txtOrderNo.ClientID%>').value.trim() == "") {
                msg += "कृपया आदेश क्रमांक भरें | \n";
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (confirm("Do you really want to apply leave.?")) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }
        $('#txtFromDate').change(function () {
            //debugger;
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');

            if ($('#txtToDate').val() != "") {
                if (start > end) {

                    if ($('#txtFromDate').val() != "") {
                        alert("From date should not be greater than To Date.");
                        $('#txtFromDate').val("");
                    }
                }
            }
            Showhide();
        });
        $('#txtToDate').change(function () {
            //debugger;
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');

            if (start > end) {

                if ($('#txtToDate').val() != "") {
                    alert("To Date can not be less than From Date.");
                    $('#txtToDate').val("");
                }
            }
            Showhide();
        });

        function UploadControlValidationForLenthAndFileFormat(maxLengthFileName, validFileFormaString, that) {
            //ex---------------
            //maxLengthFileName=50;
            //validFileFormaString=JPG*JPEG*PDF*DOCX
            //uploadControlId=upSaveBill
            //ex---------------
            var msg = '';
            if (document.getElementById(that.id).value != '') {
                var size = document.getElementById(that.id);

                var fileName = document.getElementById(that.id).value;
                var lengthFileName = parseInt(document.getElementById(that.id).value.length)

                var fileExtacntionArray = new Array();
                fileExtacntionArray = fileName.split('.');

                if (fileExtacntionArray.length == 2) {

                    var fileExtacntion = fileExtacntionArray[fileExtacntionArray.length - 1];


                    if (lengthFileName >= parseInt(maxLengthFileName) + parseInt(1)) {
                        msg += '- File name should be less than ' + maxLengthFileName + ' characters. \n';
                    }
                    for (i = 0; i <= (fileName.length - 1) ; i++) {
                        var charFileName = '';

                        charFileName = fileName.substring(i, i + 1);

                        if ((charFileName == '~') || (charFileName == '!') || (charFileName == '@') || (charFileName == '#') || (charFileName == '$') || (charFileName == '%') || (charFileName == '&') || (charFileName == '*') || (charFileName == '{') || (charFileName == '}') || (charFileName == '|') || (charFileName == '<') || (charFileName == '>') || (charFileName == '?')) {

                            msg += '- Special character not allowed in file name. \n';
                            break;
                        }

                    }
                    var isFileFormatCorrect = false;
                    var strValidFormates = '';

                    if (validFileFormaString != "") {

                        var fileFormatArray = new Array();
                        fileFormatArray = validFileFormaString.split('*');

                        for (var j = 0; j < fileFormatArray.length; j++) {
                            if (fileFormatArray[j].toUpperCase() == fileExtacntion.toUpperCase()) {
                                isFileFormatCorrect = true;
                            }

                            if (j == fileFormatArray.length - 1) {
                                strValidFormates += '.' + fileFormatArray[j].toLowerCase();

                            }
                            else {
                                strValidFormates += '.' + fileFormatArray[j].toLowerCase() + '/';

                            }
                        }

                        if (isFileFormatCorrect == false) {
                            msg += '- File format is not correct (only ' + strValidFormates + ').\n';
                        }
                    }

                }
                else {
                    msg += '- File name is incorrect';
                }
                if (msg != '') {
                    document.getElementById(that.id).value = "";
                    alert(msg);
                    return false;
                }
                else {
                    return true;
                }

            }
        }
        function ValidateFileSize(a) {

            var uploadcontrol = document.getElementById(a.id);
            if (uploadcontrol.files[0].size > 2097152) {
                alert('File size should not greater than 2 mb.');
                document.getElementById(a.id).value = '';
                return false;
            }
            else {
                return true;
            }

        }
    </script>
</asp:Content>

