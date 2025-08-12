<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptEmpTaskDetail.aspx.cs" Inherits="mis_Daily_Task_RptEmpTaskDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12">
                <div class="card mt-3  ">
                    <div class="card-header">
                        <h4>Employee Task Report</h4>
                    </div>
                    <div class="row">
                        <div class="co-md-10 justify-content-center"><asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></div>
                    </div>
                    
                    <div class="card-body">
                        <div class="row">
                            <div class="col-xl-3 col-sm-6 position-relative">
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
                            <div class="col-xl-3 col-sm-6 position-relative">
                                <div class="form-group">
                                    <span class="fa-pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                            ErrorMessage="Select From Date" ForeColor="Red"
                                            Text="<i class='fa fa-exclamation-circle' title='Select From Date !'></i>"
                                            ControlToValidate="txtFromDate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <label runat="server">FROM DATE <span style="color: red;">*</span></label>
                                    <asp:TextBox
                                        ID="txtFromDate"
                                        runat="server"
                                        CssClass="form-control datetime-local"
                                        placeholder="DD/MM/YYYY"
                                        autocomplete="off"
                                        data-date-start-date="0d"
                                        onkeydown="return false;" onchange="validateDates()" />
                                </div>
                            </div>
                            <div class="col-xl-3 col-sm-6 position-relative">
                                <div class="form-group">
                                    <span class="fa-pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                            ErrorMessage="Select To Date" ForeColor="Red"
                                            Text="<i class='fa fa-exclamation-circle' title='Select To Date !'></i>"
                                            ControlToValidate="txtToDate" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <label runat="server">TO DATE <span style="color: red;">*</span></label>
                                    <asp:TextBox
                                        ID="txtToDate"
                                        runat="server"
                                        CssClass="form-control datetime-local"
                                        placeholder="DD/MM/YYYY"
                                        autocomplete="off"
                                        data-date-start-date="0d"
                                        onkeydown="return false;" onchange="validateDates()" />
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-xl-3 col-sm-6 position-relative">
                                <div class="form-group">
                                    <asp:Button runat="server" CssClass="btn btn-block btn-outline-success" ID="btnSave" Text="Search" ValidationGroup="a" OnClick="btnSave_Click" />
                                    <a href="RptEmpTaskDetail.aspx" class="btn btn-block   btn-outline-danger">Clear</a>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
                <div class="card">
                    <div class="card-header">
                        <h4>Privious Work Status</h4>
                    </div>
                    <div class="card-body">
                        <div class="row" id="div1" runat="server">

                            <div class="table-responsive">
                                <div class="col-md-12">
                                    <asp:GridView ID="Grid" PageSize="50" runat="server" class="datatable  table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="false" AutoGenerateColumns="False">
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
                                            <asp:TemplateField HeaderText="PARENT TASK">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParentTaskName" Text='<%#Eval("ParentTaskName").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TASK NAME (CODE)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaskName" Text='<%#Eval("TaskName").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TASK TYPE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaskType" Text='<%#Eval("TaskType").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TASK DESCRIPTION">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaskDescription" Text='<%#Eval("TaskDescription").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ASSIGN BY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAssignBy" Text='<%#Eval("AssignBy").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmp_Name" Text='<%#Eval("Emp_Name").ToString() %>' runat="server" />
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
                                                    <asp:Label ID="lblTaskCompletedDate" Text='<%#Eval("TaskCompletedDate").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TASK STATUS">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lblTaskStatus" Text='<%# Eval("TaskStatusText").ToString() %>' runat="server" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DOCUMENT">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server"
                                                        Target="_blank"
                                                        NavigateUrl='<%# 
                                                          (Eval("TaskDoc") != null && Eval("TaskDoc") != DBNull.Value)
                                                          && !string.IsNullOrWhiteSpace(Convert.ToString(Eval("TaskDoc")))
                                                          && Convert.ToString(Eval("TaskDoc")).Trim().ToLower() != "null"
                                                          && Convert.ToString(Eval("TaskDoc")).Trim() != "0"
                                                          ? "~/mis/DailyTaskDoc/" + Convert.ToString(Eval("TaskDoc")).Trim()
                                                          : "" %>'
                                                        CssClass="label label-info" Text="View"
                                                        Visible='<%# (Eval("TaskDoc") != null && Eval("TaskDoc") != DBNull.Value) && !string.IsNullOrWhiteSpace(Convert.ToString(Eval("TaskDoc")))  && Convert.ToString(Eval("TaskDoc")).Trim().ToLower() != "null" && Convert.ToString(Eval("TaskDoc")).Trim() != "0" %>'>
                                                    </asp:HyperLink>
                                                    <%--<asp:HyperLink ID="hyperTaskAllocationDoc" runat="server" Target="_blank" Enabled='<%# Eval("TaskAllocationDoc").ToString() == "" ? false : true %>' NavigateUrl='<%# "~/mis/Document/" + Eval("TaskAllocationDoc") %>' CssClass="label label-info" Visible='<%# (Eval("TaskAllocationDoc") != null && Eval("TaskAllocationDoc") != DBNull.Value) && !string.IsNullOrWhiteSpace(Convert.ToString(Eval("TaskAllocationDoc")))  && Convert.ToString(Eval("TaskAllocationDoc")).Trim().ToLower() != "null" && Convert.ToString(Eval("TaskAllocationDoc")).Trim() != "0" %>' Text="View"></asp:HyperLink>--%>
                                                    <asp:Label ID="lblTaskDoc" runat="server" Visible="false" Text='<%# Eval("TaskDoc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <hr />
                <div class="card mt-2">
                    <div class="card-header">
                        <h4>Current Work Status</h4>
                    </div>
                    <div class="card-body">
                        <div class="row" style="padding: 0px 9px 2px 15px;" id="div2" runat="server">

                            <div class="table-responsive">
                                <div class="col-md-12">
                                    <asp:GridView ID="GridView1" PageSize="50" runat="server" class="datatable table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
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
                                            <asp:TemplateField HeaderText="PARENT TASK">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParentTask" Text='<%#Eval("ParentTaskName").ToString() %>' runat="server" />
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
                                            <asp:TemplateField HeaderText="TASK DESCRIPTION">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaskDescription" Text='<%#Eval("TaskDescription").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ASSIGN BY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAssignBy" Text='<%#Eval("AssignBy").ToString() %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmp_Name" Text='<%#Eval("Emp_Name").ToString() %>' runat="server" />
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
                                            <asp:TemplateField HeaderText="TASK STATUS">
                                                <ItemTemplate>
                                                    <div>
                                                        <asp:Label ID="lblTaskStatus" Text='<%# Eval("TaskStatusText").ToString() %>' runat="server" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DOCUMENT">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server"
                                                        Target="_blank"
                                                        NavigateUrl='<%# 
                                                              (Eval("TaskDoc") != null && Eval("TaskDoc") != DBNull.Value)
                                                              && !string.IsNullOrWhiteSpace(Convert.ToString(Eval("TaskDoc")))
                                                              && Convert.ToString(Eval("TaskDoc")).Trim().ToLower() != "null"
                                                              && Convert.ToString(Eval("TaskDoc")).Trim() != "0"
                                                              ? "~/mis/DailyTaskDoc/" + Convert.ToString(Eval("TaskDoc")).Trim()
                                                              : "" %>'
                                                        CssClass="label label-info" Text="View"
                                                        Visible='<%# (Eval("TaskDoc") != null && Eval("TaskDoc") != DBNull.Value) && !string.IsNullOrWhiteSpace(Convert.ToString(Eval("TaskDoc")))  && Convert.ToString(Eval("TaskDoc")).Trim().ToLower() != "null" && Convert.ToString(Eval("TaskDoc")).Trim() != "0" %>'>
                                                    </asp:HyperLink>
                                                    <%--<asp:HyperLink ID="hyperTaskAllocationDoc" runat="server" Target="_blank" Enabled='<%# Eval("TaskAllocationDoc").ToString() == "" ? false : true %>' NavigateUrl='<%# "~/mis/Document/" + Eval("TaskAllocationDoc") %>' CssClass="label label-info" Visible='<%# (Eval("TaskAllocationDoc") != null && Eval("TaskAllocationDoc") != DBNull.Value) && !string.IsNullOrWhiteSpace(Convert.ToString(Eval("TaskAllocationDoc")))  && Convert.ToString(Eval("TaskAllocationDoc")).Trim().ToLower() != "null" && Convert.ToString(Eval("TaskAllocationDoc")).Trim() != "0" %>' Text="View"></asp:HyperLink>--%>
                                                    <asp:Label ID="lblTaskDocPath" runat="server" Visible="false" Text='<%# Eval("TaskDoc") %>'></asp:Label>
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
        $(document).ready(function () {
            $(document).ready(function () {
                initCustomDataTable('.datatable', 'Employee Task Plan and Status report', 'Employee Task Plan and Status report');
            });

        });

        $(document).ready(function () {

           

            window.onload = function () {
                // ----- Start of Week (Monday) -----
                var today = new Date();
                var dayOfWeek = today.getDay(); // 0 = Sunday, 1 = Monday, ..., 6 = Saturday
                var diffToMonday = (dayOfWeek === 0 ? -6 : 1 - dayOfWeek);
                var monday = new Date(today);
                monday.setDate(today.getDate() + diffToMonday);

                // Format Monday (DD/MM/YYYY)
                var dd = String(monday.getDate()).padStart(2, '0');
                var mm = String(monday.getMonth() + 1).padStart(2, '0');
                var yyyy = monday.getFullYear();
                var mondayFormatted = dd + '/' + mm + '/' + yyyy;

                // Set Monday to txtFromDate
                document.getElementById('<%= txtFromDate.ClientID %>').value = mondayFormatted;

                // ----- End of Week (Sunday) -----
                var sunday = new Date(monday);
                sunday.setDate(monday.getDate() + 4);

                // Format Sunday (DD/MM/YYYY)
                var dd2 = String(sunday.getDate()).padStart(2, '0');
                var mm2 = String(sunday.getMonth() + 1).padStart(2, '0');
                var yyyy2 = sunday.getFullYear();
                var sundayFormatted = dd2 + '/' + mm2 + '/' + yyyy2;

                // Set Sunday to txtToDate
                document.getElementById('<%= txtToDate.ClientID %>').value = sundayFormatted;
            };
        });
    </script>



</asp:Content>

