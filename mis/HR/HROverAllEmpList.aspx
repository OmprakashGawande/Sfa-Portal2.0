<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HROverAllEmpList.aspx.cs" Inherits="mis_HR_HREmpDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <link href="css/hrcustom.css" rel="stylesheet" />
    <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <style>
        table {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>Overall Department's Employee List  (कर्मचारी सूची) 
        <small></small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home ( होम)</a></li>
                <li class="active">Overall Department's Employee List (कर्मचारी सूची) </li>
            </ol>
        </section>
        <section class="content">
            <div class="box box-pramod">
                <div class="box-header">
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlOffice_SelectedIndexChanged" ClientIDMode="Static">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" runat="server" class="datatable  table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"  EmptyDataText="No Record Found" DataKeyNames="Emp_ID">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>

                                        <ItemStyle Width="5%"></ItemStyle>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="Designation_Name" HeaderText="Designation" />
                                    <asp:BoundField DataField="UserName" HeaderText="User Name" />
                                   <%-- <asp:BoundField DataField="Password" HeaderText="Password" />
                                     <asp:BoundField DataField="Emp_FatherHusbandName" HeaderText="Father/Husband Name" />--%>
                                    <asp:BoundField DataField="Emp_MobileNo" HeaderText="Mobile No." />

                                    <%--    <asp:BoundField DataField="Department_Name" HeaderText="Department" />--%>

                                    <%--<asp:BoundField DataField="Emp_Class" HeaderText="Class" />--%>
                                    <asp:BoundField DataField="Office_Name" HeaderText="Office Name" />
                                    <%--<asp:BoundField DataField="Emp_Gender" HeaderText="Gender" />--%>
                                    <%--   <asp:BoundField DataField="Emp_Dob" HeaderText="Date Of Birth" />--%>
                                    <%-- <asp:BoundField DataField="Emp_MaritalStatus" HeaderText="Marital Status" />--%>
                                    <%--   <asp:BoundField DataField="Emp_BloodGroup" HeaderText="Blood Group" />--%>
                                    <asp:BoundField DataField="Emp_AadharNo" HeaderText="Aadhar No" />
                                    <%-- <asp:BoundField DataField="Emp_PanCardNo" HeaderText="Pan Card No" />--%>
                                    <%--<asp:BoundField DataField="Emp_Email" HeaderText="Email" />--%>
                                    <%--<asp:ImageField DataImageUrlField="Emp_ProfileImage" ControlStyle-Height="70" ControlStyle-Width="50" HeaderText="Profile Image"></asp:ImageField>--%>
                                    <%--   <asp:BoundField DataField="Emp_BasicSalery" HeaderText="Basic Salary" />
                                    <asp:BoundField DataField="Emp_JoiningDate" HeaderText="Joining Date" />--%>
                                    <%--<asp:BoundField DataField="Emp_RetirementDate" HeaderText="Retirement Date" />--%>
                                    <%-- <asp:BoundField DataField="Emp_TypeOfPost" HeaderText="Type Of Post" />--%>
                                </Columns>
                            </asp:GridView>
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
                        title: $('h1').text(),
                        exportOptions: {
                            columns: ':not(.no-print)'
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('h1').text(),
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
    </script>
</asp:Content>
