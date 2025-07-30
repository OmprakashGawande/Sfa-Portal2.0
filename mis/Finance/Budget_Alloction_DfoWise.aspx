<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Budget_Alloction_DfoWise.aspx.cs" Inherits="mis_Finance_Budget_Alloction_DfoWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="modal fade" id="myModalT_P" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #d9d9d9;">
                            <button type="button" class="close" data-dismiss="modal">
                                <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                            </button>
                            <h4 class="modal-title" id="myModalLabelT_P">Confirmation</h4>
                        </div>
                        <div class="clearfix"></div>
                        <div class="modal-body">
                            <p>
                                <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupT" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYesT_P" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
                            <asp:Button ID="Button3" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Budget Allocation</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Financial year</label><span style="color: red">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                        ErrorMessage="Select Financial Year" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Financial Year !'></i>"
                                        ControlToValidate="ddlFyear" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlFyear" runat="server" ClientIDMode="Static" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Month</label><span style="color: red">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                        ErrorMessage="Select Month" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                        ControlToValidate="ddlFyear" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlMonth" runat="server" ClientIDMode="Static" CssClass="form-control select2">
                                     <asp:ListItem Value="0">Select</asp:ListItem>
                                     
                                     <asp:ListItem Value="4">April</asp:ListItem>
                                     <asp:ListItem Value="5">May</asp:ListItem>
                                     <asp:ListItem Value="6">June</asp:ListItem>
                                     <asp:ListItem Value="7">July</asp:ListItem>
                                     <asp:ListItem Value="8">August</asp:ListItem>
                                     <asp:ListItem Value="9">September</asp:ListItem>
                                     <asp:ListItem Value="10">October</asp:ListItem>
                                     <asp:ListItem Value="11">November</asp:ListItem>
                                     <asp:ListItem Value="12">December</asp:ListItem>
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                     <asp:ListItem Value="2">February</asp:ListItem>
                                     <asp:ListItem Value="3">March</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Circle Office</label><span style="color: red">*</span>
                                <span class="pull-right">
                                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                        ErrorMessage="Select Circle Office" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Circle Office !'></i>"
                                        ControlToValidate="ddlRegionalOffice" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>--%>
                                </span>
                                <asp:DropDownList ID="ddlRegionalOffice" runat="server" ClientIDMode="Static" CssClass="form-control select2" OnSelectedIndexChanged="ddlRegionalOffice_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                        ErrorMessage="Select Office" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Office !'></i>"
                                        ControlToValidate="ddlOffice" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="a" style="margin-top:25px;" CssClass="btn btn-block btn-success"  OnClick="btnSearch_Click" />
                            </div>
                        </div>
                         <div class="col-md-1">
                                <asp:Button  runat="server" ID="btnClear" class="btn btn-block  btn-default" style="margin-top:25px;" OnClick="btnClear_Click" Text="Clear"></asp:Button>
                            </div>
                    </div>
                    <fieldset>
                        <legend>Details</legend>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView runat="server" CssClass="table table-bordered" ID="GridView1" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S. NO.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNo" runat="server" Text='<%#Container.DataItemIndex +1 %>'></asp:Label>
                                                 <asp:Label ID="lblBudgetAllocation_ID" runat="server" Text='<%# Eval("BudgetAllocation_ID").ToString()  %>' CssClass="hidden"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ledger Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedger_Code" runat="server" Text='<%# Eval("Ledger_Code").ToString()  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ledger Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedger_Name" runat="server" Text='<%# Eval("Ledger_Name").ToString()%>'></asp:Label>
                                                <asp:Label ID="lblLedger_ID" runat="server" Text='<%# Eval("Ledger_ID").ToString()%>' CssClass="hidden"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBudget_Amount" runat="server" CssClass="form-control" Text='<%# Eval("Budget_Amount").ToString()%>' onkeypress="return validateDec(this,event);" autocomplete="off" onblur="return validateAmount(this);"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                        ErrorMessage="Amount cannot be empty"  ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Amount cannot be empty !'></i>"
                                        ControlToValidate="txtBudget_Amount" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                                </asp:GridView>

                            </div>
                        </div>
                     
                    </fieldset>

                       <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-md-2">
                                <asp:Button runat="server" ID="btnSave" ValidationGroup="a" OnClick="btnSave_Click" Text="Save" OnClientClick="return ValidateSave();"  CssClass="btn btn-block btn-success" />
                            </div>
                           
                        </div>


                </div>

            </div>


        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function validateAmount(sender) {

            var pattern = /^[0-9]+(.[0-9]{1,2})?$/;
            var text = sender.value;
            if (text != "") {
                if (text.match(pattern) == null) {
                    alert('Please Enter Decimal Value Only.');
                    sender.value = "0.00";
                   
                }
                else {
                   
                }
            }
        }
        function ValidateSave() {
            debugger
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModalT_P').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupT.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModalT_P').modal('show');
                    return false;
                }
            }
        }
       
    </script>
</asp:Content>

