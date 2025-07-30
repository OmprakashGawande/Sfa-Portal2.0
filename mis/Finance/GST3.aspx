<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GST3.aspx.cs" Inherits="mis_Finance_GST3" %>

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
                    <table border="1">
                        <tr>
                            <th>Table No.</th>
                            <th>Particulars</th>
                            <th>Taxable Values</th>
                            <th>Integrated Tax Amount</th>
                            <th>Central Tax Amount</th>
                            <th>State Tax Amount</th>
                            <th>Cess Amount</th>
                            <th>Tax Amount</th>
                        </tr>
                        <tr>
                            <td>&nbsp;3.1</td>
                            <td>Outward Supplies and Inward supplies Liable to Reverse Charge</td>
                            <td>31,58,30,423.66</td>
                            <td></td>
                            <td>1,88,07,251.29</td>
                            <td>1,88,07,251.29</td>
                            <td></td>
                            <td>3,76,14,502.58</td>
                        </tr>
                        <tr>
                            <td>&nbsp;a</td>
                            <td>&nbsp;&nbsp;&nbsp;Outward Taxable Supplies (other than Zero Rated, nil rated and exempted) </td>
                            <td>30,37,59,702.16</td>
                            <td></td>
                            <td>1,88,07,251.29</td>
                            <td>1,88,07,251.29</td>
                            <td></td>
                            <td>3,76,14,502.58</td>
                        </tr>
                        <tr>
                            <td>&nbsp;b</td>
                            <td>&nbsp;&nbsp;&nbsp;Outward Taxable Supplies (Zero Rated)</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;c</td>
                            <td>&nbsp;&nbsp;&nbsp;Other Outward Supplies (Nil rated, exempted)</td>
                            <td>1,20,70,721.50</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;d</td>
                            <td>&nbsp;&nbsp;&nbsp;Inward Supplies (Liable to reverse Charge)</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;e</td>
                            <td>&nbsp;&nbsp;&nbsp;Non-GST Outward Supplies</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;3.2</td>
                            <td>Of the Supplies shown in 3.1(a) above, details of interstate supplies made of unregistered persons, Composition taxable persons and UIN holders</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>&nbsp;&nbsp;&nbsp;Supplies made to Unregisterd persons</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>&nbsp;&nbsp;&nbsp;Supplies made to Composition Taxable Person</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>&nbsp;&nbsp;&nbsp;Supplies made to UIN Holder</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;4</td>
                            <td>Eligible ITC</td>
                            <td></td>
                            <td>13,136.11</td>
                            <td>1,81,59,533.77</td>
                            <td>1,81,59,533.77</td>
                            <td></td>
                            <td>3,36,32,203.65</td>
                        </tr>
                        <tr>
                            <td>&nbsp;A</td>
                            <td>&nbsp;&nbsp;&nbsp;ITC Available (Weather in full or Part)</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;(1)</td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Import of goods</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;(2)</td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Import of Services</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;(3)</td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Inward Supplies Liable to reverse Charge (other than 1 & 2 above)</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;(4)</td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Inward Supplies from ISD</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;(5)</td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;All other ITC</td>
                            <td></td>
                            <td>13,136,11</td>
                            <td>1,81,59,533.77</td>
                            <td>1,81,59,533.77</td>
                            <td></td>
                            <td>3,63,32,203.65</td>
                        </tr>
                        <tr>
                            <td>&nbsp;B</td>
                            <td>&nbsp;&nbsp;&nbsp;ITC Reversed</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;(1)</td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;As per rules 42 & 43 of CGST Rules</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;(2)</td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Others</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;C</td>
                            <td>&nbsp;&nbsp;Net ITC Available (A) - (B)</td>
                            <td></td>
                            <td>13,136,11</td>
                            <td>1,81,59,533.77</td>
                            <td>1,81,59,533.77</td>
                            <td></td>
                            <td>3,63,32,203.65</td>
                        </tr>
                        <tr>
                            <td>&nbsp;D</td>
                            <td>&nbsp;&nbsp;Ineligible ITC</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;(1)</td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;As per section 17(5)</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;(2)</td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Others</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;5</td>
                            <td>&nbsp;&nbsp;Value of Exempt, nil rated and non-GST inward supplies</td>
                            <td>1,14,94,593.50</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;From a supplies under Composition scheme, exempt and nil rated supply</td>
                            <td>1,14,94,593.50</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Non GST Supply</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;5.1</td>
                            <td>&nbsp;&nbsp;Interest and late fee Payable</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Interest</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Late Fees</td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
