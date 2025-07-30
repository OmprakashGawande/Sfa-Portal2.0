<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminLiveTender.aspx.cs" Inherits="mis_Admin_AdminLiveTender" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <!-- general form elements -->

                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label2">Live Tenders</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <table border="1" class="table table-bordered table table-responsive table-striped table-bordered table-hover">
                                        <tr>
                                            <th>DATE</th>
                                            <th>SUBJECT</th>
                                            <th>NIT PUBLICATION DATE</th>
                                            <th>SUBMISSION DATE & TIME</th>
                                            <th>CORRIGENDUM /CLARIFICATION /</th>
                                            <th>TENDER / RCODOCUMENT (FOR VIEWING PURPOSE ONLY. TO PARTICIPATE IN eTENDER PLEASE LOG ONTO www.mpeproc.gov.in)</th>
                                            <th>STATUS</th>
                                        </tr>
                                        <tr>

                                            <td>24.09.2018</td>
                                            <td>ONLINE RATE CONTRACT OFFER DOCUMENT (RCO) FOR SUPPLY OF NURSERY POLY BAGS (USEFUL FOR PLANT PROPAGATION)</td>
                                            <td>22.09.2018</td>
                                            <td>15.10.2018 UPTO 02:00 PM</td>
                                            <td></td>
                                            <td>
                                                <label class="label label-warning">DOWNLOAD</label>
                                            </td>
                                            <td>LIVE</td>
                                        </tr>
                                        <tr>
                                            <td>11.09.2018</td>
                                            <td>ONLINE TENDER  DOCUMENT  FOR SUPPLY OF LAB – EQUIPMENTS WITH INSTALLATION AND TRAINING FOR SOIL TESTING LABS</td>
                                            <td>07.09.2018</td>
                                            <td>NEW DATE OF SUBMISSION IS 25.10.2018 UPTO 03:00 PM (VIEW CORRIGENDUM)
26.09.2018 UPTO 03:00 PM
NEW DATE OF SUBMISSION IS 10.10.2018 UPTO 03:00 PM (VIEW CORRIGENDUM)</td>
                                            <td>CORRIGENDUM FOR EXTENSION OF BID SUBMISSION DATE
TENDER DOCUMENT FEE SHOULD BE SUBMITTED BY DEMAND DRAFT INSTEAD OF ONLINE PAYMENT  CORRIGENDUM-1          CORRIGENDUM-2  
CORRIGENDUM FOR EXTENSION SUBMISSION DATE
CORRIGENDUM -DECISIONS TAKEN AFTER PRIBID MEETING</td>
                                             <td>
                                                <label class="label label-warning">DOWNLOAD</label>
                                            </td>
                                            <td>LIVE</td>
                                        </tr>

                                    </table>
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
</asp:Content>

