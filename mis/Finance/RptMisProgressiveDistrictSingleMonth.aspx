<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptMisProgressiveDistrictSingleMonth.aspx.cs" Inherits="mis_Finance_RptMisProgressiveDistrictSingleMonth" %>

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
                padding: 5px;
                word-break: break-word;
                font-size: 13px;
            }

            .misreport td:not(:first-child) , .misreport th:not(:first-child){
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h1 class="box-title">MIS Monthly Report (Branch)</h1>
                    <asp:Label ID="lblMsg" Text="" runat="server"></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row hide-print">
                         <div class="col-md-3">
                            <div class="form-group">
                                <label>Branch</label>
                                <asp:DropDownList ID="ddlOffice" runat="server" CssClass="form-control Select2" Enabled="false"></asp:DropDownList>
                            </div>
                        </div>
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



    <%--<div id="divchart" class="hidden" runat="server"></div>
    <asp:HiddenField ID="hfyear" runat="server" />
    <asp:HiddenField ID="hfoffice" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfDate" runat="server" />--%>
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
            lengthChange: false,
            ordering: false,
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
                    title: $('.lblheading').text(),
                    exportOptions: {
                        columns: [0, 1, 2]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2]
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
        //Highcharts.chart('container', {
        //    chart: { type: 'column' }, title: { text: $(".lblheading").text() + " Graphical Report." }, xAxis: {
        //        categories:
        //              ['Apples', 'Oranges', 'Pears', 'Grapes', 'Bananas']
        //    }, credits: { enabled: false }, series: [{
        //        name: 'Last Year', data:
        //            [5, 3, 4, 7, 2]
        //    }, {
        //        name: 'Current Year', data:
        //        [2, -2, -3, 2, 1]
        //    }]
        //});




    </script>

    <%-- <script>Highcharts.chart('container1', {chart: {type: 'column'},title: { text: ' Graphical Report.'}, xAxis: { categories:,PRO/LOSS
]},credits: {enabled: false }, series: [{ name: 'Last Year', data:,0.00]}, {name: 'Current Year', data:,0.00] }]});</script>--%>
</asp:Content>

