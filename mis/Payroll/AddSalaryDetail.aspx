<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AddSalaryDetail.aspx.cs" Inherits="mis_Payroll_AddSalaryDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <!-- Default box -->
            <div class="box box-success" style="min-height: 500px;">
                <div class="box-header">
                    <h3 class="box-title">Employee Wise Salary Added</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control select2" AutoPostBack="True" OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Employee<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList >
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Month <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlMonth" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
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
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group"></div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:Label ID="Label1" runat="server" Text="" Visible="true"></asp:Label>
                                <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered pagination-ys " ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="5%"></ItemStyle>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />

                                        <asp:TemplateField HeaderText="Basic Salary" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="300">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnSalaryID" runat="server" Value='<%# Eval("Salary_ID").ToString()%>'/>
                                                <asp:TextBox runat="server" CssClass="form-control time" ID="txtBasicSalary"  ToolTip='<%#Eval("Emp_ID").ToString()%>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Salary Payable Days" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="300">
                                            <ItemTemplate>
                                                <asp:TextBox runat="server" ID="txtPayableDays" CssClass="form-control txtwidth"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Salary Earning Total" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="300">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEarningTotal" runat="server" CssClass="form-control txtwidth"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salary Deduction Total" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="300">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDeductionTotal" runat="server" CssClass="form-control txtwidth"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Salary" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="300">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNetSalary" runat="server" CssClass="form-control txtwidth"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Salary NoDayEarnAmt" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="300">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNoDayEarnAmt" runat="server" CssClass="form-control txtwidth"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Save" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="300">
                                            <ItemTemplate>
                                                <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Save" OnClick="BtnSave_Click" OnClientClick="return confirm('The Department will be deleted. Are you sure want to continue?');"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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

