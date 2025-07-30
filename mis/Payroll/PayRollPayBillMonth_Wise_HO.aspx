<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayRollPayBillMonth_Wise_HO.aspx.cs" Inherits="mis_Payroll_PayRollPayBillMonth_Wise_HO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }

        table {
            white-space: nowrap;
        }

        .alignR {
            text-align: right !important;
        }
    </style>
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
                            <h3 class="box-title" id="Label1">Bank Wise Monthly Pay Bill Details</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label><span style="color: red">*</span>
                                        <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" ClientIDMode="Static">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem Value="1">Head Office</asp:ListItem>

                                        </asp:DropDownList>
                                        <small><span id="valddlOffice" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Year <span class="text-danger">*</span></label>
                                       <%-- <small><span id="valddlFinancialYear" class="text-danger"></span></small>--%>
                                        <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Month <span style="color: red;">*</span></label>
                                       <%-- <small><span id="valddlMonth" class="text-danger"></span></small>--%>
                                        <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                            <asp:ListItem Value="0">Select Month</asp:ListItem>
                                            <asp:ListItem Value="January">January</asp:ListItem>
                                            <asp:ListItem Value="February">February</asp:ListItem>
                                            <asp:ListItem Value="March">March</asp:ListItem>
                                            <asp:ListItem Value="April">April</asp:ListItem>
                                            <asp:ListItem Value="May">May</asp:ListItem>
                                            <asp:ListItem Value="June">June</asp:ListItem>
                                            <asp:ListItem Value="July">July</asp:ListItem>
                                            <asp:ListItem Value="August">August</asp:ListItem>
                                            <asp:ListItem Value="September">September</asp:ListItem>
                                            <asp:ListItem Value="October">October</asp:ListItem>
                                            <asp:ListItem Value="November">November</asp:ListItem>
                                            <asp:ListItem Value="December">December</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Type of Post (पद प्रकार) <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlEmp_TypeOfPost" runat="server" class="form-control">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem Value="Permanent">Regular/Permanent</asp:ListItem>
                                             <asp:ListItem Value="trainee">trainee</asp:ListItem>
                                            <%--<asp:ListItem Value="Fixed Employee">Fixed Employee(स्थाई कर्मी)</asp:ListItem>
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
                                             <asp:ListItem Value="Permanent NPF">Regular/Permanent NPF</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Bank name(बैंक का नाम)<span style="color: red;">*</span></label>
                                        <%-- <asp:TextBox ID="txtBank_Name" runat="server" placeholder="Enter Bank Name..." class="form-control" MaxLength="100"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlBank_Name" runat="server" placeholder="Select Bank Name..." class="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Show" ID="btnShow" OnClick="btnShow_Click" OnClientClick="return validateform();" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <a href="PayRollPayBillMonth_Wise.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>                             
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:HyperLink ID="btnbankPaybil" Target="_blank" class="btn btn-default" runat="server">Print Bank wise Paybill</asp:HyperLink>
                                    <asp:HyperLink ID="btnBankwise" Target="_blank" class="btn btn-default" runat="server">Print Cover Letter</asp:HyperLink>
                                    <%--<asp:LinkButton ID="btnBankwise"  class="btn btn-default" runat="server" OnClick="btnBankwise_Click">Print Cover Letter</asp:LinkButton>--%>
                                    <br />
                                    <div id="DivTable" runat="server">
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
            $("#valddlOffice").html("");
            $("#valddlFinancialYear").html("");
            $("#valddlMonth").html("");

            if (document.getElementById('<%=ddlFinancialYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select year. \n";
                $("#valddlFinancialYear").html("Select year.");
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
                $("#valddlMonth").html("Select Month.");
            }
            if (document.getElementById('<%=ddlEmp_TypeOfPost.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Type of Post. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }

        }
    </script>

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>
    <script src="../../mis/js/table2excel.js"></script>
    <script type="text/javascript">

        function Excel() {
            $("#EpfTable").table2excel({
                filename: "Table.xls"
            });
        }


    </script>
</asp:Content>

