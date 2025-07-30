<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Block_mst.aspx.cs" Inherits="Block_mst" %>

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
                            <h3 class="box-title" id="Label1">Block Master</h3>
                            <asp:Label ID="lblmsg" runat="server"></asp:Label>
                        </div>
                        <div class="box-body">
                            <fieldset>
                                <legend>Block Details</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label runat="server">Division (संभाग)</asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save"
                                                    ErrorMessage="Select Division" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Division !'></i>"
                                                    ControlToValidate="ddlDivision" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList runat="server" ID="ddlDivision" CssClass="form-control select2" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label runat="server">District (जिला)</asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                    ErrorMessage="Select District" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select District !'></i>"
                                                    ControlToValidate="ddlDistrict" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>

                                            <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control select2" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label runat="server">Range (सीमा)</asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="Save"
                                                    ErrorMessage="Select Range" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Range !'></i>"
                                                    ControlToValidate="ddlRange" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:DropDownList runat="server" ID="ddlRange" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label runat="server">Block Name (In English)</asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                                    ErrorMessage=" Block Name in English" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Block Name in English !'></i>"
                                                    ControlToValidate="txtBlockEnglish" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox runat="server" ID="txtBlockEnglish"  onkeypress="return lettersOnly();" AutoComplete="off" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label runat="server">विकासखण्ड का नाम (हिंदी में)</asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Save"
                                                    ErrorMessage=" विकासखण्ड का नाम हिंदी में" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='विकासखण्ड का नाम हिंदी में !'></i>"
                                                    ControlToValidate="txtBlockHindi" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:TextBox runat="server" ID="txtBlockHindi" onkeypress="return hindiOnly();" AutoComplete="off" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-md-5"></div>
                                    <div class="col-md-2 pt-4">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:Button runat="server" ID="btnSave" CssClass="btn btn-block btn-success" ValidationGroup="Save" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage() " />
                                            </div>
                                            <div class="col-md-6">
                                                <%--   <asp:Button runat="server" ID="btnClear" CssClass="btn btn-outline-danger btn-block" Text="Clear" OnClick="btnClear_Click" />--%>
                                                <a href="Block_mst.aspx" class="btn btn-block btn-default">Clear</a>
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
                                            <asp:GridView ID="GVBlock" runat="server" DataKeyNames="Block_ID" CssClass="table table-bordered" OnRowCommand="GVBlock_RowCommand" AutoGenerateColumns="false" EmptyDataText="NoRecordFound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No (सरल क्र.)" HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Division ( संभाग)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldivision" runat="server" Text='<%#Eval("Division_Name") %>'></asp:Label>
                                                            <asp:Label ID="lbldivisioId" runat="server" Text='<%#Eval("Division_ID") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="District (जिला)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldistrict" runat="server" Text='<%#Eval("District_Name") %>'></asp:Label>
                                                            <asp:Label ID="lbldistrictId" runat="server" Text='<%#Eval("District_ID") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Range ( सीमा)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="RangeName" runat="server" Text='<%#Eval("Range_Name") %>'></asp:Label>
                                                            <asp:Label ID="RangeID" runat="server" Text='<%#Eval("Range_ID") %>' Visible="false"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Block (विकासखण्ड)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblblocknameEng" runat="server" Text='<%#Eval("Block_Name") %>'></asp:Label>
                                                            <asp:Label ID="lblblockE" runat="server" Text='<%#Eval("Block_Name_Eng") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblblockH" runat="server" Text='<%#Eval("Block_Name_Hin") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="खण्ड का नाम (हिंदी में) ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblblocknameHin" runat="server" Text='<%#Eval("Block_Name_Hin") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Active">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkactive" runat="server" Checked='<%#Eval("Block_IsActive").ToString()=="True"?true:false %>' OnCheckedChanged="chkactive_CheckedChanged" AutoPostBack="true" />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="linkedit" runat="server" CommandArgument='<%#Eval("Block_ID") %>' CommandName="EditRecord" ToolTip="Edit" CssClass="btn btn-primary"><i class="fa fa-edit"></i></asp:LinkButton>
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

