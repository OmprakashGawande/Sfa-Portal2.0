<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task_Print.aspx.cs" Inherits="mis_Daily_Task_Task_Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Task List</title>
    <link rel="shortcut icon" href="image/favicon-icon.png" type="image/ico" />
    <%-- <link href="../css/bootstrap.css" rel="stylesheet" />
    <link href="../css/AdminLTE.css" rel="stylesheet" />--%>
    <style>
        .btn-pinterest {
            color: #ffffff;
            background-color: #cb2027;
            border-color: rgba(0, 0, 0, 0.2);
        }

        .btn-default {
            background-color: #f4f4f4;
            color: #444;
            border-color: #ddd;
        }

        .btn {
            border-radius: 3px;
            -webkit-box-shadow: none;
            box-shadow: none;
            border: 1px solid transparent;
        }

        .pull-right {
            float: right !important;
        }

        .btn {
            display: inline-block;
            padding: 6px 12px;
            margin-bottom: 0;
            font-size: 12px;
            font-weight: normal;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
        }
    </style>
    <style>
        @media print {
            @page {
                size: portrait;
                size: A4;
                margin: 0;
            }

            div.fix-break-print-page {
                page-break-inside: avoid;
            }

            #btnPrint, #btnBack {
                display: none;
            }

                #btnBack body {
                    /*margin: 4%;*/
                }

            /*.brPage {
                padding: 1%;
                border: none;
            }*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button runat="server" ID="btnBack" OnClick="btnBack_Click" CssClass="btn btn-default pull-right" Style="margin-left: 0.5%; margin-right: 5%; margin-top: 2%;" Text="Back" />
            <button runat="server" id="btnPrint" onclick="window.print();" class="btn btn-pinterest pull-right" style="margin-left: 0.5%; margin-top: 2%;"><i class="fa fa-print">Print</i></button>
            <div runat="server" id="divPrint"></div>
        </div>
    </form>
</body>
</html>
