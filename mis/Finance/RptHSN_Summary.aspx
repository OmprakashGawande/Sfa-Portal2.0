<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptHSN_Summary.aspx.cs" Inherits="mis_Finance_RptHSN_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .table > tbody > tr > td {
            padding: 3px;
        }

        .multiselect-native-select .dropdown-menu {
            height: 260px;
            overflow-x: hidden;
        }
         .Dtime {
            display: none;
        }

        @media print {
            .hide_print, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
            }

            tfoot, thead {
                display: table-row-group;
                bottom: 0;
            }

            .Dtime {
                display: block;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <asp:Label ID="lblTime" runat="server" CssClass="Dtime" Style="font-weight: 800;" Text="" ClientIDMode="Static"></asp:Label>
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">HSN Summary</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>From Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>To Date</label><span style="color: red">*</span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <%--<asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged">
                                </asp:DropDownList>--%>
                                <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>HSN Code<span class="text-danger">*</span></label>
                                <asp:ListBox runat="server" ID="ddlHsnCode" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success Aselect1" Style="margin-top: 24px;" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblParticulars" Style="font-size: 21px; font-weight: 700;" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Label ID="lblParticularsRate" Style="font-size: 19px; font-weight: 700;" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GridHSNSummery" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered pagination-ys"
                                    AllowPaging="True" PageSize="50" EmptyDataText="No Record Found" OnPageIndexChanging="GridHSNSummery_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="4%" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Office_Name" ItemStyle-Width="13%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOffice_Name" Text='<%# Eval("Office_Name").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HSN / SAC" ItemStyle-Width="13%" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHSN" Text='<%# Eval("HSN").ToString() %>' runat="server" Style="color: #3c8dbc" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:ButtonField ButtonType="Link" ControlStyle-CssClass="Aselect1" CommandName="View" HeaderText="HSN / SAC" DataTextField="HSN" />--%>
                                        <asp:TemplateField HeaderText="Particular" ItemStyle-Width="13%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemName" Text='<%# Eval("Particular_Name").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description" ItemStyle-Width="13%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" Text='<%# Eval("Description").ToString() %>' runat="server" />
                                                <asp:Label ID="lblHSN" CssClass="hidden" Text='<%# Eval("HSN").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type Of Supply">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTypeOfSupply" Text='<%# Eval("TypeOfSupply").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UQC">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUQC" Text='<%# Eval("UQC").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalQuantity" Text='<%# Eval("TotalQuantity").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalValue" Text='<%# Eval("TotalValue").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Taxable Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaxableValue" Text='<%# Eval("TaxableValue").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Integrated Tax Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIntegratedTaxAmount" Text='<%# Eval("IntegratedTaxAmount").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Central Tax Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCentralTaxAmount" Text='<%# Eval("CentralTaxAmount").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State Tax Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStateUTTaxAmountt" Text='<%# Eval("StateUTTaxAmount").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Cess Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCessAmt" Text='<%# Eval("CessAmt").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Total Tax Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalTaxAmt" Text='<%# Eval("TotalTaxAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
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
    <script>
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select From Date. \n";
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select To Date. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }
        }
        $('#txtFromDate').change(function () {
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');
            if ($('#txtToDate').val() != "") {
                if (start > end) {
                    if ($('#txtFromDate').val() != "") {
                        alert("From date should not be greater than To Date.");
                        $('#txtFromDate').val("");
                    }
                }
            }
        });
        $('#txtToDate').change(function () {
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');
            if (start > end) {
                if ($('#txtToDate').val() != "") {
                    alert("To Date can not be less than From Date.");
                    $('#txtToDate').val("");
                }
            }
        });
    </script>
    <%--<script src="../../../mis/js/jquery.js" type="text/javascript"></script>--%>
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script>
        $(function () {
            $('[id*=ddlOffice]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });
        });
        $(function () {
            $('[id*=ddlHsnCode]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });
        });
    </script>
    <style>
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }
    </style>
</asp:Content>


