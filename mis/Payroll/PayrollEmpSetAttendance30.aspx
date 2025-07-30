<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollEmpSetAttendance30.aspx.cs" Inherits="mis_Payroll_PayrollEmpSetAttendance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .chk1 {
            padding: 2px !important;
        }

        .box {
            min-height: 135px;
        }

        .Grid td {
            padding: 1px !important;
        }

            .Grid td select {
                padding: 3px 0px !important;
                text-align: right;
                font-size: 15px;
                font-weight: 700;
                width: 70px;
            }

        .Grid th {
            text-align: center;
        }

        .ss {
            text-align: left !important;
            min-width: 168px !important;
        }

        .ss1 {
            text-align: left !important;
            min-width: 168px !important;
        }

        .sss {
            text-align: left !important;
            min-width: 110px !important;
        }

        .sss1 {
            text-align: left !important;
            min-width: 96px !important;
        }

        .ss70 {
            text-align: left !important;
            min-width: 70px !important;
        }

        .ss72 {
            text-align: left !important;
            text-align: center !important;
            padding: 2px 2px !important;
        }

        thead {
            display: block;
            overflow: auto;
        }

        tbody {
            display: block;
            height: 440px;
            overflow: auto;
        }

        .Gridpp {
            width: 42px !important;
            padding: 8px 2px !important;
            text-align: center !important;
        }

        table > thead > tr > th {
            background-image: linear-gradient(to bottom, #14b4d6, #1291ac);
            color: #fff;
            padding: 2px 2px !important;
        }

        .table > tbody > tr > td {
            vertical-align: middle;
            padding-top: 2px;
            padding-bottom: 2px;
            padding: 2px 2px !important;
        }

        .table > tbody > tr {
            line-height: 1;
        }

        table {
            font-size: 11px;
        }

        .ss75 {
            width: 75px;
        }

        .table-bordered > thead > tr > th {
            border: 1px solid #0e839c;
        }

        .table-bordered > tbody > tr > th {
            position: sticky !important;
            z-index: 100 !important;
            top: 0 !important;
        }


        @media print {
            .print_hidden {
                display: none !important;
            }

            .table-responsive {
                overflow: visible;
            }

            #myInput, .main-footer {
                display: none;
            }

            select {
                border: none !important;
            }

            @page {
                size: landscape;
            }

            .table > tbody > tr > td {
                vertical-align: middle;
                padding: 2px 2px !important;
                font-size: 13px;
                font-family: Verdana;
            }

            select {
                /* for Firefox */
                -moz-appearance: none;
                /* for Chrome */
                -webkit-appearance: none;
            }

                /* For IE10 */
                select::-ms-expand {
                    display: none;
                }

            .table-bordered > tbody > tr > th {
                padding: 2px !important;
                font-size: 13px;
                font-family: Verdana;
                border: 1px solid #ddd !important;
            }

            table td:nth-child(1), table th:nth-child(1) {
                display: none;
            }

            #GridView1 {
                border: none;
            }

            .table-bordered > tbody > tr > th {
                border: 1px solid #ddd !important;
            }

            #GridView1 tr {
                border: 1px solid #ddd !important;
            }

            .table-responsive {
                overflow: visible;
            }

            .box-body, .box, tbody {
                height: auto !important;
            }

                .box.box-success {
                    border: none;
                }

            .table-bordered > tbody > tr > th {
                z-index: initial !important;
                top: initial !important;
                background: initial !important;
                position: initial !important;
            }

            .headingOfDept {
                display: block !important;
            }
        }

        .salary-logo {
            -webkit-filter: grayscale(100%);
            filter: grayscale(100%);
            width: 40px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success" style="min-height: 500px;">
                <div class="box-header">
                    <h3 class="box-title print_hidden">Employee Attendance</h3>
                    <span  class="print_hidden" style="font-weight:600; color:red;">[User can't search data after August, 2021.]</span>
                </div>
                <asp:Label ID="lblMsg" CssClass="print_hidden" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row print_hidden">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Month <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlMonth" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Select Month</asp:ListItem>
                                    <asp:ListItem Value="01">January</asp:ListItem>
                                    <asp:ListItem Value="02">February</asp:ListItem>
                                    <asp:ListItem Value="03">March</asp:ListItem>
                                    <asp:ListItem Value="04">April</asp:ListItem>
                                    <asp:ListItem Value="05">May</asp:ListItem>
                                    <asp:ListItem Value="06">June</asp:ListItem>
                                    <asp:ListItem Value="07">July</asp:ListItem>
                                    <asp:ListItem Value="08">August</asp:ListItem>
                                    <asp:ListItem Value="09">September</asp:ListItem>
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
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group"></div>
                    <div id="DivDetail" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box-header print_hidden">
                                    <h3 class="box-title">Please set attendance for selected month salary generation. 
                                        <p style="color:red; font-size: 15px; line-height: 21px;">
                                            <asp:Label ID="lblNotify" runat="server" Text=""></asp:Label>
                                        </p>           
                                        <p style="color:blue; font-size:12px;" class="hidden-print">नोट : प्रिंट करने के लिए कृपया Ctrl+P का उपयोग करें |</p>                   
                                    </h3>
                                </div>
                                <div class="col-md-12 headingOfDept" style="display:none"">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="text-center">
                                                <img src="../image/mpagro-logo.png" class="salary-logo" /><br />
                                            </div>


                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <p style="text-align: center">
                                                THE MADHYA PRADSH STATE AGRO INDUSTRIES DEVELOPMENT CORPORATION LIMITED<br />
                                                "PANCHANAN" 3rd FLOOR,MALAVIYA NAGAR BHOPAL<br />
                                                Phone(0755) - 2552652,2551756,2551807 Fax: 0755-2557305   
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <p style="text-align: left;">
                                                <asp:Label ID="lblSelectedItems" runat="server" Text=""></asp:Label></p>
                                        </div>

                                        <div class="col-md-6">
                                            <p style="text-align: right;">
                                                Date :  <asp:Label ID="lblAttDate" runat="server" Text=""></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                </div>

                                <div class="table-responsive" style="">
                                    <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped" ClientIDMode="Static" AutoGenerateColumns="False" AllowPaging="false" EmptyDataText="No Record Found">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-CssClass="chk1">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server"  Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SNo." HeaderStyle-CssClass="Gridpp" ItemStyle-CssClass="Gridpp text-center">
                                                <ItemTemplate>
                                                    <div style="">
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Emp_ID").ToString()%>' runat="server" />
                                                        <asp:Label ID="lblGenerateStatus" Text='<%# Eval("GenerateStatus").ToString()%>' runat="server" Visible="false" />
                                                        <asp:Label ID="lblEmp_ID" Text='<%# Eval("Emp_ID").ToString()%>' runat="server" Visible="false" />
                                                    </div>
                                                     <asp:CheckBox ID="chkSelect1" runat="server" CssClass="hidden" Checked='<%# Eval("Checked").ToString() == "true" ? true  :  false %>' Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" HeaderStyle-CssClass="ss" ItemStyle-CssClass="ss1" />

                                                                                       <%-- --------------%>
                                            <asp:TemplateField HeaderText=" 21" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay21" runat="server" CssClass="21" SelectedValue='<%#Bind("Day21") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 22" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay22" runat="server" CssClass="22" SelectedValue='<%#Bind("Day22") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 23" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay23" runat="server" CssClass="23" SelectedValue='<%#Bind("Day23") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 24" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay24" runat="server" CssClass="24" SelectedValue='<%#Bind("Day24") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 25" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay25" runat="server" CssClass="25" SelectedValue='<%#Bind("Day25") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 26" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay26" runat="server" CssClass="26" SelectedValue='<%#Bind("Day26") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 27" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay27" runat="server" CssClass="27" SelectedValue='<%#Bind("Day27") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 28" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay28" runat="server" CssClass="28" SelectedValue='<%#Bind("Day28") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 29" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay29" runat="server" CssClass="29" SelectedValue='<%#Bind("Day29") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 30" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay30" runat="server" CssClass="30" SelectedValue='<%#Bind("Day30") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText=" 31" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay31" runat="server" CssClass="31" class="form-control" SelectedValue='<%#Bind("Day31") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                          <%--  ---------------%>

                                            <asp:TemplateField HeaderText=" 1" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay1" runat="server" CssClass="1" SelectedValue='<%#Bind("Day1") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 2" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay2" runat="server" CssClass="2" SelectedValue='<%#Bind("Day2") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 3" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay3" runat="server" CssClass="3" SelectedValue='<%#Bind("Day3") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 4" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay4" runat="server" CssClass="4" SelectedValue='<%#Bind("Day4") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 5" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay5" runat="server" CssClass="5" SelectedValue='<%#Bind("Day5") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 6" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay6" runat="server" CssClass="6" SelectedValue='<%#Bind("Day6") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 7" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>

                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay7" runat="server" CssClass="7" SelectedValue='<%#Bind("Day7") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 8" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay8" runat="server" CssClass="8" SelectedValue='<%#Bind("Day8") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 9" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay9" runat="server" CssClass="9" SelectedValue='<%#Bind("Day9") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 10" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay10" runat="server" CssClass="10" SelectedValue='<%#Bind("Day10") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 11" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay11" runat="server" CssClass="11" SelectedValue='<%#Bind("Day11") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 12" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay12" runat="server" CssClass="12" SelectedValue='<%#Bind("Day12") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 13" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay13" runat="server" CssClass="13" SelectedValue='<%#Bind("Day13") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 14" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay14" runat="server" CssClass="14" SelectedValue='<%#Bind("Day14") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 15" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay15" runat="server" CssClass="15" SelectedValue='<%#Bind("Day15") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 16" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay16" runat="server" CssClass="16" SelectedValue='<%#Bind("Day16") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 17" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay17" runat="server" CssClass="17" SelectedValue='<%#Bind("Day17") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 18" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay18" runat="server" CssClass="18" SelectedValue='<%#Bind("Day18") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 19" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay19" runat="server" CssClass="19" SelectedValue='<%#Bind("Day19") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" 20" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="ss72">
                                                <ItemTemplate>
                                                    <asp:DropDownList onchange="change(this);" ID="ddlDay20" runat="server" CssClass="20" SelectedValue='<%#Bind("Day20") %>' ClientIDMode="Static" Enabled='<%# Eval("GenerateStatus").ToString() == "Not Generated" ? true : false %>'>
                                                        <asp:ListItem Value="P">P</asp:ListItem>
                                                        <asp:ListItem Value="A">A</asp:ListItem>
                                                        <asp:ListItem Value="H">H</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Payable Days" HeaderStyle-HorizontalAlign="Center" HeaderStyle-CssClass="sss" ItemStyle-CssClass="sss1">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPayableDays" runat="server" class="form-control" Text='<%#Bind("PayableDays") %>' Enabled="false" onkeypress="return validateNum(event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="form-group"></div>
                        <div class="row">
                            <div class="col-md-10"></div>
                            <div class="col-md-2">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success print_hidden" runat="server" Text="Save" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </div>
                </div>
               
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>

        $('#checkAll').click(function () {
            var inputList = document.querySelectorAll('#GridView1 tbody input[type="checkbox"]:not(:disabled)');
            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('checkAll').checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });



        $(document).ready(function () {
            //Loop through each checkbox in gridview
            //Change the GridView id here
            $("#<%=GridView1.ClientID %> input:checkbox").each(function () {
                this.onclick = function () {
                    if (this.checked)
                        this.parentNode.style.backgroundColor = 'white';
                    else
                        this.parentNode.style.backgroundColor = 'green';

                }
            })

            var checkbox = $('table tbody input[type="checkbox"]:checked(:checked)');
            for (var i = 0; i < checkbox.length; i++) {
                //  $(checkbox[i]).parents('td').css('background-color', 'green');
                $(checkbox[i]).parents('tr').css({ 'background-color': 'rgb(255, 157, 157)', 'color': '#000' });
                //  $(checkbox[i]).parents('tr').find('select').css({ 'color': '#000' });
                //  $(checkbox[i]).parents('tr').find('select').css({ 'background': 'rgb(255, 209, 209)' });
            }

        });



        //$(document).ready(function () {
        //    $('<thead></thead>').prependTo('#GridView1').append($('#GridView1 tr:first'));
        //    $('#GridView1 thead tr th:first').css('width', '36px !important');
        //});


        window.onload =
          $('select').each(function () {
              var str = $(this).val();
              if (str == 'A') {
                  $(this).css('color', 'red');
              }
              else if (str == 'H') {
                  $(this).css('color', 'blue');
                  $(this).css('border', '1px solid green;');
              }


          });
       <%-- var d = new Date();
        var month = $('#<%= ddlMonth.ClientID %>').find("option:selected").val();
        var mm = month;
        month = month - 1;
        var year = $('#<%= ddlYear.ClientID %>').find("option:selected").val();
        var getTot = daysInMonth(mm, year);
        var getLastMonthTotDays = 0;
        var LastMonth = 0;
        var LastYear = 0
        debugger;
        if (month == 0) {
            LastMonth = 12;
            LastYear = year - 1;
        }
        else {
            LastMonth = month - 1;
            LastYear = year;
        }
        getLastMonthTotDays = daysInMonth(LastMonth, LastYear);
        if (LastMonth == 12)
            LastMonth = 11;
        // console.log(getLastMonthTotDays);
        //  console.log(daysInMonth(d.getMonth(), d.getFullYear()))
        var sat = new Array();
        var sun = new Array();

        // Current Month
        for (var i = 1; i <= getLastMonthTotDays; i++) {
            if (i <= 20) {
                debugger;
                var newDate = new Date(year, month, i)
                console.log(i + "-" + newDate.getDay());
                if (newDate.getDay() == 0) {
                    sat.push(i)
                }
                if (newDate.getDay() == 6) {
                    sun.push(i)
                }
            }
            else {
                debugger;

                var newDateLastMonth = new Date(LastYear, LastMonth, i)
                console.log(i + "-" + newDateLastMonth.getDay());
                if (newDateLastMonth.getDay() == 0) {
                    sat.push(i)
                }
                if (newDateLastMonth.getDay() == 6) {
                    sun.push(i)
                }
            }

        }--%>
        // Last Month
        //for (var i = 21; i <= getLastMonthTotDays; i++) {

        //}
        //var max = sat.length;
        //var x = 0;
        //for (x = 0; x < max; x++) {
        //    var t = sat[x];
        //    t = '.' + t;
        //    $(t).each(function () {
        //        $(this).parent().html('<div style="width:8px; text-align:center;"><label style="text-align:center;color:red;font-weight:bold;font-size:12px;margin-bottom:0px;">S<label></div>');

        //    });
        //}
        //function daysInMonth(month, year) {

        //    return new Date(year, month, 0).getDate();
        //}

        function change(ddl) {
            debugger;
            var str = ddl.selectedIndex;
            if (str == 1) {
                ddl.style.color = "red";
                ddl.style.border = "1px solid red;";
            }
            else if (str == 2) {
                ddl.style.color = "blue";
                ddl.style.border = "1px solid blue;";
            }
            else {
                ddl.style.color = "black";
                ddl.style.border = "1px solid black;";
                // border: 1px solid green;
            }

        }

        function validateform() {
            var msg = "";

            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Financial Year. \n";
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {

                return true;
            }
        }

    </script>
</asp:Content>



