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

public partial class mis_Finance_RptMultiAccountPrinting : System.Web.UI.Page
{
    DataSet ds, ds2;
    StringBuilder htmlStr = new StringBuilder();
    AbstApiDBApi objdb = new APIProcedure();
    CultureInfo cult = new CultureInfo("gu-IN", true);
    NUMBERSTOWORDS NUMBERTOWORDS = new NUMBERSTOWORDS();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["Emp_ID"] != null)
            {
                if (!IsPostBack)
                {
                    ViewState["Emp_ID"] = Session["Emp_ID"].ToString();
                    ViewState["Office_ID"] = Session["Office_ID"].ToString();
                    //FillFromDate();


                    txtFromDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("dd/MM/yyyy");

                    FillDropdown();

                    ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
                    ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
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
    protected void FillFromDate()
    {
        try
        {
            ds = null;
            string firstDateOfYear = "";
            ds = objdb.ByProcedure("SpFinVoucherDate", new string[] { "flag", "Office_ID" }, new string[] { "2", ViewState["Office_ID"].ToString() }, "dataset");
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                firstDateOfYear = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }

            String sDate = DateTime.Now.ToString();
            DateTime datevalue = (Convert.ToDateTime(Convert.ToDateTime(firstDateOfYear, cult).ToString("yyyy/MM/dd")));
            int mn = datevalue.Month;
            int yy = datevalue.Year;
            if (mn < 4)
            {
                txtFromDate.Text = "01/04/" + (yy - 1).ToString();
                //txtToDate.Text = "01/04/" + (yy - 1).ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();

            }
            else
            {
                txtFromDate.Text = "01/04/" + (yy).ToString();
                //txtToDate.Text = "01/04/" + (yy).ToString();
                txtToDate.Text = ds.Tables[0].Rows[0]["VoucherDate"].ToString();
            }

        }
        catch (Exception ex)
        {
            //lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void FillDropdown()
    {
        try
        {
            ds = objdb.ByProcedure("SpFinRptMultiAccountPrinting",
                   new string[] { "flag" },
                   new string[] { "0" }, "dataset");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ddlVoucherType.DataSource = ds;
                ddlVoucherType.DataTextField = "VoucherType";
                ddlVoucherType.DataValueField = "VoucherType";
                ddlVoucherType.DataBind();
                //ddlOffice.Items.Insert(0, new ListItem("All", "0"));
                ddlVoucherType.SelectedValue = ViewState["Office_ID"].ToString();
            }

        }
        catch (Exception ex)
        {
            // lblMsg.Text = objdb.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());

        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {

        if (txtFromDate.Text != "" && txtToDate.Text != "")
        {
            ViewState["ToDate"] = Convert.ToDateTime(txtToDate.Text, cult).ToString("yyyy/MM/dd");
            ViewState["FromDate"] = Convert.ToDateTime(txtFromDate.Text, cult).ToString("yyyy/MM/dd");
            fill_details();
        }

    }

    private void fill_details()
    {
        lblpagenote.Text = "";
        div_page_content.InnerHtml = "";
        string VoucherType = "";
        
        int totalListItem = ddlVoucherType.Items.Count;
        foreach (ListItem item in ddlVoucherType.Items)
        {
            if (item.Selected)
            {
                VoucherType += item.Value + ",";
            }
        }


        ds2 = objdb.ByProcedure("SpFinRptMultiAccountPrinting",
                   new string[] { "flag", "Office_ID", "ToDate", "FromDate", "VoucherTx_Type" },
                   new string[] { "1", ViewState["Office_ID"].ToString(), ViewState["ToDate"].ToString(), ViewState["FromDate"].ToString(), VoucherType }, "dataset");
        
        if (ds2.Tables.Count > 0 && ds2.Tables[1].Rows.Count > 0)
        {
            string PageNote = "<p><b style='color: #607D8B;font-size: 15px;'> Total vouchers between seleted date range: </b></p>";

            int VCount = ds2.Tables[1].Rows.Count;
            for (int VLoop = 0; VLoop < VCount; VLoop++)
            {
                PageNote += "<p class='note_main'><b class='note_key'>" + ds2.Tables[1].Rows[VLoop]["VoucherTx_Type"].ToString() + " : </b>" + ds2.Tables[1].Rows[VLoop]["VoucherTxCount"].ToString() + "</p>";
            }

            PageNote += "<br/><p><button class='btn btn-flat btn-primary' onclick='window.print()'>Print</button></p><br/>";
            lblpagenote.Text = PageNote;

        }

        if (ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
        {

            /******************Add Voucher Count********************/

            

            /*******************************************************/

            //htmlStr.Append("<table  id='DetailGrid' class='datatable table table-bordered table-hover GridView2' style='font-family:verdana; font-size:12px; width:100%'>");
            //htmlStr.Append("<thead>");
            //htmlStr.Append("<tr>");
            //htmlStr.Append("<td>Voucher Amount</td>");
            //htmlStr.Append("<td>Voucher Date</td>");
            //htmlStr.Append("</tr>");
            //htmlStr.Append("</thead>");
            //htmlStr.Append("<tbody>");
            int RCount = ds2.Tables[0].Rows.Count;
            for (int i = 0; i < RCount; i++)
            {
                //htmlStr.Append("<tr>");
                //htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Amount"].ToString() + "</td>");
                //htmlStr.Append("<td>" + ds.Tables[0].Rows[i]["VoucherTx_Date"].ToString() + "</td>");
                //htmlStr.Append("</tr>");

                if (ds2.Tables[0].Rows[i]["VoucherTx_Type"].ToString() == "Journal" ||ds2.Tables[0].Rows[i]["VoucherTx_Type"].ToString() == "Journal HO" || ds2.Tables[0].Rows[i]["VoucherTx_Type"].ToString() == "Bank Receipt" || ds2.Tables[0].Rows[i]["VoucherTx_Type"].ToString() == "Receipt")
                {
                    fill_JournalHo(int.Parse(ds2.Tables[0].Rows[i]["VoucherTx_ID"].ToString()));

                }

                if (ds2.Tables[0].Rows[i]["VoucherTx_Type"].ToString() == "Payment"|| ds2.Tables[0].Rows[i]["VoucherTx_Type"].ToString() == "GSTService Purchase")
                {
                    fill_PaymentContra(int.Parse(ds2.Tables[0].Rows[i]["VoucherTx_ID"].ToString()));
                }
            }


            
            //htmlStr.Append("</tbody>");
            //htmlStr.Append("</table>");
            

        }
        else
        {
            htmlStr.Append("<p><b style='color: blue;font-size: 15px;'> Voucher Not Found </b></p>");
        }

        div_page_content.InnerHtml = htmlStr.ToString();
    }

    private void fill_PaymentContra(int VoucherTx_ID)
    {
        string VoucherTx_Amount = "";
        string Amount = "";
        ds = objdb.ByProcedure("SpFinPrintVoucher", new string[] { "flag", "VoucherTx_ID", "Office_ID" }, new string[] { "2", VoucherTx_ID.ToString(), ViewState["Office_ID"].ToString() }, "dataset");

        if (ds != null)
        {
            VoucherTx_Amount = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
            Amount = GenerateWordsinRs(VoucherTx_Amount);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //htmlStr.Append("<div class='invoice p-3 mb-3'>");
                htmlStr.Append("<div class='row'>");
                htmlStr.Append("<div class='col-12'>");
                htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:20px;'>THE M.P STATE AGRO INDUSTRIES DEVELOPMENT CORPORATION LTD.<br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</span><br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() + "  VOUCHER</span> </h2>");

                htmlStr.Append("</div>");
                htmlStr.Append("</div>");
                //htmlStr.Append("<div class='invoice p-3 mb-3'>");
                htmlStr.Append("<div class='row'>");
                htmlStr.Append("<div class='col-12'>");
                htmlStr.Append("<label class='lead'>Voucher No&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_No"].ToString() + "</span></label><label class='float-right lead'>Date:&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString() + "</span></label>");
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
                if (ds.Tables[4].Rows.Count > 0)
                {

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td class='lead' style='width:10px'>PAY TO&nbsp;&nbsp;<span class='small'>" + ds.Tables[4].Rows[0]["SupplierName"].ToString() + "</span></td>");
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
                htmlStr.Append("</div><p style='page-break-before: always'>");
            }
        }

    }
    private void fill_JournalHo(int VoucherTx_ID)
    {
        //htmlStr.Append(VoucherTx_ID);
        string VoucherTx_Amount = "";
        string Amount = "";
        ds = objdb.ByProcedure("SpFinPrintVoucher", new string[] { "flag", "VoucherTx_ID", "Office_ID" }, new string[] { "1", VoucherTx_ID.ToString(), ViewState["Office_ID"].ToString() }, "dataset");

        if (ds != null)
        {
            VoucherTx_Amount = ds.Tables[0].Rows[0]["VoucherTx_Amount"].ToString();
            Amount = GenerateWordsinRs(VoucherTx_Amount);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //htmlStr.Append("<div class='invoice p-3 mb-3'>");
                htmlStr.Append("<div class='row'>");
                htmlStr.Append("<div class='col-12'>");
                htmlStr.Append("<h2 class='text-center' style='font-weight:800; font-size:20px;'>THE M.P STATE AGRO INDUSTRIES DEVELOPMENT CORPORATION LTD.<br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["Office_Name"].ToString() + "</span><br /><span style='font-size:17px;'>" + ds.Tables[0].Rows[0]["VoucherTx_Type"].ToString() + "  VOUCHER</span> </h2>");

                htmlStr.Append("</div>");
                htmlStr.Append("</div>");
                htmlStr.Append("<div class=''>");
                htmlStr.Append("<div class='row'>");
                htmlStr.Append("<div class='col-12'>");
                htmlStr.Append("<label class='lead'>Voucher No&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_No"].ToString() + "</span></label><label class='float-right lead'>Date:&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_Date"].ToString() + "</span></label>");
                htmlStr.Append("</div>");
                htmlStr.Append("</div>");
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                htmlStr.Append(" <div class='row'>");
                htmlStr.Append("<div class='col-12 table-responsive'>");
                htmlStr.Append(" <table class='table'>");
                htmlStr.Append("<thead>");
                htmlStr.Append("<tr >");
                htmlStr.Append("<th class='text-center lead' style='border-bottom:1px solid black'>PARTICLARS</th>");
                htmlStr.Append("<th class='text-center lead' style='border-bottom:1px solid black'>DEBIT</th>");
                htmlStr.Append("<th class='text-center lead' style='border-bottom:1px solid black'>CREDIT</th>");
                htmlStr.Append("</tr>");
                htmlStr.Append("</thead>");
                htmlStr.Append("<tbody>");

                int count = ds.Tables[1].Rows.Count;

                for (int i = 0; i < count; i++)
                {

                    htmlStr.Append("<tr>");
                    htmlStr.Append("<td class='text-center small'>" + ds.Tables[1].Rows[i]["Ledger_Name"].ToString() + "</td>");
                    htmlStr.Append("<td class='text-center small'>" + ds.Tables[1].Rows[i]["LedgerTx_Debit"].ToString() + "</td>");
                    htmlStr.Append("<td class='text-center small'>" + ds.Tables[1].Rows[i]["LedgerTx_Credit"].ToString() + "</td>");
                    htmlStr.Append("</tr>");
                }
                htmlStr.Append("<tr>");
                htmlStr.Append("<td align='right' class='lead' >TOTAL</td>");
                htmlStr.Append("<td class='text-center small'>" + VoucherTx_Amount + "</td>");
                htmlStr.Append("<td class='text-center small'>" + VoucherTx_Amount + "</td>");
                htmlStr.Append("</tr>");


                htmlStr.Append("</tbody>");
                htmlStr.Append("</table>");
                htmlStr.Append("</div>");
                htmlStr.Append("</div>");
            }
        }



        htmlStr.Append("<div class='row'>");
        htmlStr.Append("<div class='col-12'>");
        htmlStr.Append("<label class='lead'>NARRATION:&nbsp;&nbsp;&nbsp;<span class='small'>" + ds.Tables[0].Rows[0]["VoucherTx_Narration"].ToString() + "</span></label>");
        htmlStr.Append("</div>");
        htmlStr.Append("</div>");
        htmlStr.Append("<div class='row'>");
        htmlStr.Append("<div class='col-12'>");
        htmlStr.Append("<label class='lead'>AMOUNT (IN WORDS):&nbsp;&nbsp;&nbsp;<span class='small'>" + Amount + "</span></label>");
        htmlStr.Append("</div>");
        htmlStr.Append("</div>");
        htmlStr.Append("<div class='invoice p-3 mb-3'>");
        htmlStr.Append("<div class='row' style='padding-top:80px;'>");
        htmlStr.Append("<div class='col-12'>");
        htmlStr.Append("<span class='lead' style='padding-right:50px;'>Accountant/Asstt.Manager (Account)</span><span class='lead' style='text-center'>Manager (Accounts)</span><span class='float-right lead'>CM/DGM (Accounts)/GM</span>");
        htmlStr.Append("</div>");
        htmlStr.Append("</div>");
        htmlStr.Append("</div><p style='page-break-before: always'>");
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