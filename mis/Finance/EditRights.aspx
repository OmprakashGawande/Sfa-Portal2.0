<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="EditRights.aspx.cs" Inherits="mis_Finance_EditRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('images/progress.gif') 50% 50% no-repeat rgb(249,249,249);
        }

        .table td{
             padding: 2px 2px !important;
        }
        .form-control1 {
            display: block;
            width: 100%;
            height: 27px;
            padding: 2px 3px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="loader"></div>
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success" style="min-height: 500px;">
                <div class="box-header">
                    <h3 class="box-title print_hidden">Edit Rights</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" CssClass="print_hidden" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row print_hidden">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Year<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control" AutoPostBack="false"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group"></div>

                    <div class="row">
                        <div class="col-md-8">
                            <asp:GridView ID="GridView1" runat="server" class="table table-bordered table-striped" ClientIDMode="Static" AutoGenerateColumns="False" AllowPaging="false">
                                <Columns>

                                    <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5">
                                        <ItemTemplate>
                                            <div style="">
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' ToolTip='<%# Eval("Office_ID").ToString()%>' runat="server" />
                                                <asp:Label ID="lblGenerateStatus" Text='<%# Eval("AuditStatus").ToString()%>' runat="server" Visible="false" />
                                                <asp:Label ID="lblOffice_ID" Text='<%# Eval("Office_ID").ToString()%>' runat="server" Visible="false" />
                                            </div>
                                            <%-- <asp:CheckBox ID="chkSelect1" runat="server" CssClass="hidden" Checked='<%# Eval("Checked").ToString() == "true" ? true  :  false %>' Enabled='<%# Eval("AuditStatus").ToString() == "No" ? true : false %>' />--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Office_Name" HeaderText="Office Name" />
                                    <asp:TemplateField HeaderText="Edit Days" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="120">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtValidDays" runat="server" class="form-control1" Text='<%#Bind("ValidDays") %>' Enabled='<%# Eval("AuditStatus").ToString() == "No" ? true : false %>' onkeypress="return validateNum(event)"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-CssClass="chk1" ItemStyle-Width="70" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="checkAll" runat="server" ClientIDMode="static" />
                                            Audit
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Enabled='<%# Eval("AuditStatus").ToString() == "No" ? true : false %>' Checked='<%# Eval("AuditStatus").ToString() == "No" ? false  : true  %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="form-group"></div>
                    <div class="row print_hidden">
                        <div class="col-md-6"></div>
                        <div class="col-md-2">
                            <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" runat="server" Text="Save" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        $(document).ready(function () {
    $('.loader').fadeOut();
});


     $('#checkAll').click(function () {
         var inputList = document.querySelectorAll('#GridView1 tbody input[type="checkbox"]:not(:disabled)');
         for (var i = 0; i < inputList.length; i++) {
             if (document.getElementById('checkAll').checked) {
                 inputList[i].checked = true;
             }
             else {
                 inputList[i].checked = false;
             }
         }
     });



     $(document).ready(function () {
         //Loop through each checkbox in gridview
         //Change the GridView id here
         $("#<%=GridView1.ClientID %> input:checkbox").each(function () {
                this.onclick = function () {
                    if (this.checked)
                        this.parentNode.style.backgroundColor = 'white';
                    else
                        document.getElementById('checkAll').checked = false;
                    //this.parentNode.style.backgroundColor = 'green';


                }
            })

            var checkbox = $('table tbody input[type="checkbox"]:checked(:checked)');
            //for (var i = 0; i < checkbox.length; i++) {
            //    $(checkbox[i]).parents('tr').css({ 'background-color': 'rgb(255, 157, 157)', 'color': '#000' });
            //}

        });




        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlYear.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Financial Year. \n";
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



