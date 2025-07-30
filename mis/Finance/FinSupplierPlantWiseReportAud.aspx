<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="FinSupplierPlantWiseReportAud.aspx.cs" Inherits="mis_Finance_FinSupplierPlantWiseReportAud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            padding: 3px !important;
        }
          @media print {
            .Hiderow, .main-footer {
                display: none;
            }

            .box {
                border: none;
            }

           
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">

        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-success">
                        <div class="box-header Hiderow">
                            <h3 class="box-title">Audit Plant Wise Supply Details (By Order Date)</h3>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <div class="box-body">
                            <div class="row Hiderow">
                                 <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Plant<span style="color: red;"> *</span></label>
                                            <asp:DropDownList ID="ddlPlant" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                    </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>From Date<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtFromDate" placeholder="Enter From Date..." autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>To Date<span style="color: red;"> *</span></label>
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <asp:TextBox runat="server" CssClass="form-control DateAdd" ID="txtToDate" placeholder="Enter To Date..." autocomplete="off" ClientIDMode="Static"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Button runat="server" CssClass="btn btn-block btn-success" Style="margin-top: 23px;" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>



                            <div class="row">
                                <div class="col-md-12  table-responsive">
                                    <asp:Button ID="btnPrint" runat="server" CssClass="Hiderow" Text="Print" OnClientClick="window.print();" />
                                  <%--  <div class="table-responsive">--%>
                                        <div runat="server" id="DivSupplyData" style="font-size: 12px; font-family: monospace;" ></div>
                                        
                                   <%-- </div>--%>
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










