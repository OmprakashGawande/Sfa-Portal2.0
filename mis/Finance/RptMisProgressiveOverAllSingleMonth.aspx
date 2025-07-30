<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptMisProgressiveOverAllSingleMonth.aspx.cs" Inherits="mis_Finance_RptMisProgressiveOverAllSingleMonth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>
    <style>
        .highcharts-button-symbol {
            display: none;
        }

        .highcharts-credits {
            display: none;
        }
    </style>
    <style>
        .misreport {
            font-size: 12px !important;
            font-family: verdana;
        }

            .misreport th {
                padding: 2px;
                max-width: 55px;
                word-break: break-word;
                font-size: 11px;
            }

            .misreport td:not(:first-child) {
                text-align: right;
            }

            .misreport tr td {
                padding: 2px;
            }

            .misreport tr:nth-child(16), .misreport tr:nth-child(23), .misreport tr:nth-child(24), .misreport tr:nth-child(28) {
                font-weight: 600;
            }

            .misreport table {
                width: 100%;
                margin: 0px auto;
            }

        @media print {
            .hide-print {
                display: none;
            }
        }

        .lblheading {
            text-align: center;
            font-weight: 600;
        }
    </style>
        <style>
        .show_detail {
            margin-top: 24px;
        }

        .table > tbody > tr > th {
            padding: 5px;
        }

        a:hover {
            color: red;
        }

        /*tr:hover td {
            background-color: #fefefe !important;
        }*/
        table.dataTable tbody td, table.dataTable thead td {
            padding: 1px 1px !important;
        }

        table.dataTable tbody th, table.dataTable thead th {
            padding: 2px 3px !important;
        }

        table.dataTable thead th, table.dataTable thead td {
            padding: 5px 7px;
            border-bottom: none !important;
        }

        table.dataTable tfoot th, table.dataTable tfoot td {
            border-bottom: none !important;
        }

        table.dataTable.no-footer {
            border-bottom: none !important;
        }

        a.dt-button.buttons-collection.buttons-colvis, a.dt-button.buttons-collection.buttons-colvis:hover {
            background: #EF5350;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.dt-button.buttons-excel.buttons-html5, a.dt-button.buttons-excel.buttons-html5:hover {
            background: #ff5722c2;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            margin-left: 6px;
            border: none;
        }

        a.dt-button.buttons-print, a.dt-button.buttons-print:hover {
            background: #e91e639e;
            color: white;
            border-radius: unset;
            box-shadow: 2px 2px 2px #808080;
            border: none;
        }

        thead tr th {
            background: #9e9e9ea3 !important;
        }

        tbody tr td(:fifth-child), tfoot tr td(:fifth-child) {
            text-align: right !important;
        }

       table.dataTable tbody tr {
    background-color: #ffffff !important;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h1 class="box-title">MIS Monthly Report (Overall Region Wise)</h1>
                    <asp:Label ID="lblMsg" Text="" runat="server"></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row hide-print">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Year</label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control Select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Month</label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control Select2">
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
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success" Text="Search" Style="margin-top: 25px;" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="misreport">

                               <div class="row" style="margin-top: 20px;">
                                    <div class="col-md-12">
                                        <div class="col-md-12">
                                            <div id="container1"></div>
                                            <div id="divchartPregressive" runat="server">
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div id="container2"></div>
                                            <div id="divchartPregressive2" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="table-responsive">

                                    <p style="text-align:center;">
                                        <br />
                                        <asp:Label ID="lblheading" CssClass="lblheading" runat="server" Text=""></asp:Label>
                                        <br />
                                    </p>
                                    <asp:GridView ID="GridView1" runat="server" CssClass="GridView1 datatable" EmptyDataText="No Record Found" AutoGenerateColumns="true">
                                        <Columns>

                                            <%-- <asp:TemplateField HeaderText="PARTICULARS" >
                                            <ItemTemplate>                                                
                                                <asp:Label  ID="lblParticularName" runat="server" Text='<%# Eval("Particular_Name") %>'></asp:Label>                                             
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="L YEAR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLYear" runat="server" Text='<%# Eval("LYear") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="C YEAR">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCYear" runat="server" Text='<%# Eval("CYear") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div id="DivTable" runat="server"></div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function validateform() {
            var msg = "";


            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg += "Select Year \n";

            }
            if (document.getElementById('<%=ddlMonth.ClientID%>').selectedIndex == 0) {
                msg += "Select Month \n";

            }
            if (msg != "") {
                alert(msg);
                return false;

            }
            if (msg != "") {
                alert(msg);
                return false;

            }
            else {
                if (document.getElementById('<%=btnSearch.ClientID%>').value.trim() == "Search") {

                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true

                }


            }
        }
    </script>
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
            paging: false,
            dom: 'Bfrtip',
            ordering: false,
            oSearch: { bSmart: false, bRegex: true },
            buttons: [
                {
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('.lblheading').html(),
                    exportOptions: {
                        //columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true,
                },
                {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('.lblheading').html(),
                    exportOptions: {
                        //columns: [0, 1, 2, 3, 4, 5]
                    },
                    footer: true
                }

            ]
        });
    </script>
</asp:Content>

