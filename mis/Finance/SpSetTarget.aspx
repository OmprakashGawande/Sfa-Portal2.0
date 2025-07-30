<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="SpSetTarget.aspx.cs" Inherits="mis_Finance_SpSetTarget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        th {
            background-color: #1ca79a;
            border-color: #1ca79a;
            color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content-header">
            <h1>SET TARGET</h1>
        </section>
        <!-- Main content -->
        <section class="content">

            <!-- Default box -->
            <div class="box box-success">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Financial Year</label>
                                <select name="ctl00$ContentBody$ddlFinancialYear" id="ctl00_ContentBody_ddlFinancialYear" class="form-control">
                                    <option value="">Select</option>
                                    <option value="">2017-18</option>
                                    <option value="">2018-19</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-6"></div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Total Target Forwarded (in Rs)</label>
                                <asp:TextBox runat="server" CssClass="form-control" ReadOnly="true" Text="45,458,200"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">

                                <div>
                                    <table class="table table-hover table-bordered table-responsive table-striped">
                                        <tbody>
                                            <tr>
                                                <th scope="col">SNo.</th>
                                                <th scope="col">Branch Office</th>
                                                <th scope="col">Financial Target (In Rs.)</th>
                                                <th scope="col">View/Set Target</th>
                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl02_lblRowNumber">1</span>
                                                </td>
                                                <td style="width: 8%;">Agarmalwa</td>

                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl03_lblRowNumber">2</span>
                                                </td>
                                                <td style="width: 8%;">Alirajpur</td>

                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl04_lblRowNumber">3</span>
                                                </td>
                                                <td style="width: 8%;">Anuppur</td>

                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl05_lblRowNumber">4</span>
                                                </td>
                                                <td style="width: 8%;">Ashoknagar</td>

                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl06_lblRowNumber">5</span>
                                                </td>
                                                <td style="width: 8%;">Balaghat</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl07_lblRowNumber">6</span>
                                                </td>
                                                <td style="width: 8%;">Barwani</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl08_lblRowNumber">7</span>
                                                </td>
                                                <td style="width: 8%;">Betul</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl09_lblRowNumber">8</span>
                                                </td>
                                                <td style="width: 8%;">Bhind</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl10_lblRowNumber">9</span>
                                                </td>
                                                <td style="width: 8%;">Bhopal</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl11_lblRowNumber">10</span>
                                                </td>
                                                <td style="width: 8%;">Burhanpur</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl12_lblRowNumber">11</span>
                                                </td>
                                                <td style="width: 8%;">Chhatarpur</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl13_lblRowNumber">12</span>
                                                </td>
                                                <td style="width: 8%;">Chhindwara</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl14_lblRowNumber">13</span>
                                                </td>
                                                <td style="width: 8%;">Damoh</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl15_lblRowNumber">14</span>
                                                </td>
                                                <td style="width: 8%;">Datia</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl16_lblRowNumber">15</span>
                                                </td>
                                                <td style="width: 8%;">Dewas</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl17_lblRowNumber">16</span>
                                                </td>
                                                <td style="width: 8%;">Dhar</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl18_lblRowNumber">17</span>
                                                </td>
                                                <td style="width: 8%;">Dindori</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl19_lblRowNumber">18</span>
                                                </td>
                                                <td style="width: 8%;">Guna</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl20_lblRowNumber">19</span>
                                                </td>
                                                <td style="width: 8%;">Gwalior</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl21_lblRowNumber">20</span>
                                                </td>
                                                <td style="width: 8%;">Harda</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl22_lblRowNumber">21</span>
                                                </td>
                                                <td style="width: 8%;">Hoshangabad</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl23_lblRowNumber">22</span>
                                                </td>
                                                <td style="width: 8%;">Indore</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl24_lblRowNumber">23</span>
                                                </td>
                                                <td style="width: 8%;">Jabalpur</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl25_lblRowNumber">24</span>
                                                </td>
                                                <td style="width: 8%;">Jhabua</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl26_lblRowNumber">25</span>
                                                </td>
                                                <td style="width: 8%;">Katni</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl27_lblRowNumber">26</span>
                                                </td>
                                                <td style="width: 8%;">Khandwa</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl28_lblRowNumber">27</span>
                                                </td>
                                                <td style="width: 8%;">Khargone</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl29_lblRowNumber">28</span>
                                                </td>
                                                <td style="width: 8%;">Mandla</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl30_lblRowNumber">29</span>
                                                </td>
                                                <td style="width: 8%;">Mandsaur</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl31_lblRowNumber">30</span>
                                                </td>
                                                <td style="width: 8%;">Morena</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl32_lblRowNumber">31</span>
                                                </td>
                                                <td style="width: 8%;">Narsinghpur</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl33_lblRowNumber">32</span>
                                                </td>
                                                <td style="width: 8%;">Neemuch</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>
                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl34_lblRowNumber">33</span>
                                                </td>
                                                <td style="width: 8%;">Panna</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl35_lblRowNumber">34</span>
                                                </td>
                                                <td style="width: 8%;">Raisen</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl36_lblRowNumber">35</span>
                                                </td>
                                                <td style="width: 8%;">Rajgarh</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl37_lblRowNumber">36</span>
                                                </td>
                                                <td style="width: 8%;">Ratlam</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl38_lblRowNumber">37</span>
                                                </td>
                                                <td style="width: 8%;">Rewa</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl39_lblRowNumber">38</span>
                                                </td>
                                                <td style="width: 8%;">Sagar</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl40_lblRowNumber">39</span>
                                                </td>
                                                <td style="width: 8%;">Satna</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl41_lblRowNumber">40</span>
                                                </td>
                                                <td style="width: 8%;">Sehore</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl42_lblRowNumber">41</span>
                                                </td>
                                                <td style="width: 8%;">Seoni</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl43_lblRowNumber">42</span>
                                                </td>
                                                <td style="width: 8%;">Shahdol</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl44_lblRowNumber">43</span>
                                                </td>
                                                <td style="width: 8%;">Shajapur</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl45_lblRowNumber">44</span>
                                                </td>
                                                <td style="width: 8%;">Sheopur</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl46_lblRowNumber">45</span>
                                                </td>
                                                <td style="width: 8%;">Shivpuri</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl47_lblRowNumber">46</span>
                                                </td>
                                                <td style="width: 8%;">Sidhi</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl48_lblRowNumber">47</span>
                                                </td>
                                                <td style="width: 8%;">Singrauli</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>


                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl49_lblRowNumber">48</span>
                                                </td>
                                                <td style="width: 8%;">Tikamgarh</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl50_lblRowNumber">49</span>
                                                </td>
                                                <td style="width: 8%;">Ujjain</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl51_lblRowNumber">50</span>
                                                </td>
                                                <td style="width: 8%;">Umaria</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                            <tr>
                                                <td style="width: 4%;">
                                                    <span id="ctl00_ContentBody_GridView1_ctl52_lblRowNumber">51</span>
                                                </td>
                                                <td style="width: 8%;">Vidisha</td>
                                                <td style="width: 8%;">375360</td>
                                                <td style="width: 8%;"><a data-toggle="modal" class="label label-info" data-target="#setTarget">View/Set Target</a></td>

                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="modal" id="setTarget">
                        <div class="modal-dialog" style="width: 80%">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span></button>
                                    <h4 class="modal-title">Set Target for Bhopal Branch<span id="ctl00_ContentBody_spnDistrictName" style="text-transform: uppercase;"></span></h4>

                                </div>

                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-3">

                                            <label>Item Group</label>
                                            <asp:DropDownList runat="server" CssClass="form-control">
                                                <asp:ListItem>Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Item Category</label>
                                            <asp:DropDownList runat="server" CssClass="form-control">
                                                <asp:ListItem>Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Item Name</label>
                                            <asp:DropDownList runat="server" CssClass="form-control">
                                                <asp:ListItem>Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3">
                                            <label>Amount</label>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2 pull-right">

                                            <asp:Button ID="btnSave" runat="server" Text="Add Detail" CssClass="btn btn-info btn-block" />
                                        </div>
                                    </div>

                                    <div class="row" style="padding-top: 5PX;">
                                        <div class="col-md-12">
                                            <div class="table-responsive">


                                                <table class="table table-hover table-bordered table-responsive table-striped">
                                                    <tbody>
                                                        <tr>
                                                            <th scope="col">SNo.</th>
                                                            <th scope="col">Item Group</th>
                                                            <th scope="col">Item Category</th>
                                                            <th scope="col">Item Name</th>
                                                            <th scope="col">Amount</th>
                                                        </tr>
                                                        <tr>
                                                            <td>1</td>
                                                            <td>Wheat</td>
                                                            <td>Wheat</td>
                                                            <td>Wheat</td>
                                                            <td>120210</td>
                                                        </tr>
                                                        <tr>
                                                            <td>2</td>
                                                            <td>Calcium</td>
                                                            <td>Calcium</td>
                                                            <td>Calcium</td>
                                                            <td>120210</td>
                                                        </tr>

                                                        <tr>
                                                            <td>3</td>
                                                            <td>Oil</td>
                                                            <td>Oil</td>
                                                            <td>Oil</td>
                                                            <td>120210</td>
                                                        </tr>
                                                        <tr>
                                                            <td>4</td>
                                                            <td>Tractor</td>
                                                            <td>Machine</td>
                                                            <td>Tractor</td>
                                                            <td>150000</td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">

                                        <div class="col-md-3 pull-right">
                                            <label>Total Amount : 375360</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <input type="submit" value="Save Target" class="btn btn-success" />
                                    <button type="button" class="btn btn-default" data-dismiss="modal">Close </button>
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>
                </div>

            </div>
            <!-- /.box -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

