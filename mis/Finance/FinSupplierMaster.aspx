<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinSupplierMaster.aspx.cs" Inherits="mis_Finance_FinSupplierMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Supplier Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Name Of The Bank<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNameOfTheBank" placeholder="Enter Name Of The Bank..." MaxLength="200" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Name Of The Branch<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNameOfTheBranch" placeholder="Enter Name Of The Branch..." MaxLength="200"  AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Name Of Account Holder<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNameOfAccountHolder" placeholder="Enter Name Of Account Holder..." MaxLength="200"  AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Ac. No. As On Chk. Book<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtAcNoAsOnChkBook" placeholder="Enter Ac. No. As On Chk. Book..." MaxLength="20"  onkeypress="return validateNum(event);" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>IFSC<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtIFSC" placeholder="Enter IFSC..." MaxLength="16"  onkeypress="javascript:tbx_fnAlphaOnly(event, this);" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Bank Address</label>
                                         <asp:TextBox runat="server" CssClass="form-control" ID="txtBankAddress" placeholder="Enter Bank Address..." MaxLength="500" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                             <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Supplier Address<span style="color: red;"> *</span></label>                                       
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtAddress" placeholder="Enter Supplier Address..." MaxLength="500"  AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>State<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Email ID</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtEmailID" placeholder="Enter Email ID..." MaxLength="50"  AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Mobile No.</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtMobileNo" placeholder="Enter Mobile No..." MaxLength="13"  onkeypress="return validateNum(event);" AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>GST No.<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtGSTNo" placeholder="Enter GST No..." MaxLength="16"  AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>PAN No.</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtPANNo" placeholder="Enter PAN No..." MaxLength="13"  AutoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="FinSupplierMaster.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                           
                            <div class="row">
                                <div class="col-md-12">
                                     <h3 class="box-title">Supplier Details</h3>
                                    <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ID").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NameOfTheBank" HeaderText="Name Of The Bank" />
                                            <asp:BoundField DataField="NameOfTheBranch" HeaderText="Name Of The Branch" />
                                            <asp:BoundField DataField="NameOfAccountHolder" HeaderText="Name Of Account Holder" />
                                            <asp:BoundField DataField="AcNoAsOnChkBook" HeaderText="Ac. No. As On Chk. Book" />
                                            <asp:BoundField DataField="IFSC" HeaderText="IFSC" />
                                            <asp:BoundField DataField="GSTNo" HeaderText="GST No." />
                                            <asp:BoundField DataField="Address" HeaderText="Address" />
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Select" runat="server" CssClass="label label-info" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">


        function validateform() {
            var msg = "";
            if (document.getElementById('<%=txtNameOfTheBank.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Name Of The Bank. \n";
            }
            if (document.getElementById('<%=txtNameOfTheBranch.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Name Of The Branch. \n";
            }
            if (document.getElementById('<%=txtNameOfAccountHolder.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Name Of Account Holder. \n";
            }
            if (document.getElementById('<%=txtAcNoAsOnChkBook.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Ac. No. As On Chk. Book. \n";
            }
            if (document.getElementById('<%=txtIFSC.ClientID%>').value.trim() == "") {
                msg = msg + "Enter IFSC. \n";
            }
            if (document.getElementById('<%=txtAddress.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Supplier Address. \n";
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
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
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


