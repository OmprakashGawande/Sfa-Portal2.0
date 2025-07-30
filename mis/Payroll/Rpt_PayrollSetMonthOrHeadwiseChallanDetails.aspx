<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_PayrollSetMonthOrHeadwiseChallanDetails.aspx.cs" Inherits="mis_Payroll_Rpt_PayrollSetMonthOrHeadwiseChallanDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
        <link href="../css/StyleSheet.css" rel="stylesheet" />
     <style>
        .btnViewDetails {
            padding: 3px 8px;
            margin: 0;
            float: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
   
      <asp:ScriptManager ID="ScriptManger" runat="server"></asp:ScriptManager>
     <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
     <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Head wise Challan Details</h3>
                       <asp:LinkButton ID="lnkBack" Text="Back" PostBackUrl="PayrollSetMonthOrHeadwiseChallanDetails.aspx" CssClass="btn btn-default btnViewDetails" runat="server"></asp:LinkButton>
                </div>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        
                        <script type="text/javascript">
                            $(function () {
                                initdropdown();
                            })
                            function initdropdown() {
                                $('.select2').select2();
                            }

                        </script>
                        <script>
                            Sys.Application.add_load(initdropdown);
                        </script>
                        
                        <div class="box-body">
                            <div class="row">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </div>
                            <fieldset>
                                <legend>Head wise Challan Details</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Office Name<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                    ErrorMessage="Select Office Name" ForeColor="Red" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Office Name !'></i>"
                                                    ControlToValidate="ddlOfficeName" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control select2"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                            <div class="form-group">
                                <label>Employee</label>
                                 <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Select Employee" ForeColor="Red" InitialValue="-1"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Employee !'></i>"
                                                    ControlToValidate="ddlEmployee" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Head<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    ErrorMessage="Select Head" ForeColor="Red" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Head !'></i>"
                                                    ControlToValidate="ddlHead" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlHead" runat="server" class="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                        <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Year <span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                                    ErrorMessage="Select Year" ForeColor="Red" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Year !'></i>"
                                                    ControlToValidate="ddlFinancialYear" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Month <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                    ErrorMessage="Select Month" ForeColor="Red" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                                    ControlToValidate="ddlMonth" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlMonth" runat="server" class="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div> 
                                      <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" 
                                    Style="margin-top: 23px;" ValidationGroup="a" runat="server" Text="Search" OnClick="btnSearch_Click" />
                            </div>
                        </div>                                  
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkClear" CssClass="btn btn-block btn-default" OnClick="lnkClear_Click" Style="margin-top: 23px;" runat="server" Text="Clear"></asp:LinkButton>
                                        </div>
                                    </div>
                                     <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="table table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" DataKeyNames="Emp_ID" AutoGenerateColumns="false" EmptyDataText="No Recore Found."
                                                CssClass="datatable table table-striped table-bordered table-hover">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmp_Name" runat="server" Text='<%# Eval("Emp_Name") %>'></asp:Label>
                                                            <asp:Label ID="lblEmp_ID" Visible="false" runat="server" Text='<%# Eval("Emp_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Head Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEarnDeduction_Name" runat="server" Text='<%# Eval("EarnDeduction_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Year">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSYear" runat="server" Text='<%# Eval("SYear") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Month">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMName" runat="server" Text='<%# Eval("MName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deposit Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTransactionDate" runat="server" Text='<%# Eval("TransactionDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Challan No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblChallanNo" runat="server" Text='<%# Eval("ChallanNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BSRCO No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBSRCONo" runat="server" Text='<%# Eval("BSRCONo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                            </div>
                                         </div>
                                   
                                </div>
                           
                            </fieldset>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    
</asp:Content>
