<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="RptChequePrintHO.aspx.cs" Inherits="mis_Finance_RptChequePrintHO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">

    <style>
        .pay-sheet table tr th, .pay-sheet table tr td {
            font-size: 12px;
            width: 10%;
            border: 1px dashed #ddd;
            padding-left: 1px;
            padding-top: 1px;
            line-height: 14px;
            font-family: monospace;
            overflow: hidden;
        }

        .pay-sheet table {
            width: 100%;
        }

            .pay-sheet table thead {
                background: #eee;
            }

        /*.pay-sheet table {
            border: 1px solid #ddd;
        }*/

        @media print {
            .hide_print, .Hiderow, .main-footer, .dt-buttons, .dataTables_filter {
                display: none;
            }

            /*.box {
                border: none;
            }*/

            th {
                background-color: #ddd;
                text-decoration: solid;
            }

            .tblheadingslip {
                font-size: 8px !important;
                background: black;
                color: red;
            }

            footer {
                position: relative;
                bottom: 0;
            }
        }

        .align-right {
            text-align: right !important;
            width: 10% !important;
        }

        .alignR {
            text-align: right !important;
        }

        span.Ledger_Amt {
            max-width: 30%;
            display: inline;
            float: right;
        }

        span.Ledger_Name {
            max-width: 70%;
            display: inline;
            float: left;
        }

        p.subledger {
            border-top: 1px solid #ccc;
            margin: 0px;
        }

        .report-title {
            font-weight: 600;
            font-size: 15px;
            color: #123456;
        }

        .Scut {
            color: tomato;
        }

        .tab1 td {
            padding: 2px 5px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <!-- Main content -->

        <section class="content">
            <div class="box box-success">
                <div class="box-header Hiderow">
                    <h3 class="box-title">Bank Cheque Print</h3>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
                <div class="box-body">

                    <div class="row Hiderow">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>&nbsp;</label><br />
                                <asp:CheckBox ID="chkAcPayee" runat="server" Text="A/c Payee" />

                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Cheque  Date</label><span style="color: red">*</span>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtInstrumentDate" runat="server" placeholder="Select Instrument Date.." CssClass="form-control DateAdd" autocomplete="off" data-provide="datepicker" onpaste="return false" ClientIDMode="Static"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <label>Beneficiary Name<span style="color: red;">*</span></label>
                                <asp:TextBox ID="txtBeneficiaryName" runat="server" placeholder="Enter Beneficiary Name..." class="form-control" autocomplete="off" onpaste="return false" ClientIDMode="Static" MaxLength="200"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row Hiderow">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Amount</label><span style="color: red">*</span>
                                <asp:TextBox ID="txtAmount" runat="server" placeholder="Enter Amount.." class="form-control" autocomplete="off" onpaste="return false" ClientIDMode="Static" onkeypress="return validateDec(this,event)" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <label>&nbsp;</label><br />
                            <asp:ImageButton ID="btnPnb" Style="height: 25px; width: 50px;" ImageUrl="../../images/pnb_logo.png" runat="server" OnClick="btnPnb_Click" OnClientClick="validateform();" />
                        </div>
                        <div class="col-md-1">
                            <label>&nbsp;</label><br />
                            <asp:ImageButton ID="btnSbi" Style="height: 25px; width: 50px;" ImageUrl="../../images/sbi_logo.png" runat="server" OnClick="btnSbi_Click" OnClientClick="validateform();" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">

    <script>

        function validateform() {
            var msg = "";
           
            if (document.getElementById('<%=txtInstrumentDate.ClientID%>').value.trim() == "") {
                msg = msg + "Select Cheque  Date. \n";
            }
            if (document.getElementById('<%=txtBeneficiaryName.ClientID%>').value.trim() == "") {
                msg = msg + "Select Beneficiary Name. \n";
            }

            if (document.getElementById('<%=txtAmount.ClientID%>').value.trim() == "") {
                msg = msg + "Enter Amount. \n";
            }
            if (msg != "") {

                var amount = document.getElementById('<%=txtAmount.ClientID%>').value.trim();

                document.getElementById('<%=txtAmount.ClientID%>').value = parseFloat(amount).toFixed(2)
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }




    </script>


</asp:Content>



