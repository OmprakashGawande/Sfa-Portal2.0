<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Balance-Sheet-Report.aspx.cs" Inherits="mis_Finance_Balance_Sheet_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">

    <div class="content-wrapper">
        <section class="content-header">
            <h1>Balance Sheet
                    <small></small>
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-dashboard"></i>Home</a></li>
            </ol>
        </section>
        <section class="content">
            <div class="box box-pramod" style="background-color: #FFFFFF;">
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Office Name</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlOffice" CssClass="form-control select2" ClientIDMode="Static">
                                   </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Financial Year</label><span style="color: red">*</span>
                                <asp:DropDownList runat="server" ID="ddlFinancialYear" CssClass="form-control select2" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-md-2">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-block btn-success" Style="margin-top: 24px;" Text="Search" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <asp:Panel ID="PanelSheet" runat="server">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="box-body">
                                <legend>Balance Sheet</legend>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="row">
                                            <div class="col-md-6">
                                                <fieldset class="box-body">
                                                    <legend>Liabilities</legend>
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label><a href="group-summary1.aspx">Capital Accopunt </a></label>
                                                            </div>
                                                        </div>


                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label><a href="group-summary1.aspx">Loans (Liabilities) </a></label>
                                                            </div>
                                                        </div>


                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label><a href="group-summary1.aspx">Current Liabilities</a> </label>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6" align="right">
                                                            <div class="form-group">
                                                                <label>5,000 </label>
                                                            </div>
                                                        </div>


                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label><a href="group-summary1.aspx">Profit & Loss A/C</a> </label>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6" align="right">
                                                            <div class="form-group">
                                                                <label>5,000 </label>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6" style="padding-bottom: 8px;">
                                                            <div>
                                                                Opening Balance 
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6" align="center" style="padding-bottom: 8px;">
                                                            <div>
                                                                7000
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <div>
                                                                Current Period
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6" align="center" style="padding-bottom: 8px;">
                                                            <div>
                                                                3000
                                                            </div>
                                                        </div>




                                                    </div>
                                                </fieldset>
                                            </div>
                                            <div class="col-md-6">
                                                <fieldset class="box-body">
                                                    <legend>Assets </legend>
                                                    <div class="row">

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label><a href="group-summary1.aspx">Fixed Assets</a> </label>
                                                            </div>
                                                        </div>


                                                        <div class="col-md-6" align="right">
                                                            <div class="form-group">
                                                                <label>5,000 </label>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label><a href="group-summary1.aspx">Current Assets</a> </label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6" align="right">
                                                            <div class="form-group">
                                                                <label>3,000</label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label><a href="group-summary1.aspx">Branch And Division</a> </label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-6" align="right">
                                                            <div class="form-group">
                                                                <label>2,000</label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>&nbsp;</label>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <label>&nbsp;</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                        <fieldset class="box-body">

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Total</label>
                                                </div>
                                            </div>

                                            <div class="col-md-3" align="right">
                                                <div class="form-group">
                                                    <label>10,000</label>
                                                </div>
                                            </div>

                                            <div class="col-md-3 abcline">
                                                <div class="form-group">
                                                    <label>Total</label>
                                                </div>
                                            </div>

                                            <div class="col-md-3" align="right">
                                                <div class="form-group">
                                                    <label>10,000</label>
                                                </div>
                                            </div>

                                        </fieldset>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>

                    </asp:Panel>
                </div>
            </div>
        </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>
