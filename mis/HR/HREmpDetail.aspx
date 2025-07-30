<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpDetail.aspx.cs" Inherits="mis_HR_HREmpDetail" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1 class="box-title">Employee Details</h1>
        </section>
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <%-- <h3 class="box-title">Department Master</h3>--%>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box-tools pull-right">
                                <div class="input-group input-group-sm" style="width: 200px; margin-bottom: 10px; margin-top: -20px;">
                                    <asp:TextBox ID="txtSearch" class="form-control pull-right" runat="server" placeholder="Search" MaxLength="50" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    <div class="input-group-btn">
                                        <asp:LinkButton runat="server" ID="btnSearch" Text="<i class='fa fa-search'></i>" class="btn btn-default" OnClick="txtSearch_TextChanged" />
                                    </div>
                                </div>
                            </div>
                            <!--OnSelectedIndexChanged="GridView1_SelectedIndexChanged"-->
                            <asp:GridView ID="GridView1" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="Emp_ID">
                                <Columns>
                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="Emp_FatherHusbandName" HeaderText="Father/Husband Name" />
                                    <asp:BoundField DataField="Emp_MobileNo" HeaderText="Mobile No." />
                                    <%-- <asp:BoundField DataField="Department_Name" HeaderText="Department" />
                                    <asp:BoundField DataField="Designation_Name" HeaderText="Designation" />--%>
                                    <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPersonalDetail" runat="server" CssClass="label label-default" CausesValidation="False" Text="Personal Detail" OnClick="lnkPersonalDetail_Click" CommandName="select" ToolTip='<%# Eval("Emp_ID").ToString()%>'></asp:LinkButton>
                                            <asp:LinkButton ID="lnkOfficialDetail" runat="server" CssClass="label label-default" CausesValidation="False" Text="Official Detail" CommandName="select" ToolTip='<%# Eval("Emp_ID").ToString()%>' OnClick="lnkOfficialDetail_Click"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkBankDetail" runat="server" CssClass="label label-default" CausesValidation="False" Text="Bank Detail" CommandName="select" ToolTip='<%# Eval("Emp_ID").ToString()%>' OnClick="lnkBankDetail_Click"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkFixedAssetsDetail" runat="server" CssClass="label label-default" CausesValidation="False" Text="Fixed Assets Detail" CommandName="select" ToolTip='<%# Eval("Emp_ID").ToString()%>' OnClick="lnkFixedAssetsDetail_Click"></asp:LinkButton><br />
                                            <asp:LinkButton ID="lnkChildrenDetail" runat="server" CssClass="label label-default" CausesValidation="False" Text="Children Detail" CommandName="select" ToolTip='<%# Eval("Emp_ID").ToString()%>' OnClick="lnkChildrenDetail_Click"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkNomineeDetail" runat="server" CssClass="label label-default" CausesValidation="False" Text="Nominee Detail" CommandName="select" ToolTip='<%# Eval("Emp_ID").ToString()%>' OnClick="lnkNomineeDetail_Click"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkOtherDetail" runat="server" CssClass="label label-default" CausesValidation="False" Text="Other Detail" CommandName="select" ToolTip='<%# Eval("Emp_ID").ToString()%>' OnClick="lnkOtherDetail_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkVerify" runat="server" CausesValidation="False" Text='<%# Eval("Status").ToString()%>' CommandName="select" ToolTip='<%# Eval("Emp_ID").ToString()%>' OnClick="lnkVerify_Click1" Visible='<%# Eval("Emp_IsActive").ToString()=="1" ? false : true %>' OnClientClick="return confirm('Do you really want to Verify?');"></asp:LinkButton>
                                            <asp:Label ID="lbl" Text='<%# Eval("Status").ToString()%>' runat="server" Visible='<%# Eval("Emp_IsActive").ToString()=="1" ? true : false %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <!--Personal Detail Modal-->
            <div class="row">
                <div class="col-md-12">
                    <div class="modal fade" id="PersonalDetailModal" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Personal Detail</h4>
                                </div>
                                <div class="modal-body">
                                    <section class="content">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="box box-success">
                                                    <div class="box-header">
                                                        <h3 class="box-title">Personal Detail</h3>
                                                        <asp:Label ID="lblPersonalDetail" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Employee Name (कर्मचारी का नाम)<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtEmp_Name" runat="server" placeholder="Enter Employee Name..." class="form-control" MaxLength="100" onkeypress="return validatename(event);" Enabled='<%# Eval("Emp_IsActive").ToString()=="1" ? false : true %>'></asp:TextBox>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label>Date of birth (जन्म की तारीख)<span style="color: red;">*</span></label>
                                                                    <div class="input-group date">
                                                                        <div class="input-group-addon">
                                                                            <i class="fa fa-calendar"></i>
                                                                        </div>
                                                                        <asp:TextBox ID="txtEmp_Dob" runat="server" placeholder="Select Date of birth..." Enabled='<%# Eval("Emp_IsActive").ToString()=="1" ? false : true %>' class="form-control DateAdd" autocomplete="off" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label>
                                                                        Father /Husband Name<br />
                                                                        (पिता / पति का नाम)<span style="color: red;">*</span></label>
                                                                    <table style="width: 100%;">
                                                                        <tr>
                                                                            <td style="width: 20%;">
                                                                                <%--  <label>Relation (रिश्ता)<span style="color: red;">*</span></label>--%>
                                                                                <asp:DropDownList ID="ddlEmp_Relation" runat="server" class="form-control" Style="padding-left: 0px; padding-right: 0px;">
                                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                                    <asp:ListItem Value="Father">Father</asp:ListItem>
                                                                                    <asp:ListItem Value="Husband">Husband</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="width: 80%;">
                                                                                <asp:TextBox ID="txtEmp_FatherHusbandName" runat="server" placeholder="Enter Father/Husband Name..." class="form-control" MaxLength="100" onkeypress="return validatename(event);"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Gender (लिंग)<span style="color: red;">*</span></label>
                                                                    <asp:DropDownList ID="ddlEmp_Gender" runat="server" class="form-control" Enabled='<%# Eval("Emp_IsActive").ToString()=="1" ? false : true %>'>
                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                        <asp:ListItem Value="Male">Male</asp:ListItem>
                                                                        <asp:ListItem Value="Female">Female</asp:ListItem>
                                                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label>Blood Groop (ब्लड ग्रुप)<span style="color: red;">*</span></label>
                                                                    <asp:DropDownList ID="ddlEmp_BloodGroup" runat="server" class="form-control" Enabled='<%# Eval("Emp_IsActive").ToString()=="1" ? false : true %>'>
                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                        <asp:ListItem Value="A+">A+</asp:ListItem>
                                                                        <asp:ListItem Value="A-">A-</asp:ListItem>
                                                                        <asp:ListItem Value="AB+">AB+</asp:ListItem>
                                                                        <asp:ListItem Value="AB-">AB-</asp:ListItem>
                                                                        <asp:ListItem Value="B+">B+</asp:ListItem>
                                                                        <asp:ListItem Value="B-">B-</asp:ListItem>
                                                                        <asp:ListItem Value="O+">O+</asp:ListItem>
                                                                        <asp:ListItem Value="O-">O-</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label>
                                                                        Marital Status
                                                                                        <br />
                                                                        (वैवाहिक स्थिति)<span style="color: red;">*</span></label>
                                                                    <asp:DropDownList ID="ddlEmp_MaritalStatus" runat="server" class="form-control" onchange="Validation();">
                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                        <asp:ListItem Value="Unmarried (अविवाहित)">Unmarried (अविवाहित)</asp:ListItem>
                                                                        <asp:ListItem Value="Married (विवाहित)">Married (विवाहित)</asp:ListItem>
                                                                        <asp:ListItem Value="Divorced (तलाकशुदा)">Divorced (तलाकशुदा)</asp:ListItem>
                                                                        <asp:ListItem Value="Widowed (विधवा)">Widowed (विधवा)</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group text-center">
                                                                    <label>Profile Image<span style="color: red;">*</span></label><br />
                                                                    <asp:Image ID="imgProfileImage" runat="server" ImageUrl="../image/boxed-bg.jpg" Style="height: 150px; width: 120px;" ClientIDMode="Static"></asp:Image>
                                                                </div>
                                                                <div class="form-group">
                                                                    <asp:FileUpload ID="FU_Emp_ProfileImage" runat="server" class="form-control" ClientIDMode="Static" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG', this),ValidateFileSize(this,1024*1024*1),previewProfileImage()" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Mobile no. (मोबाइल नंबर)<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtEmp_MobileNo" runat="server" placeholder="Enter Mobile No..." class="form-control" MaxLength="10" onkeypress="return validateNum(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Aadhar no. (आधार नंबर)<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtEmp_AadharNo" runat="server" Enabled="false" placeholder="Enter Aadhar No..." class="form-control" MaxLength="12" onkeypress="return validateNum(event)"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Pan Card no. (पैन कार्ड नंबर )<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtEmp_PanCardNo" runat="server" placeholder="Enter Pan Card No..." class="form-control PanCard" MaxLength="15" onkeypress="tbx_fnAlphaNumericOnly(event, this);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Email ID (ईमेल आईडी)<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtEmp_Email" runat="server" placeholder="Enter Email ID..." class="form-control" MaxLength="50" onkeypress="return validateusername(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        Husband/Wife Name
                                                                        <br />
                                                                        (का नाम)<span class="ValidateMaritalStatus" style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtEmp_HusbWifeName" runat="server" placeholder="Enter Husband / Wife Name..." class="form-control" MaxLength="100" onkeypress="return validatename(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>
                                                                        Husband/Wife Job/Business
                                                                        <br />
                                                                        (नौकरी/व्यवसाय)<span class="ValidateMaritalStatus" style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtEmp_HusbWifeJob" runat="server" placeholder="Enter Husband / Wife Job/Business..." class="form-control" MaxLength="100" onkeypress="return validatename(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Husband/Wife Designation/Department (पद/विभाग)<span class="ValidateMaritalStatus" style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtEmp_HusbWifeDep" runat="server" placeholder="Enter Husband/Wife Designation/Department..." class="form-control" MaxLength="100" onkeypress="return validatename(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Category (वर्ग)<span style="color: red;">*</span></label>
                                                                    <asp:DropDownList ID="ddlEmp_Category" runat="server" class="form-control">
                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                        <asp:ListItem Value="Scheduled Caste (अनुसूचित जाति)">Scheduled Caste (अनुसूचित जाति)</asp:ListItem>
                                                                        <asp:ListItem Value="Scheduled Tribe (अनुसूचित जनजाति)">Scheduled Tribe (अनुसूचित जनजाति)</asp:ListItem>
                                                                        <asp:ListItem Value="Other Backward Class (अन्य पिछड़ा वर्ग)">Other Backward Class (अन्य पिछड़ा वर्ग)</asp:ListItem>
                                                                        <asp:ListItem Value="General (सामान्य)">General (सामान्य)</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Religion (धर्म)<span style="color: red;">*</span></label>
                                                                    <asp:DropDownList ID="ddlEmp_Religion" runat="server" class="form-control">
                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                        <asp:ListItem Value="HINDU">HINDU</asp:ListItem>
                                                                        <asp:ListItem Value="MUSLIM">MUSLIM</asp:ListItem>
                                                                        <asp:ListItem Value="JAIN">JAIN</asp:ListItem>
                                                                        <asp:ListItem Value="CHRISTIAN">CHRISTIAN</asp:ListItem>
                                                                        <asp:ListItem Value="BUDDHISM">BUDDHISM</asp:ListItem>
                                                                        <asp:ListItem Value="SIKHISM">SIKHISM</asp:ListItem>
                                                                        <asp:ListItem Value="ZOROASTRIANISM">ZOROASTRIANISM</asp:ListItem>
                                                                        <asp:ListItem Value="JUDAISM">JUDAISM</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Physical Disability(शारीरिक विकलांगता)<span style="color: red;">*</span></label>
                                                                    <asp:RadioButtonList ID="rbtEmp_Disability" runat="server" RepeatColumns="2">
                                                                        <asp:ListItem Value="Yes">&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                                                        <asp:ListItem Value="No" Selected="True">&nbsp;No</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Disability Type(विकलांगता का प्रकार )<span style="color: red;">*</span></label>
                                                                    <asp:DropDownList ID="ddlEmp_DisabilityType" runat="server" class="form-control">
                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                        <asp:ListItem Value="NA/लागू नहीं">NA/लागू नहीं</asp:ListItem>
                                                                        <asp:ListItem Value="Vision Impaired (दृष्टि बाधित)">Vision Impaired (दृष्टि बाधित)</asp:ListItem>
                                                                        <asp:ListItem Value="Hearing Interrupted (श्रवण बाधित)">Hearing Interrupted (श्रवण बाधित)</asp:ListItem>
                                                                        <asp:ListItem Value="Bone Interrupted (अस्थि बाधित)">Bone Interrupted (अस्थि बाधित)</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <fieldset>
                                                                    <legend>CURRENT ADDRESS</legend>
                                                                    <div class="row">
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <label>State<span style="color: red;">*</span></label>
                                                                                <asp:DropDownList ID="ddlEmp_CurState" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEmp_CurState_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <label>City<span style="color: red;">*</span></label>
                                                                                <asp:DropDownList ID="ddlEmp_CurCity" runat="server" class="form-control">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <label>Pin Code <span style="color: red;">*</span></label>
                                                                                <asp:TextBox ID="txtEmp_CurPinCode" runat="server" placeholder="Enter Pin Code..." class="form-control" MaxLength="6" onkeypress="return validateNum(event)"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="form-group">
                                                                                <label>Address <span style="color: red;">*</span></label>
                                                                                <asp:TextBox ID="txtEmp_CurAddress" runat="server" placeholder="Enter Address..." TextMode="MultiLine" Rows="3" class="form-control" MaxLength="500"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </fieldset>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <fieldset>
                                                                    <legend>PERMANENT ADDRESS</legend>
                                                                    <div class="row">
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <label>State<span style="color: red;">*</span></label>
                                                                                <asp:DropDownList ID="ddlEmp_PerState" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEmp_PerState_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <label>City<span style="color: red;">*</span></label>
                                                                                <asp:DropDownList ID="ddlEmp_PerCity" runat="server" class="form-control">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <label>Pin Code <span style="color: red;">*</span></label>
                                                                                <asp:TextBox ID="txtEmp_PerPinCode" runat="server" placeholder="Enter Pin Cod..." class="form-control" MaxLength="6" onkeypress="return validateNum(event)"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="form-group">
                                                                                <label>Address <span style="color: red;">*</span></label>
                                                                                <asp:TextBox ID="txtEmp_PerAddress" runat="server" placeholder="Enter Address..." TextMode="MultiLine" Rows="3" class="form-control" MaxLength="100"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </fieldset>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <%--<div class="box-footer">
                                                                    
                                                                    <div class="form-group">
                                                                    <a class="btn btn-block btn-default" style="margin-top: 23px;" href="HREmpDetail.aspx" data-dismiss="modal">Close</a>
                                                                </div>
                                                    </div>--%>
                                            </div>
                                        </div>
                                    </section>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnUpdatePersonalDetail" CssClass="btn btn-success" runat="server" Text="Update" OnClientClick="return validatePersonalDetail()" OnClick="btnUpdatePersonalDetail_Click" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!--Official Detail Modal-->
            <div class="row">
                <div class="col-md-12">
                    <div class="modal fade" id="OfficialDetailModal" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Official Detail</h4>
                                </div>
                                <div class="modal-body">

                                    <asp:Label runat="server" ID="lblOfficialDetail"></asp:Label>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="box box-success">
                                                <div class="box-header">
                                                    <h3 class="box-title">Official Detail</h3>
                                                </div>
                                                <div class="box-body">
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Office Type (कार्यालय का प्रकार)<span style="color: red;">*</span></label>
                                                                <asp:DropDownList ID="ddlOfficeType_Title" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlOfficeType_Title_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Office (कार्यालय)<span style="color: red;">*</span></label>
                                                                <asp:DropDownList ID="ddlOffice_ID" runat="server" class="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>User Name<span style="color: red;">*</span></label>
                                                                <asp:TextBox ID="txtUserName" runat="server" placeholder="Enter ..." class="form-control" MaxLength="100"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Password<span style="color: red;">*</span></label>
                                                                <asp:TextBox ID="txtPassword" runat="server" placeholder="Enter ..." class="form-control" MaxLength="100"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Level (स्तर)<span style="color: red;">*</span></label>
                                                                <asp:DropDownList ID="ddlLevel" runat="server" class="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Class (श्रेणी)<span style="color: red;">*</span></label>
                                                                <asp:DropDownList ID="ddlEmp_Class" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlEmp_Class_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Designation (पद)<span style="color: red;">*</span></label>
                                                                <asp:DropDownList ID="ddlDesignation_ID" runat="server" class="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Department(विभाग)<span style="color: red;">*</span></label>
                                                                <asp:DropDownList ID="ddlDepartment_ID" runat="server" class="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Pay Scale ( वेतनमान)<span style="color: red;">*</span></label>
                                                                <asp:DropDownList ID="ddlPayScale_ID" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPayScale_ID_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Grade Pay (ग्रेड पे) <span style="color: red;">*</span></label>
                                                                <asp:DropDownList ID="ddlGradPay_ID" runat="server" class="form-control">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Basic Salary (मूल वेतन)<span style="color: red;">*</span></label>
                                                                <asp:TextBox ID="txtEmp_BasicSalery" runat="server" placeholder="Enter ..." class="form-control" MaxLength="100"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Joining Date ( कार्यग्रहण तिथि)<span style="color: red;">*</span></label>
                                                                <asp:Label ID="lblDob" runat="server" Text="" CssClass="hidden"></asp:Label>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtEmp_JoiningDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" autocomplete="off" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static" onchange="RetirementDate();"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Posting Date (पोस्टिंग तिथि)<span style="color: red;">*</span></label>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtEmp_PostingDate" runat="server" placeholder="Select Date..." class="form-control DateAdd" autocomplete="off" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Retirement Date (सेवानिवृत्ति की तारीख)<span style="color: red;">*</span></label>
                                                                <div class="input-group date">
                                                                    <div class="input-group-addon">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </div>
                                                                    <asp:TextBox ID="txtEmp_RetirementDate" runat="server" placeholder="Select Date..." class="form-control " autocomplete="off" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>

                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Type of Recruitment ( भर्ती का प्रकार)<span style="color: red;">*</span></label>
                                                                <asp:DropDownList ID="ddlEmp_TypeOfRecruitment" runat="server" class="form-control">
                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                    <asp:ListItem Value="Direct Recruitment">Direct Recruitment (सीधी भर्ती)</asp:ListItem>
                                                                    <asp:ListItem Value="Promotion">Promotion (पदोन्नति)</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Select GPF/DPF/NPS  <span style="color: red;">*</span></label>
                                                                <asp:DropDownList ID="ddlEmp_GpfType" runat="server" class="form-control">
                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                    <asp:ListItem Value="GPF">GPF</asp:ListItem>
                                                                    <asp:ListItem Value="DPF">DPF</asp:ListItem>
                                                                    <asp:ListItem Value="NPS">NPS</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>GPF/DPF/NPS No.<span style="color: red;">*</span></label>
                                                                <asp:TextBox ID="txtEmp_GpfNo" runat="server" placeholder="Enter ..." class="form-control" MaxLength="100"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Type of Post (पद प्रकार) <span style="color: red;">*</span></label>
                                                                <asp:DropDownList ID="ddlEmp_TypeOfPost" runat="server" class="form-control">
                                                                    <asp:ListItem>Select</asp:ListItem>
                                                                    <asp:ListItem Value="Permanent">Regular/Permanent</asp:ListItem>
                                                                    <asp:ListItem Value="Fixed Employee">Fixed Employee(स्थाई कर्मी)</asp:ListItem>
                                                                    <asp:ListItem Value="Contigent Employee">Contigent Employee</asp:ListItem>
                                                                    <asp:ListItem Value="Samvida Employee">Samvida Employee</asp:ListItem>
                                                                    <asp:ListItem Value="Theka Shramik">Theka Shramik</asp:ListItem>
                                                                    <%-- <asp:ListItem>Select</asp:ListItem>
                                            <asp:ListItem Value="Permanent">Permanent (स्थायी)</asp:ListItem>
                                            <asp:ListItem Value="Temporary">Temporary (अस्थायी)</asp:ListItem>
                                            <asp:ListItem Value="Probation">Probation (परिवीक्षा)</asp:ListItem>--%>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<div class="box-footer">
                                                                    <div class="form-group">
                                                                    <a class="btn btn-block btn-default" style="margin-top: 23px;" href="HREmpDetail.aspx" data-dismiss="modal">Close</a>
                                                                </div>
                                                    </div>--%>
                                        </div>
                                    </div>

                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnUpdateOfficialDetail" CssClass="btn btn-success" runat="server" Text="Update" OnClick="btnUpdateOfficialDetail_Click" OnClientClick="return validateOfficialDetail();" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!--Bank Detail Modal-->
            <div class="row">
                <div class="col-md-12">
                    <div class="modal fade" id="BankDetailModal" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Bank Detail</h4>
                                </div>
                                <div class="modal-body">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="box box-success">
                                                <div class="box-header">
                                                    <h3 class="box-title">Bank Details</h3>
                                                    <asp:Label runat="server" ID="lblBankDetail"></asp:Label>
                                                </div>
                                                <div class="box-body">
                                                    <div id="UpdateBankDetails" runat="server">
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Account No (खाता संख्या)<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtBank_AccountNo" runat="server" placeholder="Enter Account No..." class="form-control" MaxLength="20"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Account Type (खाते का प्रकार)<span style="color: red;">*</span></label>
                                                                    <asp:DropDownList ID="ddlBank_AccountType" runat="server" class="form-control">
                                                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                        <asp:ListItem Value="Salary Account">Salary Account</asp:ListItem>
                                                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Bank name(बैंक का नाम)<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtBank_Name" runat="server" placeholder="Enter Bank Name..." class="form-control" MaxLength="100"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Branch name(शाखा का नाम)<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtBank_BranchName" runat="server" placeholder="Enter Branch Name..." class="form-control" MaxLength="100"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>IFSC code (आई एफ एस सी कोड)<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtBank_IfscCode" runat="server" placeholder="Enter IFSC code..." class="form-control" MaxLength="100"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4"></div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnUpdateBankDetail" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Save" OnClick="btnUpdateBankDetail_Click" OnClientClick="return validateBankDetail();" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnClearBankDetail" CssClass="btn btn-block btn-default" runat="server" Style="margin-top: 23px;" Text="Clear" OnClick="btnClearBankDetail_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4"></div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:Label runat="server" ID="lblGridBankMsg" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                                            <asp:GridView ID="GridViewBankDetail" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="Bank_ID" OnSelectedIndexChanged="GridViewBankDetail_SelectedIndexChanged">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Bank_AccountNo" HeaderText="Account No (खाता संख्या)" />
                                                                    <asp:BoundField DataField="Bank_AccountType" HeaderText="Account Type (खाते का प्रकार)" />
                                                                    <asp:BoundField DataField="Bank_Name" HeaderText="Bank Name(बैंक का नाम)" />
                                                                    <asp:BoundField DataField="Bank_BranchName" HeaderText="Branch Name(शाखा का नाम)" />
                                                                    <asp:BoundField DataField="Bank_IfscCode" HeaderText="IFSC code (आई एफ एस सी कोड)" />
                                                                    <asp:TemplateField HeaderText="Edit">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label label-info" CommandName="select">Edit</asp:LinkButton>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="modal-footer">

                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!--FixedAssets Detail Modal-->
            <div class="row">
                <div class="col-md-12">
                    <div class="modal fade" id="FixedAssetsDetailModal" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Fixed Assets Detail</h4>
                                </div>
                                <div class="modal-body">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="box box-success">
                                                <div class="box-header">
                                                    <h3 class="box-title">Fixed Assets Details</h3>
                                                    <asp:Label runat="server" ID="lblFixedAssetsDetail"></asp:Label>
                                                </div>
                                                <div class="box-body">
                                                    <div id="UpdateFixedAssetsDetails" runat="server">
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Type of Property (संपत्ति का प्रकार)<span style="color: red;">*</span></label>
                                                                    <asp:DropDownList ID="ddlProperty_Type" runat="server" class="form-control">
                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                        <asp:ListItem Value="Plot">Plot</asp:ListItem>
                                                                        <asp:ListItem Value="House">House</asp:ListItem>
                                                                        <asp:ListItem Value="Agriculture">Agriculture</asp:ListItem>
                                                                        <asp:ListItem Value="Shop">Shop</asp:ListItem>
                                                                        <asp:ListItem Value="Other">Other</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Location Of Property(संपत्ति का स्थान )<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtProperty_Location" runat="server" placeholder="Enter Location Of Property..." class="form-control" MaxLength="100"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Property Purchase Year(संपत्ति खरीद वर्ष)<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtProperty_PurchaseYear" runat="server" placeholder="Enter Property Purchase Year..." class="form-control" MaxLength="4" onkeypress="return validateNum(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Property Purchase Price Rs.( संपत्ति खरीद मूल्य)<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtProperty_PurchasePrice" runat="server" placeholder="Enter Property Purchase Price Rs...." class="form-control" MaxLength="8" onkeypress="return validateDec(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Current rate of property Rs.(संपत्ति की वर्तमान दर)<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtProperty_CurrentRate" runat="server" placeholder="Enter Current rate of property Rs..." class="form-control" MaxLength="5" onkeypress="return validateDec(event);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="divFixedAssetsDetail" runat="server" class="row">
                                                            <div class="col-md-4"></div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnUpdateFixedAssetsDetail" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Save" OnClick="btnUpdateFixedAssetsDetail_Click" OnClientClick="return validateAssetsDetail();" />
                                                                    <!--OnClientClick="return validateAssetsDetail();" -->
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnClearFixedAssetsDetail" CssClass="btn btn-block btn-default" runat="server" Style="margin-top: 23px;" Text="Clear" OnClick="btnClearFixedAssetsDetail_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4"></div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:Label runat="server" ID="lblGridFixedAssetsMsg" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                                            <asp:GridView ID="GridViewFixedAssetsDetail" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="Property_ID" OnSelectedIndexChanged="GridViewFixedAssetsDetail_SelectedIndexChanged">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Property_Type" HeaderText="Type of Property (संपत्ति का प्रकार)" />
                                                                    <asp:BoundField DataField="Property_Location" HeaderText="Location Of Property(संपत्ति का स्थान )" />
                                                                    <asp:BoundField DataField="Property_PurchaseYear" HeaderText="Property Purchase Year(संपत्ति खरीद वर्ष)" />
                                                                    <asp:BoundField DataField="Property_PurchasePrice" HeaderText="Property Purchase Price( संपत्ति खरीद मूल्य)" />
                                                                    <asp:BoundField DataField="Property_CurrentRate" HeaderText="Current rate of property(संपत्ति की वर्तमान दर)" />
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkFAEdit" runat="server" CssClass="label label-info" CommandName="select">Edit</asp:LinkButton>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!--Children Detail Modal-->
            <div class="row">
                <div class="col-md-12">
                    <div class="modal fade" id="ChildrenDetailModal" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Children Detail</h4>
                                </div>
                                <div class="modal-body">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="box box-success">
                                                <div class="box-header">
                                                    <h3 class="box-title">Children Details</h3>
                                                    <asp:Label runat="server" ID="lblChildrenDetail"></asp:Label>
                                                </div>
                                                <div class="box-body">
                                                    <div id="UpdateChildrenDetails" runat="server">
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Son/Daughter (पुत्र / पुत्री)<span style="color: red;">*</span></label>
                                                                    <asp:DropDownList ID="ddlChild_Type" runat="server" class="form-control">
                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                        <asp:ListItem Value="Son">Son(पुत्र )</asp:ListItem>
                                                                        <asp:ListItem Value="Daughter">Daughter(पुत्री)</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Name (नाम)<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtChild_Name" runat="server" placeholder="Enter Name..." class="form-control" MaxLength="100"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Date of birth (जन्म की तारीख)<span style="color: red;">*</span></label>
                                                                    <div class="input-group date">
                                                                        <div class="input-group-addon">
                                                                            <i class="fa fa-calendar"></i>
                                                                        </div>
                                                                        <asp:TextBox ID="txtChild_Dob" runat="server" placeholder="Select Date of birth..." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" onpaste="return false" ClientIDMode="Static"></asp:TextBox>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Marital Status (वैवाहिक स्थिति)<span style="color: red;">*</span></label>
                                                                    <asp:DropDownList ID="ddlChild_MaritalStatus" runat="server" class="form-control">
                                                                        <asp:ListItem>Select</asp:ListItem>
                                                                        <asp:ListItem Value="Unmarried (अविवाहित)">Unmarried (अविवाहित)</asp:ListItem>
                                                                        <asp:ListItem Value="Married (विवाहित)">Married (विवाहित)</asp:ListItem>
                                                                        <asp:ListItem Value="Divorced (तलाकशुदा)">Divorced (तलाकशुदा)</asp:ListItem>
                                                                        <asp:ListItem Value="Widowed (विधवा)">Widowed (विधवा)</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Education(शिक्षा)<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtChild_Education" runat="server" placeholder="Enter Education..." class="form-control" MaxLength="100"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <div class="form-group">
                                                                    <label>Business/Job(व्यापार / नौकरी)<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtChild_Business" runat="server" placeholder="Enter Business/Job..." class="form-control" MaxLength="100"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="divChildrenDetail" runat="server" class="row">
                                                            <div class="col-md-4"></div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnUpdateChildrenDetail" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Save" OnClick="btnUpdateChildrenDetail_Click" OnClientClick="return validateChildrenDetail();" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnClearChildrenDetail" CssClass="btn btn-block btn-default" runat="server" Style="margin-top: 23px;" Text="Clear" OnClick="btnClearChildrenDetail_Click" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-4"></div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:Label runat="server" ID="lblGridChilderMsg" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                                            <asp:GridView ID="GridViewChildrenDetail" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="Child_ID" OnSelectedIndexChanged="GridViewChildrenDetail_SelectedIndexChanged">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Child_Type" HeaderText="Son/Daughter (पुत्र / पुत्री)" />
                                                                    <asp:BoundField DataField="Child_Name" HeaderText="Name (नाम)" />
                                                                    <asp:BoundField DataField="Child_Dob" HeaderText="Date of birth (जन्म की तारीख)" />
                                                                    <asp:BoundField DataField="Child_MaritalStatus" HeaderText="Marital Status (वैवाहिक स्थिति)" />
                                                                    <asp:BoundField DataField="Child_Education" HeaderText="Education(शिक्षा)" />
                                                                    <asp:BoundField DataField="Child_Business" HeaderText="Business/Job(व्यापार / नौकरी)" />
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkChildEdit" runat="server" CssClass="label label-info" CommandName="select">Edit</asp:LinkButton>

                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!--Nominee Detail Modal-->
            <div class="row">
                <div class="col-md-12">
                    <div class="modal fade" id="NomineeDetailModal" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Nominee Detail</h4>
                                </div>
                                <div class="modal-body">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="box box-success">
                                                <div class="box-header">
                                                    <h3 class="box-title">Nominee Details</h3>
                                                    <asp:Label runat="server" ID="lblNomineeDetail"></asp:Label>
                                                </div>
                                                <div class="box-body">
                                                    <div class="row" id="UpdateNomineeDetails" runat="server">
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Nominee Name(नामांकित का नाम )<span style="color: red;">*</span></label>
                                                                <asp:TextBox ID="txtNominee_Name" runat="server" placeholder="Enter Nominee Name..." class="form-control" MaxLength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-4">
                                                            <div class="form-group">
                                                                <label>Relation With Nominee (नामांकित से संबंध)<span style="color: red;">*</span></label>
                                                                <asp:TextBox ID="txtNominee_Relation" runat="server" placeholder="Enter Relation With Nominee..." class="form-control" MaxLength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div id="divNomineeDetail" runat="server">
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnUpdateNomineeDetail" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Save" OnClick="btnUpdateNomineeDetail_Click" OnClientClick="return validateNomineeDetail();" />
                                                                    <!-- OnClientClick="return validateNomineeDetail();"-->
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnClearNomineeDetail" CssClass="btn btn-block btn-default" runat="server" Text="Clear" Style="margin-top: 23px;" OnClick="btnClearNomineeDetail_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:Label runat="server" ID="lblGridNomineeMsg" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                                            <asp:GridView ID="GridViewNomineeDetail" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="Nominee_ID" OnSelectedIndexChanged="GridViewNomineeDetail_SelectedIndexChanged">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Nominee_Name" HeaderText="Nominee Name(नामांकित का नाम )" />
                                                                    <asp:BoundField DataField="Nominee_Relation" HeaderText="Relation With Nominee (नामांकित से संबंध)" />
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEditNomineeDetail" runat="server" CssClass="label label-info" CommandName="select">Edit</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <!--Other Detail Modal-->
            <div class="row">
                <div class="col-md-12">
                    <div class="modal fade" id="OtherDetailModal" role="dialog">
                        <div class="modal-dialog modal-lg">

                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="modal-title">Other Detail</h4>
                                </div>
                                <div class="modal-body">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="box box-success">
                                                <div class="box-header">
                                                    <h3 class="box-title">Other Details</h3>
                                                    <asp:Label runat="server" ID="lblOtherDetail"></asp:Label>
                                                </div>
                                                <div class="box-body">
                                                    <div id="UpdateOtherDetail" runat="server">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>Training related Information(प्रशिक्षण से संबंधित जानकारी)<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtOther_Training" runat="server" placeholder="Enter Training related Information..." TextMode="MultiLine" Rows="4" class="form-control" MaxLength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label>विशेष उपलब्धि आदि की जानकारी (इस क़ालम में निगम में यदि आपको पुरस्कृत किया है अथवा प्रशारित पत्र आदि दिया गया है उसका विवरण दिया जाना है )<span style="color: red;">*</span></label>
                                                                    <asp:TextBox ID="txtOther_Achievement" runat="server" placeholder="Enter Achievments..." TextMode="MultiLine" Rows="4" class="form-control" MaxLength="100" onkeypress="javascript:tbx_fnAlphaOnly(event, this);"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div id="divOtherDetail" runat="server" class="row">
                                                            <div class="col-md-8"></div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnUpdateOtherDetail" CssClass="btn btn-block btn-success" runat="server" Text="Save" OnClick="btnUpdateOtherDetail_Click" OnClientClick="return validateOtherDetail();" />

                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnClearOtherDetail" CssClass="btn btn-block btn-default" runat="server" Text="Clear" OnClick="btnClearOtherDetail_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:Label runat="server" ID="lblGridOtherMsg" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                                            <asp:GridView ID="GridViewOtherDetail" runat="server" class="table table-hover table-bordered pagination-ys" AutoGenerateColumns="False" DataKeyNames="Other_ID" OnSelectedIndexChanged="GridViewOtherDetail_SelectedIndexChanged">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="Other_Training" HeaderText="प्रशिक्षण से संबंधित जानकारी (Training related Information )" />
                                                                    <asp:BoundField DataField="Other_Achievement" HeaderText="विशेष उपलब्धि आदि की जानकारी" />
                                                                    <asp:TemplateField HeaderText="Action">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkEditOtherDetail" runat="server" CssClass="label label-info" CommandName="select">Edit</asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">


    <script type="text/javascript">
        $('.DateAdd').datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy'
        })

        function openModal() {
            debugger
            // $('#PersonalDetailModal').modal('show');
            $('#PersonalDetailModal').modal({
                show: 'true',
                backdrop: 'static',
                keyboard: false
            })
        };
        function openModal1() {
            debugger
            // $('#OfficialDetailModal').modal('show');
            $('#OfficialDetailModal').modal({
                show: 'true',
                backdrop: 'static',
                keyboard: false
            })
        };
        function openModal2() {
            debugger
            $('#BankDetailModal').modal({
                show: 'true',
                backdrop: 'static',
                keyboard: false
            });
        };
        function openModal3() {

            $('#FixedAssetsDetailModal').modal({
                show: 'true',
                backdrop: 'static',
                keyboard: false
            });
        };
        function openModal4() {
            debugger
            $('#ChildrenDetailModal').modal({
                show: 'true',
                backdrop: 'static',
                keyboard: false
            });
        };
        function openModal5() {
            debugger
            $('#NomineeDetailModal').modal({
                show: 'true',
                backdrop: 'static',
                keyboard: false
            });
        };
        function openModal6() {
            debugger
            $('#OtherDetailModal').modal({
                show: 'true',
                backdrop: 'static',
                keyboard: false
            });
        };

        function RetirementDate() {
            debugger;

            // Joining Date
            var Jdate = document.getElementById('<%=txtEmp_JoiningDate.ClientID%>').value;
            var JEntryDate = Jdate.replace(/-/g, '/');
            var Jdatearray = Jdate.split("/");
            // DOB
            var date = document.getElementById('<%=lblDob.ClientID%>').innerText;
            var EntryDate = date.replace(/-/g, '/');
            var datearray = date.split("/");
            if (Jdatearray[0] == 1) {
                datearray[0] = "01";
            }
            var Retdate = datearray[2] + '-' + datearray[1] + '-' + datearray[0];

            var time = new Date(Retdate);
            if (datearray[0] == 1) {
                var old_date = time.setDate(time.getDate() - 1);
                var d = new Date(old_date);
                new_day = ('0' + (d.getDate())).slice(-2);
                new_month = ('0' + (d.getMonth() + 1)).slice(-2);
                new_year = d.getFullYear();
                Retdate = new_year + '-' + new_month + '-' + new_day;
            }
            else {
                var date = time;
                // var firstDay = new Date(date.getFullYear(), date.getMonth(), 1);
                var lastDay = new Date(date.getFullYear(), date.getMonth() + 1, 0);
                Retdate = lastDay.getFullYear() + '-' + (lastDay.getMonth() + 1) + '-' + (lastDay.getDate());
            }

            var startDob = new Date(Retdate);
            Retdate = new Date(startDob.getFullYear() + 62, startDob.getMonth(), startDob.getDate());
            Retdate = (Retdate.getDate()) + '/' + (Retdate.getMonth() + 1) + '/' + Retdate.getFullYear();

            document.getElementById("txtEmp_RetirementDate").value = Retdate;
            //   alert(Retdate);

        }

        function validatePersonalDetail() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=txtEmp_Name.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Employee Name.\n";
            }
            if (document.getElementById('<%=ddlEmp_Gender.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Gender. \n";
            }
            if (document.getElementById('<%=txtEmp_Dob.ClientID%>').value.trim() == "") {
                msg += "Enter DOB. \n";
            }
            if (document.getElementById('<%=ddlEmp_Relation.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Relation. \n";
            }
           <%-- if (document.getElementById('<%=FU_Emp_ProfileImage.ClientID%>').files.length == 0) {
                msg = msg + "Upload File. \n";
            }--%>
            if (document.getElementById('<%=txtEmp_FatherHusbandName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Father / Husband Name.\n";
            }
            if (document.getElementById('<%=ddlEmp_MaritalStatus.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Marital Status. \n";
            }
            if (document.getElementById('<%=ddlEmp_BloodGroup.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Blood Group. \n";
            }
            if (document.getElementById('<%=txtEmp_MobileNo.ClientID%>').value.trim() == "") {
                msg += "Enter Mobile No.\n";
            }
            else {
                if (document.getElementById('<%=txtEmp_MobileNo.ClientID%>').value.length != 10) {
                    msg += "Enter Valid Mobile No.\n";
                }
            }
            if (document.getElementById('<%=txtEmp_AadharNo.ClientID%>').value.trim() == "") {
                msg += "Enter Aadhar No.\n";
            }
            if (document.getElementById('<%=txtEmp_AadharNo.ClientID%>').value.length != 12) {
                msg += "Enter Valid Aadhar No.\n";
            }
            if (document.getElementById('<%=txtEmp_PanCardNo.ClientID%>').value.trim() == "") {
                msg += "Enter Pancard No.\n";
            }
            if (document.getElementById('<%=txtEmp_PanCardNo.ClientID%>').value.length < 10 || document.getElementById('<%=txtEmp_PanCardNo.ClientID%>').value.length > 15) {
                msg += "Enter Valid Pancard No.\n";
            }
            if (document.getElementById('<%=txtEmp_Email.ClientID%>').value.trim() == "") {
                msg += "Enter Email Address.\n";
            }
            if (document.getElementById('<%=txtEmp_Email.ClientID%>').value != "") {

                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                if (reg.test(document.getElementById('<%=txtEmp_Email.ClientID%>').value) == false) {
                    msg += "Enter Valid Email Address. \n";
                }
            }
            if (document.getElementById('<%=txtEmp_HusbWifeName.ClientID%>').value.trim() == "" && document.getElementById('<%=ddlEmp_MaritalStatus.ClientID%>').selectedIndex != 1) {
                msg += "Enter Husband / Wife Name.\n";
            }
            if (document.getElementById('<%=txtEmp_HusbWifeJob.ClientID%>').value.trim() == "" && document.getElementById('<%=ddlEmp_MaritalStatus.ClientID%>').selectedIndex != 1) {
                msg = msg + "Enter Husband / Wife Job / Business. \n";
            }
            if (document.getElementById('<%=txtEmp_HusbWifeDep.ClientID%>').value.trim() == "" && document.getElementById('<%=ddlEmp_MaritalStatus.ClientID%>').selectedIndex != 1) {
                msg += "Enter Husband / Wife Designation / Department .\n";
            }
            if (document.getElementById('<%=ddlEmp_Category.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Category. \n";
            }
            if (document.getElementById('<%=ddlEmp_Religion.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Religion. \n";
            }
            if (document.getElementById('<%=ddlEmp_DisabilityType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Disability Type. \n";
            }
            if (document.getElementById('<%=ddlEmp_CurState.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Current State. \n";
            }
            if (document.getElementById('<%=ddlEmp_CurCity.ClientID%>').selectedIndex == 0) {
                msg += "Select Current City. \n";
            }
            if (document.getElementById('<%=txtEmp_CurPinCode.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Current Pincode. \n";
            }
            if (document.getElementById('<%=txtEmp_CurAddress.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Current Address. \n";
            }
            if (document.getElementById('<%=ddlEmp_PerState.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Permanent State. \n";
            }
            if (document.getElementById('<%=ddlEmp_PerCity.ClientID%>').selectedIndex == 0) {
                msg += "Select Permanent City.\n";
            }
            if (document.getElementById('<%=txtEmp_PerPinCode.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Permanent Pincode. \n";
            }
            if (document.getElementById('<%=txtEmp_PerAddress.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Permanent Address. \n";
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (confirm("Do you really want to Save Personal Details.?")) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }

        function validateOfficialDetail() {
            debugger;
            var msg = "";

            if (document.getElementById('<%=ddlOfficeType_Title.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office Type. \n";
            }
            if (document.getElementById('<%=ddlOffice_ID.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Office. \n";
            }
            if (document.getElementById('<%=ddlEmp_Class.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Class. \n";
            }
            if (document.getElementById('<%=ddlDepartment_ID.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Department. \n";
            }
            if (document.getElementById('<%=ddlDesignation_ID.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Designation. \n";
            }
            if (document.getElementById('<%=ddlPayScale_ID.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select PayScale. \n";
            }
            if (document.getElementById('<%=ddlGradPay_ID.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Grade Pay. \n";
            }
            if (document.getElementById('<%=txtEmp_BasicSalery.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Employee Basic Salary.\n";
            }
            if (document.getElementById('<%=txtEmp_JoiningDate.ClientID%>').value.trim() == "") {
                msg += "Select Joining Date. \n";
            }
            if (document.getElementById('<%=txtEmp_PostingDate.ClientID%>').value.trim() == "") {
                msg += "Select Posting Date. \n";
            }
            if (document.getElementById('<%=txtEmp_RetirementDate.ClientID%>').value.trim() == "") {
                msg += "Select Retirement Date. \n";
            }
            if (document.getElementById('<%=ddlEmp_TypeOfRecruitment.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Type Of Recruitment. \n";
            }
            if (document.getElementById('<%=ddlEmp_TypeOfPost.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Type Of Post. \n";
            }
            if (document.getElementById('<%=ddlEmp_GpfType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Select GPF/DPF/NPS. \n";
            }
            if (document.getElementById('<%=txtEmp_GpfNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter GPF/DPF/NPS No.\n";
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (confirm("Do you really want to Save Official Details.?")) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }

        function validateBankDetail() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=txtBank_AccountNo.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Account Number.\n";
            }
            if (document.getElementById('<%=ddlBank_AccountType.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Type Of Bank Account. \n";
            }
            if (document.getElementById('<%=txtBank_Name.ClientID%>').value.trim() == "") {
                msg += "Enter Bank Name. \n";
            }
            if (document.getElementById('<%=txtBank_BranchName.ClientID%>').value.trim() == "") {
                msg += "Enter Branch Name. \n";
            }
            if (document.getElementById('<%=txtBank_IfscCode.ClientID%>').value.trim() == "") {
                msg += "Enter IFSC Code. \n";
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (confirm("Do you really want to Save Bank Details.?")) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }
        function validateAssetsDetail() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=ddlProperty_Type.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Type Of Property. \n";
            }
            if (document.getElementById('<%=txtProperty_Location.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Property Location.\n";
            }
            if (document.getElementById('<%=txtProperty_PurchaseYear.ClientID%>').value.trim() == "") {
                msg += "Enter Property Purchase Year. \n";
            }
            if (document.getElementById('<%=txtProperty_PurchasePrice.ClientID%>').value.trim() == "") {
                msg += "Enter Property Purchase Price. \n";
            }
            if (document.getElementById('<%=txtProperty_CurrentRate.ClientID%>').value.trim() == "") {
                msg += "Enter Property Current Rate. \n";
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (confirm("Do you really want to Save Assets Details.?")) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }
        function validateChildrenDetail() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=ddlChild_Type.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Type Of Child. \n";
            }
            if (document.getElementById('<%=txtChild_Name.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Child Name.\n";
            }
            if (document.getElementById('<%=txtChild_Dob.ClientID%>').value.trim() == "") {
                msg += "Select Child Date Of Birth. \n";
            }
            if (document.getElementById('<%=ddlChild_MaritalStatus.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Child Marital Status. \n";
            }
            if (document.getElementById('<%=txtChild_Education.ClientID%>').value.trim() == "") {
                msg += "Enter Education Of Child. \n";
            }
            if (document.getElementById('<%=txtChild_Business.ClientID%>').value.trim() == "") {
                msg += "Enter Business Of Child. \n";
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (confirm("Do you really want to Save Children Details.?")) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }
        function validateNomineeDetail() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=txtNominee_Name.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Nominee Name.\n";
            }
            if (document.getElementById('<%=txtNominee_Relation.ClientID%>').value.trim() == "") {
                msg += "Enter Relation With Nominee. \n";
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (confirm("Do you really want to Save Nominee Details.?")) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }
        function validateOtherDetail() {
            debugger;
            var msg = "";
            if (document.getElementById('<%=txtOther_Training.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Training related Information .\n";
            }
            if (document.getElementById('<%=txtOther_Achievement.ClientID%>').value.trim() == "") {
                msg += "Enter Particular achievement \n";
            }
            if (msg != "") {
                alert(msg);
                return false
            }
            else {
                if (confirm("Do you really want to Save Other Details.?")) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }
        function Validation() {
            if (document.getElementById('<%=ddlEmp_MaritalStatus.ClientID%>').selectedIndex == 1) {
                $(".ValidateMaritalStatus").hide();
            }
            else {
                $(".ValidateMaritalStatus").show();
            }

        }

        $('#txtEmp_JoiningDate').change(function () {
            debugger;
            var start = $('#txtEmp_JoiningDate').datepicker('getDate');
            var end = $('#txtEmp_PostingDate').datepicker('getDate');

            if ($('#txtEmp_PostingDate').val() != "") {
                if (start > end) {

                    if ($('#txtEmp_JoiningDate').val() != "") {
                        alert("Joining date should not be greater than Posting Date.");
                        $('#txtEmp_JoiningDate').val("");
                    }
                }
            }
        });
        $('#txtEmp_PostingDate').change(function () {
            debugger;
            var start = $('#txtEmp_JoiningDate').datepicker('getDate');
            var end = $('#txtEmp_PostingDate').datepicker('getDate');

            if (start > end) {

                if ($('#txtEmp_PostingDate').val() != "") {
                    alert("Posting Date can not be less than Joining Date.");
                    $('#txtEmp_PostingDate').val("");
                }

            }
        });

    </script>
</asp:Content>
