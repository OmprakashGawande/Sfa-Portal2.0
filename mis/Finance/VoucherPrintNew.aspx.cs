using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_Finance_VoucherPrintNew : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    CommanddlFill objddl = new CommanddlFill();
    DataSet ds, Dset;
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            //ViewState["VoucherTx_ID"] = 74;
            //FillPrint();
            //lblMsg.Text = "";
            if (Session["Emp_ID"] != null && Session["Office_ID"] != null)
            {

                if (!IsPostBack)
                {

                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    //74
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
            //lblMsg.Text = "";
            StringBuilder htmlStr = new StringBuilder();
            string VoucherTx_Amount = "";
            string Amount = "";
            //htmlStr.Append("<div class='invoice p-3 mb-3'>");
            //htmlStr.Append("<div class='row'>");
            //htmlStr.Append("<div class='col-12'>");
            //htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:50px;'>The M.P State Agro Ind. Dev. Corp. Ltd.</h2>");

            //htmlStr.Append("</div>");
            //htmlStr.Append("</div>");

            ds = objdb.ByProcedure("SpFinPrintVoucher", new string[] { "flag", "VoucherTx_ID"}, new string[] { "3", ViewState["VoucherTx_ID"].ToString() }, "dataset");

            if (ds != null)
            {
                VoucherTx_Amount = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                Amount = GenerateWordsinRs(VoucherTx_Amount);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    htmlStr.Append("<table border='0'>");
                    htmlStr.Append("<thead class='header'>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='3' >");
                  
                    //htmlStr.Append("<div class='invoice p-3 mb-3'>");
                    //htmlStr.Append("<div class='row'>");
                    //htmlStr.Append("<div class='col-12'>");
                    htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:20px;'>M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd.<br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</span><br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() + "  VOUCHER</span> </h2>");
                    htmlStr.Append("</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='3' style='border-bottom:1px solid black;'>");
                    //htmlStr.Append("</div>");
                    //htmlStr.Append("</div>");
                    //htmlStr.Append("<div class='invoice p-3 mb-3'>");
                    //htmlStr.Append("<div class='row'>");
                    //htmlStr.Append("<div class='col-12'>");
                    htmlStr.Append("<label class='lead'>Voucher No&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_No"].ToString() + "</label><label class='float-right lead'>Date:&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString() + "</label>");
                    //htmlStr.Append("</div>");
                    //htmlStr.Append("</div>");
                    htmlStr.Append("</td>");
                    htmlStr.Append("</tr>");
                    
                    htmlStr.Append("</thead>");
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    //htmlStr.Append(" <div class='row'>");
                    //htmlStr.Append("<div class='col-12 table-responsive'>");
                    //htmlStr.Append("<table class='table no-border'>");
                    htmlStr.Append("<tbody>");

                    int count = ds.Tables[1].Rows.Count;
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td style='border-left:1px solid black; padding-left:20px; border-bottom:1px solid black;'>Particulars</td>");
                    htmlStr.Append("<td style='border-right:1px solid black;'></td>");
                    htmlStr.Append("<td style='text-align:right; border-right:1px solid black; border-left:1px solid black;'>Amount</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td style='border-left:1px solid black; border-top:1px solid black;'><b>Account:</b></td>");
                    htmlStr.Append("<td style='border-right:1px solid black; border-top:1px solid black;'></td>");
                    htmlStr.Append("<td style='border-top:1px solid black; border-right:1px solid black; border-left:1px solid black;'></td>");
                    htmlStr.Append("</tr>");
                    for (int i = 0; i < count; i++)
                    {
                        string LedgerID = ds.Tables[1].Rows[i]["Ledger_ID"].ToString();
						 string RowNo = ds.Tables[1].Rows[i]["LedgerTx_OrderBy"].ToString();
                        htmlStr.Append("<td style='text-align:left; border-left:1px solid black; padding-left:20px;'>" + ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "</td>");
                        htmlStr.Append("<td style='border-right:1px solid black;'></td>");
                        htmlStr.Append("<td style='text-align:right; border-right:1px solid black; border-left:1px solid black;'><b>" + ds.Tables[1].Rows[i]["LedgerAmount"].ToString() + " " + ds.Tables[1].Rows[i]["Type"].ToString() + "</b></td>");
                        htmlStr.Append("</tr>");
                        DataView dv = new DataView();
                        dv = ds.Tables[3].DefaultView;
                        dv.RowFilter = "Ledger_ID ='" + LedgerID + "'AND LedgerTx_OrderBy='" + RowNo.ToString() + "'";
                        DataTable dt = dv.ToTable();
                        if (dt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                htmlStr.Append("<tr>");
                                htmlStr.Append("<td style='text-align:left; border-left:1px solid black; padding-left:30px;'><b>" + dt.Rows[j]["SubCategoryName"].ToString() + "&nbsp;&nbsp;" + dt.Rows[j]["CategoryName"].ToString() + "</b></td>");
                                htmlStr.Append("<td style='border-right:1px solid black; width:15%;'><b>" + dt.Rows[j]["Amount"].ToString() + "</b></td>");
                                htmlStr.Append("<td style='border-right:1px solid black; border-left:1px solid black;' ></td>");
                                htmlStr.Append("</tr>");
                            }
                        }

                    }

                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td style='border-left:1px solid black;'><b>Through:</b></td>");
                    htmlStr.Append("<td style='border-right:1px solid black;'></td>");
                    htmlStr.Append("<td style='border-right:1px solid black; border-left:1px solid black;'></td>");
                    htmlStr.Append("</tr>");
                    for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                    {
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td style='text-align:left; border-left:1px solid black; padding-left:20px;'>" + ds.Tables[2].Rows[k]["Ledger_Name"].ToString() + "</td>");
                        htmlStr.Append("<td style='border-right:1px solid black; width:15%;'><b>" + ds.Tables[2].Rows[k]["LedgerAmount"].ToString() + " " + ds.Tables[2].Rows[k]["Type"].ToString() + "</b></b></td>");
                        htmlStr.Append("<td style='border-right:1px solid black; border-left:1px solid black;'></td>");
                        htmlStr.Append("</tr>");
                    }
                }
                htmlStr.Append("<tr>");
                htmlStr.Append("<td style='border-left:1px solid black;'><b>On Account Of:</b></td>");
                htmlStr.Append("<td style='border-right:1px solid black;'></td>");
                htmlStr.Append("<td style='border-right:1px solid black; border-left:1px solid black;'></td>");
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<td style='border-left:1px solid black;'>" + ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString() + "</td>");
                htmlStr.Append("<td style='border-right:1px solid black;'></td>");
                htmlStr.Append("<td style='border-right:1px solid black; border-left:1px solid black;'></td>");
                htmlStr.Append("</tr>");
                if (ds.Tables[4].Rows.Count > 0)
                {
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td style='border-left:1px solid black;'><b>Bank Transaction Details</b></td>");
                    htmlStr.Append("<td style='border-right:1px solid black;'></td>");
                    htmlStr.Append("<td style='border-right:1px solid black; border-left:1px solid black;'></td>");
                    htmlStr.Append("</tr>");
                    for (int l = 0; l < ds.Tables[4].Rows.Count; l++)
                    {
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td style='border-left:1px solid black;'>" + ds.Tables[4].Rows[l]["InstrumentType"].ToString() + "&nbsp;&nbsp;" + ds.Tables[4].Rows[l]["ChequeTx_No"].ToString() + "<span style='padding-left:150px;'>" + ds.Tables[4].Rows[l]["ChequeTx_Date"].ToString() + "</span></td>");
                        htmlStr.Append("<td style='border-right:1px solid black; width:15%;'><b>" + ds.Tables[4].Rows[l]["ChequeTx_Amount"].ToString() + "</b></td>");
                        htmlStr.Append("<td style='border-right:1px solid black; border-left:1px solid black;'></td>");
                        htmlStr.Append("</tr>");
                    }
                }

                htmlStr.Append("<tr>");
                htmlStr.Append("<td style='border-left:1px solid black;'><b>Amount(in words):</b></td>");
                htmlStr.Append("<td style='border-right:1px solid black;'></td>");
                htmlStr.Append("<td style='border-right:1px solid black; border-left:1px solid black;'></td>");
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<td style='text-align:left; border-left:1px solid black; padding-left:60px; border-bottom:1px solid black;'><b>" + Amount + "</b></td>");
                htmlStr.Append("<td style='border-bottom:1px solid black;'></td>");
                htmlStr.Append("<td style='border-right:1px solid black; border-left:1px solid black; border-bottom:1px solid black; border-top:1px solid black; '><b>" + ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString() + "</b></td>");
                htmlStr.Append("</tr>");

                htmlStr.Append("<tr>");
                htmlStr.Append("<td style='padding-top:60px;'><b>Receiver's Signature</b></td>");
                htmlStr.Append("<td colspan='2' style='padding-top:60px;'><b>Authorised Signatory</b></td>");
                
                htmlStr.Append("</tr>");
                htmlStr.Append("<tr>");
                htmlStr.Append("<td style='padding-top:60px;'><b>Prepared By</b><span style='padding-left:200px;'><b>Checked By</b></span></td>");

                htmlStr.Append("<td style='padding-top:60px;'><b>Verified By</b></td>");
                htmlStr.Append("<td></td>");
                htmlStr.Append("</tr>");
                htmlStr.Append("</tbody>");
                htmlStr.Append("</table>");
                //htmlStr.Append("</div>");
                //htmlStr.Append("</div>");

            }



            //htmlStr.Append("<div class='row'>");
            //htmlStr.Append("<div class='col-12'>");
            //htmlStr.Append("<label class='lead'>NARRATION:&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString() + "</span></label>");
            //htmlStr.Append("</div>");
            //htmlStr.Append("</div>");
            //htmlStr.Append("<div class='row'>");
            //htmlStr.Append("<div class='col-12'>");
            //htmlStr.Append("<label class='lead'>AMOUNT (IN WORDS):&nbsp;&nbsp;&nbsp;<span class='small'>" + Amount + "</span></label>");
            //htmlStr.Append("</div>");
            //htmlStr.Append("</div>");
            //htmlStr.Append("<div class='invoice p-3 mb-3'>");
            //htmlStr.Append("<div class='row' style='padding-top:80px;'>");
            //htmlStr.Append("<div class='col-12'>");
            //htmlStr.Append("<span class='lead' style='padding-right:180px;'>Accountant/Asstt.Manager (Account)</span><span class='lead' style='text-center'>Manager (Accounts)</span><span class='float-right lead'>CM/DGM (Accounts)/GM</span>");
            //htmlStr.Append("</div>");
            //htmlStr.Append("</div>");
            //htmlStr.Append("</div>");





            DivTable.InnerHtml = htmlStr.ToString();

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
            b = " and " + strwords2 + " Paise Only ";
        }

        return a + b;
    }
}