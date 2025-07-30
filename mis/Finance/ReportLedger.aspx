<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="ReportLedger.aspx.cs" Inherits="mis_Finance_ReportLedger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <link href="../css/jquery.treegrid.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">All Heads</h3>
                </div>

                <div class="box-body">
                    <table class="table tree">
                        <tbody>
                            <tr>
                                <th>Head Name</th>
                                <th></th>
                                <th>Dr.</th>
                                <th>Cr.</th>
                            </tr>
                            <tr class="treegrid-1 treegrid-collapsed">
                                <td><span class="treegrid-expander glyphicon glyphicon-chevron-right"></span>Liabilities</td>
                                <td>Additional info</td>
                                <td>1200</td>
                                <td>1500</td>
                            </tr>
                            <tr class="treegrid-2 treegrid-parent-1" style="display: none;">
                                <td><span class="treegrid-indent"></span><span class="treegrid-expander"></span>Node 1-1</td>
                                <td>Additional info</td>
                                <td>1200</td>
                                <td>1500</td>
                            </tr>
                            <tr class="treegrid-3 treegrid-parent-1 treegrid-collapsed" style="display: none;">
                                <td><span class="treegrid-indent"></span><span class="treegrid-expander glyphicon-chevron-right glyphicon"></span>Node 1-2</td>
                                <td>Additional info</td>
                                <td>1200</td>
                                <td>1500</td>
                            </tr>
                            <tr class="treegrid-4 treegrid-parent-3" style="display: none;">
                                <td><span class="treegrid-indent"></span><span class="treegrid-indent"></span><span class="treegrid-expander"></span>Node 1-2-1</td>
                                <td>Additional info</td>
                                <td>1200</td>
                                <td>1500</td>
                            </tr>
                            <tr class="treegrid-5 treegrid-expanded">
                                <td><span class="treegrid-expander glyphicon glyphicon-chevron-down"></span>Root node 2</td>
                                <td>Additional info</td>
                                <td>1200</td>
                                <td>1500</td>
                            </tr>
                            <tr class="treegrid-6 treegrid-parent-5">
                                <td><span class="treegrid-indent"></span><span class="treegrid-expander"></span>Node 2-1</td>
                                <td>Additional info</td>
                                <td>1200</td>
                                <td>1500</td>
                            </tr>
                            <tr class="treegrid-7 treegrid-parent-5 treegrid-expanded">
                                <td><span class="treegrid-indent"></span><span class="treegrid-expander glyphicon glyphicon-chevron-down"></span>Node 2-2</td>
                                <td>Additional info</td>
                                <td>1200</td>
                                <td>1500</td>
                            </tr>
                            <tr class="treegrid-8 treegrid-parent-7">
                                <td><span class="treegrid-indent"></span><span class="treegrid-indent"></span><span class="treegrid-expander"></span>Node 2-2-1</td>
                                <td>Additional info</td>
                                <td>1200</td>
                                <td>1500</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script src="../js/jquery.treegrid.js"></script>
    <script src="../js/jquery.treegrid.bootstrap3.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.tree').treegrid();
        });
    </script>
</asp:Content>
