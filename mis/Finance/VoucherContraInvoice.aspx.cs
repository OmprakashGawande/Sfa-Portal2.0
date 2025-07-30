using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;


public partial class mis_Finance_VoucherContraInvoice : System.Web.UI.Page
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

            ds = objdb.ByProcedure("SpFinPrintVoucher", new string[] { "flag", "VoucherTx_ID", "Office_ID" }, new string[] { "2", ViewState["VoucherTx_ID"].ToString(), ViewState["Office_ID"].ToString() }, "dataset");

           if (ds != null)
            {
                VoucherTx_Amount = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
                Amount = GenerateWordsinRs(VoucherTx_Amount);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    htmlStr.Append("<div class='invoice p-3 mb-3'>");
                    htmlStr.Append("<div class='row'>");
                    htmlStr.Append("<div class='col-12'>");
                    htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:20px;'>M.P. State Minor Forest Produce(T & D)Co-op. Fed. Ltd.<br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</span><br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() + "  VOUCHER</span> </h2>");

                    htmlStr.Append("</div>");
                    htmlStr.Append("</div>");
                    htmlStr.Append("<div class='invoice p-3 mb-3'>");
                    htmlStr.Append("<div class='row'>");
                    htmlStr.Append("<div class='col-12'>");
                    htmlStr.Append("<label class='lead'>Voucher No&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_No"].ToString() + "</label><label class='float-right lead'>Date:&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString() + "</label>");
                    htmlStr.Append("</div>");
                    htmlStr.Append("</div>");
                   
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    htmlStr.Append(" <div class='row'>");
                    htmlStr.Append("<div class='col-12 table-responsive'>");
                    htmlStr.Append("<table class='table no-border'>");                    
                    htmlStr.Append("<tbody>");

                    int count = ds.Tables[1].Rows.Count;

                    for (int i = 0; i < count; i++)
                    {
                        
                        if (i == 0)
                        {
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td class='lead' style='width:2px'>DEBIT</td>");
                            htmlStr.Append("<td class='small'  style='width:80px'>" + ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "</td>");
                            htmlStr.Append("<td class='small' align='center' style='width:80px'>" + ds.Tables[1].Rows[i]["LedgerTx_Debit"].ToString() + "</td>");
                            htmlStr.Append("<td align='center' style='width:80px'></td>");
                            htmlStr.Append("</tr>");
                        }
                        else
                        {
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td class='lead' style='width:2px'></td>");
                            htmlStr.Append("<td class='small'  style='width:80px'>" + ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "</td>");
                            htmlStr.Append("<td class='small' align='center' style='width:80px'>" + ds.Tables[1].Rows[i]["LedgerTx_Debit"].ToString() + "</td>");
                            htmlStr.Append("<td align='center' style='width:80px'></td>");
                            htmlStr.Append("</tr>");
                        }
                        
                        
                    }
                    int count1 = ds.Tables[2].Rows.Count;
                    for (int i = 0; i < count1; i++)
                    {
                        
                        if (i == 0)
                        {
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td class='lead' style='width:10px'>CREDIT</td>");
                            htmlStr.Append("<td class='small' style='width:80px'>" + ds.Tables[2].Rows[i]["Ledger_Name"].ToString() + "</td>");
                            htmlStr.Append("<td  align='center' style='width:80px'></td>");
                            htmlStr.Append("<td align='center' class='small' style='width:80px'>" + ds.Tables[2].Rows[i]["LedgerTx_Credit"].ToString() + "</td>");
                            htmlStr.Append("</tr>");
                        }
                        else
                        {
                            htmlStr.Append("<tr>");
                            htmlStr.Append("<td class='lead' style='width:10px'></td>");
                            htmlStr.Append("<td class='small' style='width:80px'>" + ds.Tables[2].Rows[i]["Ledger_Name"].ToString() + "</td>");
                            htmlStr.Append("<td  align='center' style='width:80px'></td>");
                            htmlStr.Append("<td align='center' class='small' style='width:80px'>" + ds.Tables[2].Rows[i]["LedgerTx_Credit"].ToString() + "</td>");
                            htmlStr.Append("</tr>");
                        }
                        
                    }
                    if(ds.Tables[4].Rows.Count> 0)
                    {
                        
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='lead' style='width:10px'>PAY TO&nbsp;&nbsp;<span class='small'>" + ds.Tables[4].Rows[0]["FavouringName"].ToString() + "</span></td>");
                        htmlStr.Append("<td style='width:80px'></td>");
                        htmlStr.Append("<td style='width:80px'></td>");
                        htmlStr.Append("<td style='width:80px'></td>");
                        htmlStr.Append("</tr>");
                    }
                    else
                    {
                        htmlStr.Append("<tr>");
                        htmlStr.Append("<td class='lead' style='width:10px'>PAY TO</td>");
                        htmlStr.Append("<td style='width:80px'></td>");
                        htmlStr.Append("<td style='width:80px'></td>");
                        htmlStr.Append("<td style='width:80px'></td>");
                        htmlStr.Append("</tr>");
                    }
                   
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td style='width:10px'></td>");
                    htmlStr.Append("<td class='lead' style='width:80px' align='left'>PARTICULARS</td>");
                    htmlStr.Append("<td style='width:80px'></td>");
                    htmlStr.Append("<td class='lead' align='center' style='width:80px'>AMOUNT</td>");  
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");                   
                    htmlStr.Append("<td class='small' colspan='3'>" + ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString() + "</td>");
                    htmlStr.Append("<td style='border-left: 2px solid black' class='small' align='center' rowspan='2'>" + ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString() + "</td>");                   
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td colspan='3' ><span class='lead'>Rs:&nbsp;&nbsp;</span><span class='small'>" + Amount + "</span></td>");
                    //htmlStr.Append("<td  style='border-left: 2px solid black' class='small' align='center'>" "</td>");                  
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr>");
                    
                    if (ds.Tables[3].Rows.Count > 0)
                    {
                        htmlStr.Append("<td style='width:10px' class='lead'>CHEQUE NO.&nbsp;&nbsp;");
                        int cheqcount = ds.Tables[3].Rows.Count;
                        for (int i = 0; i < cheqcount; i++)
                        {
                            htmlStr.Append("<span class='small'>" + ds.Tables[3].Rows[i]["ChequeTx_No"].ToString() + "</span>&nbsp&nbsp");

                        }
                        htmlStr.Append("</td>");
                        htmlStr.Append("<td class='lead' style='width:80px' align='center'>DATE&nbsp;&nbsp;");                        
                        for (int i = 0; i < cheqcount; i++)
                        {
                            htmlStr.Append("<span class='small'>" + ds.Tables[3].Rows[i]["ChequeTx_Date"].ToString() + "</span>&nbsp&nbsp");
                        }
                        htmlStr.Append("</td>");
                    }
                    else
                    {
                        htmlStr.Append("<td style='width:10px' class='lead'>CHEQUE NO.</td>");
                        htmlStr.Append("<td class='lead' style='width:80px' align='center'>DATE</td>");
                    }
                    
                    htmlStr.Append("<td class='lead' style='width:80px' align='right'>TOTAL</td>");
                    htmlStr.Append("<td style='border-left: 2px solid black' class='small' align='center'>" + ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString() + "</td>");
                    htmlStr.Append("</tr>");
                    htmlStr.Append("<tr >");
                    htmlStr.Append("<td style='padding-top:100px;' class='small'>Prepared by</td>");
                    htmlStr.Append("<td style='padding-top:100px;' class='small'colspan='2' align='center'>Signature of Passing Authority</td>");
                    htmlStr.Append("<td class='small' style='padding-top:100px;' align='right'>Receiver's Sign.</td>");
                   
                    htmlStr.Append("</tr>");
                    htmlStr.Append("</tbody>");
                    htmlStr.Append("</table>");
                    htmlStr.Append("</div>");
                    htmlStr.Append("</div>");
                }
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