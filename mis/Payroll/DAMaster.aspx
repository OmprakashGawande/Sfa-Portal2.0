<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="DAMaster.aspx.cs" Inherits="mis_Payroll_DAMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box">
                <div class="box-header">
                    <h3 class="box-title">DA Rate Master</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <fieldset>
                        <legend>DA Rate Filter</legend>
                        <div class="row">
                          <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Year <span class="text-danger">*</span></label>
                                        <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Select">Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlFinancialYear" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Effective Month <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                            <asp:ListItem Value="0">Select Month</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlMonth" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Type of Post (पद प्रकार) <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlEmp_TypeOfPost" runat="server" class="form-control">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem Value="Permanent">Regular/Permanent</asp:ListItem>
                                            <asp:ListItem Value="Fixed Employee">Fixed Employee(स्थाई कर्मी)</asp:ListItem> 
                                        </asp:DropDownList>
                                        <small><span id="valddlEmp_TypeOfPost" class="text-danger"></span></small>
                                    </div>
                                </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>DA Rate<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtDARate" onkeypress="return validateNum(event);" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                            <small><span id="valtxtDARate" class="text-danger"></span></small>
                                 </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" style="margin-top:25px;" runat="server" CssClass="btn btn-success" OnClientClick="return ValidateForm();" OnClick="btnSave_Click" Text="Save"/>
                            </div>
                        </div>
                </div>
                    </fieldset>
                    <fieldset>
                        <legend>DA Rate Detail</legend>
                        <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="GvDetail" CssClass="table table-bordered" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNo" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Year" DataField="FinancialYear"/>
                                        <asp:BoundField HeaderText="Effective Month" DataField="Month"/>
                                        <asp:BoundField HeaderText="TypeofPost" DataField="TypeofPost"/>
                                        <asp:BoundField HeaderText="DARate" DataField="DARate"/>
                                     
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    </fieldset>
                    
            </div>
                </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script>
        function ValidateForm()
        {
            var msg = "";
            $("#valddlFinancialYear").html("");
            $("#valddlMonth").html("");
            $("#valddlEmp_TypeOfPost").html("");
            $("#valtxtDARate").html("");
            if(document.getElementById('<%= ddlFinancialYear.ClientID%>').selectedIndex == 0)
            {
                msg = msg + "Select year. \n";
                $("#valddlFinancialYear").html("Select year.");
            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
                $("#valddlMonth").html("Select Month.");
            }
            if (document.getElementById('<%=ddlEmp_TypeOfPost.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Type of Post. \n";
                $("#valddlEmp_TypeOfPost").html("Select Type of Post.");
            }
            if (document.getElementById('<%=txtDARate.ClientID%>').value =="") {
                msg = msg + "Enter DA Rate. \n";
                $("#valtxtDARate").html("Enter DA Rate.");
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
        }
    </script>
</asp:Content>

