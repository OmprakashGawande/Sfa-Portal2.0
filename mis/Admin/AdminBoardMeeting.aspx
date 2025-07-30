<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminBoardMeeting.aspx.cs" Inherits="mis_Admin_AdminBoardMeeting" %>

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
                            <h3 class="box-title" id="Label2">Board Meeting Detail</h3>
                        </div>

                        <div class="box-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <table border="1" class="table table-bordered table table-responsive table-striped table-bordered table-hover">
                                        <tr>
                                            <th>S.No</th>
                                            <th>Date</th>
                                            <th>agenda</th>
                                            <th>Minutes</th>
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

