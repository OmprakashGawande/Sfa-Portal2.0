<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRAchievement.aspx.cs" Inherits="mis_HR_HRAchievement" %>

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
                            <h3 class="box-title" id="Label1">Add Achievement</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>कर्मचारी का नाम /Employee Name<span style="color: red;">*</span></label>
                                        <asp:DropDownList runat="server" ID="ddlEmployeeName" CssClass="form-control select2" ClientIDMode="Static">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlEmployeeName" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>उपलब्धि का प्रकार /Type of Achievement</label><span style="color: red">*</span>
                                        <asp:DropDownList runat="server" ID="ddlAchievementType" CssClass="form-control select2" ClientIDMode="Static">
                                            <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem>State Level</asp:ListItem>
                                            <asp:ListItem>District Level</asp:ListItem>
                                            <asp:ListItem>Other</asp:ListItem>
                                        </asp:DropDownList>
                                        <small><span id="valddlAchievementType" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>विषय / Topic<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtTopic" runat="server" placeholder="Enter विषय / Topic..." class="form-control" Onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                        <small><span id="valtxtTopic" class="text-danger"></span></small>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>संस्था/Organization<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtOrganization" runat="server" placeholder="Enter संस्था/Organization..." class="form-control" Onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                        <small><span id="valtxtOrganization" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>दिनांक /Date<span style="color: red;">*</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtDate" runat="server" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" PlaceHolder="Select From Date..." ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                        <small><span id="valtxtDate" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Document 1('JPEG*PNG*JPG*GIF*PDF*DOC*DOCX')</label>
                                        <asp:Label ID="lblSuppDoc1" runat="server" Text="" ClientIDMode="Static" ForeColor="Red"></asp:Label>
                                        <asp:FileUpload ID="txtDocument" CssClass="form-control" runat="server" ClientIDMode="Static" Onchange="uploadDoc(),ValidateFileSize(this,1024*1024)" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Description (विवरण)<span style="color: red;">*</span></label>
                                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="3" placeholder="Enter Description (विवरण)..." class="form-control" Onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
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
                                    <a class="btn btn-default btn-block" runat="server" href="HRAchievement.aspx">Clear</a>
                                </div>
                                <div class="col-md-8">
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <div class="col-md-12">
                                        <asp:GridView ID="GridView1" runat="server" class="datatable table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="AchievementID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                <asp:BoundField DataField="AchievementType" HeaderText="Achievement Type" />
                                                <asp:BoundField DataField="Achievement_Topic" HeaderText="Achievement Topic" />
                                                <asp:BoundField DataField="Achievement_Organization" HeaderText="Achievement Organization" />
                                                <asp:BoundField DataField="Achievement_Date" HeaderText="Achievement Date" />
                                                <asp:TemplateField HeaderText="Document">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="Attachment" Target="_blank" runat="server" NavigateUrl='<%# Eval("Achievement_Document").ToString() != "" ? "../HR/AchievementUpload/" + Eval("Achievement_Document") : "" %>' Text='<%# Eval("Achievement_Document").ToString() != "" ? "VIEW" : "NA" %>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Achievement_Description" HeaderText="Achievement Description" />
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
            $("#valddlEmployeeName").html("");
            $("#valddlAchievementType").html("");
            $("#valtxtTopic").html("");
            $("#valtxtOrganization").html("");
            $("#valtxtDate").html("");
            $("#valtxtDescription").html("");



            if (document.getElementById('<%=ddlEmployeeName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Please Select Employee Name. \n";
                $("#valddlEmployeeName").html("Please Select Employee Name.");
            }
            if (document.getElementById('<%=ddlAchievementType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Please Select Achievement Type. \n";
                $("#valddlAchievementType").html("Please Select Achievement Type.");
            }
            if (document.getElementById('<%=txtTopic.ClientID%>').value.trim() == "") {
                msg = msg + "Please Enter Topic. \n";
                $("#valtxtTopic").html("please Enter Topic.");
            }
            if (document.getElementById('<%=txtOrganization.ClientID%>').value.trim() == "") {
                msg = msg + "Please Enter Organization. \n";
                $("#valtxtOrganization").html("Please Enter Organization.");
            }
            if (document.getElementById('<%=txtDate.ClientID%>').value.trim() == "") {
                msg = msg + "Please Select  Date. \n";
                $("#valtxtDate").html("Please Select  Date.");
            }
            if (document.getElementById('<%=txtDescription.ClientID%>').value.trim() == "") {
                msg = msg + "Please Enter Description. \n";
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

    </script>
</asp:Content>

