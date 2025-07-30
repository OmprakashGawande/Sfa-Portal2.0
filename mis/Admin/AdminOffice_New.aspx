<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminOffice_New.aspx.cs" Inherits="mis_Admin_AdminOffice_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
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
                                <asp:DropDownList ID="ddlOfficeType_Title" runat="server" CssClass="form-control select2" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlOfficeType_Title_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" id="spanRegion" runat="server" visible="false">
                            <div class="form-group">
                                <label>Region Name<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlDivision_Name" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlDivision_Name_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" id="spanDistrict" runat="server" visible="false">
                            <div class="form-group">
                                <label>District Name<span style="color: red;"> *</span></label>
                                <asp:DropDownList ID="ddlDistrict_Name" runat="server" CssClass="form-control select2" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlDistrict_Name_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Contact Number<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control  MobileNo" ID="txtOffice_ContactNo" MaxLength="10" onkeypress="return validateNum(event);" placeholder="Enter Contact Number"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Email<span style="color: red;"> *</span></label>
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
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOffice_Address" MaxLength="500" placeholder="Enter Office Address"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Pincode<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOfficePincode" MaxLength="6" placeholder="Enter Office Pincode" onkeypress="return validateNum(event);"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>GST Number<span style="color: red;"> *</span></label>
                                <asp:TextBox ReadOnly="true" autocomplete="off" Text="23AACCM0330Q1ZM" runat="server" CssClass="form-control CapitalClass" ID="txtGstNumber" MaxLength="20" placeholder="Enter GST Number"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>PAN Number<span style="color: red;"> *</span></label>
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
                                <label>Office Name (In English)<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOffice_Name" MaxLength="150" placeholder="Enter Office Name" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                          <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name (In Hindi)<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOffice_NameHindi" MaxLength="150" placeholder="Enter Office Name" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Code<span style="color: red;"> *</span></label>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" ID="txtOffice_Code" MaxLength="150" placeholder="Enter Office Code" ClientIDMode="Static"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <hr />
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" OnClientClick="return validateform();" OnClick="btnSave_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a href="AdminOffice_New.aspx" class="btn btn-default btn-block">Clear</a>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GridView1" runat="server" class=" table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="Office_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Office_ID").ToString()%>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField DataField="Office_Name" HeaderText="Office Name (In English) " />
                                             <asp:BoundField DataField="Office_Name_Hin" HeaderText="Office Name Office Name (In Hindi)  " />
                                        <asp:BoundField DataField="Office_Code" HeaderText="Office Code" />
                                        <asp:BoundField DataField="Office_ContactNo" HeaderText="Office Contact Number" />
                                        <asp:BoundField DataField="Office_Email" HeaderText="Office Email" />
                                        <asp:BoundField DataField="OfficeType_Title" HeaderText="Office Title" />
                                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select">Edit</asp:LinkButton>
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
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function validateform() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=ddlOfficeType_Title.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office Title. \n";
            }
           <%-- if (document.getElementById('<%=ddlDivision_Name.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Division Name. \n";
            }
            if (document.getElementById('<%=ddlDistrict_Name.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select District Name. \n";
            }--%>
            if (document.getElementById('<%=txtOffice_ContactNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Contact Number. \n";
            }
            if (document.getElementById('<%=txtOffice_Email.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Office Email. \n";
            }
            else {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-])+\.([A-Za-z]{2,4})$/;
                if (reg.test(document.getElementById('<%=txtOffice_Email.ClientID%>').value) == false) {
                    msg = msg + "Please Enter Valid Office Email. \n";
                }
            }
            if (document.getElementById('<%=txtOffice_Address.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Office Address. \n";
            }
            if (document.getElementById('<%=txtOfficePincode.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Office Pincode. \n";
            }
            else if (document.getElementById('<%=txtOfficePincode.ClientID%>').value.trim().length < 6) {
                msg = msg + "Enter Valid Office Pincode. \n";
            }
            if (document.getElementById('<%=txtGstNumber.ClientID%>').value.trim() == "") {
                msg = msg + "Enter GST Number. \n";
            }
            if (document.getElementById('<%=txtPanNumber.ClientID%>').value.trim() == "") {
                msg = msg + "Enter PAN Number. \n";
            }
           <%-- if (document.getElementById('<%=txtTanNumber.ClientID%>').value.trim() == "") {
                msg = msg + "Enter TAN Number. \n";
                $("#valtxtTanNumber").html("Enter TAN Number.");
            }--%>
            if (document.getElementById('<%=txtOffice_Name.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Office Name English. \n";
            }
            if (document.getElementById('<%=txtOffice_NameHindi.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Office Name Hindi. \n";
            }
            if (document.getElementById('<%=txtOffice_Code.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Office Code. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Detail?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    if (confirm("Do you really want to Update Detail?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }
        }
        $('.TanNo').blur(function () {

            var reg = /^([a-zA-Z]{4})(\d{5})([a-zA-Z]{1})$/;
            if (document.getElementById('txtTanNumber').value != "") {
                if (reg.test(document.getElementById('txtTanNumber').value) == false) {
                    alert("Invalid Tan Number.");
                    document.getElementById('txtTanNumber').value = "";
                }
            }

        });
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
    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: false,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
    </script>
</asp:Content>

