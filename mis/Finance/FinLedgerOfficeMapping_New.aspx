<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinLedgerOfficeMapping_New.aspx.cs" Inherits="mis_Finance_FinLedgerOfficeMapping_New" %>

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
                        <asp:label id="lblMsg" runat="server" text=""></asp:label>
                        <div class="box-header">
                            <h3 class="box-title">Ledger Office Mapping</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Office Name</label><span style="color: red">*</span>
                                        <asp:dropdownlist runat="server" id="ddlOffice" cssclass="form-control select2" onselectedindexchanged="ddlOffice_SelectedIndexChanged" autopostback="true">
                                </asp:dropdownlist>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Ledger Name <span style="color: red;">*</span></label>
                                        <asp:dropdownlist id="ddlLedgerName" class="form-control select2" runat="server" clientidmode="Static"></asp:dropdownlist>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Office Type<span style="color: red;">*</span></label>
                                        <asp:dropdownlist id="ddlOfficeType" class="form-control select2" runat="server" clientidmode="Static"></asp:dropdownlist>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button id="btnSearch" runat="server" cssclass="btn btn-success" text="Search" OnClick="btnSearch_Click"/>
                                    </div>
                                </div>
                                <asp:panel id="panel1" visible="false" runat="server">                            
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
                                        <asp:CheckBox ID="chkOfficeAll" runat="server" Text="ALL" onclick="CheckOfficeAll();" OnCheckedChanged="chkOfficeAll_CheckedChanged" AutoPostBack="true" />
                                    </div>
                                   
                                  
                                </div>
                                 <div class="row">
                                     <div class="col-md-12">
                                      
                                        <div class="table-responsive">
                                             <asp:CheckBoxList ID="chkOffice" runat="server" ClientIDMode="Static" CssClass="table District customCSS cbl_all_Office" RepeatColumns="6" RepeatDirection="Horizontal" OnSelectedIndexChanged="chkOffice_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                 </div>
                            </fieldset>

                        </div>

                    </div>
                            
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Button ID="btnSave" runat="server" ClientIDMode="Static" Text="Save" OnClientClick="return validateform();" CssClass="btn btn-success btn-block" OnClick="btnSave_Click" />
                                    
                                </div>
                                 <div class="col-md-2">
                                    <asp:Button ID="btnDel" runat="server" CssClass="btn btn-danger btn-block" ClientIDMode="Static" Text="Delete"  OnClick="btnDel_Click"/>
                                </div>
                                
                            </div>
                                </asp:panel>
                            </div>
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
                document.getElementById('<%=chkOffice.ClientID%>').checked = true;



                $('.cbl_all_Office').each(function () {

                    $(this).closest('table').find('input[type=checkbox]').prop('checked', true);
                });
            }
            else {
                document.getElementById('<%=chkOffice.ClientID%>').checked = false;


                $('.cbl_all_Office').each(function () {
                    debugger
                    //$(this).closest('table').find('input[type=checkbox]').prop('checked', false);
                    $(this).closest('table').find('input[type=checkbox]').not(":disabled").prop('checked', false);
                });
            }
            return false;
        }




        function validateform() {
            debugger;
            var msg = "";

            if ($('#chkOffice input:checked').length == 0) {
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

