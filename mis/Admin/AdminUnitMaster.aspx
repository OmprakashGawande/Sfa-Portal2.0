<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminUnitMaster.aspx.cs" Inherits="mis_Admin_AdminUnit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <%--<link rel="stylesheet" href="http://cdn.datatables.net/1.10.2/css/jquery.dataTables.min.css" />--%>
    <style>
         .capitalize
        {
            text-transform: capitalize;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Unit Master</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Unit<span class="text-danger"> *</span></label>
                                <asp:TextBox ID="txtUnit"  runat="server" placeholder="Enter Unit..." class="form-control capitalize" MaxLength="20" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                <small><span id="valtxtUnit" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Quantity Type<span class="text-danger"> *</span></label>
                                <asp:DropDownList runat="server" ID="ddlQuantity_Type" CssClass="form-control" AutoPostBack="false">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>Measure</asp:ListItem>
                                    <asp:ListItem>Volume</asp:ListItem>
                                    <asp:ListItem>Weight</asp:ListItem>
                                    <asp:ListItem>Length</asp:ListItem>
                                    <asp:ListItem>Area</asp:ListItem>
                                </asp:DropDownList>
                                <small><span id="valddlQuantity_Type" class="text-danger"></span></small>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>UQC Code<span class="text-danger"> *</span></label>
                                <asp:TextBox ID="txtUqc_Code" runat="server" placeholder="Enter QUC Code..." class="form-control" MaxLength="50" ></asp:TextBox>
                                <small><span id="valtxtUqc_Code" class="text-danger"></span></small>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Number of Decimal Places  (0 - 4)<span class="text-danger"> *</span></label>
                                <asp:TextBox ID="txtNoOfDecimal" runat="server" placeholder="Enter Number of Decimal Places..." class="form-control" MaxLength="1" onkeypress="return validateNum(event);"></asp:TextBox>
                                <small><span id="valtxtNoOfDecimal" class="text-danger"></span></small>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" runat="server" Text="Accept" OnClientClick="return validateform();" OnClick="btnSave_Click" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" href="AdminUnitMaster.aspx">Clear</a>
                            </div>
                        </div>
                        <div class="col-md-8"></div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered" ClientIDMode="Static" AutoGenerateColumns="False" DataKeyNames="Unit_id" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>

<ItemStyle Width="5%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblunitname" runat="server" Text='<%# Bind("UnitName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblid" CssClass="hidden" runat="server" Text='<%# Bind("Unit_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="QuantityType" HeaderText="Quantity Type" />
                                    <asp:BoundField DataField="UQCCode" HeaderText="UQC Code" />
                                    <asp:BoundField DataField="NoOfDecimalPlace" HeaderText="Number of Decimal Places" />
                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Select" runat="server" CssClass="label Aselect1 label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                            <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return getConfirmation();"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

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
            paging: true,
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
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1,2, 3, 4]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1,2, 3, 4]
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
        function validateform() {

            var msg = "";
            $("#valtxtUnit").html("");
            $("#valddlQuantity_Type").html("");
            $("#valtxtUqc_Code").html("");
            $("#valtxtNoOfDecimal").html("");
            var NoOfDecimal = document.getElementById('<%=txtNoOfDecimal.ClientID%>').value.trim();

            if (document.getElementById('<%=txtUnit.ClientID%>').value.trim() == "") {
                msg += "Enter Unit. \n"
                $("#valtxtUnit").html("Enter Unit");
            }
            if (document.getElementById('<%=ddlQuantity_Type.ClientID%>').selectedIndex == 0) {
                msg += "Select Quantity Type. \n"
                $("#valddlQuantity_Type").html("Select Quantity Type");
            }

            if (document.getElementById('<%=txtUqc_Code.ClientID%>').value.trim() == "") {
                msg += "Enter UQC Code. \n"
                $("#valtxtUqc_Code").html("Enter UQC Code");
            }

            if (document.getElementById('<%=txtNoOfDecimal.ClientID%>').value.trim() == "") {
                msg += "Enter No Of Decimal Places. \n"
                $("#valtxtNoOfDecimal").html("Enter No Of Decimal Places");
            }
              
            else if (parseInt(NoOfDecimal) > 4 || parseInt(NoOfDecimal) < 0)
            {
                msg += "Enter No Of Decimal Places (0 - 4). \n"
                $("#valtxtNoOfDecimal").html("Enter No Of Decimal Places  (0 - 4)");
            }
            if (msg != "") {
                alert(msg)
                return false

            }
            else {
                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true
               
            }
        }
        function tbx_fnNumeric(e, ctrl) {
            if (!e) e = window.event;
            if (e.charCode) {
                if (e.charCode < 48 || e.charCode > 57) {
                    if (e.charCode != 46 || ctrl.value.indexOf('.') >= 0) {
                        if (e.preventDefault) { e.preventDefault(); }
                    }
                }
            }
            else if (e.keyCode) {
                if (e.keyCode < 48 || e.keyCode > 57) {
                    if (e.keyCode != 46 || ctrl.value.indexOf('.') >= 0) {
                        try {
                            e.keyCode = 0;
                        }
                        catch (e)
                        { }
                    }
                }
            }
        }
        function getConfirmation() {
            debugger;
            var retVal = confirm("Record will be deleted. Are you sure want to continue?");
            if (retVal == true) {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }
            else {

                return false;
            }
        }
        function tbx_fnAlphaOnly(e, cntrl) { if (!e) e = window.event; if (e.charCode) { if (e.charCode < 65 || (e.charCode > 90 && e.charCode < 97) || e.charCode > 122) { if (e.charCode != 95 && e.charCode != 32) { if (e.preventDefault) { e.preventDefault(); } } } } else if (e.keyCode) { if (e.keyCode < 65 || (e.keyCode > 90 && e.keyCode < 97) || e.keyCode > 122) { if (e.keyCode != 95 && e.keyCode != 32) { try { e.keyCode = 0; } catch (e) { } } } } }
    </script>
    <script src="../js/ValidationJs.js"></script>
</asp:Content>

