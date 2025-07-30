<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRAttendanceMonthly.aspx.cs" Inherits="mis_HR_HRAttendanceMonthly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .present {
            background: #92d050;
        }

        .absent {
            background: #ff0000;
        }

        .half_day {
            background: #ffc000;
        }

        .late_login {
            background: #8064a2;
        }

        .saturday {
            background: #c5d9f1;
        }

        .sunday {
            background: #c5d9f1;
        }

        .holiday {
            background: #c5d9f1;
        }

        .tour {
            background: #538ed5;
        }

        .attendance_table {
            font-size: 11px;
            font-family: Calibri;
        }

        table td:nth-child(2) {
            font-size: 11px;
            font-weight: 600;
            color: #123456;
        }

        table td, table th {
            padding: 2px;
        }

        table thead td {
            font-size: 11px;
            font-weight: 600;
            background: #12345680;
        }

        .casual_leave {
            background: #ffff00;
        }

        .medical_leave {
            background: #ffff00;
        }

        .earned_leave {
            background: #ffff00;
        }

        .optional_leave {
            background: #ffff00;
        }

        table {
            border-spacing: 1px;
            border-color: #607D8B;
        }

            table td {
                border: 1px solid #607d8b57;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title"><u>Monthly Attendance Report :</u></h3>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Year <span style="color: red;">*</span></label>
                                <asp:dropdownlist id="ddlYear" runat="server" class="form-control" enabled="false" autopostback="true">
                                    <asp:ListItem Value="01">2019</asp:ListItem>
                                </asp:dropdownlist>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Month <span style="color: red;">*</span></label>
                                <asp:dropdownlist id="ddlMonth" runat="server" class="form-control" autopostback="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Select Month</asp:ListItem>
                                    <asp:ListItem Value="01">January</asp:ListItem>
                                    <asp:ListItem Value="02">February</asp:ListItem>
                                    <asp:ListItem Value="03">March</asp:ListItem>
                                    <asp:ListItem Value="04">April</asp:ListItem>
                                    <asp:ListItem Value="05">May</asp:ListItem>
                                    <asp:ListItem selected="true" Value="06">June</asp:ListItem>
                                </asp:dropdownlist>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <%--<div class="col-md-12">
                            <div class="table table-responsive">
                                <asp:GridView ID="GridView1" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="EMPLOYEE NAME" />
                                        <asp:BoundField DataField="Office_Name" HeaderText="OFFICE NAME" />
                                        <asp:BoundField DataField="Emp_MobileNo" HeaderText="MOBILE nUMBER" />
                                        <asp:BoundField DataField="Emp_Email" HeaderText="EMAIL" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>--%>



                        <div ID="divIframe" class="col-md-12" runat="server">
                               <a href="Attendance/JunAttendance.xlsx" class="btn btn-primary btn-flat" style="margin:2px;">Excel</a>
                              <iframe src="Attendance/JunAttendance.pdf" width="100%" height="1000"></iframe>"
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>

