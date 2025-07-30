<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="BankMaster.aspx.cs" Inherits="mis_Masters_BankMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script src="../../js/jquery.min.js"></script>
    <script src="../../js/datatables.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".datatable").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="card card-primary">
                                <div class="card-header">
                                    <h3 class="card-title">Bank Master Entry</h3>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Bank Name<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="rfv1" ValidationGroup="a"
                                                        ErrorMessage="Enter Bank Name" Text="<i class='fa fa-exclamation-circle' title='Enter Bank Name !'></i>"
                                                        ControlToValidate="txtBankName" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="a" Display="Dynamic" runat="server" ControlToValidate="txtBankName"
                                                        ErrorMessage="Only alphabet allowed" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Only alphabet allowed. !'></i>"
                                                        SetFocusOnError="true" ValidationExpression="^[a-zA-Z\s]+$">
                                                    </asp:RegularExpressionValidator>
                                                </span>
                                                <asp:TextBox ID="txtBankName" CssClass="form-control" MaxLength="50" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Order No<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                        ErrorMessage="Enter Order No" Text="<i class='fa fa-exclamation-circle' title='Enter Order No!'></i>"
                                                        ControlToValidate="txtOrderNo" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                  
                     
                                                </span>
                                                <asp:TextBox ID="txtOrderNo" CssClass="form-control" MaxLength="5" runat="server" onkeypress="return validateNum(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Group No<span style="color: red;"> *</span></label>
                                                <span class="pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                        ErrorMessage="Enter Group No" Text="<i class='fa fa-exclamation-circle' title='Enter Group No !'></i>"
                                                        ControlToValidate="txtGroupNo" ForeColor="Red" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                   
                                                </span>
                                                <asp:TextBox ID="txtGroupNo" CssClass="form-control" MaxLength="5" runat="server" onkeypress="return validateNum(event)"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Active Status<span style="color: red;"> *</span></label><br />
                                                <asp:CheckBox runat="server" ID="chkIsActive" Checked="true" />
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-primary" ValidationGroup="a" Text="Submit" OnClick="btnSubmit_Click" AccessKey="S" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="container-fluid">
                                <div class="card card-primary">
                                    <div class="card-header">
                                        <h3 class="card-title">Bank Details List</h3>
                                    </div>
                                    <!-- /.box-header -->
                                    <div class="card-body">
                                        <div class="table-responsive">

                                            <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" CssClass="table table-bordered datatable1"
                                                EmptyDataText="No Record Found." OnRowDataBound="GridView1_RowDataBound" DataKeyNames="Bank_id">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bank Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbankid" Visible="false" runat="server" Text='<%# Eval("Bank_id") %>' />
                                                            <asp:Label ID="lblBankName" runat="server" Text='<%# Eval("BankName") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:BoundField DataField="OrderNo" HeaderText="Order No" />
                                                     <asp:BoundField DataField="GroupNo" HeaderText="Group No" />
                                                    <asp:BoundField DataField="IsActive" HeaderText="Active Status" />
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbtnEdit" runat="server" CssClass="button button-mini button-green" CommandName="EditRequest" CommandArgument='<%# Bind("Bank_id") %>'><i class="btn btn-info fa fa-edit"></i></asp:LinkButton>
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
</asp:Content>

