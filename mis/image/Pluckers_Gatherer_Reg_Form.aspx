<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Pluckers_Gatherer_Reg_Form.aspx.cs" Inherits="mis_Trade_Pluckers_Gatherer_Reg_Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
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
                            <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" ValidationGroup="a" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vsm" runat="server" ValidationGroup="b" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vsmNewmember" runat="server" ValidationGroup="c" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="vsmupdatemember" runat="server" ValidationGroup="d" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <div class="box box-success">
                    <div class="box-header with-border">

                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Pluckers Gatherer Form</h3>
                            <%-- <asp:LinkButton ID="lnkViewDetails" Text="View Details" OnClick="lnkViewDetails_Click" CssClass="btn btn-light btnViewDetails" runat="server"></asp:LinkButton>--%>
                            <asp:Label runat="server" ID="lblMsg"></asp:Label>
                        </div>
                        <div class="box-body">
                            <div runat="server" id="UsrFrom">
                                <fieldset>
                                    <legend>Pluckers Gatherer Form</legend>
                                    <%-- <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtFromDate" MaxLength="10" data-date-start-date="-365d" data-date-end-date="0d" placeholder="Enter From Date" data-provide="datepicker" onpaste="return false ;" onkeypress="return false;" data-date-format="dd/mm/yyyy" data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>--%>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Label runat="server"> फड़ का नाम <span style="color:red">*</span></asp:Label>

                                                <span class="pull-right">
                                                    <%--<asp:RequiredFieldValidator ID="rfvYear" ValidationGroup="b"
                                                ErrorMessage="Select Year" ForeColor="Red" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='Select Year'></i>"
                                                ControlToValidate="ddlPhadName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>--%>
                                                </span>
                                                <%-- <asp:Label ID="txtPhadName" MaxLength="15" runat="server" Enabled="false" CssClass="form-control"></asp:Label>--%>
                                                <asp:DropDownList runat="server" ID="ddlPhadName" CssClass="form-control select2" ValidationGroup="a">
                                                    <%--<asp:ListItem>--Select--</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3" runat="server" visible="false">
                                            <asp:Label runat="server">जारी किये गये कार्ड पर मुद्रित अनुक्रमांक  <span style="color:red">*</span></asp:Label>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtRegno" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <asp:Label runat="server"> संग्रहणकर्ताओं के परिवार के मुखिया का नाम तथा उसकी वल्दियत/पति का नाम  <span style="color:red">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvMukhiyaname" ValidationGroup="a"
                                                    ErrorMessage="मुखिया का नाम डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='मुखिया का नाम डाले !'></i>"
                                                    ControlToValidate="txtMukhiyaname" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="txtMukhiyaname"  MaxLength="50" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label runat="server">उम्र <span style="color:red">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvage" ValidationGroup="a"
                                                    ErrorMessage="मुखिया की उम्र डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='मुखिया की उम्र डाले !'></i>"
                                                    ControlToValidate="txtage" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="txtage" TextMode="Number" max="100" min="10" CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>


                                    </div>


                                    <div class="row">

                                        <div class="col-md-3">
                                            <asp:Label runat="server"> जाति <span style="color:red">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvcast" ValidationGroup="a"
                                                    ErrorMessage="मुखिया की जाति चुनें" ForeColor="Red" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='मुखिया की जाति चुनें !'></i>"
                                                    ControlToValidate="ddlcast" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:DropDownList runat="server" CssClass="form-control select2" ID="ddlcast">
                                                    <%-- <asp:ListItem>-- Select --</asp:ListItem>
                                            <asp:ListItem>सामान्य (General)</asp:ListItem>
                                            <asp:ListItem>पिछड़ा वर्ग(OBC)</asp:ListItem>
                                            <asp:ListItem>अनु. जाति(SC)</asp:ListItem>
                                            <asp:ListItem>अनु. ज. जाति(ST)</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label runat="server">गाँव का नाम  <span style="color:red">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvvillage" ValidationGroup="a"
                                                    ErrorMessage="गाँव का नाम चुनें" ForeColor="Red" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='गाँव का नाम चुनें !'></i>"
                                                    ControlToValidate="ddlvillage" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:DropDownList runat="server" CssClass="form-control select2" ID="ddlvillage">
                                                    <%--<asp:ListItem>-- Select --</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label runat="server">आधार नम्बर <span style="color:red">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvadharno" ValidationGroup="a"
                                                    ErrorMessage="मुखिया का आधार नम्बर डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='मुखिया का आधार नम्बर डाले !'></i>"
                                                    ControlToValidate="txtadharno" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revadharno" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtadharno"
                                                    ErrorMessage="अमान्य आधार नम्बर" Text="<i class='fa fa-exclamation-circle' title='अमान्य आधार नम्बर !'></i>"
                                                    SetFocusOnError="true" ForeColor="Red" ValidationExpression="^\d{12}$">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtadharno" MaxLength="12"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label runat="server">मुख्य  समग्र आईo डीo नंबर  <span style="color:red">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvMainSamagraID" ValidationGroup="a"
                                                    ErrorMessage="मुखिया के परिवार का समग्र आईo डीo नम्बर डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='मुखिया के परिवार का समग्र आईo डीo नम्बर डाले !'></i>"
                                                    ControlToValidate="txtMainSamagraID" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtMainSamagraID"
                                                    ErrorMessage="अमान्य परिवार समग्र आईo डीo नम्बर" Text="<i class='fa fa-exclamation-circle' title='अमान्य परिवार समग्र आईo डीo नम्बर !'></i>"
                                                    SetFocusOnError="true" ForeColor="Red" ValidationExpression="[0-9]{8}">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="txtMainSamagraID" CssClass="form-control" MaxLength="8"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Label runat="server"><br />समग्र आईo डीo नंबर <span style="color:red">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvsamagraid" ValidationGroup="a"
                                                    ErrorMessage="मुखिया का समग्र आईo डीo नम्बर डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='मुखिया का समग्र आईo डीo नम्बर डाले !'></i>"
                                                    ControlToValidate="txtsamagraid" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revsamagraid" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtsamagraid"
                                                    ErrorMessage="अमान्य समग्र आईo डीo नम्बर" Text="<i class='fa fa-exclamation-circle' title='अमान्य समग्र आईo डीo नम्बर !'></i>"
                                                    SetFocusOnError="true" ForeColor="Red" ValidationExpression="[0-9]{9}">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtsamagraid" MaxLength="9"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label runat="server"><br />मोबाइल नम्बर <span style="color:red">*</span></asp:Label>
                                            <span class="pull-right">
                                                <asp:RequiredFieldValidator ID="rfvmobileno" ValidationGroup="a"
                                                    ErrorMessage="मुखिया का मोबाइल नम्बर डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='मुखिया का मोबाइल नम्बर डाले !'></i>"
                                                    ControlToValidate="txtmobileno" Display="Dynamic" runat="server">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revmobileno" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtmobileno"
                                                    ErrorMessage="अमान्य मोबाइल नम्बर" Text="<i class='fa fa-exclamation-circle' title='अमान्य मोबाइल नम्बर !'></i>"
                                                    SetFocusOnError="true" ForeColor="Red" ValidationExpression="[0-9]{10}">
                                                </asp:RegularExpressionValidator>
                                            </span>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtmobileno" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label runat="server">परिवार के मुखिया के बी.पी.एल. कार्ड का नंबर (यदि हैं, तो)</asp:Label>
                                            <span class="pull-right">
                                                <%--<asp:RequiredFieldValidator ID="rfvbplcardno" ValidationGroup="a"
                                            ErrorMessage="मुखिया का बी.पी.एल. कार्ड का नम्बर डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='मुखिया का बी.पी.एल. कार्ड का नम्बर डाले !'></i>"
                                            ControlToValidate="txtbplcardno" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                            </span>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" MaxLength="15" CssClass="form-control" ID="txtbplcardno"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label runat="server">परिवार के मुखिया का संबल कार्ड का नंबर (यदि हैं, तो)</asp:Label>
                                            <span class="pull-right">
                                                <%--<asp:RequiredFieldValidator ID="rfvSamble_ID" ValidationGroup="a"
                                            ErrorMessage="मुखिया का संबल कार्ड का नम्बर डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='मुखिया का संबल कार्ड का नम्बर डाले !'></i>"
                                            ControlToValidate="txtSamble_ID" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>--%>
                                            </span>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" MaxLength="9" CssClass="form-control" ID="txtSamble_ID"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <fieldset>
                                        <legend>परिवार के सदस्यों का विवरण</legend>
                                        <div class="row" runat="server" id="divfamilymemberdetail">
                                            <div class="col-md-2">
                                                <asp:Label runat="server">नाम <span style="color:red">*</span></asp:Label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvMember_Name1" ValidationGroup="b"
                                                        ErrorMessage="सदस्य का नाम डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='सदस्य का नाम डाले !'></i>"
                                                        ControlToValidate="txtMember_Name" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="txtMember_Name"  MaxLength="50" AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label runat="server">आधार नम्बर <span style="color:red">*</span></asp:Label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvtxtAadharNo1" ValidationGroup="b"
                                                        ErrorMessage="सदस्य का आधार नम्बर डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='सदस्य का आधार नम्बर डाले !'></i>"
                                                        ControlToValidate="txtAadharNo" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revtxtAadharNo1" ValidationGroup="b" Display="Dynamic" runat="server" ControlToValidate="txtAadharNo"
                                                        ErrorMessage="अमान्य आधार नम्बर" Text="<i class='fa fa-exclamation-circle' title='अमान्य आधार नम्बर !'></i>"
                                                        SetFocusOnError="true" ForeColor="Red" ValidationExpression="^\d{12}$"> 
                                                    </asp:RegularExpressionValidator>
                                                    <%--"^\d{4}\s\d{4}\s\d{4}$"--%>
                                                </span>
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="txtAadharNo" CssClass="form-control" MaxLength="12"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-2">
                                                <asp:Label runat="server">समग्र आईo डीo नंबर  <span style="color:red">*</span></asp:Label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvChildSamagraID1" ValidationGroup="b"
                                                        ErrorMessage="सदस्य का समग्र आईo डीo नम्बर डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='सदस्य का समग्र आईo डीo नम्बर डाले !'></i>"
                                                        ControlToValidate="txtChildSamagraID" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revChildSamagraID1" ValidationGroup="b" Display="Dynamic" runat="server" ControlToValidate="txtChildSamagraID"
                                                        ErrorMessage="अमान्य समग्र आईo डीo नम्बर" Text="<i class='fa fa-exclamation-circle' title='अमान्य समग्र आईo डीo नम्बर !'></i>"
                                                        SetFocusOnError="true" ForeColor="Red" ValidationExpression="[0-9]{9}">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="txtChildSamagraID" CssClass="form-control" MaxLength="9"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label runat="server">मुखिया से सम्बन्ध  <span style="color:red">*</span></asp:Label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvRelation" ValidationGroup="b"
                                                        ErrorMessage="मुखिया से सम्बन्ध चुनें" ForeColor="Red" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='मुखिया से सम्बन्ध चुनें !'></i>"
                                                        ControlToValidate="ddlRelation" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:DropDownList runat="server" CssClass="form-control select2" ID="ddlRelation">
                                                        <%-- <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                <asp:ListItem Value="1">पिता</asp:ListItem>
                                                <asp:ListItem Value="2">माँ</asp:ListItem>
                                                <asp:ListItem Value="6">पत्नी</asp:ListItem>
                                                <asp:ListItem Value="3">भाई</asp:ListItem>
                                                <asp:ListItem Value="4">बहन</asp:ListItem>
                                                <asp:ListItem Value="5">बेटा</asp:ListItem>
                                                <asp:ListItem Value="6">बेटी</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <asp:Label runat="server">उम्र  <span style="color:red">*</span></asp:Label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfvChildAge" ValidationGroup="b"
                                                        ErrorMessage="सदस्य की उम्र डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='सदस्य की उम्र डाले !'></i>"
                                                        ControlToValidate="txtChildAge" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" max="100" min="10" ID="txtChildAge" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2" style="margin-top: 20px;">
                                                <asp:Button runat="server" ID="btnadd" ValidationGroup="b" CssClass="btn btn-success btn-block" Text="Add More " OnClick="btnadd_Click" />
                                            </div>

                                        </div>
                                        <fieldset>

                                            <div class="row" runat="server" visible="false" id="divfamilymember">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="Gridfamilymember" runat="server" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="सरल क्र." ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="PLKR_ChildName_H" HeaderText=" सदस्य का नाम" />
                                                                <asp:BoundField DataField="PLKR_ChildAdhar_No" HeaderText="आधार नम्बर" />
                                                                <asp:BoundField DataField="Samagra_ID" HeaderText="समग्र आईo डीo" />
                                                                <asp:BoundField DataField="PLKR_Relation" HeaderText="मुखिया से सम्बन्ध" />
                                                                <asp:BoundField DataField="PLKR_Child_Age" HeaderText="उम्र" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" OnClick="OnDelete" Text="हटाये" class="fa fa-trash" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                            <EmptyDataTemplate>No Records.</EmptyDataTemplate>
                                                        </asp:GridView>
                                                        <%--<table class="table table-bordered">
                                            <tr>
                                                <th>सरल क्र.</th>
                                                <th> सदस्य का नाम</th>
                                                <th>आधार नम्बर</th>
                                                 <th>समग्र आईo डीo</th>
                                                <th>मुखिया से सम्बन्ध</th>
                                                <th>उम्र</th>
                                                <th>सुधार करें/ हटाये</th>
                                            </tr>
                                            <tr>
                                                <td>1</td>
                                                <td>राम</td>
                                                <td>965845258</td>
                                                <td>965845258</td>
                                                <td>भाई</td>
                                                <td>21</td>
                                                <td><i class="fa fa-pen"></i>&nbsp;<i class="fa fa-trash"></i></td>
                                            </tr>
                                              <tr>
                                                <td>1</td>
                                                <td>रघु</td>
                                                <td>925478258</td>
                                                  <td>965845258</td>
                                                <td>भाई</td>
                                                <td>21</td>
                                                <td><i class="fa fa-pen"></i>&nbsp;<i class="fa fa-trash"></i></td>
                                            </tr>
                                            </table>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <fieldset>
                                        <legend>बैंक विवरण</legend>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label runat="server">बैंक का नाम <span style="color:red">*</span></asp:Label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a" Enabled="true"
                                                            ErrorMessage="बैंक का नाम चुनें " InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='बैंक का नाम चुनें !'></i>"
                                                            ControlToValidate="ddlBankName" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:DropDownList runat="server" ID="ddlBankName" CssClass="form-control select2">
                                                        <%--<asp:ListItem>--Select--</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label runat="server">शाखा का नाम <span style="color:red">*</span></asp:Label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvBranchName" ValidationGroup="a" Enabled="true"
                                                            ErrorMessage="शाखा का नाम डालें " Text="<i class='fa fa-exclamation-circle' title='शाखा का नाम डालें!'></i>"
                                                            ControlToValidate="txtBranchName" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:TextBox runat="server" ID="txtBranchName"  AutoComplete="off" CssClass="form-control" MaxLength="50"></asp:TextBox>

                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label runat="server">आईoएफoएसoसीo कोड <span style="color:red">*</span></asp:Label>

                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvIFSCCode" ValidationGroup="a" Enabled="true"
                                                            ErrorMessage="आईoएफoएसoसीo कोड डालें" Text="<i class='fa fa-exclamation-circle' title='आईoएफoएसoसीo कोड डालें!'></i>"
                                                            ControlToValidate="txtIFSCCode" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtIFSCCode"
                                                            ErrorMessage="अमान्य आईoएफoएसoसीo कोड" Text="<i class='fa fa-exclamation-circle' title='अमान्य आईoएफoएसoसीo कोड !'></i>"
                                                            SetFocusOnError="true" ForeColor="Red" ValidationExpression="^[0-9a-z-A-Z]{11}$">
                                                        </asp:RegularExpressionValidator>
                                                    </span>
                                                    <asp:TextBox runat="server" ID="txtIFSCCode" CssClass="form-control" MaxLength="11"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label runat="server">बैंक खाता नम्बर <span style="color:red">*</span></asp:Label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvbankaccountNo" ValidationGroup="a" Enabled="true"
                                                            ErrorMessage="बैंक खाता नम्बर डालें" Text="<i class='fa fa-exclamation-circle' title='बैंक खाता नम्बर डालें!'></i>"
                                                            ControlToValidate="txtbankaccountNo" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtbankaccountNo"
                                                            ErrorMessage="अमान्य खाता नम्बर" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='अमान्य खाता नम्बर!'></i>"
                                                            SetFocusOnError="true" ValidationExpression="^[0-9]{10,18}$">
                                                        </asp:RegularExpressionValidator>
                                                    </span>
                                                    <asp:TextBox runat="server" ID="txtbankaccountNo" CssClass="form-control" MaxLength="20"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:Label runat="server">खाता धारक का नाम <span style="color:red">*</span></asp:Label>
                                                    <span class="pull-right">
                                                        <asp:RequiredFieldValidator ID="rfvAccountHolderName" ValidationGroup="a" Enabled="true"
                                                            ErrorMessage="खाता धारक का नाम डालें" Text="<i class='fa fa-exclamation-circle' title='खाता धारक का नाम डालें!'></i>"
                                                            ControlToValidate="txtAccountHolderName" ForeColor="Red" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </span>
                                                    <asp:TextBox runat="server" ID="txtAccountHolderName" nkeypress="return hindiOnly();" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </fieldset>
                                </fieldset>
                                <div class="row">
                                    <div class="col-md-4"></div>
                                    <div class="col-md-4 pt-4">
                                        <div class="row">
                                            <div class="col-md-6">

                                                <asp:Button runat="server" ID="btnSave" ValidationGroup="a" CssClass="btn btn-success btn-block" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidatePage1()" />
                                                <%-- <asp:LinkButton runat="server" ID="lnkSearch" OnClick="lnkSearch_Click" ValidationGroup="a" CssClass="btn btn-outline-primary btn-block" Text=""><i class="fa fa-search"></i> Search</asp:LinkButton>--%>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                  <%--  <asp:Button runat="server" ID="btnclear" CssClass="btn btn-outline-danger btn-block" Text="Clear" OnClick="btnclear_Click" />--%>
                                                    <a href="Pluckers_Gatherer_Reg_Form.aspx" class="btn btn-danger btn-block">Clear</a>
                                                    <%-- <asp:LinkButton runat="server" ID="btnClear" OnClick="btnClear_Click" CssClass="btn btn-outline-danger btn-block" Text="Reset"></asp:LinkButton>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="box-footer">
                                    <fieldset runat="server" id="fieldsetdetails">
                                        <legend>विवरण</legend>

                                        <div class="row">
                                            <div class="col-md-2" style="margin-left: 14px;margin-bottom:2px;">
                                                <asp:Button runat="server" ID="btnExport" Visible="false" Text="Export" OnClick="btnExport_Click" CssClass="btn btn-primary btn-block" />
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="table-responsive" runat="server" id="divgrid" visible="true">
                                                <asp:GridView ID="GridViewPlucker" OnRowCommand="GridViewPlucker_RowCommand" PageSize="50" runat="server"
                                                    CssClass="datatable table table-striped table-bordered table-hover"
                                                    ShowHeaderWhenEmpty="true" AutoGenerateColumns="False"
                                                    EmptyDataText="No Record Found." DataKeyNames="Office_ID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="सीरियल नम्बर" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblrowID" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                <asp:Label ID="lblPLKR_ID" runat="server" Visible="false" Text='<%# Eval("PLKR_ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Active/Deactive<br />(सक्रिय/निष्क्रिय)">
                                                            <ItemTemplate>
                                                                &nbsp;<asp:CheckBox ID="chkActive" CommandArgument='<%#Eval("PLKR_ID") %>' runat="server" ToolTip="Delete" Style="color: black;" OnClientClick="return confirm('Are you sure want to change status?')" Checked='<%# Eval("IsActive").ToString()== "True" ? true: false %>' OnCheckedChanged="chkActive_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actions">
                                                            <ItemTemplate>
                                                                &nbsp;<asp:LinkButton ID="lblviewchild" CommandArgument='<%#Eval("PLKR_ID") %>' CommandName="View" runat="server" ToolTip="view" Style="color: black;" CssClass="fa fa-eye"><%--<i class="fa fa-eye"></i>--%></asp:LinkButton>
                                                                &nbsp;<asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("PLKR_ID") %>' runat="server" ToolTip="Update" Style="color: black;" CssClass="fa fa-edit"><%--<i class="fa fa-pen"></i>--%></asp:LinkButton>

                                                                &nbsp;
                                                        <asp:LinkButton ID="lnkadd" CommandName="RecordAdd" CommandArgument='<%#Eval("PLKR_ID") %>' runat="server" ToolTip="Add Member" Style="color: black;" CssClass="fa fa-plus"><%--<i class="fa fa-plus"></i>--%></asp:LinkButton>
                                                                &nbsp;<asp:LinkButton ID="lnkchildupdate" CommandName="ChildUpdate" CommandArgument='<%#Eval("PLKR_ID") %>' runat="server" ToolTip="Member Details Update" Style="color: black;" CssClass="fa fa-edit"><%--<i class="fa fa-pen"></i>--%></asp:LinkButton>
                                                                &nbsp;<asp:LinkButton Visible="false" ID="lnkDelete" CommandArgument='<%#Eval("PLKR_ID") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: black;" OnClientClick="return confirm('Are you sure want to change status?')" Text='<%# Eval("IsActive").ToString()== "True" ? "Active" : "Deactive" %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="जारी किये गये कार्ड पर मुद्रित अनुक्रमांक">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPLKR_RegNo" runat="server" Text='<%# Eval("PLKR_RegNo") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="फड़ का नाम">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPhad_Name" runat="server" Text='<%# Eval("Phad_Name") %>' />
                                                                <asp:Label ID="lblPhad_ID" Visible="false" runat="server" Text='<%# Eval("Phad_ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="मुखिया का नाम">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPLKRName_H" runat="server" Text='<%# Eval("PLKRName_H") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="उम्र">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPLKR_Age" runat="server" Text='<%# Eval("PLKR_Age") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="जाति">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCast_ID" Visible="false" runat="server" Text='<%# Eval("Cast_ID") %>' />
                                                                <asp:Label ID="lblPLKR_Cast" runat="server" Text='<%# Eval("PLKR_Cast") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="गाँव का नाम">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVillage_Name" runat="server" Text='<%# Eval("Village_Name") %>' />
                                                                <asp:Label ID="lblVillage_ID" Visible="true" runat="server" Text='<%# Eval("Village_ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="आधार नम्बर">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAdhar_No" runat="server" Text='<%# Eval("Adhar_No") %>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="मोबाइल नम्बर">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMobile_No" runat="server" Text='<%# Eval("Mobile_No") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="बैंक का नाम">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("BankName") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="बैंक खाता नम्बर">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("Bank_Account_No") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="आईoएफoएसoसीo कोड ">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("IFSC_Code") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="खाता धारक का नाम ">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("Acount_Holder_Name") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:TemplateField HeaderText=" बी.पी.एल. कार्ड का नंबर ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBPL_Card_no" runat="server" Text='<%# Eval("BPL_Card_no") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="संबल कार्ड का नंबर ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSamble_ID" runat="server" Text='<%# Eval("Samble_ID") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>

                                                                <asp:Label ID="lblBank_ID" runat="server" Text='<%# Eval("Bank_ID") %>' />
                                                                <asp:Label ID="lblBranch_Name" runat="server" Text='<%# Eval("Branch_Name") %>' />
                                                                <asp:Label ID="lblIFSC_Code" runat="server" Text='<%# Eval("IFSC_Code") %>' />
                                                                <asp:Label ID="lblBank_Account_No" runat="server" Text='<%# Eval("Bank_Account_No") %>' />
                                                                <asp:Label ID="lblAcount_Holder_Name" runat="server" Text='<%# Eval("Acount_Holder_Name") %>' />
                                                                <asp:Label ID="lblSamagra_ID" runat="server" Text='<%# Eval("Samagra_ID") %>' />

                                                                <asp:Label ID="lblMain_samagra_ID" runat="server" Text='<%# Eval("Main_samagra_ID") %>' />
                                                                <asp:Label ID="lblBPL_Card_no" runat="server" Text='<%# Eval("BPL_Card_no") %>' />
                                                                <asp:Label ID="lblSamble_ID" runat="server" Text='<%# Eval("Samble_ID") %>' />

                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                            </div>
                                        </div>

                                    </fieldset>
                                    <div id="pnldata" runat="server" visible="false">
                                        <div class="col-md-12">
                                            <asp:Label runat="server" ID="lbldatetext" Style="color: red; padding-left: 10px;"></asp:Label>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:Repeater ID="Repeater1" runat="server">
                                                <HeaderTemplate>
                                                    <table class="table table-striped table-bordered table-hover" style="border: solid 1px;">
                                                        <tr style="border: solid 1px;">

                                                            <td>
                                                                <b>सीरियल नम्बर</b>
                                                            </td>
                                                            <td>
                                                                <b>जारी किये गये कार्ड पर मुद्रित अनुक्रमांक </b>
                                                            </td>
                                                            <td>
                                                                <b>फड़ का नाम  </b>
                                                            </td>
                                                            <td>
                                                                <b>मुखिया का नाम</b>
                                                            </td>
                                                            <td>
                                                                <b>उम्र</b>
                                                            </td>
                                                            <td>
                                                                <b>जाति</b>
                                                            </td>
                                                            <td>
                                                                <b>गाँव का नाम</b>
                                                            </td>
                                                            <td>
                                                                <b>आधार नम्बर</b>
                                                            </td>
                                                            <td>
                                                                <b>मोबाइल नम्बर</b>
                                                            </td>
                                                            <td>
                                                                <b>बैंक का नाम</b>
                                                            </td>
                                                            <td>
                                                                <b>बैंक खाता नम्बर</b>
                                                            </td>
                                                            <td>
                                                                <b>आईoएफoएसoसीo कोड</b>
                                                            </td>
                                                            <td>
                                                                <b>खाता धारक का नाम </b>
                                                            </td>



                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr style="border: solid 1px;">
                                                        <td>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" />
                                                        </td>
                                                        <td>
                                                            <%# Eval("PLKR_RegNo")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Phad_Name")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("PLKRName_H")%>
                                                        </td>
                                                        <td>
                                                            <%#Eval("PLKR_Age").ToString()  %>
                                                        </td>
                                                        <td>
                                                           <%#Eval("PLKR_Cast") %>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Village_Name")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Adhar_No")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Mobile_No")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("BankName")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Bank_Account_No")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("IFSC_Code")%>
                                                        </td>
                                                        <td>
                                                            <%# Eval("Acount_Holder_Name")%>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                            </div>



                        </div>
                    </div>
                </div>
            </div>

            <div class="modal" id="familymamber" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h6 class="modal-title">
                                    <label id="LblTitle" runat="server">परिवार के सदस्यों का विवरण</label>
                                </h6>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblmsgModal" CssClass="Autoclr" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <%--<asp:Label ID="lblmsgModal" runat="server" Text="" Visible="true"></asp:Label>--%>
                                <div class="card-body">
                                    <asp:Panel ID="pnlDiv" runat="server">

                                        <div class="row">
                                            <div class="col-md-8">
                                                <label>संग्रहणकर्ताओं के परिवार के मुखिया का नाम : </label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="lblMukhiyaname" runat="server"></asp:Label>
                                            </div>
                                            <%-- <div class="col-md-3">
                                            <label>Invoice No.:</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="" runat="server"></asp:Label>
                                        </div>--%>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table table-responsive">

                                                    <asp:GridView ID="gridFamilymemberdetail" Visible="false" runat="server" DataKeyNames="PLKR_Child_ID" class="table table-hover table-bordered datatable" ShowHeaderWhenEmpty="true" ShowFooter="false" AutoGenerateColumns="False" OnRowCommand="gridFamilymemberdetail_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S. No" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    <asp:Label ID="lblPLKR_Child_ID" Visible="false" runat="server" Text='<%# Eval("PLKR_Child_ID").ToString() %>'></asp:Label>
                                                                    <asp:Label ID="lblPLKR_ID" Visible="false" runat="server" Text='<%# Eval("PLKR_ID").ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="सदस्य का नाम" ItemStyle-Width="200px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPLKR_ChildName_H" runat="server" Text='<%# Eval("PLKR_ChildName_H").ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="आधार नम्बर">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPLKR_ChildAdhar_No" runat="server" Text='<%# Eval("PLKR_ChildAdhar_No").ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="समग्र आईo डीo">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSamagra_ID" runat="server" Text='<%# Eval("Samagra_ID").ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="मुखिया से सम्बन्ध">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPLKR_Relation" runat="server" Text='<%# Eval("PLKR_Relation").ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="उम्र">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPLKR_Child_Age" runat="server" Text='<%# Eval("PLKR_Child_Age").ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    &nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("PLKR_Child_ID") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete Member" Style="color: black;" OnClientClick="return confirm('Are you sure want delete This Member?')"><i class="fa fa-trash"></i></asp:LinkButton>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataTemplate>No Data Found</EmptyDataTemplate>
                                                    </asp:GridView>


                                                </div>
                                            </div>
                                        </div>


                                        <div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </div>
            <div class="modal" id="familymamberupdate" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h6 class="modal-title">
                                    <label id="Label3" runat="server">परिवार के सदस्यों के आकड़ें अपडेट करें</label>
                                </h6>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="row">

                                    <div class="col-lg-12">
                                        <asp:Label ID="lblmsgModalupdate" runat="server" Text="" Visible="true"></asp:Label>
                                        <asp:Label ID="Label1" CssClass="Autoclr" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <%----%>
                                <div class="card-body">
                                    <asp:Panel ID="Panel1" runat="server">

                                        <div class="row">
                                            <div class="col-md-8">
                                                <label>संग्रहणकर्ताओं के परिवार के मुखिया का नाम : </label>
                                            </div>
                                            <div class="col-md-4">
                                                <asp:Label ID="Label2" runat="server"></asp:Label>
                                            </div>
                                            <%-- <div class="col-md-3">
                                            <label>Invoice No.:</label>
                                        </div>
                                        <div class="col-md-2">
                                            <asp:Label ID="" runat="server"></asp:Label>
                                        </div>--%>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table table-responsive">

                                                    <asp:GridView ID="GridViewchildupdate" ValidationGroup="d" Visible="false" runat="server" DataKeyNames="PLKR_Child_ID" class="table table-hover table-bordered datatable" ShowHeaderWhenEmpty="true" ShowFooter="false" AutoGenerateColumns="False" OnRowCommand="gridFamilymemberdetail_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S. No" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    <asp:Label ID="lblPLKR_Child_ID" Visible="false" runat="server" Text='<%# Eval("PLKR_Child_ID").ToString() %>'></asp:Label>
                                                                    <asp:Label ID="lblPLKR_ID" Visible="false" runat="server" Text='<%# Eval("PLKR_ID").ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="सदस्य का नाम" ItemStyle-Width="200px">
                                                                <ItemTemplate>
                                                                    <asp:RequiredFieldValidator ID="rfvMember_Name" ValidationGroup="d"
                                                                        ErrorMessage="सदस्य का नाम डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='सदस्य का नाम डाले !'></i>"
                                                                        ControlToValidate="txtPLKR_ChildName_H" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtPLKR_ChildName_H" runat="server" MaxLength="50" Text='<%# Eval("PLKR_ChildName_H").ToString() %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="आधार नम्बर">
                                                                <ItemTemplate>
                                                                    <asp:RequiredFieldValidator ID="rfvtxtAadharNo" ValidationGroup="d"
                                                                        ErrorMessage="सदस्य का आधार नम्बर डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='सदस्य का आधार नम्बर डाले !'></i>"
                                                                        ControlToValidate="txtPLKR_ChildAdhar_No" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revtxtAadharNo" ValidationGroup="d" Display="Dynamic" runat="server" ControlToValidate="txtPLKR_ChildAdhar_No"
                                                                        ErrorMessage="अमान्य आधार नम्बर" Text="<i class='fa fa-exclamation-circle' title='अमान्य आधार नम्बर !'></i>"
                                                                        SetFocusOnError="true" ForeColor="Red" ValidationExpression="^\d{12}$">
                                                                    </asp:RegularExpressionValidator>
                                                                    <asp:TextBox ID="txtPLKR_ChildAdhar_No" MaxLength="12" runat="server" Text='<%# Eval("PLKR_ChildAdhar_No").ToString() %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="समग्र आईo डीo">
                                                                <ItemTemplate>
                                                                    <asp:RequiredFieldValidator ID="rfvChildSamagraID" ValidationGroup="d"
                                                                        ErrorMessage="सदस्य का समग्र आईo डीo नम्बर डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='सदस्य का समग्र आईo डीo नम्बर डाले !'></i>"
                                                                        ControlToValidate="txtSamagra_ID" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revChildSamagraID" ValidationGroup="d" Display="Dynamic" runat="server" ControlToValidate="txtSamagra_ID"
                                                                        ErrorMessage="अमान्य समग्र आईo डीo नम्बर" Text="<i class='fa fa-exclamation-circle' title='अमान्य समग्र आईo डीo नम्बर !'></i>"
                                                                        SetFocusOnError="true" ForeColor="Red" ValidationExpression="[0-9]{9}">
                                                                    </asp:RegularExpressionValidator>
                                                                    <asp:TextBox ID="txtSamagra_ID" runat="server" MaxLength="9" Text='<%# Eval("Samagra_ID").ToString() %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="उम्र">
                                                                <ItemTemplate>

                                                                    <asp:RequiredFieldValidator ID="rfvChildAge" ValidationGroup="d"
                                                                        ErrorMessage="सदस्य की उम्र डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='सदस्य की उम्र डाले !'></i>"
                                                                        ControlToValidate="txtPLKR_Child_Age" Display="Dynamic" runat="server">
                                                                    </asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtPLKR_Child_Age" TextMode="Number" Max="100" min="1" runat="server" Text='<%# Eval("PLKR_Child_Age").ToString() %>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="मुखिया से सम्बन्ध">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPLKR_Relation" runat="server" Text='<%# Eval("PLKR_Relation").ToString() %>'></asp:Label>
                                                                    <asp:Label ID="lblRelation_ID" Visible="false" runat="server" Text='<%# Eval("Relation_ID").ToString() %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%-- <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                &nbsp;<asp:LinkButton ID="lnkDelete" CommandArgument='<%#Eval("PLKR_Child_ID") %>' CommandName="RecordDelete" runat="server" ToolTip="Delete" Style="color: black;" OnClientClick="return confirm('Are you sure want delete This Member?')"  ><i class="fa fa-trash"></i></asp:LinkButton>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        </Columns>
                                                        <EmptyDataTemplate>No Data Found</EmptyDataTemplate>
                                                    </asp:GridView>


                                                </div>
                                            </div>
                                            <div class="col-md-12">

                                                <div class="col-md-5">
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Button runat="server" ID="BtnUpdateChild" ValidationGroup="d" CssClass="btn btn-success btn-block" Text="Update" OnClick="BtnUpdateChild_Click" />
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                </div>
                                            </div>
                                        </div>
                                        <%--</div>


                                    <div>
                                    </div>--%>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </div>
            <div class="modal" id="ADDfamilymamber" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h6 class="modal-title">
                                    <label id="Label4" runat="server">परिवार के सदस्य जोड़े</label>
                                </h6>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblmsgmodalNew" runat="server" Text="" Visible="true"></asp:Label>
                                        <asp:Label ID="Label5" CssClass="Autoclr" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <%--<asp:Label ID="lblmsgModal" runat="server" Text="" Visible="true"></asp:Label>--%>
                                <div class="card-body">
                                    <fieldset>
                                        <legend>परिवार के सदस्यों का विवरण</legend>
                                        <div class="row" runat="server" id="div1">
                                            <div class="col-md-6">
                                                <asp:Label runat="server">नाम <span style="color:red">*</span></asp:Label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="c"
                                                        ErrorMessage="सदस्य का नाम डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='सदस्य का नाम डाले !'></i>"
                                                        ControlToValidate="txtMember_NameNew" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="txtMember_NameNew" MaxLength="50"  AutoComplete="off" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server">आधार नम्बर <span style="color:red">*</span></asp:Label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="c"
                                                        ErrorMessage="सदस्य का आधार नम्बर डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='सदस्य का आधार नम्बर डाले !'></i>"
                                                        ControlToValidate="txtAadharNoNew" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revAadharNoNew" ValidationGroup="c" Display="Dynamic" runat="server" ControlToValidate="txtAadharNoNew"
                                                        ErrorMessage="अमान्य आधार नम्बर" Text="<i class='fa fa-exclamation-circle' title='अमान्य आधार नम्बर !'></i>"
                                                        SetFocusOnError="true" ForeColor="Red" ValidationExpression="^\d{12}$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="txtAadharNoNew" CssClass="form-control" MaxLength="12"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <asp:Label runat="server">समग्र आईo डीo नंबर  <span style="color:red">*</span></asp:Label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="c"
                                                        ErrorMessage="सदस्य का समग्र आईo डीo नम्बर डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='सदस्य का समग्र आईo डीo नम्बर डाले !'></i>"
                                                        ControlToValidate="txtChildSamagraIDNew" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:RegularExpressionValidator ID="revChildSamagraIDNew" ValidationGroup="c" Display="Dynamic" runat="server" ControlToValidate="txtChildSamagraIDNew"
                                                    ErrorMessage="अमान्य समग्र आईo डीo नम्बर" Text="<i class='fa fa-exclamation-circle' title='अमान्य समग्र आईo डीo नम्बर !'></i>"
                                                    SetFocusOnError="true" ForeColor="Red" ValidationExpression="[0-9]{9}">
                                                </asp:RegularExpressionValidator>
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="txtChildSamagraIDNew" CssClass="form-control" MaxLength="9"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server">मुखिया से सम्बन्ध  <span style="color:red">*</span></asp:Label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="c"
                                                        ErrorMessage="मुखिया से सम्बन्ध चुनें" ForeColor="Red" InitialValue="0" Text="<i class='fa fa-exclamation-circle' title='मुखिया से सम्बन्ध चुनें !'></i>"
                                                        ControlToValidate="ddlRelationNew" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:DropDownList runat="server" CssClass="form-control select2" ID="ddlRelationNew">
                                                        <%--<asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                    <asp:ListItem Value="1">पिता</asp:ListItem>
                                                    <asp:ListItem Value="2">माँ</asp:ListItem>
                                                    <asp:ListItem Value="3">भाई</asp:ListItem>
                                                    <asp:ListItem Value="4">बहन</asp:ListItem>
                                                    <asp:ListItem Value="5">बेटा</asp:ListItem>
                                                    <asp:ListItem Value="6">बेटी</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label runat="server">उम्र  <span style="color:red">*</span></asp:Label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ValidationGroup="c"
                                                        ErrorMessage="सदस्य की उम्र डाले" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='सदस्य की उम्र डाले !'></i>"
                                                        ControlToValidate="txtChildAgeNew" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" max="100" min="1" ID="txtChildAgeNew" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <br />
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <asp:Button runat="server" ID="btnaddnew" ValidationGroup="c" CssClass="btn btn-success btn-block" Text="Add More " OnClick="btnaddnew_Click" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <fieldset>

                                            <div class="row" runat="server" visible="false" id="divfamilymemberNew">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GridfamilymemberNew" runat="server" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="सरल क्र." ItemStyle-Width="5%">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="PLKR_ChildName_H" HeaderText=" सदस्य का नाम" />
                                                                <asp:BoundField DataField="PLKR_ChildAdhar_No" HeaderText="आधार नम्बर" />
                                                                <asp:BoundField DataField="Samagra_ID" HeaderText="समग्र आईo डीo" />
                                                                <asp:BoundField DataField="PLKR_Relation" HeaderText="मुखिया से सम्बन्ध" />
                                                                <asp:BoundField DataField="PLKR_Child_Age" HeaderText="उम्र" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" OnClick="OnDeleteNew" Text="हटाये" class="fa fa-trash" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                            <%--  <EmptyDataTemplate>No Records.</EmptyDataTemplate>--%>
                                                        </asp:GridView>

                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </fieldset>
                                    <div class="row">
                                    </div>
                                    <div class="row">
                                        <%-- <div class="col-md-5"></div>--%>
                                        <div class="col-md-12 pt-4">
                                            <div class="row">
                                                <div class="col-md-3">
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Button runat="server" ID="btnSaveNew" Visible="false" CssClass="btn btn-success btn-block" Text="Save New Member" OnClick="btnSaveNew_Click" />
                                                </div>
                                                <div class="col-md-3">
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </div>


        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
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
        function ValidatePage1() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%= btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
        function ValidatePage2() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('b');
            }

            <%--if (Page_IsValid) {

                if (document.getElementById('<%=btnadd.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnadd.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                     $('#myModal').modal('show');
                     return false;
                 }
             }--%>
         }
        function ValidatePage3() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('c');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSaveNew.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%= btnSaveNew.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                     $('#myModal').modal('show');
                     return false;
                 }
             }
         }
    </script>
    <script>function ShowModal() {
    $('#familymamber').modal('toggle');
}
    </script>
    <script>function ShowModalmamberupdate() {
    $('#familymamberupdate').modal('toggle');
}
    </script>
    <script>function ShowModalADD() {
    $('#ADDfamilymamber').modal('toggle');
}
    </script>
</asp:Content>

