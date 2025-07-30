<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRDateRangeWiseAttendenceReport.aspx.cs" Inherits="HRDateRangeWiseAttendenceReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        input[type="radio"] {
            margin-right: 2px;
            margin-left: 6px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12 ">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Date Range Wise Attendence Report</h3>
                        </div>
                        <div class="box-body ">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>From Date<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="DD/MM/YYYY" class="form-control DateAdd" data-provide="datepicker" data-date-end-date="0d" ID="txtStartDate" ClientIDMode="Static" onkeypress="return validateNum(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>To Date<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="DD/MM/YYYY" class="form-control DateAdd" data-provide="datepicker" data-date-end-date="0d" ID="txtEndDate" ClientIDMode="Static" onkeypress="return validateNum(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlOffice" runat="server" CssClass="form-control Select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Employee</label>
                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend style="margin-bottom: 12px;">Working Hours</legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:RadioButtonList ID="Rbtn_Type1" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" onclick="checkChecks1()">
                                                        <asp:ListItem Value="1">Login but not logout</asp:ListItem>
                                                        <asp:ListItem Value="2">Less then 7 hours</asp:ListItem>
                                                        <asp:ListItem Value="3">More then 7 hours</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend style="margin-bottom: 12px;">Total Average</legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:RadioButtonList ID="Rbtn_Type2" runat="server" ClientIDMode="Static" RepeatDirection="Horizontal" onclick="checkChecks2()">
                                                        <asp:ListItem Value="4">Working hours</asp:ListItem>
                                                        <asp:ListItem Value="5">Working less then 7 hours</asp:ListItem>
                                                        <asp:ListItem Value="6">Working more then 7 hours</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                                <%--<div class="col-md-12">
                                    <div class="form-group">
                                        <asp:RadioButtonList ID="Rbtn_Type" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Selected="True">Login but not logout</asp:ListItem>
                                            <asp:ListItem Value="3">Working less then 7 hours</asp:ListItem>
                                            <asp:ListItem Value="4">Total average working hours</asp:ListItem>
                                            <asp:ListItem Value="5">Average working less then 7 hours</asp:ListItem>
                                            <asp:ListItem Value="6">Average working more then 7 hours</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>--%>
                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" runat="server" class="btn btn-block btn-success" Text="Search" OnClientClick="return validateform();" OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box-body">
                                        <asp:Label ID="lblMsg" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered"
                                                AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" Text='<%# Eval("Status").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                    <asp:TemplateField HeaderText="Attendance Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAttendanceDate" Text='<%# Eval("AttendanceDate").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LoginTime">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLoginTime" Text='<%# Eval("LoginTime").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LogoutTime">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLogoutTime" Text='<%# Eval("LogoutTime").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="WorkingHours" HeaderText="Average Working Hours" />
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
            <div id="myModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Late Login/Early Logout Permission - Detail</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateRows="false">
                                            <Fields>
                                                <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                <asp:BoundField DataField="PermissionType" HeaderText="Permission Type" />
                                                <asp:BoundField DataField="InsertedOn" HeaderText="Applied Date" />
                                                <asp:BoundField DataField="Login_LogoutTime" HeaderText="Intimation For - O'Clock" />
                                                <asp:BoundField DataField="Emp_Remark" HeaderText="Reason" />
                                            </Fields>
                                        </asp:DetailsView>
                                    </div>
                                </div>
                            </div>
                            <fieldset>
                                <legend>HR Reply Section</legend>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Status<span style="color: red;">*</span></label>
                                            <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                                <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label>Remark</label>
                                            <asp:TextBox ID="txtHRRemark" runat="server" TextMode="MultiLine" Rows="3" placeholder="Enter Remark..." class="form-control" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>
    <script src="js/dataTables.buttons.min.js"></script>
    <script src="js/buttons.flash.min.js"></script>
    <script src="js/jszip.min.js"></script>
    <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>
    <script src="js/buttons.html5.min.js"></script>
    <script src="js/buttons.print.min.js"></script>
    <script src="js/buttons.colVis.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
            pageLength: 100,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });
        function callalert() {
            $("#myModal").modal('show');
        }
        $('#txtStartDate').datepicker({
            autoclose: true
        });
        $('#txtEndDate').datepicker({
            autoclose: true
        });
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=txtStartDate.ClientID%>').value.trim() == "") {
                msg += "Select Start Date. \n";
            }
            if (document.getElementById('<%=txtEndDate.ClientID%>').value.trim() == "") {
                msg += "Select End Date. \n";
            }
            if (document.getElementById('<%=ddlOffice.ClientID%>').selectedIndex == 0) {
                msg += "Select Office.\n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }
        function checkChecks1() {
            var elementRef = document.getElementById('<%= Rbtn_Type2.ClientID %>');
            var inputElementArray = elementRef.getElementsByTagName('input');
            for (var i = 0; i < inputElementArray.length; i++) {
                var inputElement = inputElementArray[i];
                inputElement.checked = false;
            }
            return false;
        }
        function checkChecks2() {
            var elementRef = document.getElementById('<%= Rbtn_Type1.ClientID %>');
            var inputElementArray = elementRef.getElementsByTagName('input');
            for (var i = 0; i < inputElementArray.length; i++) {
                var inputElement = inputElementArray[i];
                inputElement.checked = false;
            }
            return false;
        }
    </script>
</asp:Content>

