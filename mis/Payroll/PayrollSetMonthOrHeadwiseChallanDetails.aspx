<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollSetMonthOrHeadwiseChallanDetails.aspx.cs" Inherits="mis_Payroll_PayrollSetMonthOrHeadwiseChallanDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
     <style>
        .btnViewDetails {
            padding: 3px 8px;
            margin: 0;
            float: right;
        }
    </style>
    <script>
        function checkAllbox(objRef) {
            var GridView = document.getElementById("<%=GridView1.ClientID %>");
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function Validate(sender, args) {
            var gridView = document.getElementById("<%=GridView1.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
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
      <asp:ScriptManager ID="ScriptManger" runat="server"></asp:ScriptManager>
     <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Set Head wise Challan Details</h3>
                       <asp:LinkButton ID="lnkViewDetails" Text="View Details" PostBackUrl="Rpt_PayrollSetMonthOrHeadwiseChallanDetails.aspx" CssClass="btn btn-info btnViewDetails" runat="server"></asp:LinkButton>
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
                                <legend>Set Head wise Challan Details</legend>
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
                                        <div class="col-md-3">
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
                                    <div class="col-md-3">
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
                                            <label>Deposit Date<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                                    ErrorMessage="Enter Deposit Date" ForeColor="Red"
                                                    Text="<i class='fa fa-exclamation-circle' title='Enter Deposit Date !'></i>"
                                                    ControlToValidate="txtTransactionDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" 
                                                 runat="server" Display="Dynamic" ControlToValidate="txtTransactionDate"
                                                    ErrorMessage="Invalid Deposit Date" Text="<i class='fa fa-exclamation-circle' title='Invalid Date !'></i>" SetFocusOnError="true"
                                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])/(0[13578]|1[02])/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)/(0[13456789]|1[012])/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])/02/((19|[2-9]\d)\d{2}))|(29/02/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" 
                                                ID="txtTransactionDate" MaxLength="10" data-date-start-date="0d" 
                                                data-date-end-date="0d" placeholder="Enter Applicable Date" 
                                                data-provide="datepicker" onpaste="return false ;"
                                                 onkeypress="return false;" data-date-format="dd/mm/yyyy" 
                                                data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>Challan No.<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                                    ErrorMessage="Enter Loan Amount" ForeColor="Red"
                                                    Text="<i class='fa fa-exclamation-circle' title='Enter Loan Amount !'></i>"
                                                    ControlToValidate="txtChallanNo" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                    Display="Dynamic" ValidationExpression="^[0-9]*$"
                                                    ValidationGroup="a" runat="server" ControlToValidate="txtChallanNo"
                                                    ErrorMessage="Invalid Challan No."
                                                    Text="<i class='fa fa-exclamation-circle' title='Invalid Challan No.!'></i>">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtChallanNo" autocomplete="off" runat="server" CssClass="form-control"
                                                placeholder="Enter Challan No." onkeypress="return validateNum(event)"
                                                MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label>BSRCO No.<span style="color: red;">*</span></label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="a"
                                                    ErrorMessage="Enter BSRCO No." ForeColor="Red"
                                                    Text="<i class='fa fa-exclamation-circle' title='Enter BSRCO No. !'></i>"
                                                    ControlToValidate="txtBSRCONo" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                                    Display="Dynamic" ValidationGroup="a" ErrorMessage="Invalid BSRCO No. !"
                                                    ForeColor="Red"
                                                    Text="<i class='fa fa-exclamation-circle' title='Invalid BSRCO No. !'></i>"
                                                    ControlToValidate="txtBSRCONo" ValidationExpression="^[0-9]*$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtBSRCONo" autocomplete="off" runat="server" CssClass="form-control"
                                                placeholder="Enter BSRCO No."
                                                onkeypress="return validateNum(event)" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
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
                                     <div class="col-md-12">
                                        <div class="form-group">
                                            <div class="table table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" DataKeyNames="Emp_ID" AutoGenerateColumns="false" EmptyDataText="No Recore Found."
                                                CssClass="datatable table table-striped table-bordered table-hover">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-Width="30" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick="checkAllbox(this);" />
                                                        <asp:CustomValidator ID="CustomValidator3" runat="server" ValidationGroup="a" ErrorMessage="Please select at least one record."
                                                            ClientValidationFunction="Validate" Display="Dynamic" Text="<i class='fa fa-exclamation-circle' title='Please select at least one record. !'></i>" ForeColor="Red"></asp:CustomValidator>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
            }

        }
    </script>
</asp:Content>

