<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptGSTR_1.aspx.cs" Inherits="mis_Finance_RptGSTR_1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
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
                <div class="box-header">
                    <h3 class="box-title">GSTR - 1</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="hide_print">
                        <div class="row">
                             <div class="col-md-3" runat="server" id="divRegionalOffice">
                                <div class="form-group">
                                    <label>Regional Office</label><span style="color: red">*</span>
                                    <asp:DropDownList ID="ddlRegionalOffice" runat="server"  ClientIDMode="Static" CssClass="form-control" OnSelectedIndexChanged="ddlRegionalOffice_SelectedIndexChanged"  AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Office Name</label><span style="color: red">*</span>
                                    <%-- <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged">
                                    </asp:DropDownList>--%>
                                    <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                </div>

                            </div>

                            <div class="col-md-2">
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

                            <div class="col-md-2">
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
                            <div class="col-md-2">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success Aselect1" Style="margin-top: 24px;" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>

                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">

                            <asp:Panel ID="DivTable" runat="server">

                                <div class="hide_print">
                                    <label>As Per Trial Balance</label>
                                    <div class="table-responsive">
                                        
                                        <table class="table table-bordered" style="margin-bottom: 0px;">
                                            <tr>
                                                 <th style="background-color: #90add2 !important; width: 10%;">Taxable Value</th>
                                                <th>
                                                    <asp:Label ID="lblTTaxableValue" runat="server" Text="4646464"></asp:Label>
                                                </th>
                                                <th style="background-color: #90add2 !important; width: 10%;">SGST</th>
                                                <th>
                                                    <asp:Label ID="lblSGST" runat="server" Text=""></asp:Label>
                                                </th>
                                                <th style="background-color: #90add2 !important; width: 10%;">CGST</th>
                                                <th>
                                                    <asp:Label ID="lblCGST" runat="server" Text=""></asp:Label>
                                                </th>
                                                <th style="background-color: #90add2 !important; width: 10%;">IGST</th>
                                                <th>
                                                    <asp:Label ID="lblIGST" runat="server" Text=""></asp:Label>
                                                </th>
                                            </tr>
                                        </table>
                                    </div>
                                    <label>Export Excel</label>
                                    <br />
                                    <asp:Button ID="btnAllExcel" runat="server" Text="Export GSTR-1" OnClick="btnAllExcel_Click" />
                                    <asp:Button ID="btnEB2BVoucher" runat="server" Text="B2B" OnClick="btnEB2BVoucher_Click" />
                                    <asp:Button ID="btnEB2CLVoucher" runat="server" Text="B2CL" OnClick="btnEB2CLVoucher_Click" />
                                    <asp:Button ID="btnEB2CMVoucher" runat="server" Text="B2CS" OnClick="btnEB2CMVoucher_Click" />
                                    <asp:Button ID="btnEHSN" runat="server" Text="HSN" OnClick="btnEHSN_Click" />
                                    <asp:Button ID="btnENilRatedVoucher" runat="server" Text="EXEMP" OnClick="btnENilRatedVoucher_Click" />
                                    <asp:Button ID="Button3" Visible="false" runat="server" Text="CDNR" OnClick="btnEB2BVoucher_Click" />
                                    <asp:Button ID="Button4" Visible="false" runat="server" Text="CDNUR" OnClick="btnEB2BVoucher_Click" />
                                    <asp:Button ID="Button5" Visible="false" runat="server" Text="EXP" OnClick="btnEB2BVoucher_Click" />
                                    <asp:Button ID="Button6" Visible="false" runat="server" Text="AT" OnClick="btnEB2BVoucher_Click" />
                                    <asp:Button ID="Button7" Visible="false" runat="server" Text="ATADJ" OnClick="btnEB2BVoucher_Click" />

                                    <asp:Button ID="Button9" Visible="false" runat="server" Text="DOCS" OnClick="btnEB2BVoucher_Click" />
                                    <br />
                                </div>
                                <table cellspacing="0" cellpadding="0" border="0" width="100%" style="background-color: #ecf0f5; margin-top: 5px;">
                                    <tr>
                                        <td valign="Top" width="100%" height="20">
                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="20">
                                                            <tr>
                                                                <td width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: bold 9pt 'Arial';" width="85" align="Left">GSTIN/UIN:
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160;  </td>
                                                                <td valign="Bottom" style="color: #000000; font: 9pt 'Arial';" width="110" align="Left">23AACCM0330Q1ZM
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="599">&#160;
                                                                </td>
                                                                <td width="3">&#160;  </td>
                                                                <td valign="Bottom" style="color: #000000; font: bold 9pt 'Arial';" width="163" align="Right">
                                                                    <asp:Label ID="lblDateRange" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="4"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="Top" width="100%" height="131">
                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                <tr>
                                                    <td valign="Top" width="100%" height="2"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="23">
                                                            <tr>
                                                                <td style="border-top: 2px solid #000000; border-bottom: 1px solid #000000;" width="3">&#160;  </td>
                                                                <td valign="Bottom" style="color: #000000; font: bold 9pt 'Arial'; border-top: 2px solid #000000; border-bottom: 1px solid #000000;" width="967" align="Left">Returns Summary    </td>

                                                                <td style="border-top: 2px solid #000000; border-bottom: 1px solid #000000;" width="2">&#160;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="5"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="19">
                                                            <tr>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: bold 9pt 'Arial';" width="906" align="Left">
                                                                    <asp:LinkButton ID="btnTotalVoucher" runat="server" OnClick="btnTotalVoucher_Click">Total number of vouchers for the period</asp:LinkButton>
                                                                </td>

                                                                <td width="2">&#160; </td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: bold 9pt 'Arial';" width="56" align="Right">
                                                                    <asp:Label ID="lblTotalVoucher" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="3"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="19">
                                                            <tr>
                                                                <td
                                                                    width="11">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: 9pt 'Arial';" width="898" align="Left">
                                                                    <asp:LinkButton ID="btnTotalVoucherSale" runat="server" OnClick="btnTotalVoucherSale_Click">Included in returns</asp:LinkButton>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="56" align="Right">

                                                                    <asp:Label ID="lblTotalVoucherSale" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="3"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="360">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="16">
                                                            <tr>
                                                                <td
                                                                    width="7">&#160;
                                                                </td>
                                                                <td
                                                                    width="11">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: italic 9pt 'Arial';" width="276" align="Left">
                                                                    <asp:LinkButton ID="btnVoucherReturn" runat="server" OnClick="btnVoucherReturn_Click">Included in HSN/SAC Summary</asp:LinkButton>

                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: italic 9pt 'Arial';" width="56" align="Right">
                                                                    <asp:Label ID="lblVoucherReturn" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="10">&#160;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="360">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="16">
                                                            <tr>
                                                                <td
                                                                    width="7">&#160;
                                                                </td>
                                                                <td
                                                                    width="11">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: italic 9pt 'Arial';" width="276" align="Left">
                                                                    <asp:LinkButton ID="btnVoucherInComplete" runat="server" OnClick="btnVoucherInComplete_Click"> Incomplete HSN/SAC information (to be provided)</asp:LinkButton>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="56" align="Right">
                                                                    <asp:Label ID="lblVoucherInComplete" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="10">&#160;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="19">
                                                            <tr>
                                                                <td width="11">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; font: 9pt 'Arial';" width="898" align="Left">
                                                                    <asp:LinkButton ID="btnVoucherNotRelevent" runat="server" OnClick="btnVoucherNotRelevent_Click">Not relevant for returns </asp:LinkButton>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="56" align="Right">
                                                                    <asp:Label ID="lblVoucherNotRelevent" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="3"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="19">
                                                            <tr>
                                                                <td width="11">&#160;  </td>
                                                                <td valign="Bottom" style="color: #000000; font: 9pt 'Arial';" width="898" align="Left">

                                                                    <asp:LinkButton ID="btnVoucherMismatch" runat="server" OnClick="btnVoucherMismatch_Click"> Incomplete/Mismatch in information (to be resolved) </asp:LinkButton>
                                                                </td>

                                                                <td width="2">&#160; </td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="56" align="Right">
                                                                    <asp:Label ID="lblVoucherMismatch" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="3"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="Top" width="100%" height="55">
                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                <tr>
                                                    <td valign="Top" width="100%" height="2" style="border-top: 2px solid #000000;"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="55">
                                                            <tr>
                                                                <td width="3">&#160;</td>
                                                                <td valign="Bottom" style="color: #000000; font: bold 9pt 'Arial';" width="28" align="Left">Sl No. </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="15">&#160; </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; font: bold 9pt 'Arial';" width="168" align="Left">Particulars</td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; font: bold 9pt 'Arial';" width="49" align="Right">Voucher Count </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: bold 9pt 'Arial';" width="106" align="Right">Taxable Value </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: bold 9pt 'Arial';" width="85" align="Right">Integrated Tax Amount</td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; font: bold 9pt 'Arial';" width="85" align="Right">Central Tax Amount   </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; font: bold 9pt 'Arial';" width="113" align="Right">State Tax Amount
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; font: bold 9pt 'Arial';" width="85" align="Right">Cess Amount
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160;  </td>
                                                                <td valign="Bottom" style="color: #000000; font: bold 9pt 'Arial';" width="85" align="Right">Tax Amount  </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; font: bold 9pt 'Arial';" width="106" align="Right">Invoice Amount  </td>
                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="5"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="Top" width="100%" height="360">
                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="38">
                                                            <tr>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="28" align="Left">1
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: 9pt 'Arial';" width="180" align="Left">
                                                                    <asp:LinkButton ID="btnB2BVoucher" runat="server" OnClick="btnB2BVoucher_Click">B2B Invoices - 4A, 4B, 4C, 6B, 6C </asp:LinkButton>
                                                                </td>

                                                                <td width="2"></td>
                                                                <td width="21"></td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="31" align="Left">
                                                                    <asp:Label ID="lblVB2BCount" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="lblTaxableValue" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblIntegratedTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblCentralTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="113" align="Right">
                                                                    <asp:Label ID="lblStateTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160;</td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblCess" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblTaxAmount" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="lblInVoiceAmount" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td valign="Top" width="100%" height="6"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="22">
                                                            <tr>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 9pt 'Arial';" width="28" align="Left">&#160; </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="9">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: italic 9pt 'Arial';" width="174" align="Left">

                                                                    <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton1_Click">Taxable Sales </asp:LinkButton>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="21">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="31" align="Left">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="106" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="85" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="85" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="113" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="85" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="85" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="106" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="6"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="22">
                                                            <tr>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="28" align="Left">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="9">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: italic 9pt 'Arial';" width="174" align="Left">
                                                                    <asp:LinkButton ID="LinkButton9" runat="server" OnClick="LinkButton1_Click">Reverse charge supplies</asp:LinkButton>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="21">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="31" align="Left">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="106" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="85" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="85" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="113" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="85" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="85" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: italic 
