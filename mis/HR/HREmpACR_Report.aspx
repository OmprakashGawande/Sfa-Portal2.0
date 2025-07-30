<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpACR_Report.aspx.cs" Inherits="mis_HR_HREmpACR_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Employee ACR Detail</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" DataKeyNames="ACR_ID" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeader="true"  EmptyDataText="No Record Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="Year" HeaderText="Year" />
                                        <asp:BoundField DataField="From_Date" HeaderText="From Date" />
                                        <asp:BoundField DataField="To_Date" HeaderText="To Date" />
                                        <asp:TemplateField HeaderText="View Detail">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" CommandName="select" CssClass="label label-info" runat="server">View Detail</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Download">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbDownload" CommandArgument='<%# Eval("Attach_Doc") %>' CssClass="label label-info" runat="server" OnClick="lbDownload_Click">Download</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">ACR Detail</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:DetailsView ID="DetailsView1" class="table table-hover table-bordered" AutoGenerateRows="False" runat="server">
                                                <Fields>
                                                    <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="Department_Name" HeaderText="Department" />
                                                    <asp:BoundField DataField="Designation_Name" HeaderText="Designation" />
                                                    <asp:BoundField DataField="Year" HeaderText="Year" />
                                                    <asp:BoundField DataField="From_Date" HeaderText="From Date" />
                                                    <asp:BoundField DataField="To_Date" HeaderText="To Date" />
                                                    <asp:BoundField DataField="Remark" HeaderText="Remark" />
                                                </Fields>
                                            </asp:DetailsView>
                                        </div>
                                    </div>

                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
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
        function callalert() {
            $("#myModal").modal('show');
        }
    </script>
</asp:Content>

