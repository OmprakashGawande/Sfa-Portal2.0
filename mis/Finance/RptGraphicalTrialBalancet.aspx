<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptGraphicalTrialBalancet.aspx.cs" Inherits="mis_Finance_RptGraphicalTrialBalancet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
 <script src="https://code.highcharts.com/highcharts.js"></script>
<script src="https://code.highcharts.com/modules/exporting.js"></script>
<script src="https://code.highcharts.com/modules/export-data.js"></script>
    <style>
        .highcharts-button-symbol
        {
            display:none;
        }
        .highcharts-credits
         {
            display:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            
            <div class="box box-success">
                <div class="box-header Hiderow">                  
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>  
                <div class="row">
                        <div class="col-md-12">
                            <asp:Label ID="lblheadingFirst" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                <div class="box-body">
                    <div class="col-md-6">
                        <div id="container" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                <div id="divchartCr" runat="server">
                    </div>
                </div>         
                <div class="box-body">
                    <div class="col-md-6">
                        <div id="container1" style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"></div>
                <div id="divchartDr" runat="server">
                    </div>
                </div>
                </div>
                </div>
                </div>
        </section>
    </div>  
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
 <script>

 </script>
</asp:Content>