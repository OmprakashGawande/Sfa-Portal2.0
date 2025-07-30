<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpRetirement.aspx.cs" Inherits="mis_HR_HREmpRetirement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Separation/Retirement</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <p style="color:tomato; font-size:12px;"><b>Note:</b> सेवानिवृति की जानकारी, सेवानिवृति के 15 दिन के बाद दर्ज की जावेगी | </p>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>कार्यालय / Office<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOffice" runat="server" CssClass="form-control select2" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>कर्मचारी का नाम / Employee Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 hidden" >
                            <div class="form-group">
                                <label>Old Retirement Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtoldretirementdate" runat="server" placeholder="Old Retirement Date..." Enabled="false" class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <label>Separation Type<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlSeparation_Type" runat="server" CssClass="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>Death</asp:ListItem>
                                    <asp:ListItem>Retirement</asp:ListItem>
                                    <asp:ListItem>Contract Expired</asp:ListItem>
                                    <asp:ListItem>Termination</asp:ListItem>
                                    <asp:ListItem>Voluntary Retirement Scheme</asp:ListItem>
                                    <asp:ListItem>Other</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Separated/Retired On<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtRetired_On" runat="server" placeholder="Select Date..." class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>आदेश नंबर / Order No.<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtOrder_No" runat="server" placeholder="Order No..." CssClass="form-control" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>आदेश तारीख / Order Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtOrder_Date" runat="server" placeholder="Select Date..." class="form-control DateAdd" data-date-end-date="0d" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Attach Supporting Documents<span style="color: red;">*</span></label>&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink1" CssClass="label label-info" runat="server" Visible="false">View</asp:HyperLink>
                                <asp:FileUpload ID="FileUpload1" runat="server" ClientIDMode="Static" CssClass="form-control" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX', this),ValidateFileSize(this)" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Remark<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtRemark" runat="server" placeholder="Remark.." Rows="5" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success btn-block" Style="margin-top: 23px;" OnClick="btnSave_Click" OnClientClick="return validateform()" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" style="margin-top: 23px;" href="HREmpRetirement.aspx">Clear</a>
                            </div>
                        </div>
                        <div class="col-md-2"></div>
                        <div class="col-md-2"></div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg += "Select Office. \n"
            }
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg += "Select Employee. \n"
            }
            if (document.getElementById('<%=txtoldretirementdate.ClientID%>').value.trim() == "") {
                msg += "Select Old Retirement Date. \n"
            }
            if (document.getElementById('<%=ddlSeparation_Type.ClientID%>').selectedIndex == 0) {
                msg += "Select Separation Type. \n"
            }
            
            if (document.getElementById('<%=txtRetired_On.ClientID%>').value.trim() == "") {
                msg += "Enter Separated/RetiredOn Date. \n"
            }
            if (document.getElementById('<%=txtOrder_No.ClientID%>').value.trim() == "") {
                msg += "Enter Order No. \n"
            }
            if (document.getElementById('<%=txtOrder_Date.ClientID%>').value.trim() == "") {
                msg += "Enter Order Date. \n"
            }
            if (document.getElementById('<%=txtRemark.ClientID%>').value.trim() == "") {
                msg += "Enter Remark. \n"
            }
            if (document.getElementById('<%=FileUpload1.ClientID%>').files.length == 0)
            {
                msg += "Attach Supporting Documents. \n"
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
                else if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Edit") {
                    if (confirm("Do you really want to Edit Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }
        function uploadDoc() {
            debugger
            if (document.getElementById('<%=FileUpload1.ClientID%>').files.length != 0) {
                var el = document.getElementById("FileUpload1");
                var ext = el.value.split('.').pop().toLowerCase();
                if (el.inArray(ext, ['png', 'jpg', 'pdf']) == -1) {
                    alert("केवल पीएनजी, जेपीजी, पीडीएफ दस्तावेज अपलोड करें।");
                    document.getElementById('FileUpload1').value = "";
                }
                else {

                }
            }
        }

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
                        msg += '- File Name Should be less than ' + maxLengthFileName + ' characters. \n';
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
                            msg += 'File Format Is Not Correct (Only ' + strValidFormates + ').\n';
                        }
                    }

                }
                else {
                    msg += '- File Name is incorrect';
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
    <script src="../js/ValidationJs.js"></script>
</asp:Content>

