<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRLoginPermission.aspx.cs" Inherits="HRLoginPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link rel="stylesheet" href="../css/bootstrap-timepicker.css" />
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
                            <h3 class="box-title" id="Label1">Login-Logout Intimation</h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" ClientIDMode="Static"></asp:Label>

                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <span class="text-info text-bold">When will you use this intimation form?</span>
                            <ul class="text-info text-bold">
                                <li>Leaving for the day directly from remote/client location without bio matiric Entry.</li>
                                <li>In case of Late Login and/or Early Logout from remote/client/MPAGRO location.</li>
                            </ul>
                            <div id="divFirstAppeal" runat="server">
                                <div class="row">
                                    <div class="col-md-6">
                                        <fieldset>
                                            <legend style="margin-bottom: 12px;">
                                                <asp:RadioButton ID="rbtnLateLogin" runat="server" ClientIDMode="Static" Checked="true" onclick="checkChecks1()" />
                                                Login Detail</legend>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Applying Login Date<span style="color: red;"> *</span></label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtApplyingLoginDate" runat="server" autocomplete="off" placeholder="DD/MM/YYYY" class="form-control DateAdd" data-provide="datepicker" data-date-end-date="0d" data-date-start-date="-5d" ClientIDMode="Static" onkeypress="return validateNum(event)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Login Time<span style="color: red;"> *</span></label>
                                                        <div class="input-group bootstrap-timepicker timepicker">
                                                            <asp:TextBox runat="server" CssClass="form-control input-small" ClientIDMode="Static" autocomplete="off" ID="txtLoginTime" onchange="return CalculateDiffLogInTime()"></asp:TextBox>
                                                            <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 hidden">
                                                    <div class="form-group">
                                                        <label>Difference Time</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtLoginDiffTime" ClientIDMode="Static" Enabled="false" />
                                                        <small><span id="valtxtLoginDiffTime" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>Remark<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtLoginRemark" ClientIDMode="Static" TextMode="MultiLine" />
                                                        <small><span id="valtxtLoginRemark" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="col-md-6">
                                        <fieldset>
                                            <legend style="margin-bottom: 12px;">
                                                <asp:RadioButton ID="rbtnEarlyLogout" runat="server" ClientIDMode="Static" onclick="checkChecks2()" />
                                                Logout Detail</legend>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Applying Logout Date<span style="color: red;"> *</span></label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtApplyingLogoutDate" runat="server" autocomplete="off" placeholder="DD/MM/YYYY" class="form-control DateAdd" data-provide="datepicker" data-date-end-date="0d" data-date-start-date="-5d" ClientIDMode="Static" onkeypress="return validateNum(event)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Logout Time<span style="color: red;"> *</span></label>
                                                        <div class="input-group bootstrap-timepicker timepicker">
                                                            <asp:TextBox runat="server" CssClass="form-control input-small" ClientIDMode="Static" autocomplete="off" ID="txtLogOutTime" onchange="return CalculateDiffLogOutTime()"></asp:TextBox>
                                                            <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-6 hidden">
                                                    <div class="form-group">
                                                        <label>Difference Time</label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtLogoutDiffTime" ClientIDMode="Static" Enabled="false" />
                                                        <small><span id="valtxtLogoutDiffTime" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>Remark<span style="color: red;"> *</span></label>
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtLogoutRemark" ClientIDMode="Static" TextMode="MultiLine" />
                                                        <small><span id="valtxtLogoutRemark" class="text-danger"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4"></div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" class="btn btn-block btn-success" Style="margin-top: 20px;" Text="Save" OnClientClick="return validateform();" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btCancel" runat="server" class="btn btn-block btn-default" Style="margin-top: 20px;" Text="Cancel" />
                                    </div>
                                </div>
                                <div class="col-md-4"></div>
                            </div>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
            </div>
            <!-- /.row -->

        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../js/jquery.js"></script>
    <script src="../js/bootstrap-timepicker.js"></script>
    <script>
        $('#txtLoginTime').timepicker();
        $('#txtLogOutTime').timepicker();
        window.onload = function () {
            checkChecks1();
        };

        function checkChecks1() {            
            if (document.getElementById('rbtnLateLogin').checked) {
                document.getElementById('rbtnEarlyLogout').checked = false;
                document.getElementById("txtLogOutTime").disabled = true;
                document.getElementById("txtLoginTime").disabled = false;
                document.getElementById('txtLogoutRemark').disabled = true;
                document.getElementById('txtLoginRemark').disabled = false;
                document.getElementById('txtApplyingLogoutDate').disabled = true;
                document.getElementById('txtApplyingLoginDate').disabled = false;

                document.getElementById('<%=txtLogoutDiffTime.ClientID%>').value = '';
                document.getElementById('txtLogoutRemark').value = '';
                document.getElementById('lblmsg').value = "";
                //alert ("You may only check ONE checkbox");
            }
        }
        function checkChecks2() {            
            if (document.getElementById('rbtnEarlyLogout').checked) {
                document.getElementById('rbtnLateLogin').checked = false;
                document.getElementById("txtLoginTime").disabled = true;
                document.getElementById("txtLogOutTime").disabled = false;
                document.getElementById('txtLoginRemark').disabled = true;
                document.getElementById('txtLogoutRemark').disabled = false;
                document.getElementById('<%=txtLoginDiffTime.ClientID%>').value = '';
                document.getElementById('txtLoginRemark').value = '';
                document.getElementById('txtApplyingLogoutDate').disabled = false;
                document.getElementById('txtApplyingLoginDate').disabled = true;
                document.getElementById('lblmsg').value = '';
            }
        }

        function CalculateDiffLogInTime() {
            var Logintime = document.getElementById("txtLoginTime").value;
            ;
            if (Logintime != "") {
                var timeStart = new Date("01/01/2007 " + Logintime);
                var timeEnd = new Date("01/01/2007 " + "10:00 AM");

                var diff = (timeStart - timeEnd) / 60000; //dividing by seconds and milliseconds

                var minutes = diff % 60;
                var hours = (diff - minutes) / 60;

                // alert(hours + ":" + minutes);
                document.getElementById('<%=txtLoginDiffTime.ClientID%>').value = hours + ":" + minutes;
                //if (diff < 0) {
                //     alert("Select Valid Time.");
                //      return false;
                //}
                //else {
                //      return true;
                //}
                // return false;
            }
        }
        function CalculateDiffLogOutTime() {
            var Logouttime = document.getElementById("txtLogOutTime").value;
            ;
            if (Logouttime != "") {
                var timeStart = new Date("01/01/2007 " + Logouttime);
                var timeEnd = new Date("01/01/2007 " + "05:30 PM");

                var diff = (timeEnd - timeStart) / 60000; //dividing by seconds and milliseconds

                var minutes = diff % 60;
                var hours = (diff - minutes) / 60;

                // alert(hours + ":" + minutes);
                document.getElementById('<%=txtLogoutDiffTime.ClientID%>').value = hours + ":" + minutes;
                //if (diff < 0) {
                //      alert("Select Valid Time.");
                //    return false;
                //}
                //else {
                //     return true;
                //}
            }
        }
        function validateform() {
            var msg = "";
            var remark = "";
            var DiffTime = "";
            var latemsg = "";
            var start = "";
            var end = "";
            if (document.getElementById('rbtnLateLogin').checked) {
                remark = document.getElementById('txtLoginRemark').value;
                DiffTime = document.getElementById('<%=txtLoginDiffTime.ClientID%>').value;
                start = document.getElementById('<%=txtApplyingLoginDate.ClientID%>').value;
            }
            if (document.getElementById('rbtnEarlyLogout').checked) {
                remark = document.getElementById('txtLogoutRemark').value;
                DiffTime = document.getElementById('<%=txtLogoutDiffTime.ClientID%>').value;
                start = document.getElementById('<%=txtApplyingLogoutDate.ClientID%>').value;
            }
            if (start == "") {
                msg += "Enter Applying Date. \n";
            }
            if (remark == "") {
                msg += "Enter Remark. \n";
            }

            if (DiffTime < 0) {
                msg += "Select Valid Time. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Submit Details ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script>
</asp:Content>

