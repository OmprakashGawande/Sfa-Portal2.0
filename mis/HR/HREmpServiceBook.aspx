<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="HREmpServiceBook.aspx.cs" Inherits="mis_HR_HREmpServiceBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/StyleSheet.css" rel="stylesheet" />
    <style>
        .box {
            position: relative;
            border-radius: 3px;
            background: #ffffff;
            border-top: 3px solid #d2d6de;
            margin-bottom: 20px;
            width: 100%;
            box-shadow: 0 1px 1px rgba(0,0,0,0.1);
            box-shadow: none;



            border-top: none;
        }

        .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
            border: 1px solid #e1e1e1;
        }

        .text-center h3 {
            font-size: 20px;
        }

        .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
            padding: 1px 1px;
        }

        #subheading-salary {
            font-size: 16px;
        }

        .salary-logo {
            -webkit-filter: grayscale(100%);
            filter: grayscale(100%);
            width: 40px;
        }

        .printbutton {
            border-top: 1px dashed #838383;
            margin-top: 5px;
            padding-top: 5px;
        }

        table h4 {
            font-size: 15px;
        }

        .table {
            margin-bottom: 5px;
        }

        th, td, h3 {
            text-transform: uppercase !important;
        }
        /*.content-wrapper{
            min-height:300px !important;
        }*/


        @media print {
            body * {
                visibility: hidden;
            }

            .printbutton {
                display: none;
            }

            .text-center h3 {
                font-size: 13px;
                margin: 0px;
                padding: 0px;
            }

            .section-to-print, .section-to-print * {
                visibility: visible;
                font-size: 10px;
                margin: 0px !important;
            }

            .subheading-salary {
                font-size: 12px !important;
            }

            .salary-logo {
                width: 20px;
            }

            .box-header {
                padding: 2px;
                margin-top: -10px;
            }

            .section-to-print {
                margin-top: -20px;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success" style="min-height: 0px;">
                <div class="box-header">
                    <h3 class="box-title">Add Service Book</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>कार्यालय / Office <span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlOfficeName" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlOfficeName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>कर्मचारी का नाम / Employee Name<span style="color: red;">*</span></label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" class="form-control select2"  AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" >
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>S No.<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtSNo" runat="server" placeholder="S No..." CssClass="form-control" MaxLength="50"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Upload Service Book<span style="color: red;">*</span></label>
                                <%--  <asp:FileUpload ID="FU_ServiceBook1" runat="server" class="form-control" />--%>

                                <%--<asp:FileUpload ID="FU_ServiceBook" CssClass="form-control" runat="server" ClientIDMode="Static" Onchange="uploadDoc(),ValidateFileSize(this,1024*1024)" />--%>
                                <asp:FileUpload ID="FU_ServiceBook" runat="server" class="form-control" ClientIDMode="Static" onchange="UploadControlValidationForLenthAndFileFormat(100, 'JPEG*PNG*JPG', this),ValidateFileSize(this,1024*1024*1),previewProfileImage()" />
                                <small><span id="valfuMeeting_Doc" class="text-info">max-size- 5MB, Allowed-formate-'JPEG*PNG*JPG*GIF*PDF*DOC*DOCX'</span></small>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return validateform();" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <a class="btn btn-block btn-default" style="margin-top: 23px;" href="HREmpServiceBook.aspx">Clear</a>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnPrint" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" runat="server" Text="Print" OnClientClick="CallPrint('DivPrint');" />
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="box box-success" id="DivPrint">
                <div class="box box-default section-to-print">
                    <div class="box-header text-center">
                        <h3 style="text-align: center;">
                             <img src="../image/mpagro-logo.png" class="salary-logo"><br />
                           <%-- &nbsp;&nbsp; THE M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd.<br />--%>
                            Service Book</h3>
                    </div>
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                   
                                    <asp:Repeater ID="Repeater1" runat="server">
                                        <ItemTemplate>
                                              <%--<embed src='<%# Eval("UploadServiceBook") %>' style="width: 100%; height: 1200px;" />--%>
                                            <%--<embed src='<%#Eval("UploadServiceBook") %>' style="width: 100%; height: 1200px;" />--%>
                                            <img src='<%# Eval("UploadServiceBook").ToString()%>' alt="User Image" style="width: 100%; height: 1200px;" />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <style>
                    .box {
                        position: relative;
                        border-radius: 3px;
                        background: #ffffff;
                        border-top: 3px solid #d2d6de;
                        margin-bottom: 20px;
                        width: 100%;
                        box-shadow: 0 1px 1px rgba(0,0,0,0.1);
                        box-shadow: none;
                        border-top: none;
                    }

                    .table-bordered > thead > tr > th, .table-bordered > tbody > tr > th, .table-bordered > tfoot > tr > th, .table-bordered > thead > tr > td, .table-bordered > tbody > tr > td, .table-bordered > tfoot > tr > td {
                        border: 1px solid #e1e1e1;
                    }

                    .text-center h3 {
                        font-size: 20px;
                    }

                    .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
                        padding: 1px 1px;
                    }

                    #subheading-salary {
                        font-size: 16px;
                    }

                    .salary-logo {
                        -webkit-filter: grayscale(100%);
                        filter: grayscale(100%);
                        width: 40px;
                    }

                    .printbutton {
                        border-top: 1px dashed #838383;
                        margin-top: 5px;
                        padding-top: 5px;
                    }

                    table h4 {
                        font-size: 15px;
                    }

                    .table {
                        margin-bottom: 5px;
                    }

                    th, td, h3 {
                        text-transform: uppercase !important;
                    }
                </style>
            </div>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script type="text/javascript">

        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('print.htm', 'PrintWindow', 'letf=0,top=0,width=800%,height=600,toolbar=1,scrollbars=1,status=1');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
        }

    </script>
    <script type="text/javascript">
        function ShowModal() {
            $("#ModalRelieve").modal();
        }
        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlOfficeName.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Current Office. \n";
            }
            if (document.getElementById('<%=ddlEmployee.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Employee Name. \n";
            }
            if (document.getElementById('<%=txtSNo.ClientID%>').value.trim()== "") {
                msg = msg + "Enter S No. \n";
            }
            if (document.getElementById('<%=FU_ServiceBook.ClientID%>').value.trim() == "") {
                msg = msg + "Upload Service Book. \n";
            }
            
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    if (confirm("Do you really want to Save Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Edit") {
                    if (confirm("Do you really want to Edit Details ?")) {
                        return true;
                    }
                    else {
                        return false;
                    }

                }
            }
        }

    </script>
</asp:Content>

