<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Payroll_Trn_EmployeeOvertimeEntry.aspx.cs" Inherits="mis_Payroll_Payroll_Trn_EmployeeOvertimeEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>

                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" ValidationGroup="a" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <div class="box box-success" style="min-height:200px;">
                   

                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Overtime Pay Entry</h3>

                        </div>
                        <div class="box-body">

                            <fieldset>
                                <legend>Overtime Pay Entry</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblMsg"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">


                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label runat="server">Office Name<span style="color: red;">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlOfficeName" InitialValue="0" ErrorMessage="Select Office Name"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Office Name'></i>"
                                                    SetFocusOnError="true" ForeColor="Red" ValidationGroup="a">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlOfficeName" AutoPostBack="true" OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged" runat="server" class="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label runat="server">Employee<span style="color: red;">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlEmployee" InitialValue="0" ErrorMessage="Select Employee"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Employee'></i>"
                                                    SetFocusOnError="true" ForeColor="Red" ValidationGroup="a">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label runat="server">Date <span style="color: red;">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvEffectiveDate" ValidationGroup="a"
                                                    ErrorMessage="Select Date" ForeColor="Red" 
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Date'></i>"
                                                    ControlToValidate="txtWorkingDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control"
                                                ID="txtWorkingDate" MaxLength="8" placeholder="Select Date"
                                                data-provide="datepicker" onpaste="return false ;" data-date-end-date="-0d"
                                                onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-start-date="-5d"
                                                data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label runat="server">Working Hour<%--(कार्यकारी घंटे)--%> <span style="color: red;">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvrebate1" ValidationGroup="a"
                                                    ErrorMessage="Enter Overtime Pay Rate" ForeColor="Red"
                                                    Text="<i class='fa fa-exclamation-circle' title='Enter Working Hour'></i>"
                                                    ControlToValidate="txtWorkingHour" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5"
                                                    runat="server" Display="Dynamic" ValidationGroup="a"
                                                    ErrorMessage="Enter Valid Overtime Pay Rate !" ForeColor="Red"
                                                    Text="<i class='fa fa-exclamation-circle' title='Enter Valid Working Hour. !'></i>"
                                                    ControlToValidate="txtWorkingHour" ValidationExpression="^[1-9][0-9]*$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <asp:TextBox ID="txtWorkingHour" autocomplete="off" runat="server" onkeypress="return validateNum(event);" MaxLength="2" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-2" style="margin-top: 19px;">
                                        <div class="form-group">

                                            <asp:Button runat="server" ID="btnSave" ValidationGroup="a" CssClass="btn btn-success btn-block" Text="Save" OnClientClick="return ValidatePage() " />
                                            <asp:HiddenField ID="hfrowid" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-md-2" style="margin-top: 19px;">
                                        <div class="form-group">

                                            <asp:LinkButton ID="lnkbtnClear" OnClick="lnkbtnClear_Click" runat="server" CssClass="btn btn-default btn-block" Text="Clear"></asp:LinkButton>
                                        </div>
                                    </div>

                                </div>

                            </fieldset>

                        </div>
                        
                 

                </div>

                <div class="box box-success">
                    <div class="box-header with-border">
                            <h3 class="box-title">Employee Overtime Pay Entry Details</h3>

                        </div>
                    <div class="box-header with-border">
                        <div class="box-footer">
                            <fieldset runat="server" id="gridfieldset">
                                <legend>Employee Overtime Pay Entry Details</legend>
                                <div class="row">
                                      <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label runat="server">Office Name<span style="color: red;">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                                    ControlToValidate="ddlSearchOfficeName" InitialValue="0" ErrorMessage="Select Office Name"
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Office Name'></i>"
                                                    SetFocusOnError="true" ForeColor="Red" ValidationGroup="b">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlSearchOfficeName" OnSelectedIndexChanged="ddlSearchOfficeName_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label runat="server">Employee</asp:Label>
                                            <asp:DropDownList ID="ddlSearchEmployeeName" runat="server" class="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label runat="server">Date <span style="color: red;">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="b"
                                                    ErrorMessage="Select Date" ForeColor="Red" 
                                                    Text="<i class='fa fa-exclamation-circle' title='Select Date'></i>"
                                                    ControlToValidate="txtSearchWorkingDate" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>

                                            </span>
                                            <asp:TextBox runat="server" autocomplete="off" CssClass="form-control"
                                                ID="txtSearchWorkingDate" MaxLength="8" placeholder="Select Date"
                                                data-provide="datepicker" onpaste="return false ;" data-date-end-date="-0d"
                                                onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-start-date="-5d"
                                                data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                         </div>
                                         <div class="col-md-2" style="margin-top: 19px;">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="btnSearch" OnClick="btnSearch_Click" ValidationGroup="b" CssClass="btn btn-success btn-block" Text="Search"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="">
                                            <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                EmptyDataText="No Record Found." DataKeyNames="EmpOverTimePaychildId">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                                                               <asp:Label ID="lblEmp_ID" Visible="false" runat="server" Text='<%# Eval("Emp_ID") %>'></asp:Label>
                                                               <asp:Label ID="lblOffice_ID" Visible="false" runat="server" Text='<%# Eval("Office_ID") %>'></asp:Label>
                                                              <asp:Label ID="lblEmpOverTimePayId" Visible="false" runat="server" Text='<%# Eval("EmpOverTimePayId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWorkingDate" runat="server" Text='<%# Eval("WorkingDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Working Hour">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWorkingHour" runat="server" Text='<%# Eval("WorkingHour") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  
                                                    <asp:TemplateField HeaderText="Delete" Visible="true">
                                                        <ItemTemplate>
                                                          <asp:LinkButton ID="lnkUpdate" CommandName="RecordDelete" OnClientClick="return confirm('Are you sure to Delete?')" CommandArgument='<%#Eval("EmpOverTimePaychildId") %>' runat="server" ToolTip="Update" Style="color: black;" CssClass="fa fa-trash"></asp:LinkButton>
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
    <script type="text/javascript">
        function ViewOverTimePayRateDetails() {
            $("#ViewOverTimePayRate").modal('show');

        }
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>
