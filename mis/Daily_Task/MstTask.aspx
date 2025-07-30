<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MstTask.aspx.cs" Inherits="mis_Daily_Task_MstTask" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        #txtTaskDescriptionl {
            width: 100%;
            min-height: calc(1.5em * 2 + 1rem); /* roughly 2 rows line height + padding */
            padding: 0.5rem 0.75rem;
            font-size: 1rem;
            line-height: 1.5;
            border: 1px solid #ced4da;
            border-radius: 0.375rem;
            resize: none; /* prevent manual resize */
            overflow-y: hidden; /* hide scrollbar */
            box-sizing: border-box;
            font-family: inherit;
            transition: border-color 0.2s ease;
        }

        #txtTaskDescription {
            border-color: #5c6ac4;
            outline: none;
            box-shadow: 0 0 0 3px rgba(92, 106, 196, 0.3);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <asp:HiddenField runat="server" ID="hfProjectID" />
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header">
                            <h3 class="box-title">Task Master</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label runat="server">
                                            PROJECT NAME
                                            <label style="color: red;">*</label></label>
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="a"
                                                ErrorMessage="Select Project Name" InitialValue="0" ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select Project !'></i>"
                                                ControlToValidate="ddlProject" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <div class="ms form-group">
                                            <asp:DropDownList runat="server" ID="ddlProject" ClientIDMode="Static"
                                                CssClass="form-control select2">
                                                <asp:ListItem Value="0">No record found</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>PARENT TASK</label>
                                        <asp:DropDownList runat="server" ID="ddlParentTask" ClientIDMode="Static"
                                            CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Development</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                                ErrorMessage="Enter Task Name" ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Enter Task Name !'></i>"
                                                ControlToValidate="txtTaskName" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <label>TASK NAME<span style="color: red;"> *</span></label>
                                        <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Task Name" ID="txtTaskName" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>TASK CODE<span style="color: red;"> *</span></label>
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ValidationGroup="a"
                                                ErrorMessage="Enter Task Code" ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Enter Task Code!'></i>"
                                                ControlToValidate="txtTaskCode" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:TextBox runat="server" placeholder="Enter Task Code" CssClass="form-control" ID="txtTaskCode" MaxLength="10"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>TASK CATEGOREY<span style="color: red;"> *</span></label>
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="a"
                                                ErrorMessage="Select" InitialValue="0" ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Select Task Type!'></i>"
                                                ControlToValidate="ddlTaskType" Display="Dynamic" runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <asp:DropDownList runat="server" ID="ddlTaskType" ClientIDMode="Static"
                                            CssClass="form-control select2">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">Development</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <label for="txtTaskDescription">TASK DESCRIPTION<span style="color: red;"> *</span></label>
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator4"
                                                ValidationGroup="a"
                                                ErrorMessage="Enter Task Description"
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Enter Task Description!'></i>"
                                                ControlToValidate="txtTaskDescription"
                                                Display="Dynamic"
                                                runat="server">
                                            </asp:RequiredFieldValidator>
                                        </span>
                                        <textarea
                                            id="txtTaskDescription"
                                            runat="server"
                                            class="form-control"
                                            oninput="autoResizeTextarea(this)"
                                            onkeypress="javascript:tbx_fnAlphaOnly(event, this);"
                                            placeholder="Enter Task Description"
                                            rows="2"></textarea>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="IsActive">STATUS<span style="color: red;"> *</span></label>
                                        <input type="checkbox" id="IsActive" class="form-check-input" style="margin-top: 3rem;" checked="checked" />
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSave" Text="Save" ValidationGroup="a" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a href="MstTask.aspx" class="btn btn-block btn-default">Clear</a>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gridProject" PageSize="50" runat="server" class="table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Project_ID").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PROJECT NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProject_Name_Eng" Text='<%# Eval("Project_Name_Eng").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PARENT TASK">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblParentTask" Text="Create Master Page" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TASK NAME ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaskName" Text="Development" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TASK CODE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaskCode" Text="TASK01" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TASK TYPE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaskType" Text="Development" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TASK DESCRIPTION">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTaskDescription" Text="Create Dynamic Master Page" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ASSIGNEE NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeveloperName" Text="Ramesh" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="30" HeaderText="STATUS">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkStatus" runat="server" CommandArgument='<%# Eval("Project_ID").ToString()%>' CssClass='<%# Eval("IsActive").ToString() =="True"?"label label-success":"label label-danger"  %>' CausesValidation="False" CommandName="ChangeStatus" Text='<%# Eval("IsActive").ToString() =="True"?"Active":"Deactive"  %>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="30px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ACTION">
                                                <ItemTemplate>
                                                    &nbsp;&nbsp;&nbsp;
                                                    <button type="button" class="label label-success" data-toggle="modal" data-target="#exampleModal">
                                                        Assign Task
                                                    </button>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-default" CommandArgument='<%# Eval("Project_ID").ToString()%>' CausesValidation="False" CommandName="EditRecord" Text="Edit"></asp:LinkButton>
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
            <!-- Bootstrap Modal -->
            <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">

                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <!-- Modal Header -->
                        <div class="modal-header">
                            <h4 class="modal-title" id="exampleModalLongTitle">Assign Task</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="card">
                            <div class="card-body fa-border">
                                <!-- Modal Body -->
                                <div class="modal-body">
                                    <asp:HiddenField ID="HiddenProjectID" runat="server" />
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group ms">

                                                <lable>PROJECT NAME</lable>
                                                <asp:TextBox ID="txtProjectName" runat="server" CssClass="form-control" Text="Test Project " disabled />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group ms">
                                                <lable>TASK NAME </lable>
                                                <asp:TextBox ID="tstTaskName" runat="server" CssClass="form-control" Text="Test Project " disabled />
                                            </div>
                                        </div>
                                        <div class="col-md-4">

                                            <div class="form-group ms">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator10"
                                                        ValidationGroup="b"
                                                        ErrorMessage="Select Developer Name"
                                                        ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Developer Name!'></i>"
                                                        ControlToValidate="ddlDeveloper"
                                                        Display="Dynamic"
                                                        runat="server" InitialValue="0">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <label>DEVELOPER <span style="color: red;">*</span></label>
                                                <asp:DropDownList ID="ddlDeveloper" runat="server" CssClass="form-control ">
                                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                                    <asp:ListItem Value="1">Kapil Vishwakarma</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator3"
                                                        ValidationGroup="b"
                                                        ErrorMessage="Select Start Date"
                                                        ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select Start Date'></i>"
                                                        ControlToValidate="txtStartDate"
                                                        Display="Dynamic"
                                                        runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:Label runat="server" CssClass="form-label">START DATE <span style="color: red;"> *</span></asp:Label>
                                                <asp:TextBox TextMode="Date" ID="txtStartDate" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator8"
                                                        ValidationGroup="b"
                                                        ErrorMessage="Select End Date"
                                                        ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Select End Date'></i>"
                                                        ControlToValidate="txtEndDate"
                                                        Display="Dynamic"
                                                        runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <asp:Label runat="server" CssClass="form-label">END DATE <span style="color: red;"> *</span></asp:Label>
                                                <asp:TextBox TextMode="Date" ID="txtEndDate" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD" />
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="txtTaskDescription">REMARK<span style="color: red;"> *</span></label>
                                                <span class="fa-pull-right">
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator7"
                                                        ValidationGroup="b"
                                                        ErrorMessage="Enter Remark"
                                                        ForeColor="Red"
                                                        Text="<i class='fa fa-exclamation-circle' title='Enter Remark!'></i>"
                                                        ControlToValidate="txtRemark"
                                                        Display="Dynamic"
                                                        runat="server">
                                                    </asp:RequiredFieldValidator>
                                                </span>
                                                <textarea
                                                    id="txtRemark"
                                                    runat="server"
                                                    class="form-control"
                                                    oninput="autoResizeTextarea(this)"
                                                    onkeypress="javascript:tbx_fnAlphaOnly(event, this);"
                                                    placeholder="Enter Remark" maxlength="100"
                                                    rows="2">
                                                </textarea>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Modal Footer -->
                                <div class="modal-footer">
                                    <asp:Button
                                        ID="btnSaveTask"
                                        runat="server"
                                        CssClass="btn btn-success"
                                        Text="Save"
                                        ValidationGroup="b" />
                                    <button
                                        type="button"
                                        class="btn btn-secondary"
                                        data-dismiss="modal">
                                        Cancel
                                    </button>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <script>
        function autoResizeTextarea(ths) {
            ths.style.height = 'auto'; // Reset height to calculate new scrollHeight
            ths.style.height = ths.scrollHeight + 'px'; // Set height to fit content
        }
    </script>
    <script>

        function closeModal() {
            let modalEl = document.getElementById('assignTaskModal');
            let modal = bootstrap.Modal.getInstance(modalEl);
            if (modal) modal.hide();
        }

        function openModal() {
            let modalEl = document.getElementById('assignTaskModal');
            let modal = bootstrap.Modal.getInstance(modalEl);
            if (modal) modal.show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

