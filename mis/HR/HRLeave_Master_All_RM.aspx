<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRLeave_Master_All_RM.aspx.cs" Inherits="mis_HR_HRLeave_Master_All_RM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        th.sorting, th.sorting_asc, th.sorting_desc {
            background: teal !important;
            color: white !important;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 8px 5px;
        }

        a.btn.btn-default.buttons-excel.buttons-html5 {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-pdf.buttons-html5 {
            background: #009688c9;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.btn.btn-default.buttons-print {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

            a.btn.btn-default.buttons-print:hover, a.btn.btn-default.buttons-pdf.buttons-html5:hover, a.btn.btn-default.buttons-excel.buttons-html5:hover {
                box-shadow: 1px 1px 1px #808080;
            }

            a.btn.btn-default.buttons-print:active, a.btn.btn-default.buttons-pdf.buttons-html5:active, a.btn.btn-default.buttons-excel.buttons-html5:active {
                box-shadow: 1px 1px 1px #808080;
            }

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }

        table {
            white-space: nowrap;
        }

        .table th {
            background-color: cadetblue;
        }

        .LeaveColumn {
            width: 100px !important;
        }

        table tr th:nth-child(4) {
            width: 75px !important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Leave Assign Master <small>(Assign Leaves To All Employee/Officers For a Complete Year)</small></h3>
                        </div>
                        <!-- /.box-header Mark Daily Attendance (उपस्थिति चिह्नित करें )-->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">

                                <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Select Year<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlFinancial_Year" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlFinancial_Year_SelectedIndexChanged">
                                            <asp:ListItem>select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Select Leave Type<span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlLeave_Type" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlLeave_Type_SelectedIndexChanged">
                                            <asp:ListItem>select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label><span style="color: red">*</span>
                                        <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <small><span id="valtxtOffice" class="text-danger"></span></small>
                                </div>
                                <div class="col-md-2">
                                    <label>&nbsp;</label>
                                    <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Search" ID="btnSearch" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                                </div>

                                <div class="col-md-2">
                                    <label>&nbsp;</label>
                                    <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Save" ID="BtnSubmit" OnClick="BtnSubmit_Click" OnClientClick="return confirmSaveform();" Enabled="false" />
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-8">

                                    <asp:Label ID="lblGridHeading" runat="server" Text=""></asp:Label>
                                    <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys " AutoGenerateColumns="False" DataKeyNames="Emp_ID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Employee Name" ItemStyle-CssClass="RemovePadding " ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblEmployee" Text='<%# Bind("Emp_Name") %>' runat="server" Visible="True" />
                                                    <asp:Label ID="LblEmployeeId" Text='<%# Bind("Emp_ID") %>' runat="server" Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Designation" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label ID="LblDesignation" Text='<%# Bind("Designation_Name") %>' runat="server" Visible="True" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Closing of last year (Days)" ItemStyle-CssClass="RemovePadding LeaveColumn" ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtLastYearLeave" runat="server" CssClass="form-control txtwidth" Text="0" onkeypress="return validateNum(event)" onpaste="return false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Days credited in this year (Days)" ItemStyle-CssClass="RemovePadding LeaveColumn" ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtAvailableLeave" runat="server" CssClass="form-control txtwidth" Text="0" onkeypress="return validateNum(event)" onpaste="return false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total leave for selected year (Days)" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAvailableSelectYear" Text='' runat="server" Visible="True" />
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlOffice%>').seletedIndex == 0) {
                msg = msg + "Please Select Office. \n";
                $("#valtxtOffice").html("Please Select Office.");
            }
            if (msg != "") {
                alert(msg);
                return false;
            }

        }

        function confirmSaveform() {
            var confirmsts = confirm("Do you really want to save leave details?");
            if (confirmsts == false) {
                return false;
            }
        }
    </script>
</asp:Content>



