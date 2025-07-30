<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SendMail.aspx.cs" Inherits="mis_Admin_SendMail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/bootstrap3-wysihtml5.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-header">
                    <h3 class="box-title">SEND MAIL</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="box-body">
                                <div class="form-group">
                                    <label>Add more receipents</label>
                                    <asp:TextBox ID="txtEmail" runat="server" placeholder="Use Comma , to separate Email address " class="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Subject<span style="color: red;"> *</span></label>
                                    <asp:TextBox ID="txtSubject" runat="server" placeholder="Enter Subject" class="form-control" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <textarea id="txtMessage" runat="server" class="form-control composetextarea" style="height: 300px"></textarea>
                                </div>
                                <div class="form-group">
                                    <div class="btn btn-default btn-file">
                                        <i class="fa fa-paperclip"></i>Attachment
                                        <asp:FileUpload ID="FU_Attachment" runat="server" name="attachment" onchange="ValidateFileSize(this)" />
                                    </div>
                                    <p class="help-block">Max. 24MB</p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-2">

                            <asp:Button ID="btnSend" runat="server" Text="SEND MAIL" class="btn btn-block btn-primary" OnClientClick="return validateform();" OnClick="btnSend_Click" />

                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnCancel" runat="server" Text="CANCEL" class="btn btn-block btn-primary" OnClientClick="return Cancel();" />

                        </div>
                        <div class="col-md-4">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>LOCATION<span style="color: red;"> *</span></label>
                                <select name="ctl00$ContentBody$ddlLocation" onchange="javascript:setTimeout('__doPostBack(\'ctl00$ContentBody$ddlLocation\',\'\')', 0)" id="ddlLocation" class="form-control">
                                    <option selected="selected" value="0">Select</option>
                                    <option selected="selected" value="0">All</option>
                                    <option value="Head Office">Head Office</option>
                                    <option value="District">Regional Officers</option>
                                    <option value="Institute">District Office</option>
                                    <option value="Institute">Sales Center</option>
                                    <option value="Institute">Production Unit</option>

                                </select>
                            </div>
                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-3">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <table class="table table-bordered table-striped table-hover">
                                    <tbody>
                                        <tr>
                                            <th scope="col">
                                                <input id="ctl00_ContentBody_gvDetails_ctl01_checkAll" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl01$checkAll" onclick="checkAll(this);">
                                            </th>
                                            <th scope="col">DESIGNATION NAME</th>
                                            <th scope="col">NAME</th>
                                            <th scope="col">MOBILE NO.</th>
                                            <th scope="col">Email</th>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl02_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl02$chkSelect">
                                            </td>
                                            <td>Accounts Officer</td>
                                            <td>523502001</td>
                                            <td>9999999999</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl03_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl03$chkSelect">
                                            </td>
                                            <td>Assistant Director</td>
                                            <td>Anupam Agarwal</td>
                                            <td>9425393700</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl04_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl04$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Archana Dwivedi</td>
                                            <td>8349211197</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl05_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl05$chkSelect">
                                            </td>
                                            <td>Dy. Director</td>
                                            <td>Arun Sharma</td>
                                            <td>9407295775</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl06_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl06$chkSelect">
                                            </td>
                                            <td>Assistant Director</td>
                                            <td>Arvind Chaturvedi</td>
                                            <td>9425003231</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl07_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl07$chkSelect">
                                            </td>
                                            <td>Progress Assistant</td>
                                            <td>Ashish</td>
                                            <td>8871313087</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl08_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl08$chkSelect">
                                            </td>
                                            <td>Assistant  Statistical Officer</td>
                                            <td>Ashok Kumar Verma</td>
                                            <td>7554290735</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl09_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl09$chkSelect">
                                            </td>
                                            <td>Add. Dy. Director</td>
                                            <td>Bhagwan Manghnani</td>
                                            <td>9425044657</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl10_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl10$chkSelect">
                                            </td>
                                            <td>Stenotypist</td>
                                            <td>Chandra Golani</td>
                                            <td>9584115124</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl11_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl11$chkSelect">
                                            </td>
                                            <td>Progress Assistant</td>
                                            <td>Deepali Dhoke</td>
                                            <td>7869866796</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl12_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl12$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Deepika Paraste</td>
                                            <td>7879165416</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl13_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl13$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Devram Jamre</td>
                                            <td>7770875355</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl14_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl14$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Dharmendra Singh</td>
                                            <td>9425374426</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl15_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl15$chkSelect">
                                            </td>
                                            <td>Stenographer</td>
                                            <td>Dinesh Kumar Saharey</td>
                                            <td>9826767508</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl16_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl16$chkSelect">
                                            </td>
                                            <td>Assistant Director</td>
                                            <td>Dr Abhilasha </td>
                                            <td>9425981672</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl17_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl17$chkSelect">
                                            </td>
                                            <td>Assistant  Statistical Officer</td>
                                            <td>Dulichand Patel</td>
                                            <td>9826823771</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl18_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl18$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Firoz  Parveen</td>
                                            <td>9302175390</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl19_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl19$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Gaurav  Shrivastava</td>
                                            <td>9425019397</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl20_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl20$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Gomaji Patil</td>
                                            <td>7697292442</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl21_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl21$chkSelect">
                                            </td>
                                            <td>Joint Director</td>
                                            <td>Gulab Singh Dawar</td>
                                            <td>9179114829</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl22_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl22$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Gyanendra Singh</td>
                                            <td>9981787145</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl23_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl23$chkSelect">
                                            </td>
                                            <td>computer Operator</td>
                                            <td>Gyanwati Singh</td>
                                            <td>9424443613</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl24_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl24$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Hira lal  Verma</td>
                                            <td>9039907988</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl25_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl25$chkSelect">
                                            </td>
                                            <td>Stenographer</td>
                                            <td>Jagdish Prasad Chouksey</td>
                                            <td>9981016614</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl26_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl26$chkSelect">
                                            </td>
                                            <td>computer Operator</td>
                                            <td>Javed</td>
                                            <td>9893098930</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl27_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl27$chkSelect">
                                            </td>
                                            <td>Progress Assistant</td>
                                            <td>Jay Kumar Saxena</td>
                                            <td>9425007868</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl28_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl28$chkSelect">
                                            </td>
                                            <td>Progress Assistant</td>
                                            <td>Jaykant Shukla</td>
                                            <td>7898216636</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl29_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl29$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Jeet Singh Negi</td>
                                            <td>9981323192</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl30_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl30$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Jeevan Prasad Mishra</td>
                                            <td>9993167685</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl31_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl31$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Jolly  Jakaria</td>
                                            <td>7898214908</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl32_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl32$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Jolly Jacob</td>
                                            <td>9826485745</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl33_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl33$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Josphina Toppo</td>
                                            <td>8349553326</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl34_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl34$chkSelect">
                                            </td>
                                            <td>Stenographer</td>
                                            <td>Jyoti  Bamaniya</td>
                                            <td>9827219746</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl35_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl35$chkSelect">
                                            </td>
                                            <td>Stenographer</td>
                                            <td>Jyoti Kedare</td>
                                            <td>9926272495</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl36_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl36$chkSelect">
                                            </td>
                                            <td>Suprintendent</td>
                                            <td>Kanta Kale</td>
                                            <td>9752439358</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl37_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl37$chkSelect">
                                            </td>
                                            <td>Assistant Grade -1</td>
                                            <td>Khusal Wasnik</td>
                                            <td>9893903620</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl38_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl38$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Lata Punjabi</td>
                                            <td>8109393122</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl39_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl39$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Leela Kolhe</td>
                                            <td>8989545747</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl40_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl40$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Limbaji Deshmukh</td>
                                            <td>9977826002</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl41_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl41$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Mangi Lal Jamre</td>
                                            <td>9752130653</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl42_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl42$chkSelect">
                                            </td>
                                            <td>Assistant Director</td>
                                            <td>Manish Shing</td>
                                            <td>9425036849</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl43_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl43$chkSelect">
                                            </td>
                                            <td>Suprintendent</td>
                                            <td>Manohar Tandiye</td>
                                            <td>8871065767</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl44_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl44$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Manoj Charpota</td>
                                            <td>9691274633</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl45_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl45$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Mariamma Mathew</td>
                                            <td>9893426029</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl46_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl46$chkSelect">
                                            </td>
                                            <td>Stenotypist</td>
                                            <td>Mohan Lal Arora</td>
                                            <td>9893963747</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl47_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl47$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Mohan Lal Sharma</td>
                                            <td>9425028813</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl48_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl48$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Mohd. Danish</td>
                                            <td>9926404041</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl49_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl49$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Mohd.Shahadat Khan</td>
                                            <td>9669536613</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl50_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl50$chkSelect">
                                            </td>
                                            <td>Assistant Grade -1</td>
                                            <td>Murlidhar Gaud</td>
                                            <td>9425019365</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl51_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl51$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Nandu Mavi</td>
                                            <td>9993507627</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl52_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl52$chkSelect">
                                            </td>
                                            <td>Auditor</td>
                                            <td>Narmada Malviya</td>
                                            <td>9425014381</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl53_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl53$chkSelect">
                                            </td>
                                            <td>Assistant Director</td>
                                            <td>Neelash shah</td>
                                            <td>9425452009</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl54_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl54$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Niranjana Vyas</td>
                                            <td>9425014381</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl55_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl55$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Nitin Dhomne</td>
                                            <td>9425007038</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl56_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl56$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Pankaj  Agarwal</td>
                                            <td>9424476790</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl57_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl57$chkSelect">
                                            </td>
                                            <td>Auditor</td>
                                            <td>Phool  Chand</td>
                                            <td>9827341920</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl58_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl58$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Pradeep Kumar  Nigam</td>
                                            <td>9926453178</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl59_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl59$chkSelect">
                                            </td>
                                            <td>Plant Operator</td>
                                            <td>Prakash Chandra Tiwari</td>
                                            <td>7566599432</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl60_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl60$chkSelect">
                                            </td>
                                            <td>Assistant Grade -1</td>
                                            <td>Prakash Thakare</td>
                                            <td>9981321244</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl61_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl61$chkSelect">
                                            </td>
                                            <td>Assistant Director</td>
                                            <td>Prakhar Bhargava</td>
                                            <td>9407599853</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl62_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl62$chkSelect">
                                            </td>
                                            <td>Assistant Veternary Field Officer</td>
                                            <td>Praveen Murjani</td>
                                            <td>9826632532</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl63_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl63$chkSelect">
                                            </td>
                                            <td>Progress Assistant</td>
                                            <td>Preeti Chaurasiya</td>
                                            <td>9617927135</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl64_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl64$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Preeti Pawar</td>
                                            <td>9827542269</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl65_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl65$chkSelect">
                                            </td>
                                            <td>Assistant Director</td>
                                            <td>Priyakant Pathak</td>
                                            <td>9425382896</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl66_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl66$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Punya dev Sharma</td>
                                            <td>9753338793</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl67_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl67$chkSelect">
                                            </td>
                                            <td>computer Operator</td>
                                            <td>rahul</td>
                                            <td>9893098930</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl68_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl68$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Rajendra Bahadur  Singh</td>
                                            <td>9753268226</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl69_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl69$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Rajendra Kumar  Pal</td>
                                            <td>9993707173</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl70_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl70$chkSelect">
                                            </td>
                                            <td>Director</td>
                                            <td>Rajendra Kumar Rokde</td>
                                            <td>9826445077</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl71_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl71$chkSelect">
                                            </td>
                                            <td>computer Operator</td>
                                            <td>Rajesh</td>
                                            <td>9893098930</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl72_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl72$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Rajni Diwan</td>
                                            <td>9406562654</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl73_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl73$chkSelect">
                                            </td>
                                            <td>computer Operator</td>
                                            <td>rakesh</td>
                                            <td>9893098930</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl74_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl74$chkSelect">
                                            </td>
                                            <td>Assistant  Statistical Officer</td>
                                            <td>Ram Baghel</td>
                                            <td>9754774481</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl75_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl75$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Ram Kishan Pal</td>
                                            <td>9425019364</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl76_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl76$chkSelect">
                                            </td>
                                            <td>computer Operator</td>
                                            <td>Raman Raghuvanshi</td>
                                            <td>9977515683</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl77_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl77$chkSelect">
                                            </td>
                                            <td>Assistant  Statistical Officer</td>
                                            <td>Ramdas Rajput</td>
                                            <td>9977515683</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl78_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl78$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Ramesh Chandra Thakur</td>
                                            <td>9752916999</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl79_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl79$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Rashmi Khalote</td>
                                            <td>9179052915</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl80_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl80$chkSelect">
                                            </td>
                                            <td>Headdrafts Man</td>
                                            <td>Ravindra Kumar Madrey</td>
                                            <td>9406527730</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl81_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl81$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Ravindra Solanki</td>
                                            <td>9617526450</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl82_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl82$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Sachin Sharma</td>
                                            <td>9827203802</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl83_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl83$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Sampat Kumar  Kushwaha</td>
                                            <td>9617335015</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl84_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl84$chkSelect">
                                            </td>
                                            <td>Suprintendent</td>
                                            <td>Santosh</td>
                                            <td>8602732730</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl85_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl85$chkSelect">
                                            </td>
                                            <td>Progress Assistant</td>
                                            <td>Santosh  Kumar</td>
                                            <td>9827361609</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl86_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl86$chkSelect">
                                            </td>
                                            <td>Assistant  Statistical Officer</td>
                                            <td>Satish Kumar Suneria</td>
                                            <td>9926851107</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl87_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl87$chkSelect">
                                            </td>
                                            <td>Assistant Director</td>
                                            <td>Seema Jain</td>
                                            <td>9424443876</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl88_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl88$chkSelect">
                                            </td>
                                            <td>Assistant Director</td>
                                            <td>Seema Rao</td>
                                            <td>9424438594</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl89_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl89$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Shailja Patharkar</td>
                                            <td>9039297333</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl90_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl90$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Shambhu Nath Tiwari</td>
                                            <td>9893761809</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl91_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl91$chkSelect">
                                            </td>
                                            <td>Add. Dy. Director</td>
                                            <td>Shivdutt Shrivastava</td>
                                            <td>9826222663</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl92_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl92$chkSelect">
                                            </td>
                                            <td>Assistant Grade -1</td>
                                            <td>Shobha  Gajbhiye</td>
                                            <td>9893958746</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl93_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl93$chkSelect">
                                            </td>
                                            <td>Suprintendent</td>
                                            <td>Somat Singh Raikwar</td>
                                            <td>9827383672</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl94_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl94$chkSelect">
                                            </td>
                                            <td>Statical Officer</td>
                                            <td>Somnath Mishra</td>
                                            <td>9926966365</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl95_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl95$chkSelect">
                                            </td>
                                            <td>Joint Director (F)</td>
                                            <td>Sujata Raghuwanshi</td>
                                            <td>9424490742</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl96_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl96$chkSelect">
                                            </td>
                                            <td>Progress Assistant</td>
                                            <td>Sukrita Ekka</td>
                                            <td>9893905552</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl97_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl97$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Suman Naakhre</td>
                                            <td>9993943818</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl98_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl98$chkSelect">
                                            </td>
                                            <td>Assistant Grade -1</td>
                                            <td>Sunita  Batham</td>
                                            <td>7747051888</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl99_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl99$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Suraj deen  Verma</td>
                                            <td>7723851199</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl100_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl100$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Surekha  Rao</td>
                                            <td>9425028878</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl101_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl101$chkSelect">
                                            </td>
                                            <td>Assistant  Village  Milk Production Organiser</td>
                                            <td>Surendra Nimm</td>
                                            <td>9770244547</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl102_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl102$chkSelect">
                                            </td>
                                            <td>Assistant  Statistical Officer</td>
                                            <td>Sushma Aarsey</td>
                                            <td>9200116663</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl103_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl103$chkSelect">
                                            </td>
                                            <td>Dy. Director</td>
                                            <td>Sushma Ekka</td>
                                            <td>9425030782</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl104_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl104$chkSelect">
                                            </td>
                                            <td>Dy. Director (Reve)</td>
                                            <td>test </td>
                                            <td>2136547896</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl105_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl105$chkSelect">
                                            </td>
                                            <td>Assistant  Statistical Officer</td>
                                            <td>Udaya Pratap Singh</td>
                                            <td>9425079443</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl106_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl106$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Upma Mishra</td>
                                            <td>8989545787</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl107_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl107$chkSelect">
                                            </td>
                                            <td>Add. Dy. Director</td>
                                            <td>Vani Panday</td>
                                            <td>9425624231</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl108_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl108$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Varsha HImthani</td>
                                            <td>8085612795</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl109_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl109$chkSelect">
                                            </td>
                                            <td>Assistant Director</td>
                                            <td>Vikash Saxena</td>
                                            <td>9407517897</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl110_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl110$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Vinay Kumar Mehra</td>
                                            <td>9993180771</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl111_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl111$chkSelect">
                                            </td>
                                            <td>Assistant Grade -1</td>
                                            <td>Vinod Shankar Dwivedi</td>
                                            <td>8349211197</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl112_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl112$chkSelect">
                                            </td>
                                            <td>Assistant Grade -3</td>
                                            <td>Virendra Singh Thakur</td>
                                            <td>9755990646</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 30px;">
                                                <input id="ctl00_ContentBody_gvDetails_ctl113_chkSelect" type="checkbox" name="ctl00$ContentBody$gvDetails$ctl113$chkSelect">
                                            </td>
                                            <td>Assistant Grade -2</td>
                                            <td>Vishnu Khubchandani</td>
                                            <td>9755054951</td>
                                            <td>demo@gmail.com</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <%--<div class="box-body">
                        <div class="row">

                            <div class="col-md=8">
                                <div class="form-group">
                                    <asp:GridView ID="gvDetails" DataKeyNames="EmailID" AutoGenerateColumns="false" runat="server" class="table table-hover table-bordered pagination-ys" >
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="30">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Bank Name" DataField="BankName" />
                                            <asp:BoundField HeaderText="Contact Person" DataField="ContactPerson" />
                                            <asp:BoundField HeaderText="Email Address" DataField="EmailAddress" />
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                            <div class="col-md-4"></div>
                        </div>
                    </div>--%>
                </div>
            </div>
        </section>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../js/bootstrap3-wysihtml5.all.min.js"></script>
    <script>
        $(function () {
            $(".composetextarea").wysihtml5();
        });



        function ValidateFileSize(a) {

            var uploadcontrol = document.getElementById(a.id);
            if (uploadcontrol.files[0].size > 1024 * 1024 * 24) {
                alert('File size should not greater than 24 mb.');
                document.getElementById(a.id).value = '';
                return false;
            }
            else {
                return true;
            }

        }
    </script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function validateform() {
            var msg = "";
            <%--if (document.getElementById('<%=txtEmail.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Email. \n";
            }--%>
            var txtemail = document.getElementById('<%=txtEmail.ClientID%>').value.trim();
            if (txtemail !== "") {
                var emails = txtemail.split(',');
                var index = 0;
                var k = emails.length;
                if (k != 1) {
                    for (i = 0; i < k; i++) {

                        var regMail = /^([_a-zA-Z0-9-]+)(\.[_a-zA-Z0-9-]+)*@([a-zA-Z0-9-]+\.)+([a-zA-Z]{2,3})$/;
                        if (regMail.test(emails[i]) == false && emails[i] != "") {
                            if (index == 0) {
                                msg += "invalid Email : " + emails[i];
                                index = 1;
                            }
                            else
                                msg += "," + emails[i];
                        }

                    }
                    msg += "\n";
                }
                else {
                    var regMail = /^([_a-zA-Z0-9-]+)(\.[_a-zA-Z0-9-]+)*@([a-zA-Z0-9-]+\.)+([a-zA-Z]{2,3})$/;
                    if (regMail.test(txtemail) == false) {
                        msg += "invalid Email : " + txtemail;
                    }
                    msg += "\n";
                }
            }
            if (document.getElementById('<%=txtSubject.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Subject. \n";
            }
            if (document.getElementById('<%=txtMessage.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Message. \n";
            }

            if (msg.trim() != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Send Email ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        <%-- function CheckLength(evt) {
            if (document.getElementById('<%=txtsms.ClientID%>').value.length <= 140) {
                document.getElementById('smssize').innerHTML = 140 - document.getElementById('<%=txtsms.ClientID%>').value.length;
                return true;
            }
            else {
                if (evt.keyCode == 8) {
                    document.getElementById('smssize').innerHTML = "1";
                    return true;
                }
                else {
                    return false;
                }
            }

        }--%>
    </script>
</asp:Content>

