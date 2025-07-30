<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpPromotionPending.aspx.cs" Inherits="mis_HR_HREmpPromotionPending" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">कर्मचारी पदोन्नति विवरण / Employee Promotion Detail</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:Label ID="lblMsg2" runat="server" Text="" style="color:red; font-size: 15px;"></asp:Label>
                                <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered table-striped pagination-ys" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="PromotionID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" CssClass="label label-info" runat="server" CommandName="Select">Confirm Promotion</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="OrderNo" HeaderText="Order No" />
                                        <asp:BoundField DataField="OrderDate" HeaderText="Order Date" />
                                        <asp:BoundField DataField="NewEffectiveDate" HeaderText="Effective Date" />
                                        <asp:BoundField DataField="OldLevel_Name" HeaderText="Existing Level" />
                                        <asp:BoundField DataField="OldBasicSalery" HeaderText="Existing Basic Salary" />
                                        <asp:BoundField DataField="OldClass" HeaderText="Old Class" />
                                        <asp:BoundField DataField="OldDepartment" HeaderText="Old Department" />
                                        <asp:BoundField DataField="OldDesignation" HeaderText="Old Designation" />
                                        <asp:BoundField DataField="NewLevel_Name" HeaderText="New Level" />
                                        <asp:BoundField DataField="NewBasicSalery" HeaderText="New Basic Salary" />
                                        <asp:BoundField DataField="NewClass" HeaderText="New Class" />
                                        <asp:BoundField DataField="NewDepartment" HeaderText="New Department" />
                                        <asp:BoundField DataField="NewDesignation" HeaderText="New Designation" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- The Modal -->
            <div class="modal fade" id="myModal">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title">Employee Promotion Approval</h4>
                            <button type="button" class="close" data-dismiss="modal">×</button>
                        </div>

                        <!-- Modal body -->
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Promotion Date<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtPromotionDate" runat="server" placeholder="Select Promotion Date..." class="form-control DateAdd" autocomplete="off" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnPromotion" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Approve" OnClientClick="return validateRelieving();" OnClick="btnPromotion_Click" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a class="btn btn-block btn-default" style="margin-top: 23px;" data-dismiss="modal">Cancel</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <!-- Modal footer -->
                        <div class="modal-footer">
                            <%--<button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>--%>
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
        function validateform() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=txtPromotionDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select Increment Date.\n";
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (document.getElementById('<%=btnPromotion.ClientID%>').value.trim() == "Approve") {
                    if (confirm("Do you really want to Approve increment ?")) {
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

