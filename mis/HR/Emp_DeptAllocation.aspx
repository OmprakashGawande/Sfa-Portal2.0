<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Emp_DeptAllocation.aspx.cs" Inherits="mis_HR_Emp_DeptAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
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
                            <img src="../image/question-circle.png" width="30" />&nbsp;&nbsp; 
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSubmit_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
    <%--ConfirmationModal End --%>
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Employee Department Mapping</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Employee Name</label>
                                <span style="color: red">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="rfv1" runat="server" InitialValue="0" ValidationGroup="a" Display="Dynamic" ControlToValidate="ddlEmployye_Name" ErrorMessage="Select Employee Name" Text="<i class='fa fa-exclamation-circle' title='Select Employee Name !'></i>"></asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlEmployye_Name" runat="server" OnInit="ddlEmployye_Name_Init" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                 <span class="pull-right">
                                    
                                </span>
                                <label>Department</label><span style="color: red">* &nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="" ValidationGroup="a" Display="Dynamic" ControlToValidate="ddlDepartment" ErrorMessage="Select Department" Text="<i class='fa fa-exclamation-circle' title='Select Department!'></i>"></asp:RequiredFieldValidator></span>
                               <br />
                               
                                <asp:ListBox ID="ddlDepartment" Width="50px" CssClass="form-control" runat="server" SelectionMode="Multiple" OnInit="ddlDepartment_Init">
                                </asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-2" style="margin-top: 25px;">
                            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="a" Width="100%" Text="Save" CssClass="btn btn-success" OnClientClick="return ValidatePage();" />
                        </div>

                        <div class="col-md-2" style="margin-top: 25px;">
                            <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Width="100%" ValidationGroup="clear" Text="Clear" CssClass="btn btn-default" />
                        </div>
                    </div>
                    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
                    <hr />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" EmptyDataText="No Record Found" OnRowCommand="GridView1_RowCommand" DataKeyNames="EmpDeptAllocation_id" CssClass="table table-striped table-bordered table-hover" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex + 1  %>"></asp:Label>
                                                <asp:Label ID="lblDepartment" Visible="false" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                                <asp:Label ID="lblEmp_ID" Visible="false" runat="server" Text='<%# Eval("Emp_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Empname" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Dept" HeaderText="Department" />
                                        <asp:TemplateField HeaderText="Action <br />">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("EmpDeptAllocation_id") %>' runat="server" ToolTip="Update"><i class="fa fa-pencil"></i></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("EmpDeptAllocation_id") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: red;" OnClientClick="return confirm('Are you sure to Delete?')"><i class="fa fa-trash"></i></asp:LinkButton>
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
    <link href="../css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="../js/bootstrap-multiselect.js" type="text/javascript"></script>
   <script type="text/javascript">

        $(function () {
            $('[id*=ddlDepartment]').multiselect({
                includeSelectAllOption: true
            });
        });

        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSubmit.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>

