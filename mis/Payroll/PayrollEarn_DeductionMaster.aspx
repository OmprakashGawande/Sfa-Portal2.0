<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollEarn_DeductionMaster.aspx.cs" Inherits="mis_Payroll_PayrollEarn_DeductionMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Set Applicable Earning & Deduction</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Year<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlEarnDeduction_Year" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Type<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlEarnDeduction_Type" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEarnDeduction_Year_SelectedIndexChanged">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem Value="Earning">Earning</asp:ListItem>
                                            <asp:ListItem Value="Deduction">Deduction</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group"></div>
                            <div class="form-group" id="DivDetail" runat="server">
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped" ClientIDMode="Static" AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="EarnDeduction_ID">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Eval("Status").ToString() == "0" ? false : true %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="4" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SNo.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("EarnDeduction_ID").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="5" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblHeader" runat="server" Text='Earning'></asp:Label>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEarnDeduction_Name" runat="server" Text='<%# Eval("EarnDeduction_Name").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Calculation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEarnDeduction_Calculation" runat="server" Text='<%# Eval("EarnDeduction_Calculation").ToString()%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="15" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <div class="form-group"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4"></div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" OnClientClick="return validateform()" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <a href="PayrollEarn_DeductionMaster.aspx" class="btn btn-block btn-default">Clear</a>
                                        </div>
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
    </script>
</asp:Content>

