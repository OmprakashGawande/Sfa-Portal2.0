<%@ Page Language="C#" AutoEventWireup="true" CodeFile="htmEditor.aspx.cs" Inherits="mis_Daily_Task_htmEditor" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</asp:ToolkitScriptManager>
    <form id="form1" runat="server">
       
        <div>
            <asp:TextBox ID="txtEditor" runat="server" Width="300" Height="200" />
           <%-- <asp:HtmlEditorExtender runat="server"></asp:HtmlEditorExtender>--%>
            <br />
            <asp:Button Text="Submit" runat="server" OnClick="save_Click" />
            <br />
            <asp:Label ID="lblContents" runat="server" />
        </div>
    </form>
</body>
</html>
