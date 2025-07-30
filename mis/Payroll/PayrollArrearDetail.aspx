<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollArrearDetail.aspx.cs" Inherits="mis_Payroll_PayrollArrearDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .Grid td {
            padding: 3px !important;
        }

            .Grid td input {
                padding: 3px 3px !important;
                text-align: right !important;
                font-size: 12px !important;
                height: 26px !important;
            }

        .Grid th {
            text-align: center;
        }

        .ss {
            text-align: left !important;
        }

        .bgcolor {
            background-color: #eeeeee !important;
        }

        .box {
            min-height: initial !important;
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

        .box.box-pramod {
            border-top-color: #1ca79a;
        }

        .box {
            min-height: auto;
        }

        table {
            white-space: nowrap;
        }

        legend {
            font-size: 15px;
            color: #003FF7;
            background-color: #fff;
            border: 1px solid #ddd;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Arrear Detail( एरिअर का विवरण )</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>कार्यालय का नाम <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>कर्मचारी का नाम <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>एरिअर का प्रकार <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlArrearType" runat="server" class="form-control select2">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Selected="True">Arrear</asp:ListItem>
                                    <asp:ListItem>Salary</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <fieldset>
                                <legend>किस माह का एरिअर दे रहें हैं </legend>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>किस वर्ष का <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlFYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>किस माह का <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlFMonth" runat="server" class="form-control">
                                            <asp:ListItem Value="0">Select Month</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </fieldset>
                        </div>

                        <div class="col-md-6">
                            <fieldset>
                                <legend>भुगतान का वर्ष एवं माह </legend>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>किस वर्ष में <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlCurrentYear" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>किस माह में <span style="color: red;">*</span></label>
                                        <asp:DropDownList ID="ddlCurrentMonth" runat="server" class="form-control">
                                            <asp:ListItem Value="0">Select Month</asp:ListItem>
                                            <asp:ListItem Value="1">January</asp:ListItem>
                                            <asp:ListItem Value="2">February</asp:ListItem>
                                            <asp:ListItem Value="3">March</asp:ListItem>
                                            <asp:ListItem Value="4">April</asp:ListItem>
                                            <asp:ListItem Value="5">May</asp:ListItem>
                                            <asp:ListItem Value="6">June</asp:ListItem>
                                            <asp:ListItem Value="7">July</asp:ListItem>
                                            <asp:ListItem Value="8">August</asp:ListItem>
                                            <asp:ListItem Value="9">September</asp:ListItem>
                                            <asp:ListItem Value="10">October</asp:ListItem>
                                            <asp:ListItem Value="11">November</asp:ListItem>
                                            <asp:ListItem Value="12">December</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <fieldset>
                                <legend>आदेश क्र. एवं  दिनांक</legend>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>आदेश क्र. </label>
                                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>आदेश दिनांक </label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox ID="txtOrderDate" runat="server" CssClass="form-control DateAdd" placeholder="Select Order Date" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>
                        </div>

                        <div class="col-md-4">
                            <div class="form-group">
                                <labe>Remark (कुछ रिमार्क)</labe>
                                <span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" ClientIDMode="Static" TextMode="MultiLine" Rows="3"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateSearch();" />
                            </div>
                        </div>
                    </div>
                    <div id="DivDetail" runat="server" class="DivDetail">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="box-header">
                                    <h3 class="box-title">Earning Detail</h3>
                                </div>
                                <div class="table-responsive">
                                    <table id="tblEarning" class="table table-bordered table-striped Grid">
                                        <tr>
                                            <th scope="col">Earning Heads</th>
                                            <th scope="col" style="width: 100px;">Total To Be Paid Amount</th>
                                            <th scope="col" style="width: 100px;">Paid Amount</th>
                                            <th scope="col" style="width: 100px;">Difference (Remaining Amount To Be Paid)</th>
                                        </tr>
                                        <tr>
                                            <td>BASIC SALARY</td>
                                            <td>
                                                <asp:TextBox ID="txtEarnBePaidSalary_Basic" runat="server" Text="0" class="form-control" MaxLength="13" onfocusout="return CalculateEarning();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtEarnPaidSalary_Basic" runat="server" Text="0" class="form-control " MaxLength="13" onfocusout="return CalculateEarning();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtEarnRemainingSalary_Basic" runat="server" Text="0" class="form-control" MaxLength="13"></asp:TextBox></td>
                                        </tr>
                                        <asp:Repeater ID="RepeaterEarning" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEarnDeduction_Name" runat="server" Text='<%# Eval("EarnDeduction_Name").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblEarnDeduction_ID" runat="server" CssClass="hidden" Text='<%# Eval("EarnDeduction_ID").ToString()%>'></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtToBePaidAmount" runat="server" Text='<%# Eval("Amount").ToString()%>' class="form-control" MaxLength="13" onfocusout="return CalculateEarning();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtPaidEarning" runat="server" Text='<%# Eval("Amount").ToString()%>' class="form-control" MaxLength="13" onfocusout="return CalculateEarning();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemainingEarning" runat="server" Text="0" class="form-control bgcolor" MaxLength="13" Enabled="false"></asp:TextBox></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <td><b>Total Earning</b></td>
                                            <td>
                                                <asp:TextBox ID="txtETotalBePaidAmount" ClientIDMode="Static" runat="server" Text="0" class="form-control" MaxLength="13"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtETotalSalary_Basic" ClientIDMode="Static" runat="server" Text="0" class="form-control" MaxLength="13"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtETotalRemaining" ClientIDMode="Static" runat="server" Text="0" class="form-control" MaxLength="13"></asp:TextBox></td>
                                        </tr>
                                    </table>

                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="box-header">
                                    <h3 class="box-title">Deduction Detail</h3>

                                </div>
                                <div class="table-responsive">
                                    <table id="tblDeduction" class="table table-bordered table-striped Grid">
                                        <tr>
                                            <th scope="col">Deduction Heads</th>
                                            <th scope="col" style="width: 100px;">Total To Be Paid Amount</th>
                                            <th scope="col" style="width: 100px;">Paid Amount</th>
                                            <th scope="col" style="width: 100px;">Difference (Remaining Amount To Be Paid)</th>
                                        </tr>
                                        <tr>
                                            <td>SALARY DEDUCTION (ABSENT DAYS) </td>
                                            <td>
                                                <asp:TextBox ID="txtDeductionBePaidSalary" runat="server" Text="0" class="form-control" MaxLength="13" onfocusout="return CalculateDeduction();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtDeductionPaidSalary" runat="server" Text="0" class="form-control" MaxLength="13" onfocusout="return CalculateDeduction();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtDeductionRemainingSalary" runat="server" Text="0" class="form-control" MaxLength="13"></asp:TextBox></td>
                                        </tr>
                                        <asp:Repeater ID="RepeaterDeduction" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblEarnDeduction_Name" runat="server" Text='<%# Eval("EarnDeduction_Name").ToString()%>'></asp:Label>
                                                        <asp:Label ID="lblEarnDeduction_ID" runat="server" CssClass="hidden" Text='<%# Eval("EarnDeduction_ID").ToString()%>'></asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="txtToBePaidAmount" runat="server" Text='<%# Eval("Amount").ToString()%>' class="form-control" MaxLength="13" onfocusout="return CalculateDeduction();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtPaidDeduction" runat="server" Text='<%# Eval("Amount").ToString()%>' class="form-control" MaxLength="13" onfocusout="return CalculateDeduction();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                                    <td>
                                                        <asp:TextBox ID="txtRemainingDeduction" runat="server" Text="0" class="form-control bgcolor" MaxLength="13" Enabled="false"></asp:TextBox></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <td><b>Policy : </b></td>
                                            <td>
                                                <asp:TextBox ID="txtPolicyBePaidAmount" runat="server" Text="0" class="form-control" MaxLength="13" onfocusout="return CalculateDeduction();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtPolicyPaidAmount" runat="server" Text="0" class="form-control" MaxLength="13" onfocusout="return CalculateDeduction();" onblur="return checkvalue(this);" onkeypress="return validateDec(this,event)"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtPolicyRemaining" runat="server" Text="0" class="form-control" MaxLength="13"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td><b>Total Deduction</b></td>
                                            <td>
                                                <asp:TextBox ID="txtDTotalBePaidAmount" ClientIDMode="Static" runat="server" Text="0" class="form-control" MaxLength="13"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtDTotalSalary_Basic" ClientIDMode="Static" runat="server" Text="0" class="form-control" MaxLength="13"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtDTotalRemaining" ClientIDMode="Static" runat="server" Text="0" class="form-control" MaxLength="13"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Total Arrear Earning Amount<span style="color: red;">*</span></label>
                                    <asp:TextBox ID="txtTotalEarning" ClientIDMode="Static" runat="server" Text='0' class="form-control" MaxLength="13"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Total Arrear Deduction Amount<span style="color: red;">*</span></label>
                                    <asp:TextBox ID="txtTotalDeduction" ClientIDMode="Static" runat="server" Text='0' class="form-control" MaxLength="13"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Arrear Net Payment Amount<span style="color: red;">*</span></label>
                                    <asp:TextBox ID="txtNetPayment" ClientIDMode="Static" runat="server" Text='0' class="form-control" MaxLength="13"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="form-group"></div>
                        <div class="row">
                            <div class="col-md-8"></div>
                            <div class="col-md-2">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                            </div>
                            <div class="col-md-2">
                                <a class="btn btn-block btn-default" href="PayrollArrearDetail.aspx">Clear</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-success">
                <div class="box-header main-heading">
                    <h3 class="box-title">Arrear Paid Detail (एरिअर भुगतान का विवरण ) </h3>
                    <br />
                    <p class="empoyee-name">
                        <asp:Label ID="lblEmpName" runat="server" Text=""></asp:Label></p>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="GridView1" runat="server" class="datatable table table-bordered table-striped" AutoGenerateColumns="False" DataKeyNames="EmpArrearID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="क्रमांक" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="OrderNo" HeaderText="आदेश क्रमांक" />

                                            <asp:TemplateField HeaderText="किस महीने का">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# string.Concat(Eval("FromMonth"), " - ", Eval("FromYear"))%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="किस महीने में">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# string.Concat(Eval("CurrentMonth"), " - ", Eval("CurrentYear"))%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TotalEarning" HeaderText="कुल अर्जित एरिअर " ControlStyle-CssClass="Yourclass"/>
                                            <asp:BoundField DataField="TotalDeduction" HeaderText="कुल कटौती " />
                                            <asp:BoundField DataField="NetPaymentAmount" HeaderText="कुल एरिअर" />
                                            <asp:BoundField DataField="OrderDate" HeaderText="आदेश दिनांक" />
                                            <asp:BoundField DataField="ArrearTitle" HeaderText="रिमार्क" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <link href="../HR/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="../HR/js/jquery.dataTables.min.js"></script>
    <script src="../HR/js/dataTables.bootstrap.min.js"></script>
    <script src="../HR/js/dataTables.buttons.min.js"></script>
    <script src="../HR/js/jszip.min.js"></script>
    <script src="../HR/js/pdfmake.min.js"></script>
    <script src="../HR/js/vfs_fonts.js"></script>
    <script src="../HR/js/buttons.html5.min.js"></script>
    <script src="../HR/js/buttons.print.min.js"></script>
    <script type="text/javascript">
        //if ($(".DivDetail").length) {
        //        var dearnessall = $("tr:contains(DEARNESS ALLOWANCE)");
        //        dearnessall.find("#ctl00_ContentBody_RepeaterEarning_ctl02_txtToBePaidAmount").val;
        //        alert(dearnessall);
        //        $("#MwDataList input[name=selectRadioGroup]:checked").closest('tr');
        //}



        $(document).ready(function () {
            $('.datatable').DataTable({
                paging: false,

                columnDefs: [{
                    targets: 'no-sort',
                    orderable: false
                }],
                "bSort": false,

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
                        title: $('.empoyee-name').text() + " - Arrear Detail",
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7,8]
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('.empoyee-name').text() + " - Arrear Detail",
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7,8]
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
        });


        /**
        $(document).ready(function () {
            $('.datatable').DataTable({

                paging: false,

                columnDefs: [{
                    targets: 'no-sort',
                    orderable: false
                }],
                "bSort": false,

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
                        title: $('.empoyee-name').text() + " - Arrear Detail",
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7]
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('.empoyee-name').text() + " - Arrear Detail",
                        exportOptions: {
                            columns: [0, 1, 2, 3, 4, 5, 6, 7]
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
        });
        **/

        function CalculateEarning() {
            var i = 0;
            var TotalToBePaidEarning = 0;
            var TotalPaidEarning = 0;
            var TotalRemaining = 0;
            var trCount = $('#tblEarning tr').length - 1;

            $('#tblEarning tr').each(function (index) {
                if (i > 0 && i < trCount) {
                    //debugger;
                    var ToBePaidEarning = $(this).children("td").eq(1).find('input[type="text"]').val();
                    var PaidEarning = $(this).children("td").eq(2).find('input[type="text"]').val();
                    if (ToBePaidEarning.trim() == "")
                        ToBePaidEarning = 0;
                    if (PaidEarning.trim() == "")
                        PaidEarning = 0;
                    var Remaining = parseFloat(parseFloat(ToBePaidEarning) - parseFloat(PaidEarning)).toFixed(2);
                    $(this).children("td").eq(3).find('input[type="text"]').val(Remaining);
                    var TotToBePaidEarning = TotalToBePaidEarning;
                    var TotPaidEarning = TotalPaidEarning;
                    var TotRemaining = TotalRemaining;
                    TotalToBePaidEarning = parseFloat(parseFloat(TotToBePaidEarning) + parseFloat(ToBePaidEarning)).toFixed(2);
                    TotalPaidEarning = parseFloat(parseFloat(TotPaidEarning) + parseFloat(PaidEarning)).toFixed(2);
                    TotalRemaining = parseFloat(parseFloat(TotRemaining) + parseFloat(Remaining)).toFixed(2);

                }
                i++;
            });

            document.getElementById("txtETotalBePaidAmount").value = TotalToBePaidEarning;
            document.getElementById("txtETotalRemaining").value = TotalRemaining;
            document.getElementById("txtTotalEarning").value = TotalRemaining;
            var TotalDeduction = document.getElementById("txtTotalDeduction").value.trim();
            if (TotalDeduction.trim() == "")
                TotalDeduction = 0;

            document.getElementById("txtNetPayment").value = parseFloat(parseFloat(TotalRemaining) - parseFloat(TotalDeduction)).toFixed(2);




            return true;
        }
        function CalculateDeduction() {
            var i = 0;
            var TotalToBePaidDeduction = 0;
            var TotalPaidDeduction = 0;
            var TotalRemaining = 0;
            var trCount = $('#tblDeduction tr').length - 1;

            $('#tblDeduction tr').each(function (index) {
                if (i > 0 && i < trCount) {
                    //debugger;
                    var ToBePaidDeduction = $(this).children("td").eq(1).find('input[type="text"]').val();
                    var PaidDeduction = $(this).children("td").eq(2).find('input[type="text"]').val();

                    if (ToBePaidDeduction.trim() == "")
                        ToBePaidDeduction = 0;

                    if (PaidDeduction.trim() == "")
                        PaidDeduction = 0;

                    var Remaining = parseFloat(parseFloat(ToBePaidDeduction) - parseFloat(PaidDeduction)).toFixed(2);
                    $(this).children("td").eq(3).find('input[type="text"]').val(Remaining);

                    var TotToBePaidDeduction = TotalToBePaidDeduction;
                    var TotPaidDeduction = TotalPaidDeduction;
                    var TotRemaining = TotalRemaining;

                    TotalToBePaidDeduction = parseFloat(parseFloat(TotToBePaidDeduction) + parseFloat(ToBePaidDeduction)).toFixed(2);
                    TotalRemaining = parseFloat(parseFloat(TotRemaining) + parseFloat(Remaining)).toFixed(2);

                }
                i++;
            });

            document.getElementById("txtDTotalBePaidAmount").value = TotalToBePaidDeduction;
            document.getElementById("txtDTotalRemaining").value = TotalRemaining;
            document.getElementById("txtTotalDeduction").value = TotalRemaining;
            var TotalEarning = document.getElementById("txtTotalEarning").value.trim();
            if (TotalEarning.trim() == "")
                TotalEarning = 0;

            document.getElementById("txtNetPayment").value = parseFloat(parseFloat(TotalEarning) - parseFloat(TotalRemaining)).toFixed(2);


        }



        function validateSearch() {
            var msg = "";
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }
            if (document.getElementById('<%=ddlFYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Year. \n";
            }
            if (document.getElementById('<%=ddlFMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
            }
            if (document.getElementById('<%=ddlCurrentYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Current Year. \n";
            }
            if (document.getElementById('<%=ddlCurrentMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Current Month. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;

            }
        }
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }
            if (document.getElementById('<%=ddlArrearType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Arrear Type. \n";
            }
            if (document.getElementById('<%=ddlFYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select From Year. \n";
            }
            if (document.getElementById('<%=ddlFMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select From Month. \n";
            }
            if (document.getElementById('<%=ddlCurrentYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Cuurent Year. \n";
            }
            if (document.getElementById('<%=ddlCurrentMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Current Month. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Save Details ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

    </script>
</asp:Content>
