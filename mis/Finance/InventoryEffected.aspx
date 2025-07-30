<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="InventoryEffected.aspx.cs" Inherits="mis_Finance_InventoryEffected" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        table th {
            text-align: center !important;
        }



        table > thead > tr > th, td {
            padding: 4px !important;
            font-size: 12px !important;
            white-space: nowrap;
        }

        #myInput {
            background-image: url('images/searchicon.png'); /* Add a search icon to input */
            background-position: 10px 12px; /* Position the search icon */
            background-repeat: no-repeat; /* Do not repeat the icon image */
            width: 100%; /* Full-width */
            font-size: 16px; /* Increase font-size */
            padding: 12px 20px 12px 40px; /* Add some padding */
            border: 1px solid #ddd; /* Add a grey border */
            margin-bottom: 12px; /* Add some space below the input */
        }

        #GridView1 {
            border-collapse: collapse; /* Collapse borders */
            width: 100%; /* Full-width */
            border: 1px solid #ddd; /* Add a grey border */
            font-size: 10px; /* Increase font-size */
        }

            #GridView1 th, #GridView1 td {
                text-align: left; /* Left-align text */
            }

            #GridView1 tr {
                /* Add a bottom border to all table rows */
                border-bottom: 1px solid #ddd;
            }

                #GridView1 tr.header, #GridView1 tr:hover {
                    /* Add a grey background color to the table header and on hover */
                    background-color: #f1f1f1;
                }

        .ss1, .snumber {
            color: #123456;
            font-size: 12px;
            font-weight: 600;
        }

        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('images/progress.gif') 50% 50% no-repeat rgb(249,249,249);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="loader"></div>
    <div class="content-wrapper">
        <%--  <div id="loaderWrapper" runat="server" class="loader-wrapper" style="display: none; background-color: rgba(108, 131, 243, 0.33);">
            <div id="loader"></div>
            <span id="loaderSpan">Please Wait...</span>
        </div>
         document.getElementById('<%=loaderWrapper.ClientID%>').style.display = "none";--%>
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success" style="min-height: 500px;">
                <div class="box-header">
                    <h3 class="box-title">Office Wise Sales & Purchase Ledgers and Stock Status</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control select2" AutoPostBack="True" OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Ledger <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlLedgerName" runat="server" class="form-control select2" AutoPostBack="True" OnSelectedIndexChanged="ddlLedgerName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group"></div>
                    <div id="DivDetail" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive" style="height: 400px;">
                                    <%-- <div class="body-scroll">--%>
                                    <asp:TextBox ID="myInput" runat="server" ClientIDMode="Static" onkeyup="myFunction()" placeholder="Search" title="Type in a name"></asp:TextBox>

                                    <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped Grid" ClientIDMode="Static" AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="ItemTx_ID">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" Checked="true" Enabled='<%# Eval("GenStatus").ToString() == "StockUpdated" ? false : true %>' />

                                                    <asp:Label ID="lblGenStatus" runat="server" CssClass="hidden" Text='<%# Eval("GenStatus").ToString()%>' />
                                                    <asp:Label ID="lblItemTx_ID" runat="server" CssClass="hidden" Text='<%# Eval("ItemTx_ID").ToString()%>' />
                                                    <asp:Label ID="lblVoucherTx_ID" runat="server" CssClass="hidden" Text='<%# Eval("VoucherTx_ID").ToString()%>' />
                                                    <asp:Label ID="lblOffice_ID" runat="server" CssClass="hidden" Text='<%# Eval("Office_ID").ToString()%>' />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SNo.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ItemTx_ID").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Voucher No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Eval("VoucherNo").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Voucher Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVoucherDate" runat="server" Text='<%# Eval("VoucherTx_Date").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Voucher Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVoucherTx_Name" runat="server" Text='<%# Eval("VoucherTx_Name").ToString()%>' ForeColor="Red" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitem_name" runat="server" Text='<%# Eval("item_name").ToString()%>' ForeColor="Red" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Quantity In Voucher">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity").ToString()%>' ForeColor="Red" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rate In Voucher">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate").ToString()%>' ForeColor="Red" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount In Voucher">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount").ToString()%>' ForeColor="Red" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Stock Inward">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockInward" runat="server" Text='<%# Eval("StockInward").ToString()%>' ForeColor="Red" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Stock Outward">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockOutward" runat="server" Text='<%# Eval("StockOutward").ToString()%>' ForeColor="Red" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="form-group"></div>
                        <div class="row">
                            <div class="col-md-8"></div>
                            <div class="col-md-2">
                                <asp:Button ID="btnGenerated" CssClass="btn btn-block btn-success" runat="server" Text="Update Stock" OnClick="btnGenerated_Click" />
                            </div>
                            <%--                            <div class="col-md-2">
                                <a class="btn btn-block btn-default" href="AdminDesignation.aspx">Clear</a>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        $(document).ready(function () {
            $('.loader').fadeOut();
        });
        $('#checkAll').click(function () {
            var inputList = document.querySelectorAll('#GridView1 tbody input[type="checkbox"]:not(:disabled)');
            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('checkAll').checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });


        $(document).ready(function () {


            var checkbox = $('table tbody input[type="checkbox"]:disabled');
            for (var i = 0; i < checkbox.length; i++) {
                $(checkbox[i]).parents('tr').css('background-color', 'rgba(255, 24, 0, 0.55)');
                //    $(checkbox[i]).parents('tr').css('color', '#FFFFFF');

                //$('table tbody input[type="checkbox"]').css('width', '25px');
            }
        });

        function validateform() {
            var msg = "";

            if (document.getElementById('<%=ddlLedgerName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Ledger. \n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }

    </script>
    <script>
        function myFunction() {
            var input, filter, table, tr, td, i;
            input = document.getElementById("myInput");
            filter = input.value.toUpperCase();
            table = document.getElementById("GridView1");
            tr = table.getElementsByTagName("tr");
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[3];
                if (td) {
                    if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        }
    </script>
</asp:Content>



