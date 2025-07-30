<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRDaily_Attendance.aspx.cs" Inherits="mis_HR_HRDaily_Attendance" %>

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
                            <h3 class="box-title" id="Label1">E-Attendance Register</h3>
                        </div>
                        <!-- /.box-header Mark Daily Attendance (उपस्थिति चिह्नित करें )-->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">

                                <asp:label id="lblMsg" runat="server" text="" visible="true"></asp:label>


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Select Date<span style="color: red;">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:textbox id="txtDate" runat="server" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" data-date-start-date="-31d" onpaste="return false" placeholder="Select Date" clientidmode="Static" ontextchanged="txtDate_TextChanged" autopostback="true"></asp:textbox>
                                        </div>
                                        <small><span id="valtxtDate" class="text-danger"></span></small>
                                    </div>

                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Day Type<span style="color: red;">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:dropdownlist runat="server" id="ddlDayType" cssclass="form-control select2" clientidmode="Static" autopostback="true" onselectedindexchanged="ddlDayType_SelectedIndexChanged">
                                                <asp:ListItem Value="0">Working Day</asp:ListItem>
                                                <asp:ListItem Value="1">Sunday Holiday</asp:ListItem>
                                                <asp:ListItem Value="2">II-Saturday Holiday</asp:ListItem>
                                                <asp:ListItem Value="3">III-Saturday Holiday</asp:ListItem>
                                                <asp:ListItem Value="4">Tuesday Holiday</asp:ListItem>
                                                <asp:ListItem Value="5">General Holiday</asp:ListItem>
                                            </asp:dropdownlist>
                                        </div>

                                    </div>

                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label><span style="color: red">*</span>
                                        <asp:dropdownlist runat="server" id="ddlOffice" cssclass="form-control select2" autopostback="true" onselectedindexchanged="ddlOffice_SelectedIndexChanged" clientidmode="Static" enabled="false">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:dropdownlist>
                                    </div>
                                    <small><span id="valtxtOffice" class="text-danger"></span></small>
                                </div>
                                <div class="col-md-2">
                                    <label>&nbsp;</label>
                                    <asp:button runat="server" cssclass="btn btn-success btn-block" text="Search" id="btnSearch" onclick="btnSearch_Click" onclientclick="return validateform();" />
                                </div>

                                <div class="col-md-2">
                                    <label>&nbsp;</label>
                                    <asp:button runat="server" cssclass="btn btn-success btn-block" text="Save Attendance" id="BtnSubmit" onclick="BtnSubmit_Click" onclientclick="return confirmSaveform();" enabled="false" />
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-12">

                                    <asp:label id="lblGridHeading" runat="server" text=""></asp:label>
                                    <asp:gridview id="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys " autogeneratecolumns="False" datakeynames="Emp_ID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%"></ItemStyle>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Employee Name" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="150">
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


                                            <%--  <asp:TemplateField HeaderText="Login Time" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="300">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" CssClass="form-control time" ID="txtLoginTime" MaxLength="8" TextMode="Time"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Logout Time" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="300">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtLogoutTime" CssClass="form-control txtwidth" placeholder="0" TextMode="Time" MaxLength="8" onkeypress="return validateNum(event)"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="150">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                                        <asp:ListItem>Absent</asp:ListItem>
                                                        <asp:ListItem>Casual Leave</asp:ListItem>
                                                        <asp:ListItem>Earn Leave</asp:ListItem>
                                                        <asp:ListItem>First-half Leave</asp:ListItem>
                                                        <asp:ListItem>Late Login</asp:ListItem>
                                                        <asp:ListItem>Medical Leave</asp:ListItem>
                                                        <asp:ListItem>Optional Leave</asp:ListItem>
                                                        <asp:ListItem Selected="True">Present</asp:ListItem>
                                                        <asp:ListItem>Second-Half Leave</asp:ListItem>
                                                        <asp:ListItem>Tour</asp:ListItem>
                                                        <%--                                         <asp:ListItem>Sunday</asp:ListItem>
                                                        <asp:ListItem>II Saturday</asp:ListItem>
                                                        <asp:ListItem>III Saturday</asp:ListItem>
                                                        <asp:ListItem>General Holiday</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remark" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="200">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control txtwidth"></asp:TextBox>
                                                    <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Save" OnClientClick="return validateform1();" ToolTip='<%#Eval("Emp_ID")%>' Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%-- <asp:TemplateField HeaderText="Save" ItemStyle-CssClass="RemovePadding" ItemStyle-Width="300">
                                                <ItemTemplate>
                                                    <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Save" OnClick="BtnSave_Click" OnClientClick="return validateform1();" ToolTip='<%#Eval("Emp_ID")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:gridview>
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

    <%-- <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>--%>
    <script>
        //$('.datatable').DataTable({
        //    paging: false,
        //    columnDefs: [{
        //        targets: 'no-sort',
        //        orderable: false
        //    }],
        //    dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
        //      '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
        //      '<"row"<"col-sm-5"i><"col-sm-7"p>>',
        //    fixedHeader: {
        //        header: true
        //    },
        //    buttons: {
        //        buttons: [{
        //            extend: 'print',
        //            text: '<i class="fa fa-print"></i> Print',
        //            title: $('h1').text(),
        //            exportOptions: {
        //                columns: ':not(.no-print)'
        //            },
        //            footer: true,
        //            autoPrint: true
        //        }, {
        //            extend: 'excel',
        //            text: '<i class="fa fa-file-excel-o"></i> Excel',
        //            title: $('h1').text(),
        //            exportOptions: {
        //                columns: ':not(.no-print)'
        //            },
        //            footer: true
        //        }],
        //        dom: {
        //            container: {
        //                className: 'dt-buttons'
        //            },
        //            button: {
        //                className: 'btn btn-default'
        //            }
        //        }
        //    }
        //});


        $('.DateAdd').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        })

        $("#txtDate").datepicker({
            format: 'dd/mm/yyyy',
            endDate: '0d',
            autoclose: true,
            maxDate: "+1M +1D"
        });

        function validateform() {
            // debugger;
            var msg = "";
            //var confirmsts = confirm("Do you really want to save attendance?");
            //if (confirmsts == false) {
            //    return false;
            //}

            $("#valtxtDate").html("");
            if (document.getElementById('<%=txtDate.ClientID%>').value.trim() == "") {
                msg = msg + "Please Select Date. \n";
                $("#valtxtDate").html("Please Select Date.");
            }
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
            var confirmsts = confirm("Do you really want to save attendance?");
            if (confirmsts == false) {
                return false;
            }
        }
    </script>
</asp:Content>