9pt 'Arial';"
                                                                    width="106" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td valign="Top" width="100%" height="6"></td>
                                                </tr>
                                                <%-- B2C (Large)--%>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="22">
                                                            <tr>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="28" align="Left">2
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: 9pt 'Arial';" width="180" align="Left">
                                                                    <asp:LinkButton ID="btnB2CLVoucher" runat="server" OnClick="btnB2CLVoucher_Click"> B2C(Large) Invoices - 5A, 5B</asp:LinkButton>

                                                                </td>

                                                                <td width="2"></td>
                                                                <td width="21"></td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="31" align="Left">
                                                                    <asp:Label ID="lblB2CLVCount" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="lblB2CLTaxableValue" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2CLIntegratedTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2CLCentralTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="113" align="Right">
                                                                    <asp:Label ID="lblB2CLStateTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160;</td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2CLCess" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2CLTaxAmount" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="lblB2CLInVoiceAmount" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="6"></td>
                                                </tr>
                                                <%-- B2C (Small)--%>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="22">
                                                            <tr>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="28" align="Left">3
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: 9pt 'Arial';" width="180" align="Left">
                                                                    <asp:LinkButton ID="btnB2CMVoucher" runat="server" OnClick="btnB2CMVoucher_Click"> B2C(Small) Invoices - 7</asp:LinkButton>
                                                                </td>

                                                                <td width="2"></td>
                                                                <td width="21"></td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="31" align="Left">
                                                                    <asp:Label ID="lblB2CMVCount" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="lblB2CMTaxableValue" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2CMIntegratedTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2CMCentralTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="113" align="Right">
                                                                    <asp:Label ID="lblB2CMStateTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160;</td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2CMCess" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2CMTaxAmount" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="lblB2CMInVoiceAmount" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="6"></td>
                                                </tr>
                                                <%-- Credit/Debit Notes(Registered) - 9B--%>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="22">
                                                            <tr>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="28" align="Left">4
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: 9pt 'Arial';" width="180" align="Left">
                                                                    <asp:LinkButton ID="btnB2BCrCrRegVoucher" runat="server" OnClick="btnB2BCrCrRegVoucher_Click"> Credit/Debit Notes(Registered) - 9B</asp:LinkButton>
                                                                </td>

                                                                <td width="2"></td>
                                                                <td width="21"></td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="31" align="Left">
                                                                    <asp:Label ID="lblB2B_CDNoteVCount" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="lblB2B_CDNoteTaxableValue" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2B_CDNoteIntegratedTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2B_CDNoteCentralTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="113" align="Right">
                                                                    <asp:Label ID="lblB2B_CDNoteStateTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160;</td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2B_CDNoteCess" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2B_CDNoteTaxAmount" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="lblB2B_CDNoteInVoiceAmount" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td valign="Top" width="100%" height="6"></td>
                                                </tr>
                                                <%-- Credit/Debit Notes(Unregistered) - 9B--%>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="40">
                                                            <tr>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="28" align="Left">5
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: 9pt 'Arial';" width="180" align="Left">
                                                                    <asp:LinkButton ID="btnB2BCrCrUnRegVoucher" runat="server" OnClick="btnB2BCrCrUnRegVoucher_Click"> Credit/Debit Notes(Unregistered) - 9B</asp:LinkButton>
                                                                </td>

                                                                <td width="2"></td>
                                                                <td width="21"></td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="31" align="Left">
                                                                    <asp:Label ID="lblB2B_CDNoteUnRVCount" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="lblB2B_CDNoteUnRTaxableValue" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2B_CDNoteUnRIntegratedTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2B_CDNoteUnRCentralTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="113" align="Right">
                                                                    <asp:Label ID="lblB2B_CDNoteUnRStateTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160;</td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2B_CDNoteUnRCess" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblB2B_CDNoteUnRTaxAmount" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="lblB2B_CDNoteUnRInVoiceAmount" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="6"></td>
                                                </tr>
                                                <%-- Exports Invoices - 6A--%>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="22">
                                                            <tr>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="28" align="Left">6
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: 9pt 'Arial';" width="180" align="Left">
                                                                    <asp:LinkButton ID="LinkButton14" runat="server" OnClick="LinkButton1_Click"> Exports Invoices - 6A</asp:LinkButton>
                                                                </td>

                                                                <td width="2"></td>
                                                                <td width="21"></td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="31" align="Left">
                                                                    <asp:Label ID="Label33" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="Label34" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="Label35" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="Label36" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="113" align="Right">
                                                                    <asp:Label ID="Label37" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160;</td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="Label38" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="Label39" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="Label40" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td valign="Top" width="100%" height="6"></td>
                                                </tr>
                                                <%-- Tax Liability(Advances received) - 11A(1), 11A(2)--%>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="40">
                                                            <tr>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="28" align="Left">7
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: 9pt 'Arial';" width="180" align="Left">
                                                                    <asp:LinkButton ID="LinkButton15" runat="server" OnClick="LinkButton1_Click"> Tax Liability(Advances received) - 11A(1), 11A(2)</asp:LinkButton>

                                                                </td>

                                                                <td width="2"></td>
                                                                <td width="21"></td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="31" align="Left">
                                                                    <asp:Label ID="Label41" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="Label42" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="Label43" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="Label44" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="113" align="Right">
                                                                    <asp:Label ID="Label45" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160;</td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="Label46" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="Label47" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="Label48" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="6"></td>
                                                </tr>
                                                <%-- Adjustment of Advances - 11B(1), 11B(2)--%>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="40">
                                                            <tr>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="28" align="Left">8
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: 9pt 'Arial';" width="180" align="Left">
                                                                    <asp:LinkButton ID="LinkButton16" runat="server" OnClick="LinkButton1_Click">  Adjustment of Advances - 11B(1), 11B(2)</asp:LinkButton>
                                                                </td>

                                                                <td width="2"></td>
                                                                <td width="21"></td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="31" align="Left">
                                                                    <asp:Label ID="Label49" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="Label50" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="Label51" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="Label52" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="113" align="Right">
                                                                    <asp:Label ID="Label53" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160;</td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="Label54" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="Label55" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="Label56" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="6"></td>
                                                </tr>

                                                <%-- Exports Invoices - 6A--%>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="22">
                                                            <tr>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="28" align="Left">9
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: 9pt 'Arial';" width="180" align="Left">
                                                                    <asp:LinkButton ID="btnNilRatedVoucher" runat="server" OnClick="btnNilRatedVoucher_Click">  Nil Rated Invoices - 8A, 8B, 8C, 8D</asp:LinkButton>
                                                                </td>

                                                                <td width="2"></td>
                                                                <td width="21"></td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="31" align="Left">
                                                                    <asp:Label ID="lblNillRatedVCount" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="lblNillRatedTaxableValue" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblNillRatedIntegratedTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblNillRatedCentralTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="113" align="Right">
                                                                    <asp:Label ID="lblNillRatedStateTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160;</td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblNillRatedCess" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblNillRatedTaxAmount" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="lblNillRatedInVoiceAmount" runat="server" Text=""></asp:Label>
                                                                </td>
                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td valign="Top" width="100%" height="6"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="1" style="border-top: 1px solid #000000;"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" style="border-top: 1px solid #000000; border-bottom: 3px double #000000;">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="26">
                                                            <tr>
                                                                <td width="3">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: bold 9pt 'Arial';" width="28" align="Left">&#160;  </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160;  </td>
                                                                <td valign="Bottom" style="color: #000000; font: bold 9pt 'Arial';" width="180" align="Left">Total  </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="21">&#160; </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: bold 9pt 'Arial';" width="31" align="Left">
                                                                    <asp:Label ID="lblTotalMVCount" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: bold 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="lblTotalMTaxableValue" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160;  </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: bold 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblTotalMIntegratedTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: bold 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblTotalMCentralTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: bold 9pt 'Arial';" width="113" align="Right">
                                                                    <asp:Label ID="lblTotalMStateTax" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: bold 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblTotalMCess" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: bold 9pt 'Arial';" width="85" align="Right">
                                                                    <asp:Label ID="lblTotalMTaxAmount" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: bold 9pt 'Arial';" width="106" align="Right">
                                                                    <asp:Label ID="lblTotalMInVoiceAmount" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="9"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="22">
                                                            <tr>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="28" align="Left">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; font: 9pt 'Arial';" width="180" align="Left">
                                                                    <asp:LinkButton ID="btnHSNSummary" runat="server" OnClick="btnHSNSummary_Click">  HSN/SAC Summary - 12</asp:LinkButton>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="21">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="31" align="Left">&#160;
                                                                0</td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">&#160;
                                                                <asp:Label ID="lblHSNTaxableValue" runat="server" Text=""></asp:Label>

                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">&#160;
                                                                <asp:Label ID="lblHSNIntegratedTaxAmount" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">&#160;
                                                                <asp:Label ID="lblHSNCentralTaxAmount" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="113" align="Right">&#160;
                                                                  <asp:Label ID="lblHSNStateUTTaxAmount" runat="server" Text=""></asp:Label></td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">&#160;
                                                                  <asp:Label ID="lblHSNCessAmt" runat="server" Text=""></asp:Label></td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="85" align="Right">&#160;
                                                                 <asp:Label ID="lblHSNTotalTaxAmt" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom" style="color: #000000; white-space: nowrap; font: 9pt 'Arial';" width="106" align="Right">&#160;
                                                                 <asp:Label ID="Label6" runat="server" Text=""></asp:Label>
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="6"></td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="4"></td>
                                                </tr>

                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="26">
                                                            <tr>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="28" align="Left">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; font: 9pt 'Arial';"
                                                                    width="180" align="Left">Document Summary - 13
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="21">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="31" align="Left">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="106" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="85" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="85" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="113" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="85" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="85" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #000000; white-space: nowrap; font: 9pt 'Arial';"
                                                                    width="106" align="Right">&#160;
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="Top" width="100%" height="6"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="Top" width="100%" height="16">
                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                <tr>
                                                    <td valign="Top" width="100%">
                                                        <table cellspacing="0" cellpadding="0" border="0" width="100%" height="16">
                                                            <tr>
                                                                <td
                                                                    width="3">&#160;
                                                                </td>
                                                                <td valign="Bottom"
                                                                    style="color: #0000ff; font: italic 
