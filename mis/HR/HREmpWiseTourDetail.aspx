<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpWiseTourDetail.aspx.cs" Inherits="mis_HR_HREmpWiseTourDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Applied Tour detail</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Tour Year</label>
                                <asp:DropDownList ID="ddlFinancialYear" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlFinancialYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Month <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                    <asp:ListItem Value="0">Select Month</asp:ListItem>
                                    <asp:ListItem Value="01">January</asp:ListItem>
                                    <asp:ListItem Value="02">February</asp:ListItem>
                                    <asp:ListItem Value="03">March</asp:ListItem>
                                    <asp:ListItem Value="04">April</asp:ListItem>
                                    <asp:ListItem Value="05">May</asp:ListItem>
                                    <asp:ListItem Value="06">June</asp:ListItem>
                                    <asp:ListItem Value="07">July</asp:ListItem>
                                    <asp:ListItem Value="08">August</asp:ListItem>
                                    <asp:ListItem Value="09">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success btn-block" Style="margin-top: 25px;" OnClick="btnSearch_Click" OnClientClick="return ValidateForm();" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" DataKeyNames="TourId" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" runat="server" EmptyDataText="No Record Found" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tour Status" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label Text='<%# Eval("TourStatus".ToString())%>' runat="server" ID="lbTourStatus"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TourDay" HeaderText="Tour Day" />
                                        <asp:BoundField DataField="ApprovalAuthority" HeaderText="Approval Authority" />
                                        <asp:BoundField DataField="TourType" HeaderText="Tour Type" />
                                        <asp:BoundField DataField="TourAppliedOn" HeaderText="Applied Date" />
                                        <asp:BoundField DataField="TourFromDate" HeaderText="From Date" />
                                        <asp:BoundField DataField="TourToDate" HeaderText="To Date" />
                                        <asp:TemplateField HeaderText="View More" ShowHeader="False" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="ViewMore" CssClass="label label-info" runat="server" CommandName="Select">View More</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lblMsg2" runat="server" Text="" Style="color: red; font-size: 15px;"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Tour Remark</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Tour Request</legend>
                                            <div class="table-responsive">
                                                <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateRows="false">
                                                    <Fields>
                                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                        <asp:BoundField DataField="TourType" HeaderText="Tour Type" />
                                                        <asp:BoundField DataField="TourFromDate" HeaderText="From Date" />
                                                        <asp:BoundField DataField="TourToDate" HeaderText="To Date" />
                                                        <asp:TemplateField HeaderText="Tour Request Doc" ItemStyle-Width="70%">
                                                            <ItemTemplate>
                                                                <a href='<%# Eval("TourDocument") %>' target="_blank" class="label label-info"><%# Eval("TourDocument").ToString() != "" ? "View" : "" %></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Fields>
                                                </asp:DetailsView>
                                                <div class="form-group">
                                                    <asp:Label ID="lblr" runat="server" Text="Tour Description"></asp:Label>
                                                </div>
                                                <div class="form-group">

                                                    <div id="TourReason" runat="server">
                                                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <fieldset>
                                            <legend>Reply from Approval Authority </legend>
                                            <div class="table-responsive">
                                                <asp:DetailsView ID="DetailsView2" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateRows="false">
                                                    <Fields>
                                                        <asp:TemplateField HeaderText="Tour Status" ItemStyle-Width="70%">
                                                            <ItemTemplate>
                                                                <asp:Label Text='<%# Eval("TourStatus".ToString())%>' runat="server" ID="lbTourStatus"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="TourApprovalOrderNo" HeaderText="Order No" />
                                                        <asp:BoundField DataField="TourApprovalOrderDate" HeaderText="Order Date" />
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblDocHeader" runat="server" Text='Doc'></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <a href='<%# Eval("TourApprovalOrderFile") %>' target="_blank" class="label label-info"><%# Eval("TourApprovalOrderFile").ToString() != "" ? "View" : "" %></a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Fields>
                                                </asp:DetailsView>
                                                <div class="form-group">
                                                    <asp:Label ID="Label1" runat="server" Text="Remark/Comment"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <div id="HRRemark" runat="server">
                                                        <asp:TextBox ID="txtRemarkByHR" runat="server" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div id="tourfeedback" runat="server">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <fieldset>
                                                <legend>Tour FeedBack</legend>
                                                <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>FeedBack.</label>
                                                <asp:TextBox ID="txtFeedBack" runat="server" placeholder="Enter FeedBack" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                            </div>
                                        </div>   
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Document</label>&nbsp;&nbsp;<asp:HyperLink ID="doc" runat="server" CssClass="label label-info" Text="View" Visible="false"></asp:HyperLink>
                                                <asp:FileUpload ID="FeedBackDoc" CssClass="form-control" runat="server" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG*GIF*PDF*XLS*XLSX*DOC*DOCX', this),ValidateFileSize(this)" />
                                            </div>
                                        </div>
                                        
                                    </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Action" OnClientClick="return ValidateSave()" OnClick="btnSave_Click"/>
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

        function ValidateForm() {
            var msg = "";
            if (document.getElementById('<%=ddlFinancialYear.ClientID%>').selectedIndex == 0) {
            msg += "Select Year \n";

        }
        if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
            msg += "Select Month \n";

        }
        if (msg != "") {
            alert(msg);
            return false;
        }
        else {

            return true;
        }
        }
        function ValidateSave()
        {
            var msg = "";
            if (document.getElementById('<%=txtFeedBack.ClientID%>').value.trim() =="") {
                msg += "Enter FeedBack \n";

            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Save Detail.?")) {
                    return true;
                }
                else {
                    return false;
                }      
            }
        }
    </script>
</asp:Content>

