<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollAllEmpEarnDedDetailNew.aspx.cs" Inherits="mis_Payroll_PayrollAllEmpEarnDedDetailNew" %>

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
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header with-border Hiderow">
                     <div class="row">
                           <div class="col-md-4">
                    <h3 class="box-title">Earning Deduction Details</h3>
                               </div>
                     <div class="col-md-4">
                                    <asp:RadioButtonList ID="rbnlist" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="&nbsp;Regular&nbsp;&nbsp;&nbsp;&nbsp;" Selected="True" style="float: right" Value="Generated"></asp:ListItem>
                                        <asp:ListItem Text="&nbsp;Supplementry" style="float: left" Value="Supplementry Generated"></asp:ListItem>
                                    </asp:RadioButtonList>
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
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged" ID="ddlOffice" CssClass="form-control" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Year <span class="text-danger">*</span></label>
                                <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Month <span style="color: red;">*</span></label>
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
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Type of Post (पद प्रकार) <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmp_TypeOfPost" runat="server" class="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="Permanent">Regular/Permanent</asp:ListItem>
                                    <asp:ListItem Value="Fixed Employee">Fixed Employee(स्थाई कर्मी)</asp:ListItem>
                                    <asp:ListItem Value="Contigent Employee">Contigent Employee</asp:ListItem>
                                    <asp:ListItem Value="Samvida Employee">Samvida Employee</asp:ListItem>
                                    <asp:ListItem Value="Theka Shramik">Theka Shramik</asp:ListItem>
                                    <asp:ListItem Value="Outsource Employee">Outsource Employee</asp:ListItem>
									<asp:ListItem Value="Other Employee">Other Employee</asp:ListItem>
									<asp:ListItem Value="Deputation Employee">Deputation Employee</asp:ListItem>
                                    <asp:ListItem Value="Contractual Employee">Contractual Employee</asp:ListItem>
                                    <asp:ListItem Value="Daily Wages Employee">Daily Wages Employee</asp:ListItem>
                                    <asp:ListItem Value="Daily Wages Federation">Daily Wages Federation</asp:ListItem>
                                    <asp:ListItem Value="Job Rate Employee">Job Rate Employee</asp:ListItem>
                                     <asp:ListItem Value="Permanent NPF">Regular/Permanent NPF</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-md-2">
                            <div class="form-group">
                                <label>Section No </label>
                                <span style="color: red">*</span>
                                <asp:ListBox runat="server" ID="ddlSection" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                      
                    </div>
                     <div class="row Hiderow">
                      <div class="col-md-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Search" ID="btnShow" OnClick="btnShow_Click" OnClientClick="return validateform();" />
                            </div>

                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button runat="server" CssClass="btn btn-default btn-block" Text="Print" ID="btnPrint" OnClientClick="return PrintPage();" />
                            </div>

                        </div>
                         </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div runat="server" id="DivHead" class="header_div" style="text-align: center;">
                                <h5>SFA Technologies Pvt. Ltd.  
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

                            <div id="Div1" class="footer_div" runat="server">
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
    <script>
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }
            if (document.getElementById('<%=ddlFinancialYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Year. \n";
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
            }
            if (document.getElementById('<%=ddlEmp_TypeOfPost.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Type of Post. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }

        }
        function PrintPage() {
            //document.getElementById('print').style.display = 'none';
            //window.resizeTo(960, 600);
            //document.URL = "";
            //window.location.href = "";
            window.print();
        }
    </script>
</asp:Content>





