<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollMonthlyPayBillReportNew.aspx.cs" Inherits="mis_Payroll_PayrollMonthlyPayBillReportNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .pay-sheet table tr th, .pay-sheet table tr td {
            font-size: 12px;
            width: 10%;
            border: 1px solid #c5c5c5;
            padding-left: 3px;
            padding-top: 1px;
            line-height: 14px;
            font-family: monospace;
            overflow: hidden;
            text-align: -webkit-right;
            padding-right: 3px;
        }

        .pay-sheet table {
            width: 100%;
        }

            .pay-sheet table thead {
                background: #eee;
            }

        .main-heading-print{
            margin-top:50px;
        }
        /*.pay-sheet table {
            border: 1px solid #ddd;
        }*/

        @media print {
            .Hiderow, .main-footer {
                display: none;
            }

            .box {
                border: none;
            }

            th {
                background-color: #ddd;
                text-decoration: solid;
            }

            .tblheadingslip {
                font-size: 8px !important;
                background: black;
                color: red;
            }

            .pay-sheet table {
                page-break-inside: avoid;
            }

        }

        table tr.page-break {
            page-break-after: always;
        }

        table.page-break {
            page-break-after: always;
        }

        .alignright {
            text-align: right !important;
        }

        th.text-left {
            text-align: -webkit-left !important;
        }

        th {
            border-bottom-width: 1px !important;
            background-color: #eaeaea !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header with-border Hiderow">
                    <div class="row">
                        <div class="col-md-3">
                            <h3 class="box-title">Earning Deduction Details</h3>
                        </div>
                        <div class="col-md-3">
                            <asp:RadioButtonList ID="rbnlist" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="&nbsp;Regular&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" style="float: right" Value="Generated"></asp:ListItem>
                                <asp:ListItem Text="&nbsp;Supplementry" style="float: left" Value="Supplementry Generated"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-md-6">
                              <asp:LinkButton ID="lnkViewDetails" Text="Old Report" PostBackUrl="PayrollAllEmpEarnDedDetailNew.aspx" CssClass="btn btn-dropbox pull-right" runat="server"></asp:LinkButton>
                            </div>
                    </div>
                </div>

                <div class="box-body">
                    <div class="row Hiderow">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Display="Dynamic"
                                        ControlToValidate="ddlOffice" ErrorMessage="Select Office"
                                        Text="<i class='fa fa-exclamation-circle' title='Select Office'></i>"
                                        SetFocusOnError="true" ForeColor="Red" InitialValue="0" ValidationGroup="a">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged"
                                     ID="ddlOffice" CssClass="form-control" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Year <span class="text-danger">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                        ControlToValidate="ddlFinancialYear" ErrorMessage="Select Year"
                                        Text="<i class='fa fa-exclamation-circle' title='Select Year'></i>"
                                        SetFocusOnError="true" ForeColor="Red" InitialValue="0" ValidationGroup="a">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Month <span style="color: red;">*</span></label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                        ControlToValidate="ddlMonth" ErrorMessage="Select Month"
                                        Text="<i class='fa fa-exclamation-circle' title='Select Month'></i>"
                                        SetFocusOnError="true" ForeColor="Red" InitialValue="0" ValidationGroup="a">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
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
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Type of Post <span style="color: red;">*</span></label>
                                 <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                                        ControlToValidate="ddlEmp_TypeOfPost" ErrorMessage="Select Type of Post"
                                        Text="<i class='fa fa-exclamation-circle' title='Select Type of Post'></i>"
                                        SetFocusOnError="true" ForeColor="Red" InitialValue="0" ValidationGroup="a">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlEmp_TypeOfPost" runat="server" class="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="Permanent">Regular/Permanent</asp:ListItem>
                                    <asp:ListItem Value="Fixed Employee">Fixed Employee(स्थाई कर्मी)</asp:ListItem>
                                    <asp:ListItem Value="Contigent Employee">Contigent Employee</asp:ListItem>
                                    <asp:ListItem Value="Samvida Employee">Samvida Employee</asp:ListItem>
                                    <asp:ListItem Value="Theka Shramik">Theka Shramik</asp:ListItem>
                                    <asp:ListItem Value="Outsource Employee">Outsource Employee</asp:ListItem>
                                    <asp:ListItem Value="Other Employee">Other Employee</asp:ListItem>
                                    <asp:ListItem Value="Deputation Employee">Deputation Employee</asp:ListItem>
                                    <asp:ListItem Value="Daily Wages Federation">Daily Wages Federation</asp:ListItem>
                                    <asp:ListItem Value="Job Rate Employee">Job Rate Employee</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Section No. </label>
                                <span style="color: red">*</span>
                                 <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                            ErrorMessage="Select Section No." ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Section No. !'></i>"
                                            ControlToValidate="ddlSection" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                <asp:ListBox runat="server" ID="ddlSection" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:LinkButton runat="server" CssClass="btn btn-success btn-block" ValidationGroup="a" OnClick="btnShow_Click" Text="Search" ID="btnShow"></asp:LinkButton>
                            </div>

                        </div>

                    </div>
                    <div class="row Hiderow" id="pnlprint" runat="server" visible="false" >
                        <div class="col-md-12">
                          <asp:LinkButton ID="lnkPrint" OnClientClick="window.print()" runat="server" CssClass="btn btn-primary" Text="Print"></asp:LinkButton>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-12">
                            <div runat="server" id="DivHead" visible="false" class="header_div" style="text-align: center;">
                                <h5>M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd. 
                              ( 
                                    <asp:Label ID="lblOffice" runat="server" Text=""></asp:Label>
                                    )
                                </h5>
                                <h5 style="text-decoration: underline dotted;">Pay Sheet For the Month of
                                    <asp:Label ID="lblSession" runat="server" Text=""></asp:Label>
                                    (
                                    <asp:Label ID="lblPosttype" runat="server" Text=""></asp:Label>
                                    )</h5>
                            </div>
                            <div id="DivDetail" class="pay-sheet" runat="server">
                            </div>
                        </div>
                    </div>
                </div>
           </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
     <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script>
        $(function () {
            $('[id*=ddlSection]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
    </script>
</asp:Content>

