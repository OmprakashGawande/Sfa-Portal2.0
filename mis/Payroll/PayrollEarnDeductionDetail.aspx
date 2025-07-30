<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollEarnDeductionDetail.aspx.cs" Inherits="mis_Payroll_PayrollEarnDeductionDetail" %>

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

        .ss {
            text-align: left !important;
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
                                        <table class="table table-bordered ">
                                            <tbody>
                                                <tr>
                                                    <th style="width: 25%;">BANK ACCOUNT NUMBER :
                                                    </th>
                                                    <td style="width: 25%;">
                                                        <asp:Label ID="lblBank_AccountNo" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <th style="width: 25%;">EPF NUMBER :
                                                    </th>
                                                    <td style="width: 25%;">
                                                        <asp:Label ID="lblEPF_No" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>BANK NAME :
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblBank_Name" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <th>G.Ins NUMBER :
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblGroupInsurance_No" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <th>IFSC CODE :
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblIFSCCode" runat="server" Text=""></asp:Label>
                                                    </td>
                                                    <th>BASIC SALARY :
                                                    </th>
                                                    <td>
                                                        <asp:Label ID="lblSalary_NetSalary" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
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
                                    <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped Grid" AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="ID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ID").ToString()%>' runat="server" />
                                                    <asp:Label ID="lblEarnDeduction_ID" Text='<%# Eval("EarnDeduction_ID").ToString()%>' runat="server" Visible="false" />
                                                    <asp:Label ID="lblCalculation_Type" Text='<%# Eval("Calculation_Type").ToString()%>' runat="server" Visible="false" />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EarnDeduction_Name" HeaderText="Name" HeaderStyle-CssClass="ss" />
                                            <asp:TemplateField HeaderText="Jan" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtJan_Amount" runat="server" Text='<%# Eval("Jan_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Feb" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFeb_Amount" runat="server" Text='<%# Eval("Feb_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mar" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMar_Amount" runat="server" Text='<%# Eval("Mar_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Apr" ItemStyle-Width="73" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtApr_Amount" runat="server" Text='<%# Eval("Apr_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="May" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMay_Amount" runat="server" Text='<%# Eval("May_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jun" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtJun_Amount" runat="server" Text='<%# Eval("Jun_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jul" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtJul_Amount" runat="server" Text='<%# Eval("Jul_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aug" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAug_Amount" runat="server" Text='<%# Eval("Aug_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sep" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSep_Amount" runat="server" Text='<%# Eval("Sep_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Oct" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOct_Amount" runat="server" Text='<%# Eval("Oct_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nov" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNov_Amount" runat="server" Text='<%# Eval("Nov_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dec" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDec_Amount" runat="server" Text='<%# Eval("Dec_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="box-header" runat="server" id="HeaderDeduction">
                                    <h3 class="box-title">Deduction Detail</h3>
                                    <div class="box-tools pull-right">
                                        <button type="button" style="padding: 0px 10px;" class="btn btn-box-tool togglebody"><i class="fa fa-minus"></i></button>
                                    </div>
                                </div>
                                <div class="box-body">
                                    <asp:GridView ID="GridView2" runat="server" class="table table-bordered table-striped Grid" AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="ID" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="10">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ID").ToString()%>' runat="server" />
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
                                            <asp:TemplateField HeaderText="Jan" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtJan_Amount" runat="server" Text='<%# Eval("Jan_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtJan_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center> <asp:LinkButton ID="btnJan_Amount" runat="server" CssClass="label label-info" OnClick="btnJan_Amount_Click">View</asp:LinkButton>
                                               </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Feb" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFeb_Amount" runat="server" Text='<%# Eval("Feb_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtFeb_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center><asp:LinkButton ID="btnFeb_Amount" runat="server" CssClass="label label-info" OnClick="btnFeb_Amount_Click">View</asp:LinkButton>
                                                </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mar" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMar_Amount" runat="server" Text='<%# Eval("Mar_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtMar_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center> <asp:LinkButton ID="btnMar_Amount" runat="server" CssClass="label label-info" OnClick="btnMar_Amount_Click">View</asp:LinkButton></center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Apr" ItemStyle-Width="73" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtApr_Amount" runat="server" Text='<%# Eval("Apr_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtApr_Amount" runat="server" class="form-control" MaxLength="9" Enabled="false" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center> <asp:LinkButton ID="btnApr_Amount" runat="server" CssClass="label label-info" OnClick="btnApr_Amount_Click">View</asp:LinkButton>
                                                </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="May" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMay_Amount" runat="server" Text='<%# Eval("May_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtMay_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center> <asp:LinkButton ID="btnMay_Amount" runat="server" CssClass="label label-info" OnClick="btnMay_Amount_Click">View</asp:LinkButton>
                                                </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jun" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtJun_Amount" runat="server" Text='<%# Eval("Jun_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtJun_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center>  <asp:LinkButton ID="btnJun_Amount" runat="server" CssClass="label label-info" OnClick="btnJun_Amount_Click">View</asp:LinkButton>
                                              </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Jul" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtJul_Amount" runat="server" Text='<%# Eval("Jul_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtJul_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center>  <asp:LinkButton ID="btnJul_Amount" runat="server" CssClass="label label-info" OnClick="btnJul_Amount_Click">View</asp:LinkButton>
                                               </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Aug" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAug_Amount" runat="server" Text='<%# Eval("Aug_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtAug_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center><asp:LinkButton ID="btnAug_Amount" runat="server" CssClass="label label-info" OnClick="btnAug_Amount_Click">View</asp:LinkButton>
                                                </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sep" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSep_Amount" runat="server" Text='<%# Eval("Sep_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtSep_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center> <asp:LinkButton ID="btnSep_Amount" runat="server" CssClass="label label-info" OnClick="btnSep_Amount_Click">View</asp:LinkButton>
                                                </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Oct" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOct_Amount" runat="server" Text='<%# Eval("Oct_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtOct_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center>  <asp:LinkButton ID="btnOct_Amount" runat="server" CssClass="label label-info" OnClick="btnOct_Amount_Click">View</asp:LinkButton>
                                                </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nov" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNov_Amount" runat="server" Text='<%# Eval("Nov_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtNov_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center>  <asp:LinkButton ID="btnNov_Amount" runat="server" CssClass="label label-info" OnClick="btnNov_Amount_Click">View</asp:LinkButton>
                                               </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dec" ItemStyle-Width="73">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDec_Amount" runat="server" Text='<%# Eval("Dec_Amount").ToString()%>' class="form-control" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtDec_Amount" runat="server" class="form-control" Enabled="false" MaxLength="9" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox>
                                                    <center>  <asp:LinkButton ID="btnDec_Amount" runat="server" CssClass="label label-info" OnClick="btnDec_Amount_Click">View</asp:LinkButton>
                                               </center>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                </div>
                                <div class="row">
                                    <div class="col-md-8"></div>
                                    <div class="col-md-2">
                                        <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                                    </div>
                                    <div class="col-md-2">
                                        <a class="btn btn-block btn-default" href="PayrollEarnDeductionDetail.aspx">Clear</a>
                                    </div>
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

    </script>
</asp:Content>

