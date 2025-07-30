<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpDynamicReport.aspx.cs" Inherits="mis_HR_HrEmpDynamicReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        fieldset {
            margin-bottom: 5px !important;
        }
    </style>
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
                            <h3 class="box-title" id="Label1">Employee Report</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row Hiderow">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Office Name</label><span style="color: red">*</span>
                                        <asp:ListBox runat="server" ID="ddlOffice"  ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple" >
                                        </asp:ListBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Class (श्रेणी)<span style="color: red;">*</span></label>
                                        <asp:ListBox ID="ddlEmp_Class" runat="server"  ClientIDMode="Static" class="form-control" SelectionMode="Multiple" AutoPostBack="true" OnSelectedIndexChanged="ddlEmp_Class_SelectedIndexChanged">
                                        </asp:ListBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Designation (पद)<span style="color: red;">*</span></label>
                                        <asp:ListBox ID="ddlDesignation_ID" runat="server"  ClientIDMode="Static" class="form-control" SelectionMode="Multiple">
                                        </asp:ListBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Type of Post (पद प्रकार) <span style="color: red;">*</span></label><br />
                                        <asp:ListBox ID="ddlEmp_TypeOfPost" runat="server" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple">
                                            <asp:ListItem Value="Permanent">Regular/Permanent</asp:ListItem>
                                            <asp:ListItem Value="Fixed Employee">Fixed Employee(स्थाई कर्मी)</asp:ListItem>
                                            <asp:ListItem Value="Contigent Employee">Contigent Employee</asp:ListItem>
                                            <asp:ListItem Value="Samvida Employee">Samvida Employee</asp:ListItem>
                                            <asp:ListItem Value="Theka Shramik">Theka Shramik</asp:ListItem>
											<asp:ListItem Value="Outsource Employee">Outsource Employee</asp:ListItem>
									        <asp:ListItem Value="Other Employee">Other Employee</asp:ListItem>
									        <asp:ListItem Value="Deputation Employee">Deputation Employee</asp:ListItem>
                                        </asp:ListBox>
                                        <%--<asp:DropDownList ID="ddlEmp_TypeOfPost" runat="server" class="form-control">
                                            <asp:ListItem Value="0">All</asp:ListItem>                                           
                                        </asp:DropDownList>--%>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>
                                            <label>
                                                Personal Details 
                                            <asp:CheckBox ID="chkAllP_Detail" ClientIDMode="Static" runat="server" Checked="true" onclick="CheckAllP_Detail();" />
                                            </label>
                                        </legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="chk2" CssClass="col-md-4" runat="server" Checked="true" Text="कर्मचारी का नाम" Enabled="false" />
                                                    <asp:CheckBox ID="chk3" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="लिंग" />
                                                    <asp:CheckBox ID="chk4" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="जन्म की तारीख" />
                                                    <asp:CheckBox ID="chk5" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="ब्लड ग्रुप" />
                                                    <asp:CheckBox ID="chk6" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="पिता / पति का नाम" />
                                                    <asp:CheckBox ID="chk7" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="वैवाहिक स्थिति" />

                                                    <asp:CheckBox ID="chk8" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="Profile Image" />
                                                    <asp:CheckBox ID="chk9" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="मोबाइल नंबर" />
                                                    <asp:CheckBox ID="chk10" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="आधार नंबर" />
                                                    <asp:CheckBox ID="chk11" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="पैन कार्ड नंबर" />
                                                    <asp:CheckBox ID="chk12" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="ईमेल आईडी" />
                                                    <asp:CheckBox ID="chk13" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="पति / पत्नी का नाम" />

                                                    <asp:CheckBox ID="chk14" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="नौकरी/व्यवसाय" />
                                                    <asp:CheckBox ID="chk15" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="पद/विभाग" />
                                                    <asp:CheckBox ID="chk16" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="वर्ग" />
                                                    <asp:CheckBox ID="chk17" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="धर्म" />
                                                    <asp:CheckBox ID="chk18" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="शारीरिक विकलांगता" />
                                                    <asp:CheckBox ID="chk19" CssClass="col-md-4 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="विकलांगता का प्रकार" />

                                                    <fieldset>
                                                        <legend>CURRENT ADDRESS</legend>
                                                        <asp:CheckBox ID="chk20" CssClass="col-md-3 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="State" />
                                                        <asp:CheckBox ID="chk21" CssClass="col-md-3 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="City" />
                                                        <asp:CheckBox ID="chk22" CssClass="col-md-3 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="Pin Code" />
                                                        <asp:CheckBox ID="chk23" CssClass="col-md-3 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="Address " />
                                                    </fieldset>
                                                    <fieldset>
                                                        <legend>PERMANENT ADDRESS</legend>
                                                        <asp:CheckBox ID="chk24" CssClass="col-md-3 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="State" />
                                                        <asp:CheckBox ID="chk25" CssClass="col-md-3 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="City" />
                                                        <asp:CheckBox ID="chk26" CssClass="col-md-3 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="Pin Code" />
                                                        <asp:CheckBox ID="chk27" CssClass="col-md-3 chkP" onclick="CheckPOne();" runat="server" Checked="true" Text="Address " />
                                                    </fieldset>
                                                </div>
                                            </div>
                                        </div>

                                    </fieldset>
                                </div>
                                <div class="col-md-6">
                                    <fieldset>
                                        <legend>
                                            <label>
                                                Official Details 
                                            <asp:CheckBox ID="chkAllO_Detail" ClientIDMode="Static" runat="server" Checked="true" onclick="CheckAllO_Detail();" />
                                            </label>
                                        </legend>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="chk1" CssClass="col-md-4" runat="server" Checked="true" Text="Employee ID" Enabled="false" />
                                                    <asp:CheckBox ID="chk28" CssClass="col-md-4 chkO" onclick="CheckOOne();" runat="server" Checked="true" Text="कार्यालय का प्रकार" />
                                                    <asp:CheckBox ID="chk29" CssClass="col-md-4 chkO" onclick="CheckOOne();" runat="server" Checked="true" Text="कार्यालय" />
                                                    <asp:CheckBox ID="chk30" CssClass="col-md-4 chkO" onclick="CheckOOne();" runat="server" Checked="true" Text="स्तर" />
                                                    <asp:CheckBox ID="chk31" CssClass="col-md-4 chkO" onclick="CheckOOne();" runat="server" Checked="true" Text="श्रेणी" />
                                                    <asp:CheckBox ID="chk32" CssClass="col-md-4 chkO" onclick="CheckOOne();" runat="server" Checked="true" Text="पद" />

                                                    <asp:CheckBox ID="chk33" CssClass="col-md-4 chkO" onclick="CheckOOne();" runat="server" Checked="true" Text="विभाग" />
                                                    <asp:CheckBox ID="chk34" CssClass="col-md-4 chkO" onclick="CheckOOne();" runat="server" Checked="true" Text="वेतनमान" />
                                                    <asp:CheckBox ID="chk35" CssClass="col-md-4 chkO" onclick="CheckOOne();" runat="server" Checked="true" Text="ग्रेड पे" />
                                                    <asp:CheckBox ID="chk36" CssClass="col-md-4 chkO" onclick="CheckOOne();" runat="server" Checked="true" Text="मूल वेतन" />
                                                    <asp:CheckBox ID="chk37" CssClass="col-md-4 chkO" onclick="CheckOOne();" runat="server" Checked="true" Text="कार्यग्रहण तिथि" />
                                                    <asp:CheckBox ID="chk38" CssClass="col-md-4 chkO" onclick="CheckOOne();" runat="server" Checked="true" Text="पोस्टिंग तिथि" />

                                                    <asp:CheckBox ID="chk39" CssClass="col-md-4 chkO" onclick="CheckOOne();" runat="server" Checked="true" Text="सेवानिवृत्ति की तारीख" />
                                                    <asp:CheckBox ID="chk40" CssClass="col-md-4 chkO" onclick="CheckOOne();" runat="server" Checked="true" Text="भर्ती का प्रकार" />
                                                    <asp:CheckBox ID="chk41" CssClass="col-md-4 chkO" onclick="CheckOOne();" runat="server" Checked="true" Text="पद प्रकार" />

                                                </div>
                                            </div>
                                        </div>

                                    </fieldset>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <asp:Button runat="server" CssClass="btn btn-success btn-block" Text="Search" ID="btnShow" OnClick="btnShow_Click" />
                                        </div>

                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <a href="HREmpDynamicReport.aspx" class="btn btn-default btn-block">Cancel</a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" runat="server" class="datatable  table table-hover table-bordered" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true"  EmptyDataText="No Record Found" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="UserName" HeaderText="Employee ID" />
                                                <asp:BoundField DataField="Emp_Name" HeaderText="कर्मचारी का नाम" />
                                                <asp:BoundField DataField="Emp_Gender" HeaderText="लिंग" />
                                                <asp:BoundField DataField="Emp_Dob" HeaderText="जन्म की तारीख" />
                                                <asp:BoundField DataField="Emp_BloodGroup" HeaderText="ब्लड ग्रुप" />
                                                <asp:BoundField DataField="Emp_FatherHusbandName" HeaderText="पिता / पति का नाम" />
                                                <asp:BoundField DataField="Emp_MaritalStatus" HeaderText="वैवाहिक स्थिति" />
                                                <%--Profile Image--%>
                                                <asp:TemplateField HeaderText="ProfileImage">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" Style="height: 75px;" ImageUrl='<%# Eval("Emp_ProfileImage") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Emp_MobileNo" HeaderText="मोबाइल नंबर" />
                                                <asp:BoundField DataField="Emp_AadharNo" HeaderText="आधार नंबर" />
                                                <asp:BoundField DataField="Emp_PanCardNo" HeaderText="पैन कार्ड नंबर" />
                                                <asp:BoundField DataField="Emp_Email" HeaderText="ईमेल आईडी" />

                                                <asp:BoundField DataField="Emp_HusbWifeName" HeaderText="पति / पत्नी का नाम" />
                                                <asp:BoundField DataField="Emp_HusbWifeJob" HeaderText="पति / पत्नी का नौकरी/व्यवसाय" />
                                                <asp:BoundField DataField="Emp_HusbWifeDep" HeaderText="पति / पत्नी का पद/विभाग" />
                                                <asp:BoundField DataField="Emp_Category" HeaderText="वर्ग" />
                                                <asp:BoundField DataField="Emp_Religion" HeaderText="धर्म" />
                                                <asp:BoundField DataField="Emp_Disability" HeaderText="शारीरिक विकलांगता" />
                                                <asp:BoundField DataField="Emp_DisabilityType" HeaderText="विकलांगता का प्रकार" />
                                                <asp:BoundField DataField="Emp_CurState" HeaderText="Current State" />
                                                <asp:BoundField DataField="Emp_CurCity" HeaderText="Current City" />
                                                <asp:BoundField DataField="Emp_CurPinCode" HeaderText="Current Pin Code" />
                                                <asp:BoundField DataField="Emp_CurAddress" HeaderText="Current Address" />
                                                <asp:BoundField DataField="Emp_PerState" HeaderText="Permanent State" />
                                                <asp:BoundField DataField="Emp_PerCity" HeaderText="Permanent City" />
                                                <asp:BoundField DataField="Emp_PerPinCode" HeaderText="Permanent PinCode" />
                                                <asp:BoundField DataField="Emp_PerAddress" HeaderText="Permanent Address" />

                                                <asp:BoundField DataField="OfficeType_Title" HeaderText="कार्यालय का प्रकार" />
                                                <asp:BoundField DataField="Office_Name" HeaderText="कार्यालय" />
                                                <asp:BoundField DataField="Level_Name" HeaderText="स्तर" />
                                                <asp:BoundField DataField="Emp_Class" HeaderText="श्रेणी" />
                                                <asp:BoundField DataField="Designation_Name" HeaderText="पद" />
                                                <asp:BoundField DataField="Department_Name" HeaderText="विभाग" />
                                                <asp:BoundField DataField="PayScale_Name" HeaderText="वेतनमान" />
                                                <asp:BoundField DataField="GradePay_Name" HeaderText="ग्रेड पे" />
                                                <asp:BoundField DataField="Emp_BasicSalery" HeaderText="मूल वेतन" />
                                                <asp:BoundField DataField="Emp_JoiningDate" HeaderText="कार्यग्रहण तिथि" />
                                                <asp:BoundField DataField="Emp_PostingDate" HeaderText="पोस्टिंग तिथि" />
                                                <asp:BoundField DataField="Emp_RetirementDate" HeaderText="सेवानिवृत्ति की तारीख" />
                                                <asp:BoundField DataField="Emp_TypeOfRecruitment" HeaderText="भर्ती का प्रकार" />
                                                <asp:BoundField DataField="Emp_TypeOfPost" HeaderText="पद प्रकार" />
                                                <%-- <asp:BoundField DataField="Emp_GpfType" HeaderText="GpfType" />
                                                <asp:BoundField DataField="Emp_GpfNo" HeaderText="Emp_GpfNo" />--%>
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
 <%-- <link href="../finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <link href="../finance/css/buttons.dataTables.min.css" rel="stylesheet" />
    <link href="../finance/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="../finance/js/jquery.dataTables.min.js"></script>
    <script src="../finance/js/jquery.dataTables.min.js"></script>
    <script src="../finance/js/dataTables.bootstrap.min.js"></script>
    <script src="../finance/js/dataTables.buttons.min.js"></script>
    <script src="../finance/js/buttons.flash.min.js"></script>
    <script src="../finance/js/jszip.min.js"></script>
    <script src="../finance/js/pdfmake.min.js"></script>
    <script src="../finance/js/vfs_fonts.js"></script>
    <script src="../finance/js/buttons.html5.min.js"></script>
    <script src="../finance/js/buttons.print.min.js"></script>
    <script src="../finance/js/buttons.colVis.min.js"></script>--%>
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
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel"></i> Export to XLSX',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: ':visible' // export all visible columns
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
</script>



    <script>


        function CheckAllP_Detail() {
            if (document.getElementById('<%=chkAllP_Detail.ClientID%>').checked == true) {
                $('.chkP').each(function () {

                    $(this).find('input[type=checkbox]').prop('checked', true);
                });
            }
            else {
                $('.chkP').each(function () {
                    $(this).find('input[type=checkbox]').prop('checked', false);
                });
            }
            return false;
        }
        function CheckAllO_Detail() {
            if (document.getElementById('<%=chkAllO_Detail.ClientID%>').checked == true) {
                $('.chkO').each(function () {

                    $(this).find('input[type=checkbox]').prop('checked', true);
                });
            }
            else {
                $('.chkO').each(function () {
                    $(this).find('input[type=checkbox]').prop('checked', false);
                });
            }
            return false;
        }

        function CheckPOne() {
            $(".chkP").change(function () {
                if (!$(this).prop("checked")) {
                    $("#chkAllP_Detail").prop("checked", false);
                }
                else {
                    $("#chkAllP_Detail").prop("checked", false);
                }
            });

            return false;
        }
        function CheckOOne() {
            $(".chkO").change(function () {
                if (!$(this).prop("checked")) {
                    $("#chkAllO_Detail").prop("checked", false);
                }
                else {
                    $("#chkAllO_Detail").prop("checked", false);
                }
            });

            return false;
        }
    </script>
    <script src="../../../mis/js/jquery.js" type="text/javascript"></script>
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script>

        $(function () {
            $('[id*=ddlOffice]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });

            $('[id*=ddlEmp_Class]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });
            $('[id*=ddlDesignation_ID]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });
            $('[id*=ddlEmp_TypeOfPost]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });
        });
    </script>
    <style>
        .multiselect {
            text-align: left !important;
        }

        .multiselect-selected-text, .checkbox, .dropdown-menu {
            width: 100% !important;
        }

        .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }
    </style>

</asp:Content>

