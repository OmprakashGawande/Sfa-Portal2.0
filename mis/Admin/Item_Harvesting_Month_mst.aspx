<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Item_Harvesting_Month_mst.aspx.cs" Inherits="mis_Masters_Item_Harvesting_Month_mst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
     <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">

                        <h5 class="modal-title" id="myModalLabel">Confirmation</h5>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../images/popupimg.jpg" width="30" />&nbsp;&nbsp; 
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnUpdate_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
    <div class="content-wrapper">
        <section class="content">
            <section class="container-fluid">
                <div class="box">
                    <div class="box-body">
                        <asp:Label ID="lblmsg" runat="server"></asp:Label>
                        <div class="card">
                            <div class="card card-header">Set Item Collection or Harvesting Month</div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-4">
                                    <div class="form-group">
                                    <asp:Label runat="server">Item Type (वस्तु का प्रकार)</asp:Label>
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                    <asp:Label runat="server">Item (वस्तु)</asp:Label>
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
                                    
                                   
                                </div>
                                <div class="row" id="dv" runat="server" visible="false">

                                    <div class="col-md-12">
                                        <fieldset>
                                        <legend>
                                            <asp:CheckBox ID="chkAll" runat="server" Text="All" onclick="CheckAllItem();" />
                                        </legend>
                                        <div class="table-responsive">
                                            <asp:CheckBoxList ID="chkAllMonth" style="margin-left:5px" CssClass="table District customCSS" runat="server" RepeatColumns="4" RepeatDirection="Horizontal" ClientIDMode="Static"></asp:CheckBoxList>
                                        </div>
                                            <div class="col-md-3">
                                                <div class="form-group" style="padding-top: 25px">
                                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success" Text="Update" OnClick="btnUpdate_Click" OnClientClick="return ValidatePage();" />
                                                </div>
                                            </div>
                                    </fieldset>
                                    </div>
                                    
                                </div>


                            </div>

                            </div>
                        </div>
                    </div>
              
   
    </section>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
      <style>
        .customCSS td {
            padding: 0px !important;
        }

        table {
            white-space: nowrap;
        }

        .capitalize {
            text-transform: capitalize;
        }

        ul.ui-autocomplete.ui-menu.ui-widget.ui-widget-content.ui-corner-all {
            height: 197px !important;
            overflow-y: scroll !important;
            width: 520px !important;
        }
    </style>
     <script type="text/javascript">
         function ValidatePage() {
             debugger;
             if (typeof (Page_ClientValidate) == 'function') {
                 Page_ClientValidate('Save');
             }

             if (Page_IsValid) {

                 if (document.getElementById('<%=btnUpdate.ClientID%>').value.trim() == "Update") {
                     document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                     $('#myModal').modal('show');
                     return false;
                 }

             }
         }
         function CheckAllItem() {
             if (document.getElementById('<%=chkAll.ClientID%>').checked == true) {
                 $('.District').each(function () {

                     $(this).closest('table').find('input[type=checkbox]').prop('checked', true);
                 });
             }
             else {
                 var chkOfficeAll = document.getElementById('<%=chkAllMonth.ClientID%>');
                 if (chkOfficeAll.checked == true) {
                     chkOfficeAll.checked = false;
                 }
                 $('.District').each(function () {
                     $(this).closest('table').find('input[type=checkbox]').prop('checked', false);
                 });

             }
             return false;
         }
    </script>
</asp:Content>

