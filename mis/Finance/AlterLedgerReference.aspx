<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AlterLedgerReference.aspx.cs" Inherits="mis_Finance_AlterLedgerReference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .material-switch > input[type="checkbox"] {
            display: none;
        }

        .material-switch > label {
            cursor: pointer;
            height: 0px;
            position: relative;
            width: 40px;
        }

            .material-switch > label::before {
                background: rgb(0, 0, 0);
                box-shadow: inset 0px 0px 10px rgba(0, 0, 0, 0.5);
                border-radius: 8px;
                content: '';
                height: 16px;
                margin-top: -8px;
                position: absolute;
                opacity: 0.3;
                transition: all 0.4s ease-in-out;
                width: 40px;
            }

            .material-switch > label::after {
                background: rgb(255, 255, 255);
                border-radius: 16px;
                box-shadow: 0px 0px 5px rgba(0, 0, 0, 0.3);
                content: '';
                height: 24px;
                left: -4px;
                margin-top: -8px;
                position: absolute;
                top: -4px;
                transition: all 0.3s ease-in-out;
                width: 24px;
            }

        .material-switch > input[type="checkbox"]:checked + label::before {
            background: inherit;
            opacity: 0.5;
        }

        .material-switch > input[type="checkbox"]:checked + label::after {
            background: inherit;
            left: 20px;
        }

        .customCSS td {
            padding: 0px !important;
        }

        table {
            white-space: nowrap;
        }

        .CapitalClass {
            text-transform: uppercase;
        }

        .capitalize {
            text-transform: capitalize;
        }

        input[type="text"]:focus {
            background-color: #3c8dbc7a;
            outline: 2px solid #00a1ff;
        }

        input[type="file"]:focus, input[type="radio"]:focus, input[type="checkbox"]:focus {
            outline: 2px solid #00a1ff;
        }

        select2-container:focus-within {
            background: lightyellow;
        }

        .select2-container *:focus {
            outline: 2px solid #00a1ff;
        }
        div.dataTables_wrapper div.dataTables_length select {
            margin-top:3px !important;
        }
        datepicker datepicker-dropdown dropdown-menu datepicker-orient-left datepicker-orient-top
        {
            z-index:9999;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    
                    <div class="row">
                    <div class="col-md-4"><h3 class="box-title">Alter Ledger Reference</h3></div>
                    <div class="col-md-2"></div>
                    <div class="col-md-2"></div>
                    <div class="col-md-2"></div>
                    <div class="col-md-2">
                        <asp:Button ID="btnAlterLedger" runat="server" Text="Ledger Alter" CssClass="btn btn-primary btn-block pull-left" OnClick="btnAlterLedger_Click"/>
                    </div>
                    </div>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Ledger Name<span style="color:red;">*</span></label>
                                        <asp:DropDownList ID="ddlLedger" ClientIDMode="Static" CssClass="form-control select2" runat="server" OnSelectedIndexChanged="ddlLedger_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <small><span id="valddlLedger" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Opening Balance</label>
                                        <asp:Label ID="txtOpeningBalance" runat="server" Text="" CssClass="form-control" Style="background-color: #eee;"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <%--<div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Date<span style="color: red;"> *</span></label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtBillByBillTx_Date" placeholder="DD/MM/YYYY" data-date-end-date="0d" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <small><span id="valtxtBillByBillTx_Date" style="color: red;"></span></small>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Type of Ref<span style="color: red;"> *</span></label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRefType" >
                                        <asp:ListItem Value="New Ref">New Ref</asp:ListItem> 
                                    </asp:DropDownList>
                                    <small><span id="valddlRefType" style="color: red;"></span></small>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Name<span style="color: red;">*</span></label> 
                                    <asp:TextBox runat="server"  ID="txtBillByBillTx_Ref" MaxLength="300" ClientIDMode="Static" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                    <small><span id="valtxtBillByBillTx_Ref" style="color: red;"></span></small>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Amount<span style="color: red;"> *</span></label>
                                    <asp:TextBox runat="server" ID="txtBillByBillTx_Amount" MaxLength="12" ClientIDMode="Static" CssClass="form-control"  onkeypress="return validateDec(this,event);" autocomplete="off"></asp:TextBox>
                                    <small><span id="valtxtBillByBillTx_Amount" style="color: red;"></span></small>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <div class="form-group">
                                    <label>Cr/Dr<span style="color: red;"> *</span></label>
                                    <asp:DropDownList ID="ddlBillByBillTx_crdr" CssClass="form-control select2" runat="server">
                                        <asp:ListItem Value="Cr">Cr</asp:ListItem>
                                        <asp:ListItem Value="Dr">Dr</asp:ListItem>
                                    </asp:DropDownList>
                                    <small><span id="valddlBillByBillTx_crdr" style="color: red;"></span></small>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>&nbsp;</label>
                                    <asp:Button runat="server" Text="Add" ID="btnAddBillByBill" ClientIDMode="Static" CssClass="btn btn-block btn-success"  OnClientClick="return validateForm();" OnClick="btnAddBillByBill_Click"></asp:Button>
                                </div>

                            </div>

                        </div>--%>
                             <fieldset>
                                <legend>BillByBillRef Detail</legend>
                                <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Name of Consignee</label>  
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtBillByBillTx_ConsigneeName"  autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Scheme</label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlSchemeTx_ID" >   
                                    </asp:DropDownList> 
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Order No.</label> 
                                    <asp:TextBox runat="server"  ID="txtBillByBillTx_OrderNo" MaxLength="300" ClientIDMode="Static" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Order Date</label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtBillByBillTx_OrderDate" placeholder="DD/MM/YYYY" data-date-end-date="0d" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Item Group</label>
                                    <asp:TextBox runat="server"  ID="txtBillByBillTx_ItemGroup"  CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                            <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Date<span style="color: red;"> *</span></label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtBillByBillTx_Date" placeholder="DD/MM/YYYY" data-date-end-date="0d" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <small><span id="valtxtBillByBillTx_Date" style="color: red;"></span></small>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Type of Ref<span style="color: red;"> *</span></label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRefType" >
                                        <asp:ListItem Value="New Ref">New Ref</asp:ListItem> 
                                    </asp:DropDownList>
                                    <small><span id="valddlRefType" style="color: red;"></span></small>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Name<span style="color: red;">*</span></label> 
                                    <asp:TextBox runat="server"  ID="txtBillByBillTx_Ref" MaxLength="300" ClientIDMode="Static" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                    <small><span id="valtxtBillByBillTx_Ref" style="color: red;"></span></small>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Amount<span style="color: red;"> *</span></label>
                                    <asp:TextBox runat="server" ID="txtBillByBillTx_Amount" MaxLength="12" ClientIDMode="Static" CssClass="form-control"  onkeypress="return validateDec(this,event);" autocomplete="off"></asp:TextBox>
                                    <small><span id="valtxtBillByBillTx_Amount" style="color: red;"></span></small>
                                </div>
                            </div>
                            <div class="col-md-1">
                                <div class="form-group">
                                    <label>Cr/Dr<span style="color: red;"> *</span></label>
                                    <asp:DropDownList ID="ddlBillByBillTx_crdr" CssClass="form-control select2" runat="server">
                                        <asp:ListItem Value="Cr">Cr</asp:ListItem>
                                        <asp:ListItem Value="Dr">Dr</asp:ListItem>
                                    </asp:DropDownList>
                                    <small><span id="valddlBillByBillTx_crdr" style="color: red;"></span></small>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>&nbsp;</label>
                                    <asp:Button runat="server" Text="Add" ID="btnAddBillByBill" CssClass="btn btn-block btn-success"  OnClientClick="return validateForm();" OnClick="btnAddBillByBill_Click"></asp:Button>
                                </div>

                            </div>

                        </div>
                            </fieldset>
                            <div class="row">

                            </div>
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-2"></div>
                                <div class="col-md-2"></div>
                                <div class="col-md-2"></div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <span style="text-align:right"><label>Total Adjusted Amount:</label></span>
                                        <asp:Label ID="lblremainingamnt" Text="" style="background-color: #eee; text-align:right" CssClass="form-control" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="Record Not Found!" EmptyDataRowStyle-ForeColor="Red" class="datatable1 table table-bordered" AutoGenerateColumns="False" ClientIDMode="Static" ID="GridViewBillByBillDetail" DataKeyNames="BillByBillTx_ID" OnRowDeleting="GridViewBillByBillDetail_RowDeleting" OnSelectedIndexChanged="GridViewBillByBillDetail_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("BillByBillTx_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillByBillTx_Ref" runat="server" Text='<%# Bind("BillByBillTx_Ref") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillByBillTx_ID" runat="server" Text='<%# Bind("BillByBillTx_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" SortExpression="leftBonus" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="18%" HeaderStyle-Width="18%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAmount" CssClass="hidden" runat="server" Text='<%# Bind("Amount") %>'></asp:TextBox>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("BillByBillTx_Amount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Type" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server" Text='<%# Bind("Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                 <asp:LinkButton ID="lnkEdit" CommandName="Select" CausesValidation="false" CssClass="label label-default" runat="server">Edit</asp:LinkButton>
                                                <asp:LinkButton ID="lnkDelete" CommandName="Delete" CausesValidation="false" CssClass="label label-danger" runat="server" OnClientClick="return confirm('Do you really want to delete?');" Visible='<%# Eval("DelStatus").ToString() =="false"?true:false %>'>Delete</asp:LinkButton>
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
            
        </section>
        <asp:HiddenField ID="hfvalue" runat="server" />
        
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
            "bSort": false,
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
                        columns: [0, 1, 2, 3]
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
        $('.datatable1').DataTable({
            paging: true,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            "bSort": false,
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
                        columns: [0,1, 2,3]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3]
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
       
        function validateForm() {
            var msg = "";
            debugger;
            $("#valddlBillByBillTx_Ref").html("");
            $("#valtxtBillByBillTx_Amount").html("");
            $("#valtxtBillByBillTx_Date").html("");

           if (document.getElementById('<%=ddlLedger.ClientID%>').selectedIndex == 0) {
                msg += "Select Ledger Name \n";
                $("#valddlLedger").html("Select Ledger Name");
            }
            if (document.getElementById('<%=txtBillByBillTx_Date.ClientID%>').value.trim() == "") {
                msg += "Enter Date \n";
                $("#valtxtBillByBillTx_Date").html("Enter Date");
            }
            if (document.getElementById('<%=txtBillByBillTx_Ref.ClientID%>').value.trim() == "") {
                msg += "Enter Name \n";
                $("#valtxtBillByBillTx_Ref").html("Enter Name");
            }
            if (document.getElementById('<%=txtBillByBillTx_Amount.ClientID%>').value.trim() == "") {
                msg += "Enter Amount \n";
                $("#valtxtBillByBillTx_Amount").html("Enter Amount");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {                
                    return true;   
            }               
        }
        $("#txtBillByBillTx_Date").datepicker({               
            pickerPosition: "bottom-left"
        });
    </script>
  
   <%--<script src="../js/ValidationJs.js"></script>--%>
</asp:Content>


