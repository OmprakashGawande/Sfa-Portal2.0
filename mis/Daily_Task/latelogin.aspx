<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="latelogin.aspx.cs" Inherits="mis_Daily_Task_latelogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">


    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <%--Confirmation Modal Start --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #D9D9D9;">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSendEmail_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <div class="box box-success">
                    <div class="box-header with-border">
                        <h3 class="box-title" id="Label1">Late Login</h3>
                    </div>
                    <asp:Label runat="server" ID="lblMsg"></asp:Label>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Date<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv_date" runat="server"
                                             ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Date'></i>" 
                                            ControlToValidate="txt_date" ErrorMessage=" Enter Date" SetFocusOnError="true" Display="Dynamic" ValidationGroup="a">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                        <asp:TextBox runat="server" ID="txt_date" Class="form-control DateAdd" OnTextChanged="txt_date_TextChanged" AutoPostBack="true"
                                            data-date-start-date="-7d" data-date-end-date="0d" AutoComplete="off" placeholder="dd/mm/yyyy"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Employee Name<span class="text-danger">*</span></label>
                                   <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="rfv_Emp" runat="server"  ForeColor="Red" 
                                            Text="<i class='fa fa-exclamation-circle' title='Select Employee Name'></i>" ControlToValidate="ddl_Employee"
                                             ErrorMessage="Select Employee Name" ValidationGroup="a" InitialValue="0" SetFocusOnError="true" Display="Dynamic" >
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList ID="ddl_Employee" Width="250" runat="server"   CssClass="form-control select2">
                                        <asp:ListItem Value="0">No record found</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </div>
                             <div class="col-md-2">
                                <div class="form-group">
                                    <label>Time<span class="text-danger">*</span></label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                             runat="server" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Time'></i>" 
                                            ControlToValidate="txtTime" ErrorMessage=" Enter Time"
                                             SetFocusOnError="true" Display="Dynamic" ValidationGroup="a">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                

                                        <asp:TextBox runat="server" ID="txtTime"  AutoComplete="off"
                                             Class="form-control " TextMode="Time" ></asp:TextBox>
                                
                                </div>
                            </div>


                            <div class="col-md-2" style="margin-top: 2.5rem">

                                <asp:Button ID="btnAdd" ValidationGroup="a" CssClass="btn btn-success btn-block" OnClick="btnAdd_Click" runat="server" Text="Add" />

                            </div>
                               <div class="col-md-2" style="margin-top: 2.5rem">
                                   <a href="latelogin.aspx" class="btn btn-block btn-default">Clear</a>
                               </div>

                                                    
                        </div>
                    </div>
                    <div class="box-footer">
                        <fieldset>
                            <legend>Details</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true"
                                            AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmployee_Name" runat="server" Text='<%# Eval("Employee_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Time">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLate_Time" runat="server" Text='<%# Eval("Late_Time") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Action" ShowHeader="False" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn btn-danger" OnClientClick="return confirm('Do you really want to delete?');"
                                                            CausesValidation="False" CommandName="Select"  Text="<i class='fa fa-trash'></i>"
                                                            ></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkdelete" runat="server" CommandName="Delete" ToolTip="Delete" CssClass="btn btn-danger"><i class="fa fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No record found !
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                             <div class="row">
                        <div class="col-md-10"></div>
                        <div class="col-md-2 col-sm-12 mt-3">

                            <asp:Button ID="btnSendEmail" CssClass="btn btn-primary" OnClick="btnSendEmail_Click" runat="server" Text="Send Email" />

                        </div>
                    </div>
                        </fieldset>
                       
                    </div>
                    

                    <div id="DivMail" runat="server"></div>

                </div>
                </div>
        </section>
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <%--<script type="text/javascript">
        function tbx_fnAlphaOnly(e, cntrl) {
            if (!e) e = window.event; if (e.charCode) {
                if (e.charCode < 65 || (e.charCode > 90 && e.charCode < 97) || e.charCode > 122) { if (e.charCode != 95 && e.charCode != 32) { if (e.preventDefault) { e.preventDefault(); } } }
            } else if (e.keyCode) {
                if (e.keyCode < 65 || (e.keyCode > 90 && e.keyCode < 97) || e.keyCode > 122) { if (e.keyCode != 95 && e.keyCode != 32) { try { e.keyCode = 0; } catch (e) { } } }
            }
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
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Add") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>--%>
</asp:Content>

