<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Profit-and-Loss-Report.aspx.cs" Inherits="mis_Finance_Profit_and_Loss_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    
    <div class="content-wrapper">
        <section class="content-header">
                <h1>
                    Profit & Loss Report
                    <small></small>
                </h1>
                <%--<ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                </ol>--%>
            </section>
            <section class="content">
                <div class="box box-pramod" style="background-color: #FFFFFF;">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset class="box-body">
                                    <legend>Profit & Loss A/C</legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                             

                                            <fieldset class="box-body">

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Particulars</label>
                                                    </div>
                                                </div>

                                                <div class="col-md-3" align="right">
                                                    <div class="form-group">
                                                        <label>1-July-2018 To 31-July-2018</label>
                                                    </div>
                                                </div>

                                                <div class="col-md-3 abcline">
                                                    <div class="form-group">
                                                        <label>Particulars</label>
                                                    </div>
                                                </div>

                                                <div class="col-md-3" align="right">
                                                    <div class="form-group">
                                                        <label>1-July-2018 To 31-July-2018</label>
                                                    </div>
                                                </div>

                                            </fieldset>

                                            <div class="row">
                                                <div class="col-md-6">
                                                    <fieldset class="box-body">
                                                        
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="#">Opening Stock</a></label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6" align="right">
                                                                <div class="form-group">
                                                                    <label>5,000 </label>
                                                                </div>
                                                            </div>


                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="#">Purchase Accounts </a></label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6" align="right">
                                                                <div class="form-group">
                                                                    <label>5,000 </label>
                                                                </div>
                                                            </div>


                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="#">Direct Expences</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6" align="right">
                                                                <div class="form-group">
                                                                    <label>5,000 </label>
                                                                </div>
                                                            </div>


                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="group-summary1.html">Gross Profit</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6" align="right">
                                                                <div class="form-group">
                                                                    <label>5,000 </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="group-summary1.html">Direct Expences</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6" align="right">
                                                                <div class="form-group">
                                                                    <label>5,000 </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="group-summary1.html">Nett Profit</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6" align="right">
                                                                <div class="form-group">
                                                                    <label>5,000 </label>
                                                                </div>
                                                            </div>

                                                            




                                                        </div>
                                                    </fieldset>
                                                </div>
                                                <div class="col-md-6">
                                                    <fieldset class="box-body">
                                                       
                                                        <div class="row">

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label> <a href="#">Sales Account</a> </label>
                                                                </div>
                                                            </div>


                                                            <div class="col-md-6" align="right">
                                                                <div class="form-group">
                                                                    <label>5,000 </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="#">Direct Income</a> </label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6" align="right">
                                                                <div class="form-group">
                                                                    <label>3,000</label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="#">Closing Stock</a> </label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6" align="right">
                                                                <div class="form-group">
                                                                    <label>2,000</label>
                                                                </div>
                                                            </div>
                                                            
                                                             

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="#">Gross Profit</a> </label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6" align="right">
                                                                <div class="form-group">
                                                                    <label>2,000</label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="#">Indirect Income</a> </label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6" align="right">
                                                                <div class="form-group">
                                                                    <label>2,000</label>
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



                    </div>
                </div>
            </section>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
</asp:Content>
