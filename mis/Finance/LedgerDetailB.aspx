<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="LedgerDetailB.aspx.cs" Inherits="mis_Finance_LedgerDetailB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        div.dataTables_wrapper div.dataTables_length select {
            margin-top: 3px !important;
        }

        input[type="text"]:focus {
            background-color: #3c8dbc7a;
            outline: 2px solid #00a1ff;
        }

        ul.ui-autocomplete.ui-menu.ui-widget.ui-widget-content.ui-corner-all {
            height: 197px !important;
            overflow-y: scroll !important;
            width: 520px !important;
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
                            <h3 class="box-title">Your Office Ledgers (All)</h3>
                            <asp:HyperLink ID="lnkotherledger" runat="server" NavigateUrl="LedgerDetailBOther.aspx" Target="_blank" CssClass="btn btn-primary pull-right">Other Office Ledgers (Only Sundry Creditors)</asp:HyperLink>
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:DropDownList ID="ddlHOOfficeLedger" runat="server" CssClass="form-control select1" OnSelectedIndexChanged="ddlHOOfficeLedger_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="Mapped">Mapped</asp:ListItem>
                                            <asp:ListItem Value="UnMapped">UnMapped</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Search With</label>
                                        <asp:DropDownList ID="ddlSearchWith" runat="server" CssClass="form-control" ClientIDMode="Static">
                                            <asp:ListItem Value="1">Ledger Name</asp:ListItem>
                                            <asp:ListItem Value="2">Ledger Code</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Ledger Name</label>
                                        <asp:TextBox runat="server" CssClass="form-control capitalize ui-autocomplete-12" placeholder="Enter Ledger Name" ID="txtLedgerName" MaxLength="255" ClientIDMode="Static"></asp:TextBox>
                                        <asp:HiddenField ID="hfLedgerName" runat="server" ClientIDMode="Static" />
                                        <small><span id="valtxtLedgerName" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" Text="Search" OnClick="btnSearch_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-block btn-default" Style="margin-top: 23px;" Text="Clear" OnClick="btnClear_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GVLedgerDetail" ShowHeaderWhenEmpty="true" EmptyDataText="Record Not Found!" EmptyDataRowStyle-ForeColor="Red" class="table table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="Ledger_ID" runat="server" OnSelectedIndexChanged="GVLedgerDetail_SelectedIndexChanged" OnRowDeleting="GVLedgerDetail_RowDeleting" AllowPaging="true" PageSize="25" OnPageIndexChanging="GVLedgerDetail_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="10" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" ToolTip='<%# Eval("Ledger_ID") %>' Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ledger Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLedger_Name" Text='<%# Eval("Ledger_Name") %>' runat="server" />
                                                        <%--<asp:Label ID="lblID" CssClass="hidden" Text='<%# Eval("Ledger_ID") %>' runat="server" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
												  <asp:TemplateField HeaderText="Ledger Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLedger_Code" Text='<%# Eval("Ledger_Code") %>' runat="server" />
                                                        <%--<asp:Label ID="lblID" CssClass="hidden" Text='<%# Eval("Ledger_ID") %>' runat="server" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblID" CssClass="hidden" Text='<%# Eval("Ledger_ID") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GST NO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGstNO" Text='<%# Eval("GST_No") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Ledger_Name" ControlStyle-CssClass="Ledger" ItemStyle-Wrap="true" HeaderText="Ledger Name" />--%>
                                                <%--<asp:BoundField DataField="Head_Name" HeaderText="Head Name" />--%>
                                                <asp:BoundField DataField="Office_Name" HeaderText="Office Name" />
                                                <asp:TemplateField HeaderText="Status" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Edit" runat="server" CssClass="label Aselect1 label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Delete" runat="server" CssClass="label label-danger" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return getConfirmation();" Visible='<%# Eval("DelStatus").ToString() =="false"?true:false %>'></asp:LinkButton>
                                                        <%--Visible='<%# Eval("DelStatus").ToString() =="false"?true:false %>'--%>
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
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>
    <script src="js/dataTables.buttons.min.js"></script>
    <script src="js/buttons.flash.min.js"></script>
    <script src="js/jszip.min.js"></script>
    <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>
    <script src="js/buttons.html5.min.js"></script>
    <script src="js/buttons.print.min.js"></script>
    <script src="js/buttons.colVis.min.js"></script>
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
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 6]
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
    </script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script>
        $(document).ready(function () {

            debugger;
           
            $("#<%=txtLedgerName.ClientID %>").autocomplete({

                source: function (request, response) {
                    $.ajax({

                        
                        url: '<%=ResolveUrl("LedgerDetailB.aspx/SearchLedger") %>',
                        data: "{ 'Ledger_Name': '" + $('#txtLedgerName').val() + "','SearchOption': '" + $('#ddlSearchWith').val() + "'}",
                        //  var param = { ItemName: $('#txtItem').val() };
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {

                            response($.map(data.d, function (item) {
                                return {
                                    label: item
                                    //val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("#<%=hfLedgerName.ClientID %>").val(i.item.val);
                },
                minLength: 1

            });

        });
    </script>
</asp:Content>



