<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="mis_Finance_Default2" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Ledger Master</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <fieldset>
                        <legend>Ledger Detail</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Head Name<span style="color: red;"> *</span></label>
                                    <asp:DropDownList runat="server" CssClass="form-control select2" ID="ddlHeadName" AutoPostBack="true">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <label>Ledger Name<span style="color: red;"> *</span></label>
                                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Ledger Name" ID="txtLedgerName" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Alias<span style="color: red;"> *</span></label>
                                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Alias" ID="TextBox2" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>


                        </div>

                    </fieldset>

                    <fieldset>
                        <legend>Other Detail</legend>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Name<span style="color: red;"> *</span></label>
                                    <asp:TextBox runat="server" placeholder="Enter Mailing Name" ID="txtMailing_Name" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>State <span style="color: red;">*</span></label>
                                    <asp:DropDownList runat="server" CssClass="form-control">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Address <span style="color: red;">*</span></label>
                                    <asp:TextBox runat="server" ID="txtMailing_Address" placeholder="Enter Mailing Address" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>GST No. <span style="color: red;">*</span></label>
                                    <asp:TextBox runat="server" placeholder="Enter GST No." ID="TextBox1" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>

                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>PAN No. <span style="color: red;">*</span></label>
                                    <asp:TextBox runat="server" placeholder="Enter Mailing Pan No" ID="txtMailing_PanNo" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Inventory Value are affected<span style="color: red;"> *</span></label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlInventoryAffected">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Maintain Ledger Bill by Bill<span style="color: red;"> *</span></label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="DropDownList1">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Financial Year <span style="color: red;">*</span></label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlFinancialYear">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Opening Balance <span style="color: red;">*</span></label>
                                    <asp:TextBox runat="server" placeholder="Enter Opening Balance" CssClass="form-control" onblur="return ShowModal();"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>CR / DR Status <span style="color: red;">*</span></label>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlCRDRStatus">
                                        <asp:ListItem>Select</asp:ListItem>
                                        <asp:ListItem>Credit</asp:ListItem>
                                        <asp:ListItem>Debit</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>

                    </fieldset>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">

                                <fieldset>
                                    <legend>Applicable on</legend>
                                    <asp:CheckBoxList runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem>Head Office &nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem>Regional Office&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem>District Office&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem>Production Unit&nbsp;&nbsp;&nbsp;</asp:ListItem>

                                    </asp:CheckBoxList>
                                </fieldset>

                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-block btn-success" Text="Accept"  OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a href="LedgerMaster.aspx" class="btn btn-block btn-default">Clear</a>
                            </div>
                        </div>
                    </div>
                    <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Ledger_ID").ToString()%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Ledger_Name" HeaderText="Ledger_Name" />
                            <%--   <asp:BoundField DataField="Office_ContactNo" HeaderText="Office Name" />
                            <asp:BoundField DataField="Office_Email" HeaderText="Office Email" />
                            <asp:BoundField DataField="OfficeType_Title" HeaderText="Office Type Title" />
                            <asp:BoundField DataField="Office_Address" HeaderText="Office Address" />
                            <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </section>
        <!-- Modal -->
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-lg">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Opening Balance Detail</h4>
                    </div>
                    <div class="modal-body">
                        <fieldset>
                            <legend>Opening Balance Detail</legend>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date</label>
                                        <asp:TextBox runat="server" CssClass="form-control" placeholder="Select Date"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Bill No. / Particular</label>
                                        <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Bill No. / Particular"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Item Detail</label>
                                        <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Item Detail"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Opening Balance</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtOpeningBalance" placeholder="Enter Opening Balance"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <label>Dr/Cr</label>
                                        <asp:RadioButtonList runat="server">
                                            <asp:ListItem Selected="True">Dr</asp:ListItem>
                                            <asp:ListItem>Cr</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-10"></div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-info btn-block" Text="Add" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <table class="table table-bordered table-striped table-hover">
                                            <tr>
                                                <th>SNo.</th>
                                                <th>Bill No. / Particular</th>
                                                <th>Item Detail</th>
                                                <th>Opening Balance</th>
                                                <th>Dr/Cr</th>
                                            </tr>
                                            <%-- <tr>
                                                <td colspan="4">Total</td>
                                                <td>0</td>
                                            </tr>--%>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function ShowModal() {
            $('#myModal').modal('show');
            return true;
        }
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlHeadName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Head Name. \n";
            }
            if (document.getElementById('<%=txtLedgerName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Ledger Name. \n";
            }
            if (document.getElementById('<%=ddlInventoryAffected.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Inventory Value are Affected. \n";
            }
            if (document.getElementById('<%=txtMailing_Name.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Mailing Name. \n";
            }
            if (document.getElementById('<%=txtMailing_Address.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Mailing Address. \n";
            }
            if (document.getElementById('<%=txtMailing_PanNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Mailing Pan No. \n";
            }
            if (document.getElementById('<%=ddlFinancialYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Financial Year. \n";
            }
            if (document.getElementById('<%=txtOpeningBalance.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Opening Balance. \n";
            }
            if (document.getElementById('<%=ddlCRDRStatus.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select CR / DR Status. \n";
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
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Edit") {
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


