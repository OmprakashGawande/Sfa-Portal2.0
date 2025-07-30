<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BankMaster.aspx.cs" Inherits="mis_Finance_BankMaster" %>

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
                            <h3 class="box-title">Bank Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Name of the Bank<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtBankName" onkeypress="javascript:tbx_fnAlphaOnly(event, this);" placeholder="Enter Bank Name" MaxLength="100"></asp:TextBox>
                                        <small><span id="valtxtBankName" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>A/c No<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" placeholder="Enter A/c No" ID="txtacntno" CssClass="form-control" MaxLength="18" onkeypress="return validateNum(event);"></asp:TextBox>
                                        <small><span id="valtxtacntno" style="color: red;"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>IFSC Code(CAPITAL LETTERS ONLY)<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" ID="txtifsccode" placeholder="Example: SBIN0000058" CssClass="form-control IFSC" MaxLength="12" onkeypress="return alpha(event);"></asp:TextBox>
                                        <small><span id="valtxtifsccode" style="color: red;"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSave" Text="Accept" OnClientClick="return validateform();" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="BankMaster.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <div class="table-responsive">
                                <asp:GridView runat="server" CssClass="table table-bordered" EmptyDataText="No Record Found" DataKeyNames="ID" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" ID="GridView1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.NO" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Bank_Name" HeaderText="Name of the Bank" />
                                        <asp:BoundField DataField="Acnt_No" HeaderText="A/c No" />
                                        <asp:BoundField DataField="IFSC" HeaderText="IFSC Code" />
                                         <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" Text="Edit" CssClass="label label-default" runat="server" CausesValidation="false" CommandName="Select"></asp:LinkButton>
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
                </div>
                
            </div>
        </section>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function validateform()
        {
            var msg = "";
            $("#valtxtBankName").html("");
            $("#valtxtacntno").html("");
            $("#valtxtifsccode").html("");
            if (document.getElementById('<%= txtBankName.ClientID%>').value.trim() == "")
            {
                msg += "Enter Name of the Bank.\n";
                $("#valtxtBankName").html("Enter Name of the Bank");
            }
            if (document.getElementById('<%= txtacntno.ClientID%>').value.trim() == "") {
                msg += "Enter A/c No.\n";
                $("#valtxtacntno").html("Enter A/c No");
            }
            if (document.getElementById('<%= txtifsccode.ClientID%>').value.trim() == "") {
                msg += "Enter IFSC Code.\n";
                $("#valtxtifsccode").html("Enter IFSC Code");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }
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
    </script>
</asp:Content>
