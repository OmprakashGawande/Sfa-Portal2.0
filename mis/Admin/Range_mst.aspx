<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Range_mst.aspx.cs" Inherits="Range_mst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
                            <h3 class="box-title" id="Label1">Range Master</h3>
                            <asp:Label runat="server" ID="LblMsg" Text=""></asp:Label>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Range Master</legend>
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
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label runat="server">DFO (जिला वन कार्यालय)</asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save"
                                                    ErrorMessage="Select DFO Name" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select DFO Name !'></i>"
                                                    ControlToValidate="ddlDFO" ForeColor="Red" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList ID="ddlDFO" runat="server" CssClass="form-control select2" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <%-- <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label runat="server">Block</asp:Label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save"
                                                 ErrorMessage="Select Block Name" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Block Name !'></i>"
                                                ControlToValidate="ddlBlock" ForeColor="Red" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label runat="server">Range Name (In English)</asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Save"
                                                    ErrorMessage="Enter Range Name English" Text="<i class='fa fa-exclamation-circle' title='Enter Range Name English !'></i>"
                                                    ControlToValidate="txtRangeEnglish" ForeColor="Red" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtRangeEnglish" runat="server"  onkeypress="return lettersOnly();" MaxLength="40" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label runat="server">सीमा का नाम (हिंदी में)</asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="Save"
                                                    ErrorMessage="Enter Range Name Hindi" Text="<i class='fa fa-exclamation-circle' title='Enter Range Name Hindi !'></i>"
                                                    ControlToValidate="txtRangeHindi" ForeColor="Red" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox ID="txtRangeHindi" runat="server" onkeypress="return hindiOnly();" AutoComplete="off" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                </div>
                                <div class="row">
                                    <div class="col-md-5"></div>
                                    <div class="col-md-2">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:Button runat="server" ID="btnSave" CssClass="btn  btn-block btn-success" OnClientClick="return ValidatePage()" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" />
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Button runat="server" ID="btnClear" CssClass="btn btn-block btn-default" Text="Clear" OnClick="btnClear_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <fieldset>
                                <legend>Details</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grvRangeMaster" runat="server" DataKeyNames="Range_ID" AutoGenerateColumns="false" CssClass="table table-bordered" OnRowCommand="grvRangeMaster_RowCommand" Style="text-align: center;">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="S.No. (सरल क्र.)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%#Container.DataItemIndex + 1 %>'></asp:Label>
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
                                                    <asp:TemplateField HeaderText="DFO (जिला वन कार्यालय)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDFOName" Text='<%#Eval("DFO_Name")%>'></asp:Label>
                                                            <asp:Label runat="server" ID="lblDFOId" Text='<%#Eval("DFO_ID")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:TemplateField HeaderText="Block Name">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblBlockName" Text='<%#Eval("Block_Name_E")%>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblBlockId" Text='<%#Eval("Block_ID")%>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Range Name (In English) ">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblRangeNameE" Text='<%#Eval("Range_Name_Eng")%>'></asp:Label>
                                                            <asp:Label runat="server" ID="lblRangeId" Text='<%#Eval("Range_ID") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="सीमा का नाम (हिंदी में)">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblRangeNameH" Text='<%#Eval("Range_Name_Hin")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IsActive">
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" ID="chkActiveRange" Checked='<%#Eval("Range_IsActive").ToString() == "True" ? true : false %>' AutoPostBack="true" OnCheckedChanged="chkActiveRange_CheckedChanged" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" CommandName="btnSelect" CommandArgument='<%#Eval("Range_ID") %>' CssClass="btn btn-primary"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                            <%--<fieldset>
                            <legend>Details</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <table class="table table-bordered">
                                            <tr>
                                                <th style="width: 10px;">S.No.</th>
                                                <th style="width: 20%;">Division</th>
                                                <th style="width: 20%;">District</th>
                                                 <th style="width: 20%;">Block</th>
                                                  <th style="width: 20%;">Range</th>
                                                <th style="width: 11%;">Action</th>

                                            </tr>
                                            <tr>
                                                <td>1</td>
                                                <td>Bhopal (भोपाल)</td>
                                                <td>Raisen (रायसेन)</td>
                                                 <td>Raisen (रायसेन)</td>
                                                <td></td>
                                                <td><i class="fa fa-pen"></i>&nbsp;<i class="fa fa-trash"></i></td>
                                            </tr>
                                            <tr>
                                                <td>2</td>
                                                <td>Indore (इंदौर)</td>
                                                <td>Dhar (धार)</td>
                                                 <td>Dhar (धार)</td>
                                                <td></td>
                                                <td><i class="fa fa-pen"></i>&nbsp;<i class="fa fa-trash"></i></td>
                                            </tr>
                                            <tr>
                                                <td>3</td>
                                                <td>khandwa (खंडवा)</td>
                                                <td>Khargone (खरगोन)</td>
                                                 <td>Khargone (खरगोन)</td>
                                                <td></td>
                                                <td><i class="fa fa-pen"></i>&nbsp;<i class="fa fa-trash"></i></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </fieldset>--%>
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

