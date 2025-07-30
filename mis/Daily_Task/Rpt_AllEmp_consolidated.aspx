<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_AllEmp_consolidated.aspx.cs" Inherits="mis_Daily_Task_Rpt_AllEmp_consolidated" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <div class="box box-success">
                    <div class="box-header with-border">
                        <h3 class="box-title" id="Label1">MONTHLY CONSOLIDATED TASK REPORT</h3>
                    </div>
                    <asp:Label runat="server" ID="lblMsg"></asp:Label>
                    <div class="box-body">

                        <div class="row">
                            <div class="col-md-3">
                                <label runat="server">
                                    FROM DATE
                                    <label style="color: red;">*</label></label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtFromDate"
                                        data-date-end-date="0d"
                                        data-provide="datepicker" placeholder="DD/MM/YYYY"
                                        autocomplete="off" data-date-format="dd/mm/yyyy"
                                        data-date-autoclose="true" CssClass="form-control disableFuturedate"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <label runat="server">
                                    TO DATE
                                    <label style="color: red;">*</label>
                                </label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtToDate"
                                        data-date-end-date="0d"
                                        data-provide="datepicker" placeholder="DD/MM/YYYY"
                                        autocomplete="off" data-date-format="dd/mm/yyyy"
                                        data-date-autoclose="true" CssClass="form-control disableFuturedate"></asp:TextBox>
                                </div>
                            </div>
                            <div class=" col-md-3" runat="server">
                                <label runat="server">
                                    EMPLOYEE NAME
                                    <label style="color: red;">*</label></label>
                               <%-- <span class="fa-pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                        ErrorMessage="SELECT EMPLOYEE NAME " InitialValue="0" ForeColor="Red"
                                        Text="<i class='fa fa-exclamation-circle' title='SELECT EMPLOYEE NAME !'></i>"
                                        ControlToValidate="ddlEmp" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                </span>--%>
                                <div class="ms form-group">
                                    <asp:DropDownList runat="server" ID="ddlEmp" ClientIDMode="Static"
                                        CssClass="form-control select2">
                                        <asp:ListItem Value="0">No record found</asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3" style="margin-top: 26px">
                                <div class="row">
                                    <div class="col-md-6">

                                        <asp:LinkButton runat="server" ID="btnSearch" ValidationGroup="a" CssClass="btn btn-block btn-success"
                                            OnClick="btnSearch_Click"><i class="fa fa-search"> Search</i></asp:LinkButton>

                                    </div>
                                    <div class="col-md-6">
                                        <a href="Rpt_AllEmp_consolidated.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer">
                        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana"
                                        KeepSessionAlive="false" Font-Size="8pt" WaitMessageFont-Names="Verdana"
                                        WaitMessageFont-Size="14pt" Height="786px" Width="1100px" Style="margin-right: 0px">
                                        <LocalReport ReportPath="mis\Daily_Task\Rdl_Emp_Consolidated_Rpt.rdlc">
                                        </LocalReport>
                                    </rsweb:ReportViewer>
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
</asp:Content>

