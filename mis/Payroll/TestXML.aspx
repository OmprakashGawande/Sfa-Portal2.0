<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="TestXML.aspx.cs" Inherits="mis_Payroll_TestXML" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" Runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->
        <section class="content">
            <!-- Default box -->
            <div class="box box-success" style="min-height: 500px;">
                <div class="box-header">
                    <h3 class="box-title">Genereate XML</h3>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                             <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                        <asp:LinkButton ID="lnkbtngenerate" CssClass="btn btn-success btn-block" OnClick="lnkbtngenerate_Click" Text="Generate" runat="server"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" Runat="Server">
</asp:Content>

