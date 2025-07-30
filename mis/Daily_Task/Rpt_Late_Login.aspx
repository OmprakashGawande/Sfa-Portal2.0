<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_Late_Login.aspx.cs" Inherits="mis_Daily_Task_Rpt_Late_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
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
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes"  Style="margin-top: 20px; width: 50px;" />
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
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Date<span class="danger">*</span></label>
                                    <span>
                                        <asp:RequiredFieldValidator ID="rfv_date" runat="server" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Required'></i>" ControlToValidate="txt_date" ErrorMessage=" Requred Date" SetFocusOnError="true" Display="Dynamic" ValidationGroup="a">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                 

                                        <asp:TextBox runat="server" ID="txt_date" Class="form-control DateAdd" data-date-start-date="-7d" data-date-end-date="0d" AutoComplete="off" placeholder="dd/mm/yyyy"></asp:TextBox>
                                  
                                </div>
                            </div>
                         
                            
                              <div class="col-md-2" style="margin-top: 2.5rem">

                                <asp:Button ID="btnSearch" ValidationGroup="a" CssClass="btn btn-success btn-block" OnClick="btnSearch_Click" runat="server" Text="Search" />

                            </div>
                             <div class="col-md-2" style="margin-top: 2.5rem">
                                 <a href="Rpt_Late_Login.aspx" class="btn btn-default btn-block">Clear</a>
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
                                            AutoGenerateColumns="False" DataKeyNames="Emp_Late_ID" OnRowCommand="GridView1_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Late_Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Employee Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmployee_Name" runat="server" Text='<%# Eval("Emp_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="Time">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLate_Time" runat="server" Text='<%# Eval("Late_Time") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbDelete" runat="server" CssClass="btn btn-danger" OnClientClick="return confirm('Do you really want to delete?');"
                                                            CausesValidation="False" CommandName="Select" CommandArgument='<%#Eval("Emp_Late_ID")%>' Text="<i class='fa fa-trash'></i>"
                                                            ></asp:LinkButton>
                                                    </ItemTemplate>
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
                        
                    </div>
                        </fieldset>
                       
                    </div>
                    



                </div>
                </div>
        </section>
    </div>



</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentFooter" runat="Server">
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

