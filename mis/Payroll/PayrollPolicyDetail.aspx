<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollPolicyDetail.aspx.cs" Inherits="mis_Payroll_PayrollPolicyDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .PaddingLeftRight {
            padding-left: 5px;
            padding-right: 5px;
        }

        .PaddingRight {
            padding-right: 5px;
        }

        .PaddingLeft {
            padding-left: 5px;
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
                    <h3 class="box-title">Add Policy Details</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Employee Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Policy Type<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlPolicy_Type" runat="server" class="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="LIC">LIC</asp:ListItem>
                                    <asp:ListItem Value="Other">Other</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Policy No<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtPolicy_No" runat="server" CssClass="form-control" placeholder="Enter Policy No..." MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Policy Name<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtPolicy_Name" runat="server" CssClass="form-control" placeholder="Enter Policy Name..." MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Premium Amount<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtPolicy_PremiumAmount" runat="server" CssClass="form-control" placeholder="Enter Premium Amount..." onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Premium Frequency<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlPolicy_PremiumFrequency" runat="server" class="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="Monthly">Monthly</asp:ListItem>
                                    <asp:ListItem Value="Quarterly">Quarterly</asp:ListItem>
                                    <asp:ListItem Value="Half Yearly">Half Yearly</asp:ListItem>
                                    <asp:ListItem Value="Yearly">Yearly</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Policy Start Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <%--<asp:TextBox ID="txtOrderDate" runat="server" placeholder="Select Date..." class="form-control DateAdd"  data-date-end-date="0d" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>--%>
                                    <asp:TextBox ID="txtPolicy_StartDate" runat="server" placeholder="Select Start Date..." class="form-control DateAdd" autocomplete="off"   onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Policy End Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtPolicy_EndDate" runat="server" placeholder="Select End Date..." class="form-control DateAdd" autocomplete="off"  onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                    <%--<asp:TextBox ID="txtEffectiveDate" runat="server" placeholder="Select Effective Date..." class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>--%>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Status<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlPolicy_IsActive" runat="server" class="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Close</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1 PaddingRight">
                            <div class="form-group">
                                <label>JAN</label>
                                <asp:TextBox ID="txtPolicy_JanAmount" runat="server" CssClass="form-control" placeholder="Amount..." onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 PaddingLeftRight">
                            <div class="form-group">
                                <label>FEB</label>
                                <asp:TextBox ID="txtPolicy_FebAmount" runat="server" CssClass="form-control" placeholder="Amount..." onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 PaddingLeftRight">
                            <div class="form-group">
                                <label>MAR</label>
                                <asp:TextBox ID="txtPolicy_MarAmount" runat="server" CssClass="form-control" placeholder="Amount..." onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 PaddingLeftRight">
                            <div class="form-group">
                                <label>APR</label>
                                <asp:TextBox ID="txtPolicy_AprAmount" runat="server" CssClass="form-control" placeholder="Amount..." onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 PaddingLeftRight">
                            <div class="form-group">
                                <label>MAY</label>
                                <asp:TextBox ID="txtPolicy_MayAmount" runat="server" CssClass="form-control" placeholder="Amount..." onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 PaddingLeftRight">
                            <div class="form-group">
                                <label>JUN</label>
                                <asp:TextBox ID="txtPolicy_JunAmount" runat="server" CssClass="form-control" placeholder="Amount..." onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 PaddingLeftRight">
                            <div class="form-group">
                                <label>JUL</label>
                                <asp:TextBox ID="txtPolicy_JulAmount" runat="server" CssClass="form-control" placeholder="Amount..." onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 PaddingLeftRight">
                            <div class="form-group">
                                <label>AUG</label>
                                <asp:TextBox ID="txtPolicy_AugAmount" runat="server" CssClass="form-control" placeholder="Amount..." onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 PaddingLeftRight">
                            <div class="form-group">
                                <label>SEP</label>
                                <asp:TextBox ID="txtPolicy_SepAmount" runat="server" CssClass="form-control" placeholder="Amount..." onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 PaddingLeftRight">
                            <div class="form-group">
                                <label>OCT</label>
                                <asp:TextBox ID="txtPolicy_OctAmount" runat="server" CssClass="form-control" placeholder="Amount..." onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 PaddingLeftRight">
                            <div class="form-group">
                                <label>NOV</label>
                                <asp:TextBox ID="txtPolicy_NovAmount" runat="server" CssClass="form-control" placeholder="Amount..." onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 PaddingLeft">
                            <div class="form-group">
                                <label>DEC</label>
                                <asp:TextBox ID="txtPolicy_DecAmount" runat="server" CssClass="form-control" placeholder="Amount..." onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" style="margin-top: 23px;" href="PayrollPolicyDetail.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="Policy_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Policy_Type" HeaderText="Policy Type" />
                                    <asp:BoundField DataField="Policy_No" HeaderText="Policy No" />
                                    <asp:BoundField DataField="Policy_PremiumAmount" HeaderText="Policy Premium Amount" />
                                    <asp:BoundField DataField="Policy_PremiumFrequency" HeaderText="Policy Premium Frequency" />
                                    <asp:BoundField DataField="Policy_StartDate" HeaderText="Policy Start Date" />
                                    <asp:BoundField DataField="Policy_EndDate" HeaderText="Policy End Date" />
                                    <asp:BoundField DataField="Policy_IsActive" HeaderText="Status" />
                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Select" runat="server" CssClass="label label-default" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
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
    <script type="text/javascript">
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlOfficeName.ClientID%>').selectedIndex == 0) {
                msg += "Select Office Name. \n";
            }
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg += "Select Employee Name. \n";
            }
            if (document.getElementById('<%=ddlPolicy_Type.ClientID%>').selectedIndex == 0) {
                msg += "Select Policy Type. \n";
            }
            if (document.getElementById('<%=txtPolicy_No.ClientID%>').value.trim() == "") {
                msg += "Enter Policy No. \n";
            }
            if (document.getElementById('<%=txtPolicy_Name.ClientID%>').value.trim() == "") {
                msg += "Enter Policy Name. \n";
            }
            if (document.getElementById('<%=txtPolicy_PremiumAmount.ClientID%>').value.trim() == "") {
                msg += "Enter Premium Amount. \n";
            }
            else if (!objdb.isDecimal(txtPolicy_PremiumAmount.Text)) {
                msg += "Enter Correct Premium Amount. \n";
            }
            if (document.getElementById('<%=ddlPolicy_PremiumFrequency.ClientID%>').selectedIndex == 0) {
                msg += "Select Premium Frequency. \n";
            }
            if (document.getElementById('<%=txtPolicy_StartDate.ClientID%>').value.trim() == "") {
                msg += "Enter Policy Start Date. \n";
            }
            if (document.getElementById('<%=txtPolicy_EndDate.ClientID%>').value.trim() == "") {
                msg += "Enter Policy End Date. \n";
            }
            if (document.getElementById('<%=ddlPolicy_IsActive.ClientID%>').selectedIndex == 0) {
                msg += "Select Status. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details, ?")) {
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

        $('#txtPolicy_StartDate').change(function () {
            debugger;
            var start = $('#txtPolicy_StartDate').datepicker('getDate');
            var end = $('#txtPolicy_EndDate').datepicker('getDate');

            if ($('#txtPolicy_EndDate').val() != "") {
                if (start > end) {

                    if ($('#txtPolicy_StartDate').val() != "") {
                        alert("Policy start date can not be less than Policy End Date.");
                        $('#txtPolicy_StartDate').val("");
                    }
                }
            }
        });
        $('#txtPolicy_EndDate').change(function () {
            debugger;
            var start = $('#txtPolicy_StartDate').datepicker('getDate');
            var end = $('#txtPolicy_EndDate').datepicker('getDate');

            if (start > end) {

                if ($('#txtPolicy_EndDate').val() != "") {
                    alert("Policy End Date can not be greater than Policy start Date.");
                    $('#txtPolicy_EndDate').val("");
                }

            }
        });
    </script>
</asp:Content>

