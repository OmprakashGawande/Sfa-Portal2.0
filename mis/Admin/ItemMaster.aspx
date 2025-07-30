<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ItemMaster.aspx.cs" Inherits="Trade_ItemMasterAgro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <%--<script src="../js/jquery.min.js"></script>
    <link href="../DataTable_CssJs/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../DataTable_CssJs/jquery.dataTables.min.css" rel="stylesheet" />--%>
    <style>
        /*.datepicker tbody {
            background-color: #ecfce6 !important;
            color: black;
        }

        .datepicker th {
            background-color: #608640 !important;
        }*/

        /*.label-orange {
            background-color: #f5ac45;
        }

        .label {
            display: inline;
            padding: 0.2em 0.6em 0.3em;
            font-size: 80%;
            font-weight: 700;
            line-height: 1;
            color: #fff;
            text-align: center;
            white-space: nowrap;
            vertical-align: baseline;
            border-radius: 0.25em;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #066205;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
            margin-top: 4%;
        }

        a.btn.btn-default.buttons-print {
            background: #1e79e9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
            margin-top: 4%;
        }

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
        }*/
        .search {
            padding-left: 18px;
            border: 1px solid #140f0f;
        }

        .GridPager a,
        .GridPager span {
            display: inline-block;
            padding: 0px 9px;
            margin-right: 4px;
            border-radius: 3px;
            border: solid 1px #ffffff;
            background: #e9e9e9;
            box-shadow: inset 0px 1px 0px rgba(255,255,255, .8), 0px 1px 3px rgba(0,0,0, .1);
            font-size: .875em;
            font-weight: bold;
            text-decoration: none;
            color: #717171;
            text-shadow: 0px 1px 0px rgba(255,255,255, 1);
        }

        .GridPager a {
            background-color: #f5f5f5;
            color: #969696;
            border: 1px solid #969696;
        }

        .GridPager span {
            background: #616161;
            box-shadow: inset 0px 0px 8px rgba(0,0,0, .5), 0px 1px 0px rgba(255,255,255, .8);
            color: #f0f0f0;
            text-shadow: 0px 0px 3px rgba(0,0,0, .5);
            border: 1px solid #3AC0F2;
        }
    </style>
    <%--  <script type="text/javascript">
        function validatename(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 65 || charCode > 90) && (charCode < 97 || charCode > 122) && charCode != 32) {
                return false;
            }
            return true;        }
    </script>
    <style>
        .customCSS td {
            padding: 0px !important;
        }

        table {
            white-space: nowrap;
        }

        .capitalize {
            text-transform: capitalize;
        }

        ul.ui-autocomplete.ui-menu.ui-widget.ui-widget-content.ui-corner-all {
            height: 197px !important;
            overflow-y: scroll !important;
            width: 520px !important;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="Save" ShowMessageBox="true" ShowSummary="false" />
    <%--    //-Confirmation Modal Start --%>

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">

                        <h5 class="modal-title" id="myModalLabel">Confirmation</h5>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../../images/popupimg.jpg" width="30" />&nbsp;&nbsp; 
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" OnClick="btnsave_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
    <div class="content-wrapper">

        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Item Master</h3>
                            <asp:Label ID="lblmsg" runat="server"></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label runat="server">Item Main Category (वस्तु की मुख्य श्रेणी)<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="Save"
                                                ErrorMessage="Select Main Category" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Main Category !'></i>"
                                                ControlToValidate="ddlmaincategory" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlmaincategory" runat="server" CssClass="form-control select2" OnSelectedIndexChanged="ddlmaincategory_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        <small><span id="valddlMainCategory" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label runat="server">Item Category (वस्तु की श्रेणी)<span class="text-danger">*</span></label>
                                        <span class="left">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="Save"
                                                ErrorMessage="Select Category" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Category !'></i>"
                                                ControlToValidate="ddlcategory" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList ID="ddlcategory" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        <small><span id="valddlItemCategory" class="text-danger"></span></small>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label runat="server">Item Name (In English)<span class="text-danger">*</span> </asp:Label>
                                        <span class="left">
                                            <asp:RequiredFieldValidator ID="Rfv4" ValidationGroup="Save"
                                                ErrorMessage="Enter Item Name in English " ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Enter Item Name in English !'></i>"
                                                ControlToValidate="txtitemnameE" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtitemnameE" runat="server" CssClass="form-control" MaxLength="40" placeholder="Enter Item Name" onkeypress="return lettersOnly();" AutoComplete="off"></asp:TextBox>
                                        <small><span id="valtxtItemNameE" class="text-danger"></span></small>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label runat="server">वस्तु का नाम (हिंदी में)<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RFV5" ValidationGroup="Save"
                                                ErrorMessage="वस्तु का नाम (हिंदी में) " ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='वस्तु का नाम (हिंदी में) !'></i>"
                                                ControlToValidate="txtitemnameH" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtitemnameH" runat="server" CssClass="form-control" MaxLength="50" AutoComplete="off" placeholder="वस्तु का नाम दर्ज करें" onkeypress="return hindiOnly();"></asp:TextBox>
                                        <small><span id="valtxtItemNameH" class="text-danger"></span></small>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label runat="server">Item Code (वस्तु का कोड)</label>
                                        <%--<span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RFV6" ValidationGroup="Save"
                                                ErrorMessage="Enter Item Code " ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='> Enter Item Code  !'></i>"
                                                ControlToValidate="txtitemcode" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>--%>
                                        <asp:TextBox ID="txtitemcode" runat="server" CssClass="form-control" MaxLength="10" AutoComplete="off"></asp:TextBox>
                                        <small><span id="valtxtitemaliscode" class="text-danger"></span></small>
                                        <%--  <asp:RegularExpressionValidator ID="RegExIfscCode" ValidationGroup="Save" runat="server" Display="Dynamic" ControlToValidate="txtitemcode"
                                            ErrorMessage="Invalid Item Code" SetFocusOnError="true"
                                            ForeColor="Red" ValidationExpression="^[A-Z]{4}0[A-Z0-9]{6}$"></asp:RegularExpressionValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label runat="server">Packaging Size (पैकेजिंग का आकार)</label>
                                        <%-- <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="Save"
                                                ErrorMessage="Enter Size of Packaging " ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='> Enter Size of Packaging  !'></i>"
                                                ControlToValidate="txtpackagingsize" Display="Dynamic" runat="server">
                                          </asp:RequiredFieldValidator>
                                        </span>--%>
                                        <asp:TextBox ID="txtpackagingsize" runat="server" CssClass="form-control" onkeypress=" return isNumber(event);" MaxLength="5" placeholder="Enter Packaging size" AutoComplete="off"></asp:TextBox>
                                        <small><span id="valtxtpackagingsize" class="text-danger"></span></small>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label runat="server">Item Unit (वस्तु की इकाई)<span class="text-danger">*</span></label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RFV2" ValidationGroup="Save"
                                                ErrorMessage="Select Item Unit" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Item Unit !'></i>"
                                                ControlToValidate="ddlitemunit" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>

                                        <asp:DropDownList ID="ddlitemunit" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        <small><span id="valddlUnit" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label runat="server">HSN Code<span class="text-danger">*</span></label>
                                        <%-- <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="Save"
                                                ErrorMessage="Select HSN Code" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select HSN Code !'></i>"
                                                ControlToValidate="ddlhsncode" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>--%>

                                        <asp:DropDownList ID="ddlhsncode" runat="server" CssClass="form-control select2">
                                        </asp:DropDownList>
                                        <small><span id="AlertddlHsnCode" class="text-danger"></span></small>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Purchase Ledger (खरीद लेजर)</label>&nbsp;&nbsp;<asp:CheckBox ID="chkpurchaseledger" ClientIDMode="Static" Checked="true" runat="server" onchange="checkboxpurchasechange();" />
                                        <asp:DropDownList ID="ddlpurchaseledger" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlpurchaseledger" class="text-danger"></span></small>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlHsnCode" ErrorMessage="Select HSN Code" ForeColor="Red" SetFocusOnError="true" InitialValue="Select"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Sales Ledger(बिक्री लेजर)</label>&nbsp;&nbsp;<asp:CheckBox ID="chksalesledger" ClientIDMode="Static" Checked="true" runat="server" onchange="checkboxsaleschange();" />
                                        <asp:DropDownList ID="ddlsalesledger" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlsalesledger" class="text-danger"></span></small>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlHsnCode" ErrorMessage="Select HSN Code" ForeColor="Red" SetFocusOnError="true" InitialValue="Select"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label runat="server">Item Specification(वस्तु का विवरण)</label>
                                        <span class="pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="Save"
                                                ErrorMessage="Enter Description of Item " ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title=' Enter Description of Item   !'></i>"
                                                ControlToValidate="txtitemdescription" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox ID="txtitemdescription" runat="server" CssClass="form-control" TextMode="MultiLine" ClientIDMode="Static" MaxLength="300" AutoComplete="off"></asp:TextBox>
                                        <small><span id="valtxtitemDes" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnsave" runat="server" CssClass="btn btn-block btn-success" Text="Save" ClientIDMode="Static" OnClick="btnsave_Click" ValidationGroup="Save" OnClientClick="return validateform();" />

                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnclear" runat="server" CssClass="btn btn-block btn-default" Text="Clear" ClientIDMode="Static" OnClick="btnclear_Click" />
                                    </div>
                                </div>
                            </div>
                            <fieldset>
                                <div class="row" style="margin-bottom: 10px">
                                    <div class="col-md-8"></div>
                                    <div class="col-md-1" style="margin-top: 8px">
                                        <label id="lblSearch" runat="server">Search:</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtSearch" AutoComplete="off" runat="server" placeholder="Search" CssClass="form-control search" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </div>
                                </div>
                                <legend>Item Details</legend>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">

                                            <asp:GridView ID="GVitemdetails" Visible="false" runat="server" CssClass="table table-bordered" DataKeyNames="Item_id" AutoGenerateColumns="false" OnRowCommand="GVitemdetails_RowCommand" EmptyDataText="No Record Found" AllowPaging="true" AllowCustomPaging="true" PageSize="100"
                                                OnPageIndexChanging="GVitemdetails_PageIndexChanging">
                                                <PagerStyle HorizontalAlign="Left" CssClass="GridPager" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No." HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%#(GVitemdetails.PageIndex * GVitemdetails.PageSize) + Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="Item Name(in English)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemnameE" runat="server" Text='<%#Eval("ItemName_Eng") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="MainCategory" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmaincategory" runat="server" Text='<%#Eval("MainCategory") %>'></asp:Label>
                                                            <asp:Label ID="lblmaincategoryE" runat="server" Text='<%#Eval("ItemCatName") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblmaincategoryH" runat="server" Text='<%#Eval("ItemCatName_Hin") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblmaincategoryId" runat="server" Text='<%#Eval("ItemCat_id") %>' Visible="false"></asp:Label>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ItemCategory" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemcategory" runat="server" Text='<%#Eval("ItemCategory") %>'></asp:Label>
                                                            <asp:Label ID="lblitemcategoryE" runat="server" Text='<%#Eval("ItemTypeName") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblitemcategoryH" runat="server" Text='<%#Eval("ItemTypeName_Hin") %>' Visible="false"></asp:Label>

                                                            <asp:Label ID="lblitemcategoryID" runat="server" Text='<%#Eval("ItemType_id") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name(in English)" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemnameE" runat="server" Text='<%#Eval("ItemName_Eng") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name (in Hindi)" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemnameH" runat="server" Text='<%#Eval("ItemName_Hin") %>'></asp:Label>
                                                            <asp:Label ID="lblitemcode" runat="server" Text='<%#Eval("ItemAliasCode") %>' Visible="false"></asp:Label>
                                                            <%-- <asp:Label ID="lblitempacsize" runat="server" Text='<%#Eval("PackagingSize") %>' Visible="false"></asp:Label>--%>
                                                            <asp:Label ID="lblitemDescription" runat="server" Text='<%#Eval("ItemSpecification") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblhsncode" runat="server" Text='<%#Eval("HSNCode") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="MainCategory">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmaincategory" runat="server" Text='<%#Eval("MainCategory") %>'></asp:Label>
                                                            <asp:Label ID="lblmaincategoryE" runat="server" Text='<%#Eval("ItemCatName_Eng") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblmaincategoryH" runat="server" Text='<%#Eval("ItemCatName_Hin") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblmaincategoryId" runat="server" Text='<%#Eval("ItemCat_id") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <%--<asp:TemplateField HeaderText="ItemCategory">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemcategory" runat="server" Text='<%#Eval("ItemCategory") %>'></asp:Label>
                                                            <asp:Label ID="lblitemcategoryE" runat="server" Text='<%#Eval("ItemTypeName_Eng") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblitemcategoryH" runat="server" Text='<%#Eval("ItemTypeName_Hin") %>' Visible="false"></asp:Label>

                                                            <asp:Label ID="lblitemcategoryID" runat="server" Text='<%#Eval("ItemType_id") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Packaging Size" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitempacsize" runat="server" Text='<%#Eval("PackagingSize") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Unit" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemunit" runat="server" Text='<%#Eval("ItemUnit") %>'></asp:Label>
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("UnitName_Eng") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("UnitName_Hin") %>' Visible="false"></asp:Label>

                                                            <asp:Label ID="lblunit_ID" runat="server" Text='<%#Eval("Unit_id") %>' Visible="false"></asp:Label>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Purchase Ledger " HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%#Eval("PurchaseLedger") %>'></asp:Label>
                                                            <asp:Label ID="lblPurchaseLedgerid" runat="server" Text='<%#Eval("PurchaseLedger_id") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales Ledger" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%#Eval("SalesLedger") %>'></asp:Label>
                                                            <asp:Label ID="lblSalesLedgerid" runat="server" Text='<%#Eval("SalesLedger_id") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Active" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkactiveItem" runat="server" Checked='<%#Eval("Item_IsActive").ToString()=="True"?true:false %>' OnCheckedChanged="chkactiveItem_CheckedChanged" AutoPostBack="true" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="linkedit" runat="server" CommandArgument='<%#Eval("Item_id") %>' CommandName="EditRecord" ToolTip="Edit" CssClass="btn btn-primary"> <i class ="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" />
                                            </asp:GridView>
                                            <asp:GridView ID="gvSearchItemDetails" Visible="false" runat="server" CssClass="table table-bordered" DataKeyNames="Item_id" AutoGenerateColumns="false" OnRowCommand="GVitemdetails_RowCommand" EmptyDataText="No Record Found"
                                                OnPageIndexChanging="GVitemdetails_PageIndexChanging">
                                                <%--<PagerStyle HorizontalAlign="Left" CssClass="GridPager" />--%>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No." HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%#(GVitemdetails.PageIndex * GVitemdetails.PageSize) + Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="Item Name(in English)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemnameE" runat="server" Text='<%#Eval("ItemName_Eng") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="MainCategory" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmaincategory" runat="server" Text='<%#Eval("MainCategory") %>'></asp:Label>
                                                            <asp:Label ID="lblmaincategoryE" runat="server" Text='<%#Eval("ItemCatName") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblmaincategoryH" runat="server" Text='<%#Eval("ItemCatName_Hin") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblmaincategoryId" runat="server" Text='<%#Eval("ItemCat_id") %>' Visible="false"></asp:Label>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ItemCategory" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemcategory" runat="server" Text='<%#Eval("ItemCategory") %>'></asp:Label>
                                                            <asp:Label ID="lblitemcategoryE" runat="server" Text='<%#Eval("ItemTypeName") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblitemcategoryH" runat="server" Text='<%#Eval("ItemTypeName_Hin") %>' Visible="false"></asp:Label>

                                                            <asp:Label ID="lblitemcategoryID" runat="server" Text='<%#Eval("ItemType_id") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name(in English)" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemnameE" runat="server" Text='<%#Eval("ItemName_Eng") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name (in Hindi)" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemnameH" runat="server" Text='<%#Eval("ItemName_Hin") %>'></asp:Label>
                                                            <asp:Label ID="lblitemcode" runat="server" Text='<%#Eval("ItemAliasCode") %>' Visible="false"></asp:Label>
                                                            <%-- <asp:Label ID="lblitempacsize" runat="server" Text='<%#Eval("PackagingSize") %>' Visible="false"></asp:Label>--%>
                                                            <asp:Label ID="lblitemDescription" runat="server" Text='<%#Eval("ItemSpecification") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblhsncode" runat="server" Text='<%#Eval("HSNCode") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="MainCategory">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmaincategory" runat="server" Text='<%#Eval("MainCategory") %>'></asp:Label>
                                                            <asp:Label ID="lblmaincategoryE" runat="server" Text='<%#Eval("ItemCatName_Eng") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblmaincategoryH" runat="server" Text='<%#Eval("ItemCatName_Hin") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblmaincategoryId" runat="server" Text='<%#Eval("ItemCat_id") %>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <%--<asp:TemplateField HeaderText="ItemCategory">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemcategory" runat="server" Text='<%#Eval("ItemCategory") %>'></asp:Label>
                                                            <asp:Label ID="lblitemcategoryE" runat="server" Text='<%#Eval("ItemTypeName_Eng") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblitemcategoryH" runat="server" Text='<%#Eval("ItemTypeName_Hin") %>' Visible="false"></asp:Label>

                                                            <asp:Label ID="lblitemcategoryID" runat="server" Text='<%#Eval("ItemType_id") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Packaging Size" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitempacsize" runat="server" Text='<%#Eval("PackagingSize") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Unit" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemunit" runat="server" Text='<%#Eval("ItemUnit") %>'></asp:Label>
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("UnitName_Eng") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("UnitName_Hin") %>' Visible="false"></asp:Label>

                                                            <asp:Label ID="lblunit_ID" runat="server" Text='<%#Eval("Unit_id") %>' Visible="false"></asp:Label>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Purchase Ledger " HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%#Eval("PurchaseLedger") %>'></asp:Label>
                                                            <asp:Label ID="lblPurchaseLedgerid" runat="server" Text='<%#Eval("PurchaseLedger_id") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales Ledger" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%#Eval("SalesLedger") %>'></asp:Label>
                                                            <asp:Label ID="lblSalesLedgerid" runat="server" Text='<%#Eval("SalesLedger_id") %>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Active" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkactiveItem" runat="server" Checked='<%#Eval("Item_IsActive").ToString()=="True"?true:false %>' OnCheckedChanged="chkactiveItem_CheckedChanged" AutoPostBack="true" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="linkedit" runat="server" CommandArgument='<%#Eval("Item_id") %>' CommandName="EditRecord" ToolTip="Edit" CssClass="btn btn-primary"> <i class ="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <%--<PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last" />--%>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>

                        </div>




                    </div>
                </div>
            </div>

        </section>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <%--  <script src="../DataTable_CssJs/jquery.js"></script>
    <script src="../DataTable_CssJs/jquery.dataTables.min.js"></script>
    <script src="../DataTable_CssJs/dataTables.bootstrap.min.js"></script>
    <script src="../DataTable_CssJs/dataTables.buttons.min.js"></script>
    <script src="../DataTable_CssJs/buttons.flash.min.js"></script>
    <script src="../DataTable_CssJs/jszip.min.js"></script>
    <script src="../DataTable_CssJs/pdfmake.min.js"></script>
    <script src="../DataTable_CssJs/vfs_fonts.js"></script>
    <script src="../DataTable_CssJs/buttons.html5.min.js"></script>
    <script src="../DataTable_CssJs/buttons.print.min.js"></script>
    <script src="../DataTable_CssJs/buttons.colVis.min.js"></script>--%>
    <script type="text/javascript">
        //$('.datatable').DataTable({
        //    paging: true,
        //    PageLength: 15,
        //    columnDefs: [{
        //        targets: 'no-sort',
        //        orderable: false
        //    }],
        //    dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
        //      '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
        //      '<"row"<"col-sm-5"i><"col-sm-7"p>>',
        //    fixedHeader: {
        //        header: true
        //    },
        //    buttons: {
        //        buttons: [{
        //            extend: 'print',
        //            text: '<i class="fa fa-print"></i> Print',
        //            title: $('h3').text(),
        //            exportOptions: {
        //                columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
        //            },
        //            footer: true,
        //            autoPrint: true
        //        }, {
        //            extend: 'excel',
        //            text: '<i class="fa fa-file-excel-o"></i> Excel',
        //            title: $('h3').text(),
        //            exportOptions: {
        //                columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
        //            },
        //            footer: true
        //        }],
        //        dom: {
        //            container: {
        //                className: 'dt-buttons'
        //            },
        //            button: {
        //                className: 'btn btn-default'
        //            }
        //        }
        //    }
        //});

        function previewProfileImage() {
            var preview = document.querySelector('#imgProfileImage'); //selects the query named img
            var file = document.querySelector('#FUItemImage').files[0]; //sames as here
            var reader = new FileReader();
            reader.onloadend = function () {
                preview.src = reader.result;
            }
            if (file) {
                reader.readAsDataURL(file); //reads the data as a URL
            } else {
                preview.src = "";
            }
        }

        function callalert() {

            $("#OfficeModal").modal('show');
        }
        function validateform() {
            debugger;
            var msg = "";
            $("#valtxtItemNameE").html("");
            $("#valtxtItemNameH").html("");
            $("#valddlMainCategory").html("");
            $("#valddlItemCategory").html("");
            $("#valtxtitemaliscode").html("");
            //$("#valtxtpackagingsize").html("")

            $("#valddlUnit").html("");

            $("#valddlHsnCode").html("");
            $("#valchkOffice").html("");
            //$("#valtxtitemDes").html("");
            //$("#valddlpurchaseledger").html("");
            //$("#valddlsalesledger").html("");
            //$("#valddlsalesledger").html("");
            //$("#valddlpurchaseledger").html("");
            //$("#valddlpurchaseledger").html("");
            //$("#valtxtitemDes").html("");

            if (document.getElementById('<%=ddlmaincategory.ClientID%>').selectedIndex == 0) {
                msg += "Select Main Category.\n"
                $("#valddlMainCategory").html("Select Main Category");
            }
            if (document.getElementById('<%=ddlcategory.ClientID%>').selectedIndex == 0) {
                msg += "Select Item Category. \n"
                $("#valddlItemCategory").html("Select Item Category");
            }
            if (document.getElementById('<%=txtitemnameE.ClientID%>').value.trim() == "") {
                msg += "Enter Item Name in English. \n"
                $("#valtxtItemNameE").html("Enter Item Name in English");
            }
            if (document.getElementById('<%=txtitemnameH.ClientID%>').value.trim() == "") {
                msg += "Enter Item Name in Hindi. \n"
                $("#valtxtItemNameH").html("Enter Item Name in HIndi");
            }
            <%--<%-- if (document.getElementById('<%= txtitemcode.ClientID%>').value.trim() == "") {
                msg += "Enter Item Code. \n"
                $("#valtxtitemaliscode").html("Enter Item Alias/Code");
            }--%>
            <%--if (document.getElementById('<%= txtpackagingsize.ClientID%>').value.trim() == "") {
                msg += "Enter Packaging Size \n"
                $("#valtxtpackagingsize").html("Enter Item Alias/Code");
            }--%>

            if (document.getElementById('<%=ddlitemunit .ClientID%>').selectedIndex == 0) {
                msg += "Select Item Unit.  \n"
                $("#valddlUnit").html("Select Item Unit");
            }
            <%--if (document.getElementById('<%=ddlhsncode.ClientID%>').selectedIndex == 0) {
                msg += "select hsn code. \n"
                $("#valddlhsncode").html("select hsn code");
            }--%>
            if (document.getElementById('<%=txtitemdescription .ClientID%>').selectedIndex == 0) {
                msg += "Enter Item Description. \n"
                $("#valtxtitemDes").html("Enter Item Description ");
            }
            <%--if (document.getElementById('<%=chkpurchaseledger.ClientID%>').checked) {
                if (document.getElementById('<%=ddlpurchaseledger.ClientID%>').selectedIndex == 0) {
                    msg += "Select Purchase Ledger. \n"
                    $("#valddlpurchaseledger").html("Select Purchase Ledger");
                }
            }--%>
            <%--    if (document.getElementById('<%=chksalesledger.ClientID%>').checked == false) {
                if (document.getElementById('<%=chkpurchaseledger.ClientID%>').checked == false) {
                     msg += "Select atleast one Ledger. \n"
                     $("#valddlpurchaseledger").html("Select atleast one Ledger");
                     $("#valddlsalesledger").html("Select atleast one Ledger");
                 }
             }
             if (document.getElementById('<%=chkpurchaseledger.ClientID%>').checked == false) {
                if (document.getElementById('<%=chksalesledger.ClientID%>').checked == false) {
                    msg += "Select atleast one Ledger. \n"
                    $("#valddlpurchaseledger").html("Select atleast one Ledger");
                    $("#valddlsalesledger").html("Select atleast one Ledger");
                }
            }--%>
            <%--if (document.getElementById('<%=chksalesledger.ClientID%>').checked) {
                if (document.getElementById('<%=ddlsalesledger.ClientID%>').selectedIndex == 0) {
                    msg += "Select Sales Ledger. \n"
                    $("#valddlsalesledger").html("Select Sales Ledger");
                }
            }--%>






            //if ($('#chkOffice input:checked').length > 0) {

            //}
            //else {
            //    //alert('Please select atleast one Group')
            //    msg += "Select atleast one Office. \n"
            //    $("#valchkOffice").html("Select atleast one Office");
            //}



            if (msg != "") {
                alert(msg)
                return false

            }
            else {
                if (document.getElementById('<%=btnsave.ClientID%>').value.trim() == "Submit") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }
                else if (document.getElementById('<%=btnsave.ClientID%>').value.trim() == "Update") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }
            }

        }
        function checkboxsaleschange() {

            var checkbox1 = document.getElementById('<%= chkpurchaseledger.ClientID%>').checked;
            var checkbox2 = document.getElementById('<%= chksalesledger.ClientID%>').checked;
            if (checkbox1 == false && checkbox2 == false) {
               <%-- document.getElementById('<%= chkpurchaseledger.ClientID%>').checked = true;
                 $("#valddlpurchaseledger").html("");--%>
            }

        }
        function checkboxpurchasechange() {

            var checkbox1 = document.getElementById('<%= chkpurchaseledger.ClientID%>').checked;
            var checkbox2 = document.getElementById('<%= chksalesledger.ClientID%>').checked;
            if (checkbox1 == false && checkbox2 == false) {
                <%--document.getElementById('<%= chksalesledger.ClientID%>').checked = true;
                    $("#valddlsalesledger").html("");--%>
            }

        }
    </script>
    <%-- <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />--%>
    <%-- <script>

        $(document).ready(function () {
            debugger;


            $("#<%=txtitemnameE.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({

                        url: '<%=ResolveUrl("ItemMaster.aspx/SearchCustomers") %>',
                        data: "{ 'ItemName': '" + $('#txtItemName').val() + "'}",
                        //  var param = { ItemName: $('#txtItem').val() };
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            debugger;
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
                   <% $("#<%=hfItemName.ClientID %>").val(i.item.val);
                },
                minLength: 1

            });

        });--%>
    <%--   </script>--%>
    <script type="text/javascript">
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('Save');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnsave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnsave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
        function hindiOnly() {
            var charCode = event.keyCode;
            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == (U + 0020))
                return false;
            else
                return true;
        }
        function lettersOnly() {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)

                return true;
            else
                return false;
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 32 && (charCode < 46 || charCode == 47 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function validateCode() {
            var TCode = document.getElementById("TCode").value;

            for (var i = 0; i < TCode.length; i++) {
                var char1 = TCode.charAt(i);
                var cc = char1.charCodeAt(0);

                if ((cc > 47 && cc < 58) || (cc > 64 && cc < 91) || (cc > 96 && cc < 123)) {
                } else {
                    alert("Input is not alphanumeric");
                    return false;
                }
            }
            return true;
        }
        $(function () {
            var content = $('#txtSearch').val();

            $('#txtSearch').keypress(function () {
                if ($('#txtSearch').val() != content) {
                    content = $('#txtSearch').val();
                    alert('Content has been changed');
                }
            });
        });
    </script>
</asp:Content>

