<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpListOffice.aspx.cs" Inherits="mis_HR_HREmpListOffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .users {
            text-align: center;
            align-content: center;
            border: 1px solid #9c91917a;
            min-height:173px !important;
            padding:2px;
        }

            .users > img {
                border-radius: 50%;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <div class="row">
                                <div class="col-md-6 col-sm-12 ">
                                   <%-- <h3 class="box-title">Employee(<asp:Label ID="lblNoOfEmployee" runat="server"></asp:Label>)</h3>--%>
                                     <h3 class="box-title">Employee<asp:Label ID="lblNoOfEmployee" CssClass="hidden" runat="server"></asp:Label></h3>
                                </div>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
                            <div class="row">
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-2 col-sm-6 ">
                                            <div class="form-group">
                                                <div class="users" >
                                                    <img src='<%# Eval("Emp_ProfileImage").ToString()%>' alt="User Image" style="height: 75px; width: 75px;" />
                                                    <a class="users-list-name" href='<%# String.Format("HREmpDetailView.aspx?Emp_ID="+ APIProcedure.Client_Encrypt(Eval("Emp_ID").ToString())) %>'><%# Eval("Emp_Name").ToString()%></a>
                                                    <span class="users-list-date" style="color: CadetBlue;"><%# Eval("Designation_Name").ToString()%></span>
                                                    <span class="users-list-date" style="color: CornflowerBlue;">[<%# Eval("Department_Name").ToString()%>]</span>
                                                  
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>


                            </div>
                            <div class="row">
                                <div class="col-md-12">

                                    <div id="divFillList" runat="server"></div>

                                </div>
                            </div>
                            <!-- /.users-list -->
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer text-center">
                            <%-- <a href="javascript:void(0)" class="uppercase">View All Employee</a>--%>
                            <asp:LinkButton ID="lnkViewAllEmployee" runat="server" OnClick="lnkViewAllEmployee_Click" Text="View All Employee"></asp:LinkButton>
                        </div>
                        <!-- /.box-footer -->
                    </div>
                </div>
                <!-- /.box -->
            </div>

            <!-- /.row -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

