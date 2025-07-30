<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PackagingType.aspx.cs" Inherits="mis_Masters_PackagingType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
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
                           
                            <img src="../../images/popupimg.jpg" width="30" /> &nbsp;&nbsp:
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="txtsave_Click" Style="margin-top: 20px; width: 50px;" />
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
                            <h3 class="box-title" id="Label1">Packaging Type</h3>
                            <asp:Label ID="lblmsg" runat="server"></asp:Label>
                        </div>
                        <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                       
                                        
                                 <fieldset>
                                     <legend>Type of Packaging</legend>
                                     <div class="row">
                                            <div class="col-md-4">
                                                
                                                <div class="form-group">
                                                    <asp:Label runat="server">Type ( प्रकार )</asp:Label>
                                                    <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                ErrorMessage=" Enter Packaging Type" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title=' Required !'></i>"
                                                ControlToValidate="txtpackagetype" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                                    <asp:TextBox ID="txtpackagetype" runat="server" CssClass="form-control" MaxLength="30" AutoComplete="off" onkeypress="return lettersOnly();" Placeholder="Enter Type of Packaging" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2" style="padding-top:20px">
                                                <div class="form-group">
                                          <asp:Button ID="btnsave" runat="server" CssClass="btn  btn-block btn-success" Text="Save"  ValidationGroup="Save" OnClick="txtsave_Click" OnClientClick= "return ValidatePage()"/>
                                                 
                                                </div>
                                            
                                                    
                                            </div>
                                            <div class="col-md-2" style="padding-top:20px">
                                                <div class="form-group">
                                                    <asp:Button ID="btnclear" runat="server" CssClass="btn btn-block btn-default" Text="Clear" OnClick="btnclear_Click"/>
                                                </div>
                                           
                                                </div>
                                            </div>
                                 </fieldset>
                                    </div>
                                       </div>
                               
                                        <fieldset>
                                            <legend>Details</legend>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                    <asp:GridView ID="GVPackagingtype" runat="server" CssClass="table table-bordered" DataKeyNames="Packaging_Id" AutoGenerateColumns="false" OnRowCommand="GVPackagingtype_RowCommand" EmptyDataText="No Record Found">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No.<br>( सरल क्र .)" HeaderStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Packaging Type<br>( पैकेजिंग का प्रकार ) " HeaderStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblpackagingtype" runat="server" Text='<%#Eval("Packaging_Type") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Active/Deactive <br>( सक्रिय/निष्क्रिय )" HeaderStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkactive" runat="server" Checked='<%#Eval("PackagingType_IsActive").ToString()=="True"?true:false %>' OnCheckedChanged="chkactive_CheckedChanged" AutoPostBack="true" />
                                                                </ItemTemplate> 
                                                                <ItemStyle HorizontalAlign="Center" />

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Update <br>( सुधार करें )"  HeaderStyle-CssClass="text-center">
                                                                <ItemTemplate >
                                                                    <asp:LinkButton ID="linkupdate" runat="server" CommandArgument='<%#Eval("Packaging_Id") %>' CommandName="EditRecord" ToolTip="Edit" CssClass="btn btn-primary"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />

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
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
         <script type="text/javascript">
             function ValidatePage() {
                 debugger;


                 if (typeof (Page_ClientValidate) == 'function') {
                     Page_ClientValidate('Save');
                 }

                 if (Page_IsValid) {


                     if (document.getElementById('<%=btnsave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnsave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                     $('#myModal').modal('show');
                     return false;
                 }
             }
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

