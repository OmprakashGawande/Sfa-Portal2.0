<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpProjectMapping.aspx.cs" Inherits="mis_HR_HREmpProjectMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- general form elements -->
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">EMPLOYEE PROJECT MAPPING</h3>
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                        <!-- /.box-header -->
                        <!-- form start -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Employee Name<span style="color: red;"> </span></label>
                                        <asp:DropDownList ID="ddlEmp_Name" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlEmp_Name_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div id="divProject" runat="server">
                                <div class="row">
                                <div class="col-md-12">
                                    <label>Projects</label><br />
                                    <div class="table-responsive">
                                        <asp:CheckBoxList runat="server" CssClass="table customCSS" RepeatColumns="4" RepeatDirection="Vertical" ID="chkProject" RepeatLayout="Table" ClientIDMode="Static" >
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" Text="Map" ID="btnMap" OnClick="btnMap_Click" OnClientClick="return validateform(); "/> 
                                    </div>
                                </div>
                                <!-- /.box -->
                            </div>
                            </div>
                        </div>
                        <!-- /.box-body -->
                    </div>
                    <!-- /.box -->
                </div>
            </div>
            <!-- /.row -->
        </section>
        <!-- /.content -->
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
    <script type="text/javascript">
        function CheckForm() {  // for check atleast one check box is checked.

            debugger
            var count = 0;
            var element = document.getElementById("chkProject").getElementsByTagName("tr");
            for (var i = 0; i < element.length; i++) {
                var noofcolumn = document.getElementById("chkProject").rows[i].cells;
                for (var j = 0; j < noofcolumn.length; j++) {
                    if (noofcolumn[j].checked) {
                        count++;
                    }
                    else { }
                }
            }
            if (count > 0) {
                return true;
            }
            else {
                alert("Select Atleast One Project.");
                return false;
            }
        }


        function validateform() {
            debugger
            var msg = "";
            if (document.getElementById('<%=ddlEmp_Name.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
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

