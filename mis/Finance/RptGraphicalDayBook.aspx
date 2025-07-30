<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptGraphicalDayBook.aspx.cs" Inherits="mis_Finance_RptGraphicalDayBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
   <script src="https://code.highcharts.com/highcharts.js"></script>
<script src="https://code.highcharts.com/modules/data.js"></script>
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
                 <div id="container" style="min-width: 310px; max-width: 800px; height: 400px; margin: 0 auto"></div>
                </div>
           
        </section>
    </div>
   
<div id="divchart" class="hidden" runat="server"></div>
    <asp:HiddenField ID="hfyear" runat="server" />
    <asp:HiddenField ID="hfoffice" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hfDate" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
   
 
    <script>
        var hfoffice = '<%= hfoffice.Value%>'
        var hfDate = '<%= hfDate.Value%>'
        Highcharts.chart('container', {
            data: {
                table: 'datatable'
            },
            chart: {
                type: 'column'
            },
            title: {
                text: 'Graphical Representation Of DayBook (' + hfDate + ')<br> (' + hfoffice + ')'
            },
            
            yAxis: {
                min: 0,
                tickInterval: 2,
                allowDecimals: true,
                title: {
                    text: 'No of Voucher'
                }
            },

            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.name + '</b><br/>' +
                      this.point.y + ' ' + this.point.name.toLowerCase();
                }
            }
        });
   </script>
</asp:Content>



