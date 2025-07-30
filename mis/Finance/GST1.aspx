<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GST1.aspx.cs" Inherits="mis_Finance_GST1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <p style="text-align: center">
                        <strong>M.P State Agro Id Corpn,Jhabua<br />
                            GSTR-1</strong><br />
                        1-JUL-2017 TO 13-AUG-2018
                    </p>
                </div>
            </div>
            <div class="row">
                <div class="col-md-1 pull-right">Page 1</div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <label>GSTIN/UIN</label>: 23AACCM0330Q1ZM
                </div>
                <div class="col-md-2 pull-right">
                    <label>1-Jul-2017 to 13-Aug-2018</label>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <label>Returns Summary</label>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-4">
                    <label>Total number of vouchers for the period</label>
                </div>
                <div class="col-md-1 pull-right">
                    <label>1,344</label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <table class="table">
                        <tr>
                            <th>SINO.</th>
                            <th>Particulars</th>
                            <th>Voucher Count</th>
                            <th>Taxable Value</th>
                            <th>Itegrated Tax Amount</th>
                            <th>Central Tax Amount</th>
                            <th>State Tax Amount</th>
                            <th>Cess Amount</th>
                            <th>Tax Amount</th>
                            <th>Invoice Amount</th>
                        </tr>
                        <tr>
                            <td>1</td>
                            <td>B2B Invoices - 4A,4B,4C,6B,6C</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>2</td>
                            <td>B2C(Large) Invoices - 5A,5B</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>3</td>
                            <td>B2C(Small) Invoices - 7</td>
                            <td>183</td>
                            <td>30,62,13,073.36</td>
                            <td></td>
                            <td>1,91,50,723.26</td>
                            <td>1,91,50,723.26</td>
                            <td></td>
                            <td>3,83,01,446.52</td>
                            <td>34,45,14,519.00</td>
                        </tr>
                        <tr>
                            <td>4</td>
                            <td>Credit/Debit Notes(Registered) - 9B</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>5</td>
                            <td>Credit/Debit Notes(Unregistered) - 9B</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>6</td>
                            <td>Exports Invoices - 6A</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>7</td>
                            <td>Tax Liability(Advances recieved) - 11A(1),11A(2)</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>8</td>
                            <td>Adjustment of Advances - 11B(1),11B(2)</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>9</td>
                            <td>Nil Rated Invoices - 8A,8B,8C,8D</td>
                            <td>35</td>
                            <td>1,20,70,721.50</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>1,20,70,722.00</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td><b>Total</td>
                            </b>
                            <td><b>218</b></td>
                            <td><b>31,82,83,794,86</b></td>
                            <td></td>
                            <td><b>1,91,50,723.26</b></td>
                            <td><b>1,91,50,723.26</b></td>
                            <td></td>
                            <td><b>3,83,01,446.52</b></td>
                            <td><b>35,65,85,241.00</b></td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        HSN/SAC Summary - 12
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        Document Summary - 13
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
