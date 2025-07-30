<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminOffice.aspx.cs" Inherits="mis_Admin_AdminOffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">  
 <style>
     .modal {
}
.vertical-alignment-helper {
    display:table;
    height: 100%;
    width: 100%;
}
.vertical-align-center {
    /* To center vertically */
    display: table-cell;
    vertical-align: middle;
}
.modal-content {
    /* Bootstrap sets the size of the modal in the modal-dialog class, we need to inherit it */
    width:inherit;
    height:inherit;
    /* To center horizontally */
    margin: 0 auto;
}
.CapitalClass{
    text-transform:uppercase;
}
 </style>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <%--Confirmation Modal Start --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
    <div class="vertical-alignment-helper">
        <div class="modal-dialog vertical-align-center">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                    </button>
                     <h4 class="modal-title" id="myModalLabel">Company [Office] Master</h4>

                </div>
                <div class="modal-body"><p><asp:Label ID="lblPopupAlert" runat="server"></asp:Label></p></div>
                <div class="modal-footer">
                     <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnSave_Click" />
                   <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>
</div>

     <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="vgao" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Company [Office] Master</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Title<span style="color: red;"> *</span></label>
                                <span class="pull-right"><asp:RequiredFieldValidator ID="rfv1" ValidationGroup="vgao"
                                    InitialValue="0" ErrorMessage="Please Select Office Tittle" Text="<i class='fa fa-exclamation-circle' title='Please Select Office Tittle !'></i>"
                                    ControlToValidate="ddlOfficeType_Title" Display="Dynamic" runat="server">
                                </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlOfficeType_Title" runat="server" CssClass="form-control select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlOfficeType_Title_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" id="spanRegion" runat="server" visible="false">
                            <div class="form-group">
                                <label>Region Name<span style="color: red;"> *</span></label>
                                <span class="pull-right"><asp:RequiredFieldValidator ID="rfv2" ValidationGroup="vgao"
                                    InitialValue="0" ErrorMessage="Please Select Region Name" Text="<i class='fa fa-exclamation-circle' title='Please Select Region Name !'></i>"
                                    ControlToValidate="ddlDivision_Name" Display="Dynamic" runat="server">
                                </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlDivision_Name" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlDivision_Name_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" id="spanDistrict" runat="server" visible="false">
                            <div class="form-group">
                                <label>District Name<span  style="color: red;"> *</span></label>
                                <span class="pull-right"><asp:RequiredFieldValidator ID="rfv3" ValidationGroup="vgao"
                                    InitialValue="0" ErrorMessage="Please Select District Name" Text="<i class='fa fa-exclamation-circle' title='Please Select District Name !'></i>"
                                    ControlToValidate="ddlDistrict_Name" Display="Dynamic" runat="server">
                                </asp:RequiredFieldValidator></span>
                                <asp:DropDownList ID="ddlDistrict_Name" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlDistrict_Name_SelectedIndexChanged">
                                   <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <%--<div class="col-md-3">
                            <div class="form-group">
                                <label>Office Title<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlOfficeType_Title" runat="server" CssClass="form-control" ClientIDMode="Static" OnSelectedIndexChanged="ddlOfficeType_Title_SelectedIndexChanged" AutoPostBack="true" onchange="ChangeMantField()"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" id="spanRegion" hidden="hidden">
                            <div class="form-group">
                                <label>Region Name<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlDivision_Name" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlDivision_Name_SelectedIndexChanged" onchange="fillRegionOffice();"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" id="spanDistrict" hidden="hidden">
                            <div class="form-group">
                                <label>District Name<span  style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlDistrict_Name" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlDistrict_Name_SelectedIndexChanged" onchange="fillDistrictOffice()">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>--%>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Contact Number<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                   <asp:RequiredFieldValidator ID="rfv4" ValidationGroup="vgao"
                                    ErrorMessage="Please Enter Contact Number" Text="<i class='fa fa-exclamation-circle' title='Please Enter Contact Number !'></i>"
                                    ControlToValidate="txtOffice_ContactNo" Display="Dynamic" runat="server">
                                </asp:RequiredFieldValidator>
                               <%-- <asp:RegularExpressionValidator ID="revmobile" runat="server" Display="Dynamic"  ValidationGroup="vgao"
                                    ErrorMessage="Please Enter Valid Mobile No !" Text="<i class='fa fa-exclamation-circle' title='Please Enter Valid Mobile No !'></i>" ControlToValidate="txtOffice_ContactNo"
                                    ValidationExpression="^[5-9][0-9]{9}$">
                                </asp:RegularExpressionValidator>--%>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control  MobileNo" ID="txtOffice_ContactNo" MaxLength="10" onkeypress="return validateNum(event);" placeholder="Enter Contact Number"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Email<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                   <asp:RequiredFieldValidator ID="rfv5" ValidationGroup="vgao"
                                    ErrorMessage="Please Enter Office Email" Text="<i class='fa fa-exclamation-circle' title='Please Enter Office Email !'></i>"
                                    ControlToValidate="txtOffice_Email" Display="Dynamic" runat="server">
                                </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revemail" runat="server" ControlToValidate="txtOffice_Email"
                                    ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                    Display = "Dynamic" ErrorMessage = "Invalid email address" ValidationGroup="vgao" Text="<i class='fa fa-exclamation-circle' title='Invalid email address !'></i>"/>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOffice_Email" MaxLength="50" placeholder="Enter Email" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3 hidden">
                            <div class="form-group">
                                <label>Block Name<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlBlock_Name" runat="server" CssClass="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Address<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                   <asp:RequiredFieldValidator ID="rfv6" ValidationGroup="vgao"
                                    ErrorMessage="Please Enter Office Address" Text="<i class='fa fa-exclamation-circle' title='Please Enter Office Address !'></i>"
                                    ControlToValidate="txtOffice_Address" Display="Dynamic" runat="server">
                                </asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOffice_Address" MaxLength="500" placeholder="Enter Office Address"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Pincode<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="vgao"
                                    ErrorMessage="Please Enter Office Pincode" Text="<i class='fa fa-exclamation-circle' title='Please Enter Office Pincode !'></i>"
                                    ControlToValidate="txtOfficePincode" Display="Dynamic" runat="server">
                                </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="vgao" Display="Dynamic" runat="server" ControlToValidate="txtOfficePincode" ErrorMessage="Invalid Pincode" Text="<i class='fa fa-exclamation-circle' title='Invalid Pincode !'></i>" SetFocusOnError="true" ValidationExpression="^[1-9]{1}[0-9]{5}$"></asp:RegularExpressionValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOfficePincode" MaxLength="6" placeholder="Enter Office Pincode" onkeypress="return validateNum(event);"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>GST Number<span style="color: red;"> *</span></label>
                                 <span class="pull-right">
                                   <asp:RequiredFieldValidator ID="rfv7" ValidationGroup="vgao"
                                    ErrorMessage="Please Enter GST Number" Text="<i class='fa fa-exclamation-circle' title='Please Enter GST Number !'></i>"
                                    ControlToValidate="txtGstNumber" Display="Dynamic" runat="server">
                                   </asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ReadOnly="true" autocomplete="off" Text="23AACCM0330Q1ZM" runat="server" CssClass="form-control CapitalClass" ID="txtGstNumber" MaxLength="20" placeholder="Enter GST Number"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>PAN Number<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="vgao"
                                    ErrorMessage="Please Enter PAN Number" Text="<i class='fa fa-exclamation-circle' title='Please Enter PAN Number !'></i>"
                                    ControlToValidate="txtPanNumber" Display="Dynamic" runat="server">
                                   </asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox ReadOnly="true" autocomplete="off" Text="AACCM0330Q" runat="server" CssClass="form-control CapitalClass PanCard" ID="txtPanNumber" MaxLength="10" placeholder="Enter PAN Number"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>TAN Number<%--<span style="color: red;"> *</span>--%></label>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control CapitalClass TanNo" ID="txtTanNumber" ClientIDMode="Static" MaxLength="10" placeholder="Enter TAN Number"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="vgao"
                                    ErrorMessage="Please Enter Office Name" Text="<i class='fa fa-exclamation-circle' title='Please Enter Office Name !'></i>"
                                    ControlToValidate="txtOffice_Name" Display="Dynamic" runat="server">
                                   </asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOffice_Name" MaxLength="150" placeholder="Enter Office Name" ClientIDMode="Static" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                     <div class="row">
                        <div class="col-md-3">
                             <div class="form-group">
                                <label>Office Code<span style="color: red;"> *</span></label>
                                <span class="pull-right">
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="vgao"
                                    ErrorMessage="Please Enter Office Code" Text="<i class='fa fa-exclamation-circle' title='Please Enter Office Code !'></i>"
                                    ControlToValidate="txtOffice_Code" Display="Dynamic" runat="server">
                                   </asp:RequiredFieldValidator>
                                </span>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOffice_Code" MaxLength="150" placeholder="Enter Office Code" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            </div></div>
                    
                    <div class="row">
                        <hr />
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-success"  ValidationGroup="vgao" ID="btnSave" Text="Save" OnClientClick="return ValidatePage();" AccessKey="S"  />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnClear" OnClick="btnClear_Click" runat="server" Text="Clear" CssClass="btn btn-block btn-default" />
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="Office_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                        <Columns>
                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Office_ID").ToString()%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Office_Name" HeaderText="Office Name" />
                            <asp:BoundField DataField="Office_Code" HeaderText="Office Code" />
                            <asp:BoundField DataField="Office_ContactNo" HeaderText="Office Contact Number" />
                            <asp:BoundField DataField="Office_Email" HeaderText="Office Email" />
                            <asp:BoundField DataField="OfficeType_Title" HeaderText="Office Title" />
                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Select" runat="server" CausesValidation="False" ForeColor="#ff9900" CommandName="Select"><i class='fa fa-pencil'></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="30" HeaderText="Status">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" OnCheckedChanged="chkSelect_CheckedChanged" runat="server" ToolTip='<%# Eval("Office_ID").ToString()%>' Checked='<%# Eval("Office_IsActive").ToString()=="1" ? true : false %>' AutoPostBack="true" />
                                </ItemTemplate>
                                <ItemStyle Width="30px"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </section>
    </div>
