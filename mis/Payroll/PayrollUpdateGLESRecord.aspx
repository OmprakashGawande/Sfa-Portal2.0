<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollUpdateGLESRecord.aspx.cs" Inherits="mis_Payroll_PayrollUpdateGLESRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Update GLES Record</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" PageSize="50" runat="server" class="table table-hover table-bordered table-striped pagination-ys " ShowHeaderWhenEmpty="true" ClientIDMode="Static" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                            <ItemTemplate>
                                                <asp:Label ID="Emp_Name" runat="server" Text='<%# Eval("Emp_Name") %>' ToolTip='<%# Eval("Emp_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Office_Name" HeaderText="OFFICE NAME" />
                                        <asp:TemplateField HeaderText="GLES LIC ID">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGLESLICID" runat="server" Text='<%# Eval("GLES_LIC_ID") %>' CssClass="form-control" placeholder="Enter EDLI ID" onkeypress="return validateNum(event);"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GLES EMPLOYEE NUMBER">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtGLESEMPNO" runat="server" Text='<%# Eval("GLES_EMP_NO") %>' CssClass="form-control" placeholder="Enter EDLI NUMBER" onkeypress="return validateNum(event);"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="EDLI FREQUENCY">
                                            <ItemTemplate>
                                                <asp:Label ID="txtEDLIFreq" runat="server" Text='<%# Eval("EDLIFrequency") %>' CssClass="form-control" placeholder="Enter EDLI FREQUENCY"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOB">
                                            <ItemTemplate>
                                                <asp:Label ID="txtDOB" runat="server" Text='<%# Eval("Emp_Dob") %>' CssClass="form-control" placeholder="Select DOB" onblur="RetirementDate();"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOJ">
                                            <ItemTemplate>
                                                <asp:Label ID="txtDOJ" runat="server" Text='<%# Eval("Emp_JoiningDate") %>' CssClass="form-control" placeholder="Select DOJ" onblur="RetirementDate();"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOR">
                                            <ItemTemplate>
                                                <asp:Label ID="txtDOR" runat="server" Text='<%# Eval("Emp_RetirementDate") %>' CssClass="form-control" placeholder="Select DOR"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-block" Text="Save" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
     <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
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
                        columns: [0, 1, 2, 3]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3]
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
        $('.datatable1').DataTable({
            paging: true,
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
                        columns: [1, 2, 3]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [1, 2, 3]
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
        function RetirementDate() {
            debugger;

            // Joining Date
            var i = 0;

            var trCount = $('#GridView1 tr').length - 1;

            $('#GridView1 tr').each(function (index) {
                if (i > 0 && i < trCount) {
                    debugger;
                    var Jdate = $(this).children("td").eq(7).find('input[type="text"]').val();
                    var JEntryDate = Jdate.replace(/-/g, '/');
                    var Jdatearray = Jdate.split("/");
                    var date = $(this).children("td").eq(6).find('input[type="text"]').val();

                    var EntryDate = date.replace(/-/g, '/');
                    var datearray = date.split("/");
                    if (Jdatearray[0] == 1) {
                        datearray[0] = "01";
                    }
                    var Retdate = datearray[2] + '-' + datearray[1] + '-' + datearray[0];

                    var time = new Date(Retdate);
                    if (datearray[0] == 1) {
                        var old_date = time.setDate(time.getDate() - 1);
                        var d = new Date(old_date);
                        new_day = ('0' + (d.getDate())).slice(-2);
                        new_month = ('0' + (d.getMonth() + 1)).slice(-2);
                        new_year = d.getFullYear();
                        Retdate = new_year + '-' + new_month + '-' + new_day;
                    }
                    else {
                        var date = time;
                        // var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
                        var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
                        Retdate = lastDay.getFullYear() + '-' + (lastDay.getMonth() + 1) + '-' + (lastDay.getDate());
                    }

                    var startDob = new Date(Retdate);
                    Retdate = new Date(startDob.getFullYear() + 62, startDob.getMonth(), startDob.getDate());
                    Retdate = (Retdate.getDate()) + '/' + (Retdate.getMonth() + 1) + '/' + Retdate.getFullYear();

                    $(this).children("td").eq(8).find('input[type="text"]').val(Retdate);

                }
                i++;
            });




        }

    </script>
</asp:Content>


