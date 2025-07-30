<%@ Page Title="" Language="C#" MasterPageFile="~/mis/MainMaster.master" AutoEventWireup="true" CodeFile="VoucherDate.aspx.cs" Inherits="mis_Finance_VoucherDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHeader" runat="Server">
    <style>
        .datepicker{
            z-index:9999 !important; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentBody" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="box box-success">
                <div class="box-header">
                    <h3 class="box-title">Voucher Date</h3>
                </div>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-3 hidden" >
                            <div class="form-group">
                                <label>Financial Year</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtFY" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Voucher Date<span style="color:red;">*</span></label>
                                <div class="input-group date">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <asp:TextBox ID="txtVoucherdate" AutoComplete="off" placeholder="Select Date" ClientIDMode="Static" runat="server" data-date-end-date="0d" class="form-control DateAdd"></asp:TextBox>
                                   
                                </div>
                                <small><span id="valtxtVoucherdate" style="color: red;"></span></small>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success btn-block" ClientIDMode="Static" Style="margin-top: 25px;" OnClientClick="return validateform();" OnClick="btnSave_Click"/>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentFooter" runat="Server">
    <script>
        function validateform()
        {
            var msg = "";
            $("valtxtVoucherdate").html("");
            if (document.getElementById('<%=txtVoucherdate.ClientID%>').value.trim() == "")
            {
                msg = "Select Voucher Date."
                $("#valtxtVoucherdate").html("Select Voucher Date");
            }
            if(msg != "")
            {
                alert(msg);
                return false;
            }
            else
            {
                    document.querySelector('.popup-wrapper').style.display = 'block';
                    return true;
            }
        }
        
       
    </script>
</asp:Content>

