<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREarn_DeductionMaster.aspx.cs" Inherits="mis_HR_HREarningAndDecductionMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-5">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Earning & Deduction Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Year<span style="color: red;"> *</span></label>
                                       <asp:DropDownList ID="ddlEarnDeduction_Year" runat="server" CssClass="form-control" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlEarnDeduction_Year_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                    </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Type<span style="color: red;"> *</span></label>
                                       <asp:DropDownList ID="ddlEarnDeduction_Type" runat="server" CssClass="form-control" AutoPostBack="true" ClientIDMode="Static" OnSelectedIndexChanged="ddlEarnDeduction_Type_SelectedIndexChanged">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                           <asp:ListItem Value="Earning">Earning</asp:ListItem>
                                           <asp:ListItem Value="Deduction">Deduction</asp:ListItem>
                                       </asp:DropDownList>
                                    </div>
                                    </div>
                                    <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Name<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control text-uppercase" ID="txtEarnDeduction_Name" onkeypress="javascript:tbx_fnAlphaOnly(event, this);" placeholder="Enter Name" autocomplete="off" MaxLength="100"></asp:TextBox>
                                    </div>
                                         </div>
                                
                                </div>
                                 <div class="row">
                                     <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Calculation<span style="color: red;"> *</span></label>
                                       <asp:DropDownList ID="ddlEarnDeduction_Calculation" runat="server" CssClass="form-control" ClientIDMode="Static">
                                           <asp:ListItem Value="Select">Select</asp:ListItem>
                                           <asp:ListItem Value="Amount(Rs)">Amount(Rs)</asp:ListItem>
                                           <asp:ListItem Value="Percentage(%)">Percentage(%)</asp:ListItem>
                                       </asp:DropDownList>
                                    </div>
                                    </div>
                                    <%--<div class="col-md-12">
                                    <div class="form-group">
                                        <label>Is Active<span style="color: red;"> *</span></label>
                                        <asp:CheckBox ID="chkEarnDeduction_IsActive" runat="server" Checked="true" />
                                    </div>
                                </div>--%>
                                 </div>
                                 
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" OnClientClick="return validateform()" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                                <div class="col-md-3 ">
                                    <div class="form-group">
                                        <a href="HREarn_DeductionMaster.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                            
                        </div>
                    </div>
                </div>

                <!--Grid-->

                <div class="col-md-7">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Earning & Deduction Detail</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblRecord" runat="server" Text="" ForeColor="Red"></asp:Label>
                                    <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true" 
                                                  AutoGenerateColumns="False" DataKeyNames="EarnDeduction_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("EarnDeduction_ID").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Financial_Year" HeaderText="Year" />
                                            <asp:BoundField DataField="EarnDeduction_Type" HeaderText="Type" />
                                            <asp:BoundField DataField="EarnDeduction_Name" HeaderText="Name" />
                                             <asp:BoundField DataField="EarnDeduction_Calculation" HeaderText="Calculation" />
                                             
                                            <asp:TemplateField HeaderText="Edit" ShowHeader="False">
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
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script>

  
    function tbx_fnAlphaOnly(e, cntrl) {
            if (!e) e = window.event; if (e.charCode) {
                if (e.charCode < 65 || (e.charCode > 90 && e.charCode < 97) || e.charCode > 122)
                { if (e.charCode != 95 && e.charCode != 32) { if (e.preventDefault) { e.preventDefault(); } } }
            } else if (e.keyCode) {
                if (e.keyCode < 65 || (e.keyCode > 90 && e.keyCode < 97) || e.keyCode > 122)
                { if (e.keyCode != 95 && e.keyCode != 32) { try { e.keyCode = 0; } catch (e) { } } }
            }
    }
          </script>
    <script type="text/javascript">
        function validateform() {
            debugger
            var msg = "";
            if (document.getElementById("ddlEarnDeduction_Year").selectedIndex == 0) {
                msg += "Select Year\n";
            }
            if (document.getElementById("ddlEarnDeduction_Type").selectedIndex == 0) {
                msg += "Select Type\n";
            }
            if (document.getElementById('<%=txtEarnDeduction_Name.ClientID%>').value.trim() == "") {
                
                msg = msg + "Enter Name. \n";
            }
            if (document.getElementById("ddlEarnDeduction_Calculation").selectedIndex == 0) {
                msg += "Select Calculation\n";
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

