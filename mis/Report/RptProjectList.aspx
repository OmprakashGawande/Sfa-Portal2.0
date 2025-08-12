<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptProjectList.aspx.cs" Inherits="mis_Report_RptProjectList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <!-- Main content -->
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12">
                <div class="row">
                    <div class="co-md-10 justify-content-center">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></div>
                </div>
                <div class="card mt-3  ">
                    <div class="card-header">
                        <h4>Projects List </h4>
                    </div>
                    <hr />
                    <div class="card-body">



                        <div class="row">
                            <div class="col-md-3" style="margin-left: 1rem;">
                                <div class="form-group">
                                    <label>SELECT PROJECT TYPE</label>
                                    <asp:DropDownList ID="ddlTypeOfProject" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>

                            </div>

                        </div>
                    </div>
                     </div>
                    <div class="card">
                        <div class="card-header">
                           <h4>  Project Detail</h4>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <div class="col-md-12">
                                        <asp:GridView ID="Grid" PageSize="50" runat="server" class="datatable  table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="false" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" runat="server"
                                                            Text='<%# Container.DataItemIndex + 1 %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProjectName" runat="server"
                                                            Text='<%# Eval("ProjectName") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Type of Project">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTypeOfProject" runat="server"
                                                            Text='<%# Eval("TypeOfProjectName") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Start Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStartDate" runat="server"
                                                            Text='<%# Eval("ProjectStartDate") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="End Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEndDate" runat="server"
                                                            Text='<%# Eval("ProjectEndDate") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Duration (Days)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDuration" runat="server"
                                                            Text='<%# Eval("TotalNoOfDays") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Technology Used">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTechnology" runat="server"
                                                            Text='<%# Eval("TechnologyNames") %>' />
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>           
            $(document).ready(function () {
                $(document).ready(function () {
                    initCustomDataTable('.datatable', 'Project List', 'Project List Data');
                });

            });     
    </script>

</asp:Content>

