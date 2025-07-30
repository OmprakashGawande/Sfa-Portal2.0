using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Text;

public partial class mis_Finance_VoucherSalepurchaseInvocie : System.Web.UI.Page
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
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'><b>" + ds.Tables[2].Rows[i]["Rate"].ToString() + "</b></td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'><b>" + ds.Tables[2].Rows[i]["Quantity"].ToString() + " " + ds.Tables[2].Rows[i]["UnitName"].ToString() + "</b></td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'><b>" + ds.Tables[2].Rows[i]["Amount"].ToString() + "</b></td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["PER"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["CGSTAmt"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black; border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["SGSTAmt"].ToString() + "</td>");
                        sb.Append("<td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:right; line-height:1.1'>" + ds.Tables[2].Rows[i]["TOTALAMOUNT"].ToString() + "</td>");
                        sb.Append("</tr>");


                        /********Sub Items**********/
                       DataSet ds_subitem = objdb.ByProcedure("SpFinSalePurchasePrint", new string[] { "flag", "VoucherTx_ID", "Item_id" }, new string[] { "1", ViewState["VoucherTx_ID"].ToString(), ds.Tables[2].Rows[i]["item_id"].ToString() }, "dataset");
                        if (ds_subitem != null)
                        {
                            if (ds_subitem.Tables[0].Rows.Count > 0)
                            {
                                int subitemCount = ds_subitem.Tables[0].Rows.Count;
                                for (int j = 0; j < subitemCount; j++)
                                {

                                    sb.Append("<tr style='font-style:italic; font-weight:10px;'>");
                                    sb.Append("<td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:right; line-height:1'></td>");
                                    sb.Append("<td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:left; line-height:1'>&nbsp;&nbsp;&nbsp;-" + ds_subitem.Tables[0].Rows[j]["Ingredient_Name"].ToString() + " (" + ds_subitem.Tables[0].Rows[j]["Ingredient_Size"].ToString() + ")</td>");
                                    sb.Append("<td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:right; line-height:1'></td>");
                                    sb.Append("<td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:right; line-height:1'>" + ds_subitem.Tables[0].Rows[j]["Rate"].ToString() + "</td>");
                                    sb.Append("<td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:right; line-height:1'>" + ds_subitem.Tables[0].Rows[j]["Quantity"].ToString() + " " + ds_subitem.Tables[0].Rows[j]["Ingredient_Unit"].ToString() + "</td>");
                                    sb.Append("<td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:right; line-height:1'>" + ds_subitem.Tables[0].Rows[j]["Amount"].ToString() + "</td>");
                                    sb.Append("<td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:right; line-height:1'></td>");
                                    sb.Append("<td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:right; line-height:1'></td>");
                                    sb.Append("<td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:right; line-height:1'></td>");
                                    sb.Append("<td class='PSemibold' style='border-left:1px solid black;  border-right:1px solid black; text-align:right; line-height:1'></td>");
                                    sb.Append("</tr>");
                                }
                            }
                        }
                        /************************/

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
                    for (int i = 0; i < DebtorCount;i ++ )
                    {
                        if (ds.Tables[1].Rows[i]["Mailing_Address"].ToString() != "")
                        {
                            HTML += ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "<br>" + ds.Tables[1].Rows[i]["Mailing_Address"].ToString() + "<br>" + "<b>GSTIN</b>" + " " + ds.Tables[1].Rows[i]["GST_No"].ToString() + "<br><br>";
                        }
                        else
                        {
                            HTML += ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "<br>" + "<b>GSTIN</b>" + " " + ds.Tables[1].Rows[i]["GST_No"].ToString() + "<br><br>";
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
               sb.Append("<td class='cssborder Pbold' colspan='5' style='text-align:center'>" + CRAmount.ToString() + "</td>"); 
               sb.Append("</tr>");
               divitem.InnerHtml = sb.ToString();


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