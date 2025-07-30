<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollTaxAssesmentFormSingle.aspx.cs" Inherits="mis_Payroll_PayrollTaxAssesmentFormSingle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .salary-logo {
            -webkit-filter: grayscale(100%);
            filter: grayscale(100%);
            width: 46px;
            float: right;
        }

        @media print {
            .print_hidden {
                display: none !important;
            }

            .box-body, .box, tbody {
                height: auto !important;
            }

                .box.box-success {
                    border: none;
                }


            .headingOfDept {
                display: block !important;
            }

            .salary-logo {
                -webkit-filter: grayscale(100%);
                filter: grayscale(100%);
                width: 40px;
            }

            .lblEmpDetail {
                font-size: 14px !important;
            }

            .main-footer {
                display: none;
            }

            .textarea {
                border: none;
            }
        }

        .textarea {
            width: 100%;
        }

        .lbltaxwithcess {
            display: block;
            min-width: 200px;
            border-top: 1px solid black;
            font-weight: 900;
        }

        .table > tbody > tr > td {
            padding: 3px;
        }

        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            padding: 2px;
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
                    <h3 class="box-title print_hidden">Income Tax Assessment Form</h3>
                </div>
                <asp:label id="lblMsg" runat="server" text=""></asp:label>
                <div class="box-body">
                    <div class="row print_hidden">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:dropdownlist id="ddlOfficeName" runat="server" class="form-control select2" enabled="false">
                                </asp:dropdownlist>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Employee<span style="color: red;">*</span></label>
                                <asp:dropdownlist id="ddlEmployee" runat="server" class="form-control select2" enabled="false">
                                </asp:dropdownlist>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Year<span style="color: red;">*</span></label>
                                <asp:dropdownlist id="ddlYear" runat="server" cssclass="form-control"></asp:dropdownlist>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:button id="btnSearch" cssclass="btn btn-block btn-success Aselect1" style="margin-top: 23px;" runat="server" text="Search" onclick="btnSearch_Click" onclientclick="return validateform1();" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group"></div>

                    <div class="row">
                        <div class="col-md-12 table-responsive" style="max-height: 400px;">
                            <asp:label id="lblEmpDetail" class="lblEmpDetail print_hidden" runat="server" text="" visible="true" style="color: black; font-size: 17px;"></asp:label>
                            <p class="print_hidden" style="color: blue"><b>नोट: </b>प्रिंट करने के लिए कृपया Ctrl+P का उपयोग करें |</p>
                            <asp:gridview id="GridView1" datakeynames="EarnDeduction_ID" runat="server" autogeneratecolumns="False" class="datatable table table-hover table-bordered pagination-ys">
                                <Columns>
                                    <%--<asp:BoundField DataField="EarnDeduction_Name" HeaderText="Head" />--%>
                                    <asp:TemplateField HeaderText="Head" ItemStyle-CssClass="alignR">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetsalary" CssClass='<%# Eval("EarnDeduction_Type")%>' runat="server" Text='<%# Eval("EarnDeduction_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EarnDeduction_Type" HeaderText="Earning & Deduction" />

                                    <asp:BoundField DataField="Total" HeaderText="Total In FY" />

                                    <asp:BoundField DataField="April" HeaderText="April" />
                                    <asp:BoundField DataField="AprilArr" HeaderText="April Arrear" />

                                    <asp:BoundField DataField="May" HeaderText="May" />
                                    <asp:BoundField DataField="MayArr" HeaderText="May Arrear" />

                                    <asp:BoundField DataField="June" HeaderText="June" />
                                    <asp:BoundField DataField="JuneArr" HeaderText="June Arrear" />

                                    <asp:BoundField DataField="July" HeaderText="July" />
                                    <asp:BoundField DataField="JulyArr" HeaderText="July Arrear" />

                                    <asp:BoundField DataField="August" HeaderText="August" />
                                    <asp:BoundField DataField="AugustArr" HeaderText="August Arrear" />

                                    <asp:BoundField DataField="September" HeaderText="September" />
                                    <asp:BoundField DataField="SeptemberArr" HeaderText="September Arrear" />

                                    <asp:BoundField DataField="October" HeaderText="October" />
                                    <asp:BoundField DataField="OctoberArr" HeaderText="October Arrear" />

                                    <asp:BoundField DataField="November" HeaderText="November" />
                                    <asp:BoundField DataField="NovemberArr" HeaderText="November Arrear" />

                                    <asp:BoundField DataField="December" HeaderText="December" />
                                    <asp:BoundField DataField="DecemberArr" HeaderText="December Arrear" />

                                    <asp:BoundField DataField="January" HeaderText="January" />
                                    <asp:BoundField DataField="JanuaryArr" HeaderText="January Arrear" />

                                    <asp:BoundField DataField="February" HeaderText="February" />
                                    <asp:BoundField DataField="FebruaryArr" HeaderText="February Arrear" />

                                    <asp:BoundField DataField="March" HeaderText="March" />
                                    <asp:BoundField DataField="MarchArr" HeaderText="March Arrear" />


                                </Columns>
                            </asp:gridview>
                        </div>
                    </div>


                    <div class="row" id="dvAssesmentForm" runat="server" visible="false">
                        <div class="col-md-12">
                            <table class="table">
                                <tbody>
                                    <tr>
                                        <th>
                                            <img src="../image/mpagro-logo.png" class="salary-logo" /></th>
                                        <th colspan="5">
                                            <p style="text-align: center">
                                                THE MADHYA PRADESH STATE AGRO INDUSTRIES DEVELOPMENT CORPORATION LIMITED<br>
                                                "PANCHANAN" 3rd FLOOR,MALAVIYA NAGAR BHOPAL<br>
                                                Phone(0755) - 2552652,2551756,2551807 Fax: 0755-2557305   
                                            </p>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Name</th>
                                        <td>
                                            <asp:label id="lblEmpName" runat="server" text=""></asp:label>
                                        </td>
                                        <th>Place Of Posting</th>
                                        <td>
                                            <asp:label id="lblPostingPlace" runat="server" text=""></asp:label>
                                        </td>
                                        <th>Designation</th>
                                        <td>
                                            <asp:label id="lblDesignation" runat="server" text=""></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>Pan No</th>
                                        <td>
                                            <asp:label id="lblPanNo" runat="server" text=""></asp:label>
                                        </td>
                                        <th>Accounting Year</th>
                                        <td>
                                            <asp:label id="lblAccountingYear" runat="server" text=""></asp:label>
                                        </td>
                                        <th>Assessment Year</th>
                                        <td>
                                            <asp:label id="lblAssessmentYear" runat="server" text=""></asp:label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="6">
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th colspan="6">COMPUTATION OF TAXABLE INCOME  <small style="color: teal">&nbsp;&nbsp; (
                                            <asp:label id="lblEmployeeType" runat="server" text=""></asp:label>
                                            <asp:hiddenfield id="hfEmployeeType" runat="server" />
                                            )</small></th>
                                    </tr>
                                    <tr>
                                        <td colspan="6"></td>
                                    </tr>
                                    <tr>
                                        <td>1</td>
                                        <td colspan="4">a) Total gross pay(Including arrear)</td>
                                        <td>
                                            <asp:label id="lblGrossSalary" cssclass="lblGrossSalary" runat="server" text=""></asp:label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td colspan="4">b) Income from house property</td>
                                        <td>
                                            <asp:textbox id="txtHouseProperty" cssclass="textarea txtHouseProperty" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:textbox>
                                            <%--<asp:Label ID="lblOtherIncome" CssClass="lblOtherIncome" runat="server" Text=""></asp:Label>--%>

                                        </td>
                                    </tr>


                                    <tr>
                                        <td></td>
                                        <td colspan="4">c) Income from other source</td>
                                        <td>
                                            <asp:textbox id="txtOtherIncome" cssclass="textarea txtOtherIncome" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:textbox>
                                            <%--<asp:Label ID="lblOtherIncome" CssClass="lblOtherIncome" runat="server" Text=""></asp:Label>--%>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td colspan="4">d) Medical Reimbursement</td>
                                        <td>
                                            <asp:textbox id="txtMedicalReimbur" cssclass="textarea txtMedicalReimbur" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:textbox>
                                            <%--<asp:Label ID="lblOtherIncome" CssClass="lblOtherIncome" runat="server" Text=""></asp:Label>--%>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>2</td>
                                        <td colspan="4">Allowable deduction 150000 or Less (Under section section 80C</td>
                                        <td>
                                            <asp:textbox id="txtbox80c" cssclass="textarea txtbox80c" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:textbox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>3</td>
                                        <td colspan="4">Standard deduction (50000)</td>
                                        <td>
                                            <asp:label id="lblStandardDeduction" cssclass="lblStandardDeduction" runat="server" text=""></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>4</td>
                                        <td colspan="4">Any other deduction as per IT Act(If applicable  80D/Under Section 24/80DD</td>
                                        <td>
                                            <%-- <asp:TextBox ID="txtbox80g" CssClass="textarea txtbox80g" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:TextBox>--%>
                                            <asp:label id="lblbox80g" cssclass="lblbox80g" runat="server" text=""></asp:label>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td colspan="3">a) Interest on home loan (Section 24)</td>
                                        <td>
                                            <asp:textbox id="txtbox24" cssclass="textarea txtbox24" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:textbox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td colspan="3">b) Mediclaim (Section 80D)</td>
                                        <td>
                                            <asp:textbox id="txtbox80d" cssclass="textarea txtbox80d" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:textbox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td colspan="3">c) Disabled dependant (Section 80DD)</td>
                                        <td>
                                            <asp:textbox id="txtbox80dd" cssclass="textarea txtbox80dd" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:textbox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td colspan="3">d) Other (Covid-19, PM Relief Fund, etc.)</td>
                                        <td>
                                            <asp:textbox id="txtcovid" cssclass="textarea txtcovid" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:textbox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td colspan="3">e) Interest on fixed deposit u section 80TTA</td>
                                        <td>
                                            <asp:textbox id="txt80tta" cssclass="textarea txt80tta" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:textbox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td colspan="3">f) Interest on fixed deposit u section 80TTB</td>
                                        <td>
                                            <asp:textbox id="txt80ttb" cssclass="textarea txt80ttb" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:textbox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td colspan="3">g) HRA Rebate</td>
                                        <td>
                                            <asp:textbox id="txthrrebate" cssclass="textarea txthrrebate" runat="server" onkeypress="return allowNegativeNumber(event);"></asp:textbox>
                                        </td>
                                    </tr>


                                    <%--                                    <tr>
                                        <td></td>
                                        <td>Remark:</td>
                                        <td colspan="4">
                                            <asp:TextBox ID="txtbox80greason" CssClass="textarea" TextMode="multiline" Columns="100" Rows="6" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td>5</td>
                                        <td colspan="4">Professional tax</td>
                                        <td>
                                            <asp:label id="lblProfessionalTax" cssclass="lblProfessionalTax" runat="server" text=""></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>6</td>
                                        <td colspan="4">Taxable income</td>
                                        <td>
                                            <asp:label id="lblTaxableIncome" cssclass="lblTaxableIncome" runat="server" text=""></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>7</td>
                                        <td colspan="4">Total payable tax</td>
                                        <td>
                                            <asp:label id="lblTotalPayableTax" cssclass="lblTotalPayableTax" runat="server" text=""></asp:label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td colspan="4">cess (4%)</td>
                                        <td>
                                            <asp:label id="lblcess" cssclass="lblcess" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td colspan="4">Total payable tax including cess</td>
                                        <td>
                                            <asp:label id="lbltaxwithcess" cssclass="lbltaxwithcess" runat="server"></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6"><b>Note:  </b>(i) Employees claiming exemption for H.R.A. have to enclose original receipt of rent paid to the owner for evidence.</td>
                                    </tr>
                                    <tr>
                                        <td colspan="6"><b>DECLARATION:-  </b>I declare that the information given as above is based on my best possible estimate and proposed investments payments/ deposits will be made by end of financial year.</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            Dated: </td>
                                        <td colspan="4">
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                        </td>
                                        <td>
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <b>SIGNATURE</b></td>
                                    </tr>

                                </tbody>
                            </table>
                            <div class="col-md-4 print_hidden"></div>
                            <div class="col-md-2 print_hidden">
                                <asp:button id="btnSave" onclick="btnSave_Click" cssclass="btn btn-success btn-block Aselect1" runat="server" text="Calculate Payable Tax" />
                            </div>
                            <div class="col-md-2 print_hidden">
                                <asp:button id="btnPrint" cssclass="btn btn-primary btn-block" runat="server" text="Print" onclientclick="printpage();" />
                            </div>
                            <div class="col-md-4 print_hidden"></div>
                        </div>
                    </div>

                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">


        function validateform1() {
            var msg = "";
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Financial Year. \n";
            }
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to see details ?")) {
                    return true;
                }
                else {
                    return false;
                }

            }
        }

        function printpage() {
            print();
        }


        function allowNegativeNumber(e) {
            var charCode = (e.which) ? e.which : event.keyCode
            if (charCode > 31 && (charCode < 45 || charCode > 57)) {
                return false;
            }
            return true;

        }



        $('.txtbox80c,.txtbox80dd,.txtbox80d,.txtbox24,.txtcovid,.txtOtherIncome,.txtHouseProperty,.txtMedicalReimbur,.txt80tta,.txt80ttb,.txthrrebate').on('focusout', function () {
            calculateTotal();
        });

        function calculateTotal() {
            var lblTaxableIncome = 0;
            var lblbox80g = 0;

            //var lblTotalPayableTax = 0;
            lblbox80g = ((Number($(".txtbox80dd").val()) + (Number($(".txtbox80d").val()) + (Number($(".txtbox24").val())) + (Number($(".txtcovid").val())) + (Number($(".txt80tta").val())) + (Number($(".txt80ttb").val())) + (Number($(".txthrrebate").val())))));
            //console.log(lblbox80g);
            lblTaxableIncome = ((Number($(".lblGrossSalary").text()) + Number($(".txtOtherIncome").val()) + Number($(".txtHouseProperty").val()) + Number($(".txtMedicalReimbur").val())) - Number(Number($(".lblProfessionalTax").text()) + (Number($(".lblStandardDeduction").text()) + (Number($(".txtbox80c").val()) + (Number(lblbox80g))))));

            /****************/
            $('.lblTaxableIncome').text(lblTaxableIncome.toFixed(2));
            $('.lblbox80g').text(lblbox80g.toFixed(2));
            //$('.lblTotalPayableTax').val(lblTotalPayableTax.toFixed(2));

        }


    </script>
    <link href="../finance/css/dataTables.bootstrap.min.css" rel="stylesheet" />
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
    <script src="../finance/js/buttons.colVis.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: false,
            ordering: false,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }
            ],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<""tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('.lblEmpDetail').text(),
                    exportOptions: {
                        //columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.lblEmpDetail').text(),
                    exportOptions: {
                        //columns: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]
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


        //$(".Earning").parent().parent().css("color", "green");
        //$(".Deduction").parent().parent().css("color", "red");
        $(".Deduction,.Earning").css("color", "black");
        $(".Deduction,.Earning").parent().css("background", "#eaeaea");
    </script>
</asp:Content>

