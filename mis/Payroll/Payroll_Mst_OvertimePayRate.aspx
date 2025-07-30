<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Payroll_Mst_OvertimePayRate.aspx.cs" Inherits="mis_Payroll_Payroll_Mst_OvertimePayRate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>

                        </button>
                        <h4 class="modal-title" id="myModalLabel">Confirmation</h4>

                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <img src="../assets/images/question-circle.png" width="30" />&nbsp;&nbsp;
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" ValidationGroup="a" Text="Yes" ID="btnYes" OnClick="btnSave_Click" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />

                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>

        </div>
    </div>
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <div class="box box-success">
                    <div class="box-header with-border">

                        <div class="box-header with-border">
                            <h3 class="box-title" id="Label1"> Overtime Pay Rate Master</h3>
                            
                        </div>
                        <div class="box-body">
                          
                                <fieldset>
                                    <legend>Overtime Pay Rate Master</legend>
                                    <div class="row">
                                         <div class="col-md-12">
                                            <asp:Label runat="server" ID="lblMsg"></asp:Label>
                                        </div>
                                        <div class="col-md-12">
                                          
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label runat="server">Effective Month <br />प्रभावी माह  <span style="color: red;">*</span></asp:Label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="rfvEffectiveDate" ValidationGroup="a"
                                                                ErrorMessage="Select Effective Date" ForeColor="Red" Text="<i class='fa fa-exclamation-circle' title='Select Effective Date'></i>"
                                                                ControlToValidate="txtEffectiveDate" Display="Dynamic" runat="server">
                                                            </asp:RequiredFieldValidator>
                                                            
                                                        </span>
                                                        <asp:TextBox runat="server" autocomplete="off" CssClass="form-control" 
                                                            ID="txtEffectiveDate" MaxLength="8" placeholder="Select Effective Month" 
                                                            data-provide="datepicker" onpaste="return false ;" 
                                                            onkeypress="return false;" data-date-format="mm/yyyy" 
                                                            data-date-autoclose="true" ClientIDMode="Static"></asp:TextBox>
                                                    </div>

                                                </div>                                         
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label runat="server">Overtime Pay Rate(Per Hour) <br />ओवरटाइम वेतन दर(प्रति घंटा) <span style="color: red;">*</span></asp:Label>
                                                        <span class="pull-right">
                                                            <asp:RequiredFieldValidator ID="rfvrebate1" ValidationGroup="a"
                                                                ErrorMessage="Enter Overtime Pay Rate" ForeColor="Red"
                                                                Text="<i class='fa fa-exclamation-circle' title='Enter Overtime Pay Rate'></i>"
                                                                ControlToValidate="txtOvertimePayRate" Display="Dynamic" runat="server">
                                                            </asp:RequiredFieldValidator>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator5"
                                                                  runat="server" Display="Dynamic" ValidationGroup="a"
                                                                ErrorMessage="Enter Valid Overtime Pay Rate !" ForeColor="Red" 
                                                                 Text="<i class='fa fa-exclamation-circle' title='Enter Valid Overtime Pay Rate. !'></i>" 
                                                                 ControlToValidate="txtOvertimePayRate" ValidationExpression="^[1-9][0-9]*$">
                                        </asp:RegularExpressionValidator>
                                                        </span>
                                                        <asp:TextBox ID="txtOvertimePayRate" autocomplete="off" runat="server" onkeypress="return validateNum(event);" MaxLength="5" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <div class="col-md-2" style="margin-top:37px;">
                                                     <div class="form-group">
                                                          
                                                <asp:Button runat="server" ID="btnSave" ValidationGroup="a" CssClass="btn btn-success btn-block" Text="Save" OnClientClick="return ValidatePage() " />
                                                         <asp:HiddenField ID="hfrowid" runat="server" />
                                                         </div>
                                            </div>
                                            <div class="col-md-2" style="margin-top:37px;">
                                                <div class="form-group">
                                                   
                                                <asp:LinkButton ID="lnkbtnClear" OnClick="lnkbtnClear_Click" runat="server" CssClass="btn btn-default btn-block" Text="Clear"></asp:LinkButton>
                                                    </div>
                                            </div>
                                           


                                        </div>

                                    </div>

                                </fieldset>
                              
                                </div>
                            <div class="box-footer">
                                <fieldset runat="server" id="gridfieldset">
                                    <legend>Overtime Pay Rate Details</legend>
                                    <div class="row">
                                       
                                        <div class="col-md-12">
                                            <div class="">
                                                <asp:GridView ID="GridView1" OnRowCommand="GridView1_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                    EmptyDataText="No Record Found." DataKeyNames="OverTimePayRateId">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Effective Month <br />(प्रभावी माह)">
                                                            <ItemTemplate>
                                                                
                                                                <asp:Label ID="lblMonthAndYear" runat="server" Text='<%# Eval("MonthAndYear") %>'></asp:Label>
                                                                <asp:Label ID="lblEffectiveMonthYear" Visible="false" runat="server" Text='<%# Eval("EffectiveMonthYear") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Overtime Pay Rate(Per Hour) <br />ओवरटाइम वेतन दर(प्रति घंटा)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayRate" runat="server" Text='<%# Eval("PayRate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>                                                      
                                                        <asp:TemplateField HeaderText="Update<br />(सुधार करें)" Visible="true">
                                                            <ItemTemplate>
                                                                 <asp:LinkButton ID="lnkView" CommandName="RecordView" Visible='<%#( Eval("RateUpdatedStatus").ToString()==""?false:true) %>' CommandArgument='<%#Eval("OverTimePayRateId") %>' runat="server" ToolTip="View" Style="color: black;" CssClass="fa fa-eye"></asp:LinkButton>
                                                                &nbsp;&nbsp;<asp:LinkButton ID="lnkUpdate" CommandName="RecordUpdate" CommandArgument='<%#Eval("OverTimePayRateId") %>' runat="server" ToolTip="Update" Style="color: black;" CssClass="fa fa-edit"></asp:LinkButton>
                                                                
                                                              
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>

                    </div>
                </div>
            <div class="modal" id="ViewOverTimePayRate" tabindex="-1" role="dialog" data-backdrop="static" data-keyboard="false">
                <div style="display: table; height: 100%; width: 100%;">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header" style="padding: 0.5rem !important">

                              
                                   <h4>Pay Rate Details</h4>
                               
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                            </div>
                            <div class="modal-body" style="height: 200px; overflow: auto;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="datatable table table-striped table-bordered table-hover"
                                                    EmptyDataText="No Record Found." DataKeyNames="OverTimePayRate_ChildId">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Effective Month <br />(प्रभावी माह)">
                                                            <ItemTemplate>
                                                                
                                                                <asp:Label ID="lblMonthAndYear" runat="server" Text='<%# Eval("MonthAndYear") %>'></asp:Label>
                                                                <asp:Label ID="lblEffectiveMonthYear" Visible="false" runat="server" Text='<%# Eval("EffectiveMonthYear") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Overtime Pay Rate(Per Hour) <br />ओवरटाइम वेतन दर(प्रति घंटा)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPayRate" runat="server" Text='<%# Eval("PayRate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                    </Columns>
                                                </asp:GridView>
                                        </div>  
                                </div>
                                <div class="modal-footer">
                                     <button type="button" class="btn btn-outline-danger" data-dismiss="modal">CLOSE </button>
                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                    </div>
                    <!-- /.modal-dialog -->
                </div>
            </div>
                </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        $("#txtEffectiveDate").datepicker({
            format: "mm/yyyy",
            viewMode: "months",
            minViewMode: "months",
            autoclose: true,
        });
    </script>
    <script type="text/javascript">
        function ViewOverTimePayRateDetails() {
            $("#ViewOverTimePayRate").modal('show');

        }
        function ValidatePage() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate('a');
            }

            if (Page_IsValid) {

                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Update") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Update this record?";
                    $('#myModal').modal('show');
                    return false;
                }
                if (document.getElementById('<%=btnSave.ClientID%>').value.trim() == "Save") {
                    document.getElementById('<%=lblPopupAlert.ClientID%>').textContent = "Are you sure you want to Save this record?";
                    $('#myModal').modal('show');
                    return false;
                }
            }
        }
    </script>
</asp:Content>
