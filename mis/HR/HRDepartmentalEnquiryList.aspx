<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRDepartmentalEnquiryList.aspx.cs" Inherits="mis_HR_HRDepartmentalEnquiryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title">Departmental Enquiry List</h3>
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" DataKeyNames="DepartmentEnquiry_ID" runat="server" CssClass="table table-bordered table-hover table-striped" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"  EmptyDataText="No Record Found" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrownumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EmployeeName" HeaderText="EMPLOYEE NAME" />
                                                    <asp:BoundField DataField="OrderNo" HeaderText="ORDER NO" />
                                                    <asp:BoundField DataField="OrderDate" HeaderText="ORDER DATE" />
                                                    <asp:BoundField DataField="EnquiryOfficer" HeaderText="ENQUIRY OFFICER" />
                                                    <asp:BoundField DataField="PresentingOfficer" HeaderText="PRESENTING OFFICER" />
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkView" runat="server" CssClass="label label-default" CommandName="select" CausesValidation="false">View More</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="myModal" class="modal fade" role="dialog">
                                <div class="modal-dialog">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                            <h4 class="modal-title">Enquiry Detail</h4>
                                        </div>
                                        <div class="modal-body">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset>
                                                        <legend>Enquiry Remark</legend>
                                                        <div class="form-group">
                                                            <label>Remark</label>
                                                            <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset>
                                                        <legend>Enquiry Detail</legend>
                                                        <div class="table-responsive">
                                                            <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover table-striped" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblrownumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ENQ_OrderNo" HeaderText="ORDER NO" />
                                                                    <asp:BoundField DataField="ENQ_OrderDate" HeaderText="ORDER DATE" />
                                                                    <asp:BoundField DataField="EnquiryOfficer" HeaderText="ENQUIRY OFFICER" />
                                                                    <asp:BoundField DataField="PresentingOfficer" HeaderText="PRESENTING OFFICER" />
                                                                    <asp:BoundField DataField="ENQ_Remark" HeaderText="REMARK" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>Status</label>
                                                        <div>
                                                            <asp:RadioButtonList ID="RblStatus" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Selected="True">Open</asp:ListItem>
                                                                <asp:ListItem>Close</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Order No <span style="color: red;">*</span></label>
                                                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control" placeholder="Enter Order No"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Order Date <span style="color: red;">*</span></label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <asp:TextBox ID="txtOrderDate" runat="server" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" PlaceHolder="Select Ordre Date" ClientIDMode="Static"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Enquiry Officer <span style="color: red;">*</span></label>
                                                        <asp:DropDownList ID="ddlEnquiryOfficer" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-6">
                                                    <div class="form-group">
                                                        <label>Presenting Officer <span style="color: red;">*</span></label>
                                                        <asp:DropDownList ID="ddlPresentingOfficer" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>Enquiry Remark <span style="color: red;">*</span></label>
                                                        <asp:TextBox ID="txtENQ_Remark" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Enter Enquiry Remark"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <asp:Button ID="btnSave" class="btn btn-success pull-left" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return Validatefrom();" />
                                            <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>
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
    <script>
        function callalert() {
            $('#myModal').modal({
                backdrop: 'static',
                keyboard: false
            })
        }


        function Validatefrom() {
            var msg = "";
            if (document.getElementById('<%=txtOrderNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Order No. \n";
            }
            if (document.getElementById('<%=txtOrderDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select Order Date. \n";
            }
            if (document.getElementById('<%=ddlEnquiryOfficer.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Enquiry Officer. \n";
            }
            if (document.getElementById('<%=ddlPresentingOfficer.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Presenting Officer. \n";
            }
            if (document.getElementById('<%=txtENQ_Remark.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Enquiry Remark. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }
    </script>
</asp:Content>