<%-- </ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        //===========TAN NUMBER VALIDATION START ====================
        $('.TanNo').blur(function () {
           
            var reg = /^([a-zA-Z]{4})(\d{5})([a-zA-Z]{1})$/;
            if (document.getElementById('txtTanNumber').value != "")
            {
                if (reg.test(document.getElementById('txtTanNumber').value) == false) {
                    alert("Invalid Tan Number.");
                    document.getElementById('txtTanNumber').value = "";
                }
            }
            
        });

        //===========TAN NUMBER VALIDATION END====================


        //window.onload = $('#txtOffice_Name').val(""); ChangeMantField(); fillDistrictOffice(); fillRegionOffice();
        //function validateNum(evt) {
        //    evt = (evt) ? evt : window.event;
        //    var charCode = (evt.which) ? evt.which : evt.keyCode;
        //    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        //        return false;
        //    }
        //    return true;
        //}

        <%--function ChangeMantField() {
            debugger
            var offType = ddlOfficeType_Title.options[ddlOfficeType_Title.selectedIndex].text;
            if (document.getElementById('<%=ddlOfficeType_Title.ClientID%>').selectedIndex == 0) {
                spanRegion.hidden = true;
                spanDistrict.hidden = true;
                offType = "";
            }
            if (document.getElementById('<%=ddlOfficeType_Title.ClientID%>').selectedIndex == 1) {
                spanRegion.hidden = true;
                spanDistrict.hidden = true;
                $('#txtOffice_Name').val(offType);
            }
            else if (document.getElementById('<%=ddlOfficeType_Title.ClientID%>').selectedIndex == 2) {
                spanRegion.hidden = false;
                spanDistrict.hidden = true;
                $('#txtOffice_Name').val(offType);
            }
            else if (document.getElementById('<%=ddlOfficeType_Title.ClientID%>').selectedIndex == 3) {
                spanRegion.hidden = false;
                spanDistrict.hidden = false;
                $('#txtOffice_Name').val(offType);
            }
            else {
                spanRegion.hidden = false;
                spanDistrict.hidden = false;
                $('#txtOffice_Name').val(offType);
            }
        }--%>

        <%--function fillDistrictOffice() {
            debugger
            var offType = ddlOfficeType_Title.options[ddlOfficeType_Title.selectedIndex].text;
            if (document.getElementById('<%=ddlOfficeType_Title.ClientID%>').selectedIndex == 3) {
                var district = ddlDistrict_Name.options[ddlDistrict_Name.selectedIndex].text;
                if (document.getElementById('<%=ddlDistrict_Name.ClientID%>').selectedIndex == 0) {
                    $('#txtOffice_Name').val(offType);
                }
                else {
                    $('#txtOffice_Name').val(district + ' - ' + offType);
                }
            }
        }

        function fillRegionOffice() {
            debugger
            var offType = ddlOfficeType_Title.options[ddlOfficeType_Title.selectedIndex].text;
            if (document.getElementById('<%=ddlOfficeType_Title.ClientID%>').selectedIndex == 2) {
                var region = ddlDivision_Name.options[ddlDivision_Name.selectedIndex].text;
                if (document.getElementById('<%=ddlDivision_Name.ClientID%>').selectedIndex == 0) {
                    $('#txtOffice_Name').val(offType);
                }
                else {
                    $('#txtOffice_Name').val(region + ' - ' + offType);
                }
            }
        }--%>

        <%--function validateform() {
            var msg = "";
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
            var officetype = document.getElementById('<%=ddlOfficeType_Title.ClientID%>').selectedIndex;
            if (officetype == 0) {
                msg = msg + "Select Office Title. \n";
            }
            else if (officetype == 1) {

            }
            else if (officetype == 2) {
                if (document.getElementById('<%=ddlDivision_Name.ClientID%>').selectedIndex == 0) {
                    msg = msg + "Select Region Name. \n";
                }
                else { }
            }
            else if (officetype >= 3) {
                if (document.getElementById('<%=ddlDivision_Name.ClientID%>').selectedIndex == 0) {
                    msg = msg + "Select Region Name. \n";
                }
                if (document.getElementById('<%=ddlDistrict_Name.ClientID%>').selectedIndex == 0) {
                    msg = msg + "Select District Name. \n";
                }
            }
            if (document.getElementById('<%=txtOffice_Name.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Office Name. \n";
            }
            if (document.getElementById('<%=txtOffice_ContactNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Office Contact Number. \n";
            }
            else {
                if (document.getElementById('<%=txtOffice_ContactNo.ClientID%>').value.length != 10) {
                    msg = msg + "Enter Correct Contact Number. \n";
                }
            }
            if (document.getElementById('<%=txtOffice_Email.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Office Email Address. \n";
            }
            else {
                if (reg.test(document.getElementById('<%=txtOffice_Email.ClientID%>').value) == false) {
                    msg = msg + "Please enter valid email address. \n";
                }
            }
            
          if (document.getElementById('<%=ddlBlock_Name.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Block Name. \n";
            }
            if (document.getElementById('<%=txtOffice_Address.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Office Address. \n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Edit") {
                    if (confirm("Do you really want to Edit Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }--%>

        //$('.PanCard').blur(function () {
        //    var Obj = $('.PanCard').val();
        //    if (Obj == null) Obj = window.event.srcElement;
        //    if (Obj != "") {
        //        ObjVal = Obj;
        //        var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
        //        var code = /([C,P,H,F,A,T,B,L,J,G])/;
        //        var code_chk = ObjVal.substring(3, 4);
        //        if (ObjVal.search(panPat) == -1) {
        //            alert("Invalid Pan No");
        //            //message_error("Error", "Invalid Pan Card.");
        //            //Obj.focus();
        //            $('.PanCard').val('');
        //            return false;
        //        }
        //        if (code.test(code_chk) == false) {
        //            alert("Invaild PAN Card No.");
        //            //message_error("Error", "Invalid Pan Card.");
        //            $('.PanCard').val('');
        //            return false;
        //        }
        //    }
        //});
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
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
        $('.MobileNo').blur(function () {
            debugger;
            var Obj = $('.MobileNo').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                var MobileNo = /^[6-9]{1}[0-9]{9}$/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(MobileNo) == -1) {
                    alert("Invalid Mobile No.");
                    //message_error("Error", "Invalid IFSC Code.");
                    //Obj.focus();
                    $('.MobileNo').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild Mobile No.");
                    //message_error("Error", "Invalid IFSC Code.");
                    $('.MobileNo').val('');
                    return false;
                }
            }
        });
    </script>
</asp:Content>

