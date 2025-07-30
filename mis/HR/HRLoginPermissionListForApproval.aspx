<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HRLoginPermissionListForApproval.aspx.cs" Inherits="HRLoginPermissionListForApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <%--<section class="content-header">
            <div class="row">
                <!-- left column -->
                <div class="col-md-10 col-md-offset-1">
                    
                </div>
            </div>
        </section>--%>

        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12 ">
                    <!-- general form elements -->

                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Login-Logout Intimation List For Approval</h3>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body ">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>FROM DATE<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="DD/MM/YYYY" class="form-control DateAdd" data-provide="datepicker" data-date-end-date="0d" ID="txtStartDate" ClientIDMode="Static" onkeypress="return validateNum(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>TO DATE<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" autocomplete="off" placeholder="DD/MM/YYYY" class="form-control DateAdd" data-provide="datepicker" data-date-end-date="0d" ID="txtEndDate" ClientIDMode="Static" onkeypress="return validateNum(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" runat="server" class="btn btn-block btn-success" Style="margin-top: 24px;" Text="Search" OnClientClick="return validateform();" OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box-body">
                                        <asp:Label ID="lblMsg" runat="server" ClientIDMode="Static" Text=""></asp:Label>
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView1" DataKeyNames="Per_ID" runat="server" class="datatable table table-hover table-bordered"
                                                AutoGenerateColumns="false" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowHeaderWhenEmpty="True" EmptyDataText="No Record Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" Text='<%# Eval("Status").ToString()%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="InsertedOn" HeaderText="Date" />
                                                    <asp:BoundField DataField="Login_LogoutTime" HeaderText="Intimation For - O'Clock" />
                                                    <%-- <asp:BoundField DataField="Login_LogoutTime" HeaderText="O'Clock" />
                                                 <asp:BoundField DataField="DiffTime" HeaderText="Difference(In min)" />--%>
                                                    <asp:BoundField DataField="Emp_Remark" HeaderText="Reason" />
                                                    <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSelect" runat="server" CssClass="label label-info" CausesValidation="False" CommandName="Select" Text="View"></asp:LinkButton>
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
                    <!-- /.box -->

                </div>

            </div>
            <!-- /.row -->
            <div id="myModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Login-Logout Permission - Detail</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered table-striped table-hover" AutoGenerateRows="false">
                                                <Fields>
                                                    <asp:BoundField DataField="Emp_Name" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="PermissionType" HeaderText="Permission Type" />
                                                    <asp:BoundField DataField="InsertedOn" HeaderText="Applied Date" />
                                                    <asp:BoundField DataField="Login_LogoutTime" HeaderText="Intimation For - O'Clock" />
                                                    <asp:BoundField DataField="Emp_Remark" HeaderText="Reason" />                                                   
                                                </Fields>
                                            </asp:DetailsView> 
                                        </div>
                                    </div>
                                </div>
                                <fieldset>
                                    <legend>HR Reply Section</legend>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label>Status<span style="color: red;">*</span></label>
                                                <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                                    <asp:ListItem>Select</asp:ListItem>
                                                    <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                                    <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label>Remark</label>
                                                <asp:TextBox ID="txtHRRemark" runat="server" TextMode="MultiLine" Rows="3" placeholder="Enter Remark..." class="form-control" ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>                                    
                                </fieldset>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnApprove" class="btn btn-success" runat="server" Text="Action" OnClick="btnApprove_Click" OnClientClick="return ValidateApprovalform();"/>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            </div>
                            
                        </div>
                    </div>
                </div>
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
   <link href="css/dataTables.bootstrap.min.css" rel="stylesheet" />
   <link href="css/buttons.dataTables.min.css" rel="stylesheet" />
   <link href="css/jquery.dataTables.min.css" rel="stylesheet" />
   <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/dataTables.bootstrap.min.js"></script>
    <script src="js/dataTables.buttons.min.js"></script>
    <script src="js/buttons.flash.min.js"></script>
    <script src="js/jszip.min.js"></script>
    <script src="js/pdfmake.min.js"></script>
    <script src="js/vfs_fonts.js"></script>
    <script src="js/buttons.html5.min.js"></script>
    <script src="js/buttons.print.min.js"></script>
    <script src="js/buttons.colVis.min.js"></script>
    <script>
        $('.datatable').DataTable({
            paging: true,
            pageLength: 100,
            columnDefs: [{
                targets: 'no-sort',
                orderable: false
            }],
            dom: '<"row"<"col-sm-6"Bl><"col-sm-6"f>>' +
              '<"row"<"col-sm-12"<"table-responsive"tr>>>' +
              '<"row"<"col-sm-5"i><"col-sm-7"p>>',
            fixedHeader: {
                header: true
            },
            buttons: {
                buttons: [{
                    extend: 'print',
                    text: '<i class="fa fa-print"></i> Print',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4,5]
                    },
                    footer: true,
                    autoPrint: true
                }, {
                    extend: 'excel',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    title: $('h1').text(),
                    exportOptions: {
                        columns: [0, 1, 2, 3, 4,5]
                    },
                    footer: true
                }],
                dom: {
                    container: {
                        className: 'dt-buttons'
                    },
                    button: {
                        className: 'btn btn-default'
                    }
                }
            }
        });

        function callalert() {
            $("#myModal").modal('show');
        }
        $('#txtStartDate').datepicker({
            autoclose: true
        });
        $('#txtEndDate').datepicker({
            autoclose: true
        });

        function validateform() {
            debugger
            var msg = "";
            var fromdate = document.getElementById('<%=txtStartDate.ClientID%>').value.trim();
            var Todate = document.getElementById('<%=txtEndDate.ClientID%>').value.trim();
            if (document.getElementById('<%=txtStartDate.ClientID%>').value.trim() == "") {
                msg += "Select From Date. \n";
            }
            if (document.getElementById('<%=txtEndDate.ClientID%>').value.trim() == "") {
                msg += "Select To Date. \n";
            }
            if (fromdate != "" && Todate != "") {
                var Fdate = new Date(fromdate);
                var Tdate = new Date(Todate);

                if (Fdate > Tdate) {
                    alert("Invalid Date Range!\nStart Date cannot be after End Date!")
                    document.getElementById("lblMsg").innerHTML = "";
                }
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }
        function ValidateApprovalform() {
            debugger
            var msg = "";
           
            if (document.getElementById('<%=ddlStatus.ClientID%>').selectedIndex == 0) {
                msg += "Select Status. \n";
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
</asp:Content>

