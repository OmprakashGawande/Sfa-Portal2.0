<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Item_Set_MinSaleQty.aspx.cs" Inherits="mis_Admin_Set_Item_MinSaleQty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
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
                        <h3 class="box-title">Set Sale Minimum Quantity</h3>

                    </div>
                    <asp:Label runat="server" ID="lblMsg"></asp:Label>
                    <div class="box-body">

                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label runat="server">Item Type / <br />वस्तु का प्रकार</asp:Label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Select Item Type" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Item Type !'></i>"
                                            ControlToValidate="ddlItemType" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList runat="server" ID="ddlItemType" OnSelectedIndexChanged="ddlItemType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label runat="server">Item /<br />वस्तु</asp:Label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="a"
                                            ErrorMessage="Select Item " InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Item  !'></i>"
                                            ControlToValidate="ddlItem" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList runat="server" ID="ddlItem" CssClass="form-control select2" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:Label runat="server">Minimum Quantity  /<br />न्यूनतम मात्रा
                                    <asp:Label runat="server" ID="lblUnitRate"></asp:Label></asp:Label>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                        ErrorMessage="Please Enter Sale Rate" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Please Enter Sale Rate  !'></i>"
                                        ControlToValidate="txtMinQty" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <div class="form-group">
                                   
                                    <asp:TextBox runat="server" ID="txtMinQty" onkeypress="return isNumberKey(this, event);" oninput="validate(this)" onpaste="return false;" AutoComplete="off" MaxLength="8" CssClass="form-control"></asp:TextBox>

                                 <asp:CompareValidator ID="CompareValidator1" runat="server" ValueToCompare="0"
ControlToValidate="txtMinQty" Display="Dynamic" ErrorMessage="Enter valid value" ForeColor="Red" ValidationGroup="a"
Operator="GreaterThanEqual" Type="Double"></asp:CompareValidator>  
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label runat="server">Applicable Date   /<br />लागु दिनांक</asp:Label>
                                    <span class="pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ValidationGroup="a"
                                            ErrorMessage="Please Enter Applicable Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Please Enter Applicable Date  !'></i>"
                                            ControlToValidate="txtDate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:TextBox runat="server" ID="txtDate" data-date-start-date="0d" data-provide="datepicker" placeholder="DD/MM/YYYY" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" CssClass="form-control disableFuturedate"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-4"></div>
                            <div class="col-md-4 pt-4">
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Button runat="server" ID="btnSave" ValidationGroup="a" OnClick="btnSave_Click" CssClass="btn btn-success btn-block" Text="Save"/>
                                    </div>
                                    <div class="col-md-6">
                                        <%--<asp:Button runat="server" ID="btnClear" CssClass="btn btn-outline-danger btn-block" Text="Clear" />--%>
                                        <a href="Item_Set_MinSaleQty.aspx" class="btn btn-default btn-block">Clear</a>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                    <div class="box-footer">
                        <fieldset>
                            <legend>Details</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gridview" DataKeyNames="MinID" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="gridview_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No. /<br/> सरल क्र.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="View Details  <br/>/ विवरण देखें" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkViewDetails" runat="server" CommandName="ViewDetails" ToolTip="View" CommandArgument='<%# Eval("Item_Id") %>' CssClass="btn btn-sm btn-primary"><i class="fa fa-eye"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="10" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Update</br>/ सुधार करें" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" CommandName="btnUpdate" CommandArgument='<%#Eval("MinID").ToString() %>' ToolTip="Edit" OnClientClick="return confirm('Are You Really Want To Update')" CssClass="mr-2  btn btn-sm btn-primary"><i class="fa fa-edit"></i></asp:LinkButton>

                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="10" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Type /<br/> वस्तु का प्रकार">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%#Eval("ItemTypeName").ToString() %>'></asp:Label>
                                                        <asp:Label runat="server" ID="ItemTypeId" Visible="false" Text='<%#Eval("ItemType_Id").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item / <br/>वस्तु">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%#Eval("ItemName").ToString() %>'></asp:Label>
                                                        <asp:Label runat="server" Visible="false" ID="ItemId" Text='<%#Eval("Item_Id").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Minimum Quantity /<br/> न्यूनतम मात्रा">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="ItemMinSaleQty" Visible="false" onkeypress="return isNumber(event);" AutoComplete="off" MaxLength="8" Text='<%#Eval("Item_MinSaleQty").ToString() %>'></asp:Label>
                                                        <asp:Label runat="server" ID="Label1" onkeypress="return isNumber(event);" AutoComplete="off" MaxLength="8" Text='<%#Eval("Item_MinSaleQty").ToString()+" " + Eval("UnitName_Eng").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Applicable Date /<br/>लागु दिनांक ">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="EffectiveDat" Text='<%#Eval("EffectiveDate").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <%--  <asp:TemplateField HeaderText="Active /Deactive </br>(सक्रिय /निष्क्रिय )" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                          <asp:CheckBox runat="server" ID="cbxactive" CssClass="black" ToolTip='<%# Eval("IsActive").ToString() == "True" ? "Deactive":"Active" %>' Checked='<%# Eval("IsActive").ToString() == "True" ? true:false %>' OnCheckedChanged="cbxactive_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                                       
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="10" />
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="ViewDetails" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title" style="color: blue">Sale Minimum Quantity Record :-</h4>
                        </div>
                       <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView runat="server" ID="gridviewHistory" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowDataBound="gridviewHistory_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.<br/> (सरल क्र.)" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                            <asp:Image runat="server" ID="ImgNew" Style="width: 40px; margin-left: 5px;" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item <br/>(वस्तु)" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%#Eval("ItemName").ToString() %>'></asp:Label>
                                                            <asp:Label runat="server" Visible="false" ID="ItemId" Text='<%#Eval("Item_Id").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Purchase Rate<br/> (खरीद दर)" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Visible="false" ID="PurchaseRat" Text='<%#Eval("Item_MinSaleQty").ToString() %>'></asp:Label>
                                                            <asp:Label runat="server" ID="Label1" Text='<%#Eval("Item_MinSaleQty").ToString()+"/- per " + Eval("UnitName_Eng").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Applicable Date <br/>(लागु दिनांक) " HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="EffectiveDat" Text='<%#Eval("EffectiveDate").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <!-- /.modal-content -->
                        </div>
                    </div>
                    <!-- /.modal-dialog -->
                </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
      <script type="text/javascript">
          function ValidatePage() {
              debugger;
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
    <script src="../js/daterangepicker.js"></script>
    <script src="../js/bootstrap-datepicker.js"></script>
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 32 && (charCode < 46 || charCode == 47 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function lettersOnly() {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)

                return true;
            else
                return false;
        }
        function CapitalLettersOnly() {
            var charCode = event.keyCode;

            if (charCode > 32 && (charCode < 48 || charCode > 57) || (charCode > 64 && charCode < 91) || charCode == 8 || charCode == 32)

                return true;
            else
                return false;
        }
        var validate = function (e) {
            var t = e.value;
            e.value = (t.indexOf(".") >= 0) ? (t.substr(0, t.indexOf(".")) + t.substr(t.indexOf("."), 3)) : t;
        }
        function isNumberKey(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                //Check if the text already contains the . character
                if (txt.value.indexOf('.') === -1) {
                    return true;
                } else {
                    return false;
                }
            } else {
                if (charCode > 31 &&
                  (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
        }
        function ViewDetails() {
            $("#ViewDetails").modal('show');

        }
    </script>
</asp:Content>

