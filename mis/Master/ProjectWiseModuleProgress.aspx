<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ProjectWiseModuleProgress.aspx.cs" Inherits="mis_Master_ProjectWiseModuleProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">

     <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title" style="margin-left: 2rem;">Project Wise Module Progress</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RFV1" ValidationGroup="a"
                                                        ErrorMessage="Select To Date" ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Project Name !'></i>"
                                                        ControlToValidate="ddlProjectName" Display="Dynamic" runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label runat="server">PROJECT NAME <span style="color: red;">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlProjectName" ClientIDMode="Static" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" AutoPostBack="true"
                                                    CssClass="form-control select2">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RFV2" ValidationGroup="a"
                                                        ErrorMessage="Select Module" ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Module!'></i>"
                                                        ControlToValidate="ddlProjectModule" InitialValue="0" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label runat="server">MODULE <span style="color: red;">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlProjectModule" ClientIDMode="Static"
                                                    CssClass="form-control select2" >
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                         <div class="col-md-3">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                        ErrorMessage="Select Module Phase" ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Module Phase!'></i>"
                                                        ControlToValidate="ddlPhase" InitialValue="0" Display="Dynamic" runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label runat="server">MODULE PHASE <span style="color: red;">*</span></label>
                                                <asp:DropDownList runat="server" ID="ddlPhase" ClientIDMode="Static"
                                                    CssClass="form-control select2" >
                                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                                    <asp:ListItem  Value="1">Development Phase</asp:ListItem>
                                                    <asp:ListItem  Value="2">Testing Phase</asp:ListItem>
                                                    <asp:ListItem  Value="3">Production Phase</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>                                      
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>REMARK</label>
                                            
                                                <textarea
                                                    id="txtRemark"
                                                    runat="server"
                                                    class="form-control"
                                                    oninput="autoResizeTextarea(this)"                                                    
                                                    placeholder="Enter Remark" maxlength="150"></textarea>

                                            </div>
                                        </div>
                                    </div>                                  
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Button runat="server" Style="margin-top: 22px;" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" ValidationGroup="a" OnClick="btnSave_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <a href="ProjectWiseModuleProgress.aspx" style="margin-top: 22px;" class="btn btn-block btn-default">Clear</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />

                            <%--grid--%>
                            <div class="card">
                                <div class="card">
                                    <div class="row" style="padding: 0px 9px 2px 15px;" id="div1" runat="server">
                                        <div>
                                            <h4 style="margin-left: 2rem;">Detail </h4>
                                        </div>
                                        <div class="table-responsive">
                                            <div class="col-md-12">
                                                <asp:GridView ID="Grid" PageSize="50" runat="server" class="datatable  table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="false" AutoGenerateColumns="False" OnRowCommand="Grid_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PROJECT NAME">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProjectName" Text='<%# Eval("ProjectName").ToString() %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblProjectId" Text='<%# Eval("ProjectId").ToString() %>' Visible="false" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MODULE NAME ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTModuleName" Text='<%# Eval("ModuleName").ToString() %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MODULE PHASE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPhase" Text='<%# Eval("PhaseName").ToString() %>' runat="server"></asp:Label>
                                                                <asp:Label ID="lblPhaseId" Text='<%# Eval("ModulePhase").ToString() %>' runat="server" Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                       
                                                        <asp:TemplateField HeaderText="REMARK ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemark" Text='<%# Eval("Remark").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>        
                                                         <%--  <asp:TemplateField HeaderText="MODULE STATUS UPDATE DATE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDate" Text='<%# Eval("Date").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField> --%>
                                                        <asp:TemplateField HeaderText="ACTION">
                                                            <ItemTemplate>
                                                                <asp:LinkButton
                                                                    ID="lnkEdit"
                                                                    runat="server"
                                                                    CssClass="label label-default"
                                                                    CommandArgument='<%# Eval("ModulePhaseId").ToString() %>'
                                                                    
                                                                    CausesValidation="False"
                                                                    CommandName="EditRecord"
                                                                    Text="Edit">
                                                                </asp:LinkButton>
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">

     <script>
         $(document).ready(function () {
             $('.datatable').DataTable({
                 paging: true,
                 columnDefs: [{
                     targets: 'no-sort',
                     orderable: false
                 }],
                 "order": [[0, 'asc']],

                 dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
                     '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
                     '<"row"<"col-sm-5"i><"col-sm-7"p>>',
                 fixedHeader: {
                     header: true
                 },

                 buttons: {
                     buttons: [{
                         extend: 'print',
                         text: '<i class="fa fa-print"></i> Print',
                         title: 'Project Wise Module phase',
                         exportOptions: {
                             columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                         },
                         footer: true,
                         autoPrint: true
                     }, {
                         extend: 'excel',
                         text: '<i class="fa fa-file-excel-o"></i> Excel',
                         title: 'Project Wise Module phase',
                         exportOptions: {
                             columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]
                         },
                         footer: true
                     }],

                     dom: {
                         container: {
                             className: 'dt-buttons'
                         },
                         button: {
                             className: 'btn btn-default'
                         }
                     }
                 }
             });
             t.on('order.dt search.dt', function () {
                 t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                     cell.innerHTML = i + 1;
                 });
             }).draw();
         });
     </script>
</asp:Content>

