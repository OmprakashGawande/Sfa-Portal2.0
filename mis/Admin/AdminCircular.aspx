<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="AdminCircular.aspx.cs" Inherits="mis_Admin_AdminSurvoler" %>

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
                            <h3 class="box-title" id="Label2">Circular Detail</h3>
                        </div>


                        <div class="box-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <table border="1" class="table table-bordered table table-responsive table-striped table-bordered table-hover">
                                        <tr>
                                            <th>S. No</th>
                                            <th>Subject</th>
                                            <th>View circular</th>
                                        </tr>
                                        <tr>
                                            <td>1</td>
                                            <td>जीएसटी टी डी एस दो प्रतिशत कटोती वावत</td>
                                            <td><a href="Upload/IMG-20181018-WA0021.jpg" class="label label-default">View circular</a></td>
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

