<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChequePrint.aspx.cs" Inherits="mis_Finance_ChequePrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
            font-weight:700;
        }
        </style>
      <%--<style>
        body {
            margin: 0mm 0mm 0mm 0mm;
            height: 3.67in;
            width: 8.00in;
        }
        page {
            height: 3.67in;width:8.00in;
        }
        .divcss {
            position:fixed;
            left:0;
            top:0;
            height: 3.67in;
            width: 8.00in;
        } .amtcss {
            position: fixed;
            top: 1.5in;
            left: 6.2in;
        }
        .partycss {
            position: fixed;
            top: 0.85in;
            left: 0.8in;
            font-size:13px;
        }
      </style>--%>
</head>
<body>
    <form id="form1" runat="server">
   <%-- <div class="divcss">
        <span style="padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.33in; display: block;">2&nbsp;&nbsp;&nbsp;9&nbsp;-&nbsp;0&nbsp;&nbsp;7&nbsp;-&nbsp;2&nbsp;&nbsp;&nbsp;0&nbsp;&nbsp;&nbsp;2&nbsp;&nbsp;&nbsp;2</span>
        <span style="padding-left: 0.8in; padding-right: 2.8in; display: block; font-size: 13px; line-height: 2.5; ">&nbsp;</span>
        <span class="partycss">mp state agro corporation bhopal mp state agro corporation bhopal</span>
        <span style="padding-left: 0.5in; padding-right: 0.29in; display: block; font-size: 13px; line-height: 2;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;seventy-seven million seven hundred </span>
       <span class="amtcss">&nbsp;&nbsp;7894561230</span>
    </div>--%>
        <div runat="server" id="divCheque"></div>
        <script>
            window.print();
        </script>
    </form>
</body>
</html>
