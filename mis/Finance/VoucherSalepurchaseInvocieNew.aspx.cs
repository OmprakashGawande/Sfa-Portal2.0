using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using QRCoder;
using System.Drawing;
using System.IO;


public partial class mis_Finance_VoucherSalepurchaseInvocieNew : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, Dset;
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //lblMsg.Text = "";
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {

                if (!IsPostBack)
                {

                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    //FillPrint1();
                    if (Request.QueryString["VoucherTx_ID"] != null)
                    {
                        ViewState["VoucherTx_ID"] = objdb.Decrypt(Request.QueryString["VoucherTx_ID"].ToString());
                        FillPrint();

                    }

                }
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillPrint()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinSalePurchasePrint", new string[] { "flag", "VoucherTx_ID" }, new string[] { "0", ViewState["VoucherTx_ID"].ToString() }, "dataset");
            if (ds != null)
            {
                decimal CRAmount = 0;
                decimal DRAmount = 0;
                StringBuilder sb = new StringBuilder();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //CashSale Vouche
                    spnbranch.InnerHtml = ds.Tables[0].Rows[0]["Office_Name"].ToString();
                    spnemail.InnerHtml = ds.Tables[0].Rows[0]["Office_Email"].ToString();
                    OffcAddress.InnerHtml = ds.Tables[0].Rows[0]["Office_Address"].ToString();
                    spninvno.InnerHtml = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                    spndate.InnerHtml = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                    spnorderno.InnerHtml = ds.Tables[0].Rows[0]["VoucherTx_OrderNo"].ToString();
                    spnorddate.InnerHtml = ds.Tables[0].Rows[0]["VoucherTx_OrderDate"].ToString();
                    spnschemename.InnerHtml = ds.Tables[0].Rows[0]["SchemeTx_Name"].ToString();
                    spnNarration.InnerHtml = ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString();
                    spnbankname.InnerHtml = ds.Tables[0].Rows[0]["Bank_Name"].ToString();
                    spnactno.InnerHtml = ds.Tables[0].Rows[0]["Acnt_No"].ToString();
                    spnifsccode.InnerHtml = ds.Tables[0].Rows[0]["IFSC"].ToString();
                    spnRegNo.InnerHtml = ds.Tables[0].Rows[0]["VoucherTx_RegNo"].ToString();

                }

                if (ds.Tables[2].Rows.Count > 0)
                {
                    int Count = ds.Tables[2].Rows.Count;


                    for (int i = 0; i < Count; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; text-align:center; line-height:1.1'>" + (i + 1).ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:left; line-height:1.1'>" + ds.Tables[2].Rows[i]["ItemName"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:center; line-height:1.1'>" + ds.Tables[2].Rows[i]["HSN_Code"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["Rate"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["Quantity"].ToString() + " " + ds.Tables[2].Rows[i]["UnitName"].ToString() + "</td>");//+ "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["Amount"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["PER"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["CGSTAmt"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["SGSTAmt"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["TOTALAMOUNT"].ToString() + "</td>");
                        sb.Append("</tr>");

                    }

                    CRAmount = ds.Tables[2].AsEnumerable().Sum(row => row.Field<decimal>("Amount"));
                }
                sb.Append("<tr>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1 '></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("<td style='padding-bottom:30px; border-bottom:1px solid black; border-right:1px solid black; border-left:1px solid black; line-height:1.1'></td>");
                sb.Append("</tr>");
                if (ds.Tables[3].Rows.Count > 0)
                {
                    int LedgerCount = ds.Tables[3].Rows.Count;
                    sb.Append("<tr>");
                    sb.Append("</tr>");
                    for (int i = 0; i < LedgerCount; i++)
                    {
                        sb.Append("<tr>");
                        sb.Append("<td class='PSemibold cssborder' colspan='5'  style='text-align:left'>" + ds.Tables[3].Rows[i]["Ledger_Name"].ToString() + "(" + ds.Tables[3].Rows[i]["CRDR"].ToString() + ")</td>");
                        sb.Append("<td class='PSemibold cssborder'   style='border-left:1px solid black; text-align:right'>" + ds.Tables[3].Rows[i]["LedgerTx_Credit"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold cssborder' colspan='4'  style='border-left:1px solid black; text-align:right'>" + ds.Tables[3].Rows[i]["LedgerTx_Debit"].ToString() + "</td>");
                        sb.Append("</td>");
                        sb.Append("</tr>");

                    }

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    int DebtorCount = ds.Tables[1].Rows.Count;
                    string HTML = "";
                    for (int i = 0; i < DebtorCount; i++)
                    {
                        if (ds.Tables[1].Rows[i]["Mailing_Address"].ToString() != "")
                        {
                            HTML += ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "<br>" + ds.Tables[1].Rows[i]["Mailing_Address"].ToString() + "<br>" + "<b>GSTIN </b>" + " " + ds.Tables[1].Rows[i]["GST_No"].ToString() + "<br>" + "<b>Sold To </b>" + " " + ds.Tables[0].Rows[0]["VoucherTx_SoldTo"].ToString() + "<br>";
                        }
                        else
                        {
                            HTML += ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "<br>" + "<b>GSTIN</b>" + " " + ds.Tables[1].Rows[i]["GST_No"].ToString() + "<br>" + "<b>Sold To </b>" + " " + ds.Tables[0].Rows[0]["VoucherTx_SoldTo"].ToString() + "<br>";
                        }
                    }
                    spnto.InnerHtml = HTML.ToString();




                }
                if (ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() == "GSTGoods Purchase" || ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() == "Goods Purchase Tax Free")
                {
                    DRAmount = ds.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_CreditAmt"));
                }
                else
                {
                    DRAmount = ds.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_DebitAmt"));
                }
                if (ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() == "GSTGoods Purchase" || ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() == "Goods Purchase Tax Free")
                {
                    CRAmount = CRAmount + ds.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_DebitAmt"));
                }
                else
                {
                    CRAmount = CRAmount + ds.Tables[3].AsEnumerable().Sum(row => row.Field<decimal>("LedgerTx_CreditAmt"));
                }

                spnAmount.InnerHtml = GenerateWordsinRs(DRAmount.ToString());
                sb.Append("<tr>");
                sb.Append("<td class='cssborder Pbold' colspan='5' style='text-align:right'>GRAND TOTAL</td>");
                //sb.Append("<td class='cssborder Pbold' colspan='5' style='text-align:center'>" + CRAmount.ToString() + "</td>");
                sb.Append("<td class='cssborder Pbold' colspan='5' style='text-align:center'>" + ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString() + "</td>");
                sb.Append("</tr>");
                divitem.InnerHtml = sb.ToString();

                DataSet ds_subitem = objdb.ByProcedure("SpFinSalePurchasePrint", new string[] { "flag", "VoucherTx_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString() }, "dataset");
                if (ds_subitem.Tables[0].Rows.Count > 0)
                {
                    if ((ds_subitem.Tables[0].Rows[0]["Office_QrPa"].ToString() != "0") && (ds_subitem.Tables[0].Rows[0]["Office_QrPa"].ToString() != "0"))
                    {

                        /*************START OTHER DETAIL*************/
                        string QR_pa = ds_subitem.Tables[0].Rows[0]["Office_QrPa"].ToString();
                        string QR_pn = ds_subitem.Tables[0].Rows[0]["Office_QrPn"].ToString();
                        /*************END***************************/
                        /*************QR CODE*************/
                        string QR_mc = "1";
                        string QR_tr = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                        string QR_url = Request.Url.AbsoluteUri;
                        string QR_am = "";
                        //DRAmount.ToString();
                        string QR_tid = ViewState["VoucherTx_ID"].ToString();
                        string QR_tn = "Purchase in Merchant ";
                        string QR_gstIn = "23CAIPP4329R12";
                        string QR_gstBrkUp = CRAmount.ToString();//ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                        string QR_invoiceNo = ds.Tables[0].Rows[0]["VoucherTx_No"].ToString();
                        string QR_invoiceDate = ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString();
                        //QR_tn = QR_tn + " INV No  " + QR_invoiceNo;
                        //string code = "upi://pay?pa=" + QR_pa.Trim() + "&pn=" + QR_pn.Trim() + "&mc=" + QR_mc.Trim() + "&tr=" + QR_tr.Trim() + "&am=" + QR_am.Trim() + "&cu=INR&tid=" + QR_tid.Trim() + "&gstIn=" + QR_gstIn.Trim() + "&tn=" + QR_tn.Trim() + "&gstBrkUp=" + QR_gstBrkUp.Trim() + "&invoiceNo=" + QR_invoiceNo.Trim() + "&invoiceDate=" + QR_invoiceDate.Trim() + ""; //&url=" + QR_url.Trim() + "
                        string code = "upi://pay?pa=" + QR_pa.Trim() + "&pn=" + QR_pn.Trim() + "&mc=1&tr=" + QR_invoiceNo + "&am=" + QR_am.Trim() + "&cu=INR&tid=" + QR_tid.Trim() + "&gstIn=23CAIPP4329R12&tn=Purchase in  Merchant&invoiceNo=" + QR_invoiceNo.Trim() + "&invoiceDate=" + QR_invoiceDate.Trim() + "";
                        //lblGram.Text = "<b>Gram : </b>" + code.ToString();
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                        imgBarCode.Height = 170;
                        imgBarCode.Width = 170;
                        //lblSado.Text = "<b>SADO : </b>" + code.ToString();
                        using (Bitmap bitMap = qrCode.GetGraphic(20))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                byte[] byteImage = ms.ToArray();
                                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                            }
                            PlaceHolder1.Controls.Add(imgBarCode);
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    private string GenerateWordsinRs(string value)
    {
        decimal numberrs = Convert.ToDecimal(value);
        CultureInfo ci = new CultureInfo("en-IN");
        string aaa = String.Format("{0:#,##0.##}", numberrs);
        aaa = aaa + " " + ci.NumberFormat.CurrencySymbol.ToString();
        // label6.Text = aaa;


        string input = value;
        string a = "";
        string b = "";

        // take decimal part of input. convert it to word. add it at the end of method.
        string decimals = "";

        if (input.Contains("."))
        {
            decimals = input.Substring(input.IndexOf(".") + 1);
            // remove decimal part from input
            input = input.Remove(input.IndexOf("."));

        }
        string strWords = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(input));

        if (!value.Contains("."))
        {
            a = strWords + " Rupees Only";
        }
        else
        {
            a = strWords + " Rupees";
        }

        if (decimals.Length > 0)
        {
            // if there is any decimal part convert it to words and add it to strWords.
            string strwords2 = NUMBERTOWORDS.NumbersToWords(Convert.ToInt32(decimals));
            b = " and " + strwords2 + " Paisa Only ";
        }

        return a + b;
    }

}