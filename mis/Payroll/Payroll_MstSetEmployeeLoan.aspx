<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Payroll_MstSetEmployeeLoan.aspx.cs" Inherits="mis_Payroll_Payroll_MstSetEmployeeLoan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">

                        <span class="modal-title" style="float: left" id="myModalLabel">Confirmation</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <%--<img src="../assets/images/question-circle.png" width="30" />--%>&nbsp;&nbsp; 
                           <i class="fa fa-question-circle"></i>
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="Button1" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ScriptManager ID="ScriptManger" runat="server"></asp:ScriptManager>
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Set Loan Master</h3>
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
                                <legend>Set Loan Master</legend>
                                <div class="row">
                                    <div class="col-md-4">
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
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Employee Name<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                    ErrorMessage="Select Employee Name" ForeColor="Red" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Employee Name !'></i>"
                                                    ControlToValidate="ddlEmployee" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Loan Head<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                    ErrorMessage="Select Loan Head" ForeColor="Red" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Loan Head !'></i>"
                                                    ControlToValidate="ddlLoanHead" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlLoanHead" runat="server" class="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Loan Amount<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    ErrorMessage="Enter Loan Amount" ForeColor="Red"
                                                    Text="<i class='fa fa-exclamation-circle' title='Enter Loan Amount !'></i>"
                                                    ControlToValidate="txtLoanAmount" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                    Display="Dynamic" ValidationExpression="(^\d*\.?\d*[1-9]+\d*$)|(^[1-9]+\d*\.\d*$)"
                                                    ValidationGroup="a" runat="server" ControlToValidate="txtLoanAmount"
                                                    ErrorMessage="Invalid Loan Amount"
                                                    Text="<i class='fa fa-exclamation-circle' title='Invalid Loan Amount!'></i>">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtLoanAmount" autocomplete="off" runat="server" CssClass="form-control"
                                                placeholder="Enter Loan Amount..." onkeypress="return validateDectwoplace(this,event)"
                                                MaxLength="12"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Installment Amount<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                                    ErrorMessage="Enter Installment Amount" ForeColor="Red"
                                                    Text="<i class='fa fa-exclamation-circle' title='Enter Installment Amount !'></i>"
                                                    ControlToValidate="txtInstallmentAmount" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                    Display="Dynamic" ValidationExpression="(^\d*\.?\d*[1-9]+\d*$)|(^[1-9]+\d*\.\d*$)"
                                                    ValidationGroup="a" runat="server" ControlToValidate="txtInterestAmount"
                                                    ErrorMessage="Invalid Installment Amount"
                                                    Text="<i class='fa fa-exclamation-circle' title='Invalid Installment Amount!'></i>">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtInstallmentAmount" autocomplete="off" runat="server" CssClass="form-control"
                                                placeholder="Enter Installment Amount..." onkeypress="return validateDectwoplace(this,event)" MaxLength="12"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Installment No.<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                                    ErrorMessage="Enter Installment No." ForeColor="Red"
                                                    Text="<i class='fa fa-exclamation-circle' title='Enter Installment No. !'></i>"
                                                    ControlToValidate="txtInstallmentNo" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                                    Display="Dynamic" ValidationGroup="a" ErrorMessage="Invalid Installment No. !"
                                                    ForeColor="Red"
                                                    Text="<i class='fa fa-exclamation-circle' title='Invalid Installment No. !'></i>"
                                                    ControlToValidate="txtInstallmentNo" ValidationExpression="^[1-9][0-9]*$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtInstallmentNo" autocomplete="off" runat="server" CssClass="form-control"
                                                placeholder="Enter Installment No."
                                                onkeypress="return validateNum(this,event)" MaxLength="3"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Interest Amount</label>
                                            <span class="pull-right">
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator54J"
                                                    Display="Dynamic" ValidationExpression="(^\d*\.?\d*[1-9]+\d*$)|(^[1-9]+\d*\.\d*$)"
                                                    ValidationGroup="a" runat="server" ControlToValidate="txtInterestAmount"
                                                    ErrorMessage="Invalid Interest Amount"
                                                    Text="<i class='fa fa-exclamation-circle' title='Invalid Interest Amount!'></i>">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtInterestAmount" autocomplete="off" runat="server" CssClass="form-control"
                                                placeholder="Enter Interest Amount..." onkeypress="return validateDectwoplace(this,event)"
                                                MaxLength="12"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Loan Deduction Year <span class="text-danger">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                                    ErrorMessage="Select Loan Deduction Year" ForeColor="Red" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Loan Deduction Year !'></i>"
                                                    ControlToValidate="ddlFinancialYear" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Loan Deduction Month <span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ValidationGroup="a"
                                                    ErrorMessage="Select Loan Deduction Month" ForeColor="Red" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Loan Deduction Month !'></i>"
                                                    ControlToValidate="ddlMonth" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlMonth" runat="server" class="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <%-- <div class="col-md-3">
                            <div class="form-group">
                                <label>Loan Status<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlLoanStatus" runat="server" class="form-control">
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Close</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" ValidationGroup="a"
                                                OnClientClick="return ValidatePage();"
                                                Style="margin-top: 23px;" runat="server" Text="Save" />
                                            <asp:HiddenField ID="rowid" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkClear" CssClass="btn btn-block btn-default" OnClick="lnkClear_Click" Style="margin-top: 23px;" runat="server" Text="Clear"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Loan Details</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Office Name<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="b"
                                                    ErrorMessage="Select Office Name" ForeColor="Red" InitialValue="0"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Office Name !'></i>"
                                                    ControlToValidate="ddlSearchOfficeName" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlSearchOfficeName" OnSelectedIndexChanged="ddlSearchOfficeName_SelectedIndexChanged" runat="server" class="form-control select2"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Employee Name<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="b"
                                                    ErrorMessage="Select Employee Name" ForeColor="Red" InitialValue="-1"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Employee Name !'></i>"
                                                    ControlToValidate="ddlSearchEmployeeName" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlSearchEmployeeName" runat="server" class="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-block btn-success" ValidationGroup="b"
                                                Style="margin-top: 23px;" runat="server" Text="Search" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <p style="color:red"> Note : Only interest amount can be update.</p>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="table table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" DataKeyNames="EmployeeLoanId" AutoGenerateColumns="false" EmptyDataText="No Recore Found."
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
                                                            <asp:Label ID="lblOffice_ID" Visible="false" runat="server" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Head">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEarnDeduction_Name" runat="server" Text='<%# Eval("EarnDeduction_Name") %>'></asp:Label>
                                                            <asp:Label ID="lblEarnDeduction_ID" Visible="false" runat="server" Text='<%# Eval("EarnDeduction_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Loan No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLoanNo" runat="server" Text='<%# Eval("LoanNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLoanAmount" runat="server" Text='<%# Eval("LoanAmount") %>'></asp:Label>
                                                            <asp:Label ID="lblIntallmentNo" Visible="false" runat="server" Text='<%# Eval("IntallmentNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Intallment Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIntallmentAmount" runat="server" Text='<%# Eval("IntallmentAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Interest Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInterestAmount" runat="server" Text='<%# Eval("InterestAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Balance Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBalanceAmount" runat="server" Text='<%# Eval("BalanceAmount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Deduction Year">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLoanDeductionFromYear" runat="server" Text='<%# Eval("LoanDeductionFromYear") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Loan Deduction Month">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeductionMonth" runat="server" Text='<%# Eval("DeductionMonth") %>'></asp:Label>
                                                            <asp:Label ID="lblLoanDeductionFromMonth" Visible="false" runat="server" Text='<%# Eval("LoanDeductionFromMonth") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                         
                                                            <asp:LinkButton ID="lnkEdit" CommandArgument='<%# Eval("EmployeeLoanId") %>'
                                                                CommandName="RecordEdit" CssClass="btn btn-primary" runat="server" ToolTip="Edit"><i class="fa fa-pencil"></i>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lnkDelete" CommandArgument='<%# Eval("EmployeeLoanId") %>' CommandName="RecordDelete" CssClass="btn btn-danger" runat="server" ToolTip="Delete"
                                                                OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">

        function ValidatePage() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }
            if (Page_IsValid) {
                if (document.getElementById('<%=btnSave.ClientID %>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID %>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID %>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID %>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }

        }
        function validateDectwoplace(el, evt) {
            var digit = 2;
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (digit == 0 && charCode == 46) {
                return false;
            }

            var number = el.value.split('.');
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            //just one dot (thanks ddlab)
            if (number.length > 1 && charCode == 46) {
                return false;
            }
            //get the carat position
            var caratPos = getSelectionStart(el);
            var dotPos = el.value.indexOf(".");
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > digit - 1)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

