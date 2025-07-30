<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="RptMultiAccountPrinting.aspx.cs" Inherits="mis_Finance_RptMultiAccountPrinting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
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
            padding: 5px 5px !important;
        }

        table.dataTable tbody th, table.dataTable thead th {
            padding: 8px 10px !important;
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

        tbody tr td:not(:first-child), tfoot tr td:not(:first-child) {
            text-align: right !important;
        }
    </style>
    <style type="text/css">
        .btn-danger {
            color: #4b4c9d;
        }

        table, tr, th, td {
            border: 2px solid black;
            border-bottom: 2px solid black;
        }

        .float-right {
            float: right !important;
        }

        .lead {
            font-size: 1.50rem;
            font-weight: 700;
        }

        .small {
            font-size: 1.50rem;
            font-weight: 500;
        }

        .lblpagenote {
            display: block;
            background: #e0eae7;
            text-align: -webkit-center;
            margin-bottom: 12px;
            padding: 10px;
        }

        @media print {
            .print_hidden {
                display: none;
            }

            .box-pramod {
                border: none;
            }

            .main-footer {
                display: none;
            }
        }

        p.note_main {
            font-weight: 600;
            color: blue;
            font-style: italic;
            display: initial;
            margin-right: 10px;
            border-right: 1px solid #5d5d5d;
            padding: 1px 10px;
        }

        b.note_key {
            color: orange;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content-header">
            <h1>Multi Account Printing
                   <small></small>
            </h1>
        </section>
        <section class="content">
            <div class="box box-pramod" style="background-color: #FFFFFF;">
                <div class="box-body">
                    <div class="row print_hidden">
                        <div class="col-md-12">
                            <fieldset class="box-body fieldset">
                                <legend>Multi Account Printing</legend>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>From Date<span style="color: red;">*</span></label>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>To Date</label><span style="color: red">*</span>
                                            <div class="input-group date">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar"></i>
                                                </div>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Voucher Type</label><span style="color: red">*</span>
                                            <asp:ListBox runat="server" ID="ddlVoucherType" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>



                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Button ID="btn" CssClass="btn btn-md btn-primary show_detail Aselect1" runat="server" Text="Submit" OnClick="btn_Click" />

                                            <%--<asp:Button ID="Button1" CssClass="btn btn-md clearbutton" runat="server" Text="Show Report" OnClick="btn_Click" />--%>
                                        </div>
                                    </div>
                                </div>

                            </fieldset>


                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12 print_hidden">
                            <asp:Label ID="lblprinttext" CssClass="printtext" ToolTip="" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblexceltext" CssClass="exceltext" ToolTip="" ClientIDMode="Static" runat="server" Text=""></asp:Label>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-12">
                                <asp:Label ID="lblpagenote" CssClass="print_hidden lblpagenote" runat="server" Text=""></asp:Label>

                                <div id="div_page_content" runat="server" class="page_content"></div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <%-- start data table--%>
    <%--<link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
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
    <script src="js/buttons.colVis.min.js"></script>--%>
    <script>

        //alert($('.printtext').attr('title'));

        //$('.datatable').DataTable({
        //    paging: true,
        //    columnDefs: [{
        //        targets: 'no-sort',
        //        orderable: false
        //    }],
        //    dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
        //      '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
        //      '<"row"<"col-sm-5"i><"col-sm-7"p>>',
        //    fixedHeader: {
        //        header: true
        //    },
        //    buttons: {
        //        buttons: [{
        //            extend: 'print',
        //            text: '<i class="fa fa-print"></i> Print',
        //            //title: $('h1').text(),
        //            title: $('.printtext').attr('title'),
        //            exportOptions: {
        //                columns: [0, 1, 2, 3, 4, 5, 6]
        //            },
        //            footer: true,
        //            autoPrint: true
        //        }, {
        //            extend: 'excel',
        //            text: '<i class="fa fa-file-excel-o"></i> Excel',
        //            //title: $('h1').text(),
        //            title: $('.exceltext').attr('title'),
        //            exportOptions: {
        //                columns: [0, 1, 2, 3, 4, 5, 6]
        //            },
        //            footer: true
        //        }],
        //        dom: {
        //            container: {
        //                className: 'dt-buttons'
        //            },
        //            button: {
        //                className: 'btn btn-default'
        //            }
        //        }
        //    }
        //});

        //end data table

        $('#txtFromDate').change(function () {
            //debugger;
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');
            if ($('#txtToDate').val() != "") {
                if (start > end) {
                    if ($('#txtFromDate').val() != "") {
                        alert("From date should not be greater than To Date.");
                        $('#txtFromDate').val("");
                    }
                }
            }
        });
        $('#txtToDate').change(function () {
            //debugger;
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');
            if (start > end) {
                if ($('#txtToDate').val() != "") {
                    alert("To Date can not be less than From Date.");
                    $('#txtToDate').val("");
                }
            }

        });
    </script>
    <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script>

        $(function () {
            $('[id*=ddlVoucherType]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
        //$(function () {
        //    $('[id*=ddlGroup]').multiselect({
        //        includeSelectAllOption: true,
        //        includeSelectAllOption: true,
        //        buttonWidth: '100%',

        //    });
        //});

    </script>
    <script>
        $('table tr td').each(function () {
            if ($(this).text() == '0') {
                $(this).css('color', 'red');
            }
        });
    </script>

    <script>
        function AddVoucher(voucherData) {
            //console.log(voucherData);
            //var data = $(data);
            //console.log(voucherData);
            //$(".page_content").append(data);
            //$(data).appendTo(".page_content");
            alert(voucherData);
        }
    </script>
    <style>
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
            max-height: 200px;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }

        ul.multiselect-container.dropdown-menu {
            overflow-y: scroll;
            overflow-x: hidden;
        }
    </style>
</asp:Content>
