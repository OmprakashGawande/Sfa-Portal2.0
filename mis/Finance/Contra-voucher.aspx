<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="Contra-voucher.aspx.cs" Inherits="mis_Finance_Contra_voucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    
    <div class="content-wrapper">
         <section class="content-header">
                <h1>
                    Contra Voucher
                    <small></small>
                </h1>
               <%-- <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                </ol>--%>
            </section>
            <section class="content">
                <div class="box box-pramod" style="background-color: #FFFFFF;">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <fieldset class="box-body">
                                    <legend>Voucher Detail </legend>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Voucher No <span style="color: red;">*</span></label>
                                                        <input name="cttxtDOB" type="text" id="txtDOB" class="form-control" placeholder="Enter Voucher No" />
                                                    </div>
                                                </div>

                                                <div class="col-md-5"></div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Voucher Date <span style="color: red;">*</span></label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <input name="ctl00$ContainerBody$txtDOB" type="text" id="txtDOB" class="form-control" autocomplete="off" placeholder="Enter Date" data-provide="datepicker" onpaste="return false">
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <fieldset class="box-body">
                                                        <legend> DR</legend>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Ledger Name (DR) <span style="color: red;">*</span></label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <select class="form-control">
                                                                        <option>Select</option>
                                                                        <option>Allahabad Bank, HO</option>
                                                                        <option>STATE BANK OF INDIA, HO</option>
                                                                        <option>Allahabad Bank Sweep Transfer Account</option>
                                                                        <option>Vidisha Bhopal Gramin Bank</option>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                                <div class="col-md-6">
                                                    <fieldset class="box-body">
                                                        <legend>CR </legend>
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <label>Ledger Name (CR) <span style="color: red;">*</span></label>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <select class="form-control">
                                                                        <option>Select</option>
                                                                        <option>Allahabad Bank, HO</option>
                                                                        <option>STATE BANK OF INDIA, HO</option>
                                                                        <option>Allahabad Bank Sweep Transfer Account</option>
                                                                        <option>Vidisha Bhopal Gramin Bank</option>
                                                                    </select>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <label>Cheque/ DD No. <span style="color: red;">*</span></label>
                                                        <input name="cttxtDOB" type="text" id="txtDOB" class="form-control" placeholder="Enter Cheque/ DD No.">
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Cheque/ DD Date<span style="color: red;">*</span></label>
                                                        <div class="input-group date">
                                                            <div class="input-group-addon">
                                                                <i class="fa fa-calendar"></i>
                                                            </div>
                                                            <input name="ctl00$ContainerBody$txtDOB" type="text" id="txtDOB" class="form-control" autocomplete="off" placeholder="Enter Cheque/ DD Date" data-provide="datepicker" onpaste="return false">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>Amount Rs. <span style="color: red;">*</span></label>
                                                        <input name="cttxtDOB" type="text" id="txtDOB" class="form-control" placeholder="Enter Amount Rs.">
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <label>&nbsp;</label>
                                                        <asp:Button runat="server" CssClass="btn btn-info btn-block" Text="Add" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label>Narration:</label>
                                                        <textarea placeholder="Enter Narration" class="form-control" style=""></textarea>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <fieldset class="box-body">
                                                        <legend>Action </legend>
                                                        <div class="col-md-2">
                                                            <div class="form-group">
                                                                <input type="submit" name="ctl00$ContainerBody$btnSubmit" value="Accept" onclick="return validateform();" id="ctl00_ContainerBody_btnSubmit" class="btn btn-block btn-primary">
                                                            </div>
                                                        </div>
                                                        <div class="col-md-2">
                                                            <div class="form-group">
                                                                <input type="submit" name="ctl00$ContainerBody$btnSubmit" value="Cancel" onclick="return validateform();" id="ctl00_ContainerBody_btnSubmit" class="btn btn-block btn-primary">
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
