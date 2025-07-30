<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Village_Master.aspx.cs" Inherits="Village_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .GridPager a,
        .GridPager span {
            display: inline-block;
            padding: 0px 9px;
            margin-right: 4px;
            border-radius: 3px;
            border: solid 1px #ffffff;
            background: #e9e9e9;
            box-shadow: inset 0px 1px 0px rgba(255,255,255, .8), 0px 1px 3px rgba(0,0,0, .1);
            font-size: .875em;
            font-weight: bold;
            text-decoration: none;
            color: #717171;
            text-shadow: 0px 1px 0px rgba(255,255,255, 1);
        }

        .GridPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
        }

        .GridPager span {
            background: #616161;
            box-shadow: inset 0px 0px 8px rgba(0,0,0, .5), 0px 1px 0px rgba(255,255,255, .8);
            color: #f0f0f0;
            text-shadow: 0px 0px 3px rgba(0,0,0, .5);
            border: 1px solid #3AC0F2;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
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
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Village Master</h3>
                            <asp:Label runat="server" ID="LblMsg" Text=""></asp:Label>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Village Master</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label runat="server">Division (संभाग)</asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                    ErrorMessage="Select Division Name" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Division Name !'></i>"
                                                    ControlToValidate="ddlDivision" ForeColor="Red" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label runat="server">District (जिला)</asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                                    ErrorMessage="Select District Name" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select District Name !'></i>"
                                                    ControlToValidate="ddlDistrict" ForeColor="Red" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label runat="server">Range (सीमा)</asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Save"
                                                    ErrorMessage="Select Range Name" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Range Name !'></i>"
                                                    ControlToValidate="ddlRange" ForeColor="Red" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlRange" runat="server" OnSelectedIndexChanged="ddlRange_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control select2">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label runat="server">Block (विकासखण्ड)</asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save"
                                                    ErrorMessage="Select Block Name" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Block Name !'></i>"
                                                    ControlToValidate="ddlBlock" ForeColor="Red" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label runat="server">Village Name (In English)</asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="Save"
                                                    ErrorMessage="Enter Village Name in English" Text="<i class='fa fa-exclamation-circle' title='Enter Village Name in English !'></i>"
                                                    ControlToValidate="txtVillageEnglish" ForeColor="Red" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox runat="server" ID="txtVillageEnglish" onkeypress="return lettersOnly();" AutoComplete="off" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label runat="server">गाँव का नाम (हिंदी में)</asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="Save"
                                                    ErrorMessage="Enter Village Name in Hindi" Text="<i class='fa fa-exclamation-circle' title='Enter Village Name in Hindi !'></i>"
                                                    ControlToValidate="txtVillageHindi" ForeColor="Red" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox runat="server" ID="txtVillageHindi" onkeypress="return hindiOnly();" AutoComplete="off" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-4"></div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <asp:Button runat="server" ID="btnSave" CssClass="btn btn-block btn-success" OnClientClick="return ValidatePage();" ValidationGroup="Save" Text="Save" OnClick="btnSave_Click" />
                                        </div>
                                        <div class="col-md-2">
                                            <%-- <asp:Button runat="server" ID="btnClear" CssClass="btn btn-outline-danger btn-block" Text="Clear" OnClick="btnClear_Click" />--%>
                                            <a href="Village_Master.aspx" class="btn btn-block btn-default">Clear</a>
                                        </div>
                                    </div>
                                    <div class="col-md-2"></div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Details</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grvVillageMaster" runat="server" DataKeyNames="Village_ID" AutoGenerateColumns="false" CssClass="table table-bordered" OnRowCommand="grvVillageMaster_RowCommand" Style="text-align: center;" AllowPaging="true" AllowCustomPaging="true" PageSize="100"
                                                OnPageIndexChanging="grvVillageMaster_PageIndexChanging">
                                                <PagerStyle HorizontalAlign = "Left" CssClass = "GridPager" />
                                                <Columns>

                                                    <asp:TemplateField HeaderText="S.No. (सरल क्र.)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%#(grvVillageMaster.PageIndex * grvVillageMaster.PageSize) + Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Division (संभाग)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDivisionName" Text='<%#Eval("Division_Name")%>'></asp:Label>
                                                            <asp:Label runat="server" ID="lblDivisionId" Text='<%#Eval("Division_ID")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="District (जिला)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDistrictName" Text='<%#Eval("District_Name")%>'></asp:Label>
                                                            <asp:Label runat="server" ID="lblDistrictId" Text='<%#Eval("District_ID")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Range (सीमा)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblRangeName" Text='<%#Eval("Range_Name")%>'></asp:Label>
                                                            <asp:Label runat="server" ID="lblRangeId" Text='<%#Eval("Range_ID")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Block (विकासखण्ड)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblBlockName" Text='<%#Eval("Block_Name")%>'></asp:Label>
                                                            <asp:Label runat="server" ID="lblBlockId" Text='<%#Eval("Block_ID")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Village Name (In English)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblVillageNameE" Text='<%#Eval("Village_Name_Eng")%>'></asp:Label>
                                                            <asp:Label runat="server" ID="lblVillageId" Text='<%#Eval("Village_ID") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="गाँव का नाम (हिंदी में)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblVillageNameH" Text='<%#Eval("Village_Name_Hin")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IsActive">
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" ID="chkIsActiveVillage" Checked='<%#Eval("Village_IsActive").ToString() == "True" ? true : false %>' OnCheckedChanged="chkIsActiveVillage_CheckedChanged" AutoPostBack="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" CommandName="btnSelect" CommandArgument='<%#Eval("Village_ID") %>' CssClass="btn btn-primary"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" />
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
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Modify") {
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
        function hindiOnly() {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == (u + 0020))
                return false;
            else
                return true;
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

    </script>
</asp:Content>

