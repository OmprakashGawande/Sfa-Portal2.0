<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRDateRangeWiseAttendenceRpt_New.aspx.cs" Inherits="mis_HR_HRDateRangeWiseAttendenceRpt_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        input[type="radio"] {
            margin-right: 2px;
            margin-left: 6px;
        }

        .cssPedding {
            padding: 3px !important;
        }

        .dataTables_wrapper, div#ModalAttendence {
            font-size: 12px;
        }

            div#ModalAttendence .form-control {
                height: 26px;
                padding: 1px 4px;
                font-size: 12px;
            }

            .dataTables_wrapper th, .dataTables_wrapper td {
                padding: 5px !important;
            }

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

        .dt-buttons {
            margin-bottom: 11px;
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
                            <asp:Label ID="lblMsg" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                        </div>
                        <div class="box-body ">
                            <div class="row">
                                <div class="col-md-2">
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
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>To Date<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="DD/MM/YYYY" class="form-control DateAdd" data-provide="datepicker" ID="txtEndDate" ClientIDMode="Static" onkeypress="return validateNum(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Office<span style="color: red;"> *</span></label>
                                        <asp:DropDownList ID="ddlOffice" runat="server" CssClass="form-control Select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Employee Type</label><span style="color: red">*</span>
                                        <asp:DropDownList ID="ddlEmployeeType" runat="server" class="form-control select2" OnSelectedIndexChanged="ddlEmployeeType_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="Permanent">Permanent Employee</asp:ListItem>
                                            <asp:ListItem Value="Fixed Employee">Fixed Employee</asp:ListItem>
                                            <asp:ListItem Value="Samvida Employee">Samvida Employee</asp:ListItem>
                                            <asp:ListItem Value="Outsource Employee">Outsource Employee</asp:ListItem>
					    <asp:ListItem Value="Contigent Employee">Contigent Employee</asp:ListItem>
					    <asp:ListItem Value="Theka Shramik">Theka Shramik</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Employee</label>
                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" runat="server" class="btn btn-block btn-success" Style="margin-top: 24px;" Text="Search" OnClientClick="return validateform();" OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                            </div>


                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box-body">
                                        <asp:Label ID="lbltotalworkingdays" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                                        <br />
                                        <br />
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" DataKeyNames="Emp_ID" runat="server" class="datatable table table-hover table-bordered"
                                                AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found" OnRowCommand="GridView1_RowCommand">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmp_Name" Text='<%# Eval("Emp_Name").ToString() %>' runat="server" />
                                                            <asp:HiddenField ID="HF_Emp_ID" Value='<%# Eval("Emp_ID").ToString() %>' runat="server" />
                                                            <%--  <asp:Label ID="lblEmp_ID" CssClass="hidden" Text='<%# Eval("Emp_ID").ToString() %>' runat="server" />--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PresentDays" HeaderText="Full Present E-Attendance" />
                                                    <asp:BoundField DataField="AdministrativeApproved" HeaderText="Administrative Approved (Days)" />
                                                    <asp:BoundField DataField="TourDays" HeaderText="Tour (Days)" />
                                                    <asp:BoundField DataField="OnLeaveDays" HeaderText="On Leave (Days)" />
                                                    <asp:BoundField DataField="LwpDays" HeaderText="LWP (Days)" />
                                                    <asp:BoundField DataField="DifferenceDays" HeaderText="Difference In Days" />
                                                    <%--

                                                    <asp:BoundField DataField="OfficialWorkingDays" HeaderText="Official Working Days" />
                                                    <asp:BoundField DataField="TotalHoliDays" HeaderText="Holidays" />--%>
                                                    <%--<asp:BoundField DataField="AbsentDays" HeaderText="Absent Days" />--%>
                                                    <asp:BoundField DataField="TotalWorkingHours" HeaderText="Total Working Hours" />
                                                    <asp:BoundField DataField="AverageWorkingHours" HeaderText="Average Working Hours" />
                                                    <asp:ButtonField ButtonType="Link" CommandName="View" ControlStyle-CssClass="label label-info" HeaderText="Action" Text="Admin Approval" />
                                                    <%-- <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkview" CssClass="label label-info" runat="server" CommandName="Select" Text="Admin Approval"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
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
            <div id="ModalAttendence" class="modal fade" role="dialog">
                <div class="modal-dialog modal-lg" style="width: 90%;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Date Wise Attendence Of 
                                <asp:Label ID="lblEmployee" runat="server" Text=""></asp:Label></h4>
                            <asp:Label ID="lblMsgEmp" runat="server" ClientIDMode="Static" Text=""></asp:Label><br />
                            <asp:Label ID="lblAvailableLeave" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView2" DataKeyNames="Per_ID" runat="server" class="datatable table table-hover table-bordered"
                                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found" OnRowDataBound="GridView2_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" Text='<%# Eval("Status").ToString()%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" Text='<%# Eval("DATE").ToString()%>' runat="server" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:BoundField DataField="DATE" HeaderText="Date" />--%>
                                                <asp:BoundField DataField="Day" HeaderText="Day" />
                                                <asp:BoundField DataField="LoginTime" HeaderText="LoginTime" />
                                                <asp:BoundField DataField="LogoutTime" HeaderText="LogoutTime" />
                                                <asp:BoundField DataField="WorkingHours" HeaderText="Working Hours" />
                                                <asp:BoundField DataField="PermissionType" HeaderText="Late Login/Early Logout" />
                                                <asp:TemplateField HeaderText="Allow Full Day" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAllow" Text='<%# Eval("Allow_Status").ToString()%>' runat="server" Visible="false" />
                                                        <asp:Label ID="lblEmpStatus" Text='<%# Eval("EmpStatus").ToString()%>' runat="server" Visible="false" />

                                                        <asp:CheckBox ID="chkAllow" runat="server" Checked='<%# Eval("Allow_Status").ToString() == "Yes" ? true : false %>' Visible='<%# Eval("PresentSattus").ToString() == "Less" ? true : false %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reason Allow Full Day" ItemStyle-Width="15%" ItemStyle-CssClass="cssPedding">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMyQuantity" CssClass="lblMyQuantity" ForeColor="#0033cc" Text='<%# Eval("Reason").ToString()%>' runat="server" />
                                                        <br />
                                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlMyQuantity" Enabled='<%# Eval("PresentSattus").ToString() == "Less"? true : false %>' SelectedValue='<%# Eval("Reason") %>'>
                                                            <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                                            <%--<asp:ListItem Value="Earned Leave" Text="Earned Leave"></asp:ListItem>--%>
                                                            <asp:ListItem Value="Full Day" Text="Full Day(Casual Leave)"></asp:ListItem>
                                                            <asp:ListItem Value="First Half" Text="Half Day First Half(Casual Leave)"></asp:ListItem>
                                                            <asp:ListItem Value="Second Half" Text="Half Day Second Half(Casual Leave)"></asp:ListItem>
                                                            <asp:ListItem Value="Late 1/3" Text="Late 1/3 (Casual Leave)"></asp:ListItem>
                                                            <asp:ListItem Value="LWP(Leave Without Pay)" Text="LWP(Leave Without Pay)"></asp:ListItem>
                                                            <asp:ListItem Value="Medical Leave" Text="Medical Leave"></asp:ListItem>
                                                            <asp:ListItem Value="Meeting" Text="Meeting"></asp:ListItem>
                                                            <asp:ListItem Value="Not Logout" Text="Not Logout"></asp:ListItem>
                                                            <asp:ListItem Value="Not Login/Logout" Text="Not Login/ Logout"></asp:ListItem>
                                                            <asp:ListItem Value="Official Work" Text="Official Work"></asp:ListItem>
                                                            <asp:ListItem Value="Others" Text="Others"></asp:ListItem>
                                                            <asp:ListItem Value="Special Holiday" Text="Special Holiday"></asp:ListItem>
                                                            <asp:ListItem Value="Technical Problem" Text="Technical Problem"></asp:ListItem>
                                                            <asp:ListItem Value="Tour" Text="Tour"></asp:ListItem>
                                                            <asp:ListItem Value="Training" Text="Training"></asp:ListItem>
                                                            <asp:ListItem Value="Working Out Of Office" Text="Working Out Of Office"></asp:ListItem>
                                                            <asp:ListItem Value="Manual Attendence" Text="Manual Attendence"></asp:ListItem>
                                                            <asp:ListItem Value="Covid-19/WFH" Text="Covid-19/WFH"></asp:ListItem>
						            <asp:ListItem Value="Optional Leave" Text="Optional Leave"></asp:ListItem>
                                                        </asp:DropDownList>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnAllow" runat="server" class="btn btn-success" Text="Submit" OnClientClick="return validateform();" OnClick="btnAllow_Click" />
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
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h3').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
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
        function ModalAttendenceFun() {
            $("#ModalAttendence").modal('show');
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
        <%-- function checkChecks1() {
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
        }--%>

        
    </script>
    <script>
        $('#txtStartDate').change(function () {
            var start = $('#txtStartDate').datepicker('getDate');
            var end = $('#txtEndDate').datepicker('getDate');

            if ($('#txtEndDate').val() != "") {
                if (start > end) {

                    if ($('#txtStartDate').val() != "") {
                        alert("From date should not be greater than To Date.");
                        $('#txtStartDate').val("");
                    }
                }
            }
        });
        $('#txtEndDate').change(function () {
            var start = $('#txtStartDate').datepicker('getDate');
            var end = $('#txtEndDate').datepicker('getDate');

            if (start > end) {

                if ($('#txtEndDate').val() != "") {
                    alert("To Date can not be less than From Date.");
                    $('#txtEndDate').val("");
                }
            }
        });
    </script>
</asp:Content>


