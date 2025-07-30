<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Emp_Att_Importdata.aspx.cs" Inherits="mis_HR_Emp_Att_Importdata" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Upload Attendance</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
               
                <div class="box-body">

                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Select Date<span style="color: red;">*</span></label>
                                <div class="input-group date">
                                    <asp:TextBox ID="txtAttDate" runat="server" class="form-control DateAdd" autocomplete="off" data-provide="datepicker" data-date-end-date="0d" data-date-start-date="-17d" onpaste="return false" placeholder="Select Date" ClientIDMode="Static" ></asp:TextBox>
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Upload mdb File <span style="color: red;">*</span></label>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <label style="color: #ff0000; font-weight: 600; margin-top: 4px;">(Allow only mdb format for file upload.)</label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnImport" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Import" OnClientClick="return validateform()" OnClick="btnImport_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                 
                                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover table-striped" AutoGenerateColumns="true" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrownumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=txtAttDate.ClientID%>').value.trim() == "") {
                msg += "Enter Attendance Date \n";
            }
            var fileslength = document.getElementById('<%=FileUpload1.ClientID%>').files.length;
            if (fileslength == 0) {
                msg += "Enter Upload File \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (confirm("Do you really want to Save Details ?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        $("#FileUpload2").change(function () {
            var fileExtension = ['mdb'];
            if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert("Allow only mdb format for file upload.");
                this.value = '';
            }
        });
    </script>
</asp:Content>

