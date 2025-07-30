<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="LedgerMasterB.aspx.cs" Inherits="mis_Finance_LedgerMasterB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script src="https://www.google.com/jsapi" type="text/javascript">
    </script>
    <script type="text/javascript">
        google.load("elements", "1", { packages: "transliteration" });

        function onLoad() {
            var options = {
                //Source Language
                sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
                // Destination language to Transliterate
                destinationLanguage: [google.elements.transliteration.LanguageCode.HINDI],
                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };

            var control = new google.elements.transliteration.TransliterationControl(options);
            control.makeTransliteratable(['txtLedgerNameHindi']);

        }
        google.setOnLoadCallback(onLoad);
    </script>
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

        ul.ui-autocomplete.ui-menu.ui-widget.ui-widget-content.ui-corner-all {
            height: 197px !important;
            overflow-y: scroll !important;
            width: 520px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Ledger Creation</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3"></div>
                        <div class="col-md-3"></div>
                        <div class="col-md-3"></div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date<span style="color: red;"> *</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox runat="server" ClientIDMode="Static" data-date-end-date="0d" CssClass="form-control DateAdd" ID="txtVoucherTx_Date" placeholder="DD/MM/YYYY" Autocomplete="off" OnTextChanged="txtVoucherTx_Date_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <small><span id="valtxtVoucherTx_Date" style="color: red;"></span></small>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="pnbody" runat="server">
                        <asp:Panel ID="pnlOB" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>Ledger Detail</legend>
                                        <div class="row">

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Ledger Name<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control capitalize ui-autocomplete-12" placeholder="Enter Ledger Name" onblur="fillAcntholdername();" ID="txtLedgerName" MaxLength="255" ClientIDMode="Static"></asp:TextBox>
                                                    <asp:HiddenField ID="hfLedgerName" runat="server" ClientIDMode="Static" />
                                                    <small><span id="valtxtLedgerName" style="color: red;"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Ledger Name(In Hindi)</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Ledger Name In Hindi" ID="txtLedgerNameHindi" MaxLength="255" ClientIDMode="Static"></asp:TextBox>
                                                    <small><span id="valtxtLedgerNameHindi" style="color: red;"></span></small>
                                                </div>
                                            </div>
                                             <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Ledger Code<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Ledger Code" ID="txtLedgerCode" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                                                   <small><span id="valtxtLedgerCode" style="color: red;"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Alias</label>
                                                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Alias" ID="txtLedgerAlias" ClientIDMode="Static" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label>Group Name<span style="color: red;"> *</span></label>
                                                    <asp:DropDownList runat="server" CssClass="form-control select1 select2" ID="ddlHeadName" OnSelectedIndexChanged="ddlHeadName_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="true">
                                                        <asp:ListItem>Select</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <small><span id="valddlHeadName" style="color: red;"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-2" id="divType" runat="server">
                                                <div class="form-group">
                                                    <label>Type<span style="color: red;"> *</span></label>
                                                    <asp:DropDownList runat="server" CssClass="form-control select2" ID="ddlType" ClientIDMode="Static">
                                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="Other" Selected="True">Other</asp:ListItem>
                                                        <asp:ListItem Value="Institution">Institution</asp:ListItem>
                                                        <asp:ListItem Value="Distributor">Distributor</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <small><span id="valddlType" style="color: red;"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                            <asp:Panel ID="pnacndetails" runat="server">
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Bank Account Details</legend>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>A/c Holders Name</label>
                                                        <asp:TextBox runat="server" placeholder="Enter A/C Holders Name" ID="txtachlder_name" ClientIDMode="Static" CssClass="form-control capitalize" MaxLength="255" onkeypress="return validatename(event);"></asp:TextBox>
                                                        <asp:HiddenField ID="hfachlder_name" ClientIDMode="Static" runat="server" />
                                                        <small><span id="valtxtachlder_name" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>A/c No</label>
                                                        <asp:TextBox runat="server" placeholder="Enter A/c No" ID="txtacntno" CssClass="form-control" MaxLength="18" onkeypress="return validateNum(event);"></asp:TextBox>
                                                        <small><span id="valtxtacntno" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>IFSC Code(CAPITAL LETTERS ONLY)</label>
                                                        <asp:TextBox runat="server" ID="txtifsccode" placeholder="Example: SBIN0000058" CssClass="form-control IFSC" MaxLength="12" onkeypress="return alpha(event);"></asp:TextBox>
                                                        <small><span id="valtxtifsccode" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Bank Name</label>
                                                        <asp:TextBox runat="server" placeholder="Enter Bank Name" ID="txtbankname" CssClass="form-control" MaxLength="50" ClientIDMode="Static" onkeypress="return validatename(event);"></asp:TextBox>
                                                        <small><span id="valtxtbankname" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Branch</label>
                                                        <asp:TextBox runat="server" placeholder="Enter Branch" ID="txtbranch" CssClass="form-control" MaxLength="50" onkeypress="return validatename(event);"></asp:TextBox>
                                                        <small><span id="valtxtbranch" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Mailing Details</legend>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Name</label>
                                                        <asp:TextBox runat="server" placeholder="Enter Mailing Name" ID="txtMailing_Name" CssClass="form-control" MaxLength="50" onkeypress="return validatename(event);"></asp:TextBox>
                                                        <small><span id="valtxtMailing_Name" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>State</label>
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlState">
                                                            <asp:ListItem>Select</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small><span id="valddlState" style="color: red;"></span></small>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>City </label>
                                                        <asp:TextBox runat="server" placeholder="Enter City" ID="txtCity" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                        <small><span id="valtxtCity" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="pinno" runat="server" visible="true">
                                                    <div class="form-group">
                                                        <label>PinCode</label>
                                                        <asp:TextBox runat="server" placeholder="Enter PinCode" ID="txtpincode" CssClass="form-control PinCode" MaxLength="6" onkeypress="return validateNum(event)"></asp:TextBox>
                                                        <small><span id="valtxtpincode" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Address</label>
                                                        <asp:TextBox runat="server" ID="txtMailing_Address" placeholder="Enter Mailing Address" CssClass="form-control" MaxLength="300"></asp:TextBox>
                                                        <small><span id="valtxtMailing_Address" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Mobile No</label>
                                                        <asp:TextBox runat="server" placeholder="Enter Mobile No" ID="txtMobileno" CssClass="form-control MobileNo" MaxLength="10" onkeypress="return validateNum(event)"></asp:TextBox>
                                                        <small><span id="valtxtMobileno" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Email</label>
                                                        <asp:TextBox runat="server" placeholder="Enter Email" ID="txtEmail" CssClass="form-control Email" Style="text-transform: lowercase"></asp:TextBox>
                                                        <small><span id="valtxtEmail" style="color: red;"></span></small>
                                                    </div>
                                                </div>

                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Tax Registration Details</legend>
                                            <div class="row">

                                                <div class="col-md-3" id="panno" runat="server" visible="true">
                                                    <div class="form-group">
                                                        <label>PAN/IT No.(CAPITAL LETTERS ONLY)</label>
                                                        <asp:TextBox runat="server" placeholder="Example: ABCPE1234F" ID="txtMailing_PanNo" CssClass="form-control PanCard" MaxLength="10"></asp:TextBox>
                                                        <small><span id="valtxtMailing_PanNo" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Registration Types</label>
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlRegistrationType">
                                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                                            <asp:ListItem>Composition</asp:ListItem>
                                                            <asp:ListItem>Consumer</asp:ListItem>
                                                            <asp:ListItem>Regular</asp:ListItem>
                                                            <asp:ListItem>Unregistered</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small><span id="valddlRegistrationType" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-3" id="gstno" runat="server" visible="true">
                                                    <div class="form-group">
                                                        <label>GST No.(CAPITAL LETTERS ONLY)<span style="color: red;" id="gstvisible" runat="server" visible="false">*</span></label>
                                                        <asp:TextBox runat="server" placeholder="Enter GST No." ID="txtGSTNo" ClientIDMode="Static" CssClass="form-control GSTNo" MaxLength="15"></asp:TextBox>
                                                        <small><span id="valtxtGSTNo" style="color: red;"></span></small>
                                                    </div>

                                                </div>


                                                <div class="col-md-3" id="effectivedate" runat="server" visible="false">
                                                    <div class="form-group">
                                                        <label>Effective Date for Reconsilation<span style="color: red;"> *</span></label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtEffectivedate" AutoComplete="off" placeholder="Select Date" data-date-start-date="0d" ClientIDMode="Static" runat="server" class="form-control DateAdd"></asp:TextBox>
                                                        </div>
                                                        <small><span id="valtxtEffectivedate" style="color: red;"></span></small>
                                                    </div>
                                                </div>


                                            </div>
                                        </fieldset>


                                    </div>
                                </div>
                            </asp:Panel>

                        </asp:Panel>
                        <asp:Panel ID="GSTPanel" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <fieldset>
                                        <legend>GST Details</legend>
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Applicable From<span style="color: red;"> *</span></label>
                                                    <div class="input-group date">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar"></i>
                                                        </div>
                                                        <asp:TextBox runat="server" ClientIDMode="Static" data-date-end-date="0d" CssClass="form-control DateAdd" ID="txtapplicablefrom" placeholder="DD/MM/YYYY" Autocomplete="off"></asp:TextBox>
                                                    </div>
                                                    <small><span id="valtxtapplicablefrom" style="color: red;"></span></small>
                                                </div>
                                            </div>
                                            <div class="col-md-3" runat="server">
                                                <div class="form-group">
                                                    <label>GST Applicable<span style="color: red;"> *</span></label>
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlGSTApplicable" onchange="ShowhideGSTDetails();">
                                                        <%--<asp:ListItem Value="NA">Select</asp:ListItem>--%>
                                                        <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="No">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <small><span id="valddlGSTApplicable" style="color: red;"></span></small>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Panel ID="gstdetails" runat="server">
                                            <div class="row">

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Type Of Supply</label>
                                                        <asp:DropDownList runat="server" ID="ddltypeofsupply" CssClass="form-control" OnSelectedIndexChanged="ddltypeofsupply_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem>Select</asp:ListItem>
                                                            <asp:ListItem Value="Goods">Goods</asp:ListItem>
                                                            <asp:ListItem Selected="True" Value="Services">Services</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small><span id="valddltypeofsupply" style="color: red;"></span></small>
                                                    </div>
                                                </div>


                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <strong>
                                                            <asp:Label ID="lblHSN" runat="server" Text=""></asp:Label></strong>
                                                        <asp:DropDownList runat="server" placeholder="Enter A/c No" ID="ddlHSNCode" CssClass="form-control select2" OnSelectedIndexChanged="ddlHSNCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <small><span id="valddlHSNCode" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Description</label>
                                                        <asp:TextBox runat="server" placeholder="Enter Description" ID="txtDescription" ClientIDMode="Static" CssClass="form-control capitalize" MaxLength="255" onkeypress="return validatename(event);"></asp:TextBox>
                                                        <small><span id="valtxtDescription" style="color: red;"></span></small>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Taxability</label>
                                                        <asp:DropDownList runat="server" ID="ddlTaxability" CssClass="form-control" OnSelectedIndexChanged="ddlTaxability_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="NA">Select</asp:ListItem>
                                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                            <asp:ListItem Value="No">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small><span id="valddlTaxability" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Is reverse charge applicable?</label>
                                                        <asp:DropDownList runat="server" ID="ddlrcm" CssClass="form-control">
                                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                            <asp:ListItem Selected="True" Value="No">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small><span id="valddlrcm" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Is Ineligible for Input Credit?</label>
                                                        <asp:DropDownList runat="server" ID="ddlineligibleforinputcredit" CssClass="form-control">
                                                            <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                            <asp:ListItem Selected="True" Value="No">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <small><span id="valtxtineligibleforinputcredit" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Integrated Tax(IGST%)</label>
                                                        <asp:TextBox runat="server" Text="0" ID="txtIGST" CssClass="form-control"></asp:TextBox>
                                                        <small><span id="valtxtIGST" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Central Tax(CGST%)</label>
                                                        <asp:TextBox runat="server" Text="0" ID="txtCGST" CssClass="form-control"></asp:TextBox>
                                                        <small><span id="valtxtCGST" style="color: red;"></span></small>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>State Tax(SGST%)</label>
                                                        <asp:TextBox runat="server" Text="0" ID="txtSGST" CssClass="form-control"></asp:TextBox>
                                                        <small><span id="valtxtSGST" style="color: red;"></span></small>
                                                    </div>
                                                </div>



                                            </div>
                                        </asp:Panel>
                                    </fieldset>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend>Opening Balance</legend>
                                    <div class="row">

                                        <div class="col-md-3" id="inventoryvalue" runat="server">
                                            <div class="form-group">
                                                <label>Inventory Values are affected<span style="color: red;"> *</span></label>
                                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlInventoryAffected">
                                                    <%--<asp:ListItem Value="NA">Select</asp:ListItem>--%>
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="No">No</asp:ListItem>
                                                </asp:DropDownList>
                                                <small><span id="valddlInventoryAffected" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Maintain Balances BillByBill<span style="color: red;">*</span></label>
                                                <asp:DropDownList ID="ddlBalBillByBill" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlBalBillByBill_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="No">No</asp:ListItem>
                                                </asp:DropDownList>
                                                <small><span id="valddlBalBillByBill" style="color: red;"></span></small>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Cost Centre</label>
                                                <asp:DropDownList runat="server" ID="ddlCostCentre" CssClass="form-control" OnSelectedIndexChanged="ddlCostCentre_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No" Selected="True">No</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Dr./Cr.</label>
                                                <asp:DropDownList runat="server" ID="ddlDrCr" CssClass="form-control">
                                                    <asp:ListItem Value="Credit">Credit</asp:ListItem>
                                                    <asp:ListItem Value="Debit">Debit</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>Opening Balance <span style="color: red;">*</span></label>
                                                <asp:TextBox runat="server" CssClass="form-control" MaxLength="15" onkeypress="return validateDec(this,event);" placeholder="Enter Opening Balance" ID="txtOpeningBalance" Text="0"></asp:TextBox>
                                                <small><span id="valtxtOpeningBalance" style="color: red;"></span></small>
                                            </div>
                                        </div>


                                    </div>
                                </fieldset>
                            </div>
                        </div>



                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-block btn-success" Text="Save & Next" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                                </div>
                            </div>
                            <div class="col-md-2" id="clear" runat="server">
                                <div class="form-group">
                                    <a href="LedgerMasterB.aspx" class="btn btn-block btn-default">Clear</a>
                                </div>
                            </div>
                        </div>

                    </asp:Panel>
                </div>
            </div>
            <asp:HiddenField ID="hfvalue" runat="server" />
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        // \d{2}[A-Z]{5}\d{4}[A-Z]{1}[A-Z\d]{1}[Z]{1}[A-Z\d]{1}
        //===========GST NUMBER VALIDATION START ====================
        $('.GSTNo').blur(function () {

            //var reg = /^(d{2}[A-Z]{5}\d{4}[A-Z]{1}[A-Z\d]{1}[Z]{1}[A-Z\d]{1})$/;
            //var reg = /^(\d{2})([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})(\d{1})([a-zA-Z]{1})(\d{1})$/;
            var reg = /^(\d{2})([A-Z]{5})(\d{4})([A-Z]{1})([0-9A-Z]{1})([Z]{1})([0-9A-Z]{1})$/;
            if (document.getElementById('txtGSTNo').value != "") {
                if (reg.test(document.getElementById('txtGSTNo').value) == false) {
                    alert("Invalid GST Number.");
                    document.getElementById('txtGSTNo').value = "";
                }
            }

        });

        //===========GST NUMBER VALIDATION END====================
        $('.PanCard').blur(function () {
            debugger;
            var Obj = $('.PanCard').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                //var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
                var panPat = /^([A-Z]{5})(\d{4})([A-Z]{1})$/;
                var code = /([C,P,H,F,A,T,B,L,J,G])/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(panPat) == -1) {
                    alert("Invalid Pan No");
                    //message_error("Error", "Invalid Pan Card.");
                    //Obj.focus();
                    $('.PanCard').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild PAN Card No.");
                    //message_error("Error", "Invalid Pan Card.");
                    $('.PanCard').val('');
                    return false;
                }
            }
        });
        $('.PinCode').blur(function () {
            var Obj = $('.PinCode').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                var panPat = /^([1-9]{1})([0-9]{5})$/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(panPat) == -1) {
                    alert("Invalid PinCode");
                    //message_error("Error", "Invalid Pan Card.");
                    //Obj.focus();
                    $('.PinCode').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild PinCode.");
                    //message_error("Error", "Invalid Pan Card.");
                    $('.PinCode').val('');
                    return false;
                }
            }
        });
        $('.IFSC').blur(function () {
            debugger;
            var Obj = $('.IFSC').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                //var IFSC = /^[A-Za-z]{4}[0]{1}[0-9A-Za-z]{6}$/;
                var IFSC = /^[A-Z]{4}[0]{1}[0-9A-Z]{6}$/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(IFSC) == -1) {
                    alert("Invalid IFSC Code");
                    //message_error("Error", "Invalid IFSC Code.");
                    //Obj.focus();
                    $('.IFSC').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild IFSC Code.");
                    //message_error("Error", "Invalid IFSC Code.");
                    $('.IFSC').val('');
                    return false;
                }

                if ($('.IFSC').val.length != 11) {
                    alert("Invaild IFSC Code.");
                    return false;
                }
            }
        });
        $('.MobileNo').blur(function () {
            debugger;
            var Obj = $('.MobileNo').val();
            if (Obj == null) Obj = window.event.srcElement;
            if (Obj != "") {
                ObjVal = Obj;
                var MobileNo = /^[6-9]{1}[0-9]{9}$/;
                var code_chk = ObjVal.substring(3, 4);
                if (ObjVal.search(MobileNo) == -1) {
                    alert("Invalid Mobile No.");
                    //message_error("Error", "Invalid IFSC Code.");
                    //Obj.focus();
                    $('.MobileNo').val('');
                    return false;
                }
                if (code.test(code_chk) == false) {
                    alert("Invaild Mobile No.");
                    //message_error("Error", "Invalid IFSC Code.");
                    $('.MobileNo').val('');
                    return false;
                }
            }
        });

        function ShowModal() {
            $('#myModal').modal('show');
            return true;
        }
        function ShowBillDetailModal() {
            $('#myModal').modal('show');
            $("#ddlBillByBillTx_Ref").hide();
        }
        function ShowRefDetailModal() {
            $('#AgstRefModal').modal('show');
        }


        function validateform() {
            debugger;
            var msg = "";
            $("#valddlHeadName").html("");
            $("#valtxtLedgerName").html("");
            $("#valddltypeofsupply").html("");
            $("#valddlTaxability").html("");
            $("#valddlHSNCode").html("");
            $("#valddlBalBillByBill").html("");
            $("#valddlRegistrationType").html("");
            $("#valddlState").html("");
            if (document.getElementById('<%=txtVoucherTx_Date.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Date . \n";
                $("#valtxtVoucherTx_Date").html("Enter Date ");
            }
            if (document.getElementById('<%=txtLedgerName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Ledger Name. \n";
                $("#valtxtLedgerName").html("Enter Ledger Name");
            }
            if (document.getElementById('<%=ddlHeadName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Group Name. \n";
                $("#valddlHeadName").html("Select Group Name");
            }
            if (document.getElementById('<%=txtLedgerCode.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Ledger Code. \n";
                $("#valtxtLedgerCode").html("Enter Ledger Code");
            }
            if (document.getElementById('<%=ddlType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Type. \n";
                $("#valddlType").html("Select Type");
            }
            if (document.getElementById('<%=ddlHeadName.ClientID%>').selectedIndex > 0) {

                var HiddenValue = '<%=hfvalue.Value%>';
                if (HiddenValue == "true") {
                    if (document.getElementById('<%=ddlState.ClientID%>').selectedIndex == 0) {
                        msg = msg + "Select State. \n";
                        $("#valddlState").html("Select State");
                    }
                    if (document.getElementById('<%=ddlRegistrationType.ClientID%>').selectedIndex == 0) {
                        msg = msg + "Select Registration Types. \n";
                        $("#valddlRegistrationType").html("Registration Types");
                    }
                }

            }
            if (document.getElementById('<%=ddlRegistrationType.ClientID%>').selectedIndex > 0) {
                if (document.getElementById('<%=ddlRegistrationType.ClientID%>').value.trim() == "Composition" || document.getElementById('<%=ddlRegistrationType.ClientID%>').value.trim() == "Regular") {
                    //document.getElementById('gstvisible').style.display = 'block';
                    <%--if (document.getElementById('<%=txtGSTNo.ClientID%>').value.trim() == "") {
                        msg = msg + "Enter GST No. \n";
                        $("#valtxtGSTNo").html("Enter GST No");
                    }--%>


                }
                else {
                    $("#valtxtGSTNo").html("");
                }

            }
            if (document.getElementById('<%=ddlGSTApplicable.ClientID%>').selectedIndex == 0) {
                if (document.getElementById('<%=ddltypeofsupply.ClientID%>').selectedIndex == 0) {
                    msg = msg + "Select Type Of Supply. \n";
                    $("#valddltypeofsupply").html("Select Type Of Supply");

                }
                if (document.getElementById('<%=ddlHSNCode.ClientID%>').selectedIndex == 0) {
                    if (document.getElementById('<%=ddltypeofsupply.ClientID%>').selectedIndex == 2) {
                        msg = msg + "Select SAC Code. \n";
                        $("#valddlHSNCode").html("Select SAC Code");
                    }
                    else {
                        msg = msg + "Select HSN Code. \n";
                        $("#valddlHSNCode").html("Select HSN Code");
                    }

                }
                if (document.getElementById('<%=ddlTaxability.ClientID%>').selectedIndex == 0) {
                    msg = msg + "Select Taxability. \n";
                    $("#valddlTaxability").html("Select Taxability");

                }
            }
            if (document.getElementById('<%=ddlBalBillByBill.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Maintain Balances BillByBill.\n";
                $("#valddlBalBillByBill").html("Select Maintain Balances BillByBill");
            }
            if (document.getElementById('<%=txtOpeningBalance.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Opening Balance.\n";
                $("#valtxtOpeningBalance").html("Enter Opening Balance");
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save & Next") {
                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true;
                }
            }
        }
        $('.Email').blur(function () {
            debugger;
            //var filter = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-])+\.([A-Za-z]{2,4})$/;
            var filter = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-])+\.([A-Za-z]{2,4})$/;
            if ($('.Email').val() != "") {
                if (!filter.test($('.Email').val())) {
                    //alert('Please provide a valid email address');
                    alert("Please provide a valid email address");
                    //$('.Email').focus();
                    $('.Email').val("");
                    return false;
                }
            }
        });
        $('input').keydown(function (e) {
            var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
            if (key == 13) {
                e.preventDefault();
                var inputs = $(this).closest('form').find(':input:visible');
                inputs.eq(inputs.index(this) + 1).focus();
            }
        });
        function allowNegativeNumber(e) {
            var charCode = (e.which) ? e.which : event.keyCode
            if (charCode > 31 && (charCode < 45 || charCode > 57)) {
                return false;
            }
            return true;

        }


        function fillAcntholdername() {
            debugger;
            var LedgerName = document.getElementById('<%=txtLedgerName.ClientID%>').value.trim();
            $("#txtachlder_name").val(LedgerName);
            $("#hfachlder_name").val(LedgerName);
            document.getElementById('<%=txtachlder_name.ClientID%>').value = LedgerName;


        }
        $(document).ready(function () {
            ShowhideGSTDetails();
            var TypeofSupply = document.getElementById('<%=ddltypeofsupply.ClientID%>').selectedIndex;
            if (TypeofSupply == "2") {
                document.getElementById('<%=lblHSN.ClientID%>').innerHTML = "SAC Code";
            }
            else {
                document.getElementById('<%=lblHSN.ClientID%>').innerHTML = "HSN Code";
            }
        });
        function ShowhideGSTDetails() {
            debugger;
            var Val = document.getElementById('<%=ddlGSTApplicable.ClientID%>').value.trim();
            if (Val == "Yes") {
                document.getElementById('<%=gstdetails.ClientID %>').style.display = 'block';

            }
            else {
                document.getElementById('<%=gstdetails.ClientID %>').style.display = 'none';
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

                        url: '<%=ResolveUrl("LedgerMasterB.aspx/SearchCustomers") %>',
                        data: "{ 'Ledger_Name': '" + $('#txtLedgerName').val() + "'}",
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

    <script src="../js/ValidationJs.js"></script>

</asp:Content>




