<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Ledger-Monthly-Summary.aspx.cs" Inherits="mis_Finance_Ledger_Monthly_Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    
    <div class="content-wrapper">
        
        <section class="content-header">
                <h1>
                    Ledger Monthly Summary
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
                                    <legend>Ledger Monthly Details</legend>
                                    <div class="row">
                                        <div class="col-md-12">


                                            <div class="row">
                                                <div class="col-md-12">

                                                    <fieldset class="box-body">

                                                        <div class="row">


                                                            <div class="col-md-6">

                                                                <label>Particulars </label>

                                                            </div>

                                                            <div class="col-md-2" align="right">

                                                                <label>Debit </label>

                                                            </div>

                                                            <div class="col-md-2" align="right">

                                                                <label>Credit </label>

                                                            </div>

                                                            <div class="col-md-2" align="right">

                                                                <label>Closing Balance </label>

                                                            </div>




                                                        </div>

                                                    </fieldset>


                                                    <fieldset class="box-body">

                                                        <div class="row">
                                                            <div class="col-md-10">
                                                                <div class="form-group">
                                                                    <label>Opening Balance </label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <div class="form-group" align="right">
                                                                    <label>1,00,000 Cr</label>
                                                                </div>
                                                            </div>
                                                        </div>



                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="Ledger-Voucher.aspx">April</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    1,000
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    1,000
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    1,000 Cr
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="Ledger-Voucher.aspx">May</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    1,000
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    1,000
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    1,000 Cr
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="Ledger-Voucher.aspx">June</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    1,000
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    1,000
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    1,000 Cr
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="Ledger-Voucher.aspx">July</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    1,000
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    1,000
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    1,000 Cr
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="Ledger-Voucher.aspx">August</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="Ledger-Voucher.aspx">September</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="Ledger-Voucher.aspx">October</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="Ledger-Voucher.aspx">November</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="Ledger-Voucher.aspx">December</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="Ledger-Voucher.aspx">January</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                        </div>


                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="Ledger-Voucher.aspx">February</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label><a href="Ledger-Voucher.aspx">March</a> </label>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                            <div class="col-md-2" align="right">
                                                                <div class="form-group">
                                                                    &nbsp;
                                                                </div>
                                                            </div>

                                                        </div>





                                                    </fieldset>

                                                    <fieldset class="box-body">

                                                        <div class="row">


                                                            <div class="col-md-6">

                                                                <label>Grand Total </label>

                                                            </div>

                                                            <div class="col-md-2" align="right">

                                                                <label>3,000 </label>

                                                            </div>

                                                            <div class="col-md-2" align="right">

                                                                <label>1,500 </label>

                                                            </div>

                                                            <div class="col-md-2" align="right">

                                                                <label>1,500 Cr</label>

                                                            </div>




                                                        </div>

                                                    </fieldset>

                                                </div>

                                            </div>



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


