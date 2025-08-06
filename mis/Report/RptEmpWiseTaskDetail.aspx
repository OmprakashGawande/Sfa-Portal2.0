<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptEmpWiseTaskDetail.aspx.cs" Inherits="mis_Report_RptEmpWiseTaskDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Employee Task Detail Report</h3>
                            <hr />
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <span class="fa-pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            ErrorMessage="Select Employee" ForeColor="Red"
                                            Text="<i class='fa fa-exclamation-circle' title='Select Employee!'></i>"
                                            ControlToValidate="ddlEmp" InitialValue="0" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <label>EMPLOYEE <span style="color: red;">*</span></label>
                                    <div class="form-group ms">
                                        <asp:DropDownList ID="ddlEmp" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                ErrorMessage="Select From Date" ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select From Date !'></i>"
                                                ControlToValidate="txtFromDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <label runat="server">FROM DATE <span style="color: red;">*</span></label>
                                        <asp:TextBox runat="server" ID="txtFromDate"
                                            data-provide="datepicker" placeholder="DD/MM/YYYY"
                                            autocomplete="off" data-date-format="dd/mm/yyyy"
                                            data-date-autoclose="true" CssClass="form-control" OnChange="validateDates()"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                                ErrorMessage="Select To Date" ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select To Date !'></i>"
                                                ControlToValidate="txtToDate" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <label runat="server">TO DATE <span style="color: red;">*</span></label>
                                        <asp:TextBox runat="server" ID="txtToDate"
                                            data-provide="datepicker" placeholder="DD/MM/YYYY"
                                            autocomplete="off" data-date-format="dd/mm/yyyy"
                                            data-date-autoclose="true" CssClass="form-control" OnChange="validateDates()" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Button runat="server" Style="margin-top: 22px;" CssClass="btn btn-block btn-success" ID="btnSearch" Text="Search" ValidationGroup="a" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <a href="RptEmpWiseTaskDetail.aspx" style="margin-top: 22px;" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row" style="padding: 0px 9px 2px 15px;" id="div2" runat="server">
                                <div>
                                    <h4 style="margin-left: 2rem;">EMPLOYEE TASK DETAIL </h4>

                                </div>
                                <div class="row" id="dvexportbtn" runat="server">
                                    <div class="col-md-12" style="display: flex; justify-content: end !important; margin: -15px;">
                                        <asp:Button ID="btnExportExcel" runat="server"
                                            Text="📥 Export to Excel"
                                            CssClass="btn btn-success mb-2"
                                            OnClick="btnExportExcel_Click" />

                                    </div>
                                </div>
                                <div class="table-responsive">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridView" PageSize="50" OnRowDataBound="GridView_RowDataBound" runat="server" class="table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ProjectId").ToString() %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PROJECT NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProject_Name_Eng" Text='<%# Eval("ProjectName").ToString() %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ASSIGN BY">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAssignBy" Text='<%#Eval("AssignedBy").ToString() %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>  
                                               <%-- <asp:TemplateField HeaderText="ASSIGNER DESIGNATION">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAssigneDesignation" Text='<%#Eval("AssigneDesignation").ToString() %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmp_Name" Text='<%#Eval("EmployeeName").ToString() %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="TASK NAME (CODE)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaskName" Text='<%#Eval("TaskName").ToString() %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TASK TYPE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaskType" Text='<%#Eval("TaskType").ToString() %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="INTERNAL CHALLANGE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInternalChallange" Text='<%#Eval("InternalChallange").ToString() %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EXTERNAL CHALLANGE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblExternalChallange" Text='<%#Eval("ExternalChallange").ToString() %>' runat="server" />
                                                    </ItemTemplate> 
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="START DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFromDate" Text='<%#Eval("FromDate").ToString() %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="END DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblToDate" Text='<%#Eval("ToDate").ToString() %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="COMPLETION DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaskCompletedDate" Text='<%#Eval("TaskCompletedDate").ToString() %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Task Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTaskStatus" runat="server"
                                                            Text='<%# Eval("TaskStatusText") %>'
                                                            CssClass="status-label" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                            <EmptyDataTemplate>
                                                <tr>
                                                    <td colspan="12" style="text-align: center; color: red; font-weight: bold;">No record found.
                                                    </td>
                                                </tr>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
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
    <script type="text/javascript">
    function validateDates() {
        var fromDateElem = document.getElementById('<%= txtFromDate.ClientID %>');
     var toDateElem = document.getElementById('<%= txtToDate.ClientID %>');

        var fromDate = fromDateElem.value;
        var toDate = toDateElem.value;

        if (fromDate !== '' && toDate !== '') {
            var partsFrom = fromDate.split('/');
            var partsTo = toDate.split('/');

            var from = new Date(partsFrom[2], partsFrom[1] - 1, partsFrom[0]); // dd/mm/yyyy
            var to = new Date(partsTo[2], partsTo[1] - 1, partsTo[0]);

            if (from > to) {
                alert('From Date cannot be greater than To Date!');
                // You can clear one or both fields, depending on preference:
                fromDateElem.value = '';
                // toDateElem.value = '';
                fromDateElem.focus();
            }
        }
    }
    </script>
</asp:Content>

