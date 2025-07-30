<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayrollCoverLetter.aspx.cs" Inherits="mis_Payroll_PayrollCoverLetter" %>

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
                        THE MADHYA PRADSH STATE AGRO INDUSTRIES DEVELOPMENT CORPORATION LIMITED<br />
                        "PANCHANAN" 3rd FLOOR,MALAVIYA NAGAR BHOPAL<br />
                        Phone(0755) - 2552652,2551756,2551807 Fax: 0755-2557305   
                    </p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <p style="text-align:left;">क़ /मुख्या /लेखा /2018.19/</p>
                    
                </div>
                
                <div class="col-md-6">
                    <p style="text-align:right;">
                        दिनांक :
                        <asp:Label runat="server" ID="lblDate"></asp:Label>
                    </p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-2">
                    प्रति,<br />
                    <p>
                        &nbsp;&nbsp;&nbsp;&nbsp;मुख्य महाप्रबन्धक<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;पंजाब नेशनल बैंक<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;न्यू मार्केट, भोपाल |  
                    </p>
                </div>

            </div>
            <div class="row">
                <div class="col-md-12">
                    <p style="text-decoration-line: underline">
                        <strong>विषय: - निगम के चालू खाता क्रमांक 1276001800000012 को रू <%=TotalSalary %> डेबिट करने
                            बाबत |</strong>
                    </p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <p>महोदय,</p>
                    <p>
                        निगम के कर्मचारी / अधिकरियों के वेतन मद के अंतर्गत चालू खाता क्रमांक
                        1276001800000012 को रूपये <strong><%=TotalSalary %> </strong>डेबिट करने को कष्ट करें | जिसका विवरण
                        निम्नानुसार हैं :-
                    </p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="table-responsive">
                        <asp:GridView runat="server" CssClass="table table-bordered" ID="GridView1" AutoGenerateColumns="false" ShowFooter="true">
                            <Columns>
                                <asp:TemplateField HeaderText="क्रमांक">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="बैंक का नाम" DataField="Bank_Name" />
                                <asp:BoundField HeaderText="राशि रुपये" DataField="Salary_NetSalary" DataFormatString="{0:N2}" />
                                <asp:BoundField HeaderText="आई. एफ. एस. सी. नंबर" DataField="Bank_IfscCode" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <p>उक्त के सम्बन्ध में विस्तृत जानकारी मेल द्वारा दी जाती है |</p>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <br /><br /><br />
                        <p style="text-align:right;">महाप्रबंधक(वित्त एवं कर)</p>
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