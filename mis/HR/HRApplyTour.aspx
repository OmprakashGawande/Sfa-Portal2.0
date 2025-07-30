<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRApplyTour.aspx.cs" Inherits="mis_HR_HRApplyTour" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="css/hrcustom.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper" style="min-height: 960px;">
        <section class="content-header">
            <h1>दौरे के लिए आवेदन करें 
        (Apply  for Tour)
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
                <li class="active">दौरे के लिए आवेदन करें  (Apply for Tour)</li>
            </ol>
        </section>
        <section class="content">
            <div class="row">
                <div class="col-md-12 main-form">
                    <div class="box box-pramod">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>टूर का प्रकार (Types of Tour)<span class="text-red">*</span></label>
                                        <asp:DropDownList ID="ddlTourType" runat="server" class="form-control" ClientIDMode="Static" onchange="Showhide()">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>दिनांक से (From Date)<span class="text-red">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFromDate" runat="server" placeholder="From Date" class="form-control DateAdd" autocomplete="off" data-date-start-date="-31d" data-date-end-date="+30d" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>दिनांक तक  (To Date)<span class="text-red">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtToDate" runat="server" placeholder="To Date" data-date-start-date="-31d" data-date-end-date="+30d" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="clearfix"></div>

                            </div>
                            <div class="row">

                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label>स्वीकृत करता अधिकारी (Tour Approval Authority)<span class="text-red">*</span></label>
                                        <asp:DropDownList ID="ddlEmployeeName" runat="server" class="form-control select2">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label id="lblDoc">
                                            दस्तावेज संलग्न करें (Attach Supporting Document)
                                        </label>
                                        <div class="form-group">
                                            <asp:FileUpload ID="EmpTourDoc" CssClass="form-control" runat="server" ClientIDMode="Static" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF', this),ValidateFileSize(this)" />
                                            <span style="font-size: 10px; color: blue;">Only JPEG/PNG/JPG/PDF Formats are allowed.<br />
                                                Maximum Allowed Filesize (2MB)
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label id="lblReason">दौरे का विवरण  (Description Of Tour)<span class="text-red">*</span></label>
                                        <asp:TextBox ID="txtTourRemark" TextMode="MultiLine" Rows="3" class="form-control" runat="server" placeholder="Description Of Tour"></asp:TextBox>
                                        <span class="text-red text-error"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="applytour" class="btn btn-block btn-success" runat="server" Text="Apply For Tour" OnClick="applytour_Click" OnClientClick="return validateTourForm();" />
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

        <%--function Showhide() {
            debugger;
            var FromDate = "";
            var ToDate = "";
            var e = document.getElementById("ddlTourTpye");
            var strUser = e.options[e.selectedIndex].value;
            var RadioValue
            FromDate = document.getElementById('<%=txtFromDate.ClientID%>').value.trim();
            ToDate = document.getElementById('<%=txtToDate.ClientID%>').value.trim();
            $("div[id*=divTourDay]").hide();
            if (strUser == 2) {
                if (FromDate == ToDate) {
                    $("div[id*=divTourDay]").show();
                }
            }
            else {
                $("div[id*=divTourDay]").hide();
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
            var TourDays = "";
            TourDays = $('input[name=RbTourDays]:checked').text;
            if (TourDays == "Half Day") {

            }
        }--%>
        function validateTourForm() {
            var msg = "";
            var TourType = "";
            debugger;
            if (document.getElementById('<%=ddlTourType.ClientID%>').selectedIndex == 0) {
                msg = msg + "कृपया दौरे का प्रकार चुनें | \n";
            }
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "कृपया दिनांक से चुने | \n";
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg += "कृपया दिनांक तक चुने | \n";
            }
            if (document.getElementById('<%=ddlEmployeeName.ClientID%>').selectedIndex == 0) {
                msg = msg + "कृपया कर्मचारी का नाम चुने | \n";
            }
            if (document.getElementById('<%=txtTourRemark.ClientID%>').value.trim() == "") {
                msg += "कृपया दौरे का विवरण भरें | \n";
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (confirm("Do you really want to apply tour.?")) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }
        //$('#txtFromDate').change(function () {
        //    debugger;
        //    var start = $('#txtFromDate').datepicker('getDate');
        //    var end = $('#txtToDate').datepicker('getDate');

        //    if ($('#txtToDate').val() != "") {
        //        if (start > end) {

        //            if ($('#txtFromDate').val() != "") {
        //                alert("From date should not be greater than To Date.");
        //                $('#txtFromDate').val("");
        //            }
        //        }
        //    }
        //    Showhide();
        //});
        //$('#txtToDate').change(function () {
        //    debugger;
        //    var start = $('#txtFromDate').datepicker('getDate');
        //    var end = $('#txtToDate').datepicker('getDate');

        //    if (start > end) {

        //        if ($('#txtToDate').val() != "") {
        //            alert("To Date can not be less than From Date.");
        //            $('#txtToDate').val("");
        //        }
        //    }
        //    Showhide();
        //});

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

