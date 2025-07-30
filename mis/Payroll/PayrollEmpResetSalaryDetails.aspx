<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="PayrollEmpResetSalaryDetails.aspx.cs" Inherits="mis_Payroll_PayrollEmpResetSalaryDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        table th {
            text-align: center !important;
        }



        table > thead > tr > th, td {
            padding: 4px !important;
            font-size: 12px !important;
            white-space: nowrap;
        }

        #myInput {
            background-image: url('images/searchicon.png'); /* Add a search icon to input */
            background-position: 10px 12px; /* Position the search icon */
            background-repeat: no-repeat; /* Do not repeat the icon image */
            width: 100%; /* Full-width */
            font-size: 16px; /* Increase font-size */
            padding: 12px 20px 12px 40px; /* Add some padding */
            border: 1px solid #ddd; /* Add a grey border */
            margin-bottom: 12px; /* Add some space below the input */
        }

        #GridView1 {
            border-collapse: collapse; /* Collapse borders */
            width: 100%; /* Full-width */
            border: 1px solid #ddd; /* Add a grey border */
            font-size: 10px; /* Increase font-size */
        }

            #GridView1 th, #GridView1 td {
                text-align: left; /* Left-align text */
            }

            #GridView1 tr {
                /* Add a bottom border to all table rows */
                border-bottom: 1px solid #ddd;
            }

                #GridView1 tr.header, #GridView1 tr:hover {
                    /* Add a grey background color to the table header and on hover */
                    background-color: #f1f1f1;
                }

        .ss1, .snumber {
            color: #123456;
            font-size: 12px;
            font-weight: 600;
        }

        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('images/progress.gif') 50% 50% no-repeat rgb(249,249,249);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="loader"></div>
    <div class="content-wrapper">
        <%--  <div id="loaderWrapper" runat="server" class="loader-wrapper" style="display: none; background-color: rgba(108, 131, 243, 0.33);">
            <div id="loader"></div>
            <span id="loaderSpan">Please Wait...</span>
        </div>
         document.getElementById('<%=loaderWrapper.ClientID%>').style.display = "none";--%>
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success" style="min-height: 500px;">
                <div class="box-header">
                    <h3 class="box-title">Reset Employee Wise Salary Detail</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">

                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Office Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control select2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Month <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlMonth" runat="server" class="form-control">
                                    <%--<asp:ListItem Value="0">Select Month</asp:ListItem>
                                    <asp:ListItem Value="01">January</asp:ListItem>
                                    <asp:ListItem Value="02">February</asp:ListItem>
                                    <asp:ListItem Value="03">March</asp:ListItem>
                                    <asp:ListItem Value="04">April</asp:ListItem>
                                    <asp:ListItem Value="05">May</asp:ListItem>
                                    <asp:ListItem Value="06">June</asp:ListItem>
                                    <asp:ListItem Value="07">July</asp:ListItem>
                                    <asp:ListItem Value="08">August</asp:ListItem>
                                    <asp:ListItem Value="09">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>--%>

                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group"></div>
                    <asp:Label ID="lblTab" runat="server" Style="font-weight: 700; font-size: 20px; color: red;" Text=""></asp:Label>
                    <div id="DivDetail" runat="server">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive" style="height: 400px;">
                                    <%-- <div class="body-scroll">--%>
                                    <asp:TextBox ID="myInput" runat="server" ClientIDMode="Static" onkeyup="myFunction()" placeholder="Search for names.." title="Type in a name"></asp:TextBox>
                                    <asp:Label ID="lblrowcount" Style="color: red;" runat="server" Text=""></asp:Label>
                                    <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped Grid" ClientIDMode="Static" AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="Emp_ID">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" Checked="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SNo.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Emp_ID").ToString()%>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UserName" HeaderText="Employee-ID" />
                                            <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                            <asp:TemplateField HeaderText="Basic Salary">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalary_Basic" runat="server" Text='<%# Eval("Salary_Basic").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Basic Pay Scale" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalary_BasicPayScale" runat="server" Text='<%# Eval("Salary_BasicPayScale").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payable Days">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalary_PayableDays" runat="server" Text='<%# Eval("Salary_PayableDays").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Earning Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalary_EarningTotal" runat="server" Text='<%# Eval("Salary_EarningTotal").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deduction Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalary_DeductionTotal" runat="server" Text='<%# Eval("Salary_DeductionTotal").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Salary">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalary_NetSalary" runat="server" Text='<%# Eval("Salary_NetSalary").ToString()%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Detail">
                                                <ItemTemplate>
                                                    <a id="a1" runat="server" style="font-weight: bold" target="_blank" href='<%# "PayrollEmpSalarySlip.aspx?Emp_ID=" +APIProcedure.Client_Encrypt(Eval("Emp_ID").ToString())+"&&Office_ID=" +APIProcedure.Client_Encrypt(Eval("Office_ID").ToString())+"&&Year=" +APIProcedure.Client_Encrypt(Eval("Year").ToString())+"&&MonthNo=" +  APIProcedure.Client_Encrypt(Eval("MonthNo").ToString())+"&&GenStatus=" +  APIProcedure.Client_Encrypt(Eval("GenStatus").ToString()) %>'>Detail</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="form-group"></div>
                        <div class="row">
                            <div class="col-md-8"></div>
                            <div class="col-md-2">
                                <asp:Button ID="btnReset" CssClass="btn btn-block btn-success" runat="server" Text="Reset Salary" OnClick="btnReset_Click" OnClientClick="return validateform();" />
                            </div>
                            <%--                            <div class="col-md-2">
                                <a class="btn btn-block btn-default" href="AdminDesignation.aspx">Clear</a>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        $(document).ready(function () {
            $('.loader').fadeOut();
        });
        $('#checkAll').click(function () {
            var inputList = document.querySelectorAll('#GridView1 tbody input[type="checkbox"]:not(:disabled)');
            for (var i = 0; i < inputList.length; i++) {
                if (document.getElementById('checkAll').checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        });



        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlOfficeName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office name. \n";
            }

            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Month. \n";
            }
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Financial Year. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }

    </script>
    <script>
        function myFunction() {
            var input, filter, table, tr, td, i;
            input = document.getElementById("myInput");
            filter = input.value.toUpperCase();
            table = document.getElementById("GridView1");
            tr = table.getElementsByTagName("tr");
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[3];
                if (td) {
                    if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        }
    </script>
</asp:Content>





