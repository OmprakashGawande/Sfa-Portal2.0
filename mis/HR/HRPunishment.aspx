<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRPunishment.aspx.cs" Inherits="mis_HR_HRPunishment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <link href="css/hrcustom.css" rel="stylesheet" />
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
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
                            <h3 class="box-title" id="Label1">Add Punishment</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Employee Found Guilty ( कर्मचारी दोषी पाया गया )<span style="color: red;">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlEmployee" CssClass="form-control select2" ClientIDMode="Static">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlEmployee" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Punishment Or Suspension</label><span style="color: red">*</span>
                                        <asp:DropDownList runat="server" ID="ddlPunishmentSuspension" CssClass="form-control select2" ClientIDMode="Static">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Punishment</asp:ListItem>
                                            <asp:ListItem>Suspension</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlPunishmentSuspension" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Punishment Type</label><span style="color: red">*</span>
                                        <asp:DropDownList runat="server" ID="ddlPunishmentType" CssClass="form-control select2" ClientIDMode="Static">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Cumulative</asp:ListItem>
                                            <asp:ListItem>Non-Cumulative</asp:ListItem>
                                            <asp:ListItem>Other</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlPunishmentType" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Punishment Status</label><span style="color: red">*</span>
                                        <asp:DropDownList runat="server" ID="ddlPunishmentStatus" CssClass="form-control select2" ClientIDMode="Static">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>Active</asp:ListItem>
                                            <asp:ListItem>Finished</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlPunishmentStatus" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>From Date ( तारीख से)<span style="color: red;">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtFrom_Date" runat="server" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" PlaceHolder="Select From Date..." ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                        <small><span id="valtxtFrom_Date" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>End Date (तारीख तक)<span style="color: red;">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtEnd_Date" runat="server" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" PlaceHolder="Select End Date..." ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                        <small><span id="valtxtEnd_Date" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label>Title (शीर्षक )<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtTitle" runat="server" TextMode="MultiLine" placeholder="Enter Title (शीर्षक )..." class="form-control" Onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                        <small><span id="valtxtTitle" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Document 1 (Only 'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX')</label>
                                        <asp:Label ID="lblSuppDoc1" runat="server" Text="" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                        <asp:FileUpload ID="txtDocument" CssClass="form-control" runat="server" ClientIDMode="Static" Onchange="uploadDoc(),ValidateFileSize(this,1024*1024)" />
                                        <asp:HyperLink ID="UploadDocument" Target="_blank" runat="server">View</asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Description (विवरण)<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="6" placeholder="Enter Description (विवरण)..." class="form-control" Onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                        <small><span id="valtxtDescription" class="text-danger"></span></small>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <label>&nbsp;</label>
                                    <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Save" ID="btnSave" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                                </div>
                                <div class="col-md-2">
                                    <label>&nbsp;</label>
                                    <a class="btn btn-default btn-block" runat="server" href="HRPunishment.aspx">Clear</a>
                                </div>
                                <div class="col-md-8">
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="Punishment_ID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                <asp:BoundField DataField="PunishmentSuspension" HeaderText="Punishment/Suspension" />
                                                <asp:BoundField DataField="Punishment_Type" HeaderText="Punishment Type" />
                                                <asp:BoundField DataField="Punishment_Status" HeaderText="Punishment Status" />
                                                <asp:BoundField DataField="From_Date" HeaderText="From Date" />
                                                <asp:BoundField DataField="End_Date" HeaderText="End Date" />
                                                <asp:BoundField DataField="Title" HeaderText="Title" />
                                                <asp:TemplateField HeaderText="Document">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="Attachment" Target="_blank" runat="server" NavigateUrl='<%# Eval("Document").ToString() != "" ? "../HR/PunishmentUpload/" + Eval("Document") : "" %>' Text='<%# Eval("Document").ToString() != "" ? "VIEW" : "NA" %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                                <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Select" runat="server" CssClass="label badge bg-blue" CausesValidation="False" CommandName="Select" Text="Edit"></asp:LinkButton>
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
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>
    <script src="js/dataTables.buttons.min.js"></script>
    <%-- <script src="js/buttons.flash.min.js"></script>--%>
    <script src="js/jszip.min.js"></script>
    <%-- <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>--%>
    <script src="js/buttons.html5.min.js"></script>
    <script src="js/buttons.print.min.js"></script>

    <script>
        $(document).ready(function () {
            $('.datatable').DataTable({

                paging: false,

                columnDefs: [{
                    targets: 'no-sort',
                    orderable: false
                }],
                "order": [[0, 'asc']],

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
                            columns: ':not(.no-print)'
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('h3').text(),
                        exportOptions: {
                            columns: ':not(.no-print)'
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
            t.on('order.dt search.dt', function () {
                t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
            }).draw();
        });

        function validateform() {
            var msg = "";
            $("#valddlEmployee").html("");
            $("#valddlPunishmentSuspension").html("");
            $("#valddlPunishmentType").html("");
            $("#valddlPunishmentStatus").html("");
            $("#valtxtFrom_Date").html("");
            $("#valtxtEnd_Date").html("");
            $("#valtxtTitle").html("");
            $("#valtxtDescription").html("");


            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Please Select Employee Name. \n";
                $("#valddlEmployee").html("Please Select Employee Name.");
            }
            if (document.getElementById('<%=ddlPunishmentSuspension.ClientID%>').selectedIndex == 0) {
                msg = msg + "Please Select Punishment or Suspension. \n";
                $("#valddlPunishmentSuspension").html("Please Select Punishment or Suspension.");
            }
            if (document.getElementById('<%=ddlPunishmentType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Please Select Punishment Type. \n";
                $("#valddlPunishmentType").html("Please Select Punishment Type.");
            }
            if (document.getElementById('<%=ddlPunishmentStatus.ClientID%>').selectedIndex == 0) {
                msg = msg + "Please Select Punishment Status. \n";
                $("#valddlPunishmentStatus").html("Please Select Punishment Status.");
            }
            if (document.getElementById('<%=txtFrom_Date.ClientID%>').value.trim() == "") {
                msg = msg + "please Select From Date. \n";
                $("#valtxtFrom_Date").html("please Select From Date.");
            }
            if (document.getElementById('<%=txtEnd_Date.ClientID%>').value.trim() == "") {
                msg = msg + "please Select End Date. \n";
                $("#valtxtEnd_Date").html("please Select End Date.");
            }
            if (document.getElementById('<%=txtTitle.ClientID%>').value.trim() == "") {
                msg = msg + "please Enter Title. \n";
                $("#valtxtTitle").html("please Enter Title.");
            }
            if (document.getElementById('<%=txtDescription.ClientID%>').value.trim() == "") {
                msg = msg + "please Enter Description. \n";
                $("#valtxtDescription").html("please Enter Description.");
            }

            if (msg != "") {
                alert(msg);
                return false;
            }

            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Edit") {
                    if (confirm("Do you really want to Edit Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }

                }
            }
        }

        function uploadDoc() {
            if (document.getElementById('<%=txtDocument.ClientID%>').files.length != 0) {
                var ext = $('#txtDocument').val().split('.').pop().toLowerCase();
                if ($.inArray(ext, ['png', 'jpg', 'pdf', 'jpeg', 'gif', 'doc', 'docx']) == -1) {
                    $('#lblSuppDoc1').text("केवल JPEG*PNG*JPG*GIF*PDF*DOC*DOCX' दस्तावेज अपलोड करें।");
                    document.getElementById('txtDocument').value = "";
                }
                else {
                    $('#lblSuppDoc1').text("");
                }
            }
            else {
                $('#lblSuppDoc1').text("");
            }
        }
        function ValidateFileSize(a, size) {
            // 1 kb =(size=1024) 
            // 1 mb =(size=1024 * 1024 * 1) 

            var uploadcontrol = document.getElementById(a.id);
            if (uploadcontrol.files[0].size > size) {
                alert('File size should not greater than' + size / 1024 + ' kb.');
                document.getElementById(a.id).value = '';
                return false;
            }
            else {
                return true;
            }

        }

        $('#txtFrom_Date').change(function () {
            debugger;
            var start = $('#txtFrom_Date').datepicker('getDate');
            var end = $('#txtEnd_Date').datepicker('getDate');

            if ($('#txtEnd_Date').val() != "") {
                if (start > end) {

                    if ($('#txtFrom_Date').val() != "") {
                        alert("From date should not be greater than To Date.");
                        $('#txtFrom_Date').val("");
                    }
                }
            }
        });

        $('#txtEnd_Date').change(function () {
            debugger;
            var start = $('#txtFrom_Date').datepicker('getDate');
            var end = $('#txtEnd_Date').datepicker('getDate');

            if (start > end) {

                if ($('#txtEnd_Date').val() != "") {
                    alert("End Date can not be less than From Date.");
                    $('#txtEnd_Date').val("");
                }

            }
        });

    </script>
</asp:Content>

