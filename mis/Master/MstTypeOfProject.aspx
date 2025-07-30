<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MstTypeOfProject.aspx.cs" Inherits="mis_Master_MstTypeOfProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Type Of Project Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                        ErrorMessage="Select To Date" ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select To Date !'></i>"
                                                        ControlToValidate="txtTypeOfProject" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label runat="server">TYPE OF PROJECT<span style="color: red;">*</span></label>
                                                <asp:TextBox runat="server" AutoComplete="off" onkeypress="return AlphabetOnly();" ID="txtTypeOfProject" placeholder="Enter Type Of Project" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label for="chkIsActive">STATUS </label><br />
                                                <asp:CheckBox ID="chkIsActive" runat="server" CssClass="form-check-input" />
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Button runat="server" Style="margin-top: 22px;" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" ValidationGroup="a" OnClick="btnSave_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <a href="MstTechnology.aspx" style="margin-top: 22px;" class="btn btn-block btn-default">Clear</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="card">
                                <div class="card-body">
                                    <div class="row" style="padding: 0px 9px 2px 15px;" id="div1" runat="server">
                                        <div>
                                            <h4>DETAIL </h4>
                                        </div>
                                        <div class="table-responsive">
                                            <div class="col-md-12">
                                                <asp:GridView ID="Grid" PageSize="50" runat="server" class="datatable  table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="false" AutoGenerateColumns="False" OnRowCommand="Grid_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("TypeOfProjectId").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TYPE OF PROJECT">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTechnologyName" Text='<%# Eval("TypeOfProjectName").ToString() %>' runat="server" />
                                                                <asp:Label ID="lblTechnologyID" Visible="false" runat="server" Text='<%# Eval("TypeOfProjectId").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-Width="30" HeaderText="STATUS">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkStatus" runat="server" CommandArgument='<%# Eval("TypeOfProjectId").ToString()%>' CssClass='<%# Eval("IsActive").ToString() =="True"?"label label-success":"label label-danger"  %>' CausesValidation="False" CommandName="ChangeStatus" Text='<%# Eval("IsActive").ToString() =="True"?"Active":"Deactive"  %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="30px"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ACTION">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-default" CommandArgument='<%# Eval("TypeOfProjectId").ToString()%>' CausesValidation="False" CommandName="EditRecord" Text="Edit"></asp:LinkButton>
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
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function AlphabetOnly() {
            var keyCode = event.keyCode;
            if ((keyCode < 65 || keyCode > 90) && (keyCode < 97 || keyCode > 123) && keyCode != 32)

                return false;
            return true;
        }
    </script>
</asp:Content>

