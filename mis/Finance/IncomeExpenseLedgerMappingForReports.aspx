<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="IncomeExpenseLedgerMappingForReports.aspx.cs" Inherits="mis_Finance_IncomeExpenseLedgerMappingForReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <fieldset>
                                <legend>Mapping Filter</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Report Name</label>
                                            <asp:DropDownList ID="ddlReportName" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Income</asp:ListItem>
                                                <asp:ListItem Value="2">Expense</asp:ListItem>
                                            </asp:DropDownList>
                                            <small><span id="valddlReportName" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Head</label>
                                            <asp:DropDownList ID="ddlHeadName" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                            <small><span id="valddlHeadName" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Ledger Name</label>
                                            <asp:DropDownList ID="ddlLedger_ID" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                            <small><span id="valddlLedger_ID" style="color: red;"></span></small>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Save" Style="margin-top: 25px;" OnClick="btnSave_Click" OnClientClick="return ValidateForm();"/>
                                    </div>
                                </div>
                            </fieldset>


                            <fieldset>
                                <legend>Mapped Data Head Wise</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Head</label>
                                            <asp:DropDownList ID="ddlHead_flt" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlHead_flt_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvMappedLedgerData" runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" AutoGenerateColumns="false" OnRowCommand="gvMappedLedgerData_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Report Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReportName" runat="server" Text='<%# Eval("ReportName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Head Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHead_Name" runat="server" Text='<%# Eval("Head_Name") %>'></asp:Label>
                                                            <asp:Label ID="lblHead_ID" runat="server" CssClass="hidden" Text='<%# Eval("Head_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ledger_Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLedger_Name" runat="server" Text='<%# Eval("Ledger_Name") %>'></asp:Label>
                                                            <asp:Label ID="lblLedger_ID" runat="server" CssClass="hidden" Text='<%# Eval("Ledger_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditRecord" CommandArgument='<%# Eval("Mapping_ID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" OnClientClick="return confirm('Do you really want to delete Record?')" CommandName="DeleteRecord" CommandArgument='<%# Eval("Mapping_ID") %>'><i class="fa fa-trash"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
        function ValidateForm()
        {
            $("#valddlReportName").html("");
            $("#valddlHeadName").html("");
            $("#valddlLedger_ID").html("");
            var msg = "";
            if (document.getElementById('<%=ddlReportName.ClientID%>').selectedIndex == 0) {
                msg += "Select Report Name  \n";
                $("#valddlReportName").html("Select Report Name");
            }
            if (document.getElementById('<%=ddlHeadName.ClientID%>').selectedIndex == 0) {
                msg += "Select Head Name  \n";
                $("#valddlHeadName").html("Select Head Name");
            }
            if (document.getElementById('<%=ddlLedger_ID.ClientID%>').selectedIndex == 0) {
                msg += "Select Ledger Name  \n";
                $("#valddlLedger_ID").html("Select Ledger Name");
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

