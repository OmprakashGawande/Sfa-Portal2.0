<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ChequeBookDetail.aspx.cs" Inherits="mis_Finance_ChequeBookDetail" %>

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
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Cheque Book Detail</h3>
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Financial Year <span style="color: red;">*</span></label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlFinancialYear" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlFinancialYear" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Select Ledger<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlLedger" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>
                                        <small><span id="valddlLedger" style="color: red;"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Cheque Details</legend>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cheque No. From<span style="color: red;">*</span></label>
                                                <asp:TextBox ID="txtChqFrom" runat="server" CssClass="form-control" placeholder="Cheque No. From" MaxLength="6" onkeypress="return validateNum(event)" ></asp:TextBox>
                                                <small><span id="valtxtChqFrom" style="color: red;"></span></small>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cheque No. To<span style="color: red;">*</span></label>
                                                <asp:TextBox ID="txtChqTo" runat="server" CssClass="form-control" placeholder="Cheque No.To" MaxLength="6" onkeypress="return validateNum(event)"></asp:TextBox>
                                                <small><span id="valtxtChqTo" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>No Of Cheques<span style="color: red;">*</span></label>
                                                <asp:TextBox ID="txtNoOfChq" runat="server" CssClass="form-control" placeholder="No Of Cheques" MaxLength="3" onkeypress="return validateNum(event)"></asp:TextBox>
                                                <small><span id="valtxtNoOfChq" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cheque Book Name<span style="color: red;">*</span></label>
                                                <asp:TextBox ID="txtChqBkName" runat="server" CssClass="form-control" placeholder="Cheque Book Name" MaxLength="50" onkeypress="return validatename(event)"></asp:TextBox>
                                                <small><span id="valtxtChqBkName" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-block btn-success" ClientIDMode="Static" Text="Save" OnClientClick="return validateform();" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2" id="clear" runat="server">
                                    <div class="form-group">
                                        <a href="ChequeBookDetail.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                         <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="Gvchqdetails"  runat="server" DataKeyNames="ChequeBook_ID" class="datatable table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" OnSelectedIndexChanged="Gvchqdetails_SelectedIndexChanged" OnRowDeleting="Gvchqdetails_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ChequeBook_ID").ToString()%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Financial_Year" HeaderText="Financial Year" />
                                                <asp:BoundField DataField="Ledger_Name" HeaderText="Ledger Name" />
                                                <asp:BoundField DataField="ChequeNoFrom" HeaderText="Cheque No From" />
                                                <asp:BoundField DataField="ChequeNoTo" HeaderText="Cheque No To" />
                                                <asp:BoundField DataField="NoOfCheques" HeaderText="No Of Cheques" />
                                                <asp:BoundField DataField="ChequeBookName" HeaderText="ChequeBook Name" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="label label-default" CommandName="Select" CausesValidation="false">Edit</asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnDel" runat="server" CssClass="label label-danger" CommandName="Delete" CausesValidation="false" OnClientClick="return confirm('The Record will be deleted. Are you sure want to continue?');">Delete</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
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
                        columns: ':not(.no-print)'
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: ':not(.no-print)'
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
            $("#valddlFinancialYear").html("");
            $("#valddlLedger").html("");
            $("#valtxtChqFrom").html("");
            $("#valtxtChqTo").html("");
            $("#valtxtNoOfChq").html("");
            $("#valtxtChqBkName").html("");
            if (document.getElementById('<%=ddlFinancialYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Financial Year. \n"
                $("#valddlFinancialYear").html("Select Financial Year");
            }
            if (document.getElementById('<%=ddlLedger.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Ledger. \n"
                $("#valddlLedger").html("Select Ledger");
            }
            if (document.getElementById('<%=txtChqFrom.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Cheque No. From. \n"
                $("#valtxtChqFrom").html("Enter Cheque No. From");
            }
            if (document.getElementById('<%=txtChqTo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Cheque No. To. \n"
                $("#valtxtChqTo").html("Enter Cheque No. To");
            }
            if (document.getElementById('<%=txtNoOfChq.ClientID%>').value.trim() == "") {
                msg = msg + "Enter No Of Cheques. \n"
                $("#valtxtNoOfChq").html("Enter No Of Cheques");
            }
            if (document.getElementById('<%=txtChqBkName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Cheque Book Name. \n"
                $("#valtxtChqBkName").html("Enter Cheque Book Name");
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
                else if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    if (confirm("Do you really want to Edit Details ?")) {
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

