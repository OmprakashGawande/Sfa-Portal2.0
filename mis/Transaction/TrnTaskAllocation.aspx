<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="TrnTaskAllocation.aspx.cs" Inherits="mis_Transaction_TrnTaskAllocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12">
                <div class="card mt-3  border-warning">
                    <div class="card-header">
                        <h4>Task Allocation</h4>
                    </div>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    <div class="card-body">
                        <div class="row g-3">
                            <div class="col-md-3 col-sm-6 position-relative">
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
                            <div class="col-md-3 col-sm-6 position-relative">
                                <div class="form-group">
                                    <span class="fa-pull-right">
                                        <asp:RequiredFieldValidator ID="RFV" ValidationGroup="a"
                                            ErrorMessage="Select To Date" ForeColor="Red"
                                            Text="<i class='fa fa-exclamation-circle' title='Enter Module !'></i>"
                                            ControlToValidate="ddlProjectModule" Display="Dynamic" runat="server" InitialValue="0">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <label runat="server">MODULE <span style="color: red;">*</span></label>
                                    <asp:DropDownList runat="server" ID="ddlProjectModule" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectModule_SelectedIndexChanged"
                                        CssClass="form-control select2">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6 position-relative">
                                <div class="form-group">
                                    <span class="fa-pull-right">
                                        <asp:RequiredFieldValidator ID="RFV2" ValidationGroup="a"
                                            ErrorMessage="Select To Date" ForeColor="Red"
                                            Text="<i class='fa fa-exclamation-circle' title='Select MAIN POWER!'></i>"
                                            ControlToValidate="ddlMainPower" InitialValue="0" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <label runat="server">MAN POWER <span style="color: red;">*</span></label>
                                    <asp:DropDownList runat="server" ID="ddlMainPower" ClientIDMode="Static"
                                        CssClass="form-control select2" OnSelectedIndexChanged="ddlMainPower_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6 position-relative">
                                <div class="form-group">
                                    <span class="fa-pull-right">
                                        <asp:RequiredFieldValidator ID="RFV3" ValidationGroup="a"
                                            ErrorMessage="Select To Date" ForeColor="Red"
                                            Text="<i class='fa fa-exclamation-circle' title='Select TASK!'></i>"
                                            ControlToValidate="ddlTask" InitialValue="0" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <label runat="server">TASK<span style="color: red;">*</span></label>
                                    <%--<asp:ListBox ID="ddlTask" runat="server" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>--%>
                                    <asp:DropDownList runat="server" ID="ddlTask" ClientIDMode="Static"
                                        CssClass="form-control select2">
                                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6 position-relative">
                                <div class="form-group">
                                    <label>TASK CATEGORY<span style="color: red;"> *</span></label>
                                    <span class="fa-pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="a"
                                            ErrorMessage="Select" ForeColor="Red"
                                            Text="<i class='fa fa-exclamation-circle' title='Select Task Category'></i>"
                                            ControlToValidate="ddlWorkCategoryId" InitialValue="0" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>

                                    <asp:DropDownList runat="server" ID="ddlWorkCategoryId" ClientIDMode="Static"
                                        CssClass="form-control select2">
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6 position-relative">
                                <div class="form-group">
                                    <span class="fa-pull-right">
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator5"
                                            ValidationGroup="a"
                                            ErrorMessage="Select Start Date"
                                            ForeColor="Red"
                                            Text="<i class='fa fa-exclamation-circle' title='Select Work Order Date'></i>"
                                            ControlToValidate="txtFromDate"
                                            Display="Dynamic"
                                            runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>

                                    <label>FROM DATE  <span style="color: red;">*</span></label>

                                    <%--<asp:TextBox ID="txtFromDate" runat="server"
                                        placeholder="DD/MM/YYYY"
                                        autocomplete="off"
                                        data-date-format="dd/mm/yyyy"
                                        data-date-autoclose="true"
                                        CssClass="form-control datepicker-here"
                                        data-date-start-date="0d"
                                        onkeydown="return false;" data-language="en" onchange="validateDates()" />--%>
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
                            <div class="col-md-3 col-sm-6 position-relative">
                                <div class="form-group">
                                    <span class="fa-pull-right">
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator6"
                                            ValidationGroup="a"
                                            ErrorMessage="Select Start Date"
                                            ForeColor="Red"
                                            Text="<i class='fa fa-exclamation-circle' title='Select Complition Date'></i>"
                                            ControlToValidate="txtToDate"
                                            Display="Dynamic"
                                            runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>

                                    <label>TO DATE <span style="color: red;">*</span></label>
                                    <asp:TextBox
                                        ID="txtToDate"
                                        runat="server"
                                        CssClass="form-control datetime-local"
                                        placeholder="DD/MM/YYYY"
                                        autocomplete="off"
                                        data-date-start-date="0d"
                                        onkeydown="return false;" onchange="validateDates()" />
                                    <%-- <asp:TextBox ID="txtToDate" runat="server"
                                        placeholder="DD/MM/YYYY"
                                        autocomplete="off" data-date-format="dd/mm/yyyy"
                                        data-date-autoclose="true"
                                        data-date-start-date="0d"
                                        CssClass="form-control datepicker-here" data-language="en" 
                                        onchange="validateDates()" />--%>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6 position-relative">
                                <div class="form-group">
                                    <label>TASK PRIORITY<span style="color: red;"> *</span></label>
                                    <span class="fa-pull-right">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                            ErrorMessage="Select" ForeColor="Red"
                                            Text="<i class='fa fa-exclamation-circle' title='Select Task Category'></i>"
                                            ControlToValidate="ddlTaPriority" InitialValue="0" Display="Dynamic" runat="server">
                                        </asp:RequiredFieldValidator>
                                    </span>
                                    <asp:DropDownList runat="server" ID="ddlTaPriority" ClientIDMode="Static"
                                        CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-6 position-relative">
                                <div class="form-group">
                                    <label>DOCUMENT</label>
                                    <asp:FileUpload runat="server" ID="FUDoc" CssClass="form-control"></asp:FileUpload>
                                </div>
                            </div>

                            <div class="col-md-6 col-sm-6 position-relative">
                                <div class="form-group">
                                    <label>REMARK<%--<span style="color: red;"> *</span>--%></label>
                                    <%--  <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator7"
                                                        ValidationGroup="a"
                                                        ErrorMessage="Enter REMARK"
                                                        ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter REMARK!'></i>"
                                                        ControlToValidate="txtDiscription"
                                                        Display="Dynamic"
                                                        runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>--%>
                                    <textarea
                                        id="txtDiscription"
                                        runat="server"
                                        class="form-control"
                                        oninput="autoResizeTextarea(this)"
                                        onkeypress="javascript:tbx_fnAlphaOnly(event, this);"
                                        placeholder="Enter Remark"></textarea>
                                    <asp:Label runat="server" ID="lblCounter" ForeColor="Red"></asp:Label>

                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-xl-3">
                                <div class="form-group">
                                    <asp:Button runat="server" CssClass="btn btn-block btn-outline-success" ID="btnSave" Text="Save" ValidationGroup="a" OnClick="btnSave_Click" />
                                    <a href="TrnTaskAllocation.aspx" class="btn btn-block   btn-outline-danger">Clear</a>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-md-12">
                                <div class="table-responsive" runat="server" id="DivWorkinProject" visible="false">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grvWorkingProject" PageSize="50" runat="server" class=" table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="false" AutoGenerateColumns="False" OnRowCommand="Grid_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PROJECT NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProjectName" Text='<%# Eval("ProjectName").ToString() %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MANAGER NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOWNERNAME" Text='<%# Eval("OWNERNAME").ToString() %>' runat="server"></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ALLOCATION DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAllocationDate" Text='<%# Eval("AllocationDate").ToString() %>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>
                        </div>


                        <%--grid--%>
                    </div>

                </div>

                <div class="card border-warning">
                    <div class="card-header">
                        <h4>Task Allocation Detail </h4>
                    </div>
                    <div class="card-body">
                        <div class="table-responsive dt-ext ">
                            <asp:GridView ID="Grid" PageSize="50" runat="server"
                                CssClass="datatable display table table-hover table-bordered pagination-ys"
                                ShowHeaderWhenEmpty="false" AutoGenerateColumns="False" OnRowCommand="Grid_RowCommand" AllowPaging="true">

                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="PROJECT NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProjectName" Text='<%# Eval("ProjectName") %>' runat="server" />
                                            <asp:Label ID="lblProjectId" Text='<%# Eval("ProjectId") %>' Visible="false" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MODULE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblModuleName" Text='<%# Eval("ModuleName") %>' runat="server" />
                                            <asp:Label ID="lblModuleId" Text='<%# Eval("ModuleId") %>' Visible="false" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="TEAM LEAD ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTeamLeadName" Text='<%# Eval("TeamLeadName") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MAN POWER ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmp_Name" Text='<%# Eval("Emp_Name") %>' runat="server" />
                                            <asp:Label ID="lblEmpId" Text='<%# Eval("EmpId") %>' Visible="false" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="TASK">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaskName" Text='<%# Eval("TaskName") %>' runat="server" />
                                            <asp:Label ID="lblTaskId" Text='<%# Eval("TaskId") %>' Visible="false" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="CATEGORY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkCategoryEng" Text='<%# Eval("WorkCategoryEng") %>' runat="server" />
                                            <asp:Label ID="lblCategoreyId" Text='<%# Eval("CategoreyId") %>' Visible="false" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="TASK ALLOCATION DATE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaskAllocationDate" Text='<%# Eval("TaskAllocationDate", "{0:dd-MM-yyyy}") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="FROM DATE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFromDate" Text='<%# Eval("FromDate", "{0:dd-MM-yyyy}") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="TO DATE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToDate" Text='<%# Eval("ToDate", "{0:dd-MM-yyyy}") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="TASK PRIORITY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaskPriority" Text='<%# Eval("TaskPriority") %>' runat="server" />
                                            <asp:Label ID="lblTaskPriorityId" Text='<%# Eval("TaskPriorityId") %>' Visible="false" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="REMARK ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDiscrisption" Text='<%# Eval("Discription") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="WORK STATUS">
                                        <ItemTemplate>
                                            <asp:Label
                                                ID="lblTaskStatusText"
                                                runat="server"
                                                Text='<%# Eval("TaskStatusText") %>'
                                                Style='<%# 
                        Eval("TaskStatusText").ToString() == "Complete" ? "color:green;": Eval("TaskStatusText").ToString() == "Working in Progress" ? "color:orange;" :
                        Eval("TaskStatusText").ToString() == "TaskNotFilled" ? "color:red;" :
                        "color:black;" 
                    %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DOCUMENT">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HyperLink1" runat="server"
                                                Target="_blank"
                                                NavigateUrl='<%# !string.IsNullOrWhiteSpace(Convert.ToString(Eval("TaskAllocationDoc"))) && 
                                 Convert.ToString(Eval("TaskAllocationDoc")).Trim().ToLower() != "null" && 
                                 Convert.ToString(Eval("TaskAllocationDoc")).Trim() != "0"
                                 ? "~/mis/Document/" + Convert.ToString(Eval("TaskAllocationDoc")).Trim()
                                 : "" %>'
                                                CssClass="btn btn-info btn-sm" Text="View"
                                                Visible='<%# !string.IsNullOrWhiteSpace(Convert.ToString(Eval("TaskAllocationDoc"))) && 
                               Convert.ToString(Eval("TaskAllocationDoc")).Trim().ToLower() != "null" && 
                               Convert.ToString(Eval("TaskAllocationDoc")).Trim() != "0" %>'>
                                            </asp:HyperLink>
                                            <asp:Label ID="lblTaskAllocationDocPath" runat="server" Visible="false" Text='<%# Eval("TaskAllocationDoc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ACTION">
                                        <ItemTemplate>
                                            <asp:LinkButton
                                                ID="lnkEdit" runat="server" CssClass="edit" CommandArgument='<%# Eval("TaskAllocationId") %>' CommandName="EditRecord"
                                                CausesValidation="False"
                                                Visible='<%# Eval("TaskStatusText").ToString() != "Complete" && ((DateTime)Eval("CreatedOn")).AddHours(1) > DateTime.Now %>'>
                                            <i class="icon-pencil-alt"></i></asp:LinkButton>
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
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        CharactersCount(150);
        const element = document.getElementById('<%=txtDiscription.ClientID%>');
        // Pass CharactersCount directly with a parameter using an inline arrow function
        element.addEventListener("keyup", (event) => CharactersCount(150));

        function CharactersCount(_length) {
            var txtMsg = document.getElementById('<%=txtDiscription.ClientID%>');
            var lblCount = document.getElementById('<%=lblCounter.ClientID%>');
            if (txtMsg.value.length > _length) {
                txtMsg.value = txtMsg.value.substring(0, _length);
            }
            // Calculate and display the remaining characters
            const remaining = _length - txtMsg.value.length;
            lblCount.innerHTML = `${remaining} characters remaining`;
        }

        function checkTextAreaMaxLength(textBox, e, length) {

            var mLen = textBox["MaxLength"];
            if (null == mLen)
                mLen = length;

            var maxLength = parseInt(mLen);
            if (!checkSpecialKeys(e)) {
                if (textBox.value.length > maxLength - 1) {
                    if (window.event)//IE
                        e.returnValue = false;
                    else//Firefox
                        e.preventDefault();
                }
            }
        }
        function checkSpecialKeys(e) {
            if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                return false;
            else
                return true;
        }
    </script>
    <script>
        function autoResizeTextarea(ths) {
            ths.style.height = 'auto'; // Reset height to calculate new scrollHeight
            ths.style.height = ths.scrollHeight + 'px'; // Set height to fit content
        }



        $(document).ready(function () {
            initCustomDataTable('.datatable', 'Task Allocation Detail', 'Task Allocation Detail', [13, 14]);
        });


    </script>
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

