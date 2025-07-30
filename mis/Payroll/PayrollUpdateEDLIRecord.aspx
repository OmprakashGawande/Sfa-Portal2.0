<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollUpdateEDLIRecord.aspx.cs" Inherits="mis_Payroll_PayrollUpdateEDLIRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">EDLI Record</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true" ClientIDMode="Static" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                            <ItemTemplate>
                                                <asp:Label ID="Emp_Name" runat="server" Text='<%# Eval("Emp_Name") %>' ToolTip='<%# Eval("Emp_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Office_Name" HeaderText="OFFICE NAME" />
                                        <asp:TemplateField HeaderText="EDLI ID">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEDLIID" runat="server" Text='<%# Eval("EDLI_ID") %>' CssClass="form-control" placeholder="Enter EDLI ID"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EDLI NUMBER">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEDLINO" runat="server" Text='<%# Eval("EDLI_EmpNo") %>' CssClass="form-control" placeholder="Enter EDLI NUMBER"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EDLI FREQUENCY">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEDLIFreq" runat="server" Text='<%# Eval("EDLIFrequency") %>' CssClass="form-control" placeholder="Enter EDLI FREQUENCY"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOB">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDOB" runat="server" Text='<%# Eval("Emp_Dob") %>' CssClass="form-control" placeholder="Select DOB" onblur="RetirementDate();"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOJ">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDOJ" runat="server" Text='<%# Eval("Emp_JoiningDate") %>' CssClass="form-control" placeholder="Select DOJ" onblur="RetirementDate();"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOR">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDOR" runat="server" Text='<%# Eval("Emp_RetirementDate") %>' CssClass="form-control" placeholder="Select DOR"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-block" Text="Save" OnClick="btnSave_Click" />
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

        function RetirementDate() {
            debugger;

            // Joining Date
            var i = 0;

            var trCount = $('#GridView1 tr').length - 1;

            $('#GridView1 tr').each(function (index) {
                if (i > 0 && i < trCount) {
                    debugger;
                    var Jdate = $(this).children("td").eq(7).find('input[type="text"]').val();
                    var JEntryDate = Jdate.replace(/-/g, '/');
                    var Jdatearray = Jdate.split("/");
                    var date = $(this).children("td").eq(6).find('input[type="text"]').val();

                    var EntryDate = date.replace(/-/g, '/');
                    var datearray = date.split("/");
                    if (Jdatearray[0] == 1) {
                        datearray[0] = "01";
                    }
                    var Retdate = datearray[2] + '-' + datearray[1] + '-' + datearray[0];

                    var time = new Date(Retdate);
                    if (datearray[0] == 1) {
                        var old_date = time.setDate(time.getDate() - 1);
                        var d = new Date(old_date);
                        new_day = ('0' + (d.getDate())).slice(-2);
                        new_month = ('0' + (d.getMonth() + 1)).slice(-2);
                        new_year = d.getFullYear();
                        Retdate = new_year + '-' + new_month + '-' + new_day;
                    }
                    else {
                        var date = time;
                        // var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
                        var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
                        Retdate = lastDay.getFullYear() + '-' + (lastDay.getMonth() + 1) + '-' + (lastDay.getDate());
                    }

                    var startDob = new Date(Retdate);
                    Retdate = new Date(startDob.getFullYear() + 62, startDob.getMonth(), startDob.getDate());
                    Retdate = (Retdate.getDate()) + '/' + (Retdate.getMonth() + 1) + '/' + Retdate.getFullYear();

                    $(this).children("td").eq(8).find('input[type="text"]').val(Retdate);

                }
                i++;
            });




        }

    </script>
</asp:Content>

