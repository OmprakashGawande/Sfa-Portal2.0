<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AddProject.aspx.cs" Inherits="mis_Daily_Task_AddProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <asp:HiddenField runat="server" ID="hfProjectID" />
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Project Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Project Name (In English)<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_ProjectsNameEng" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Project Name (In Hindi)<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_ProjectsNameHin" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <a href="AddProject.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="box box-success">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gridProject" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" DataKeyNames="Project_ID" OnRowCommand="gridProject_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Project_ID").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Prjoject Name (In English)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProject_Name_Eng" Text='<%# Eval("Project_Name_Eng").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Prjoject Name (In Hindi)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProject_Name_Hin" Text='<%# Eval("Project_Name_Hin").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-default" CommandArgument='<%# Eval("Project_ID").ToString()%>' CausesValidation="False" CommandName="EditRecord" Text="Edit"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30" HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkStatus" runat="server" CommandArgument='<%# Eval("Project_ID").ToString()%>' CssClass='<%# Eval("IsActive").ToString() =="True"?"label label-success":"label label-danger"  %>' CausesValidation="False" CommandName="ChangeStatus" Text='<%# Eval("IsActive").ToString() =="True"?"Active":"Deactive"  %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="30px"></ItemStyle>
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
            if (document.getElementById('<%=txt_ProjectsNameEng.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Project Name (In English). \n";
            }
            if (document.getElementById('<%=txt_ProjectsNameHin.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Project Name (In Hindi). \n";
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

