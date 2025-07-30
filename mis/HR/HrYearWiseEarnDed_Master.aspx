<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HrYearWiseEarnDed_Master.aspx.cs" Inherits="mis_HR_HrYearWiseEarnDed_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box">
                <div class="box-header">
                    <h3 class="box-title">Year Wise Earning/Deduction</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">


                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Year</label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="2021">2021-2022</asp:ListItem>
                                    <asp:ListItem Value="2022">2022-2023</asp:ListItem>
                                    <asp:ListItem Value="2023">2023-2024</asp:ListItem>
                                    <asp:ListItem Value="2024">2024-2025</asp:ListItem>
                                    <asp:ListItem Value="2025">2025-2026</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" CssClass="table table-bordered table-condensed" runat="server" AutoGenerateColumns="false">
                                        <Columns>

                                            <asp:TemplateField>
                                               <HeaderTemplate>
                                                    <input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" runat="server" />
                                                    All
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Eval("ID").ToString()=="0"?false:true %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEarnDeduction_Type" runat="server" Text='<%# Eval("EarnDeduction_Type") %>' />
                                                    <asp:Label ID="lblEarnDeduction_ID" CssClass="hidden" runat="server" Text='<%# Eval("EarnDeduction_ID") %>' />
                                                    <asp:Label ID="lblEarnDedMaster_ID" CssClass="hidden" runat="server" Text='<%# Eval("EarnDedMaster_ID") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEarnDeduction_Name" runat="server" Text='<%# Eval("EarnDeduction_Name") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEarnDeduction_Calculation" runat="server" Text='<%# Eval("EarnDeduction_Calculation") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Button ID="btnSave" Visible="false" Text="Save" runat="server" CssClass="btn btn-success" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=GridView1.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
    </script>
</asp:Content>

