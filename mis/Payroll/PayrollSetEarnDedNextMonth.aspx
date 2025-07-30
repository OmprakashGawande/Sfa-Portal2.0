<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollSetEarnDedNextMonth.aspx.cs" Inherits="mis_Payroll_PayrollSetEarnDedNextMonth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">

                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Set Earn. & Ded. Details Next Month</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12" style="color: teal; font-weight: 400">
                                    <b style="font-size: 16px; color: orange">नोट  : -</b><br />
                                     From Month & Year से उस माह एवं वर्ष का चयन करें जिस माह का वेतन दुसरे माह में सेट करना है | <br />
                                     To Month & Year में उस माह एवं वर्ष का चयन करें जिस माह में सेट करना है | <br /><br />
                                                    
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-md-5">
                                    <fieldset>
                                        <legend>From Month & Year ( जिस माह का वेतन दुसरे माह में सेट करना है )</legend>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Year <span class="text-danger">*</span></label>
                                                    <asp:DropDownList ID="ddlFromYear" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Month <span style="color: red;">*</span></label>
                                                    <asp:DropDownList ID="ddlFromMonth" runat="server" class="form-control">
                                                        <asp:ListItem Value="0">Select Month</asp:ListItem>
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="col-md-5">
                                    <fieldset>
                                        <legend>To Month & Year ( जिस माह में सेट करना है )</legend>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Year <span class="text-danger">*</span></label>
                                                    <asp:DropDownList ID="ddlToYear" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label>Month <span style="color: red;">*</span></label>
                                                    <asp:DropDownList ID="ddlToMonth" runat="server" class="form-control">
                                                        <asp:ListItem Value="0">Select Month</asp:ListItem>
                                                        <asp:ListItem Value="1">January</asp:ListItem>
                                                        <asp:ListItem Value="2">February</asp:ListItem>
                                                        <asp:ListItem Value="3">March</asp:ListItem>
                                                        <asp:ListItem Value="4">April</asp:ListItem>
                                                        <asp:ListItem Value="5">May</asp:ListItem>
                                                        <asp:ListItem Value="6">June</asp:ListItem>
                                                        <asp:ListItem Value="7">July</asp:ListItem>
                                                        <asp:ListItem Value="8">August</asp:ListItem>
                                                        <asp:ListItem Value="9">September</asp:ListItem>
                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="col-md-2">
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Save" ID="btnSave" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                                    </div>

                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <a href="PayrollSetEarnDedNextMonth.aspx" class="btn btn-block btn-default">Clear</a>
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

    <script>
        function validateform() {
            var msg = "";

            if (document.getElementById('<%=ddlFromYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select From Year. \n";
            }
            if (document.getElementById('<%=ddlFromMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select From Month. \n";
            }
            if (document.getElementById('<%=ddlToYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select To Year. \n";
            }
            if (document.getElementById('<%=ddlToMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select To Month. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Save Details ?")) {
                    return true;
                }
                else {
                    return false;
                }

            }

        }
    </script>
</asp:Content>