9pt 'Arial';"
                                                                    width="967" align="Left">Note: Voucher count and values are not provided for HSN/SAC Summary and Document Summary. Drill down for details.
                                                                </td>

                                                                <td width="2">&#160;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-11">
                            <asp:Label ID="lblParticulars" Style="font-size: 21px; font-weight: 700;" runat="server" Text=""></asp:Label>
                            <br />
                            <asp:Label ID="lblParticularsRate" Style="font-size: 19px; font-weight: 700;" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-1">
                            <asp:Button ID="btnBack" runat="server" CssClass="btn btn-block label-primary Aselect1" Text="Back" OnClick="btnBack_Click" OnClientClick="return validateform();" />
                            <asp:Button ID="btnBackNext" runat="server" CssClass="btn btn-block label-primary Aselect1" Text="Back" OnClick="btnBackNext_Click" OnClientClick="return validateform();" />
                            <asp:Button ID="btnShowDetailBook" runat="server" CssClass="hidden" Text="Show Bank Detail" OnClick="btnShowDetailBook_Click" AccessKey="Q" />
                        </div>
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GridRateWise" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" OnRowCommand="GridRateWise_RowCommand" ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="Aselect1" CommandName="View" HeaderText="Particulars" DataTextField="Particulars" />

                                        <asp:TemplateField HeaderText="Rate Of Tax" ItemStyle-Width="13%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRateOfTax" Text='<%# Eval("RateOfTax").ToString() %>' runat="server" />
                                                <asp:Label ID="lblParticulars1" CssClass="hidden" Text='<%# Eval("Particulars").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Taxable Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaxableValue" Text='<%# Eval("TaxableValue").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGST" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIGSTAmt" Text='<%# Eval("IGSTAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGST" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCGSTAmt" Text='<%# Eval("CGSTAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SGST" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSGSTAmt" Text='<%# Eval("SGSTAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cess Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCessAmount" Text='<%# Eval("CessAmount").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TotalTaxAmt" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalTaxAmt" Text='<%# Eval("TotalTaxAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <asp:GridView ID="GridView2" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedger_Name" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vch Type" ItemStyle-Width="13%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_Type" Text='<%# Eval("VoucherTx_Type").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vch No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_No" Text='<%# Eval("VoucherTx_No").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Taxable Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTaxableValue" Text='<%# Eval("TaxableValue").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="IGST" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIGSTAmt" Text='<%# Eval("IGSTAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGST" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCGSTAmt" Text='<%# Eval("CGSTAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SGST" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSGSTAmt" Text='<%# Eval("SGSTAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cess Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCessAmount" Text='<%# Eval("CessAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Tax Amt" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalTaxAmt" Text='<%# Eval("TotalTaxAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice Value" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalTaxAmt" Text='<%# Eval("InvoiceValue").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hpView" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("1")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info">View</asp:HyperLink>
                                                <asp:HyperLink ID="hpEdit" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("2")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info" Visible='<%# Eval("V_Editright").ToString() == "Yes" ? true : false %>'>Edit</asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView ID="GridView1" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedger_Name" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vch Type" ItemStyle-Width="13%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_Type" Text='<%# Eval("VoucherTx_Type").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vch No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_No" Text='<%# Eval("VoucherTx_No").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit Amt." ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDebitAmt" Text='<%# Eval("DebitAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amt." ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreditAmt" Text='<%# Eval("CreditAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("1")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info">View</asp:HyperLink>
                                                <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("2")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info" Visible='<%# Eval("V_Editright").ToString() == "Yes" ? true : false %>'>Edit</asp:HyperLink>

                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                </asp:GridView>
                                <asp:GridView ID="GridHSNSummery" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" OnRowCommand="GridHSNSummery_RowCommand" ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField ButtonType="Link" ControlStyle-CssClass="Aselect1" CommandName="View" HeaderText="HSN / SAC" DataTextField="HSN" />

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
                                        <asp:TemplateField HeaderText="TotalQuantity">
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
                                        <asp:TemplateField HeaderText="Cess Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCessAmt" Text='<%# Eval("CessAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Tax Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalTaxAmt" Text='<%# Eval("TotalTaxAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:GridView ID="GridHSNSummeryDes" DataKeyNames="VoucherTx_ID" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_Date" Text='<%# Eval("VoucherTx_Date").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLedger_Name" Text='<%# Eval("Ledger_Name").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GST No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGST_No" Text='<%# Eval("GST_No").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vch Type" ItemStyle-Width="13%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_Type" Text='<%# Eval("VoucherTx_Type").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Vch No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherTx_No" Text='<%# Eval("VoucherTx_No").ToString() %>' runat="server" />
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
                                                <asp:Label ID="lblStateTaxAmountt" Text='<%# Eval("StateTaxAmount").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cess Amount" ItemStyle-Width="10%" ItemStyle-CssClass="align-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCessAmt" Text='<%# Eval("CessAmt").ToString() %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hpView" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("1")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info">View</asp:HyperLink>
                                                <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl='<%# Eval("PageURL").ToString()+ "?VoucherTx_ID=" + APIProcedure.Client_Encrypt(Eval("VoucherTx_ID").ToString())+"&Action="+ APIProcedure.Client_Encrypt("2")+"&Office_ID="+ APIProcedure.Client_Encrypt(Eval("Office_ID").ToString()) %>' CssClass="label label-info" Visible='<%# Eval("V_Editright").ToString() == "Yes" ? true : false %>'>Edit</asp:HyperLink>
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
            debugger;
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
            debugger;
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
            max-height: 200px;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }

        ul.multiselect-container.dropdown-menu {
            overflow-y: scroll;
            overflow-x: hidden;
        }
    </style>

</asp:Content>


