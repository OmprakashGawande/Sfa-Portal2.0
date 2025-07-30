<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Item_Unit_Mst.aspx.cs" Inherits="Item_Unit_Mst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
     <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
<%--    //-Confirmation Modal Start --%>

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">
                       
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>
                         <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <%--<img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp; --%>
                            <img src="../../images/popupimg.jpg" width="30" /> &nbsp;&nbsp:
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>

    <div class="content-wrapper">
       
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Item Unit Master</h3>
                            <asp:Label ID="lblmsg" runat="server"></asp:Label>
                        </div>
                        <div class="box-body">
                      
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Item Unit</legend>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:Label runat="server">Unit (In English) </asp:Label>
                                             <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                ErrorMessage="Enter Unit(English)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title=' Unit (in English) !'></i>"
                                                ControlToValidate="txtUnitEnglish" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="txtUnitEnglish" CssClass="form-control" placeholder="Enter Item Unit" onkeypress="return lettersOnly();" MaxLength="30" AutoComplete="off" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label runat="server">इकाई (हिंदी में)</asp:Label>
                                             <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                                ErrorMessage="इकाई (हिंदी में)" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='इकाई (हिंदी में) !'></i>"
                                                ControlToValidate="txtUnitHindi" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="txtUnitHindi" placeholder="इकाई का नाम दर्ज करें " CssClass="form-control" onkeypress="return hindiOnly();" MaxLength="30" AutoComplete="off" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4 pt-4"></div>
                                          <div class="col-md-4 pt-4">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:Button runat="server" ID="btnSave" CssClass="btn  btn-block btn-success" ValidationGroup="Save" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage()" />
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button runat="server" ID="btnClear" CssClass="btn btn-block btn-default" Text="Clear" OnClick="btnClear_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                              </div>
                            
                      
                                <fieldset>
                                    <legend> Details  </legend>
                                        
                                   
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="GVItemUnit" runat="server" CssClass="table table-bordered" DataKeyNames="Unit_id" OnRowCommand="GVItemUnit_RowCommand" AutoGenerateColumns="false" EmptyDataText="No Record Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.<br> (सरल क्र.)" HeaderStyle-Width="20%"  HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsno" runat="server" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                            </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText=" Unit <br>(इकाई)"  HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                               <asp:Label ID="lblItemUnit" runat="server" Text='<%#Eval("ItemUnit") %>'></asp:Label>
                                                 <asp:Label ID="lblItemUnitE" runat="server" Text='<%#Eval("UnitName_Eng") %>' Visible="false"></asp:Label>
                                                 <asp:Label ID="lblItemUnitH" runat="server" Text='<%#Eval("UnitName_Hin") %>' Visible="false"></asp:Label>
                                                 <asp:Label ID="lblItemUnitId" runat="server" Text='<%#Eval("Unit_id") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="Active/Deactive <br>(सक्रिय/निष्क्रिय )"  HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkactive" runat="server" Checked='<%#Eval("Unit_IsActive").ToString()=="True"?true:false %>'  OnCheckedChanged="chkactive_CheckedChanged" AutoPostBack="true"/>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Update <br>(सुधार करें)"  HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="linkedit" runat="server" CommandArgument='<%#Eval("Unit_id") %>' CommandName="EditRecord" ToolTip="Edit" CssClass="btn btn-primary" ><i class="fa fa-edit"></i></asp:LinkButton>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
        function ValidatePage() {
            debugger;
            

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
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
        function hindiOnly() {
            var charCode = event.keyCode;
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == (U + 0020))
                return false;
            else
                return true;
        }
        function lettersOnly() {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)

                return true;
            else
                return false;
        }
    </script>
</asp:Content>

