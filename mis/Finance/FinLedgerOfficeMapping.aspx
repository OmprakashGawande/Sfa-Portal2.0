<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinLedgerOfficeMapping.aspx.cs" Inherits="mis_Finance_FinLedgerOfficeMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .customCSS td {
            padding: 0px !important;
        }




        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-header">
                            <h3 class="box-title">Ledger Office Mapping</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                 <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Ledger Name <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlLedgerName" class="form-control select2" runat="server" ClientIDMode="Static" OnSelectedIndexChanged="ddlLedgerName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            <asp:Panel ID="panel1" Visible="false" runat="server">                            
                            <div class="row hidden" >
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Mapped Office</legend>
                                        <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:CheckBoxList ID="chkboxmappedofc" runat="server"  ClientIDMode="Static" CssClass="table customCSS " RepeatColumns="5" RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                                    </fieldset>
                                </div>
                            </div>
                            <div class="row">

                        <div class="col-md-12">

                            <fieldset>
                                <legend>Applicable on<span class="text-danger">*</span></legend>
                                <div class="row">

                                    <div class="col-md-2">
                                        <asp:CheckBox ID="chkOfficeAll" runat="server" Text="ALL" onclick="CheckOfficeAll();" OnCheckedChanged="chkOfficeAll_CheckedChanged" AutoPostBack="true"/>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:CheckBox ID="chkHeadOffice" runat="server" Text="Head Office" OnCheckedChanged="chkHeadOffice_CheckedChanged" AutoPostBack="true"/>
                                    </div>
                                    <div class="col-md-2">
                                        
                                    </div>
                                    <div class="col-md-2">
                                       
                                    </div>
                                    <div class="col-md-2">
                                        
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <legend><asp:CheckBox ID="chkDistrict" runat="server" Text="ALL District Office" onclick="CheckOfficeAllDistrict();" OnCheckedChanged="chkDistrict_CheckedChanged" AutoPostBack="true"/></legend>
                                        <div class="table-responsive">
                                            <asp:CheckBoxList ID="chkOffice" runat="server" ClientIDMode="Static" CssClass="table District customCSS cbl_all_Office" RepeatColumns="3" RepeatDirection="Horizontal" OnSelectedIndexChanged="chkOffice_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <legend> <asp:CheckBox ID="chkProductionUnit" runat="server" Text="ALL Production Unit" onclick="CheckOfficeAllProduction();" OnCheckedChanged="chkProductionUnit_CheckedChanged" AutoPostBack="true"/></legend>
                                        <div class="table-responsive">
                                            <asp:CheckBoxList ID="chkAllProductionUnit" runat="server" ClientIDMode="Static" CssClass="table Production customCSS cbl_all_Office" RepeatColumns="1" RepeatDirection="Horizontal" OnSelectedIndexChanged="chkAllProductionUnit_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:CheckBoxList>
                                        </div>

                                        <br />
                                     <%--   <legend><asp:CheckBox ID="chkAllOtherOffice" runat="server" Text="ALL Other Office" onclick="CheckOfficeAllOther();" OnCheckedChanged="chkAllOtherOffice_CheckedChanged" AutoPostBack="true"/></legend>
                                        <div class="table-responsive">
                                            <asp:CheckBoxList ID="chkOtherOffice" runat="server" ClientIDMode="Static" CssClass="table Other customCSS cbl_all_Office" RepeatColumns="1" RepeatDirection="Horizontal" OnSelectedIndexChanged="chkOtherOffice_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:CheckBoxList>
                                        </div>--%>
                                    </div>
                                </div>
                                <small><span id="valchkOffice" class="text-danger"></span></small>
                            </fieldset>

                        </div>

                    </div>
                            <%--<div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:CheckBoxList ID="chkOffice" runat="server" ClientIDMode="Static" CssClass="table customCSS cbl_all_Office" RepeatColumns="5" RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" OnClientClick="return validateform();" CssClass="btn btn-success btn-block" OnClick="btnSave_Click" />
                                    
                                </div>
                                 <div class="col-md-2">
                                    <asp:Button ID="btnDel" runat="server" CssClass="btn btn-danger btn-block" ClientIDMode="Static" Text="Delete"  OnClick="btnDel_Click"/>
                                </div>
                                
                            </div>
                                </asp:Panel> 
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function CheckOfficeAll() {
            if (document.getElementById('<%=chkOfficeAll.ClientID%>').checked == true) {
                 document.getElementById('<%=chkHeadOffice.ClientID%>').checked = true;
                document.getElementById('<%=chkDistrict.ClientID%>').checked = true;
                document.getElementById('<%=chkProductionUnit.ClientID%>').checked = true;
              

                $('.cbl_all_Office').each(function () {

                    $(this).closest('table').find('input[type=checkbox]').prop('checked', true);
                });
            }
            else {
                document.getElementById('<%=chkHeadOffice.ClientID%>').checked = false;
                document.getElementById('<%=chkDistrict.ClientID%>').checked = false;
                document.getElementById('<%=chkProductionUnit.ClientID%>').checked = false;
              

                $('.cbl_all_Office').each(function () {
                    debugger
                    //$(this).closest('table').find('input[type=checkbox]').prop('checked', false);
                    $(this).closest('table').find('input[type=checkbox]').not(":disabled").prop('checked', false);
                });
            }
            return false;
        }
        function CheckOfficeAllDistrict() {
            if (document.getElementById('<%=chkDistrict.ClientID%>').checked == true) {
                    $('.District').each(function () {

                        $(this).closest('table').find('input[type=checkbox]').prop('checked', true);
                    });
                }
                else {
                    var chkOfficeAll = document.getElementById('<%=chkOfficeAll.ClientID%>');
                    if (chkOfficeAll.checked == true) {
                        chkOfficeAll.checked = false;
                    }
                    $('.District').each(function () {
                        //$(this).closest('table').find('input[type=checkbox]').prop('checked', false);
                        $(this).closest('table').find('input[type=checkbox]').not(":disabled").prop('checked', false);
                    });
                }
                return false;
            }
            function CheckOfficeAllProduction() {
                if (document.getElementById('<%=chkProductionUnit.ClientID%>').checked == true) {
                $('.Production').each(function () {
                    debugger

                    $(this).closest('table').find('input[type=checkbox]').prop('checked', true);
                });
            }
            else {
                var chkOfficeAll = document.getElementById('<%=chkOfficeAll.ClientID%>');
                if (chkOfficeAll.checked == true) {
                    chkOfficeAll.checked = false;
                }

                $('.Production').each(function () {
                    debugger;

                    //$(this).closest('table').find('input[type=checkbox]').prop('checked', false);
                    $(this).closest('table').find('input[type=checkbox]').not(":disabled").prop('checked', false);


                });
            }
            return false;
        }
        <%--function CheckOfficeAllOther() {
            if (document.getElementById('<%=chkAllOtherOffice.ClientID%>').checked == true) {
                $('.Other').each(function () {

                    $(this).closest('table').find('input[type=checkbox]').prop('checked', true);
                });
            }
            else {
                var chkOfficeAll = document.getElementById('<%=chkOfficeAll.ClientID%>');
                if (chkOfficeAll.checked == true) {
                    chkOfficeAll.checked = false;
                }
                $('.Other').each(function () {
                    // $(this).closest('table').find('input[type=checkbox]').prop('checked', false);
                    $(this).closest('table').find('input[type=checkbox]').not(":disabled").prop('checked', false);
                });
            }
            return false;
        }--%>
        function CheckUncheckOfficeAllProduction() {

            //Determine the reference CheckBox in Header row.
            var chkAll = document.getElementById('<%=chkOfficeAll.ClientID%>');
            var chkProductionUnit = document.getElementById('<%=chkProductionUnit.ClientID%>');


            chkAll.checked = true;
            chkProductionUnit.checked = true;

            var sList = "";
            $('.Production').each(function () {
                debugger
                sList += "(" + $(this).val() + (this.checked ? "checked" : "not checked") + ")";
                if (sList == "(not checked)") {
                    chkAll.checked = false;
                    chkProductionUnit.checked = false;

                }

            });
            //Execute loop on all rows excluding the Header row.

        };
        function CheckUncheckOfficeAllDistrict() {

            //Determine the reference CheckBox in Header row.
            var chkAll = document.getElementById('<%=chkOfficeAll.ClientID%>');
                var chkDistrict = document.getElementById('<%=chkDistrict.ClientID%>');


                chkAll.checked = true;
                chkDistrict.checked = true;

                var sList = "";
                $('.District').each(function () {
                    debugger
                    sList += "(" + $(this).val() + (this.checked ? "checked" : "not checked") + ")";
                    if (sList == "(not checked)") {
                        chkAll.checked = false;
                        chkDistrict.checked = false;

                    }

                });



                //Execute loop on all rows excluding the Header row.

            };
            <%--function CheckuncheckOfficeAllOther() {

                //Determine the reference CheckBox in Header row.
                var chkAll = document.getElementById('<%=chkOfficeAll.ClientID%>');
                var chkAllOtherOffice = document.getElementById('<%=chkAllOtherOffice.ClientID%>');


                chkAll.checked = true;
                chkAllOtherOffice.checked = true;

                var sList = "";
                $('.Other').each(function () {
                    debugger
                    sList += "(" + $(this).val() + (this.checked ? "checked" : "not checked") + ")";
                    if (sList == "(not checked)") {
                        chkAll.checked = false;
                        chkAllOtherOffice.checked = false;

                    }

                });
                //Execute loop on all rows excluding the Header row.

            };--%>
        function validateform() {
            debugger;
            var msg = "";
            
            if ($('#chkOffice input:checked').length == 0 && $('#chkAllProductionUnit input:checked').length == 0 && $('#chkOtherOffice input:checked').length == 0 && document.getElementById('<%=chkHeadOffice.ClientID%>').checked == false) {
                msg += "Select atleast one Office. \n"
                $("#valchkOffice").html("Select atleast one Office");
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
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    if (confirm("Do you really want to Update Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }

                }
            }
        }
    </script>
</asp:Content>

