<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayrollBankWisePaybill.aspx.cs" Inherits="mis_Payroll_PayrollBankWisePaybill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap.css" rel="stylesheet" />
    <style>
        .salary-logo {
            -webkit-filter: grayscale(100%);
            filter: grayscale(100%);
            width: 40px;
        }
    </style>
</head>
<body style="font-size: 18px; width: 1200px;">
    <form id="form2" runat="server">

        <p style="text-align: center">
           M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd.<br />
           <%-- PANCHANAN, 3rd FLOOR, MALVIYA NAGAR BHOPAL<br />--%>
            BANK WISE DETAIL OF
                        <asp:Label ID="lblyear" runat="server" Text=""></asp:Label>
        </p>
        <br />
        <div id="DivTable" runat="server">
        </div>
        <table style="width: 100%;">
            <tr>
                <td style="width: 33%; text-align: center">
                    <p style="text-align: center;"><b>DY MANAGER (A/CS)</b></p>
                </td>
                <td style="width: 33%; text-align: center">
                   <%-- <p style="text-align: center;"><b>DGM(ACCTS)</b></p>--%>
                </td>
                <td style="width: 33%; text-align: center">
                    <p style="text-align: center;"><b>GM(ACCTS)</b></p>
                </td>
            </tr>
        </table>


        <script>
              window.print();
        </script>
    </form>
</body>
</html>
