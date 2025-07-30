<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollEarnDeductionDetailFullNew.aspx.cs" Inherits="mis_Payroll_PayrollEarnDeductionDetailFullNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .Grid td {
            padding: 3px !important;
        }

            .Grid td input {
                padding: 5px 3px !important;
                text-align: right;
                font-size: 12px;
            }

        .Grid th {
            text-align: center;
        }

        GridTotal th {
            text-align: center;
        }

        .GridTotal td input {
            padding: 5px 3px !important;
            text-align: right;
            font-size: 12px;
        }

        .ss {
            text-align: left !important;
        }

        .form-control[disabled] {
            background: #8fbc8f94;
        }

        .Grid td input {
            padding: 0px 0px !important;
            text-align: right;
            font-size: 14px;
            color: #828282;
            font-weight: 600;
            font-family: inherit;
        }

        .Grid td {
            font-weight: 600;
            font-family: inherit;
        }

        table.table.table-bordered.basic_detail {
            font-size: 11px;
            font-family: verdana;
            padding: 0px;
        }

        p.para_des {
            font-size: 12px;
            font-family: verdana;
            line-height: 16px;
        }
        .datafocus{
            color:#123456;
            font-size:14px;
            font-weight:600;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Earning & Deduction Detail</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control select2" AutoPostBack="True" OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Employee<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
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
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform1();" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group"></div>
                    <div class="row" runat="server" id="divDetail">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box-body">
                                    <div class="table table-responsive">
                                        <table class="table table-bordered basic_detail">
                                            <tbody>
                                                <tr>
                                                    <th style="width: 20%;">BANK ACCOUNT NUMBER :
                                                    </th>
                                                    <td style="width: 30%;">
                                                        <asp:Label ID="lblBank_AccountNo" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <th style="width: 10%;">EPF NUMBER :
                                                    </th>
                                                    <td style="width: 15%;">
                                                        <asp:Label ID="lblEPF_No" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <th style="width: 15%;">G.Ins NUMBER :
                                                    </th>
                                                    <td style="width: 10%;">
                                                        <asp:Label ID="lblGroupInsurance_No" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>BANK NAME :
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblBank_Name" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <th>IFSC CODE :
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblIFSCCode" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <th>BASIC SALARY :
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblSalary_NetSalary" runat="server" Text="" CssClass="basic_salary"></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>

                                        <p class="para_des">
                                            <div class="col-md-12">
                                                <div class="col-md-8">
                                                    => कृपया पहले यह सुनिश्चित करें की अटेंडेंस सेट की जा चुकी है |<br />
                                                    => अटेंडेंस के अनुसार बेसिक वेतन बनेगा और उसके अनुसार Permanent कर्मचारी का DA 
                                             <asp:Label ID="lblPermanent_DARate"  CssClass="datafocus"  runat="server" Text=""></asp:Label>% एवं स्थाई कर्मी का DA  
                                             <asp:Label ID="lblFixed_DARate"  CssClass="datafocus"  runat="server" Text=""></asp:Label>% स्वतः ही आजेगा |<br />
                                                    => इसी प्रकार से EPF = BASIC+DA का 12% स्वतः ही आ जाएगा|<br />
                                                    => प्रोफेशनल TAX 250-250 10 महीने तक स्वतः ही आजेगा|<br />
                                                    => कुछ भी परिवर्तन करने के पश्चात या अटेंडेंस सेट करने के पश्चात , कृपया Save बटन में क्लिक करके इसे सुरक्षित करें |<br />
                                                </div>
                                            
                                                <div class="col-md-4" style="color:teal; font-weight:400">
                                                     <%-- <b style="font-size:14px; color:orange">माह अप्रैल में एक दिन का  मूल वेतन  : -</b>
                                                    <asp:Label ID="lblarilbasic" CssClass="datafocus"   runat="server" Text=""></asp:Label><br />
                                                    <b style="font-size:16px; color:orange">एक दिन का  मूल वेतन  : -</b><br />
                                                    28 दिन के माह के अनुसार :
                                                    <asp:Label ID="lbl28daysBasic" CssClass="datafocus"   runat="server" Text=""></asp:Label><br />
                                                    29 दिन के माह के अनुसार :
                                                    <asp:Label ID="lbl29daysBasic" CssClass="datafocus"  runat="server" Text=""></asp:Label><br />
                                                    30 दिन के माह के अनुसार :
                                                    <asp:Label ID="lbl30daysBasic" CssClass="datafocus"   runat="server" Text=""></asp:Label><br />
                                                    31 दिन के माह के अनुसार  :
                                                    <asp:Label ID="lbl31daysBasic" CssClass="datafocus"  runat="server" Text=""></asp:Label>--%>
                                                </div>
                                            </div>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="box">
                                <div class="box-header" runat="server" id="HeaderEarning">
                                    <h3 class="box-title">Earning Detail</h3>
                                    <div class="box-tools pull-right">
                                        <button type="button" style="padding: 0px 10px;" class="btn btn-box-tool togglebody"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <asp:GridView ID="GridView1" runat="server" ClientIDMode="Static" class="table table-bordered table-striped Grid" AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="ID_1">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ID_1").ToString()%>' runat="server" />
                                                    <asp:Label ID="lblRowNumber_2" Visible="false" ToolTip='<%# Eval("ID_2").ToString()%>' runat="server" />
                                                    <asp:Label ID="lblEarnDeduction_ID" Text='<%# Eval("EarnDeduction_ID").ToString()%>' runat="server" Visible="false" />
                                                    <asp:Label ID="lblCalculation_Type" Text='<%# Eval("Calculation_Type").ToString()%>' runat="server" Visible="false" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EarnDeduction_Name" HeaderText="Name" HeaderStyle-CssClass="ss" />

                                            <asp:TemplateField HeaderText="Apr" ItemStyle-Width="73" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtApr_Amount" runat="server" Text='<%# Eval("Apr_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="EarningTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="May" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMay_Amount" runat="server" Text='<%# Eval("May_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="EarningTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jun" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtJun_Amount" runat="server" Text='<%# Eval("Jun_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="EarningTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jul" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtJul_Amount" runat="server" Text='<%# Eval("Jul_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="EarningTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aug" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAug_Amount" runat="server" Text='<%# Eval("Aug_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="EarningTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sep" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSep_Amount" runat="server" Text='<%# Eval("Sep_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="EarningTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Oct" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOct_Amount" runat="server" Text='<%# Eval("Oct_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="EarningTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nov" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNov_Amount" runat="server" Text='<%# Eval("Nov_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="EarningTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dec" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDec_Amount" runat="server" Text='<%# Eval("Dec_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="EarningTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jan" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtJan_Amount" runat="server" Text='<%# Eval("Jan_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="EarningTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Feb" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFeb_Amount" runat="server" Text='<%# Eval("Feb_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="EarningTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mar" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMar_Amount" runat="server" Text='<%# Eval("Mar_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="EarningTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                        <table class="table table-bordered table-striped Grid">
                                        <tbody>
                                            <tr>
                                                <th style="width: 20%;"></th>
                                                <th>Apr</th>
                                                <th>May</th>
                                                <th>Jun</th>
                                                <th>Jul</th>
                                                <th>Aug</th>
                                                <th>Sep</th>
                                                <th>Oct</th>
                                                <th>Nov</th>
                                                <th>Dec</th>
                                                <th>Jan</th>
                                                <th>Feb</th>
                                                <th>Mar</th>

                                            </tr>
                                            <tr>
                                                <th>Basic</th>

                                                <td>
                                                    <asp:TextBox ID="txtEarTotApr" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtEarTotMay" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtEarTotJun" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtEarTotJul" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtEarTotAug" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtEarTotSep" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtEarTotOct" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtEarTotNov" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtEarTotDec" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtEarTotJan" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtEarTotFeb" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtEarTotMar" ClientIDMode="Static" class="form-control" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <th>Gross Amount</th>
                                                <td>
                                                    <asp:TextBox ID="txtGrossToApr" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossToMay" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossToJun" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossToJul" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossToAug" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossToSep" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossToOct" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossToNov" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossToDec" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossToJan" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossToFeb" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtGrossToMar" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="box-header" runat="server" id="HeaderDeduction">
                                    <h3 class="box-title">Deduction Detail</h3>
                                    <div class="box-tools pull-right">
                                        <button type="button" style="padding: 0px 10px;" class="btn btn-box-tool togglebody"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <asp:GridView ID="GridView2" runat="server" ClientIDMode="Static" class="table table-bordered table-striped Grid" AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="ID_1" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ID_1").ToString()%>' runat="server" />
                                                    <asp:Label ID="lblRowNumber_2" Visible="false" ToolTip='<%# Eval("ID_2").ToString()%>' runat="server" />
                                                    <asp:Label ID="lblEarnDeduction_ID" Text='<%# Eval("EarnDeduction_ID").ToString()%>' runat="server" Visible="false" />
                                                    <asp:Label ID="lblCalculation_Type" Text='<%# Eval("Calculation_Type").ToString()%>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblRowNumber" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <%--  <asp:BoundField DataField="EarnDeduction_Name" HeaderText="Name" HeaderStyle-CssClass="ss" />--%>
                                            <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="ss">
                                                <ItemTemplate>
                                                    <asp:Label ID="EarnDeduction_Name" Text='<%# Eval("EarnDeduction_Name").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblEarnDeduction_Name" Text='Policy' runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Apr" ItemStyle-Width="73" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtApr_Amount" runat="server" Text='<%# Eval("Apr_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="DeductionTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtApr_Amount" runat="server" class="form-control" MaxLength="9" Enabled="false" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center> <asp:LinkButton ID="btnApr_Amount" runat="server" CssClass="label label-info" OnClick="btnApr_Amount_Click">View</asp:LinkButton>
                                                </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="May" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMay_Amount" runat="server" Text='<%# Eval("May_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="DeductionTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtMay_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center> <asp:LinkButton ID="btnMay_Amount" runat="server" CssClass="label label-info" OnClick="btnMay_Amount_Click">View</asp:LinkButton>
                                                </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jun" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtJun_Amount" runat="server" Text='<%# Eval("Jun_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="DeductionTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtJun_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center>  <asp:LinkButton ID="btnJun_Amount" runat="server" CssClass="label label-info" OnClick="btnJun_Amount_Click">View</asp:LinkButton>
                                              </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jul" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtJul_Amount" runat="server" Text='<%# Eval("Jul_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="DeductionTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtJul_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center>  <asp:LinkButton ID="btnJul_Amount" runat="server" CssClass="label label-info" OnClick="btnJul_Amount_Click">View</asp:LinkButton>
                                               </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aug" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAug_Amount" runat="server" Text='<%# Eval("Aug_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="DeductionTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtAug_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center><asp:LinkButton ID="btnAug_Amount" runat="server" CssClass="label label-info" OnClick="btnAug_Amount_Click">View</asp:LinkButton>
                                                </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sep" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSep_Amount" runat="server" Text='<%# Eval("Sep_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="DeductionTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtSep_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center> <asp:LinkButton ID="btnSep_Amount" runat="server" CssClass="label label-info" OnClick="btnSep_Amount_Click">View</asp:LinkButton>
                                                </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Oct" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOct_Amount" runat="server" Text='<%# Eval("Oct_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="DeductionTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtOct_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center>  <asp:LinkButton ID="btnOct_Amount" runat="server" CssClass="label label-info" OnClick="btnOct_Amount_Click">View</asp:LinkButton>
                                                </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nov" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNov_Amount" runat="server" Text='<%# Eval("Nov_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="DeductionTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtNov_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center>  <asp:LinkButton ID="btnNov_Amount" runat="server" CssClass="label label-info" OnClick="btnNov_Amount_Click">View</asp:LinkButton>
                                               </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dec" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDec_Amount" runat="server" Text='<%# Eval("Dec_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="DeductionTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtDec_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center>  <asp:LinkButton ID="btnDec_Amount" runat="server" CssClass="label label-info" OnClick="btnDec_Amount_Click">View</asp:LinkButton>
                                               </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jan" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtJan_Amount" runat="server" Text='<%# Eval("Jan_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="DeductionTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtJan_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center> <asp:LinkButton ID="btnJan_Amount" runat="server" CssClass="label label-info" OnClick="btnJan_Amount_Click">View</asp:LinkButton>
                                               </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Feb" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFeb_Amount" runat="server" Text='<%# Eval("Feb_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="DeductionTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtFeb_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center><asp:LinkButton ID="btnFeb_Amount" runat="server" CssClass="label label-info" OnClick="btnFeb_Amount_Click">View</asp:LinkButton>
                                                </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mar" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMar_Amount" runat="server" Text='<%# Eval("Mar_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onfocusout="DeductionTotal();" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtMar_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center> <asp:LinkButton ID="btnMar_Amount" runat="server" CssClass="label label-info" OnClick="btnMar_Amount_Click">View</asp:LinkButton></center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <table class="table table-bordered table-striped Grid">
                                        <tbody>
                                            <tr>
                                                <th style="width: 21%;"></th>
                                                <th>Apr</th>
                                                <th>May</th>
                                                <th>Jun</th>
                                                <th>Jul</th>
                                                <th>Aug</th>
                                                <th>Sep</th>
                                                <th>Oct</th>
                                                <th>Nov</th>
                                                <th>Dec</th>
                                                <th>Jan</th>
                                                <th>Feb</th>
                                                <th>Mar</th>

                                            </tr>
                                            <tr>
                                                <th>Deduction Total</th>
                                                <td>
                                                    <asp:TextBox ID="txtDedTotApr" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDedTotMay" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDedTotJun" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDedTotJul" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDedTotAug" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDedTotSep" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDedTotOct" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDedTotNov" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDedTotDec" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDedTotJan" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDedTotFeb" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDedTotMar" ClientIDMode="Static" class="form-control" runat="server" Enabled="false"></asp:TextBox></td>

                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="row">
                                    <div class="col-md-10"></div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                                    </div>
                                    <%--                         <div class="col-md-2">
                                        <a class="btn btn-block btn-default" href="PayrollEarnDeductionDetail.aspx">Clear</a>
                                    </div>--%>
                                </div>
                                <div class="form-group"></div>
                            </div>

                        </div>

                    </div>

                </div>
            </div>


            <div id="ModalPolicyDetail" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Policy Detail</h4>
                        </div>
                        <div class="modal-body">
                            <asp:GridView ID="GridView3" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="true" AllowPaging="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="modal-footer">
                            <a type="button" class="btn btn-default" href="PayrollPolicyDetail.aspx" target="_blank">Edit Policy Detail</a>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">

        // alert($(".basic_salary").text());

        $('.togglebody').click(function () {
            $(this).parent().parent().next().slideToggle(1000);
            if ($(this).children().hasClass("fa-minus")) {
                $(this).children().removeClass("fa-minus");
                $(this).children().addClass("fa-plus");
            } else {
                $(this).children().removeClass("fa-plus");
                $(this).children().addClass("fa-minus");
            }

        });


        function MonthDetail() {
            $('#ModalPolicyDetail').modal('show');
        }
        function validateform1() {
            var msg = "";
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Year. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;

            }
        }
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Financial Year. \n";
            }
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
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


        $(document).ready(function () {
            var $cells = $(".table tr td");
            $inputs = $cells.find('input[type="text"]');
            $.each($inputs, function () {
                var value = this.value;
                if (value > 0) {
                    $(this).css({ 'color': 'darkblue' });
                }
            });
        });

        function ColorText() {
            var $cells = $(".table tr td");
            $inputs = $cells.find('input[type="text"]');
            $.each($inputs, function () {
                var value = this.value;
                if (value > 0) {
                    $(this).css({ 'color': 'darkblue' });
                }
            });
        }

        /// Earning Total
        function EarningTotal() {

            var TotApr_Amount = 0.00;
            var TotMay_Amount = 0.00;
            var TotJun_Amount = 0.00;
            var TotJul_Amount = 0.00;
            var TotAug_Amount = 0.00;
            var TotSep_Amount = 0.00;
            var TotOct_Amount = 0.00;
            var TotNov_Amount = 0.00;
            var TotDec_Amount = 0.00;
            var TotJan_Amount = 0.00;
            var TotFeb_Amount = 0.00;
            var TotMar_Amount = 0.00;


            var i = 0;
            var Tval = 0;

            $('#GridView1 tr').each(function (index) {

                if (i > 0) {

                    var Apr_Amount = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var May_Amount = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var Jun_Amount = $(this).children("td").eq(4).find('input[type="text"]').val();
                    var Jul_Amount = $(this).children("td").eq(5).find('input[type="text"]').val();
                    var Aug_Amount = $(this).children("td").eq(6).find('input[type="text"]').val();
                    var Sep_Amount = $(this).children("td").eq(7).find('input[type="text"]').val();
                    var Oct_Amount = $(this).children("td").eq(8).find('input[type="text"]').val();
                    var Nov_Amount = $(this).children("td").eq(9).find('input[type="text"]').val();
                    var Dec_Amount = $(this).children("td").eq(10).find('input[type="text"]').val();
                    var Jan_Amount = $(this).children("td").eq(11).find('input[type="text"]').val();
                    var Feb_Amount = $(this).children("td").eq(12).find('input[type="text"]').val();
                    var Mar_Amount = $(this).children("td").eq(13).find('input[type="text"]').val();

                    if (Apr_Amount == "")
                        Apr_Amount = 0;
                    if (May_Amount == "")
                        May_Amount = 0;
                    if (Jun_Amount == "")
                        Jun_Amount = 0;
                    if (Jul_Amount == "")
                        Jul_Amount = 0;
                    if (Aug_Amount == "")
                        Aug_Amount = 0;
                    if (Sep_Amount == "")
                        Sep_Amount = 0;
                    if (Oct_Amount == "")
                        Oct_Amount = 0;
                    if (Nov_Amount == "")
                        Nov_Amount = 0;
                    if (Dec_Amount == "")
                        Dec_Amount = 0;
                    if (Jan_Amount == "")
                        Jan_Amount = 0;
                    if (Feb_Amount == "")
                        Feb_Amount = 0;
                    if (Mar_Amount == "")
                        Mar_Amount = 0;

                    TotApr_Amount = parseFloat(parseFloat(TotApr_Amount) + parseFloat(Apr_Amount)).toFixed(2);
                    TotMay_Amount = parseFloat(parseFloat(TotMay_Amount) + parseFloat(May_Amount)).toFixed(2);
                    TotJun_Amount = parseFloat(parseFloat(TotJun_Amount) + parseFloat(Jun_Amount)).toFixed(2);
                    TotJul_Amount = parseFloat(parseFloat(TotJul_Amount) + parseFloat(Jul_Amount)).toFixed(2);
                    TotAug_Amount = parseFloat(parseFloat(TotAug_Amount) + parseFloat(Aug_Amount)).toFixed(2);
                    TotSep_Amount = parseFloat(parseFloat(TotSep_Amount) + parseFloat(Sep_Amount)).toFixed(2);
                    TotOct_Amount = parseFloat(parseFloat(TotOct_Amount) + parseFloat(Oct_Amount)).toFixed(2);
                    TotNov_Amount = parseFloat(parseFloat(TotNov_Amount) + parseFloat(Nov_Amount)).toFixed(2);
                    TotDec_Amount = parseFloat(parseFloat(TotDec_Amount) + parseFloat(Dec_Amount)).toFixed(2);
                    TotJan_Amount = parseFloat(parseFloat(TotJan_Amount) + parseFloat(Jan_Amount)).toFixed(2);
                    TotFeb_Amount = parseFloat(parseFloat(TotFeb_Amount) + parseFloat(Feb_Amount)).toFixed(2);
                    TotMar_Amount = parseFloat(parseFloat(TotMar_Amount) + parseFloat(Mar_Amount)).toFixed(2);
                }
                i++;
            });



            document.getElementById('<%=txtEarTotApr.ClientID%>').value = TotApr_Amount;
            document.getElementById('<%=txtEarTotMay.ClientID%>').value = TotMay_Amount;
            document.getElementById('<%=txtEarTotJun.ClientID%>').value = TotJun_Amount;
            document.getElementById('<%=txtEarTotJul.ClientID%>').value = TotJul_Amount;
            document.getElementById('<%=txtEarTotAug.ClientID%>').value = TotAug_Amount;
            document.getElementById('<%=txtEarTotSep.ClientID%>').value = TotSep_Amount;
            document.getElementById('<%=txtEarTotOct.ClientID%>').value = TotOct_Amount;
            document.getElementById('<%=txtEarTotNov.ClientID%>').value = TotNov_Amount;
            document.getElementById('<%=txtEarTotDec.ClientID%>').value = TotDec_Amount;
            document.getElementById('<%=txtEarTotJan.ClientID%>').value = TotJan_Amount;
            document.getElementById('<%=txtEarTotFeb.ClientID%>').value = TotFeb_Amount;
            document.getElementById('<%=txtEarTotMar.ClientID%>').value = TotMar_Amount;

            ColorText();
        }

        // Deduction Detail Total

        function DeductionTotal() {

            var TotApr_Amount = 0.00;
            var TotMay_Amount = 0.00;
            var TotJun_Amount = 0.00;
            var TotJul_Amount = 0.00;
            var TotAug_Amount = 0.00;
            var TotSep_Amount = 0.00;
            var TotOct_Amount = 0.00;
            var TotNov_Amount = 0.00;
            var TotDec_Amount = 0.00;
            var TotJan_Amount = 0.00;
            var TotFeb_Amount = 0.00;
            var TotMar_Amount = 0.00;


            var i = 0;
            var Tval = 0;

            $('#GridView2 tr').each(function (index) {

                if (i > 0) {

                    var Apr_Amount = $(this).children("td").eq(2).find('input[type="text"]').val();
                    var May_Amount = $(this).children("td").eq(3).find('input[type="text"]').val();
                    var Jun_Amount = $(this).children("td").eq(4).find('input[type="text"]').val();
                    var Jul_Amount = $(this).children("td").eq(5).find('input[type="text"]').val();
                    var Aug_Amount = $(this).children("td").eq(6).find('input[type="text"]').val();
                    var Sep_Amount = $(this).children("td").eq(7).find('input[type="text"]').val();
                    var Oct_Amount = $(this).children("td").eq(8).find('input[type="text"]').val();
                    var Nov_Amount = $(this).children("td").eq(9).find('input[type="text"]').val();
                    var Dec_Amount = $(this).children("td").eq(10).find('input[type="text"]').val();
                    var Jan_Amount = $(this).children("td").eq(11).find('input[type="text"]').val();
                    var Feb_Amount = $(this).children("td").eq(12).find('input[type="text"]').val();
                    var Mar_Amount = $(this).children("td").eq(13).find('input[type="text"]').val();

                    if (Apr_Amount == "")
                        Apr_Amount = 0;
                    if (May_Amount == "")
                        May_Amount = 0;
                    if (Jun_Amount == "")
                        Jun_Amount = 0;
                    if (Jul_Amount == "")
                        Jul_Amount = 0;
                    if (Aug_Amount == "")
                        Aug_Amount = 0;
                    if (Sep_Amount == "")
                        Sep_Amount = 0;
                    if (Oct_Amount == "")
                        Oct_Amount = 0;
                    if (Nov_Amount == "")
                        Nov_Amount = 0;
                    if (Dec_Amount == "")
                        Dec_Amount = 0;
                    if (Jan_Amount == "")
                        Jan_Amount = 0;
                    if (Feb_Amount == "")
                        Feb_Amount = 0;
                    if (Mar_Amount == "")
                        Mar_Amount = 0;

                    TotApr_Amount = parseFloat(parseFloat(TotApr_Amount) + parseFloat(Apr_Amount)).toFixed(2);
                    TotMay_Amount = parseFloat(parseFloat(TotMay_Amount) + parseFloat(May_Amount)).toFixed(2);
                    TotJun_Amount = parseFloat(parseFloat(TotJun_Amount) + parseFloat(Jun_Amount)).toFixed(2);
                    TotJul_Amount = parseFloat(parseFloat(TotJul_Amount) + parseFloat(Jul_Amount)).toFixed(2);
                    TotAug_Amount = parseFloat(parseFloat(TotAug_Amount) + parseFloat(Aug_Amount)).toFixed(2);
                    TotSep_Amount = parseFloat(parseFloat(TotSep_Amount) + parseFloat(Sep_Amount)).toFixed(2);
                    TotOct_Amount = parseFloat(parseFloat(TotOct_Amount) + parseFloat(Oct_Amount)).toFixed(2);
                    TotNov_Amount = parseFloat(parseFloat(TotNov_Amount) + parseFloat(Nov_Amount)).toFixed(2);
                    TotDec_Amount = parseFloat(parseFloat(TotDec_Amount) + parseFloat(Dec_Amount)).toFixed(2);
                    TotJan_Amount = parseFloat(parseFloat(TotJan_Amount) + parseFloat(Jan_Amount)).toFixed(2);
                    TotFeb_Amount = parseFloat(parseFloat(TotFeb_Amount) + parseFloat(Feb_Amount)).toFixed(2);
                    TotMar_Amount = parseFloat(parseFloat(TotMar_Amount) + parseFloat(Mar_Amount)).toFixed(2);
                }
                i++;
            });


            document.getElementById('<%=txtDedTotApr.ClientID%>').value = TotApr_Amount;
            document.getElementById('<%=txtDedTotMay.ClientID%>').value = TotMay_Amount;
            document.getElementById('<%=txtDedTotJun.ClientID%>').value = TotJun_Amount;
            document.getElementById('<%=txtDedTotJul.ClientID%>').value = TotJul_Amount;
            document.getElementById('<%=txtDedTotAug.ClientID%>').value = TotAug_Amount;
            document.getElementById('<%=txtDedTotSep.ClientID%>').value = TotSep_Amount;
            document.getElementById('<%=txtDedTotOct.ClientID%>').value = TotOct_Amount;
            document.getElementById('<%=txtDedTotNov.ClientID%>').value = TotNov_Amount;
            document.getElementById('<%=txtDedTotDec.ClientID%>').value = TotDec_Amount;
            document.getElementById('<%=txtDedTotJan.ClientID%>').value = TotJan_Amount;
            document.getElementById('<%=txtDedTotFeb.ClientID%>').value = TotFeb_Amount;
            document.getElementById('<%=txtDedTotMar.ClientID%>').value = TotMar_Amount;

            ColorText();
        }





    </script>
</asp:Content>


