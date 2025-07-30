<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="addEmpGross.aspx.cs" Inherits="mis_HR_addEmpGross" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">

                <div class="box box-success">
                    <div class="box-header with-border">
                        <h3 class="box-title">Add gross salary</h3>
                    </div>
                    <asp:Label runat="server" ID="lblMsg"></asp:Label>
                    <div class="box-body">
                        <fieldset id="gridfieldset" runat="server">
                            <legend>Details</legend>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ClientIDMode="Static" ID="gridview" AutoGenerateColumns="false" class="table table-hover table-bordered table-striped pagination-ys" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="10" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAl(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10" HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="13" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="70" HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp Name ">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%#Eval("Emp_Name").ToString() %>'></asp:Label>
                                                        <asp:Label runat="server" ID="Emp_ID" Visible="false" Text='<%# Eval("Emp_ID").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="250"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Gross salary" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox runat="server" CssClass="field3 form-control" ID="Emp_GrossSalery" Text='<%# Eval("Emp_GrossSalery").ToString() %>' onkeypress="return isNumber(event);" AutoComplete="off" MaxLength="8" oninput="validate(this)"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <div class="row">
                            <div class="col-md-5"></div>
                            <div class="col-md-2 pt-4">
                                <asp:Button runat="server" ID="btnSave" CssClass="btn btn-success btn-block" Text="Save" OnClick="btnSave_Click" />
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
        function checkAl(objRef) {

            var GridV = objRef.parentNode.parentNode.parentNode;
            var inputList = GridV.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (inputList[i].disabled == false) {


                        if (objRef.checked) {
                            inputList[i].checked = true;
                        }
                        else {

                            inputList[i].checked = false;
                        }
                    }

                }

            }
        }
    </script>
</asp:Content>

