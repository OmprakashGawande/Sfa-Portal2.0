<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollUpdateGGCARecord.aspx.cs" Inherits="mis_Payroll_PayrollUpdateGGCARecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">GGCA Record</h3>
                    <asp:label id="lblMsg" runat="server" text=""></asp:label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:gridview id="GridView1" pagesize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys " showheaderwhenempty="true" clientidmode="Static" autogeneratecolumns="False">
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
                                        <asp:TemplateField HeaderText="LIC ID">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLicID" runat="server" Text='<%#Eval("GGCALicId") %>' CssClass="form-control" placeholder="Enter LIC ID"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NEW ID">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNewId" runat="server" Text='<%#Eval("GGCANewId") %>' CssClass="form-control" placeholder="Enter NEW ID"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EMP NUMBER">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtEmpNo" runat="server" Text='<%#Eval("GGCAEmpNO") %>' CssClass="form-control" placeholder="Enter EMP NUMBER"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:gridview>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:button id="btnSave" runat="server" cssclass="btn btn-success btn-block" text="Save" onclick="btnSave_Click" />
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

