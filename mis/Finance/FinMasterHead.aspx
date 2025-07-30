<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinMasterHead.aspx.cs" Inherits="mis_Finance_FinMasterHead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <script src="https://www.google.com/jsapi" type="text/javascript">
    </script>
    <script  type="text/javascript">
        google.load("elements", "1", { packages: "transliteration" });

        function onLoad() {
            var options = {
                //Source Language
                sourceLanguage: google.elements.transliteration.LanguageCode.ENGLISH,
                // Destination language to Transliterate
                destinationLanguage: [google.elements.transliteration.LanguageCode.HINDI],
                shortcutKey: 'ctrl+g',
                transliterationEnabled: true
            };

            var control = new google.elements.transliteration.TransliterationControl(options);
            control.makeTransliteratable(['txtHeadNameHindi']);

        }
        google.setOnLoadCallback(onLoad);
    </script>
    <style>
         .capitalize
        {
            text-transform: capitalize;
        }
         .noPrint
    {
        display: none !important;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1">Finance Head Master</h3>
                            <asp:Label ID="lblMsg" runat="server" Text="" Visible="true"></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Group<span style="color: red;"> *</span></label>
                                        <asp:DropDownList runat="server" ID="ddlHead_Name" CssClass="form-control select1 select2" ClientIDMode="Static" OnSelectedIndexChanged="ddlHead_Name_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Group Name</label><span style="color: red">*</span>
                                        <asp:TextBox runat="server" Placeholder="Enter Group Name " ID="txtHeadName" CssClass="form-control capitalize" ClientIDMode="Static" onkeypress="javascript:tbx_fnAlphaOnly(event, this);" MaxLength="50"></asp:TextBox>
                                        <small><span id="valtxtHeadName" class="text-danger"></span></small>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Group Name(In Hindi)</label>
                                        <asp:TextBox runat="server" Placeholder="Enter Group Name In Hindi " ID="txtHeadNameHindi" CssClass="form-control capitalize" ClientIDMode="Static" onkeypress="javascript:tbx_fnAlphaOnly(event, this);" MaxLength="50"></asp:TextBox>
                                        <small><span id="valtxtHeadNameHindi" class="text-danger"></span></small>
                                    </div>
                                </div>
                                 <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Group Code<span style="color: red">*</span></label>
                                        <asp:TextBox runat="server" Placeholder="Enter Group Code" ID="txtHeadCode" CssClass="form-control" ClientIDMode="Static" onkeypress="return validateNum(event);"></asp:TextBox>
                                        <small><span id="valtxtHeadCode" class="text-danger"></span></small>
                                    </div>
                                </div>
                                </div>
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button ID="BtnSubmit" CssClass="btn btn-block btn-success" runat="server" Text="Submit" OnClick="BtnSubmit_Click" OnClientClick="return validateform();" />
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <a class="btn btn-block btn-default" href="FinMasterHead.aspx">Clear</a>
                                    </div>
                                </div>

                            </div>
                            <div class="box-footer"></div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="GridView1" DataKeyNames="Head_ID" runat="server" class="datatable table table-hover table-bordered table-striped" ClientIDMode="Static" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDeleting="GridView1_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SNo." ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Group Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHead_Name" Text='<%# Eval("Head_Name").ToString() %>' runat="server" />
                                                        <%--<asp:Label ID="lblHead_ID" CssClass="hidden noPrint" Text='<%# Eval("Head_ID").ToString() %>' runat="server" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Group Name Hindi">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHead_Name_Hindi" Text='<%# Eval("Head_Name_Hindi").ToString() %>' runat="server" />
                                                        <%--<asp:Label ID="lblHead_ID" CssClass="hidden noPrint" Text='<%# Eval("Head_ID").ToString() %>' runat="server" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Group Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHead_Code" Text='<%# Eval("Head_Code").ToString() %>' runat="server" />
                                                        <%--<asp:Label ID="lblHead_ID" CssClass="hidden noPrint" Text='<%# Eval("Head_ID").ToString() %>' runat="server" />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <ItemTemplate>
                                                        
                                                        <asp:Label ID="lblHead_ID" CssClass="hidden noPrint" Text='<%# Eval("Head_ID").ToString() %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Head_Name" HeaderText="Group Name" />--%>
                                                <asp:BoundField DataField="Head_ParentName" HeaderText="Parent Group" />
                                                <asp:BoundField DataField="MainHeadName" HeaderText="Nature of Group" />
                                                <%--<asp:BoundField DataField="Head_Level" HeaderText="Level" />--%>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="label Aselect1 label-default" Text="Edit" CommandName="Select" CausesValidation="false"></asp:LinkButton>
                                                    </ItemTemplate> 
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="label label-danger" Text="Delete" CommandName="Delete" CausesValidation="false" OnClientClick="return getConfirmation();"></asp:LinkButton>
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
            </div>

        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <link href="https://cdn.datatables.net/1.10.18/css/dataTables.bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.datatables.net/1.10.18/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.18/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.flash.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/pdfmake.min.js"></script>
    <script src="https://cdn.rawgit.com/bpampuch/pdfmake/0.1.27/build/vfs_fonts.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script>
      
        $(document).ready(function () {
            $('.datatable').DataTable({

                paging: true,

                columnDefs: [{
                    targets: 'no-sort',
                    orderable: false
                }],
                "order": [[0, 'asc']],

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
                            columns: [0, 1,2,3,4,5]
                        },
                        footer: true,
                        autoPrint: true
                    }, {
                        extend: 'excel',
                        text: '<i class="fa fa-file-excel-o"></i> Excel',
                        title: $('h1').text(),
                        exportOptions: {
                            columns: [0, 1, 2, 3,4,5]
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
            t.on('order.dt search.dt', function () {
                t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    cell.innerHTML = i + 1;
                });
            }).draw();
        });




        function validateform() {
            var msg = "";
            if (document.getElementById('<%=ddlHead_Name.ClientID%>').selectedIndex == 0) {
                msg = msg + "Select Group. \n";
            }
            if (document.getElementById('<%=txtHeadName.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Group Name. \n";
            }
            if (document.getElementById('<%=txtHeadCode.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Group Code. \n";
            }
            if (msg != "") {
                alert(msg);
                return false;
            }
            else {
                if (document.getElementById('<%=BtnSubmit.ClientID%>').value.trim() == "Submit") {
                   
                        document.querySelector('.popup-wrapper').style.display = 'block';
                        return true;
                }
                if (document.getElementById('<%=BtnSubmit.ClientID%>').value.trim() == "Update") {
                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true;

                }
            }
        }
        function getConfirmation() {
            debugger;
            var retVal = confirm("Record will be deleted. Are you sure want to continue?");
            if (retVal == true) {
                document.querySelector('.popup-wrapper').style.display = 'block';
                return true;
            }
            else {

                return false;
            }
        }
    </script>
</asp:Content>

