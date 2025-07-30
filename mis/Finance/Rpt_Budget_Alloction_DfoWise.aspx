<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Rpt_Budget_Alloction_DfoWise.aspx.cs" Inherits="mis_Finance_Rpt_Budget_Alloction_DfoWise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
    <style>
        @media print {
              
              .Hiderow {
                display: none;
            }
              
          }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
     <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Budget Utilization </h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">
                    <div class="row Hiderow">

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Financial year</label><span style="color: red">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="a"
                                        ErrorMessage="Select Financial Year" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Financial Year !'></i>"
                                        ControlToValidate="ddlFyear" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlFyear" runat="server" ClientIDMode="Static" CssClass="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Month</label><span style="color: red">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="a"
                                        ErrorMessage="Select Month" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Month !'></i>"
                                        ControlToValidate="ddlFyear" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                </span>
                                <asp:DropDownList ID="ddlMonth" runat="server" ClientIDMode="Static" CssClass="form-control select2">
                                     <asp:ListItem Value="0">Select</asp:ListItem>
                                     
                                     <asp:ListItem Value="4">April</asp:ListItem>
                                     <asp:ListItem Value="5">May</asp:ListItem>
                                     <asp:ListItem Value="6">June</asp:ListItem>
                                     <asp:ListItem Value="7">July</asp:ListItem>
                                     <asp:ListItem Value="8">August</asp:ListItem>
                                     <asp:ListItem Value="9">September</asp:ListItem>
                                     <asp:ListItem Value="10">October</asp:ListItem>
                                     <asp:ListItem Value="11">November</asp:ListItem>
                                     <asp:ListItem Value="12">December</asp:ListItem>
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                     <asp:ListItem Value="2">February</asp:ListItem>
                                     <asp:ListItem Value="3">March</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Circle Office</label>
                                <span class="pull-right">
                                    
                                </span>
                                <asp:DropDownList ID="ddlRegionalOffice" runat="server" ClientIDMode="Static" CssClass="form-control select2" OnSelectedIndexChanged="ddlRegionalOffice_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <span class="pull-right">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a"
                                        ErrorMessage="Select Office" InitialValue="0" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Office !'></i>"
                                        ControlToValidate="ddlOffice" Display="Dynamic" runat="server">
                                    </asp:RequiredFieldValidator>
                                </span>
                               <%-- <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2">
                                </asp:DropDownList>--%>
                                 <asp:ListBox runat="server" ID="ddlOffice" ClientIDMode="Static" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                            </div>
                        </div>
                         <div class="col-md-1" style="margin-top: 23px;">
                            <div class="form-group">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" ValidationGroup="a" CssClass="btn btn-block btn-success" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top: 23px;">
                            <div class="form-group">
                            <a runat="server" id="btnClear" class="btn  btn-block btn-default" href="Rpt_Budget_Alloction_DfoWise.aspx">Clear</a></div>
                        </div>

                    </div>

                  
                    <fieldset>
                        <legend class="Hiderow">Budget Utilization Details</legend>
                        <div class="row">
                             <div class="col-md-3 Hiderow">
                                <div class="form-group">
                                 <a id="dlink" style="display: none;"></a>
                                <asp:Button id="btnPrint" runat="server" Text="Print" CssClass="btn btn-default Hiderow" OnClientClick="window.print()" />
                               <asp:Button runat="server" Text="Export" OnClientClick="tableToExcel('testTable', 'Budget Utilization','Budget Utilization')" ID="btnExport" class="btn btn-flat btn-success Hiderow" />
                     
                            </div>
                                </div>
                             <div id="testTable">
                                 <div class="col-md-12">
                                      <table id="tblHeader" style="width:100%">
                              <tr>
                                  <td colspan="4" style="text-align:center;"> <asp:Label ID="lblHeader" style="text-align:center;" runat="server" Text=""></asp:Label></td>
                              </tr>
                          </table>
                                 </div>
                                  
                            <div class="col-md-12">
                                <asp:GridView runat="server" ShowFooter="true" CssClass="datatable table table-striped table-bordered table-hover" ID="GridView1" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S. NO.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNo" runat="server" Text='<%#Container.DataItemIndex +1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ledger Code">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("Ledger_Code").ToString()  %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ledger Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("Ledger_Name").ToString()%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Allocation">
                                            <ItemTemplate>
                                               <asp:Label runat="server" Text='<%# Eval("Allocation").ToString()%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Utilization">
                                            <ItemTemplate>
                                               <asp:Label runat="server" Text='<%# Eval("Utilization").ToString()%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="% of Utilization">
                                            <ItemTemplate>
                                               <asp:Label runat="server" Text='<%# string.Concat(Eval("UtilizationPer").ToString()," ","%")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
                                </asp:GridView>

                            </div>
                                </div>
                            
                        </div>

                    </fieldset>




                </div>

            </div>


        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
     <link href="../../../mis/css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="../../../mis/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script>

        $(function () {
            $('[id*=ddlOffice]').multiselect({
                includeSelectAllOption: true,
                includeSelectAllOption: true,
                buttonWidth: '100%',

            });


        });
    </script>
     <style>
        .multiselect-native-select .multiselect {
            text-align: left !important;
        }

        .multiselect-native-select .multiselect-selected-text {
            width: 100% !important;
        }

        .multiselect-native-select .checkbox, .multiselect-native-select .dropdown-menu {
            width: 100% !important;
        }

        .multiselect-native-select .btn .caret {
            float: right !important;
            vertical-align: middle !important;
            margin-top: 8px;
            border-top: 6px dashed;
        }
    </style>
     <script>
         var tableToExcel = (function () {
             var uri = 'data:application/vnd.ms-excel;base64,'
               , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
               , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
               , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
             // return function (table, name) {
             return function (table, name, filename) {
                 var x = $("#" + table).clone();
                 $(x).find("tr td a").replaceWith(function () {
                     return $.text([this]);
                 });
                 //console.log(x);
                 //console.log(x.innerHTML);
                 if (!table.nodeType) table = x
                 //console.log(table[0].innerHTML);
                 //if (!table.nodeType) table = document.getElementById(table)
                 var ctx = { worksheet: name || 'Worksheet', table: table[0].innerHTML }
                 //window.location.href = uri + base64(format(template, ctx))
                 document.getElementById("dlink").href = uri + base64(format(template, ctx));
                 document.getElementById("dlink").download = filename;
                 document.getElementById("dlink").click();
             }
         })()
    </script>
</asp:Content>

