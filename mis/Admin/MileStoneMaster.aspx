<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MileStoneMaster.aspx.cs" Inherits="mis_Admin_MileStoneMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">TL Master</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Important Subject<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtImpSubject" runat="server" ClientIDMode="Static" Placeholder="Important Subject...." CssClass="form-control"></asp:TextBox>
                                <small><span id="valtxtImpSubject" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>TL Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtTlDate" runat="server" placeholder="TL Date..." class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <small><span id="valtxtTlDate" class="text-danger"></span></small>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Officer Name<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtOfficerName" runat="server" Placeholder="Officer Name...." MaxLength="50" CssClass="form-control" onkeypress="return validatename(event);"></asp:TextBox>
                                <small><span id="valtxtOfficerName" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>File Upload</label>&nbsp;&nbsp;<asp:HyperLink ID="HyperLink1" CssClass="label label-default" runat="server"></asp:HyperLink>
                                <asp:FileUpload ID="FileUpload1" runat="server"  ClientIDMode="Static" CssClass="form-control" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX', this),ValidateFileSize(this)"/>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success btn-block" Style="margin-top: 23px;" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" style="margin-top: 23px;" href="MileStoneMaster.aspx">Clear</a>
                            </div>
                        </div>
                        <div class="col-md-2"></div>
                        <div class="col-md-2"></div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            
                            <div class="table-responsive">
                                <asp:Label ID="GridMsg" runat="server" Text="" Style="color: red; font-size: 15px;"></asp:Label>
                                <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="MileStone_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>

                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ImportantSubject" HeaderText="Important Subject" />
                                        <asp:BoundField DataField="TLDate" HeaderText="TL Date" />
                                        <asp:BoundField DataField="OfficerName" HeaderText="Officer Name" />
                                        <asp:TemplateField HeaderText="FileUpload" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="document" runat="server" NavigateUrl='<%# Eval("FileUpload").ToString() %>' CssClass="label label-default" Target="_blank" Text='<%# Eval("FileUpload").ToString() == ""? "NA":"VIEW"  %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="label label-info" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Do you really want to Delete Details?');"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
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

        function validateform() {
            var msg = "";
            $("#valtxtImpSubject").html("");
            $("#valtxtTlDate").html("");
            $("#valtxtOfficerName").html("");
            if (document.getElementById('<%=txtImpSubject.ClientID%>').value.trim() == "") {
                msg += "Enter Important Subject. \n"
                $("#valtxtImpSubject").html("Enter Important Subject");
            }
            if (document.getElementById('<%=txtTlDate.ClientID%>').value.trim() == "") {
                msg += "Select TL Date. \n"
                $("#valtxtTlDate").html("Select TL Date");
            }
            if (document.getElementById('<%=txtOfficerName.ClientID%>').value.trim() == "") {
                msg += "Enter Officer Name. \n"
                $("#valtxtOfficerName").html("Enter Officer Name");
            }
            <%--if (document.getElementById('<%=FileUpload1.ClientID%>').files.length == 0) {
                msg += "Select File \n"
            }--%>
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value == "Save")
                {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else if (document.getElementById('<%=btnSave.ClientID%>').value == "Update") {
                    if (confirm("Do you really want to Update Details ?")) {
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

</asp:Content>

