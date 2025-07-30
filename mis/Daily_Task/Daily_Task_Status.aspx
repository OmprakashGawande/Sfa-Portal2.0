<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Daily_Task_Status.aspx.cs" Inherits="mis_Daily_Task_Daily_Task_Status" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <asp:ValidationSummary ID="vs" runat="server" ValidationGroup="a" ShowMessageBox="true" ShowSummary="false" />
    <%--Confirmation Modal Start --%>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div style="display: table; height: 100%; width: 100%;">
            <div class="modal-dialog" style="width: 340px; display: table-cell; vertical-align: middle;">
                <div class="modal-content" style="width: inherit; height: inherit; margin: 0 auto;">
                    <div class="modal-header" style="background-color: #d9d9d9;">

                        <span class="modal-title" style="float: left" id="myModalLabel">Confirmation</span>
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true">&times;</span><span class="sr-only">Close</span>
                        </button>
                    </div>
                    <div class="clearfix"></div>
                    <div class="modal-body">
                        <p>
                            <%--<img src="../assets/images/question-circle.png" width="30" />--%>&nbsp;&nbsp; 
                           <i class="fa fa-question-circle"></i>
                            <asp:Label ID="lblPopupAlert" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button runat="server" CssClass="btn btn-success" Text="Yes" ID="btnYes" Style="margin-top: 20px; width: 50px;" />
                        <asp:Button ID="btnNo" ValidationGroup="no" runat="server" CssClass="btn btn-danger" Text="No" data-dismiss="modal" Style="margin-top: 20px; width: 50px;" />
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
    </div>
    <%--ConfirmationModal End --%>
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">

                <div class="box box-success">
                    <div class="box-header with-border">
                        <h3 class="box-title" id="Label1">TASK STATUS</h3>
                    </div>
                    <asp:Label runat="server" ID="lblMsg"></asp:Label>
                    <div class="box-body">

                        <div class="row">
                            <div class="col-md-3">
                                <label runat="server">DATE </label>
                                <div class="form-group">
                                    <asp:TextBox runat="server" ID="txtDate"
                                        data-date-end-date="0d"
                                        data-provide="datepicker" placeholder="DD/MM/YYYY"
                                        autocomplete="off" data-date-format="dd/mm/yyyy"
                                        data-date-autoclose="true" CssClass="form-control disableFuturedate"
                                       ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="margin-top:22px">
                           
                                <asp:LinkButton runat="server" ID="btnSearch" CssClass="btn btn-block btn-success" 
                                    OnClick="btnSearch_Click"><i class="fa fa-search"> Search</i></asp:LinkButton>

                            </div>
                            <div class="col-md-2" style="margin-top:22px">
                                <a href="Daily_Task_Status.aspx" class="btn btn-block btn-default">Clear</a>
                            </div>
                             <div class="col-md-2" style="margin-top:22px">
                           
                                <asp:LinkButton runat="server" ID="btnprint" CssClass="btn btn-pinterest btn-block" 
                                    OnClick="btnprint_Click"><i class="fa fa-print">Print</i></asp:LinkButton>

                            </div>
                             <div class="col-md-2" style="margin-top:22px">
                           
                                <asp:LinkButton runat="server" ID="btnSendEmail" CssClass="btn btn-pinterest btn-block" 
                                    OnClick="btnSendEmail_Click"><i class="fa fa-print">Print</i></asp:LinkButton>

                            </div>
                        </div>
                    </div>
                    <div class="box-body">
                        <fieldset>
                            <legend>Details</legend>
                            <div class="table-responsive">
                                <asp:GridView runat="server" AutoGenerateColumns="false" ID="gridvew1"
                                    CssClass="table table-bordered table-hover" OnRowCommand="gridvew1_RowCommand">

                                    <Columns>
                                       
                                        <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="13">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <%-- <asp:Label runat="server" Text='<%#Eval("TaskDelete_Status").ToString() %>'></asp:Label>--%>
                                                              <asp:LinkButton ID="lnkDeleteTask"  CommandName="DeleteTask" ToolTip='<%# Eval("Task_Id") %>'
                                                     CommandArgument='<%# Eval("Task_Id") %>' runat="server" 
                                                      CssClass="label label-danger"   OnClientClick='<%# "return confirm(\"Do you really want to delete task ?\")"%>'
                                                      Visible='<%# Eval("Task_Id").ToString() != "" && Eval("Task_Id").ToString() != "0" ? Eval("TaskDelete_Status").ToString() == "1"? true:false : false %>'><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="STATUS">
                                            <ItemTemplate>
                                                             <%--<asp:LinkButton ID="lnkViewDetails"  CommandName="ViewDetails" ToolTip="View" 
                                                                 CommandArgument='<%# Eval("Task_Id") %>' runat="server"  CssClass='<%#Eval("Emp_ID").ToString() == "0"? "label label-danger" : "label label-success" %>' Text='<%#Eval("Emp_ID").ToString() == "0"? "Not Filled" : " Filled" %>'></asp:LinkButton>--%>
                                               <%-- <asp:LinkButton ID="lnkViewDetails"  CommandName="ViewDetails" ToolTip="View" 
                                                                 CommandArgument='<%# Eval("Task_Id") %>' runat="server"  CssClass='<%#Eval("Emp_ID").ToString() == "0"?  Eval("Day_Name").ToString() == "Saturday" || Eval("Day_Name").ToString() =="Sunday" ?"label label-warning" : "label label-danger" : "label label-success" %>' 
                                                     Text='<%#Eval("Emp_ID").ToString() == "0"? Eval("Day_Name").ToString() == "Saturday" || Eval("Day_Name").ToString() =="Sunday" ?"Holiday" : "Not Filled" : " Filled" %>'></asp:LinkButton>   --%>                                           
                                             <asp:LinkButton ID="lnkViewDetails"  CommandName="ViewDetails" ToolTip="View" 
                                                     CommandArgument='<%# Eval("Task_Id") %>' runat="server" 
                                                      CssClass='<%#Eval("Task_StatusClass").ToString() %>' 
                                                     Text='<%# Eval("Task_Status").ToString()  %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="EMPLOYEE NAME">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%#Eval("Emp_Name").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="DESIGNATION">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%#Eval("Designation_Name").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="TOTAL WORKING HOURS">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%#Eval("Total_Hourse").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="TASK SUBMIT ON">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%#Eval("Task_Submit_On").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>No record found</EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
                        <div class="modal fade" id="ViewDetails" role="dialog">
                <div class="modal-dialog modal-lg">
                    <!-- Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title" style="color: blue">TASK STATUS :-</h4>
                        </div>
                       <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView runat="server" ID="gridEmpTask" CssClass="table table-bordered"
                                                 AutoGenerateColumns="false" >
                                                <Columns>
                                                <asp:TemplateField HeaderText="S.No." HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                        <asp:Image runat="server" ID="ImgNew" Style="width: 40px; margin-left: 5px;" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PROJECT NAME" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%#Eval("Project_Name").ToString() %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="WORK CATEGORY" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%#Eval("WorkCategory").ToString() %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WORK DESCRIPTION " HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="EffectiveDat" Text='<%#Eval("Work_Description").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="TOTAL WORKING HOURS" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="PurchaseRat" Text='<%#Eval("Total_Hourse").ToString() %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                                <EmptyDataTemplate>No Record Found</EmptyDataTemplate>
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
</asp:Content>

