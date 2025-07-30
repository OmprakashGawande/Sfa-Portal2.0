<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRAttendancePermissionLetter.aspx.cs" Inherits="mis_HR_HRAttendancePermissionLetter" %>

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
<body style="font-size: 18px;">
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="text-center">
                        <img src="../image/mpagro-logo.png" class="salary-logo" /><br />
                    </div>


                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <p style="text-align: center">
                        THE MADHYA PRADESH STATE AGRO INDUSTRIES DEVELOPMENT CORPORATION LIMITED<br />
                        "PANCHANAN" 3rd FLOOR,MALAVIYA NAGAR BHOPAL<br />
                        Phone(0755) - 2552652,2551756,2551807 Fax: 0755-2557305   
                    </p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <p style="text-align: left;">क्रमांक /मुख्या /...........................</p>
                </div>

                <div class="col-md-6">
                    <p style="text-align: right;">
                        दिनांक :
                        <asp:Label runat="server" ID="lblDate"></asp:Label>
                    </p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <p style="text-align: center;">
                       :: कार्यालयीन पत्र ::
                    </p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-4">
                    प्रति,<br />
                    <p>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblEmpName" Text=""></asp:Label><br />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label runat="server" ID="lblEmpDesignation" Text=""></asp:Label><br />
                    </p>
                </div>                
            </div>
            <br />
            <br />
            <div class="row">
                <div class="col-md-12">
                    <p style="text-decoration-line: underline">
                        <strong>विषय: - दैनिक उपस्थिति की अनियमितता के सम्बन्ध में |</strong>

                    </p>
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <p>
                        ई. आर . पी . सॉफ्टवेयर से प्राप्त उपस्थिति के अनुसार , यह पाया गया की आपकी दैनिक उपस्थिति की अनियमितता की स्थिति निम्नानुसार है |
                    </p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" CssClass="table table-bordered" ID="GridView1" AutoGenerateColumns="false" ShowFooter="true">
                            <Columns>
                                <asp:TemplateField HeaderText="क्रमांक (1)">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="दिनांक (2)" DataField="Att_Date" />
                                <asp:BoundField HeaderText="लॉग इन  समय (3)" DataField="LoginTime" />
                                <asp:BoundField HeaderText="लॉग आउट समय (4)" DataField="LogoutTime" />
                                <asp:BoundField HeaderText="लॉग इन एवं लॉग आउट के बीच का समय(5) " DataField="WorkingHours" /> 
                                <asp:BoundField HeaderText="अनियमितता विवरण (6)" DataField="Att_Type" />
                                <asp:BoundField HeaderText="प्रशासकीय निर्णय (7)" DataField="Allow_Status2" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <p>अतः उक्त स्थिति को देखते हुए, कॉलम क्रमांक 7 के अनुसार कार्यवाही की जाती है |</p>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <br />
                        <br />
                        <br />
                        <p style="text-align: right;">महाप्रबंधक(प्रशासन)</p>
                        <p style="text-align: right;">एम पी स्टेट एग्रो इंडस्ट्रीज डेवलपमेंट कोर्प.</p>
                        <p style="text-align: right;">पंचानन भवन , भोपाल </p>
                    </div>
                </div>
            </div>

        </div>
        <script>
            window.print();
        </script>
    </form>
</body>
</html>
