<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollUpdateArrearsDAOnly.aspx.cs" Inherits="mis_Payroll_PayrollUpdateArrearsDAOnly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Generate DA Arrears </h3>
                    <p><strong>Note:</strong> From Jan 2019 to August 2019  Permanent Officers/Employee <span style="color: blue">(12 %)</span> , Fixed Employee <span style="color: blue">(154%)</span></p>  <p>
                        <asp:HyperLink ID="HyperLink1" Target="_blank" CssClass="badge bg-aqua" NavigateUrl="PayrollUpdateArrearsDAOnlyReport.aspx" runat="server">Click Here To View DA Report</asp:HyperLink></p>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Arrear Months<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlArrearMonths" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlArrearMonths_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Text="From Jan'19 To March'19" />
                                    <asp:ListItem Value="2" Text="From Apr'19 To Aug'19" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Employee<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true" ClientIDMode="Static" AutoGenerateColumns="False" ShowFooter="true">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" ToolTip='<%# Eval("CurrentMonthNo")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Month">
                                            <ItemTemplate>
                                                <asp:Label ID="ArrearMonth" runat="server" Text='<%# Eval("ArrearMonth") %>' ToolTip='<%# Eval("CurrentYear")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid BasicSalary">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBasicSalary" runat="server" Text='<%# Eval("BasicSalary") %>' CssClass="form-control" placeholder="Basic Salary" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid Basic Salary In Arrear">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtArrearBasicSalary" runat="server" Text='<%# Eval("BasicPaidInArrear") %>' CssClass="form-control" placeholder="Arrears Basic Salary" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Basic Salary Paid">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTotalBasicSalary" runat="server" Text='<%# Eval("TotalBasicPaid") %>' CssClass="form-control" placeholder="Total Basic Paid" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Paid DA">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPaidDa" runat="server" Text='<%# Eval("PaidDa") %>' CssClass="form-control" placeholder="Paid Da" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DA To Be Paid(As Per New Rate)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDaToBePaid" runat="server" Text='<%# Eval("DaToBePaid") %>' CssClass="form-control" placeholder="DA To Be Paid" ReadOnly="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DA Remaining Arrear Amount">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDaRemainingArrearAmount" runat="server" Text='<%# Eval("DaRemainingArrearAmount") %>' CssClass="form-control txtDaRemainingArrearAmount" onkeypress="return validateNum(event)" placeholder="DaRemainingArrearAmount" onpaste="return false;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remaining EPF Amount">
                                            <ItemTemplate>
<%--                                                <asp:TextBox ID="txtRemainingEpfAmount" runat="server" Text='<%# Eval("RemainingEpfAmount") %>' CssClass="form-control txtRemainingEpfAmount" placeholder="RemainingEpfAmount" onkeypress="return validateNum(event)" ReadOnly="true"></asp:TextBox>--%>
                                                <asp:Label ID="lblRemainingEpfAmount" runat="server" Text='<%# Eval("RemainingEpfAmount") %>' CssClass="form-control txtRemainingEpfAmount"></asp:Label>
                                                <asp:HiddenField ID="hfRemainingEpfAmount" runat="server" Value='<%# Eval("RemainingEpfAmount") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Net Payment">
                                            <ItemTemplate>
<%--                                                <asp:TextBox ID="txtNetPayment" runat="server" Text='<%# Eval("NetPayment") %>' CssClass="form-control txtNetPayment" placeholder="NetPayment" onkeypress="return validateNum(event)" ReadOnly="true"></asp:TextBox>--%>
                                                 <asp:Label ID="lblNetPayment" runat="server" Text='<%# Eval("NetPayment") %>' CssClass="form-control txtNetPayment"></asp:Label>
                                                
                                                <asp:HiddenField ID="hfNetPayment" runat="server" Value='<%# Eval("NetPayment") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-block" Text="Save" OnClick="btnSave_Click" />
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
        $('.txtDaRemainingArrearAmount').on('focusout', function () {
            var RemainingArrearAmount = $(this).val();
            //$(this).val(RemainingArrearAmount);
            //alert(RemainingArrearAmount);
            //var RemainingEpfAmount = $(this).parent().parent().find('.txtRemainingEpfAmount').text();
            //var NetPayment = $(this).parent().parent().find('.txtNetPayment').text();

            //RemainingEpfAmount = Math.round((RemainingArrearAmount * 0.12));
            //NetPayment = Math.round(RemainingArrearAmount) - Math.round(RemainingEpfAmount);

            var RemainingEpfAmount = (RemainingArrearAmount * 0.12);
            var NetPayment = (RemainingArrearAmount) - (RemainingEpfAmount);

            $(this).parent().parent().find('.txtRemainingEpfAmount').text(RemainingEpfAmount);
            $(this).parent().parent().find('#hfRemainingEpfAmount').val(RemainingEpfAmount);
            $(this).parent().parent().find('.txtNetPayment').text(NetPayment);
            $(this).parent().parent().find('#hfNetPayment').val(NetPayment);


            calculateTotal();

            //console.log($(this).val());
            //console.log($(this).parent().parent().find('.txtRemainingEpfAmount').val());
            //console.log($(this).parent().parent().find('.txtNetPayment').val());
        });
        function calculateTotal() {
            var dasum = 0;
            var epfsum = 0;
            var netsum = 0;

            $('.txtDaRemainingArrearAmount').each(function () {
                dasum += Number($(this).val());
            });

            $('.txtRemainingEpfAmount').each(function () {
                epfsum += Number($(this).text());
            });
            $('.txtNetPayment').each(function () {
                netsum += Number($(this).text());
            });


            $('.TotalDa').html(dasum);
            $('.TotalEpf').html(epfsum);
            $('.TotalNet').html(netsum);


        }
    </script>
</asp:Content>

