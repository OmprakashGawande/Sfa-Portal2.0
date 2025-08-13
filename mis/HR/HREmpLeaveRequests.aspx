<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpLeaveRequests.aspx.cs" Inherits="mis_HR_HREmpLeaveRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <div class="row g-4">
            <div class="col-md-12">
                <div class="card mt-3  border-warning">
                    <div class="card-header">
                        <h4>Pending Leave Requests</h4>
                    </div>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    <div class="card-body">
                        <div class="row">

                            <div class="col-xl-4 col-sm-6 position-relative">
                                <div class="form-group">
                                    <label>Year</label>
                                    <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Leave Approval</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="box-header">
                                                <%--<h3 class="box-title">Leave Approval</h3>--%>
                                                <asp:Label ID="lblmsgmodal" runat="server"></asp:Label>
                                            </div>
                                            <div class="table-responsive">
                                                <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateRows="false">
                                                    <Fields>
                                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                        <asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" />
                                                        <asp:BoundField DataField="BalanceLeave" HeaderText="Balance Leave" />
                                                        <asp:BoundField DataField="TakenLeave" HeaderText="Leave For" />
                                                        <asp:BoundField DataField="LeaveFromDate" HeaderText="From Date" />
                                                        <asp:BoundField DataField="LeaveToDate" HeaderText="To Date" />
                                                        <asp:TemplateField HeaderText="Leave Related Doc" ItemStyle-Width="70%">
                                                            <ItemTemplate>
                                                                <a href='<%# Eval("LeaveDocument") %>' target="_blank" class="label label-info"><%# Eval("LeaveDocument").ToString() != "" ? "View" : "" %></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Fields>
                                                </asp:DetailsView>
                                                <div class="form-group">
                                                    <asp:Label ID="lblr" runat="server" Text="छुट्टी का कारण (Reason Of Leave)"></asp:Label>
                                                </div>
                                                <div class="form-group">

                                                    <div id="LeaveReason" runat="server">
                                                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <fieldset>
                                        <legend>HR Reply Section</legend>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Status<span style="color: red;">*</span></label>
                                                    <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                        <asp:ListItem Value="Approved">Approve</asp:ListItem>
                                                        <asp:ListItem Value="Rejected">Reject</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label>Remark</label>
                                                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Rows="3" placeholder="Enter Remark..." class="form-control" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Order No.</label>
                                                    <asp:TextBox ID="txtOrderNo" runat="server" placeholder="Enter Order No" class="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <%--<div class="col-md-6">
                                            <div class="form-group">
                                                <label>Order Date</label>
                                                <asp:TextBox ID="txtOrderDate" runat="server" placeholder="Select Order Date..." class="form-control DateAdd" autocomplete="off" data-date-Start-date="-100d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>--%>

                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Document</label>
                                                    <asp:FileUpload ID="ApprovalDoc" CssClass="form-control" runat="server" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*XLS*XLSX*DOC*DOCX', this),ValidateFileSize(this)" />
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnApprove" class="btn btn-success" runat="server" Text="Submit" OnClick="btnApprove_Click" OnClientClick="return validateLeaveForm();" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card mt-3  border-warning">
                    <div class="card-header">
                        <h4>Pending Leave Requests</h4>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <%--<div class="table table-responsive">--%>
                                <asp:GridView ID="GridView1" DataKeyNames="LeaveId" class="datatable table table-hover table-bordered" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" AutoGenerateColumns="False" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave Status" ItemStyle-Width="12%">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Eval("LeaveStatus".ToString())%>' runat="server" ID="lbLeaveStatus"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" ItemStyle-Width="18%" />
                                        <asp:BoundField DataField="Office_Name" HeaderText="Office Name" ItemStyle-Width="20%" />
                                        <asp:BoundField DataField="LeaveAppliedOn" HeaderText="Applied Date" />
                                        <asp:BoundField DataField="Leave_Type" HeaderText="Leave Type" ItemStyle-Width="12%" />
                                        <asp:BoundField DataField="TotalRemainingLeave" HeaderText="Balance Leaves (Days)" />
                                        <asp:BoundField DataField="TakenLeave" HeaderText="Leave For (Days)" />
                                        <asp:BoundField DataField="LeaveFromDate" HeaderText="From Date" />
                                        <asp:BoundField DataField="LeaveToDate" HeaderText="To Date" />
                                        <asp:TemplateField HeaderText="Action" ShowHeader="False" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="ViewMore" CssClass="label label-info approvalbutton" runat="server" CommandName="Select">Approve Leave</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lblMsg2" runat="server" Text="" Style="color: red; font-size: 15px;"></asp:Label>
                                <%--</div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script type="text/javascript">
        function callalert() {
            $("#myModal").modal('show');
        }

        function validateLeaveForm() {
            var msg = "";
            if (document.getElementById('<%=ddlStatus.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Status. \n";
            }
<%--            if (document.getElementById('<%=txtRemark.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Remark \n";
            }--%>

           <%-- if (document.getElementById('<%=txtOrderDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select Date \n";
            }--%>
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (confirm("Do you really want to Save Detail.?")) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }
        $(document).ready(function () {
            initCustomDataTable('.datatable', 'Pending Leave Requests Detail', 'Pending Leave Requests Detail', [13, 14]);
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
                    for (i = 0; i <= (fileName.length - 1); i++) {
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

