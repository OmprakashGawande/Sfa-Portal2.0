<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="MstProject.aspx.cs" Inherits="mis_Master_MstProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        #txtDiscription {
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

        #txtDiscription {
            border-color: #5c6ac4;
            outline: none;
            box-shadow: 0 0 0 3px rgba(92, 106, 196, 0.3);
        }
    </style>
    <link href="../css/select2.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-12">
                <div>

                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                    <div class="card mt-3  border-warning">
                        <div class="card-header">
                            <h4>Project Master</h4>
                        </div>
                        <div class="card-body">
                            <div class="row g-3">
                                <div class="col-xl-3 col-sm-6 position-relative">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorProjectName"
                                                ValidationGroup="b"
                                                ErrorMessage="Please Enter Project."
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Please Enter Project.'></i>"
                                                ControlToValidate="txtProjectName"
                                                Display="Dynamic"
                                                runat="server" />
                                        </span>
                                        <label runat="server">PROJECT NAME <span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtProjectName" runat="server" placeholder="Enter Project Name" CssClass="form-control"></asp:TextBox>
                                        <div class="invalid-tooltip">Please Enter Project.</div>
                                    </div>
                                </div>

                                <div class="col-xl-3 col-sm-6 position-relative">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorTechnology"
                                                ValidationGroup="b"
                                                ErrorMessage="Please Select Technology."
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Please Select Technology.'></i>"
                                                ControlToValidate="ddlTechnology"
                                                InitialValue=""
                                                Display="Dynamic"
                                                runat="server" />
                                        </span>
                                        <label class="form-label">TECHNOLOGY <span style="color: red;">*</span></label>
                                        <%--<asp:DropDownList ID="ddlTechnology" runat="server" ClientIDMode="Static" CssClass="form-control" multiple multiselect-search="true" multiselect-select-all="true" multiselect-max-items="3"></asp:DropDownList>--%>
                                        <asp:ListBox ID="ddlTechnology" runat="server" SelectionMode="Multiple" multiselect-search="true" multiselect-select-all="true" multiselect-max-items="3" CssClass="form-control" Height="150px">

                                        </asp:ListBox>

                                    </div>
                                </div>

                                <div class="col-xl-3 col-sm-6 position-relative">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorTypeOfProject"
                                                ValidationGroup="b"
                                                ErrorMessage="Please Select Type of Project."
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Please Select Type of Project.'></i>"
                                                ControlToValidate="ddlTypeofProject"
                                                InitialValue="0"
                                                Display="Dynamic"
                                                runat="server" />
                                        </span>
                                        <label runat="server">TYPE OF PROJECT <span style="color: red;">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlTypeofProject" CssClass="form-select select2"></asp:DropDownList>

                                    </div>
                                </div>

                                <div class="col-xl-3 col-sm-6 position-relative">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorOwner"
                                                ValidationGroup="b"
                                                ErrorMessage="Please Select Incharge."
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Please Select Incharge.'></i>"
                                                ControlToValidate="ddlOwner"
                                                InitialValue="0"
                                                Display="Dynamic"
                                                runat="server" />
                                        </span>
                                        <label runat="server">INCHARGE <span style="color: red;">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlOwner" ClientIDMode="Static" CssClass="form-control select2"></asp:DropDownList>

                                    </div>
                                </div>

                                <div class="col-xl-3 col-sm-6 position-relative">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorWorkStartDate"
                                                ValidationGroup="b"
                                                ErrorMessage="Please Select Work Start Date."
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Please Select Work Start Date.'></i>"
                                                ControlToValidate="txtWorkOrderDate"
                                                Display="Dynamic"
                                                runat="server" />
                                        </span>
                                        <label>WORK START DATE  <span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtWorkOrderDate" runat="server" placeholder="DD/MM/YYYY" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" CssClass="form-control datepicker-here" data-language="en" />

                                    </div>
                                </div>

                                <div class="col-xl-3 col-sm-6 position-relative">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorTargetDate"
                                                ValidationGroup="b"
                                                ErrorMessage="Please Select Target Date Of Completion."
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Please Select Target Date Of Completion.'></i>"
                                                ControlToValidate="txtComplitionDate"
                                                Display="Dynamic"
                                                runat="server" />
                                        </span>
                                        <label>TARGET DATE OF COMPLETION <span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtComplitionDate" runat="server" placeholder="DD/MM/YYYY" autocomplete="off" data-date-format="dd/mm/yyyy" data-date-autoclose="true" CssClass="form-control datepicker-here" data-language="en" />
                                        <div class="invalid-tooltip">Please Select Target Date Of Completion.</div>
                                    </div>
                                </div>

                                <div class="col-xl-6 col-sm-6 position-relative">
                                    <div class="form-group">
                                        <span class="fa-pull-right">
                                            <asp:RequiredFieldValidator
                                                ID="RequiredFieldValidatorDescription"
                                                ValidationGroup="b"
                                                ErrorMessage="Please Enter Description."
                                                ForeColor="Red"
                                                Text="<i class='fa fa-exclamation-circle' title='Please Enter Description.'></i>"
                                                ControlToValidate="txtDiscription"
                                                Display="Dynamic"
                                                runat="server" />
                                        </span>
                                        <label>DESCRIPTION <span style="color: red;">*</span></label>
                                        <textarea id="txtDiscription" runat="server" class="form-control" oninput="autoResizeTextarea(this)" maxlength="140" rows="2" placeholder="Enter Description"></textarea>
                                        <div class="invalid-tooltip">Please Enter Description.</div>
                                    </div>
                                </div>

                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-xl-3">
                                    <div class="form-group">
                                        <asp:Button runat="server" Style="margin-top: 22px;" CssClass="btn btn-block btn-outline-success" ValidationGroup="b" ID="btnSave1" Text="Save" OnClick="btnSave1_Click" />
                                           <a href="MstProject.aspx" style="margin-top: 22px;" class="btn btn-block   btn-outline-danger">Clear</a>
                                    </div>
                                </div>                                
                            </div>
                        </div>
                    </div>
                    <hr />

                    <%--grid--%>
                    <div class="card border-warning">
                        <div class="card-header">
                            <h4>Project Detail</h4>
                        </div>

                        <div class="card-body">
                            <div class="row" style="padding: 0px 9px 2px 15px;" id="div1" runat="server">


                                <div class="table-responsive dt-ext ">
                                    <div class="col-md-12">
                                        <asp:GridView ID="Grid" PageSize="50" runat="server" class="datatable display  table table-hover table-bordered pagination-ys" ShowHeaderWhenEmpty="false" AutoGenerateColumns="False" OnRowCommand="Grid_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("ProjectId").ToString() %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PROJECT NAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProjectName" Text='<%# Eval("ProjectName").ToString() %>' runat="server" />
                                                        <asp:Label Visible="false" ID="lblTypeOfProjectId" Text='<%# Eval("TypeOfProjectId") %>' runat="server"></asp:Label>
                                                        <asp:Label Visible="false" ID="lblTechnologyId" Text='<%# Eval("TechnologyId") %>' runat="server"></asp:Label>

                                                        <asp:TextBox runat="server" ID="txtProjectName" AutoComplete="off" Text="Text Project Name" Visible="false"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TECHNOLOGY ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTechnologyName" Text='<%# Eval("TechnologyName").ToString() %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TYPE OF PROJECT ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTypeOfProject" Text='<%# Eval("TypeOfProjectName").ToString() %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="INCHARGE NAME ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOwnerName" Text='<%# Eval("OwnerName").ToString() %>' runat="server" />
                                                        <asp:Label ID="lblOwnerId" Text='<%# Eval("OwnerId").ToString() %>' runat="server" Visible="false" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WORK START DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWorkOrderDate" Text='<%# Eval("WorkOrderDate").ToString() %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="TARGET DATE OF COMPLITION">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblComplitionDate" Text='<%# Eval("ComplitionDate").ToString() %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DISCRIPTION ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDiscription" Text='<%# Eval("Discription").ToString() %>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="30" HeaderText="STATUS">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkStatus" runat="server" CommandArgument='<%# Eval("ProjectId").ToString()%>' CssClass='<%# Eval("IsActive").ToString() =="True"?"btn btn-xs btn-pill  btn-success":"btn  btn-xs  btn-pill  btn-danger"  %>' CausesValidation="False" CommandName="ChangeStatus" Text='<%# Eval("IsActive").ToString() =="True"?"Active":"Deactive"  %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="30px"></ItemStyle>
                                                </asp:TemplateField>
                                                <%--   <asp:TemplateField HeaderText="ADD MANPOWER">
                                                            <ItemTemplate>
                                                                <button type="button" class="label label-success" data-toggle="modal" data-target="#exampleModal">
                                                                    ADD 
                                                                </button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="ADD MANPOWER">
                                                    <ItemTemplate>
                                                        <asp:LinkButton
                                                            ID="btnAddManpower"
                                                            runat="server"
                                                            Text="Add"
                                                            CssClass="btn btn-xs btn-pill btn-success"
                                                            CommandName="AddManpower"
                                                            CommandArgument='<%# Eval("ProjectId") %>' />
                                                        <%-- prevent postback if needed --%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ADD TASK">
                                                    <ItemTemplate>
                                                        <asp:LinkButton
                                                            ID="btnTask"
                                                            runat="server"
                                                            Text="Add"
                                                            CssClass="btn btn-xs btn-pill btn-success"
                                                            CommandName="AddTask"
                                                            CommandArgument='<%# Eval("ProjectId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ACTION">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="edit" CommandArgument='<%# Eval("ProjectId").ToString()%>' CausesValidation="False" CommandName="EditRecord"><i class="icon-pencil-alt"></i></asp:LinkButton>
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

        <!-- Bootstrap Modal -->
        <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">

            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title" id="exampleModalLongTitle">Add Man Power</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>


                </div>
            </div>
        </div>
        <div id="exampleModal" class="modal fade bd-example-modal-xl" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-xl">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myLargeModalLabel">Add Man Power</h4>
                        <button class="btn-close py-0" type="button" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body dark-modal">
                        <div class="card">
                            <br />
                            <div class="row" style="padding: 7px; s">

                                <div class="col-md-12">
                                    <asp:Label runat="server" ID="lblMsgManPower" Text=""></asp:Label></div>


                            </div>
                            <div class="card-body fa-border">
                                <!-- Modal Body -->
                                <div class="">
                                    <asp:HiddenField ID="HiddenProjectID" runat="server" />
                                    <div class="">
                                        <div class="">
                                            <div class="row">

                                                <div class="col-xl-3 col-sm-6 position-relative">

                                                    <span class="fa-pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="c"
                                                            ErrorMessage="Select" ForeColor="Red"
                                                            Text="<i class='fa fa-exclamation-circle' title='Select Employee'></i>"
                                                            ControlToValidate="ddlEmployee" Display="Dynamic" runat="server" InitialValue="0">
                                                        </asp:RequiredFieldValidator>
                                                    </span>


                                                    <label>EMPLOYEE <span style="color: red;">*</span></label>
                                                    <div class=" form-group">
                                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-select select2">
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                                <div class="col-xl-3 col-sm-6 position-relative ">
                                                    <div class="form-group ms">
                                                        <label>Team Lead</label>
                                                        <div class="ms form-group">
                                                            <asp:DropDownList ID="ddlTeamLead" runat="server" CssClass="form-control select2">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xl-3 col-sm-6 position-relative">

                                                    <div class="form-group ms">


                                                        <span class="fa-pull-right">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="c"
                                                                ErrorMessage="Select" ForeColor="Red"
                                                                Text="<i class='fa fa-exclamation-circle' title='Select Role'></i>"
                                                                ControlToValidate="ddlRole" Display="Dynamic" runat="server" InitialValue="0">
                                                            </asp:RequiredFieldValidator>
                                                        </span>

                                                        <label>ROLE <span style="color: red;">*</span></label>
                                                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>



                                                    </div>
                                                </div>
                                                <div class="col-xl-3 col-sm-6 position-relative">
                                                    <div class="form-group">
                                                        <label>TASK CATEGOREY<span style="color: red;"> *</span></label>
                                                        <span class="fa-pull-right">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ValidationGroup="c"
                                                                ErrorMessage="Select" ForeColor="Red"
                                                                Text="<i class='fa fa-exclamation-circle' title='Select Task Category'></i>"
                                                                ControlToValidate="ddlWorkCategoryId" Display="Dynamic" runat="server">
                                                            </asp:RequiredFieldValidator>
                                                        </span>
                                                        <%-- <asp:ListBox ID="ddlWorkCategoryId" runat="server" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>--%>
                                                        <asp:DropDownList runat="server" ID="ddlWorkCategoryId" ClientIDMode="Static"
                                                            CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-xl-3 col-sm-6 position-relative">
                                                    <div class="form-group">
                                                        <span class="fa-pull-right">
                                                            <asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator8"
                                                                ValidationGroup="c"
                                                                ErrorMessage="Select Allocation Date"
                                                                ForeColor="Red"
                                                                Text="<i class='fa fa-exclamation-circle' title='Select Allocation Date'></i>"
                                                                ControlToValidate="txtAllocationDate"
                                                                Display="Dynamic"
                                                                runat="server">
                                                            </asp:RequiredFieldValidator>
                                                        </span>

                                                        <label>ALLOCATION DATE <span style="color: red;">*</span></label>
                                                        <asp:TextBox ID="txtAllocationDate"
                                                            runat="server" CssClass="form-control datepicker-here" data-language="en"
                                                            placeholder="DD/MM/YYYY"
                                                            autocomplete="off" data-date-format="dd/mm/yyyy"
                                                            data-date-autoclose="true" />
                                                    </div>
                                                </div>

                                                <div class="col-md-2 mt-3">
                                                    <div class="form-group">
                                                        <asp:Button runat="server" Style="margin-top: 20px;" CssClass="btn btn-block btn-success" ID="btnAddManPower" Text="Add" OnClick="btnAddManPower_Click" ValidationGroup="c" />
                                                    </div>

                                                </div>
                                            </div>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <hr />
                                                    <h4>Man Power Detail</h4>
                                                    <br />

                                                    <asp:GridView ID="grdManpower" class=" table  table-bordered  table-hover" runat="server" AutoGenerateColumns="false" OnRowCommand="grdManpower_RowCommand">
                                                        <Columns>
                                                            <asp:BoundField DataField="EmployeeName" HeaderText="Employee" />
                                                            <asp:BoundField DataField="TeamLead" HeaderText="Team Lead" />
                                                            <asp:BoundField DataField="RoleName" HeaderText="Role" />
                                                            <asp:BoundField DataField="WorkCategoreyName" HeaderText="Categorey" />
                                                            <asp:BoundField DataField="AllocationDate" HeaderText="Allocation Date" />
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteRow" CommandArgument='<%# Container.DataItemIndex %>' Text="Delete" CssClass="btn btn-danger btn-sm" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="GridManPowerDetail" class="table table-bordered table-hover " runat="server" AutoGenerateColumns="false" OnRowCommand="GridManPowerDetail_RowCommand">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("MainPowerId").ToString() %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee" />
                                                                <asp:BoundField DataField="TeamLeadName" HeaderText="Team Lead" />
                                                                <asp:BoundField DataField="RoleName" HeaderText="Role" />
                                                                <asp:BoundField DataField="WorkCategory" HeaderText="Category" />
                                                                <asp:BoundField DataField="AllocationDate" HeaderText="Allocation Date" />
                                                                <asp:TemplateField HeaderText="STATUS">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkStatus" runat="server"
                                                                            CommandArgument='<%# Eval("MainPowerId").ToString()%>'
                                                                            CssClass='<%# Eval("IsActive").ToString() =="True" ? "btn btn-xs btn-pill  btn-success" : "btn btn-xs btn-pill  btn-danger"  %>'
                                                                            CausesValidation="False" CommandName="ChangeStatus"
                                                                            Text='<%# Eval("IsActive").ToString() =="True" ? "Active" : "Deactive"  %>'></asp:LinkButton>
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
                                <!-- Modal Footer -->
                                <div class="modal-footer">
                                    <asp:Button
                                        ID="btnSaveManPower"
                                        runat="server"
                                        CssClass="btn btn-success"
                                        Text="Save"
                                        OnClick="btnSaveManPower_Click" />
                                    <button
                                        type="button"
                                        class="btn btn-secondary "
                                        data-bs-dismiss="modal">
                                        Cancel
                                    </button>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>








        <!-- Bootstrap Modal -->
        <div class="modal fade" id="exampleModal2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title" id="exampleModalLongTitle2">Task Detail</h4>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <asp:Label ID="lblMsgTask" runat="server" Text=""></asp:Label>
                    <div class="card">
                        <div class="card-body fa-border">
                            <!-- Modal Body -->
                            <div class="modal-body">
                                <asp:HiddenField ID="HiddenField1" runat="server" />


                                <div class="card">
                                    <div class="card-body">

                                        <div class="row">
                                            <%--<div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>PROJECT NAME <span style="color: red;">*</span></label>
                                                        <span class="fa-pull-right">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="T"
                                                                ErrorMessage="Select Project Name" InitialValue="0" ForeColor="Red"
                                                                Text="<i class='fa fa-exclamation-circle' title='Select Project !'></i>"
                                                                ControlToValidate="ddlProjectName" Display="Dynamic" runat="server">
                                                            </asp:RequiredFieldValidator>
                                                        </span>
                                                        <asp:DropDownList runat="server" ID="ddlProjectName" ClientIDMode="Static"
                                                            CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>--%>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <label>PARENT TASK</label>
                                                    <asp:DropDownList runat="server" ID="ddlParentTask" ClientIDMode="Static"
                                                        CssClass="form-control ">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    <span class="fa-pull-right">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ValidationGroup="T"
                                                            ErrorMessage="Enter Task Name" ForeColor="Red"
                                                            Text="<i class='fa fa-exclamation-circle' title='Enter Task Name !'></i>"
                                                            ControlToValidate="txtTaskName" Display="Dynamic" runat="server">
                                                        </asp:RequiredFieldValidator>
                                                    </span>
                                                    <label>TASK NAME<span style="color: red;"> *</span></label>
                                                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Enter Task Name" ID="txtTaskName" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">


                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label for="txtTaskDescription">TASK DESCRIPTION<span style="color: red;"> *</span></label>
                                                    <span class="fa-pull-right">
                                                        <asp:RequiredFieldValidator
                                                            ID="RequiredFieldValidator15"
                                                            ValidationGroup="T"
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
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <asp:Button runat="server" CssClass="btn btn-block btn-success" ID="btnSaveTask" Text="Save" OnClick="btnSaveTask_Click" ValidationGroup="T" />
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <a href="MstProject.aspx" class="btn btn-block btn-default">Clear</a>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="row">
                                            <div class="col-md-12">

                                                <asp:GridView ID="GridTaskDetail" class="datatable  table table-hover table-bordered pagination-ys" runat="server" AutoGenerateColumns="false" OnRowCommand="GridTaskDetail_RowCommand">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("TaskId").ToString() %>' runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ParentTaskName" HeaderText="PARENT TASK NAME" />
                                                        <asp:BoundField DataField="TaskName" HeaderText="TASK NAME" />
                                                        <asp:BoundField DataField="TaskCode" HeaderText="TASK CODE" />
                                                        <%--    <asp:BoundField DataField="WorkCategoryEng" HeaderText="TASK CATEGOERY" />--%>
                                                        <asp:BoundField DataField="TaskDescription" HeaderText="TASK DISCRIPTION" />

                                                        <asp:TemplateField HeaderText="STATUS">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkStatus" runat="server"
                                                                    CommandArgument='<%# Eval("TaskId").ToString()%>'
                                                                    CssClass='<%# Eval("IsActive").ToString() =="True" ? "label label-success" : "label label-danger"  %>'
                                                                    CausesValidation="False" CommandName="ChangeStatus"
                                                                    Text='<%# Eval("IsActive").ToString() =="True" ? "Active" : "Deactive"  %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>

                                </div>


                                <!-- Modal Footer -->
                                <div class="modal-footer">
                                    <%--  <asp:Button
                                        ID="Button2"
                                        runat="server"
                                        CssClass="btn btn-success"
                                        Text="Save"
                                        ValidationGroup="b" />--%>
                                    <button
                                        type="button"
                                        class="btn btn-secondary"
                                        data-dismiss="modal">
                                        Close
                                    </button>
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
</asp:Content>

