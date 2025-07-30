<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MisProgressiveReport.aspx.cs" Inherits="mis_Finance_MisProgressiveReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h1 class="box-title">MIS Progressive</h1>
                    <br />
                    <br />
                    <p style="color: blue"><b>नोट :</b> कृपया केवल चयनित महीने का ही टर्नओवर भरें | </p>
                    <br />
                    <asp:Label ID="lblMsg" Text="" runat="server"></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office</label>
                                <asp:DropDownList ID="ddlOffice" runat="server" CssClass="form-control Select2" Enabled="false"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Year</label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control Select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Month</label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control Select2">
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
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success" Text="Search" Style="margin-top: 25px;" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.N0" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex +1 %>'></asp:Label>
                                            <asp:Label CssClass="hidden" ID="lblMisID" runat="server" Text='<%# Eval("Mis_ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PARTICULARS">
                                        <ItemTemplate>

                                            <asp:Label ID="lblParticularName" runat="server" Text='<%# Eval("Particular_Name") %>'></asp:Label>
                                            <asp:Label CssClass="hidden" ID="lblParticularID" runat="server" Text='<%# Eval("Particular_ID") %>'></asp:Label>
                                            <asp:Label CssClass="hidden" ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="C YEAR">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCYear" runat="server" required CssClass='<%# Eval("CalculationId") %>' Text='<%# Eval("Amount") %>' onkeypress="return allowNegativeNumber(event);"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <div class="row">
                                <div class="col-md-3"></div>
                                <div class="col-md-3"></div>
                                <div class="col-md-3"></div>
                                <div class="col-md-2 pull-right">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-block btn-success" Text="Save" OnClick="btnSave_Click" OnClientClick="validatesaveform();" />
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
        function validateform() {
            var msg = "";

            if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg += "Select  Office \n";

            }
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg += "Select Year \n";

            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg += "Select Month \n";

            }
            if (msg != "") {
                alert(msg);
                return false;

            }
            if (msg != "") {
                alert(msg);
                return false;

            }
            else {
                if (document.getElementById('<%=btnSearch.ClientID%>').value.trim() == "Search") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }


            }
        }
    </script>
    <script>

        function validatesaveform() {
            if (confirm("Do you really want to update details ?")) {
                return true;
            }
            else {
                return false;
            }
        }
        function allowNegativeNumber(e) {
            var charCode = (e.which) ? e.which : event.keyCode
            if (charCode > 31 && (charCode < 45 || charCode > 57)) {
                return false;
            }
            return true;

        }
        $('.Total,.Proloss,.TotalWithoutRte').parent().parent().css('background', 'lavender');

        $('.Group,.GroupRte,.OtherChargesContribution,.OtherChargesBranch,.OtherChargesHOShare').on('focusout', function () {
            //var RemainingArrearAmount = $(this).val();
            //var RemainingEpfAmount = (RemainingArrearAmount * 0.12);
            //var NetPayment = (RemainingArrearAmount) - (RemainingEpfAmount);
            //$(this).parent().parent().find('.txtRemainingEpfAmount').text(RemainingEpfAmount);
            //$(this).parent().parent().find('#hfRemainingEpfAmount').val(RemainingEpfAmount);
            //$(this).parent().parent().find('.txtNetPayment').text(NetPayment);
            //$(this).parent().parent().find('#hfNetPayment').val(NetPayment);

            calculateTotal();
        });

        function calculateTotal() {
            var grouptotal = 0;
            var groupwrte = 0;
            var proloss = 0;
            /****************/
            $('.Group').each(function () {
                grouptotal += Number($(this).val());
                groupwrte += Number($(this).val());
            });
            $('.GroupRte').each(function () {
                grouptotal += Number($(this).val());
            });
            /****************/
            //$('.txtRemainingEpfAmount').each(function () {
            // proloss += Number($(this).text());
            //});
            
            proloss = (Number($('.OtherChargesContribution').val()) - (Number($('.OtherChargesBranch').val()) + Number($('.OtherChargesHOShare').val())));

            /****************/
            $('.Total').val(grouptotal.toFixed(2));
            $('.TotalWithoutRte').val(groupwrte.toFixed(2));
            $('.Proloss').val(proloss.toFixed(2));

        }

        $("input").focus(function () {
                $(this).select();
        });

    </script>


</asp:Content>

