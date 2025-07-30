<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Payroll_RptOfficeWiseOvertimePay.aspx.cs" Inherits="mis_Payroll_Payroll_RptOfficeWiseOvertimePay" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
       <asp:ScriptManager ID="ScriptManger" runat="server"></asp:ScriptManager>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <div class="box box-success">
                    <div class="box-header with-border">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1"> Overtime Pay Report</h3>

                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Overtime Pay Report</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label runat="server" ID="lblMsg"></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label runat="server">Office Name<span style="color: red;">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlOfficeName" InitialValue="0" ErrorMessage="Select Office Name"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Office Name'></i>"
                                                    SetFocusOnError="true" ForeColor="Red" ValidationGroup="a">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label runat="server">Month <span style="color: red;">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvEffectiveDate" ValidationGroup="a"
                                                    ErrorMessage="Select Month" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month'></i>"
                                                    ControlToValidate="txtMonth" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control"
                                                ID="txtMonth" MaxLength="8" placeholder="Select Month"
                                                data-provide="datepicker" onpaste="return false ;"
                                                onkeypress="return false;" data-date-format="mm/yyyy"
                                                data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-2" style="margin-top: 19px;">
                                        <div class="form-group">

                                            <asp:Button runat="server" ID="btnSearch" ValidationGroup="a" OnClick="btnSearch_Click" CssClass="btn btn-success btn-block" Text="Search" />
                                        </div>
                                    </div>
                                    <div class="col-md-2" style="margin-top: 19px;">
                                        <div class="form-group">

                                            <asp:LinkButton ID="lnkbtnClear" OnClick="lnkbtnClear_Click" runat="server" CssClass="btn btn-default btn-block" Text="Clear"></asp:LinkButton>
                                        </div>
                                    </div>
                                     <div class="col-md-12" id="pnlreport" runat="server">
                                         <div class="table-responsive">
                                                <rsweb:ReportViewer ID="ReportViewer1" runat="server" KeepSessionAlive="false"
                                                    Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana"
                                                    WaitMessageFont-Size="14pt" Height="600" Width="1200px" Style="margin-right: 0px">
                                                </rsweb:ReportViewer>
                                            </div>
                                      </div>
                                </div>

                            </fieldset>
                        </div>

                    </div>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
      <script>
          $("#txtMonth").datepicker({
              format: "mm/yyyy",
              viewMode: "months",
              minViewMode: "months",
              autoclose: true,
          });
         </script>
</asp:Content>

