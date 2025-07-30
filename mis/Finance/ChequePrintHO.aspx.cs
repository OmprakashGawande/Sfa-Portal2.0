using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;
using System.Drawing;
using System.IO;

public partial class mis_Finance_ChequePrintHO : System.Web.UI.Page
{
    APIProcedure objdb = new APIProcedure();
    DataSet ds;
    CultureInfo cult = new CultureInfo("gu-IN", true);
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //lblMsg.Text = "";
            if (Request.QueryString["Type"] != null)
            {

                if (!IsPostBack)
                {
                    if (Session["ChkInstrumentDate"] != null && Session["ChkAmount"] != null && Session["ChkAcPayee"] != null && Session["BeneficiaryName"] != null)
                    {
                      
                        string InstrumentDate = Session["ChkInstrumentDate"].ToString().Replace("/", "");
                        string Amount = Session["ChkAmount"].ToString();
                        string AcPayee = Session["ChkAcPayee"].ToString();
                        string BeneficiaryName = Session["BeneficiaryName"].ToString();
                        string Type = objdb.Decrypt(Request.QueryString["Type"].ToString());
                        FillPrint(InstrumentDate, Amount, AcPayee, BeneficiaryName, Type);
                    }
                    else
                    {
                        Response.Redirect("~/mis/Login.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("~/mis/Login.aspx");
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillPrint(string InstrumentDate, string Amount, string AcPayee, string BeneficiaryName, string Type)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
          
            if (Type == "PNB")
            {
                sb.Append(" <style>");
                sb.Append(" body {  ");
                sb.Append(" margin: 0mm 0mm 0mm 0mm;");
                sb.Append(" height: 3.67in; ");
                sb.Append(" width: 8.00in; ");
                sb.Append(" } ");
                sb.Append(" page { ");
                sb.Append(" height: 3.67in; ");
                sb.Append(" width:8.00in; ");
                sb.Append(" } ");
                sb.Append(" .divcss { ");
                sb.Append(" position:fixed;");
                sb.Append("  left:0; ");
                sb.Append("  top:0;  ");
                sb.Append("  height: 3.67in;  ");
                sb.Append("  width: 8.00in;  ");
                sb.Append("  } .amtcss { ");
                sb.Append("  position: fixed; ");
                sb.Append("  top: 1.4in; ");
                sb.Append("  left: 6.2in; ");
                sb.Append("  }");
                sb.Append("  .partycss { ");
                sb.Append("  position: fixed; ");
                sb.Append("  top: 0.85in; ");
                sb.Append("  left: 0.8in; ");
                sb.Append("  font-size:13px; ");
                sb.Append(" } ");
                if (AcPayee == "Yes")
                {
                    sb.Append(" .Linecss{ ");
                    sb.Append("  position: fixed; ");
                    sb.Append("  top: 0.3in; ");
                    sb.Append("  left: 0in; ");
                    sb.Append("  width:120px; ");
                    sb.Append("  height:3px; ");
                    sb.Append("  border-top:1px solid black; ");
                    sb.Append("  border-bottom:1px solid black; ");
                    sb.Append("  transform: rotate(150deg); ");
                    sb.Append("  } ");
                }

                sb.Append(" .Datecss{ ");
                sb.Append("  position: fixed; ");
                sb.Append("  top: 0.32in; ");
                sb.Append("  left:  6.12in;  ");
                sb.Append("  letter-spacing: 11px; ");
                sb.Append(" } ");

                sb.Append(" </style> ");
                sb.Append("  <div class='divcss'>");
                if (AcPayee == "Yes")
                {
                    sb.Append("  <div class='Linecss'></div>");
                }

                sb.Append("<div class='Datecss'>" + InstrumentDate + "</div>");
                sb.Append(" <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.33in; display: block;'>&nbsp;</span>");

                //sb.Append("  <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.33in; display: block;'>2&nbsp;&nbsp;&nbsp;9&nbsp;-&nbsp;0&nbsp;&nbsp;7&nbsp;-&nbsp;2&nbsp;&nbsp;&nbsp;0&nbsp;&nbsp;&nbsp;2&nbsp;&nbsp;&nbsp;2</span>");
                sb.Append("  <span style='padding-left: 0.8in; padding-right: 2.8in; display: block; font-size: 13px; line-height: 2.5; '>&nbsp;</span>");
                sb.Append("  <span class='partycss'>" + BeneficiaryName + "</span>");
                sb.Append("  <span style='padding-left: 0.5in; padding-right: 0.29in; display: block; font-size: 13px; line-height: 2;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + GenerateWordsinRs(Amount) + " </span>");
                sb.Append("  <span class='amtcss'>&nbsp;&nbsp;" + Amount + "</span>");
                sb.Append("  </div>");

                divCheque.InnerHtml = sb.ToString();
                // spnAmount.InnerHtml = GenerateWordsinRs(lblNetAmountdue.Text.ToString());


            }
            else if (Type == "SBI")
            {
                sb.Append(" <style>");
                sb.Append(" body {  ");
                sb.Append(" margin: 0mm 0mm 0mm 0mm;");
                sb.Append(" height: 3.67in; ");
                sb.Append(" width: 8.00in; ");
                sb.Append(" } ");
                sb.Append(" page { ");
                sb.Append(" height: 3.67in; ");
                sb.Append(" width:8.00in; ");
                sb.Append(" } ");
                sb.Append(" .divcss { ");
                sb.Append(" position:fixed;");
                sb.Append("  left:0; ");
                sb.Append("  top:0;  ");
                sb.Append("  height: 3.67in;  ");
                sb.Append("  width: 8.00in;  ");
                sb.Append("  } .amtcss { ");
                sb.Append("  position: fixed; ");
                sb.Append("  top: 1.45in; ");
                sb.Append("  left: 6.1in; ");
                sb.Append("  }");
                sb.Append("  .partycss { ");
                sb.Append("  position: fixed; ");
                sb.Append("  top: 0.83in; ");
                sb.Append("  left: 0.8in; ");
                sb.Append("  font-size:13px; ");
                sb.Append(" } ");
                if (AcPayee == "Yes")
                {
                    sb.Append(" .Linecss{ ");
                    sb.Append("  position: fixed; ");
                    sb.Append("  top: 0.3in; ");
                    sb.Append("  left: 0in; ");
                    sb.Append("  width:120px; ");
                    sb.Append("  height:3px; ");
                    sb.Append("  border-top:1px solid black; ");
                    sb.Append("  border-bottom:1px solid black; ");
                    sb.Append("  transform: rotate(150deg); ");
                    sb.Append("  } ");
                }

                sb.Append(" .Datecss{ ");
                sb.Append("  position: fixed; ");
                sb.Append("  top: 0.33in; ");
                sb.Append("  left:  6.1in;  ");
                sb.Append("  letter-spacing: 11px; ");
                sb.Append(" } ");

                sb.Append(" </style> ");
                sb.Append("  <div class='divcss'>");
                if (AcPayee == "Yes")
                {
                    sb.Append("  <div class='Linecss'></div>");
                }

                sb.Append("<div class='Datecss'>" + InstrumentDate + "</div>");
                sb.Append(" <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.22in; display: block;'>&nbsp;</span>");

                //sb.Append("  <span style='padding-left: 6.19in; padding-top: 0.31in; padding-bottom: 0.33in; display: block;'>2&nbsp;&nbsp;&nbsp;9&nbsp;-&nbsp;0&nbsp;&nbsp;7&nbsp;-&nbsp;2&nbsp;&nbsp;&nbsp;0&nbsp;&nbsp;&nbsp;2&nbsp;&nbsp;&nbsp;2</span>");
                sb.Append("  <span style='padding-left: 0.8in; padding-right: 2.8in; display: block; font-size: 13px; line-height: 2.5; '>&nbsp;</span>");
                sb.Append("  <span class='partycss'>" + BeneficiaryName + "</span>");
                sb.Append("  <span style='padding-left: 0.5in; padding-right: 0.29in; display: block; font-size: 13px; line-height: 2;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + GenerateWordsinRs(Amount) + " </span>");
                sb.Append("  <span class='amtcss'>&nbsp;&nbsp;" + Amount + "</span>");
                sb.Append("  </div>");

                divCheque.InnerHtml = sb.ToString();
                // spnAmount.InnerHtml = GenerateWordsinRs(lblNetAmountdue.Text.ToString());


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
            b = " And " + strwords2 + " Paise Only ";
        }

        return a + b;
    }
}