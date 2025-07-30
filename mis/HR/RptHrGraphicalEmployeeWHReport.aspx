<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptHrGraphicalEmployeeWHReport.aspx.cs" Inherits="mis_HR_RptHrGraphicalEmployeeWHReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/data.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
    <script src="https://code.highcharts.com/modules/export-data.js"></script>
    <style>
        .highcharts-button-symbol {
            display: none;
        }

        .highcharts-credits {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>From Date<span style="color: red;">*</span></label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtFromDate" runat="server" placeholder="Select From Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>To Date<span style="color: red;">*</span></label>
                                    <div class="input-group date">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtToDate" runat="server" placeholder="Select To Date.." class="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success btn-block" OnClick="btnSearch_Click" OnClientClick="return validateform();" Style="margin-top: 25px;" />
                                </div>
                            </div>
                        </div>

                    </div>

                    <div id="container" style="min-width: 310px; max-width: 800px; height: 400px; margin: 0 auto"></div>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" class="table table-hover table-bordered" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found" OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Time Interval." ItemStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkingHour" Text='<%# Eval("WorkingHour").ToString() %>' runat="server" />
                                            <asp:Label ID="lblStartHour" CssClass="hidden" Text='<%# Eval("StartHour").ToString() %>' runat="server" />
                                            <asp:Label ID="lblEndHour" CssClass="hidden" Text='<%# Eval("EndHour").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No Of Employees">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotalEmployees" Text='<%# Eval("TEmployee").ToString() %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="13%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="hpView" runat="server" CssClass="label label-info" CommandName="View" CommandArgument='<%# Container.DataItemIndex %>' Text="View"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <div class="modal fade" id="ModalEmployeeDetail" role="dialog">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Employee Working Hours Details</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView runat="server" CssClass="table table-bordered" ShowHeaderWhenEmpty="true" ID="GVEmployee" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No." ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:BoundField DataField="SelectDate" HeaderText="Date" />--%>
                                        <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
<%--                                        <asp:BoundField DataField="LoginTime" HeaderText="LoginTime" />
                                        <asp:BoundField DataField="LogoutTime" HeaderText="LogoutTime" ItemStyle-HorizontalAlign="Right" />--%>
                                        <asp:BoundField DataField="WorkingHours" HeaderText="WorkingHours" />

                                    </Columns>
                                </asp:GridView>
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
    <div id="divchart" class="hidden" runat="server"></div>
    <asp:HiddenField ID="hfyear" runat="server" />
    <asp:HiddenField ID="hfoffice" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfDate" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">


    <script>
        function ModalView() {
            $('.modal-title').text('Employees Average Working Hours From ' + txtFromDate + ' To ' + txtToDate + '');
            $('#ModalEmployeeDetail').modal('show');
        }
        var hfoffice = '<%= hfoffice.Value%>'
        var hfDate = '<%= hfDate.Value%>'

        var txtFromDate = document.getElementById('<%=txtFromDate.ClientID%>').value.trim();
        var txtToDate = document.getElementById('<%=txtToDate.ClientID%>').value.trim();
        Highcharts.chart('container', {
            data: {
                table: 'datatable'
            },
            chart: {
                type: 'column'
            },
            title: {
                text: 'Graphical Representation of Employees Average Working Hours From ' + txtFromDate + ' To ' + txtToDate + ''
            },

            yAxis: {
                min: 0,
                tickInterval: 2,
                allowDecimals: true,
                title: {
                    text: 'No Of Employees'
                }
            },

            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.name + '</b> - ' + this.point.y + '<br/>' +
                      this.point.name.toLowerCase();
                }
            }
        });



        $('#txtFromDate').datepicker({
            autoclose: true
        });
        $('#txtToDate').datepicker({
            autoclose: true
        });
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=txtFromDate.ClientID%>').value.trim() == "") {
                msg += "Select Start Date. \n";
            }
            if (document.getElementById('<%=txtToDate.ClientID%>').value.trim() == "") {
                msg += "Select End Date. \n";
            }

            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }
    </script>
    <script>
        $('#txtFromDate').change(function () {
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');

            if ($('#txtToDate').val() != "") {
                if (start > end) {

                    if ($('#txtFromDate').val() != "") {
                        alert("From date should not be greater than To Date.");
                        $('#txtFromDate').val("");
                    }
                }
            }
        });
        $('#txtToDate').change(function () {
            var start = $('#txtFromDate').datepicker('getDate');
            var end = $('#txtToDate').datepicker('getDate');

            if (start > end) {

                if ($('#txtToDate').val() != "") {
                    alert("To Date can not be less than From Date.");
                    $('#txtToDate').val("");
                }
            }
        });
    </script>
</asp:Content>



