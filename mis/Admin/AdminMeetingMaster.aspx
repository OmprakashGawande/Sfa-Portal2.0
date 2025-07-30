<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminMeetingMaster.aspx.cs" Inherits="mis_Admin_AdminMeetingMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/bootstrap-timepicker.css" rel="stylesheet" />
    <link href="../css/bootstrap-datepicker.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) --
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Meeting Master</h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" ClientIDMode="Static"></asp:Label>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Subject<span style="color: red;"> *</span></label>
                                        <asp:TextBox ID="txtMeeting_Subject" runat="server" CssClass="form-control input-small" ClientIDMode="Static" autocomplete="off" placeholder="Enter Subject" MaxLength="250"></asp:TextBox>
                                        <small><span id="valtxtMeeting_Subject" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Venue<span style="color: red;"> *</span></label>
                                        <asp:TextBox ID="txtMeeting_Venue" runat="server" CssClass="form-control" ClientIDMode="Static" autocomplete="off" placeholder="Enter Venue" MaxLength="250" />
                                        <small><span id="valtxtMeeting_Venue" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Officer Name<span style="color: red;"> *</span></label>
                                        <asp:TextBox ID="txtMeeting_OfficerName" runat="server" CssClass="form-control" ClientIDMode="Static" autocomplete="off" onkeypress="return validatename(event)" MaxLength="100" placeholder="Enter Officer Name" />
                                        <small><span id="valtxtMeeting_OfficerName" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label>Date<span style="color: red;"> *</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtMeeting_Date" runat="server" placeholder="DD/MM/YYYY" class="form-control" data-date-start-date="0d" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                                            </div>
                                            <small><span id="valtxtMeeting_Date" class="text-danger"></span></small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Time<span style="color: red;"> *</span></label>
                                        <div class="input-group bootstrap-timepicker timepicker">
                                            <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                            <asp:TextBox ID="txtMeeting_StartTime" runat="server" CssClass="form-control input-small" ClientIDMode="Static" autocomplete="off"></asp:TextBox>

                                        </div>
                                        <small><span id="valtxtMeeting_StartTime" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Attach Document</label>
                                        <asp:FileUpload ID="fuMeeting_Doc" runat="server" ClientIDMode="Static" CssClass="form-control" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX', this),ValidateFileSize(this)" />
                                        <small><span id="valfuMeeting_Doc" class="text-info">max-size- 5MB, Allowed-formate-'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX'</span></small>
                                        <%-- <span style="font-size: 10px; color: blue;">Only JPEG,PNG,JPG,GIF,PDF,DOC,DOCX Formats are allowed.<br />
                                                Maximum Allowed Filesize (5MB)
                                            </span>--%>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Description</label>
                                        <asp:TextBox ID="txtMeeting_Description" runat="server" CssClass="form-control" ClientIDMode="Static" TextMode="MultiLine" placeholder="Enter Description" />
                                        <small><span id="valtxtMeeting_Description" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" class="btn btn-block btn-success" Text="Save" OnClientClick="return validateform();" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnClear" runat="server" class="btn btn-block btn-default" Text="Clear" OnClick="btnClear_Click" />
                                    </div>
                                </div>
                                <div class="col-md-3"></div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblGridMsg" runat="server" ClientIDMode="Static" Text="" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                    <asp:GridView ID="GridView1" DataKeyNames="Meeting_ID" runat="server" class="table table-hover table-bordered table-stripted pagination-ys"
                                        AutoGenerateColumns="false" OnRowDeleting="GridView1_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subject">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRTI_Status" Text='<%# Eval("Meeting_Subject").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Meeting_Venue" HeaderText="Venue" />
                                            <asp:BoundField DataField="Meeting_OfficerName" HeaderText="Officer Name" />
                                            <asp:BoundField DataField="Meeting_Date" HeaderText="Date" />
                                            <asp:BoundField DataField="Meeting_StartTime" HeaderText="Time" />
                                            <asp:TemplateField HeaderText="Attachment" ShowHeader="False">
                                                <ItemTemplate>
                                                    <%--<asp:HyperLink ID="hyprRTI_FARequestDoc" runat="server" Text="Attachment" Target="_blank" NavigateUrl='<%# Eval("Meeting_Doc").ToString()%>'></asp:HyperLink>--%>
                                                    <asp:HyperLink ID="hyprRTI_FARequestDoc" Visible='<%# Eval("Meeting_Doc").ToString()!="" ? true : false %>' NavigateUrl='<%# Eval("Meeting_Doc").ToString() %>' Target="_blank" runat="server">Attachment</asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Meeting_Description" HeaderText="Description" />
                                           <%-- <asp:CommandField SelectText="Delete" HeaderText="Delete" ShowDeleteButton="true" ControlStyle-CssClass="label label-danger" />--%>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="deletebtn" runat="server" CommandName="Delete"
                                                        Text="Delete" OnClientClick="return confirm('Do you want to delete? ');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>


                    </div>
                    <!-- /.box-body -->
                </div>
                <!-- /.box -->
            </div>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../js/jquery.js"></script>
    <script src="../js/bootstrap-timepicker.js"></script>
    <script src="../js/bootstrap-datepicker.js"></script>
    <script>
        $('#txtMeeting_StartTime').timepicker();

        $('#txtMeeting_Date').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        });



        function validatename(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && charCode != 32) {
                return false;
            }
            return true;
        }
        function validateform() {

            $("#valtxtMeeting_Subject").html("");
            $("#valtxtMeeting_Venue").html("");
            $("#valtxtMeeting_OfficerName").html("");
            $("#valtxtMeeting_Date").html("");
            $("#valtxtMeeting_StartTime").html("");
            $("#valfuMeeting_Doc").html("");
            debugger;
            var msg = "";
            if (document.getElementById('<%=txtMeeting_Subject.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Subject. \n";
                $("#valtxtMeeting_Subject").html("Enter Subject");
            }
            if (document.getElementById('<%=txtMeeting_Venue.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Venue. \n";
                $("#valtxtMeeting_Venue").html("Enter Venue");
            }
            if (document.getElementById('<%=txtMeeting_OfficerName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Officer Name. \n";
                $("#valtxtMeeting_OfficerName").html("Enter Officer Name");
            }
            if (document.getElementById('<%=txtMeeting_Date.ClientID%>').value.trim() == "") {
                msg = msg + "Select Meeting Date. \n";
                $("#valtxtMeeting_Date").html("Select Meeting Date");
            }
            if (document.getElementById('<%=txtMeeting_StartTime.ClientID%>').value.trim() == "") {
                msg = msg + "Select Meeting Time. \n";
                $("#valtxtMeeting_StartTime").html("Select Meeting Time");
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            if (msg == "") {
                return true
            }

        }
        function UploadControlValidationForLenthAndFileFormat(maxLengthFileName, validFileFormaString, that) {
            //ex---------------
            //maxLengthFileName=50;
            //validFileFormaString=JPG*JPEG*PDF*DOCX
            //uploadControlId=upSaveBill
            //ex---------------
            $("#valtxtMeeting_Subject").html("");
            $("#valtxtMeeting_Venue").html("");
            $("#valtxtMeeting_OfficerName").html("");
            $("#valtxtMeeting_Date").html("");
            $("#valfuMeeting_Doc").html("");
            $("#valtxtMeeting_StartTime").html("");
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
                        msg += '- File Name Should be less than ' + maxLengthFileName + ' characters. \n';
                        $("#valfuMeeting_Doc").html("Special character not allowed in file name. \n");
                    }
                    for (i = 0; i <= (fileName.length - 1) ; i++) {
                        var charFileName = '';

                        charFileName = fileName.substring(i, i + 1);

                        if ((charFileName == '~') || (charFileName == '!') || (charFileName == '@') || (charFileName == '#') || (charFileName == '$') || (charFileName == '%') || (charFileName == '&') || (charFileName == '*') || (charFileName == '{') || (charFileName == '}') || (charFileName == '|') || (charFileName == '<') || (charFileName == '>') || (charFileName == '?')) {

                            msg += '- Special character not allowed in file name. \n';
                            $("#valfuMeeting_Doc").html("Special character not allowed in file name. \n");
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
                            msg += 'File Format Is Not Correct (Only ' + strValidFormates + ').\n';
                            $("#valfuMeeting_Doc").html("Special character not allowed in file name. \n");
                        }
                    }

                }
                else {
                    msg += '- File Name is incorrect';
                    $("#valfuMeeting_Doc").html("Special character not allowed in file name. \n");
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
            $("#valtxtMeeting_Subject").html("");
            $("#valtxtMeeting_Venue").html("");
            $("#valtxtMeeting_OfficerName").html("");
            $("#valtxtMeeting_Date").html("");
            $("#valfuMeeting_Doc").html("");

            var uploadcontrol = document.getElementById(a.id);
            if (uploadcontrol.files[0].size > 20971520) {
                alert('File size should not greater than 5 mb.');
                document.getElementById(a.id).value = '';
                return false;
            }
            else {
                return true;
            }

        }

    </script>
</asp:Content>

